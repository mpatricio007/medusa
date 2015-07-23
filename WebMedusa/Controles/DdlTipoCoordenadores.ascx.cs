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
    public partial class DdlTipoCoordenadores : System.Web.UI.UserControl  
    {
        public TipoCoordenador Value
        {
            get
            {
                return (TipoCoordenador)Enum.Parse(typeof(TipoCoordenador),lista.SelectedValue);
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
                lista.DataSource = Enum.GetNames(typeof(TipoCoordenador));
                lista.Items.Insert(0, new ListItem("selecione um tipo...", "0"));
                lista.DataBind();
            }
        }
    }
}