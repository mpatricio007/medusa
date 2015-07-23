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


namespace Medusa.Controles.Pesquisa
{

    public partial class PesquisaOrigemEmail : PagePesquisa<OrigemDestinatariosEmailBLL>
    {

        public IQueryable<OrigemDestinatariosEmail> Retorno
        {
            get
            {
                return ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0).OfType<OrigemDestinatariosEmail>().AsQueryable();
            }
        }


         
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id";
            // painel do grid
            pGrid = panelGrid;
            // gridview
            _grid = grid;
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


    }
}