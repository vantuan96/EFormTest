using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Admin.Common.Extentions
{
    public static class StringExtension
    {
        /// <summary>
        /// Đưa kiểu unikey về không dấu
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetVnStringOnlyCharactersAndNumbers(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            StringBuilder sb = new StringBuilder();
            char[] ca = value.Trim().ToCharArray();
            foreach (char t in ca)
            {
                char x = GetValidVnChar(t);
                sb.Append(x);
            }
            return sb.ToString();
        }
        private static char GetValidVnChar(char x)
        {
            if ((x >= 'a' && x <= 'z') || (x >= '0' && x <= '9') || (x >= 'A' && x <= 'Z'))
            {
                return x;
            }
            string s = x.ToString();

            if ("àáạảãâầấậẩẫăắằặẳẵ".Contains(s)) return 'a';
            if ("èéẹẻẽêềếệểễ".Contains(s)) return 'e';
            if ("ìíịỉĩ".Contains(s)) return 'i';
            if ("đ".Contains(s)) return 'd';
            if ("òóọỏõôồốộổỗơờớợởỡ".Contains(s)) return 'o';
            if ("ùúụủũưừứựửữ".Contains(s)) return 'u';
            if ("ỳýỵỷỹ".Contains(s)) return 'y';
            if ("ÀÁẠẢÃÂẦẤẬẨẪĂẮẰẶẲẴ".Contains(s)) return 'A';
            if ("ÈÉẸẺẼÊỀẾỆỂỄ".Contains(s)) return 'E';
            if ("ÌÍỊỈĨ".Contains(s)) return 'I';
            if ("Đ".Contains(s)) return 'D';
            if ("ÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠ".Contains(s)) return 'O';
            if ("ÙÚỤỦŨƯỪỨỰỬỮ".Contains(s)) return 'U';
            if ("ỲÝỴỶỸ".Contains(s)) return 'Y';
            return x;
        }
    }
}