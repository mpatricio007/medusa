using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.LIB;

namespace Medusa.BLL.LayoutBB
{
    public class SaldoAnteriorExtratoBB : LinhaExtratoBB
    {
        public string agencia { get; set; }
        public string conta_num { get; set; }
        public string conta_dig { get; set; }
        public string conta_status { get; set; }
        public decimal saldoAnterior { get; set; }
        public DateTime dataSaldoAnterior { get; set; }

        public SaldoAnteriorExtratoBB(string strLinha)
        {

            agencia = strLinha.Substring(17, 4);
            conta_num = strLinha.Substring(29, 11).TrimStart('0');
            conta_dig = strLinha.Substring(40, 1);
            conta_status = strLinha.Substring(104, 1);
            saldoAnterior = conta_status == "C" ? Convert.ToDecimal(strLinha.Substring(86, 18)) / 100 : Convert.ToDecimal(strLinha.Substring(86, 18)) * (-1) / 100;
            dataSaldoAnterior = Util.StringSemBarrasToDate(strLinha.Substring(181, 8)).GetValueOrDefault();
        }

    }
}