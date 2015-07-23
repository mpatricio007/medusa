using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.BLL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Medusa.LIB
{
    public class BaseControl : UserControl
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
            if (!Page.IsPostBack)
            {
                Security();
            }
        }

        protected virtual void Security()
        {
            if (Seguranca.Value)
            {
                if (SecurityBLL.GetCurrentUsuario().id_usuario != 0)
                {
                    bool bGravacao = false;
                    if (!SecurityBLL.GetPermission(this.ToString(), out bGravacao))
                    {
                        try
                        {
                            var div = (HtmlGenericControl)FindControl("dConteudo");
                            div.InnerHtml = "<div class=\"ui-state-error ui-corner-all\"><p><strong>Alerta:</strong> Acesso Negado!</p></div>";
                            base.DataBind();
                        }
                        catch (Exception)
                        {                            
                            // throw;
                        }
                        
                    }

                    if(!bGravacao)
                    {
                        int i = 0;
                        string divId = "dGravacao";
                        while (true)
                        {
                            var div = (HtmlGenericControl)FindControl(i == 0 ? divId : divId + Convert.ToString(i));
                            if (div != null)
                            {
                                div.Attributes["class"] = "invisible";
                                i += 1;
                            }
                            else
                                break;
                        }
                    }
                }
            }
        }
    }
}
