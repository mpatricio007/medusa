using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    public class ArquivoAnexoProjeto
    {
        [Key]
        public int id_arquivo_anexo_proj { get; set; }

        [MaxLength(100)]
        public string nome_arquivo { get; set; }

        public int id_arquivo { get; set; }

        [ForeignKey("id_arquivo")]
        public virtual Arquivo arq { get; set; }

        public string arquivo { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }
    }

    public class ArquivoAnexoProjetoConfiguration : EntityTypeConfiguration<ArquivoAnexoProjeto>
    {
        public ArquivoAnexoProjetoConfiguration()
        {
            ToTable("ArquivoAnexoProjeto");
        }
    }
}