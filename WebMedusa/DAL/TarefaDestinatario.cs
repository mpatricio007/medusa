using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class TarefaDestinatario
    {
        [Key]
        public int id_tarefa_destinatario { get; set; }

        [Required]
        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        [Required]
        public int id_tarefa { get; set; }

        [ForeignKey("id_tarefa")]
        public virtual Tarefa Tarefa { get; set; }        
    }

    public class TarefaDestinatarioConfiguration : EntityTypeConfiguration<TarefaDestinatario>
    {
        public TarefaDestinatarioConfiguration()
        {
            HasRequired(t => t.Usuario).WithMany().WillCascadeOnDelete(false);            
            ToTable("TarefaDestinatario");
        }
    }
}