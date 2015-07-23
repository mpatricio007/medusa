using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoImpArquivo
    {
        [Key]
        public int id_tipoimp { get; set; }

        [Required]
        [MaxLength(30)]
        public string descricao { get; set; }
    }


    public class TipoImpArquivoConfiguration : EntityTypeConfiguration<TipoImpArquivo>
    {
        public TipoImpArquivoConfiguration()
        {
            ToTable("TipoImpArquivo");
        }
    }
}