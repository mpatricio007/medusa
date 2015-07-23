using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medusa.Controles
{
    public partial class Email : System.Web.UI.UserControl
    {
        public DAL.Email Value
        {
            get
            {
                return new DAL.Email(txt.Text);
            }
            set
            {
                value = value ?? new Medusa.DAL.Email();
                txt.Text = value.value;

            }
        }

        public string Text
        {
            get
            {
                return txt.Text;
            }
            set
            {
                txt.Text = value;
            }
        }

        public bool EnableValidator
        {
            get
            {
                return rfv.Enabled;
            }
            set
            {
                rfv.Enabled = value;

            }
        }

        public string ValidationGroup
        {
            get
            {
                return rfv.ValidationGroup;
            }
            set
            {
                rex.ValidationGroup = value;
                rfv.ValidationGroup = value;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void Focus()
        {
            this.txt.Focus();
        }
    }
}