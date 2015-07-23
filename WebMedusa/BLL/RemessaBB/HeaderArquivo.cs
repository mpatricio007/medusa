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
    /// tipo de registro = 0.
    /// </summary>
    public class HeaderArquivo : LinhaArquivo
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
        /// <summary>
        /// 9 brancos,
        /// uso exclusivo da FEBRABAN/CNAB
        /// de 8 até 16,
        /// 9 posições.
        /// </summary>
        public string brancos9 { get; set; }
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
        /// <summary>
        /// nome do banco,
        /// de 102 até 131,
        /// 30 posições.
        /// </summary>
        public string nome_banco { get; set; }
        /// <summary>
        /// 10 brancos,
        /// uso exclusivo da FEBRABAN/CNAB
        /// de 132 até 141,
        /// 10 posições.
        /// </summary>
        public string brancos10 { get; set; }
        /// <summary>
        /// codigo remessa/retorno,
        /// 1 - REM , 2 - RET
        /// de 142 até 142,
        /// 1 posição.
        /// </summary>
        public TipoRemRet codigo_rem_ret { get; set; }
        /// <summary>
        /// data de geração do arquivo,
        /// formato DDMMAAAA,
        /// de 143 até 150,
        /// 8 posições.
        /// </summary>
        public string data_geracao { get; set; }
        /// <summary>
        /// hora de geração do arquivo,
        /// formato HHMMSS,
        /// de 151 até 156,
        /// 6 posições.
        /// </summary>
        public string hora_geracao { get; set; }

        public string num_arq { get; set; }

        public string versao_layout { get; set; }

        public string densidade_arq { get; set; }

        public string reservado_banco { get; set; }

        public string reservado_empresa { get; set; }

        public string brancos11 { get; set; }

        public string ident_csp { get; set; }

        public string vans { get; set; }

        public string tipo_servico { get; set; }

        //public TipoServico tipo_servico { get; set; }

        public string cod_ocorrencias { get; set; }

        public int tipoConciliacao { get; set; }

        public HeaderArquivo(Lote l)
        {
            cod_banco = l.Conta.BancoAgencia.Banco.codigo;
            lote_servico = new String('0', 4);
            tipo_registro = TipoLinha.HeaderArquivo;
            brancos9 = new String(' ', 9);
            tipo_inscricao = TipoInscricao.CNPJ;
            num_inscricao_empresa = Constantes.CNPJ_FUSP;
            convenio_banco = l.Conta.BancoAgencia.num_convenio.PadRight(20);
            agencia = l.Conta.BancoAgencia.numero.PadLeft(5,'0');
            digito_agencia = l.Conta.BancoAgencia.digito;
            conta = l.Conta.numero.PadLeft(12,'0');
            conta_digito = l.Conta.digito;
            digito_ag_conta = new String(' ', 1);
            nome_empresa = Constantes.NOME_FUSP.PadRight(30);
            nome_banco = l.Conta.BancoAgencia.Banco.nome.PadRight(30);
            brancos10 = new String(' ', 10);
            codigo_rem_ret = TipoRemRet.Remessa;
            data_geracao = Util.DateToStringSemBarras(DateTime.Now);
            hora_geracao = Util.DateToStringHoraMinutoSegundo(DateTime.Now);
            num_arq = String.Format("{0:000000}", l.id_lote);
            versao_layout = Constantes.LAYOUT_ARQUIVO;
            densidade_arq = new String('0', 5);
            reservado_banco = new String(' ', 20);
            reservado_empresa = new String(' ', 20);
            brancos11 = new String(' ', 11);
            ident_csp = new String(' ', 3);
            vans = new String(' ', 3);
            tipo_servico = Constantes.TIPO_SERVICO;// TipoServico.GetTipoPorLote[l.GetType().BaseType];
            cod_ocorrencias = new String(' ', 10);
        }

        public HeaderArquivo(string strLinha)
        {
             cod_banco = strLinha.Substring(0,3);
             lote_servico = strLinha.Substring(3, 4);
             tipo_registro = TipoLinha.GetLinha[strLinha.Substring(7, 1)];
             brancos9 = strLinha.Substring(8, 9);
             tipo_inscricao = (TipoInscricao)Enum.Parse(typeof(TipoInscricao), strLinha.Substring(17, 1));
             num_inscricao_empresa = strLinha.Substring(18, 14);
             convenio_banco = strLinha.Substring(32, 20);
             agencia = strLinha.Substring(52, 5);
             digito_agencia = strLinha.Substring(57,1);
             conta = strLinha.Substring(58, 12);
             conta_digito = strLinha.Substring(70, 1);
             digito_ag_conta = strLinha.Substring(71, 1);
             nome_empresa = strLinha.Substring(72, 30);
             nome_banco = strLinha.Substring(102, 30);
             brancos10 = strLinha.Substring(132, 10);
             codigo_rem_ret = (TipoRemRet)Enum.Parse(typeof(TipoRemRet), strLinha.Substring(142, 1));
             data_geracao = strLinha.Substring(143, 8);
             hora_geracao = strLinha.Substring(151, 6);
             num_arq = strLinha.Substring(157, 6);
             versao_layout = strLinha.Substring(163, 3);
             densidade_arq = strLinha.Substring(166, 5);
             reservado_banco = strLinha.Substring(171, 20);
             reservado_empresa = strLinha.Substring(191, 20);
             brancos11 = strLinha.Substring(211, 11);
             ident_csp = strLinha.Substring(222, 3);
             vans = strLinha.Substring(225, 3);
             tipo_servico = strLinha.Substring(228, 2);// TipoServico.GetTipo[strLinha.Substring(228, 2)];
             cod_ocorrencias = strLinha.Substring(230,10);
             tipoConciliacao = TipoConciliacaoBLL.RetornaIdpeloNome(strLinha.Substring(180, 10));
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}",
                cod_banco,
                lote_servico,
                tipo_registro.value,
                brancos9,
                (int)tipo_inscricao,
                num_inscricao_empresa,
                convenio_banco,
                agencia,
                digito_agencia,
                conta,
                conta_digito,
                digito_ag_conta,
                nome_empresa,
                nome_banco,
                brancos10,
                (int)codigo_rem_ret,
                data_geracao,
                hora_geracao,
                num_arq,
                versao_layout,
                densidade_arq,
                reservado_banco,
                reservado_empresa,
                brancos11,
                ident_csp,
                vans,
                new String(' ', 2), //tipo_servico.codigo, -- problemas no retorno do banco
                cod_ocorrencias);
        }


        
    }
}

