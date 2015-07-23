using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class StatusEditaisLic
    {
        [Key]
        public int id_status_edital_lic { get; set; }

        public string nome { get; set; }

        public int? ordem { get; set; }
    }


    public class StatusEditaisLicConfiguration : EntityTypeConfiguration<StatusEditaisLic>
    {
        public StatusEditaisLicConfiguration()
        {
            ToTable("StatusEditaisLic");
        }
    }
}