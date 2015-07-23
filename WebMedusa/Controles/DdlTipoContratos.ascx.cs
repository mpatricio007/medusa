using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlTipoContratos : System.Web.UI.UserControl
    {
        public int Id_tipo_contrato
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
                TipoContratoBLL tc = new TipoContratoBLL();
                lista.DataSource = tc.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione um tipo de contrato...", "0"));
                lista.DataBind();
            }
        }
    }
}