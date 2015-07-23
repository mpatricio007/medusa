using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Assin
    {
        [Key]
        public int id_assin { get; set; }

        public DateTime validade { get; set; }

        [MaxLength(100)]
        public string nome_arquivo { get; set; }

        [NotMapped]
        public string arquivo 
        {
            get
            {
                return String.Format(@"<a href='{0}' target='_blank'>arquivo</a>", nome_arquivo);
            }
        }

        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public Projeto Projeto { get; set; }
    }

    public class AssinConfiguration : EntityTypeConfiguration<Assin>
    {
        public AssinConfiguration()
        {
            ToTable("Assin");
        }
    }
}