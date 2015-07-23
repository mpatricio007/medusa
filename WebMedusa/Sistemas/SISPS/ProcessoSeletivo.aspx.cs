using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.BLL;
using Medusa.DAL;
using System.Text;


namespace Medusa.Sistemas.SISPS
{
    public partial class ProcessoSeletivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cv = new ClassificacaoVagaBLL();
                var html = new StringBuilder();
                var dsClassificacoes = cv.GetAll("id_classificacao_vaga").OfType<ClassificacaoVaga>();
                html.Append("<div id='tabs'>");
                html.Append("<ul>");
                int count = 1;
                
                foreach (var item in dsClassificacoes)
                {
                    html.AppendFormat("<li><a href='#tabs-{0}'>{1}</a></li>", count, item.nome);
                    count += 1;
                }                
                html.Append("</ul>");
                count = 1;
                
                foreach (var item in dsClassificacoes)
                {
                    html.AppendFormat("<div id='tabs-{0}'>", count);
                    html.Append("<div class='multiOpenAccordion'>");
                    
                    foreach (var vaga in item.Vagas.OrderByDescending(it => it.id_vaga))
                    {
                        html.AppendFormat("<h3><a href='#'>{0}</a></h3><div><p>", vaga.descricao_vaga);

                        foreach (var edital in vaga.Editais.OrderByDescending(it => it.id_edital))
                        {
                            if (edital.data_publicacao.AddDays(7) >= DateTime.Now)
                                html.AppendFormat("<img src='../../Styles/img/arrow.gif' />{0} <img src='../../Styles/img/novo.jpeg' /><br />", edital.edital_link);
                            else
                                html.AppendFormat("<img src='../../Styles/img/arrow.gif' />{0}<br />", edital.edital_link);
                            
                        }
                        
                        //fecha div editais
                        html.Append("</p></div>");
                    }
                    //fecha div multiOpenAccordion
                    html.Append("</div>");
                    //fecha div tabs-{0}
                    html.Append("</div>");
                    count += 1;
                }
                //fecha div tabs
                html.Append("</div>");
                controleVagas.InnerHtml = html.ToString();
             
            }
        }
    }
}
 