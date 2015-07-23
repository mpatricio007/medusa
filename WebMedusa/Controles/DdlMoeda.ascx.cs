using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlMoeda : System.Web.UI.UserControl
    {
        public int Id_moeda
        {
            get
            {
                return Convert.ToInt16(lista.SelectedValue);
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
                MoedaBLL m = new MoedaBLL();
                lista.DataSource = m.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione uma moeda...", "0"));
                lista.DataBind();
            }
        }
    }
}