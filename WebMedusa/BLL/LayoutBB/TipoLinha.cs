using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL.LayoutBB
{
    public enum TipoLinha : byte
    {
        Header = 0,
        SaldoAnterior = 10,
        Lancamento = 11,
        SaldoAtual = 12,
        Trailer = 9
    }
}