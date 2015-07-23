using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;

namespace Medusa.Controles
{
    public partial class ControleCNPJ : System.Web.UI.UserControl
    {

        // Delegate
        public delegate void TextChangedChangedEventHandler(object sender, System.EventArgs e);
        public event TextChangedChangedEventHandler TextChanged;

        public CNPJ Value
        {
       
            get
            {
                return new CNPJ(this.txtCnpj.Text.Replace(".", "").Replace("-", "").Replace("/",""));
            }
            set
            {
                if (value == null)
                    value = new CNPJ();
                this.txtCnpj.Text = value.ToString();
            }
        }

        public override void Focus()
        {
            txtCnpj.Focus();
        }

        private bool isValid;

        public bool IsValid
        {
            get
            {
                cvCnpj_ServerValidate(null, null);
                return isValid;
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

        public bool Enable
        {
            get
            {
                return txtCnpj.Enabled;
            }
            set
            {
                txtCnpj.Enabled = value;

            }
        }


        public string ValidationGroup
        {
            get
            {
                return cvCnpj.ValidationGroup;
            }
            set
            {
                cvCnpj.ValidationGroup = value;
                txtCnpj.ValidationGroup = value;

            }
        }

        public bool AutoPostBack
        {
            get
            {
                return txtCnpj.AutoPostBack;
            }
            set
            {
                txtCnpj.AutoPostBack = value;
            }
        }

         protected void cvCnpj_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CNPJ cnpj = new CNPJ(this.txtCnpj.Text);
            //args.IsValid = cnpj.IsValid;
            //isValid = args.IsValid;
            isValid = cnpj.IsValid;          

        }         

         protected void txtCnpj_TextChanged(object sender, EventArgs e)
         {
             if (TextChanged != null) TextChanged(sender, e);
         }
    }
}