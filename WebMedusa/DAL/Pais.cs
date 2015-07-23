using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Pais
    {
        [Key]
        public int id_pais { get; set; }


        [MaxLength(50)]
        public string nome { get; set; }
        
        public int ddi { get; set; }
    }


    public class PaisConfiguration : EntityTypeConfiguration<Pais>
    {
        public PaisConfiguration()
        {
            ToTable("Pais");
        }
    }
}