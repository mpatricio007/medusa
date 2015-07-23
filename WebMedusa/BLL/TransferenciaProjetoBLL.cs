using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class TransferenciaProjetoBLL : DespesaBLL<TransferenciaProjeto>
    {
        public override List<LancamentoItem> LctosProjeto(List<LancamentoItem> lst)
        {
            return lst.Where(t => t.debitoProjeto != 0 || t.creditoProjeto != 0).ToList();
        }

        public override List<LancamentoItem> ProcessarPlanosContas()
        {
            var pctl = new PlanoContaTipoLancamentoBLL(_dbContext);
            var tipoLanctos = pctl.Find(y => y.id_lancto_tipo == ObjEF.id_lancto_tipo).ToList();

            var txBLL = new TaxaBLL(_dbContext);

            var lst = new List<LancamentoItem>();
            var valor = ObjEF.valor;

            var projBLL = new ProjetoBLL(_dbContext);
            projBLL.Get(ObjEF.id_projeto);
            ObjEF.Projeto = projBLL.ObjEF;

            var projTransBLL = new ProjetoBLL(_dbContext);
            projTransBLL.Get(ObjEF.id_projeto_trans);
            ObjEF.ProjetoTrans = projTransBLL.ObjEF;

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

                lst.Add(new LancamentoItem()
                {
                    codigo = pc.plconta.codigo,
                    descricao = String.Format("{0} {1}", pc.plconta.classe, pc.plconta.item),
                    debito = pc.plconta.debito == EnumPlanoConta.PESSOA ? valor : 0,
                    credito = pc.plconta.credito == EnumPlanoConta.PESSOA ? valor : 0,
                    debitoProjeto = 0,
                    creditoProjeto = valor,
                    id_projeto = ObjEF.id_projeto_trans,
                    projeto = ObjEF.ProjetoTrans
                });
            }
            return lst;
        }

        public override bool DataIsValid(ref string strMsg)
        {
            if (ObjEF.id_projeto == ObjEF.id_projeto_trans) 
            {
                strMsg = "transferência para mesmo projeto";
                return false;
            }

            var projBLL = new ProjetoBLL(_dbContext);
            projBLL.Get(ObjEF.id_projeto_trans);
            return projBLL.DataIsValid(ObjEF.data_pagto, ref strMsg) ? base.DataIsValid(ref strMsg) : false;
        }
    }
}
