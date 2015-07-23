using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class MaterialConsumoBLL : AbstractCrudWithLog<MaterialConsumo>
    {        
        public void AlterarQtdadeTotal(int intQtdade)
        {
            ObjEF.qtde_total += intQtdade;
        }

        public MaterialConsumoBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }
                    
        public List<MaterialConsumo> GetMateriaisDeficientes()
        {
            return _dbSet.ToList().Where(it => it.Total < it.qtde_minima).ToList();
        }

        public MaterialConsumoBLL()
        {
            // TODO: Complete member initialization
        }

        //public bool QtdeDisponivel()
        //{
        //    var reqBLL = new RequisicaoMaterialBLL();
        //    if (ObjEF.qtde_total >= reqBLL.ObjEF.quantidade)
        //        return true;

        //    else
        //        return false;
        //}
    }
}
