using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Security;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class PaginaLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btEntrar_Click(object sender, EventArgs e)
        {
            string url = String.Empty;
            string saida = String.Empty;
            Session["id_usuario"] = SecurityBLL.Login(this.cTextoLogin.Text, this.cTextoSenha.Text, out saida, out url);
            Session.Timeout = 60;
            this.lblMsg.Text = saida;

            if ((int)Session["id_usuario"] != 0)
            {
                var usu = SecurityBLL.GetCurrentUsuario();
                FormsAuthentication.RedirectFromLoginPage(usu.PessoaFisica.nome, false);
                if (usu.primeiro_acesso)
                    Response.Redirect("AlterarSenha.aspx");
                else
                    Response.Redirect("~/Sistemas/PessoaJuridica/Fornecedores.aspx");

            }
        }
    }
}