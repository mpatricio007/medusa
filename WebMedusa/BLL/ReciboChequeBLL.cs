using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class ReciboChequeBLL : Abstract_Crud<ReciboCheque>
    {
        public bool Exists()
        {
            return ObjEF.id_cheque != 0;
        }
    }
}
