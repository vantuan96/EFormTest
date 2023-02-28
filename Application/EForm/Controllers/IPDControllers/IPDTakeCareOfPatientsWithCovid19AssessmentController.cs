using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDTakeCareOfPatientsWithCovid19AssessmentController : BaseIPDApiController
    {
        private readonly string formCode = "IPDTCOPWC19";
        [HttpGet]
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Assessment/CheckFormLocked/{id}")]
        [Permission(Code = "IPD28092101")]
        public IHttpActionResult CheckFormLockedAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.ChamSocBNCovid)
            });
        }
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Assessment/{id}")]
        [Permission(Code = "IPD28092101")]
        public IHttpActionResult GetAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var forms = unitOfWork.IPDTakeCareOfPatientsWithCovid19AssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == id
            ).OrderBy(e => e.AssessmentAt).ToList();
            
            var LastConfirm = unitOfWork.IPDTakeCareOfPatientsWithCovid19RecommentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit.Id
            ).OrderByDescending(e => e.HandOverAt).FirstOrDefault();
            
            return Content(HttpStatusCode.OK, new { LastConfirmDateTime = LastConfirm != null ? LastConfirm.HandOverAt : null, Results = forms.Select(e => FormatOutput(visit, e)) });
        }
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Assessment/{id}/{item_id}")]
        [Permission(Code = "IPD28092102")]
        public IHttpActionResult GetDetailAPI(Guid id, Guid item_id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var form = GetForm(item_id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, FormatOutput(visit, form));
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Assessment/Create/{id}")]
        [Permission(Code = "IPD28092103")]
        public IHttpActionResult CreateAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            
            var form_data = new IPDTakeCareOfPatientsWithCovid19Assessment
            {
                VisitId = id
            };
            form_data.AssessmentAt = HandleDatetimeField(request["AssessmentAt"]?.ToString());
            unitOfWork.IPDTakeCareOfPatientsWithCovid19AssessmentRepository.Add(form_data);
            HandleUpdateOrCreateFormDatas(id, form_data.Id, formCode, request["Datas"]);
            
            UpdateVisit(visit);
            return Content(HttpStatusCode.OK, new { form_data.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Assessment/Update/{id}/{item_id}")]
        [Permission(Code = "IPD28092104")]
        public IHttpActionResult UpdateAPI(Guid id, Guid item_id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(item_id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            
            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, item_id, formCode, request["Datas"]);
            form.AssessmentAt = HandleDatetimeField(request["AssessmentAt"]?.ToString());
            unitOfWork.IPDTakeCareOfPatientsWithCovid19AssessmentRepository.Update(form);
            
            UpdateVisit(form.Visit);
            return Content(HttpStatusCode.OK, new { form.Id });
        }
        private IPDTakeCareOfPatientsWithCovid19Assessment GetForm(Guid id)
        {
            return unitOfWork.IPDTakeCareOfPatientsWithCovid19AssessmentRepository.GetById(id);
        }
        private dynamic FormatOutput(IPD visit, IPDTakeCareOfPatientsWithCovid19Assessment fprm)
        {
          
            return new
            {
                fprm.Id,
                Datas = GetFormData(visit.Id, fprm.Id, formCode),
                fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = visit.Id,
                AssessmentAt = fprm.AssessmentAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
            };
        }
    }
}