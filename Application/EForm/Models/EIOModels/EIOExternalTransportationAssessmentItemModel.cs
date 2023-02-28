using DataAccess.Models;
using DataAccess.Models.EIOModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EForm.Models.EIOModels
{
    public class EIOExternalTransportationAssessmentItemModel
    {
        public Guid Id { get; set; }
        public string DoctorTime { get; set; }
        [JsonIgnore]
        public User Doctor { get; set; }
        public string NurseTime { get; set; }
        [JsonIgnore]
        public User Nurse { get; set; }
        public dynamic Datas { get; set; }
        public dynamic Equipments { get; set; }
        public bool IsLocked { get; set; }
    }
}
