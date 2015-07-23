using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Medusa.BLL;
using Medusa.DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Medusa.LIB;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.UI.HtmlControls;

namespace Medusa
{
    public partial class Home : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                this.txtLogin.Focus();
                UsuarioFuspBLL u = new UsuarioFuspBLL();
                BulletedList1.DataSource = u.Ramais();
                BulletedList1.DataBind();
                BulletedList2.DataSource = u.RamaisPorSetor();
                BulletedList2.DataBind();


            }
        }


       

         
        protected void btEntrar_Click(object sender, EventArgs e)
        {
            string saida = String.Empty;
            string url = String.Empty;
            Session["id_usuario"] = SecurityBLL.Login(this.txtLogin.Text, this.txtSenha.Text, out saida, out url);
            Session.Timeout = 60;
            this.lblMsg.Text = saida;

            if ((int)Session["id_usuario"] != 0)
            {
                var usu = SecurityBLL.GetCurrentUsuario();
                
                FormsAuthentication.RedirectFromLoginPage(usu.PessoaFisica.nome, false);
                if (usu.primeiro_acesso)
                    Response.Redirect("AlterarSenha.aspx");
                else
                    Response.Redirect(url);
            }
        }
        
    }



 
}