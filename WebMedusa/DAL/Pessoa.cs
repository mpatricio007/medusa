using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Text;


namespace Medusa.DAL
{
    
    public abstract class Pessoa
    {
        [Key]
        public int id_pessoa { get; set; }

        [Required]
        [MaxLength(100)]
        public string nome { get; set; }

        [MaxLength(1)]
        public string sexo { get; set; }

        public DateTime? dtNascto { get; set; }

        private ICollection<PessoaEmail> emails;

        public virtual ICollection<PessoaEmail> Emails
        {
            get
            {
                if (emails == null)
                    emails = new List<PessoaEmail>();
                return emails;
            }
            set { emails = value; }
        }

        private ICollection<PessoaTelefone> telefones;
        public virtual ICollection<PessoaTelefone> Telefones
        {
            get
            {
                if (telefones == null)                
                    telefones = new List<PessoaTelefone>();
                return telefones;
            }
            set { telefones = value; }
        }

        private ICollection<PessoaEndereco> enderecos;
        public virtual ICollection<PessoaEndereco> Enderecos
        {
            get
            {
                if (enderecos == null)
                    enderecos = new List<PessoaEndereco>();
                return enderecos;
            }
            set { enderecos = value; }
        }

        //[NotMapped]
        public string strListEmails 
        { 
            get
            {
                var em = new StringBuilder();
                foreach (var item in Emails)
                {
                    em.AppendLine(String.Format("- {0}<br/>",item.email.value));
                    em.AppendLine();
                }
                return em != null ? em.ToString() : String.Empty;
            }
            set { }
        }

        //[NotMapped]
        public bool SetEmails { get; set; }
       
        //[NotMapped]
        public bool SetTelefones { get; set; }       

        //[NotMapped]
        public bool SetEnderecos { get; set; }       

        public Pessoa()
        {
            
        }
    }

    public class PessoaConfiguration : EntityTypeConfiguration<Pessoa>
    {
        public PessoaConfiguration()
        {
            Ignore(it => it.SetEmails);
            Ignore(it => it.SetEnderecos);
            Ignore(it => it.SetTelefones);
            Ignore(it => it.strListEmails);
            HasMany(p => p.Emails).WithRequired().HasForeignKey(p => p.id_pessoa).WillCascadeOnDelete();
            HasMany(p => p.Telefones).WithRequired().HasForeignKey(p => p.id_pessoa).WillCascadeOnDelete();
            HasMany(p => p.Enderecos).WithRequired().HasForeignKey(p => p.id_pessoa).WillCascadeOnDelete();
            
            ToTable("Pessoa");
        }
    }

 
}