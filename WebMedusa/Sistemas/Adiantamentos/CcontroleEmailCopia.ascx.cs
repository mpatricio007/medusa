using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.BLL;
using Medusa.DAL;
using BoletoNet;

namespace Medusa.Sistemas.SCP
{
    public partial class CcontroleEmailCopia : ControlCrud<EmailCopiaBLL>
    {
        public int? Id_email_padrao 
        {
            get
            {
                if (ViewState[ID] == null)
                    ViewState[ID] = 0;
                return (int)ViewState[ID];
            }
            set { ViewState[ID] = value; }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_email_copia";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // painel do grid
            pGrid = panelGrid;
            // painel do formulário de alteração
            pCadastro = panelCadastro;
            // gridview
            _grid = grid;
            lbMsg = lblMsg;
            _btAlterar = btAlterar;
            _btAlterar0 = new Button();
            _btInserir = btInserir;
            _btInserir0 = new Button();
            _btExcluir = btExcluir;
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
                //base.Page_Load(sender, e);
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "DESC";
                SetGrid();
                SetAdd();
            }
        }

        protected override void SetView()
        {
            pGrid.Visible = true;
            pCadastro.Visible = true;  
        }

        protected override void SetAdd()
        {
            lbMsg.Text = String.Empty;
            pGrid.Visible = true;
            pCadastro.Visible = true;
            btInserir.Visible = true;
            btAlterar.Visible = false;
            btExcluir.Visible = false;
        }

        protected override void SetModify()
        {
            lbMsg.Text = String.Empty;
            pCadastro.Visible = true;
            pGrid.Visible = true;
            btInserir.Visible = false;
            btAlterar.Visible = true;
            btExcluir.Visible = true;
        }

        protected override void Get()
        {
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_email_copia);
            this.cEmail1.Value = ObjBLL.ObjEF.email;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_email_copia = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.email = this.cEmail1.Value;
            ObjBLL.ObjEF.id_email_padrao = Id_email_padrao.GetValueOrDefault();
        }

        protected override void SetGrid()
        {
            //Filter f = new Filter()
            //{
            //    property = "id_email_padrao",
            //    value = Convert.ToString(Id_email_padrao),
            //    mode = "=="
            //};
            //filtros.Add(f);
            var epBLL = new EmailPadraoBLL();
            epBLL.Get(Id_email_padrao);
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = epBLL.GetEmailCopiasThis();
            //grid.DataSource = ObjBLL.Find(
            //    filtros,
            //    (string)ViewState["SortExpression"],
            //    (string)ViewState["SortDirection"], 0);
            grid.DataBind();
            //filtros.Remove(f);
        }

        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Util.NovaJanela(String.Format("Boletos.aspx?pk={0}", grid.DataKeys[e.NewSelectedIndex]["id_boleto"].ToString().Criptografar()));
            //e.Cancel = true;
        }

        protected override void btInserir_Click(object sender, EventArgs e)
        {
            base.btInserir_Click(sender, e);
            SetGrid();
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            cEmail1.Value = null;
            SetGrid();
            SetAdd();
        }

        public override void DataBind()
        {
            base.DataBind();
            cEmail1.Value = null;
            SetGrid();
            SetAdd();
        }
    }
}