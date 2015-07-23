using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Medusa.BLL;

namespace Medusa
{
    /// <summary>
    /// Summary description for WebData
    /// </summary>    
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]    
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebData : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string ProcessarArquivosFtpBB()
        {
            Session["id_usuario"] = SecurityBLL.GetIdUsuarioAutomatic();
            var arqRetornoBLL = new Medusa.BLL.ArquivosRetornoBLL();
            return arqRetornoBLL.ProcessarArquivosFtpBB();
        }

        [WebMethod(EnableSession = true)]
        public string ProcessarArquivosConciliacao()
        {
            Session["id_usuario"] = SecurityBLL.GetIdUsuarioAutomatic();
            var arqBB = new Medusa.BLL.LayoutBB.ExtratoBB();
            return arqBB.ProcessarArquivosFtpBB();
        }
    }
}
