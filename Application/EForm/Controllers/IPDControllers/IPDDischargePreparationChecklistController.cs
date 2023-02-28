using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDDischargePreparationChecklistController : BaseApiController
    {
        private readonly string formCode = "A03_046_050919_VE";
        [HttpGet]
        [Route("api/IPD/IPDDischargePreparationChecklist/Doctor/{id}")]
        [Permission(Code = "IMRDPC01")]
        public IHttpActionResult GetAPI(Guid id)
        {
            var visit = GetVisit(id, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, "Doctor");
            if (form == null) {
                bool IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiRaVien);
                return Content(HttpStatusCode.NotFound, new { ViMessage = "Form không tồn tại", EnMessage = "Form is not found", IsLocked });
            }
            return Content(HttpStatusCode.OK, FormatOutput(form, visit));
        }
        [HttpPost]
        [Route("api/IPD/IPDDischargePreparationChecklist/Doctor/Create/{id}")]
        [Permission(Code = "IMRDPC01")]
        public IHttpActionResult CreateAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(id, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, "Doctor");
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new IPDDischargePreparationChecklist
            {
                VisitId = id,
                Type = "Doctor"
            };
            if (!string.IsNullOrEmpty(request["Time"].ToString()))
                form_data.Time = DateTime.ParseExact(request["Time"].ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            unitOfWork.IPDDischargePreparationChecklistRepository.Add(form_data);

            UpdateVisit(visit, "IPD");

            return Content(HttpStatusCode.OK, new { form_data.Id });
        }

        [HttpPost]
        [Route("api/IPD/IPDDischargePreparationChecklist/Doctor/Update/{id}")]
        [Permission(Code = "IMRDPC01")]
        public IHttpActionResult UpdateAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(id, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, "Doctor");
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formCode, request["Datas"]);
            if (!string.IsNullOrEmpty(request["Time"].ToString()))
                form.Time = DateTime.ParseExact(request["Time"].ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            unitOfWork.IPDDischargePreparationChecklistRepository.Update(form);

            UpdateVisit(visit, "IPD");

            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [HttpGet]
        [Route("api/IPD/IPDDischargePreparationChecklist/Nurse/{id}")]
        [Permission(Code = "IMRDPC01")]
        public IHttpActionResult GetAPIForNurse(Guid id)
        {
            var visit = GetVisit(id, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, "Nurse");
            if (form == null)
            {
                bool IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangKiemChuanBiRaVien);
                return Content(HttpStatusCode.NotFound, new { ViMessage = "Form không tồn tại", EnMessage = "Form is not found", IsLocked });
            }

            return Content(HttpStatusCode.OK, FormatOutput(form, visit));
        }
        [HttpPost]
        [Route("api/IPD/IPDDischargePreparationChecklist/Nurse/Create/{id}")]
        [Permission(Code = "IMRDPC01")]
        public IHttpActionResult CreateAPIForNurse(Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(id, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, "Nurse");
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new IPDDischargePreparationChecklist
            {
                VisitId = id,
                Type = "Nurse"
            };
            if (!string.IsNullOrEmpty(request["Time"].ToString()))
                form_data.Time = DateTime.ParseExact(request["Time"].ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            unitOfWork.IPDDischargePreparationChecklistRepository.Add(form_data);

            UpdateVisit(visit, "IPD");

            return Content(HttpStatusCode.OK, new { form_data.Id });
        }

        [HttpPost]
        [Route("api/IPD/IPDDischargePreparationChecklist/Nurse/Update/{id}")]
        [Permission(Code = "IMRDPC01")]
        public IHttpActionResult UpdateAPIForNurse(Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(id, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, "Nurse");
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formCode, request["Datas"]);

            if (!string.IsNullOrEmpty(request["Time"].ToString()))
                form.Time = DateTime.ParseExact(request["Time"].ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

            unitOfWork.IPDDischargePreparationChecklistRepository.Update(form);

            UpdateVisit(visit, "IPD");

            return Content(HttpStatusCode.OK, new { form.Id });
        }
        private IPDDischargePreparationChecklist GetForm(Guid visit_id, string type)
        {
            return unitOfWork.IPDDischargePreparationChecklistRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.Type == type).FirstOrDefault();
        }
        private dynamic FormatOutput(IPDDischargePreparationChecklist fprm, IPD ipd)
        {
            var form = unitOfWork.IPDDischargePreparationChecklistRepository.Find(e => !e.IsDeleted && e.VisitId == ipd.Id).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            return new
            {
                fprm.Id,
                Time = fprm.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCode),
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemChuanBiRaVien),
                fprm.CreatedBy,
                fprm.UpdatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = fprm.Id,
                UpdatedAt = fprm.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitStatus = new
                {
                    ipd.EDStatus.ViName,
                    ipd.EDStatus.Code,
                    ipd.EDStatus.EnName,
                    ipd.EDStatus.Id
                },
                LastUpdate = new { form.UpdatedBy, form.UpdatedAt}
            };
        }
    }

}