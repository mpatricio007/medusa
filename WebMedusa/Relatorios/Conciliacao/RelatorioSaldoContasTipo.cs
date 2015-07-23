using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;


namespace Medusa.Relatorios.Conciliacao
{
    public class RelatorioSaldoContasTipo
    {
        public IEnumerable<SaldoConta> GerarRelatorio(DateTime data)
        {
            Contexto _dbContext = new Contexto();
            return _dbContext.Lista_Saldocontas(data).OrderBy(o => o.descricao).OrderBy(q=>q.banco).OrderBy(p => p.numero).ToList();
        }

    }
}