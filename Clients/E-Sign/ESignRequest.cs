using Helper.ESign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.E_Sign
{
    public class ESignRequest
    {
        public bool Execute(string body, string file_path)
        {
            var base64_file = EsignFile.pathToBase64(file_path);
            var client = new EsignService.serviceSignFileSoapClient();
            // var sign = client.API_ESIGN_SERVER();
            return true;
        }
    }
}
