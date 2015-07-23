using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class RamoAtividade
    {
        [Key]
        public int id_ramo_atividade { get; set; }

        [MaxLength(15)]
        public string codigo { get; set; }

        public string nome { get; set; } 

        [NotMapped]
        public string strRamoAtividade
        {
            get
            { return String.Format("{0} - {1}", codigo, nome); }
        }
    }


    public class RamoAtividadeConfiguration : EntityTypeConfiguration<RamoAtividade>
    {
        public RamoAtividadeConfiguration()
        {
            ToTable("RamoAtividade");
        }
    }
}
