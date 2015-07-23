using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using System.IO;

namespace Medusa.BLL
{
    public class EditalBLL : AbstractCrudWithLog<Edital>
    {
        public Upload up { get; set; }

        public override void Add()
        {
            ObjEF.data_publicacao = DateTime.Now;
            base.Add();
            if (up.file != null)
            {
                if (base.SaveChanges())
                {
                    saveFile();
                    base.Update();
                }
            }
        }

        public override void Delete()
        {
            deleteFile();
            base.Delete();
        }

        public override void Update()
        {
            deleteFile();
            saveFile();
            base.Update();
        }   

        protected void saveFile()
        {
            if (up.file != null)
            {
                up.where_upload = System.Web.HttpContext.Current.Server.MapPath("\\editais\\");
                up.fileName = Convert.ToString(ObjEF.id_edital);
                if (up.Do())
                    ObjEF.edital_link = String.Format(@"<a href='../../editais/{0}{1}' target='blank'>{2}</a>", up.fileName, up.fileExtension,ObjEF.titulo);
            }
        }

        protected void deleteFile()
        {
            if (!String.IsNullOrEmpty(ObjEF.edital_link))
            {
                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\editais\\")).
                    Where(it => it.Contains(Convert.ToString(ObjEF.id_edital))).ToArray();
                foreach (string f in files)
                    File.Delete(f);
            }
        }
    }
}