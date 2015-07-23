using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class TiposAdiantamentoBLL : AbstractCrudWithLog<TiposAdiantamento>
    {
        public static int adiantamento 
        {
            get { return 2; }
        }

        public static int viagem
        {
            get { return 1; }
        }

        public TiposAdiantamentoBLL()
        {
        }

        public TiposAdiantamentoBLL(Contexto ctx): base(ctx)
        {
        }
    }
}
