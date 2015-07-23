using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medusa.LIB;
using Microsoft.Reporting.WebForms;

namespace Medusa.Relatorios
{
    public partial class PageOfReport : System.Web.UI.Page
    {
        public static string ThisPath
        {
            get
            {
                return @"../../Relatorios/PageOfReport.aspx";
            }
        }

        public static string SessionName
        {
            get
            {
                return "CurrentReportViewer";
            }
        }

        public static string iframe
        {
            get
            {
                return String.Format("<iframe src=\"{0}\" width=\"100%\" height=\"1000px\"></iframe>", ThisPath);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Util.ExportReportToPDF((ReportViewer)Session[SessionName]);
            }
        }
    }
}