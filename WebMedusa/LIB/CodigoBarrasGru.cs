using Medusa.BLL;
using Medusa.DAL;
using Medusa.LIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medusa.LIB
{
    public class CodigoBarrasGru : ICodigoBarras
    {
        public string RepresentacaoNumerica { get; set; }

        public string Digito { get; set; }

        public IdentificacaoSegmento IdentificacaoSegmento
        {
            get
            {
                var isBLL = new IdentificacaoSegmentoBLL();
                isBLL.GetPorCodigo(RepresentacaoNumerica.Substring(1, 1));
                return isBLL.ObjEF;
            }
        }


        public string RepresentacaoNumericaSemDig()
        {
            try
            {
                return string.Format("{0}{1}{2}{3}", RepresentacaoNumerica.Substring(0, 11),
                    RepresentacaoNumerica.Substring(12, 11), RepresentacaoNumerica.Substring(24, 11), RepresentacaoNumerica.Substring(36, 11));
            }
            catch (Exception)
            {
                return String.Empty;
            }
            
        }
        public bool ValidaCodBarra()
        {
            bool saida = true;
            if (RepresentacaoNumerica.Length != 48)
            {
                saida = false;
            }
            else
            {
                Digito = RepresentacaoNumerica.Substring(3, 1);
                saida = (CalculaDigito() == Digito);
            } 
            return saida;
        }

        public string CalculaDigito()
        {
            int resultado;
            string parte1 = string.Format("{0}{1}",RepresentacaoNumerica.Substring(0,3),RepresentacaoNumerica.Substring(4,7));
            string parte2 = RepresentacaoNumerica.Substring(12,11);
            string parte3 = RepresentacaoNumerica.Substring(24,11);
            string parte4 = RepresentacaoNumerica.Substring(36,11);
            string multip = string.Format("{0}{1}{2}{3}",parte1,parte2,parte3,parte4);
            if (RepresentacaoNumerica.Substring(2,1) != "8")
            {
                int p =2;
                int soma =0;
                string resString="";
                for(int i =0;i<=2;i++)
                {
                    soma=Convert.ToInt16(multip.Substring(i,1))*p;
                    resString=string.Format("{0}{1}",resString,soma);
                    p=p-1;
                    if (p==0) p=2;
                }
                p=1;
                for(int i =3;i<=42;i++)
                {
                    soma=Convert.ToInt16(multip.Substring(i,1))*p;
                    resString=string.Format("{0}{1}",resString,soma);
                    p=p+1;
                    if (p>2) p=1;
                }
                resultado=0;
                for(int i =0;i<resString.Length;i++) resultado = resultado+Convert.ToInt16(resString.Substring(i,1));
                if (resultado!=0)
                {
                    if ((resultado % 10)!=0)
                        resultado=10-(resultado%10);
                    else
                        resultado=0;
                }
                else
                    resultado=0;
            }
            else 
            {
                int p=4;
                int soma=0;
                for(int i =0;i<=2;i++)
                {
                    soma=soma + Convert.ToInt16(multip.Substring(i,1))*p;                    
                    p=p-1;
                }
                p=9;
                for(int i =3;i<=42;i++)
                {
                    soma=soma+Convert.ToInt16(multip.Substring(i,1))*p;
                    p=p-1;
                    if (p<2)
                        p=9;
                }
                resultado=soma % 11;
                if (resultado>1)
                    resultado=11-resultado;
                else
                    resultado=0;
            }
            return Convert.ToString(resultado);
        }

        public CodigoBarrasGru(string strCodBarra)
        {
            strCodBarra = strCodBarra.Replace(" ", "").Replace(".", "").Replace("-", "");
            RepresentacaoNumerica = strCodBarra;
        }

        public CodigoBarrasGru()
        {
        }

        public decimal Valor()
        {
            try
            {
                return Convert.ToDecimal(String.Format("{0}{1}", RepresentacaoNumerica.Substring(4, 7), RepresentacaoNumerica.Substring(12, 4))) / 100;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override string ToString()
        {
            
            return String.Format("{0}-{1} {2}-{3} {4}-{5} {6}-{7}",
                RepresentacaoNumerica.Substring(0, 11),
                RepresentacaoNumerica.Substring(11, 1),
                RepresentacaoNumerica.Substring(12, 11),
                RepresentacaoNumerica.Substring(23, 1),
                RepresentacaoNumerica.Substring(24, 11),
                RepresentacaoNumerica.Substring(35, 1),
                RepresentacaoNumerica.Substring(36, 11),
                RepresentacaoNumerica.Substring(47, 1));

        }

        
    }
}