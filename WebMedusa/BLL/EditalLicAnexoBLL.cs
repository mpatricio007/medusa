using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;
using System.Web;

namespace Medusa.BLL
{
    public class EditalLicAnexoBLL : AbstractCrudWithLog<EditalLicAnexo>
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
            return ObjEF.id_edital_lic != 0;
        }

        public bool SalvarAnexo()
        {
            bool rt = false;
            if (arqBLL.SendFile(ObjEF.descricao))
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

        public EditalLicAnexoBLL() { arqBLL = new ArquivoBLL(_dbContext); }

        public EditalLicAnexoBLL(Contexto ctx): base(ctx)
        {  
            arqBLL = new ArquivoBLL(ctx);
        }

        public override bool DataIsValid(ref string strMsg)
        {
            //if (!permittedExtensions.Contains(up.fileExtension))
            //{
            //    strMsg = "nenhum arquivo adicionado / extenção de arquivo não permitida";
            //    return false;
            //}
            return true;
        }
    }
}
