using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{    
    [Serializable]
    public class PessoaEmail 
    {
        [Key]
        public int id_email { get; set; }

        public Email email { get; set; }
        
        public int id_pessoa { get; set; }

        [NonSerialized()]
        private Pessoa pessoa;

        [ForeignKey("id_pessoa")]
        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }

        public PessoaEmail()
        {

        }

        public PessoaEmail(Email em)
        {
            email = em;
        } 
    }

    public class PessoaEmailConfiguration : EntityTypeConfiguration<PessoaEmail>
    {
        public PessoaEmailConfiguration()
        {
            ToTable("PessoaEmails");
        }
    }
}