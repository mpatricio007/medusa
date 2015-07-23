using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;

namespace Medusa.Controles
{
    public partial class Telefone : System.Web.UI.UserControl
    {
        public DAL.Telefone Value
        {
            get
            {
                return new DAL.Telefone(txt.Text, txtRamal.Text, (DAL.TipoTelefone)Enum.Parse(typeof(DAL.TipoTelefone), ddlTipo.SelectedValue));
            }
            set
            {
                txt.Text = value.value;
                txtRamal.Text = value.ramal;
                
                if(value.tipo.HasValue)
                    ddlTipo.SelectedValue = Enum.GetName(typeof(DAL.TipoTelefone), value.tipo);  

            }
        }

        ////public string Ramal
        //{
        //    get
        //    {
        //        return txtRamal.Text;
        //    }
        //    set
        //    {
        //        txtRamal.Text = value;
        //    }
        //}

        //public DAL.TipoTelefone Tipo
        //{
        //    get
        //    {

        //        return (DAL.TipoTelefone)Enum.Parse(typeof(DAL.TipoTelefone), ddlTipo.SelectedValue);
        //    }
        //    set
        //    {
        //        ddlTipo.SelectedValue = Enum.GetName(typeof(DAL.TipoTelefone), value);
        //    }
        //}

        public bool EnableValidator
        {
            get
            {
                return rfv.Enabled;
            }
            set
            {
                rfv.Enabled = value;

            }
        }

        public string ValidationGroup
        {
            get
            {
                return rfv.ValidationGroup;
            }
            set
            {                
                rfv.ValidationGroup = value;

            }
        }

        public override void Focus()
        {
            this.txt.Focus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTipo.DataSource = Enum.GetNames(typeof(DAL.TipoTelefone));
                ddlTipo.DataBind();
            }
        }
    }
}