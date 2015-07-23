using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class MaterialNotaFiscalBLL : AbstractCrudWithLog<MaterialNotaFiscal>
    {
        public MaterialNotaFiscalBLL(Contexto ctx)
            : base(ctx)
        {
            // TODO: Complete member initialization
            //this._dbContext = ctx;
        }
    }
}
