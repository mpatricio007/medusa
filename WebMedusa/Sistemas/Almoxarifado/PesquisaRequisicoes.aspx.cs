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
using Medusa.Controles;
using Medusa.Relatorios.Almoxarifado;
using Microsoft.Reporting.WebForms;

namespace Medusa.Sistemas.Almoxarifado
{
    public partial class PesquisaRequisicoes : PageCrud<RequisicaoBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_requisicao";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = new Button();
            _btExcluir = new Button();
            _btExcluir0 = new Button();
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
            this.lblData.Text = Util.DateToString(ObjBLL.ObjEF.data);
            this.txtUsuario.Text = ObjBLL.ObjEF.Usuario.PessoaFisica.nome;
            SetGridMateriais();

        }

        protected override void SetGrid()
        {
            if (ddlSize.SelectedValue == "0")
                grid.PageSize = 50;
            else
                grid.PageSize = Convert.ToInt32(ddlSize.SelectedValue);

            int size = 10 * Convert.ToInt32(this.ddlSize.SelectedValue);
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.FindEnviados(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size).Where(it => it.Status == rdStatus.SelectedValue).ToList();
            grid.DataBind();
        }

        public void SetGridMateriais()
        {
            gridMateriais.DataKeyNames = new string[] { "id_requisicao_material" };
            gridMateriais.DataSource = ObjBLL.ObjEF.RequisicaoMateriais.ToList();
            gridMateriais.DataBind();
        }

        protected override void Set()
        {
           
        }

        protected void gridMateriais_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var panel = (Panel)gridMateriais.Rows[e.NewSelectedIndex].FindControl("pHistorico");
            panel.Visible = !panel.Visible;
            var cControleHistoricoRequisicao1 = (ControleHistoricoRequisicao)gridMateriais.Rows[e.NewSelectedIndex].FindControl("cControleHistoricoRequisicao1");

            var imgButton = (ImageButton)gridMateriais.Rows[e.NewSelectedIndex].FindControl("imgAdd");
            imgButton.ImageUrl = panel.Visible ? "../../Shared/Images/minus.png" : "../../Shared/Images/plus.png";

            var rm = new RequisicaoMaterialBLL();
            rm.Get(Util.StringToInteiro(gridMateriais.DataKeys[e.NewSelectedIndex]["id_requisicao_material"].ToString()).GetValueOrDefault());

            cControleHistoricoRequisicao1.Id_requisicao_material = rm.ObjEF.id_requisicao_material;
            cControleHistoricoRequisicao1.DataBind();

            var DdlStatusRequisicaoMaterial1 = (DdlStatusRequisicaoMaterial)cControleHistoricoRequisicao1.FindControl("DdlStatusRequisicaoMaterial1");
            DdlStatusRequisicaoMaterial1.DataBind();

            e.Cancel = true;
        }

        protected override void SetView()
        {
            base.SetView();
            //ControleHistoricoRequisicao1.Id_requisicao_material = 0;
        }

        protected void gridMateriais_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var rmBLL = new RequisicaoMaterialBLL();
            rmBLL.Get(Convert.ToInt32(gridMateriais.DataKeys[e.NewEditIndex]["id_requisicao_material"]));
            rmBLL.DesfazerAtendimento();
        }
        
        protected void SetPrint()
        {
            pGrid.Visible = false;
            pCadastro.Visible = false;
        }
    }
}