using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Medusa.DAL;
using Medusa.BLL;

namespace Medusa.Sistemas.Recibos
{
    public partial class ControleReciboCheques : ControlCrud<ReciboChequeBLL>
    {
        public int Id_recibo
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_recibo = 0;

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
            PRIMARY_KEY = "id_cheque";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // gridview
            _grid = gridCheque;
            _btAlterar = btAlterar;
            _btInserir = btInserir;
            _btExcluir = btExcluir;
            _ddlSize = new DropDownList();
            _ddlMode = new DropDownList();
            _ddlOptions = new DropDownList();
            _ckFilter = new CheckBox();
            _dataListFiltros = new DataList();
            lbMsg = lblMsg;

            if (!IsPostBack)
            {
                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "ASC";

                SetGrid();
                _btExcluir.OnClientClick = "return confirm('confirma exclusão?')";
        }
    }
        protected override void SetAdd()
        {
            lbMsg.Visible = true;
            lbMsg.Text = String.Empty;
            btInserir.Visible = true;
            btAlterar.Visible = false;
            btExcluir.Visible = false;
            SetGrid();
        }

        protected override void SetModify()
        {
            lbMsg.Visible = true;
            lbMsg.Text = String.Empty;
            btInserir.Visible = false;
            btAlterar.Visible = true;
            btExcluir.Visible = true;
            SetGrid();
        }

        protected override void Get()
        {
            PkValue = PkValue == null ? Convert.ToInt32(txtCodigo.Text) : PkValue;
            
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_cheque);
            this.cIntNumCheque.Value = ObjBLL.ObjEF.num_cheque;
            this.cData.Value = ObjBLL.ObjEF.data;
            this.cValor.Value = ObjBLL.ObjEF.valor;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_cheque = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_recibo = Id_recibo;
            ObjBLL.ObjEF.num_cheque = Convert.ToInt32(this.cIntNumCheque.Value);
            ObjBLL.ObjEF.data = Convert.ToDateTime(this.cData.Value);
            ObjBLL.ObjEF.valor = Convert.ToDecimal(this.cValor.Value);
        }

        protected override void msg(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Green;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !", msg);
        }

        protected override void msgError(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Red;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !", msg);
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
                SetAdd();
            }
            else
                msgError("erro inclusão");
        }

        protected override void btAlterar_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            Set();
            ObjBLL.Update();
            if (ObjBLL.SaveChanges())
            {
                msg("Alteração efetuada");
                ObjBLL.Detach();
                PkValue = 0;
                Get();
                SetAdd();
            }
            else
                msgError("erro alteração");
        }

        protected override void btExcluir_Click(object sender, EventArgs e)
        {
            ObjBLL.Get(PkValue);
            ObjBLL.Delete();
            if (ObjBLL.SaveChanges())
                msg("exclusão efetuada");
            else
                msgError("erro exclusão");
            Get();
            SetAdd();
        }

        protected override void btCancelar_Click(object sender, EventArgs e)
        {
            PkValue = 0;
            Get();
            SetAdd();
        }

        protected override void SetGrid()
        {
            // chave primária da tabela
            //PRIMARY_KEY = "id_cheque";
            //valor da chave primária
            //PkValue = Convert.ToInt32(this.txtCodigo.Text);

            if (ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = PRIMARY_KEY;

            if (ViewState["SortDirection"] == null)
                ViewState["SortDirection"] = "ASC";

            Filter f = new Filter()
            {
                property = "id_recibo",
                value = Convert.ToString(Id_recibo),
                mode = "=="
            };
            filtros.Add(f);
            gridCheque.DataKeyNames = new string[] { PRIMARY_KEY };
            gridCheque.DataSource = ObjBLL.Find(filtros,
               (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0);
            gridCheque.DataBind();
            filtros.Remove(f);
        }

        public override void DataBind()
        {
            Get();
            SetAdd();
            base.DataBind();
        }

        protected void grid_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            PkValue = Convert.ToInt32(gridCheque.DataKeys[e.NewEditIndex][PRIMARY_KEY]);
            Get();
            gridCheque.DataBind();
            SetModify();
            e.Cancel = true;
        }
    }
}