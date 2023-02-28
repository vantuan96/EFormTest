using DataAccess.Models;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class PatientMedicationListController : BaseApiController
    {
        [HttpGet]
        [Route("api/PatientMedicationList/{id}")]
        [Permission(Code = "GPAML1")]
        public IHttpActionResult GetPatientMedicationListAPI(Guid id, string VisitTypeGroupCode, string Type)
        {
            dynamic visit = null;
            if (VisitTypeGroupCode == "ED")
                visit = GetED(id);
            else if(VisitTypeGroupCode == "OPD")
                visit = GetOPD(id);
            else if (VisitTypeGroupCode == "IPD")
                visit = GetIPD(id);
            else if (VisitTypeGroupCode == "EOC")
                visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            
            return Content(HttpStatusCode.OK, new { 
                visit.Id,
                VisitTypeGroupCode,
                Type,
                Datas = GetPatientMedicationList(visit.Id, Type)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/PatientMedicationList/{id}")]
        [Permission(Code = "GPAML2")]
        public IHttpActionResult UpdatePatientMedicationListAPI(Guid id, [FromBody]JObject request)
        {
            var visit_type = request["VisitTypeGroupCode"]?.ToString();
            var type = request["Type"]?.ToString();

            dynamic visit = null;
            if (visit_type == "ED")
                visit = GetED(id);
            else if (visit_type == "OPD")
                visit = GetOPD(id);
            else if (visit_type == "IPD")
                visit = GetIPD(id);
            else if (visit_type == "EOC")
                visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDETR", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            HandleUpdateOrCreateOrder(visit.Id, type, request["Datas"]);

            return Content(HttpStatusCode.OK, new
            {
                visit.Id,
                VisitTypeGroupCode = visit_type,
                Type = type,
                Datas = GetPatientMedicationList(visit.Id, type),
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/PatientMedicationList/Sync/{id}")]
        [Permission(Code = "GPAML3")]
        public IHttpActionResult SyncPatientMedicationListAPI(Guid id, [FromBody]JObject request)
        {
            var visit_type = request["VisitTypeGroupCode"]?.ToString();

            dynamic visit = null;
            if (visit_type == "ED")
                visit = GetED(id);
            else if (visit_type == "OPD")
                visit = GetOPD(id);
            else if (visit_type == "IPD")
                visit = GetIPD(id);
            else if (visit_type == "EOC")
                visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var data = GetAllPatientMedicationList(visit.CustomerId, visit.AdmittedDate);

            return Content(HttpStatusCode.OK, new { visit.Id, Datas = data });
        }
        private dynamic GetAllPatientMedicationList(Guid? customer_id, DateTime admitted_date)
        {
            var visit_ids = new List<Guid>();
            var ed_ids = unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate < admitted_date
            ).Select(e => e.Id).ToList();
            visit_ids.AddRange(ed_ids);

            var visit_status = new VisitStatus(unitOfWork, "OPD");
            var no_examination = visit_status.GetNoExaminationStatus();
            var opd_ids = unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.EDStatusId != null &&
                e.EDStatusId != no_examination.Id &&
                e.AdmittedDate != null &&
                e.AdmittedDate < admitted_date
            ).Select(e => e.Id).ToList();
            visit_ids.AddRange(opd_ids);

            var ipd_ids = unitOfWork.IPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate < admitted_date
            ).Select(e => e.Id).ToList();
            visit_ids.AddRange(ipd_ids);

            var un_distinct_order = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                visit_ids.Contains((Guid)i.VisitId)
            )
            .OrderByDescending(o => o.LastDoseDate).Select(o => new PatientMedicationListModel
            {
                Drug = o.Drug,
                Dosage = o.Dosage,
                Route = o.Route,
                LastDoseDate = o.LastDoseDate?.ToString(Constant.DATE_FORMAT),
            }).Distinct().ToList();

            var distinct_order = new List<PatientMedicationListModel>();

            foreach(var order in un_distinct_order)
            {
                var i = distinct_order.FirstOrDefault(
                    e => (e.Drug == order.Drug || (string.IsNullOrEmpty(e.Drug) && string.IsNullOrEmpty(order.Drug)))&&
                    (e.Dosage == order.Dosage || (string.IsNullOrEmpty(e.Dosage) && string.IsNullOrEmpty(order.Dosage))) &&
                    (e.Route == order.Route || (string.IsNullOrEmpty(e.Route) && string.IsNullOrEmpty(order.Route)))
                );
                if (i != null) continue;

                distinct_order.Add(order);
            }
            return distinct_order;
        }

        

    }
}
