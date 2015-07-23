using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class BoletoCobranca
    {
        [Key]
        public int id_boleto { get; set; }

        public int id_evento_sacado { get; set; }

        [ForeignKey("id_evento_sacado")]
        public virtual EventoSacado EventoSacado { get; set; }

        public decimal valor { get; set; }

        public DateTime data_vencto { get; set; }

        public DateTime? data_pgto { get; set; }

        public decimal? valor_pgto { get; set; }

        public string codigo { get; set; }

        public string obs { get; set; }

        public int num_parcela { get; set; }


        [NotMapped]
        public decimal juros
        {
            get 
            {
                if (valor_pgto.HasValue)
                    return valor_pgto.GetValueOrDefault() - valor;
                else
                    return 0;
            }
        }
        

        //public DateTime enviadoEm { get; set; }

        //public DateTime LidoEm { get; set; }
    }

    public class BoletoCobrancaConfiguration : EntityTypeConfiguration<BoletoCobranca>
    {
        public BoletoCobrancaConfiguration()
        {
            ToTable("BoletoCobranca");
        }
    }
}