using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
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
    public class EDPatientOwnMedicationsChartController : EIOPatientOwnMedicationsChartController
    {

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/PatientOwnMedicationsChart/Create/{id}")]
        [Permission(Code = "EPOMC1")]
        public IHttpActionResult CreatePatientOwnMedicationsChartAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var med_chart = GetEIOPatientOwnMedicationsChart(ed.Id, "ED");
            if (med_chart != null)
                return Content(HttpStatusCode.NotFound, Message.ED_POMC_EXIST);

            CreateEIOPatientOwnMedicationsChart(ed.Id, "ED");
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/PatientOwnMedicationsChart/{id}")]
        [Permission(Code = "EPOMC2")]
        public IHttpActionResult GetPatientOwnMedicationsChartAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var med_chart = GetEIOPatientOwnMedicationsChart(ed.Id, "ED");
            if (med_chart == null)
                return Content(HttpStatusCode.NotFound, Message.ED_POMC_NOT_FOUND);

            return Content(HttpStatusCode.OK, GetPatientOwnMedicationsChartResult(med_chart, ed.Customer));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/PatientOwnMedicationsChart/{id}")]
        [Permission(Code = "EPOMC3")]
        public IHttpActionResult UpdatePatientOwnMedicationsChartAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var med_chart = GetEIOPatientOwnMedicationsChart(ed.Id, "ED");
            if (med_chart == null)
                return Content(HttpStatusCode.NotFound, Message.ED_POMC_NOT_FOUND);

            HandleUpdateOrCreatePatientOwnMedicationsChart(med_chart, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/PatientOwnMedicationsChart/Confirm/{id}")]
        [Permission(Code = "EPOMC4")]
        public IHttpActionResult ConfirmPatientOwnMedicationsChartAPI(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var med_chart = GetEIOPatientOwnMedicationsChart(ed.Id, "ED");
            if (med_chart == null)
                return Content(HttpStatusCode.NotFound, Message.ED_POMC_NOT_FOUND);

            var error = ConfirmPatientOwnMedicationsChart(med_chart, request);
            if (error != null)
                return Content(HttpStatusCode.NotFound, error);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
    }
}
