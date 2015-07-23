using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class SolicitacaoDeProjetoBLL : AbstractCrudWithLog<SolicitacaoDeProjeto>
    {
        public static int adiantamento
        {
            get
            {
                return 1;
            }
        }

        public static int pac
        {
            get
            {
                return 2;
            }
        }
    }
}
