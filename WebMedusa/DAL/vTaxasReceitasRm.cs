using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class vTaxasReceitasRm
    {
        public string codgerencial { get; set; }
        public int projeto { get; set; }
        public string nome_projeto { get; set; }
        public decimal recebimentos { get; set; }
        public decimal despesas_indiretas { get; set; }
        public decimal percentual { get; set; }
    }
}