using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data;
using System.Data.Entity;

namespace Medusa.BLL
{
    public class EventoBLL : AbstractCrudWithLog<Evento>
    {
        public void SalvarEventoSacados(IQueryable<Sacado> retorno)
        {
            var ds = retorno.Select(it => it.id_sacado).Except(
                ObjEF.EventosSacados.Select(it => it.id_sacado));
            var evsBLL = new EventoSacadoBLL();
            foreach (var id_sacado in ds)
            {
                evsBLL.ObjEF = new EventoSacado()
                {
                    id_evento = ObjEF.id_evento,
                    id_sacado = id_sacado
                };               
                evsBLL.Add();
            }
            evsBLL.SaveChanges();
        }


        public bool GerarParcelas(List<EventoSacado> retorno, int qtdadeParcelas, int startParcela, DateTime dtVenctoInicial, decimal valor)
        {
            foreach (var evs in retorno)
            {
                var es = ObjEF.EventosSacados.Single(it => it.id_evento_sacado == evs.id_evento_sacado);
                for (int i = 0; i < qtdadeParcelas; i++)
                {
                    var bc = new BoletoCobranca();
                    bc.data_vencto = dtVenctoInicial.AddMonths(i);
                    bc.valor = valor;
                    bc.codigo = String.Format("{0:0000000000000}", bc.id_boleto);
                    bc.num_parcela = startParcela + i;
                    es.Boletos.Add(bc);
                }
            }
            _dbContext.Entry(ObjEF).State = EntityState.Modified;
            return SaveChanges();
            
        }


        public void EnviarBoletosEmail()
        {
            try 
	        {	        
                string saida;
                var bolBLL = new BoletoCobrancaBLL();
                foreach (var item in ObjEF.EventosSacados)
                    bolBLL.EnviarEmail(item.Boletos.ToList(), out saida);
            
                Medusa.LIB.Util.ShowMessage("E-mails enviados com sucesso!");
	        }
	        catch (Exception)
	        {
		
		        Medusa.LIB.Util.ShowMessage("Erro!");
	        }
        }
    }
}
            
          
   