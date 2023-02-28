using DataAccess.Models.EDModel;
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
    public class EDBloodRequestSupplyAndConfirmationController : EIOBloodRequestSupplyAndConfirmationController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/Create/{id}")]
        [Permission(Code = "EBRSC1")]
        public IHttpActionResult CreateEDBloodRequestSupplyAndConfirmationAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_EXIST);

            brsac = CreateBloodRequestSupplyAndConfirmation(ed.Id, "ED", ed.SpecialtyId, ed.Version);

            return Content(HttpStatusCode.OK, new { brsac.Id });
        }

        [HttpGet]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/{id}")]
        [Permission(Code = "EBRSC2")]
        public IHttpActionResult GetEDBloodRequestSupplyAndConfirmationAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetBloodRequestSupplyAndConfirmationDatas(brsac, ed, ed.DischargeInformationId, "ED"));
        }

        [HttpGet]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/PurchaseRequest/{id}")]
        [Permission(Code = "EBRSC3")]
        public IHttpActionResult GetEDBloodRequestAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetBloodRequest(brsac, ed.DischargeInformationId, "ED"));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/PurchaseRequest/{id}")]
        [Permission(Code = "EBRSC4")]
        public IHttpActionResult UpdateEDBloodRequestAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            if (brsac.DoctorConfirmId != null && brsac.HeadOfDeptId != null)
                return Content(HttpStatusCode.BadRequest, Message.DOCTOR_ACCEPTED);

            HandleCreateOrUpdateBloodRequestData(brsac, request, ed.Customer);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/PurchaseRequest/Confirm/{id}")]
        [Permission(Code = "EBRSC5")]
        public IHttpActionResult ConfirmEDBloodRequestAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var action = GetActionOfUser(user, "EBRSC5");
            if (action == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            ConfirmBloodRequest(brsac, user.Id);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [HttpPost]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/PurchaseRequest/DoctorConfirm/{visitId}")]
        [Permission(Code = "EDBSXNPDTM")]
        public IHttpActionResult DoctorConfirmIPDBloodRequesAPI(Guid visitId, [FromBody] JObject request)
        {
            ED visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "ED");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var acction = GetActionOfUser(user, "EDBSXNPDTM");
            if (acction == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            ConfirmBloodRequestDatas(brsac);
            brsac.DoctorConfirmId = user.Id;
            brsac.DoctorConfirmTime = DateTime.Now;
            unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/Supply/{id}")]
        [Permission(Code = "EBRSC6")]
        public IHttpActionResult GetEDBloodSupplyAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetBloodSupply(brsac));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/Supply/{id}")]
        [Permission(Code = "EBRSC7")]
        public IHttpActionResult UpdateEDBloodSupplyAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            HandleCreateOrUpdateBloodSupplyData(brsac, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/Supply/Confirm/{id}")]
        [Permission(Code = "EBRSC8")]
        public IHttpActionResult ConfirmEDBloodSupplyAPI(Guid id, [FromBody]JObject request)
        {
            var supply = unitOfWork.EIOBloodSupplyDataRepository.GetById(id);
            if (supply == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains("Nurse"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var is_success = ConfirmBloodSupply(supply, kind, user.Username);
            if(is_success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
        }

        [HttpGet]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/TransfusionConfirmation/{id}")]
        [Permission(Code = "EBRSC9")]
        public IHttpActionResult GetEDBloodTransfusionConfirmationAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetBloodTransfusionConfirmation(brsac));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/BloodRequestSupplyAndConfirmation/TransfusionConfirmation/{id}")]
        [Permission(Code = "EBRSC10")]
        public IHttpActionResult UpdateEDBloodTransfusionConfirmationAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(ed.Id, "ED");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            var ListId = HandleCreateOrUpdateBloodTransfusionConfirmationData(brsac, request);

            return Content(HttpStatusCode.OK, new {
                ViMessage = "Thành công",
                EnMessage = "Success",
                ListId
            });
        }

        private void ConfirmBloodRequestDatas(EIOBloodRequestSupplyAndConfirmation brsac)
        {
            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac.Id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_PURCHASE &&
                !e.IsConfirm
            );
            foreach (var data in datas)
            {
                data.IsConfirm = true;
                unitOfWork.EIOBloodProductDataRepository.Update(data);
            }
        }
    }
}
