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
    public class OPDMedicalRecordController : BaseMedicalRecodeApiController
    {
        [HttpGet]
        [Route("api/MedicalRecord/OPD/{id}")]
        [Permission(Code = "MODMR1")]
        public IHttpActionResult GetOPDMedicalRecordsAPI(Guid id)
        {
            var opd = unitOfWork.OPDRepository.GetById(id);
            if (opd == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);

            var forms = GetFormsOPD(opd);
            return Content(HttpStatusCode.OK, forms);
        }
    }
}
