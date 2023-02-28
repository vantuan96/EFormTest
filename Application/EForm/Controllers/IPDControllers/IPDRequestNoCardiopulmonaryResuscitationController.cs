using DataAccess.Models.IPDModel;
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

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDRequestNoCardiopulmonaryResuscitationController : EIORequestNoCardiopulmonaryResuscitationController
    {
        private const string visitType = "IPD";

        [HttpGet]
        [Route("api/IPD/RequestNoCardiopulmonaryResuscitation/Detailt/{visitId}")]
        [Permission(Code = "IPDVIEWKHSTP")]
        public IHttpActionResult IPDViewDetailtForm(Guid visitId)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            bool isLocked = IPDIsBlock(visit, formCode);
            var form = GetFormByVisitId(visit.Id, visitType);
            if (form == null)
                return Content(HttpStatusCode.NotFound, new { IsLocked = isLocked, Message.FORM_NOT_FOUND });

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
                IsLocked = isLocked,
                Diagnosis = GetAndFormatDiagnosis(visit.Id, visitType),
                form.Id
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/RequestNoCardiopulmonaryResuscitation/Create/{visitId}")]
        [Permission(Code ="IPDCREATEKHSTP")]
        public IHttpActionResult IPDCreatedForm(Guid visitId)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetFormByVisitId(visit.Id, visitType);
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            if (IPDIsBlock(visit, formCode))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            CreateForm(visit.Id, visitType);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/RequestNoCardiopulmonaryResuscitation/Update/{visitId}")]
        [Permission(Code ="IPDUPDATEKHSTP")]
        public IHttpActionResult IPDUpdateForm(Guid visitId, [FromBody] JObject req)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetFormByVisitId(visit.Id, visitType);
            if (form == null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            if (IPDIsBlock(visit, formCode))
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);

            CreateOrUpdateDatasForm(form, req);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
