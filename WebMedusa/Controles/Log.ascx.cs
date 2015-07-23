using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Controles
{
    public partial class Log : ControlCrud<LogSistemaBLL>
    {
        public int Id_entidade
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_entidade = 0;
                return (int)ViewState[ID];
            }
            set { ViewState[ID] = value; }
        }

        public Type TEntidade
        {
            get
            {
                if (ViewState["type" + ID] == null)
                    TEntidade = typeof(int);
                return (Type)ViewState["type" + ID];
            }
            set { ViewState["type" + ID] = value; }
        }

        public bool PermitePesquisa
        {
            get
            {
                return dPesquisa.Visible;
            }
            set { dPesquisa.Visible = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_log";
            //valor da chave primária
            PkValue = 0;
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
            //
            _btProcurar = btSearch;
            _ckFilter = ckFilter;
            _dataListFiltros = DataListFiltros;
            //

            if (!IsPostBack)
            {
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "DESC";
                this.ddlOptions_SelectedIndexChanged(null, null);
            }
        }

        protected override void Get()
        {
           
        }

        protected override void Set()
        {

        }

        protected override void SetGrid()
        {
            Filter f1 = new Filter()
            {
                property = "id_entidade",
                value = Convert.ToString(Id_entidade),
                mode = "=="
            };
            filtros.Add(f1);

            int index = TEntidade.Name.LastIndexOf("_");
                string strEntidade = index == -1 ? TEntidade.Name : TEntidade.Name.Substring(0, index);

            Filter f2 = new Filter()
            {
                property = "entidade",
                value = strEntidade,
                mode = "Equals"
            };
            filtros.Add(f2);

            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0);
            _grid.DataBind();
            filtros.Remove(f1);
            filtros.Remove(f2);
        }

        public override void DataBind()
        {
            SetGrid();
            SetView();
            base.DataBind();
        }
    }
}