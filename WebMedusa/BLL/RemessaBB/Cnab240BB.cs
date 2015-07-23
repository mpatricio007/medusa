using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using System.IO;
using Medusa.LIB;

namespace Medusa.BLL.RemessaBB
{
    public class Cnab240BB
    {
        public static Dictionary<TipoLinha, Func<string, LinhaArquivo>> dicTipoLinha = new Dictionary<TipoLinha, Func<string, LinhaArquivo>>()
        {
            { TipoLinha.HeaderArquivo, LinhaArquivo.CriarHeader },        
            { TipoLinha.HeaderLote, LinhaArquivo.CriarHeaderLote },
            { TipoLinha.SegmentoJ, LinhaArquivo.CriarSegmentoJ },
            { TipoLinha.SegmentoA, LinhaArquivo.CriarSegmentoA },
            { TipoLinha.SegmentoB, LinhaArquivo.CriarSegmentoB },
            { TipoLinha.SegmentoO, LinhaArquivo.CriarSegmentoO },
            { TipoLinha.SegmentoW, LinhaArquivo.CriarSegmentoW },
            { TipoLinha.SegmentoZ, LinhaArquivo.CriarSegmentoZ },
            { TipoLinha.SegmentoN, LinhaArquivo.CriarSegmentoN },
            { TipoLinha.TrailerLote, LinhaArquivo.CriarTrailerLote },
            { TipoLinha.TrailerArquivo, LinhaArquivo.CriarTrailerArquivo },

        };

        public static Dictionary<int, Func<string, int, string>> dicTipoArquivo = new Dictionary<int, Func<string, int, string>>()
        {
            { TipoArquivoBLL.Boleto, Cnab240BB.LerArquivoBoleto },        
            { TipoArquivoBLL.Guias, Cnab240BB.LerArquivoGuias },
            { TipoArquivoBLL.GpsSemBarra, Cnab240BB.LerArquivoGpsSemBarra },
            { TipoArquivoBLL.Pagamentos, Cnab240BB.LerArquivoPagamentos }
        };

        public const string pathRemessa =  @"C:\BancoBrasil\BBTransf\Remessa\";
        public const string pathRetorno =  @"C:\BancoBrasil\BBTransf\Retorno\";

        public void GerarArquivo(LoteBoleto lote)
        {
            string arq = String.Format("{0}{1}{2}_{3}{4}", pathRemessa, "tit", lote.id_lote, lote.Conta.StrContaDigito, ".txt");
            int countReg = 0;
            int countLote = 0;
            int countTit;
            decimal soma;


            var listaTitulos = lote.Boletos.Where(it => it.id_tipo_ret != TipoRetornoBLL.Estornado & /*it.id_tipo_ret != TipoRetornoBLL.Rejeitado & */!it.id_tipo_conciliacao.HasValue).ToList();
            StreamWriter file = File.CreateText(arq);
            try
            {

                countReg++;
                file.WriteLine(new HeaderArquivo(lote).ToString());


                var ds = from l in listaTitulos                         
                         group l by l.FormaPagto
                             into g
                             select new
                             {
                                 g.Key,
                                 titulos = g
                             };

                foreach (var gr in ds.OrderBy(it => it.Key.codigo))
                {
                    countLote++;
                    countReg++;
                    file.WriteLine(new HeaderLote(lote, gr.Key.codigo, countLote).ToString());
                    soma = 0;
                    countTit = 0;
                    foreach (var bol in gr.titulos.OrderBy(it => it.id_remessa))
                    {
                        countTit++;
                        countReg++;
                        soma += bol.valor;
                        file.WriteLine(new SegmentoJ(bol, countTit, countLote).ToString());
                    }
                    file.WriteLine(new TrailerLote(lote, countLote, countTit, soma));
                    countReg++;
                }
                countReg++;
                file.WriteLine(new TrailerArquivo(lote, countLote, countReg).ToString());


                var remTit = new RemessaTitBLL();
                foreach (var item in listaTitulos)
                {
                    remTit.Get(item.id_remessa);
                    remTit.Processar();
                }

                var log = new LogSistemaBLL();
                log.GravarLog("Arquivo de Lote Titulos BB gerado com sucesso!");
                file.Close();
            }
            catch (Exception ex)
            {
                var log = new LogSistemaBLL();
                log.GravarLog(String.Format("Erro ao gerar arquivo Lote Titulos BB! {0}", ex.Message));

            }

        }

