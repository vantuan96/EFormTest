using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.IPDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDThrombosisRiskFactorAssessmentController: BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/ThrombosisRiskFactorAssessment/")]
        [Permission(Code = "IPDTRFA1")]
        public IHttpActionResult GetIPDThrombosisRiskFactorAssessment([FromUri] IPDThrombosisRiskFactorAssessmentParams request)
        {
            Guid ipdId = Guid.Parse(request.IPDId.ToString());
            IPD ipd = GetIPD(ipdId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;
            Nullable<DateTime> fromDate = new DateTime();
            Nullable<DateTime> toDate = new DateTime();
            string assessor = request.Assessor;
            string formCode = request.FormCode;

            if (string.IsNullOrEmpty(formCode))
                return Content(HttpStatusCode.BadRequest, new { ViName = "Mã form sai định dạng", EnName = "Form code is wrong format" });

            if (request.FromDate != null)
            {
                fromDate = DateTime.ParseExact(request.FromDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }
            else
            {
                fromDate = null;
            }

            if (request.ToDate != null)
            {
                toDate = DateTime.ParseExact(request.ToDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }
            else
            {
                toDate = null;
            }
            var thrombosisRisk = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository
                .Find(
                    e => !e.IsDeleted &&
                    e.IPDId != null &&
                    e.IPDId == ipd.Id &&
                    e.FormCode == formCode
                )
                .OrderByDescending(e => e.StartingAssessment)
                .Select(e => new
                {
                    ItemId = e.Id,
                    StartingAssessment = e.StartingAssessment,
                    FinishingAssessment = e.FinishingAssessment,
                    PaduaTotalPoint = e.PaduaTotalPoint,
                    IMPROVETotalPoint = e.IMPROVETotalPoint,
                    CreatedAt = e.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    CreatedBy = e.CreatedBy,
                    Is24hLocked = IPDIsBlock(ipd, formCode, e.Id),
                    VTERiskFactors = e.VTERiskFactors,
                    ConditionOfPatient = e.ConditionOfPatient,
                    MechanicalProphylaxis = e.MechanicalProphylaxis,
                    ContraindicationsForAntiCoagulant = e.ContraindicationsForAntiCoagulant,
                    UpdateBy = e.UpdatedBy,
                    UpdateAt = e.UpdatedAt,
                    Version = ipd.Version >= 7 ? ipd.Version : 1,
                    ConfirmCreated = GetInfoConfirm(e.Id)

                });
            
            if (fromDate != null && toDate != null)
            {
                thrombosisRisk = thrombosisRisk.Where(e => e.StartingAssessment >= fromDate && e.FinishingAssessment <= toDate);
            }

            if (assessor != null)
            {
                thrombosisRisk = thrombosisRisk.Where(e => e.CreatedBy.ToUpper() == assessor.ToUpper());
            }
            var count = thrombosisRisk.Count();
            thrombosisRisk = thrombosisRisk.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return Content(HttpStatusCode.OK, new 
            {
                thrombosisRisk,
                Count = count,
                Is24hLock = IPDIsBlock(ipd, formCode),
                LastUpdate = GetTimeOfLastEdit(ipd.Id),
                Version = ipd.Version >= 7 ? ipd.Version : 1,
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/ThrombosisRiskFactorAssessment/Create/{id}")]
        [Permission(Code = "IPDTRFA2")]
        public IHttpActionResult CreateIPDThrombosisRiskFactorAssessment(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (string.IsNullOrEmpty(request["FormCode"]?.ToString()))
                return Content(HttpStatusCode.BadRequest, new { ViName = "Mã form sai định dạng", EnName = "Form code is wrong format" });

            if (IPDIsBlock(ipd, request["FormCode"]?.ToString()))
                return Content(HttpStatusCode.BadRequest, new { ViName = "Hồ sơ đã khóa không thể chỉnh sửa", EnName = "Profile is locked and cannot be edited" });

            var thrombosisRisk = new IPDThrombosisRiskFactorAssessment
            {
                IPDId = ipd.Id,
                PaduaTotalPoint = Convert.ToDouble(request["paduaTotalPoint"]),
                IMPROVETotalPoint = Convert.ToDouble(request["IMPROVETotalPoint"]),
                StartingAssessment = Convert.ToDateTime(request["startingAssessment"]),
                FinishingAssessment = Convert.ToDateTime(request["finishingAssessment"]),
                FormCode = request["FormCode"]?.ToString()
            };

            unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.Add(thrombosisRisk);

            HandleUpdateOrCreateThrombosisRiskFactorAssessment(thrombosisRisk, request);
            CreateOrUpdateFormForSetupOfAdmin(ipd.Id, thrombosisRisk.Id, thrombosisRisk.FormCode);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/ThrombosisRiskFactorAssessment/Detail/{id}")]
        [Permission(Code = "IPDTRFA3")]
        public IHttpActionResult GetDetailIPDThrombosisRiskFactorAssessment(Guid id)
        {
            var thrombosisRisk = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.GetById(id);
            if (thrombosisRisk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_NOT_FOUND);

            var ipd = thrombosisRisk.IPD;
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;

            var datas = thrombosisRisk.IPDThrombosisRiskFactorAssessmentDatas
                .Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value, e.CreatedBy });
            var confirmInfo = unitOfWork.EIOFormConfirmRepository.FirstOrDefault(e => !e.IsDeleted && e.FormId == id);
            var confirm = GetInfoConfirm(id);

            return Content(HttpStatusCode.OK, new
            {
                IPDId = thrombosisRisk.IPDId,
                PaduaTotalPoint = thrombosisRisk.PaduaTotalPoint,
                IMPROVETotalPoint = thrombosisRisk.IMPROVETotalPoint,
                StartingAssessment = thrombosisRisk.StartingAssessment,
                FinishingAssessment = thrombosisRisk.FinishingAssessment,
                CreatedBy = thrombosisRisk.CreatedBy,
                Datas = datas,
                FormCode = thrombosisRisk.FormCode,
                Version = ipd.Version >= 7? ipd.Version : 1,
                ConfirmCreated = confirm,
            });
        }

        [HttpPost]
        [Route("api/IPD/ThrombosisRiskFactorAssessment/Update/{id}")]
        [Permission(Code = "IPDTRFA4")]
        public IHttpActionResult UpdateIPDThrombosisRiskFactorAssessment(Guid id, [FromBody] JObject request)
        {
            var thrombosisRisk = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.GetById(id);
            if (thrombosisRisk == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_THROMBOSISRISK_FACTOR_ASSESSMENT_NOT_FOUND);

            var ipd = thrombosisRisk.IPD;
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            string formCode = request["FormCode"]?.ToString();
            if (string.IsNullOrEmpty(formCode))
                return Content(HttpStatusCode.BadRequest, new { ViName = "Mã form sai định dạng", EnName = "Form code is wrong format" });

            if (formCode == Constant.IPDFormCode.DanhGiaNguyCoThuyenTacMachNgoaiKhoa)
            {
                string curren_user = GetUser().Username;
                if (curren_user != thrombosisRisk.CreatedBy)
                    return Content(HttpStatusCode.BadRequest, new { ViName = "Bạn không có quyền chỉnh sửa form này", EnName = "You do not have permission to edit this form" });

                if(IPDIsBlock(ipd, formCode))
                    return Content(HttpStatusCode.BadRequest, new { ViName = "Hồ sơ đã khóa không thể chỉnh sửa", EnName = "Profile is locked and cannot be edited" });
            }

            HandleUpdateOrCreateThrombosisRiskFactorAssessment(thrombosisRisk, request);
            CreateOrUpdateFormForSetupOfAdmin(ipd.Id, thrombosisRisk.Id, thrombosisRisk.FormCode);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }       
        private void HandleUpdateOrCreateThrombosisRiskFactorAssessment(IPDThrombosisRiskFactorAssessment thrombosis_risk, JObject request)
        {
            thrombosis_risk.PaduaTotalPoint = Convert.ToDouble(request["paduaTotalPoint"]);
            thrombosis_risk.IMPROVETotalPoint = Convert.ToDouble(request["IMPROVETotalPoint"]);
            thrombosis_risk.StartingAssessment = DateTime.ParseExact(request["StartingAssessment"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
            thrombosis_risk.FinishingAssessment = DateTime.ParseExact(request["FinishingAssessment"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);

            var datas = thrombosis_risk.IPDThrombosisRiskFactorAssessmentDatas.Where(e => !e.IsDeleted).ToList();

            thrombosis_risk.VTERiskFactors = request["VTERiskFactors"]?.ToString();
            thrombosis_risk.ConditionOfPatient = request["ConditionOfPatient"]?.ToString();
            thrombosis_risk.MechanicalProphylaxis = request["MechanicalProphylaxis"]?.ToString();
            thrombosis_risk.ContraindicationsForAntiCoagulant = request["ContraindicationsForAntiCoagulant"]?.ToString();
            thrombosis_risk.FormCode = request["FormCode"]?.ToString();

            foreach (var item in request["Datas"])
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateThrombosisRiskFactorAssessmentData(code, thrombosis_risk.Id, datas);
                if (data == null) continue;

                UpdateIPDThrombosisRiskFactorAssessmentData(item["Value"]?.ToString(), data);
            }

            unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.Update(thrombosis_risk);
            unitOfWork.Commit();
        }
        private EIOFormConfirm GetConfirm(Guid id)
        {
            return unitOfWork.EIOFormConfirmRepository.Find(e => e.FormId == id).OrderByDescending(e => e.CreatedAt).FirstOrDefault();
        }
        private IPDThrombosisRiskFactorAssessmentData GetOrCreateThrombosisRiskFactorAssessmentData(string code, Guid form_id, List<IPDThrombosisRiskFactorAssessmentData> datas)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data != null) return data;

            data = new IPDThrombosisRiskFactorAssessmentData
            {
                IPDThrombosisRiskFactorAssessmentId = form_id,
                Code = code
            };
            unitOfWork.IPDThrombosisRiskFactorAssessmentDataRepository.Add(data);

            return data;
        }

        private void UpdateIPDThrombosisRiskFactorAssessmentData(string value, IPDThrombosisRiskFactorAssessmentData data)
        {
            data.Value = value;
            unitOfWork.IPDThrombosisRiskFactorAssessmentDataRepository.Update(data);
        }

        private dynamic GetTimeOfLastEdit(Guid visit_Id)
        {
            var throm = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.AsQueryable()
                        .Where(
                            th => !th.IsDeleted
                            && th.IPDId == visit_Id
                            && th.FormCode == "A01_049_050919_VE"
                        ).OrderByDescending(t => t.UpdatedAt).ToList();

            if(throm == null || throm.Count == 0)               
                return null;

            return new
            {
                UpdateAt = throm[0].UpdatedAt?.ToString("HH:mm dd/MM/yyy"),
                UpdateBy = throm[0].UpdatedBy
            };
        }
        private dynamic GetInfoConfirm(Guid id)
        {
            dynamic Confirmcreated = null;

            var confirm = GetConfirm(id);
            if(confirm != null)
            {
                Confirmcreated = new
                {
                    ConfirmAt = confirm.IsDeleted ? null : confirm.ConfirmAt,
                    ConfirmBy = confirm.IsDeleted ? null : confirm.ConfirmBy,
                    ConfirmType = confirm.IsDeleted ? null : confirm.ConfirmType,
                    IsUnlockConfirm = confirm.IsDeleted
                };
            }            
            return Confirmcreated;
        }
    }
}
