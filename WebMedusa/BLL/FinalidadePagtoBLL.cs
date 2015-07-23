using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class FinalidadePagtoBLL : AbstractCrudWithLog<FinalidadePagto>
    {
        public List<FinalidadePagto> GetAllMesmoBanco(bool eh_mesmo_banco)
        {
            if (eh_mesmo_banco)
                return _dbSet.Where(it => it.eh_outros).ToList();
            else
                return _dbSet.ToList();
        }
    }
}
