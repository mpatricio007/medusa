using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class ProjetoSolicitacaoBLL : AbstractCrudWithLog<ProjetoSolicitacao>
    {

        
        public ProjetoSolicitacaoBLL()
        {
        }

        public ProjetoSolicitacaoBLL(Contexto ctx)
        {
            _dbContext = ctx;
        }
        public bool EhProposta 
        { 
            get
            {
                return ObjEF.id_tipo_solicitacao == 2;
            }
        }

        public bool EhPA
        {
            get
            {
                return ObjEF.id_ultimo_status == 4;
            }
        }

        public string GerarProjetoA(Int32 cod_a, string sub_a)
        {
            var p = new ProjetoBLL(_dbContext);
            var msgPa = new StringBuilder();
            int? pa = 0;      
            try
            {
                if (!TemPA(out pa))
                {
                    if (!p.ExisteProjetoA(cod_a, sub_a))
                    {
                        p.ObjEF.cod_a_projeto = cod_a;
                        p.ObjEF.sub_projeto_a = sub_a;
                        msgPa.AppendFormat("Projeto A n°: {0} gerado com sucesso! <br/>", p.ObjEF.cod_a_projeto);

                        //p.ObjEF.id_sol_proj = ObjEF.id_sol_proj;
                        p.ObjEF.ProjetoSolicitacao = ObjEF;
                        if (ObjEF.id_coordenador.HasValue)
                            p.ObjEF.Coordenadores.Add(new ProjetoCoordenadores()
                            {
                                id_coordenador = ObjEF.id_coordenador.GetValueOrDefault(),
                                tipo = TipoCoordenador.coordenador
                            });
                        else
                            msgPa.AppendFormat("- cadastrar coordenador: {0} <br/>", objEF.strCoordenador);


                        if (ObjEF.id_sub_coordenador.HasValue)
                            p.ObjEF.Coordenadores.Add(new ProjetoCoordenadores()
                            {
                                id_coordenador = ObjEF.id_sub_coordenador.GetValueOrDefault(),
                                tipo = TipoCoordenador.subcoordenador
                            });
                        else
                            msgPa.AppendFormat("- cadastrar sub coordenador: {0} <br/>", objEF.strSubCoordenador);

                        if (ObjEF.id_financiador.HasValue)
                            p.ObjEF.Financiadores.Add(new ProjetoFinanciador()
                            {
                                id_financiador = ObjEF.id_financiador.GetValueOrDefault()
                            });
                        else
                            msgPa.AppendFormat("- cadastrar financiador: {0}", objEF.strFinanciador);

                        p.ObjEF.titulo = ObjEF.titulo;

                        p.ObjEF.data_cod_a = DateTime.Now;

                        p.ObjEF.id_unidade = ObjEF.id_unidade;

                        p.ObjEF.id_departamento = ObjEF.id_departamento;

                        p.ObjEF.id_laboratorio = ObjEF.id_laboratorio;

                        p.ObjEF.objetivo = ObjEF.descricao;

                        p.ObjEF.id_moeda = ObjEF.id_moeda;

                        p.ObjEF.valor = ObjEF.valor_global;

                        p.ObjEF.data_inicio = ObjEF.inicio;

                        p.ObjEF.data_termino = ObjEF.termino;

                        p.ObjEF.id_instrumento_contratual = ObjEF.id_instrumento_contratual;

                        p.ObjEF.num_contrato = ObjEF.contrato_patrocinio;

                        p.ObjEF.pendencias = msgPa.ToString();

                        p.Add();

                        TornarPA();
                        if(!SaveChanges())
                            return "Erro ao gerar projeto A!";

                    }
                    else
                        msgPa.AppendFormat("Proposta de abertura de projeto já possui o Projeto A n°: {0} ", pa);
                }
                else
                    msgPa.AppendFormat("Já existe este projéto");
            }
            catch (Exception)
            {
                return "Erro ao gerar projeto A!";
            }
            return msgPa.ToString();                
        }

        public bool TemPA(out int? pa)
        {
            pa = 0;
            try
            {
                pa = _dbContext.Projetos.FirstOrDefault(it => it.ProjetoSolicitacao.id_sol_proj == ObjEF.id_sol_proj).cod_a_projeto;
            }
            catch (Exception)
            {
            }
            return pa != 0;
        }

        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.data_solicitacao = DateTime.Now;
            ObjEF.codigo = ProximoPX();
            set();
            base.Add();
            SalvarPrimeiroStatus();
        }

        public int ProximoPX()
        {
            return _dbSet.Max(c => (int?)c.codigo).GetValueOrDefault() + 1;
        }

        private void SalvarPrimeiroStatus()
        {
            var hpxBLL = new HistoricoPxBLL(_dbContext);
            hpxBLL.ObjEF.data = ObjEF.data_solicitacao;
            hpxBLL.ObjEF.id_usuario = ObjEF.id_usuario;
            hpxBLL.ObjEF.id_sol_proj = ObjEF.id_sol_proj;
            hpxBLL.ObjEF.id_status_solicitacao = _dbContext.StatusSolicitacoes.OrderBy(it => it.ordem).First().id_status_solicitacao;
            hpxBLL.Add();

            //salvao o utimo status
            ObjEF.id_ultimo_status = hpxBLL.ObjEF.id_status_solicitacao;
        }
       
        public void TornarPA()
        {
            var hpxBLL = new HistoricoPxBLL(_dbContext);
            hpxBLL.ObjEF.data = ObjEF.data_solicitacao;
            hpxBLL.ObjEF.id_usuario = ObjEF.id_usuario;
            hpxBLL.ObjEF.id_sol_proj = ObjEF.id_sol_proj;
            hpxBLL.ObjEF.id_status_solicitacao = _dbContext.StatusSolicitacoes.OrderByDescending(it => it.ordem).First().id_status_solicitacao;
            hpxBLL.Add();

            //salva o ultimo status
            ObjEF.id_ultimo_status = hpxBLL.ObjEF.id_status_solicitacao;
        }

        public override void Update()
        {
            set();
            if (_dbContext.Entry(ObjEF).Property(it => it.id_tipo_solicitacao).IsModified)
            {
                //set novo historico
                var hpx = new HistoricoPx();
                hpx.data = ObjEF.data_solicitacao;
                hpx.id_usuario = ObjEF.id_usuario;
                hpx.id_sol_proj = ObjEF.id_sol_proj;
                
                //verificar depois
                hpx.id_status_solicitacao = 6;
                ObjEF.Historicos.Add(hpx);
                ObjEF.id_ultimo_status = hpx.id_status_solicitacao;
            }
            else
            {
                //update normal
                base.Update();
            }
        }

        public override bool SaveChanges()
        {

            return base.SaveChanges();
        }

        public bool Exists()
        {
            return ObjEF.id_sol_proj != 0;
        }

        private void set()
        {
            var coordenador = _dbContext.Coordenadores.Where(it => it.PessoaFisica.nome == ObjEF.strCoordenador).FirstOrDefault();
            if(coordenador != null)
                ObjEF.id_coordenador =  coordenador.id_coordenador;

            coordenador = _dbContext.Coordenadores.Where(it => it.PessoaFisica.nome == ObjEF.strSubCoordenador).FirstOrDefault();
            if (coordenador != null)
                ObjEF.id_sub_coordenador = coordenador.id_coordenador;


            var financiador = _dbContext.Financiadores.Where(it => it.nome == ObjEF.strFinanciador).FirstOrDefault();
            if(financiador != null)
                ObjEF.id_financiador = financiador.id_financiador;
        }

    }
}