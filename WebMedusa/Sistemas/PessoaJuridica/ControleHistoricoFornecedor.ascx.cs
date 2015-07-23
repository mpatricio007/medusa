using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class ControleHistoricoFornecedor : System.Web.UI.UserControl
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

        private HistoricoFornecedorBLL objBLL = new HistoricoFornecedorBLL();
        private int id_hist_fornecedor = 0;
        private const string PRIMARY_KEY = "id_hist_fornecedor";
        protected void Page_Load(object sender, EventArgs e)
        {
            id_hist_fornecedor = Convert.ToInt32(this.txtCodigo.Text);
            if (!Page.IsPostBack)
            {
                SetGrid();
            }
        }

        protected void SetAdd()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = true; 
            SetGrid();
        }

        protected virtual void SetModify()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = false;
            btCancelar.Visible = true;
            SetGrid();
        }

        protected void Get()
        {
            objBLL.Get(id_hist_fornecedor);
            this.txtCodigo.Text = Convert.ToString(objBLL.ObjEF.id_hist_fornecedor);
            this.cTextoObs.Text = objBLL.ObjEF.observacao;
            this.cddlStatusFornecedores1.id_status_fornecedor = objBLL.ObjEF.id_status_fornecedor;          
        }

        protected void Set()
        {
            objBLL.ObjEF.id_hist_fornecedor = Convert.ToInt32(this.txtCodigo.Text);
            objBLL.ObjEF.observacao = this.cTextoObs.Text;
            objBLL.ObjEF.id_status_fornecedor = this.cddlStatusFornecedores1.id_status_fornecedor;
            objBLL.ObjEF.id_fornecedor = Id_fornecedor;
        }

        protected virtual void msg(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Green;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !", msg);
        }

        protected virtual void msgError(string msg)
        {
            lbMsg.BackColor = System.Drawing.Color.Red;
            lbMsg.ForeColor = System.Drawing.Color.White;
            lbMsg.Text = string.Format("* {0} !", msg);
        }

        protected void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            objBLL.Add();
            if (objBLL.SaveChanges())
            {
                objBLL.EnviarEmail();
                msg("inclusão efetuada");
                objBLL.Detach();
                id_hist_fornecedor = 0;
                Get();
                SetAdd();

            }
            else
                msgError("erro inclusão");
        }

        //protected void btAlterar_Click(object sender, EventArgs e)
        //{
        //    objBLL.Get(id_hist_fornecedor);
        //    Set();
        //    objBLL.Update();
        //    if(objBLL.SaveChanges())
        //    {
                
        //        msg("Alteração efetuada");
        //        objBLL.Detach();
        //        id_hist_fornecedor = 0;
        //            Get();
        //        SetAdd();

        //    }
        //    else
        //        msgError("erro alteração");
        //}

        //protected void btExcluir_Click(object sender, EventArgs e)
        //{
        //    objBLL.Get(id_hist_fornecedor);
        //    objBLL.Delete();
        //    if (objBLL.SaveChanges())
        //        msg("exclusão efetuada");
        //    else
        //        msgError("erro exclusão");
        //    Get();
        //    SetAdd();
        //}

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            id_hist_fornecedor = 0;
            Get();
            SetAdd();
        }

        protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            id_hist_fornecedor = Convert.ToInt32(grid.DataKeys[e.NewEditIndex][PRIMARY_KEY]);
            Get();
            grid.DataBind();
            SetModify();
            e.Cancel = true;
        }

        protected void SetGrid()
        {
            var filtros = new List<Filter>();
            filtros.Add(new Filter()
            {
                property = "id_fornecedor",
                value = Convert.ToString(Id_fornecedor),
                mode = "=="
            });
            grid.DataKeyNames = new string[] { PRIMARY_KEY };
            grid.DataSource = objBLL.Find(filtros,
               "data",
               "DESC", 0);
            grid.DataBind();
            }

        public override void DataBind()
        {
            Get();
            SetAdd();
            SetGrid();
            base.DataBind();
        }
    }
}