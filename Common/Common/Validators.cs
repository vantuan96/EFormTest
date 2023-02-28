using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Common
{
    public class Validators
    {
        public static bool ValidateDate(dynamic date_input)
        {
            try
            {
                DateTime date = DateTime.ParseExact(date_input, Constants.DATE_FORMAT, null);
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        public static bool ValidateDateTime(dynamic datetime_input)
        {
            try
            {
                DateTime date = DateTime.ParseExact(datetime_input, Constants.DATE_TIME_FORMAT, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool ValidateDateTimeWithoutSecond(dynamic datetime_input)
        {
            try
            {
                DateTime date = DateTime.ParseExact(datetime_input, Constants.DATE_TIME_FORMAT_WITHOUT_SECOND, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateTime(string time_input)
        {
            try
            {
                DateTime date = DateTime.ParseExact(time_input, Constants.TIME_FORMAT, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateTimeDate(string datetime_input)
        {
            try
            {
                if (string.IsNullOrEmpty(datetime_input))
                    return true;

                DateTime date = DateTime.ParseExact(datetime_input, Constants.TIME_DATE_FORMAT, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool ValidateTimeDateWithoutSecond(string datetime_input)
        {
            try
            {
                if (string.IsNullOrEmpty(datetime_input))
                    return true;

                DateTime date = DateTime.ParseExact(datetime_input, Constants.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateInt(dynamic gender_input)
        {
            try
            {
                return gender_input is int;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static bool ValidateString(dynamic name_input)
        {
            try
            {
                return name_input is string;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateListString(dynamic str)
        {
            try
            {
                str.Split(',');
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static bool ValidatePhone(dynamic phone)
        {
            return phone.Length <= 30;
        }

        public static bool ValidatePID(dynamic pid)
        {
            try
            {
                return Regex.Match(pid, @"^([0-9]{6,12})$").Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidateGuid(string id)
        {
            try
            {
                string[] lid = id.Split(',');
                foreach (var i in lid)
                {
                    Guid g = new Guid(i);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}