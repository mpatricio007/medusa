using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class ReciboFusp
    {
        [Key]
        public int id_recibo_fusp { get; set; }

        public string nome { get; set; }

        public DateTime data { get; set; }

        public string tipo_pagamento { get; set; }

        public string descricao { get; set; }

        public decimal valor { get; set; }

        public string observacao { get; set; }

        public int id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }

        public string motivo_cancel { get; set; }

        public bool status_recibo { get; set; }

        public int intTipo { get; set; }

        public CNPJ cnpj { get; set; }

        public CPF cpf { get; set; }

        public int id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        [NotMapped]
        public TipoInscricao tipo
        {
            get
            {
                intTipo = intTipo == 0 ? 1 : intTipo;
                return (TipoInscricao)intTipo;
            }
            set { intTipo = (int)value; }
        }

        [NotMapped]
        public string strDocumentos
        {
            get
            {
                return tipo == TipoInscricao.CPF ? String.Format("CPF: {0}", cpf.Value) : String.Format("CNPJ: {0}", cnpj.Value);
            }
        }
    }

    public class ReciboFuspConfiguration : EntityTypeConfiguration<ReciboFusp>
    {
        public ReciboFuspConfiguration()
        {
            ToTable("ReciboFusp");
        }
    }
}