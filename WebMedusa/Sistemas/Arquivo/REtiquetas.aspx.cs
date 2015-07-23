using System;
using System.Collections.Generic;
using System.Linq;
using Medusa.BLL;
using Medusa.LIB;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Microsoft.Reporting.WebForms;
using Medusa.Relatorios.Arquivo;
using Medusa.DAL;

namespace Medusa.Sistemas.Arquivo
{
    public partial class REtiquetas : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var volBLL = new VolumeBLL();
                //ReportViewer1.Visible = false;
            }
        }

        protected void GerarRelatorio()
        {
            //ReportViewer1.Visible = true;
            //var r = new EtiquetaArquivo();
            //ReportDataSource rpd = new ReportDataSource("dsArquivo", r.GetEtiquetas(cInteiroDe.Value.GetValueOrDefault(),
            //cInteiroAte.Value.GetValueOrDefault()));
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rpd);
            //ReportViewer1.LocalReport.Refresh();
        }

        protected void btGerarRelatorio_Click(object sender, EventArgs e)
        {
            Util.NovaJanela(String.Format("../../Relatorios/Arquivo/ReportArquivo.aspx?pk1={0}&pk2={1}", Convert.ToString(cInteiroDe.Value.GetValueOrDefault()).Criptografar(),
                Convert.ToString(cInteiroAte.Value.GetValueOrDefault()).Criptografar()));
            //GerarRelatorio();
            //Session["de"] = cInteiroDe.Value.GetValueOrDefault();
            //Session["ate"] = cInteiroAte.Value.GetValueOrDefault();
            //Session["top"] = cInteiroTop.Value.GetValueOrDefault();
            //Session["bottom"] = cInteiroBottom.Value.GetValueOrDefault();
            //Session["left"] = cInteiroLeft.Value.GetValueOrDefault();
            //Session["right"] = cInteiroRight.Value.GetValueOrDefault();
            //Util.NovaJanela("../../Sistemas/Arquivo/PdfEtiquetas.aspx");
        }
    }
}