using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlEventos : System.Web.UI.UserControl
    {
        public int Id_evento
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

        public string Text
        {
            get
            {
                return lista.SelectedItem.Text;
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
                EventoBLL ev = new EventoBLL();
                lista.DataSource = ev.GetAll("id_evento");
                lista.Items.Insert(0, new ListItem("selecione um evento...", "0"));
                lista.DataBind();
            }
        }

    }
}