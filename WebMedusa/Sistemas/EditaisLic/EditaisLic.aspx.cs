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
using System.Text;

namespace Medusa.Sistemas.EditaisLic
{
    public partial class EditaisLic : PageCrud<EditalLicBLL>
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_edital_lic";
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
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_edital_lic);


            this.txtContent.Text = ObjBLL.ObjEF.descricao;
            this.cTextoTitulo.Text = ObjBLL.ObjEF.titulo;
            this.cData1.Value = ObjBLL.ObjEF.data;
            this.DdlStatusEditaisLic1.Id_status_edital_lic = ObjBLL.ObjEF.id_status_edital_lic;

            ControleEditaisLicAnexo1.Id_edital_lic = ObjBLL.ObjEF.id_edital_lic;
            ControleEditaisLicAnexo1.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_edital_lic = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.descricao = this.txtContent.Text;
            ObjBLL.ObjEF.titulo = this.cTextoTitulo.Text;
            ObjBLL.ObjEF.data = Convert.ToDateTime(this.cData1.Value);
            ObjBLL.ObjEF.id_status_edital_lic = this.DdlStatusEditaisLic1.Id_status_edital_lic;

        }

        protected override void SetAdd()
        {
            panelAnexos.Visible = false;
            base.SetAdd();
        }

        protected override void SetModify()
        {
            panelAnexos.Visible = true;
            base.SetModify();
        }

        protected override void SetView()
        {
            panelAnexos.Visible = false;
            base.SetView();
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

        protected void btInserir0_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                PkValue = ObjBLL.ObjEF.id_edital_lic;
                ObjBLL.Detach();
                Get();
                SetModify();
            }
            else
                msgError("erro inclusão");
        }
    }

}