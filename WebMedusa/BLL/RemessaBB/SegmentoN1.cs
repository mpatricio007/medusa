using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL.RemessaBB
{
    public class SegmentoN : LinhaArquivo
    {
        public string cod_banco { get; set; }

        public string lote_servico { get; set; }

        public TipoLinha tipo_registro { get; set; }

        public string num_seq { get; set; }

        public string cod_segmento { get; set; }

        public string tipo_mov { get; set; }

        public string cod_instrucao { get; set; }

        public string seu_numero { get; set; }

        public string nosso_numero { get; set; }

        public string contribuinte { get; set; }

        public string data_pagto { get; set; }

        public string valor_pagto { get; set; }

        public string codigo_receita { get; set; }

        public string identificacao_contribuinte { get; set; }

        public string mes_ano { get; set; }

        public string valor_outras_entidades { get; set; }

        public string atualizacao_monetaria { get; set; }

        public string brancos45 { get; set; }

        public string cod_ocorrencias { get; set; }

        public SegmentoN(RemessaCons cons,int contaCons, int contaHeader)
        {
            cod_banco = cons.Lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}",contaHeader);
            tipo_registro = TipoLinha.SegmentoN;
            num_seq = String.Format("{0:00000}", contaCons);
            cod_segmento = TipoLinha.SegmentoN.codigo;
            tipo_mov = new String('0',1);
            cod_instrucao = new String('0', 2);
            seu_numero =  String.Format("{0:00000000000000000000}",cons.id_remessa);
            nosso_numero = new String(' ', 20);
            contribuinte = cons.nome_fav_cedente;
            data_pagto = Util.DateToStringSemBarras(cons.Lote.data_pgto);
            valor_pagto = Util.DecimalToStringSoDigitos(cons.valor).PadLeft(15,'0');
            //codigo_receita = 
            //num_doc_empresa = Convert.ToString(cons.id_remessa).PadLeft(20, '0');
            //num_doc_banco = new String(' ', 20);
            //brancos45 = new String(' ', 68);
            //cod_ocorrencias = new String(' ', 10);            

        

        //public string codigo_receita { get; set; }

        //public string identificacao_contribuinte { get; set; }

        //public string mes_ano { get; set; }

        //public string valor_outras_entidades { get; set; }

        //public string atualizacao_monetaria { get; set; }

        //public string brancos45 { get; set; }

        //public string cod_ocorrencias { get; set; }
        
        }

        public SegmentoN(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            cod_segmento = strLinha.Substring(13, 1);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1) + cod_segmento];            
            num_seq = strLinha.Substring(8, 5);
            tipo_mov = strLinha.Substring(14, 1);
            cod_instrucao = strLinha.Substring(15, 2);
            //cod_barras = strLinha.Substring(17, 44);
            //conc_org_publico = strLinha.Substring(61, 30);
            //data_vencto = strLinha.Substring(91, 8);
            //data_pagto = strLinha.Substring(99, 8);
            //valor_pagto = strLinha.Substring(107, 15);
            //num_doc_empresa = strLinha.Substring(122, 20);
            //num_doc_banco = strLinha.Substring(142, 20);
            brancos45 = strLinha.Substring(162, 68);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                cod_banco,
                lote_servico,
                tipo_registro.value,
                num_seq,
                cod_segmento,
                tipo_mov,
                cod_instrucao,
                //cod_barras,
                //conc_org_publico,
                //data_vencto,
                //data_pagto,
                //valor_pagto,
                //num_doc_empresa,
                //num_doc_banco,
                //brancos45,
                cod_ocorrencias);
              
        }
    }
}