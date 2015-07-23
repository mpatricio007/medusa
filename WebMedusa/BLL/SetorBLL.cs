using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class SetorBLL: AbstractCrudWithLog<Setor>
    {

        public const int Diretoria = 2;

        public const int Gerencia = 22;

        public static List<int> NaoParticipantes 
        {
            get 
            {
                return new List<int>()
                {
                    Diretoria
                };
            }
        }

        public List<Setor> GetAllEntrada()
        {
            return _dbSet.Where(it => !OutEntrada.Contains(it.id_setor)).ToList();
        }

        public static List<int> OutEntrada 
        {
            get
            {
                return new List<int> 
                {
                    Diretoria,
                    Gerencia
                };
            }
        }
    }
}
