using EForm.Models;
using EMRModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EForm.Helper
{
    public class ApiHelper
    {
        private static string tentantId = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string Proxy = ConfigurationManager.AppSettings["AzureProxy"];
        private static string _clientId = ConfigurationManager.AppSettings["ClientId"].ToString();
        private static string _Authority = ConfigurationManager.AppSettings["AzureAuthority"].ToString();
        private static string _ClientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();
        public static HttpClient client;
        static ApiHelper()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
        }
        public static async Task<HttpResponseMessage> HttpGet(string uri, string token = "")
        {
            var url = ConfigurationManager.AppSettings["API_ManageApp_URL"].ToString() + uri;

            try
            {
                if (!string.IsNullOrWhiteSpace(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await client.GetAsync(url);
            }
            catch (System.Exception ex)
            {
                var re = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };

                return await Task.FromResult(re);
            }
        }


        public static string HttpGetCheck(string uri, string token = "")
        {
            var url = ConfigurationManager.AppSettings["API_ManageApp_URL"].ToString() + uri;
            var client = new RestClient(url);
            //client.Timeout = -1;
            //if (!string.IsNullOrEmpty(Proxy))
            //{
            //    client.Proxy = new WebProxy(Proxy);
            //}
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {token}");
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public static string ChecloginEMR(string email, string sessionId)
        {
            var uri = "api/ManageAppBase/CheckLogin?email=" + email + "&sessionId=" + sessionId;
            var value = HttpGetCheck(uri, "2krojMdNQkSpZzwybnoR6g==");

            return value;
        }
        public static async Task<HttpResponseMessage> HttpPost<T>(string uri, T obj, string authorization = "Bearer", string token = "")
        {
            var url = ConfigurationManager.AppSettings["API_ManageApp_URL"].ToString() + uri;

            try
            {
                if (!string.IsNullOrWhiteSpace(authorization) && !string.IsNullOrWhiteSpace(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorization, token);

                var content = JsonConvert.SerializeObject(obj);

                var data = new StringContent(content, Encoding.UTF8, "application/json");

                return await client.PostAsync(url, data);
            }
            catch (System.Exception ex)
            {
                var re = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };

                return await Task.FromResult(re);
            }
        }
        public static async Task<string> GetToken()
        {
           
            var client = new RestClient(_Authority  + tentantId + "/oauth2/v2.0/token");
            client.Timeout = -1;
            if (!string.IsNullOrEmpty(Proxy))
            {
                client.Proxy = new WebProxy(Proxy);
            }
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id",_clientId);
            request.AddParameter("scope", "openid profile offline_access");
            request.AddParameter("redirect_uri", Startup.RedirectUri);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_secret", _ClientSecret);
            request.AddParameter("code", Startup.token);

            IRestResponse response = client.Execute(request);


            ParseJsonToModel json = JsonConvert.DeserializeObject<ParseJsonToModel>(response.Content);
            // gán token  từ azure

            var result = new Azure_User()
            {
                Id_Token = json.id_token,
                Refresh_Token = json.refresh_token,
                Access_Token = json.access_token
            };
            var ob = new Azure_User();
            // lấy thông tin  từ email  trả ra azure 

            //var idToken = ReadToken(result.Id_Token);
            //var accessToken = ReadToken(result.Access_Token);

            return result.Access_Token;
        }

        public static bool ValidateAccesstoken(string acesstoken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            WebClient WebClient = new WebClient();
            string urlJson = string.Format(_Authority + "/{0}/discovery/v2.0/keys?appId={1}", tentantId, _clientId);
            WebClient.Proxy = new WebProxy();
            if (!string.IsNullOrEmpty(Proxy) || !string.IsNullOrWhiteSpace(Proxy))
            {
                WebClient.Proxy = new WebProxy(Proxy);
            }
            else
            {
                WebClient.Proxy = new WebProxy();
            }
            string discoveryJson = WebClient.DownloadString(urlJson);
            RootKeyAzure Azurejson = JsonConvert.DeserializeObject<RootKeyAzure>(discoveryJson);
            var parsedJwt = tokenHandler.ReadToken(acesstoken) as JwtSecurityToken;
            string kid = parsedJwt.Header.Where(c => c.Key == "kid").Select(c => c.Value).FirstOrDefault().ToString();
            var _data = Azurejson.keys.Where(m => m.kid == kid).FirstOrDefault();
            var json = JsonConvert.SerializeObject(_data);

            var jwk = JsonWebKeyConverter.ConvertFromRSASecurityKey(json);
            jwk.KeyId = "authentication";
            jwk.Use = "sig";
            jwk.Alg = "RS256";
            //var json = JsonConvert.SerializeObject(jwk);
            try
            {
                JsonWebKey JsonWebKey = new JsonWebKey(json);
              //var m =  new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtSecret"])),
                var validationParameters = new System.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidIssuer = string.Format(_Authority + "/{0}/v2.0", tentantId),
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = ConvertFromSecurityKey(Microsoft.IdentityModel.Tokens.SecurityKey JsonWebKey) ,
                    AuthenticationType = "Cookies",

                };

                try
                {
                    System.IdentityModel.Tokens.SecurityToken validatedToken;
                    var principal = tokenHandler.ValidateToken(acesstoken, validationParameters, out validatedToken);
                }
                catch (Exception)
                {

                    return false;
                }
            }
            catch (Exception ex1)
            {

                return false;
            }
           
          
            return true;
        }

        public static List<UserInfo> HttpGetRestClient(string uri, string token)
        {
            var url = ConfigurationManager.AppSettings["API_ManageApp_URL"].ToString() + uri;
            var client = new RestClient(url);
            client.Timeout = -1;
            if (!string.IsNullOrEmpty(Proxy))
            {
                client.Proxy = new WebProxy(Proxy);
            }
            var request = new RestRequest(Method.GET);
            request.AddHeader("Bearer", token);
            IRestResponse response = client.Execute(request);


            List<UserInfo> json = JsonConvert.DeserializeObject<List<UserInfo>>(response.Content);

            return json;
        }

        public class ParseJsonToModel
        {
            public string id_token { get; set; }
            public string refresh_token { get; set; }
            public string access_token { get; set; }

        }
        public class RootKeyAzure
        {
            public List<KeyAzure> keys { get; set; }
        }
        public class Azure_User
        {
            public string Email { get; set; }
            public string Id_Token { get; set; }
            public string Refresh_Token { get; set; }
            public string Access_Token { get; set; }
        }
        public class KeyAzure
        {
            public string kty { get; set; }
            public string use { get; set; }
            public string kid { get; set; }
            public string x5t { get; set; }
            public string n { get; set; }
            public string e { get; set; }
            public List<string> x5c { get; set; }
            public string issuer { get; set; }
        }

    }
}