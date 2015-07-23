using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class vDespesasToDbfBLL : AbstractCrudWithLog<vDespesasToDbf>
    {
        public void ResponseToPage(int id_lancto)
        {
            var l = _dbContext.Lancamentos.Find(id_lancto);
            string url = String.Format("../../{0}?pk={1}",l.tipo.Pagina.url, l.id_lancto.ToString().Criptografar());
            System.Web.HttpContext.Current.Response.Redirect(url);
        }
    }
}
