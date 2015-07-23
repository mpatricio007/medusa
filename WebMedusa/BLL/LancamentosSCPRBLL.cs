using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class LancamentosSCPRBLL : AbstractCrudWithLog<LancamentosSCPR>
    {
        public LancamentosSCPRBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }

    }
}
