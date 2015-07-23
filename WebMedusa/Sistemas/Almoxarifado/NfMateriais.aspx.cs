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

namespace Medusa.Sistemas.Almoxarifado
{
    public partial class NfMateriais : PageCrud<NfMaterialBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_nf_material";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_nf_material);

            if (ObjBLL.ObjEF.MaterialNotaFiscais == null)
                ObjBLL.ObjEF.MaterialNotaFiscais = new List<MaterialNotaFiscal>();

            this.cData.Value = ObjBLL.ObjEF.data;
            this.cIntNf.Value = ObjBLL.ObjEF.numero;
            this.cValor.Value = ObjBLL.ObjEF.valor;
            this.cTextoEmpresa.Text = ObjBLL.ObjEF.nomeEmpresa;
            this.AsyncFileUpload1.Visible = false;
            this.spanUploading.Visible = false;
            this.lbArquivo.Text = ObjBLL.ObjEF.arquivo;
            this.cTextoObs.Text = ObjBLL.ObjEF.obs;

            ControleMaterialNf.Id_nf_material = ObjBLL.ObjEF.id_nf_material;
            ControleMaterialNf.Value = ObjBLL.ObjEF.MaterialNotaFiscais.ToList();
            ControleMaterialNf.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_nf_material = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.data = Convert.ToDateTime(this.cData.Value);
            ObjBLL.ObjEF.numero = Convert.ToInt32(this.cIntNf.Value);
            ObjBLL.ObjEF.valor = this.cValor.Value.GetValueOrDefault();
            ObjBLL.ObjEF.nomeEmpresa = this.cTextoEmpresa.Text;
            ObjBLL.ObjEF.obs = this.cTextoObs.Text;
            ObjBLL.up = new BLL.Upload();
            ObjBLL.up.file = (HttpPostedFile)Session["file"];
            Session.Remove("file");

            ObjBLL.oldMateriais = ObjBLL.ObjEF.MaterialNotaFiscais.ToList();
            ObjBLL.oldMateriais.ForEach(it => ObjBLL.ObjEF.MaterialNotaFiscais.Remove(it));

            ObjBLL.ObjEF.MaterialNotaFiscais = ControleMaterialNf.Value;
        }

        protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            Session["file"] = AsyncFileUpload1.PostedFile;
        }

        protected void lkAddFile_Click(object sender, EventArgs e)
        {
            this.AsyncFileUpload1.Visible = true;
            this.spanUploading.Visible = true;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                PkValue = ObjBLL.ObjEF.id_nf_material;
                ObjBLL.Detach();
                Get();
                SetModify();
            }
            else
                msgError("erro inclusão");
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
                msg("alteração efetuada");
            else
                msgError("erro alteração");
            Get();
        }
    }
}