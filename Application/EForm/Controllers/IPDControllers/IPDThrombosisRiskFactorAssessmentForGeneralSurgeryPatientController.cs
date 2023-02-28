using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/ThrombosisRiskFactorAssessmentForGeneralSurgery/All/{visitId}")]
        [Permission(Code ="TTMNK01")]
        public IHttpActionResult GetListIPDThrombosisRiskFactorAssessment(Guid visitId, [FromBody] JObject request)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            int numberPage;
            var thrombos = FilterInitialAssessmentsForGeneralSurgery(visit.Id, out numberPage, request);
            if (thrombos == null || thrombos.Count == 0)
                return Content(HttpStatusCode.NotFound, Message.IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_FOR_GENERAL_SURGERY_NOT_FOUND);

            var datas = BuilListThrombosisRiskFactorAssessmentForGeneralSurgery(thrombos);
            var isLocked = IPDIsBlock(visit, Constant.IPDFormCode.DanhGiaNguyCoThuyenTacMachNgoaiKhoa);

            return Content(HttpStatusCode.OK, new { Datas = datas, IsLocked = isLocked, NumberPage = numberPage });
        }
        [HttpGet]
        [Route("api/IPD/ThrombosisRiskFactorAssessmentForGeneralSurgery/Detail/{visitId}/{formId}")]
        [Permission(Code = "TTMNK01")]
        public IHttpActionResult GetDetailIPDThrombosisRiskFactorAssessment(Guid visitId, Guid formId)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var thrombosisRisk = GetIPDThrombosisRiskFactorAssessmentForGeneralSurgery(visit.Id, formId);
            if (thrombosisRisk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_FOR_GENERAL_SURGERY_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuilDetailThrombosisRiskFactorAssessmentForGeneralSurgery(thrombosisRisk));
        }

        [HttpPost]
        [Route("api/IPD/ThrombosisRiskFactorAssessmentForGeneralSurgery/Create/{visitId}")]
        [Permission(Code = "TTMNK03")]
        public IHttpActionResult CreateIPDThrombosisRiskFactorAssessment(Guid visitId, [FromBody] JObject request)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var checkIsLocked = IPDIsBlock(visit, Constant.IPDFormCode.DanhGiaNguyCoThuyenTacMachNgoaiKhoa);
            if (checkIsLocked)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient thrombo = null;
            var status = CreateOfUpdateIPThromBoRisk(visit.Id, ref thrombo);
            if (status == "Success")
                return Content(HttpStatusCode.OK, new { IdForm = thrombo.Id, Message.SUCCESS });
           
            return Content(HttpStatusCode.BadRequest, new { Viname = "Tạo Form thất bại!, Có lỗi sảy ra" });
        }

        [HttpPost]
        [Route("api/IPD/ThrombosisRiskFactorAssessmentForGeneralSurgery/Update/{visitId}/{formId}")]
        [Permission(Code = "TTMNK02")]
        public IHttpActionResult UpdateIPDThrombosisRiskFactorAssessment(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var thrombo = GetIPDThrombosisRiskFactorAssessmentForGeneralSurgery(visitId, formId);
            if (thrombo == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_FOR_GENERAL_SURGERY_NOT_FOUND);

            var curren_User = GetUser().Username;
            if (thrombo.CreatedBy != curren_User)
                return Content(HttpStatusCode.BadRequest, new { Viname = "Bạn không có quền chỉnh sửa form này" });

            var success = HandleUpdateOrCreateThrombosisRiskFactorAssessmentForGeneralSurgery(thrombo, visitId, request);
            if (success != "Success")
                return Content(HttpStatusCode.BadRequest, new { ViName = "Thất bại", EnName = "Falie" });
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private string HandleUpdateOrCreateThrombosisRiskFactorAssessmentForGeneralSurgery(IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient thrombosis_risk, Guid visitId, JObject request)
        {
            if (request["StartDate"].ToString() == null || request["StartDate"].ToString() == "")
                thrombosis_risk.StartDate = DateTime.Now;
            else
            {
                DateTime startDate;
                bool success = DateTime.TryParseExact(request["StartDate"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                if (!success)
                    return "Start Date Error";
                thrombosis_risk.StartDate = startDate;
            }

            if (request["FinishDate"].ToString() == null || request["FinishDate"].ToString() == "")
                thrombosis_risk.FinishDate = null;
            else
            {
                DateTime enddate;
                bool succes = DateTime.TryParseExact(request["FinishDate"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out enddate);
                if (!succes)
                    return "End Date Error";
                if (enddate < thrombosis_risk.StartDate)
                    return "End Date Error";
                thrombosis_risk.FinishDate = enddate;
            }

            var datas = unitOfWork.FormDatasRepository.AsQueryable()
                        .Where(e => !e.IsDeleted && e.FormId == thrombosis_risk.Id)
                        .ToList();

            foreach (var item in request["Datas"])
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateThrombosisRiskFactorAssessmentDataToFormDatas(code, thrombosis_risk.Id, visitId, datas);
                if (data == null) continue;

                UpdateIPDThrombosisRiskFactorAssessmentDataToFormDatas(item["Value"]?.ToString(), data);
            }

            //unitOfWork.IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepository.Update(thrombosis_risk);
            string createSuccess = CreateOfUpdateIPThromBoRisk(visitId, ref thrombosis_risk);
            if (createSuccess != "Success")
                return "Faile";
            unitOfWork.Commit();
            return "Success";
        }

        private int NumberAgeCustumer(IPD visit, int now)
        {
            var custumer = visit.Customer;
            int birtYear_custumer = custumer.DateOfBirth.Value.Year;
            int age_custumer = now - birtYear_custumer;
            return age_custumer;
        }

        private void UpdateIPDThrombosisRiskFactorAssessmentDataToFormDatas(string val, FormDatas data)
        {
            data.Value = val;
            data.UpdatedAt = DateTime.Now;
            data.UpdatedBy = GetUser().Username;
            unitOfWork.FormDatasRepository.Update(data);
        }

        private FormDatas GetOrCreateThrombosisRiskFactorAssessmentDataToFormDatas(string code, Guid formId, Guid visitId, List<FormDatas> datas)
        {
            var data = datas.Where(d => d.Code == code).FirstOrDefault();
            if (data != null)
                return data;
            data = new FormDatas()
            {
                Code = code,
                FormId = formId,
                VisitId = visitId,
                FormCode = Constant.IPDFormCode.DanhGiaNguyCoThuyenTacMachNgoaiKhoa,
                VisitType = "IPD",
                CreatedAt = DateTime.Now,
                CreatedBy = GetUser().Username,
                UpdatedAt = DateTime.Now,
                UpdatedBy = GetUser().Username,
            };
            unitOfWork.FormDatasRepository.Add(data);
            return data;
        }

        private string CreateOfUpdateIPThromBoRisk(Guid visitId, ref IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient thrombo)
        {
            var masterdata = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                              where !m.IsDeleted && m.Form == "" && m.Level == 1
                              select new
                              {
                                  Viname = m.ViName,
                                  EnName = m.EnName,
                                  Code = m.Code
                              }).ToList();

            JavaScriptSerializer converTostring = new JavaScriptSerializer();
            if (thrombo == null)
            {
                var newIPDThromboRisk = new IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient()
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = GetUser().Username,
                    CreatedBy = GetUser().Username,
                    VisitId = visitId,
                    CapriniScoreCalculator = converTostring.Serialize((from m in masterdata
                                                                       where m.Code == ""
                                                                       select m).FirstOrDefault()),
                    IndividualBleedingRiskScore = converTostring.Serialize((from m in masterdata
                                                                            where m.Code == ""
                                                                            select m).FirstOrDefault()),
                    BaselineSurgicalBleedingRisk = converTostring.Serialize((from m in masterdata
                                                                             where m.Code == ""
                                                                             select m).FirstOrDefault()),
                    AntiFreeze = converTostring.Serialize((from m in masterdata
                                                           where m.Code == ""
                                                           select m).FirstOrDefault()),
                    MechanicalProphylaxis = converTostring.Serialize((from m in masterdata
                                                                      where m.Code == ""
                                                                      select m).FirstOrDefault()),
                    ThromboembolismProphylaxis = converTostring.Serialize((from m in masterdata
                                                                           where m.Code == ""
                                                                           select m).FirstOrDefault()),
                };
                unitOfWork.IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepository.Add(newIPDThromboRisk);
                thrombo = newIPDThromboRisk;
            }
            else
            {
                var idForm = thrombo.Id;

                var data = (from f in unitOfWork.FormDatasRepository.AsQueryable()
                            where !f.IsDeleted && f.FormId == idForm && f.Value != ""
                            && f.Value != "False" && !string.IsNullOrEmpty(f.Value)
                            join mas in unitOfWork.MasterDataRepository.AsQueryable()
                            on f.Code equals mas.Code
                            select new
                            {
                                ViName = mas.ViName,
                                EnName = mas.EnName,
                                Code = mas.Code,
                                Value = f.Value,
                                Group = mas.Group
                            }).ToList();
                var data_master = (from m in masterdata
                                   join d in data
                                   on m.Code equals d.Group into query
                                   from q in query.DefaultIfEmpty()
                                   select new
                                   {
                                       Father = new { m.Code, m.Viname, m.EnName },
                                       Children = (q == null) ? null : new { q.Code, q.ViName, q.EnName, q.Value }
                                   } into data_query
                                   group data_query by data_query.Father into data_choices
                                   select new
                                   {
                                       Father = data_choices.Key,
                                       Children = data_choices.ToList().Select(s => s.Children)
                                   }).ToList();


                thrombo.CapriniScoreCalculator = converTostring.Serialize((from d in data_master
                                                                           where d.Father.Code == ""
                                                                           select d).FirstOrDefault());
                thrombo.IndividualBleedingRiskScore = converTostring.Serialize((from d in data_master
                                                                                where d.Father.Code == ""
                                                                                select d).FirstOrDefault());
                thrombo.BaselineSurgicalBleedingRisk = converTostring.Serialize((from d in data_master
                                                                                 where d.Father.Code == ""
                                                                                 select d).FirstOrDefault());
                thrombo.AntiFreeze = converTostring.Serialize((from d in data_master
                                                               where d.Father.Code == ""
                                                               select d).FirstOrDefault());
                thrombo.MechanicalProphylaxis = converTostring.Serialize((from d in data_master
                                                                          where d.Father.Code == ""
                                                                          select d).FirstOrDefault());
                thrombo.ThromboembolismProphylaxis = converTostring.Serialize((from d in data_master
                                                                               where d.Father.Code == ""
                                                                               select d).FirstOrDefault());
                thrombo.UpdatedAt = DateTime.Now;
                thrombo.UpdatedBy = GetUser().Username;
                unitOfWork.IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepository.Update(thrombo);
            }
            return "Success";
        }

        private IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient GetIPDThrombosisRiskFactorAssessmentForGeneralSurgery(Guid visitId, Guid formId)
        {
            var thrombo = unitOfWork.IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepository
                          .FirstOrDefault(t => !t.IsDeleted && t.VisitId == visitId && t.Id == formId);
            return thrombo;
        }

        private List<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient> FilterInitialAssessmentsForGeneralSurgery(Guid visitId, out int numberPage, JObject request)
        {
            DateTime? start;
            DateTime startDateFillter;
            bool success_start = DateTime.TryParseExact(request["StartDateFillter"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateFillter);
            if (success_start)
                start = startDateFillter;
            else start = null;

            DateTime? end;
            DateTime endDateFillter;
            bool success_end = DateTime.TryParseExact(request["EndDateFillter"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateFillter);
            if (success_end)
                end = endDateFillter;
            else
                end = null;

            string userNameOfFullName = request["UserNameOfFullName"].ToString();

            int indexPage;
            bool sucess_int = int.TryParse(request["IndexPage"].ToString(), out indexPage);
            if (!sucess_int)
                indexPage = 1;

            var listThrombo = GetListIPDThrombosisRiskFactorAssessmentsForGeneralSurgery(visitId);
            if (start == null)
                start = listThrombo.Min(t => t.StartDate);
            if (end == null)
                end = listThrombo.Max(t => t.FinishDate);

            if (userNameOfFullName != null && userNameOfFullName != "")
            {
                var list_user = (from u in unitOfWork.UserRepository.AsQueryable()
                                 where !u.IsDeleted && !u.IsLocked && u.Username.Contains(userNameOfFullName)
                                 || u.Fullname.Contains(userNameOfFullName)
                                 select u.Username).ToList();
                listThrombo = (from l in listThrombo
                               where !string.IsNullOrEmpty(l.CreatedBy)
                               && list_user.Contains(l.CreatedBy)
                               && l.StartDate >= start && l.FinishDate <= end
                               select l).OrderByDescending(l => l.CreatedAt).ToList();
                numberPage = (listThrombo.Count % 5) == 0 ? listThrombo.Count / 5 : listThrombo.Count / 5 + 1;
                listThrombo = listThrombo.Skip((indexPage - 1) * 5).Take(5).OrderByDescending(c => c.CreatedAt).ToList();
                return listThrombo;
            }

            if (userNameOfFullName == null)
                userNameOfFullName = GetUser().Username;

            numberPage = 0;
            listThrombo = (from l in listThrombo
                           where l.CreatedBy.Contains(userNameOfFullName)
                           && l.StartDate >= start && l.FinishDate <= end
                           select l).OrderByDescending(l => l.CreatedAt).ToList();
            numberPage = (listThrombo.Count % 5) == 0 ? listThrombo.Count / 5 : listThrombo.Count / 5 + 1;
            listThrombo = listThrombo.Skip((indexPage - 1) * 5).Take(5).OrderByDescending(c => c.CreatedAt).ToList();
            return listThrombo;
        }
        private List<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient> GetListIPDThrombosisRiskFactorAssessmentsForGeneralSurgery(Guid visitId)
        {

            var thrombosisRisks = unitOfWork.IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepository.AsQueryable()
                                 .Where(t => !t.IsDeleted && t.VisitId == visitId)
                                 .OrderByDescending(t => t.CreatedAt).ToList();
            return thrombosisRisks;
        }

        private dynamic BuilDetailThrombosisRiskFactorAssessmentForGeneralSurgery(IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient thrombosisRick)
        {
            var data = unitOfWork.FormDatasRepository.AsQueryable()
                       .Where(d => !d.IsDeleted && d.VisitId == thrombosisRick.VisitId
                                    && d.FormId == thrombosisRick.Id
                       ).Select(d => new
                       {
                           d.Id,
                           d.Code,
                           d.Value
                       }).ToList();

            IPD visit = GetIPD(thrombosisRick.VisitId);
            var userCreate = thrombosisRick.CreatedBy;
            var curren_user = GetUser().Username;
            bool isUserCreated = curren_user == userCreate ? true : false;
            int age = NumberAgeCustumer(visit, thrombosisRick.CreatedAt.Value.Year);
            return new
            {
                VisitId = thrombosisRick.VisitId,
                TableId = thrombosisRick.Id,
                IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.DanhGiaNguyCoThuyenTacMachNgoaiKhoa),
                CreateAt = thrombosisRick.CreatedAt,
                CreateBy = thrombosisRick.CreatedBy,
                StartDate = thrombosisRick.StartDate,
                FinishDate = thrombosisRick.FinishDate,
                IsUserCreated = isUserCreated,
                Age = age,
                Datas = data
            };
        }

        private dynamic BuilListThrombosisRiskFactorAssessmentForGeneralSurgery(List<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient> listThrombo)
        {
            if (listThrombo == null || listThrombo.Count == 0)
                return Content(HttpStatusCode.NotFound, Message.IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_FOR_GENERAL_SURGERY_NOT_FOUND);

            JavaScriptSerializer json = new JavaScriptSerializer();
            var datas = (from thrombos in listThrombo
                         select new
                         {
                             VisitId = thrombos.VisitId,
                             TableId = thrombos.Id,
                             CreateBy = thrombos.CreatedBy,
                             CreateAt = thrombos.CreatedAt,
                             UpddateBy = thrombos.UpdatedBy,
                             UpdateAt = thrombos.UpdatedAt,
                             StartDate = thrombos.StartDate,
                             FinishDate = thrombos.FinishDate,
                             CapriniScoreCalculator = json.Deserialize<Dictionary<dynamic, dynamic>>(thrombos.CapriniScoreCalculator),
                             IndividualBleedingRiskScore = json.Deserialize<Dictionary<dynamic, dynamic>>(thrombos.IndividualBleedingRiskScore),
                             BaselineSurgicalBleedingRisk = json.Deserialize<Dictionary<dynamic, dynamic>>(thrombos.BaselineSurgicalBleedingRisk),
                             AntiFreeze = json.Deserialize<Dictionary<dynamic, dynamic>>(thrombos.AntiFreeze),
                             MechanicalProphylaxis = json.Deserialize<Dictionary<dynamic, dynamic>>(thrombos.MechanicalProphylaxis),
                             ThromboembolismProphylaxis = json.Deserialize<Dictionary<dynamic, dynamic>>(thrombos.ThromboembolismProphylaxis)
                         }).ToList();

            var datas_choices = (from d in datas
                                 select new
                                 {
                                     VisitId = d.VisitId,
                                     TableId = d.TableId,
                                     CreateBy = d.CreateBy,
                                     CreateAt = d.CreateAt,
                                     UpddateBy = d.UpddateBy,
                                     UpdateAt = d.UpdateAt,
                                     StartDate = d.StartDate,
                                     FinishDate = d.FinishDate,
                                     Datas = new List<dynamic>()
                                                 {
                                                     d.CapriniScoreCalculator == null ? null:d.CapriniScoreCalculator,
                                                     d.BaselineSurgicalBleedingRisk == null ? null:d.BaselineSurgicalBleedingRisk,
                                                     d.IndividualBleedingRiskScore == null ? null : d.IndividualBleedingRiskScore,
                                                     d.AntiFreeze == null ? null : d.AntiFreeze,
                                                     d.MechanicalProphylaxis == null ? null : d.MechanicalProphylaxis,
                                                     d.ThromboembolismProphylaxis == null ? null : d.ThromboembolismProphylaxis
                                                 }
                                 }).ToList();

            return datas_choices;
        }
    }
}
