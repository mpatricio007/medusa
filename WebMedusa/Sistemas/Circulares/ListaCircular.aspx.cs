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
using System.Net;

namespace Medusa.Sistemas.Circulares
{
    public partial class ListaCircular : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LerCircular();
        }

        protected void LerCircular()
        {
            string e_mail = Request.QueryString["p2"].OldDesCriptografar();
            CorrespondenciaEmailBLL ceBLL = new CorrespondenciaEmailBLL();     
            if (ceBLL.Existe(Request.QueryString["p1"].OldDesCriptografar())) 
            {
                if (ceBLL.ExisteDestinario(e_mail))
                {
                    ceBLL.ConfirmarLeituraDestinatario(e_mail);
                    DestinatarioEmailBLL deBLL = new DestinatarioEmailBLL();                    
                    var correspondencia = ceBLL.ObjEF.Correspondencia;            
                    string pdfPath = Server.MapPath(correspondencia.fileName); 
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(pdfPath);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }
            }
        }

     
        
       
    }
}