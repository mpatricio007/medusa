using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class DestinatarioEmailBLL : AbstractCrudWithLog<DestinatarioEmail>
    {
        public void ConfirmarLeitura()
        {
            ObjEF.confirmacao_leitura = DateTime.Now;
            Update();
            SaveChanges();
        }

        public override void Delete()
        {
            // Só pode excluir destinatário se o e-mail ainda não foi enviado
            if (ObjEF.CorrespondenciaEmail.enviadoEm==null)
                base.Delete();
        }
    }
}
