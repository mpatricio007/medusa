using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Security;

namespace Medusa.DAL
{    
    public class Usuario : AbstractPessoaFisica
    {
        [Key]
        public int id_usuario { get; set; }

        public int nivel { get; set; }

        [Required]
        [MaxLength(50)]
        public string login { get; set; }

        [Required]
        [MaxLength(50)]
        public string senha { get; set; }

        public bool status { get; set; }

        public bool primeiro_acesso { get; set; }

        public virtual ICollection<LogSistema> LogSistema { get; set; }

        [Search("sistemas","UsuarioSistema.Sistema.nome")]
        public virtual ICollection<UsuarioSistema> UsuarioSistema { get; set; }

        public virtual ICollection<Tarefa> Tarefas { get; set; }

        public virtual ICollection<TarefaProvidencia> Providencias { get; set; }
    }

    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            HasMany(u => u.Providencias).WithRequired(p => p.Usuario).WillCascadeOnDelete(false);
            ToTable("Usuario");
        }
    }
}