using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.PrescriptionModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDConsultationDrugWithAnAsteriskMarkController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/ConsultationDrugWithAnAsteriskMark/All/{visitId}")]
        [Permission(Code = "ICDWA2")]
        public IHttpActionResult GetAllForm(Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var forms = GetAllFormByVisit(visitId);
            if (forms == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanBenhNhanSuDungThuocCoDauSao),
                    ViMessage = "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*) không tồn tại",
                    EnMessage = "Minutes of consultation for patient using drug with an asterisk mark(*) is not found"
                });
            var IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanBenhNhanSuDungThuocCoDauSao);            

            return Content(HttpStatusCode.OK, new { Datas = forms, IsLocked });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/ConsultationDrugWithAnAsteriskMark/Create/{id}")]
        [Permission(Code = "ICDWA1")]
        public IHttpActionResult CreateIPDConsultationDrugWithAnAsteriskMarkAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanBenhNhanSuDungThuocCoDauSao))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }

            var consultation_drug = new IPDConsultationDrugWithAnAsteriskMark()
            {
                CreatedAt = DateTime.Now,
                CreatedBy = GetUser().Username,
                VisitId = id
            };
            unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.Add(consultation_drug);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { Message.SUCCESS, FormId = consultation_drug.Id });
        }

        [HttpGet]
        [Route("api/IPD/ConsultationDrugWithAnAsteriskMark/{id}/{formId}")]
        [Permission(Code = "ICDWA2")]
        public IHttpActionResult GetIPDConsultationDrugWithAnAsteriskMarkAPI(Guid id, Guid formId)
        {
          
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            bool IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanBenhNhanSuDungThuocCoDauSao, formId);
            var form = unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.GetById(formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    IsLocked,
                    ViMessage = "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*) không tồn tại",
                    EnMessage = "Minutes of consultation for patient using drug with an asterisk mark(*) is not found"
                });

            var consultation_drug = GetOrUpdateNewestDataConsultationDrug(ipd, form.Id);
            var director = consultation_drug.HospitalDirectorOrHeadDepartment;
            var doctor = consultation_drug.Doctor;            
            var DiagnosisAndICD = GetVisitDiagnosisAndICD(ipd.Id, "IPD", false);

            var order = unitOfWork.OrderRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == formId &&
                !string.IsNullOrEmpty(e.OrderType) &&
                e.OrderType == Constant.IPD_DRUG_WITH_AN_ASTERISK_MARK
            ).Select(e => new { e.Id, e.Drug, e.Reason, e.Concentration, e.Route });

            var response = new
            {
                IsLocked,
                consultation_drug.Id,
                consultation_drug.RoomNumber,
                AddmisionDate = consultation_drug.AddmisionDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConsultationDate = consultation_drug.ConsultationDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                consultation_drug.Diagnosis,
                DiagnosisAndICD,
                consultation_drug.AntibioticsTreatmentBefore,
                consultation_drug.DiagnosisAfterConsultation,
                HospitalDirectorOrHeadDepartmentTime = consultation_drug.HospitalDirectorOrHeadDepartmentTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HospitalDirectorOrHeadDepartment = new { director?.Username, director?.Fullname, director?.DisplayName },
                DoctorTime = consultation_drug.DoctorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Doctor = new { doctor?.Username, doctor?.Fullname, doctor?.DisplayName },
                Orders = order,
            };
            return Content(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/ConsultationDrugWithAnAsteriskMark/{id}/{formId}")]
        [Permission(Code = "ICDWA3")]
        public IHttpActionResult UpdateConsultationDrugWithAnAsteriskMarkAPI(Guid id, Guid formId, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanBenhNhanSuDungThuocCoDauSao, formId);            

            var consultation_drug = unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.GetById(formId);
            if (consultation_drug == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CDWAAM_NOT_FOUND);           
           
            if (IsLocked)
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            if (consultation_drug.HospitalDirectorOrHeadDepartmentId != null && consultation_drug.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            UpdateConsultationDrugWithAnAsteriskMark(ipd.Id, consultation_drug, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/ConsultationDrugWithAnAsteriskMark/Accept/{id}/{formId}")]
        [Permission(Code = "ICDWA4")]
        public IHttpActionResult AcceptConsultationDrugWithAnAsteriskMarkAPI(Guid id, Guid formId, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);           

            var consultation_drug = unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.GetById(formId);
            if (consultation_drug == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CDWAAM_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanBenhNhanSuDungThuocCoDauSao, formId))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var director = consultation_drug.HospitalDirectorOrHeadDepartment;
            var doctor = consultation_drug.Doctor;            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);  
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);            
            if (kind == "HospitalDirectorOrHeadDepartment" && positions.Contains("Director"))
            {
                consultation_drug.HospitalDirectorOrHeadDepartmentId = user.Id;
                consultation_drug.HospitalDirectorOrHeadDepartmentTime = DateTime.Now;
            }
            else if (kind == "Doctor" && positions.Contains("Doctor"))
            {
                consultation_drug.DoctorId = user.Id;
                consultation_drug.DoctorTime = DateTime.Now;
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
            unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.Update(consultation_drug);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        private string formatIcd(string icd)
        {
            var icdArr = TryFormatJson(icd);
            if (icdArr != null)
            {
                List<string> result = new List<string>();
                foreach (JToken icd10 in icdArr)
                {
                    result.Add(icd10.Value<string>("code"));
                }
                return String.Join(", ", result.ToArray());
            }
            return "";
        }
        private static dynamic TryFormatJson(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject(str);
            }
            catch
            {
                return null;
            }
        }
        private IPDConsultationDrugWithAnAsteriskMark GetOrUpdateNewestDataConsultationDrug(IPD ipd, Guid formId)
        {
            var consultation_drug = unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.GetById(formId);
            if (consultation_drug.HospitalDirectorOrHeadDepartmentId != null || consultation_drug.DoctorId != null)
                return consultation_drug;

            consultation_drug.AddmisionDate = ipd.AdmittedDate;

            var DiagnosisAndICD = GetVisitDiagnosisAndICD(ipd.Id, "IPD", false);
            List<string> icdCode = new List<string>();
            var Icd10String = formatIcd(DiagnosisAndICD.ICD);
            if (Icd10String != "") icdCode.Add(Icd10String);
            var ICD10OptionString = formatIcd(DiagnosisAndICD.ICDOption);
            if (ICD10OptionString != "") icdCode.Add(ICD10OptionString);

            string icd_code = "";
            if (icdCode.Count > 0) icd_code = string.Format("({0})", String.Join(", ", icdCode.ToArray()));

            consultation_drug.Diagnosis = string.Format("{0}, {1} {2}", DiagnosisAndICD.Diagnosis, DiagnosisAndICD.DiagnosisOption, icd_code);

            unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.Update(consultation_drug, is_anonymous: true, is_time_change:false);
            unitOfWork.Commit();
            return consultation_drug;
        }

        private void UpdateConsultationDrugWithAnAsteriskMark(Guid visit_id, IPDConsultationDrugWithAnAsteriskMark consultation_drug, JObject request)
        {
            consultation_drug.RoomNumber = request["RoomNumber"]?.ToString();
            var consultation_date_str = request["ConsultationDate"]?.ToString();
            if (!string.IsNullOrEmpty(consultation_date_str))
                consultation_drug.ConsultationDate = DateTime.ParseExact(consultation_date_str, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                consultation_drug.ConsultationDate = null;
            consultation_drug.AntibioticsTreatmentBefore = request["AntibioticsTreatmentBefore"]?.ToString();
            consultation_drug.DiagnosisAfterConsultation = request["DiagnosisAfterConsultation"]?.ToString();
            unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.Update(consultation_drug);

            foreach (var item in request["Orders"])
            {
                var str_id = item.Value<string>("Id");
                var data = GetOrCreateOrder(item.Value<string>("Id"), visit_id, consultation_drug.Id);
                if (data != null)
                    UpdateOrder(data, item);
            }
            unitOfWork.Commit();
        }

        private Order GetOrCreateOrder(string str_id, Guid visitId, Guid formId)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                var order = new Order()
                {
                    VisitId = visitId,
                    FormId = formId,
                    OrderType = Constant.IPD_DRUG_WITH_AN_ASTERISK_MARK
                };
                unitOfWork.OrderRepository.Add(order);
                return order;
            }

            Guid order_id = new Guid(str_id);
            return unitOfWork.OrderRepository.GetById(order_id);
        }

        private void UpdateOrder(Order order, JToken item)
        {
            order.Drug = item.Value<string>("Drug");
            order.Reason = item.Value<string>("Reason");
            order.Concentration = item.Value<string>("Concentration");
            order.Route = item.Value<string>("Route");
            unitOfWork.OrderRepository.Update(order);
        }

        private dynamic GetAllFormByVisit(Guid visitId)
        {
            var forms = unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.AsQueryable()
                       .Where(f => !f.IsDeleted && f.VisitId == visitId)
                       .ToList();

            if (forms == null || forms.Count == 0)
                return null;

            var datas = forms.Select(
                    f =>
                    {
                        return new
                        {
                            f.Id,
                            f.CreatedBy,
                            f.CreatedAt,
                            f.UpdatedBy,
                            f.UpdatedAt
                        };
                    }
                ).OrderBy(f => f.CreatedAt).ToList();

            return datas;
        }

    }

    public class JsonICD
    {
        public string code { get; set; }
        public string label { get; set; }
        public string ViName { get; set; }
    }
}