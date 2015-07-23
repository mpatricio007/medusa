using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.LIB;

namespace Medusa.BLL.LayoutBB
{
    public class HeaderExtratoBB : LinhaExtratoBB
    {
        public DateTime dataGravacao { get; set; }
        public HeaderExtratoBB(string strLinha)
        {
            dataGravacao = Util.StringSemBarrasToDate(strLinha.Substring(181, 8)).GetValueOrDefault();
        }


    }
  
}