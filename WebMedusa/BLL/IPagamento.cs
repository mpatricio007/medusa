using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
namespace Medusa.BLL
{
    public interface IPagamento
    {  
        bool Agendar(ref string msg);
        bool Alterar(ref string msg);
        void Processar();
        void Conciliar(string codigoRetorno, string autBancaria, int intTipoConciliacao, int intId_arquivo);
        void EstornarConciliacao();
        bool Estornar(ref string msg);        
        void ImprimirComprovante();
        bool EstaBloqueado();
        void ProcessarContaLacto(ContaLancto cl);

     }
}
