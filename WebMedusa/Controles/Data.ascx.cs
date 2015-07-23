using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using System.Text;

namespace Medusa.Controles
{
    public partial class Data : System.Web.UI.UserControl
    {
        public DateTime? Value
        {
            get 
            {               
                return Util.StringToDate(txtData.Text);
            }
            set 
            {                
                txtData.Text = Util.DateToString(value);                
            }
        }

        public string Text
        {
            get
            {
                return txtData.Text;
            }
            set
            {
                txtData.Text = value;

            }
        }


        public bool EnableValidator
        {
            get
            {
                return rfvData.Enabled;
            }
            set
            {
                rfvData.Enabled = value;
                      
            }
        }

        public string ValidationGroup
        {
            get
            {
                return rfvData.ValidationGroup;
            }
            set
            {
                rfvData.ValidationGroup = value;

            }
        }

        public string ErrorMsg
        {
            get
            {
                return rfvData.ErrorMessage;
            }
            set
            {
                rfvData.EnableTheming = false;
                rfvData.ForeColor = System.Drawing.Color.Red;
                rfvData.ErrorMessage = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return txtData.Enabled;
            }
            set
            {
                //txtData.ReadOnly = value;
                txtData.Enabled = value;
                EnableValidator = value;
            }
        }

        public int Width
        {
            get
            {
                return Convert.ToInt16(txtData.Width);
            }
            set
            {

                txtData.Width = value;

            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string script = " function $('.date').datepicker(); $('.date').dateEntry();";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "date", script, true);
            }
        }
    }
}