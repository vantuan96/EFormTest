using EForm.Common;
using System;

namespace EForm.Models
{
    public class LogViewModel
    {
        public DateTime? CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string RequestId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Username { get; set; }
    }
    public class LogModel
    {
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string URI { get; set; }
        public string RequestTime { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Username { get; set; }
    }
    public class LogParameterModel
    {
        public string Id { get; set; }
        public string ModelName { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(this.Id) || !Validator.ValidateGuid(this.Id))
            {
                return false;
            }
            return true;
        }
    }
}