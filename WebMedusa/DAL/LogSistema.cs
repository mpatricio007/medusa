       using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class LogSistema
    {
        [Key]
        public int id_log { get; set; }
                
        [MaxLength(20)]
        public string acao { get; set; }        
        
        [MaxLength(50)]
        public string entidade { get; set; }

        [MaxLength(20)]
        public string ip { get; set; }
                
        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        public DateTime? data { get; set; }

        public int? id_entidade { get; set; }

        public string descricao { get; set; }

        public string alteracoes { get; set; }

    }

    public class LogSistemaConfiguration : EntityTypeConfiguration<LogSistema>
    {
        public LogSistemaConfiguration()
        {
            HasRequired(l => l.Usuario).WithMany(u => u.LogSistema).HasForeignKey(l => l.id_usuario);
            ToTable("LogSistemas");
        }
    }
}
