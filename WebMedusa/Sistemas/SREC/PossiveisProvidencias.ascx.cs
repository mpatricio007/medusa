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
    public partial class PossiveisProvidencias : System.Web.UI.UserControl
    {
        public int Id_status_atual
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

        public List<PossivelProvidencia> Value
        {
            get
            {
                if (Cache["Value"] == null)
                    Cache["Value"] = new List<PossivelProvidencia>();
                return (List<PossivelProvidencia>)Cache["Value"];
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
            var pp = new PossivelProvidencia();
            var p = new ProvidenciaBLL();
            p.Get(cDdlProvidencia1.Id_providencia);
            pp.Providencia = p.ObjEF;
            pp.id_providencia = p.ObjEF.id_providencia;
            pp.id_status_atual = Id_status_atual;

            if (index < 0)
                Value.Add(pp);
            else
                Value[index] = pp;
        }

        protected void btInserir_Click(object sender, EventArgs e)
        {
            Set();
            DataBind();
        }

        protected void SetGrid()
        {
            grid.DataSource = Value;
            grid.DataBind();
        }

        public override void DataBind()
        {
            grid.DataBind();
            base.DataBind();

            cDdlProvidencia1.Id_providencia = 0;
            cDdlProvidencia1.Focus();
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
    }
}