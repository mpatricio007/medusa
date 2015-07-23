using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class LoteCons : Lote
    {        
        public virtual ICollection<RemessaCons> Guias { get; set; } 

        public LoteCons()
        {
            Guias = new List<RemessaCons>();
        }
    }
}