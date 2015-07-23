using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class ControleRepresentanteLegal : ControlCrud<RepresentateLegalBLL>
    {
        public int Id_fornecedor
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_fornecedor = 0;
               
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
            PRIMARY_KEY = "id_representante_legal";
            //valor da chave primária
            PkValue = Convert.ToInt32(this.txtCodigo.Text);
            // gridview
            _grid = grid;
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
            lbMsg.Text = String.Empty;
            btInserir.Visible = true;
            btAlterar.Visible = false;
            btExcluir.Visible = false;
            SetGrid();

        }

        protected override void SetModify()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = false;
            btAlterar.Visible = true;
            btExcluir.Visible = true;
            SetGrid();
        }

        protected override void Get()
        {
            if(PkValue == null)
                PkValue = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.Get(PkValue);
            this.txtCodigo.Text = Convert.ToString(ObjBLL.ObjEF.id_representante_legal);
            this.cTextoNome.Text = ObjBLL.ObjEF.nome;
            this.cCPF.Value = ObjBLL.ObjEF.cpf;
            this.cTextoRg.Text = ObjBLL.ObjEF.rg;
            this.cEmail.Value = ObjBLL.ObjEF.email;
        }

        protected override void Set()
        {
            ObjBLL.ObjEF.id_representante_legal = Convert.ToInt32(this.txtCodigo.Text);
            ObjBLL.ObjEF.id_fornecedor = Id_fornecedor;
            ObjBLL.ObjEF.nome = this.cTextoNome.Text;
            ObjBLL.ObjEF.cpf = this.cCPF.Value;
            ObjBLL.ObjEF.rg = this.cTextoRg.Text;
            ObjBLL.ObjEF.email = this.cEmail.Value;
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

        protected override void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PkValue = Convert.ToInt32(grid.DataKeys[e.NewEditIndex][PRIMARY_KEY]);
            Get();
            grid.DataBind();
            SetModify();
            e.Cancel = true;
        }

        protected override void SetGrid()
        {
            // chave primária da tabela
            PRIMARY_KEY = "id_representante_legal";
            //valor da chave primária
            //PkValue = Convert.ToInt32(this.txtCodigo.Text);

            if(ViewState["SortExpression"] == null)
                ViewState["SortExpression"] = PRIMARY_KEY;

            if (ViewState["SortDirection"] == null)
                ViewState["SortDirection"] = "ASC";

            Filter f = new Filter()
            {
                property = "id_fornecedor",
                value = Convert.ToString(Id_fornecedor),
                mode = "=="
            };
            filtros.Add(f);
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = ObjBLL.Find(filtros,
               (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], 0);
            grid.DataBind();
            filtros.Remove(f);
        }

        public override void DataBind()
        {
            Get();
            SetAdd();
            base.DataBind();
        }
    }
}