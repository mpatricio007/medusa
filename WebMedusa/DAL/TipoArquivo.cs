using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class TipoArquivo
    {
        [Key]
        public int id_tipo_arquivo { get; set; }

        public int codigo { get; set; }

        public string nome { get; set; }

        public string tipo_segmento { get; set; }
    }

        public class TipoArquivoConfiguration : EntityTypeConfiguration<TipoArquivo>
        {
            public TipoArquivoConfiguration()
            {
                ToTable("TipoArquivo");
            }
        }
    }
