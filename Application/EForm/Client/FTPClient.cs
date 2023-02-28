using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace EForm.Client
{
    public class FTPClient
    {
        private static readonly string ftp_server = System.Configuration.ConfigurationManager.AppSettings["FTP_SERVER"];
        private static readonly string ftp_user = System.Configuration.ConfigurationManager.AppSettings["FTP_USER"];
        private static readonly string ftp_pass = System.Configuration.ConfigurationManager.AppSettings["FTP_PASS"];


        public static string CreateDirectory(string directory)
        {

            try
            {
                string path = string.Format("{0}{1}", ftp_server, directory);
                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(ftp_user, "");
                requestDir.EnableSsl = false;
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
                return directory;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool UploadFile(string path, HttpPostedFile file)
        {
            try
            {
                var uri = new Uri(string.Format("{0}{1}", ftp_server, path));
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftp_user.Normalize(), ftp_pass.Normalize());
                StreamReader sourceStream = new StreamReader(file.InputStream);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void DownloadFile()
        {

        }
    }
}