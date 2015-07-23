using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Medusa.DAL
{
    public class PessoaJuridica : Pessoa
    {        
        public CNPJ cnpj { get; set; }
       
    }

    public class PessoaJuridicaConfiguration : EntityTypeConfiguration<PessoaJuridica>
    {
        public PessoaJuridicaConfiguration()
        {                        
            Property(p => p.cnpj.Value).HasColumnName("cnpj").IsRequired();
            ToTable("PessoaJuridica");
        }
    }
}