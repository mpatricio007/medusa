using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;
using System.Text;
using System.IO;

namespace Medusa.Sistemas.EditaisLic
{
    public partial class ControleEditaisLicAnexo : ControlCrud<EditalLicAnexoBLL>
    {
        public int Id_edital_lic
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_edital_lic = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_edital_lic_anexo";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = new Panel();
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = btInserir0;
            _btInserir0 = new Button();
            _btExcluir = new Button();
            _btExcluir0 = new Button();
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            if (!IsPostBack)
            {
                grid.DataBind();
                SetGrid();

       
           
            }
        }


        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_edital_lic_anexo);
            this.cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            this.cIntOrdem.Value = ObjBLL.ObjEF.ordem;
            this.spanUploading.Visible = false;
            this.RbLoginObrigatorio.SelectedValue = Convert.ToString(ObjBLL.ObjEF.login_obrigatorio);
            lkArquivo.Text = ObjBLL.ObjEF.descricao;

        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_edital_lic_anexo = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.descricao = this.cTextoDescricao.Text;
            ObjBLL.ObjEF.ordem = this.cIntOrdem.Value.GetValueOrDefault();
            ObjBLL.ObjEF.id_edital_lic = Id_edital_lic; 
            ObjBLL.ObjEF.login_obrigatorio = Convert.ToBoolean(this.RbLoginObrigatorio.SelectedValue);

            ObjBLL.arqBLL.bytes = (byte[])Session["file"];
            ObjBLL.arqBLL.contentType = Convert.ToString(Session["content"]);
            Session.Remove("file");
            Session.Remove("content");

        }

        protected override void SetAdd()
        {
            btInserir0.Visible = true;
            btCancelar0.Visible = true;
            pArquivo.Visible = false;
        }

        protected override void SetModify()
        {
            btInserir0.Visible = false;
            btCancelar0.Visible = true;
            pArquivo.Visible = true;
        }

        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        { 
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(AsyncFileUpload1.PostedFile.InputStream))            
                bytes = br.ReadBytes((Int32)AsyncFileUpload1.PostedFile.InputStream.Length);
            
            Session["file"] = bytes;
            Session["content"] = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
        }

        protected override void SetGrid()
        {
            ObjBLL.Detach();
            var filtros = new List<Filter>();
            filtros.Add(new Filter()
            {
                property = "id_edital_lic",
                value = Convert.ToString(Id_edital_lic),
                mode = "=="
            });
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(filtros,
               "ordem",
               "ASC", 0);
            grid.DataBind();
        }

        public override void DataBind()
        {
            Get();
            SetAdd();
            SetGrid();
            base.DataBind();
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            PkValue = 0;
            Get();
            SetAdd();
            SetGrid();
        }
        
        protected override void btInserir_Click(object sender, EventArgs e)
        {            
            Set();
            string strMsg = String.Empty;
            if (ObjBLL.DataIsValid(ref strMsg))
            {

                if (ObjBLL.SalvarAnexo())
                {
                    ObjBLL.Detach();
                    ObjBLL.ObjEF.id_edital_lic_anexo = 0;
                    Get();
                    SetAdd();
                    SetGrid();
                }
                else
                    msgError("erro inclusão");
            }
            else
                msgError(strMsg);
        }

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ObjBLL.ExcluirAnexo(Convert.ToInt32(grid.DataKeys[e.RowIndex][PRIMARY_KEY]));          
            SetGrid();
        }

        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {            
            PkValue = Convert.ToInt32(grid.DataKeys[e.NewSelectedIndex][PRIMARY_KEY]);
            Get();
            SetModify();
            var scriptManager = ScriptManager.GetCurrent(Page);
            scriptManager.RegisterPostBackControl(lkArquivo);
            e.Cancel = true;
        }

        protected void lkArquivo_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.arqBLL.GetFile(ObjBLL.ObjEF.id_arquivo); 
        }
    }
}