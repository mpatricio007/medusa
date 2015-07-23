using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;
using System.Linq.Expressions;
using System.Reflection;

namespace Medusa.Sistemas.SISPS
{
    public partial class Editais : PageCrud<EditalBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_edital";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = btAlterar;
            _btAlterar0 = btAlterar0;
            _btInserir = btInserir;
            _btInserir0 = btInserir0;
            _btExcluir = btExcluir;
            _btExcluir0 = btExcluir0;
            _ddlSize = ddlSize;
            _ddlMode = ddlMode;
            _ddlOptions = ddlOptions;
            _txtProcura = txtProcura;
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;

            if (!IsPostBack)
            {

                base.Page_Load(sender, e);
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_edital);
            this.cTextoTitulo.Text = ObjBLL.ObjEF.titulo;
           //this.cTextoEdital_link.Text = ObjBLL.ObjEF.edital_link;
            this.cDdlVagas1.Id_vaga = ObjBLL.ObjEF.id_vaga;
            this.CheckStatus.Checked = ObjBLL.ObjEF.status;
            this.lbData.Text = Util.DateToString(ObjBLL.ObjEF.data_publicacao);
            this.AsyncFileUpload1.Visible = false;
            this.spanUploading.Visible = false;
            this.lbArquivo.Text = ObjBLL.ObjEF.edital_link;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_edital = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.titulo = this.cTextoTitulo.Text;
            //ObjBLL.ObjEF.edital_link = this.cTextoEdital_link.Text;
            ObjBLL.ObjEF.id_vaga = this.cDdlVagas1.Id_vaga;
            ObjBLL.ObjEF.status = this.CheckStatus.Checked;

            ObjBLL.up = new BLL.Upload();
            ObjBLL.up.file = (HttpPostedFile)Session["file"];
            Session.Remove("file");
        }

        protected void lkAddFile_Click(object sender, EventArgs e)
        {
            this.AsyncFileUpload1.Visible = true;
            this.spanUploading.Visible = true;
        
        }
        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            Session["file"] = AsyncFileUpload1.PostedFile;
        }




    }
}