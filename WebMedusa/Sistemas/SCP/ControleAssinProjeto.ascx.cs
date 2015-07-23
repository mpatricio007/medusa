using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleAssinProjeto : ControlCrud<AssinBLL>
    {
        public int Id_projeto
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_projeto = 0;
                return (int)ViewState[ID];
            }
            set { ViewState[ID] = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_assin";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            //_grid = grid_PageIndexChanging;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = btInserir0;
            _btExcluir = new Button();
            _btExcluir0 = new Button();
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            if(!IsPostBack)
            {
                Security();
            }
        }

        public void ExibeUltimoAssin()
        {
            string strMsg = String.Empty;
            if (ObjBLL.GetAssin(Id_projeto, ref strMsg))
                msg(strMsg);
            else
                msgError(strMsg);
            divAssin.InnerHtml = !String.IsNullOrEmpty(ObjBLL.ObjEF.nome_arquivo) ?
            String.Format("<iframe src=\"{0}\" width=\"100%\" height=\"1000px\"></iframe>", ObjBLL.ObjEF.nome_arquivo) :
            String.Empty;
            divAssin.DataBind();
            SetView();
        }

        protected override void Get()
        {
            lblMsg0.Text = String.Empty;
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_assin);
            cDataValidade.Value = ObjBLL.ObjEF.validade;
        }

        protected override void Set()
        {
            
                ObjBLL.ObjEF.id_projeto = Id_projeto;
                ObjBLL.ObjEF.id_assin = Convert.ToInt32(txtCodigo.Text);
                ObjBLL.ObjEF.validade = cDataValidade.Value.GetValueOrDefault();

                ObjBLL.up = new BLL.Upload();
                ObjBLL.up.file = (HttpPostedFile)Session["file"];
                Session.Remove("file");
          

        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            ExibeUltimoAssin();
        }

        public override void DataBind()
        {
            ExibeUltimoAssin();
            base.DataBind();
        }

        protected override void SetView()
        {
            base.SetView();
        }
       
        protected override void btInserir_Click(object sender, EventArgs e)
        {
            var file = (HttpPostedFile)Session["file"];
            if (file.ContentType == "application/pdf")
            {
                base.btInserir_Click(sender, e);
                ExibeUltimoAssin();
            }
            else
                lblMsg0.Text = "Somente arquivos em pdf!";
        }

        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            Session["file"] = AsyncFileUpload1.PostedFile;
        }
    }
}