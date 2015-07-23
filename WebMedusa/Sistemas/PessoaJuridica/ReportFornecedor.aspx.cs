using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.DAL;
using Medusa.Relatorios.PessoaJuridica;
using Medusa.LIB;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class ReportFornecedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var rvFornecedor = new ReportViewer();
                    rvFornecedor.LocalReport.ReportPath = @"Relatorios\PessoaJuridica\RelatorioFornecedor.rdlc";
                    var r = new RelatorioFornecedor();
                    ReportDataSource rpd = new ReportDataSource("dsRelatorioFornecedor", r.GerarRelatorio(pkValue));
                    ReportDataSource rpd1 = new ReportDataSource("DataSet1", r.GetRepresentanteLegal(pkValue));
                    rvFornecedor.LocalReport.DataSources.Clear();
                    rvFornecedor.LocalReport.DataSources.Add(rpd);
                    rvFornecedor.LocalReport.DataSources.Add(rpd1);
                    rvFornecedor.LocalReport.Refresh();

                    Util.ExportReportToPDF(rvFornecedor);
                  
                }
            }
        }
    }
}