using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data.Entity;
using System.Data;

namespace Medusa.BLL
{
    public class ImportacaoBLL : AbstractCrudWithLog<Importacao>
    {

        public bool ImportaFolhaPagto(ref string strMsg)
        {            
            bool rt = false;
            StringBuilder txt = new StringBuilder();
            
                try
                {

                    ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
                    ObjEF.data_execucao = DateTime.Now;
                    Add();
                    SaveChanges();                                
                    var lstCarregarFolha = _dbContext.CarregarFolha(ObjEF.data_folha, ObjEF.id_tipo_folha_pagto).ToList();

                    var semprojs = lstCarregarFolha.Where(it => !it.id_projeto.HasValue).Select(it => it.proj).OrderBy(it => Convert.ToInt32(it)).Distinct();
                    if (semprojs.Count() > 0)
                    {
                        txt.AppendLine(" Cadastrar projeto(s):");
                        foreach (var p in semprojs)
                        {
                            txt.AppendLine(p);
                        }
                        strMsg = txt.ToString();
                       
                    }
                    else
                    {
                        var sembancos = lstCarregarFolha.Where(it => !it.id_banco.HasValue & it.id_tipo_folha_pagto!=7 /* Scpr tipo imp = 7 */ ).Select(it => it.bank);
                        if (sembancos.Count() > 0) 
                        {
                            txt.AppendLine(" Cadastrar bancos(s):");
                            foreach (var p in sembancos)
                            {
                                txt.AppendLine(p);
                            }
                            strMsg = txt.ToString();
                        }

                        else
                        {
                            foreach (var pgto in lstCarregarFolha.OrderBy(it => it.Projeto.codigo))
                            {                               
                                rt = TipoFolhaPagtoBLL.dicTipoFolha[pgto.TipoFolha.sigla](pgto, ObjEF.id_importacao);
                                if (!rt)
                                {
                                    strMsg = String.Format("Erro ao importar Folha de Pagamentos!");
                                    break;
                                }
                            }
                            
                            strMsg = String.Format("Folha de Pagamentos importada com sucesso! Total Bruto: {0:n2} ", lstCarregarFolha.Sum(it => it.valorbruto));
                        }
                    }
                }
                catch (Exception ex)
                {
                    rt = false;
                    strMsg = String.Format("Erro ao importar Folha de Pagamentos! {0}", ex.Message);
                }
                if (!rt)
                    ExcluirImportacaoFolha();
            return rt;
        }

        public static bool CriarDespesaAutono(CarregarFolha pgto, int id_imp)
        {
            string msg = "";
            bool rt = false;
           
            var dpBLL = new DespesaAutonomoBLL();
            dpBLL.ObjEF.id_projeto = pgto.id_projeto.GetValueOrDefault();
            dpBLL.ObjEF.valor = pgto.valorbruto;
            dpBLL.ObjEF.data_pagto = pgto.data_pgto;
            dpBLL.ObjEF.deducaoINSS = pgto.deducaoINSS;
            dpBLL.ObjEF.iss = pgto.iss > 0;
                
            dpBLL.ObjEF.cpf = pgto.cpf_cnpj;
            dpBLL.ObjEF.nome = pgto.nome;
            dpBLL.ObjEF.agencia = pgto.agencia;
            dpBLL.ObjEF.digito_agencia = pgto.digito_agencia;
            dpBLL.ObjEF.conta = pgto.conta;
            dpBLL.ObjEF.digito_conta = pgto.digito_conta;
            dpBLL.ObjEF.id_banco = pgto.id_banco.GetValueOrDefault();
            dpBLL.ObjEF.num_dependentes = pgto.ndeppes;
            dpBLL.ObjEF.id_imp = id_imp;
            dpBLL.ObjEF.codpf = pgto.codpf;
            dpBLL.ObjEF.rp = pgto.rdpf;
            msg = dpBLL.Agendar(ref rt);       
         
            if(!rt)                     
                throw new System.ArgumentException();
           
            return rt;
        }

        public static bool CriarDespesaInovacao(CarregarFolha pgto, int id_imp)
        {
            bool rt = false;
            string msg = "";
           
            var dpBLL = new DespesaInovacaoBLL();
            dpBLL.ObjEF.id_projeto = pgto.id_projeto.GetValueOrDefault();
            dpBLL.ObjEF.valor = pgto.valorbruto;
            dpBLL.ObjEF.data_pagto = pgto.data_pgto;
            dpBLL.ObjEF.iss = pgto.iss > 0;

            dpBLL.ObjEF.cpf = pgto.cpf_cnpj;
            dpBLL.ObjEF.nome = pgto.nome;
            dpBLL.ObjEF.agencia = pgto.agencia;
            dpBLL.ObjEF.digito_agencia = pgto.digito_agencia;
            dpBLL.ObjEF.conta = pgto.conta;
            dpBLL.ObjEF.digito_conta = pgto.digito_conta;
            dpBLL.ObjEF.id_banco = pgto.id_banco.GetValueOrDefault();
            dpBLL.ObjEF.num_dependentes = pgto.ndeppes;
            dpBLL.ObjEF.id_imp = id_imp;
            dpBLL.ObjEF.codpf = pgto.codpf;
            dpBLL.ObjEF.rp = pgto.rdpf;
            msg = dpBLL.Agendar(ref rt);

            if (!rt)
                throw new System.ArgumentException();
         
            return rt;
        }



