using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.BLL;
using Medusa.DAL;
using System.Text;

namespace Medusa.Relatorios.RemessaBancaria
{
    public class RelatorioRemessaPAG
    {

        public int id_remessa { get; set; }

        public string projeto { get; set; }

        public string descricao { get; set; }

        public string cedente { get; set; }
        
        public decimal valor { get; set; }

        public string conta_debito { get; set; }

        public string nome_banco { get; set; }

        public string agencia { get; set; }

        public DateTime? data_pagamento { get; set; }

        public int num_lote { get; set; }

        public string tipoInscricao { get; set; }

        public string inscricao { get; set; }

        public string forma_pagto { get; set; }

        public string cabecalho { get; set; }

        public string autenticacao { get; set; }

        public string cod_bancoCredito { get; set; }

        public string agência_credito { get; set; }

        public string conta_credito { get; set; }

        public string cod_forma_pagto { get; set; }

        public string tipo { get; set; }
        
        public RelatorioRemessaPAG()
        {
        }

        public RelatorioRemessaPAG(RemessaPAG rp)
        {
            id_remessa = rp.id_remessa;
            projeto = rp.Projeto.codigo.ToString();
            descricao = rp.descricao;
            cedente = rp.nome_fav_cedente;
            valor = rp.valor;
            agencia = rp.Lote.Conta.BancoAgencia.numero;
            conta_debito = rp.Lote.Conta.numero;
            nome_banco = rp.Lote.Conta.BancoAgencia.Banco.nome;
            data_pagamento = rp.Lote.data_pgto.GetValueOrDefault();
            num_lote = rp.id_lote;
            tipoInscricao = rp.tipoInscr.ToString();
            inscricao = rp.tipoInscr == TipoInscricao.CPF ? rp.cpf.ToString() : rp.cnpj.ToString();
            forma_pagto = rp.FormaPagto.nome;
            cod_forma_pagto = rp.FormaPagto.codigo;
            autenticacao = rp.aut_bancaria;
            if (rp.id_tipo_conciliacao.HasValue)
                tipo = rp.TipoConciliacao.nome;

            var txtConta = new StringBuilder();
            txtConta.AppendFormat("{0}-{1}",
                rp.Lote.Conta.numero, rp.Lote.Conta.digito);
            conta_debito = txtConta.ToString();

            var txtAgencia = new StringBuilder();
            txtAgencia.AppendFormat("{0}-{1}",
                rp.Lote.Conta.BancoAgencia.numero, rp.Lote.Conta.BancoAgencia.digito);
            agencia = txtAgencia.ToString();

            var txtcabecalho = new StringBuilder();
            txtcabecalho.AppendFormat("AUTORIZAÇÃO PARA LIBERAÇÃO DOS CRÉDITOS - {0}, Agência: {1} - {2}",
                rp.Lote.Conta.BancoAgencia.nome, rp.Lote.Conta.BancoAgencia.numero, rp.Lote.Conta.BancoAgencia.digito);
            cabecalho = txtcabecalho.ToString();

            cod_bancoCredito = rp.BancoDestino.codigo;
            agência_credito = String.Format("{0}-{1}", rp.agencia, rp.digito_agencia);
            conta_credito = String.Format("{0}-{1}", rp.conta, rp.digito_conta);
        }

        public IEnumerable<RelatorioRemessaPAG> GerarRelatorioAgendados(int? de, int? ate, DateTime? dt, string descricao)
        { 
            var remPagBLL = new RemessaPAGBLL();
            return (from pagtos in remPagBLL.GetRemessasNaoRejeitadosPorLoteDataPagto2(de, ate, dt, descricao)
                    orderby pagtos.Projeto.codigo
                    select new RelatorioRemessaPAG(pagtos)).ToList();
        }

        public IEnumerable<RelatorioRemessaPAG> GerarRelatorioComprovantes(List<RemessaPAG> lstPagtos)
        {
            return (from rel in lstPagtos
                    select new RelatorioRemessaPAG(rel)).ToList();
        }
    }
}


