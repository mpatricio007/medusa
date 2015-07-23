using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class LoteGPS : Lote
    {
        public virtual ICollection<RemessaGpsSemCodBarra> Guias { get; set; }

        public LoteGPS()
        {
            Guias = new List<RemessaGpsSemCodBarra>();
        }
    }
}