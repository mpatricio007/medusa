using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.BLL;
using Medusa.DAL;
using System.Text;
using Medusa.LIB;

namespace Medusa.Relatorios.RemessaBancaria
{
    public class RelatorioRemessaTitulo
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

        public int num_lote { get; set; }

        public string cabecalho { get; set; }

        public string cod_bancoDestino { get; set; }

        public string bancoTit { get; set; }

        public string autenticacao { get; set; }

        public string tipo { get; set; }


        public RelatorioRemessaTitulo()
        {
        }

        public RelatorioRemessaTitulo(RemessaTit rt)
        {
            projeto = rt.Projeto.codigo.ToString();
            descricao = rt.descricao;
            cedente = rt.nome_fav_cedente;
            valor = rt.valor;
            agencia = rt.Lote.Conta.BancoAgencia.numero;
            conta_debito = rt.Lote.Conta.numero;
            nome_banco = rt.Lote.Conta.BancoAgencia.Banco.nome;
            cod_barras = rt.Boleto.ToString();
            data_pagamento = rt.Lote.data_pgto.GetValueOrDefault();
            data_vencimento = rt.dataVencto;
            num_lote = rt.id_lote;
            cod_bancoDestino = rt.BancoDestino.codigo;
            autenticacao = rt.aut_bancaria;
            if(rt.id_tipo_conciliacao.HasValue)
                tipo = rt.TipoConciliacao.nome;
            var txtcabecalho = new StringBuilder();
            txtcabecalho.AppendFormat("AUTORIZAÇÃO PARA PAGAMENTO - {0}, Agência: {1} - {2}",
                rt.Lote.Conta.BancoAgencia.nome, rt.Lote.Conta.BancoAgencia.numero, rt.Lote.Conta.BancoAgencia.digito);
            cabecalho = txtcabecalho.ToString();

            var txtConta = new StringBuilder();
            txtConta.AppendFormat("{0}-{1}",
                rt.Lote.Conta.numero, rt.Lote.Conta.digito);
            conta_debito = txtConta.ToString();

            var txtAgencia = new StringBuilder();
            txtAgencia.AppendFormat("{0}-{1}",
                rt.Lote.Conta.BancoAgencia.numero, rt.Lote.Conta.BancoAgencia.digito);
            agencia = txtAgencia.ToString();

            var txtBanco = new StringBuilder();
            txtBanco.AppendFormat("{0} - {1}",
                rt.BancoDestino.codigo, rt.BancoDestino.nome);
            bancoTit = txtBanco.ToString();
        }

        public IEnumerable<RelatorioRemessaTitulo> GerarRelatorioAgendados(int? de, int? ate,DateTime? dt)
        {
            var remTitBLL = new RemessaTitBLL();

            return (from titulo in remTitBLL.GetRemessasNaoRejeitadosPorLoteDataPagto(de, ate, dt)
                    orderby titulo.Projeto.codigo 
                    select new RelatorioRemessaTitulo(titulo)).ToList();
        }

        public IEnumerable<RelatorioRemessaTitulo> GerarRelatorioComprovantes(List<RemessaTit> lstTitulos)
        {
            return (from rel in lstTitulos
                    select new RelatorioRemessaTitulo(rel)).ToList();
        }
    }
}


