using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class CategoriaDocumento
    {
        [Key]
        public int id_cat_doc { get; set; }  

        [Required]
        public bool status { get; set; }

        public int id_documento { get; set; }

        [ForeignKey("id_documento")]
        public virtual DocumentoCategoria Documento { get; set; }

        public int id_categoria { get; set; }

        [ForeignKey("id_categoria")]
        public virtual Categoria Categoria { get; set; }
    }


    public class CategoriaDocumentoConfiguration : EntityTypeConfiguration<CategoriaDocumento>
    {
        public CategoriaDocumentoConfiguration()
        {
            ToTable("CategoriaDocumento");
        }
    }
}