using DataAccess.Models;
using DataAccess.Models.EOCModel;
using DataAccess.Models.OPDModel;
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
using System.Web.Http;

namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCStandingOrderController : BaseEOCApiController
    {
        [HttpGet]
        [Route("api/EOC/StandingOrder/{id}")]
        [Permission(Code = "EOC055")]
        public IHttpActionResult GetOPDStandingOrderAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var user_confirm = unitOfWork.UserRepository.AsQueryable();

            var results = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.EOC_STANDING_ORDER)
            )
            .OrderBy(o => o.CreatedAt)
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
                Position = GetListPosotionUserByUserName(o.CreatedBy),
                o.UpdatedAt,
                o.UpdatedBy,
                ConfirmCreated = GetEIOFormConfirmByFormId(o.Id),
                FullNameDoctorConfirm = user_confirm.FirstOrDefault(e => !e.IsDeleted && e.Username == o.DoctorConfirm)?.Fullname,
                o.DoctorTime
            }).ToList();

            var lastTime = results.Max(e => e.UpdatedAt);
            var lastObj = results.FirstOrDefault(e => e.UpdatedAt == lastTime);

            return Content(HttpStatusCode.OK, new { Datas = results, LastUpdated = new { lastObj?.UpdatedAt, lastObj?.UpdatedBy }, Version = visit.Version });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/StandingOrder/{id}")]
        [Permission(Code = "EOC057")]
        public IHttpActionResult UpdateOPDStandingOrderAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var user = GetUser();

            var list_stand = new List<Guid>();
            foreach (var item in request["Datas"])
            {
                string item_id = item["Id"]?.ToString();
                var order = GetOrCreateOrder(item_id, id);
                if (order != null && IsUserCreateFormManual(user.Username, order.CreatedBy))
                {
                    UpdateOrder(order, item);
                    list_stand.Add(order.Id);
                }
            }
            unitOfWork.Commit();

            if (NeedConfirmOrder(id))
                CreateConfirmNotification(user.Username, visit);
            return Content(HttpStatusCode.OK, list_stand);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/StandingOrder/Confirm/{id}")]
        [Permission(Code = "EOC055")] 
        public IHttpActionResult ConfirmOrderByDoctorAPI(Guid id, [FromBody] JObject request)
        {
            var order = unitOfWork.OrderRepository.GetById(id);
            if (order == null || order.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ORDER_NOT_FOUND);

            var visit = GetEOC((Guid)order.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            if (visit.Version >= 7 && request["kind"]?.ToString()?.ToUpper() == "CREATEDCONFIRM")
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

            var acction = GetActionOfUser(user, "EOC058");
            if (acction == null)
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var oen = GetOutpatientExaminationNote(id);
            if ((visit.PrimaryDoctorId != null && visit.PrimaryDoctorId == user.Id) || (visit.AuthorizedDoctorId != null && visit.AuthorizedDoctorId == user.Id))
            {
                if (visit.AuthorizedDoctorId == user.Id)
                {
                    oen.IsAuthorizeDoctorChangeForm = true;
                }
                order.DoctorId = user.Id;
                order.DoctorConfirm = user.Username;
                order.DoctorTime = DateTime.Now;
                order.IsConfirm = true;
                unitOfWork.OrderRepository.Update(order);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/StandingOrder/ConfirmAll/{id}")]
        [Permission(Code = "EOC058")]
        public IHttpActionResult ConfirmAllOrderByDoctorAPI(Guid id, [FromBody] JObject request)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains("Doctor"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);


            var not_confirm_orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.EOC_STANDING_ORDER) &&
                !i.IsConfirm
            ).ToList();
            var oen = GetOutpatientExaminationNote(id);
            if (((visit.PrimaryDoctorId != null && visit.PrimaryDoctorId == user.Id) || (visit.AuthorizedDoctorId != null && visit.AuthorizedDoctorId == user.Id)))
            {
                if (visit.AuthorizedDoctorId == user.Id)
                {
                    oen.IsAuthorizeDoctorChangeForm = true;
                }
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
            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/StandingOrder/Delete/{id}")]
        [Permission(Code = "EOC059")]
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

            var visit = GetEOC((Guid)order.VisitId);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);


            unitOfWork.OrderRepository.Delete(order);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private Order GetOrCreateOrder(string order_id, Guid opd_id)
        {
            if (string.IsNullOrEmpty(order_id))
            {
                Order order = new Order
                {
                    VisitId = opd_id,
                    OrderType = Constant.EOC_STANDING_ORDER,
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
                e.OrderType.Equals(Constant.EOC_STANDING_ORDER) &&
                !e.IsConfirm
            );
            if (order != null)
                return true;
            return false;
        }
        private void CreateConfirmNotification(string from_user, EOC opd)
        {
            var spec = opd.Specialty;
            var customer = opd.Customer;
            var primary_doctor = opd.PrimaryDoctor;
            if (primary_doctor != null)
            {
                var vi_mes = string.Format(
                    "<b>[EOC - {0}]</b> Bạn có yêu cầu xác nhận <b>Standing Order</b> cho bệnh nhân <b>{1}</b>",
                    spec?.ViName,
                    customer?.Fullname
                );
                var en_mes = string.Format(
                    "<b>[EOC - {0}]</b> Bạn có yêu cầu xác nhận <b>Standing Order</b> cho bệnh nhân <b>{1}</b>",
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
                    visit_id: opd.Id,
                    group_code: "EOC",
                    form_frontend: "StandingOrder"
                );
                noti_creator.Create();
            }
        }
    }
}
