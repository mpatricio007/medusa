using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Arquivo
    {
        [Key]
        public int id_arquivo { get; set; }

        public string file_name { get; set; }

        public int id_extensao { get; set; }
        [ForeignKey("id_extensao")]
        public virtual Extensao Extensao { get; set; }

        public byte[] data { get; set; }
    }

    public class ArquivoConfiguration : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoConfiguration()
        {
            ToTable("Arquivo");
        }
    }
}