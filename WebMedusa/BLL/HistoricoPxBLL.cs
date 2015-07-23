using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class HistoricoPxBLL : AbstractCrudWithLog<HistoricoPx>
    {

        public HistoricoPxBLL()
            : base()
        {
        }

        public HistoricoPxBLL(Contexto ctx )
            : base(ctx)
        {
        }

        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.data = DateTime.Now;
            base.Add();
        }

        public override bool SaveChanges()
        {
            bool rt = false;

            var psBLL = new ProjetoSolicitacaoBLL();
            psBLL.Get(ObjEF.id_sol_proj);

            if (_dbContext.StatusSolicitacoes.OrderByDescending(it => it.ordem).First().id_status_solicitacao == psBLL.ObjEF.id_ultimo_status)
                Util.ShowMessage("esta solicitação já é Projeto A!");
            else
            
                    rt = base.SaveChanges();
            


            return rt;
        }
    }
}
