using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data;

namespace Medusa.BLL
{
    public class HistoricoRequisicaoBLL : AbstractCrudWithLog<HistoricoRequisicao>
    {
        public override void Add()
        {
            ObjEF.RequisicaoMaterial = _dbContext.RequisicaoMateriais.Where(it => it.id_requisicao_material == ObjEF.id_requisicao_material).First();
            ObjEF.data = DateTime.Now;
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.RequisicaoMaterial.id_ultimo_status = ObjEF.id_status_requisicao_material;

            if (ObjEF.id_status_requisicao_material == StatusRequisicaoMaterialBLL.Atendido)
                ObjEF.RequisicaoMaterial.MaterialConsumo.qtde_total -= ObjEF.quantidade;

            base.Add();
        }

        public override bool SaveChanges()
        {
            try
            {
                bool rt = base.SaveChanges();
              
                return rt;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exists()
        {
            return ObjEF.id_historico_requisicao != 0;
        }
    }
}
