using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data;
using System.Data.Entity;

namespace Medusa.BLL
{
    public class ContaTransfBLL : AbstractCrudWithLog<ContaTransf>
    {
        public override void Add()
        {
            ObjEF.Lancamentos = new List<ContaLancto>();
            ContaLancto clCredito = new ContaLancto();
            clCredito.data = ObjEF.data;
            clCredito.id_conta = ObjEF.id_conta_credito;
            clCredito.id_tipo_lcto = 7; // Código para Transferência Crédito
            clCredito.descricao = ObjEF.descricao;
            clCredito.valor = ObjEF.valor;

            ObjEF.Lancamentos.Add(clCredito);

            ContaLancto clDebito = new ContaLancto();
            clDebito.data = ObjEF.data;
            clDebito.id_conta = ObjEF.id_conta_debito;
            clDebito.id_tipo_lcto = 5;  // Código para Transferência Dédito
            clDebito.descricao = ObjEF.descricao;
            clDebito.valor = ObjEF.valor;

            ObjEF.Lancamentos.Add(clDebito);

            base.Add();
        }

        public override void Delete()
        {
            if (!ObjEF.HasLactoConciliado)
            {  
                   ContaLancto[] ar = ObjEF.Lancamentos.ToArray();
                   for (int i = 0; i < ar.Count(); i++)
                       _dbContext.Entry<ContaLancto>(ar[i]).State = EntityState.Deleted;
                base.Delete();
            }   
        }

        public override void Update()
        {
            if (!ObjEF.HasLactoConciliado)
            {
                ContaLancto[] ar = ObjEF.Lancamentos.ToArray();
                for (int i = 0; i < ar.Count(); i++)
                    _dbContext.Entry<ContaLancto>(ar[i]).State = EntityState.Deleted;

                ContaLancto clCredito = new ContaLancto();
                clCredito.data = ObjEF.data;
                clCredito.id_conta = ObjEF.id_conta_credito;
                clCredito.id_tipo_lcto = 5;
                clCredito.descricao = ObjEF.descricao;
                clCredito.valor = ObjEF.valor;

                ObjEF.Lancamentos.Add(clCredito);

                ContaLancto clDebito = new ContaLancto();
                clDebito.data = ObjEF.data;
                clDebito.id_conta = ObjEF.id_conta_debito;
                clDebito.id_tipo_lcto = 4;
                clDebito.descricao = ObjEF.descricao;
                clDebito.valor = ObjEF.valor;

                ObjEF.Lancamentos.Add(clDebito);
                base.Update();
            }   
            
        }
    }
}
