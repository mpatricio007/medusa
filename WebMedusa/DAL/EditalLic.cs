using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class EditalLic
    {
        [Key]
        public int  id_edital_lic { get; set; }

        public string titulo { get; set; }

        public string descricao { get; set; }

        public DateTime data { get; set; }

        public int id_status_edital_lic { get; set; }

        [ForeignKey("id_status_edital_lic")]
        public virtual StatusEditaisLic StatusEditaisLic { get; set; }

        private ICollection<EditalLicAnexo> editalLicAnexos;
        public virtual ICollection<EditalLicAnexo> EditalLicAnexos
        {
            get
            {
                if (editalLicAnexos == null)
                    editalLicAnexos = new List<EditalLicAnexo>();
                return editalLicAnexos;
            }

            set
            {
                editalLicAnexos = value;
            }
        }
    }


    public class EditalLicConfiguration : EntityTypeConfiguration<EditalLic>
    {
        public EditalLicConfiguration()
        {
            ToTable("EditalLic");
        }
    }
}