using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class DestinatarioEntradaBLL : AbstractCrudWithLog<DestinatarioEntrada>
    {
        public DestinatarioEntradaBLL() { }

        public DestinatarioEntradaBLL(Contexto ctx)
            : base(ctx)
        {
        }
    }
}
