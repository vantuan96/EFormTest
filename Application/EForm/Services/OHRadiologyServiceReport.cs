using Bussiness;
using DataAccess.Models;
using EForm.Client;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace EForm.Services
{
    public class OHRadiologyServiceReport
    {
        private string strXml;
        private readonly string serviceUrl;
        private WebRequestHandler handler;
        public OHRadiologyServiceReport()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var certificateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["OHCertificateFilePath"]);
            var certificateFilePassword = ConfigurationManager.AppSettings["OHCertificatePassword"];
            X509Certificate2 certificate = new X509Certificate2(certificateFilePath, certificateFilePassword);
            handler = new WebRequestHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
            handler.ClientCertificates.Add(certificate);
            serviceUrl = ConfigurationManager.AppSettings["OH_API_CDHA"];

            strXml = @"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:int=""http://www.orionhealth.com/his/integration"">
                                       <soap:Header xmlns:wsa=""http://www.w3.org/2005/08/addressing"" xmlns=""http://schemas.orionhealth.com/2019/12/01/ws/platform/authentication"">
                                            <wsa:Action>http://www.orionhealth.com/his/integration/IRadiologyResultService/GetByReportId</wsa:Action>
                                            <wsa:To>{2}</wsa:To>
                                        </soap:Header>
                                       <soap:Body>
                                          <int:GetByReportId>
                                             <int:userName>{0}</int:userName>
                                             <int:reportId>{1}</int:reportId>
                                             <int:dataCultureLocal>1</int:dataCultureLocal>
                                          </int:GetByReportId>
                                       </soap:Body>
                                    </soap:Envelope>";
        }

        public KeyValuePair<HttpStatusCode, string> ExeRequet(string reportId, string userName)
        {
            if (string.IsNullOrEmpty(reportId) || string.IsNullOrEmpty(userName))
                return new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.BadRequest, "Tham số chưa chính xác!");
          
            XmlDocument doc = new XmlDocument();
            var strXml = MappingParamWithXml(reportId, userName);
            doc.LoadXml(strXml);


            using (HttpClient client = new HttpClient(this.handler))
            {
                client.Timeout = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString()));
                // CustomLog.Info(string.Format("<Making SOAP request with data: {0}", strXml));
                var content = new StringContent(doc.InnerXml, Encoding.UTF8, "application/soap+xml");
                try
                {
                    var url = this.serviceUrl;
                    HttpResponseMessage response = AsyncHelper.RunSync(() => client.PostAsync(url, content));
                    // System.Threading.Tasks.Task<HttpResponseMessage> response = System.Threading.Tasks.Task.Run(() => client.PostAsync(serviceURL, content));
                    var receiveStream = response.Content.ReadAsStringAsync().Result;


                    return new KeyValuePair<HttpStatusCode, string>(response.StatusCode, receiveStream);
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null) ex = ex.InnerException;

                    return new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.InternalServerError, ex.GetBaseException().Message);
                }
            }
        }

        private string MappingParamWithXml(string reportId, string userName)
        {
            string _str = this.strXml;
            var str_result = string.Format(_str, userName, reportId, this.serviceUrl);

            return str_result;
        }
    }
}