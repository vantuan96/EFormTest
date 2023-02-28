using DataAccess.Models.EOCModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;

using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDVitalSignController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/VitalSign/{id}")]
        [Permission(Code = "EVISI1")]
        public IHttpActionResult GetOrderAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            //var user = GetUser();
            //var position = user.UserPosition;
            //if (position == null)
            //    return Content(HttpStatusCode.NotFound, Message.POSITION_NOT_FOUND);

            //var is_waiting_accept_error_message = GetWaitingAcceptMessageByPosition(ed, position.EnName);
            //if (is_waiting_accept_error_message != null)
            //    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);

            var etr = ed.EmergencyTriageRecord;
            if (etr == null || etr.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_ETR_NOT_FOUND);

            var oc = ed.ObservationChart;
            if (etr == null || etr.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_OC0_NOT_FOUND);

            try
            {
                var etr_data = etr.EmergencyTriageRecordDatas.Where(e => !e.IsDeleted).ToList();
                var oc_data = oc.ObservationChartDatas.Where(e => !e.IsDeleted).OrderByDescending(e => e.NoteAt).Take(1).ToList()[0];
                return Content(HttpStatusCode.OK, new
                {
                    Temparature = oc_data?.Temperature,
                    oc_data?.Pulse,
                    RR = oc_data?.Resp,
                    oc_data?.SysBP,
                    oc_data?.DiaBP,
                    BloodGroup = "",
                    Height = etr_data.FirstOrDefault(e => e.Code == "ETRHEIANS")?.Value,
                    Weight = etr_data.FirstOrDefault(e => e.Code == "ETRWEIANS")?.Value,
                    OT = "",
                });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, Message.ED_OC0_NOT_FOUND);
            }
        }
        [HttpGet]
        [Route("api/IPD/VitalSign/{id}")]
        [Permission(Code = "EVISI1")]
        public IHttpActionResult GetIPDOrderAPI(Guid id)
        {
            var visit = GetIPD(id);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var etr = visit.IPDInitialAssessmentForAdult;
            if (etr == null || etr.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.DATA_NOT_FOUND);

            try
            {
                var etr_data = etr.IPDInitialAssessmentForAdultDatas.Where(e => !e.IsDeleted).ToList();
                return Content(HttpStatusCode.OK, new
                {
                    DiaBP = "",
                    BloodGroup = "",

                    SysBP = etr_data.FirstOrDefault(e => e.Code == "IPDIAAUBLPRANS")?.Value,
                    RR = etr_data.FirstOrDefault(e => e.Code == "IPDIAAURERAANS")?.Value,
                    Pulse = etr_data.FirstOrDefault(e => e.Code == "IPDIAAUPULSANS")?.Value,
                    Temparature = etr_data.FirstOrDefault(e => e.Code == "IPDIAAUTEMPANS")?.Value,
                    Height = etr_data.FirstOrDefault(e => e.Code == "IPDIAAUHEIGANS")?.Value,
                    Weight = etr_data.FirstOrDefault(e => e.Code == "IPDIAAUWEIGANS")?.Value,

                    OT = "",
                });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, Message.DATA_NOT_FOUND);
            }
        }
        [HttpGet]
        [Route("api/OPD/VitalSign/{id}")]
        [Permission(Code = "EVISI1")]
        public IHttpActionResult GetOPDOrderAPI(Guid id)
        {
            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            //var user = GetUser();
            //var position = user.UserPosition;
            //if (position == null)
            //    return Content(HttpStatusCode.NotFound, Message.POSITION_NOT_FOUND);

            //var is_waiting_accept_error_message = GetWaitingAcceptMessageByPosition(ed, position.EnName);
            //if (is_waiting_accept_error_message != null)
            //    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);

            var ForShortTerm = opd.OPDInitialAssessmentForShortTerm;
            if (ForShortTerm == null || ForShortTerm.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_ETR_NOT_FOUND);

            try
            {
                var etr_data = ForShortTerm.OPDInitialAssessmentForShortTermDatas.Where(e => !e.IsDeleted).ToList();
                return Content(HttpStatusCode.OK, new
                {
                    DiaBP = "",
                    BloodGroup = "",

                    SysBP = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPBP0ANS")?.Value,
                    RR = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPRR0ANS")?.Value,
                    Pulse = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPPULANS")?.Value,
                    Temparature = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPTEMANS")?.Value,
                    Height = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPHEIANS")?.Value,
                    Weight = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPWEIANS")?.Value,

                    OT = "",
                });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, Message.ED_OC0_NOT_FOUND);
            }
        }
        [HttpGet]
        [Route("api/EOC/VitalSign/{id}")]
        [Permission(Code = "EVISI1")]
        public IHttpActionResult GetEOCOrderAPI(Guid id)
        {
            var visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            //var user = GetUser();
            //var position = user.UserPosition;
            //if (position == null)
            //    return Content(HttpStatusCode.NotFound, Message.POSITION_NOT_FOUND);

            //var is_waiting_accept_error_message = GetWaitingAcceptMessageByPosition(ed, position.EnName);
            //if (is_waiting_accept_error_message != null)
            //    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);

            var form = GetForm(id);
            if (form == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            try
            {
                var etr_data = GetFormData(id, form.Id, "OPDIAFSTOP");
                return Content(HttpStatusCode.OK, new
                {
                    DiaBP = "",
                    BloodGroup = "",

                    SysBP = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPBP0ANS")?.Value,
                    RR = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPRR0ANS")?.Value,
                    Pulse = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPPULANS")?.Value,
                    Temparature = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPTEMANS")?.Value,
                    Height = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPHEIANS")?.Value,
                    Weight = etr_data.FirstOrDefault(e => e.Code == "OPDIAFSTOPWEIANS")?.Value,

                    OT = "",
                });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotFound, Message.ED_OC0_NOT_FOUND);
            }
        }
        private EOCInitialAssessmentForShortTerm GetForm(Guid VisitId)
        {
            var form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
    }
    
}
