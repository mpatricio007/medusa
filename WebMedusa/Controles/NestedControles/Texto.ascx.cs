using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Medusa.LIB;

namespace Medusa.Controles
{
    public partial class Texto : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void TextChangedEventHandler(object sender, System.EventArgs e);
        public event TextChangedEventHandler TextChanged;

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

        public bool Enable
        {
            get
            {
                return txt.Enabled;
            }
            set
            {
                txt.Enabled = value;
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

        public Unit Width
        {
            get
            {
                return txt.Width;
            }
            set
            {
                txt.Width = value;
            }
        }

        public Unit Height
        {
            get
            {
                return txt.Height;
            }
            set
            {
                txt.Height = value;
            }
        }

        public string ErrorMsg
        {
            get
            {
                return rfv.ErrorMessage;
            }
            set
            {
                rfv.EnableTheming = false;
                rfv.ForeColor = System.Drawing.Color.Red;
                rfv.ErrorMessage = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return txt.MaxLength;
            }
            set
            {
                txt.MaxLength = value;
            }
        }

        public TextBoxMode TextMode
        {
            get
            {
                return txt.TextMode;
            }
            set
            {
                txt.TextMode = value;
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
                rfv.ValidationGroup = value;
            }
        }

        public string CssClass
        {
            get
            {
                return txt.CssClass;
            }
            set
            {
                txt.CssClass = value;
            }
        }

        public bool AutoPostBack
        {
            get
            {
                return txt.AutoPostBack;
            }
            set
            {
                txt.AutoPostBack = value;
            }
        }

        public short TabIndex
        {
            get
            {
                return txt.TabIndex;
            }
            set
            {
                txt.TabIndex = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((TextMode == TextBoxMode.MultiLine)&(MaxLength != 0))
            {
                var script = new StringBuilder();
                script.AppendFormat("var txt = document.getElementById('{0}');", txt.ClientID);
                script.Append("if(txt != null){");
                script.AppendFormat("$('#{0}').limit('{1}');", txt.ClientID, MaxLength);
                script.Append("}");
                Util.ChamarScript(script.ToString(), txt.ClientID);
            }
        }
        public override void Focus()
        {
            this.txt.Focus();
        }

        protected void txt_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null) TextChanged(sender, e);
        }
    }
}