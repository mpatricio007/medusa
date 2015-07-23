using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Laboratorio
    {
        [Key]
        public int id_laboratorio{ get; set; }

        public int id_departamento { get; set; }

        [ForeignKey("id_departamento")]
        public virtual Departamento Departamento { get; set; }

        [Required]
        [MaxLength(15)]
        public string sigla { get; set; }

        [Required]
        [MaxLength(150)]
        public string nome { get; set; }

        public Endereco ender { get; set; }
    }


    public class LaboratorioConfiguration : EntityTypeConfiguration<Laboratorio>
    {
        public LaboratorioConfiguration()
        {
            ToTable("Laboratorio");
        }
    }
}