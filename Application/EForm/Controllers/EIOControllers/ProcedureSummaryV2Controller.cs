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

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class ProcedureSummaryV2Controller : BaseApiController
    {
        private readonly string formCode = "A01_084_050919_V";
        protected IHttpActionResult GetListItemsByVisitId(Guid visitId, string visitType)
        {
            var data = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(e => e.VisitId == visitId && e.VisitType == visitType);
            var visit = GetVisit(visitId, visitType);
            if (data == null || visit == null)
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    Message = Message.FORM_NOT_FOUND,
                    IsLocked = CheckIsBlock(visit, visitType)
                });
            }

            var listForms = unitOfWork.ProcedureSummaryV2Repository
                .Find(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == visitId && 
                    e.VisitType == visitType 
                ).OrderBy(x => x.CreatedAt)
                .Select(e => new
                {
                    Id = e.Id,
                    VisitId = e.VisitId,
                    CreateAt = e.CreatedAt,
                    CreateBy = e.CreatedBy,
                    UpdatedAt = e.UpdatedAt,
                    VisitType = e.VisitType,
                    TransactionDate = e.ProcedureTime,
                    Version = e.Version
                }).ToList();
            var IsLocked = CheckIsBlock(visit, visitType);            
            return Content(HttpStatusCode.OK, new
            {
                ListItems = listForms,
                Count = listForms.Count,
                IsLocked

            });
        }
        protected IHttpActionResult GetDetail(Guid visitId,Guid formId,string visitType)
        {
            var form = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            var ProcedureDoctorConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && form.ProcedureDoctorId != null && u.Id == form.ProcedureDoctorId);
            var data = GetFormData(visitId, formId, formCode);
            var visit = GetVisit(visitId, visitType);
            var IsLocked = CheckIsBlock(visit, visitType, formId);
            return Content(HttpStatusCode.OK, new
            {
                formId,
                Datas = data,
                form.CreatedBy,
                form.ProcedureTime,
                form.VisitType,
                form.UpdatedBy,
                form.UpdatedAt,
                IsLocked,
                DoctorConfirm = new
                {
                    UserName = ProcedureDoctorConfirm?.Username,
                    FullName = ProcedureDoctorConfirm?.Fullname,
                },
                Version = form.Version,
            });

        }
        protected IHttpActionResult CreateProcedureSummaryV2(Guid visitId,string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            }
            if (CheckIsBlock(visit, visitType))
            {
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            }
            var formData = new ProcedureSummaryV2
            {
                VisitId = visitId,
                VisitType = visitType,
                Version = "2"
            };

            unitOfWork.ProcedureSummaryV2Repository.Add(formData);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { formData.Id });
        }
        protected IHttpActionResult Update(Guid visitId, Guid formId, String visitType, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            }
            if(CheckIsBlock(visit, visitType, formId))
            {
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
            }
            var form = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }

            //var listForms = unitOfWork.ProcedureSummaryV2Repository
            //    .Find(
            //        e => !e.IsDeleted &&
            //        e.VisitId != null &&
            //        e.VisitId == visitId
            //    ).OrderByDescending(e => e.ProcedureTime).Select(e => new
            //    {
            //        TransactionDate = e.ProcedureTime
            //    }).ToList();
            //int second = 0;
            //if (listForms.Count == 1)
            //{
            //    second = listForms[0].TransactionDate.Value.Second;
            //}
            //else
            //{
            //    second = listForms[1].TransactionDate.Value.Second;
            //}
            //if (request["TransactionDate"]?.ToString() != "" && request["TransactionDate"]?.ToString() != null)
            //{
            //    form.ProcedureTime = DateTime.ParseExact(request["Value"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture).Add(TimeSpan.FromSeconds(second + 1));
            //}           
            if(request != null && request["Datas"] != null)
            {
                HandleUpdateOrCreateFormDatas(visitId, formId, formCode, request["Datas"]);                             
                unitOfWork.ProcedureSummaryV2Repository.Update(form);
                unitOfWork.Commit();
            }    
            else
            {
                return Content(HttpStatusCode.BadRequest, Message.INTERAL_SERVER_ERROR);
            }    

            return Content(HttpStatusCode.OK, new { form.Id });
        }
        public IHttpActionResult ConfirmAPI(Guid visitId, Guid formId, String visitType,[FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var form = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, formCode));            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
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
        private bool ConfirmUser(ProcedureSummaryV2 form, User user)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (positions.Contains("DOCTOR") && form.ProcedureDoctorId == null)
            {
                form.ProcedureTime = DateTime.Now;
                form.ProcedureDoctorId = user?.Id;
            }
            else
            {
                return false;
            }    
            unitOfWork.ProcedureSummaryV2Repository.Update(form);
            unitOfWork.Commit();
            return true;
        }
        private bool CheckIsBlock(dynamic visit, string visitType, Guid? formId = null)
        {
            if (visitType == "IPD")
            {
                if (IPDIsBlock(visit, Constant.IPDFormCode.TomTatThuThuatV2, formId))
                    return true;
            }
            else if (visitType == "OPD")
            {
                var user = GetUser();
                var is_block_after_24h = IsBlockAfter24h(visit.CreatedAt, formId);
                var has_unlock_permission = HasUnlockPermission(visit.Id, "OPDPSV2", user.Username, formId);
                if (!has_unlock_permission && is_block_after_24h)
                    return true;
            }
            return false;
        }
    }
}