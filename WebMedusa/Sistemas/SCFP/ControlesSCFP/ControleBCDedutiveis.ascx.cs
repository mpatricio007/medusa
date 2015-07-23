using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.SCFP.ControlesSCFP
{
    public partial class ControleBCDedutiveis : ControlCrud<BaseCalculoDedutivelBLL>
    {
        public int Id_tabela
        {
            get
            {
                if (ViewState["Id_tabela"] == null)
                    Id_tabela = 0;
                return (int)ViewState["Id_tabela"];
            }
            set
            {
                ViewState["Id_tabela"] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_bc_dedutivel";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = new Label();
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = btInserir;
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

        protected override void SetAdd()
        {
            panelGrid.Visible = panelCadastro.Visible = true;
        }

        protected override void SetModify()
        {
            SetAdd();
        }

        protected override void SetView()
        {
            SetAdd();
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            lista.SelectedValue = Util.InteiroToString(ObjBLL.ObjEF.id_taxa);
            lista.DataBind();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_taxa = Util.StringToInteiro(lista.SelectedValue).GetValueOrDefault();
            ObjBLL.ObjEF.id_tabela = Id_tabela;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                ObjBLL.Detach();
                PkValue = 0;
                Get();
                SetGrid();
            }
            else
                msgError("erro inclusão");
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "id_tabela",
                value = Util.InteiroToString(Id_tabela),
                mode = "=="
            };
            filtros.Add(f);
            grid.DataKeyNames = new string[] { "id_taxa" };
            grid.DataSource = ObjBLL.Find(filtros,
               "id_bc_dedutivel",
               "ASC", 0).OfType<BaseCalculoDedutivel>().ToList().Select(it=>it.taxa).ToList();
            filtros.Remove(f);
            grid.DataBind();
        }

        public override void DataBind()
        {
            base.DataBind();

            TaxaBLL t = new TaxaBLL();
            TabelaTaxasBLL tt = new TabelaTaxasBLL();
            tt.Get(Id_tabela);
            lista.Items.Clear();
            lista.DataSource = t.GetAll("nome").OfType<Taxa>().Where(it => it.id_taxa != tt.ObjEF.id_taxa);
            lista.Items.Insert(0, new ListItem("selecione uma taxa...", "0"));
            lista.DataBind();

            SetGrid();
            SetAdd();
        }

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ObjBLL.Get(Id_tabela, Convert.ToInt32(grid.DataKeys[e.RowIndex]["id_taxa"]));
            ObjBLL.Delete();
            ObjBLL.SaveChanges();
            ObjBLL.Detach();
            PkValue = 0;
            Get();
            SetGrid();
        }
    }
}