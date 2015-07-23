using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class PlanoContaTipoLancamentoBLL : AbstractCrudWithLog<PlanoContaTipoLancamento>
    {

        public PlanoContaTipoLancamentoBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }

        public PlanoContaTipoLancamentoBLL()
        {
        }
    }
}