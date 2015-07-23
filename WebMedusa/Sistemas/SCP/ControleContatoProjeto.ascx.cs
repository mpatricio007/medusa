using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleContatoProjeto : ControlCrud<ContatoProjetoBLL>
    {
        public int Id_projeto
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_projeto = 0;
                return (int)ViewState[ID];
            }
            set { ViewState[ID] = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_contato_projeto";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = btAlterar0;
            _btInserir = new Button();
            _btInserir0 = btInserir0;
            _btExcluir = new Button();
            _btExcluir0 = btExcluir0;
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
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "ASC";

                SetGrid();
                _btExcluir.OnClientClick = "return confirm('confirma exclusão?')";
                _btExcluir0.OnClientClick = "return confirm('confirma exclusão?')";
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_contato_projeto);
            cDdlContato1.Id_contato = ObjBLL.ObjEF.id_contato;
            SetGridSolDeProj();
            SetGridSolProjThis();
            dNotificacoes.Visible = ObjBLL.Exixtes();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_contato_projeto = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.id_projeto = Id_projeto;
            ObjBLL.ObjEF.id_contato = cDdlContato1.Id_contato;  
        }

        protected override void SetGrid()
        {
            Filter f = new Filter()
            {
                property = "id_projeto",
                value = Convert.ToString(Id_projeto),
                mode = "=="
            };
            filtros.Add(f);

            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0);
            _grid.DataBind();
            filtros.Remove(f);
        }

        public override void DataBind()
        {
            SetGrid();
            SetView();
            base.DataBind();
        }

        protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PkValue = _grid.DataKeys[e.NewEditIndex][PRIMARY_KEY];
            Get();
            _grid.DataBind();
            SetModify();
            e.Cancel = true;
            SetGrid();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            ObjBLL.Add();
            
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                ObjBLL.Detach();
                PkValue = 0;
                Get();
            }
            else
                msgError("erro inclusão");
            
            SetGrid();
            SetView();
        }

        protected override void btCriar_Click(object sender, EventArgs e)
        {
            PkValue = 0;
            Get();
            SetAdd();
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            SetModify();
            base.btAlterar_Click(sender, e);
            SetGrid();
            SetView();
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            base.btExcluir_Click(sender, e);
            SetGrid();
            SetView();
        }

        protected override void btProcurar_Click(object sender, EventArgs e)
        {
            filtros.Clear();
            filtros.Add(setFilter());
            SetGrid();
            SetView();
        }

        protected override Filter setFilter()
        {
            try
            {
                Filter f = new Filter();
                f.property = "Contato.Pessoa.nome";
                f.property_name = "Contato.Pessoa.nome";
                f.value = this._txtProcura.Text.ToUpper();
                f.mode = "StartsWith";
                f.mode_name = "começando com";
                return f;
            }

            catch (Exception)
            {
                return new Filter();
            }
        }

        protected void SetGridSolDeProj()
        {
            GridSolDeProj.DataKeyNames = new string[] { "id_sol_de_proj" };
            GridSolDeProj.DataSource = ObjBLL.GetAllExeptThisContatoProj();
            GridSolDeProj.DataBind();
        }

        protected void SetGridSolProjThis()
        {
            GridSolDeProjThis.DataKeyNames = new string[] { "id_notificacao" };
            GridSolDeProjThis.DataSource = ObjBLL.ObjEF.Notificacoes;
            GridSolDeProjThis.DataBind();
        }

        protected void GridSolDeProj_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var cppBLL = new NotificacaoBLL();
            cppBLL.ObjEF.id_sol_de_proj = Convert.ToInt32(GridSolDeProj.DataKeys[e.NewEditIndex]["id_sol_de_proj"].ToString());
            cppBLL.ObjEF.id_contato_projeto = (int)PkValue;
            cppBLL.Add();
            if (!cppBLL.SaveChanges())
                msgError("* erro ao tentar inserir !?");
            Get();
            cppBLL.Detach();
            e.Cancel = true;
        }

        protected void GridSolDeProjThis_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var cppBLL = new NotificacaoBLL();
            cppBLL.Get(Convert.ToInt32(GridSolDeProjThis.DataKeys[e.NewEditIndex]["id_notificacao"].ToString()));
            cppBLL.Delete();
            if (!cppBLL.SaveChanges())
                msgError("* erro ao tentar excluir !?");
            Get();
            cppBLL.Detach();
            e.Cancel = true;
        }
    }
}