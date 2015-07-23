using Medusa.BLL;
using Medusa.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Medusa.Relatorios.RemessaBancaria
{
    public class RelatorioRemessaGRU
    {
        public int id_remessa { get; set; }
        public string projeto { get; set; }
        public string cedente { get; set; }
        public string descricao { get; set; }
        public string conta { get; set; }
        public decimal valor_gru { get; set; }
        public DateTime? data_pagamento { get; set; }
        public int num_lote { get; set; }
        public string identificador { get; set; }
        public string cabecalho { get; set; }
        public DateTime data_vencto { get; set; }
        public decimal valor_total { get; set; }
        public string tipo_retorno { get; set; }
        public string codbarra { get; set; }
        public string cod_recolhimento { get; set; }
        public string ug_gestao { get; set; }
        public decimal desc_abat { get; set; }
        public decimal outras_deducoes { get; set; }
        public decimal mora_multa { get; set; }
        public decimal juros_encargos { get; set; }
        public decimal outros_acrescimos { get; set; }
        public int num_referencia { get; set; }
        public string agencia { get; set; }
        public string autenticacao { get; set; }
        public string tipo { get; set; }
        
        public RelatorioRemessaGRU()
        {
            
        }

        public RelatorioRemessaGRU(RemessaGru rgru)
        {
            id_remessa = rgru.id_remessa;
            projeto = rgru.Projeto.cod_def_projeto.ToString();
            cedente = rgru.nome_fav_cedente;
            descricao = rgru.descricao;
            conta = rgru.Lote.Conta.numero;
            valor_gru = rgru.valor_gru;
            data_pagamento = rgru.Lote.data_pgto.GetValueOrDefault();
            num_lote = rgru.id_lote;
            identificador = rgru.id_contribuinte;
            valor_total = rgru.valor;
            data_vencto = rgru.data_vencto;
            tipo_retorno = rgru.TipoRetorno.descricao;
            codbarra = rgru.Guia.ToString();
            cod_recolhimento = rgru.cod_recolhimento;
            ug_gestao = rgru.ug_gestao;
            desc_abat = rgru.desc_abatimento;
            outras_deducoes = rgru.outras_deducoes;
            mora_multa = rgru.mora_multa;
            juros_encargos = rgru.juros_encargos;
            outros_acrescimos = rgru.outros_acrescimos;
            num_referencia = rgru.num_referencia;
            autenticacao = rgru.aut_bancaria;
            if (rgru.id_tipo_conciliacao.HasValue)
                tipo = rgru.TipoConciliacao.nome;
            var txtConta = new StringBuilder();
            txtConta.AppendFormat("{0}-{1}",
                rgru.Lote.Conta.numero, rgru.Lote.Conta.digito);
            conta = txtConta.ToString();

            var txtAgencia = new StringBuilder();
            txtAgencia.AppendFormat("{0}-{1}",
                rgru.Lote.Conta.BancoAgencia.numero, rgru.Lote.Conta.BancoAgencia.digito);
            agencia = txtAgencia.ToString();
            
            var txtcabecalho = new StringBuilder();
            txtcabecalho.AppendFormat("AUTORIZAÇÃO PARA LIBERAÇÃO DOS CRÉDITOS - {0}, Agência: {1} - {2}",
                rgru.Lote.Conta.BancoAgencia.nome, rgru.Lote.Conta.BancoAgencia.numero, rgru.Lote.Conta.BancoAgencia.digito);
            cabecalho = txtcabecalho.ToString();

        }

        public IEnumerable<RelatorioRemessaGRU> GerarRelatorio(int? de, int? ate, DateTime? dt)
        {
            var remGRUBLL = new RemessaGruBLL();
            return (from pagtos in remGRUBLL.GetRemessasNaoRejeitadosPorLoteDataPagto(de, ate, dt)
                    orderby pagtos.Projeto.codigo
                    select new RelatorioRemessaGRU(pagtos)).ToList();
        }

        public IEnumerable<RelatorioRemessaGRU> GerarRelatorioComprovantes(List<RemessaGru> lstGru)
        {
            return (from rel in lstGru
                    select new RelatorioRemessaGRU(rel)).ToList();
        }
    }
}