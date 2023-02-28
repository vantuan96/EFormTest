using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDRequestNoCardiopulmonaryResuscitationController : EIORequestNoCardiopulmonaryResuscitationController
    {
        private const string visitType = "ED";

        [HttpGet]
        [Route("api/ED/RequestNoCardiopulmonaryResuscitation/Detailt/{visitId}")]
        [Permission(Code = "EDVIEWKHSTP")]
        public IHttpActionResult EDViewDetailtForm(Guid visitId)
        {
            ED visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetFormByVisitId(visit.Id, visitType);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var datas = GetListDatasForm(form);
            var specialty = visit.Specialty;
            var custormer = visit.Customer;
            var doctor = GetUserByUsername(form.CreatedBy);

            return Content(HttpStatusCode.OK, new
            {
                FullNameDoctor = doctor?.Fullname,
                CreatedBy = form.CreatedBy,
                CreateAt = form.CreatedAt,
                UpdatedBy = form.UpdatedBy,
                UpdatedAt = form.UpdatedAt,
                Specialty = new { ViName = specialty?.ViName, EnName = specialty?.EnName },
                Custormer = custormer?.Fullname,
                IsCustormer = datas.FirstOrDefault(e => e.Code.ToUpper() == "IsCustormer".ToUpper())?.Value,
                NameOfFamily = datas.FirstOrDefault(e => e.Code.ToUpper() == "NameOfFamily".ToUpper())?.Value,
                DateTimeOfPatient = datas.FirstOrDefault(e => e.Code.ToUpper() == "DateTimeOfPatient".ToUpper())?.Value,
                DateTimeOfPhysician = datas.FirstOrDefault(e => e.Code.ToUpper() == "DateTimeOfPhysician".ToUpper())?.Value,
                Picture = datas.FirstOrDefault(e => e.Code.ToUpper() == "Picture".ToUpper())?.Value,
                Picture1 = datas.FirstOrDefault(e => e.Code.ToUpper() == "Picture1".ToUpper())?.Value,
                Diagnosis = GetAndFormatDiagnosis(visit.Id, visitType, visit),
                form.Id
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/RequestNoCardiopulmonaryResuscitation/Create/{visitId}")]
        [Permission(Code = "EDCREATEkHSTP")]
        public IHttpActionResult EDCreatedForm(Guid visitId)
        {
            ED visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetFormByVisitId(visit.Id, visitType);
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            CreateForm(visit.Id, visitType);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/RequestNoCardiopulmonaryResuscitation/Update/{visitId}")]
        [Permission(Code = "EDUPDATEKHSTP")]
        public IHttpActionResult EDUpdateForm(Guid visitId, [FromBody] JObject req)
        {
            ED visit = GetED(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetFormByVisitId(visit.Id, visitType);
            if (form == null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            CreateOrUpdateDatasForm(form, req);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
