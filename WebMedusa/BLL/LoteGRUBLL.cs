using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;
using Medusa.BLL.RemessaBB;

namespace Medusa.BLL
{
    public class LoteGRUBLL : LoteBLL<LoteGRU>
    {
        public override bool GerarArquivo()
        {
            Cnab240BB c = new Cnab240BB();
            c.GerarArquivo(ObjEF);
            ObjEF.data_envio = DateTime.Now;
            Update();
            return SaveChanges();
        }

        public LoteGRUBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }

        public LoteGRUBLL()
        {
        }
    }
}
