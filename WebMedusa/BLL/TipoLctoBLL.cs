using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Collections;

namespace Medusa.BLL
{
    public class TipoLctoBLL : AbstractCrudWithLog<TipoLcto>
    {
        public override IEnumerable GetAll()
        {
            return _dbSet.OrderBy(it => it.dc).ThenBy(it => it.descricao).ToList();
        }

        public static int PAGAMENTOS
        {
            get
            {
                return 8;
            }
        }
    }
}
