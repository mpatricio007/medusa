using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class old_empresa
    {
        [Key]
        public string id { get; set; }

        public string cnpj { get; set; }
                
        public string razaosocial { get; set; }

        public int bloqueado { get; set; }

        public string senha { get; set; }

        public string email { get; set; }

        public DateTime? validade { get; set; }
    }


    public class old_empresaConfiguration : EntityTypeConfiguration<old_empresa>
    {
        public old_empresaConfiguration()
        {
            ToTable("old_empresa");
        }
    }
}