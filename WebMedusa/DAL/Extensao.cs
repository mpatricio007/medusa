using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Extensao
    {
        [Key]
        public int id_extensao { get; set; }

        [Required]
        [MaxLength(70)]
        public string nome { get; set; }

        [Required]
        [MaxLength(10)]
        public string extensao { get; set; }
    }


    public class ExtensaoConfiguration : EntityTypeConfiguration<Extensao>
    {
        public ExtensaoConfiguration()
        {
            ToTable("Extensao");
        }
    }
}