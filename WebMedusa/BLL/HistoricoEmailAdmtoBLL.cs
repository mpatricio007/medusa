using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class HistoricoEmailAdmtoBLL : AbstractCrudWithLog<HistoricoEmailAdmto >
    {
        public HistoricoEmailAdmtoBLL()
        { 
        }

        public HistoricoEmailAdmtoBLL(Contexto ctx)
            : base(ctx)
        { }

        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            base.Add();
        }
    }
}
