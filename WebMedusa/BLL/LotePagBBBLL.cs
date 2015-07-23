using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;
using Medusa.LIB;
using System.Data.OleDb;
using System.Text;

namespace Medusa.BLL
{
    public class LotePagBBBLL : LoteBLL<LotePagBB>
    {
        public LotePagBBBLL()
        {
        }

        public LotePagBBBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
            this._dbSet = _dbContext.LotePagBBs;
        }

        public override bool GerarArquivo()
        {
            Cnab240BB c = new Cnab240BB();
            c.GerarArquivo(ObjEF);
            ObjEF.data_envio = DateTime.Now;
            Update();
            return SaveChanges();
        }

        public void ImportaFolhaPagtoAntiga(DateTime dt, int id_tipo_folha, ref string strMsg)
        {   
            try
            {
                var remPagBLL = new RemessaPAGBLL(_dbContext);
                var lstCarregarFolha = new List<CarregarFolhaAntiga>();
                var lstCarregarFolhaNova = new List<CarregarFolhaBolsaNova>();
                var tipoFolhaBLL = new TipoFolhaPagtoBLL();
                tipoFolhaBLL.Get(id_tipo_folha);

                if (tipoFolhaBLL.Exists())
                {
                    lstCarregarFolha = _dbContext.CarregarFolhaAntiga.Where(u => u.datapf == dt & u.tipofol == tipoFolhaBLL.ObjEF.sigla).OrderBy(k => k.tipofol).ThenBy(l => l.ctaproj).ThenBy(m => m.Codproj).ThenBy(n => n.nomepes).ThenBy(o => o.datapf).ToList();

                    if (tipoFolhaBLL.ObjEF.id_tipo_folha_pagto == 6)
                        lstCarregarFolhaNova = _dbContext.CarregarFolhaBolsaNova(dt).OrderBy(it => it.projeto).ThenBy(it => it.nome_bolsista).ToList();
                }
                else
                {
                    lstCarregarFolha = _dbContext.CarregarFolhaAntiga.Where(u => u.datapf == dt).OrderBy(k => k.tipofol).ThenBy(l => l.ctaproj).ThenBy(m => m.Codproj).ThenBy(n => n.nomepes).ThenBy(o => o.datapf).ToList();
                    lstCarregarFolhaNova = _dbContext.CarregarFolhaBolsaNova(dt).OrderBy(it => it.projeto).ThenBy(it => it.nome_bolsista).ToList();
                }

                var contaProj = "0";

                Projeto projeto = new Projeto();


                var ds = from l in lstCarregarFolha
                         group l by new { l.datapf, l.ctaproj, l.tipofol }
                             into g
                             select new
                                 {
                                     g.Key,
                                     pagtos = g
                                 };

                foreach (var l in ds)
                {
                    var c = l.Key;
                    string conta = Util.TirarFormatoConta(c.ctaproj);
                    var cont = _dbContext.Contas.Where(t => t.numero == conta & t.BancoAgencia.Banco.codigo == "001" & t.status == true).FirstOrDefault();
                    
                    if(cont == null)
                        throw new Exception(String.Format("Cadastrar conta {0}!", conta));

                    var lote = new LotePagBB();
                    lote.Conta = cont;
                    lote.data_pgto = c.datapf;
                    lote.data_processamento = DateTime.Now;
                    contaProj = c.ctaproj;
                    tipoFolhaBLL.Get(c.tipofol);
                    lote.descricao = String.Format("{0} {1:d}", tipoFolhaBLL.ObjEF.nome, dt);
                    foreach (var pagto in l.pagtos.OrderBy(it => it.descricao).ThenBy(it => it.Codproj))
                    {
                        if (projeto.codigo != pagto.Codproj)
                        {
                            projeto = _dbContext.Projetos.Where(it => it.codigo == pagto.Codproj).FirstOrDefault();

                            if (projeto == null)
                                throw new Exception(String.Format("Cadastrar projeto {0}!", pagto.Codproj));
                            
                        }

                        remPagBLL.Agendar(lote,
                               pagto.nomepes,
                               pagto.cpfpes,
                               pagto.bcopes,
                               pagto.agenpes,
                               pagto.digag,
                               pagto.ctapes,
                               pagto.digcta,
                               pagto.valorpf,
                               pagto.tipoctapes == "01",
                               projeto.id_projeto,
                               pagto.descricao);
                    }
                    _dbSet.Add(lote);
                    _dbContext.SaveChanges();
                }

               
                var ds2 = from l in lstCarregarFolhaNova
                          join proj in _dbContext.Projetos on l.projeto equals proj.codigo
                          let conta = new ContaBLL(_dbContext).GetAllAg1897x(proj.id_projeto).FirstOrDefault()
                          group l by conta
                              into g
                              select new
                              {
                                  conta = g.Key,
                                  pagtos = g,
                              };

                tipoFolhaBLL.Get(6);
                
                foreach (var lot in ds2)
                {
                    var lote = new LotePagBB();

                    lote.Conta = lot.conta;
                    lote.data_pgto = dt;
                    lote.data_processamento = DateTime.Now;
                    lote.descricao = String.Format("{0} {1:d}", tipoFolhaBLL.ObjEF.nome, dt);
                    
                    foreach (var pagto in lot.pagtos)
                    {
                        if (projeto.codigo != pagto.projeto)
                        {
                            projeto = _dbContext.Projetos.Where(it => it.codigo == pagto.projeto).FirstOrDefault();

                            if (projeto == null)
                                throw new Exception(String.Format("Cadastrar projeto {0}!", pagto.projeto));
                            
                        }
                        
                        remPagBLL.Agendar(lote,
                               pagto.nome_bolsista,
                               pagto.cpf_bolsista,
                               pagto.banco,
                               pagto.agencia,
                               pagto.digagencia,
                               pagto.conta,
                               pagto.digcta,
                               pagto.valor,
                               true,
                               projeto.id_projeto,
                               pagto.bolsa);
                    }

                    _dbSet.Add(lote);
                    _dbContext.SaveChanges();

                }
                CriarDBF(lstCarregarFolhaNova, dt);
                strMsg = "Folha de Pagamentos importada com sucesso!";

            }
            catch(Exception ex)
            {                
                strMsg = String.Format("Erro ao importar Folha de Pagamentos! {0}", ex.Message);
            }           
        }


