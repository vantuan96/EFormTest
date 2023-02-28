using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class VisitController : BaseApiController
    {
        [HttpGet]
        [Route("api/{type}/sync/ScreeningOfInfectiousDiseases/{id}")]
        [Permission(Code = "GROOM1")]
        public IHttpActionResult GetScreeningOfInfectiousDiseases(Guid id, string type = "ED")
        {
            var current_visit = GetVisit(id, type);

            List<VisitInfoModel> visits = GetVisitIn24H(current_visit.CustomerId, id, current_visit.AdmittedDate);

            foreach (VisitInfoModel visit in visits)
            {
                if (visit.VisitType == "ED")
                {
                    var ed = GetED(visit.Id);
                    if (ed.EmergencyTriageRecord != null) {
                        return Content(HttpStatusCode.OK, new
                        {
                            visit.Specialty,
                            visit.AdmittedDate,
                            Datas = ed.EmergencyTriageRecord.EmergencyTriageRecordDatas
                                    .Where(e => !e.IsDeleted)
                                    .Select(etrd => new { etrd.Id, etrd.Code, etrd.Value }).ToList(),
                        });
                    }
                }
                if (visit.VisitType == "OPD")
                {
                    var opd = GetOPD(visit.Id);
                    if (opd.OPDInitialAssessmentForShortTerm != null) {
                        return Content(HttpStatusCode.OK, new
                        {
                            visit.Specialty,
                            visit.AdmittedDate,
                            Datas = opd.OPDInitialAssessmentForShortTerm.OPDInitialAssessmentForShortTermDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                        });
                    }
                }
                if (visit.VisitType == "EOC")
                {
                    var form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.Id);
                    if (form != null)
                    {
                        return Content(HttpStatusCode.OK, new
                        {
                            visit.Specialty,
                            visit.AdmittedDate,
                            Datas = GetFormData(visit.Id, form.Id, "OPDIAFSTOP"),
                        });
                    }
                }
                if (visit.VisitType == "IPD")
                {
                    var ipd = GetIPD(visit.Id);
                    if (ipd.IPDInitialAssessmentForAdult != null)
                    {
                        return Content(HttpStatusCode.OK, new
                        {
                            visit.Specialty,
                            visit.AdmittedDate,
                            Datas = ipd.IPDInitialAssessmentForAdult.IPDInitialAssessmentForAdultDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList(),
                        });
                    }
                }
            }
            return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
        }
    }
}