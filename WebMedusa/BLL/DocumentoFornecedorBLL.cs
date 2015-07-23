using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class DocumentoFornecedorBLL : AbstractCrudWithLog<DocumentoFornecedor>
    {
        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.data = DateTime.Now;
            ObjEF.id_status_docFornecedor = StatusDocFornecedorBLL.Incluso;
            base.Add();
        }

        public void Update(string obs)
        {
            ObjEF.obs = obs;
            ObjEF.id_status_docFornecedor = StatusDocFornecedorBLL.Cancelado;
            base.Update();
        }

        public bool Exists()
        {
            return ObjEF.id_doc_fornecedor != 0;
        }
    }
}