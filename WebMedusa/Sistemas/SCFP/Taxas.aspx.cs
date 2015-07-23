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

namespace Medusa.Sistemas.SCFP
{
    public partial class Taxas : PageCrud<TaxaBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_taxa";
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
            _btAlterar0 = new Button();
            _btInserir = btInserir;
            _btInserir0 = new Button();
            _btExcluir = btExcluir;
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
                cControleTabelas1.DataBind();
            }
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            trTabelas.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            trTabelas.Visible = true;
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_taxa);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            if(ObjBLL.Exists())
                this.cDdlPlanoConta1.Id_plano_conta = ObjBLL.ObjEF.PlanoConta.id_plano_conta;
            SetGridTabelas();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_taxa = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.id_plano_conta = this.cDdlPlanoConta1.Id_plano_conta;
        }

        protected void SetGridTabelas()
        {
            cControleTabelas1.Id_taxa = Convert.ToInt32(this.txtCodigo.Text);
            cControleTabelas1.DataBind();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            if (ObjBLL.DataIsValid())
            {
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    msg("inclusão efetuada");
                    ObjBLL.Detach();
                    PkValue = ObjBLL.ObjEF.id_taxa;
                    Get();
                    SetModify();
                }
                else
                    msgError("erro inclusão");
            }
            else
                msgError("erro inclusão");
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            base.btProcurar_Click(sender, e);
            cControleTabelas1.EditIndex = -1;
            cControleTabelas1.DataBind();
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            base.btCancelar_Click(sender, e);
            cControleTabelas1.EditIndex = -1;
            cControleTabelas1.DataBind();
        }

        #region grids
        //#region gridTabelasTabelasConfig
        //protected void gridTabelas_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (!e.Row.Equals(null) & e.Row.RowType.Equals(DataControlRowType.DataRow))
        //    {
        //        Panel panel = (Panel)e.Row.FindControl("pFaixas");
        //        if (panel.Visible = e.Row.RowIndex == GridTabelasEditIndex)
        //        {
        //            TabelaTaxasBLL ttBLL = new TabelaTaxasBLL();
        //            ttBLL.Get(Convert.ToInt32(gridTabelas.DataKeys[GridTabelasEditIndex]["id_tabela"]));
        //            var gridFaixas = (GridView)panel.FindControl("gridFaixas");
        //            gridFaixas.DataSource = ttBLL.ObjEF.faixas;
        //            gridFaixas.DataBind();
        //        }
        //    }
        //}

        //protected void gridTabelas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    ObjBLL.Get(PkValue);
        //    GridTabelasEditIndex = gridTabelas.EditIndex = -1;
        //    SetGridTabelas();
        //}

        //protected void gridTabelas_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    ObjBLL.Get(PkValue);
        //    GridTabelasEditIndex = grid.EditIndex = e.NewEditIndex;
        //    SetGridTabelas();
        //}

        //protected void imgViewFaixas_Click(object sender, ImageClickEventArgs e)
        //{
        //}

        //private void SetGridTabelas()
        //{
        //    gridTabelas.DataSource = ObjBLL.ObjEF.Tabelas;
        //    gridTabelas.DataBind();

        //    //cControleTabelas1.Id_taxa = Convert.ToInt32(this.txtCodigo.Text);
        //    //cControleTabelas1.DataBind();
        //}
        //#endregion

        //#region gridFaixasConfig
        //protected void gridFaixas_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    Panel panel = (Panel)gridTabelas.Rows[GridTabelasEditIndex].FindControl("pFaixas");
        //    TabelaTaxasBLL ttBLL = new TabelaTaxasBLL();
        //    ttBLL.Get(Convert.ToInt32(gridTabelas.DataKeys[GridTabelasEditIndex]["id_tabela"]));
        //    var gridFaixas = (GridView)panel.FindControl("gridFaixas");
        //    gridFaixas.EditIndex = GridFaixasEditIndex = e.NewEditIndex;
        //    gridFaixas.DataSource = ttBLL.ObjEF.faixas;
        //    gridFaixas.DataBind();
        //}

        //protected void gridFaixas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    Panel panel = (Panel)gridTabelas.Rows[GridTabelasEditIndex].FindControl("pFaixas");
        //    TabelaTaxasBLL ttBLL = new TabelaTaxasBLL();
        //    ttBLL.Get(Convert.ToInt32(gridTabelas.DataKeys[GridTabelasEditIndex]["id_tabela"]));
        //    var gridFaixas = (GridView)panel.FindControl("gridFaixas");
        //    gridFaixas.EditIndex = GridFaixasEditIndex = -1;
        //    gridFaixas.DataSource = ttBLL.ObjEF.faixas;
        //    gridFaixas.DataBind();
        //}

        //protected void SetGridFaixas()
        //{ 
        //}
        //#endregion
        #endregion
    }
}