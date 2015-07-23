using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.DAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class Adiantamento
    {
        [Key]
        public int id_adiantamento { get; set; }

        public int id_projeto { get; set; }
        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public int? id_tipo_admto { get; set; }
        [ForeignKey("id_tipo_admto")]
        public virtual TiposAdiantamento TiposAdiantamento { get; set; }

        public DateTime data { get; set; }

        public DateTime? data_vencimento { get; set; }

        public DateTime? data_pagamento { get; set; }

        public DateTime? data_rd { get; set; }

        public decimal? total_rd { get; set; }

        public decimal valor { get; set; }

        public string descricao { get; set; }

        public string descricao_rd { get; set; }

        [MaxLength(10)]
        public string rp { get; set; }

        [MaxLength(200)]
        public string obs { get; set; }

        public int? id_beneficiario { get; set; }
        [ForeignKey("id_beneficiario")]
        public virtual Beneficiario Beneficiario { get; set; }

        [Invisible]
        public int? id_ultimo_status { get; set; }

        [Search("status", "StatusAdiantamento.nome")]
        [ForeignKey("id_ultimo_status")]
        public virtual StatusAdiantamento StatusAdiantamento { get; set; }

        private ICollection<HistoricoAdiantamento> historicos;

        [Invisible]
        public virtual ICollection<HistoricoAdiantamento> Historicos
        {
            get
            {
                if (historicos == null)
                    historicos = new List<HistoricoAdiantamento>();
                return historicos;
            }
            set { historicos = value; }
        }

        private ICollection<HistoricoEmailAdmto> historicoEmailAdmtos;

        [Invisible]
        public virtual ICollection<HistoricoEmailAdmto> HistoricoEmailAdmtos
        {
            get
            {
                if (historicoEmailAdmtos == null)
                    historicoEmailAdmtos = new List<HistoricoEmailAdmto>();
                return historicoEmailAdmtos;
            }
            set { historicoEmailAdmtos = value; }
        }
    }


    public class AdiantamentoConfiguration : EntityTypeConfiguration<Adiantamento>
    {
        public AdiantamentoConfiguration()
        {
            ToTable("Adiantamento");
        }
    }
}