using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class DespesaPremioBLL : DespesaPessoaFisicaBLL<DespesaPremio>
    {
       
        public DespesaPremioBLL()
        {

        }

        public override List<LancamentoItem> ProcessarPlanosContas()
        {
            var pctl = new PlanoContaTipoLancamentoBLL(_dbContext);
            var tipoLancto = pctl.Find(y => y.id_lancto_tipo == ObjEF.id_lancto_tipo).ToList();

            var txBLL = new TaxaBLL(_dbContext);

            var lst = new List<LancamentoItem>();



            foreach (var pc in tipoLancto)
            {
               
                    decimal valor = ObjEF.valor;
                    var projBLL = new ProjetoBLL(_dbContext);
                    projBLL.Get(ObjEF.id_projeto);
                    ObjEF.Projeto = projBLL.ObjEF;
                    if (pc.plconta.Taxa != null)
                    {
                        txBLL.Get(pc.plconta.Taxa.id_taxa);
                        txBLL.data_pgto = ObjEF.data_pagto;
                        var lstDedutiveis = txBLL.GetDedutiveis();

                        txBLL.valorBase = ObjEF.valor;
                        txBLL.valorAdeduzir = 0;
                        txBLL.inss_outr_empresa = 0;
                        if (txBLL.GetTabela().cumulativo_mensal)
                        {
                            txBLL.valorBase += GetTotalMes();
                            txBLL.valorAdeduzir += GetPagtosPorPlanosdeContasDebitos(pc.plconta.codigo);
                        }
                        // código do inss 11% pessoa física

                        txBLL.valorBase -= GetPagtosPorPlanosdeContasDedutiveis(lstDedutiveis) + lst.Where(it => lstDedutiveis.Contains(it.codigo)).Sum(it => it.debito + it.valorDeducao);
                        valor = txBLL.calcular();

                    }



                    if ((pc.plconta.credito != EnumPlanoConta.PESSOA) && (pc.plconta.debito != EnumPlanoConta.PESSOA) && pc.plconta.id_projeto_destino.HasValue)
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
                            debito = 0,
                            credito = 0,
                            debitoProjeto = pc.plconta.debito == EnumPlanoConta.PROJETO_DESTINO ? valor : 0,
                            creditoProjeto = pc.plconta.credito == EnumPlanoConta.PROJETO_DESTINO ? valor : 0,
                            id_projeto = pc.plconta.id_projeto_destino.GetValueOrDefault(),
                            projeto = pc.plconta.projeto_destino
                        });

                    }

                    else if ((pc.plconta.debito == EnumPlanoConta.SEM_LANCTO) & (pc.plconta.credito == EnumPlanoConta.SEM_LANCTO))
                    {

                        // código de despesas dedução dependente
                        if (pc.plconta.codigo.Equals(Constantes.DEDUCAO_DEP))
                        {
                            lst.Add(new LancamentoItem()
                            {
                                codigo = pc.plconta.codigo,
                                descricao = String.Format("{0} {1}", pc.plconta.classe, pc.plconta.item),
                                debito = 0,
                                credito = 0,
                                debitoProjeto = 0,
                                creditoProjeto = 0,
                                valorDeducao = valor * ObjEF.num_dependentes,
                                id_projeto = pc.plconta.id_projeto_destino.HasValue ? pc.plconta.id_projeto_destino.GetValueOrDefault() : ObjEF.id_projeto,
                                projeto = pc.plconta.id_projeto_destino.HasValue ? pc.plconta.projeto_destino : ObjEF.Projeto
                            });
                        }
                    }
                    else
                    {
                        lst.Add(new LancamentoItem()
                        {
                            codigo = pc.plconta.codigo,
                            descricao = String.Format("{0} {1}", pc.plconta.classe, pc.plconta.item),
                            debito = pc.plconta.debito == EnumPlanoConta.PESSOA ? valor : 0,
                            credito = pc.plconta.credito == EnumPlanoConta.PESSOA ? valor : 0,
                            debitoProjeto = pc.plconta.debito != EnumPlanoConta.PESSOA ? valor : 0,
                            creditoProjeto = pc.plconta.credito != EnumPlanoConta.PESSOA ? valor : 0,
                            id_projeto = pc.plconta.id_projeto_destino.HasValue ? pc.plconta.id_projeto_destino.GetValueOrDefault() : ObjEF.id_projeto,
                            projeto = pc.plconta.id_projeto_destino.HasValue ? pc.plconta.projeto_destino : ObjEF.Projeto
                        });

                    }

                
            }



            return lst.ToList();

        }            
       

    } 
}