using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDBloodRequestSupplyAndConfirmationController : EIOBloodRequestSupplyAndConfirmationController
    {
        #region Phẩn Chung
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/Create/{id}")]
        [Permission(Code = "IBRSC01")]
        public IHttpActionResult CreateEDBloodRequestSupplyAndConfirmationAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");

            if (brsac != null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_EXIST);

            brsac = CreateBloodRequestSupplyAndConfirmation(visit.Id, "IPD", visit.SpecialtyId, visit.Version);

            return Content(HttpStatusCode.OK, new { brsac.Id });
        }


        [HttpGet]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/{id}")]
        [Permission(Code = "IBRSC02")]
        public IHttpActionResult GetEDBloodRequestSupplyAndConfirmationAPI(Guid id)
        {
            var visit = GetIPD(id);
            
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");
            bool IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac?.Id);
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu dự trù, cung cấp và xác nhận dự trù máu - chế phẩm máu không tồn tại",
                    EnMessage = "Phiếu dự trù, cung cấp và xác nhận dự trù máu - chế phẩm máu is not found",
                    IsLocked,
                });

            return Content(HttpStatusCode.OK, GetBloodRequestSupplyAndConfirmationDatas(brsac, visit, visit.IPDMedicalRecordId, "IPD", IsLocked));
        }
        #endregion

        #region Tab1
        [HttpGet]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/PurchaseRequest/{id}")]
        [Permission(Code = "IBRSC03")]
        public IHttpActionResult GetEDBloodRequestAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            var ipdisblock = IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id);            

            return Content(HttpStatusCode.OK, GetBloodRequest(brsac, null, "IPD", ipdisblock));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/PurchaseRequest/{id}")]
        [Permission(Code = "IBRSC04")]
        public IHttpActionResult UpdateEDBloodRequestAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);            
            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);                    
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id))            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            if (brsac.DoctorConfirmId != null && brsac.HeadOfDeptId != null)
                return Content(HttpStatusCode.NotFound, Message.DOCTOR_ACCEPTED);
            HandleCreateOrUpdateBloodRequestData(brsac, request, visit.Customer);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/PurchaseRequest/Confirm/{id}")]
        [Permission(Code = "IBRSC05")]
        public IHttpActionResult ConfirmEDBloodRequestAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);           
            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id))            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains("Head Of Department"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            ConfirmBloodRequest(brsac, user.Id);                     
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/PurchaseRequest/DoctorConfirm/{visitId}")]
        [Permission(Code = "BSXNPDTM")]
        public IHttpActionResult DoctorConfirmIPDBloodRequesAPI(Guid visitId, [FromBody] JObject request)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var acction = GetActionOfUser(user, "BSXNPDTM");
            if (acction == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            ConfirmBloodRequestDatas(brsac);
            brsac.DoctorConfirmId = user.Id;
            brsac.DoctorConfirmTime = DateTime.Now;
            unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        #endregion

        #region Tab2
        [HttpGet]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/Supply/{id}")]
        [Permission(Code = "IBRSC06")]
        public IHttpActionResult GetEDBloodSupplyAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            return Content(HttpStatusCode.OK, GetBloodSupply(brsac, IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id)));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/Supply/{id}")]
        [Permission(Code = "IBRSC07")]
        public IHttpActionResult UpdateEDBloodSupplyAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id))            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            
            HandleCreateOrUpdateBloodSupplyData(brsac, request);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/Supply/Confirm/{id}")]
        [Permission(Code = "IBRSC08")]
        public IHttpActionResult ConfirmEDBloodSupplyAPI(Guid id, [FromBody] JObject request)
        {
            var supply = unitOfWork.EIOBloodSupplyDataRepository.GetById(id);
            if (supply == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);

            var visit = GetIPD((Guid)supply.EIOBloodRequestSupplyAndConfirmation.VisitId);
            if(visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, id))            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);            
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
            if (is_success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
        }

        #endregion


        #region Tab3
        [HttpGet]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/TransfusionConfirmation/{id}")]
        [Permission(Code = "IBRSC09")]
        public IHttpActionResult GetEDBloodTransfusionConfirmationAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");

            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);
            var is24hlock = IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id);            
            return Content(HttpStatusCode.OK, GetBloodTransfusionConfirmation(brsac, is24hlock));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/BloodRequestSupplyAndConfirmation/TransfusionConfirmation/{id}")]
        [Permission(Code = "IBRSC10")]
        public IHttpActionResult UpdateEDBloodTransfusionConfirmationAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);            
            var brsac = GetBloodRequestSupplyAndConfirmation(visit.Id, "IPD");
            if (brsac == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_BRSAC_NOT_FOUND);            
            if (IPDIsBlock(visit, Constant.IPDFormCode.PhieuDuTruMau, brsac.Id))            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            var ListId = HandleCreateOrUpdateBloodTransfusionConfirmationData(brsac, request);

            return Content(HttpStatusCode.OK, new
            {
                ViMessage = "Thành công",
                EnMessage = "Success",
                ListId
            });
        }
        #endregion

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
