using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;
using System.Data;

namespace Medusa.Sistemas.SCFP.ControlesSCFP
{
    public partial class ControleTabelas : ControlCrud<TabelaTaxasBLL>
    {
        public int Id_taxa
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_taxa = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        public int EditIndex
        {
            get
            {
                if (ViewState["EditIndex"] == null)
                    EditIndex = -1;
                return (int)ViewState["EditIndex"];
            }
            set
            {
                ViewState["EditIndex"] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_tabela";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
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
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            if (!IsPostBack)
            {
                Security();
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
            grid.EditIndex = EditIndex;
            Filter f = new Filter()
            {
                value = Util.InteiroToString(Id_taxa),
                property = "id_taxa",
                mode = "=="
            };

            filtros.Add(f);
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(
                filtros,
                "id_tabela",
                "ASC", 0);
            grid.DataBind();
            filtros.Remove(f);
        }

        public override void DataBind()
        {
            base.DataBind();
            SetGrid();
        }

        protected override void grid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            base.grid_RowCreated(sender,e);
            
            if (!e.Row.Equals(null) & e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                Panel panel = (Panel)e.Row.FindControl("pFaixas");
                if (panel.Visible = e.Row.RowIndex == EditIndex)
                {
                    ControleBCDedutiveis cbc = (ControleBCDedutiveis)panel.FindControl("ControleBCDedutiveis1");
                    ControleFaixaTaxas cft = (ControleFaixaTaxas)panel.FindControl("ControleFaixaTaxas1");
                    cbc.Id_tabela = cft.Id_tabela = Convert.ToInt32(grid.DataKeys[EditIndex]["id_tabela"]);
                    cft.DataBind();
                    cbc.DataBind();
                }
            }

            //if (!e.Row.Equals(null) & e.Row.RowType.Equals(DataControlRowType.EmptyDataRow))
            //{
            //    var ctr = (ControleFaixaTaxas)e.Row.FindControl("ControleFaixaTaxas1");
            //    ctr.DataBind();
            //}

        }

        protected void grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EditIndex = -1;
            SetGrid();
        }

        protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EditIndex = e.NewEditIndex;
            SetGrid();
        }

        protected void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ObjBLL.Get(Convert.ToInt32(grid.DataKeys[e.RowIndex][PRIMARY_KEY]));
            ObjBLL.ObjEF.data_ini = ((Medusa.Controles.Data)grid.Rows[e.RowIndex].FindControl("cDataInicio")).Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_fim = ((Medusa.Controles.Data)grid.Rows[e.RowIndex].FindControl("cDataTermino")).Value.GetValueOrDefault();
            ObjBLL.ObjEF.cumulativo_mensal = ((CheckBox)grid.Rows[e.RowIndex].FindControl("ckCumulativoMensal")).Checked;
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
            {
                Util.ShowMessage("alteração efetuada");
            }
            else
                msgError("erro alteração");
        }

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ObjBLL.Get(Convert.ToInt32(grid.DataKeys[e.RowIndex][PRIMARY_KEY]));
            ObjBLL.Delete();
            if (ObjBLL.SaveChanges())
            {
                EditIndex = -1;
                SetGrid();
            }
            else
                Util.ShowMessage("erro exclusão");
        }

        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            ObjBLL.ObjEF.data_ini = ((Medusa.Controles.Data)grid.Controls[0].Controls[0].FindControl("cAddDataInicio")).Value.GetValueOrDefault();
            ObjBLL.ObjEF.data_fim = ((Medusa.Controles.Data)grid.Controls[0].Controls[0].FindControl("cAddDataTermino")).Value.GetValueOrDefault();
            ObjBLL.ObjEF.cumulativo_mensal = ((CheckBox)grid.Controls[0].Controls[0].FindControl("ckAddCumulativoMensalmente")).Checked;
            ObjBLL.ObjEF.id_taxa = Id_taxa;
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                ((Medusa.Controles.Data)grid.Controls[0].Controls[0].FindControl("cAddDataInicio")).Value = new DateTime();
                ((Medusa.Controles.Data)grid.Controls[0].Controls[0].FindControl("cAddDataTermino")).Value = new DateTime();
                ((CheckBox)grid.Controls[0].Controls[0].FindControl("ckAddCumulativoMensalmente")).Checked = false;
                EditIndex = grid.Rows.Count;
                SetGrid();
            }
            else
                msgError("erro inclusão");
        }

        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                ObjBLL.ObjEF.data_ini = ((Medusa.Controles.Data)grid.FooterRow.FindControl("cAddDataInicio")).Value.GetValueOrDefault();
                ObjBLL.ObjEF.data_fim = ((Medusa.Controles.Data)grid.FooterRow.FindControl("cAddDataTermino")).Value.GetValueOrDefault();
                ObjBLL.ObjEF.cumulativo_mensal = ((CheckBox)grid.FooterRow.FindControl("ckAddCumulativoMensalmente")).Checked;
                ObjBLL.ObjEF.id_taxa = Id_taxa;
                ObjBLL.Add();
                if (ObjBLL.SaveChanges())
                {
                    ((Medusa.Controles.Data)grid.FooterRow.FindControl("cAddDataInicio")).Value = new DateTime();
                    ((Medusa.Controles.Data)grid.FooterRow.FindControl("cAddDataTermino")).Value = new DateTime();
                    ((CheckBox)grid.FooterRow.FindControl("ckAddCumulativoMensalmente")).Checked = false;
                    EditIndex = grid.Rows.Count;
                    SetGrid();
                }
                else
                    msgError("erro inclusão");
            }
        }
    }
}