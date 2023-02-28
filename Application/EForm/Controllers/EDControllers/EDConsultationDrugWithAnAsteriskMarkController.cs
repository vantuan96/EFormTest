using DataAccess.Models;
using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDConsultationDrugWithAnAsteriskMarkController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/ConsultationDrugWithAnAsteriskMark/All/{visitId}")]
        [Permission(Code = "ICDWA2")]
        public IHttpActionResult GetAllForm(Guid visitId)
        {
            var ed = GetED(visitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var forms = GetAllFormByVisit(visitId);
            if (forms == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*) không tồn tại",
                    EnMessage = "Minutes of consultation for patient using drug with an asterisk mark(*) is not found"
                });

            return Content(HttpStatusCode.OK, new { Datas = forms });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/ConsultationDrugWithAnAsteriskMark/Create/{id}")]
        [Permission(Code = "ECDWA1")]
        public IHttpActionResult CreateEDConsultationDrugWithAnAsteriskMarkAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation_drug = new EDConsultationDrugWithAnAsteriskMark();
            consultation_drug.VisitId = ed.Id;
            unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.Add(consultation_drug);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { FormId = consultation_drug.Id, Message.SUCCESS });
        }

        [HttpGet]
        [Route("api/ED/ConsultationDrugWithAnAsteriskMark/{id}/{formId}")]
        [Permission(Code = "ECDWA2")]
        public IHttpActionResult GetEDConsultationDrugWithAnAsteriskMarkAPI(Guid id, Guid formId)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var form = unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.GetById(formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CDWAAM_NOT_FOUND);

            var consultation_drug = GetOrUpdateNewestDataConsultationDrug(ed, form);
            var director = consultation_drug.HospitalDirectorOrHeadDepartment;
            var doctor = consultation_drug.Doctor;

            var order = unitOfWork.OrderRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == ed.Id &&
                !string.IsNullOrEmpty(e.OrderType) &&
                e.OrderType == Constant.ED_DRUG_WITH_AN_ASTERISK_MARK &&
                e.FormId == consultation_drug.Id
            ).Select(e => new { e.Id, e.Drug, e.Reason, e.Concentration, e.Route });

            var response = new
            {
                consultation_drug.Id,
                consultation_drug.RoomNumber,
                AddmisionDate = consultation_drug.AddmisionDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConsultationDate = consultation_drug.ConsultationDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                consultation_drug.Diagnosis,
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
        [Route("api/ED/ConsultationDrugWithAnAsteriskMark/{id}/{formId}")]
        [Permission(Code = "ECDWA3")]
        public IHttpActionResult UpdateConsultationDrugWithAnAsteriskMarkAPI(Guid id, Guid formId, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation_drug = unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.GetById(formId);
            if (consultation_drug == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CDWAAM_NOT_FOUND);

            if (consultation_drug.HospitalDirectorOrHeadDepartmentId != null && consultation_drug.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            UpdateConsultationDrugWithAnAsteriskMark(ed.Id, consultation_drug, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/ConsultationDrugWithAnAsteriskMark/Accept/{id}/{formId}")]
        [Permission(Code = "ECDWA4")]
        public IHttpActionResult AcceptConsultationDrugWithAnAsteriskMarkAPI(Guid id, Guid formId, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var consultation_drug = unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.GetById(formId);
            if (consultation_drug == null)
                return Content(HttpStatusCode.NotFound, Message.ED_CDWAAM_NOT_FOUND);

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
            unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.Update(consultation_drug);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private EDConsultationDrugWithAnAsteriskMark GetOrUpdateNewestDataConsultationDrug(ED ed, EDConsultationDrugWithAnAsteriskMark form)
        {
            if (form.HospitalDirectorOrHeadDepartmentId != null || form.DoctorId != null)
                return form;

            var etr = ed.EmergencyTriageRecord;
            form.AddmisionDate = etr.TriageDateTime;

            var emer = ed.EmergencyRecord;
            var emer_diagnosis = emer.EmergencyRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0ID0ANS")?.Value;
            var di = ed.DischargeInformation;
            var di_diagnosis = di.DischargeInformationDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
            form.Diagnosis = !string.IsNullOrEmpty(di_diagnosis) ? di_diagnosis : emer_diagnosis;

            unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.Update(form, is_anonymous: true, is_time_change:false);
            unitOfWork.Commit();
            return form;
        }

        private void UpdateConsultationDrugWithAnAsteriskMark(Guid visit_id, EDConsultationDrugWithAnAsteriskMark consultation_drug, JObject request)
        {
            consultation_drug.RoomNumber = request["RoomNumber"]?.ToString();
            var consultation_date_str = request["ConsultationDate"]?.ToString();
            if (!string.IsNullOrEmpty(consultation_date_str))
                consultation_drug.ConsultationDate = DateTime.ParseExact(consultation_date_str, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                consultation_drug.ConsultationDate = null;
            consultation_drug.AntibioticsTreatmentBefore = request["AntibioticsTreatmentBefore"]?.ToString();
            consultation_drug.DiagnosisAfterConsultation = request["DiagnosisAfterConsultation"]?.ToString();
            unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.Update(consultation_drug);

            foreach (var item in request["Orders"])
            {
                var str_id = item.Value<string>("Id");
                var data = GetOrCreateOrder(item.Value<string>("Id"), visit_id, consultation_drug.Id);
                if (data != null)
                    UpdateOrder(data, item);
            }
            unitOfWork.Commit();
        }

        private Order GetOrCreateOrder(string str_id, Guid visit_id, Guid formId)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                var order = new Order()
                {
                    VisitId = visit_id,
                    FormId = formId,
                    OrderType = Constant.ED_DRUG_WITH_AN_ASTERISK_MARK
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
            var forms = unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.AsQueryable()
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
}
