using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class TipoSolicitacaoBLL : AbstractCrudWithLog<TipoSolicitacao>
    {
        public bool EhProposta
        {
            get
            {
                return ObjEF.id_tipo_solicitacao == 2;
            }
        }
    }
}
