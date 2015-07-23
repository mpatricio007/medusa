using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ContratoPagamento
    {
        [Key]
        public int id_contrato_pagamento { get; set; }

        public int num_parcela { get; set; }

        public DateTime data_vencimento { get; set; }

        public DateTime data_pagamento { get; set; }

        public string observacao { get; set; }

        public decimal valor { get; set; }

        public bool cancelado { get; set; }

        public int id_contrato { get; set; }

        [ForeignKey("id_contrato")]
        public virtual ContratoPessoaFisica ContratoPessoaFisica { get; set; }

    }


    public class ContratoPagamentoConfiguration : EntityTypeConfiguration<ContratoPagamento>
    {
        public ContratoPagamentoConfiguration()
        {
            ToTable("ContratoPagamento");
        }
    }
}