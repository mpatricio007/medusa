using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    public static class ListExtensionsMethods
    {
        //public static List<Email> ConvertToListEmail(this List<PessoaEmail> pessoaEmails)
        //{
        //    return (from pemail in pessoaEmails
        //           select pemail.email).ToList();
        //}

        //public static List<PessoaEmail> ConvertToListPessoaEmail(this List<Email> emails)
        //{
        //    return (from em in emails
        //            select new PessoaEmail(em)).ToList();
        //}

        //public static List<Telefone> ConvertToListTelefone(this List<PessoaTelefone> pessoaTelefones)
        //{
        //    return (from ptel in pessoaTelefones
        //            select ptel.telefone).ToList();
        //}

        //public static List<PessoaTelefone> ConvertToListPessoaTelefone(this List<Telefone> telefones)
        //{
        //    return (from tel in telefones
        //            select new PessoaTelefone(tel)).ToList();
        //}

        public static List<Endereco> ConvertToListEndereco(this List<PessoaEndereco> pessoaEnderecos)
        {
            return (from pender in pessoaEnderecos
                    select pender.endereco).ToList();
        }

        public static List<PessoaEndereco> ConvertToListPessoaEndereco(this List<Endereco> enderecos)
        {
            return (from ender in enderecos
                    select new PessoaEndereco(ender)).ToList();
        }


        public static List<Email> ConvertToListEmail(this List<FinanciadorEmail> fEmails)
        {
            return (from femail in fEmails
                    select femail.email).ToList();
        }

        public static List<FinanciadorEmail> ConvertToListFinanciadorEmail(this List<Email> emails)
        {
            return (from em in emails
                    select new FinanciadorEmail(em)).ToList();
        }

        public static List<Telefone> ConvertToListTelefone(this List<FinanciadorTelefone> fTelefones)
        {
            return (from ftel in fTelefones
                    select ftel.telefone).ToList();
        }

        public static List<FinanciadorTelefone> ConvertToListFinanciadorTelefone(this List<Telefone> telefones)
        {
            return (from tel in telefones
                    select new FinanciadorTelefone(tel)).ToList();
        }
    }
}