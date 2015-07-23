using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Tarefa   
    {
        [Key]
        public int id_tarefa { get; set; }

        [Required]
        public int id_usuario_de { get; set; }

        [ForeignKey("id_usuario_de")]
        public virtual Usuario UsuarioDe { get; set; }
                
        public string tarefa { get; set; }

        [NotMapped]
        public virtual string tarefaToString
        {
            get { return tarefa; }            
        }

        [Required]
        public DateTime data { get; set; }

        [Required]
        [MaxLength(10)]
        public string status { get; set; }

        public virtual ICollection<TarefaDestinatario> Destinatarios { get; set; }

        public virtual ICollection<TarefaProvidencia> Providencias { get; set; }

        public bool? respostaUnica { get; set; }

    }

    public class TarefaConfiguration : EntityTypeConfiguration<Tarefa>
    {
        public TarefaConfiguration()
        {
            HasMany(t => t.Providencias).WithRequired().WillCascadeOnDelete(true);
            ToTable("Tarefa");
        }
    }
}