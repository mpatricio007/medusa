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
    public partial class DdlBeneficiario : System.Web.UI.UserControl
    {
        public int Id_beneficiario
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var beneBLL = new BeneficiarioBLL();
                lista.DataSource = from b in beneBLL.GetAll("PessoaFisica.nome").OfType<Beneficiario>()
                                   select new 
                                   {
                                       b.PessoaFisica.nome,
                                       b.id_beneficiario
                                   };
                lista.Items.Insert(0, new ListItem("selecione um beneficiario...", "0"));
                lista.DataBind();
            }
        }
    }
}