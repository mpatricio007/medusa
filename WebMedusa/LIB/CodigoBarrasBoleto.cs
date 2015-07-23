using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.LIB
{

    public class CodigoBarrasBoleto : ICodigoBarras
    {
        public string RepresentacaoNumerica { get; set; }
                
        public string CodigoBanco()
        {
            try
            {
                return RepresentacaoNumerica.Substring(0, 3);
            }
            catch (Exception)
            {
                return String.Empty; ;
            }
            
        }

        public string CodigoMoeda()
        {
            try
            {
                return RepresentacaoNumerica.Substring(3, 1);
            }
            catch (Exception)
            {
                return String.Empty; ;
            }

        }

        public string ValorImpresso()
        {
            try
            {
                return RepresentacaoNumerica.Substring(33, 14);
            }
            catch (Exception)
            {
                return String.Empty; ;
            }

        }

        public int Id_banco()
        {   
            try
            {
                BLL.BancoBLL bancoBLL = new BLL.BancoBLL();
                Dictionary<string, DAL.Banco> dic = bancoBLL.GetAll().OfType<DAL.Banco>().ToDictionary(it => it.codigo);
                return dic[CodigoBanco()].id_banco;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public string CampoLivre()
        {

            if (RepresentacaoNumerica != String.Empty)
            {
                try
                {   
                    return String.Format("{0}{1}{2}", RepresentacaoNumerica.Substring(4, 5), RepresentacaoNumerica.Substring(10, 10), RepresentacaoNumerica.Substring(21, 10));
                }
                catch (Exception)
                {
                    return String.Empty;
                }
            }
            else
                return String.Empty;
        }
        
        public decimal Valor()
        {

            if (RepresentacaoNumerica.Substring(33,14).Trim().Length==14)
            {
                try
                {                    
                    return Convert.ToDecimal(RepresentacaoNumerica.Substring(37, 10)) / 100;
                }
                catch (Exception)
                {
                    return 0;                    
                }
            }
            else
                return 0;
        }

        public DateTime DataVencimento()
        {
            if (RepresentacaoNumerica.Substring(33,3)!="000") 
            {
                try 
	            {
                    return Convert.ToDateTime("07/10/1997").AddDays(Convert.ToInt16(RepresentacaoNumerica.Substring(33, 4)));
	            }
	            catch (Exception)
	            {
                    return DateTime.Now;
	            }
            }
                
            else
                return DateTime.Now;    
        }

        public bool ValidaCodBarra()
        {
            bool saida = false;
            try
            {
                if (RepresentacaoNumerica.Length < 47)
                {
                    RepresentacaoNumerica = string.Format("{0}{1}", RepresentacaoNumerica, "000000000000000000000000000").Substring(1, 47);
                }
                if (RepresentacaoNumerica.Length != 47)
                    saida = false;
                else
                {
                    saida = (CalculaDigito() == RepresentacaoNumerica.Substring(32, 1));
                }
            }
            catch (Exception)
            {
                saida = false;
            }
            
            return saida;
        }
        
        public string CalculaDigito()
        {
            string saida = "";
            string sequencia;
            try
            {
                sequencia = string.Format("{0}{1}{2}{3}{4}", RepresentacaoNumerica.Substring(0, 4), RepresentacaoNumerica.Substring(32, 15), RepresentacaoNumerica.Substring(4, 5), RepresentacaoNumerica.Substring(10, 10), RepresentacaoNumerica.Substring(21, 10));
                sequencia = string.Format("{0}{1}", sequencia.Substring(0, 4), sequencia.Substring(5, 39));
            }
            catch (Exception)
            {
                return saida;
            }
            string caracter;
            int intMultiplicador = 2;
            int intNumero = 0;
            int intTotalNumero = 0;
            int intResto = 0;

            for (int intContador = 42;intContador >= 0; intContador-- )
            {
                caracter = sequencia.Substring(intContador, 1);
                if (intMultiplicador > 9)
                {
                    intMultiplicador = 2;
                    intNumero = 0;
                }
                intNumero = Convert.ToInt16(caracter) * intMultiplicador;
                intTotalNumero = intTotalNumero + intNumero;
                intMultiplicador = intMultiplicador + 1;
            }
            intResto = (11 - (intTotalNumero % 11));
            if (intResto >= 10) saida = "1"; else saida = Convert.ToString(intResto);
            return saida;
             
        }
        
        public CodigoBarrasBoleto(string strCodBarra)
        {
            strCodBarra = strCodBarra.Replace(" ", "").Replace(".", "");
            RepresentacaoNumerica = strCodBarra;
        }
        public CodigoBarrasBoleto()
        {
        }

        public override string ToString()
        {
            return String.Format("{0}.{1} {2}.{3} {4}.{5} {6} {7}",
                RepresentacaoNumerica.Substring(0, 5),
                RepresentacaoNumerica.Substring(5, 5),
                RepresentacaoNumerica.Substring(10, 5),
                RepresentacaoNumerica.Substring(15, 6),
                RepresentacaoNumerica.Substring(21, 5),
                RepresentacaoNumerica.Substring(26, 6),
                RepresentacaoNumerica.Substring(32, 1),
                RepresentacaoNumerica.Substring(33, 14));

        }
   
    }

    
}