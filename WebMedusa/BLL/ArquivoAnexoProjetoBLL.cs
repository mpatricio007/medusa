using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;

namespace Medusa.BLL
{
    public class ArquivoAnexoProjetoBLL : AbstractCrudWithLog<ArquivoAnexoProjeto>
    {
        //public Upload up { get; set; }

        public ArquivoBLL arqBLL { get; set; }

        public static string[] permittedExtensions
        {
            get
            {
                return new string[] { ".pdf", ".zip", ".docx" };
            }
        }

        public bool Exists()
        {
            return ObjEF.id_projeto != 0;
        }

        public bool SalvarAnexo()
        {
            bool rt = false;
            if (arqBLL.SendFile(ObjEF.nome_arquivo))
            {
                ObjEF.id_arquivo = arqBLL.ObjEF.id_arquivo;
                base.Add();
                rt = SaveChanges();
            }
            return rt;
        }

        public bool ExcluirAnexo(int id_edital)
        {
            Get(id_edital);
            arqBLL.Get(ObjEF.id_arquivo);
            arqBLL.Delete();
            base.Delete();
            return SaveChanges();
        }

        public ArquivoAnexoProjetoBLL() { arqBLL = new ArquivoBLL(_dbContext); }

        public ArquivoAnexoProjetoBLL(Contexto ctx)
            : base(ctx)
        {  
            arqBLL = new ArquivoBLL(ctx);
        }

        public override bool DataIsValid(ref string strMsg)
        {
           return true;
        }

        private void salvarLog()
        {
            var log = new LogSistemaBLL();
            log.ObjEF.acao = "consulta";
            log.ObjEF.entidade = "ArquivoProjeto";
            log.ObjEF.id_entidade = ObjEF.id_arquivo_anexo_proj;
            log.Add();
            log.SaveChanges();

        }
    }
}
