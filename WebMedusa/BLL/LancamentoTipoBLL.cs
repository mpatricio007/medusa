using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class LancamentoTipoBLL : AbstractCrudWithLog<LancamentoTipo>
    {

        public LancamentoTipoBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }

        public LancamentoTipoBLL()
        {
        }

        public bool DataIsValid(int id_plano_conta)
        {
            return !ObjEF.PlanoContaTipoLanctos.Select(it => it.id_plano_conta).Contains(id_plano_conta);
        }

    }
}