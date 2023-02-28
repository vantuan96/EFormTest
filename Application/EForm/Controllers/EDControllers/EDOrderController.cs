using DataAccess.Models;
using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDOrderController : BaseEDApiController
    {
        private string FormCode = "EDVOFV1";
        private int currentVersion = 1;
        [HttpGet]
        [Route("api/ED/Order/{id}")]
        [Permission(Code = "EORDE1")]
        public IHttpActionResult GetOrderAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == ed.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.ED_ORDER)
            )
            .OrderBy(o => o.CreatedAt)
            .Select(o => new
            {
                o.Id,
                Time = o.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                o.Drug,
                o.Dosage,
                o.Route,
                UsedAt = o.UsedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                o.MedicalStaffName,
                o.DoctorConfirm,
                o.IsConfirm,
                o.Status,
                o.Quantity
            })
            .ToList();
            int ver = getVesion(id);
            return Content(HttpStatusCode.OK, new{ 
                EdId = ed.Id,
                DatasOrder = orders,
                Version = ver,
                Customer = GetCustomerInfoInVisit(ed, "ED"),
                FormCode,
                Datas = GetFormData(id, id, FormCode),
                DiagnosisOption = GetDiagnosForICD10D(ed)
            });
        }

        private int getVesion(Guid id)
        {
            var EDVerbalOrder = unitOfWork.EDVerbalOrderRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == id);
            if (EDVerbalOrder == null) return 0;
            else return EDVerbalOrder.Version;
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/Order/Create/{id}")]
        [Permission(Code = "EORDE7")]
        public IHttpActionResult Create(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var EDVerbalOrder = unitOfWork.EDVerbalOrderRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == id);
            if (EDVerbalOrder == null) { 
                var VerbalOrder = new EDVerbalOrder
                {
                    VisitId = ed.Id,
                    Version = currentVersion
                };
                unitOfWork.EDVerbalOrderRepository.Add(VerbalOrder);
                unitOfWork.Commit();
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            return Content(HttpStatusCode.NotFound, Message.FORBIDDEN);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/Order/{id}")]
        [Permission(Code = "EORDE2")]
        public IHttpActionResult UpdateOrderAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDORD", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            HandleUpdateOrCreateFormDatas(id, id, FormCode, request["Datas"]);
            HandleUpdateOrCreateOrder(ed.Id, request["DatasOrder"]);

            if (NeedConfirmOrder(ed.Id))
                CreateConfirmNotification(user.Username, ed);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/Order/Confirm/{id}")]
        [Permission(Code = "EORDE3")]
        public IHttpActionResult ConfirmOrderByDoctorAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if(user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains("Doctor"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var not_confirm_orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == ed.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.ED_ORDER) &&
                !i.IsConfirm
            ).ToList();
            foreach (var order in not_confirm_orders)
            {
                order.DoctorId = user.Id;
                order.DoctorConfirm = user.Username;
                order.IsConfirm = true;
                unitOfWork.OrderRepository.Update(order);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/Order/NurseConfirm/{id}")]
        [Permission(Code = "EORDE6")]
        public IHttpActionResult ConfirmOrderByNurseAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains("Nurse"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var not_confirm_orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == ed.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.ED_ORDER) &&
                i.Status &&
                string.IsNullOrEmpty(i.MedicalStaffName)
            ).ToList();
            foreach (var order in not_confirm_orders)
            {
                order.MedicalStaffName = user.Username;
                unitOfWork.OrderRepository.Update(order);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/Order/Delete/{id}")]
        [Permission(Code = "EORDE4")]
        public IHttpActionResult DeleteOrderAPI(Guid id)
        {
            Order order = unitOfWork.OrderRepository.GetById(id);
            if(order.IsConfirm)
                return Content(HttpStatusCode.BadRequest, Message.SUCCESS);

            unitOfWork.OrderRepository.Delete(order);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void HandleUpdateOrCreateOrder(Guid ed_id, JToken request_datas)
        {
            foreach (var item in request_datas)
            {
                string item_id = item["Id"]?.ToString();
                if (string.IsNullOrEmpty(item_id))
                {
                    CreateOrder(ed_id, item);
                }
                else
                {
                    Guid order_id = new Guid(item_id);
                    Order order = unitOfWork.OrderRepository.GetById(order_id);
                    if (item["Remove"]?.ToString() == "True")
                        if (!order.IsConfirm)
                            order.IsDeleted = true;
                    UpdateOrder(order, item);
                }
            }
            unitOfWork.Commit();
        }

        private void CreateOrder(Guid ed_id, JToken item)
        {
            Order order = new Order();
            order.VisitId = ed_id;
            order.OrderType = Constant.ED_ORDER;
            order.Drug = item["Drug"]?.ToString();
            order.Dosage = item["Dosage"]?.ToString();
            order.Route = item["Route"]?.ToString();
            order.Quantity = item["Quantity"]?.ToString();
            var used_at = item["UsedAt"]?.ToString();
            
            if (!string.IsNullOrEmpty(used_at))
                order.UsedAt = DateTime.ParseExact(used_at, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                order.UsedAt = null;
            order.MedicalStaffName = item["MedicalStaffName"]?.ToString();
            order.Status = item["Status"].ToObject<bool>();
            unitOfWork.OrderRepository.Add(order);
        }

        private void UpdateOrder(Order order, JToken item)
        {
            bool is_change = false;
            if (!order.IsConfirm)
            {
                var old = new
                {
                    order.Drug,
                    order.Dosage,
                    order.Route,
                    order.Quantity,
                    UsedAt = order.UsedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    order.MedicalStaffName,
                };
                var _new = new
                {
                    Drug = item["Drug"]?.ToString(),
                    Dosage = item["Dosage"]?.ToString(),
                    Route = item["Route"]?.ToString(),
                    UsedAt = item["UsedAt"]?.ToString(),
                    Quantity = item["Quantity"]?.ToString(),
                    MedicalStaffName = item["MedicalStaffName"]?.ToString(),
                };

                if (JsonConvert.SerializeObject(old) != JsonConvert.SerializeObject(_new))
                {
                    is_change = true;
                    order.Drug = _new.Drug;
                    order.Dosage = _new.Dosage;
                    order.Route = _new.Route;
                    order.Quantity = _new.Quantity;
                    if (!string.IsNullOrEmpty(_new.UsedAt))
                        order.UsedAt = DateTime.ParseExact(_new.UsedAt, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                    else
                        order.UsedAt = null;
                    order.MedicalStaffName = _new.MedicalStaffName;
                }
            }
            else
            {
                var status = item["Status"].ToObject<bool>();
                if (order.Status != status)
                {
                    is_change = true;
                    order.Status = status;
                }
            }
            if (is_change)
            {
                unitOfWork.OrderRepository.Update(order);
            }
        }

        private bool NeedConfirmOrder(Guid visit_id)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                e.OrderType.Equals(Constant.ED_ORDER) &&
                !e.IsConfirm
            );
            if (order != null)
                return true;
            return false;
        }
        private void CreateConfirmNotification(string from_user, ED ed)
        {
            var spec = ed.Specialty;
            var customer = ed.Customer;
            string primary_doctor_user_name = null;
            var discharge = ed.DischargeInformation;
            if (!IsNew(discharge.CreatedAt, discharge.UpdatedAt))
                primary_doctor_user_name = discharge.UpdatedBy;
            if (!string.IsNullOrEmpty(primary_doctor_user_name))
            {
                var vi_mes = string.Format(
                    "<b>[ED - {0}]</b> Bạn có yêu cầu xác nhận <b>y lệnh miệng</b> cho bệnh nhân <b>{1}</b>",
                    spec?.ViName,
                    customer?.Fullname
                );
                var en_mes = string.Format(
                    "<b>[OPD - {0}]</b> Bạn có yêu cầu xác nhận <b>y lệnh miệng</b> cho bệnh nhân <b>{1}</b>",
                    spec?.ViName,
                    customer?.Fullname
                );
                var noti_creator = new NotificationCreator(
                    unitOfWork: unitOfWork,
                    from_user: from_user,
                    to_user: primary_doctor_user_name,
                    priority: 4,
                    vi_message: vi_mes,
                    en_message: en_mes,
                    spec_id: spec?.Id,
                    visit_id: ed.Id,
                    group_code: "ED",
                    form_frontend: "Order"
                );
                noti_creator.Create();
            }
        }

        private string GetDiagnosForICD10D(ED visit)
        {
            if (visit.DischargeInformationId == null)
                return null;
            var data = unitOfWork.DischargeInformationDataRepository.Find(d => !d.IsDeleted && d.Code == "DI0DIAOPT2" && d.DischargeInformationId == visit.DischargeInformationId)
                       .Select(e => new MasterDataValue()
                       {
                           Code = e.Code,
                           Value = e.Value
                       }).ToList();


            return getValueFromMasterDatas("DI0DIAOPT2", data);
        }
    }
}
