using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class StatusAdiantamentoBLL : AbstractCrudWithLog<StatusAdiantamento>
    {
        public static int eh_aprovado 
        {
            get
            {
                return 2;
            }
        }

        public static int primeiro_status 
        {
            get
            { return 1; }
        }

        public static int eh_reprovado
        {
            get
            {
                return 3;
            }
        }

        public static int eh_concluido
        {
            get
            {
                return 4;
            }
        }

        public static int eh_aberto
        {
            get
            {
                return 1;
            }
        }


        public static int eh_cancelado
        {
            get
            {
                return 5;
            }
        }

        public static List<int> VisibleRelatorio
        {
            get
            {
                return new List<int>() { eh_aberto, eh_aprovado };
            }
        }

        public List<StatusAdiantamento> GetAllVisible()
        {
            return _dbSet.Where(it => it.visible == true ).ToList();
        }

        
    }
}
