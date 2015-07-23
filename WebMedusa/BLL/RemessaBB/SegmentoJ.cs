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
    public class SegmentoJ : LinhaArquivo
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

        public string tipo_mov { get; set; }

        public string cod_instrucao { get; set; }

        public string cod_banco_destino { get; set; }

        public string cod_moeda { get; set; }

        public string dig_cod_bar { get; set; }

        public string valor_cod_barra { get; set; }

        public string campo_livre { get; set; }

        public string cedente { get; set; }

        public string data_vencto { get; set; }

        public string valor_titulo { get; set; }

        public string valor_desc_abat { get; set; }

        public string valor_mora_multa { get; set; }

        public string data_pagto { get; set; }

        public string valor_pagto { get; set; }

        public string qtdade_moeda { get; set; }

        public string cod_ref_sacado { get; set; }

        public string num_doc_banco { get; set; }

        public string brancos8 { get; set; }

        public string cod_ocorrencias { get; set; }

        public SegmentoJ(RemessaTit tit, int contaTit,int contaHeader)
        {
            cod_banco = tit.Lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}", contaHeader);
            tipo_registro = TipoLinha.SegmentoJ;
            num_seq = String.Format("{0:00000}", contaTit);
            cod_segmento = TipoLinha.SegmentoJ.codigo;
            tipo_mov = new String('0', 1); // 0 - INC                                                                          
            cod_instrucao = new String('0', 2);
            cod_banco_destino = tit.Boleto.CodigoBanco();
            cod_moeda = tit.Boleto.CodigoMoeda();
            dig_cod_bar = tit.Boleto.CalculaDigito();
            valor_cod_barra = tit.Boleto.ValorImpresso();
            campo_livre = tit.Boleto.CampoLivre();
            cedente = tit.nome_fav_cedente.RemoverAcentos().PadRight(30);
            data_vencto = Util.DateToStringSemBarras(tit.dataVencto);
            valor_titulo =  Util.DecimalToStringSoDigitos(tit.valor).PadLeft(15, '0');
            valor_desc_abat = new String('0', 15); 
            valor_mora_multa = new String('0', 15);
            data_pagto = Util.DateToStringSemBarras(tit.Lote.data_pgto);
            valor_pagto = Util.DecimalToStringSoDigitos(tit.valor).PadLeft(15, '0');
            qtdade_moeda = new String('0', 15);
            cod_ref_sacado = Convert.ToString(tit.id_remessa).PadLeft(20, '0');
            num_doc_banco = tit.descricao.PadRight(20);
            brancos8 = new String(' ', 8);
            cod_ocorrencias = new String(' ', 10);
            
        }

        public SegmentoJ(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            
            num_seq = strLinha.Substring(8, 5);
            cod_segmento = strLinha.Substring(13, 1);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1) + cod_segmento];
            tipo_mov = strLinha.Substring(14, 1);
            cod_instrucao = strLinha.Substring(15, 2);
            cod_banco_destino = strLinha.Substring(17, 3);
            cod_moeda = strLinha.Substring(20, 1);
            dig_cod_bar = strLinha.Substring(21, 1);
            valor_cod_barra = strLinha.Substring(22, 14);
            campo_livre = strLinha.Substring(36, 25);
            cedente = strLinha.Substring(61, 30);
            data_vencto = strLinha.Substring(91, 8);
            valor_titulo = strLinha.Substring(99, 15);
            valor_desc_abat = strLinha.Substring(114, 15);
            valor_mora_multa = strLinha.Substring(129, 15);
            data_pagto = strLinha.Substring(144, 8);
            valor_pagto = strLinha.Substring(152, 15);
            qtdade_moeda = strLinha.Substring(167, 15);
            cod_ref_sacado = strLinha.Substring(182, 20);
            num_doc_banco = strLinha.Substring(202, 20);
            brancos8 = strLinha.Substring(222, 8);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}",
                cod_banco,
                lote_servico,
                tipo_registro.value,
                num_seq,
                cod_segmento,
                tipo_mov,
                cod_instrucao,
                cod_banco_destino, 
                cod_moeda,
                dig_cod_bar, 
                valor_cod_barra,
                campo_livre,
                cedente,
                data_vencto,
                valor_titulo,
                valor_desc_abat,
                valor_mora_multa,
                data_pagto,
                valor_pagto,
                qtdade_moeda,
                cod_ref_sacado,
                num_doc_banco,
                brancos8,
                cod_ocorrencias);
        }
    }
}
