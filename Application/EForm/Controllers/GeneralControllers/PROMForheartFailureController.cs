using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
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

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class PROMForheartFailureController : BaseApiController
    {
        private readonly string formCode = "PROMFHF";
        protected IHttpActionResult GetFormByVisitId(Guid visitId, string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var form = unitOfWork.PROMForheartFailureRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && visitType == e.VisitType);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            var userconfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && form.UserConfirmId != null && u.Id == form.UserConfirmId);            

            var data = GetFormData(visitId, form.Id, formCode);
            var IsLocked = false;
            if (visitType == "IPD")
            {
                IsLocked = IPDIsBlock(visit, "PROMFHF", form.Id);
            }
            if (visitType == "OPD")
            {
                var user = GetUser();
                IsLocked = Is24hLocked(visit.CreatedAt, visitId, "PROMFHF", user.Username, form.Id);
            }
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                Datas = data,
                form.CreatedBy,
                form.CreatedAt,
                form.VisitType,
                form.UpdatedBy,
                form.UpdatedAt,
                IsLocked,
                UserConfirm = new
                {
                    UserName = userconfirm?.Username,
                    FullName = userconfirm?.Fullname,
                }, 
                form.UserConfirmAt,
                IsCheckUnlockConfirm = IsCheckConfirm(form.Id)
        });

        }
        protected IHttpActionResult CreateForm(Guid visitId, string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)            
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);           
            
            var form = GetForm(visitId);
            if (form != null)
                return Content(HttpStatusCode.BadRequest,
                    new
                    {
                        ViMessage = "PROM bệnh nhân suy tim đã tồn tại",
                        EnMessage = "PROM for heart failure existed"
                    });
            var formData = new PROMForheartFailure
            {
                VisitId = visitId,
                VisitType = visitType,              
            };

            unitOfWork.PROMForheartFailureRepository.Add(formData);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(visitId, formData.Id, "PROMFHF");
            return Content(HttpStatusCode.OK, new { formData.Id, formData.CreatedAt, formData.CreatedBy, formData.VisitId });
        }
        protected IHttpActionResult UpdateForm(Guid visitId, String visitType, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)            
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);            
            var form = unitOfWork.PROMForheartFailureRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.VisitType == visitType);
            if (form == null)            
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);            
            var user = GetUser();
            if (user.Username != form.CreatedBy && !IsCheckConfirm(form.Id))
            {
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);
            }

            if (request != null && request["Datas"] != null)
            {
                HandleUpdateOrCreateFormDatas(visitId, form.Id, formCode, request["Datas"]);
                unitOfWork.PROMForheartFailureRepository.Update(form);
                unitOfWork.Commit();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);
            }

            CreateOrUpdateFormForSetupOfAdmin(visitId, form.Id, "PROMFHF");
            return Content(HttpStatusCode.OK, new { form.Id });
        }
        public IHttpActionResult ConfirmAPI(Guid visitId, String visitType, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var form = unitOfWork.PROMForheartFailureRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.VisitType == visitType);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, formCode));
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            if (user.Username != form.CreatedBy && !IsCheckConfirm(form.Id))
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    Message = "Bạn không có quyền xác nhận"
                });
            }
            var successConfirm = ConfirmUser(form, user);
            if (successConfirm)
            {
                UpdateVisit(visit, visitType);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        private bool ConfirmUser(PROMForheartFailure form, User user)
        {
            if (form.UserConfirmId == null)
            {
                form.UserConfirmAt = DateTime.Now;
                form.UserConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.PROMForheartFailureRepository.Update(form);
            unitOfWork.Commit();
            return true;
        }        
        private PROMForheartFailure GetForm(Guid visit_id)
        {
            return unitOfWork.PROMForheartFailureRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id).FirstOrDefault();
        }
    }
}
