using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace EForm.Authentication
{
    public class Recaptcha
    {
        public static bool IsReCaptchValid(string EncodedResponse)
        {
            var result = false;
            try
            {
                string PrivateKey = System.Configuration.ConfigurationManager.AppSettings["ReCaptCha-Secret-Key"];
                var requestUri = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse);
                var request = (HttpWebRequest)WebRequest.Create(requestUri);
                var proxy_addr = System.Configuration.ConfigurationManager.AppSettings["ProxySever"];
                string raw_data = string.Empty;
                if (!string.IsNullOrEmpty(proxy_addr))
                {
                    WebProxy wp = new WebProxy(proxy_addr);
                    request.Proxy = wp;
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        raw_data = stream.ReadToEnd();
                        JObject jResponse = JObject.Parse(raw_data);
                        CustomLog.apigwlog.Info(new
                        {
                            URI = "GOOGLE",
                            Response = raw_data,
                        });
                        var isSuccess = jResponse.Value<bool>("success");
                        result = (isSuccess) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                var log_response = string.Format("{0}", ex.ToString());
                CustomLog.apigwlog.Info(new
                {
                    URI = "GOOGLE",
                    Response = log_response,
                });
                return false;
            }
            return result;
        }
    }
}