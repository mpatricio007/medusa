using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL.RemessaBB
{
    public class LinhaArquivo
    {
        public static HeaderArquivo CriarHeader(string strLinha)
        {
            return new HeaderArquivo(strLinha);
        }

        public static HeaderLote CriarHeaderLote(string strLinha)
        {
            return new HeaderLote(strLinha);
        }

        public static SegmentoJ CriarSegmentoJ(string strLinha)
        {
            return new SegmentoJ(strLinha);
        }

        public static SegmentoO CriarSegmentoO(string strLinha)
        {
            return new SegmentoO(strLinha);
        }

        public static SegmentoW CriarSegmentoW(string strLinha)
        {
            return new SegmentoW(strLinha);
        }

        public static SegmentoA CriarSegmentoA(string strLinha)
        {
            return new SegmentoA(strLinha);
        }

        public static SegmentoB CriarSegmentoB(string strLinha)
        {
            return new SegmentoB(strLinha);
        }

        public static SegmentoZ CriarSegmentoZ(string strLinha)
        {
            return new SegmentoZ(strLinha);
        }


        public static SegmentoN CriarSegmentoN(string strLinha)
        {
            return new SegmentoN(strLinha);
        }

        public static TrailerArquivo CriarTrailerArquivo(string strLinha)
        {
            return new TrailerArquivo(strLinha);
        }

        public static TrailerLote CriarTrailerLote(string strLinha)
        {
            return new TrailerLote(strLinha);
        }
    }
}