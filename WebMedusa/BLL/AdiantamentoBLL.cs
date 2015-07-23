using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class AdiantamentoBLL : AbstractAdiantamentoBLL<Adiantamento>
    {
        


        public List<Adiantamento> GetAdiantamentosSemPrestacaoContas()
        {

            return _dbSet.Where(it => it.StatusAdiantamento.id_status_admto != StatusAdiantamentoBLL.eh_cancelado && it.StatusAdiantamento.id_status_admto != StatusAdiantamentoBLL.eh_concluido
                && it.StatusAdiantamento.id_status_admto != StatusAdiantamentoBLL.eh_reprovado && it.id_projeto == ObjEF.id_projeto & it.id_adiantamento != ObjEF.id_adiantamento 
                && it.id_tipo_admto == TiposAdiantamentoBLL.adiantamento).
                OrderBy(it => it.data).ThenBy(it => it.Beneficiario.PessoaFisica.nome).ToList();
        }

        public List<Adiantamento> GetAdiantamentosAnteriores()
        {
            return _dbSet.Where(it => it.id_projeto == ObjEF.id_projeto & it.id_adiantamento != ObjEF.id_adiantamento & it.id_tipo_admto == TiposAdiantamentoBLL.adiantamento).
                OrderBy(it => it.data).ThenBy(it => it.Beneficiario.PessoaFisica.nome).ToList();
        }

        public override void GerarValidade()
        {
            if (!Exists())
            {
                var tipoBLL = new TiposAdiantamentoBLL(_dbContext);
                tipoBLL.Get(ObjEF.id_tipo_admto);
                ObjEF.TiposAdiantamento = tipoBLL.ObjEF;
            }
            int numDays = ObjEF.TiposAdiantamento.num_dias;

            ObjEF.data_vencimento =  ObjEF.TiposAdiantamento.business_days ? ObjEF.data_pagamento.Value.AddBusinessDays(numDays) : ObjEF.data_pagamento.Value.AddDays(numDays);
        }

        public List<Adiantamento> GetAdiantamentosVisibleRelatorio()
        {
            return _dbSet.Where(it => it.id_projeto == ObjEF.id_projeto && it.id_adiantamento != ObjEF.id_adiantamento && it.id_tipo_admto == TiposAdiantamentoBLL.adiantamento
                && StatusAdiantamentoBLL.VisibleRelatorio.Contains(it.StatusAdiantamento.id_status_admto) ).
              OrderBy(it => it.data).ThenBy(it => it.Beneficiario.PessoaFisica.nome).ToList();
        }
    }
}
