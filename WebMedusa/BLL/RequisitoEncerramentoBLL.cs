using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class RequisitoEncerramentoBLL : AbstractCrudWithLog<RequisitoEncerramento>
    {

        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            base.Add();
        }

        public bool Exists()
        {
            return ObjEF.id_req_enc != 0;
        }
    }
}
