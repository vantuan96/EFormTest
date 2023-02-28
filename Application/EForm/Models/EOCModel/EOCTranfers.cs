using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.EOCModel
{
    public class EOCInfo
    {
        public bool NoEOC { get; set; }
        public string AcceptBy { get; set; }
        public DateTime? TransferAt { get; set; }
        public Guid? VisitId { get; set; }
        public bool IsDone { get; set; }
        public DataAccess.Models.EDStatus Status { get; set; }

    }
    public class EOCTranfers
    {
        internal string Fullname;
        internal DateTime? DateOfBirth;
        internal string SpecialtyName;
        internal string TransferBy;
        internal DateTime? TransferAt;
        internal DateTime? AcceptAt;
        internal string AcceptBy;
        internal Guid? SpecialtyId;
        internal Guid? SiteId;
        //internal Guid? CustomerId;

        public Guid Id { get; set; }
        public string PID { get; set; }
        public string VisitCode { get; set; }
        public string checkTranfer { get; set; }
    }
}