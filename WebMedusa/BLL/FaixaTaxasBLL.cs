using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class FaixaTaxasBLL : AbstractCrudWithLog<FaixaTaxas>
    {
        public bool DataIsValid(string faixaDe, string faixaAté, string valorMax, string valorMin, string deducao, string aliquota, ref string strMsg)
        {
            StringBuilder str = new StringBuilder();
            bool rt = true;
            decimal result = 0;
            if (!Decimal.TryParse(faixaDe, out result))
            {
                str.AppendLine("formato incorreto em 'faixa de'<br/>");
                rt = false;
            }
            if (!Decimal.TryParse(faixaAté, out result))
            {
                str.AppendLine("formato incorreto em 'faixa até'<br/>");
                rt = false;
            }
            if (!Decimal.TryParse(valorMax == String.Empty ? "0" : valorMax, out result))
            {
                str.AppendLine("formato incorreto em 'valor máximo'<br/>");
                rt = false;
            }
            if (!Decimal.TryParse(valorMin, out result))
            {
                str.AppendLine("formato incorreto em 'valor mínimo'<br/>");
                rt = false;
            }
            if (!Decimal.TryParse(deducao, out result))
            {
                str.AppendLine("formato incorreto em 'dedução'<br/>");
                rt = false;
            }
            if (!Decimal.TryParse(aliquota, out result))
            {
                str.AppendLine("formato incorreto em 'alíquota'<br/>");
                rt = false;
            }

            strMsg = str.AppendLine().ToString();
            return rt;
        }
    }
}
