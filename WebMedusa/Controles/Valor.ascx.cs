using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;

namespace Medusa.Controles
{
    public partial class Valor : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void TextChangedEventHandler(object sender, System.EventArgs e);
        public event TextChangedEventHandler TextChanged;

        
        public Decimal? Value
        {
            get 
            {
                return Util.StringToDecimal(txtValor.Text);
            }
            set 
            {
                txtValor.Text = Util.DecimalToString(value);                
            }
        }

        public string Text
        {
            get
            {
                return txtValor.Text;
            }
            set
            {
                txtValor.Text = value;
            }
        }

        public bool EnableValidator
        {
            get
            {
                return rfvValor.Enabled;
            }
            set
            {
                rfvValor.Enabled = value;
                          
            }
        }

        public bool Enabled
        {
            get
            {
                return txtValor.Enabled;
            }
            set
            {
                txtValor.Enabled = value;

            }
        }

        public string CssClass
        {
            get
            {
                return txtValor.CssClass;
            }
            set
            {
                txtValor.CssClass = value;

            }
        }

        public string ValidationGroup
        {
            get
            {
                return rfvValor.ValidationGroup;
            }
            set
            {

                rfvValor.ValidationGroup = value;

            }
        }

        public Unit Width
        {
            get
            {
                return txtValor.Width;
            }
            set
            {
                txtValor.Width = value;

            }
        }

        public int MaxLength
        {
            get
            {
                return txtValor.MaxLength;
            }
            set
            {
                txtValor.MaxLength = value;

            }
        }

        public bool AutoPostBack
        {
            get
            {
                return txtValor.AutoPostBack;
            }
            set
            {
                txtValor.AutoPostBack = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Common.RegisterStyleSheet(StyleSheet, Common.GetHost() + "Shared/StyleSheet/jquery.css");
                //Common.RegisterStyleSheet(StyleSheet, Common.GetHost() + "Shared/StyleSheet/jquery.dateentry.css");
                //Common.RegisterStyleSheet(StyleSheet, Common.GetHost() + "Shared/StyleSheet/jquery.timeentry.css");
            }
        }


        protected void txt_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null) TextChanged(sender, e);
        }

       
    }
}