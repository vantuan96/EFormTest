using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models.EIOModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOBloodTransfusionChecklistController : BaseApiController
    {
        protected EIOBloodTransfusionChecklist CreateEIOBloodTransfusionChecklist(Guid visit_id, string visit_type, Guid? spec_id)
        {
            var btc = new EIOBloodTransfusionChecklist()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
                SpecialtyId = spec_id,
                Version = 2
            };
            unitOfWork.EIOBloodTransfusionChecklistRepository.Add(btc);
            unitOfWork.Commit();
            return btc;
        }
        protected dynamic GetEIOBloodTransfusionChecklistDetail(EIOBloodTransfusionChecklist btc, Guid? bed_id, Guid? diag_id, bool IsLocked = false)
        {
            var brsac = GetBloodRequestSupplyAndConfirmation((Guid)btc.VisitId, btc.VisitTypeGroupCode);
            var supplies = GetBloodSupplyDatas(brsac.Id);
            var specialty = btc.Specialty;
            var head_of_lab = btc.HeadOfLab;
            var tech_1 = btc.FirstTechnician;
            var tech_2 = btc.SecondTechnician;
            if (bed_id.HasValue || diag_id.HasValue)
            {
                if (head_of_lab == null && tech_1 == null && tech_2 == null)
                {
                    var bed = GetBed(bed_id, btc.VisitTypeGroupCode);
                    if (string.IsNullOrEmpty(btc.BedNo))
                        btc.BedNo = bed;
                    var diagnosis = new Diagnosis(unitOfWork, diag_id, btc.VisitTypeGroupCode).GetData();
                    btc.Diagnosis = diagnosis;
                    if(btc.VisitTypeGroupCode == "ED")
                        btc.Diagnosis = diagnosis + EDGetAndFormatICD10(diag_id);

                    btc.PatientBloodTypeABO = brsac.BloodTypeABO;
                    btc.PatientBloodTypeRH = brsac.BloodTypeRH;
                    //unitOfWork.EIOBloodTransfusionChecklistRepository.Update(btc); không update log thời gian khi Get về 
                    unitOfWork.Commit();
                }
            }

            var datas = unitOfWork.EIOBloodTransfusionChecklistDataRepository.Find(
                e => !e.IsDeleted &&
                e.EIOBloodTransfusionChecklistId != null &&
                e.EIOBloodTransfusionChecklistId == btc.Id
            ).OrderBy(e => e.Time).Select(e => new
            {
                e.Id,
                e.CreatedBy,
                Time = e.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.TransfusionSpeed,
                e.ColorOfSkin,
                e.BreathsPerMinute,
                e.PulsePerMinute,
                e.Temp,
                e.Other,
                e.Period
            });
            var physician = btc.Physician;
            var nurse = btc.Nurse;

            return new
            {
                btc.Id,
                btc.Diagnosis,
                Specialty = new { specialty?.ViName, specialty?.EnName },
                btc.BedNo,
                btc.TypeOfBloodProducts,
                btc.Quanlity,
                btc.Code,
                DateOfBloodCollection = btc.DateOfBloodCollection?.ToString(Constant.DATE_FORMAT),
                Expiry = btc.Expiry?.ToString(Constant.DATE_FORMAT),
                ThawedTimeAt = btc.ThawedTimeAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ThawedTimeTo = btc.ThawedTimeTo?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                btc.PatientBloodTypeABO,
                btc.PatientBloodTypeRH,
                btc.DonorBloodTypeABO,
                btc.DonorBloodTypeRH,
                btc.OtherCheckTests,
                btc.OtherTests,
                btc.MajorCrossMatchSalt,
                btc.MajorCrossMatchAntiGlobulin,
                btc.MinorCrossMatchSalt,
                btc.MinorCrossMatchAntiGlobulin,
                Supplies = supplies,
                HeadOfLabConfirmTime = btc.HeadOfLabConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfLab = new { head_of_lab?.Fullname, head_of_lab?.Username, head_of_lab?.DisplayName, head_of_lab?.Title },
                FirstTechnicianConfirmTime = btc.FirstTechnicianConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                FirstTechnician = new { tech_1?.Fullname, tech_1?.Username, tech_1?.DisplayName, tech_1?.Title },
                SecondTechnicianConfirmTime = btc.SecondTechnicianConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                SecondTechnician = new { tech_2?.Fullname, tech_2?.Username, tech_2?.DisplayName, tech_2?.Title },

                btc.NumberOfBloodTransfusion,
                btc.Crossmatch,
                StartTransfusionAt = btc.StartTransfusionAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                StopTransfusionAt = btc.StopTransfusionAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                btc.ActualAmountOfBloodTransmitted,
                btc.Remark,
                Datas = datas,
                PhysicianConfirmTime = btc.PhysicianConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Physician = new { physician?.Fullname, physician?.Username, physician?.DisplayName, physician?.Title },
                NurseConfirmTime = btc.NurseConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new { nurse?.Fullname, nurse?.Username, nurse?.DisplayName, nurse?.Title },
                IsLocked,
                btc.Version,
                btc.CreatedAt,
                btc.CreatedBy,
                btc.UpdatedBy,
                btc.UpdatedAt,
                brsac.IsFrequently
            };
        }
        protected dynamic UpdateEIOBloodTransfusionChecklist(EIOBloodTransfusionChecklist btc, JObject request)
        {
            UpdateEIOBloodTransfusionChecklistPartI(btc, request);
            UpdateEIOBloodTransfusionChecklistPartII(btc, request);
            return null;
        }
        protected EIOBloodRequestSupplyAndConfirmation GetBloodRequestSupplyAndConfirmation(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }
        private List<EIOBloodSupplyViewModel> GetBloodSupplyDatas(Guid brsac_id)
        {
            var result = new List<EIOBloodSupplyViewModel>();

            var supplies = unitOfWork.EIOBloodSupplyDataRepository.Find(
                e => !e.IsDeleted &&
                e.EIOBloodRequestSupplyAndConfirmationId != null &&
                e.EIOBloodRequestSupplyAndConfirmationId == brsac_id
            ).OrderBy(e => e.CreatedAt);

            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac_id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_SUPPLY
            ).OrderBy(e => e.CreatedAt).ToList();

            foreach (var sup in supplies)
            {
                result.Add(new EIOBloodSupplyViewModel()
                {
                    Id = sup.Id,
                    Name = sup.Name,
                    CreatedBy = sup.CreatedBy,
                    Quanlity = sup.Quanlity,
                    NurseTime = sup.NurseTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    NurseUser = sup.NurseUser,
                    CuratorTime = sup.CuratorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    CuratorUser = sup.CuratorUser,
                    ProviderTime = sup.ProviderTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    ProviderUser = sup.ProviderUser,
                    Datas = datas.Where(e => e.GroupId == sup.Id).Select(e => new
                    {
                        e.Id,
                        e.Code,
                        e.BloodTypeABO,
                        e.BloodTypeRH,
                        e.Capacity,
                        e.CreatedAt
                    }),
                });
            }

            return result;
        }
        private dynamic UpdateEIOBloodTransfusionChecklistPartI(EIOBloodTransfusionChecklist btc, JObject request)
        {
            var head_of_lab = btc.HeadOfLab;
            var tech_1 = btc.FirstTechnician;
            var tech_2 = btc.SecondTechnician;
            if (head_of_lab != null || tech_1 != null || tech_2 != null)
                return Common.Message.OWNER_FORBIDDEN;

            btc.BedNo = request["BedNo"]?.ToString();
            btc.TypeOfBloodProducts = request["TypeOfBloodProducts"]?.ToString();
            btc.Quanlity = request["Quanlity"]?.ToString();
            btc.Code = request["Code"]?.ToString();

            var date_of_blood_collection = request["DateOfBloodCollection"]?.ToString();
            if (!string.IsNullOrEmpty(date_of_blood_collection))
                btc.DateOfBloodCollection = DateTime.ParseExact(date_of_blood_collection, Constant.DATE_FORMAT, null);
            else
                btc.DateOfBloodCollection = null;

            var expiry = request["Expiry"]?.ToString();
            if (!string.IsNullOrEmpty(expiry))
                btc.Expiry = DateTime.ParseExact(expiry, Constant.DATE_FORMAT, null);
            else
                btc.Expiry = null;

            var thawed_time_at = request["ThawedTimeAt"]?.ToString();
            if (!string.IsNullOrEmpty(thawed_time_at))
                btc.ThawedTimeAt = DateTime.ParseExact(thawed_time_at, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                btc.ThawedTimeAt = null;

            var thawed_time_to = request["ThawedTimeTo"]?.ToString();
            if (!string.IsNullOrEmpty(thawed_time_to))
                btc.ThawedTimeTo = DateTime.ParseExact(thawed_time_to, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                btc.ThawedTimeTo = null;

            btc.PatientBloodTypeABO = request["PatientBloodTypeABO"]?.ToString();
            btc.PatientBloodTypeRH = request["PatientBloodTypeRH"]?.ToString();
            btc.DonorBloodTypeABO = request["DonorBloodTypeABO"]?.ToString();
            btc.DonorBloodTypeRH = request["DonorBloodTypeRH"]?.ToString();
            btc.OtherCheckTests = request["OtherCheckTests"]?.ToString();
            btc.OtherTests = request["OtherTests"]?.ToString();
            btc.MajorCrossMatchSalt = request["MajorCrossMatchSalt"]?.ToString();
            btc.MajorCrossMatchAntiGlobulin = request["MajorCrossMatchAntiGlobulin"]?.ToString();
            btc.MinorCrossMatchSalt = request["MinorCrossMatchSalt"]?.ToString();
            btc.MinorCrossMatchAntiGlobulin = request["MinorCrossMatchAntiGlobulin"]?.ToString();
            unitOfWork.EIOBloodTransfusionChecklistRepository.Update(btc);
            unitOfWork.Commit();
            return null;
        }
        private dynamic UpdateEIOBloodTransfusionChecklistPartII(EIOBloodTransfusionChecklist btc, JObject request)
        {
            var physician = btc.Physician;
            var nurse = btc.Nurse;
            if (physician != null || nurse != null)
                return Common.Message.OWNER_FORBIDDEN;
            btc.NumberOfBloodTransfusion = request["NumberOfBloodTransfusion"]?.ToString();
            btc.Crossmatch = request["Crossmatch"]?.ToString();
            var start_transfusion_at = request["StartTransfusionAt"]?.ToString();
            if (!string.IsNullOrEmpty(start_transfusion_at))
                btc.StartTransfusionAt = DateTime.ParseExact(start_transfusion_at, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                btc.StartTransfusionAt = null;
            var stop_transfusion_at = request["StopTransfusionAt"]?.ToString();
            if (!string.IsNullOrEmpty(stop_transfusion_at))
                btc.StopTransfusionAt = DateTime.ParseExact(stop_transfusion_at, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                btc.StopTransfusionAt = null;
            btc.ActualAmountOfBloodTransmitted = request["ActualAmountOfBloodTransmitted"]?.ToString();
            btc.Remark = request["Remark"]?.ToString();

            var datas = unitOfWork.EIOBloodTransfusionChecklistDataRepository.Find(
                e => !e.IsDeleted &&
                e.EIOBloodTransfusionChecklistId != null &&
                e.EIOBloodTransfusionChecklistId == btc.Id
            ).ToList();

            foreach (var item in request["Datas"])
            {
                var str_id = item.Value<string>("Id");
                var data = GetOrCreateEIOBloodTransfusionChecklistData(item.Value<string>("Id"), btc, datas);
                if (data != null)
                    UpdateEIOBloodTransfusionChecklistData(data, item, btc.Version);
            }

            DeleteTableDatas(request["TableDeleted"], datas);
            unitOfWork.EIOBloodTransfusionChecklistRepository.Update(btc);
            unitOfWork.Commit();
            return null;
        }

        private void DeleteTableDatas(JToken req, List<EIOBloodTransfusionChecklistData> datas)
        {
            var user = GetUser();
            foreach(var item in req)
            {
                var data = (from d in datas
                            where d.Id == item.Value<Guid>("Id")
                            select d).FirstOrDefault();

                if (data != null)
                    if(data.CreatedBy == user?.Username)
                        data.IsDeleted = true;
            }    
        }

        private EIOBloodTransfusionChecklistData GetOrCreateEIOBloodTransfusionChecklistData(string str_id, EIOBloodTransfusionChecklist btc, List<EIOBloodTransfusionChecklistData> datas)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                var data = new EIOBloodTransfusionChecklistData()
                {
                    EIOBloodTransfusionChecklistId = btc.Id,
                };
                unitOfWork.EIOBloodTransfusionChecklistDataRepository.Add(data);
                return data;
            }

            Guid data_id = new Guid(str_id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateEIOBloodTransfusionChecklistData(EIOBloodTransfusionChecklistData data, JToken item, int version)
        {
            var time = item.Value<string>("Time");
            if (!string.IsNullOrEmpty(time))
                data.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                data.Time = null;
            data.TransfusionSpeed = item.Value<string>("TransfusionSpeed");
            data.ColorOfSkin = item.Value<string>("ColorOfSkin");
            data.BreathsPerMinute = item.Value<string>("BreathsPerMinute");
            data.PulsePerMinute = item.Value<string>("PulsePerMinute");
            data.Temp = item.Value<string>("Temp");
            data.Other = item.Value<string>("Other");
           
            if(version == 2)
            {
                data.Period = item.Value<int>("Period");
                if (GetUser()?.Username == data.CreatedBy)
                    data.IsDeleted = item.Value<bool>("IsDeleted");
            }

            unitOfWork.EIOBloodTransfusionChecklistDataRepository.Update(data);
        }
        protected bool ConfirmEIOBloodTransfusionChecklist(EIOBloodTransfusionChecklist btc, Guid user_id, List<string> positions, string kind)
        {
            var success = false;
            if (kind == "Physician" && positions.Contains("Doctor") && btc.PhysicianId == null)
            {
                btc.PhysicianId = user_id;
                btc.PhysicianConfirmTime = DateTime.Now;
                success = true;
            }
            else if (kind == "Nurse" && positions.Contains("Nurse") && btc.NurseId == null)
            {
                btc.NurseId = user_id;
                btc.NurseConfirmTime = DateTime.Now;
                success = true;
            }
            else if (kind == "HeadOfLab" && btc.HeadOfLabId == null)
            {
                btc.HeadOfLabId = user_id;
                btc.HeadOfLabConfirmTime = DateTime.Now;
                success = true;
            }
            else if (kind == "FirstTechnician" && btc.FirstTechnicianId == null)
            {
                btc.FirstTechnicianId = user_id;
                btc.FirstTechnicianConfirmTime = DateTime.Now;
                success = true;
            }
            else if (kind == "SecondTechnician" && btc.SecondTechnicianId == null)
            {
                btc.SecondTechnicianId = user_id;
                btc.SecondTechnicianConfirmTime = DateTime.Now;
                success = true;
            }
            if (success)
            {
                unitOfWork.EIOBloodTransfusionChecklistRepository.Update(btc);
                unitOfWork.Commit();                                  
            }
            return success;
        }

        private string GetBed(Guid? form_id, string visit_type)
        {
            if (visit_type == "ED")
            {
                var bed = unitOfWork.EmergencyTriageRecordRepository.GetById((Guid)form_id)?.Bed;
                return bed;
            }
            return string.Empty;
        }

        private string EDGetAndFormatICD10(Guid? dischargeInfomationId)
        {
            string[] codes = { "DI0DIAICD", "DI0DIAOPT" };

            var string_json = (from d in unitOfWork.DischargeInformationDataRepository.AsQueryable()
                               where !d.IsDeleted && d.DischargeInformationId == dischargeInfomationId
                               && codes.Contains(d.Code)
                               select d).ToList();
            string result = String.Empty;

            foreach (var code in codes)
            {
                string text = string_json.FirstOrDefault(e => e.Code == code)?.Value;
                if (text == null || text == $"\"\"")
                    text = "";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(text);
                string _str = String.Empty;
                if (objs != null)
                {
                    int lengthOfobjs = objs.Count;
                    for (int j = 0; j < lengthOfobjs; j++)
                    {
                        var codeIcd10 = objs[j]["code"]?.ToString();
                        if (j == 0)
                            _str += codeIcd10;
                        else
                            _str += $", {codeIcd10}";
                    }

                    if (!string.IsNullOrEmpty(result))
                        result += "/ " + _str;
                    else
                        result += _str;
                }
            }

            if (string.IsNullOrEmpty(result))
                return "";

            string format = $" ({result})";
            return format;
        }
    }
}