        public static bool CriarDespesaBolsa(CarregarFolha pgto, int id_imp)
        {
            bool rt = false;
            string msg = "";

            var dpBLL = new DespesaBolsaBLL();
            dpBLL.ObjEF.id_projeto = pgto.id_projeto.GetValueOrDefault();
            dpBLL.ObjEF.valor = pgto.valorbruto;
            dpBLL.ObjEF.data_pagto = pgto.data_pgto;

            dpBLL.ObjEF.cpf = pgto.cpf_cnpj;
            dpBLL.ObjEF.nome = pgto.nome;
            dpBLL.ObjEF.agencia = pgto.agencia;
            dpBLL.ObjEF.digito_agencia = pgto.digito_agencia;
            dpBLL.ObjEF.conta = pgto.conta;
            dpBLL.ObjEF.digito_conta = pgto.digito_conta;
            dpBLL.ObjEF.id_banco = pgto.id_banco.GetValueOrDefault();
            dpBLL.ObjEF.num_dependentes = pgto.ndeppes;
            dpBLL.ObjEF.id_imp = id_imp;
            dpBLL.ObjEF.codpf = pgto.codpf;
            dpBLL.ObjEF.rp = pgto.rdpf;
            msg = dpBLL.Agendar(ref rt);
            if (!rt)
                throw new System.ArgumentException();
            return rt;
        }

        public static bool CriarDespesaAluguel(CarregarFolha pgto, int id_imp)
        {
            bool rt = false;
            string msg = "";

            var dpBLL = new DespesaAluguelBLL();
            dpBLL.ObjEF.id_projeto = pgto.id_projeto.GetValueOrDefault();
            dpBLL.ObjEF.valor = pgto.valorbruto;
            dpBLL.ObjEF.data_pagto = pgto.data_pgto;

            dpBLL.ObjEF.cpf = pgto.cpf_cnpj;
            dpBLL.ObjEF.nome = pgto.nome;
            dpBLL.ObjEF.agencia = pgto.agencia;
            dpBLL.ObjEF.digito_agencia = pgto.digito_agencia;
            dpBLL.ObjEF.conta = pgto.conta;
            dpBLL.ObjEF.digito_conta = pgto.digito_conta;
            dpBLL.ObjEF.id_banco = pgto.id_banco.GetValueOrDefault();
            dpBLL.ObjEF.num_dependentes = pgto.ndeppes;
            dpBLL.ObjEF.id_imp = id_imp;
            dpBLL.ObjEF.codpf = pgto.codpf;
            dpBLL.ObjEF.rp = pgto.rdpf;
            msg = dpBLL.Agendar(ref rt);
            if (!rt)
                throw new System.ArgumentException();
            return rt;
        }


        public static bool CriarDespesaPremio(CarregarFolha pgto, int id_imp)
        {
            bool rt = false;
            string msg = "";

            var dpBLL = new DespesaPremioBLL();
            dpBLL.ObjEF.id_projeto = pgto.id_projeto.GetValueOrDefault();
            dpBLL.ObjEF.valor = pgto.valorbruto;
            dpBLL.ObjEF.data_pagto = pgto.data_pgto;
            dpBLL.ObjEF.cpf = pgto.cpf_cnpj;
            dpBLL.ObjEF.nome = pgto.nome;
            dpBLL.ObjEF.agencia = pgto.agencia;
            dpBLL.ObjEF.digito_agencia = pgto.digito_agencia;
            dpBLL.ObjEF.conta = pgto.conta;
            dpBLL.ObjEF.digito_conta = pgto.digito_conta;
            dpBLL.ObjEF.id_banco = pgto.id_banco.GetValueOrDefault();
            dpBLL.ObjEF.num_dependentes = pgto.ndeppes;
            dpBLL.ObjEF.id_imp = id_imp;
            dpBLL.ObjEF.codpf = pgto.codpf;
            dpBLL.ObjEF.rp = pgto.rdpf;
            msg = dpBLL.Agendar(ref rt);
            if (!rt)
                throw new System.ArgumentException();
            return rt;
        }

        public static bool CriarDespesaServTerc(CarregarFolha pgto, int id_imp)
        {
            bool rt = false;
            string msg = "";

            var dpBLL = new DespesaServTercPJBLL();
            dpBLL.ObjEF.id_projeto = pgto.id_projeto.GetValueOrDefault();
            dpBLL.ObjEF.valor = pgto.valorbruto;
            dpBLL.ObjEF.data_pagto = pgto.data_pgto;
            dpBLL.ObjEF.cnpj = pgto.cpf_cnpj;
            dpBLL.ObjEF.nome = pgto.nome;
            dpBLL.ObjEF.agencia = pgto.agencia.somenteNumeros();
            dpBLL.ObjEF.digito_agencia = pgto.digito_agencia;
            dpBLL.ObjEF.conta = pgto.conta.somenteNumeros();
            dpBLL.ObjEF.digito_conta = pgto.digito_conta;
            dpBLL.ObjEF.id_banco = pgto.id_banco.GetValueOrDefault();
            dpBLL.ObjEF.id_imp = id_imp;
            dpBLL.ObjEF.codlan = pgto.codpf;
            dpBLL.ObjEF.codBarra = pgto.codbarra;
            dpBLL.ObjEF.rp = pgto.rdpf;
            msg = dpBLL.Agendar(ref rt);
            if (!rt)
                throw new System.ArgumentException();
            return rt;
        }


        public void ExcluirImportacaoFolha()
        {            
            Delete();
            SaveChanges();
        }
    }
}
