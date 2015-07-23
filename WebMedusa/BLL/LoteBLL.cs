using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;
using Medusa.LIB;


namespace Medusa.BLL
{
    public abstract class LoteBLL<T> : AbstractCrudWithLog<T> where T : Lote,new()
    {
        public bool JahEnviado()
        {
            return ObjEF.data_envio.HasValue;
        }

        public override void Add()
        {            
            ObjEF.data_processamento = DateTime.Now;
            base.Add();
        }

        public bool AgendarPagamentoNoLote(IPagamento pagamento, ref string msg)
        {            
            return pagamento.Agendar(ref msg);        
        }

        public bool AlterarPagamentoNoLote(IPagamento pagamento, ref string msg)
        {
            return pagamento.Alterar(ref msg);
        }

        public bool EstornarPagamentoNoLote(IPagamento pagamento, ref string msg)
        {
            return pagamento.Estornar(ref msg);
        }

        public abstract bool GerarArquivo();

        public void GerarArquivo(string justificativa)
        {
            if (GerarArquivo())
            {
                LogSistemaBLL log = new LogSistemaBLL();
                log.ObjEF.id_entidade = ObjEF.id_lote;
                log.ObjEF.descricao = justificativa;
                log.ObjEF.entidade = typeof(T).Name;
                log.Add();
                log.SaveChanges();
            }
        }

        public List<T> GetLotes(int de, int ate, DateTime? dt)
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
                Func<T, bool> fDt = it => it.data_pgto == (DateTime)dt;
                l.Add(fDt);
            }

            var ds = _dbSet.ToList<T>();

            foreach (var item in l)
                ds = ds.Where(item).ToList();

            return ds;
            //return dt.HasValue ?  _dbSet.Where(it => it.id_lote >= de & it.id_lote <= ate & it.data_pgto == (DateTime)dt).ToList<T>() :
            //    _dbSet.Where(it => it.id_lote >= de & it.id_lote <= ate).ToList<T>();
        }

        public List<T> GerarArquivosRemessa(int de, int ate, DateTime? dt)
        {
            var log = new LogSistemaBLL();
            List<T> lotesEnviados = new List<T>();        

            try
            {
                log.ObjEF.descricao = String.Format("Lote remessa de {0} ate {1} {2}", de, ate, dt.HasValue ? 
                    String.Format("com data de pagamento em {0}", dt) : "");
                log.ObjEF.entidade = typeof(T).ToString();
                foreach (var item in GetLotes(de, ate, dt))
                {
                    ObjEF = item;
                    if (!JahEnviado())
                    {
                        GerarArquivo();
                    }
                    else
                    {
                        lotesEnviados.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                log.ObjEF.descricao = String.Format("Erro ao gerar arquivos remessa! {0}", ex.Message);
            }
            
            log.Add();
            log.SaveChanges();
            return lotesEnviados;
        }

        public override bool DataIsValid(ref string strMsg)
        {
            int? id_projeto;
            strMsg = String.Empty;

            if (ObjEF.data_envio.HasValue)
            {
                strMsg = "Lote já enviado para pagamento!";
                return false;    
            }

            var contaBLL = new ContaBLL(_dbContext);
            contaBLL.Get(ObjEF.id_conta);

            if (contaBLL.ObjEF.id_tipoconta == 5) // BLOQUEADO
            {
                strMsg = "Conta bloqueada para movimentações!";
                return false;    
            }

            id_projeto = contaBLL.ObjEF.id_projeto;
            
            if (id_projeto.HasValue)
            {
                var projBLL = new ProjetoBLL(_dbContext);
                projBLL.Get(id_projeto.GetValueOrDefault());
                return projBLL.DataIsValid(ObjEF.data_pgto.GetValueOrDefault(), ref strMsg);
            }

            return true;
        }

        public bool Exists()
        {
            return ObjEF.id_lote != 0;
        }

        public bool TemContaEspecifica()
        {
            if (Exists())
            {
                if (ObjEF.Conta.id_projeto.HasValue)
                {
                    var pBLL = new ProjetoBLL(_dbContext);
                    pBLL.Get(ObjEF.Conta.id_projeto.GetValueOrDefault());
                    return pBLL.TemContaEspecifica();
                }
                else
                    return false;
            }
            else
                return false;
        }

        public List<Projeto> GetAllProjetos()
        {
            if (Exists())
            {
                
                    var cBLL = new  ContaBLL(_dbContext);
                    cBLL.ObjEF = ObjEF.Conta;
                    return cBLL.GetAllProjetosPorConta();
            }
            else
                return null;
        }

        
    }
}
