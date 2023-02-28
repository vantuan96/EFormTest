using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EForm.Common
{
    public class OHClient
    {
        private static readonly HttpClient client = new HttpClient();


        public static List<dynamic> SearchCustomerByPID(string pid)
        {
            string url_postfix = string.Format("/searchPatientByPID?PID={0}", pid);
            var response = RequestForSearchPatients(url_postfix);
            return BuildResult(response);
        }

        public static List<dynamic> SearchCustomerByPhone(string phone)
        {
            string url_postfix = string.Format("/searchPatientByPhone?phone={0}", phone);
            var response = RequestForSearchPatients(url_postfix);
            return BuildResult(response);
        }

        public static List<dynamic> SearchCustomerByName(string name)
        {
            string url_postfix = string.Format("/searchPatientByname?Patientname={0}", name);
            var response = RequestForSearchPatients(url_postfix);
            if (response != null)
            {
                return BuildResult(response);
            }
            return new List<dynamic>();
        }

        public static List<dynamic> SearchCustomerByNameAndDOB(string name, string dob)
        {
            DateTime date = DateTime.ParseExact(dob, Constant.DATE_FORMAT, null);
            string date_of_birth = date.ToString("yyyy-MM-dd");
            string url_postfix = string.Format("/searchPatientByNameAndDob?Patientname={0}&dob={1}", name, date_of_birth);
            var response = RequestForSearchPatients(url_postfix);
            if (response != null)
            {
                return BuildResult(response);
            }
            return new List<dynamic>();
        }

        public static List<dynamic> SearchCustomerByNameGenderAndDOB(string name, int gender, string dob)
        {
            DateTime date = DateTime.ParseExact(dob, Constant.DATE_FORMAT, null);
            string date_of_birth = date.ToString("yyyy-MM-dd");
            string gender_param = "";
            if (gender == 0)
            {
                gender_param = "F";
            }
            else if (gender == 1)
            {
                gender_param = "M";
            }
            string url_postfix = string.Format("/searchPatient_v2?name={0}&dob={1}&sex={2}", name, date_of_birth, gender_param);
            var response = RequestForSearchPatients(url_postfix);
            if (response != null)
            {
                return BuildResult(response);
            }
            return new List<dynamic>();
        }

        public static List<dynamic> SearchCustomerByNameAndYOB(string name, string yob)
        {
            string url_postfix = string.Format("/searchPatientByNameAndYob?Patientname={0}&yob={1}", name, yob);
            var response = RequestForSearchPatients(url_postfix);
            if (response != null)
            {
                return BuildResult(response);
            }
            return new List<dynamic>();
        }

        private static JToken RequestForSearchPatients(string url_postfix)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constant.OH_API_SERVER_TOKEN);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string url = string.Format("{0}{1}", Constant.OH_API_SERVER_URL, url_postfix);
            try
            {
                var response = client.GetAsync(url);
                var raw_data = response.Result.Content.ReadAsStringAsync().Result;
                JObject json_data = JObject.Parse(raw_data);
                //JToken customer_data = json_data["Entries"]["Entry"];
                JToken customer_data = json_data["DSBenhNhan"]["BenhNhan"];
                return customer_data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        private static List<dynamic> BuildResult(JToken customer_data)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (JToken customer in customer_data)
            {
                result.Add(new
                {
                    Fullname = customer.Value<string>("TenBenhNhan"),
                    PID = customer.Value<string>("PID"),
                    Address = customer.Value<string>("DiaChi"),
                    Phone = customer.Value<string>("SoDienThoai"),
                    Gender = FormatGender(customer.Value<string>("GioiTinh")),
                    Email = customer.Value<string>("Email"),
                    DateOfBirth = customer.Value<string>("NgaySinh"),
                    Nationality = customer.Value<string>("QuocTich"),
                });
            }
            return result;
        }

        private static string FormatDOB(string rawData)
        {
            if (rawData != null)
            {
                DateTime date = DateTime.ParseExact(rawData, "dd-MM-yyyy", null);
                return date.ToString(Constant.DATE_FORMAT);
            }
            return rawData;
        }

        private static int FormatGender(string rawData)
        {
            foreach (string gender in Constant.MALE_SAMPLE)
            {
                if (rawData == gender)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
