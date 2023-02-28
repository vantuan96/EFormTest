using DataAccess.Models.EOCModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.MedicalRecordModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.MedicalRecordControllers
{
    [SessionAuthorize]
    public class EOCMedicalRecordController : BaseMedicalRecodeApiController
    {
        [HttpGet]
        [Route("api/MedicalRecord/EOC/{id}")]
        [Permission(Code = "MODMR1")]
        public IHttpActionResult GetOPDMedicalRecordsAPI(Guid id)
        {
            EOC visit = GetEOC(id);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);

            var forms = GetFormsEOC(visit);
            return Content(HttpStatusCode.OK, forms);
        }
       
    }
}
