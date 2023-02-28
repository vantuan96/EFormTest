using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using static EForm.Models.OrdersRequest;
using static EForm.Models.OrdersResponse;
using System.Configuration;
using Newtonsoft.Json.Linq;
using static EForm.Models.FileCKSModel;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class EMRCKSController : BaseApiController
    {
       private const string _action = "http://10.111.125.83/Service/kyso/serviceEsign.asmx?op=API_ESIGN_SERVER";
        [HttpPost]
        [Route("api/EMRCKS/{id}")]
        public IHttpActionResult GetEMRCKS(Guid id, [FromBody] JObject request)
        {
            var _url = "http://10.111.125.83/Service/kyso/serviceEsign.asmx";
            //var _action = "http://10.111.125.83/Service/kyso/serviceEsign.asmx?op=API_ESIGN_SERVER";
            var loger = new LogModel()
            {
                URI = _action,
                StartAt = DateTime.Now,
            };
            try
            {
                var username = request["username"]?.ToString();
                var password = request["password"]?.ToString();
                var urldoc = request["urldoc"]?.ToString();
                var urlimage = request["urlimage"]?.ToString();
                var positionX = request["positionX"]?.ToString();
                float positionXcv = float.Parse(positionX);
                var positionY = request["positionY"]?.ToString();
                float positionYcv = float.Parse(positionY);
                var withImage = request["withImage"]?.ToString();
                float withImagecv = float.Parse(withImage);
                var heightImage = request["heightImage"]?.ToString();
                float heightImagecv = float.Parse(heightImage);
                var pageIndex = request["pageIndex"]?.ToString();
                var email = request["email"]?.ToString();
                var sw = request["sw"].ToString();
                var typefollow = request["typefollow"].ToString();
                var type = request["Type"]?.ToString();
                var typeSign = request["typeSign"].ToString();
                #region convert pdf to base64
                //file doc
                var filepath_doc = ConfigurationManager.AppSettings["FilePath"] + urldoc;
                if(!File.Exists(filepath_doc)) 
                    return Content(HttpStatusCode.NotFound, new { ViMessage = "Không tìm thấy đường dẫn của file", EnMessage = "" });
                Byte[] bytespdf = File.ReadAllBytes(filepath_doc);
                String filedoc = Convert.ToBase64String(bytespdf);
                //file ảnh
                var filepath_image = ConfigurationManager.AppSettings["FilePath"] + urlimage;
                if (!File.Exists(filepath_image))
                    return Content(HttpStatusCode.NotFound, new { ViMessage = "Không tìm thấy đường dẫn của file", EnMessage = "" });
                Byte[] bytes = File.ReadAllBytes(filepath_image);
                String fileimage = Convert.ToBase64String(bytes);
                #endregion
                XmlDocument soapEnvelopeXml = CreateSoapEnvelope(id, username, password, filedoc, fileimage, positionXcv, positionYcv,withImagecv,heightImagecv, pageIndex, email, sw, typefollow, typeSign, type);
                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                var start_time = DateTime.Now;
                using (Stream stream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);  
                }
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult;
                        soapResult = rd.ReadToEnd();
                        var end_time = DateTime.Now;
                        loger.EndAt = end_time;
                        loger.Response = soapResult;
                        loger.RequestTime = (end_time - start_time).ToString();
                        CustomLog.apigwlog.Info(JsonConvert.SerializeObject(loger));
                        if(response.StatusCode == HttpStatusCode.OK)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(FileCKSModel.Envelope));
                            var Envelope = (FileCKSModel.Envelope)serializer.Deserialize(new StringReader(soapResult));
                            var data = Envelope.Body.API_ESIGN_SERVERResponse.API_ESIGN_SERVERResult.data;
                            var code = Envelope.Body.API_ESIGN_SERVERResponse.API_ESIGN_SERVERResult.code;
                            if (code == "500")
                            {
                                return Content(HttpStatusCode.InternalServerError,
                                    new { ViMessage = "Phải truyền đúng yêu cầu ký số 0,1,2,5,6", EnMessage = "" });
                            }
                            if (code == "200")
                            {
                                byte[] sPDFDecoded = Convert.FromBase64String(data);
                                var upload_path = ConfigurationManager.AppSettings["FilePath"];
                                var success_file = new List<string>();
                                var date_now = DateTime.Now.ToString("dd-MM-yyyy");
                                var folder_virtual_path = $"/UploadFiles/Images/Temp/{date_now}";
                                var folder_physic_path = $"{upload_path}{folder_virtual_path}";
                                bool exists = Directory.Exists(folder_physic_path);
                                if (!exists)
                                    Directory.CreateDirectory(folder_physic_path);
                                string file_name = $"{Guid.NewGuid().ToString()}.pdf";
                                string file_virtual_path = $"{folder_virtual_path}/{file_name}";
                                string file_physic_path = $"{upload_path}{file_virtual_path}";
                                File.WriteAllBytes(file_physic_path, sPDFDecoded);
                                string result = Encoding.UTF8.GetString(sPDFDecoded);
                                return Content(HttpStatusCode.OK, new { ViMessage = "Thành công", EnMessage = "" , Url = file_virtual_path});
                            }
                            if (code == "203")
                            {
                                return Content(HttpStatusCode.NonAuthoritativeInformation,new { ViMessage = "Gửi email thất bại", EnMessage = "" });
                            }
                            if (code == "400")
                            {
                                return Content(HttpStatusCode.BadRequest, new { ViMessage = "Phải truyền kiểu ký số. LOCALTION = Ký theo tọa độ, POSITION = Ký theo vị trí", EnMessage = "" });                                                    }
                            if (code == "502")
                            {
                                return Content(HttpStatusCode.BadGateway, new { ViMessage = "không có chữ ký số", EnMessage = "" });
                            }
                            return Content(HttpStatusCode.NotFound, new { ViMessage = "Không tìm thấy", EnMessage = "" });
                        }
                        else
                        {

                            return Content(HttpStatusCode.NotFound, new { ViMessage = "Không tìm thấy", EnMessage = "" });

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
        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "application/soap+xml";
            webRequest.Accept = "*/*";
            webRequest.Method = "POST";
            return webRequest;
        }
        private static XmlDocument CreateSoapEnvelope(Guid id, string username, string password, string filedoc, string fileimage, float positionX, float positionY, float withImage,float heightImage, string pageIndex, string email, string sw, string typefollow,string typeSign,string type)
        {
            string _id = id.ToString();
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
                                    $@"<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                                        xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                                        xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                                            <soap12:Body>
                                                <API_ESIGN_SERVER xmlns=""http://tempuri.org/"">
                                                    <userName>{username}</userName> 
                                                      <passWord>{password}</passWord> 
                                                      <b_Id>{_id}</b_Id> 
                                                      <dataBase64>{filedoc}</dataBase64>                                               
                                                      <imageSignBase64>{fileimage}</imageSignBase64>
                                                      <Type>{type}</Type>
                                                      <typeSign>{typeSign}</typeSign> 
                                                      <locationKey></locationKey> 
                                                      <positionX>{positionX}</positionX>
                                                      <positionY>{positionY}</positionY> 
                                                      <withImg>{withImage}</withImg> 
                                                      <heightImg>{heightImage}</heightImg> 
                                                      <pageIndex>{pageIndex}</pageIndex> 
                                                      <sw>{sw}</sw> 
                                                     <typefollow>{typefollow}</typefollow> 
                                                     <email>{email}</email> 
                                                     <followCode></followCode> 
                                                      <link_callback></link_callback> 
                                                </API_ESIGN_SERVER>
                                            </soap12:Body>
                                        </soap12:Envelope>");
            
            return soapEnvelopeDocument;

        }        
    }
}
