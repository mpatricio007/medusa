using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data;
using Medusa.BLL;

namespace Medusa.BLL
{
    public class ReciboBLL : Abstract_Crud<Recibo>
    {
        public override void Add()
        {
            var usuDemonstrativo = new vUsuariosDemonstrativoBLL();

            ObjEF.cpfUsuario = usuDemonstrativo.GetCurrentvUsuariosDemonstrativoCPF();
            ObjEF.status_recibo = true;
            base.Add();
        }

        public override void Delete()
        {
            ObjEF.status_recibo = false;
        }

        public bool Exists()
        {
            return ObjEF.id_recibo != 0;
        }

        public IEnumerable<Recibo> Find(List<Filter> lstFilters, string sortExpression, string sortDirection, int top, int id_recibo_curso)
        {
            var vBLL = new vUsuariosDemonstrativoBLL();
            vBLL.GetCurrentvUsuariosDemonstrativo();
            var projs = vBLL.GetProjetos().Select(it => it.id_projeto);
            var customFilters = new List<Filter>();
            
            if (id_recibo_curso != 0)
            {
                var rc = _dbContext.ReciboCursos.Find(id_recibo_curso);
                customFilters.Add(new Filter()
                {
                    mode = "==",
                    property = "id_recibo_curso",
                    property_name = "Curso",
                    displayValue = rc.nome,
                    value = id_recibo_curso.ToString()
                });
            }

            foreach (var item in customFilters)
            {
                if (!lstFilters.Contains(item))
                    lstFilters.Add(item);
            }

            var ds = top == 0 ? _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList() :
                _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();

            return ds.Where(it => projs.Contains(it.ReciboCurso.id_projeto)).ToList();
        }

        public IEnumerable<Recibo>  FindConsulta(List<Filter> lstFilters, string sortExpression, string sortDirection, int top, int id_recibo_curso)
        {
 
          
            var customFilters = new List<Filter>();
            
            if (id_recibo_curso != 0)
            {
                var rc = _dbContext.ReciboCursos.Find(id_recibo_curso);
                customFilters.Add(new Filter()
                {
                    mode = "==",
                    property = "id_recibo_curso",
                    property_name = "Curso",
                    displayValue = rc.nome,
                    value = id_recibo_curso.ToString()
                });
            }

            foreach (var item in customFilters)
            {
                if (!lstFilters.Contains(item))
                    lstFilters.Add(item);
            }

            var ds = top == 0 ? _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList() :
                _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();

            return ds.ToList();
        }        
    }
}