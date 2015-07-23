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

namespace Medusa.Sistemas.RemessaBancaria
{
    public partial class PesquisaRemessas : PageCrud<RemessaBLL<Remessa>>
    {
        Dictionary<string, string> dicSearch = new Dictionary<string, string>()
        {
            {"status","id_tipo_ret"}
        };

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_remessa";
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

                var trBLL = new TipoRetornoBLL();
                listaStatus.DataSource = from tr in trBLL.GetAll().OfType<TipoRetorno>()
                    select new
                    {
                        tr.codigo,
                        tr.id_tipo_ret
                    };
                listaStatus.Items.Insert(0, new ListItem("selecione um status...", "0"));
                listaStatus.DataBind();
            }
        }

        protected override void Get()
        {
            
        }

        protected override void Set()
        {
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            int id_tipo_ret = 0;

            if (!_ckFilter.Checked)
                filtros.Clear();
            switch (ddlOptions.SelectedValue)
            {
                case ("status"):
                    {
                        int.TryParse(listaStatus.SelectedValue, out id_tipo_ret);
                        break;
                    }
                default:
                    {
                        if (!String.IsNullOrEmpty(this._txtProcura.Text))
                            filtros.Add(setFilter());
                        break;
                    }
            }

            SetView();


            if (_ddlSize.SelectedValue == "0")
                _grid.PageSize = 50;
            else
                _grid.PageSize = Convert.ToInt32(_ddlSize.SelectedValue);


            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size,
                id_tipo_ret);

            _grid.DataBind();
            DataListFiltros.DataBind();
            this._txtProcura.Text = String.Empty;
        }

        protected override void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlOptions.SelectedValue)
            {
                case ("status"):
                    {
                        GetModes(typeof(string));
                        listaStatus.Visible = true;
                        ddlMode.Visible = txtProcura.Visible = false;
                        break;
                    }
                default:
                    {
                        listaStatus.SelectedValue = "0";
                        listaStatus.Visible = false;
                        ddlMode.Visible = txtProcura.Visible = true;
                        base.ddlOptions_SelectedIndexChanged(sender, e);
                        break;
                    }
            }
        }
    }
}