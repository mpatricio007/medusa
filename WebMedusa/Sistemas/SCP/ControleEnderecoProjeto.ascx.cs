using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.DAL;
using Medusa.BLL;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleEnderecoProjeto : ControlCrud<EnderecoProjetoBLL>
    {
        //delegate
        public delegate void SelectedIndexChangedEventHandler(object sender, System.EventArgs e);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

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

        public bool Pc
        {
            get
            {
                return panelCadastro.Visible;
            }
            set
            {
                panelCadastro.Visible = value;
            }
        }

        public Endereco endereco
        {
            get
            {
                return cEnder1.Value;
            }
            set
            {
                cEnder1.Value = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_ender_projeto";
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
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_ender_projeto);
            if(ObjBLL.ObjEF.id_ender_projeto != 0)
                cDdlTipoEndereco1.Id_tipo_ender = ObjBLL.ObjEF.id_tipo_ender;
            cEnder1.Value = ObjBLL.ObjEF.endereco;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_ender_projeto = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.id_tipo_ender = cDdlTipoEndereco1.Id_tipo_ender;
            ObjBLL.ObjEF.id_projeto = Id_projeto;
            ObjBLL.ObjEF.endereco = cEnder1.Value;

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
            base.btInserir_Click(sender, e);
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
                f.property = "endereco.logradouro";
                f.property_name = "endereco.logradouro";
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

        protected void rbTipoEnderecoCorrespondencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(sender, e);
        }
    }
}