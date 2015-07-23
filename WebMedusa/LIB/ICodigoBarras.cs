using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.LIB
{
    public interface ICodigoBarras
    {
        bool ValidaCodBarra();
        string CalculaDigito();
    }
}