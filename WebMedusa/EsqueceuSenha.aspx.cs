using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa
{
    public partial class EsqueceuSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btSendSenha_Click(object sender, EventArgs e)
        {
            var usuBLL = new UsuarioBLL();
            usuBLL.GetUsuarioFuspPorCpf(cCPF1.Value.Value);
            this.lbLog.Text = SecurityBLL.SendPasswordEmail(usuBLL.ObjEF);
        }
    }
}