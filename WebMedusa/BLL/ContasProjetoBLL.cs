using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class ContasProjetoBLL : AbstractCrudWithLog<ContasProjeto>
    {

        public bool Exists()
        {
            return ObjEF.id_conta_projeto != 0;
        }

   
    }
}
