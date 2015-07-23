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
    public partial class DdlUsuarioFUSP : System.Web.UI.UserControl
    {
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
                return cvUsuario.ValidationGroup;
            }
            set
            {
                cvUsuario.ValidationGroup = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioFuspBLL s = new UsuarioFuspBLL();
                lista.DataSource = from u in s.GetAll("PessoaFisica.nome").OfType<UsuarioFusp>()
                                   where u.status == true
                                   select new { id_usuario = u.id_usuario, nome = u.PessoaFisica.nome };
                lista.Items.Insert(0, new ListItem("selecione um usuário...", "0"));
                lista.DataBind();
            }
        }
    }
}