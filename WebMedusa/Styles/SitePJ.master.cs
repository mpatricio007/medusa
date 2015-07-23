using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Styles
{
    public partial class SitePJ : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userBLL = new UsuarioBLL();
                userBLL.Get(SecurityBLL.GetCurrentUsuario().id_usuario);
                ddlSistemas.DataTextField = "nome";
                ddlSistemas.DataValueField = "id_sistema";
                ddlSistemas.DataSource = userBLL.ObjEF.UsuarioSistema.Select(it => it.Sistema).OrderBy(it => it.nome);
                ddlSistemas.DataBind();
                ddlSistemas.SelectedValue = Convert.ToString(Session["id_sistema"]);
            }
        }

         protected void btEntrar_Click(object sender, EventArgs e)
        {
            Session["id_sistema"] = Convert.ToInt32(ddlSistemas.SelectedValue);
            SistemaBLL s = new SistemaBLL();
            Response.Redirect(s.ResponseUrl(Convert.ToInt32(ddlSistemas.SelectedValue)));
        }
    }
}