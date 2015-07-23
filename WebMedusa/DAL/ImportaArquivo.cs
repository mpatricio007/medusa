using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ImportaArquivo
    {
        [Key]
        public int id_imparq { get; set; }

        [Required]
        public int id_tipoimp { get; set; }

        [ForeignKey("id_tipoimp")]
        public virtual TipoImpArquivo TipoArquivo { get; set; }

        public DateTime data { get; set; }

        public DateTime data_importacao { get; set; }
               
        public int id_usuario  { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<ContaLancto> Lancamentos { get; set; }

        public virtual ICollection<ContaSaldoFinal> Saldos { get; set; }        
    }

    public class ImportaArquivoConfiguration : EntityTypeConfiguration<ImportaArquivo>
    {
        public ImportaArquivoConfiguration()
        {   
            HasMany(i => i.Lancamentos).WithOptional(l => l.ImportaArquivo).HasForeignKey(l => l.id_imparq).WillCascadeOnDelete(true);
            HasMany(i => i.Saldos).WithRequired(s => s.ImportaArquivo).HasForeignKey(l => l.id_imparq).WillCascadeOnDelete(true);       
            ToTable("ImportaArquivo");
        }
    }
}