using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDSurgeryAndProcedureSummaryV3Controller : SurgeryAndProcedureSummaryV3Controller
    {
        private readonly string vistiType = "OPD";
        [HttpGet]
        [Route("api/OPD/SurgeryAndProcedureSummaryV3/GetListItemsByVisitId/{visitId}")]
        [Permission(Code = "OPDSAPSV3GL")]
        public IHttpActionResult OPDGetListItemsByVisitId(Guid visitId)
        {
            return GetListItemsByVisitId(visitId, vistiType);
        }

        [HttpGet]
        [Route("api/OPD/SurgeryAndProcedureSummaryV3/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "OPDSAPSV3GD")]
        public IHttpActionResult OPDGetDetail(Guid visitId, Guid formId)
        {
            return GetDetail(visitId, formId, vistiType);
        }
        [HttpPost]
        [Route("api/OPD/SurgeryAndProcedureSummaryV3/Create/{visitId}")]
        [Permission(Code = "OPDSAPSV3C")]
        public IHttpActionResult OPDCreateProcedureSummaryV2(Guid visitId)
        {
            return CreateSurgeryAndProcedureSummaryV3(visitId, vistiType);
        }

        [HttpPost]
        [Route("api/OPD/SurgeryAndProcedureSummaryV3/Update/{visitId}/{formId}")]
        [Permission(Code = "OPDSAPSV3U")]
        public IHttpActionResult OPDUpdate(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return Update(visitId,formId, vistiType, request);
        }

        [HttpPost]
        [Route("api/OPD/SurgeryAndProcedureSummaryV3/Confirm/{visitId}/{formId}")]
        [Permission(Code = "OPDSAPSV3CF")]
        public IHttpActionResult OPDConfirmAPI(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            return ConfirmAPI(visitId, formId, vistiType, request);
        }
        [HttpGet]
        [Route("api/OPD/SurgeryAndProcedureSummaryV3/Infor/{visitId}")]
        public IHttpActionResult OPDGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "OPD");
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);
            var user = GetUser();
            bool IsLocked = Is24hLocked(visit.CreatedAt, visit.Id, "A01_085_120522_VE", user.Username);
            return Content(HttpStatusCode.OK, IsLocked);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/SurgeryAndProcedureSummaryV3/Sync/{id}")]
        [Permission(Code = "OPRSU6")]
        public IHttpActionResult SyncDiagnosisAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Common.Message.OPD_NOT_FOUND);

            SurgeryAndProcedureSummaryV3 procedure = GetProcedureSummary(id, "OPD");
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.EIO_PRSU_NOT_FOUND);

            var oen = opd.OPDOutpatientExaminationNote;
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            var definitive_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value;
            return Content(HttpStatusCode.OK, definitive_diagnosis);
        }
        protected SurgeryAndProcedureSummaryV3 GetProcedureSummary(Guid visit_id, string visit_type)
        {
            return unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitType == visit_type
            );
        }
    }
}