using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlMes : System.Web.UI.UserControl
    {
        public int Value
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lista.Items.Insert(0, new ListItem("selecione um mês...", "0"));
                for (int i = 1; i <= 12; i++)                
                    lista.Items.Add(new ListItem(string.Format("{0}",i)));
                lista.DataBind();
            }
        }
    }
}