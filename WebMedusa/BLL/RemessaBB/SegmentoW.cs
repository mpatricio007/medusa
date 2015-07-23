using Medusa.DAL;
using Medusa.LIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL.RemessaBB
{
    public class SegmentoW : LinhaArquivo
    {
        public string cod_banco { get; set; }
        public string lote_servico { get; set; }
        public string tipo_registro { get; set; }
        public string num_seq_lote { get; set; }
        public string cod_segmento { get; set; }
        public string num_seq_compl { get; set; }
        public string cod_info_compl { get; set; }  
        public string num_ref { get; set; }
        public string competencia { get; set; }
        public string contribuinte { get; set; }
        public string valor_principal { get; set; }
        public string desc_abatimento { get; set; }
        public string brancos12 { get; set; }
        public string outras_deducoes { get; set; }
        public string mora_multa { get; set; }
        public string juros_encargos { get; set; }
        public string outros_acrescimos { get; set; }
        public string brancos24 { get; set; }
        public string ident_tributo { get; set; }
        public string info_complementar { get; set; }
        public string uso_banco { get; set; }
        public string cod_ocorrencias { get; set; }

        public SegmentoW(RemessaGru rgru, int contaGRU, int contaHeader)
        {
            cod_banco = rgru.Lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}", contaHeader);
            //tipo_registro = TipoLinha.SegmentoW;
            tipo_registro = TipoLinha.SegmentoW.value;
            num_seq_lote = String.Format("{0:00000}", contaGRU);
            cod_segmento = TipoLinha.SegmentoW.codigo;
            num_seq_compl = String.Format("{0:0}", contaGRU);
            cod_info_compl = "9";
            num_ref = String.Format("{0:00000000000000000000}", rgru.num_referencia);
            competencia = String.Format("{0:000000}", rgru.mes_ano);
            contribuinte = String.Format("{0:00000000000000}", rgru.id_contribuinte);
            valor_principal = String.Format("{0:00000000000000}",Util.DecimalToStringSoDigitos(rgru.valor_gru).PadLeft(14, '0'));
            desc_abatimento = Util.DecimalToStringSoDigitos(rgru.desc_abatimento).PadLeft(14, '0');
            brancos12 = new String(' ', 12);
            outras_deducoes = Util.DecimalToStringSoDigitos(rgru.outras_deducoes).PadLeft(14, '0');
            mora_multa = Util.DecimalToStringSoDigitos(rgru.mora_multa).PadLeft(14, '0');
            juros_encargos = Util.DecimalToStringSoDigitos(rgru.juros_encargos).PadLeft(14, '0');
            outros_acrescimos = Util.DecimalToStringSoDigitos(rgru.outros_acrescimos).PadLeft(14, '0');
            brancos24 = new String(' ', 24);
            ident_tributo = "88";
            info_complementar = new String(' ', 50);
            uso_banco = new String(' ', 2);
            cod_ocorrencias = new String(' ', 10);
        }

        public SegmentoW(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            cod_segmento = strLinha.Substring(13, 1);
            tipo_registro = Convert.ToString(TipoLinha.GetLinha[strLinha.Substring(7, 1) + cod_segmento]);
            num_seq_lote = strLinha.Substring(8, 5);
            num_seq_compl = strLinha.Substring(14, 1);
            cod_info_compl = strLinha.Substring(14, 1);
            num_ref = strLinha.Substring(16, 20);
            competencia = strLinha.Substring(36, 6);
            contribuinte = strLinha.Substring(42, 14);
            valor_principal = strLinha.Substring(56, 14);
            desc_abatimento = strLinha.Substring(70, 14);
            brancos12 = strLinha.Substring(84, 12);
            outras_deducoes = strLinha.Substring(96, 14);
            mora_multa = strLinha.Substring(110, 14);
            juros_encargos = strLinha.Substring(124, 14);
            outros_acrescimos = strLinha.Substring(138, 14);
            brancos24 = strLinha.Substring(152, 24);
            ident_tributo = strLinha.Substring(176, 2);
            info_complementar = strLinha.Substring(178, 50);
            uso_banco = strLinha.Substring(228, 2);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}",
                cod_banco,
                lote_servico,
                tipo_registro,
                num_seq_lote,
                cod_segmento,
                num_seq_compl,
                cod_info_compl,
                num_ref,
                competencia,
                contribuinte,
                valor_principal,
                desc_abatimento,
                brancos12,
                outras_deducoes,
                mora_multa,
                juros_encargos,
                outros_acrescimos,
                brancos24,
                ident_tributo,
                info_complementar,
                uso_banco,
                cod_ocorrencias);
        }
    }
}