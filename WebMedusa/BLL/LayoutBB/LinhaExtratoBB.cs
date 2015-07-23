using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL.LayoutBB
{
    public class LinhaExtratoBB
    {
        public static HeaderExtratoBB CriarHeader(string strLinha)
        {
            return new HeaderExtratoBB(strLinha);
        }

        public static LancamentoExtratoBB CriarLancamento(string strLinha)
        {
            return new LancamentoExtratoBB(strLinha);
        }

        public static SaldoAtualExtratoBB CriarSaldoAtual(string strLinha)
        {
            return new SaldoAtualExtratoBB(strLinha);
        }

        public static SaldoAnteriorExtratoBB CriarSaldoAnterior(string strLinha)
        {
            return new SaldoAnteriorExtratoBB(strLinha);
        }

        public static TrailerExtratoBB CriarTrailer(string strLinha)
        {
            return new TrailerExtratoBB(strLinha);
        }
    }
}