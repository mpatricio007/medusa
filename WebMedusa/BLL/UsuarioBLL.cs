using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using System.Data;
using System.Data.Entity;

namespace Medusa.BLL
{
    public class UsuarioBLL : PessoaFisBLL<Usuario>
    {
        public void GetUsuarioFuspPorCpf(string strCpf)
        {
            ObjEF = (from u in _dbContext.Usuarios.OfType<Usuario>()
                     where u.PessoaFisica.cpf.Value == strCpf
                     select u).FirstOrDefault();
        }

        public bool Exists()
        {
            return ObjEF.id_usuario != 0;
        }

        public override void Get(CPF cpf)
        {
            var pes = _dbContext.Pessoas.OfType<PessoaFisica>().Where(it => it.cpf.Value == cpf.Value).FirstOrDefault() ?? new PessoaFisica();
            ObjEF.id_pessoa = pes.id_pessoa;
            _dbContext.Entry(pes).State = EntityState.Detached;
        }

        public bool ExistUsuario(CPF cpf)
        {
            return _dbContext.Usuarios.SingleOrDefault(it => it.PessoaFisica.cpf.Value == cpf.Value) != null;
        }
        protected override void setPessoa()
        {
            base.setPessoa();
            var oldPes = pesEntry.GetDatabaseValues().ToObject() as PessoaFisica;
            ObjEF.PessoaFisica.dtNascto = oldPes.dtNascto;
        }
    }
}