using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    public enum TipoInscricao : byte
    {
        CPF = 1,
        CNPJ = 2
    }

    public class TipoInscricaoBLL
    {
        public static string ToString(TipoInscricao tipo)
        {
            string rt = "";
            switch (tipo)
            {
                case TipoInscricao.CPF:
                    rt = "pessoa fisica";
                    break;
                case TipoInscricao.CNPJ:
                    rt = "pessoa juridica";
                    break;
                default:
                    rt = "erro";
                    break;
            }
            return rt;
        }
    }
}