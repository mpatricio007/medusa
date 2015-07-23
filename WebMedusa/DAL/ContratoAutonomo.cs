using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class ContratoAutonomo : ContratoPessoaFisica
    {
        public string tipo { get; set; }
    }


    public class ContratoAutonomoConfiguration : EntityTypeConfiguration<ContratoAutonomo>
    {
        public ContratoAutonomoConfiguration()
        {
           ToTable("ContratoAutonomo");
        }
    }
}