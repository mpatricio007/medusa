using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlSetor : System.Web.UI.UserControl
    {
        public int Id_setor
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
                return cvSetor.ValidationGroup;
            }
            set
            {               
                cvSetor.ValidationGroup = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetorBLL s = new SetorBLL();
                lista.DataSource = s.GetAll("nome");
                lista.Items.Insert( 0, new ListItem("selecione um setor...", "0"));
                lista.DataBind();
            }
        }
    }
}