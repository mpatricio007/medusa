using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class RemessaPAG : Remessa
    {
        [ForeignKey("id_lote")]
        public virtual LotePagBB Lote { get; set; }

        [Required]
        public int id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco BancoDestino { get; set; }

        [Required]
        [MaxLength(5)]
        public string agencia { get; set; }

        [MaxLength(1)]
        public string digito_agencia { get; set; }

        [Required]
        [MaxLength(12)]
        public string conta { get; set; }

        [MaxLength(1)]
        public string digito_conta { get; set; }

        public int tipo_inscricao { get; set; }

        [Required]
        [NotMapped]
        public TipoInscricao tipoInscr 
        {
            get
            {
                tipo_inscricao = tipo_inscricao == 0 ? 1 : tipo_inscricao;
                return (TipoInscricao)tipo_inscricao;
            }
            set { tipo_inscricao = (int)value; }
        }

        [Required]
        [MaxLength(14)]
        public string inscricao { get; set; }

        [NotMapped]
        public CPF cpf
        {
            get
            {
                if (tipoInscr == TipoInscricao.CPF)
                    return new CPF(inscricao);
                else
                    return null;
            }
        }

        [NotMapped]
        public CNPJ cnpj
        {
            get
            {
                if (tipoInscr == TipoInscricao.CNPJ)
                    return new CNPJ(inscricao);
                else
                    return null;
            }
        }

        [NotMapped]
        public string StrContaFavorecido 
        {
            get
            {
                return String.Format("BCO:{0} AG:{1}-{2} C/C:{3}-{4}", BancoDestino.codigo,
                    agencia, digito_agencia, conta, digito_conta);
            }
        }

      
    }
    public class RemessaPAGConfiguration : EntityTypeConfiguration<RemessaPAG>
    {
        public RemessaPAGConfiguration()
        {          
            HasRequired(it => it.FormaPagto).WithMany().HasForeignKey(it => it.id_forma_pagto).WillCascadeOnDelete(false);
            

            //HasRequired(it => it.FinalidadePagto).WithMany().HasForeignKey(it => it.id_fin_pagto).WillCascadeOnDelete(false);
            ToTable("RemessaPAG");
        }
    }
}