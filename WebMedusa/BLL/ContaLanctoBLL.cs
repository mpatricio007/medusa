using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Collections;

namespace Medusa.BLL
{
    public class ContaLanctoBLL : AbstractCrudWithLog<ContaLancto>
    {
        public override void Update()
        {
            if (!ObjEF.conciliado)
                base.Update();
        }

        public IEnumerable FindConsultaProjeto(List<Filter> lstFilters, string sortExpression, string sortDirection, int top)
        {
            if (top == 0)
                return _dbSet.Where(t => t.proj_num != null && t.proj_num != "").Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList();
            else
                return _dbSet.Where(t => t.proj_num != null && t.proj_num != "").Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }


        public override void Delete()
        {
            if (!ObjEF.conciliado)
                base.Delete();
        }

        public bool ContemRemessas()
        {
            return ObjEF.Remessas.Any();
        }

        public bool Conciliar(int intId_lcto)
        {
            Get(intId_lcto);
            if (!ObjEF.conciliado)
            {
                ObjEF.data_conciliado = DateTime.Now;
                ObjEF.conciliado = true;
                Update();
                return SaveChanges();
            }
            else
                return false;

        }

        public ContaLanctoBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
            _dbSet = _dbContext.ContaLanctos;
        }
        public ContaLanctoBLL()
        {
        }

        public void Add(ContaLancto cl)
        {
            _dbSet.Add(cl);
        }

        public bool Exists()
        {
            return ObjEF.id_lcto_conta != 0;
        }
    }
}