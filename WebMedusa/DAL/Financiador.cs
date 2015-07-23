using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Financiador
    {
        [Key]
        public int id_financiador { get; set; }

        public string logotipo { get; set; }
        
        public CNPJ cnpj { get; set; }

        [Required]
        [MaxLength(150)]
        public string nome { get; set; }
        
        public int id_natureza { get; set; }

        [ForeignKey("id_natureza")]
        public virtual Natureza Natureza { get; set; }

        public Endereco ender { get; set; }

        private ICollection<FinanciadorTelefone> telefones;
        public virtual ICollection<FinanciadorTelefone> Telefones 
        {
            get
            {
                if (telefones == null)
                    telefones = new List<FinanciadorTelefone>();
                return telefones;
            }
            set
            {
                telefones = value;
            } 
        }

        private ICollection<FinanciadorEmail> emails;
        public virtual ICollection<FinanciadorEmail> Emails 
        {
            get
            {
                if (emails == null)
                    emails = new List<FinanciadorEmail>();
                return emails;
            }
            set
            {
                emails = value;
            } 
        }
        
        [NotMapped]
        public string strFinanciador
        {
            get 
            {
                return String.Format("{0} CNPJ:{1}", nome, cnpj);                
            }            
        }

        [NotMapped]
        public string fileName
        {
            get
            {
                return logotipo.Split(Convert.ToChar("'"))[1];
            }
        }
        

    }


    public class FinanciadorConfiguration : EntityTypeConfiguration<Financiador>
    {
        public FinanciadorConfiguration()
        {
            Property(f => f.cnpj.Value).HasColumnName("cnpj").IsRequired();
            HasMany(f => f.Emails).WithRequired().HasForeignKey(e => e.id_financiador).WillCascadeOnDelete();
            HasMany(f => f.Telefones).WithRequired().HasForeignKey(t => t.id_financiador).WillCascadeOnDelete();
            ToTable("Financiador");
        }
    }
}