using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRModels
{
    public class GetViHCByPID
    {
        public string Status { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public string PackageName { get; set; }
        public string SiteCode { get; set; }
        public string StatusText { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public DateTime? GPCompletedTime { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string PackageCode { get; set; }
        public string ConclusionDoctor { get; set; }
    }
    public class GetServiceDepartment
    {
        public Guid? ServiceDepartmentId { get; set; }
    }
}
