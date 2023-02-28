using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Utils;
using EMRModels;
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
    public class IPDVerbalOrderController : BaseIPDApiController
    {
        private string FormCode = "A03_030_290321_VE";
        private int currentVersion = 1;

        [HttpGet]
        [Route("api/IPD/Order/{id}")]
        [Permission(Code = "IPDVBO")]
        public IHttpActionResult GetOrderAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == ipd.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.IPD_ORDER)
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
            return Content(HttpStatusCode.OK, new
            {
                ipdId = ipd.Id,
                DatasOrder = orders,
                Version = ver,
                Customer = GetCustomerInfoInVisit(ipd, "IPD"),
                FormCode,
                IsLocked24h = IPDIsBlock(ipd, FormCode),
                Datas = GetFormData(id, id, FormCode),
                DiagnosisOption = GetTextValue(ipd, "IPDMRPTCDKTANS")
            });
        }

        private int getVesion(Guid id)
        {
            var IPDVerbalOrder = unitOfWork.EDVerbalOrderRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == id);
            if (IPDVerbalOrder == null) return 0;
            else return IPDVerbalOrder.Version;
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/Order/Create/{id}")]
        [Permission(Code = "IPDVBO1")]
        public IHttpActionResult Create(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            if(IPDIsBlock(ipd, FormCode))
                return Content(HttpStatusCode.BadRequest, new { ViName = "Hồ sơ đã khóa không thể chỉnh sửa", EnName = "Profile is locked and cannot be edited" });

            var IPDVerbalOrder = unitOfWork.EDVerbalOrderRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == id);
            if (IPDVerbalOrder == null)
            {
                var VerbalOrder = new EDVerbalOrder
                {
                    VisitId = ipd.Id,
                    Version = currentVersion
                };
                unitOfWork.EDVerbalOrderRepository.Add(VerbalOrder);
                unitOfWork.Commit();
                CreateOrUpdateFormForSetupOfAdmin(ipd.Id, VerbalOrder.Id, FormCode);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            return Content(HttpStatusCode.NotFound, Message.FORBIDDEN);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/Order/{id}")]
        [Permission(Code = "IPDVBO1")]
        public IHttpActionResult UpdateOrderAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var user = GetUser();

            if (IPDIsBlock(ipd, FormCode))
                return Content(HttpStatusCode.BadRequest, new { ViName = "Hồ sơ đã khóa không thể chỉnh sửa", EnName = "Profile is locked and cannot be edited" });

            HandleUpdateOrCreateFormDatas(id, id, FormCode, request["Datas"]);
            HandleUpdateOrCreateOrder(ipd.Id, request["DatasOrder"]);

            if (NeedConfirmOrder(ipd.Id))
                CreateConfirmNotification(user.Username, ipd);

            var IPDVerbalOrder = unitOfWork.EDVerbalOrderRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == id);
            IPDVerbalOrder.UpdatedAt = DateTime.Now;
            IPDVerbalOrder.UpdatedBy = GetUser().Username;
            unitOfWork.EDVerbalOrderRepository.Update(IPDVerbalOrder);
            unitOfWork.Commit();

            CreateOrUpdateFormForSetupOfAdmin(ipd.Id, IPDVerbalOrder.Id, FormCode);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/Order/Confirm/{id}")]
        [Permission(Code = "IPDVBO2")]
        public IHttpActionResult ConfirmOrderByDoctorAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();
            if (!positions.Contains("Doctor"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var not_confirm_orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == ipd.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.IPD_ORDER) &&
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
        [Route("api/IPD/Order/NurseConfirm/{id}")]
        [Permission(Code = "IPDVBO3")]
        public IHttpActionResult ConfirmOrderByNurseAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName).ToList();
            if (!positions.Contains("Nurse"))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var not_confirm_orders = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == ipd.Id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.IPD_ORDER) &&
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
        [Route("api/IPD/Order/Delete/{id}")]
        [Permission(Code = "IPDVBO4")]
        public IHttpActionResult DeleteOrderAPI(Guid id)
        {
            Order order = unitOfWork.OrderRepository.GetById(id);
            if (order.IsConfirm)
                return Content(HttpStatusCode.BadRequest, new { ViName = "Form đã xác nhận không thể xóa", EnName = "Form is Confirm and cannot deleted" });

            unitOfWork.OrderRepository.Delete(order);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void HandleUpdateOrCreateOrder(Guid ipd_id, JToken request_datas)
        {
            foreach (var item in request_datas)
            {
                string item_id = item["Id"]?.ToString();
                if (string.IsNullOrEmpty(item_id))
                {
                    CreateOrder(ipd_id, item);
                }
                else
                {
                    Guid order_id = new Guid(item_id);
                    Order order = unitOfWork.OrderRepository.GetById(order_id);
                    if (item["Remove"]?.ToString() == "True")
                        if(!order.IsConfirm)
                            order.IsDeleted = true;
                    UpdateOrder(order, item);
                }
            }
            unitOfWork.Commit();
        }

        private void CreateOrder(Guid ipd_id, JToken item)
        {
            Order order = new Order();
            order.VisitId = ipd_id;
            order.OrderType = Constant.IPD_ORDER;
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
                e.OrderType.Equals(Constant.IPD_ORDER) &&
                !e.IsConfirm
            );
            if (order != null)
                return true;
            return false;
        }
        private void CreateConfirmNotification(string from_user, IPD ipd)
        {
            var spec = ipd.Specialty;
            var customer = ipd.Customer;
            string primary_doctor_user_name = null;
            if (ipd.PrimaryDoctor != null)
                primary_doctor_user_name = ipd.PrimaryDoctor.Username;

            if (!string.IsNullOrEmpty(primary_doctor_user_name))
            {
                var vi_mes = string.Format(
                    "<b>[IPD - {0}]</b> Bạn có yêu cầu xác nhận <b>y lệnh miệng</b> cho bệnh nhân <b>{1}</b>",
                    spec?.ViName,
                    customer?.Fullname
                );
                var en_mes = string.Format(
                    "<b>[IPD - {0}]</b> Bạn có yêu cầu xác nhận <b>y lệnh miệng</b> cho bệnh nhân <b>{1}</b>",
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
                    visit_id: ipd.Id,
                    group_code: "IPD",
                    form_frontend: FormCode
                );
                noti_creator.Create();
            }
        }

        private string GetTextValue(IPD visit, string code)
        {
            List<MasterDataValue> datas_Part2 = null;
            var part2_Id = visit.IPDMedicalRecord?.IPDMedicalRecordPart2Id;
            if (part2_Id != null)
                datas_Part2 = unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                              .Where(d => !d.IsDeleted && d.IPDMedicalRecordPart2Id == part2_Id)
                              .Select(d => new MasterDataValue
                              {
                                  Code = d.Code,
                                  Value = d.Value
                              }).ToList();

            return getValueFromMasterDatas(code, datas_Part2);
        }
    }
}
