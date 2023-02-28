using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDStandingOrderController : BaseIPDApiController
    {
        private const string code_unlockedForm = "A03_029_050919_VE";

        [HttpGet]
        [Route("api/IPD/StandingOrder/{id}")]
        [Permission(Code = "ORDERVIEW")]
        public IHttpActionResult GetIPDStandingOrderAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            bool isLocked = IPDIsBlock(visit, code_unlockedForm, id);

            var _results = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.IPD_STANDING_ORDER)
            ).ToList();

            var user_confirm = unitOfWork.UserRepository.AsQueryable();

            //bool isUnlockConfirm = IsUnlockConfirm(visit.Id);
            //if (isUnlockConfirm)
            //    RemoveConfirmOrder(_results, visit);

            var results = _results.OrderBy(o => o.CreatedAt)
            .Select(o => new
            {
                o.Id,
                o.StandingOrderMasterDataId,
                StandingOrderName = o.StandingOrderMasterData?.Name,
                o.Drug,
                o.Dosage,
                o.Route,
                UsedAt = o.UsedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                o.MedicalStaffName,
                o.DoctorConfirm,
                o.IsConfirm,
                o.Status,
                o.Note,
                IsLocked = isLocked,
                Position = GetListPosotionUserByUserName(o.CreatedBy),
                o.UpdatedAt,
                o.UpdatedBy,
                ConfirmCreated = GetEIOFormConfirmByFormId(o.Id),
                FullNameDoctorConfirm = user_confirm.FirstOrDefault(e => !e.IsDeleted && e.Username == o.DoctorConfirm)?.Fullname,
                o.DoctorTime
            }).ToList();

            var lastTime = results.Max(e => e.UpdatedAt);
            var lastObj = results.FirstOrDefault(e => e.UpdatedAt == lastTime);

            return Content(HttpStatusCode.OK, new { Datas = results, LastUpdated = new {lastObj?.UpdatedAt, lastObj?.UpdatedBy }, Version = visit.Version });
        }

        [HttpGet]
        [Route("api/IPD/StandingOrder/IsLocked/{visitId}")]
        public IHttpActionResult GetLockStatus(Guid visitId)
        {
            IPD visit = GetIPD(visitId);
            if ((visit == null))
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, new { Is24hLocked = IPDIsBlock(visit, code_unlockedForm, visitId) });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/StandingOrder/{id}")]
        [Permission(Code = "ORDERCREATE")]
        public IHttpActionResult UpdateIPDStandingOrderAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (IPDIsBlock(visit, code_unlockedForm, id))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);


            var user = GetUser();

            var list_stand = new List<Guid>();
            foreach (var item in request["Datas"])
            {
                string item_id = item["Id"]?.ToString();
                var order = GetOrCreateOrder(item_id, visit.Id);
                if (order != null)
                {
                    UpdateOrder(order, item);
                    list_stand.Add(order.Id);
                }
            }
            unitOfWork.Commit();

            if (NeedConfirmOrder(visit.Id))
                CreateConfirmNotification(user.Username, visit);

            return Content(HttpStatusCode.OK, list_stand);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/StandingOrder/Confirm/{id}")]
        [Permission(Code = "ORDERVIEW")]
        public IHttpActionResult ConfirmOrderByDoctorAPI(Guid id, [FromBody] JObject request)
        {
            var order = unitOfWork.OrderRepository.GetById(id);
            if (order == null || order.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ORDER_NOT_FOUND);

            var visit = GetIPD((Guid)order.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (IPDIsBlock(visit, code_unlockedForm, id))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            if(visit.Version >=7 && request["kind"]?.ToString()?.ToUpper() == "CREATEDCONFIRM")
            {
                if (order.CreatedBy.ToLower() != user.Username.ToLower())
                    return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
                order.NurseId = user.Id;
                order.NurseTime = DateTime.Now;
                unitOfWork.OrderRepository.Update(order);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }

            if (order.IsConfirm)
                return Content(HttpStatusCode.NotFound, Message.DOCTOR_ACCEPTED);

            var action = GetActionOfUser(user, "ORDERCONFIRM");
            if (action == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            order.DoctorId = user.Id;
            order.DoctorConfirm = user.Username;
            order.DoctorTime = DateTime.Now;
            order.IsConfirm = true;
            unitOfWork.OrderRepository.Update(order);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/StandingOrder/ConfirmAll/{id}")]
        [Permission(Code = "ORDERCONFIRMALL")]
        public IHttpActionResult ConfirmAllOrderByDoctorAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            if (IPDIsBlock(visit, code_unlockedForm, id))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var action = GetActionOfUser(user, "ORDERCONFIRMALL");
            if (action == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var not_confirm_orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.IPD_STANDING_ORDER) &&
                !i.IsConfirm
            ).ToList();

            foreach (var order in not_confirm_orders)
            {
                order.DoctorId = user.Id;
                order.DoctorConfirm = user.Username;
                order.DoctorTime = DateTime.Now;
                order.IsConfirm = true;
                unitOfWork.OrderRepository.Update(order);
            }
            unitOfWork.Commit();



            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/StandingOrder/Delete/{id}")]
        [Permission(Code = "ORDERDELETE")]
        public IHttpActionResult DeleteOrderAPI(Guid id)
        {
            Order order = unitOfWork.OrderRepository.GetById(id);
            if (order.IsConfirm)
                return Content(HttpStatusCode.BadRequest, Message.SUCCESS);

            //cua ai nguoi do xoa
            var user = GetUser();
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.CSRF_MISSING);
            if (user.Username.ToLower() != order.CreatedBy.ToLower())
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var visit = GetIPD((Guid)order.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (IPDIsBlock(visit, code_unlockedForm, id))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            unitOfWork.OrderRepository.Delete(order);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private Order GetOrCreateOrder(string order_id, Guid visitId)
        {
            if (string.IsNullOrEmpty(order_id))
            {
                Order order = new Order
                {
                    VisitId = visitId,
                    OrderType = Constant.IPD_STANDING_ORDER,
                    MedicalStaffName = GetUser().Username,
                    Status = true
                };
                unitOfWork.OrderRepository.Add(order);
                return order;
            }
            return unitOfWork.OrderRepository.GetById(new Guid(order_id));
        }

        private void UpdateOrder(Order order, JToken item)
        {
            if (!order.IsConfirm)
            {
                var old = new
                {
                    StandingOrderMasterDataId = order.StandingOrderMasterDataId.ToString(),
                    order.Drug,
                    order.Dosage,
                    order.Route,
                    UsedAt = order.UsedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    order.Note,
                };
                var _new = new
                {
                    StandingOrderMasterDataId = item["StandingOrderMasterDataId"]?.ToString(),
                    Drug = item["Drug"]?.ToString(),
                    Dosage = item["Dosage"]?.ToString(),
                    Route = item["Route"]?.ToString(),
                    UsedAt = item["UsedAt"]?.ToString(),
                    Note = item["Note"]?.ToString(),
                };

                if (JsonConvert.SerializeObject(old) != JsonConvert.SerializeObject(_new))
                {
                    var st_id = new Guid(_new.StandingOrderMasterDataId);
                    order.StandingOrderMasterDataId = st_id;
                    order.Drug = _new.Drug;
                    order.Dosage = _new.Dosage;
                    order.Route = _new.Route;
                    order.Note = _new.Note;
                    if (!string.IsNullOrEmpty(_new.UsedAt))
                        order.UsedAt = DateTime.ParseExact(_new.UsedAt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                    else
                        order.UsedAt = null;
                    unitOfWork.OrderRepository.Update(order);
                }
            }
        }

        private bool NeedConfirmOrder(Guid visit_id)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                e.OrderType.Equals(Constant.IPD_STANDING_ORDER) &&
                !e.IsConfirm
            );
            if (order != null)
                return true;
            return false;
        }
        private void CreateConfirmNotification(string from_user, IPD visit)
        {
            var spec = visit.Specialty;
            var customer = visit.Customer;
            var primary_doctor = visit.PrimaryDoctor;

            if (primary_doctor != null)
            {
                var vi_mes = string.Format(
                    "<b>[IPD - {0}]</b> Bạn có yêu cầu xác nhận <b>Standing Order</b> cho bệnh nhân <b>{1}</b>",
                    spec?.ViName,
                    customer?.Fullname
                );
                var en_mes = string.Format(
                    "<b>[IPD - {0}]</b> Bạn có yêu cầu xác nhận <b>Standing Order</b> cho bệnh nhân <b>{1}</b>",
                    spec?.ViName,
                    customer?.Fullname
                );
                var noti_creator = new NotificationCreator(
                    unitOfWork: unitOfWork,
                    from_user: from_user,
                    to_user: primary_doctor.Username,
                    priority: 4,
                    vi_message: vi_mes,
                    en_message: en_mes,
                    spec_id: spec?.Id,
                    visit_id: visit.Id,
                    group_code: "IPD",
                    form_frontend: "StandingOrder"
                );
                noti_creator.Create();
            }
        }

        private void RemoveConfirmOrder(List<Order> orders, ED visit)
        {
            foreach(var order in orders)
            {
                order.DoctorId = null;
                order.DoctorConfirm = null;
                order.IsConfirm = false;
            }
        }
    }
}

