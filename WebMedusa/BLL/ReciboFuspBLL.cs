using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class ReciboFuspBLL : AbstractCrudWithLog<ReciboFusp>
    {
        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.status_recibo = true;
            base.Add();
        }

        public override void Delete()
        {
            ObjEF.status_recibo = false;
        }

        public bool Exists()
        {
            return ObjEF.id_recibo_fusp != 0;
        }
    }
}



