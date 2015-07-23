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
    public partial class Financiadores : PageCrud<FinanciadorBLL>
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_financiador";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_financiador);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cCNPJ1.Value = ObjBLL.ObjEF.cnpj;
            this.cDdlNatureza1.Id_natureza = ObjBLL.ObjEF.id_natureza;
            
            this.cListaEmails1.Value = ObjBLL.ObjEF.Emails.ToList().ConvertToListEmail();
            this.cListaEmails1.DataBind();

            this.cListaTelefones1.Value = ObjBLL.ObjEF.Telefones.ToList().ConvertToListTelefone();
            this.cListaTelefones1.DataBind();

            this.cEnder1.Value = ObjBLL.ObjEF.ender;
            this.AsyncFileUpload1.Visible = false;
            this.spanUploading.Visible = false;
            this.lbArquivo.Text = ObjBLL.ObjEF.logotipo;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_financiador = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.cnpj = this.cCNPJ1.Value;
            ObjBLL.ObjEF.id_natureza = this.cDdlNatureza1.Id_natureza;
            ObjBLL.ObjEF.Emails = this.cListaEmails1.Value.ConvertToListFinanciadorEmail();
            ObjBLL.ObjEF.Telefones = this.cListaTelefones1.Value.ConvertToListFinanciadorTelefone();
            ObjBLL.ObjEF.ender = this.cEnder1.Value;

            ObjBLL.Up.file = (HttpPostedFile)Session["file"];
            Session.Remove("file");
        }

        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            Session["file"] = AsyncFileUpload1.PostedFile;
        } 

        protected void cDdlNatureza1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.cCNPJ1.EnableValidator = (cDdlNatureza1.Id_natureza != 5);
        }

        protected void lkAddFile_Click(object sender, EventArgs e)
        {
            this.AsyncFileUpload1.Visible = true;
            this.spanUploading.Visible = true;
        }
    }
}