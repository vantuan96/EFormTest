using DataAccess.Models;
using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class PROMForCoronaryDiseaseController : BaseApiController
    {
        private readonly string formCode = "PROMFCD";
        public IHttpActionResult GetInfoForm(Guid visitId, string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            }
            var IsLocked = CheckIsBlock(visit, visitType);
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IsLocked
            });
        }
        protected IHttpActionResult GetFormByVisitId(Guid visitId,string visitType)
        {
            var form = unitOfWork.PROMForCoronaryDiseaseRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && visitType == e.VisitType);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            var ProcedureConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && form.ProcedureConfirmId != null && u.Id == form.ProcedureConfirmId);
            var data = GetFormData(visitId, form.Id, formCode);
            var visit = GetVisit(visitId, visitType);
            var IsLocked = CheckIsBlock(visit, visitType, form.Id);
            var IsUnlockedConfirm = IsCheckConfirm(form.Id);

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
                IsUnlockedConfirm,
                UserConfirm = new
                {
                    UserName = ProcedureConfirm?.Username,
                    FullName = ProcedureConfirm?.Fullname,
                    ConfirmAt = form.ProcedureConfirmTime
                },
                Version = form.Version,
            });

        }
        protected IHttpActionResult CreateForm(Guid visitId,string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            }
            var form = unitOfWork.PROMForCoronaryDiseaseRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.VisitType == visitType);
            if(form != null)
            {
                return Content(HttpStatusCode.BadRequest,
                    new
                    {
                        ViMessage = "PROM bệnh nhân mạch vành đã tồn tại",
                        EnMessage = "PROM for coronary disease existed"
                    });
            }    
            if (CheckIsBlock(visit, visitType))
            {
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            }
            var formData = new PROMForCoronaryDisease
            {
                VisitId = visitId,
                VisitType = visitType,
                Version = "1"
            };

            unitOfWork.PROMForCoronaryDiseaseRepository.Add(formData);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(visitId, formData.Id, formCode);
            return Content(HttpStatusCode.OK, new { formData.Id });
        }
        protected IHttpActionResult UpdateForm(Guid visitId, String visitType, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            }
            var form = unitOfWork.PROMForCoronaryDiseaseRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.VisitType == visitType);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            if (CheckIsBlock(visit, visitType, form.Id))
            {
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            }
            var user = GetUser();
            if (user.Username != form.CreatedBy && !IsCheckConfirm(form.Id))
            {
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);
            }    
            if(request != null && request["Datas"] != null)
            {
                HandleUpdateOrCreateFormDatas(visitId, form.Id, formCode, request["Datas"]);                             
                unitOfWork.PROMForCoronaryDiseaseRepository.Update(form);
                unitOfWork.Commit();
            }    
            else
            {
                return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);
            }

            CreateOrUpdateFormForSetupOfAdmin(visitId, form.Id, formCode);
            return Content(HttpStatusCode.OK, new { form.Id });
        }
        public IHttpActionResult ConfirmAPI(Guid visitId, String visitType,[FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var form = unitOfWork.PROMForCoronaryDiseaseRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.VisitType == visitType);
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
        private bool ConfirmUser(PROMForCoronaryDisease form, User user)
        {
            //var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (form.ProcedureConfirmId == null)
            {
                form.ProcedureConfirmTime = DateTime.Now;
                form.ProcedureConfirmId = user?.Id;
            }
            else
            {
                return false;
            }    
            unitOfWork.PROMForCoronaryDiseaseRepository.Update(form);
            unitOfWork.Commit();
            return true;
        }
        private bool CheckIsBlock(dynamic visit, string visitType, Guid? formId = null)
        {
            if (visitType == "IPD")
            {
                if (IPDIsBlock(visit, formCode, formId))
                    return true;
            }
            else if (visitType == "OPD")
            {
                var user = GetUser();
                var is_block_after_24h = IsBlockAfter24h(visit.CreatedAt, formId);
                var has_unlock_permission = HasUnlockPermission(visit.Id, formCode, user.Username, formId);
                if (!has_unlock_permission && is_block_after_24h)
                    return true;
            }
            return false;
        }
    }
}