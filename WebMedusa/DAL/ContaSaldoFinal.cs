using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContaSaldoFinal
    {
        [Key]
        public int id_saldo_final { get; set; }

        [Required]
        public int id_conta { get; set; }

        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }

        [Required]
        public DateTime data { get; set; }

        [Required]
        public decimal saldo { get; set; }

        [Required]
        public int id_imparq { get; set; }

        [ForeignKey("id_imparq")]
        public virtual ImportaArquivo ImportaArquivo { get; set; }
    }

    public class ContaSaldoFinalConfiguration : EntityTypeConfiguration<ContaSaldoFinal>
    {
        public ContaSaldoFinalConfiguration()
        {
            ToTable("ContaSaldoFinal");
        }
    }
}