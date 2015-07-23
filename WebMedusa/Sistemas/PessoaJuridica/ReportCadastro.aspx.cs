using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Medusa.Relatorios.PessoaJuridica;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Sistemas.PessoaJuridica
{
    public partial class ReportCadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pkValue = Convert.ToInt32(HttpContext.Current.Request.QueryString["pk"].DesCriptografar());
                if ((int)pkValue != 0)
                {
                    var rvCadastroFornecedor = new ReportViewer();
                    rvCadastroFornecedor.LocalReport.ReportPath = @"Relatorios\PessoaJuridica\RelatorioCadastroFornecedor.rdlc";
                    var r = new RelatorioFornecedor();
                    ReportDataSource rpd = new ReportDataSource("dsCadastroFornecedor", r.GerarRelatorio(pkValue));
                    ReportDataSource rpd1 = new ReportDataSource("dsRepresentanteLegal", r.GetRepresentanteLegal(pkValue));
                    ReportDataSource rpd2 = new ReportDataSource("dsSocios", r.GetSocios(pkValue));
                    ReportDataSource rpd3 = new ReportDataSource("dsDiretores", r.GetDiretor(pkValue));
                    ReportDataSource rpd4 = new ReportDataSource("dsDocumentos", r.GetDocumentos(pkValue));
                    ReportDataSource rpd5 = new ReportDataSource("dsReferenciaBancaria", r.GetReferenciaBancaria(pkValue));
                    rvCadastroFornecedor.LocalReport.DataSources.Clear();
                    rvCadastroFornecedor.LocalReport.DataSources.Add(rpd);
                    rvCadastroFornecedor.LocalReport.DataSources.Add(rpd1);
                    rvCadastroFornecedor.LocalReport.DataSources.Add(rpd2);
                    rvCadastroFornecedor.LocalReport.DataSources.Add(rpd3);
                    rvCadastroFornecedor.LocalReport.DataSources.Add(rpd4);
                    rvCadastroFornecedor.LocalReport.DataSources.Add(rpd5);
                    rvCadastroFornecedor.LocalReport.Refresh();

                    Util.ExportReportToPDF(rvCadastroFornecedor);
                }
            }
        }
    }
}