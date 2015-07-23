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
    public partial class DdlContato : System.Web.UI.UserControl
    {
        public int Id_contato
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
                var cBLL = new ContatoBLL();
                lista.DataSource = from c in cBLL.GetAll("PessoaFisica.nome").OfType<Contato>()
                    select new
                        {
                            c.PessoaFisica.nome,
                            c.id_contato
                        };
                lista.Items.Insert(0, new ListItem("selecione um contato...", "0"));
                lista.DataBind();
            }
        }

        protected void lista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}