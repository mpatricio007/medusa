using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class ContatoProjetoBLL : AbstractCrudWithLog<ContatoProjeto>
    {
        public bool JaContemContato()
        {
            return _dbContext.ContatoProjeto.Any(it => it.id_projeto == ObjEF.id_projeto);
        }

        public List<SolicitacaoDeProjeto> GetAllExeptThisContatoProj()
        {
            return _dbContext.SolicitacaoDeProjetos.ToList().Except(ObjEF.Notificacoes.Select(it => it.SolicitacaoDeProjeto).ToList()).ToList();
        }

        public bool Exixtes()
        {
            return ObjEF.id_contato_projeto != 0;
        }
    }
}
