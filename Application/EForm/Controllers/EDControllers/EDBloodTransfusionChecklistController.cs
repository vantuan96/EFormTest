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
    public class EDBloodTransfusionChecklistController : EIOBloodTransfusionChecklistController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodTransfusionChecklist/Create/{id}")]
        [Permission(Code = "EBLTC1")]
        public IHttpActionResult CreateEDBloodTransfusionChecklistAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            var btc = CreateEIOBloodTransfusionChecklist(ed.Id, "ED", ed.SpecialtyId);
            var detail = GetEIOBloodTransfusionChecklistDetail(btc, ed.EmergencyTriageRecordId, ed.DischargeInformationId);

            return Content(HttpStatusCode.OK, detail);
        }

        [HttpGet]
        [Route("api/ED/BloodTransfusionChecklist/{id}")]
        [Permission(Code = "EBLTC2")]
        public IHttpActionResult GetEDBloodTransfusionChecklistAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            bool check_isExistBrsac = true;
            if (brsac == null)
                check_isExistBrsac = false;

            var btc_id = unitOfWork.EIOBloodTransfusionChecklistRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == ed.Id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED"
            ).OrderBy(e=> e.CreatedAt).Select(e=> new { e.Id, e.CreatedAt, e.CreatedBy });

            return Content(HttpStatusCode.OK, new { Datas = btc_id, IsExistBrsac = check_isExistBrsac });
        }

        [HttpGet]
        [Route("api/ED/BloodTransfusionChecklist/Detail/{id}")]
        [Permission(Code = "EBLTC3")]
        public IHttpActionResult GetEDBloodTransfusionChecklistDetailAPI(Guid id)
        {
            var btc = unitOfWork.EIOBloodTransfusionChecklistRepository.GetById(id);
            if(btc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BTC_NOT_FOUND);    

            var ed = GetED((Guid)btc.VisitId);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var detail = GetEIOBloodTransfusionChecklistDetail(btc, ed.EmergencyTriageRecordId, ed.DischargeInformationId);

            return Content(HttpStatusCode.OK, detail);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodTransfusionChecklist/Detail/{id}")]
        [Permission(Code = "EBLTC4")]
        public IHttpActionResult UpdateEDBloodTransfusionChecklistDetailAPI(Guid id, [FromBody]JObject request)
        {
            var btc = unitOfWork.EIOBloodTransfusionChecklistRepository.GetById(id);
            if (btc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BTC_NOT_FOUND);

            var error = UpdateEIOBloodTransfusionChecklist(btc, request);
            if (error != null)
                return Content(HttpStatusCode.OK, Message.SUCCESS);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodTransfusionChecklist/Confirm/{id}")]
        [Permission(Code = "EBLTC5")]
        public IHttpActionResult ConfirmEDBloodTransfusionChecklistDetailAPI(Guid id, [FromBody]JObject request)
        {
            var btc = unitOfWork.EIOBloodTransfusionChecklistRepository.GetById(id);
            if (btc == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BTC_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            if (string.IsNullOrEmpty(kind))
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();
            var success = ConfirmEIOBloodTransfusionChecklist(btc, user.Id, positions, kind);
            if(success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
        }    
    }
}
