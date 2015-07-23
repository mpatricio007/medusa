using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class BaseCalculoDedutivelBLL : AbstractCrudWithLog<BaseCalculoDedutivel>
    {
        public void Get(int id_tabela, int id_taxa)
        {
            ObjEF = _dbSet.Where(it => it.id_tabela == id_tabela & it.id_taxa == id_taxa).FirstOrDefault();
        }
    }
}
