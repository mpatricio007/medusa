using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL.LayoutBB
{
    public class TrailerExtratoBB : LinhaExtratoBB
    {
        public decimal soma_creditos { get; set; }
        public decimal soma_debitos { get; set; }

        public TrailerExtratoBB(string strLinha)
        {
            soma_debitos = Convert.ToDecimal(strLinha.Substring(12, 16)) / 100;
            soma_creditos = Convert.ToDecimal(strLinha.Substring(28, 16)) / 100;
        }
    }
}