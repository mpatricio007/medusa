using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleHistoricoProjeto : ControlCrud<HistoricoProjetoBLL>
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
            PRIMARY_KEY = "id_hsp";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = new Panel();
            // painel do formulário de alteração
            pCadastro = new Panel();
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = new Button();
            _btAlterar0 = new Button();
            _btInserir = new Button();
            _btInserir0 = btInserir;
            _btExcluir = new Button();
            _btExcluir0 = new Button();
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _txtProcura = new TextBox();
            _btProcurar = new Button();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();

            if (!IsPostBack)
            {
                Security();

                ViewState["SortExpression"] = "data";
                ViewState["SortDirection"] = "DESC";


                
                
            }
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_hsp);
            cTextoObs.Text = ObjBLL.ObjEF.observacao;
            cDdlStatusProjeto1.Id_status_projeto = ObjBLL.ObjEF.id_status_projeto;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_hsp = Convert.ToInt32(txtCodigo.Text);
            ObjBLL.ObjEF.observacao = cTextoObs.Text;
            ObjBLL.ObjEF.id_status_projeto = cDdlStatusProjeto1.Id_status_projeto;
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

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            string txtMsg;
            ObjBLL.Get(Convert.ToInt32(txtCodigo.Text));
            if (ObjBLL.ValidarStatus(out txtMsg, cDdlStatusProjeto1.Id_status_projeto,Id_projeto))
                Inserir();
            msgError(txtMsg);
        }

        protected override void msgError(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Red;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = msg;
        }

        public void Inserir()
        {
            Set();
            ObjBLL.Add();
            if (ObjBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                ObjBLL.Detach();
                PkValue = 0;
                Get();
                SetAdd();
                var p = (ProjetosDefinitivos)base.Page;
                p.GetExternal();
            }
            else
                msgError("erro inclusão");
            SetView();
            SetGrid();
        }

        protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.grid_RowEditing(sender, e);
            SetAdd();
            SetGrid();
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            base.btCancelar_Click(sender, e);
            lblMsg.Text = String.Empty;
            PkValue = 0;
            Get();
        }
    }
}