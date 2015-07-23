using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Medusa.BLL
{
    public class DespesaBLL<T> : AbstractCrudWithLog<T> where T : Despesa, new()
    {
        public decimal TotalLiquidoBeneficiario()
        {
            return LctosBeneficiario(ObjEF.Itens.ToList()).Sum(it => it.credito - it.debito);
        }


        public virtual List<LancamentoItem> LctosBeneficiario(List<LancamentoItem> lst)
        {
            return lst.Where(t => t.debito != 0 || t.credito != 0 || t.valorDeducao != 0).ToList();
        }

        public virtual List<LancamentoItem> LctosProjeto(List<LancamentoItem> lst)
        {
            return lst.Where(t => t.id_projeto == ObjEF.id_projeto & (t.debitoProjeto != 0 || t.creditoProjeto != 0)).ToList();
        }

        public virtual List<LancamentoItem> LctosProvisaoImpostos(List<LancamentoItem> lst)
        {
            return lst.Where(t => t.id_projeto != ObjEF.id_projeto & (t.debitoProjeto != 0 || t.creditoProjeto != 0)).ToList();
        }

       
        public bool Exists()
        {
            return ObjEF.id_lancto != 0;
           }
        public override void Update()
        {
            _dbContext.Entry(ObjEF).State = EntityState.Modified;
        }

        public virtual string Agendar()
        {        
            string msg =  "";
            bool rt = DataIsValid(ref msg);
            if (rt)
            {
                ObjEF.Itens = new List<LancamentoItem>();
                ObjEF.Projeto = null;
                ObjEF.id_forma = 1;
                foreach (var item in ProcessarPlanosContas())
                {
                    item.projeto = null;
                    ObjEF.Itens.Add(item);

                }
                try
                {
                    if (!Exists())
                        Add();
                    else
                    {
                        //ObjEF.Projeto = null;
                        //ObjEF.RemessaGerada = null;
                        //ObjEF.formaPagto = null;
                        Update();
                    }

                    if (SaveChanges())
                        return "Despesa agendada com sucesso!";
                    else
                        throw new System.ArgumentException();

                }
                catch (Exception ex)
                {
                    return String.Format("Erro ao agendar despesa! {0}", ex.Message);
                }
            }
            return msg;

        }

        public virtual string Agendar(ref bool rt)
        {
            string msg =  "";
            rt = DataIsValid(ref msg);
            if (rt)
            {

                ObjEF.Itens = new List<LancamentoItem>();
                ObjEF.Projeto = null;
                ObjEF.id_forma = 1;
                foreach (var item in ProcessarPlanosContas())
                {
                    item.projeto = null;
                    ObjEF.Itens.Add(item);

                }
                try
                {
                    if (!Exists())
                        Add();
                    else
                        Update();
                    rt = SaveChanges();
                    if (rt)
                        return "Despesa agendada com sucesso!";
                    else
                        throw new System.ArgumentException();

                }
                catch (Exception ex)
                {
                    return String.Format("Erro ao agendar despesa! {0}", ex.Message);
                }
            }
            return msg;

        }

        public virtual List<LancamentoItem> ProcessarPlanosContas()
        {
            var pctl = new PlanoContaTipoLancamentoBLL(_dbContext);
            var tipoLanctos = pctl.Find(y => y.id_lancto_tipo == ObjEF.id_lancto_tipo).ToList();

            var txBLL = new TaxaBLL(_dbContext);

            var lst = new List<LancamentoItem>();
            var valor = ObjEF.valor;
            foreach (var pc in tipoLanctos)
            {
                lst.Add(new LancamentoItem()
                {
                    codigo = pc.plconta.codigo,
                    descricao = String.Format("{0} {1}", pc.plconta.classe, pc.plconta.item),
                    debito = pc.plconta.debito == EnumPlanoConta.PESSOA ? valor : 0,
                    credito = pc.plconta.credito == EnumPlanoConta.PESSOA ? valor : 0,
                    debitoProjeto = pc.plconta.debito == EnumPlanoConta.PROJETO ? valor : 0,
                    creditoProjeto = pc.plconta.credito == EnumPlanoConta.PROJETO ? valor : 0,
                    id_projeto = ObjEF.id_projeto,
                    projeto = ObjEF.Projeto
                });
            }
            return lst;
        }


        public virtual string Estornar()
        {
            string msg =  "";
            bool rt = DataIsValid(ref msg);
            if (rt)
            {


                var oldItens = ObjEF.Itens;
                foreach (var item in oldItens)
                    _dbContext.LancamentosItens.Remove(item);
                try
                {
                    Update();
                    if (SaveChanges())
                        return "Despesa estornada com sucesso!";
                    return "";
                }
                catch (Exception ex)
                {
                    return String.Format("Erro ao estornar despesa! {0}", ex.Message);
                }
            }
            return msg;
        }

        public virtual string ReAgendar()
        {
            var oldItens = ObjEF.Itens.ToList();
            foreach (var item in oldItens)
            {
                var entry = _dbContext.Entry(item);
                entry.State = EntityState.Deleted;
            }
            _dbContext.SaveChanges();
            return Agendar();
        }

      

        public virtual List<T> GetListPagtosDoMes()
        {
            return null;
        }

        public decimal GetTotalMes()
        {
            return GetListPagtosDoMes().Sum(it => it.valor);
        }

        public decimal? TotalMesPorPlanoConta(string strCodigoPlanoConta)
        {
            return (from p in _dbSet.Where(it => it.data_pagto.Month == ObjEF.data_pagto.Month)
                    from i in p.Itens
                    where i.codigo == strCodigoPlanoConta
                    select i.credito).Sum();
        }

        public decimal GetPagtosPorPlanosdeContasDedutiveis(List<string> dedutiveis)
        {
            return (from d in GetListPagtosDoMes()
                    from i in d.Itens
                    where dedutiveis.Contains(i.codigo)
                    select i.debito).Sum();
        }

        public decimal GetPagtosPorPlanosdeContasCreditos(string strCodigoPlanoConta)
        {
            return (from d in GetListPagtosDoMes()
                    from i in d.Itens
                    where i.codigo == strCodigoPlanoConta
                    select i.credito).Sum();
        }

        public decimal GetPagtosPorPlanosdeContasDebitos(string strCodigoPlanoConta)
        {
            return (from d in GetListPagtosDoMes()
                    from i in d.Itens
                    where i.codigo == strCodigoPlanoConta
                    select i.debito).Sum();
        }

        public override bool DataIsValid(ref string strMsg)
        {
            if (ObjEF.valor <= 0)
            {
                strMsg = String.Format("Valor menor ou igual a zero!");
                return false;
            }


            var projBLL = new ProjetoBLL(_dbContext);
            projBLL.Get(ObjEF.id_projeto);            
            return projBLL.DataIsValid(ObjEF.data_pagto, ref strMsg);
        }
    }
}
