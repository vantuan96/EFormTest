using EForm.Models;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Utils
{
    public class ICDConvert
    {
        public static List<ICDModel> Operate(string raw)
        {
            try
            {
                var json = JArray.Parse(raw);
                return json.Select(e => new ICDModel
                {
                    Code = e.Value<string>("code"),
                    Label = e.Value<string>("label"),
                }).ToList();
            }
            catch (Exception)
            {
                return new List<ICDModel>();
            }
        }
    }
}