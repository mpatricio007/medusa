using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Collections.Specialized;

namespace Medusa.BLL
{
    public abstract class PessoaJurBLL<T> : AbstractCrudWithLog<T> where T : AbstractPessoaJuridica, new()
    {
        protected DbEntityEntry<PessoaJuridica> pesEntry;
        protected PessoaJuridica newPes;
        public override void Add()  
        {
            updateEntries();
            base.Add();
        }

        public override void Update()
        {             
            updateEntries();
            base.Update();           
        }

        protected virtual void updateEntries()
        {
            if (ObjEF.PessoaJuridica.id_pessoa != 0)
            {

                var newTelefones = ObjEF.PessoaJuridica.Telefones.ToList();
                newTelefones.ForEach(it => ObjEF.PessoaJuridica.Telefones.Remove(it));

                var newEnderecos = ObjEF.PessoaJuridica.Enderecos.ToList();
                newEnderecos.ForEach(it => ObjEF.PessoaJuridica.Enderecos.Remove(it));

                var newEmails = ObjEF.PessoaJuridica.Emails.ToList();
                newEmails.ForEach(it => ObjEF.PessoaJuridica.Emails.Remove(it));

                _dbContext.Pessoas.Attach(ObjEF.PessoaJuridica);
                pesEntry = _dbContext.Entry(ObjEF.PessoaJuridica);
                newPes = (PessoaJuridica)pesEntry.CurrentValues.ToObject();
                pesEntry.Reload();
                setPessoa();

                #region emails
                if (ObjEF.PessoaJuridica.SetEmails)
                {
                    pesEntry.Collection(it => it.Emails).Load();
                    foreach (var em in newEmails)
                    {
                        var originalEm = ObjEF.PessoaJuridica.Emails.Where(it => it.id_email == em.id_email & it.id_email != 0).SingleOrDefault();
                        if (originalEm != null)
                        {
                            var emEntry = _dbContext.Entry(originalEm);
                            emEntry.CurrentValues.SetValues(em);
                        }
                        else
                        {
                            ObjEF.PessoaJuridica.Emails.Add(em);
                        }
                    }
                    foreach (var em in ObjEF.PessoaJuridica.Emails.Where(it => it.id_email != 0).ToList())
                    {
                        if (!newEmails.Any(it => it.id_email == em.id_email))
                            _dbContext.PessoaEmails.Remove(em);
                    }
                }
                #endregion

                #region telefones
                if (ObjEF.PessoaJuridica.SetTelefones)
                {
                    pesEntry.Collection(it => it.Telefones).Load();
                    foreach (var tel in newTelefones)
                    {
                        var originalTel = ObjEF.PessoaJuridica.Telefones.Where(it => it.id_telefone == tel.id_telefone & tel.id_telefone != 0).SingleOrDefault();
                        if (originalTel != null)
                        {
                            var telEntry = _dbContext.Entry(originalTel);
                            telEntry.CurrentValues.SetValues(tel);
                        }
                        else
                        {
                            ObjEF.PessoaJuridica.Telefones.Add(tel);
                        }
                    }
                    foreach (var tel in ObjEF.PessoaJuridica.Telefones.Where(it => it.id_telefone != 0).ToList())
                    {
                        if (!newTelefones.Any(it => it.id_telefone == tel.id_telefone))
                            _dbContext.PessoaTelefones.Remove(tel);
                    }
                }
                #endregion

                #region enderecos
                if (ObjEF.PessoaJuridica.SetEnderecos)
                {

                    pesEntry.Collection(it => it.Enderecos).Load();

                    foreach (var ender in ObjEF.PessoaJuridica.Enderecos.Where(it => it.id_endereco != 0).ToList())
                    {
                            _dbContext.PessoaEnderecos.Remove(ender);
                    }

                    foreach (var ender in newEnderecos)
                    {
                        ender.id_pessoa = ObjEF.PessoaJuridica.id_pessoa;
                        _dbContext.PessoaEnderecos.Add(ender);
                    }
                    
                }
                #endregion
             
            }
        }

        protected virtual void setPessoa()
        {
            pesEntry.CurrentValues.SetValues(newPes);
        }

        public virtual void Get(CNPJ cnpj)
        {
            ObjEF.PessoaJuridica = _dbContext.Pessoas.OfType<PessoaJuridica>().Where(it => it.cnpj.Value == cnpj.Value).FirstOrDefault() ?? new PessoaJuridica();                  
        }
    }
}



    //#region enderecos
    //            if (ObjEF.PessoaFisica.SetEnderecos)
    //            {



                   


    //                pesEntry.Collection(it => it.Enderecos).Load();

    //                foreach (var ender in ObjEF.PessoaFisica.Enderecos.Where(it => it.id_endereco != 0).ToList())
    //                {
    //                    if (!newEnderecos.Any(it => it.id_endereco == ender.id_endereco))
    //                        _dbContext.PessoaEnderecos.Remove(ender);
    //                }

    //                foreach (var ender in newEnderecos)
    //                {
    //                    var originalEnder = ObjEF.PessoaFisica.Enderecos.Where(it => it.id_endereco == ender.id_endereco & ender.id_endereco != 0).SingleOrDefault();
    //                    if (originalEnder != null)
    //                    {
    //                        var enderEntry = _dbContext.Entry(originalEnder);
    //                        enderEntry.CurrentValues.SetValues(ender);
    //                    }
    //                    else
    //                    {
    //                        ObjEF.PessoaFisica.Enderecos.Add(ender);
    //                    }
    //                }
                    
    //            }
    //            #endregion