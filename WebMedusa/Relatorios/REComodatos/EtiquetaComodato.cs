using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;
using System.Data.Entity;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using System.Text;

namespace Medusa.Relatorios.REComodatos
{
    public class EtiquetaComodato
    {
        public string codigoComodato { get; set; }

        public byte[] barcode { get; set; }

        public byte[] logo { get; set; }

        //private ComodatoBLL comBLL = new ComodatoBLL();
        public EtiquetaComodato()
        {
 
        }

        public EtiquetaComodato(Patrimonio p)
        {
            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.StartStopText = true;
            code128.Code = String.Format("{0:0000/}",  p.num_patrimonio);
            code128.BarHeight = 60;

            System.Drawing.Image bc = code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

            MemoryStream ms = new MemoryStream();
            bc.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            barcode = ms.ToArray();
            ms.Flush();
            ms.Dispose();

            if (p.Comodato.Projeto.Financiadores.FirstOrDefault() != null)
            {   
                if (!String.IsNullOrEmpty(p.Comodato.Projeto.Financiadores.FirstOrDefault().Financiador.logotipo))
                {
                    var webClient = new WebClient();
                    var image = System.Web.HttpContext.Current.Server.MapPath(p.Comodato.Projeto.Financiadores.FirstOrDefault().Financiador.fileName);
                    logo = webClient.DownloadData(image);
                }

                codigoComodato = String.Format("{0:0000/}", p.num_patrimonio);
            }
           
        }

        public IEnumerable<EtiquetaComodato> GetEtiquetas(int de, int ate)
        {
            var ctx = new Contexto();
            var l = new List<EtiquetaComodato>();
            foreach (var item in ctx.Patrimonios.Where(it => it.id_comodato >= de & it.id_comodato <= ate).OrderBy(it => it.id_patrimonio).ToList())
                l.Add(new EtiquetaComodato(item));
            return l;
        }

        public IEnumerable<EtiquetaComodato> GetEtiquetas(IEnumerable<Patrimonio> patrimonios)
        {
            var ctx = new Contexto();
            return (from c in patrimonios
                    select new EtiquetaComodato()
                    {
                        codigoComodato = Convert.ToString(c.id_patrimonio),
                    }).ToList();
        }
    }
}