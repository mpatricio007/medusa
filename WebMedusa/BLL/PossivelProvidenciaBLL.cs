using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class PossivelProvidenciaBLL : AbstractCrudWithLog<PossivelProvidencia>
    {
        public PossivelProvidenciaBLL() { }

        public PossivelProvidenciaBLL(Contexto ctx)
            : base(ctx)
        {
        }

        public List<PossivelProvidencia> GetAllPossiveis(int Id_status_atual)
        {
            if (Id_status_atual != 0)
                return _dbSet.Where(it => it.id_status_atual == Id_status_atual).ToList();
            else
                return GetAll().OfType<PossivelProvidencia>().ToList();
        }
    }
}
