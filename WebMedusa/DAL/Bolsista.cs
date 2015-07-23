using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{    
    public class Bolsista:AbstractPessoaFisica
    {
        [Key]
        public int id_bolsista { get; set; }

        public DateTime data_cadastro  { get; set; }
    }

    public class BolsistaConfiguration : EntityTypeConfiguration<Bolsista>
    {
        public BolsistaConfiguration()
        {
            ToTable("Bolsista");
        }
    }
}