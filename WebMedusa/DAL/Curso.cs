using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Curso
    {
        [Key]
        public int id_curso { get; set; }

        [MaxLength(250)]
        public string nome { get; set; }

        public string id_projeto { get; set; }

        [MaxLength(250)]
        public string instrucao { get; set; }

        public int id_conta { get; set; }

        [ForeignKey("id_conta")]
        public virtual Conta conta { get; set; }
        
        
    }


    public class CursoConfiguration : EntityTypeConfiguration<Curso>
    {
        public CursoConfiguration()
        {
            ToTable("Curso");
        }
    }
}