//procedure TFcadRemessaTit.Button2Click(Sender: TObject);
//var F:TextFile;
//    txtConta, txtFormaPagto, tpPag,tpPag2: string;
//    somavalor:real;
//    codpag,contaReg,contaHeader,contaRegHeader: integer;
//    vmot: string;
//begin
//  abrirPagtos(2);
//  Qpagto.first;
//  FlogRemessa.txtMotivo.Text:='';

//  While not Qpagto.eof do
//  begin
//     with udados1.QlogRemessa do
//     begin
//        close;
//        Sql.Clear;
//        Sql.add('SELECT * FROM logRemessa where tipo="T" and numeroremessa='+Qpagtocodigo.AsString);
//        Open;
//        vmot:='REMESSA NORMAL';
//        if not eof
//         then begin
//           if FlogRemessa.showmodal<> mrCancel
//            then begin
//              vmot:=FlogRemessa.txtMotivo.Text;
//            end
//            else begin
//              codpag:=Qpagtocodigo.AsInteger;
//              While (not Qpagto.eof) and (codpag=Qpagtocodigo.AsInteger) do Qpagto.next;
//              continue;
//            end;
//         end;
//        append;
//        udados1.QlogRemessanumeroremessa.AsString:=Qpagtocodigo.AsString;
//        udados1.QlogRemessatipo.AsString:='T';
//        udados1.QlogRemessadata.Value:=date;
//        udados1.QlogRemessausuario.AsString:=strNomeUsuario;
//        udados1.QlogRemessamotivo.AsString:=vmot;
//        post;
//     end;
//     AssignFile(F, 'C:\bancobrasil\BBTransf\remessa\tit'+Qpagtocodigo.AsString+'_'+Qpagtoconta.AsString+'.txt'  );
//     Rewrite(F);
//     txtConta:=Qpagtoconta.AsString;
//     With QAlteraDataRemessa do
//     begin
//        close;
//        Sql.Clear;
//        Sql.add('update remessatit set DataGerArquivo=#'+_DataMMDD(date)+'# where codigo='+Qpagtocodigo.AsString);
//        ExecSQL;
//     end;
//     contaReg:=0;
//     contaHeader:=0;
//     Write(F,'001'); // código do BB
//     Write(F,_StrZero(contaHeader,4));
//     Write(F,'0'); // Header 0
//     Write(F,_Exatamente(' ',9,'E'));   // uso febraban
//     Write(F,'2'); // identificado cnpj 2
//     Write(F,'68314830000127'); // cnpj da FUSP

