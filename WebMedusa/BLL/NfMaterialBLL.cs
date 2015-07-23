using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;
using System.Data.Entity;
using System.Data;

namespace Medusa.BLL
{
    public class NfMaterialBLL : AbstractCrudWithLog<NfMaterial>
    {
        public List<MaterialNotaFiscal> oldMateriais { get; set; }
        
        public Upload up { get; set; }

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

        protected virtual void updateEntries()
        {
            var newMateriais = ObjEF.MaterialNotaFiscais.ToList();
            newMateriais.ForEach(it => ObjEF.MaterialNotaFiscais.Remove(it));

            if (ObjEF.id_nf_material != 0)
            {
                var reqEntry = _dbContext.Entry(ObjEF);
                reqEntry.State = System.Data.EntityState.Modified;

            }

            foreach (var reqMat in oldMateriais)
            {
                var rBLL = new MaterialNotaFiscalBLL(_dbContext);
                rBLL.Get(reqMat.id_material_nf);
                rBLL.Delete();
            }
            foreach (var reqMat in newMateriais)
            {
                var rBLL = new MaterialNotaFiscalBLL(_dbContext);
                rBLL.ObjEF = new MaterialNotaFiscal();
                rBLL.ObjEF.id_material = reqMat.id_material;
                rBLL.ObjEF.quantidade = reqMat.quantidade;
                rBLL.ObjEF.id_material_nf = 0;
                rBLL.ObjEF.id_nf_material = ObjEF.id_nf_material;
                rBLL.Add();
            }

        }
        public override void Update()
        {
            deleteFile();
            saveFile();
            updateEntries();
            base.Update();
        }

        public override bool SaveChanges()
        {
            bool rt = base.SaveChanges();

            var ds = ObjEF.MaterialNotaFiscais.ToList();
            foreach (var item in ds)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }

            return rt;
        }

        protected void saveFile()
        {
            if (up.file != null)
            {
                up.where_upload = System.Web.HttpContext.Current.Server.MapPath("\\nfMateriais\\");
                up.fileName = Convert.ToString(ObjEF.id_nf_material);
                if (up.Do())
                    ObjEF.arquivo = String.Format(@"<a href='../../nfMateriais/{0}{1}' target='blank'>arquivo</a>", up.fileName, up.fileExtension);
            }
        }

        protected void deleteFile()
        {
            if (!String.IsNullOrEmpty(ObjEF.arquivo))
            {

                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\nfMateriais\\")).
                    Where(it => it.Contains(Convert.ToString(ObjEF.id_nf_material))).ToArray();
                foreach (string f in files)
                    File.Delete(f);
            }
        }
        //private Upload up;

        //public Upload Up
        //{
        //    get
        //    {
        //        if (up == null)
        //            up = new Upload();
        //        return up;
        //    }
        //    set { up = value; }
        //}

        //public override void Add()
        //{
        //    if (up.file != null)
        //    {
        //        if (base.SaveChanges())
        //        {
        //            saveFile();
        //            base.Update();
        //        }
        //    }
        //    base.Add();
        //}

        //public override void Delete()
        //{
        //    deleteFile();
        //    base.Delete();
        //}

        //public override void Update()
        //{
        //    deleteFile();
        //    saveFile();
        //    base.Update();
        //}

        //protected void saveFile()
        //{
        //    if (Up.file != null)
        //    {
        //        up.where_upload = System.Web.HttpContext.Current.Server.MapPath("\\nfMateriais\\");
        //        up.fileName = Convert.ToString(ObjEF.id_nf_material);
        //        if (up.Do())
        //            ObjEF.arquivo = String.Format(@"<a href='../../nfMateriais/{0}{1}' target='blank'>arquivo</a>", up.fileName, up.fileExtension);
        //    }
        //}

        //protected void deleteFile()
        //{
        //    if (!String.IsNullOrEmpty(ObjEF.arquivo) & (Up.file != null))
        //    {

        //        string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\nfMateriais\\")).
        //            Where(it => it.Contains(Convert.ToString(ObjEF.id_nf_material))).ToArray();
        //        foreach (string f in files)
        //            File.Delete(f);
        //    }
        //}
    }
}
