using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class StatusProjetoBLL : AbstractCrudWithLog<StatusProjeto>
    {
        public static int Ativo
        {
            get
            {
                return 1;
            }
        }

        public static int Bloqueado
        {
            get
            {
                return 2;
            }
        }

        public static int Inativo
        {
            get
            {
                return 3;
            }
        }

        public static int Preliminar
        {
            get
            {
                return 4;
            }
        }
    }
}
