using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.IO;

namespace Medusa.BLL
{
    public class AssinBLL : AbstractCrudWithLog<Assin>
    {
        public Upload up { get; set; }

        public override void Add()
        {            
            base.Add();
            if (up.file != null)
            {
                if (base.SaveChanges())
                {
                    saveFile();
                    base.Update();
                }
            }
        }

        

        public override void Delete()
        {
            deleteFile();
            base.Delete();
        }

        public override void Update()
        {
            deleteFile();
            saveFile();
            base.Update();
        }
        
        protected void saveFile()
        {
            if (up.file != null)
            {
                up.where_upload = System.Web.HttpContext.Current.Server.MapPath("\\assin\\");
                up.fileName = Convert.ToString(ObjEF.id_assin);
                if (up.Do())
                    ObjEF.nome_arquivo = String.Format(@"../../assin/{0}{1}", up.fileName, up.fileExtension);
            }
        }

        protected void deleteFile()
        {
            if (!String.IsNullOrEmpty(ObjEF.nome_arquivo))
            {
                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\assin\\")).
                    Where(it => it.Contains(Convert.ToString(ObjEF.id_assin))).ToArray();
                foreach (string f in files)
                    File.Delete(f);
            }
        }

        public bool GetAssin(int cod_proj, string sub,ref string msg)
        {   
            if (String.IsNullOrEmpty(sub))
                ObjEF = _dbSet.Where(it => it.Projeto.cod_def_projeto == cod_proj & it.Projeto.sub_projeto == null).OrderByDescending(it => it.validade).FirstOrDefault();
            else
                if (sub == "00")
                   ObjEF = _dbSet.Where(it => it.Projeto.cod_def_projeto == cod_proj & it.Projeto.sub_projeto == "00").OrderByDescending(it => it.validade).FirstOrDefault();
                else
                    ObjEF = _dbSet.Where(it => it.Projeto.cod_def_projeto == cod_proj & it.Projeto.sub_projeto == sub).OrderByDescending(it => it.validade).FirstOrDefault();

            return isValid(ref msg);
        }

        public bool GetAssin(int id_projeto,ref string msg)
        {
            ObjEF = _dbSet.Where(it => it.id_projeto == id_projeto).OrderByDescending(it => it.id_assin).FirstOrDefault();

            return isValid(ref msg);
        }

        private bool isValid(ref string msg)
        {
            if (ObjEF.id_assin == 0)
            {
                msg = "cartão de assinatura não encontrado";
                return false;
            }

            else if (ObjEF.validade >= DateTime.Today)
            {
                msg = String.Format("cartão de assinatura valido até {0:d}", ObjEF.validade);
                salvarLog();
                return true;
            }
            else
            {
                msg = String.Format("cartão de assinatura vencido em {0:d}", ObjEF.validade);
                salvarLog();
                return false;
            }
        }

        private void salvarLog()
        {
            var log = new LogSistemaBLL();
            log.ObjEF.acao = "consulta";
            log.ObjEF.entidade = "Assin";
            log.ObjEF.id_entidade = ObjEF.id_assin;
            log.Add();
            log.SaveChanges();

        }
    }
}
