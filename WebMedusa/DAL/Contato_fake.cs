using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Contato_fake
    {
        [Key]
        public int id_contato { get; set; }

        [MaxLength(50)]
        public string nome { get; set; }

        [MaxLength(50)]
        public string email { get; set; }

        [MaxLength(50)]
        public string tipo { get; set; }

    }


    public class Contato_fakeConfiguration : EntityTypeConfiguration<Contato_fake>
    {
        public Contato_fakeConfiguration()
        {
            //HasMany(b => b.TiposLctos).WithRequired(t => t.Contato_fake).WillCascadeOnDelete(false);
            ToTable("Contato_fake");
        }
    }
}