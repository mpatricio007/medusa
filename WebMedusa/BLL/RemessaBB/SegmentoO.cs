using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL.RemessaBB
{ 
    public class SegmentoO : LinhaArquivo
    {
        public string cod_banco { get; set; }
        public string lote_servico { get; set; }
        public string tipo_registro { get; set; }
        public string num_seq { get; set; }
        public string cod_segmento { get; set; }
        public string tipo_mov { get; set; }
        public string cod_instrucao { get; set; }
        public string cod_barras { get; set; }
        public string conc_org_publico { get; set; }
        public string data_vencto { get; set; }
        public string data_pagto { get; set; }
        public string valor_pagto { get; set; }
        public string num_doc_empresa { get; set; }
        public string num_doc_banco { get; set; }
        public string brancos68 { get; set; }
        public string cod_ocorrencias { get; set; }

        public SegmentoO(RemessaCons cons,int contaCons, int contaHeader)
        {
            cod_banco = cons.Lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}",contaHeader);
            tipo_registro = TipoLinha.SegmentoO.value;
            num_seq = String.Format("{0:00000}", contaCons);
            cod_segmento = TipoLinha.SegmentoO.codigo;
            tipo_mov = new String('0',1);
            cod_instrucao = new String('0', 2);
            cod_barras = cons.Guia.RepresentacaoNumericaSemDig();
            conc_org_publico = cons.nome_fav_cedente.RemoverAcentos().PadRight(30);
            data_vencto = Util.DateToStringSemBarras(cons.dataVencto);
            data_pagto = Util.DateToStringSemBarras(cons.Lote.data_pgto);
            valor_pagto = Util.DecimalToStringSoDigitos(cons.valor).PadLeft(15,'0');
            num_doc_empresa = Convert.ToString(cons.id_remessa).PadLeft(20, '0');
            num_doc_banco = new String(' ', 20);
            brancos68 = new String(' ', 68);
            cod_ocorrencias = new String(' ', 10);            
        }

        public SegmentoO(RemessaGru cons, int contaCons, int contaHeader)
        {
            cod_banco = cons.Lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}", contaHeader);
            tipo_registro = TipoLinha.SegmentoO.value;
            num_seq = String.Format("{0:00000}", contaCons);
            cod_segmento = TipoLinha.SegmentoO.codigo;
            tipo_mov = new String('0', 1);
            cod_instrucao = new String('0', 2);
            cod_barras = cons.Guia.RepresentacaoNumericaSemDig();
            conc_org_publico = cons.nome_fav_cedente.RemoverAcentos().PadRight(30);
            data_vencto = Util.DateToStringSemBarras(cons.data_vencto);
            data_pagto = Util.DateToStringSemBarras(cons.Lote.data_pgto);
            valor_pagto = Util.DecimalToStringSoDigitos(cons.valor).PadLeft(15, '0');
            num_doc_empresa = Convert.ToString(cons.id_remessa).PadLeft(20, '0');
            num_doc_banco = new String(' ', 20);
            brancos68 = new String(' ', 68);
            cod_ocorrencias = new String(' ', 10);  
        }

        public SegmentoO(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            cod_segmento = strLinha.Substring(13, 1);
            tipo_registro = Convert.ToString(TipoLinha.GetLinha[strLinha.Substring(7, 1) + cod_segmento]); 
            num_seq = strLinha.Substring(8, 5);
            tipo_mov = strLinha.Substring(14, 1);
            cod_instrucao = strLinha.Substring(15, 2);
            cod_barras = strLinha.Substring(17, 44);
            conc_org_publico = strLinha.Substring(61, 30);
            data_vencto = strLinha.Substring(91, 8);
            data_pagto = strLinha.Substring(99, 8);
            valor_pagto = strLinha.Substring(107, 15);
            num_doc_empresa = strLinha.Substring(122, 20);
            num_doc_banco = strLinha.Substring(142, 20);
            brancos68 = strLinha.Substring(162, 68);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                cod_banco,
                lote_servico,
                tipo_registro,
                num_seq,
                cod_segmento,
                tipo_mov,
                cod_instrucao,
                cod_barras,
                conc_org_publico,
                data_vencto,
                data_pagto,
                valor_pagto,
                num_doc_empresa,
                num_doc_banco,
                brancos68,
                cod_ocorrencias);
              
        }
    }
}