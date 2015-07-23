using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Medusa.LIB;
using Medusa.DAL;
using System.Text;

namespace Medusa.BLL.LayoutBB
{
    public class ExtratoBB
    {
        public const string pathRetorno = @"c:\BancoBrasil\BBTransf\RetornoConciliacao\";
        public static Dictionary<TipoLinha, Func<string, LinhaExtratoBB>> dicTipoLinha = new Dictionary<TipoLinha, Func<string, LinhaExtratoBB>>()
        {
            {TipoLinha.Header, LinhaExtratoBB.CriarHeader },
            {TipoLinha.Lancamento, LinhaExtratoBB.CriarLancamento},
            {TipoLinha.SaldoAnterior, LinhaExtratoBB.CriarSaldoAnterior },
            {TipoLinha.SaldoAtual, LinhaExtratoBB.CriarSaldoAtual },
            {TipoLinha.Trailer, LinhaExtratoBB.CriarTrailer }
        };

        public static List<LinhaExtratoBB> GetArquivoRetorno(StreamReader reader)
        {
            List<LinhaExtratoBB> lst = new List<LinhaExtratoBB>();
            try
            {
                while (!reader.EndOfStream)
                {
                    string strLinha = reader.ReadLine();
                    lst.Add(ExtratoBB.dicTipoLinha[ExtratoBB.GetTipoLinha(strLinha)](strLinha));
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return lst;
        }

        public static string ImportarLancamentos(List<LinhaExtratoBB> extrato, out bool rt)
        {
            int i = 0;
            Contexto ctx = new Contexto();
            rt = false;
            try
            {
                DateTime data;

                Banco banco = ctx.Bancos.Where(it => it.codigo == "001").FirstOrDefault();

                var header = extrato.OfType<HeaderExtratoBB>().FirstOrDefault();
                data = header.dataGravacao;
                TipoImpArquivo impArq = ctx.TipoImpArquivos.Where(it => it.id_tipoimp == 2).FirstOrDefault();// importação de extrato bb
                ImportaArquivo imp = ImportaArquivoBLL.VerificaSeJaImportou(impArq, data);//ctx.ImportaArquivos.Where(i => i.data == data & i.id_tipoimp == 2).FirstOrDefault();
                if (imp == null)
                    imp = new ImportaArquivo();
                else
                    return String.Format("Importação de extrato do BB já realizada em {0:d} por {1}. Exclua a importação!", imp.data_importacao, imp.Usuario.PessoaFisica.nome);
                imp.data = data;
                imp.data_importacao = DateTime.Now;
                imp.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;

                imp.id_tipoimp = impArq.id_tipoimp;
                imp.Lancamentos = new List<ContaLancto>();
                imp.Saldos = new List<ContaSaldoFinal>();
                Conta conta = new Conta();

                foreach (var l in extrato.OfType<LancamentoExtratoBB>())
                {



                    if (l.conta_num + l.conta_dig != conta.numero + conta.digito)
                    {
                        conta = ctx.Contas.Where(c => c.BancoAgencia.numero == l.agencia & c.BancoAgencia.id_banco == banco.id_banco
                          & c.numero == l.conta_num & c.digito == l.conta_dig).FirstOrDefault();

                        if (conta == null)
                            return String.Format("Erro na importação do extrato. Cadastre a conta número {0}-{1} agência {2}", l.conta_num, l.conta_dig, l.agencia);
                        else
                        {
                            var sf = extrato.OfType<SaldoAtualExtratoBB>().Where(it => it.agencia == conta.BancoAgencia.numero
                                & it.conta_num == conta.numero & it.conta_dig == conta.digito).FirstOrDefault();

                            ContaSaldoFinal saldofinal = new ContaSaldoFinal();

                            saldofinal.Conta = conta;
                            saldofinal.data = sf.data;
                            saldofinal.saldo = sf.valor;
                            imp.Saldos.Add(saldofinal);
                        }
                    }


                    if (!conta.saldo_inicial.HasValue)
                    {
                        var saldo = extrato.OfType<SaldoAnteriorExtratoBB>().Where(it => it.agencia == conta.BancoAgencia.numero
                            & it.conta_num == conta.numero & it.conta_dig == conta.digito).FirstOrDefault();
                        conta.saldo_inicial = saldo.saldoAnterior;
                        conta.data_saldo_inicial = saldo.dataSaldoAnterior;
                    }

                    TipoLcto tipo = new TipoLcto();
                    if (l.cod_lcto != tipo.codigo)
                        tipo = ctx.TipoLctos.Where(t => t.id_banco == banco.id_banco & t.codigo == l.cod_lcto).FirstOrDefault();

                    if (tipo == null)
                    {
                        ctx = new Contexto();
                        Tarefa tar = new Tarefa();
                        tar.data = DateTime.Now;
                        tar.id_usuario_de = imp.id_usuario;
                        tar.tarefa = String.Format("Cadastre o tipo de lançamento {0} do BB.", tipo.codigo);
                        tar.Destinatarios = new List<TarefaDestinatario>();
                        ctx.Pessoas.OfType<UsuarioFusp>().Where(u => u.nivel == 1).ToList().ForEach(u => tar.Destinatarios.Add(new TarefaDestinatario() { Usuario = u, Tarefa = tar }));
                        ctx.Tarefas.Add(tar);
                        ctx.SaveChanges();
                        return "Erro na importação do extrato. Contate administrador!";
                    }

                    else if (tipo.importar)
                    {
                        ContaLancto lcto = new ContaLancto();
                        lcto.data = data;
                        lcto.id_tipo_lcto = tipo.id_tipo_lcto;
                        lcto.id_conta = conta.id_conta;
                        lcto.valor = l.valor;
                        lcto.descricao = l.descricao;
                        lcto.num_documento = l.num_documento;
                        lcto.Tarefas = new List<TarefaLcto>();
                        TarefaLcto tar = new TarefaLcto();
                        tar.data = DateTime.Now;
                        tar.id_usuario_de = imp.id_usuario;
                        tar.status = Constantes.TAREFA_PENDENTE;
                        tar.Destinatarios = new List<TarefaDestinatario>();
                        tipo.UsuarioTipoLcto.ToList().ForEach(u => tar.Destinatarios.Add(new TarefaDestinatario() { Usuario = u.Usuario, Tarefa = tar }));
                        lcto.Tarefas.Add(tar);
                        imp.Lancamentos.Add(lcto);
                    }
                    i++;

                }
                ctx.ImportaArquivos.Add(imp);
                //var trailer = extrato.OfType<TrailerExtratoBB>().FirstOrDefault();
                rt = ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return String.Format("Erro na importação do extrato! {0}", ex.Message + i.ToString());
            }
            return rt ? "Importação de extrato do BB realizada com sucesso!" : "Erro na importação do extrato!";
        }

        public static TipoLinha GetTipoLinha(string strLinha)
        {
            TipoLinha t;
            Enum.TryParse(strLinha.Substring(0, 1), out t);
            if (!Enum.IsDefined(typeof(TipoLinha), t))
                Enum.TryParse(strLinha.Substring(0, 1) + strLinha.Substring(41, 1), out t);
            return t;
        }

        public string ProcessarArquivosFtpBB()
        {

            bool rt = false;
            string saida = "";
            try
            {
                string[] filePaths = Directory.GetFiles(ExtratoBB.pathRetorno, "*.ret");
                foreach (var item in filePaths)
                {
                    saida = BLL.LayoutBB.ExtratoBB.ImportarLancamentos(
                    BLL.LayoutBB.ExtratoBB.GetArquivoRetorno(new StreamReader(item)),out rt);
                }
            }
            catch (Exception ex)
            {
                saida += ex.Message;                
            }
          
            return saida;
        }
    }
}