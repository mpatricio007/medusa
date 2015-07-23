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

namespace Medusa.Sistemas.SREC
{
    public partial class CaixaDeSaida : PageCrud<EntradaBLL>
    {
        public int filterByTab
        {
            get
            {
                if (ViewState["filterByTab"] == null)
                    ViewState["filterByTab"] = 0;
                return Convert.ToInt32(ViewState["filterByTab"]);
            }
            set { ViewState["filterByTab"] = value; }
        }

        public List<StatusEntrada> lstStatusEntrada
        {
            get
            {
                var seBLL = new StatusEntradaBLL();
                return seBLL.GetAll().OfType<StatusEntrada>().ToList();
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_entrada";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = new Panel();
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
                this.ddlAno.DataSource = ObjBLL.GetAnos();
                this.ddlAno.DataBind();

                base.Page_Load(sender, e);
                StringBuilder str = new StringBuilder();
                foreach (var item in lstStatusEntrada.OrderBy(it => it.ordem))
                {
                    str.AppendFormat("<li id='{0}'><a href='#aba-1' class='aba-1'>{1}</a></li>", item.id_status_entrada, item.nome);
                }
                ulAbas.InnerHtml = str.ToString();
                filterByTab =  lstStatusEntrada.OrderBy(it => it.ordem).First().id_status_entrada;
            }
        }

        protected override void SetAdd()
        {
        }

        protected override void SetModify()
        {
        }

        protected override void SetView()
        {

        }

        protected override void Get()
        {
        }

        protected override void Set()
        {
        }

        protected override void SetGrid()
        {
            //base.SetGrid();
            if (ddlSize.SelectedValue == "0")
                grid.PageSize = 50;
            else
                grid.PageSize = Convert.ToInt32(ddlSize.SelectedValue);

            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);

            grid.DataKeyNames = new string[] { PRIMARY_KEY };

            grid.DataSource = ObjBLL.FindCaixaSaida(
               filtros,
               (string)ViewState["SortExpression"],
               (string)ViewState["SortDirection"], size, SecurityBLL.GetCurrentUsuario().id_usuario, filterByTab, Util.StringToInteiro(ddlAno.SelectedValue).GetValueOrDefault())
                    .OfType<Entrada>().ToList().OrderByDescending(it => it.EstadoAtual.id_historico_entrada).ToList();

            #region old
            //grid.DataSource = ObjBLL.Find(
             //  filtros,
             //  (string)ViewState["SortExpression"],
             //  (string)ViewState["SortDirection"], size).OfType<Entrada>().ToList()
             //       .Where(it => filterByTab == it.EstadoAtual.id_status_entrada
             //           && !it.EstadoAtual.DestinatariosEntrada.Select(d => d.UsuarioFusp.id_usuario).Contains(SecurityBLL.GetCurrentUsuario().id_usuario)
             //           && it.Historicos.Select(h => SecurityBLL.GetCurrentSetor(h.id_usuario_de.GetValueOrDefault()).id_setor)
             //               .Contains(SecurityBLL.GetCurrentSetor(SecurityBLL.GetCurrentUsuario().id_usuario).id_setor)
            //       ).ToList();
            #endregion

            grid.DataBind();
        }

        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var panel = (Panel)grid.Rows[e.NewSelectedIndex].FindControl("pHistorico");
            panel.Visible = !panel.Visible;

            var imgButton = (ImageButton)grid.Rows[e.NewSelectedIndex].FindControl("imgAdd");
            imgButton.ImageUrl = panel.Visible ? "../../Shared/images/minus.png" : "../../Shared/images/plus.png";

            var gridHistorico = (GridView)grid.Rows[e.NewSelectedIndex].FindControl("gridHistorico");
            ObjBLL.Get(Util.StringToInteiro(grid.DataKeys[e.NewSelectedIndex][PRIMARY_KEY].ToString()));

            if (ObjBLL.Exists())
                gridHistorico.DataSource = ObjBLL.ObjEF.Historicos.OrderByDescending(it => it.id_historico_entrada).ToList();
            gridHistorico.EmptyDataText = "historico indisponivel";
            gridHistorico.DataBind();

            e.Cancel = true;
        }

        protected void btTabSelect_Click(object sender, EventArgs e)
        {
            filterByTab = Util.StringToInteiro(txtTabIndex.Text).GetValueOrDefault();
            SetGrid();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGrid();
        }
    }
}