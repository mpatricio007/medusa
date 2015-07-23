using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class FormaPagtoBLL : AbstractCrudWithLog<FormaPagto>
    {
        public List<FormaPagto> GetAllFormasPagtosLotePagBB(int idBanco, bool eh_mesmo_banco, decimal valor)
        {

            if (eh_mesmo_banco)
                return _dbSet.Where(it => it.id_banco == idBanco & it.eh_pagamentos
                      & it.eh_mesmo_banco == eh_mesmo_banco).OrderBy(it => it.nome).ToList();

            else
                return _dbSet.Where(it => it.id_banco == idBanco & it.eh_pagamentos
                    & !it.eh_mesmo_banco
                    & it.valor_min <= valor & it.valor_max >= valor).OrderBy(it => it.nome).ToList();
        }

        public FormaPagto GetFormaPagtoLotePagBB(int idBanco, bool eh_mesmo_banco, decimal valor, bool ehCC)
        {
            var cod_de_conta_corrente = "01";
            if (eh_mesmo_banco)
                if (ehCC)
                    return _dbSet.Where(it => it.id_banco == idBanco & it.eh_pagamentos
                      & it.eh_mesmo_banco == eh_mesmo_banco & it.codigo == cod_de_conta_corrente).OrderBy(it => it.nome).FirstOrDefault();
                else
                    return _dbSet.Where(it => it.id_banco == idBanco & it.eh_pagamentos
                      & it.eh_mesmo_banco == eh_mesmo_banco & it.codigo != cod_de_conta_corrente).OrderBy(it => it.nome).FirstOrDefault();
            else
                if (ehCC)
                    return _dbSet.Where(it => it.id_banco == idBanco & it.eh_pagamentos
                    & !it.eh_mesmo_banco & it.finalidade_pagto == cod_de_conta_corrente
                    & it.valor_min <= valor & it.valor_max >= valor).OrderBy(it => it.nome).FirstOrDefault();
                else
                    return _dbSet.Where(it => it.id_banco == idBanco & it.eh_pagamentos
                    & !it.eh_mesmo_banco & it.finalidade_pagto != cod_de_conta_corrente
                    & it.valor_min <= valor & it.valor_max >= valor).OrderBy(it => it.nome).FirstOrDefault();
        }


        public FormaPagtoBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }

        public FormaPagtoBLL()
        {
        }

        public static int CREDITO_CC
        {
            get
            {
                return 1;
            }
        }

        public static List<int> TED
        {
            get
            {
                return new List<int>()
                {
                    3,12
                };
            }
        }

        public static List<int> DOC
        {
            get
            {
                return new List<int>()
                {
                    2,11
                };
            }
        }

        public static int CREDITO_CP
        {
            get
            {
                return 6;
            }
        }

        public static int BOLETO_MESMO_BANCO
        {
            get
            {
                return 8;
            }

        }

        public static int BOLETO_OUTRO_BANCO
        {
            get
            {
                return 9;
            }
        }

        public static int GUIAS_CONTAS_CONSUMO
        {
            get
            {
                return 10;
            }

        }

        public static int GUIAS_GPS
        {
            get
            {
                return 15;
            }

        }

        public static int GUIAS_GRU
        {
            get
            {
                return 16;
            }
        }
    }
}
