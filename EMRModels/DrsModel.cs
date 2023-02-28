using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRModels
{
    public class DrsModel
    {

    }
    public class OHServiceResult
    {
        public int OK { get; set; }
        public int Failed { get; set; }
        public int Total { get; set; }
        public OHServiceResult()
        {

        }
        public OHServiceResult(int oK, int failed, int total)
        {
            OK = oK;
            Failed = failed;
            Total = total;
        }
    }
}
