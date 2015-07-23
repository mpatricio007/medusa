using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;
using System.IO;
namespace Medusa.BLL
{
    public class LoteConsBLL : LoteBLL<LoteCons>
    {
        public override bool GerarArquivo()
        {
            Cnab240BB c = new Cnab240BB();
            c.GerarArquivo(ObjEF);
            ObjEF.data_envio = DateTime.Now;
            Update();
            return SaveChanges();
        }

        public LoteConsBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }

        public LoteConsBLL()
        {
        }
    }
}