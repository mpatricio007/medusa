using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Departamento
    {
        [Key]
        public int id_departamento{ get; set; }

        public int id_unidade { get; set; }

        [ForeignKey("id_unidade")]
        public virtual Unidade unidade { get; set; }

        [Required]
        [MaxLength(15)]
        public string sigla { get; set; }

        [Required]
        [MaxLength(150)]
        public string nome { get; set; }

        public Endereco ender { get; set; }        

        private ICollection<Laboratorio> laboratorios;
        public virtual ICollection<Laboratorio> Laboratorios
        {
            get
            {
                if (laboratorios == null)
                    laboratorios = new List<Laboratorio>();
                return laboratorios;
            }
            set { laboratorios = value; }
        }
    }


    public class DepartamentoConfiguration : EntityTypeConfiguration<Departamento>
    {
        public DepartamentoConfiguration()
        {
            ToTable("Departamento");
        }
    }
}