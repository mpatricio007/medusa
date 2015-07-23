using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;

namespace Medusa.Controles
{
    public partial class ControleCPF : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void TextChangedChangedEventHandler(object sender, System.EventArgs e);
        public event TextChangedChangedEventHandler TextChanged;

        public CPF Value
        {
            get
            {
                return new CPF(this.txtCpf.Text.Replace(".", "").Replace("-", ""));
            }
            set
            {
                if (value == null)
                    value = new CPF();
                this.txtCpf.Text = value.ToString();
            }
        }

        private bool isValid;

        public bool IsValid
        {
            get 
            {
                cvCpf_ServerValidate(null, null);
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

        public bool Enabled
        {
            get
            {
                return txtCpf.Enabled;
            }
            set
            {
                txtCpf.Enabled = value;
            }
        }

        public string ValidationGroup
        {
            get
            {
                return cvCpf.ValidationGroup;
            }
            set
            {
                cvCpf.ValidationGroup = value;
                rfv.ValidationGroup = value;
            }
        }

        public bool AutoPostBack
        {
            get
            {
                return txtCpf.AutoPostBack;
            }
            set
            {
                txtCpf.AutoPostBack = value;
            }
        }

        public override void Focus()
        {
            txtCpf.Focus();
        }

        protected void cvCpf_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CPF cpf = new CPF(this.txtCpf.Text);            
            isValid = cpf.IsValid;          
        }

        protected void txtCpf_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null) TextChanged(sender, e);
        }

      

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}