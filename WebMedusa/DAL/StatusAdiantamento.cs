using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class StatusAdiantamento
    {
        [Key]
        public int id_status_admto { get; set; }

        public string nome { get; set; }

        public int? ordem { get; set; }

        public bool gerar_lancto { get; set; }

        public bool? visible { get; set; }

        public bool? exige_setor { get; set; }

        public bool? exige_data { get; set; }
    }

    public class StatusAdiantamentoConfiguration : EntityTypeConfiguration<StatusAdiantamento>
    {
        public StatusAdiantamentoConfiguration()
        {
            ToTable("StatusAdiantamento");
        }
    }
}