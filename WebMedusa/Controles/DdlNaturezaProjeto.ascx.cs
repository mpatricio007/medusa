using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlNaturezaProjeto : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void SelectedIndexChangedEventHandler(object sender, System.EventArgs e);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

        public int Id_nat_projeto
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

        public bool AutoPostBack
        {
            get
            {
                return lista.AutoPostBack;
            }
            set
            {
                lista.AutoPostBack = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NaturezaProjetoBLL n = new NaturezaProjetoBLL();
                lista.DataSource = n.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione uma natureza de projeto...", "0"));
                lista.DataBind();
                this.Attributes.Add("Event", "SelectedIndexChanged");
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(sender, e);
        }
    }
}