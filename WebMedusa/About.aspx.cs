using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.Relatorios.Projeto;
using Microsoft.Reporting.WebForms;
using Medusa.LIB;
using Medusa.Relatorios.REComodatos;
using System.IO;
using Medusa.BLL;

namespace Medusa
{
    public partial class About : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    //var ctx = new Contexto();///
        //    //var ds = ctx.Cartas.Where(it => it.arquivo != null ).ToList();
        //    string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\correspondencias\\"));
        //    foreach (var item in files)
        //    {
        //        //string[] files2 = files.Where(it => it.Contains(Convert.ToString(item.id_correspondencia))).ToArray();
        //        //if (files2.Count() > 0)
        //        //{
              


        //            var stream = new StreamReader(item);

        //            var arqBLL = new ArquivoBLL();
        //            using (BinaryReader br = new BinaryReader(stream.BaseStream))
        //                arqBLL.bytes = br.ReadBytes((Int32)stream.BaseStream.Length);
        //            arqBLL.contentType = GetContentTypeByExtension(item.Split('.')[1]);
        //            if (!arqBLL.SendFile(item))
        //                break;
        //            //item.id_arquivo = arqBLL.ObjEF.id_arquivo;
        //        //}
                
        //    }
        //    //ctx.SaveChanges();
        //}


        //public string GetContentTypeByExtension(string strExtension)
        //{
        //    switch (strExtension)
        //    {
        //        case "pdf":
        //            return "application/pdf";
        //        case "doc":
        //            return "application/msword";
        //        case "docx":
        //            return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //        case "xls":
        //            return "application/vnd.ms-excel";
        //        case "xlsx":
        //            return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        case "dotx":
        //            return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
        //        default:
        //            return "";
        //    }


        //}
    }
}
