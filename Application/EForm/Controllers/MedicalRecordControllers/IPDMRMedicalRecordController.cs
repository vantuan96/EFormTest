using DataAccess.Models.IPDModel;
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
    public class IPDMRMedicalRecordController : BaseMedicalRecodeApiController
    {
        [HttpGet]
        [Route("api/MedicalRecord/IPD/{id}")]
        [Permission(Code = "MIDMR1")]
        public IHttpActionResult GetIPDMedicalRecordsAPI(Guid id)
        {
            var ipd = unitOfWork.IPDRepository.GetById(id);
            if (ipd == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var forms = GetFormsIPD(ipd);
            return Content(HttpStatusCode.OK, forms);
        }
    }
}
