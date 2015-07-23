using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class ContaTipoBLL : AbstractCrudWithLog<ContaTipo>
    {
        public static int BLOQUEADO
        {
            get
            {
                return 5;
            }
        }
    }
}