        public void GerarArquivo(LotePagBB lote)
        {
            string arq = String.Format("{0}{1}{2}_{3}{4}", pathRemessa, "rem", lote.id_lote, lote.Conta.StrContaDigito, ".txt");
            int countReg = 0;
            int countLote = 0;
            int countTit;
            decimal soma;


            var listaPagtos = lote.Remessas.Where(it => it.id_tipo_ret != TipoRetornoBLL.Estornado & !it.id_tipo_conciliacao.HasValue).ToList();
            StreamWriter file = File.CreateText(arq);
            try
            {

                countReg++;
                file.WriteLine(new HeaderArquivo(lote).ToString());


                var ds = from l in listaPagtos
                         group l by l.FormaPagto.codigo
                             into g
                             select new
                             {
                                 g.Key,
                                 remessas = g
                             };

                foreach (var gr in ds.OrderBy(it => it.Key))
                {
                    countLote++;
                    countReg++;
                    file.WriteLine(new HeaderLote(lote, gr.Key, countLote).ToString());
                    soma = 0;
                    countTit = 0;
                    foreach (var rem in gr.remessas.OrderBy(it => it.id_remessa))
                    {
                        countTit++;
                        countReg++;
                        soma += rem.valor;
                        file.WriteLine(new SegmentoA(rem, countTit, countLote).ToString());
                        countTit++;
                        countReg++;
                        file.WriteLine(new SegmentoB(rem, countTit, countLote).ToString());
                    }
                    file.WriteLine(new TrailerLote(lote, countLote, countTit, soma));
                    countReg++;
                }
                countReg++;
                file.WriteLine(new TrailerArquivo(lote, countLote, countReg).ToString());


                var remTit = new RemessaPAGBLL();
                foreach (var item in listaPagtos)
                {
                    remTit.Get(item.id_remessa);
                    remTit.Processar();
                }

                var log = new LogSistemaBLL();
                log.GravarLog("Arquivo de Lote Pagamentos BB gerado com sucesso!");
                file.Close();
            }
            catch (Exception ex)
            {
                var log = new LogSistemaBLL();
                log.GravarLog(String.Format("Erro ao gerar arquivo Lote Titulos BB! {0}", ex.Message));

            }
        }

        public void GerarArquivo(LoteCons lote)
        {
            string arq = String.Format("{0}{1}{2}_{3}{4}", pathRemessa, "cons", lote.id_lote, lote.Conta.StrContaDigito, ".txt");
            int countReg = 0;
            int countLote = 0;
            int countGuias;
            decimal soma;


            var listaTitulos = lote.Guias.Where(it => it.id_tipo_ret != TipoRetornoBLL.Estornado & !it.id_tipo_conciliacao.HasValue).ToList();
            StreamWriter file = File.CreateText(arq);
            try
            {
                 var ds = from l in listaTitulos                         
                         group l by l.FormaPagto
                             into g
                             select new
                             {
                                 g.Key,
                                 titulos = g
                             };

                 foreach (var gr in ds.OrderBy(it => it.Key.codigo))
                 {
                     countReg++;
                     file.WriteLine(new HeaderArquivo(lote).ToString());
                     countLote++;
                     countReg++;
                     file.WriteLine(new HeaderLote(lote, gr.Key.codigo, countLote).ToString());
                     soma = 0;
                     countGuias = 0;
                     foreach (var guia in listaTitulos)
                     {
                         countGuias++;
                         countReg++;
                         soma += guia.valor;
                         file.WriteLine(new SegmentoO(guia, countGuias, countLote).ToString());
                     }
                     countReg++;
                     file.WriteLine(new TrailerLote(lote, countLote, countGuias, soma));

                     countReg++;
                     file.WriteLine(new TrailerArquivo(lote, countLote, countReg).ToString());

                     var remcons = new RemessaConsBLL();
                     foreach (var item in listaTitulos)
                     {
                         remcons.Get(item.id_remessa);
                         remcons.Processar();
                     }
                     var log = new LogSistemaBLL();
                     log.GravarLog("Arquivo de Lote Guias e Tributos BB gerado com sucesso!");
                 }

            }
            catch (Exception ex)
            {
                var log = new LogSistemaBLL();
                log.GravarLog(String.Format("Erro ao gerar arquivo Lote de Guias e Tributos BB! {0}", ex.Message));

            }
            file.Close();
        }

