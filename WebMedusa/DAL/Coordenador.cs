using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Coordenador : AbstractPessoaFisica
    {
        [Key]
        public int id_coordenador { get; set; }

        public int id_unidade { get; set; }

        [ForeignKey("id_unidade")]
        public virtual Unidade Unidade { get; set; }

        public int? id_departamento { get; set; }

        [ForeignKey("id_departamento")]
        public virtual Departamento Departamento { get; set; }

        public int? id_laboratorio { get; set; }

        [ForeignKey("id_laboratorio")]
        public virtual Laboratorio Laboratorio { get; set; }

        [NotMapped]
        public string email
        {
            get
            {
                var em = PessoaFisica.Emails.FirstOrDefault();
                return em != null ? em.email.value : String.Empty;
            }
        }

        [NotMapped]
        public string emails
        {
            get
            {
                var em = new StringBuilder();
                foreach (var item in PessoaFisica.Emails)
                {
                    em.AppendFormat("- {0}", item.email.value);
                }
                return em != null ? em.ToString() : String.Empty;
            }
        }
    }

    public class CoordenadorConfiguration : EntityTypeConfiguration<Coordenador>
    {
        public CoordenadorConfiguration()
        {              
            ToTable("Coordenador");
        }
    }
}