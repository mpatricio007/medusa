using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class HistoricoProjetoBLL : AbstractCrudWithLog<HistoricoProjeto>
    {
        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.data = DateTime.Now;
            SalvarUltimoStatus();
            base.Add();
        }

        private void SalvarUltimoStatus()
        {
            var pBLL = new ProjetoBLL(_dbContext);
            pBLL.Get(ObjEF.id_projeto);
            pBLL.ObjEF.id_ultimo_status = ObjEF.id_status_projeto;
        }

        public bool RequisitosPendentes()
        {
            bool ret = false;
            if (ObjEF.id_status_projeto == StatusProjetoBLL.Inativo)
            {
                ret = ObjEF.Projeto.RequisitoEncerramento.Where(it => it.status == true).Count() > 0 ? true : false;
            }
            return ret;
        }

        public bool ValidarStatus(out string msg, int Id_status_projeto, int Id_projeto)
        {
            bool rt = true;
            var txtMsg = new StringBuilder();

            var hsp = _dbContext.Projetos.Where(it => it.id_projeto == Id_projeto).First();

            if (Id_status_projeto == StatusProjetoBLL.Ativo)
            {
                if (String.IsNullOrEmpty(hsp.sigla))
                {
                    rt = false;
                    txtMsg.AppendLine("* informe a sigla do projeto!<br/>");
                }

                if (!hsp.id_moeda.HasValue)
                {
                    rt = false;
                    txtMsg.AppendLine("* selecione a moeda!<br/>");
                }

                if (!hsp.id_instrumento_contratual.HasValue)
                {
                    rt = false;
                    txtMsg.AppendLine("* selecione o instrumento contratual!<br/>");
                }

                if (!hsp.id_unidade.HasValue)
                {
                    rt = false;
                    txtMsg.AppendLine("* selecione a unidade!<br/>");
                }

                if (String.IsNullOrEmpty(hsp.titulo))
                {
                    rt = false;
                    txtMsg.AppendLine("* informe o titulo do projeto!<br/>");
                }

                if (String.IsNullOrEmpty(hsp.objetivo))
                {
                    rt = false;
                    txtMsg.AppendLine("* informe o objetivo do projeto!<br>");
                }

                if (!hsp.data_inicio.HasValue)
                {
                    rt = false;
                    txtMsg.AppendLine("* informe a data de início do projeto<br>");
                }

                if (hsp.Coordenadores.Where(it => it.tipo == TipoCoordenador.coordenador).Count() == 0)
                {
                    rt = false;
                    txtMsg.AppendLine("* cadastre ao menos um coordenador!<br/>");
                }

                if (hsp.Financiadores.Count() == 0)
                {
                    rt = false;
                    txtMsg.AppendLine("* cadastre ao menos um financiador!<br/>");
                }

                if (hsp.Enderecos.Count() == 0)
                {
                    rt = false;
                    txtMsg.AppendLine("* cadastre ao menos um endereço!<br/>");
                }

                //if (ObjEF.Contatos.Count() == 0)
                //{
                //    rt = false;
                //    txtMsg.AppendLine("* cadastre ao menos um contato!<br/>");
                //}

                if (hsp.Documentos.Count() == 0)
                {
                    rt = false;
                    txtMsg.AppendLine("* cadastre ao menos um documento!<br/>");
                }

                //if (ObjEF.Taxas.Count() == 0)
                //{
                //    rt = false;
                //    txtMsg.AppendLine("* cadastre ao menos uma taxa!<br/>");
                //}

                if (hsp.SetorResponsavel.Count() == 0)
                {
                    rt = false;
                    txtMsg.AppendLine("* cadastre ao menos uma setor responsável!<br/>");
                }
            }

            if (Id_status_projeto == StatusProjetoBLL.Inativo)
            {
                if (hsp.RequisitoEncerramento.Where(it => it.status == true).Count() > 0)
                {
                    txtMsg.AppendLine("* requisitos para encerramento pendentes");
                    rt = false;
                }
            }

            if (rt)
                msg = String.Empty;
            else
                msg = txtMsg.ToString();

            return rt;
        }
    }
}
