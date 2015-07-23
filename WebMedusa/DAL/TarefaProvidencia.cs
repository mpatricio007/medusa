using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class TarefaProvidencia
    {
        [Key]
        public int id_providencia { get; set; }
                
        public int id_tarefa { get; set; }

        [ForeignKey("id_tarefa")]
        public virtual Tarefa Tarefa { get; set; }

        [Required]
        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        [Required]
        public DateTime data { get; set; }

        [Required]
        public string providencia { get; set; }
    }

    public class TarefaProvidenciaConfiguration : EntityTypeConfiguration<TarefaProvidencia>
    {
        public TarefaProvidenciaConfiguration()
        {
            HasRequired(p => p.Tarefa).WithMany(t => t.Providencias).WillCascadeOnDelete(true);
            ToTable("TarefaProvidencia");
        }
    }
}