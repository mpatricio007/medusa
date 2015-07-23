using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Web.Security;


namespace Medusa.BLL
{
    public class vUsuariosDemonstrativoBLL : AbstractCrudWithLog<vUsuariosDemonstrativo>
    {        
        public bool Login(string strCpf, string strSenha)
        {
            
            bool rt = false;
            Get(strCpf);

            rt = ObjEF.senha == strSenha & !String.IsNullOrEmpty(ObjEF.cpf);

            if (rt)
            {
                System.Web.HttpContext.Current.Session["cpf_usuMySQL"] = ObjEF.cpf;
                GetProjetos();
            }

            return rt;
        }

        public bool PerfilIsFUSP()
        {
            return ObjEF.perfil == "F";
        }

        public List<Projeto> GetProjetos()
        {
            var lst = (List<Projeto>)System.Web.HttpContext.Current.Session[String.Format("projetos{0}", System.Web.HttpContext.Current.Session["cpf_usuMySQL"])];
            if (lst == null)
            {
                if (PerfilIsFUSP())
                    System.Web.HttpContext.Current.Session[String.Format("projetos{0}", System.Web.HttpContext.Current.Session["cpf_usuMySQL"])] = _dbContext.Projetos.Where(it => it.cod_def_projeto.HasValue).OrderBy(it => it.codigo).ToList();

                else
                    System.Web.HttpContext.Current.Session[String.Format("projetos{0}", System.Web.HttpContext.Current.Session["cpf_usuMySQL"])] = ObjEF.vUsuariosProjetosDemonstrativo.OrderBy(it => it.Projeto.codigo).Select(it => it.Projeto).ToList();
            }
            return lst;
        }

        public List<ReciboCurso> GetRecibosCursos()
        {
            var x = GetProjetos().Select(it => it.id_projeto).ToList();
            return _dbContext.ReciboCursos.Where(it =>  x.Contains(it.id_projeto)).OrderBy(it => it.nome).ToList();         
        }

        public vUsuariosDemonstrativo GetCurrentvUsuariosDemonstrativo()
        {
            
            Get(System.Web.HttpContext.Current.Session["cpf_usuMySQL"]);
            if (String.IsNullOrEmpty(objEF.cpf))
            {
                Medusa.LIB.Util.ResponseToPage("http://demonstrativo.fusp.org.br", "Sua sessão acabou!");

                System.Web.HttpContext.Current.Session.Clear();
                FormsAuthentication.SignOut();
            }
            return ObjEF;
        }

        public string GetCurrentvUsuariosDemonstrativoCPF()
        {
            string cpf = Convert.ToString(System.Web.HttpContext.Current.Session["cpf_usuMySQL"]);
            
            if (String.IsNullOrEmpty(cpf))
            {
                Medusa.LIB.Util.ResponseToPage("http://demonstrativo.fusp.org.br", "Sua sessão acabou!");

                System.Web.HttpContext.Current.Session.Clear();
                FormsAuthentication.SignOut();
            }
            return cpf;
        }
    }
}
