using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using System.IO;

namespace Medusa.BLL
{
    public class ComodatoBLL: AbstractCrudWithLog<Comodato>
    {
        private Upload up;

        public Upload Up
        {
            get
            {
                if (up == null)
                    up = new Upload();
                return up;
            }
            set { up = value; }
        }

        public override void Add()
        {
            var status = _dbContext.StatusComodatos.SingleOrDefault(it => it.ordem == 0);
            ObjEF.StatusComodatos = status;
            ObjEF.Historicos = new List<HistoricoComodato>();
            var primeiroStatus = new HistoricoComodato();
            primeiroStatus.StatusComodatos = status;
            primeiroStatus.data = DateTime.Now;
            primeiroStatus.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.Historicos.Add(primeiroStatus);
            
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

        public void SalvarUltimoStatus()
        {
            if (ObjEF.Historicos.Count() > 0)
            {
                int LAST = ObjEF.Historicos.OrderByDescending(it => it.id_historico).First().id_status_comodato;
                ObjEF.id_ultimo_status = ObjEF.Historicos.OrderByDescending(it => it.id_historico).First().id_status_comodato;
                Update();
                SaveChanges();
                LAST = ObjEF.Historicos.OrderByDescending(it => it.id_historico).First().id_status_comodato;
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
            if (Up.file != null)
            {
                up.where_upload = System.Web.HttpContext.Current.Server.MapPath("\\comodatos\\");
                up.fileName = Convert.ToString(ObjEF.id_comodato);
                if (up.Do())
                    ObjEF.arquivo = String.Format(@"<a href='../../comodatos/{0}{1}' target='blank'>arquivo</a>", up.fileName, up.fileExtension);
            }
        }

        protected void deleteFile()
        {
            if (!String.IsNullOrEmpty(ObjEF.arquivo) & (Up.file != null))
            {

                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("\\comodatos\\")).
                    Where(it => it.Contains(Convert.ToString(ObjEF.id_comodato))).ToArray();
                foreach (string f in files)
                    File.Delete(f);
            }
        }

        public IEnumerable<Comodato> Find(List<Filter> lstFilters, string sortExpression, string sortDirection, int top, int id_financiador)
        {
            var customFilters = new List<Filter>();

            if (id_financiador != 0)
            {
                var projetosFinanciadores = _dbContext.ProjetoFinanciadores.Where(it => it.id_financiador == id_financiador);

                if (projetosFinanciadores.Count() == 0)
                {
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_projeto",
                        property_name = "id_projeto",
                        value = "0"
                    });
                }

                foreach (var pf in projetosFinanciadores)
                {
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_projeto",
                        property_name = "id_projeto",
                        value = pf.id_projeto.ToString()
                    });
                }
            }

            foreach (var item in customFilters)
            {
                if (!lstFilters.Contains(item))
                    lstFilters.Add(item);
            }


            var ds = top == 0 ? _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList() :
                _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
            return ds;
        }
    }
}
