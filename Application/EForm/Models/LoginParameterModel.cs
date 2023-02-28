using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models
{
    public class LoginParameterModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string captcha { get; set; }
        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.password))
            {
                return false;
            }
            return true;
        }
    }
}