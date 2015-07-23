using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Conta
    {
        [Key]
        public int id_conta { get; set; }

        [Required]        
        public int id_agencia { get; set; }

        [ForeignKey("id_agencia")]
        public virtual BancoAgencia BancoAgencia { get; set; }

        [Required]
        [MaxLength(10)]
        public string numero { get; set; }
        
        [MaxLength(1)]
        public string digito { get; set; }

        [Required]        
        public int id_tipoconta { get; set; }

        [ForeignKey("id_tipoconta")]                
        public virtual ContaTipo ContaTipo { get; set; }
        
        //public int? cod_def_projeto { get; set; }

        public int? id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }
                
        public decimal? saldo_inicial { get; set; }
                
        public DateTime? data_saldo_inicial { get; set; }

        public DateTime? data_abertura { get; set; }

        public bool? status { get; set; }

        public virtual ICollection<ContaLancto> ContaLancto { get; set; }

        public virtual ICollection<ContaAplicacao> ContaAplicacao { get; set; }

        public virtual ICollection<IdentificadorDeposito> IdentificadorDepositos { get; set; }

        [NotMapped]
        public string StrConta { get { return this.ToString(); } }


        [NotMapped]
        public string StrBancoAgenciaConta { get { return String.Format("BCO:{0} AG:{1}-{2} C/C:{3}-{4}", 
            BancoAgencia.Banco.codigo,BancoAgencia.numero,BancoAgencia.digito, numero, digito); } }

        [NotMapped]
        public string StrContaDigito { get { return String.Format("{0}-{1}", numero, digito); } }

        [NotMapped]
        public string HtmlPaginaConta
        {
            get
            {
                return String.Format(@"<a href='../comum/contas.aspx?pk={0}'>Conta Corrente: {1}-{2}</a>", id_conta.ToString().Criptografar(), numero, digito);
            }
        }

        [NotMapped]
        public string HtmlPaginaLctosConta
        {
            get
            {
                return String.Format(@"<a href='../conciliacao/ContasLancto.aspx?id_conta={0}'>Banco:{1} Agencia: {2}-{3} nº {4}-{5}</a>",
                    id_conta.ToString().Criptografar(), BancoAgencia.Banco.nome, BancoAgencia.numero, BancoAgencia.digito, numero, digito);
            }
        }
        public override string ToString()
        {
            return String.Format("agência:{0}-{1} número:{2}-{3} banco:{4}",
                //id_projeto.HasValue ? String.Format("projeto:{0} ", Projeto.codigo) : String.Empty,
                BancoAgencia.numero,
                BancoAgencia.digito,
                numero,
                digito,
                BancoAgencia.Banco.nome);
        }
    }

    
    public class ContaConfiguration : EntityTypeConfiguration<Conta>
    {
        public ContaConfiguration()
        {
            HasMany(c => c.IdentificadorDepositos).WithRequired(i => i.Conta).WillCascadeOnDelete(true);
            ToTable("Conta");
        }
    }
}