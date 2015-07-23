using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class PlanoContaBLL : AbstractCrudWithLog<PlanoConta>
    {
        public void GetPorCodigo(string cod)
        {
            ObjEF = _dbSet.Where(it => it.codigo == cod).FirstOrDefault();
        }

        public bool DataIsValid(int id_lancto_tipo)
        {
            return !ObjEF.PlanoContaTipoLanctos.Select(it => it.id_lancto_tipo).Contains(id_lancto_tipo);
        }

        public PlanoContaBLL() { }

        public PlanoContaBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }
        //    : base(ctx)
        //{

        //}
    }
}
