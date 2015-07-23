using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public abstract class Documento
    {
        [Key]
        public int id_documento{ get; set; }

        [MaxLength(10)]
        public string sigla { get; set; }

        [Required]
        [MaxLength(100)]
        public string nome { get; set; }

        [Required]
        public bool status { get; set; }

    }


    public class DocumentoConfiguration : EntityTypeConfiguration<Documento>
    {
        public DocumentoConfiguration()
        {
            Map<DocumentoProjeto>(it => it.Requires("tipo_documento").HasValue("Projeto"));
            Map<DocumentoCategoria>(it => it.Requires("tipo_documento").HasValue("CadastroPJ"));
            ToTable("Documento");
        }
    }
}