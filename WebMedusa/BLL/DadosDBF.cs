using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL
{
    public class DadosDBF
    {
        public string rp_rd { get; set; }
        public int projeto { get; set; }
        public int item { get; set; }
        public Decimal iss { get; set; }
        public string calcinss { get; set; }
        public Decimal pensao { get; set; }
        public Decimal valinss { get; set; }
        public Decimal inss11 { get; set; }
        public Decimal valir { get; set; }
        public DateTime data { get; set; }
        public string historico { get; set; }
        public Decimal valpago { get; set; }
        public Decimal valcc { get; set; }
        public string cpmf { get; set; }
    }
}