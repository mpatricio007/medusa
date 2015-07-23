using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Medusa.LIB;

namespace Medusa.DAL
{
    public abstract class DespesaImpostoConsumo : Despesa 
    {
        public string cedente { get; set; }

        public string descricao { get; set; }

        [Required]
        public string codbarra { get; set; }

        [NotMapped]
        public CodigoBarrasConsumo Guia
        {
            get
            {
                return new CodigoBarrasConsumo
                {
                    RepresentacaoNumerica = codbarra
                };
            }
            set { codbarra = value.RepresentacaoNumerica; }
        }

        [Required]
        public DateTime dataVencto { get; set; }
    }


    public class DespesaImpostoConsumoConfiguration : EntityTypeConfiguration<DespesaImpostoConsumo>
    {
        public DespesaImpostoConsumoConfiguration()
        {
            ToTable("DespesaImpostoConsumo");
        }
    }
}