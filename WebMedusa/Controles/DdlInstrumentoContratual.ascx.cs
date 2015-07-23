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
    public partial class DdlInstrumentoContratual : System.Web.UI.UserControl
    {
        public int? Id_instrumento_contratual
        {
            get
            {
                return Util.StringToInteiroOrNullable(lista.SelectedValue);
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
                var i = new InstrumentoContratualBLL();
                lista.DataSource = i.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione um instrumento contratual...", "0"));
                lista.DataBind();
            }
        }
    }
}