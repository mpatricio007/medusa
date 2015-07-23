using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class OpcaoAdiantamento
    {
        [Key]
        public int id_opcao_admto { get; set; }

        public DateTime data { get; set; }

        [MaxLength(150)]
        public string obs { get; set; }

        public int? id_tipo_admto { get; set; }
        [ForeignKey("id_tipo_admto")]
        public virtual TiposAdiantamento TiposAdiantamento { get; set; }

        public int? id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public int? id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }
    }


    public class OpcaoAdiantamentoConfiguration : EntityTypeConfiguration<OpcaoAdiantamento>
    {
        public OpcaoAdiantamentoConfiguration()
        {
            ToTable("OpcaoAdiantamento");
        }
    }
}