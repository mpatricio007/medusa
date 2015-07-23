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
    public abstract class PessoaFisBLL<T> : AbstractCrudWithLog<T> where T : AbstractPessoaFisica, new()
    {
        protected DbEntityEntry<PessoaFisica> pesEntry;
        protected PessoaFisica newPes;
        public override void Add()  
        {
            base.Add();
            updateEntries();
        }

        public override void Update()
        {             
            updateEntries();
            base.Update();           
        }

        protected virtual void updateEntries()
        {
            if (ObjEF.PessoaFisica.id_pessoa != 0)
            
            {

                var newTelefones = ObjEF.PessoaFisica.Telefones.ToList();
                newTelefones.ForEach(it => ObjEF.PessoaFisica.Telefones.Remove(it));

                var newEnderecos = ObjEF.PessoaFisica.Enderecos.ToList();
                newEnderecos.ForEach(it => ObjEF.PessoaFisica.Enderecos.Remove(it));

                var newEmails = ObjEF.PessoaFisica.Emails.ToList();
                newEmails.ForEach(it => ObjEF.PessoaFisica.Emails.Remove(it));
                
                _dbContext.Pessoas.Attach(ObjEF.PessoaFisica);
                pesEntry = _dbContext.Entry(ObjEF.PessoaFisica);
                newPes = (PessoaFisica)pesEntry.CurrentValues.ToObject();
                pesEntry.Reload();
                setPessoa();

                #region emails
                if (ObjEF.PessoaFisica.SetEmails)
                {
                    pesEntry.Collection(it => it.Emails).Load();
                    foreach (var em in newEmails)
                        ObjEF.PessoaFisica.Emails.Add(em);

                    foreach (var em in _dbContext.PessoaEmails.Where(it => it.id_pessoa == ObjEF.PessoaFisica.id_pessoa))
                        _dbContext.PessoaEmails.Remove(em);
                }
                #endregion

                #region telefones
                if (ObjEF.PessoaFisica.SetTelefones)
                {
                    pesEntry.Collection(it => it.Telefones).Load();
                    foreach (var tel in newTelefones)
                        ObjEF.PessoaFisica.Telefones.Add(tel);

                    foreach (var tel in _dbContext.PessoaTelefones.Where(it => it.id_pessoa == ObjEF.PessoaFisica.id_pessoa))
                        _dbContext.PessoaTelefones.Remove(tel);
                }
                
                #endregion

                #region enderecos
                if (ObjEF.PessoaFisica.SetEnderecos)
                {

                    pesEntry.Collection(it => it.Enderecos).Load();

                    foreach (var ender in newEnderecos)
                        ObjEF.PessoaFisica.Enderecos.Add(ender);

                    foreach (var ender in _dbContext.PessoaEnderecos.Where(it => it.id_pessoa == ObjEF.PessoaFisica.id_pessoa))
                        _dbContext.PessoaEnderecos.Remove(ender);

                }
                #endregion

                //#region emails
                //if (ObjEF.PessoaFisica.SetEmails)
                //{
                //    pesEntry.Collection(it => it.Emails).Load();
                //    foreach (var em in newEmails)
                //    {
                //        var originalEm = ObjEF.PessoaFisica.Emails.Where(it => it.id_email == em.id_email & it.id_email != 0).SingleOrDefault();
                //        if (originalEm != null)
                //        {
                //            var emEntry = _dbContext.Entry(originalEm);
                //            emEntry.CurrentValues.SetValues(em);
                //        }
                //        else
                //        {
                //            ObjEF.PessoaFisica.Emails.Add(em);
                //        }
                //    }
                //    foreach (var em in ObjEF.PessoaFisica.Emails.Where(it => it.id_email != 0).ToList())
                //    {
                //        if (!newEmails.Any(it => it.id_email == em.id_email))
                //            _dbContext.PessoaEmails.Remove(em);
                //    }
                //}
                //#endregion

                //#region telefones
                //if (ObjEF.PessoaFisica.SetTelefones)
                //{
                //    pesEntry.Collection(it => it.Telefones).Load();
                //    foreach (var tel in newTelefones)
                //    {
                //        var originalTel = _dbContext.PessoaTelefones.Where(it => it.id_telefone == tel.id_telefone & tel.id_telefone != 0).SingleOrDefault();
                //        if (originalTel != null)
                //        {
                //            var telEntry = _dbContext.Entry(originalTel);
                //            telEntry.CurrentValues.SetValues(tel);
                //        }
                //        else
                //        {
                //            ObjEF.PessoaFisica.Telefones.Add(tel);
                //        }
                //    }
                //    foreach (var tel in ObjEF.PessoaFisica.Telefones.Where(it => it.id_telefone != 0).ToList())
                //    {
                //        if (!newTelefones.Any(it => it.id_telefone == tel.id_telefone))
                //            _dbContext.PessoaTelefones.Remove(tel);
                //    }
                //}
                //#endregion

                //#region enderecos
                //if (ObjEF.PessoaFisica.SetEnderecos)
                //{

                //    pesEntry.Collection(it => it.Enderecos).Load();

                //    foreach (var ender in _dbContext.PessoaEnderecos.Where(it => it.id_endereco != 0).ToList())
                //    {
                //        _dbContext.PessoaEnderecos.Remove(ender);
                //    }

                //    foreach (var ender in newEnderecos)
                //    {
                //        ender.id_pessoa = ObjEF.PessoaFisica.id_pessoa;
                //        _dbContext.PessoaEnderecos.Add(ender);
                //    }

                //}
                //#endregion
             
            }
        }

        protected virtual void setPessoa()
        {
            pesEntry.CurrentValues.SetValues(newPes);
        }

        public virtual void Get(CPF cpf)
        {
            ObjEF.PessoaFisica = _dbContext.Pessoas.OfType<PessoaFisica>().Where(it => it.cpf.Value == cpf.Value).FirstOrDefault() ?? new PessoaFisica();                  
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