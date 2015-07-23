using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlAno : System.Web.UI.UserControl
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
                lista.Items.Insert(0, new ListItem("selecione um ano...", "0"));
                for (int i = DateTime.Now.Year - 10; i < DateTime.Now.Year + 10; i++)                
                    lista.Items.Add(new ListItem(i.ToString()));
                lista.DataBind();
            }
        }
    }
}