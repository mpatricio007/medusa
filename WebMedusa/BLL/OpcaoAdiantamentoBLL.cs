using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class OpcaoAdiantamentoBLL : AbstractCrudWithLog<OpcaoAdiantamento>
    {
        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            base.Add();
        }

        public List<OpcaoAdiantamento> GetOpcoesDoProjeto(int Id_projeto)
        {
            return _dbSet.Where(it => it.id_projeto == Id_projeto).ToList();
        }
    }
}
