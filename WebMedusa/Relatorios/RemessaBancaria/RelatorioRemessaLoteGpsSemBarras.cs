using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.BLL;
using Medusa.DAL;
using System.Text;

namespace Medusa.Relatorios.RemessaBancaria
{
    public class RelatorioRemessaLoteGpsSemBarras
    {
        public int id_remessa { get; set; }

        public string projeto { get; set; }

        public string cedente { get; set; }

        public string descricao { get; set; }

        public string conta { get; set; }

        public string agencia { get; set; }
        
        public decimal valor_gps { get; set; }

        public DateTime? data_pagamento { get; set; }

        public int num_lote { get; set; }

        public string identificador { get; set; }

        public string mes_ano { get; set; }

        public string codigo_pagto { get; set; }

        public string cabecalho { get; set; }

        public string autenticacao { get; set; }

        public DateTime data_vencto { get; set; }

        public decimal valor_total { get; set; }

        public decimal outras_entidades { get; set; }

        public decimal atualização_monetaria { get; set; }

        public string tipo_retorno { get; set; }

        public string tipo { get; set; }
        
        public RelatorioRemessaLoteGpsSemBarras()
        {
            
        }

        public RelatorioRemessaLoteGpsSemBarras(RemessaGpsSemCodBarra rgps)
        {
            id_remessa = rgps.id_remessa;
            projeto = rgps.Projeto.cod_def_projeto.ToString();
            cedente = rgps.nome_fav_cedente;
            descricao = rgps.descricao;
            conta = rgps.Lote.Conta.numero;
            valor_gps = rgps.valor_gps;
            data_pagamento = rgps.Lote.data_pgto.GetValueOrDefault();
            num_lote = rgps.id_lote;
            identificador = rgps.id_contribuinte;
            mes_ano = rgps.mes_ano;
            codigo_pagto = rgps.cod_receita;
            autenticacao = rgps.aut_bancaria;
            valor_total = rgps.valor;
            data_vencto = rgps.dataVencto;
            outras_entidades = rgps.outras_entidades;
            atualização_monetaria = rgps.atual_monetaria;
            tipo_retorno = rgps.TipoRetorno.descricao;
            if (rgps.id_tipo_conciliacao.HasValue)
                tipo = rgps.TipoConciliacao.nome;

            var txtConta = new StringBuilder();
            txtConta.AppendFormat("{0}-{1}",
                rgps.Lote.Conta.numero, rgps.Lote.Conta.digito);
            conta = txtConta.ToString();
            
            var txtAgencia = new StringBuilder();
            txtAgencia.AppendFormat("{0}-{1}",
                rgps.Lote.Conta.BancoAgencia.numero, rgps.Lote.Conta.BancoAgencia.digito);
            agencia = txtAgencia.ToString();

            var txtcabecalho = new StringBuilder();
            txtcabecalho.AppendFormat("AUTORIZAÇÃO PARA LIBERAÇÃO DOS CRÉDITOS - {0}, Agência: {1} - {2}",
                rgps.Lote.Conta.BancoAgencia.nome, rgps.Lote.Conta.BancoAgencia.numero, rgps.Lote.Conta.BancoAgencia.digito);
            cabecalho = txtcabecalho.ToString();

        }

        public IEnumerable<RelatorioRemessaLoteGpsSemBarras> GerarRelatorio(int? de, int? ate, DateTime? dt)
        {
            var remGpsBLL = new RemessaGpsSemCodBarraBLL();
            return (from pagtos in remGpsBLL.GetRemessasNaoRejeitadosPorLoteDataPagto(de, ate, dt)
                    orderby pagtos.Projeto.codigo
                    select new RelatorioRemessaLoteGpsSemBarras(pagtos)).ToList();
        }

        public IEnumerable<RelatorioRemessaLoteGpsSemBarras> GerarRelatorioComprovantes(List<RemessaGpsSemCodBarra> lstGru)
        {
            return (from rel in lstGru
                    select new RelatorioRemessaLoteGpsSemBarras(rel)).ToList();
        }
    }
}