        public void CriarDBF(List<CarregarFolhaBolsaNova> listaFbn, DateTime dt)
        {
            try
            {
                //Microsoft.Jet.OLEDB.4.0

                OleDbConnection oConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\bancobrasil;Extended Properties=dBASE IV");
                oConn.Open();

                OleDbCommand cmd = new OleDbCommand("DELETE FROM CMM.DBF");
                cmd.Connection = oConn;
                cmd.ExecuteNonQuery();

                foreach (var item in listaFbn)
                {

                    StringBuilder commandSQL = new StringBuilder();
                    commandSQL.Append("INSERT INTO CMM(RD,PROJETO,ITEM,ISS,CALCINSS,PENSAO,VALINSS,INSS11,VALIR,DATA,HISTORICO,VALPAGO,VALCC,CPMF) VALUES(@RD,@PROJETO,@ITEM,@ISS,@CALCINSS,@PENSAO,@VALINSS,@INSS11,@VALIR,@DATA,@HISTORICO,@VALPAGO,@VALCC,@CPMF) ");
                    OleDbCommand cmdInsert = new OleDbCommand();
                    cmdInsert.Connection = oConn;
                    cmdInsert.CommandText = commandSQL.ToString();
                    cmdInsert.Parameters.AddWithValue("@RD", "RP SN");
                    cmdInsert.Parameters.AddWithValue("@PROJETO", item.projeto);
                    cmdInsert.Parameters.AddWithValue("@ITEM", 120030);
                    cmdInsert.Parameters.AddWithValue("@ISS", 0);
                    cmdInsert.Parameters.AddWithValue("@CALCINSS", "N");
                    cmdInsert.Parameters.AddWithValue("@PENSAO", 0);
                    cmdInsert.Parameters.AddWithValue("@VALINSS", 0);
                    cmdInsert.Parameters.AddWithValue("@INSS11", 0);
                    cmdInsert.Parameters.AddWithValue("@VALIR", 0);
                    cmdInsert.Parameters.AddWithValue("@DATA", dt);
                    cmdInsert.Parameters.AddWithValue("@HISTORICO", item.nome_bolsista);
                    cmdInsert.Parameters.AddWithValue("@VALPAGO", Util.DecimalToString(item.valor));
                    cmdInsert.Parameters.AddWithValue("@VALCC", Util.DecimalToString(item.valor));
                    cmdInsert.Parameters.AddWithValue("@CPMF", "S");
                    cmdInsert.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                var log = new LogSistemaBLL();
                log.ObjEF.entidade = "CriarDBF";
                log.ObjEF.descricao = ex.Message;
                log.Add();
                log.SaveChanges();
            }
        }


    }
}