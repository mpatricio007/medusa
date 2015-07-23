using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.Controles
{
    public partial class DdlUsuariosFuspEntradas : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void SelectedIndexChangedEventHandler(object sender, System.EventArgs e);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;


        public int Id_usuario
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

        public bool EnableValidator
        {
            get
            {
                return lista.CausesValidation;
            }
            set
            {
                lista.CausesValidation = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lista.Items.Clear();
                UsuarioFuspBLL u = new UsuarioFuspBLL();
                lista.DataSource = u.GetAllUsersEntrada();
                lista.Items.Insert(0, new ListItem("selecione um usuario...", "0"));
                lista.DataBind();
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }

        public override void DataBind()
        {
            lista.SelectedIndex = 0;
            lista.DataBind();
            base.DataBind();
        }
    }
}