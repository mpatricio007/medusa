using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class LoteGRU : Lote
    {
        public virtual ICollection<RemessaGru> Guias { get; set; }

        public LoteGRU()
        {
            Guias = new List<RemessaGru>();
        }
    }
}