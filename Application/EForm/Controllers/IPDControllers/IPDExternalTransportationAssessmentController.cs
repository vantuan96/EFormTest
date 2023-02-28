using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models.PrescriptionModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDExternalTransportationAssessmentController : EIOExternalTransportationAssessmentController
    {
        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/ExternalTransportationAssessment/Create/{visitId}")]
        [Permission(Code = "IPDEXTA1")]
        public IHttpActionResult CreateExternalTransportationAssessmentAPI(Guid visitId)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.BangDanhGiaNhuCauTrangThietBi))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            //var ex_assess = GetEIOExternalTransportationAssessment(visit.Id, "IPD");
            //if (ex_assess != null)
            //    return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            EIOExternalTransportationAssessment form = CreateEIOExternalTransportationAssessment(visit.Id, "IPD");
            return Content(HttpStatusCode.OK, new
            {
                Message.SUCCESS,
                ItemId = form.Id
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/ExternalTransportationAssessment/Confirm/{visitId}/{itemId}")]
        [Permission(Code = "IPDEXTA2")]
        public IHttpActionResult ConfirmExternalTransportationAssessmentAPI(Guid visitId, Guid itemId, [FromBody] JObject request)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);            
            var ex_assess = GetEIOExternalTransportationAssessment(visit.Id, itemId, "IPD");
            if (ex_assess == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            if (IPDIsBlock(visit, Constant.IPDFormCode.BangDanhGiaNhuCauTrangThietBi, ex_assess.Id))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            var error = ConfirmExternalTransportationAssessment(ex_assess, request);
            if (error != null)
            {
                if (error.ViMessage == "Bạn không có quyền truy cập")
                {
                    return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
                }
                else if (error.ViMessage == "Thông tin xác nhận không đúng")
                {
                    return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
                }
            }            
            var user = GetUser();

            if (user != null)
            {
                var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();

                HandleUpdateOrCreateExternalTransportationAssessment(ex_assess, positions, request, visit);
            }           
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/ExternalTransportationAssessment/{visitID}/{itemId}")]
        [Permission(Code = "IPDEXTA3")]
        public IHttpActionResult GetExternalTransportationAssessmentAPI(Guid visitId, Guid itemId)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var ex_assess = GetEIOExternalTransportationAssessment(visit.Id, itemId, "IPD");
            bool IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangDanhGiaNhuCauTrangThietBi, itemId);
            
            if (ex_assess == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Bảng đánh giá nhu cầu trang thiết bị/nhân lực vận chuyển ngoại viện không tồn tại",
                    EnMessage = "Bảng đánh giá nhu cầu trang thiết bị/nhân lực vận chuyển ngoại viện không tồn tại",
                    IsLocked
                });        
            var ex_assessResult = GetExternalTransportationAssessmentResult(ex_assess);
            return Content(HttpStatusCode.OK, ex_assessResult);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/ExternalTransportationAssessment/{visitID}/{itemId}")]
        [Permission(Code = "IPDEXTA4")]
        public IHttpActionResult UpdateExternalTransportationAssessmentAPI(Guid visitId,Guid itemId, [FromBody] JObject request)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var islock24h = IPDIsBlock(visit, Constant.IPDFormCode.BangDanhGiaNhuCauTrangThietBi, itemId);            
            var ex_assess = GetEIOExternalTransportationAssessment(visit.Id, itemId, "IPD");
            
            if (ex_assess == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            ex_assess.UpdatedAt = DateTime.Now;   
            if (islock24h)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            
            if (ex_assess.DoctorId != null || ex_assess.NurseId != null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var user = GetUser();
            if (user == null) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            ex_assess.UpdatedBy = user.Username;
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();

            HandleUpdateOrCreateExternalTransportationAssessment(ex_assess, positions, request, visit);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/ExternalTransportationAssessment/List/{visitId}")]
        [Permission(Code = "IPDEXTA5")]
        public IHttpActionResult GetListExternalTransportationAssessmentAPI(Guid visitId)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            List<EIOExternalTransportationAssessment> listEIOExternalTransportationAssessment = GetListEIOExternalTransportationAssessment(visit.Id, "IPD");
            bool IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangDanhGiaNhuCauTrangThietBi);                     
            return Content(HttpStatusCode.OK, new
            {
                EIOExternalTransportationAssessments = listEIOExternalTransportationAssessment,
                IsLocked
            });
        }
       
    }
}
