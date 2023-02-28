using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class EIOFormController : BaseApiController
    {
        private readonly string formCode = "EXAMPLE";
        [HttpGet]
        [Route("api/form/{visttype}/{formcode}/{id}")]
        [CSRFCheck]
        [Permission(Code = "IPDPRE")]
        protected IHttpActionResult GetAPI(Guid id, string visttype, string formcode)
        {
            var visit = GetVisit(id, visttype);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, formCode);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, FormatOutput(form));
        }
        protected IHttpActionResult GetsAPI(Guid id, string vist_type, string form_Code)
        {
            var visit = GetVisit(id, vist_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var forms = GetForms(id, vist_type, form_Code);
            if (forms.Count == 0)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, new {
                visitId = id,
                Datas = forms
                .Select(form => FormatOutput(form)),
            });
        }
        protected IHttpActionResult CreateAPI(Guid id, string visit_type)
        {
            var visit = GetVisit(id, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, formCode);
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new EIOForm
            {
                VisitId = id
            };
            unitOfWork.EIOFormRepository.Add(form_data);

            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, new { form_data.Id });
        }
        protected IHttpActionResult UpdateAPI(Guid id, [FromBody] JObject request, string visit_type)
        {
            var visit = GetVisit(id, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(id, formCode);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (form.ConfirmBy != null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formCode, request["Datas"]);

            form.Note = request["Note"]?.ToString();
            form.Comment = request["Comment"]?.ToString();
            unitOfWork.EIOFormRepository.Update(form);

            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, new { form.Id });
        }
        protected IHttpActionResult ConfirmAPI(Guid id, [FromBody] JObject request, string visit_type)
        {
            var visit = GetVisit(id, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var form = GetForm(id, formCode);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (form.ConfirmBy != null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            //check phân quyền cho user xác nhận
            var PermissionCode = "IPDPRE"; //code ở trên            
            var ischeckpermission = ICheckPermission(username, PermissionCode);
            if(!ischeckpermission)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            Guid ? formid = form.Id;
            //ví dụ 2 chân ký có kind1 = TeamLeader, kind2 = FormCompleted
            var getconfirm = GetFormConfirms(form.Id);
            if(getconfirm.Count() >0)
            {
                foreach (var item in getconfirm)
                {
                    if (kind == "TeamLeader" && item.ConfirmType != "TeamLeader")
                    {
                        SaveConfirm(user.Username, kind, formid);
                    }
                    if (kind == "FormCompleted" && item.ConfirmType != "FormCompleted")
                    {
                        SaveConfirm(user.Username, kind, formid);
                    } else
                    {
                        return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
                    }
                }
            }
            else
            {
                SaveConfirm(user.Username, kind, formid);
            }


            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        
        
    }
}