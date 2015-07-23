using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class Unidade
    {
        [Key]
        public int id_unidade { get; set; }

        [Required]
        [MaxLength(15)]
        public string sigla { get; set; }

        [Required]
        [MaxLength(150)]
        public string nome { get; set; }

        public Endereco ender { get; set; }


        private ICollection<Departamento> departamentos;
        public virtual ICollection<Departamento> Departamentos
        {
            get
            {
                if (departamentos == null)
                    departamentos = new List<Departamento>();
                return departamentos;
            }
            set { departamentos = value; }
        }

        public string StrUnidade
        {
            get
            {
                return String.Format("{0} - {1}", sigla, nome);
            }
        }
    }


    public class UnidadeConfiguration : EntityTypeConfiguration<Unidade>
    {
        public UnidadeConfiguration()
        {
            ToTable("Unidade");
        }
    }
}