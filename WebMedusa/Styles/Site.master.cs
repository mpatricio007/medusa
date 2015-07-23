using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;

namespace Medusa.Styles
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var usu = SecurityBLL.GetCurrentUsuario();
                if (usu.id_usuario != 0)
                {
                    ddlSistemas.DataTextField = "nome";
                    ddlSistemas.DataValueField = "id_sistema";
                    ddlSistemas.DataSource = usu.UsuarioSistema.Select(it => it.Sistema).OrderBy(it => it.nome);
                    ddlSistemas.Items.Insert(0, new ListItem("selecione um sistema...", "0"));
                    ddlSistemas.DataBind();

                    ddlSistemas.SelectedValue = Convert.ToString(SecurityBLL.GetCurrentSistema());
                }
                else
                    Response.Redirect("~/Home.aspx");
            }
        }

         protected void btEntrar_Click(object sender, EventArgs e)
        {
            //Session["id_sistema"] = Convert.ToInt32(ddlSistemas.SelectedValue);
            //SistemaBLL s = new SistemaBLL();
            //Response.Redirect(s.ResponseUrl(Convert.ToInt32(ddlSistemas.SelectedValue)));
        }

         protected void ddlSistemas_SelectedIndexChanged(object sender, EventArgs e)
         {
             Session["id_sistema"] = Convert.ToInt32(ddlSistemas.SelectedValue);
             SistemaBLL s = new SistemaBLL();
             Response.Redirect(s.ResponseUrl(Convert.ToInt32(ddlSistemas.SelectedValue)));
         }
    }
}