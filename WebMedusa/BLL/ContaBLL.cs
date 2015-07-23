using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Collections;

namespace Medusa.BLL
{
    public class ContaBLL : AbstractCrudWithLog<Conta>
    {

        public static Dictionary<string, string> dicSaldoConta = new Dictionary<string, string>()
        {            
            {"numero","numero"},
            {"id_conta","id_conta"},
            {"Projeto.codigo","cod_def_projeto"},
            {"BancoAgencia.Banco.nome","banco"},
            {"BancoAgencia.nome","agencia"},            
        };

        public IEnumerable<SaldoConta> Lista_SaldoContas(DateTime data, List<Filter> lstFilters, string sortExpression, string sortDirection, int top)
        {
  
            if (top == 0)
                return _dbContext.Lista_Saldocontas(data).Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList();
            else
                return _dbContext.Lista_Saldocontas(data).Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }

        public decimal GetSaldoNaData(DateTime data)
        {
            return _dbContext.SaldoConta_PorData(data, ObjEF.id_conta);
        }

        public IEnumerable<ExtratoConta> ExtratoPeriodo(DateTime dtDe, DateTime dtAte)
        {
            return _dbContext.ExtratoConta_PorPeriodo(ObjEF.id_conta, dtDe, dtAte).ToList();
        }

        public IEnumerable<ContaAplicacao> GetAplicacoesPeriodo(DateTime dtDe, DateTime dtAte)
        {
            return ObjEF.ContaAplicacao != null ? ObjEF.ContaAplicacao.Where(it => it.data >= dtDe & it.data <= dtAte).ToList()
                : new List<ContaAplicacao>();
        }

        public ContaBLL()
        {
         
        }

        public ContaBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
            this._dbSet = _dbContext.Set<Conta>();
        }

        public List<Conta> GetAllAg1897x(int id_projeto)
        {
            if (id_projeto != 0)
            {
                var pBLL = new ProjetoBLL(_dbContext);
                pBLL.Get(id_projeto);
                

                if (pBLL.TemContaEspecifica())
                    return _dbSet.Where(it => it.status == true & it.BancoAgencia.numero == "1897" & it.id_projeto == id_projeto
                        & it.id_tipoconta != ContaTipoBLL.BLOQUEADO)
                        .OrderBy(it => it.id_tipoconta).ThenBy(it => it.numero).ToList();
                if (pBLL.EhSubProjeto())
                {
                    pBLL.ObjEF = pBLL.GetProjetoPai();
                    if (pBLL.Exists())
                        if (pBLL.TemContaEspecifica())
                            return _dbSet.Where(it => it.status == true & it.BancoAgencia.numero == "1897" & it.id_projeto == pBLL.ObjEF.id_projeto
                                & it.id_tipoconta != ContaTipoBLL.BLOQUEADO)
                                .OrderBy(it => it.id_tipoconta).ThenBy(it => it.numero).ToList();
                }                
                return _dbSet.Where(it => it.status == true & it.BancoAgencia.numero == "1897" & it.ContaTipo.conta_especifico == false & it.id_tipoconta != ContaTipoBLL.BLOQUEADO).ToList();
            }
           return null;
        }

        public List<Projeto> GetAllProjetosPorConta()
        {
            var ds = new List<Projeto>();
            if (ObjEF.id_conta != 0)
            {
                var pBLL = new ProjetoBLL(_dbContext);
                pBLL.Get(ObjEF.id_projeto);

                if (pBLL.Exists())
                {

                    if (pBLL.TemContaEspecifica())
                    {
                        
                        if (!pBLL.EhSubProjeto())
                        {
                            ds = _dbContext.Projetos.Where(it => it.cod_def_projeto == pBLL.ObjEF.cod_def_projeto &
                               it.Contas.Where(k => k.ContaTipo.conta_especifico).Count() == 0).ToList();

                        }
                        ds.Add(pBLL.ObjEF);
                    }
                    else
                        ds = _dbContext.Projetos.Where(it => it.cod_def_projeto.HasValue && it.Contas.Where(k => k.ContaTipo.conta_especifico && k.status == true).Count() == 0).ToList();
                    
                }
                else
                    ds = _dbContext.Projetos.Where(it => it.cod_def_projeto.HasValue && it.Contas.Where(k => k.ContaTipo.conta_especifico && k.status == true).Count() == 0 ).ToList();
            }
            return ds.OrderBy(it => it.codigo).ToList();
        }
    }
}