using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class IdentificadorDeposito
    {
        [Key]
        public int id_identificador { get; set; }

        [Required]
        public string num_identificador { get; set; }

        [Required]
        public int cod_def_projeto { get; set; }
        
        public int id_conta { get; set; }

        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }
    }

    public class IdentificadorDepositoConfiguration : EntityTypeConfiguration<IdentificadorDeposito>
    {
        public IdentificadorDepositoConfiguration()
        {            
            ToTable("IdentificadorDeposito");
        }
    }
}