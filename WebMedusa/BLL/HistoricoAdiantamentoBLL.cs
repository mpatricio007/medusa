using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class HistoricoAdiantamentoBLL : AbstractCrudWithLog<HistoricoAdiantamento>
    {
        public HistoricoAdiantamentoBLL()
        {

        }

        public HistoricoAdiantamentoBLL(Contexto ctx)
            : base(ctx)
        {
        }

        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            base.Add();
        }

        public override bool SaveChanges()
        {
            var adm = _dbContext.Adiantamentos.Find(ObjEF.id_adiantamento);
            adm = adm ?? ObjEF.adiantamento;
            adm.id_ultimo_status = ObjEF.id_status_admto;
            return base.SaveChanges();
        }

        public bool EhBloqueado(int id_adiantamento)
        {
            var adBLL = new AdiantamentoBLL();
            adBLL.Get(id_adiantamento);
            return adBLL.EhConcluido() || adBLL.EhReprovado() || adBLL.EhAprovado() || adBLL.EhCancelado();// || !adBLL.Aberto();
        }
    }
}