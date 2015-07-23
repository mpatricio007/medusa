using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Sistemas.SREC
{
    public partial class SetorsCompetentes : System.Web.UI.UserControl
    {
        public int Id_providencia
        {
            get
            {
                if (ViewState[ID] == null)
                    ViewState[ID] = 0;
                return (int)ViewState[ID];
            }
            set
            {
                ViewState[ID] = value;
            }
        }

        public List<SetorCompetente> Value
        {
            get
            {
                if (Cache["Value"] == null)
                    Cache["Value"] = new List<SetorCompetente>();
                return (List<SetorCompetente>)Cache["Value"];
            }
            set
            {
                Cache["Value"] = value;
            }
        }

        private int index
        {
            get
            {
                return Convert.ToInt32(txtCodigo.Text);
            }
            set
            {
                txtCodigo.Text = Convert.ToString(value);

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cache.Remove("Value");
            }
        }

        protected void Set()
        {
            var sc = new SetorCompetente();
            var s = new SetorBLL();
            s.Get(cDdlSetor1.Id_setor);
            sc.Setor = s.ObjEF;
            sc.id_setor = s.ObjEF.id_setor;
            sc.id_providencia = Id_providencia;

            if (index < 0)
                Value.Add(sc);
            else
                Value[index] = sc;
        }

        public override void DataBind()
        {
            grid.DataBind();
            base.DataBind();

            cDdlSetor1.Id_setor = 0;
            cDdlSetor1.Focus();
        }

        protected void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            DataBind();
        }

        public void SetGrid()
        {
            grid.DataSource = Value;
            grid.DataBind();
        }

        protected void grid_DataBinding(object sender, EventArgs e)
        {
            grid.DataSource = Value;
        }

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Value.RemoveAt(e.RowIndex);
            index = -1;
            grid.DataBind();
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            index = -1;
            grid.DataBind();
        }

        protected void btInserir_Click(object sender, ImageClickEventArgs e)
        {
            Set();
            DataBind();
        }
    }
}