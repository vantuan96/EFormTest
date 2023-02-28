using DataAccess.Models.EOCModel;
using DataAccess.Repository;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
namespace EForm.Controllers.EOCControllers
{
    [SessionAuthorize]
    public class EOCFallRiskScreeningController : BaseEOCApiController
    {
        [HttpGet]
        [Route("api/eoc/InitialAssessment/FallRiskScreening/{type}/{visitId}")]
        [Permission(Code = "EOC014")]
        public IHttpActionResult GetFallRiskScreenings(Guid visitId)
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var forms = unitOfWork.EOCFallRiskScreeningRepository.Find(e => !e.IsDeleted && e.VisitId == visitId).OrderBy(o => o.CreatedAt).ToList().Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                form.CreatedAt,
                form.UpdatedAt,
                form.UpdatedBy,
                form.Version
            }).ToList();
            
            return Content(HttpStatusCode.OK, new
            {
                Datas = forms,
            });
        }

        [HttpGet]
        [Route("api/eoc/InitialAssessment/FallRiskScreening/{type}/{visitId}/{id}")]
        [Permission(Code = "EOC014")]
        public IHttpActionResult Get(Guid visitId,Guid id,string type = "A02_007_220321_VE")
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            var form = GetForm(visitId,id);
            if (form == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, FormatOutput(type,visit, form));
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/FallRiskScreening/Create/{type}/{visitId}")]
        [Permission(Code = "EOC015")]
        public IHttpActionResult Post(Guid visitId,string type = "A02_007_220321_VE")
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            //var form = GetForm(id);
            //if (form != null) return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);
            var form_data = new EOCFallRiskScreening
            {
                VisitId = visitId,
                Version = visit.Version >= 7 ? visit.Version : 2
            };
            unitOfWork.EOCFallRiskScreeningRepository.Add(form_data);
            UpdateVisit(visit);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt,
                form_data.Version
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/InitialAssessment/FallRiskScreening/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "EOC016")]
        public IHttpActionResult Update(Guid visitId, Guid id, [FromBody]JObject request,string type = "A02_007_220321_VE")
        {
            var visit = GetEOC(visitId);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var form = GetForm(visitId,id);
            if (form == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            HandleUpdateOrCreateFormDatas(visitId, form.Id, type, request["Datas"]);

            unitOfWork.EOCFallRiskScreeningRepository.Update(form);
            unitOfWork.Commit();
            var lastFallRick = unitOfWork.EOCFallRiskScreeningRepository.Find(x => !x.IsDeleted && x.VisitId == visit.Id).ToList().OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if (lastFallRick.Id == id)
            {
                if (lastFallRick.Version == 1)
                {
                    visit.IsHasFallRiskScreening = unitOfWork.FormDatasRepository.Count(
                          e => e.FormId == id && !e.IsDeleted &&
                          e.FormCode == type &&
                          e.VisitType == "EOC" &&
                          new List<string> { "OPDFRSFOPDPHANS1", "OPDFRSFOPDPHANS2", "OPDFRSFOPDPHANS3", "OPDFRSFOPDPNANS1", "OPDFRSFOPDPNANS2", "OPDFRSFOPDPNANS3" }.Contains(e.Code) &&
                          e.Value == "1") > 0;
                }
                else
                {
                    visit.IsHasFallRiskScreening = unitOfWork.FormDatasRepository.Count(e => e.Value == "1" && e.FormId == id && !e.IsDeleted && e.FormCode == type && e.VisitType == "EOC" && e.Code == "OPDFRSFOPUTHANS1") > 0;
                }
            }
            UpdateVisit(visit);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new {
                form.Id,
                form.VisitId,
                form.UpdatedAt,
                form.UpdatedBy,
                form.Version
            });
        }
        private EOCFallRiskScreening GetForm(Guid visitId, Guid id)
        {
            var form = unitOfWork.EOCFallRiskScreeningRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == id);
            return form;
        }
        private dynamic FormatOutput(string type,EOC visit, EOCFallRiskScreening fprm)
        {
            var eocOlds = unitOfWork.EOCRepository.Find(x => x.CustomerId == visit.CustomerId && !x.IsDeleted).OrderByDescending(x => x.AdmittedDate).ToList();
            var specialty = string.Empty;
            if (eocOlds.Count > 0)
            {
                specialty = eocOlds[0].Specialty.ViName;
            }
            return new
            {
                fprm.Id,
                IsNew = fprm.CreatedAt != fprm.UpdatedAt,
                Datas = GetFormData(visit.Id, fprm.Id, type),
                fprm.CreatedAt,
                fprm.CreatedBy,
                fprm.UpdatedAt,
                fprm.UpdatedBy,
                VisitId = visit.Id,
                PrimaryDoctor = new
                {
                    visit.PrimaryDoctorId,
                    visit.PrimaryDoctor?.Username
                },
                fprm.Version,
                Specialty = specialty
            };
        }
    }
}