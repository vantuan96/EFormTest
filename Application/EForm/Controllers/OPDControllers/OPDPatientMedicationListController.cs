using DataAccess.Models;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDPatientMedicationListController : BaseOPDApiController
    {
        [HttpGet]
        [Route("api/OPD/Order/ForShortTerm/{id}")]
        [Permission(Code = "OPAML1")]
        public IHttpActionResult GetOrderForShortTermAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                opd.RecordCode,
                OPDId = opd.Id,
                Datas = GetOrder(opd.Id),
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/Order/ForShortTerm/{id}")]
        [Permission(Code = "OPAML2")]
        public IHttpActionResult UpdateOrderForShortTermAPI(Guid id, [FromBody]JObject request)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();
            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDOEN", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            HandleUpdateOrCreateOrder(opd.Id, request["Datas"]);

            return Content(HttpStatusCode.OK, new
            {
                OPDId = opd.Id,
                Datas = GetOrder(opd.Id),
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/Order/ForShortTerm/Sync/{id}")]
        [Permission(Code = "OPAML3")]
        public IHttpActionResult SyncPatientMedicationListAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            if (opd.AdmittedDate == null)
                return Content(HttpStatusCode.BadRequest, Message.SYNC_NOT_FOUND);

            var data = GetLastestPatientMedicationList(opd.Id, opd.AdmittedDate, opd.CustomerId);
            if(data.Count < 1)
                return Content(HttpStatusCode.BadRequest, Message.SYNC_NOT_FOUND);

            return Content(HttpStatusCode.OK, new { OPDId = opd.Id, Datas = data });
        }

 
        private void CreateOrder(Guid opd_id, JToken item)
        {
            Order order = new Order();
            order.VisitId = opd_id;
            order.OrderType = Constant.OPD_PATIENT_MEDICATION_LIST;
            order.Drug = item["Drug"]?.ToString();
            order.Dosage = item["Dosage"]?.ToString();
            order.Route = item["Route"]?.ToString();
            var last_dose_date = item["LastDoseDate"]?.ToString();
            if (last_dose_date != null && !string.IsNullOrEmpty(last_dose_date))
                order.LastDoseDate = DateTime.ParseExact(item["LastDoseDate"]?.ToString(), Constant.DATE_FORMAT, null);
            else
                order.LastDoseDate = null;
            unitOfWork.OrderRepository.Add(order);
        }

        private void UpdateOrder(Order order, JToken item)
        {
            var old = new
            {
                order.Drug,
                order.Dosage,
                order.Route,
                LastDoseDate = order.LastDoseDate?.ToString(Constant.DATE_FORMAT),
            };
            var _new = new
            {
                Drug = item["Drug"]?.ToString(),
                Dosage = item["Dosage"]?.ToString(),
                Route = item["Route"]?.ToString(),
                LastDoseDate = item["LastDoseDate"]?.ToString(),
            };

            if (JsonConvert.SerializeObject(old) != JsonConvert.SerializeObject(_new))
            {
                order.Drug = _new.Drug;
                order.Dosage = _new.Dosage;
                order.Route = _new.Route;
                if (!string.IsNullOrEmpty(_new.LastDoseDate))
                    order.LastDoseDate = DateTime.ParseExact(_new.LastDoseDate, Constant.DATE_FORMAT, null);
                else
                    order.LastDoseDate = null;
                unitOfWork.OrderRepository.Update(order);
            }
        }

        private void HandleUpdateOrCreateOrder(Guid opd_id, JToken request_datas)
        {
            var user = GetUser();
            foreach (var item in request_datas)
            {
                string item_id = item["Id"]?.ToString();
                if (string.IsNullOrEmpty(item_id))
                {
                    CreateOrder(opd_id, item);
                }
                else
                {
                    Guid order_id = new Guid(item_id);
                    Order order = unitOfWork.OrderRepository.GetById(order_id);
                    if(IsUserCreateFormManual(user.Username, order.CreatedBy))
                        UpdateOrder(order, item);
                }
            }
            unitOfWork.Commit();
        }

        private dynamic GetOrder(Guid opd_id)
        {
            return unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == opd_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.OPD_PATIENT_MEDICATION_LIST)
            )
            .OrderBy(o => o.CreatedAt).Select(o => new
            {
                o.Id,
                o.Drug,
                o.Dosage,
                o.Route,
                LastDoseDate = o.LastDoseDate?.ToString(Constant.DATE_FORMAT),
            }).ToList();
        }
    }
}
