using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDTakeCareOfPatientsWithCovid19RecommentController : BaseIPDApiController
    {
        private readonly string formCode = "IPDTCOPRC19";
        [HttpGet]
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Recomment/CheckFormLocked/{id}")]
        [Permission(Code = "IPD28092105")]
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
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Recomment/{id}")]
        [Permission(Code = "IPD28092105")]
        public IHttpActionResult GetAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var forms = unitOfWork.IPDTakeCareOfPatientsWithCovid19RecommentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == id
            ).OrderBy(e => e.CreatedAt).ToList();

            return Content(HttpStatusCode.OK, forms.Select(e => FormatOutput(visit, e)));
        }
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Recomment/{id}/{item_id}")]
        [Permission(Code = "IPD28092106")]
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
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Recomment/Create/{id}")]
        [Permission(Code = "IPD28092107")]
        public IHttpActionResult CreateAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuChamSocNVCovid))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var form_data = new IPDTakeCareOfPatientsWithCovid19Recomment
            {
                VisitId = id,
                HandOverBy = getUsername()
            };
            form_data.HandOverAt = HandleDatetimeField(request["HandOverAt"]?.ToString());
            unitOfWork.IPDTakeCareOfPatientsWithCovid19RecommentRepository.Add(form_data);
            HandleUpdateOrCreateFormDatas(id, form_data.Id, formCode, request["Datas"]);
            
            UpdateVisit(visit);
            return Content(HttpStatusCode.OK, new { form_data.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Recomment/Update/{id}/{item_id}")]
        [Permission(Code = "IPD28092108")]
        public IHttpActionResult UpdateAPI(Guid id, Guid item_id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuChamSocNVCovid))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var form = GetForm(item_id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            
            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, item_id, formCode, request["Datas"]);
            
            form.HandOverAt = HandleDatetimeField(request["HandOverAt"]?.ToString());
            
            unitOfWork.IPDTakeCareOfPatientsWithCovid19RecommentRepository.Update(form);
            
            UpdateVisit(form.Visit);
            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/TakeCareOfPatientsWithCovid19/Recomment/Confirm/{id}/{item_id}")]
        [Permission(Code = "IPD28092109")]
        public IHttpActionResult ConfirmAPI(Guid id, Guid item_id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.PhieuChamSocNVCovid))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var form = GetForm(item_id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (form.ReceivingBy != null)
                return Content(HttpStatusCode.NotFound, Message.INFO_INCORRECT);            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            form.ReceivingAt = DateTime.Now;
            form.ReceivingBy = user.Username;

            unitOfWork.IPDTakeCareOfPatientsWithCovid19RecommentRepository.Update(form);
            UpdateVisit(form.Visit);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        
        private IPDTakeCareOfPatientsWithCovid19Recomment GetForm(Guid id)
        {
            return unitOfWork.IPDTakeCareOfPatientsWithCovid19RecommentRepository.GetById(id);
        }
        private dynamic FormatOutput(IPD visit, IPDTakeCareOfPatientsWithCovid19Recomment fprm)
        {
            return new
            {
                fprm.Id,
                IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.PhieuChamSocNVCovid),
                Datas = GetFormData(visit.Id, fprm.Id, formCode),
                fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = visit.Id,
                HandOverAt = fprm.HandOverAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                fprm.HandOverBy,
                fprm.ReceivingBy,
                ReceivingAt = fprm.ReceivingAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
            };
        }
    }
}