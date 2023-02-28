using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Customer : VBaseModel
    {
        public Customer()
        {
            this.ComplexOutpatientCaseSummarys = new HashSet<ComplexOutpatientCaseSummary>();
        }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string PID { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(255)]
        [Index]
        public string Fullname { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string AddressEn { get; set; }
        public string NationalityEn { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
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
        [ForeignKey("EDStatusId")]
        public virtual EDStatus EDStatus { get; set; }
        public virtual ICollection<ComplexOutpatientCaseSummary> ComplexOutpatientCaseSummarys { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string PIDEhos { get; set; }
        [NotMapped]
        public string DateOfBirthConverted
        {
            get
            {
                if (this.DateOfBirth == null)
                {
                    return null;
                }
                return this.DateOfBirth?.ToString("dd/MM/yyyy");
            }
        }
        [NotMapped]
        public string AgeFormated
        {
            get
            {
                if (this.DateOfBirth == null)
                {
                    return null;
                }
                var now = DateTime.Now;
                var dob = (DateTime)this.DateOfBirth;
                var days = (now - dob).TotalDays;
                if (Math.Floor(days) < 0)
                {
                    return "N/A";
                }
                if (Math.Floor(days) <= 30)
                {
                    return string.Format("{0}-{1}", Math.Floor(days).ToString(), "DD"); // 10-DD
                }
                if (Math.Floor(days) < 2160)
                {
                    return string.Format("{0}-{1}", (Math.Floor(days/30)).ToString(), "MM"); // 34-MM
                }
                return string.Format("{0}-{1}", (now.Year - dob.Year).ToString(), "YYYY"); // 30-YYYY
            }
        }
        //[NotMapped]
        //public string Age
        //{
        //    get
        //    {
        //        if (this.DateOfBirth == null)
        //        {
        //            return null;
        //        }
        //        var now = DateTime.Now;
        //        var dob = (DateTime)this.DateOfBirth;
        //        var days = (now - dob).TotalDays;
        //        if (days < 0)
        //        {
        //            return "N/A";
        //        }
        //        if (days <= 30)
        //        {
        //            return string.Format("{0}-{1}", days.ToString(), "DD");
        //        }
        //        if (days <= 2160)
        //        {
        //            return string.Format("{0}-{1}", (Math.Floor(days / 30)).ToString(), "MM");
        //        }
        //        return string.Format("{0}-{1}", (now.Year - dob.Year).ToString(), "YYYY");
        //    }
        //}
        public string RelationshipID { get; set; } //Số cmt, căn cước của người giám hộ
        public bool IsVip { get; set; }
        [NotMapped]
        public string HealthInsuranceNumber { get; set; }  //Số thẻ BHYT
        [NotMapped]
        public DateTime? StartHealthInsuranceDate { get; set; }
        [NotMapped]
        public DateTime? ExpireHealthInsuranceDate { get; set; }
        [NotMapped]
        public string Province { get; set; }
        [NotMapped]
        public string District { get; set; }
        [NotMapped]
        public string Object { get; set; }

    }
}
