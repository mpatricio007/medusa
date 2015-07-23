using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class AdiantamentoViagem : Adiantamento
    {   
        public DateTime? data_partida { get; set; }

        public DateTime? data_retorno { get; set; }
    }


    public class AdiantamentoViagemConfiguration : EntityTypeConfiguration<AdiantamentoViagem>
    {
        public AdiantamentoViagemConfiguration()
        {
            ToTable("AdiantamentoViagem");
        }
    }
}