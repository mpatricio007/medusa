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
        //GPS 
        public string codigo_receita { get; set; }
        public string tipo_id_contribuinte { get; set; }
        public string identificacao_contribuinte { get; set; }
        public string identificacao_tributo { get; set; }
        public string mes_ano { get; set; }
        public string valor_previsto { get; set; }
        public string valor_outras_entidades { get; set; }
        public string atualizacao_monetaria { get; set; }
        public string brancos45 { get; set; }
        //
        public string cod_ocorrencias { get; set; }

        public SegmentoN(RemessaGpsSemCodBarra cons,int contaCons, int contaHeader)
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
            contribuinte = cons.nome_fav_cedente.RemoverAcentos().PadRight(30);
            data_pagto = Util.DateToStringSemBarras(cons.Lote.data_pgto);
            valor_pagto = Util.DecimalToStringSoDigitos(cons.valor).PadLeft(15,'0');
            codigo_receita = cons.cod_receita;
            tipo_id_contribuinte = "01";
            identificacao_contribuinte = cons.id_contribuinte;
            identificacao_tributo = "17";
            mes_ano = cons.mes_ano;
            valor_previsto = Util.DecimalToStringSoDigitos(cons.valor_gps).PadLeft(15, '0');
            valor_outras_entidades = Util.DecimalToStringSoDigitos(cons.outras_entidades).PadLeft(15, '0');
            atualizacao_monetaria =  Util.DecimalToStringSoDigitos(cons.atual_monetaria).PadLeft(15, '0');
            brancos45 = new String(' ', 45);
            cod_ocorrencias = new String(' ', 10);
 
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
            seu_numero = strLinha.Substring(17, 20);
            nosso_numero = strLinha.Substring(37, 20);
            contribuinte = strLinha.Substring(57, 30);
            data_pagto = strLinha.Substring(87, 8);
            valor_pagto = strLinha.Substring(95, 15);
            codigo_receita = strLinha.Substring(110, 6);
            tipo_id_contribuinte = strLinha.Substring(116, 2);
            identificacao_contribuinte = strLinha.Substring(118, 14);
            mes_ano = strLinha.Substring(134, 6);
            valor_previsto = strLinha.Substring(140, 15);
            valor_outras_entidades = strLinha.Substring(155, 15);
            atualizacao_monetaria = strLinha.Substring(170, 15);
            brancos45 = strLinha.Substring(185, 45);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}",
            cod_banco,
            lote_servico,
            tipo_registro.value,
            num_seq,
            cod_segmento,
            tipo_mov,
            cod_instrucao,
            seu_numero,
            nosso_numero,
            contribuinte,
            data_pagto,
            valor_pagto,
            codigo_receita,
            tipo_id_contribuinte,
            identificacao_contribuinte,
            identificacao_tributo,
            mes_ano,
            valor_previsto,
            valor_outras_entidades,
            atualizacao_monetaria,
            brancos45,
                cod_ocorrencias);
            
              
        }
    }
}