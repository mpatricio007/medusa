using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.DAL
{
    public class CarregarFolha
    {
        [Key]
        public Int64 id { get; set; }

        public int id_tipo_folha_pagto { get; set; }

        [ForeignKey("id_tipo_folha_pagto")]
        public virtual TipoFolhaPagto TipoFolha { get; set; }

        public string proj { get; set; }

        public int? id_projeto { get; set; }

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto { get; set; }

        public decimal valorbruto { get; set; }

        public DateTime data_pgto { get; set; }

        public decimal deducaoINSS { get; set; }
        
        public decimal iss { get; set; }
    
        public string cpf_cnpj { get; set; }

        public string nome { get; set; }

        public int? id_banco { get; set; }

        [ForeignKey("id_banco")]
        public virtual Banco banco { get; set; }

        public string bank { get; set; }

        private string ag;

        public string agencia
        {
            get { return ag !=null? ag.LastIndexOf('-') < 0 ? ag : ag.Left(ag.LastIndexOf('-')): ""; }
            set { ag = value; }
        }
        

        [NotMapped]
        public string digito_agencia
        {
            get
            {
                return ag !=null? agencia.LastIndexOf('-') < 0 ? String.Empty : agencia.LastOrDefault().ToString(): "";
            }
        }



        private string cont;

        public string conta
        {
            get { return cont != null ? cont.LastIndexOf('-') < 0 ? cont : cont.Left(cont.LastIndexOf('-')) : ""; }
            set { cont = value; }
        }
        

        [NotMapped]
        public string digito_conta
        {
            get
            {
                return cont != null ? cont.LastIndexOf('-') < 0 ? String.Empty : cont.LastOrDefault().ToString(): "";
            }
        }

        public int ndeppes { get; set; }

        public long codpf { get; set; }

        public string codbarra { get; set; }

        public string rdpf { get; set; }
    }
}