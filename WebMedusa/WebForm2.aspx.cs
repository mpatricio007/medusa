using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using Medusa.LIB;
using System.Text;

namespace Medusa
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string ftp = "ftp://fusp.org.br";
        string user = "fusp5";
        string password = "Medusa01";
        string localpath = @"C:\Users\marcelo.INTRANET\Desktop\teste";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["id_usuario"] = 3;
                Session["id_sistema"] = 5;
                //Upload(@"C:\Users\marcelo.INTRANET\Desktop\xpto.txt", "xpto.txt");

                //GridView1.DataSource = GetFileList().Where(it => it.EndsWith(".txt"));
                //GridView1.DataBind();


                //foreach (var item in GetFileList().Where(it => it.EndsWith(".txt")))
                //{
                //    Download(item);
                //}
            }
        }


        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(user, password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch (Exception)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }


        private void Download(string file)
        {                       
            try
            {                
                
                Uri serverUri = new Uri(ftp);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return;
                }       
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp + "/" + file));                             
                reqFTP.Credentials = new NetworkCredential(user, password);                
                reqFTP.KeepAlive = false;                
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;                                
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;                 
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(localpath + @"\" + file, FileMode.Create);                
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);               
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }                
                writeStream.Close();
                response.Close(); 
            }
            catch (WebException wEx)
            {
                Util.ShowMessage(wEx.Message + " Download Error");
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message + " Download Error");
            }
        }


        private void Upload(string oldfile,string newfile)
        {
            try
            {

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + "/" + newfile);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(user, password);

                // Copy the contents of the file to the request stream.
                StreamReader sourceStream = new StreamReader(oldfile);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException wEx)
            {
                Util.ShowMessage(wEx.Message + " Upload Error");
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message + " Download Error");
            }
        }
    }
}