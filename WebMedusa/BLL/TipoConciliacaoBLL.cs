using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class TipoConciliacaoBLL : AbstractCrudWithLog<TipoConciliacao>
    {
        public static int CONSOLIDAD
        {
            get
            {
                return 3;
            }
        }
        
        
        public static int RetornaIdpeloNome(string txtNome)
        {
            var ctx = new Contexto();

            var tEf = ctx.TiposConciliacoes.Where(t => t.nome == txtNome).FirstOrDefault();
            if (tEf == null)
                return 0;
            else
                return tEf.id_tipo_conciliacao;
        }

        
    }
}
