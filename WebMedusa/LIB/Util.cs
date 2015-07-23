using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using System.Globalization;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Medusa.LIB
{
    public class Util
    {
        public static string TirarFormatoConta(string cta)
        {           
            return cta.Substring(0, cta.Length - 1).Replace(".", "").Replace("-", "");
        }

        public static string DateToString(DateTime? date)
        {
            return (!date.HasValue || date == DateTime.MinValue) ? String.Empty : date.GetValueOrDefault().ToShortDateString(); 
        }

        public static string DateToString(DateTime? date,string strFormat)
        {
            return (!date.HasValue || date == DateTime.MinValue) ? String.Empty : date.GetValueOrDefault().ToString(strFormat);
        }

        public static string DateToStringHoraMinutoSegundo(DateTime? date)
        {
            return DateToString(date, "HHmmss");
        }

        public static string DateToStringSemBarras(DateTime? date)
        {
            return DateToString(date, "ddMMyyyy");
        }

        public static DateTime? StringToDate(string strData)
        {
            DateTime dtOut;
            if (DateTime.TryParse(strData, out dtOut))
                return dtOut;
            else
                return null;
        }

        public static DateTime? StringSemBarrasToDate(string strData)
        {
            DateTime dtOut;
            if (DateTime.TryParseExact(strData, "ddMMyyyy", new CultureInfo("pt-BR"),DateTimeStyles.None, out dtOut))
                return dtOut;
            else
                return null;
        }

        public static DateTime? StringSemBarrasToDate(string strData,string strHora)
        {
            DateTime dtOut;
            if (DateTime.TryParseExact(String.Format("{0} {1}",strData,strHora) , "ddMMyyyy HHmmss", new CultureInfo("pt-BR"), DateTimeStyles.None, out dtOut))
                return dtOut;
            else
                return null;
        }

        public static string DecimalToString(Decimal? valor)
        {
            return (!valor.HasValue) ? String.Empty : String.Format("{0:N2}",valor.GetValueOrDefault());
        }

        public static string DecimalToStringSoDigitos(Decimal? valor)
        {
            return (!valor.HasValue) ? String.Empty : String.Format("{0}",Convert.ToInt64(valor.GetValueOrDefault() * 100));
        }

        public static Decimal? StringSemPontosToDecimal(string strDecimal)
        {
            Decimal decOut;
            if (Decimal.TryParse(strDecimal, out decOut))
                return decOut / 100;
            else
                return null;
        }
        

        public static Decimal? StringToDecimal(string strDecimal)
        {
            Decimal decOut;
            if (Decimal.TryParse(strDecimal, out decOut))
                return decOut;
            else
                return null;
        }

        public static string InteiroToString(int? inteiro)
        {
            return (!inteiro.HasValue) ? String.Empty : inteiro.GetValueOrDefault().ToString();
        }

        public static int? StringToInteiro(string strInteiro)
        {
            int intOut;
            if (int.TryParse(strInteiro, out intOut))
                return intOut;
            else
                return null;
        }

        public static int? StringToInteiroOrNullable(string strInteiro)
        {
            if (strInteiro == "0")
                return null;
            else
                return StringToInteiro(strInteiro);
        }

        public static int? IntToInteiroOrNullable(int intInteiro)
        {
            if (intInteiro == 0)
                return new Nullable<int>();
            else
                return intInteiro;
        }

        public static void ShowMessage(string message)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            var script = new StringBuilder();
            
            script.Append("var dlg = $('<div id=\"dialog-message\" title=\"Alerta\">");            
            script.AppendFormat("<p>{0}</p></div>", message);
            script.Append("').appendTo('body');");
		    script.Append("$( \"#dialog:ui-dialog\" ).dialog( \"destroy\" );");
	        script.Append("$( \"#dialog-message\" ).dialog({modal: true,");
            script.Append("buttons: {Ok: function() {$( this ).dialog( \"close\" ); }}, ");//});");
            script.Append("close: function () { dlg.remove();}");
            script.Append("});");
            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
                ScriptManager.RegisterStartupScript( page, page.GetType(), "alert",script.ToString(), true);
            else
                page.ClientScript.RegisterStartupScript(page.GetType(),"alert",script.ToString());
        }

        public static void ShowMessage(string message,string focus)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            var script = new StringBuilder();

            script.Append("var dlg = $('<div id=\"dialog-message\" title=\"Alerta\">");
            script.AppendFormat("<p>{0}</p></div>", message);
            script.Append("').appendTo('body');");
            script.Append("$( \"#dialog:ui-dialog\" ).dialog( \"destroy\" );");
            script.Append("$( \"#dialog-message\" ).dialog({modal: true,");
            script.Append("buttons: {Ok: function() {$( this ).dialog( \"close\" ); " + "$(\"" + focus + "\").focus(); }}, ");//});");
            script.Append("close: function () { dlg.remove();}");
            script.Append("});");
            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", script.ToString(), true);
            else
                page.ClientScript.RegisterStartupScript(page.GetType(), "alert", script.ToString());
        }

        public static void ResponseToLogin(string mensagem)
        {
            ResponseToPage("../../Home.aspx", mensagem);
            
            System.Web.HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();

        }

        public static void ResponseToPage(string url,string mensagem)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            var script = new StringBuilder();

            script.Append("var dlg = $('<div id=\"dialog-message\" title=\"Alerta\">");
            script.AppendFormat("<p>{0}</p></div>", mensagem);
            script.Append("').appendTo('body');");
            script.Append("$( \"#dialog:ui-dialog\" ).dialog( \"destroy\" );");
            script.Append("$( \"#dialog-message\" ).dialog({modal: true,");
            script.Append("buttons: {Ok: function() { window.location = '" + url + "' }}, ");
            script.Append("close: function () { window.location = '" + url + "';  }");
            script.Append("});");
            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", script.ToString(), true);
            else
                page.ClientScript.RegisterStartupScript(page.GetType(), "alert", script.ToString());
            

        }

        public static void PrintArea(string id)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
            {
                StringBuilder txt = new StringBuilder();
                txt.Append("var oPrint, oJan;");
                txt.Append(String.Format("oPrint  = window.document.getElementById('{0}').innerHTML;", id));
                txt.Append("oJan= window.open();oJan.document.write(oPrint);");
                txt.Append("oJan.history.go();oJan.window.print();");
                ScriptManager.RegisterStartupScript(page, page.GetType(), "Print", txt.ToString(), true);
            }

        }

        public static void NovaJanela(string url)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
            {
                StringBuilder txt = new StringBuilder();
                txt.Append(String.Format("window.open('{0}','url');", url));                
                ScriptManager.RegisterStartupScript(page, page.GetType(), "new", txt.ToString(), true);
            }

        }

        public static void ChamarScript(string script, string scriptName)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
            {       
                ScriptManager.RegisterStartupScript(page, page.GetType(), scriptName, script.ToString(), true);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), scriptName, script.ToString());
            }

        }

        public static void ExportReportToPDF(ReportViewer report)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
            {

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                byte[] bytes = report.LocalReport.Render(
                   "PDF", null, out mimeType, out encoding,
                    out extension,
                   out streamids, out warnings);
                page.Response.Clear();
                page.Response.ContentType = "Application/pdf";
                page.Response.BinaryWrite(bytes);
                page.Response.End();
            }
        }

        public static void Bytes2File(byte[] bytes, string fileName, string contentType)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null)
            {
                page.Response.Clear();
                page.Response.Buffer = true;
                page.Response.Charset = "";
                page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                page.Response.ContentType = contentType;
                page.Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", fileName));
                page.Response.BinaryWrite(bytes);
                page.Response.End();
            }
        }
    }
}


       
  