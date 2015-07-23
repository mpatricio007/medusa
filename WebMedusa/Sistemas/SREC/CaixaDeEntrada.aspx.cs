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
    public partial class CaixaDeEntrada : PageCrud<EntradaBLL>
    {
        public List<int> lstSelected
        {
            get
            {
                if (ViewState["lstSelected"] == null)
                    ViewState["lstSelected"] = new List<int>();
                return (List<int>)ViewState["lstSelected"];
            }
            set { ViewState["lstSelected"] = value; }
        }

        public int filterByTab
        {
            get
            {
                if (ViewState["filterByTab"] == null)
                    ViewState["filterByTab"] = new List<int>();
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
                foreach (var item in lstStatusEntrada.OrderBy(it=>it.ordem))
                {
                    str.AppendFormat("<li id='{0}'><a href='#aba-1' class='aba-1'>{1}</a></li>", item.id_status_entrada, item.nome);
                }
                ulAbas.InnerHtml = str.ToString();
                filterByTab = lstStatusEntrada.OrderBy(it => it.ordem).First().id_status_entrada;
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
            if (ddlSize.SelectedValue == "0")
                grid.PageSize = 50;
            else
                grid.PageSize = Convert.ToInt32(ddlSize.SelectedValue);

            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            grid.DataKeyNames = new string[] { PRIMARY_KEY };



            grid.DataSource = ObjBLL.FindCaixaEntrada(
               filtros,
               (string)ViewState["SortExpression"],
               (string)ViewState["SortDirection"], size, 
               SecurityBLL.GetCurrentUsuario().id_usuario, 
               filterByTab, Util.StringToInteiro(ddlAno.SelectedValue).GetValueOrDefault()).OfType<Entrada>().ToList().OrderByDescending(it=>it.EstadoAtual.id_historico_entrada).ToList();
              
            grid.DataBind();
            
            foreach (GridViewRow row in grid.Rows)
            {
                CheckBox ckItem = (CheckBox)row.FindControl("ckItem");
                ckItem.Checked = lstSelected.Contains(Convert.ToInt32(grid.DataKeys[row.RowIndex][PRIMARY_KEY]));
            }

            btProvidencia.Visible = ((List<Entrada>)grid.DataSource).Count() > 0;
        }

        protected override void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UpdateSelectedRows();
            base.grid_PageIndexChanging(sender, e);
        }

        public void UpdateSelectedRows()
        {
            foreach (GridViewRow row in grid.Rows)
            {
                int id = Convert.ToInt32(grid.DataKeys[row.RowIndex][PRIMARY_KEY]);
                CheckBox ckItem = (CheckBox)row.FindControl("ckItem");
                if (ckItem.Checked)
                {
                    if (!lstSelected.Contains(id))
                        lstSelected.Add(id);
                }
                else
                    if (lstSelected.Contains(id))
                        lstSelected.Remove(id);
            }
        }

        protected void btProvidencia_Click(object sender, EventArgs e)
        {
            lblMsg.Text = String.Empty;
            UpdateSelectedRows();
            string strMsg = "";
            if (ObjBLL.DataIsValid(ref strMsg, lstSelected))
            {
                cDdlPossivelProvidencia1.Id_status_atual = Util.StringToInteiro(txtTabIndex.Text).GetValueOrDefault();
                cDdlPossivelProvidencia1.DataBind();
                cDdlPossivelProvidencia1_SelectedIndexChanged(null, null);
                cTextoUltimaObs.Text = String.Empty;
                Util.ChamarScript("openDialog();", "");
            }
            else
                Util.ShowMessage(strMsg);
        }

        protected void btTabSelect_Click(object sender, EventArgs e)
        {
            lstSelected.Clear();
            filterByTab =   Util.StringToInteiro(txtTabIndex.Text).GetValueOrDefault();
            SetGrid();
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            lblMsg.Text = String.Empty;
            lblMsgErrorProvidencia.Text = String.Empty;

            string strMsg = "";
            if (ObjBLL.DataIsValid(ref strMsg, cDdlPossivelProvidencia1.Id_posivel_providencia, lstSelected))
            {
                Util.ChamarScript("closeDialog();", "");
                if (ObjBLL.AtualizarEstado(ObjBLL.Find(it => lstSelected.Contains(it.id_entrada)).ToList(), GetNewEstado(), PesquisaUsuarioEntrada11.Retorno.ToList()))
                {
                    PkValue = 0;
                    ObjBLL.Detach();
                    btTabSelect_Click(null, null);
                }
                else
                {
                    msgError("erro em um ou mais documentos");
                }
                PesquisaUsuarioEntrada11.DataBind();
            }
            else
                msgErrorProvidencia(strMsg);

        }

        private void msgErrorProvidencia(string strMsg)
        {
            lblMsgErrorProvidencia.BackColor = System.Drawing.Color.Red;
            lblMsgErrorProvidencia.ForeColor = System.Drawing.Color.White;
            lblMsgErrorProvidencia.Text = string.Format("* {0} !?", strMsg);
        }

        public HistoricoEntrada GetNewEstado()
        {
            var pp = new PossivelProvidenciaBLL();
            pp.Get(cDdlPossivelProvidencia1.Id_posivel_providencia);

            return new HistoricoEntrada()
            {
                StatusEntrada = pp.ObjEF.Providencia.StatusFinal,
                id_status_entrada = pp.ObjEF.Providencia.id_status_final,
                id_usuario_de = SecurityBLL.GetCurrentUsuario().id_usuario,
                obs = cTextoUltimaObs.Text
            };
        }

        protected void cDdlPossivelProvidencia1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pp = new PossivelProvidenciaBLL();
            pp.Get(cDdlPossivelProvidencia1.Id_posivel_providencia);
            trShortUsers.Visible = cDdlPossivelProvidencia1.Id_posivel_providencia == 0 ? false : pp.ObjEF.Providencia.StatusFinal.escolhe_destinatarios;
            PesquisaUsuarioEntrada11.DataBind();
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

        protected override void grid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (!e.Row.Equals(null) & e.Row.RowType.Equals(DataControlRowType.Header))
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    //try
                    //{
                    if (cell.HasControls() & !cell.Controls.Contains(cell.FindControl("imbClear")))
                    {
                        LinkButton button = (LinkButton)cell.Controls[0];

                        if (!button.Equals(null))
                        {
                            Image image = new Image();
                            if (ViewState["SortExpression"].Equals(button.CommandArgument))
                            {
                                if (ViewState["SortDirection"].Equals("ASC"))
                                    image.ImageUrl = @"~/Styles/img/SortAsc.png";
                                else
                                    image.ImageUrl = @"~/Styles/img/SortDesc.png";

                                cell.Controls.Add(image);
                            }
                        }
                    }
                    //}
                    //catch (Exception)
                    //{

                    //}
                }
            }
        }

        protected void imbClear_Click(object sender, ImageClickEventArgs e)
        {
            lstSelected.Clear();
            SetGrid();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGrid();
        }
    }
}
