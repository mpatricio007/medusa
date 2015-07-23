using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.BLL;

namespace Medusa.BLL
{
    public class AdiantamentoViagemBLL : AbstractAdiantamentoBLL<AdiantamentoViagem>
    {

        public override void GerarValidade()
        {
            if (!Exists())
            {
                var tipoBLL = new TiposAdiantamentoBLL(_dbContext);
                tipoBLL.Get(ObjEF.id_tipo_admto);
                ObjEF.TiposAdiantamento = tipoBLL.ObjEF;
            }
            int numDays = ObjEF.TiposAdiantamento.num_dias;

            ObjEF.data_vencimento = ObjEF.TiposAdiantamento.business_days ? ObjEF.data_retorno.Value.AddBusinessDays(numDays) : ObjEF.data_retorno.Value.AddDays(numDays);
        }
    }
}
