using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Web;

namespace Medusa.BLL
{
    public class CorrespondenciaBLL<T> : AbstractCrudWithLog<T> where T : Correspondencia, new()
    {
        public Upload up { get; set; }

        public override void Add()
        { 

            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.num = getNum();
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

        protected int getNum()
        {           
            return _dbSet.Where(c => c.data.Year == ObjEF.data.Year).Max(c=> (int?)c.num).GetValueOrDefault() + 1;
        }

        public bool MesmoAno()
        {
             return (ObjEF.data.Year == DateTime.Now.Year);
        }

        public bool MesmoUsuario()
        {
            return (ObjEF.id_usuario == SecurityBLL.GetCurrentUsuario().id_usuario);
        }

        protected void saveFile()
        {
            if (up.file != null)
            {
                up.where_upload = System.Web.HttpContext.Current.Server.MapPath("\\correspondencias\\");
                up.fileName = Convert.ToString(ObjEF.id_correspondencia);
                if (up.Do())
                    ObjEF.arquivo = String.Format(@"<a href='../../correspondencias/{0}{1}' target='blank'>arquivo</a>", up.fileName,up.fileExtension);
            }
        }

        protected void deleteFile()
        {
            if (!String.IsNullOrEmpty(ObjEF.arquivo))
            {

                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\correspondencias\\")).
                    Where(it => it.Contains(Convert.ToString(ObjEF.id_correspondencia))).ToArray();
                foreach(string f in files)
                    File.Delete(f);
            }            
        }

        public IEnumerable<int> GetAnos()
        {
            var anos = (from c in _dbSet                        
                    select c.data.Year).Distinct().ToList();

            anos.Sort(delegate(int x1, int x2)
            {
                return x2.CompareTo(x1);
            });
            
            return anos; 
        }
    }
}
        