using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Web;

namespace Medusa.BLL
{
    public class ImportaArquivoBLL : AbstractCrudWithLog<ImportaArquivo>
    {
        public override void Add()
        {
            ObjEF.data_importacao = DateTime.Now;
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            base.Add();
        }

        public static ImportaArquivo VerificaSeJaImportou(TipoImpArquivo tipo,DateTime data)
        {
            Contexto ctx = new Contexto();
            return ctx.ImportaArquivos.Where(i => i.data == data & i.id_tipoimp == tipo.id_tipoimp).FirstOrDefault();
        }
    }
}
