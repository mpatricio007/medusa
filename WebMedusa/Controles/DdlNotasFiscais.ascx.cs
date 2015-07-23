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
    public partial class DdlNotasFiscais : System.Web.UI.UserControl
    {
        public int? Id_nf_material
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
                NfMaterialBLL nf = new NfMaterialBLL();
                lista.DataSource = nf.GetAll("nomeEmpresa");
                lista.Items.Insert(0, new ListItem("selecione uma nota fiscal...", "0"));
                lista.DataBind();
            }
        }
    }
}