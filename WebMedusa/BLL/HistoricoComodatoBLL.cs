using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class HistoricoComodatoBLL : AbstractCrudWithLog<HistoricoComodato>
    {
        public override void Add()
        {
            ObjEF.data = DateTime.Now;
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            base.Add();
        }

        public override bool SaveChanges()
        {
            try
            {
                bool rt = base.SaveChanges();
                var comBLL = new ComodatoBLL();
                comBLL.Get(ObjEF.id_comodato);
                comBLL.SalvarUltimoStatus();
                return rt;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}