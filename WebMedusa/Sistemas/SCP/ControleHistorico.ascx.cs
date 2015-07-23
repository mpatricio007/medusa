using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Sistemas.SCP
{
    public partial class ControleHistorico : System.Web.UI.UserControl
    {
        public int Id_sol_proj
        {
            get
            {
                if (ViewState[ID] == null)
                    Id_sol_proj = 0;
                return (int)ViewState[ID];
            }

            set
            {
                ViewState[ID] = value;
            }
        }

        

        private HistoricoPxBLL objBLL = new HistoricoPxBLL();
        private int id_hpx = 0;
        private const string PRIMARY_KEY = "id_hpx";
        protected void Page_Load(object sender, EventArgs e)
        {
            id_hpx = Convert.ToInt32(this.txtCodigo.Text);
            if (!Page.IsPostBack)
            {
                SetGrid();

            }
        }            

        protected void SetAdd()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = true;
            btAlterar.Visible = false;
            btExcluir.Visible = false;
            SetGrid();
        }

        protected virtual void SetModify()
        {
            lbMsg.Text = String.Empty;
            btInserir.Visible = false;
            btAlterar.Visible = true;
            btExcluir.Visible = true;
            SetGrid();
        }

        protected void Get()
        {
            objBLL.Get(id_hpx);
            this.txtCodigo.Text = Convert.ToString(objBLL.ObjEF.id_hpx);
            this.cTextoObs.Text = objBLL.ObjEF.observacao;
            this.cDdlStatusSolicitacao1.Id_status_solicitacao = objBLL.ObjEF.id_status_solicitacao;
            //pCadastro.Visible = objBLL.ObjEF.ProjetoSolicitacao.id_sol_proj != 0 ? objBLL.ObjEF.ProjetoSolicitacao.id_ultimo_status != 4 : true;
        }

        protected void Set()
        {            
            objBLL.ObjEF.id_hpx = Convert.ToInt32(txtCodigo.Text);
            objBLL.ObjEF.observacao = cTextoObs.Text;
            objBLL.ObjEF.id_status_solicitacao = cDdlStatusSolicitacao1.Id_status_solicitacao;
            objBLL.ObjEF.id_sol_proj = Id_sol_proj;
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
            lbMsg.Text = string.Format("* {0} !?", msg);
        }

        protected virtual void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            objBLL.Add();
            if (objBLL.SaveChanges())
            {
                msg("inclusão efetuada");
                objBLL.Detach();
                id_hpx = 0;
                Get();
                SetAdd();
            }
            else
                msgError("erro inclusão");
        }

        protected virtual void btAlterar_Click(object sender, EventArgs e)
        {
            objBLL.Get(id_hpx);
            Set();
            objBLL.Update();
            if (objBLL.SaveChanges())
            {
                msg("alteração efetuada");
                id_hpx = 0;
                SetAdd();
            }
            else
                msgError("erro alteração");
            Get();
            
        }

        protected virtual void btExcluir_Click(object sender, EventArgs e)
        {
            objBLL.Get(id_hpx);
            objBLL.Delete();
            if (objBLL.SaveChanges())
                msg("exclusão efetuada");
            else
                msgError("erro exclusão");
            Get();
            SetAdd();
        }

        protected virtual void btCancelar_Click(object sender, EventArgs e)
        {
            id_hpx = 0;
            Get();
            SetAdd();
        }

        protected virtual void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            id_hpx = Convert.ToInt32(grid.DataKeys[e.NewEditIndex][PRIMARY_KEY]);
            Get();
            grid.DataBind();
            SetModify();
            e.Cancel = true;
        }

        protected virtual void SetGrid()
        {
            var filtros = new List<Filter>();
            filtros.Add(new Filter()
            {
                property = "id_sol_proj",
                value = Convert.ToString(Id_sol_proj),
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
