using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class CarregarPagAgendados
    {
        

        public string GerarLancamentosBB(DateTime data)
        {
            string msg = String.Format("Importação de pagamentos agendados na data {0} concluída com sucesso!", data.ToShortDateString());
            try
            {
               
                // Transferência Conta Corrente DOC TED
                Contexto _dbContext = new Contexto();

                TipoImpArquivo impArq = _dbContext.TipoImpArquivos.Where(it => it.id_tipoimp == 1).FirstOrDefault();// importação de extrato bb
                ImportaArquivo imp = ImportaArquivoBLL.VerificaSeJaImportou(impArq, data);//ctx.ImportaArquivos.Where(i => i.data == data & i.id_tipoimp == 2).FirstOrDefault();
                if (imp == null)
                    imp = new ImportaArquivo();
                else
                    return String.Format("Importação de agenda já realizada em {0:d} por {1}. Exclua a importação!", imp.data_importacao, imp.Usuario.PessoaFisica.nome);
                imp.data = data;
                imp.data_importacao = DateTime.Now;
                imp.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;

                imp.id_tipoimp = impArq.id_tipoimp;
                imp.Lancamentos = new List<ContaLancto>();
                try
                {
                    #region CarregarPagtos
                    var lstCarregarPagto = _dbContext.CarregarPagtos.Where(u => u.datapagto == data).ToList();
                    foreach (CarregarPagto c in lstCarregarPagto)
                    {
                        string conta = Util.TirarFormatoConta(c.conta);
                        Conta cont = _dbContext.Contas.Where(t => t.numero == conta & t.BancoAgencia.Banco.codigo == "001" & t.status == true).FirstOrDefault();
                        if (cont != null)
                        {
                            ContaLancto clDebito = new ContaLancto();
                            clDebito.data = data;
                            clDebito.id_conta = cont.id_conta;
                            clDebito.id_tipo_lcto = TipoLctoBLL.PAGAMENTOS; // Código para Pagamentos                    
                            FormaPagto fpDAL = _dbContext.FormaPagtos.Where(t => t.banco.codigo == "001" & t.codigo == c.forma).FirstOrDefault();
                            if (fpDAL == null)
                                return String.Format("Forma de pagamento {0} não encontrada!Cadastre.", c.forma);
                            clDebito.descricao = fpDAL.nome;
                            clDebito.valor = c.valor;
                            imp.Lancamentos.Add(clDebito);
                        }
                        else
                        {
                            return String.Format("Conta {0} não encontrada! Cadastre.", c.conta);
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }


                try
                {
                    #region Pagamentos de Boletos Bancários
                    var lstCarregarBoletos = _dbContext.CarregarBoletos.Where(u => u.datapagto == data).ToList();
                    foreach (CarregarBoletos c in lstCarregarBoletos)
                    {
                        string conta = Util.TirarFormatoConta(c.conta);
                        Conta cont = _dbContext.Contas.Where(t => t.numero == conta & t.BancoAgencia.Banco.codigo == "001" & t.status == true).FirstOrDefault();
                        if (cont != null)
                        {
                            ContaLancto clDebito = new ContaLancto();
                            clDebito.data = data;
                            clDebito.id_conta = cont.id_conta;
                            clDebito.id_tipo_lcto = TipoLctoBLL.PAGAMENTOS; // Código para Pagamentos                    
                            FormaPagto fpDAL = _dbContext.FormaPagtos.Where(t => t.banco.codigo == "001" & t.codigo == c.forma).FirstOrDefault();
                            if (fpDAL == null)
                                return String.Format("Forma de pagamento {0} não encontrada!Cadastre.", c.forma);
                            clDebito.descricao = fpDAL.nome;
                            clDebito.valor = c.valor;
                            imp.Lancamentos.Add(clDebito);
                        }
                        else
                        {
                            return String.Format("Conta {0} não encontrada!Cadastre.", conta);
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }

                try
                {
                    #region impostos e contas de consumo
                    var lstCarregarImpostoConsumo = _dbContext.CarregarImpostoConsumo.Where(u => u.datapagto == data).ToList();

                    foreach (CarregarImpostoConsumo c in lstCarregarImpostoConsumo)
                    {

                        ContaBLL ct = new ContaBLL();
                        string conta = Util.TirarFormatoConta(c.conta);
                        Conta cont = ct.Find(t => t.numero == conta & t.BancoAgencia.Banco.codigo == "001" & t.status == true).FirstOrDefault();
                        if (cont != null)
                        {
                            ContaLancto clDebito = new ContaLancto();
                            clDebito.data = data;
                            clDebito.id_conta = cont.id_conta;
                            clDebito.id_tipo_lcto = TipoLctoBLL.PAGAMENTOS; // Código para Pagamentos
                            clDebito.descricao = c.descricao;
                            clDebito.valor = c.valor;
                            imp.Lancamentos.Add(clDebito);
                        }
                        else
                        {
                            return String.Format("Conta {0} não encontrada!Cadastre.", conta);
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }
                _dbContext.ImportaArquivos.Add(imp);
               _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                msg = String.Format("Erro!{0}", ex.Message);
            }
            return msg;
            
            
        }
    }
}
