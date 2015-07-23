using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class ArquivosRetorno
    {
        [Key]
        public int id_arquivo { get; set; }

        public DateTime? data_processado { get; set; }

        public int codigo_banco { get; set; }

        public DateTime? data_criacao { get; set; }

        public int? id_tipo_conciliacao { get; set; }

        [ForeignKey("id_tipo_conciliacao")]
        public virtual TipoConciliacao TipoConciliacao { get; set; }

        [MaxLength(500)]
        public string log_importacao { get; set; }

        public virtual ICollection<Remessa> Remessas { get; set; }

        public bool? status { get; set; }

        public int? id_tipo_arquivo { get; set; }

        [ForeignKey("id_tipo_arquivo")]
        public virtual TipoArquivo TipoArquivo { get; set; }

        public string obs { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }
   
    }


    public class ArquivosRetornoConfiguration : EntityTypeConfiguration<ArquivosRetorno>
    {
        public ArquivosRetornoConfiguration()
        {
            ToTable("ArquivosRetorno");
        }
    }
}