using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Collections;

namespace Medusa.BLL
{
    public class TipoRetornoBLL : AbstractCrudWithLog<TipoRetorno>
    {
        public static int Agendado
        {
            get
            {                
                return RetornaIdpeloCodigo("1A");
            }
        }

        public static int Enviado
        {
            get
            {
                return RetornaIdpeloCodigo("1E");
            }
        }

        public static int Estornado
        {
            get
            {
                return RetornaIdpeloCodigo("1X");
            }
        }

        public static int Rejeitado
        {
            get
            {
                return RetornaIdpeloCodigo("1R");
            }
        }

        public static int PagtoConfirmado
        {
            get
            {
                return RetornaIdpeloCodigo("00");
            }
        }

        public static int InclusaoEfetuada
        {
            get
            {
                return RetornaIdpeloCodigo("BD");
            }
        }

        public static List<int> NaoRejeitados
        {
            get
            {
                return new List<int>(){
                    InclusaoEfetuada
                    ,PagtoConfirmado
                    ,Agendado
                    ,Enviado     
                    ,Estornado
                };
            }
        }

      

        public static int RetornaIdpeloCodigo(string txtCodigo)
        {
            var ctx = new Contexto();

            TipoRetorno tEf = ctx.TipoRetornos.Where(t => t.codigo == txtCodigo).FirstOrDefault();
            if (tEf == null)
                return 0;
            else
                return tEf.id_tipo_ret;
        }



    }
}
