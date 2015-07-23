using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class PatrimonioBLL : AbstractCrudWithLog<Patrimonio>
    {
        //public override void Add()
        //{
        //    ObjEF.codigo = _dbSet.Where(it => it.id_comodato == ObjEF.id_comodato).Select(it => (int?)it.codigo).Max().GetValueOrDefault() +1;
        //    base.Add();
        //}
    }
}
