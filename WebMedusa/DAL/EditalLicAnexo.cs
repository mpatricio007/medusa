using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class EditalLicAnexo
    {
        [Key]
        public int id_edital_lic_anexo { get; set; }

        public string arquivo { get; set; }

        public string descricao { get; set; }

        public int id_edital_lic { get; set; }

        [ForeignKey("id_edital_lic")]
        public virtual EditalLic EditalLic { get; set; }

        public int ordem { get; set; }

        public bool login_obrigatorio { get; set; }

        public int id_arquivo { get; set; }

        [ForeignKey("id_arquivo")]
        public virtual Arquivo arq { get; set; }
    }


    public class EditalLicAnexoConfiguration : EntityTypeConfiguration<EditalLicAnexo>
    {
        public EditalLicAnexoConfiguration()
        {
            ToTable("EditalLicAnexo");
        }
    }
}