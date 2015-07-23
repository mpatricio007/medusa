using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.LIB;

namespace Medusa.BLL.LayoutBB
{
    public class LancamentoExtratoBB : LinhaExtratoBB
    {
        public string agencia { get; set; }
        public string conta_num { get; set; }
        public string conta_dig { get; set; }
        public string descricao { get; set; }
        public string cod_lcto { get; set; }
        public DateTime data { get; set; }
        public decimal valor { get; set; }
        public string num_documento { get; set; }

        public LancamentoExtratoBB(string strLinha)
        {
            agencia = strLinha.Substring(17, 4);
            conta_num = strLinha.Substring(29, 11).TrimStart('0');
            conta_dig = strLinha.Substring(40, 1);
            cod_lcto = strLinha.Substring(42, 3);
            descricao = strLinha.Substring(49, 25).TrimEnd();
            valor = Convert.ToDecimal(strLinha.Substring(86, 18)) / 100;
            data = Util.StringSemBarrasToDate(strLinha.Substring(181, 8)).GetValueOrDefault();
            num_documento = strLinha.Substring(135, 15).TrimStart('0');
        }
    }

}