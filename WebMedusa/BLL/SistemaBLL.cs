using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
 
namespace Medusa.BLL
{
    public class SistemaBLL: AbstractCrudWithLog<Sistema>
    {
        public string CreateHtmlMenu()
        {
            
            if (ObjEF.id_sistema == 0)
                return String.Empty;
            StringBuilder menu = new StringBuilder();            
            menu.Append("<ul id=\"menu1\" class=\"topmenu\">");                  
            foreach (Menu mn in ObjEF.Menus.OrderBy(t=>t.ordem))
            {
                //abre o menu
                menu.Append(String.Format("<li class=\"topfirst\"><a href=\"#\" title=\"{0}\" style=\"height:15px;line-height:15px;\">{1}</a>",
                mn.descricao, mn.nome));
                if (mn.MenuPaginas.Count > 0)
                {
                    //abre a pagina
                    menu.Append("<ul>");
                    foreach (MenuPagina mpg in mn.MenuPaginas.Where(it => it.Pagina.tipo == TipoPagina.pagina).OrderBy(y=>y.ordem))
                        menu.Append(String.Format("<li class=\"subfirst\"><a href=\"../../{0}\" title=\"{1}\">{1}</a></li>", mpg.Pagina.url, mpg.Pagina.nome));
                    //fecha pagina
                    menu.Append("</ul>");
                }
                //fecha menu
                menu.Append("</li>");
            }
            menu.Append("</ul>");
            return menu.ToString();
        }

        public IEnumerable<Sistema> GetSistemasDisponiveis(int intId_usuario)
        {   
            var ds = from sisUsu in _dbContext.UsuarioSistemas
                     where sisUsu.id_usuario == intId_usuario
                     select sisUsu.id_sistema;
            

            return (from s in _dbContext.Sistemas
                   where
                        !ds.Contains(s.id_sistema)
                       orderby s.nome
                   select s).ToList();
            

        }

        public string ResponseUrl(Int32 intId_Sistema)
        {
            Get(intId_Sistema);
            return string.Format("{0}/{1}","~",ObjEF.Pagina.url);
        }
    }
}
