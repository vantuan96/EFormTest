using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDScaleForAssessmentOfSuicideIntentController : BaseApiController
    {
        private const string type = "A01_221_210121_V";
        private readonly string visit_type = "IPD";
        [HttpPost]
        [Route("api/IPD/IPDScaleForAssessmentOfSuicideIntent/Create/{type}/{visitId}")]
        [Permission(Code = "IPDSFAOSIC")]
        public IHttpActionResult CreateScaleForAssessmentOfSuicideIntent(Guid visitId, string type)
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            bool IsLocked = IPDIsBlock(visit, type);
            if (IsLocked)
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            var form = GetForm(visitId);
            if (form != null)
                return Content(HttpStatusCode.BadRequest,
                    new
                    {
                        ViMessage = "Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát đã tồn tại",
                        EnMessage = "Rating scale and Degree of Suicide Intent existed"
                    });

            var form_data = new IPDScaleForAssessmentOfSuicideIntent()
            {
                VisitId = visitId
            };
            unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.Add(form_data);
            UpdateVisit(visit, visit_type);           
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }

        [HttpPost]
        [Route("api/IPD/IPDScaleForAssessmentOfSuicideIntent/Update/{type}/{visitId}")]
        [Permission(Code = "IPDSFAOSIU")]
        public IHttpActionResult UpdateScaleForAssessmentOfSuicideIntent(Guid visitId, [FromBody] JObject request, string type)
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);                      
            var form = GetForm(visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));
            bool IsLocked = IPDIsBlock(visit, type, form.Id);
            if (IsLocked)
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            var user = GetUser();
            if (user.Username != form.CreatedBy && !IsCheckConfirm(form.Id))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);            
            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, type, request["Datas"]);
            unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.Update(form);
            UpdateVisit(visit, visit_type);            
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }
        [HttpPost]
        [Route("api/IPD/IPDScaleForAssessmentOfSuicideIntent/Confirm/{type}/{visitId}")]
        [Permission(Code = "IPDSFAOSICF")]
        public IHttpActionResult ConfirmForm(Guid visitId, [FromBody] JObject request, string type)
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            if (user.Username != form.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            var successConfirm = ConfirmUser(form, user);
            if (successConfirm)
            {
                UpdateVisit(visit, visit_type);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        [HttpGet]
        [Route("api/IPD/IPDScaleForAssessmentOfSuicideIntent/{type}/{ipdId}")]
        [Permission(Code = "IPDSFAOSICV")]
        public IHttpActionResult GetDetail(string type, Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var user = GetUser();            
            var form = unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.Find(e => !e.IsDeleted && e.VisitId == ipdId).FirstOrDefault();
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND_WITH_LOCKED);            
            return Content(HttpStatusCode.OK, FormatOutput(type, visit, form));
        }
        [HttpGet]
        [Route("api/IPD/IPDScaleForAssessmentOfSuicideIntent/Info/{type}/{ipdId}")]
        [Permission(Code = "IPDSFAOSICV")]
        public IHttpActionResult GetInfo(Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);          
            var form = unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.Find(e => !e.IsDeleted && e.VisitId == ipdId).FirstOrDefault();
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND_WITH_LOCKED);      
            return Content(HttpStatusCode.OK, new { FormId = form.Id});
        }
        [HttpGet]
        [Route("api/IPD/IPDScaleForAssessmentOfSuicideIntent/InfoLock24h/{type}/{ipdId}")]
        [Permission(Code = "IPDSFAOSICV")]
        public IHttpActionResult GetInfoLock24h(Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);           
            bool IsLocked = IPDIsBlock(visit, type);
            return Content(HttpStatusCode.OK, new {Lock24h = IsLocked });
        }

        private IPDScaleForAssessmentOfSuicideIntent GetForm(Guid visit_id)
        {
            return unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.Find(e => !e.IsDeleted && e.VisitId == visit_id).FirstOrDefault();
        }
        private bool ConfirmUser(IPDScaleForAssessmentOfSuicideIntent ipdScaleForAssessmentOfSuicideIntent, User user)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
           
            if (ipdScaleForAssessmentOfSuicideIntent.DoctorConfirmId == null)
            {
                ipdScaleForAssessmentOfSuicideIntent.DoctorConfirmAt = DateTime.Now;
                ipdScaleForAssessmentOfSuicideIntent.DoctorConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.Update(ipdScaleForAssessmentOfSuicideIntent);
            unitOfWork.Commit();
            return true;
        }
        private dynamic FormatOutput(string type, IPD ipd, IPDScaleForAssessmentOfSuicideIntent fprm)
        {
            var datas = GetFormData((Guid)fprm.VisitId, fprm.Id, type);
            var doctor = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.DoctorConfirmId);
            return new
            {
                Id = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = datas,
                CreatedBy = fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                UpdatedAt = fprm.UpdatedAt,
                IsLocked = IPDIsBlock(ipd, type, fprm.Id),
                DoctorConfirm = new
                {
                    UserName = doctor?.Username,
                    FullName = doctor?.Fullname,
                },
                DoctorConfirmAt = fprm.DoctorConfirmAt,
            };
        }
    }
}
