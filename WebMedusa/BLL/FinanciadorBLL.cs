using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using System.Data;
using System.IO;
using System.Data.Entity;
namespace Medusa.BLL
{
    public class FinanciadorBLL : Abstract_Crud<Financiador>
    {
        public override void Update()
        {
            ObjEF.Emails.Where(it => it.id_email != 0).
                ToList().ForEach(it => _dbContext.Entry<FinanciadorEmail>(it).State = EntityState.Deleted);

            ObjEF.Telefones.Where(it => it.id_telefone != 0).
               ToList().ForEach(it => _dbContext.Entry<FinanciadorTelefone>(it).State = EntityState.Deleted);

            base.Update();
            deleteFile();
            saveFile();
        }

        private Upload up;

        public Upload Up
        {
            get
            {
                if (up == null)
                    up = new Upload();
                return up;
            }
            set { up = value; }
        }

        public override void Add()
        {
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

        protected void saveFile()
        {
            if (Up.file != null)
            {
                up.where_upload = System.Web.HttpContext.Current.Server.MapPath("\\financiadores\\");
                up.fileName = Convert.ToString(ObjEF.id_financiador);
                if (up.Do())
                    ObjEF.logotipo = String.Format(@"<a href='../../financiadores/{0}{1}' target='blank'>logotipo</a>", up.fileName, up.fileExtension);
            }
        }

        protected void deleteFile()
        {
            if (!String.IsNullOrEmpty(ObjEF.logotipo) & (Up.file != null))
            {

                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\comodatos\\")).
                    Where(it => it.Contains(Convert.ToString(ObjEF.id_financiador))).ToArray();
                foreach (string f in files)
                    File.Delete(f);
            }
        }
    }
}