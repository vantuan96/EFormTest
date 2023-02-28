using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDConsentForTransfusionOfBloodController : BaseApiController
    {
        [HttpPost]
        [Route("api/ConsentForTransfusionOfBlood/ED/create/A01_006_080721_V/{visitId}")]
        [Permission(Code = "EDCFTOB01")]
        public IHttpActionResult CreateAPI(Guid visitId)
        {
            var visit = GetVisit(visitId, "ED");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, "A01_006_080721_V");
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new EIOForm
            {
                VisitId = visitId,
                Version = 1,
                FormCode = "A01_006_080721_V",
                VisitTypeGroupCode = "ED"
            };
            unitOfWork.EIOFormRepository.Add(form_data);

            UpdateVisit(visit, "ED");

            return Content(HttpStatusCode.OK, new { form_data.Id });
        }

        [HttpGet]
        [Route("api/ConsentForTransfusionOfBlood/ED/A01_006_080721_V/{visitId}")]
        //[CSRFCheck]
        [Permission(Code = "EDCFTOB02")]
        public IHttpActionResult GetAPI(Guid visitId)
        {
            var visit = GetVisit(visitId, "ED");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, "A01_006_080721_V");
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, FormatOutput(form, visit));
        }


        [HttpPost]
        [Route("api/ConsentForTransfusionOfBlood/ED/update/A01_006_080721_V/{visitId}")]
        //[CSRFCheck]
        [Permission(Code = "EDCFTOB03")]
        public IHttpActionResult UpdateAPI(Guid visitId, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, "ED");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, "A01_006_080721_V");
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (form.ConfirmBy != null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, "A01_006_080721_V", request["Datas"]);

            form.Note = request["Note"]?.ToString();
            form.Comment = request["Comment"]?.ToString();
            unitOfWork.EIOFormRepository.Update(form);

            UpdateVisit(visit, "ED");

            return Content(HttpStatusCode.OK, new { form.Id });
        }

        private dynamic FormatOutput(EIOForm fprm, dynamic visit)
        {
            ED ed = (ED)visit;
            var emer = ed.EmergencyRecord;
            string diagnosis = "";
            string ICD10 = "";
            if (emer != null)
            {
                diagnosis = emer.EmergencyRecordDatas.FirstOrDefault((e => !string.IsNullOrEmpty(e.Code) && e.Code == "ER0ID0ANS"))?.Value;
                ICD10 = emer.EmergencyRecordDatas.FirstOrDefault((e => !string.IsNullOrEmpty(e.Code) && e.Code == "ER0ICD102"))?.Value;
            }
            var fullnameCreatedby = "";
            if (fprm.CreatedBy.Count() > 1)
            {
                var user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == fprm.CreatedBy);
                if (user != null)
                {
                    fullnameCreatedby = user.Fullname;
                }
            }
            return new
            {
                fprm.Id,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, "A01_006_080721_V"),
                fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = fprm.Id,
                fprm.Note,
                fprm.Comment,
                UpdatedAt = fprm.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConfirmInfos = GetFormConfirms(fprm.Id),
                ChanDoanBenhChinh = diagnosis,
                ICD10 = ICD10,
                Specialty = ed.Specialty,
                FullNameCreatedBy = fullnameCreatedby,
                CustomerFullName = ed.Customer.Fullname
            };

        }
    }
}
