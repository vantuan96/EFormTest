using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Helper
{
    public class StringHelper
    {
        public static Guid? StringToGuid(string a)
        {
            if (string.IsNullOrWhiteSpace(a)) return null;
            return new Guid(a);
        }
        public static string FormatUnicodeString(string s_input)
        {
            if (s_input == null) return "";

            s_input = s_input.ToLower();

            object obj = s_input;
            string s = obj.ToString();
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);

            string str_removeUnicode = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            StringBuilder stringResult = new StringBuilder();
            int lengthOfString = str_removeUnicode.Length;
            for (int i = 0; i < lengthOfString; i++)
            {
                if (str_removeUnicode[i] == ' ')
                    continue;
                if (str_removeUnicode[i] == ' ')
                    continue;

                stringResult.Append(str_removeUnicode[i]);
            }

            string result = "/" + stringResult.ToString().ToUpper() + "/";

            return result;
        }
    }
}
