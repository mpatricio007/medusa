using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set; }

        public string nome { get; set; }

        public bool status { get; set; }

        public bool? emitir_crc { get; set; }


        private ICollection<CategoriaDocumento> categoriadocumentos;
        public virtual ICollection<CategoriaDocumento> CategoriaDocumentos
        {
            get
            {
                if (categoriadocumentos == null)
                    categoriadocumentos = new List<CategoriaDocumento>();
                return categoriadocumentos;
            }
            set { categoriadocumentos = value; }
        }
    }


    public class CategoriaConfiguration : EntityTypeConfiguration<Categoria>
    {
        public CategoriaConfiguration()
        {
            ToTable("Categoria");
        }
    }
}