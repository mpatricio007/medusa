using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class BancoBLL : AbstractCrudWithLog<Banco>
    {
        public static int RetornaIdpeloCodigo(string strCodigo)
        {
            var ctx = new Contexto();

            var bEF = ctx.Bancos.Where(t => t.codigo == strCodigo).FirstOrDefault();
            if (bEF == null)
                return 0;
            else
                return bEF.id_banco;
        }

        public static int BANCO_DO_BRASIL
        {
            get
            {
                return 2;
            }
        }
    }
}
