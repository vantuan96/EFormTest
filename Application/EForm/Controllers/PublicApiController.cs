using DataAccess;
using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
namespace EForm.Controllers
{
    [SessionAuthorize]
    public class PublicApiController : BaseApiController
    {
        [HttpPost]
        [Route("api/PublicApi/Charge/UpdateStatus")]
        public IHttpActionResult GetCustomer([FromBody] ChargeUpdateModel request)
        {
            if (request.ChargeId == null || request.ChargeId == Guid.Empty)
                return Content(HttpStatusCode.BadRequest, "Missing parameter ChargeId");
            if (string.IsNullOrEmpty(request.Status))
                return Content(HttpStatusCode.BadRequest, "Missing parameter Status");

            try
            {
                var findChargeItem = unitOfWork.ChargeItemRepository.AsQueryable().Where(c => c.ChargeId == request.ChargeId).ToList();
                if (findChargeItem.Count > 0)
                {
                    foreach (var c in findChargeItem)
                    {
                        if (request.Status == Constant.AlliesRadResponseStatus.Placed)
                        {
                            if (c.Status == Constant.ChargeItemStatus.Placing)
                            {
                                c.Status = Constant.ChargeItemStatus.Placed;
                            }
                            else if (c.Status == Constant.ChargeItemStatus.Cancelling)
                            {
                                c.Status = Constant.ChargeItemStatus.Cancelled;
                            }
                        }
                        else if (request.Status == Constant.AlliesRadResponseStatus.PlaceFailed)
                        {
                            c.Status = Constant.ChargeItemStatus.Failed;
                            c.FailedReason = request.FailedReason;
                        }
                        else if (request.Status == Constant.AlliesRadResponseStatus.CancelFailed)
                        {
                            c.FailedReason = request.FailedReason;
                        }
                        unitOfWork.ChargeItemRepository.Update(c);
                    }

                    unitOfWork.Commit();
                    return Content(HttpStatusCode.OK, new
                    {
                        Message = "Charge updated successfully"
                    });
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Message = "No charge found"
                    });
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    ex.Message
                });
            }

        }
        [HttpPost]
        [Route("api/PublicApi/Ping")]
        public IHttpActionResult Ping()
        {
            return Content(HttpStatusCode.OK, new
            {
                Message = "Pong"
            });
        }
        [HttpPost]
        [Route("api/eSignCallback")]
        public IHttpActionResult eSignCallback()
        {
            return Content(HttpStatusCode.OK, new
            {
                Message = "Đã nhận thông tin"
            });
        }
    }
}