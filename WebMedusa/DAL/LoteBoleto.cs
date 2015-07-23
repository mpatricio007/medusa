using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    public class LoteBoleto : Lote
    {        
        public virtual ICollection<RemessaTit> Boletos { get; set; }

        public LoteBoleto()
        {
            Boletos = new List<RemessaTit>();
        }
    }
}