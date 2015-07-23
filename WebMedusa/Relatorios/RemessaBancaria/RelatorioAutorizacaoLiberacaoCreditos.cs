using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;
using System.Text;

namespace Medusa.Relatorios.RemessaBancaria
{
    public class RelatorioAutorizacaoLiberacaoCreditos
    {
        public IEnumerable<vResumoAutorizacaoPagtos> GerarRelatorio(DateTime dt)
        {
            var ctx = new Contexto();
            var ds = ctx.Lista_ResumoAutorizacaoPagtos(dt);
            foreach (var item in ds.Where(it => it.rejeitados != 0))
	        {
                var str = new StringBuilder();
                foreach (var rem in ctx.Remessas.Where(it=> it.id_lote == item.id_lote & it.TipoRetorno.somar_rejeitados))
                    str.AppendFormat("{0} {1} - {2} - {3}<br />", Util.DecimalToString(rem.valor), rem.descricao, rem.TipoRetorno.codigo, rem.TipoRetorno.descricao);
                str.AppendFormat("{0} Líquido: {1}", Util.DecimalToString(item.rejeitados), Util.DecimalToString(item.total - item.rejeitados));
                str.AppendLine();
                item.decricao_rejeitado = str.ToString();
	        } 
            return ds;
        }
    }
}