using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using iTextSharp.text.pdf;
using System.IO;
using Medusa.DAL;

namespace Medusa.Relatorios.Arquivo
{
    public class EtiquetaArquivo
    {
        public int codigoVolume { get; set; }

        public byte[] barcode { get; set; }
        
        public EtiquetaArquivo()
            {

            }

    public EtiquetaArquivo(Volume v)
        {
            Barcode128 code128 = new Barcode128();
            code128.CodeType = Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.StartStopText = true;
            code128.Code = String.Format("{0:000000000000}", v.id_volume);
            code128.BarHeight = 60;

            System.Drawing.Image bc = code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

            MemoryStream ms = new MemoryStream();
            bc.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            barcode = ms.ToArray();
            ms.Flush();
            ms.Dispose();
            codigoVolume =  (Int32)v.id_volume;
        }

    public IEnumerable<EtiquetaArquivo> GetEtiquetas(int de, int ate)
        {
            var ctx = new Contexto();
            var l = new List<EtiquetaArquivo>();
            foreach (var item in ctx.Volumes.Where(it => it.id_volume >= de & it.id_volume <= ate).ToList())
                l.Add(new EtiquetaArquivo(item));
            return l;
        }

    public IEnumerable<EtiquetaArquivo> GetEtiquetas(IEnumerable<Volume> volumes)
        {
            var ctx = new Contexto();
            return (from v in volumes
                    select new EtiquetaArquivo()
                    {
                        codigoVolume = v.id_volume,
                    }).ToList();
        }
    }
}
