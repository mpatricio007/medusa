using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Patrimonio
    {
        [Key]
        public int id_patrimonio { get; set; }

        public string descricao { get; set; }

        public string observacao { get; set; }

        public int id_comodato { get; set; }

        [ForeignKey("id_comodato")]
        public virtual Comodato Comodato { get; set; }

        public int id_unidade { get; set; }

        [ForeignKey("id_unidade")]
        public virtual Unidade Unidade { get; set; }

        public DateTime inicio { get; set; }

        public DateTime? termino { get; set; }

        [Required]
        [MaxLength(50)]
        public string nf { get; set; }

        public DateTime? data_nf { get; set; }

        public Decimal? valor { get; set; }

        public string num_patrimonio { get; set; }

        public int quantidade { get; set; }
    }


    public class PatrimonioConfiguration : EntityTypeConfiguration<Patrimonio>
    {
        public PatrimonioConfiguration()
        {
            ToTable("Patrimonio");
        }
    }
}