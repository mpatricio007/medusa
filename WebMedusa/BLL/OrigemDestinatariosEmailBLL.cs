using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class OrigemDestinatariosEmailBLL : AbstractCrudWithLog<OrigemDestinatariosEmail>
    {
        public IEnumerable<OrigemDestinatariosEmail> GetDestinatarios()
        {
            return (from c in _dbContext.OrigemDestinatariosEmails
                    orderby c.tipo
                    select c).ToList();
        }
    }
}
