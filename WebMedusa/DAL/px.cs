using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class px
    {
        [Key]
        public int codigo { get; set; }

        public DateTime? data { get; set; }

        public int projeto { get; set; }

        public string coordenador { get; set; }

        public string unidade { get; set; }

        public string patrocinador { get; set; }

        public string titulo { get; set; }

        public string obs { get; set; }

        public int? proj { get; set; }

        public int? projA { get; set; }
    }


    public class pxConfiguration : EntityTypeConfiguration<px>
    {
        public pxConfiguration()
        {
            ToTable("pxs");
        }
    }
}