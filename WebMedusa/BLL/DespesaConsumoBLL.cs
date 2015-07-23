using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class DespesaConsumoBLL : DespesaBLL<DespesaConsumo>
    {
        public override List<LancamentoItem> ProcessarPlanosContas()
        {
            var lstIspc = ObjEF.Guia.IdentificacaoSegmento.IdentficacaoSegmentoPlanosContas.Select(it => it.id_plano_conta).ToList();
            var pctl = new PlanoContaTipoLancamentoBLL(_dbContext);
            var tipoLanctos = pctl.Find(y => y.id_lancto_tipo == ObjEF.id_lancto_tipo & lstIspc.Contains(y.id_plano_conta)).ToList();

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

        public override bool DataIsValid(ref string strMsg)
        {
            if (!ObjEF.Guia.ValidaCodBarra())
            {
                strMsg = "Boleto inválido!";
                return false;
            }

            if ((!((int[])Enum.GetValues(typeof(IdentificacoesSegmentoConsumo))).Contains(Util.StringToInteiro(ObjEF.Guia.IdentificacaoSegmento.codigo).GetValueOrDefault())))
            {
                strMsg = "somente contas de consumo";
                return false;
            }

            return base.DataIsValid(ref strMsg);
        }
    }
}
