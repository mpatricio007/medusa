using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class HistoricoProjeto
    {
        [Key]
        public int id_hsp { get; set; }

        [Required]
        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        [Required]
        public int id_status_projeto { get; set; }
        [ForeignKey("id_status_projeto")]
        public virtual StatusProjeto StatusProjeto { get; set; }

        [Required]
        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        [MaxLength(100)]
        public string observacao { get; set; }

        public DateTime data { get; set; }
    }


    public class HistoricoProjetoConfiguration : EntityTypeConfiguration<HistoricoProjeto>
    {
        public HistoricoProjetoConfiguration()
        {
            ToTable("HistoricoProjeto");
        }
    }
}