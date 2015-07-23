using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ProjetoDocumento
    {
        [Key]
        public int id_projeto_documento { get; set; }

        public int id_documento { get; set; }
        [ForeignKey("id_documento")]
        public virtual Documento Documento { get; set; }

        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public DateTime? data { get; set; }

        public string observacao { get; set; }
        

    }


    public class ProjetoDocumentoConfiguration : EntityTypeConfiguration<ProjetoDocumento>
    {
        public ProjetoDocumentoConfiguration()
        {
            ToTable("ProjetoDocumento");
        }
    }
}