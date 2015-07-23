using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class DespesaServTercPJBLL : DespesaPessoaJuridicaBLL<DespesaServTercPJ>
    {
        public String CodigoDespesaImposto { get; set; }
        public ProjetoBLL p { get; set; }
        public DespesaServTercPJBLL()
        {
            CodigoDespesaImposto = "210001";
            p = new ProjetoBLL();
            p.Get(1425); // Projeto 2- Exigibilidades
        }

        public override List<LancamentoItem> ProcessarPlanosContas()
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
            var dst = new LancamentosSCPRBLL(_dbContext);
            var ltos = dst.Find(it => it.codlan == ObjEF.codlan).ToList();
            foreach (var item in ltos)
            {
              
                lst.Add(new LancamentoItem()
                {
                    codigo = CodigoDespesaImposto,
                    descricao =  item.descde,
                    debito = item.valorli,
                    credito = 0,
                    debitoProjeto = 0,
                    creditoProjeto = item.valorli,
                    id_projeto = p.ObjEF.id_projeto,
                    projeto = p.ObjEF
                });
                
            }


            return lst;
        }


        public override string Agendar(ref bool rt)
        {
            string msg =  "";
            rt = DataIsValid(ref msg);
            if (rt)
            {

                ObjEF.Itens = new List<LancamentoItem>();
                ObjEF.Projeto = null;

                ObjEF.id_forma = String.IsNullOrEmpty(ObjEF.codBarra) ? 1 : 2;
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


    }
}