using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL.RemessaBB
{
    /// <summary>
    /// Trailer do Lote,
    /// tipo de registro = 5.
    /// </summary>
    public class TrailerLote : LinhaArquivo
    {
        /// <summary>
        /// código do banco, 
        /// tipo númerico,
        /// de 0 até 2,
        /// 3 posições.
        /// </summary>
        public string cod_banco { get; set; }
        /// <summary>
        /// lote de serviço,
        /// tipo númerico,
        /// de 3 até 6, 
        /// 4 posições,
        /// conteúdo 0000.
        /// </summary>
        public string lote_servico { get; set; }

        public TipoLinha tipo_registro { get; set; }

        public string brancos9 { get; set; }

        public string qtdade_registros { get; set; }

        public string sum_valores { get; set; }

        public string brancos189 { get; set; }

        public string cod_ocorrencias { get; set; }

        public TrailerLote(Lote lote, int contaHeader, int contaPagtos, decimal soma)
        {        
            cod_banco = lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}", contaHeader);
            tipo_registro = TipoLinha.TrailerLote;
            brancos9 = new String(' ', 9);
            qtdade_registros = String.Format("{0:000000}", contaPagtos + 2);
            sum_valores = Util.DecimalToStringSoDigitos(soma).PadLeft(18, '0');
            brancos189 = new String(' ', 189);
            cod_ocorrencias = new String(' ', 10);
        }

        public TrailerLote(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1)];
            brancos9 = strLinha.Substring(8, 9);
            qtdade_registros = strLinha.Substring(17, 6);
            sum_valores = strLinha.Substring(23, 16);
            brancos189 = strLinha.Substring(41, 189);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
               cod_banco,
               lote_servico,
               tipo_registro.value,
               brancos9,
               qtdade_registros,
               sum_valores,
               brancos189,
               cod_ocorrencias);
        }
       
    }
}