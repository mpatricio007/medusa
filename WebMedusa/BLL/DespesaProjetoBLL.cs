using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class DespesaProjetoBLL : DespesaBLL<DespesaProjeto>
    {
        public override List<LancamentoItem> ProcessarPlanosContas()
        {
            var pctl = new PlanoContaTipoLancamentoBLL(_dbContext);
            var tipoLanctos = pctl.Find(y => y.id_plano_conta == ObjEF.id_plano_conta).ToList();

            var txBLL = new TaxaBLL(_dbContext);

            var lst = new List<LancamentoItem>();
            var valor = ObjEF.valor;
            var projBLL = new ProjetoBLL(_dbContext);
            projBLL.Get(ObjEF.id_projeto);
            ObjEF.Projeto = projBLL.ObjEF;

            foreach (var pc in tipoLanctos)
            {
                lst.Add(new LancamentoItem()
                {
                    codigo = pc.plconta.codigo,
                    descricao = String.Format("{0} {1}", pc.plconta.classe, pc.plconta.item),
                    debito = 0,
                    credito = 0,
                    debitoProjeto = valor,
                    creditoProjeto = 0,
                    id_projeto = ObjEF.id_projeto,
                    projeto = ObjEF.Projeto
                });
            }
            return lst;
        }
    }
}
