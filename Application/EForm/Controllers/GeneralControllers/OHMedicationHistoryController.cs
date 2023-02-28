using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Client;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using EForm.Helper;
using Newtonsoft.Json;
using static EForm.Models.OrdersRequest;
using AutoMapper;
using static EForm.Models.OrdersResponse;
using System.Configuration;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class OHMedicationHistoryController : BaseApiController
    {
        [HttpGet]
        [Route("api/OHMedicationHistory/{pid}")]
        public IHttpActionResult GetMedicationHistory(string pid)
        {
            var _url = System.Configuration.ConfigurationManager.AppSettings["OH_API_SERVER_URL_ORDERS"];
            var loger = new LogModel()
            {
                URI = _url,
                StartAt = DateTime.Now,
            };

            try
            {
                XmlDocument soapEnvelopeXml = CreateSoapEnvelope(pid);
                HttpWebRequest webRequest = CreateWebRequest(_url);
                var start_time = DateTime.Now;
                using (Stream stream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);

                }
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        var end_time = DateTime.Now;
                        loger.EndAt = end_time;
                        loger.Response = soapResult;
                        loger.RequestTime = (end_time - start_time).ToString();
                        CustomLog.apigwlog.Info(JsonConvert.SerializeObject(loger));
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(OrdersResponse.Envelope));
                            var Envelope = (OrdersResponse.Envelope)serializer.Deserialize(new StringReader(soapResult));
                            List<MedicationOrdersResult> medicationOrdersResults = new List<MedicationOrdersResult>();
                            List<OrdersOutput> listOrdersOutput = new List<OrdersOutput>();
                            var listMedicationOrdersResult = Envelope.Body.ListMedicationOrdersResponse.ListMedicationOrdersResult;
                            for (int i = 0; i < listMedicationOrdersResult.Count(); i++)
                            {
                                MedicationOrder medicationOrder = (MedicationOrder)listMedicationOrdersResult[i];
                                listOrdersOutput.Add(Mapper.Map<MedicationOrder, OrdersOutput>(medicationOrder));
                            }
                            return Content(HttpStatusCode.OK, new
                            {
                                Orders = listOrdersOutput
                            });
                        }
                        else
                        {

                            return Content(HttpStatusCode.NotFound, "Error From OH");

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                CustomLog.errorlog.Info(ex.ToString);
                return Content(HttpStatusCode.InternalServerError, ex);
            }

        }
        private XmlDocument CreateSoapEnvelope(string pid)
        {
            var _url = System.Configuration.ConfigurationManager.AppSettings["OH_API_SERVER_URL_ORDERS"];
            string body = buildXml(pid);

            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(body);


            var start_time = DateTime.Now;

            var loger = new LogModel()
            {
                URI = _url,
                StartAt = DateTime.Now,
            };
            loger.Request = body;
            var end_time = DateTime.Now;
            loger.RequestTime = (end_time - start_time).ToString();
            CustomLog.apigwlog.Info(JsonConvert.SerializeObject(loger));


            return soapEnvelopeDocument;
        }
        private static HttpWebRequest CreateWebRequest(string url)
        {

            string certName = AppDomain.CurrentDomain.BaseDirectory + "\\" + System.Configuration.ConfigurationManager.AppSettings["OHCertificateFilePath"];
            string password = ConfigurationManager.AppSettings["OHCertificatePassword"];
            X509Certificate2 certificate = new X509Certificate2(certName, password);

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
            webRequest.ClientCertificates.Add(certificate);

            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "application/soap+xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            return webRequest;
        }
        public string buildXml(string pid)
        {

            ListMedicationOrdersFilterPatient patient = new ListMedicationOrdersFilterPatient
            {
                Value = uint.Parse(pid),
                System = "urn:orion:pas:core:patient:id"
            };

            ListMedicationOrdersFilter filter = new ListMedicationOrdersFilter();
            filter.Patient = patient;

            ListMedicationOrders listMedicationOrders = new ListMedicationOrders();
            listMedicationOrders.filter = filter;

            ListMedicationOrders listOrder = new ListMedicationOrders();
            listOrder = listMedicationOrders;


            OrdersRequest.EnvelopeBody body = new OrdersRequest.EnvelopeBody();
            body.ListMedicationOrders = listOrder;

            OrdersRequest.Envelope envelope = new OrdersRequest.Envelope();
            OrdersRequest.EnvelopeHeader header = new OrdersRequest.EnvelopeHeader();
            header.Action = "http://orionhealth.com/pharmacy/orders/IMedicationOrderService/ListMedicationOrders";
            header.To = "https://svm-ent-uat.vingroup.local/OrionHealthComponents/soap-cert/v20.2/Orchestral.Pharmacy.Orders.Services.MedicationOrderService.svc";
            envelope.Header = header;
            envelope.Body = body;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("soap", "http://www.w3.org/2003/05/soap-envelope");
            ns.Add("ord", "http://orionhealth.com/pharmacy/orders");
            ns.Add("cor", "http://schemas.orionhealth.com/2020/08/01/ws/platform/coretypes");
            ns.Add("dir", "http://schemas.orionhealth.com/2017/08/01/ws/common/directory");

            XmlSerializer serializer = new XmlSerializer(typeof(OrdersRequest.Envelope));

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = true,
            };
            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder, settings))
            {

                serializer.Serialize(writer, envelope, ns);
            }
            return builder.ToString();

        }
    }


}
