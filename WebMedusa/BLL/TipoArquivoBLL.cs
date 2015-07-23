using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;

namespace Medusa.BLL
{
    public class TipoArquivoBLL : AbstractCrudWithLog<TipoArquivo>
    {
        //public static int RetornaIdpeloCodigo(int intCodigo)
        //{
        //    var ctx = new Contexto();

        //    var tEf = ctx.TiposArquivos.Where(t => t.codigo == intCodigo).FirstOrDefault();
        //    if (tEf == null)
        //        return 0;
        //    else
        //        return tEf.id_tipo_arquivo;
        //}


        public static int Boleto
        {
            get
            {
                return 1;
            }
        }

        public static int Pagamentos
        {
            get
            {
                return 3;
            }
        }

        public static int GpsSemBarra
        {
            get
            {
                return 4;
            }
        }

        public static int Guias
        {
            get
            {
                return 2;
            }
        }

        public void GetPorTipoSegmento(string tipo_segmento)
        {
            ObjEF =  _dbSet.Where(t => t.tipo_segmento == tipo_segmento).FirstOrDefault();
        }
    }
}
