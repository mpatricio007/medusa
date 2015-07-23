using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Site_pag_inicial
    {
        [Key]
        public int id_site_pag_inicial { get; set; }

        public string titulo { get; set; }

        public string descricao { get; set; }

        public int ordem { get; set; }

        public string link { get; set; }

        public DateTime? data { get; set; }

        public bool ativo { get; set; }

        [NotMapped]
        public string HtmlLink
        {
            get
            {
                return String.Format(@"<a href='{0}'>{1}</a>",
                   link, titulo);
            }
        }
    }


    public class Site_pag_inicialConfiguration : EntityTypeConfiguration<Site_pag_inicial>
    {
        public Site_pag_inicialConfiguration()
        {
            ToTable("Site_pag_inicial");
        }
    }
}