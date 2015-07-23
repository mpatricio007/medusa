using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class StatusDocFornecedorBLL : AbstractCrudWithLog<StatusDocFornecedor>
    {
        public static int Cancelado
        {
            get
            {
                return 2;
            }
        }

        public static int Incluso
        {
            get
            {
                return 1;
            }
        }
    }
}
