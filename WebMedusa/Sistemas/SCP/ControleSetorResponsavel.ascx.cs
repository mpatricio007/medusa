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
    public partial class ControleSetorResponsavel : ControlCrud<SetorResponsavelBLL>
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
            PRIMARY_KEY = "id_setor_resp";
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
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_setor_resp);
            cDdlSetor1.Id_setor = ObjBLL.ObjEF.id_setor;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_setor_resp = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.id_setor = cDdlSetor1.Id_setor;
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

  

        protected override Filter setFilter()
        {
            try
            {
                Filter f = new Filter();
                f.property = "Setor.nome";
                f.property_name = "Setor.nome";
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

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            base.btExcluir_Click(sender, e);
            SetGrid();
            SetView();
        }

    }
}