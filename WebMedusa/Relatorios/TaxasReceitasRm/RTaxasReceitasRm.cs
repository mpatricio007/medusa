using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.Relatorios.TaxasReceitasRm
{
    public class RTaxasReceitasRm
    {
        public List<vTaxasReceitasRm> GetAllByData(DateTime dtDe, DateTime dtAte,int projDe, int projAte)
        {
            var ctx = new Contexto();
            return ctx.ExtratoTaxasReceitasRm_PorPeriodo(dtDe, dtAte).Where(it => it.projeto >= projDe & it.projeto <= projAte).OrderBy(it => it.codgerencial).ToList();
        }
    }
}