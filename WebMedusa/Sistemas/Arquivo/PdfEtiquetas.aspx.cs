using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Medusa.LIB;
using System.IO;
using Medusa.DAL;

namespace Medusa.Sistemas.Arquivo
{
    public partial class PdfEtiquetas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VolumeBLL volBLL = new VolumeBLL();
            var ds = volBLL.GetVolumesDeAte(Convert.ToInt32(Session["de"]), Convert.ToInt32(Session["ate"]));
            //Contexto ctx = new Contexto();
            //var ds = ctx.Pessoas.OfType<UsuarioFusp>().OrderBy(it => it.nome).ToList();
            if (ds.Count() > 3)
            {
                var ms = new MemoryStream();
                Document doc = new Document(PageSize.LETTER);//, 10, 10, 40, 15);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                PdfPTable tabela = new PdfPTable(3) { WidthPercentage = 100, HorizontalAlignment = 1 };
                tabela.DefaultCell.PaddingTop = 7;// Convert.ToInt32(Session["top"]);       //10;//5;//7;
                tabela.DefaultCell.PaddingBottom = 7; // Convert.ToInt32(Session["bottom"]);        //10;//5;//5;
                tabela.DefaultCell.PaddingLeft = 58;// Convert.ToInt32(Session["left"]);      //55;
                tabela.DefaultCell.PaddingRight = 58;// Convert.ToInt32(Session["right"]);       //55;
                tabela.DefaultCell.Border = 0;

                for (int i = 0; i < ds.Count(); i++)
                {
                    Barcode128 code128 = new Barcode128();
                    code128.CodeType = Barcode.CODE128;
                    code128.ChecksumText = true;
                    code128.GenerateChecksum = true;
                    code128.StartStopText = true;
                    code128.Code = Convert.ToString(ds[i]);//.cpf);
                    code128.BarHeight = 60;// 54; // 60;

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(
                        code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White),
                        System.Drawing.Imaging.ImageFormat.Gif);

                    var bodyFont = FontFactory.GetFont("Arial",10, Font.NORMAL);

                    PdfPTable tab = new PdfPTable(1);
                    tab.DefaultCell.Border = 0;
                    tab.DefaultCell.HorizontalAlignment = 1;
                    tab.AddCell(img);

                    tab.AddCell(new Phrase(Convert.ToString(ds[i]/*.nome.Length > 12 ? ds[i].nome.Substring(0,12) : ds[i].nome*/), bodyFont));
                    tabela.AddCell(tab);
                }
                doc.Add(tabela);
                doc.Close();
                
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
            }
            else
                Util.ShowMessage("Poucas etiquetas para impressão!");
        }
    }
}