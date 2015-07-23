using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class LotePagBB : Lote
    {
       

        public virtual ICollection<RemessaPAG> Remessas { get; set; }

        public LotePagBB()
        {
            Remessas = new List<RemessaPAG>();
        }
    }

    public class LotePagBBConfiguration : EntityTypeConfiguration<LotePagBB>
    {
        public LotePagBBConfiguration()
        {
            HasMany(it => it.Remessas).WithRequired(r => r.Lote).WillCascadeOnDelete(false);
        }
    }
}