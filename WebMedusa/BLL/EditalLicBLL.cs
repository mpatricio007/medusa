using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;
using System.Data.Entity;
using System.Data;

namespace Medusa.BLL
{
    public class EditalLicBLL : AbstractCrudWithLog<EditalLic>
    {
        public List<EditalLicAnexo> oldAnexos { get; set; }

        //protected virtual void updateEntries()
        //{
        //    var newEditalLicAnexos = ObjEF.EditalLicAnexos.ToList();
        //    newEditalLicAnexos.ForEach(it => ObjEF.EditalLicAnexos.Remove(it));

        //    if (ObjEF.id_edital_lic != 0)
        //    {
        //        var reqEntry = _dbContext.Entry(ObjEF);
        //        reqEntry.State = System.Data.EntityState.Modified;
        //    }

        //    foreach (var edtAnexo in oldAnexos)
        //    {
        //        var rBLL = new EditalLicAnexoBLL(_dbContext);
        //        rBLL.Get(edtAnexo.id_edital_lic_anexo);
        //        rBLL.Delete();
        //    }

        //    foreach (var item in newEditalLicAnexos)
        //    {
        //        var editAnexoBLL = new EditalLicAnexoBLL(_dbContext);
        //        editAnexoBLL.ObjEF = new EditalLicAnexo();
        //        editAnexoBLL.ObjEF.id_edital_lic_anexo = item.id_edital_lic_anexo;
        //        editAnexoBLL.ObjEF.arquivo = item.arquivo;
        //        editAnexoBLL.ObjEF.descricao = item.descricao;
        //        editAnexoBLL.ObjEF.id_edital_lic = item.id_edital_lic;
        //        editAnexoBLL.Add();
        //    }
        //}

        public override void Update()
        {
            //updateEntries();
            base.Update();
        }

        public override bool SaveChanges()
        {
            bool rt = base.SaveChanges();

            var ds = ObjEF.EditalLicAnexos.ToList();
            foreach (var item in ds)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }

            return rt;
        }
    }
}
