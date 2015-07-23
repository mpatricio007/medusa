using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Contato : AbstractPessoaFisica
    {
        [Key]
        public int id_contato { get; set; }

        public int? id_unidade { get; set; }

        [ForeignKey("id_unidade")]
        public virtual Unidade Unidade { get; set; }
        
        [NotMapped]
        public string email
        {
            get 
            {
                var em = PessoaFisica.Emails.FirstOrDefault();
                return  em != null ? em.email.value : String.Empty; 
            }
            
        }

        [NotMapped]
        public string telefone
        {
            get 
            { 
                var tel = PessoaFisica.Telefones.FirstOrDefault();
                return tel != null ? tel.telefone.ToString() : String.Empty; 
            }

        }
    }

    public class ContatoConfiguration : EntityTypeConfiguration<Contato>
    {
        public ContatoConfiguration()
        {
            ToTable("Contato");
        }
    }
}