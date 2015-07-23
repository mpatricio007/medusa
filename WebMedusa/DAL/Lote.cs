using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Lote
    {
        [Key]
        public int id_lote { get; set; }

        public DateTime? data_pgto { get; set; }

        public DateTime? data_envio { get; set; }

        public int id_conta { get; set; }

        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }

        public string descricao { get; set; }

        //private int id_tipo_lote { get; set; }

        //[ForeignKey("id_tipo_lote")]
        //public virtual TipoLote TipoLote { get; set; }

        public DateTime data_processamento { get; set; }

    }
    public class LoteConfiguration : EntityTypeConfiguration<Lote>
    {
        public LoteConfiguration()
        {
            Map<LotePagBB>(it => it.Requires("id_tipo_lote").HasValue(1));
            Map<LoteBoleto>(it => it.Requires("id_tipo_lote").HasValue(2));
            Map<LoteCons>(it => it.Requires("id_tipo_lote").HasValue(3));
            Map<LoteGPS>(it => it.Requires("id_tipo_lote").HasValue(4));
            Map<LoteGRU>(it => it.Requires("id_tipo_lote").HasValue(5));
            ToTable("Lote");
        }
    }
}