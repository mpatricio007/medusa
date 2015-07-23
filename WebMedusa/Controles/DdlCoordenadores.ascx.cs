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
    public partial class DdlCoordenadores : System.Web.UI.UserControl
    {
            public int Id_coordenador
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
                var coorBLL = new CoordenadorBLL();
                lista.DataSource = from c in coorBLL.GetAll("PessoaFisica.nome").OfType<Coordenador>()
                                   select new
                                       {
                                           c.PessoaFisica.nome,
                                           c.id_coordenador
                                       };                                   

                lista.Items.Insert(0, new ListItem("selecione um coordenador...", "0"));
                lista.DataBind();
            }
        }
    }
}