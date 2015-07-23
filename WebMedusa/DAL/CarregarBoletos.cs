using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class CarregarBoletos
    {
        [Key]
        public Int64 id { get; set; }
        public string conta { get; set; }
        public string descricao { get; set; }
        public DateTime datapagto { get; set; }      
        public string forma { get; set; }       
        public Decimal valor { get; set; }
    }


    public class CarregarBoletosConfiguration : EntityTypeConfiguration<CarregarBoletos>
    {
        public CarregarBoletosConfiguration()
        {
            ToTable("vSischBoletos");
        }
    }
}