using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL.RemessaBB
{
    public class SegmentoB : LinhaArquivo
    {

        public string cod_banco { get; set; }

        public string lote_servico { get; set; }

        public TipoLinha tipo_registro { get; set; }
        
        public string num_seq { get; set; }

        public string cod_segmento { get; set; }    
       
        public string brancos_3 { get; set; }
      
        public string tipo_inscricao { get; set; }

        public string num_inscricao { get; set; }

        public string lougradouro { get; set; }

        public string numero { get; set; }

        public string complemento { get; set; }

        public string bairro { get; set; }

        public string cidade { get; set; }

        public string cep { get; set; }

        public string comp_cep { get; set; }

        public string uf { get; set; }  
    
        public string data_vencto { get; set; }

        public string valor_doc { get; set; }

        public string brancos_90 { get; set; }      

        public SegmentoB(RemessaPAG pagto,int contaPag, int contaHeader)
        {
            cod_banco = pagto.Lote.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}",contaHeader);
            tipo_registro = TipoLinha.SegmentoB;
            num_seq = String.Format("{0:00000}", contaPag);
            cod_segmento = TipoLinha.SegmentoB.codigo;
            brancos_3 = new String(' ',3);
            tipo_inscricao = pagto.tipo_inscricao.ToString();
            num_inscricao = pagto.inscricao.PadLeft(14, '0');
            lougradouro = new String(' ', 30);
            numero = new String(' ', 5);
            complemento = new String(' ', 15);
            bairro = new String(' ', 15);
            cidade = new String(' ', 20);
            cep = new String(' ', 5);
            comp_cep = new String(' ', 3);
            uf = new String(' ', 2);
            data_vencto = Util.DateToStringSemBarras(pagto.Lote.data_pgto);
            valor_doc = Util.DecimalToStringSoDigitos(pagto.valor).PadLeft(15, '0');           
            brancos_90 = new String(' ', 90);
        }

        public SegmentoB(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            cod_segmento = strLinha.Substring(13, 1);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1) + cod_segmento];
            num_seq = strLinha.Substring(8, 5);
            
            brancos_3 = strLinha.Substring(14, 3);
            tipo_inscricao = strLinha.Substring(17, 1);
            num_inscricao = strLinha.Substring(18, 14);
            lougradouro = strLinha.Substring(32, 30);
            numero = strLinha.Substring(62, 5);
            complemento = strLinha.Substring(67, 15);
            bairro = strLinha.Substring(82, 15);
            cidade = strLinha.Substring(97, 20);
            cep = strLinha.Substring(117, 5);
            comp_cep = strLinha.Substring(122, 3);
            uf = strLinha.Substring(125, 2);
            data_vencto = strLinha.Substring(127, 8);
            valor_doc = strLinha.Substring(135, 15);           
            brancos_90 = strLinha.Substring(149, 90);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}",

            cod_banco,
            lote_servico,
            tipo_registro.value,
            num_seq,
            cod_segmento,
             brancos_3,
            tipo_inscricao,
            num_inscricao,
            lougradouro,
            numero,
            complemento,
            bairro,
            cidade,
            cep,
            comp_cep,
            uf,
            data_vencto,
            valor_doc,
            brancos_90);
        }
    }
}