using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class PessoaFisica : Pessoa
    {        
        public CPF cpf { get; set; }
        public string rg { get; set; }

        //[MaxLength(1)]
        //public string sexo { get; set; }

        //public DateTime? dtNascto { get; set; }

        private ContaPessoaFisica contaPessoaFisica;

        public ContaPessoaFisica ContaPessoaFisica //{ get; set; }
        {
            get 
            {
                if (contaPessoaFisica == null)
                    contaPessoaFisica = new ContaPessoaFisica();
                return contaPessoaFisica; 
            }
            set { contaPessoaFisica = value; }
        }

        [NotMapped]
        public bool SetConta { get; set; }
    }

    public class PessoaFisicaConfiguration : EntityTypeConfiguration<PessoaFisica>
    {
        public PessoaFisicaConfiguration()
        {
            
            Property(p => p.cpf.Value).HasColumnName("cpf").IsRequired();
            Property(p => p.rg).HasMaxLength(20).IsOptional();
            
            
            ToTable("PessoaFisica");
        }
    }
}