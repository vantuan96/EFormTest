using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ESign
{
    public class ESignHelper
    {
        public static List<MasterData> DataToDb(List<MasterData> data, Guid form_id, string form_code)
        {
            return data;
        }
        public static string DataToFile(string template_path, List<MasterData> data)
        {
            var save_file_to_path = "";
            return save_file_to_path;
        }
        public static string FileToBase64(string path)
        {
            return "";
        }
    }
}
