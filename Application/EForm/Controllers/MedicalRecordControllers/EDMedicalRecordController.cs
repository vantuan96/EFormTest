using DataAccess.Models.EDModel;
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
    public class EDMedicalRecordController : BaseMedicalRecodeApiController
    {
        [HttpGet]
        [Route("api/MedicalRecord/ED/{id}")]
        [Permission(Code = "MEDMR1")]
        public IHttpActionResult GetEDMedicalRecordsAPI(Guid id)
        {
            var ed = unitOfWork.EDRepository.GetById(id);
            if (ed == null)
                return Content(HttpStatusCode.BadRequest, Message.ED_NOT_FOUND);

            var forms = GetFormsED(ed);
            return Content(HttpStatusCode.OK, forms);
        }
       
    }
}
