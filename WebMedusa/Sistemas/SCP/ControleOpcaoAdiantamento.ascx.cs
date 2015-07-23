using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleOpcaoAdiantamento : ControlCrud<OpcaoAdiantamentoBLL>
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
            PRIMARY_KEY = "id_opcao_admto";
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
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = btInserir0;
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
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "DESC";

                SetGrid();
                _btExcluir.OnClientClick = "return confirm('confirma exclusão?')";
                _btExcluir0.OnClientClick = "return confirm('confirma exclusão?')";
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_opcao_admto);
            cData1.Value = ObjBLL.ObjEF.data;
            cTextoObs.Text = ObjBLL.ObjEF.obs;
            cDdlTiposAdiantamentos1.Id_tipo_admto = ObjBLL.ObjEF.id_tipo_admto.GetValueOrDefault();
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_opcao_admto = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.data = cData1.Value.GetValueOrDefault();
            ObjBLL.ObjEF.obs = cTextoObs.Text;
            ObjBLL.ObjEF.id_tipo_admto = cDdlTiposAdiantamentos1.Id_tipo_admto;
            ObjBLL.ObjEF.id_projeto = Id_projeto;
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
                DataBind();
            }
            else
                msgError("erro inclusão");
        }
    }
}