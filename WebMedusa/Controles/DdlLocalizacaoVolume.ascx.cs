using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlLocalizacaoVolume : System.Web.UI.UserControl
    {
        public string Id_localizacao
        {
            get
            {
                return lista.SelectedValue;
            }
            set
            {
                lista.SelectedValue = value;
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
                LocalizacaoVolumeBLL l = new LocalizacaoVolumeBLL();
                lista.DataSource = l.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione uma localização do volume...", "0"));
                lista.DataBind();
            }
        }
    }
}