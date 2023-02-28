using Common;
using EMRModels;
using Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EMRModels.LogerModel;

namespace Clients.HisClient
{
    public class ApiGWRequest
    {
        private static int api_time_for_checking = string.IsNullOrEmpty(ConfigurationManager.AppSettings["API_CHECKING_TIME"]) ? 5 : int.Parse(ConfigurationManager.AppSettings["API_CHECKING_TIME"].ToString());
        public static async Task<string> PostAsync(string url, string auth_token, int? timeout = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["HIS_API_SERVER_TOKEN"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = timeout == null ? TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString())) : TimeSpan.FromSeconds((int)timeout);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                };
                if (!string.IsNullOrWhiteSpace(auth_token))
                {
                    request.Headers.Add("Authorization", auth_token);
                }
                string raw_data = string.Empty;
                HttpResponseMessage results = null;
                try
                {
                    results = await client.GetAsync(url);

                    raw_data = await results.Content.ReadAsStringAsync();
                    try
                    {
                        return raw_data.ToString();
                    }
                    catch
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    var log_response = string.Format("{0}\n{1}", ex.ToString(), raw_data);
                    return null;
                }
                finally
                {
                    if (results != null)
                    {
                        results.Dispose();
                    }
                }
            }
        }
        public static async Task<JToken> GetAsync(string url_postfix, string json_collection, string json_item, int? timeout = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["HIS_API_SERVER_TOKEN"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = timeout == null ? TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString())) : TimeSpan.FromSeconds((int)timeout);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                string url = string.Format("{0}{1}", ConfigurationManager.AppSettings["HIS_API_SERVER_URL"], url_postfix);
                string raw_data = string.Empty;
                var start_time = DateTime.Now;
                var loger = new LogModel()
                {
                    URI = url,
                    StartAt = start_time
                };
                HttpResponseMessage results = null;
                try
                {
                    results = await client.GetAsync(url);

                    raw_data = await results.Content.ReadAsStringAsync();

                    JObject json_data = JObject.Parse(raw_data);
                    if (results.StatusCode != HttpStatusCode.OK)
                        HandleApiILog.Error(url, raw_data);
                    else
                        HandleApiILog.Success(url);
                    var log_response = json_data.ToString();
                    loger.Response = log_response;
                    var end_time = DateTime.Now;
                    loger.EndAt = end_time;
                    var request_time = (end_time - start_time);
                    loger.RequestTime = request_time.ToString();
                    CustomLogs.apigwlog.Info(JsonConvert.SerializeObject(loger));

                    if (request_time.Seconds >= api_time_for_checking)
                    {
                        HandleApiILog.ApiGwInfo(url, start_time, end_time, "200");
                    }

                    try
                    {
                        JToken customer_data = json_data[json_collection][json_item];
                        return customer_data;
                    }
                    catch
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    var log_response = string.Format("{0}\n{1}", ex.ToString(), raw_data);
                    HandleApiILog.Error(url, log_response);
                    var end_time = DateTime.Now;
                    loger.EndAt = end_time;
                    loger.Response = log_response;
                    loger.RequestTime = (end_time - start_time).ToString();
                    CustomLogs.apigwlog.Info(JsonConvert.SerializeObject(loger));
                    return null;
                }
                finally
                {
                    if (results != null)
                    {
                        results.Dispose();
                    }
                }
            }
        }

        public static JToken GetNoAsync(string url_postfix, string json_collection, string json_item, int? timeout = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["HIS_API_SERVER_TOKEN"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = timeout == null ? TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString())) : TimeSpan.FromSeconds((int)timeout);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                string url = string.Format("{0}{1}", ConfigurationManager.AppSettings["HIS_API_SERVER_URL"], url_postfix);
                string raw_data = string.Empty;
                var start_time = DateTime.Now;
                var loger = new LogModel()
                {
                    URI = url,
                    StartAt = start_time
                };
                HttpResponseMessage results = null;
                try
                {
                    results = AsyncHelper.RunSync(() => client.GetAsync(url));

                    // raw_data = results.Content.ReadAsStringAsync().Result;
                    raw_data = AsyncHelper.RunSync(() => results.Content.ReadAsStringAsync());

                    JObject json_data = JObject.Parse(raw_data);

                    var log_response = json_data.ToString();
                    loger.Response = log_response;
                    var end_time = DateTime.Now;
                    loger.EndAt = end_time;
                    var request_time = (end_time - start_time);

                    if (request_time.Seconds >= api_time_for_checking)
                    {
                        HandleApiILog.ApiGwInfo(url, start_time, end_time, "200");
                    }

                    loger.RequestTime = request_time.ToString();
                    CustomLogs.apigwlog.Info(JsonConvert.SerializeObject(loger));

                    if (results.StatusCode != HttpStatusCode.OK)
                        HandleApiILog.Error(url, raw_data);
                    else
                        HandleApiILog.Success(url);

                    try
                    {
                        JToken customer_data = json_data[json_collection][json_item];
                        return customer_data;
                    }
                    catch
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    var log_response = string.Format("{0}\n{1}", ex.ToString(), raw_data);
                    var end_time = DateTime.Now;
                    loger.EndAt = end_time;
                    loger.Response = log_response;
                    loger.RequestTime = (end_time - start_time).ToString();
                    CustomLogs.apigwlog.Info(JsonConvert.SerializeObject(loger));
                    return null;
                }
                finally
                {
                    if (results != null)
                    {
                        results.Dispose();
                    }
                }
            }
        }
    }
    public class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskContinuationOptions.None,
                        TaskScheduler.Default);

        public static TReturn RunSync<TReturn>(Func<Task<TReturn>> task)
        {
            return _taskFactory.StartNew(task)
                                .Unwrap()
                                .GetAwaiter()
                                .GetResult();
        }
    }
    public class ApiRequest
    {
        public static async Task<string> PostAsync(string url, dynamic data, string auth_token, int? timeout = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                client.Timeout = timeout == null ? TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["HIS_API_TIMEOUT"].ToString())) : TimeSpan.FromSeconds((int)timeout);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                };
                //if (!string.IsNullOrWhiteSpace(auth_token))
                //{
                //    request.Headers.Add("Authorization", auth_token);
                //}
                string json = JsonConvert.SerializeObject(data);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                string raw_data = string.Empty;
                HttpResponseMessage results = null;
                try
                {
                    results = await client.PostAsync(url, httpContent);

                    raw_data = await results.Content.ReadAsStringAsync();
                    try
                    {
                        return raw_data.ToString();
                    }
                    catch
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    var log_response = string.Format("{0}\n{1}", ex.ToString(), raw_data);
                    return null;
                }
                finally
                {
                    if (results != null)
                    {
                        results.Dispose();
                    }
                }
            }
        }
    }
}
