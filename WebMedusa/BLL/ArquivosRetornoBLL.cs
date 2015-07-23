using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;
using Medusa.BLL.RemessaBB;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class ArquivosRetornoBLL : AbstractCrudWithLog<ArquivosRetorno>
    {
        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            base.Add();
        }

        public void CancelarProcessamento()
        {
            if (ObjEF.status.GetValueOrDefault())
            {
                var remBLL = new RemessaBLL<Remessa>();
                foreach (var item in ObjEF.Remessas)
                {
                    remBLL.Get(item.id_remessa);
                    remBLL.EstornarConciliacao();
                }

                ObjEF.status = false;
                Update();
                SaveChanges();

            }
        }

        public List<ArquivosRetorno> GetArquivosNaoProcessados()
        {   
            string[] filePaths = Directory.GetFiles(Cnab240BB.pathRetorno, "*.ret");
            var l = new List<ArquivosRetorno>();
            foreach (var item in filePaths)
            {
                ObjEF = GetArq(item);

                if (!JaProcessado(ObjEF.codigo_banco))
                    l.Add(ObjEF);
            } 
            return l.OrderBy(it => it.codigo_banco).ToList();

        }

        public ArquivosRetorno GetArq(string file)
        {
            var reader = new StreamReader(file);
            var header = new HeaderArquivo(reader.ReadLine());
            var headerLote = new HeaderLote(reader.ReadLine());            
            var arq = new ArquivosRetorno();    
            arq.codigo_banco = Convert.ToInt32(header.num_arq);
            arq.data_criacao = Util.StringSemBarrasToDate(header.data_geracao, header.hora_geracao).GetValueOrDefault();
            
            arq.log_importacao = file;
            
            var tipoConBLL = new TipoConciliacaoBLL();
            tipoConBLL.Get(header.tipoConciliacao);
            arq.TipoConciliacao = tipoConBLL.ObjEF;
            arq.id_tipo_conciliacao = header.tipoConciliacao;

            var tipoArqBLL = new TipoArquivoBLL();
            tipoArqBLL.GetPorTipoSegmento(reader.ReadLine().Substring(13, 1));
            arq.id_tipo_arquivo = tipoArqBLL.ObjEF.id_tipo_arquivo;
            arq.TipoArquivo = tipoArqBLL.ObjEF;
            reader.Close();
            return arq;
        }

        public bool JaProcessado(int intCod_banco)
        {
            return _dbContext.ArquivosRetornos.Where(it => it.codigo_banco == intCod_banco && it.status == true).Count() > 0;
        }

        public string ProcessarPrevia()
        {

            try
            {
                DateTime dt = Util.StringToDate("15/05/2013").GetValueOrDefault(); // dia de implantação da agenda diretamente do remessa bancária

                #region Pagamentos de Boletos
                var lctTitulos = from l in _dbContext.RemessaTits.Where(it => it.id_tipo_ret == TipoRetornoBLL.InclusaoEfetuada
                    & it.id_lcto_conta == null                 
                    & it.LoteRemessa.data_pgto >= dt
                              )
                                 group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa }
                                     into g
                                     select g;

                 

                foreach (var item in lctTitulos)               
                    CreateContaLancto(item);
                #endregion

                #region Pagamentos de Guias
                var lctGuias = from l in _dbContext.RemessaCons.Where(it => it.id_tipo_ret == TipoRetornoBLL.InclusaoEfetuada
                    & it.id_lcto_conta == null                        
                    & it.LoteRemessa.data_pgto >= dt
                              )
                                 group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa }
                                     into g
                                     select g;

                foreach (var item in lctGuias)
                    CreateContaLancto(item);
                #endregion

                #region Pagamentos de Pagamentos
                var lctPagamentos = from l in _dbContext.RemessaPAGs.Where(it => it.id_tipo_ret == TipoRetornoBLL.InclusaoEfetuada 
                        & it.id_lcto_conta == null
                        
                             & it.LoteRemessa.data_pgto >= dt
                              )
                                    group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa, mesmaAg = l.agencia == l.Lote.Conta.BancoAgencia.numero }
                                        into g
                                        select g;

                foreach (var item in lctPagamentos)
                    CreateContaLancto(item);
                #endregion

                #region Pagamento de GPS
                var lctGPS = from l in _dbContext.RemessaGpsSemCodBarra.Where(it => it.id_tipo_ret == TipoRetornoBLL.InclusaoEfetuada
                    & it.id_lcto_conta == null
                    & it.LoteRemessa.data_pgto >= dt
                            )
                             group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa }
                                 into g
                                 select g;

                foreach (var item in lctGPS)
                    CreateContaLancto(item);
                #endregion

                #region Pagamento de GRU
                var lctGRU = from l in _dbContext.RemessaGRUs.Where(it => it.id_tipo_ret == TipoRetornoBLL.InclusaoEfetuada
                    & it.id_lcto_conta == null
                    & it.LoteRemessa.data_pgto >= dt
                            )
                             group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa }
                                 into g
                                 select g;

                foreach (var item in lctGRU)
                    CreateContaLancto(item);
                #endregion

                _dbContext.SaveChanges();
                
                return String.Format("Prévia processada com sucesso!");



            }
            catch (Exception ex)
            {
                return String.Format("Erro ao processar prévia! {0}", ex.Message);
            }
        }


        public string ProcessarPreviaComErro(DateTime dt, List<int> lst)
        {

            try
            {               

                #region Pagamentos de Boletos
                var lctTitulos = from l in _dbContext.RemessaTits.Where(it => lst.Contains(it.id_tipo_ret)
                    & it.id_lcto_conta == null
                    & it.LoteRemessa.data_pgto == dt)
                                 group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa }
                                     into g
                                     select g;



                foreach (var item in lctTitulos)
                    CreateContaLancto(item);
                #endregion

                #region Pagamentos de Guias
                var lctGuias = from l in _dbContext.RemessaCons.Where(it => lst.Contains(it.id_tipo_ret)
                    & it.id_lcto_conta == null
                    & it.LoteRemessa.data_pgto == dt)
                               group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa }
                                   into g
                                   select g;

                foreach (var item in lctGuias)
                    CreateContaLancto(item);
                #endregion

                #region Pagamentos de Pagamentos
                var lctPagamentos = from l in _dbContext.RemessaPAGs.Where(it => lst.Contains(it.id_tipo_ret)
                        & it.id_lcto_conta == null
                        & it.LoteRemessa.data_pgto == dt)
                                    group l by new GroupLoteFormaPagto { FormaPagto = l.FormaPagto, LoteRemessa = l.LoteRemessa, mesmaAg = l.agencia == l.Lote.Conta.BancoAgencia.numero }
                                        into g
                                        select g;

                foreach (var item in lctPagamentos)
                    CreateContaLancto(item);
                #endregion

                _dbContext.SaveChanges();

                return String.Format("Prévia processada com sucesso!xxxxxxxxxx");



            }
            catch (Exception ex)
            {
                return String.Format("Erro ao processar prévia! {0}", ex.Message);
            }
        }

        private void CreateContaLancto(IGrouping<GroupLoteFormaPagto, Remessa> item)
        {
            var clDAL = new ContaLancto();
            clDAL.data = item.Key.LoteRemessa.data_pgto.GetValueOrDefault();
            clDAL.id_conta = item.Key.LoteRemessa.id_conta;
            clDAL.id_tipo_lcto = TipoLctoBLL.PAGAMENTOS;
            clDAL.descricao = item.Key.FormaPagto.nome;
            clDAL.valor = item.Sum(it => it.valor);

            foreach (var remTit in item)
                remTit.ContaLancto = clDAL;

            _dbContext.ContaLanctos.Add(clDAL);
        }

        public void ProcessarArquivos()
        {
            try
            {
                var cnab240 = new Cnab240BB();
                foreach (var item in GetArquivosNaoProcessados())
                {
                    item.TipoArquivo = null;
                    item.TipoConciliacao = null;
                    ObjEF = item;
                    ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
                    ObjEF.data_processado = DateTime.Now;
                    Add();
                    SaveChanges();

                    ObjEF.log_importacao = Cnab240BB.dicTipoArquivo[item.id_tipo_arquivo.GetValueOrDefault()](item.log_importacao, item.id_arquivo);
                    ObjEF.status = String.IsNullOrEmpty(ObjEF.log_importacao);
                    Update();
                    SaveChanges();
                    Detach();
                }

                Util.ResponseToPage(String.Format("../../relatorios/remessabancaria/RPagtosComOcorrencia.aspx?pk={0}",Util.DateToString(DateTime.Now).Criptografar()), "Arquivos processados!");
            }
            catch (Exception ex)
            {
                Util.ShowMessage(String.Format("Erro ao processar arquivos! {0}", ex.Message));
            }
            finally
            {
                ProcessarPrevia();
            }

        }

        public string ProcessarArquivosFtpBB()
        {  

            string rt = "";
            try
            {
                var cnab240 = new Cnab240BB();
                foreach (var item  in GetArquivosNaoProcessados())
                {
                    item.TipoArquivo = null;
                    item.TipoConciliacao = null;
                    ObjEF = item;
                    ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
                    ObjEF.data_processado = DateTime.Now;
                    _dbSet.Add(ObjEF);
                    _dbContext.SaveChanges();
                    ObjEF.log_importacao = Cnab240BB.dicTipoArquivo[item.id_tipo_arquivo.GetValueOrDefault()](item.log_importacao, item.id_arquivo);
                    ObjEF.status = String.IsNullOrEmpty(ObjEF.log_importacao);

                    _dbContext.SaveChanges();
                    Detach();
                }
                
            }
            catch (Exception ex)
            {

                string w = "";
                foreach (System.Data.Entity.Validation.DbEntityValidationResult erro in _dbContext.GetValidationErrors())
                {
                    foreach (System.Data.Entity.Validation.DbValidationError msg in erro.ValidationErrors)
                        w += msg.ErrorMessage;
                }

                return String.Format("Erro: {0} (1}", ex.Message,w);
            }
           
            rt += ProcessarPrevia();
           
            return rt;
        }
    }
}


class GroupLoteFormaPagto
{
    public Lote LoteRemessa { get; set; }

    public FormaPagto FormaPagto { get; set; }

    public bool mesmaAg { get; set; }
}