using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.LIB;

namespace Medusa.BLL.LayoutBB
{
    public class SaldoAtualExtratoBB : LinhaExtratoBB
    {
        public string agencia { get; set; }
        public string conta_num { get; set; }
        public string conta_dig { get; set; }     
        public DateTime data { get; set; }
        public decimal valor { get; set; }

        public SaldoAtualExtratoBB(string strLinha)
        {
            agencia = strLinha.Substring(17, 4);
            conta_num = strLinha.Substring(29, 11).TrimStart('0');
            conta_dig = strLinha.Substring(40, 1);
            valor = strLinha.Substring(104, 1) == "C" ? Convert.ToDecimal(strLinha.Substring(86, 18)) / 100 
                : Convert.ToDecimal(strLinha.Substring(86, 18)) / (-100);
            data = Util.StringSemBarrasToDate(strLinha.Substring(181, 8)).GetValueOrDefault();
       
        }
    }
}