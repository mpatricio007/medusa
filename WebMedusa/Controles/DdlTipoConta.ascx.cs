using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlTipoConta : System.Web.UI.UserControl
    {
        public int Id_tipoConta
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
                ContaTipoBLL ct = new ContaTipoBLL();
                lista.DataSource = ct.GetAll("descricao");
                lista.Items.Insert(0, new ListItem("selecione um tipo de conta...", "0"));
                lista.DataBind();
            }
        }
    }
}