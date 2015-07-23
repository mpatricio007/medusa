using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL.RemessaBB
{
    /// <summary>
    ///  Tipo de Arquivo Remessa ou Retorno
    ///  Remessa = 1
    ///  Retorno = 2
    /// </summary>
    public enum TipoRemRet :byte
    {
        Remessa = 1,
        Retorno = 2
    }
}