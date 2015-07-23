using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using Medusa.LIB;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class RemessaTit:Remessa
    {
        [ForeignKey("id_lote")]
        public virtual LoteBoleto Lote { get; set; }

        [Required]  
        public string codbarra { get; set; }
        
        [NotMapped]
        public CodigoBarrasBoleto Boleto
        {
            get
            {
                return new CodigoBarrasBoleto
                {
                    RepresentacaoNumerica = codbarra
                };
            }
            set { codbarra = value.RepresentacaoNumerica; }

        }        
       

        [Required]
        public DateTime dataVencto { get; set; }

        [Required]
        public int id_banco_destino { get; set; }

        [ForeignKey("id_banco_destino")]
        public virtual Banco BancoDestino { get; set; }
    }
    public class RemessaTitConfiguration : EntityTypeConfiguration<RemessaTit>
    {
        public RemessaTitConfiguration()
        {            
            ToTable("RemessaTit");
        }
    }
}