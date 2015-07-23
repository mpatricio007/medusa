using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data;
using System.Data.Entity;

namespace Medusa.BLL
{
    public class ContaAplicacaoBLL : AbstractCrudWithLog<ContaAplicacao>
    {
        public override void Add()
        {
            ObjEF.ContaLancto = new ContaLancto();
            ObjEF.ContaLancto.id_conta = ObjEF.id_conta;
            ObjEF.ContaLancto.data = ObjEF.data;
            ObjEF.ContaLancto.id_tipo_lcto = ObjEF.id_tipo_lcto;
            ObjEF.ContaLancto.descricao = ObjEF.descricao;
            ObjEF.ContaLancto.valor = ObjEF.valor;
            base.Add();
        }

        public override void Update()
        {
            if (!ObjEF.ContaLancto.conciliado)
            {
                ObjEF.ContaLancto.id_conta = ObjEF.id_conta;
                ObjEF.ContaLancto.data = ObjEF.data;
                ObjEF.ContaLancto.id_tipo_lcto = ObjEF.id_tipo_lcto;
                ObjEF.ContaLancto.descricao = ObjEF.descricao;
                ObjEF.ContaLancto.valor = ObjEF.valor;
                base.Update();
            }
        }

        public override void Delete()
        {
            if (!ObjEF.ContaLancto.conciliado)
            {
                _dbContext.Entry<ContaLancto>(ObjEF.ContaLancto).State = EntityState.Deleted;
                base.Delete();
            }
        }
    }
}
