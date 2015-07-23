using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.BLL.RemessaBB
{
    /// <summary>
    /// Detalhe do Arquivo,
    /// tipo de registro = 3.
    /// </summary>
    public class SegmentoZ : LinhaArquivo
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

        public string num_seq { get; set; }

        public string cod_segmento { get; set; }
        public string aut_legislacao { get; set; }
        public string aut_bancaria { get; set; }
        public string cnab_febraban { get; set; }
        public string cod_ocorrencias { get; set; }
       

        public SegmentoZ(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);            
            num_seq = strLinha.Substring(8, 5);
            cod_segmento = strLinha.Substring(13, 1);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1) + cod_segmento];
            aut_legislacao = strLinha.Substring(14, 64);
            aut_bancaria = strLinha.Substring(78, 25);
            cnab_febraban = strLinha.Substring(103, 127);           
            cod_ocorrencias = strLinha.Substring(230, 10);
        }


        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                cod_banco,
                lote_servico,
                tipo_registro.value,
                num_seq,
                cod_segmento,
                aut_legislacao,
                aut_bancaria,
                cnab_febraban,
                cod_ocorrencias);
        }
    }
}
