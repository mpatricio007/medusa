using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL.RemessaBB
{
    /// <summary>
    /// Header do Arquivo,
    /// tipo de registro = 1.
    /// </summary>
    public class HeaderLote : LinhaArquivo
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

        public string tipo_operacao { get; set; }

        //public TipoServico tipo_servico { get; set; }

        public string tipo_servico { get; set; }

        public string forma_lancto { get; set; }

        public string versao_layout { get; set; }

        public string brancos1 { get; set; }

        /// <summary>
        /// tipo de inscrição da empresa,
        /// 1 - CPF, 2 - CGC,
        /// de 17 até 17,
        /// 1 posição.
        /// </summary>
        public TipoInscricao tipo_inscricao { get; set; }
        /// <summary>
        /// nº de inscrição da empresa,
        /// CPF ou CGC,
        /// de 18 até 31,
        /// 14 posições.
        /// </summary>
        public string num_inscricao_empresa { get; set; }
        /// <summary>
        /// código do convenio do banco,        
        /// de 32 até 51,
        /// 20 posições.
        /// </summary>
        public string convenio_banco { get; set; }
        /// <summary>
        /// n° da agencia mantenedora da conta,
        /// de 52 até 56,
        /// 5 posições.
        /// </summary>
        public string agencia { get; set; }
        /// <summary>
        /// digito da agencia mantenedora da conta,
        /// de 57 até 57,
        /// 1 posições.
        /// </summary>
        public string digito_agencia { get; set; }
        /// <summary>
        /// n° da conta,
        /// de 58 até 69,
        /// 12 posições.
        /// </summary>
        public string conta { get; set; }
        /// <summary>
        /// digito da conta,
        /// de 70 até 70,
        /// 1 posição.
        /// </summary>
        public string conta_digito { get; set; }
        /// <summary>
        /// digito verificador ag/conta,
        /// de 71 até 71,
        /// 1 posição.
        /// </summary>
        public string digito_ag_conta { get; set; }
        /// <summary>
        /// nome da empresa,
        /// de 72 até 101,
        /// 30 posições.
        /// </summary>
        public string nome_empresa { get; set; }

        public string mensagem { get; set; }

        public string logradouro { get; set; }

        public string numero { get; set; }

        public string complemento { get; set; }

        public string cidade { get; set; }

        public string cep { get; set; }        

        public string uf { get; set; }
      
        public string brancos8 { get; set; }

        public string cod_ocorrencias { get; set; }

        public HeaderLote(Lote l, string strFormaPagto, int contaHeader)
        {
            cod_banco = l.Conta.BancoAgencia.Banco.codigo;
            lote_servico = String.Format("{0:0000}", contaHeader);
            tipo_registro = TipoLinha.HeaderLote;            
            tipo_operacao  = "C";

            tipo_servico = Constantes.TIPO_SERVICO; // TipoServico.Pagamentos; // TipoServico.GetTipoPorLote[l.GetType().BaseType];
            forma_lancto = strFormaPagto;
            versao_layout = Constantes.LAYOUT_LOTE;            
            brancos1  = new String(' ',1);            
            tipo_inscricao  = TipoInscricao.CNPJ;            
            num_inscricao_empresa  = Constantes.CNPJ_FUSP;
            convenio_banco = l.Conta.BancoAgencia.num_convenio.PadRight(20);        
            agencia = l.Conta.BancoAgencia.numero.PadLeft(5,'0');
            digito_agencia = l.Conta.BancoAgencia.digito;
            conta = l.Conta.numero.PadLeft(12,'0');
            conta_digito = l.Conta.digito;
            digito_ag_conta = new String(' ', 1);
            nome_empresa  = Constantes.NOME_FUSP.PadRight(30);
            mensagem  = new String(' ', 40);
            logradouro = Constantes.ENDERECO_FUSP.PadRight(30);
            numero = new String(' ', 5);
            complemento = Constantes.BAIRRO_FUSP.PadRight(15);
            cidade = Constantes.CIDADE_FUSP.PadRight(20);
            cep = Constantes.CEP_FUSP.PadRight(8);
            uf = Constantes.UF_FUSP;
            brancos8  = new String(' ',8);
            cod_ocorrencias = new String(' ', 10);
        }

        public HeaderLote(string strLinha)
        {
            cod_banco = strLinha.Substring(0, 3);
            lote_servico = strLinha.Substring(3, 4);
            tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1)];
            tipo_operacao = strLinha.Substring(8, 1);
            tipo_servico = strLinha.Substring(9, 2); //TipoServico.GetTipo[strLinha.Substring(9, 2)];
            forma_lancto = strLinha.Substring(11, 2);
            versao_layout = strLinha.Substring(13, 3);
            brancos1 = strLinha.Substring(16, 1);
            tipo_inscricao = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), strLinha.Substring(17, 1));
            num_inscricao_empresa = strLinha.Substring(18, 14);
            convenio_banco = strLinha.Substring(32, 20);
            agencia = strLinha.Substring(53, 5);
            digito_agencia = strLinha.Substring(57, 1);
            conta = strLinha.Substring(58, 12);
            conta_digito = strLinha.Substring(70, 1);
            digito_ag_conta = strLinha.Substring(71, 1);
            nome_empresa = strLinha.Substring(72, 30);
            mensagem = strLinha.Substring(102, 40);
            logradouro = strLinha.Substring(142, 30);
            numero = strLinha.Substring(172, 5);
            complemento = strLinha.Substring(178, 15);
            cidade = strLinha.Substring(192, 20);
            cep = strLinha.Substring(212, 8);
            uf = strLinha.Substring(220, 2);
            brancos8 = strLinha.Substring(222, 8);
            cod_ocorrencias = strLinha.Substring(230, 10);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}",
                cod_banco,
                lote_servico,
                tipo_registro.value,
                tipo_operacao,
                tipo_servico,//.codigo,
                forma_lancto,
                versao_layout,
                brancos1,
                (int)tipo_inscricao,
                num_inscricao_empresa,
                convenio_banco,
                agencia,
                digito_agencia,
                conta,
                conta_digito,
                digito_ag_conta,
                nome_empresa,
                mensagem,
                logradouro,
                numero,
                complemento,
                cidade,
                cep,
                uf,
                brancos8,
                cod_ocorrencias);
        }
    }
}