using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.BLL.RemessaBB
{
    /// <summary>
    /// Trailer do Arquivo,
    /// tipo de registro = 9.
    /// </summary>
    public class TrailerArquivo : LinhaArquivo
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

        public string qtdade_lotes { get; set; }

        public string qtdade_registros { get; set; }

        public string qtdade_contas { get; set; }        

        public string brancos205 { get; set; }


        public TrailerArquivo(Lote lote, int contaHeader, int contaReg)
        {
            cod_banco = lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = "9999";
            tipo_registro = TipoLinha.TrailerArquivo;
            brancos9 = new String(' ', 9);
            qtdade_lotes = String.Format("{0:000000}", contaHeader);
            qtdade_registros = String.Format("{0:000000}", contaReg);
            qtdade_contas = new String(' ', 6);
            brancos205 = new String(' ', 205);
        }

        public TrailerArquivo(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1)];
            brancos9 = strLinha.Substring(8, 9);
            qtdade_lotes = strLinha.Substring(17, 6);
            qtdade_registros = strLinha.Substring(23, 6);
            qtdade_contas = strLinha.Substring(29, 6);
            brancos205 = strLinha.Substring(35, 205);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
               cod_banco,
               lote_servico,
               tipo_registro.value,
               brancos9,
               qtdade_lotes,
               qtdade_registros,
               qtdade_contas,
               brancos205);
        }
    }
}