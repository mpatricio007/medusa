using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class CarregarPagtosBLL : AbstractCrudWithLog<CarregarPagto>
    {
        

        public void GerarLancamentosBB(DateTime data)
        {
            CarregarPagtosBLL cp = new CarregarPagtosBLL();
            IQueryable lstCarregarPagto = cp.Find(u => u.datapagto == data).AsQueryable();
            foreach (CarregarPagto c in lstCarregarPagto)
            {

                ContaBLL ct = new ContaBLL();
                string conta = Util.TirarFormatoConta(c.conta);
                conta = conta.Substring(0,conta.Length-1);            
                Conta cont = ct.Find(t => t.numero.StartsWith(conta) & t.BancoAgencia.Banco.codigo == "001" & t.status==true).First();
                if (cont.BancoAgencia != null)
                {
                    ContaLanctoBLL clDebito = new ContaLanctoBLL();
                    clDebito.ObjEF.data = data;
                    clDebito.ObjEF.id_conta = cont.id_conta;
                    clDebito.ObjEF.id_tipo_lcto = TipoLctoBLL.PAGAMENTOS; // Código para Pagamentos
                    FormaPagtoBLL fp = new FormaPagtoBLL();
                    FormaPagto fpDAL = fp.Find(t => t.banco.codigo == "001" & t.codigo == c.forma).FirstOrDefault();
                    clDebito.ObjEF.descricao = fpDAL.nome;
                    clDebito.ObjEF.valor = c.valor;
                    clDebito.Add();
                    clDebito.SaveChanges();                   
                }                
            }
        }
    }
}
