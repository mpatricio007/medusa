using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;
using System.Web.UI.WebControls;

namespace Medusa.BLL
{
    public abstract class AbstractAdiantamentoBLL<T> : AbstractCrudWithLog<T> where T : Adiantamento, new()
    {
        public override void Add()
        {
            SalvarPrimeiroStatus();
            //GerarValidade();
            base.Add();
        }

        //public void Concluir()
        //{
        //    var histAdiantamentoBLL = new HistoricoAdiantamentoBLL(_dbContext);
        //    histAdiantamentoBLL.ObjEF.data = DateTime.Now;
        //    histAdiantamentoBLL.ObjEF.id_status_admto = StatusAdiantamentoBLL.eh_concluido;
        //    histAdiantamentoBLL.ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
        //    histAdiantamentoBLL.ObjEF.adiantamento = ObjEF;
        //    ObjEF.id_ultimo_status = histAdiantamentoBLL.ObjEF.id_status_admto;
        //    histAdiantamentoBLL.Add();
        //}

        //public void Cancelar(string strJustificativa)
        //{
        //    var histAdiantamentoBLL = new HistoricoAdiantamentoBLL(_dbContext);
        //    histAdiantamentoBLL.ObjEF.data = DateTime.Now;
        //    histAdiantamentoBLL.ObjEF.id_status_admto = StatusAdiantamentoBLL.eh_cancelado;
        //    histAdiantamentoBLL.ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
        //    histAdiantamentoBLL.ObjEF.adiantamento = ObjEF;
        //    histAdiantamentoBLL.ObjEF.observacao = strJustificativa;
        //    ObjEF.id_ultimo_status = histAdiantamentoBLL.ObjEF.id_status_admto;
        //    histAdiantamentoBLL.Add();
        //}

        //public void Update(string justificativa)
        //{
        //    var histAdiantamentoBLL = new HistoricoAdiantamentoBLL(_dbContext);
        //    histAdiantamentoBLL.ObjEF.data = DateTime.Now;
        //    histAdiantamentoBLL.ObjEF.id_status_admto = StatusAdiantamentoBLL.eh_aberto;
        //    histAdiantamentoBLL.ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
        //    histAdiantamentoBLL.ObjEF.adiantamento = ObjEF;
        //    histAdiantamentoBLL.ObjEF.observacao = justificativa;
        //    ObjEF.id_ultimo_status = histAdiantamentoBLL.ObjEF.id_status_admto;
        //    histAdiantamentoBLL.Add();
        //}

        public abstract void GerarValidade();       

