using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDProcedureSummaryController : EIOProcedureSummaryController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/ProcedureSummary/Create/{id}")]
        [Permission(Code = "EPRSU1")]
        public IHttpActionResult CreateProcedureSummaryAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            CreateProcedureSummary(id, "ED");
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [HttpGet]
        [Route("api/ED/ProcedureSummary/List/{id}")]
        [Permission(Code = "EPRSU2")]
        public IHttpActionResult GetListProcedureSummaryAPI(Guid id)
        {
            var visit = GetED(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var procedures = GetListProcedureSummary(id, "ED");
            if (procedures.Count > 0)
            {
                return Content(HttpStatusCode.OK, procedures.Select(e => new {
                    e.CreatedBy,
                    e.CreatedAt,
                    e.Id,
                    Version = e.Version,
                    IsLocked = false,
                    e.EIOProcedureSummaryDatas,
                    UpdateAt = e.UpdatedAt,
                    UpdateBy = e.UpdatedBy
                }).ToList());
            }
            else
            {
                return Content(HttpStatusCode.OK, new
                {
                    IsLocked = false,
                    Count = 0
                });
            }
        }
        [HttpGet]
        [Route("api/ED/ProcedureSummary/{id}")]
        [Permission(Code = "EPRSU3")]
        public IHttpActionResult GetDetailProcedureSummaryAPI(Guid id)
        {
            EIOProcedureSummary procedure = GetProcedureSummary(id);
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Message.EIO_PRSU_NOT_FOUND);

            var doctor = procedure.ProcedureDoctor;
            var head_of_dept = procedure.HeadOfDepartment;
            var director = procedure.Director;            
            var data = procedure.EIOProcedureSummaryDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Code, e.Value, e.EnValue });

            return Content(HttpStatusCode.OK, new
            {
                procedure.Id,
                ProcedureDoctor = new { doctor?.Username, doctor?.Fullname, doctor?.DisplayName, doctor?.Title },
                ProcedureTime = procedure.ProcedureTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfDepartment = new { head_of_dept?.Username, head_of_dept?.Fullname, head_of_dept?.DisplayName, head_of_dept?.Title },
                HeadOfDepartmentTime = procedure.HeadOfDepartmentTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.DisplayName, director?.Title },
                DirectorTime = procedure.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Datas = data,
                Version = procedure.Version
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/ProcedureSummary/{id}")]
        [Permission(Code = "EPRSU4")]
        public IHttpActionResult UpdateDetailProcedureSummaryAPI(Guid id, [FromBody]JObject request)
        {
            EIOProcedureSummary procedure = GetProcedureSummary(id);
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Message.EIO_PRSU_NOT_FOUND);            
            if (procedure.ProcedureDoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateProcedureSummary(procedure, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/ProcedureSummary/Confirm/{id}")]
        [Permission(Code = "EPRSU5")]
        public IHttpActionResult ConfirmDetailProcedureSummaryAPI(Guid id, [FromBody]JObject request)
        {
            EIOProcedureSummary procedure = GetProcedureSummary(id);
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Message.EIO_PRSU_NOT_FOUND);            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var success = ConfirmProcedureSummary(procedure, user, kind);
            if (success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
        }
    }
}