//     Write(F,'0007876210126       '); // número convênio
//     Write(F,'01897X'); // n. ag e digito
//     Write(F,FormataC(SomenteNumeros(Qpagtoconta.AsString),13)); // conta de debito remessa
//     Write(F,' '); // digito verificador ag conta
//     Write(F,_Exatamente('FUNDACAO DE APOIO A USP',30,'E')); // nome da empresa
//     Write(F,_Exatamente('BANCO DO BRASIL S/A',30,'E')); // nome do banco
//     Write(F,_Exatamente(' ',10,'E')); // brancos febraban exclusivo
//     Write(F,'1'); // remessa 1 retorno 2
//     Write(F,DataSemBarras(date)); // data de envio ddmmaaaa
//     Write(F,Copy(timeToStr(time),1,2)+Copy(timeToStr(time),4,2)+Copy(timeToStr(time),7,2)); // hora envio hhmmss
//     Write(F,_StrZero(Qpagtocodigo.AsFloat,6)); // n. sequencial do arquivo
//     Write(F,'030'); // versão do layout;
//     Write(F,'00000'); // densidade
//     Write(F,_Exatamente(' ',51,'E'));
//     Write(F,'   ');
//     Writeln(F,_Exatamente(' ',15,'E'));
//     inc(contaReg);
//     While (txtConta=Qpagtoconta.AsString) and (not Qpagto.eof) do
//     begin
//        inc(contaHea   der);
//        Write(F,'001'); // código do BB
//        Write(F,_StrZero(contaHeader,4));
//        Write(F,'1'); // Header 1
//        Write(F,'C'); // Tipo da operação C
//        Write(F,'98'); // Tipo do Serviço
//        Write(F,Qpagtoforma.asstring); // Forma de Lançamento
//        Write(F,'020'); // versão do layout
//        Write(F,_Exatamente(' ',1,'E'));   // uso febraban
//        Write(F,'2'); // identificado cnpj 2
//        Write(F,'68314830000127'); // cnpj da FUSP
//        Write(F,'0007876210126       '); // número convênio
//        Write(F,'01897X'); // n. ag e digito
//        Write(F,FormataC(SomenteNumeros(Qpagtoconta.AsString),13)); // conta de debito remessa
//        Write(F,' '); // digito verificador ag conta
//        Write(F,_Exatamente('FUNDACAO DE APOIO A USP',30,'E')); // nome da empresa
//        Write(F,_Exatamente(' ',40,'E')); // mensagem
//        Write(F,_Exatamente('AV AFRANIO PEIXOTO, 14',35,'E')); // rua avenida
//        Write(F,_Exatamente('BUTANTA',15,'E')); // Bairro
//        Write(F,_Exatamente('SAO PAULO',20,'E')); // Cidade
//        Write(F,_Exatamente('05507000',8,'E')); // CEP
//        Write(F,_Exatamente('SP',2,'E')); // UF
//        WriteLN(F,_Exatamente(' ',18,'E'));
//        inc(contaReg);
//        contaRegHeader:=0;
//        somavalor:=0;
//        tpPag2:=Qpagtoforma.asstring;
//        While (Qpagtoforma.asstring=tpPag2) and (txtConta=Qpagtoconta.AsString) and (not Qpagto.eof) do
//        begin
//           somavalor:=somavalor+(QpagtoValorPagto.AsFloat*100);
//           inc(contaRegheader);
//           Write(F,'001'); // código do BB
//           Write(F,_StrZero(contaHeader,4));
//           Write(F,'3'); // Header 1
//           Write(F,_StrZero(contaRegHeader,5));   // n. sequencial no lote
//           Write(F,'J'); // Segmento A
//           Write(F,'0'); // tipo de movimento
//           Write(F,'00'); // código da instrução do movimento
//           Write(F,_Exatamente(QpagtoBancoDestino.asstring,3,'E')); // banco destino
//           Write(F,_Exatamente(QpagtoCodMoeda.AsString,1,'E')); // moeda
//           Write(F,_Exatamente(QpagtoDigVerCodBarra.AsString,1,'E')); // dígito cód barras
//           Write(F,_Exatamente(QpagtoValorImpCodBarra.AsString,14,'E')); // valor impresso no código de barras
//           Write(F,_Exatamente(QpagtoCampoLivreCodBarra.AsString,25,'E')); // campo livre cod barras
//           Write(F,_Exatamente(QpagtoNomeCedente.AsString,30,'E')); // nome do cedente
//           Write(F,DataSemBarras(QpagtoDataVencto.Value)); // data vencto
//           Write(F,_StrZero(QpagtoValor.AsFloat*100,15));   // valor
//           Write(F,_StrZero(QpagtoDesconto.AsFloat*100,15));   // desconto
//           Write(F,_StrZero(QpagtoMoraMulta.AsFloat*100,15));   // mora multa
//           Write(F,DataSemBarras(QpagtoDataPagto.Value)); // data pagto
//           Write(F,_StrZero(QpagtoValorPagto.AsFloat*100,15));   // valor pagamento
//           Write(F,_StrZero(0,15));   // valor moeda
//           Write(F,_StrZero(Qpagtocodtit.AsFloat,20)); // código Id do título DadosRemessaTit
//           Write(F,_Exatamente(QpagtoDescricao.AsString,20,'E')); // outras informações
//           Writeln(F,_Exatamente(' ',18,'E'));  // febraban
//           inc(contaReg);
//           Qpagto.next;
//        end;
//        Write(F,'001'); // código do BB
//        Write(F,_StrZero(contaHeader,4));
//        Write(F,'5'); // Header 1
//        Write(F,_Exatamente(' ',9,'E'));   // uso febraban
//        Write(F,_StrZero(contaRegHeader+2,6));  // total de registro 1 3 e 5
//        Write(F,_StrZero(somavalor,18));   // total valor
//        WriteLN(F,_Exatamente(' ',199,'E'));
//        inc(contaReg);
//     end;
//     inc(contaReg);
//     Write(F,'001'); // código do BB
//     Write(F,'9999');
//     Write(F,'9'); // Header 1
//     Write(F,_Exatamente(' ',9,'E'));   // uso febraban
//     Write(F,_StrZero(contaHeader,6));  // total de registro 1
//     Write(F,_StrZero(contaReg,6));  // total de registro do arquivo
//     WriteLN(F,_Exatamente(' ',211,'E'));
//     CloseFile(F);
//  end;
//  Showmessage('Arquivos CNAB240 de remessa de títulos processados com sucesso');
//end;

//procedure TFcadRemessaTit.BitBtn5Click(Sender: TObject);
//begin
//   FImpDda.show;
//end;

//end.