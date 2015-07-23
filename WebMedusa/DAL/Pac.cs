using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public enum TipoPac
    {
        bem = 1,
        servicospj = 2,
        locacaobem = 3,
        passagem = 4,
        locacaoveiculos = 5,
        autonomo = 6
    }
    

    public class Pac
    {        
        public int id_pac { get; set; }

    }

    public class PacConfiguration : EntityTypeConfiguration<Pac>
    {
        public PacConfiguration()
        {
            HasKey(it => it.id_pac);
            ToTable("Pac");
        }
    }
}