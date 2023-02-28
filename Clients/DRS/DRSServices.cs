using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Configuration;
using DataAccess.Models;
using System.Threading.Tasks;
using Helper;

namespace Clients.DRS
{
    public class DRSServices
    {
        public static async Task<KeyValuePair<HttpStatusCode, string>> MakeSoapRequestAsync(string strXml)
        {
            var max_connec = ConfigurationManager.AppSettings["MaxConnectionsPerServer"] != null ? int.Parse(ConfigurationManager.AppSettings["MaxConnectionsPerServer"].ToString()) : 100;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var certificateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["OHCertificateFilePath"]);
            var certificateFilePassword = ConfigurationManager.AppSettings["OHCertificatePassword"];
            X509Certificate2 certificate = new X509Certificate2(certificateFilePath, certificateFilePassword);
            WebRequestHandler handler = new WebRequestHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
            handler.ClientCertificates.Add(certificate);
            handler.MaxConnectionsPerServer = int.MaxValue;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);

            LogTmp log = new LogTmp
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Ip = ":1",
                URI = ConfigurationManager.AppSettings["OHLaboratoryServiceURL"],
                Action = "INFO",
                Request = strXml,
                Reason = "DONE"
            };

            using (HttpClient client = new HttpClient(handler))
            {
                // HandleApiILog.InfoToTmpLog(string.Format("<Making SOAP request with data: {0}", strXml));
                HttpResponseMessage response = null;
                var content = new StringContent(doc.InnerXml, Encoding.UTF8, "application/soap+xml");
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString()));

                    response = await client.PostAsync(ConfigurationManager.AppSettings["OHLaboratoryServiceURL"], content);

                    var receiveStream = await response.Content.ReadAsStringAsync();
                    //response.Content.ReadAsStringAsync().Result;

                    log.Response = string.Format("{0}-{1}", response.StatusCode, receiveStream);
                    log.UpdatedAt = DateTime.Now;
                    HandleApiILog.InfoToTmpLog(log);
                    return new KeyValuePair<HttpStatusCode, string>(response.StatusCode, receiveStream);
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null) ex = ex.InnerException;
                    // HandleApiILog.InfoToTmpLog(string.Format("<Making SOAP error: {0}", ex.GetBaseException().Message));

                    log.Response = string.Format("{0}-{1}", "500", ex.GetBaseException().Message);
                    log.UpdatedAt = DateTime.Now;
                    log.Reason = "ERROR";
                    HandleApiILog.InfoToTmpLog(log);
                    return new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
        }

        public static async Task<KeyValuePair<HttpStatusCode, string>> MakeSoapRequestBasicAuthAsync(string strXml, string serviceURL, string action)
        {
            var authenticationString = $"{ConfigurationManager.AppSettings["OHServiceUsername"]}:{ConfigurationManager.AppSettings["OHServicePassword"]}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);

            LogTmp log = new LogTmp
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Ip = ":1",
                URI = serviceURL,
                Action = "INFO",
                Request = strXml,
                Reason = "DONE"
            };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                client.DefaultRequestHeaders.Add("SOAPAction", action);
                client.Timeout = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString()));
                // HandleApiILog.InfoToTmpLog(string.Format("<Making SOAP request with data: {0}", strXml));
                HttpResponseMessage response = null;
                var content = new StringContent(doc.InnerXml, Encoding.UTF8, "text/xml");
                try
                {
                    response = await client.PostAsync(serviceURL, content);
                    // System.Threading.Tasks.Task<HttpResponseMessage> response = System.Threading.Tasks.Task.Run(() => client.PostAsync(serviceURL, content));
                    // var receiveStream = response.Content.ReadAsStringAsync().Result;

                    var receiveStream = await response.Content.ReadAsStringAsync();

                    log.Response = string.Format("{0}-{1}", response.StatusCode, receiveStream);
                    log.UpdatedAt = DateTime.Now;
                    HandleApiILog.InfoToTmpLog(log);

                    return new KeyValuePair<HttpStatusCode, string>(response.StatusCode, receiveStream);
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null) ex = ex.InnerException;

                    log.Response = string.Format("{0}-{1}", "500", ex.GetBaseException().Message);
                    log.UpdatedAt = DateTime.Now;
                    log.Reason = "ERROR";
                    HandleApiILog.InfoToTmpLog(log);

                    return new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
        }

    }
}
