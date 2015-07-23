using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ProcessamentoDespesa
    {
        [Key]
        public int id_precessamento_despesa { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp Usuario { get; set; }

        public DateTime data_despesa { get; set; }

        public DateTime data_execucao { get; set; }

        public string obs { get; set; }

        public int id_lancto_tipo { get; set; }
        
        [ForeignKey("id_lancto_tipo")]
        public virtual LancamentoTipo LancamentoTipo { get; set; }
    }

    public class ProcessamentoDespesaConfiguration : EntityTypeConfiguration<ProcessamentoDespesa>
    {
        public ProcessamentoDespesaConfiguration()
        {
            ToTable("ProcessamentoDespesa");
        }
    }
}