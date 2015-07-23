using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Reporting.WebForms;

namespace Medusa.Relatorios.Conciliacao
{
    public partial class RelLanctoImportado : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ReportViewer1_Init(null, null);
        }

        protected void GerarRelatorio()
        {
        }

        protected void ReportViewer1_Init(object sender, EventArgs e)
        {
            if(Session["id_imparq"] == null) Response.Redirect("~/sistemas/conciliacao/ImportacaoDeDados.aspx");
            ImportaArquivoBLL impBLL = new ImportaArquivoBLL();
            impBLL.Get(Convert.ToInt32(Session["id_imparq"]));
            string titulo = String.Format("tipo: {0} data: {1:d}", impBLL.ObjEF.TipoArquivo.descricao, impBLL.ObjEF.data);
            ReportParameter[] parameters = new ReportParameter[1];            
            parameters[0] = new ReportParameter("DescrFiltroUtilizado", titulo);
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = @"Relatorios\Conciliacao\RelDadosImportados.rdlc";
            ReportViewer1.LocalReport.SetParameters(parameters);
            var r = new RelatorioLanctosImportados();
            ReportDataSource rpd = new ReportDataSource("DS_Lancto", r.GerarRelatorio(impBLL.ObjEF.id_imparq));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rpd);
            ReportViewer1.LocalReport.Refresh();

            Session[PageOfReport.SessionName] = ReportViewer1;
            dRelatorio.InnerHtml = PageOfReport.iframe;
            dRelatorio.DataBind();

        }
   
        
    }
}