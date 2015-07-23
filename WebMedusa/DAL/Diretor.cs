using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Diretor : Representante
    {
        [Key]
        public int id_diretor { get; set; }
    }

    public class DiretorConfiguration : EntityTypeConfiguration<Diretor>
    {
        public DiretorConfiguration()
        {
            ToTable("Diretor");
        }
    }
}