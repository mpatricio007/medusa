using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.LIB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class RemessaGru : Remessa
    {
        [ForeignKey("id_lote")]
        public virtual LoteGRU Lote { get; set; }

        [Required]
        public string cod_recolhimento { get; set; }

        [Required]
        public int num_referencia { get; set; }

        [Required]
        public DateTime data_vencto { get; set; }

        [Required]
        public string id_contribuinte { get; set; }

        [Required]
        public string ug_gestao { get; set; }

        [Required]
        public decimal valor_gru { get; set; }

        [Required]
        public decimal desc_abatimento { get; set; }

        [Required]
        public decimal outras_deducoes { get; set; }

        [Required]
        public decimal mora_multa { get; set; }

        [Required]
        public decimal juros_encargos { get; set; }

        [Required]
        public decimal outros_acrescimos { get; set; }

        [Required]
        public string mes_ano { get; set; }

        [Required]
        public string codbarra { get; set; }

        [NotMapped]
        public CodigoBarrasGru Guia
        {
            get
            {
                return new CodigoBarrasGru
                {
                    RepresentacaoNumerica = codbarra
                };
            }
            set { codbarra = value.RepresentacaoNumerica; }

        }
    }
    public class RemessaGruConfiguration : EntityTypeConfiguration<RemessaGru>
    {
        public RemessaGruConfiguration()
        {
            ToTable("RemessaGru");
        }
    }
}