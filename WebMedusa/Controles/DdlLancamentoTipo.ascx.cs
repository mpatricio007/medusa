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
    public partial class DdlLancamentoTipo : System.Web.UI.UserControl
    {
        // Delegate
        public delegate void SelectedIndexChangedEventHandler(object sender, System.EventArgs e);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;


        public int Id_lancto_tipo
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

        public bool CausesValidation
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
                LancamentoTipoBLL ltBLL = new LancamentoTipoBLL();
                lista.DataSource = ltBLL.GetAll("nome").OfType<LancamentoTipo>().Where(it=>it.status).ToList();
                lista.Items.Insert(0, new ListItem("selecione um tipo de lançamento...", "0"));
                lista.DataBind();
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }
    }
}