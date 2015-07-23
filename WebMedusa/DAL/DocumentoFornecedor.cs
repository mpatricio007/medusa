using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class DocumentoFornecedor
    {
        [Key]
        public int id_doc_fornecedor { get; set; }

        public string numero { get; set; }

        public DateTime data { get; set; }

        public DateTime? validade { get; set; }

        public int id_documento { get; set; }

        [ForeignKey("id_documento")]
        public virtual DocumentoCategoria documentocategoria { get; set; }

        public int id_fornecedor { get; set; }

        [ForeignKey("id_fornecedor")]
        public virtual Fornecedor fornecedor { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual UsuarioFusp usuario { get; set; }

        public string obs { get; set; }

        public int id_status_docFornecedor { get; set; }

        [ForeignKey("id_status_docFornecedor")]
        public virtual StatusDocFornecedor StatusDocFornecedor { get; set; }
    }


    public class DocumentoFornecedorConfiguration : EntityTypeConfiguration<DocumentoFornecedor>
    {
        public DocumentoFornecedorConfiguration()
        {
            ToTable("DocumentoFornecedor");
        }
    }
}
