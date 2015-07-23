using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data;

namespace Medusa.BLL
{
    public class RequisicaoBLL : AbstractCrudWithLog<Requisicao>
    {
        public List<RequisicaoMaterial> oldMateriais { get; set; }

        public override void Add()
        {
            updateEntries();
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.id_status_requisicao = StatusRequisicaoBLL.NaoEnviado;
            ObjEF.data = DateTime.Now;
            base.Add();
        }

        //public override bool DataIsValid(ref string msg)
        //{
        //    var m = new MaterialConsumoBLL();
        //    //m.Get(ObjEF.id_material);

        //    bool rt = true;
        //    var reqBLL = new RequisicaoMaterialBLL(_dbContext);
        //    //reqBLL.Get(ObjEF.id_requisicao_material);

        //    if (reqBLL.ObjEF.quantidade <= 0)
        //    {
        //        msg = "Quantidade deve ser sempre maior que zero";
        //        return false;
        //    }

        //    if (!reqBLL.MesmoUsuario())
        //    {
        //        rt = false;
        //        msg = "Somente quem solicitou a requisição poderá modificá-la!";
        //    }
        //    else
        //    {
        //        if (reqBLL.ObjEF.quantidade > m.ObjEF.qtde_total)
        //        {
        //            rt = false;
        //            //msg = "Quantidade indisponível no momento. Favor solicitar a compra deste material!";
        //            msg = String.Format("Quantidade indisponível no momento! Quantidade em estoque é de {0}, favor solicitar a compra deste matérial!", m.ObjEF.qtde_total);
        //        }
        //    }
        //    return rt;
        //}


        protected virtual void updateEntries()
        {
            var newMateriais = ObjEF.RequisicaoMateriais.ToList();
            newMateriais.ForEach(it => ObjEF.RequisicaoMateriais.Remove(it));

            if (ObjEF.id_requisicao != 0)
            {
                var reqEntry = _dbContext.Entry(ObjEF);
                reqEntry.State = EntityState.Modified;

            }

            foreach (var reqMat in oldMateriais)
            {
                var rBLL = new RequisicaoMaterialBLL(_dbContext);
                rBLL.Get(reqMat.id_requisicao_material);
                rBLL.Delete();
            }
            foreach (var reqMat in newMateriais)
            {
                var rBLL = new RequisicaoMaterialBLL(_dbContext);
                rBLL.ObjEF = new RequisicaoMaterial();
                rBLL.ObjEF.id_material = reqMat.id_material;
                rBLL.ObjEF.quantidade = reqMat.quantidade;
                rBLL.ObjEF.id_requisicao_material = 0;
                rBLL.ObjEF.id_requisicao = ObjEF.id_requisicao;
                rBLL.Add();
            }

        }
        public override void Update()
        {
            updateEntries();
            base.Update();
        }

        public override bool SaveChanges()
        {
            bool rt = base.SaveChanges();

            var ds = ObjEF.RequisicaoMateriais.ToList();
            foreach (var item in ds)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }

            return rt;
        }

        public bool Exists()
        {
            return ObjEF.id_requisicao != 0;
        }

        public bool MesmoUsuario()
        {
            return (ObjEF.id_usuario == SecurityBLL.GetCurrentUsuario().id_usuario);
        }

        public RequisicaoBLL(Contexto _dbContext)
        {
            this._dbContext = _dbContext;
            
        }

        public RequisicaoBLL()
        {
        }

        public bool Enviar()
        {
            if (ObjEF.RequisicaoMateriais.Count() > 0)
            {
                ObjEF.id_status_requisicao = StatusRequisicaoBLL.Enviado;
                Update();
                return SaveChanges();
            }
            else return false;
        }

        public IEnumerable<Requisicao> FindEnviados(List<Filter> lstFilters, string sortExpression, string sortDirection, int top)
        {
            if (top == 0)
                return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Where(it => it.id_status_requisicao == StatusRequisicaoBLL.Enviado).ToList();
            else
                return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).Where(it => it.id_status_requisicao == StatusRequisicaoBLL.Enviado).ToList();
        }

        public static string EmAberto 
        {
            get
            {
                return "em aberto";
            }
        }

        public static string Encerrado
        {
            get
            {
                return "encerrado";
            }
        }
    }
}
