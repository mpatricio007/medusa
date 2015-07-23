using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class DespesaBoletoBLL : DespesaBLL<DespesaBoleto>
    {
        public override bool DataIsValid(ref string strMsg)
        {
            if (!ObjEF.Boleto.ValidaCodBarra())
            {
                strMsg = "Boleto inválido!";
                return false;
            }
            
            return base.DataIsValid(ref strMsg);
        }
    }
}
