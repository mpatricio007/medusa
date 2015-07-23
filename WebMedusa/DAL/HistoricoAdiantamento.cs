using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class HistoricoAdiantamento
    {
        [Key]
        public int id_hist_admto { get; set; }

        public DateTime data { get; set; }

        [MaxLength(200)]
        public string observacao { get; set; }

        public int id_adiantamento { get; set; }

        [ForeignKey("id_adiantamento")]
        public virtual Adiantamento adiantamento { get; set; }

        public int id_status_admto { get; set; }

        [ForeignKey("id_status_admto")]
        public virtual StatusAdiantamento  statusAdiantamento{ get; set; }

        public int? id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        public int? id_setor { get; set; }
        [ForeignKey("id_setor")]
        public virtual Setor Setor { get; set; }

        public DateTime? data_prorrogacao { get; set; }

    }


    public class HistoricoAdiantamentoConfiguration : EntityTypeConfiguration<HistoricoAdiantamento>
    {
        public HistoricoAdiantamentoConfiguration()
        {
            ToTable("HistoricoAdiantamento");
        }
    }
}