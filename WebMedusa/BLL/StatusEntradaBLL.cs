using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data;

namespace Medusa.BLL
{
    public class StatusEntradaBLL : AbstractCrudWithLog<StatusEntrada>
    {
        public const int Encaminhado =   1;

        public const int Recebido = 2;
               
        public const int Saida = 3;
              
        public const int SaidaCancelada = 4;

        public const int Arquivado = 5;

        public const int Diretoria = 6;

        public static List<int?> lstCxEnt
        {
            get
            {
                return new List<int?>()
                { 
                    Encaminhado,
                    Recebido
                };
            }
        }

        public static List<int?> lstStatusEntrada
        {
            get
            {
                return new List<int?>()
                { 
                    Encaminhado,
                    Recebido,
                    Saida,
                    SaidaCancelada,
                    Arquivado,
                    Diretoria
                };
            }
        }

        public static string ToString(int status)
        {
            string rt = "";
            switch (status)
            {
                case Encaminhado:
                    rt = "em transito";
                    break;
                case Recebido:
                    rt = "recebido";
                    break;
                case Saida:
                    rt = "saida";
                    break;
                case SaidaCancelada:
                    rt = "saida cancelada";
                    break;
                case Arquivado:
                    rt = "arquivado";
                    break;
                case Diretoria:
                    rt = "diretoria";
                    break;
                default:
                    rt = "erro";
                    break;
            }

            return rt;
        }

        #region OldProvidencias
        //public static List<int?> lstProvidencias
        //{
        //    get
        //    {
        //        return new List<int?>()
        //        { 
        //            EmTransito,
        //            Arquivado,
        //            Diretoria
        //        };
        //    }
        //}

        //public static string ToStringProvidencia(int status)
        //{
        //    string rt = "";
        //    switch (status)
        //    {
        //        case 1:
        //            rt = "encaminhar";
        //            break;
        //        case 2:
        //            rt = "receber";
        //            break;
        //        case 3:
        //            rt = "efetuar saída";
        //            break;
        //        case 4:
        //            rt = "cancelar saída";
        //            break;
        //        case 5:
        //            rt = "arquivar no próprio setor";
        //            break;
        //        case 6:
        //            rt = "enviar a diretoria";
        //            break;
        //        default:
        //            rt = "erro";
        //            break;
        //    }

        //    return rt;
        //}
        #endregion

        public List<PossivelProvidencia> oldPossiveisProvidencias { get; set; }

        protected virtual void updateEntries()
        {
            var newPossiveisProvidencias = ObjEF.PossiveisProvidencias.ToList();
            newPossiveisProvidencias.ForEach(it => ObjEF.PossiveisProvidencias.Remove(it));

            if (Existis())
            {
                var reqEntry = _dbContext.Entry(ObjEF);
                reqEntry.State = EntityState.Modified;

            }

            foreach (var sc in oldPossiveisProvidencias)
            {
                var ppBLL = new PossivelProvidenciaBLL(_dbContext);
                ppBLL.Get(sc.id_possivel_providencia);
                ppBLL.Delete();
            }
            foreach (var pp in newPossiveisProvidencias)
            {
                var ppBLL = new PossivelProvidenciaBLL(_dbContext);
                ppBLL.ObjEF = new PossivelProvidencia();
                ppBLL.ObjEF.id_possivel_providencia = 0;
                ppBLL.ObjEF.id_providencia = pp.id_providencia;
                ppBLL.ObjEF.id_status_atual = pp.id_status_atual;
                ppBLL.Add();
            }

        }

        public bool Existis()
        {
            return ObjEF.id_status_entrada != 0;
        }

        public override void Update()
        {
            updateEntries();
            base.Update();
        }

        public override bool SaveChanges()
        {
            bool rt = base.SaveChanges();

            var ds = ObjEF.PossiveisProvidencias.ToList();
            foreach (var item in ds)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }

            return rt;
        }
    }

    public enum MeusDocumentos
    {
        enviados,
        recebidos
    }

    public enum ProvidenciaEnum
    {
        encaminhar = StatusEntradaBLL.Encaminhado,
        arquivar = StatusEntradaBLL.Arquivado,
        diretoria = StatusEntradaBLL.Diretoria
    }

    public class ProvidenciaEnumBLL
    {
        public static string ToString(ProvidenciaEnum p) 
        {
            string rt = "";
            switch (p)
            {
                case ProvidenciaEnum.encaminhar:
                    rt = "encaminhar";
                    break;
                case ProvidenciaEnum.arquivar:
                    rt = "arquivar no próprio setor";
                    break;
                case ProvidenciaEnum.diretoria:
                    rt = "encaminhar a diretoria";
                    break;
                default:
                    break;
            }
            return rt;
        }
    }
}
