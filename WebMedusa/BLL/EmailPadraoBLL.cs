using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class EmailPadraoBLL : AbstractCrudWithLog<EmailPadrao>
    {

        public List<EmailCopia> GetEmailCopiasThis()
        {
            return ObjEF.EmailCopias.ToList();
        }

        public bool Existes()
        {
            return ObjEF.id_email_padrao != 0;
        }
    }
}