        public int Tipo
        {
            get
            {
                if (ObjEF is AdiantamentoViagem)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }

        internal void SalvarPrimeiroStatus()
        {
            var histAdiantamentoBLL = new HistoricoAdiantamentoBLL(_dbContext);
            histAdiantamentoBLL.ObjEF.data = DateTime.Now;
            histAdiantamentoBLL.ObjEF.id_status_admto = StatusAdiantamentoBLL.primeiro_status;
            histAdiantamentoBLL.ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            histAdiantamentoBLL.ObjEF.adiantamento = ObjEF;
            ObjEF.id_ultimo_status = histAdiantamentoBLL.ObjEF.id_status_admto;
            histAdiantamentoBLL.Add();
        }

        public bool EhAberto()
        {
            return ObjEF.id_ultimo_status == StatusAdiantamentoBLL.eh_aberto; 
        }

        public bool EhCancelado()
        {
            return ObjEF.id_ultimo_status == StatusAdiantamentoBLL.eh_cancelado;
        }

        public bool EhAprovado()
        {
            return ObjEF.id_ultimo_status == StatusAdiantamentoBLL.eh_aprovado; 
        }

        public bool EhConcluido()
        {
            return ObjEF.id_ultimo_status == StatusAdiantamentoBLL.eh_concluido;
        }

        public bool PrestarContas()
        {
            return EhAprovado() & !ObjEF.data_rd.HasValue & ObjEF.data_vencimento.HasValue;
        }

        public bool EhReprovado()
        {
            return ObjEF.id_ultimo_status == StatusAdiantamentoBLL.eh_reprovado;
        }

        public bool EstaBloqueado()
        {
            return EhConcluido() || EhReprovado() || EhAberto() || EhCancelado();
        }

        public bool PermiteCancelamento()
        {
            return Exists() & (EhAberto() || EhAprovado());
        }

        public bool PermitidoAlteracaoInclusao()
        {
            return !Exists() || (!EstaBloqueado());
        }
        
        public List<T> GetAdiantamentosVencidos(int? id_projeto, DateTime? vencto_ate, int? status)
        {
            var lst = new List<Filter>();

            var filt = new Filter() { property = "id_tipo_admto", value = Convert.ToString(Tipo), mode = "==" };
            lst.Add(filt);

            if (id_projeto != 0)
            {
                var f1 = new Filter() { property = "id_projeto", value = Convert.ToString(id_projeto), mode = "==" };
                lst.Add(f1);
            }

            if (vencto_ate.HasValue)
            {
                var f2 = new Filter() { property = "data_vencimento", value = Convert.ToString(vencto_ate), mode = "<=" };
                lst.Add(f2);
            }

            if (status != 0)
            {
               var f3 = new Filter() { property = "id_ultimo_status", value = Convert.ToString(status), mode = "==" };
               lst.Add(f3);
            }

            return _dbSet.Where(lst).ToList();

            //if (id_projeto == 0)
            //    return _dbSet.Where(it => it.data_vencimento < DateTime.Now & it.id_ultimo_status == StatusAdiantamentoBLL.eh_aprovado & it.id_tipo_admto == Tipo).
            //        OrderBy(it => it.Projeto.codigo).ThenBy(it => it.Beneficiario.PessoaFisica.nome).ToList();
            //else
            //    return _dbSet.Where(it => it.id_projeto == id_projeto & it.data_vencimento < DateTime.Now & it.id_ultimo_status == StatusAdiantamentoBLL.eh_aprovado & it.id_tipo_admto == Tipo).
            //        OrderBy(it => it.Projeto.codigo).ThenBy(it => it.Beneficiario.PessoaFisica.nome).ToList();
        }

        public List<EmailPadrao> GetEmailsPadraoThisType()
        {
            return ObjEF.TiposAdiantamento.EmailPadroes.Where(it => it.id_tipo_admto == Tipo).ToList();
        }

        public bool EnviarEmail(string[] copias, string[] destinatarios, string assunto, string corpo, out string saida)
        {
            bool ret = true;
            var strSaida = new StringBuilder();

            if (ObjEF.Projeto.Coordenadores.Where(it => it.tipo == TipoCoordenador.coordenador).Count() == 0)
            {
                strSaida.AppendLine("* projeto não possui nenhum coordenador cadastrado!");
                ret = false;
            }

            if (ObjEF.Projeto.Contatos.Where(it => it.Notificacoes.Where(it2 => it2.id_sol_de_proj == SolicitacaoDeProjetoBLL.adiantamento).Count() > 0).Count() == 0)
            {
                strSaida.AppendLine("* projeto não possui nenhum contato que receba notificações de adiantamento/diarias!");
                ret = false;
            }

            saida = strSaida.ToString();

            if (ret)
            {
                var sendemail = new SendEmail(ContaEmail.fusp);
                sendemail.Destinatarios = ObjEF.Projeto.Coordenadores.Where(it => it.tipo == TipoCoordenador.coordenador).Select(it => it.Coordenador.email).ToArray().Union(destinatarios).ToArray();
                sendemail.DestinatariosBcc = ObjEF.Projeto.Contatos.Where(it => it.Notificacoes.Where(it2 => it2.id_sol_de_proj == SolicitacaoDeProjetoBLL.adiantamento).Count() > 0).Select(it => it.Contato.email).Union(copias).ToArray();
                sendemail.Subject = assunto;
                sendemail.Body = corpo;
                if (sendemail.Send())
                {
                    var emails = new StringBuilder();
                    emails.Append("* e-mail enviados para: ");
                    foreach (var item in sendemail.Destinatarios)
                        emails.Append(String.Format("- {0} ", item));
                    emails.Append("com sucesso!");
                    saida = emails.ToString();
                    SalvarHistoricoEmail(assunto);
                }
                else
                {
                    saida = "Erro ao enviar e-mail!";
                    ret = false;
                }
            }

            return ret;
        }

        public void SalvarHistoricoEmail(string assunto)
        {
            var hstEmail = new HistoricoEmailAdmtoBLL();
            hstEmail.ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            hstEmail.ObjEF.data = DateTime.Now;
            hstEmail.ObjEF.id_adiantamento = ObjEF.id_adiantamento;
            hstEmail.ObjEF.assunto = assunto;
            hstEmail.Add();
            hstEmail.SaveChanges();
        }

        public bool Exists()
        {
            return ObjEF.id_adiantamento != 0;
        }
    }
}

