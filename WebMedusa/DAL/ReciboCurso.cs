using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ReciboCurso
    {
        [Key]
        public int id_recibo_curso { get; set; }

        public string nome { get; set; }

        public decimal valor { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }
    }

    public class ReciboCursoConfiguration : EntityTypeConfiguration<ReciboCurso>
    {
        public ReciboCursoConfiguration()
        {
            ToTable("ReciboCurso");
        }
    }
}