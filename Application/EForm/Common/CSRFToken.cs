using System;

namespace EForm.Common
{
    public static class CSRFToken
    {
        public static string Generate()
        {
            var key = CustomDES.GenerateKey(100);
            var date = DateTime.Now.AddDays(1).ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
            var token = string.Format("{0}{1}", key, CustomDES.Encrypt(date));
            return CustomDES.Encode(token);
        }

        public static bool IsValid(string raw_token)
        {
            var decode = CustomDES.Decode(raw_token);
            var date_time = CustomDES.Decrypt(decode.Substring(100));
            try
            {
                DateTime date = DateTime.ParseExact(date_time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                if (date > DateTime.Now)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}