using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class StatusRequisicaoBLL : AbstractCrudWithLog<StatusRequisicao>
    {
        public static int Enviado
        {
            get
            {
                return 1;
            }
        }

        public static int NaoEnviado
        {
            get
            {
                return 2;
            }
        }
    }
}
