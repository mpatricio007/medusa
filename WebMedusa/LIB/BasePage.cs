using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Authentication;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Medusa.BLL;

namespace Medusa.LIB
{   
    public class BasePage : System.Web.UI.Page
    {
        private bool? seguranca;

        protected bool? Seguranca
        {
            get
            {
                if (!seguranca.HasValue)
                    seguranca = true;
                return seguranca;
            }
            set { seguranca = value; }
        }    


        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Seguranca.Value)
                {
                    if (SecurityBLL.GetCurrentUsuario().id_usuario != 0)
                    {
                        bool bGravacao = false;
                        if (!SecurityBLL.GetPermission(this.ToString(), out bGravacao))
                            Response.Redirect("~/AcessoNegado.aspx");

                        else
                        {

                            int i = 0;
                            string divId = "dGravacao";
                            while (true)
                            {
                                var cphMain = (ContentPlaceHolder)Page.Master.Master.FindControl("MainContentBaseMaster").FindControl("MainContent");
                                var div = (HtmlGenericControl)cphMain.FindControl(i == 0 ? divId : divId + Convert.ToString(i));
                                if (div != null)
                                {
                                    div.Visible = bGravacao;
                                    i += 1;
                                }
                                else
                                    break;
                            }
                        }

                    }
                    else
                        Response.Redirect("~/Home.aspx");
                }



            }
        }
    }
}