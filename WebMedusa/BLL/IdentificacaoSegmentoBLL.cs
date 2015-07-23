using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class IdentificacaoSegmentoBLL : AbstractCrudWithLog<IdentificacaoSegmento>
    {
        public void GetPorCodigo(string codigo)
        {
            ObjEF = _dbSet.Where(it => it.codigo.Equals(codigo)).FirstOrDefault();
        }

        public bool DataIsValid(int p)
        {
            return true;
        }
    }
}