        public static string LerArquivoBoleto(string file, int id_arquivo)
        {
            string msg = "";
            string aut = "";
            var reader = new StreamReader(file);
            List<LinhaArquivo> l = new List<LinhaArquivo>();
            LinhaArquivo linha;
            var remTitBLL = new RemessaTitBLL();
            var tipoConciliacaoBLL = new TipoConciliacaoBLL();
            string strLinha = "";
            try
            {
                while (!reader.EndOfStream)
                {
                    strLinha = reader.ReadLine();
                    linha = Cnab240BB.dicTipoLinha[TipoLinha.GetTipoLinha(strLinha)](strLinha);
                    l.Add(linha);
                }

                var header = l.OfType<HeaderArquivo>().FirstOrDefault();
                tipoConciliacaoBLL.Get(header.tipoConciliacao);
                foreach (var item in l.OfType<SegmentoJ>())
                {
                    remTitBLL.ObjEF = new RemessaTit();
                    remTitBLL.Get(Convert.ToInt32(item.cod_ref_sacado));
                    if (remTitBLL.RetornoEhValido(item))
                    {
                        var retorno = item.cod_ocorrencias.Trim().Left(2);
                                                
                        if(l[l.IndexOf(item) + 1] is SegmentoZ)
                        //if (tipoConciliacaoBLL.ObjEF.tem_autenticacao.GetValueOrDefault() & retorno == TipoRetornoBLL.CodConsolidado)
                        {
                            var segz = (SegmentoZ)l[l.IndexOf(item) + 1];
                            aut = segz.aut_bancaria;
                        }
                        else
                            aut = "";
                        remTitBLL.Conciliar(retorno, aut, header.tipoConciliacao, id_arquivo);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = String.Format("Erro ao ler arquivo Lote Titulos BB! {0}", ex.Message);
            }
            reader.Close();
            return msg;
        }

        public void GerarArquivo(LoteGPS lote)
        {
            string arq = String.Format("{0}{1}{2}_{3}{4}", pathRemessa, "GPS", lote.id_lote, lote.Conta.StrContaDigito, ".txt");
            int countReg = 0;
            int countLote = 0;
            int countGuias;
            decimal soma;


            var listaTitulos = lote.Guias.Where(it => it.id_tipo_ret != TipoRetornoBLL.Estornado & !it.id_tipo_conciliacao.HasValue).ToList();
            StreamWriter file = File.CreateText(arq);
            try
            {
                var ds = from l in listaTitulos
                         group l by l.FormaPagto
                             into g
                             select new
                             {
                                 g.Key,
                                 titulos = g
                             };

                foreach (var gr in ds.OrderBy(it => it.Key.codigo))
                {
                    countReg++;
                    file.WriteLine(new HeaderArquivo(lote).ToString());
                    countLote++;
                    countReg++;
                    file.WriteLine(new HeaderLote(lote, gr.Key.codigo, countLote).ToString());
                    soma = 0;
                    countGuias = 0;
                    foreach (var guia in listaTitulos)
                    {
                        countGuias++;
                        countReg++;
                        soma += guia.valor;
                        file.WriteLine(new SegmentoN(guia, countGuias, countLote).ToString());

                    }
                    countReg++;
                    file.WriteLine(new TrailerLote(lote, countLote, countGuias, soma));

                    countReg++;
                    file.WriteLine(new TrailerArquivo(lote, countLote, countReg).ToString());

                    var remcons = new RemessaConsBLL();
                    foreach (var item in listaTitulos)
                    {
                        remcons.Get(item.id_remessa);
                        remcons.Processar();
                    }
                    var log = new LogSistemaBLL();
                    log.GravarLog("Arquivo de Lote GPS sem código de barras BB gerado com sucesso!");
                }

            }
            catch (Exception ex)
            {
                var log = new LogSistemaBLL();
                log.GravarLog(String.Format("Erro ao gerar arquivo Lote GPS sem código de barras BB! {0}", ex.Message));

            }
            file.Close();

        }

        public void GerarArquivo(LoteGRU lote)
        {
            string arq = String.Format("{0}{1}{2}_{3}{4}", pathRemessa, "GRU", lote.id_lote, lote.Conta.StrContaDigito, ".txt");
            int countReg = 0;
            int countLote = 0;
            int countGuias;
            decimal soma;


            var listaTitulos = lote.Guias.Where(it => it.id_tipo_ret != TipoRetornoBLL.Estornado & !it.id_tipo_conciliacao.HasValue).ToList();
            StreamWriter file = File.CreateText(arq);
            try
            {
                var ds = from l in listaTitulos
                         group l by l.FormaPagto
                             into g
                             select new
                             {
                                 g.Key,
                                 titulos = g
                             };

                foreach (var gr in ds.OrderBy(it => it.Key.codigo))
                {
                    countReg++;
                    file.WriteLine(new HeaderArquivo(lote).ToString());
                    countLote++;
                    countReg++;
                    file.WriteLine(new HeaderLote(lote, gr.Key.codigo, countLote).ToString());
                    soma = 0;
                    countGuias = 0;

                    foreach (var guia in listaTitulos)
                    {
                        countGuias++;
                        countReg++;
                        soma += guia.valor;
                        file.WriteLine(new SegmentoO(guia, countGuias, countLote).ToString());
                        countGuias++;
                        countReg++;
                        file.WriteLine(new SegmentoW(guia, countGuias, countLote).ToString());

                    }
                    countReg++;
                    file.WriteLine(new TrailerLote(lote, countLote, countGuias, soma));

                    countReg++;
                    file.WriteLine(new TrailerArquivo(lote, countLote, countReg).ToString());

                    var remcons = new RemessaConsBLL();
                    foreach (var item in listaTitulos)
                    {
                        remcons.Get(item.id_remessa);
                        remcons.Processar();
                    }
                    var log = new LogSistemaBLL();
                    log.GravarLog("Arquivo de Lote GRU gerado com sucesso!");
                }

            }
            catch (Exception ex)
            {
                var log = new LogSistemaBLL();
                log.GravarLog(String.Format("Erro ao gerar arquivo Lote GRU! {0}", ex.Message));

            }
            file.Close();

        }

        public static string LerArquivoGuias(string file, int id_arquivo)
        {
            string msg = "";
            string aut = "";
            var reader = new StreamReader(file);
            List<LinhaArquivo> l = new List<LinhaArquivo>();
            LinhaArquivo linha;
            var remConsBLL = new RemessaConsBLL();
            var tipoConciliacaoBLL = new TipoConciliacaoBLL();
            var remGruBLL = new RemessaGruBLL();
            string strLinha = "";
            try
            {
                while (!reader.EndOfStream)
                {
                    strLinha = reader.ReadLine();                   
                    linha = Cnab240BB.dicTipoLinha[TipoLinha.GetTipoLinha(strLinha)](strLinha);
                    l.Add(linha);
                }
                
                //verificar esse codigo para modificar o gps
                var header = l.OfType<HeaderArquivo>().FirstOrDefault();
                tipoConciliacaoBLL.Get(header.tipoConciliacao);
                foreach (var item in l.OfType<SegmentoO>())
                {
                    if (l[l.IndexOf(item) + 1] is SegmentoW)
                    {
                        remGruBLL.ObjEF = new RemessaGru();
                        var segO = (SegmentoO)l[l.IndexOf(item)];
                        remGruBLL.Get(Convert.ToInt32(segO.num_doc_empresa));
                        if (remGruBLL.RetornoEhValido(item))
                        {
                            var retorno = item.cod_ocorrencias.Trim().Left(2);
                            if (l[l.IndexOf(item) + 2] is SegmentoZ)
                            //if (tipoConciliacaoBLL.ObjEF.tem_autenticacao.GetValueOrDefault() & retorno == TipoRetornoBLL.CodConsolidado)
                            {
                                var segz = (SegmentoZ)l[l.IndexOf(item) + 2];
                                aut = segz.aut_bancaria;
                            }
                            else
                                aut = "";
                            remGruBLL.Conciliar(retorno, aut, header.tipoConciliacao, id_arquivo);
                        }
                    }
                    else
                    {
                        remConsBLL.ObjEF = new RemessaCons();
                        remConsBLL.Get(Convert.ToInt32(item.num_doc_empresa));
                        if (remConsBLL.RetornoEhValido(item))
                        {
                            var retorno = item.cod_ocorrencias.Trim().Left(2);
                            if (l[l.IndexOf(item) + 1] is SegmentoZ)
                            //if (tipoConciliacaoBLL.ObjEF.tem_autenticacao.GetValueOrDefault() & retorno == TipoRetornoBLL.CodConsolidado)
                            {
                                var segz = (SegmentoZ)l[l.IndexOf(item) + 1];
                                aut = segz.aut_bancaria;
                            }
                            else
                                aut = "";
                            remConsBLL.Conciliar(retorno, aut, header.tipoConciliacao, id_arquivo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = String.Format("Erro ao ler arquivo de Guias e Tributos BB! {0}", ex.Message);
            }
            reader.Close();
            return msg;
        }

        public static string LerArquivoGpsSemBarra(string file, int id_arquivo)
        {
            string msg = "";
            string aut = "";
            var reader = new StreamReader(file);
            List<LinhaArquivo> l = new List<LinhaArquivo>();
            LinhaArquivo linha;
            var remConsBLL = new RemessaGpsSemCodBarraBLL();
            var tipoConciliacaoBLL = new TipoConciliacaoBLL();
            string strLinha = "";
            try
            {
                while (!reader.EndOfStream)
                {
                    strLinha = reader.ReadLine();
                    linha = Cnab240BB.dicTipoLinha[TipoLinha.GetTipoLinha(strLinha)](strLinha);
                    l.Add(linha);
                }


                var header = l.OfType<HeaderArquivo>().FirstOrDefault();
                tipoConciliacaoBLL.Get(header.tipoConciliacao);
                foreach (var item in l.OfType<SegmentoN>())
                {
                    remConsBLL.ObjEF = new RemessaGpsSemCodBarra();
                    remConsBLL.Get(Convert.ToInt32(item.seu_numero));
                    if (remConsBLL.RetornoEhValido(item))
                    {
                        var retorno = item.cod_ocorrencias.Trim().Left(2);
                        if (l[l.IndexOf(item) + 1] is SegmentoZ)
                        //if (tipoConciliacaoBLL.ObjEF.tem_autenticacao.GetValueOrDefault() & retorno == TipoRetornoBLL.CodConsolidado)
                        {
                            var segz = (SegmentoZ)l[l.IndexOf(item) + 1];
                            aut = segz.aut_bancaria;
                        }
                        else
                            aut = "";
                        remConsBLL.Conciliar(retorno, aut, header.tipoConciliacao, id_arquivo);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = String.Format("Erro ao ler arquivo de GPS sem código de barras", ex.Message);
            }
            reader.Close();
            return msg;
        }

        public static string LerArquivoPagamentos(string file, int id_arquivo)
        {
            string msg = "";
            string aut = "";
            var reader = new StreamReader(file);
            List<LinhaArquivo> l = new List<LinhaArquivo>();
            LinhaArquivo linha;
            var remPagBLL = new RemessaPAGBLL();
            var tipoConciliacaoBLL = new TipoConciliacaoBLL();
            string strLinha = "";
            try
            {
                while (!reader.EndOfStream)
                {
                    strLinha = reader.ReadLine();
                    linha = Cnab240BB.dicTipoLinha[TipoLinha.GetTipoLinha(strLinha)](strLinha);
                    l.Add(linha);
                }

                var header = l.OfType<HeaderArquivo>().FirstOrDefault();
                tipoConciliacaoBLL.Get(header.tipoConciliacao);
                foreach (var item in l.OfType<SegmentoA>())
                {
                    remPagBLL.ObjEF = new RemessaPAG();
                    remPagBLL.Get(Convert.ToInt32(item.num_empresa));
                    if (remPagBLL.RetornoEhValido(item,(SegmentoB)l[l.IndexOf(item) + 1]))
                    {
                        var retorno = item.cod_ocorrencias.Trim().Left(2);
                        if (l[l.IndexOf(item) + 2] is SegmentoZ)
                        //if (tipoConciliacaoBLL.ObjEF.tem_autenticacao.GetValueOrDefault() & retorno == TipoRetornoBLL.CodConsolidado)
                        {
                            var segz = (SegmentoZ)l[l.IndexOf(item) + 2];
                            aut = segz.aut_bancaria;
                        }
                        else
                            aut = "";
                        remPagBLL.Conciliar(retorno, aut, header.tipoConciliacao, id_arquivo);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = String.Format("Erro ao ler arquivo de Pagamentos BB! {0}", ex.Message);
            }
            reader.Close();
            return msg;
        }
    }
}

 