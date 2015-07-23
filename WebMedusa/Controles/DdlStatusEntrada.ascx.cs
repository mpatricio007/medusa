using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.LIB;

namespace Medusa.Controles
{
    public partial class DdlStatusEntrada : System.Web.UI.UserControl
    {
        public int? Id_status_entrada
        {
            get
            {
                return Util.StringToInteiro(lista.SelectedValue);
            }
            set
            {
                lista.SelectedValue = Util.InteiroToString(value);
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
                StatusEntradaBLL se = new StatusEntradaBLL();
                lista.DataSource = se.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione um status...", "0"));
                lista.DataBind();
            }
        }
    }
}