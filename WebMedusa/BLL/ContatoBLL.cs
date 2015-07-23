using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using System.Data;

namespace Medusa.BLL
{
    public class ContatoBLL : PessoaFisBLL<Contato>
    {  
        protected override void setPessoa()
        {  
            base.setPessoa();
            var oldPes = pesEntry.GetDatabaseValues().ToObject() as PessoaFisica;
            ObjEF.PessoaFisica.dtNascto = oldPes.dtNascto;
            ObjEF.PessoaFisica.rg = oldPes.rg;
        }
    }
}