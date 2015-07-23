using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class RemessaPAGBLL : RemessaBLL<RemessaPAG>
    {
        
        public void GetAllPorInsc(string insc)
        {
            ObjEF = _dbSet.Where(it => it.inscricao == insc).OrderByDescending(it => it.id_remessa).FirstOrDefault();
        }

        public bool RetornoEhValido(SegmentoA segA,SegmentoB segB)
        {
            bool rt = Exists();
            if (rt)
            {
                rt = ObjEF.valor == Util.StringSemPontosToDecimal(segA.valor_debito)
                    & (ObjEF.tipoInscr == TipoInscricao.CPF ? ObjEF.cpf.Value == segB.num_inscricao.Right(11) :
                        ObjEF.cnpj.Value == segB.num_inscricao);
                    
            }
            return rt;
        }

        public void Agendar(LotePagBB lote, string nome, string cpf, string codBanco, string agencia, string digag, string conta, string digcta, decimal valor, bool ehContaCorrente,int id_projeto,string descricao)
        {
            var pag = new RemessaPAG();            
            //pag.Lote = lote;
            pag.nome_fav_cedente = nome;
            pag.inscricao = cpf;
            pag.tipoInscr = TipoInscricao.CPF;
            pag.valor = valor;
            pag.id_banco = BancoBLL.RetornaIdpeloCodigo(codBanco);
            int traco = agencia.IndexOf("-");
            if (traco != -1)
            {
                pag.agencia = agencia.Substring(0, traco);
                pag.digito_agencia = digag;
            }
            else
            {
                pag.agencia = agencia;
                pag.digito_agencia = "0";
            }
            pag.conta = conta.somenteNumeros();
            pag.digito_conta = digcta;
            pag.descricao = descricao;

            var formaPagtoBLL = new FormaPagtoBLL();
            FormaPagto fp = formaPagtoBLL.GetFormaPagtoLotePagBB(lote.Conta.BancoAgencia.Banco.id_banco,
                lote.Conta.BancoAgencia.Banco.id_banco == pag.id_banco,
                pag.valor,ehContaCorrente);
            pag.id_forma_pagto = fp.id_forma_pagto;
            pag.id_projeto = id_projeto;
            
            pag.id_tipo_ret = TipoRetornoBLL.Agendado;
            lote.Remessas.Add(pag);
        }

        public RemessaPAGBLL()
        {
            // TODO: Complete member initialization
        }

        public RemessaPAGBLL(Contexto ctx)
        {
            _dbContext = ctx;
            _dbSet = _dbContext.RemessaPAGs;
        }

       
    }
}
