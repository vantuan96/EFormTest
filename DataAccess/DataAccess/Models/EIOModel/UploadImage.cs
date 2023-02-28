using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.EIOModel
{
    public class UploadImage : VBaseModel
    {
   
        public string Name { get; set; }
        public string Title { get; set; }
        public string Path { get; set; } 
        public string Url { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public string VisitType { get; set; }
        public Guid VisitId { get; set; }   
        public Guid FormId { get; set; }
        public string FormCode { get; set; }
    }
}
