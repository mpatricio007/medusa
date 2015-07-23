using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlTipoSolicitacao : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void SelectedIndexChangedEventHandler(object sender, System.EventArgs e);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;



        public Int32 Id_tipo_solicitacao
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

        public bool Enabled
        {
            get
            {
                return lista.Enabled;
            }
            set
            {
                lista.Enabled = value;
            }
        }

        public bool AutoPostBack
        {
            get
            {
                return lista.AutoPostBack;
            }
            set
            {
                lista.AutoPostBack = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TipoSolicitacaoBLL ts = new TipoSolicitacaoBLL();
                lista.DataSource = ts.GetAll("nome");
                lista.Items.Insert(0, new ListItem("selecione um tipo de solicitação...", "0"));
                lista.DataBind();

                
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(sender, e);
        }
    }
}