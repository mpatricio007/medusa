using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Controles
{
    public partial class DdlPossivelProvidencia : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void SelectedIndexChangedEventHandler(object sender, System.EventArgs e);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

        public int Id_status_atual { get; set; }

        public int Id_posivel_providencia
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
                SetLista();
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }

        public override void DataBind()
        {
            base.DataBind();
            SetLista();
        }

        public void SetLista()
        {
            lista.Items.Clear();
            PossivelProvidenciaBLL ppBLL = new PossivelProvidenciaBLL();
            lista.DataSource = ppBLL.GetAllPossiveis(Id_status_atual);
            lista.Items.Insert(0, new ListItem("selecione uma providencia...", "0"));
            lista.DataBind();
        }

    }
}