using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Collections;

namespace Medusa.BLL
{
    public class RequisicaoMaterialBLL : AbstractCrudWithLog<RequisicaoMaterial>
    {
        public override void Add()
        {
            var status = _dbContext.StatusRequisicaoMateriais.SingleOrDefault(it => it.ordem == 0);
            ObjEF.StatusRequisicaoMaterial = status;
            ObjEF.Historicos = new List<HistoricoRequisicao>();
            var primeiroStatus = new HistoricoRequisicao();
            primeiroStatus.StatusRequisicaoMaterial = status;
            primeiroStatus.data = DateTime.Now;
            primeiroStatus.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.Historicos.Add(primeiroStatus);
            ObjEF.id_ultimo_status = StatusRequisicaoBLL.NaoEnviado;
            base.Add();
        }

        public void SalvarUltimoStatus()
        {
            if (ObjEF.Historicos.Count() > 0)
            {
                ObjEF.id_ultimo_status = ObjEF.Historicos.OrderByDescending(it => it.id_historico_requisicao).First().id_status_requisicao_material;
                Update();
                SaveChanges();
            }
        }

        public override void Delete()
        {
            var matBLL = new MaterialConsumoBLL();
            matBLL.Get(ObjEF.id_material);
            base.Delete();
        }

        public bool MesmoUsuario()
        {
            return (ObjEF.Requisicao.id_usuario == SecurityBLL.GetCurrentUsuario().id_usuario);
            
        }

        public void DesfazerAtendimento()
        {
            //ObjEF.MaterialConsumo.qtde_total += ObjEF.quantidade;
            var h = new HistoricoRequisicao();
            h.id_requisicao_material = ObjEF.id_requisicao_material;
            h.id_status_requisicao_material = ObjEF.id_ultimo_status = StatusRequisicaoMaterialBLL.AguardandoAtendimento;
            h.data = DateTime.Now;
            h.quantidade = ObjEF.quantidade;
            h.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.Historicos.Add(h);

            Update();
        }

        public bool Exists()
        {
            return ObjEF.id_requisicao_material != 0;
        }


        public RequisicaoMaterialBLL(Contexto ctx)
            : base(ctx)
        {
            // TODO: Complete member initialization
            //this._dbContext = ctx;
        }

        public RequisicaoMaterialBLL()
        {
        }

        //public IEnumerable FindEnviados(List<Filter> lstFilters, string sortExpression, string sortDirection, int top)
        //{
        //    if (top == 0)
        //        return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Where(it => it.id_ultimo_status == StatusRequisicaoBLL.Enviado).ToList();
        //    else
        //        return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).Where(it => it.id_ultimo_status == StatusRequisicaoBLL.Enviado).ToList();
        //}

        //public static int Aguardando
        //{
        //    get 
        //    {
        //        return 1;
        //    }
        //}

        //public static int FaltaMaterial
        //{
        //    get
        //    {
        //        return 3;
        //    }
        //}

    }
}
