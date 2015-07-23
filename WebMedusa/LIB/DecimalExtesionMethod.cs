using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.LIB
{
    public static class DecimalExtesionMethod
    {
        public static string ExtensoEmReal(this decimal value)
        {
            NumeroPorExtenso n = new NumeroPorExtenso(value);
            return n.ToString();
        }

        public static decimal Truncar(this decimal value, int num_casas = 2)
        {
            decimal step = (decimal)Math.Pow(10, num_casas);
            return Math.Truncate(step * value) / step;
            
        }
    }
}