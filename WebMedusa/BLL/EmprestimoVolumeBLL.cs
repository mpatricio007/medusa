using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class EmprestimoVolumeBLL : AbstractCrudWithLog<EmprestimoVolume>
    {
        public static string FinalizarEmprestimo = "999999999";

        public bool RetiradaVolumes(List<Volume> volumes, string strCpf)
        {
            bool rt = false;
            try
            {
                UsuarioFuspBLL usu = new UsuarioFuspBLL();
                usu.GetUsuarioFuspPorCpf(strCpf);
                foreach (Volume vol in volumes)
                {
                    ObjEF.dt_retirada = DateTime.Now;
                    ObjEF.id_usuario_retirada = usu.ObjEF.id_usuario;
                    ObjEF.id_volume = vol.id_volume;
                    Add();
                    rt = SaveChanges();
                }
                
                
            }
            catch (Exception)
            {
                
                
            }
            return rt;
        }

        public bool DevolucaoVolumes(List<Volume> volumes, string strCpf, string strObservacao)
        {
            bool rt = false;
            try
            {
                UsuarioFuspBLL usu = new UsuarioFuspBLL();
                usu.GetUsuarioFuspPorCpf(strCpf);
                foreach (Volume vol in volumes)
                {
                    if (VolumeEstaEmprestado(Convert.ToString(vol.id_volume)))
                    {
                        ObjEF.dt_devolucao = DateTime.Now;
                        ObjEF.id_usuario_devolucao = usu.ObjEF.id_usuario;
                        ObjEF.observacao = strObservacao;
                        Update();
                        rt = SaveChanges();
                    }
                }


            }
            catch (Exception)
            {


            }
            return rt;
        }

        public bool VolumeEstaEmprestado(string strId_volume)
        {
            int id_volume = 0;
            int.TryParse(strId_volume, out id_volume);

            ObjEF = (from e in _dbContext.EmprestimoVolumes
                     where e.dt_devolucao == null & e.id_volume == id_volume
                     select e).FirstOrDefault();

            return ObjEF.id_emprestimo != 0;
        }
    }
}
