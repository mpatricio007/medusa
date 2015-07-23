using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data.Entity.Infrastructure;
using System.Collections.Specialized;

namespace Medusa.BLL
{
    public class InscricaoPregaoBLL : Abstract_Crud<InscricaoPregao>
    {
        protected DbEntityEntry<InscricaoPregaoTelefone> inscEntry;
        protected InscricaoPregaoTelefone newInsc;

        public void GetUsuarioPorCpf(string strCpf)
        {
            ObjEF = (from i in _dbContext.InscricaoPregoes.OfType<InscricaoPregao>()
                     where i.cpf.Value == strCpf
                     select i).FirstOrDefault();
        }

        //public override bool SaveChanges()
        //{
        //    try
        //    {
        //        var state = _dbContext.Entry(ObjEF).State;
        //        if (state == EntityState.Added)
        //            return (state == EntityState.Unchanged) ? true : _dbContext.SaveChanges() > 0;
        //        else
        //            return base.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public void GetCnpj(CNPJ cnpj)
        {
            ObjEF = _dbSet.SingleOrDefault<InscricaoPregao>(it => it.cnpj.Value == cnpj.Value);
        }

        public void GetCpf(CPF cpf)
        {
            ObjEF = _dbSet.SingleOrDefault<InscricaoPregao>(it => it.cpf.Value == cpf.Value);
        }

        public bool Exists()
        {
            return ObjEF.id_inscricao_pregao != 0;
        }

        public void GetUsuarioPorCnpj(string strCnpj)
        {
            ObjEF = (from i in _dbContext.InscricaoPregoes.OfType<InscricaoPregao>()
                     where i.cnpj.Value == strCnpj
                     select i).FirstOrDefault();
        }

        public override void Add()
        {
            ObjEF.status = true;
            
            base.Add();
        }

        public override void Update()
        {
            base.Update();
        }

        public override bool DataIsValid()
        {
            return !Exists();
        }
    }
}
