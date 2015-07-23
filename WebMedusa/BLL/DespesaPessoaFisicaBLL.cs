using Medusa.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class DespesaPessoaFisicaBLL<T> : DespesaBLL<T> where T : DespesaPessoaFisica,new()
    {
        public virtual bool temIss { get; set; }

        public bool temInss { get; set; }

        public override List<T> GetListPagtosDoMes()
        {
            return _dbSet.Where(it => it.cpf == ObjEF.cpf && it.data_pagto.Month == ObjEF.data_pagto.Month & it.id_lancto != ObjEF.id_lancto).ToList();
        }

        public bool CalcularEsteCodigoPC(string codigo)
        {
            // Não Calcular ISS
            
            if(!temIss)
                return (codigo != Constantes.ISS);
            else
                return true;
        }

        public decimal TotalMes(string cpf)
        {
            return _dbSet.Where(it => it.cpf == cpf & it.data_pagto.Month == ObjEF.data_pagto.Month & it.id_lancto != ObjEF.id_lancto).Sum(it => it.valor);
        }

       
    } 

}