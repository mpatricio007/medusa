using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data.Entity;
using System.Data;

namespace Medusa.BLL
{
    public class ProvidenciaBLL : AbstractCrudWithLog<Providencia>
    {
        public List<SetorCompetente> oldSetoresCompetentes { get; set; }

        protected virtual void updateEntries()
        {
            var newSetoresCompetentes = ObjEF.SetoresCompetentes.ToList();
            newSetoresCompetentes.ForEach(it => ObjEF.SetoresCompetentes.Remove(it));

            if (Existis())
            {
                var reqEntry = _dbContext.Entry(ObjEF);
                reqEntry.State = System.Data.EntityState.Modified;

            }

            foreach (var sc in oldSetoresCompetentes)
            {
                var scBLL = new SetorCompetenteBLL(_dbContext);
                scBLL.Get(sc.id_setor_competente);
                scBLL.Delete();
            }
            foreach (var sc in newSetoresCompetentes)
            {
                var scBLL = new SetorCompetenteBLL(_dbContext);
                scBLL.ObjEF = new SetorCompetente();
                scBLL.ObjEF.id_setor_competente = 0;
                scBLL.ObjEF.id_setor = sc.id_setor;
                scBLL.ObjEF.id_providencia = sc.id_providencia;
                scBLL.Add();
            }

        }

        public bool Existis()
        {
            return ObjEF.id_providencia != 0;
        }

        public override void Update()
        {
            updateEntries();
            base.Update();
        }

        public override bool SaveChanges()
        {
            bool rt = base.SaveChanges();

            var ds = ObjEF.SetoresCompetentes.ToList();
            foreach (var item in ds)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }

            return rt;
        }
    }
}
