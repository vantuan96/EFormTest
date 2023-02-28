using DataAccess.Models.EDModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDSelfHarmRiskScreeningToolController : BaseEDApiController
    {
        private readonly string formCode = "EDSHRST";

        [HttpGet]
        [Route("api/ED/SelfHarmRiskScreeningTool/All/{visitId}")]
        [Permission(Code = "ED05102101")]
        public IHttpActionResult GetAllForm(Guid visitId)
        {
            var visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var datas_froms = GetDatasFormsByVisit(visit.Id);
            if (datas_froms == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, new { Datas = datas_froms });

        }

        [HttpGet]
        [Route("api/ED/SelfHarmRiskScreeningTool/{id}/{formId}")]
        [Permission(Code = "ED05102101")]
        public IHttpActionResult GetAPI(Guid id, Guid formId)
        {
            var visit = GetED(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetFormById(formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, FormatOutput(visit, form));
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/SelfHarmRiskScreeningTool/Create/{id}")]
        [Permission(Code = "ED05102102")]
        public IHttpActionResult CreateAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetED(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);          

            var form_data = new EDSelfHarmRiskScreeningTool
            {
                VisitId = id,
                ScreeningTime = DateTime.Now
            };
            unitOfWork.EDSelfHarmRiskScreeningToolRepository.Add(form_data);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { form_data.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/SelfHarmRiskScreeningTool/Update/{id}/{formId}")]
        [Permission(Code = "ED05102103")]
        public IHttpActionResult UpdateAPI(Guid id,Guid formId, [FromBody] JObject request)
        {
            var visit = GetED(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ScreeningTime = HandleDatetimeField(request["ScreeningTime"]?.ToString());
            if (ScreeningTime == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var form = GetFormById(formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            if (form.ConfirmBy != null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formCode, request["Datas"]);
            form.Visit.SelfHarmRiskScreeningResults = GetSelfHarmRiskScreeningResults(visit);
            form.ScreeningTime = (DateTime)ScreeningTime;
            
            unitOfWork.EDSelfHarmRiskScreeningToolRepository.Update(form);
            
            UpdateVisit(form.Visit);
            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/SelfHarmRiskScreeningTool/Confirm/{id}/{formId}")]
        [Permission(Code = "ED05102104")]
        public IHttpActionResult ConfirmAPI(Guid id,Guid formId, [FromBody] JObject request)
        {
            var ipd = GetED(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var form = GetFormById(formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (form.ConfirmBy != null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var position = user.PositionUsers.Where(p => !p.IsDeleted).Select(p => p.Position.EnName);
            if (!position.Contains("Doctor"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            form.ConfirmAt = DateTime.Now;
            form.ConfirmBy = user.Username;

            unitOfWork.EDSelfHarmRiskScreeningToolRepository.Update(form);

            UpdateVisit(form.Visit);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/SelfHarmRiskScreeningTool/Sync/{id}")]
        [Permission(Code = "ED05102101")]
        public IHttpActionResult SyncAPI(Guid id)
        {
            var form = GetFormById(id);
            if (form == null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);

            var visit = GetED((Guid)form.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var most_recent_model = NearestSampling(visit, form);
            if (most_recent_model == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, FormatOutput(most_recent_model.Visit, most_recent_model));
        }
        private int? GetSelfHarmRiskScreeningResults(ED visit)
        {
            var forms = unitOfWork.EDSelfHarmRiskScreeningToolRepository.AsQueryable()
                        .Where(f => !f.IsDeleted && f.VisitId == visit.Id)
                        .OrderByDescending(f => f.CreatedAt).ToList();

            if (forms == null || forms.Count == 0)
                return null;

            var formId = forms[0].Id;
            string[] codes = new string[] { "EDSHRST00SR02", "EDSHRST00SR03" };
            var codeDatas = unitOfWork.FormDatasRepository.AsQueryable()
                            .Where(
                                d => !d.IsDeleted &&
                                !string.IsNullOrEmpty(d.Code) &&
                                d.FormId == formId &&
                                codes.Contains(d.Code)
                            ).ToList();

            if (codeDatas == null || codeDatas.Count == 0)
                return null;

            foreach (var item in codeDatas)
            {
                var code = item.Code;
                if (string.IsNullOrEmpty(code)) continue;
                var value = item.Value;
                if (code == "EDSHRST00SR03" && value == "True") return 1;
                if (code == "EDSHRST00SR02" && value == "True") return 2;
            }
            return 0;
        }

        private dynamic GetDatasFormsByVisit(Guid visitId)
        {
            var forms = unitOfWork.EDSelfHarmRiskScreeningToolRepository.AsQueryable()
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
        private EDSelfHarmRiskScreeningTool GetForm(Guid id)
        {
            return unitOfWork.EDSelfHarmRiskScreeningToolRepository.Find(e => !e.IsDeleted && e.VisitId == id).FirstOrDefault();
        }
        private EDSelfHarmRiskScreeningTool GetFormById(Guid id)
        {
            return unitOfWork.EDSelfHarmRiskScreeningToolRepository.GetById(id);
        }
        private EDSelfHarmRiskScreeningTool NearestSampling(ED visit, EDSelfHarmRiskScreeningTool form)
        {
            var forms = unitOfWork.EDSelfHarmRiskScreeningToolRepository.AsQueryable()
                        .Where(
                            f => !f.IsDeleted &&
                            f.VisitId == form.VisitId
                        ).ToList();

            var finalForm = forms[forms.Count - 1];
            if(form.Id == finalForm.Id)
            {
                if(CheckIsNew(form.Id))
                    return unitOfWork.EDSelfHarmRiskScreeningToolRepository.Find(e => !e.IsDeleted && e.Visit.CustomerId == visit.CustomerId && e.Id != form.Id && form.CreatedAt > e.CreatedAt).OrderByDescending(e => e.CreatedAt).FirstOrDefault();

                return form;
            }

            return unitOfWork.EDSelfHarmRiskScreeningToolRepository.Find(e => !e.IsDeleted && e.Visit.CustomerId == visit.CustomerId && e.Id != form.Id).OrderByDescending(e => e.CreatedAt).FirstOrDefault();
        }
        private dynamic FormatOutput(ED visit, EDSelfHarmRiskScreeningTool fprm)
        {
            return new
            {
                fprm.Id,
                Datas = GetFormData(visit.Id, fprm.Id, formCode),
                fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = visit.Id,
                ScreeningTime = fprm.ScreeningTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConfirmAt = fprm.ConfirmAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                fprm.ConfirmBy,
                UpdatedAt = fprm.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
            };
        }

        private bool CheckIsNew(Guid id)
        {
            var form = unitOfWork.EDSelfHarmRiskScreeningToolRepository
                       .FirstOrDefault(
                            p => !p.IsDeleted &&
                            p.Id == id 
                        );
            if (form != null)
            {
                if (form.UpdatedAt == null)
                    return IsNew(form.CreatedAt, form.CreatedAt);

                string str_createAt = form.CreatedAt?.ToString("DD/MM/YY HH:mm:ss");
                DateTime createAt = DateTime.ParseExact(str_createAt, "DD/MM/YY HH:mm:ss", CultureInfo.InvariantCulture);
                string str_updateAt = form.UpdatedAt?.ToString("DD/MM/YY HH:mm:ss");
                DateTime updateAt = DateTime.ParseExact(str_updateAt, "DD/MM/YY HH:mm:ss", CultureInfo.InvariantCulture);
                return IsNew(createAt, updateAt);
            }
            return true;
        }
    }
}