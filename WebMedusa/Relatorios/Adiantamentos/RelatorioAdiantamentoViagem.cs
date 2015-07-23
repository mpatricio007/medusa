using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.BLL;
using Medusa.LIB;
using System.Text;
using Medusa.DAL;

namespace Medusa.Relatorios.Adiantamentos
{
    public class RelatorioAdiantamentoViagem : RelatorioAdiantamento
    {
        public string dataPartida { get; set; }

        private AdiantamentoViagemBLL admtoViagemBLL = new AdiantamentoViagemBLL();
        public RelatorioAdiantamentoViagem()
        {
        }

        public RelatorioAdiantamentoViagem(Medusa.DAL.AdiantamentoViagem av)
        {
            dataPartida = Util.DateToString(av.data_partida);
            solicitacao = av.id_adiantamento;
            data = Util.DateToString(av.data);
            valor = av.valor;
            descricao = av.descricao;
            projeto = av.Projeto.codigo.GetValueOrDefault();
            dataVencimento = Util.DateToString(av.data_vencimento);
            statusAdiantamento = av.StatusAdiantamento.nome;
            beneficiario = av.Beneficiario.PessoaFisica.nome;
            textoObs = av.obs;
            total = av.valor;
            textoObs = av.obs;
            rp_num = av.rp;
            setor = av.Historicos.Last().id_setor.HasValue ? av.Historicos.Last().Setor.sigla : "";
            if (total > 10000)
            {
                 textoAviso = "AVISO! O valor total de adiantamentos solicitados ultrapassou o valor máximo de R$10.000,00!";
            }
            else
            {
                textoAviso = "";
            }
        }

        public IEnumerable<RelatorioAdiantamentoViagem> GerarRelatorioViagem(int id_adiantamento)
        {
            var lst = new List<RelatorioAdiantamentoViagem>();
            admtoViagemBLL.Get(id_adiantamento);
            lst.Add(new RelatorioAdiantamentoViagem(admtoViagemBLL.ObjEF));
            return lst;
        }

        public override IEnumerable<RelatorioAdiantamento> GetAdiantamentosEmAtraso(int projeto,int? status ,DateTime? vencto_ate)
        {
            var lst = new List<RelatorioAdiantamento>();
            var admBLL = new AdiantamentoViagemBLL();
            foreach (var item in admBLL.GetAdiantamentosVencidos(projeto, vencto_ate, status))
            {
                var str = new StringBuilder();
                var r = new RelatorioAdiantamento();
                r.projeto = item.Projeto.codigo;
                r.dataVencimento = Util.DateToString(item.data_vencimento);
                r.beneficiario = "-<b>NOME:</b> " + item.Beneficiario.PessoaFisica.nome + "<br/>-<b>CPF:</b> " + item.Beneficiario.PessoaFisica.cpf.Value;
                r.coordenador = item.Projeto.Coordenadores.Where(it => it.tipo == TipoCoordenador.coordenador).DefaultIfEmpty(new ProjetoCoordenadores() { Coordenador = new Coordenador() { PessoaFisica = new PessoaFisica() } }).FirstOrDefault().Coordenador.PessoaFisica.nome;
                r.valor = item.valor;
                r.rp_num = item.rp;
                r.data_pagto = Util.DateToString(item.data_pagamento);
                r.statusAdiantamento = item.StatusAdiantamento.nome;
                if (item.Beneficiario.id_unidade.HasValue)
                    r.unidade = item.Beneficiario.Unidade.sigla;
                foreach (var i in item.HistoricoEmailAdmtos.OrderByDescending(it => it.id_hist_email_admto))
                {
                    r.strEmailsWithDate = r.strEmailsWithDate + "-<b>ASSUNTO:</b> " + i.assunto + "<br/>-<b>DATA:</b> " + Util.DateToString(i.data) + "<br/>" + new string('_', 25) + "<br/>";
                }
                lst.Add(r);


            }
            return lst; 
        }
    }
}


   