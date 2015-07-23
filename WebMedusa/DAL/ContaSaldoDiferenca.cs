using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContaSaldoDiferenca
    {
        [Key]
        public Int64 id { get; set; }
       
        public int id_conta { get; set; }

        [ForeignKey("id_conta")]
        public virtual Conta Conta { get; set; }

        public decimal saldosistema { get; set; }

        public decimal saldobanco { get; set; }

        public DateTime data { get; set; }

        public decimal saldodiferenca { get; set; }

        [NotMapped]
        public string HtmlPaginaLctosConta 
        {
            get
            {
                return
                    String.Format(@"<a href='../conciliacao/ContasLancto.aspx?id_conta={0}&dtDe={6}&dtAte={6}'>Banco:{1} Agencia: {2}-{3} nº {4}-{5}</a>",
                    id_conta.ToString().Criptografar(), Conta.BancoAgencia.Banco.nome, Conta.BancoAgencia.numero,
                    Conta.BancoAgencia.digito, Conta.numero, Conta.digito, data.ToShortDateString().Criptografar());
            }
        }
    } 


    public class ContaSaldoDiferencaConfiguration : EntityTypeConfiguration<ContaSaldoDiferenca>
    {
        public ContaSaldoDiferencaConfiguration()
        {
            ToTable("vContaSaldoDiferencas");
        }
    }
}