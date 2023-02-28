using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Models.EIOModels
{
    public class EIOBloodRequestSupplyAndConfirmationModel
    {
    }

    public class EIOBloodSupplyViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public int Quanlity { get; set; }
        public string NurseTime { get; set; }
        public string NurseUser { get; set; }
        public string CuratorTime { get; set; }
        public string CuratorUser { get; set; }
        public string ProviderTime { get; set; }
        public string ProviderUser { get; set; }
        public dynamic Datas { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}