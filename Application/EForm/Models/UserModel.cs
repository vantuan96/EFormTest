using System;
using System.Collections.Generic;

namespace EForm.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string FullShortName { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
    }
    public class ValidateUserModel
    {
        public Guid? Id { get; set; }
        public dynamic ErrorMsg { get; set; }
    }
    public class UserParameterModel
    {
        public string Username { get; set; }
        public string Usernames { get; set; }
        public string Search { get; set; }
        public Guid? SpecialtyId { get; set; }
        public Guid? SiteId { get; set; }
        public string Position { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? Id { get; set; }
    }
}