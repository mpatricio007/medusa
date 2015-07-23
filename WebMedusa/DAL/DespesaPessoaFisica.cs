using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public abstract class DespesaPessoaFisica : Despesa
    {
        public string cpf { get; set; }

        public string nome { get; set; }


        public int num_dependentes { get; set; }

        public int id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco BancoDestino { get; set; }

        [MaxLength(5)]
        public string agencia { get; set; }

        [MaxLength(1)]
        public string digito_agencia { get; set; }

        [MaxLength(12)]
        public string conta { get; set; }

        [MaxLength(1)]
        public string digito_conta { get; set; }

        public long? codpf { get; set; }

        //public int id_remessa { get; set; }

        //[ForeignKey("id_remessa")]
        //public virtual Remessa RemessaGerada { get; set; }

    }

    public class DespesaPessoaFisicaConfiguration : EntityTypeConfiguration<DespesaPessoaFisica>
    {
        public DespesaPessoaFisicaConfiguration()
        {            
            ToTable("DespesaPessoaFisica");
        }
    }
}