using DataAccess.Model.BaseModel;
using DataAccess.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        //public Nullable<DateTime> UpdatedAt { get; set; }
        //public string UpdatedBy { get; set; }
        //public bool IsDeleted { get; set; }
        //public Nullable<DateTime> DeletedAt { get; set; }
        //public string DeletedBy { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [Index]
        public string URI { get; set; }
        public string Name { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Ip { get; set; }
        //[Column(TypeName = "VARCHAR")]
        //[StringLength(80)]
        //[Index]
        //public string RequestId { get; set; }
        public string Reason { get; set; }
    }
    public class LogTmp : VBaseModel
    {
        public string Username { get; set; }
        public string Action { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [Index]
        public string URI { get; set; }
        public string Name { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Ip { get; set; }
        //[Column(TypeName = "VARCHAR")]
        //[StringLength(80)]
        //[Index]
        //public string RequestId { get; set; }
        public string Reason { get; set; }
    }
}
