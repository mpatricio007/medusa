using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL;
using System.Text;
using Medusa.LIB;

namespace Medusa.Relatorios.Adiantamentos
{
    public class RelatorioAdiantamento
    {
        public int solicitacao { get; set; }

        public string data { get; set; }

        public string data_pagto { get; set; }

        public decimal valor { get; set; }

        public string descricao { get; set; }

        public int? projeto { get; set; }

        public string coordenador { get; set; }

        public string beneficiario { get; set; }

        public string dataVencimento { get; set; }

        public string statusAdiantamento { get; set; }

        public decimal total { get; set; }

        public string textoAviso { get; set; }

        public string textoObs { get; set; }

        public string cpf { get; set; }

        public string unidade { get; set; }

        public string rp_num { get; set; }

        public string prorrogacao { get; set; }

        public string setor { get; set; }

        public string strEmailsWithDate { get; set; }

        AdiantamentoBLL admtoBLL = new AdiantamentoBLL();
        

        public RelatorioAdiantamento()
        {

        }

        public RelatorioAdiantamento(Medusa.DAL.Adiantamento a)
        {
            setAdiantamento(a);
        }

        private void setAdiantamento(Medusa.DAL.Adiantamento a)
        {
            solicitacao = a.id_adiantamento;
            data = Util.DateToString(a.data);
            valor = a.valor;
            descricao = a.descricao;
            projeto = a.Projeto.codigo;
            dataVencimento = Util.DateToString(a.data_vencimento);
            statusAdiantamento = a.StatusAdiantamento.nome;
            beneficiario = a.Beneficiario.PessoaFisica.nome;
            textoObs = a.obs;
            total = admtoBLL.GetAdiantamentosSemPrestacaoContas().Sum(it => it.valor) + a.valor;
            textoObs = a.obs;
            cpf = a.Beneficiario.PessoaFisica.cpf.Value;
            unidade = a.Beneficiario.id_unidade.HasValue ? a.Beneficiario.Unidade.nome : String.Empty;
            rp_num = a.rp;
            setor = a.Historicos.Last().id_setor.HasValue ? a.Historicos.Last().Setor.sigla : "";
            if (total > 10000)
            {
                textoAviso = "AVISO! O valor total de adiantamentos solicitados ultrapassou o valor máximo de R$10.000,00!";
            }
            else
            {
                textoAviso = String.Empty;
            }
        }

        public IEnumerable<RelatorioAdiantamento> GetAdiantamentosAbertos()
        {
            
            var lst = new List<RelatorioAdiantamento>();
            foreach (var item in admtoBLL.GetAdiantamentosVisibleRelatorio())
            {
                var r = new RelatorioAdiantamento();
                r.projeto = item.Projeto.codigo;
                r.data = Util.DateToString(item.data);
                r.dataVencimento = Util.DateToString(item.data_vencimento);
                r.statusAdiantamento = item.StatusAdiantamento.nome;
                r.beneficiario = item.Beneficiario.PessoaFisica.nome;
                r.valor = item.valor;
                //r.total = admtoBLL.GetAdiantamentosAbertos().Sum(it => it.valor) + r.valor;
                lst.Add(r);
            }
           
            return lst;
        }

        public IEnumerable<RelatorioAdiantamento> GerarRelatorio(int id_adiantamento)
        {   
            var lst = new List<RelatorioAdiantamento>();
            admtoBLL.Get(id_adiantamento);
            setAdiantamento(admtoBLL.ObjEF);
            lst.Add(this);
            return lst;
        }

        public virtual IEnumerable<RelatorioAdiantamento> GetAdiantamentosEmAtraso(int projeto, int? status, DateTime? vencto_ate)
        {
            var lst = new List<RelatorioAdiantamento>();

            foreach (var item in admtoBLL.GetAdiantamentosVencidos(projeto, vencto_ate,status.GetValueOrDefault()))
            {
                var str = new StringBuilder();
                var r = new RelatorioAdiantamento();
                r.projeto = item.Projeto.codigo;
                r.dataVencimento = Util.DateToString(item.data_vencimento);
                r.beneficiario = "-<b>NOME:</b> " + item.Beneficiario.PessoaFisica.nome + "<br/>-<b>CPF:</b> " + item.Beneficiario.PessoaFisica.cpf.Value;
                r.coordenador = item.Projeto.Coordenadores.Where(it => it.tipo == TipoCoordenador.coordenador).DefaultIfEmpty(new ProjetoCoordenadores() { Coordenador = new Coordenador() { PessoaFisica = new PessoaFisica() } }).FirstOrDefault().Coordenador.PessoaFisica.nome;
                r.cpf = item.Beneficiario.PessoaFisica.cpf.Value;
                r.valor = item.valor;
                r.data_pagto = Util.DateToString(item.data_pagamento);
                r.rp_num = item.rp;
                r.prorrogacao = Util.DateToString(item.Historicos.Where(it => it.data_prorrogacao.HasValue).DefaultIfEmpty(new HistoricoAdiantamento()).Last().data_prorrogacao);
                r.statusAdiantamento = item.StatusAdiantamento.nome;
                if (item.Beneficiario.id_unidade.HasValue)
                    r.unidade = item.Beneficiario.Unidade.sigla;
                foreach (var i in item.HistoricoEmailAdmtos.OrderByDescending(it => it.id_hist_email_admto))
                {
                    r.strEmailsWithDate = r.strEmailsWithDate + "-<b>ASSUNTO:</b> " + i.assunto + "<br/>-<b>DATA:</b> " + Util.DateToString(i.data) + "<br/>" + new string('_',25) + "<br/>";
                }

                lst.Add(r);
            }
            return lst;
        }

        public string GetTitulo(int projeto, int? status, DateTime? vencto_ate)
        {
            StringBuilder str = new StringBuilder();
            if (projeto != 0)
            {
                ProjetoBLL pBLL = new ProjetoBLL();
                pBLL.Get(projeto);
                str.AppendFormat(" do Projeto:{0}", pBLL.ObjEF.codigo);
            }
            if (status != 0)
            {
                StatusAdiantamentoBLL saBLL = new StatusAdiantamentoBLL();
                saBLL.Get(status);
                str.AppendFormat(" de Status: {0}", saBLL.ObjEF.nome);
            }
            if (vencto_ate.HasValue)
                str.AppendFormat(" com Vencimento até:{0}", Util.DateToString(vencto_ate));
            return String.IsNullOrEmpty(str.ToString()) ? String.Empty : str.ToString();
        }
    }
}


