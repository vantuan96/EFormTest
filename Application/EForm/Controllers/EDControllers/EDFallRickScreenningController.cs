using DataAccess.Models.EDModel;
using DataAccess.Models.GeneralModel;
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

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDFallRickScreenningController : BaseEDApiController
    {
        private const string formCode = "A02_007_220321_VE_VERSION2";

        [HttpGet]
        [Route("api/ED/FallRiskScreening/Detail/{formId}")]
        [Permission(Code = "XEMSLN")]
        public IHttpActionResult GetDetail(Guid formId)
        {
            var form = GetEDFallRickScreenningById(formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.ED_FALL_NOT_FOUND);

            ED visit = GetED(form.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var datas = (from d in unitOfWork.FormDatasRepository.AsQueryable()
                         where !d.IsDeleted && d.FormCode == formCode
                         && d.FormId == form.Id
                         select new
                         {
                             d.Code,
                             d.Value
                         }).ToList();

            var responsive = new
            {
                Id = form.Id,
                VisitId = form.VisitId,
                TransessionDate = form.TransessionDate,
                CreatedBy = form.CreatedBy,
                Datas = datas,
                Version = visit.Version >= 7 ? visit.Version.ToString() : "3",
                Clinic = form.ClinicName,
                form.UpdatedAt,
                form.UpdatedBy
            };

            return Content(HttpStatusCode.OK, responsive);

        }

        [HttpGet]
        [Route("api/ED/FallRiskScreening/All/{visitId}")]
        [Permission(Code = "XEMSLN")]
        public IHttpActionResult GetAllForms(Guid visitId)
        {
            ED visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var forms = GetAllFormsByVisit(visit.Id);

            var result = forms.OrderBy(f => f.CreatedAt).ToList();

            return Content(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("api/ED/FallRiskScreening/Create/{visitId}")]
        [Permission(Code = "TAOSLN")]
        public IHttpActionResult CreateForm(Guid visitId)
        {
            ED visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var newForm = CreateFormByVisit(visit);
            if (newForm == null)
                return Content(HttpStatusCode.BadRequest, new { ViName = "Lượt khám này không được sử dụng biểu mẫu này!", EnName = "This visit cannot use this form!" });

            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { Id = newForm.Id });
        }

        [HttpPost]
        [Route("api/ED/FallRiskScreening/Update/{formId}")]
        [Permission(Code = "SUASLN")]
        public IHttpActionResult UpdateForm(Guid formId, [FromBody] JObject req)
        {
            var form = GetEDFallRickScreenningById(formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.ED_FALL_NOT_FOUND);

            ED ed = GetED(form.VisitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            string curren_user = GetUser().Username;
            if (curren_user != form.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.FORBIDDEN);

            UpdateForm(form, req);
            HandleUpdateOrCreateEDFallRickScreenningData(form, req["Datas"]);
            unitOfWork.Commit();
            var lastFallRick = unitOfWork.EDFallRickScreenningRepository.Find(x => !x.IsDeleted && x.VisitId == form.VisitId).ToList().OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            bool isHasFallRiskScreening = false;
            if (lastFallRick.Id == formId)
            {
                int countForm = unitOfWork.FormDatasRepository.Count(e => e.Value == "1" && e.FormId == form.Id && !e.IsDeleted && e.VisitType == "ED" && e.Code == "ETRUTHANS1"); 
                if(countForm > 0)
                {
                    isHasFallRiskScreening = true;
                }
                else
                {
                  int countEmer =   unitOfWork.EmergencyTriageRecordDataRepository
                       .Count(
                       e => !e.IsDeleted &&
                       e.EmergencyTriageRecordId != null &&
                       e.EmergencyTriageRecordId == ed.EmergencyTriageRecordId &&
                       new List<string> { "ETRDPH1", "ETRDPH2", "ETRDPHA3", "ETRDPN1", "ETRDPN2", "ETRDPN3" }.Contains(e.Code) &&
                       e.Value == "1");
                    if(countEmer > 0)
                    {
                        isHasFallRiskScreening = true;
                    }
                }
               
            }
            ed.IsHasFallRiskScreening = isHasFallRiskScreening;
            UpdateVisit(ed);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void HandleUpdateOrCreateEDFallRickScreenningData(EDFallRickScreennings obj, JToken request_etr_data)
        {
            var obj_datas = (from d in unitOfWork.FormDatasRepository.AsQueryable()
                             where !d.IsDeleted && d.FormId == obj.Id &&
                             d.FormCode == formCode
                             select d).ToList();
            foreach (var item in request_etr_data)
            {
                var code = item.Value<string>("Code");
                if (code == null) continue;

                var value = item.Value<string>("Value");

                if (code != null)
                    CreateOrUpdateDatas(obj_datas, code, value, obj);
            }
        }

        private void CreateOrUpdateDatas(List<FormDatas> datas, string code, string value, EDFallRickScreennings obj)
        {
            var code_data = datas.FirstOrDefault(e => e.Code == code && e.FormId == obj.Id && e.FormCode == formCode);
            if (code_data == null)
            {
                FormDatas new_data = new FormDatas()
                {
                    Code = code,
                    Value = value,
                    FormCode = formCode,
                    FormId = obj.Id,
                    VisitId = obj.VisitId,
                    VisitType = "ED"
                };
                unitOfWork.FormDatasRepository.Add(new_data);
            }
            else
            {
                code_data.Value = value;
                unitOfWork.FormDatasRepository.Update(code_data);
            }
        }
        protected void UpdateForm(EDFallRickScreennings form, JObject req)
        {
            DateTime transession;
            bool success = DateTime.TryParseExact(req["TransessionDate"]?.ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out transession);
            if (!success)
                transession = DateTime.Now;

            form.TransessionDate = transession;
            form.ClinicName = req["Clinic"]?.ToString();

            unitOfWork.EDFallRickScreenningRepository.Update(form);
        }

        protected EDFallRickScreennings GetEDFallRickScreenningById(Guid id)
        {
            return unitOfWork.EDFallRickScreenningRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
        }

        protected List<EDFallRickScreennings> GetAllFormsByVisit(Guid visitId)
        {
            return unitOfWork.EDFallRickScreenningRepository.AsQueryable()
                    .Where(e => !e.IsDeleted && e.VisitId == visitId).ToList();
        }

        protected EDFallRickScreennings CreateFormByVisit(ED visit)
        {
            bool isVisitLastUpdate = IsVisitLastTimeUpdate(visit, "UPDATE_VERSION2_A02_007_220321_VE");
            if (!isVisitLastUpdate)
                return null;

            var newForm = new EDFallRickScreennings()
            {
                VisitId = visit.Id,
                TransessionDate = DateTime.Now,
                ClinicName = null
            };
            unitOfWork.EDFallRickScreenningRepository.Add(newForm);
            return newForm;
        }
    }
}
