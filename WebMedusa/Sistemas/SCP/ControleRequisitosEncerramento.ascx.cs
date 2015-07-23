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
    public partial class ControleRequisitosEncerramento : ControlCrud<RequisitoEncerramentoBLL>
    {
        public int Id_projeto
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_projeto = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {

            // chave primária da tabela
            PRIMARY_KEY = "id_req_enc";
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
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_req_enc);
            cTextoDescricao.Text = ObjBLL.ObjEF.descricao;
            cDataSolucao.Value = ObjBLL.ObjEF.data_solucao;
            cTextoSolucao.Text = ObjBLL.ObjEF.solucao;
            ckStatus.Checked = ObjBLL.ObjEF.status.GetValueOrDefault();

            dSolucao.Visible = ObjBLL.Exists();
            btAtivar.Visible = ObjBLL.Exists() ? !ObjBLL.ObjEF.status.GetValueOrDefault() : false;
            btResolver.Visible = ObjBLL.ObjEF.status.GetValueOrDefault();
        }

        

        protected override void Set()
        {
            ObjBLL.ObjEF.id_req_enc = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.descricao = cTextoDescricao.Text;
            ObjBLL.ObjEF.data_solucao = cDataSolucao.Value;
            ObjBLL.ObjEF.solucao = cTextoSolucao.Text;
            ObjBLL.ObjEF.id_projeto = Id_projeto;
            ObjBLL.ObjEF.status = ckStatus.Checked;
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

            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(
               filtros,
               (string)ViewState["SortExpression"],
               (string)ViewState["SortDirection"], 0);
            grid.DataBind();
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

        protected override void btCriar_Click(object sender, EventArgs e)
        {
            PkValue = 0;
            Get();
            SetAdd();
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            ckStatus.Checked = true;
            base.btInserir_Click(sender, e);
            grid.DataBind();
            SetGrid();
            SetView();
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
            {
                msg("alteração efetuada");
                Get();
            }
            else
                msgError("erro alteração");
        }

        protected void btResolver_Click(object sender, EventArgs e)
        {
            ckStatus.Checked = false;
            btAlterar_Click(sender, e);
        }

        protected void btAtivar_Click(object sender, EventArgs e)
        {
            ckStatus.Checked = true;
            cTextoSolucao.Text = String.Empty;
            cDataSolucao.Value = new Nullable<DateTime>();
            btAlterar_Click(sender, e);
        }

        protected override void SetAdd()
        {
            base.SetAdd();
            //dSolucao.Visible = false;
            //btResolver.Visible = false;
            //btAtivar.Visible = false;
        }

        protected override void SetModify()
        {
            base.SetModify();
            //dSolucao.Visible = true;
            //btResolver.Visible = ckStatus.Checked;
            //btAtivar.Visible = !btResolver.Visible;
        }

        protected override void SetView()
        {
            base.SetView();
            //btResolver.Visible = false;
            //btAtivar.Visible = false;
        }
    }
}