using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class RemessaBLL<T> : AbstractCrudWithLog<T>,IPagamento  where T:  Remessa, new()
    {
        public bool EstaAgendada()
        {
            return (ObjEF.id_tipo_ret == TipoRetornoBLL.Agendado);
        }

        public bool EstaEstornada()
        {
            return (ObjEF.id_tipo_ret == TipoRetornoBLL.Estornado);
        }

        public bool EstaRejeitada()
        {
            return (ObjEF.id_tipo_ret == TipoRetornoBLL.Rejeitado);
        }

        public bool EstaEnviada()
        {
            return (ObjEF.id_tipo_ret == TipoRetornoBLL.Enviado);
        }

        public bool EstaIncluida()
        {
            return (ObjEF.id_tipo_ret == TipoRetornoBLL.InclusaoEfetuada);
        }

        public bool Estornar(ref string msg)
        {
            bool rt = false;
            if (EstaAgendada() & !EstaBloqueado())
            {
                ObjEF.id_tipo_ret = TipoRetornoBLL.Estornado;
                Update();
                if (SaveChanges())
                {
                    msg = "Pagamento estornado com sucesso!";
                    rt = true;
                }
                else
                    msg = "Erro ao estornar pagamento!";
            }
            else
                msg = "Este pagamento já está processado!";
            return rt;
        }

        public virtual bool Alterar(ref string msg)
        {
            bool rt = false;
            if (EstaAgendada() & (!EstaBloqueado()))
            {
                if (DataIsValid(ref msg))
                {
                    Update();
                    if (SaveChanges())
                    {
                        msg = "Pagamento alterado!";
                        rt = true;
                    }
                    else
                        msg = "Erro ao alterar pagamento!";
                }
            }
            else
                msg = "Este pagamento já está processado!";
            return rt;
        }

        public virtual bool Agendar(ref string msg)
        {
            bool rt = false;
            if(DataIsValid(ref msg))
            {
                ObjEF.id_tipo_ret = TipoRetornoBLL.Agendado;
                Add();
                if (SaveChanges())
                {
                    msg = "pagamento agendado!";
                    rt = true;
                }
                else
                    msg = "erro ao agendar pagamento!";
            }
            return rt;
        }

        public virtual bool Agendar()
        {
            string msg = "";
            return Agendar(ref msg);
        }

        public virtual void Processar()
        {
            ObjEF.id_tipo_ret = TipoRetornoBLL.Enviado;
            Update();
            SaveChanges();
        }

        public virtual void Conciliar(string codigoRetorno, string autBancaria,int intTipoConciliacao, int intId_arquivo)
        {
            try
            {
                if (!EstaBloqueado())
                {

                    ObjEF.aut_bancaria = !String.IsNullOrEmpty(autBancaria) ? String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
                                        autBancaria.Substring(0, 1),
                                        autBancaria.Substring(1, 3),
                                        autBancaria.Substring(4, 3),
                                        autBancaria.Substring(7, 3),
                                        autBancaria.Substring(10, 3),
                                        autBancaria.Substring(13, 3)) : String.Empty;
                   

                    ObjEF.id_tipo_ret = TipoRetornoBLL.RetornaIdpeloCodigo(codigoRetorno);
                    ObjEF.data_conciliacao = DateTime.Now;
                    ObjEF.id_tipo_conciliacao = intTipoConciliacao;
                    ObjEF.id_arquivo = intId_arquivo;
                    Update();
                    SaveChanges();
                }
            }
            catch (Exception)
            {                
             
            }
           
        }

        public virtual void ImprimirComprovante()
        {
            
        }

        public virtual bool EstaBloqueado()
        {
            return ObjEF.id_tipo_conciliacao.HasValue ? ObjEF.id_tipo_conciliacao == TipoConciliacaoBLL.CONSOLIDAD : false;
        }

        public virtual void EstornarConciliacao()
        {
            ObjEF.id_tipo_ret = TipoRetornoBLL.Enviado;
            ObjEF.id_tipo_conciliacao = null;
            ObjEF.aut_bancaria = null;
            ObjEF.data_conciliacao = null;
            Update();
            SaveChanges();
        }

        public virtual bool RetornoEhValido()
        {
            return true;
        }

        public bool Exists()
        {
            return ObjEF.id_remessa != 0;
        }

        public List<T> GetRemessasNaoRejeitadosPorLoteDataPagto(int? de, int? ate, DateTime? dt)
        {
            var l = new List<Func<T, bool>>();

            if (de != 0)
            {
                Func<T, bool> fDe = it => it.id_lote >= (int)de;
                l.Add(fDe);
            }

            if (ate != 0)
            {
                Func<T, bool> fAte = it => it.id_lote <= (int)ate;
                l.Add(fAte);
            }

            if (dt.HasValue)
            {
                Func<T, bool> fDt = it => it.LoteRemessa.data_pgto == (DateTime)dt;
                l.Add(fDt);
            }

            var ds = _dbSet.Where(it => (it.id_tipo_ret != TipoRetornoBLL.Estornado & it.id_tipo_ret != TipoRetornoBLL.Rejeitado)).ToList();

            foreach (var item in l)
                ds = ds.Where(item).ToList();

            return ds;
        }

        public List<T> GetRemessasNaoRejeitadosPorLoteDataPagto2(int? de, int? ate, DateTime? dt, string descricao)
        {
            var l = new List<Func<T, bool>>();

            if (de != 0)
            {
                Func<T, bool> fDe = it => it.id_lote >= (int)de;
                l.Add(fDe);
            }

            if (ate != 0)
            {
                Func<T, bool> fAte = it => it.id_lote <= (int)ate;
                l.Add(fAte);
            }

            if (dt.HasValue)
            {
                Func<T, bool> fDt = it => it.LoteRemessa.data_pgto == (DateTime)dt;
                l.Add(fDt);
            }

            if (descricao != "")
            {
                Func<T, bool> fDescr = it => it.LoteRemessa.descricao == (string)descricao;
                l.Add(fDescr);
            }

            var ds = _dbSet.Where(it => (it.id_tipo_ret != TipoRetornoBLL.Estornado & it.id_tipo_ret != TipoRetornoBLL.Rejeitado)).ToList();

            foreach (var item in l)
                ds = ds.Where(item).ToList();

            return ds;
        }


        public bool Rejeitar(string motivo, ref string msg)
        {
            bool rt = false;
            if (EstaEnviada() || EstaIncluida())
            {
                ObjEF.id_tipo_ret = TipoRetornoBLL.Rejeitado;
                ObjEF.motivo_rejeicao = motivo;
                Update();
                if (SaveChanges())
                {
                    msg = "** pagamento rejeitado com sucesso !!";
                    rt = true;
                }
                else
                    msg = "** erro ao tentar rejeitar o pagamento";
            }
            else
                msg = "Opção inválida no momento";
            return rt;
        }       

        public override bool DataIsValid(ref string strMsg)
        {
            var lote = _dbContext.Lotes.Find(ObjEF.id_lote);
            
            bool projLoteTemContaEsp = false;
            bool projetoLoteEhValido = true;
            
            var projBLL = new ProjetoBLL(_dbContext);
            projBLL.Get(ObjEF.id_projeto);
            bool projetoRemEhValido = projBLL.DataIsValid(lote.data_pgto.GetValueOrDefault(), ref strMsg);

            if (!projetoRemEhValido)
                return false;

            bool projRemTemContaEsp = projBLL.TemContaEspecifica();

            int? id_projeto_lote = lote.Conta.id_projeto;

            if (id_projeto_lote.HasValue)
            {
                projBLL.Get(id_projeto_lote.GetValueOrDefault());
                projetoLoteEhValido = projBLL.DataIsValid(lote.data_pgto.GetValueOrDefault(), ref strMsg);
                if (!projetoLoteEhValido)
                    return false;
                else
                    projLoteTemContaEsp = projBLL.TemContaEspecifica();
            }

            if (projLoteTemContaEsp)
            {
                var contaBLL = new ContaBLL(_dbContext);
                contaBLL.ObjEF = lote.Conta;
                
                if (!contaBLL.GetAllProjetosPorConta().Select(it => it.id_projeto).Contains(ObjEF.id_projeto))
                {
                    strMsg = String.Format("Este lote possui conta específica do projeto {0}!", projBLL.ObjEF.codigo);
                    return false;
                }
            }
            else if (projRemTemContaEsp)
            {
                if (projBLL.ObjEF.id_projeto != ObjEF.id_projeto)
                {
                    strMsg = String.Format("Este projeto {0} possui conta específica!", projBLL.ObjEF.codigo);
                    return false;
                }
            }

            return true;
        }


        public void ProcessarContaLacto(ContaLancto cl)
        {
            
            ObjEF.ContaLancto = cl;
        }
        public IEnumerable<Remessa> Find(List<Filter> lstFilters, string sortExpression, string sortDirection, int size, int id_tipo_ret)
        {
            if (id_tipo_ret != 0)
            {
                var Remessas = _dbSet.Where(it => it.id_tipo_ret == id_tipo_ret).ToList();

                var customFilters = new List<Filter>();
                if (Remessas.Count() == 0)
                {
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_remessa",
                        property_name = "id_remessa",
                        value = "0"
                    });
                }
                foreach (var r in Remessas)
                {
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_remessa",
                        value = r.id_remessa.ToString(),
                    });
                }

                foreach (var item in customFilters)
                {
                    if (!lstFilters.Contains(item))
                        lstFilters.Add(item);
                }
            }
            return size == 0 ? _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList() :
                _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(size).ToList();
        }

        public IEnumerable<Remessa> GetComOcorrencias(int? de, int? ate, DateTime? dtProcessado, DateTime? dtPagtoDe,DateTime? dtPagtoAte)
        {
            var l = new List<Func<T, bool>>();

            if (de != 0)
            {
                Func<T, bool> fDe = it => it.id_lote >= (int)de;
                l.Add(fDe);
            }

            if (ate != 0)
            {
                Func<T, bool> fAte = it => it.id_lote <= (int)ate;
                l.Add(fAte);
            }

            if (dtProcessado.HasValue)
            {
                Func<T, bool> fDt = it => it.ArquivoRetorno.data_processado.GetValueOrDefault().ToShortDateString() == dtProcessado.GetValueOrDefault().ToShortDateString();
                l.Add(fDt);
            }

            if (dtPagtoDe.HasValue)
            {
                Func<T, bool> fDt = it => it.LoteRemessa.data_pgto.GetValueOrDefault() >= dtPagtoDe.GetValueOrDefault();
                l.Add(fDt);
            }

            if (dtPagtoAte.HasValue)
            {
                Func<T, bool> fDt = it => it.LoteRemessa.data_pgto.GetValueOrDefault() <= dtPagtoAte.GetValueOrDefault();
                l.Add(fDt);
            }

            var ds = _dbSet.Where(it => it.TipoRetorno.somar_rejeitados & it.id_arquivo.HasValue).ToList();

            foreach (var item in l)
                ds = ds.Where(item).ToList();

            return ds;
        }
    }
}
