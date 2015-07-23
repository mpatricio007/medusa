using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class Ceps : PagePesquisa<CepBLL>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_cep";        
            // painel do grid
            pGrid = panelGrid;  
            // gridview
            _grid = grid;
            lbMsg = new Label();           
            _ddlSize = ddlSize;
            _ddlMode = ddlMode;
            _ddlOptions = ddlOptions;
            _txtProcura = txtProcura;
            //instanciar para fucnionar os filtro múltiplos
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