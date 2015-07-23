using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using System.Web.Security;

namespace Medusa
{
    public partial class AlterarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SecurityBLL.GetCurrentUsuario().id_usuario == 0)
                    FormsAuthentication.RedirectToLoginPage();
                
            }
        }

        protected void btAlterarSenha_Click(object sender, EventArgs e)
        {
            var usuBLL = new UsuarioFuspBLL();
            this.lbLog.Text = SecurityBLL.AlterarSenha(this.txtSenha.Text, this.txtNewSenha.Text);
        }
    }
}