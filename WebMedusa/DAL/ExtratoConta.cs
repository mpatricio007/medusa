using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    public class ExtratoConta
    {
        public int? id_lcto_conta { get; set; }
        public int? id_conta { get; set; }
        public DateTime data { get; set; }
        public string descricao { get; set; }
        public string dc { get; set; }
        public decimal? valor { get; set; }
        public decimal? saldo { get; set; }
        public string num_documento { get; set; }
        public string proj_num { get; set; }
    }
}
      