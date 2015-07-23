using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL;
using System.Text;

namespace Medusa.Relatorios.RemessaBancaria
{
    public class RelatorioRemessaCons
    {
        public string projeto { get; set; }

        public string descricao { get; set; }

        public string cedente { get; set; }

        public decimal valor { get; set; }

        public string conta_debito { get; set; }

        public string nome_banco { get; set; }

        public string agencia { get; set; }

        public string cod_barras { get; set; }

        public DateTime data_vencimento { get; set; }

        public DateTime? data_pagamento { get; set; }

        public decimal totalGeral { get; set; }

        public int num_lote { get; set; }

        public decimal desconto { get; set; }

        public string cabecalho { get; set; }

        public string cod_bancoDestino { get; set; }

        public string bancoTit { get; set; }

        public string autenticacao { get; set; }

        public string tipo { get; set; }

        public RelatorioRemessaCons()
        {

        }

        public RelatorioRemessaCons(RemessaCons rc)
        {
            projeto = rc.Projeto.codigo.ToString();
            descricao = rc.descricao;
            cedente = rc.nome_fav_cedente;
            valor = rc.valor;
            agencia = rc.Lote.Conta.BancoAgencia.numero;
            conta_debito = rc.Lote.Conta.numero;
            nome_banco = rc.Lote.Conta.BancoAgencia.Banco.nome;
            cod_barras = rc.Guia.ToString();
            data_pagamento = rc.Lote.data_pgto.GetValueOrDefault();
            data_vencimento = rc.dataVencto;
            num_lote = rc.id_lote;
            autenticacao = rc.aut_bancaria;
            if(rc.id_tipo_conciliacao.HasValue)
                tipo = rc.TipoConciliacao.nome;

            var txtcabecalho = new StringBuilder();
            txtcabecalho.AppendFormat("AUTORIZAÇÃO PARA PAGAMENTO - {0}, Agência: {1} - {2}",
                rc.Lote.Conta.BancoAgencia.nome, rc.Lote.Conta.BancoAgencia.numero, rc.Lote.Conta.BancoAgencia.digito);
            cabecalho = txtcabecalho.ToString();

            var txtConta = new StringBuilder();
            txtConta.AppendFormat("{0}-{1}",
                rc.Lote.Conta.numero, rc.Lote.Conta.digito);
            conta_debito = txtConta.ToString();

            var txtAgencia = new StringBuilder();
            txtAgencia.AppendFormat("{0}-{1}",
                rc.Lote.Conta.BancoAgencia.numero, rc.Lote.Conta.BancoAgencia.digito);
            agencia = txtAgencia.ToString();
        }

        public IEnumerable<RelatorioRemessaCons> GerarRelatorioAgendados(int? de, int? ate, DateTime? dt)
        {
            var remConsBLL = new RemessaConsBLL();

            return (from guias in remConsBLL.GetRemessasNaoRejeitadosPorLoteDataPagto(de, ate, dt)
                    orderby guias.Projeto.codigo
                    select new RelatorioRemessaCons(guias)).ToList();
        }

        public IEnumerable<RelatorioRemessaCons> GerarRelatorioComprovantes(List<RemessaCons> lstGuias)
        {
            return (from rel in lstGuias
                    select new RelatorioRemessaCons(rel)).ToList();
        }
    }
}