using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string PID { get; set; }
        public string Fullname { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string AddressEn { get; set; }
        public string NationalityEn { get; set; }
        public string Phone { get; set; }
        public string Job { get; set; }
        public string Fork { get; set; }
        public string WorkPlace { get; set; }
        public string BloodTypeABO { get; set; }
        public string BloodTypeRH { get; set; }
        public bool IsAllergy { get; set; }
        public string Allergy { get; set; }
        public string KindOfAllergy { get; set; }
        public DateTime? LastUpdateAllergy { get; set; }
        public string Weight { get; set; }
        public DateTime? LastUpdateWeight { get; set; }
        public string Height { get; set; }
        public DateTime? LastUpdateHeight { get; set; }
        public bool IsChronic { get; set; }
        public string IdentificationCard { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public string Relationship { get; set; }
        public string RelationshipContact { get; set; }
        public string RelationshipAddress { get; set; }
        public string RelationshipKind { get; set; }
        public string MOHJob { get; set; }
        public string MOHJobCode { get; set; }
        public string MOHEthnic { get; set; }
        public string MOHEthnicCode { get; set; }
        public string MOHNationality { get; set; }
        public string MOHNationalityCode { get; set; }
        public string MOHAddress { get; set; }
        public string MOHProvince { get; set; }
        public string MOHProvinceCode { get; set; }
        public string MOHDistrict { get; set; }
        public string MOHDistrictCode { get; set; }
        public string MOHObject { get; set; }
        public string MOHObjectOther { get; set; }
        public Nullable<Guid> EDStatusId { get; set; }
        public string PIDEhos { get; set; }
        public string RelationshipID { get; set; }
        public bool IsVip { get; set; }
    }
}
