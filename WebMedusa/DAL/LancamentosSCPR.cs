using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class LancamentosSCPR
    {
        [Key]
        public Int64 id { get; set; }

        public String descde { get; set; }
        public long codlan { get; set; }

        public Int32 codde { get; set; }

        public decimal valorli { get; set; }
    }


    public class LancamentosSCPRConfiguration : EntityTypeConfiguration<LancamentosSCPR>
    {
        public LancamentosSCPRConfiguration()
        {
            ToTable("vLancamentosSCPR");
        }
    }
}
