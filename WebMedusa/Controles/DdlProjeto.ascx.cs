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
    public partial class DdlProjeto : System.Web.UI.UserControl
    {
        //delegate
        public delegate void SelectedIndexChangedEventHandler(object sender, System.EventArgs e);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

        public int Id_projeto
        {
            get
            {
                return Util.StringToInteiro(lista.SelectedValue).GetValueOrDefault();
            }
            set
            {
                lista.SelectedValue = Convert.ToString(value);
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

        public List<Projeto> DataSource
        {
            get
            {
                return (List<Projeto>)lista.DataSource;
            }
            set
            {
                lista.Items.Clear();
                lista.DataSource = value;
                lista.Items.Insert(0, new ListItem("selecione um projeto...", "0"));
                lista.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var p = new ProjetoBLL();
                lista.DataSource = p.GetAllDefinitivos();
                lista.Items.Insert(0, new ListItem("selecione um projeto...", "0"));
                lista.DataBind();
            }
        }

        public bool causesvalidation
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

        public override void Focus()
        {
            lista.Focus();
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(sender, e);
        }

        public override void DataBind()
        {
            lista.DataBind();
            base.DataBind();
        }
    }
}