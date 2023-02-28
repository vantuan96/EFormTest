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

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDPatientMedicationListController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/PatientMedicationList/{id}")]
        [Permission(Code = "EPAML1")]
        public IHttpActionResult GetPatientMedicationListAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                EdId = ed.Id,
                ed.RecordCode,
                Datas = GetOrder(ed.Id),
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/PatientMedicationList/{id}")]
        [Permission(Code = "EPAML2")]
        public IHttpActionResult UpdatePatientMedicationListAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDETR", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            
            HandleUpdateOrCreateOrder(ed.Id, request["Datas"]);

            return Content(HttpStatusCode.OK, new
            {
                EDId = ed.Id,
                Datas = GetOrder(ed.Id),
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/PatientMedicationList/Sync/{id}")]
        [Permission(Code = "EPAML3")]
        public IHttpActionResult SyncPatientMedicationListAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            if (ed.AdmittedDate == null)
                return Content(HttpStatusCode.BadRequest, Message.SYNC_NOT_FOUND);

            var data = GetLastestPatientMedicationList(ed.Id, ed.AdmittedDate, ed.CustomerId);
            if (data.Count < 1)
                return Content(HttpStatusCode.BadRequest, Message.SYNC_NOT_FOUND);
            return Content(HttpStatusCode.OK, new { EdId = ed.Id, Datas = data });
        }

        private void CreateOrder(Guid ed_id, JToken item)
        {
            Order order = new Order();
            order.VisitId = ed_id;
            order.OrderType = Constant.ED_PATIENT_MEDICATION_LIST;
            order.Drug = item["Drug"]?.ToString();
            order.Dosage = item["Dosage"]?.ToString();
            order.Route = item["Route"]?.ToString();
            var last_dose_date = item["LastDoseDate"]?.ToString();
            if (!string.IsNullOrEmpty(last_dose_date))
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
                    UpdateOrder(order, item);
                }
            }
            unitOfWork.Commit();
        }

        private dynamic GetOrder(Guid ed_id)
        {
            return unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == ed_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.ED_PATIENT_MEDICATION_LIST)
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
