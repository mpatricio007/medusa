using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class TipoFolhaPagtoBLL : AbstractCrudWithLog<TipoFolhaPagto>
    {

        public static Dictionary<string, Func<CarregarFolha,int, bool>> dicTipoFolha = new Dictionary<string, Func<CarregarFolha,int, bool>>()
        {
            {"A",  ImportacaoBLL.CriarDespesaAutono },
            {"B",  ImportacaoBLL.CriarDespesaBolsa },
            {"I",  ImportacaoBLL.CriarDespesaInovacao },
            {"L",  ImportacaoBLL.CriarDespesaAluguel },
            {"P",  ImportacaoBLL.CriarDespesaPremio },            
            {"J",  ImportacaoBLL.CriarDespesaServTerc },      
            {"N", ImportacaoBLL.CriarDespesaBolsa }
        };

        public bool Exists()
        {
            return ObjEF.id_tipo_folha_pagto != 0;
        }
        public void Get(string strSigla)
        {
            ObjEF = _dbSet.Where(it => it.sigla == strSigla).FirstOrDefault();
        }
    }
}
