using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class DocumentosCategoria
    {
        [Key]
        public int id_documento { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }
    }


    public class DocumentosCategoriaConfiguration : EntityTypeConfiguration<DocumentosCategoria>
    {
        public DocumentosCategoriaConfiguration()
        {
            ToTable("DocumentosCategoria");
        }
    }
}