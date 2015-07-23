using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;

namespace Medusa.Sistemas.Comum
{
    public partial class BarCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             string code = Request.QueryString["code"];
             if (!String.IsNullOrEmpty(code))
             {
                 Barcode128 code128 = new Barcode128();
                 code128.CodeType = Barcode.CODE128;
                 code128.ChecksumText = true;
                 code128.GenerateChecksum = true;
                 code128.StartStopText = true;                 
                 code128.Code = code;
                 code128.BarHeight = 60;                 
                 System.Drawing.Image bc = code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                 Response.ContentType = "image/gif";
                 bc.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
             }
        }
    }
}