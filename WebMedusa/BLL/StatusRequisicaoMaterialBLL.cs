using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class StatusRequisicaoMaterialBLL : AbstractCrudWithLog<StatusRequisicaoMaterial>
    {
        public static int AguardandoAtendimento
        {
            get
            {
                return 1;
            }
        }
        
        public static int Atendido
        {
            get
            {
                return 2;
            }
        }

        public static int NaoAtendido
        {
            get
            {
                return 3;
            }
        }

        public static List<int> LstStatusFree
        {
            get 
            {
                return new List<int>() 
                {
                    NaoAtendido,
                    Atendido
                };
            }
        }

        
        public static List<int> lstEncerrado
        {
            get
            {
                return new List<int>()
                {
                  NaoAtendido,
                  Atendido
                };
            }
        }
    }
}
