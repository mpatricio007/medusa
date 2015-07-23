using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class ProcessamentoDespesaBLL : AbstractCrudWithLog<ProcessamentoDespesa>
    {

        public static Dictionary<int, Func<int,DateTime,string, int>> dicLote = new Dictionary<int, Func<int,DateTime,string, int>>()
        {
            {1,  CriarLotePag },
            {2,  CriarLoteBoleto },
            {3,  CriarLoteConsumo },
        };


        public static Dictionary<string, Func<Despesa, int, bool>> dicDespesa = new Dictionary<string, Func<Despesa, int, bool>>()
        {
            {"11",  DespesaAutonomoRemessaPgto },
            {"32",  DespesaConsumoRemessaConsumo },
            {"13",  DespesaBolsaRemessaPgto },
            {"14",  DespesaInovacaoRemessaPgto },
            {"15",  DespesaAluguelRemessaPgto },
            {"16",  DespesaPremioRemessaPgto },
            {"38",  DespesaDAMSPRemessaConsumo },
            {"311",  DespesaGRFRemessaConsumo },
            {"312",  DespesaGPSRemessaConsumo },
            {"313",  DespesaDARFRemessaConsumo },
            {"314",  DespesaGRFRRemessaConsumo },
            {"29",  DespesaBoletoRemessaTit },
            {"110",  DespesaSCPRRemessaPagto },
            {"210",  DespesaSCPRRemessaTit },
        };

        public bool ProcessarDespesas(ref string strMsg)
        {
            bool rt = false;
            StringBuilder txt = new StringBuilder();

            try
            {

                ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
                ObjEF.data_execucao = DateTime.Now;
                Add();
                SaveChanges();
                Detach();
                Get(ObjEF.id_precessamento_despesa);

                ContaBLL c = new ContaBLL();
                var lstDespesasprojetos = _dbContext.Despesas.Where(it => it.data_pagto == ObjEF.data_despesa & it.id_lancto_tipo == ObjEF.id_lancto_tipo).GroupBy(it => new {it.id_projeto, it.id_forma }).ToList();
                                

                var lstDespesas = new List<ContaDespesa>();

                foreach (var item in lstDespesasprojetos)
                    lstDespesas.Add(new ContaDespesa() { conta = c.GetAllAg1897x(item.Key.id_projeto).FirstOrDefault(), despesas = item.ToList(), id_forma = (int)item.Key.id_forma });

                int id_conta = 0;
                int id_lote = 0;
                int id_forma = 0;
                foreach (var loteDespesas in lstDespesas.OrderBy(it => it.conta.numero))
                {
                    if ((id_conta != loteDespesas.conta.id_conta) || (id_forma != loteDespesas.id_forma))
                    {
                        id_conta =loteDespesas.conta.id_conta;
                        id_forma = loteDespesas.id_forma;
                        id_lote = dicLote[loteDespesas.id_forma](id_conta, ObjEF.data_despesa, ObjEF.LancamentoTipo.nome);
                    }

                    foreach (var item in loteDespesas.despesas.OrderBy(it => it.Projeto.codigo))
	                {
                       
                      rt = dicDespesa[String.Format("{0}{1}",loteDespesas.id_forma, ObjEF.id_lancto_tipo)](item,id_lote);
                      if (!rt)
                          break;
                    }
                    
                    if (!rt)
                    {
                        strMsg = String.Format("Erro ao processar despesa!{0}",strMsg);                       
                        Delete();
                        return rt;
                    }
                }
                rt = true;
                strMsg = String.Format("Processamento de {0} realizado com sucesso!", ObjEF.LancamentoTipo.nome);
                   
            }
            catch (Exception ex)
            {
                strMsg = String.Format("Erro ao processar Despesas de {1}! {0}", ex.Message, ObjEF.LancamentoTipo.nome);
            }
            if (!rt)
                ExcluirRemessasGeradas();
            return rt;
        }

        public static int CriarLotePag(int id_conta, DateTime data, string tipoDespesa)
        {
            int id_lote = 0;
            try
            {
                var lote = new LotePagBBBLL();
                lote.ObjEF = new LotePagBB();
                lote.ObjEF.data_pgto = data;
                lote.ObjEF.id_conta = id_conta;
                lote.ObjEF.descricao = String.Format("Processamento de {0}", tipoDespesa);
                lote.Add();
                if(lote.SaveChanges()) 
                    id_lote = lote.ObjEF.id_lote;
                
            }
            catch (Exception)
            {


            }
            return id_lote;
        }

        public static int CriarLoteBoleto(int id_conta, DateTime data, string tipoDespesa)
        {
            int id_lote = 0;
            try
            {
                var lote = new LoteBoletoBLL();
                lote.ObjEF = new LoteBoleto();
                lote.ObjEF.data_pgto = data;
                lote.ObjEF.id_conta = id_conta;
                lote.ObjEF.descricao = String.Format("Processamento de {0}", tipoDespesa);
                lote.Add();
                if (lote.SaveChanges())
                    id_lote = lote.ObjEF.id_lote;

            }
            catch (Exception)
            {


            }
            return id_lote;
        }

        public static int CriarLoteConsumo(int id_conta, DateTime data, string tipoDespesa)
        {
            int id_lote = 0;
            try
            {
                var lote = new LoteConsBLL();
                lote.ObjEF = new LoteCons();
                lote.ObjEF.data_pgto = data;
                lote.ObjEF.id_conta = id_conta;
                lote.ObjEF.descricao = String.Format("Processamento de {0}", tipoDespesa);
                lote.Add();
                if (lote.SaveChanges())
                    id_lote = lote.ObjEF.id_lote;

            }
            catch (Exception)
            {


            }
            return id_lote;
        }

        public static bool DespesaAutonomoRemessaPgto(Despesa desp, int id_lote)
        {            
            bool rt = false;
            var rem = new RemessaPAGBLL();
            var dpBLL = new DespesaAutonomoBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaPAG();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.nome;
            rem.ObjEF.valor = dpBLL.TotalLiquidoBeneficiario();
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "AUTONOMO";
            rem.ObjEF.id_banco = dpBLL.ObjEF.id_banco;
            rem.ObjEF.tipoInscr = TipoInscricao.CPF;
            rem.ObjEF.inscricao = dpBLL.ObjEF.cpf;
            rem.ObjEF.agencia = dpBLL.ObjEF.agencia;
            rem.ObjEF.digito_agencia = dpBLL.ObjEF.digito_agencia;
            rem.ObjEF.conta = dpBLL.ObjEF.conta;
            rem.ObjEF.digito_conta = dpBLL.ObjEF.digito_conta;
            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaBolsaRemessaPgto(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaPAGBLL();
            var dpBLL = new DespesaBolsaBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaPAG();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.nome;
            rem.ObjEF.valor = dpBLL.TotalLiquidoBeneficiario();
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "BOLSA";
            rem.ObjEF.id_banco = dpBLL.ObjEF.id_banco;
            rem.ObjEF.tipoInscr = TipoInscricao.CPF;
            rem.ObjEF.inscricao = dpBLL.ObjEF.cpf;
            rem.ObjEF.agencia = dpBLL.ObjEF.agencia;
            rem.ObjEF.digito_agencia = dpBLL.ObjEF.digito_agencia;
            rem.ObjEF.conta = dpBLL.ObjEF.conta;
            rem.ObjEF.digito_conta = dpBLL.ObjEF.digito_conta;
            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaInovacaoRemessaPgto(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaPAGBLL();
            var dpBLL = new DespesaInovacaoBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaPAG();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.nome;
            rem.ObjEF.valor = dpBLL.TotalLiquidoBeneficiario();
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "INOVAÇÃO";
            rem.ObjEF.id_banco = dpBLL.ObjEF.id_banco;
            rem.ObjEF.tipoInscr = TipoInscricao.CPF;
            rem.ObjEF.inscricao = dpBLL.ObjEF.cpf;
            rem.ObjEF.agencia = dpBLL.ObjEF.agencia;
            rem.ObjEF.digito_agencia = dpBLL.ObjEF.digito_agencia;
            rem.ObjEF.conta = dpBLL.ObjEF.conta;
            rem.ObjEF.digito_conta = dpBLL.ObjEF.digito_conta;
            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaAluguelRemessaPgto(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaPAGBLL();
            var dpBLL = new DespesaAluguelBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaPAG();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.nome;
            rem.ObjEF.valor = dpBLL.TotalLiquidoBeneficiario();
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "ALUGUEL";
            rem.ObjEF.id_banco = dpBLL.ObjEF.id_banco;
            rem.ObjEF.tipoInscr = TipoInscricao.CPF;
            rem.ObjEF.inscricao = dpBLL.ObjEF.cpf;
            rem.ObjEF.agencia = dpBLL.ObjEF.agencia;
            rem.ObjEF.digito_agencia = dpBLL.ObjEF.digito_agencia;
            rem.ObjEF.conta = dpBLL.ObjEF.conta;
            rem.ObjEF.digito_conta = dpBLL.ObjEF.digito_conta;
            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaPremioRemessaPgto(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaPAGBLL();
            var dpBLL = new DespesaPremioBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaPAG();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.nome;
            rem.ObjEF.valor = dpBLL.TotalLiquidoBeneficiario();
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "PRÊMIO";
            rem.ObjEF.id_banco = dpBLL.ObjEF.id_banco;
            rem.ObjEF.tipoInscr = TipoInscricao.CPF;
            rem.ObjEF.inscricao = dpBLL.ObjEF.cpf;
            rem.ObjEF.agencia = dpBLL.ObjEF.agencia;
            rem.ObjEF.digito_agencia = dpBLL.ObjEF.digito_agencia;
            rem.ObjEF.conta = dpBLL.ObjEF.conta;
            rem.ObjEF.digito_conta = dpBLL.ObjEF.digito_conta;
            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaConsumoRemessaConsumo(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaConsBLL();
            var dpBLL = new DespesaConsumoBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaCons();               
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.cedente;
            rem.ObjEF.valor = dpBLL.ObjEF.valor;
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "DESPESA CONSUMO";
            rem.ObjEF.Guia = dpBLL.ObjEF.Guia; 
            rem.ObjEF.dataVencto = dpBLL.ObjEF.dataVencto;

            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaDAMSPRemessaConsumo(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaConsBLL();
            var dpBLL = new DespesaDAMSPBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaCons();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.cedente;
            rem.ObjEF.valor = dpBLL.ObjEF.valor;
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "DAMSP";
            rem.ObjEF.Guia = dpBLL.ObjEF.Guia;
            rem.ObjEF.dataVencto = dpBLL.ObjEF.dataVencto;

            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaGRFRemessaConsumo(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaConsBLL();
            var dpBLL = new DespesaGRFBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaCons();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.cedente;
            rem.ObjEF.valor = dpBLL.ObjEF.valor;
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "GRF";
            rem.ObjEF.Guia = dpBLL.ObjEF.Guia;
            rem.ObjEF.dataVencto = dpBLL.ObjEF.dataVencto;

            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaGPSRemessaConsumo(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaConsBLL();
            var dpBLL = new DespesaGPSBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaCons();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.cedente;
            rem.ObjEF.valor = dpBLL.ObjEF.valor;
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "GPS";
            rem.ObjEF.Guia = dpBLL.ObjEF.Guia;
            rem.ObjEF.dataVencto = dpBLL.ObjEF.dataVencto;

            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaDARFRemessaConsumo(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaConsBLL();
            var dpBLL = new DespesaDARFBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaCons();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.cedente;
            rem.ObjEF.valor = dpBLL.ObjEF.valor;
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "DARF";
            rem.ObjEF.Guia = dpBLL.ObjEF.Guia;
            rem.ObjEF.dataVencto = dpBLL.ObjEF.dataVencto;

            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaGRFRRemessaConsumo(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaConsBLL();
            var dpBLL = new DespesaGRFRBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaCons();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.cedente;
            rem.ObjEF.valor = dpBLL.ObjEF.valor;
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "GRFR";
            rem.ObjEF.Guia = dpBLL.ObjEF.Guia;
            rem.ObjEF.dataVencto = dpBLL.ObjEF.dataVencto;

            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaBoletoRemessaTit(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaTitBLL();
            var dpBLL = new DespesaBoletoBLL();
          

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaTit();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.cedente;
            rem.ObjEF.valor = dpBLL.ObjEF.valor;
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "GRFR";
            rem.ObjEF.Boleto = dpBLL.ObjEF.Boleto;
            rem.ObjEF.dataVencto = dpBLL.ObjEF.dataVencto;

            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaSCPRRemessaTit(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaTitBLL();
            var dpBLL = new DespesaServTercPJBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaTit();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.nome;
            rem.ObjEF.valor = dpBLL.TotalLiquidoBeneficiario();
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "Serviço de Terceiros";
            var codBarra = new LIB.CodigoBarrasBoleto(dpBLL.ObjEF.codBarra);
            rem.ObjEF.Boleto = codBarra;
            rem.ObjEF.dataVencto = dpBLL.ObjEF.data_pagto;
            rem.ObjEF.id_banco_destino = codBarra.Id_banco();
            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public static bool DespesaSCPRRemessaPagto(Despesa desp, int id_lote)
        {
            bool rt = false;
            var rem = new RemessaPAGBLL();
            var dpBLL = new DespesaServTercPJBLL();
            var formaPagtoBLL = new FormaPagtoBLL();

            dpBLL.Get(desp.id_lancto);
            rem.ObjEF = new RemessaPAG();
            rem.ObjEF.id_lote = id_lote;
            rem.ObjEF.nome_fav_cedente = dpBLL.ObjEF.nome;
            rem.ObjEF.valor = dpBLL.TotalLiquidoBeneficiario();
            rem.ObjEF.id_projeto = dpBLL.ObjEF.id_projeto;
            rem.ObjEF.descricao = "Serviço de Terceiros";
            rem.ObjEF.id_banco = dpBLL.ObjEF.id_banco;
            rem.ObjEF.tipoInscr = TipoInscricao.CNPJ;
            rem.ObjEF.inscricao = dpBLL.ObjEF.cnpj;
            rem.ObjEF.agencia = dpBLL.ObjEF.agencia;
            rem.ObjEF.digito_agencia = dpBLL.ObjEF.digito_agencia;
            rem.ObjEF.conta = dpBLL.ObjEF.conta;
            rem.ObjEF.digito_conta = dpBLL.ObjEF.digito_conta;
            rt = rem.Agendar();

            if (rt)
            {
                dpBLL.ObjEF.id_remessa = rem.ObjEF.id_remessa;
                dpBLL.Update();
                dpBLL.SaveChanges();
            }
            return rt;

        }

        public void ExcluirRemessasGeradas()
        {
            Delete();
            SaveChanges();
        }
    }

    class ContaDespesa
    {
        public int id_forma { get; set; }

        public Conta conta { get; set; }

        public List<Despesa> despesas { get; set; }
    }
}
