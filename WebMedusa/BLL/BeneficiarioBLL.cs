using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class BeneficiarioBLL : PessoaFisBLL<Beneficiario>
    {
        public List<Adiantamento> GetAdiantamentosAbertos()
        {
            return _dbContext.Adiantamentos.Where(it => it.id_beneficiario == ObjEF.id_beneficiario & it.StatusAdiantamento.id_status_admto == 1).ToList();
        }
        protected override void setPessoa()
        {
            base.setPessoa();
            var oldPes = pesEntry.GetDatabaseValues().ToObject() as PessoaFisica;
            ObjEF.PessoaFisica.dtNascto = oldPes.dtNascto;
        }
    }
}   