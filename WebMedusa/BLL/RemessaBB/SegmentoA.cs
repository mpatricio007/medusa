using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL.RemessaBB
{
    public class SegmentoA : LinhaArquivo
    {        
        public string cod_banco { get; set; }

        public string lote_servico { get; set; }

        public TipoLinha tipo_registro { get; set; }
        
        public string num_seq { get; set; }

        public string cod_segmento { get; set; }

        public string tipo_mov { get; set; }

        public string cod_instrucao { get; set; }

        public string cod_camara { get; set; }
        
        public string cod_banco_pag { get; set; }

        public string ag { get; set; }

        public string ag_digito { get; set; }

        public string cc { get; set; }

        public string digito_cc { get; set; }

        public string digito_ag_cc { get; set; }

        public string nome_pag { get; set; }
       
        public string num_empresa { get; set; }

        public string data_debito { get; set; }

        public string tipo_moeda { get; set; }

        public string qtd_moeda { get; set; }

        public string valor_debito { get; set; }

        public string num_doc_banco { get; set; }

        public string dt_efetivacao_dbto { get; set; }

        public string valor_efetivacao_dbto { get; set; }        

        public string outras_info { get; set; }

        public string compl_tipo_servico { get; set; }

        public string brancos_10 { get; set; }

        public string aviso { get; set; }

        public string cod_ocorrencias { get; set; }

        public SegmentoA(RemessaPAG pagto,int contaPag, int contaHeader)
        {
            cod_banco = pagto.Lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}",contaHeader);
            tipo_registro = TipoLinha.SegmentoA;
            num_seq = String.Format("{0:00000}", contaPag);
            cod_segmento = TipoLinha.SegmentoA.codigo;
            tipo_mov = new String('0',1);
            cod_instrucao = new String('0', 2);
            cod_camara = pagto.FormaPagto.cod_camara; 
            cod_banco_pag = pagto.BancoDestino.codigo.PadLeft(3,'0');
            ag = pagto.agencia.PadLeft(5, '0');
            ag_digito = pagto.digito_agencia;
            cc = pagto.conta.PadLeft(12, '0');
            digito_cc = pagto.digito_conta;
            digito_ag_cc = new String(' ', 1);
            nome_pag = pagto.nome_fav_cedente.RemoverAcentos().PadRight(30);
            num_empresa = Convert.ToString(pagto.id_remessa).PadLeft(20, '0');
            data_debito = Util.DateToStringSemBarras(pagto.Lote.data_pgto);
            tipo_moeda = "BRL"; // somente real
            qtd_moeda = new String('0', 15);
            valor_debito = Util.DecimalToStringSoDigitos(pagto.valor).PadLeft(15, '0');
            num_doc_banco = new String(' ', 20);
            dt_efetivacao_dbto = new String('0',8);
            valor_efetivacao_dbto = new String('0',15);
            outras_info = pagto.descricao.RemoverAcentos().PadRight(40);
            compl_tipo_servico = pagto.FormaPagto.finalidade_pagto;
            brancos_10 = new String(' ', 10);
            aviso = "0"; // aviso ao favorecido 0 = não
            cod_ocorrencias = new String(' ', 10);   
        }

        public SegmentoA(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            cod_segmento = strLinha.Substring(13, 1);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1) + cod_segmento];
            num_seq = strLinha.Substring(8, 5);            
            tipo_mov = strLinha.Substring(14, 1);
            cod_instrucao = strLinha.Substring(15, 2);
            cod_camara = strLinha.Substring(17, 3);
            cod_banco_pag = strLinha.Substring(20, 3);
            ag = strLinha.Substring(23, 5);
            ag_digito = strLinha.Substring(20, 1);
            cc = strLinha.Substring(29, 12);
            digito_cc = strLinha.Substring(41, 1);
            digito_ag_cc = strLinha.Substring(42, 1);
            nome_pag = strLinha.Substring(43, 30);
            num_empresa = strLinha.Substring(73, 20);
            data_debito = strLinha.Substring(93, 8);
            tipo_moeda = strLinha.Substring(101, 3);
            qtd_moeda = strLinha.Substring(104, 10);
            valor_debito = strLinha.Substring(119, 15);
            num_doc_banco = strLinha.Substring(134, 20);
            dt_efetivacao_dbto = strLinha.Substring(154, 8);
            valor_efetivacao_dbto = strLinha.Substring(162, 13);
            outras_info = strLinha.Substring(177, 40);
            compl_tipo_servico = strLinha.Substring(217, 2);
            brancos_10 = strLinha.Substring(220, 10);
            aviso = strLinha.Substring(229, 1);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}",

            cod_banco,
            lote_servico,
            tipo_registro.value,
            num_seq,
            cod_segmento,
            tipo_mov,
            cod_instrucao,
            cod_camara,
            cod_banco_pag,
            ag,
            ag_digito,
            cc,
            digito_cc,
            digito_ag_cc,
            nome_pag,
            num_empresa,
            data_debito,
            tipo_moeda,
            qtd_moeda,
            valor_debito,
            num_doc_banco,
            dt_efetivacao_dbto,
            valor_efetivacao_dbto,
            outras_info,
            compl_tipo_servico,
            brancos_10,
            aviso,
            cod_ocorrencias);
        }
    }
}