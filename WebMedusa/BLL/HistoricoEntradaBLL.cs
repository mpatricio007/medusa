using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;

namespace Medusa.BLL
{
    public class HistoricoEntradaBLL : AbstractCrudWithLog<HistoricoEntrada>
    {
        public HistoricoEntradaBLL() { }

        public HistoricoEntradaBLL(Contexto ctx)
            : base(ctx)
        {
        }

        public override void Add()
        {
            ObjEF.data = DateTime.Now;
            base.Add();
        }

        public override bool SaveChanges()
        {
            //var ent = _dbContext.Entradas.Find(ObjEF.id_entrada);
            //ent = ent ?? ObjEF.Entrada;
            //ent.id_ultimo_status = ObjEF.id_status_entrada;
            //ent.id_ultimo_para = ObjEF.id_usuario_para;
            return base.SaveChanges();
        }
    }
}
