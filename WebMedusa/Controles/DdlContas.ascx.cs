using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlContas : System.Web.UI.UserControl
    {
        public int Id_Conta
        {
            get
            {
                return Convert.ToInt32(lista.SelectedValue);
            }
            set
            {
                lista.SelectedValue = Convert.ToString(value);
            }
        }

        public bool EnableValidator
        {
            get
            {
                return cv.Enabled;
            }
            set
            {
                cv.Enabled = value;

            }
        }


        public string ValidationGroup
        {
            get
            {
                return cv.ValidationGroup;
            }
            set
            {                
                cv.ValidationGroup = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ContaBLL  c = new ContaBLL();
                lista.DataSource = c.GetAll("numero");
                lista.Items.Insert(0, new ListItem("selecione uma conta...", "0"));
                lista.DataBind();
            }
        }
    }
}