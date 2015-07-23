using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Site_bndes
    {
        [Key]
        public int id_site_bndes { get; set; }

        public string titulo { get; set; }

        public string atividade { get; set; }

        public string coordenador { get; set; }

        public string copatrocinador { get; set; }

        public int ordem { get; set; }    

        public bool ativo { get; set; }

    }


    public class Site_bndesConfiguration : EntityTypeConfiguration<Site_bndes>
    {
        public Site_bndesConfiguration()
        {
            ToTable("Site_bndes");
        }
    }
}