using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.SCFP
{
    public partial class vDespesasToDbfs : PageCrud<vDespesasToDbfBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_lancto";
            //valor da chave primária
            //PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = new Panel();
            // gridview
            _grid = grid;
            lbMsg = new Label();
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
           
        }

        protected override void Set()
        {
        }

        protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
            ObjBLL.ResponseToPage(Convert.ToInt32(_grid.DataKeys[e.NewEditIndex][PRIMARY_KEY]));

            _grid.DataBind();
            e.Cancel = true;
        }
    }
}