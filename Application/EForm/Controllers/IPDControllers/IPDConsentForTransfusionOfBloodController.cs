using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EMRModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDConsentForTransfusionOfBloodController : BaseApiController
    {
        [HttpPost]
        [Route("api/ConsentForTransfusionOfBlood/IPD/create/A01_006_080721_V/{visitId}")]
        [Permission(Code = "IPDCFTOB01")]
        public IHttpActionResult CreateAPI(Guid visitId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, "A01_006_080721_V");
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new EIOForm
            {
                VisitId = visitId,
                Version = 1,
                FormCode = "A01_006_080721_V",
                VisitTypeGroupCode = "IPD"
            };
            unitOfWork.EIOFormRepository.Add(form_data);

            UpdateVisit(visit, "IPD");

            return Content(HttpStatusCode.OK, new { form_data.Id });
        }

        [HttpGet]
        [Route("api/ConsentForTransfusionOfBlood/IPD/A01_006_080721_V/{visitId}")]
        //[CSRFCheck]
        [Permission(Code = "IPDCFTOB02")]
        public IHttpActionResult GetAPI(Guid visitId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, "A01_006_080721_V");
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            return Content(HttpStatusCode.OK, FormatOutput(form, visit));
        }


        [HttpPost]
        [Route("api/ConsentForTransfusionOfBlood/IPD/update/A01_006_080721_V/{visitId}")]
        //[CSRFCheck]
        [Permission(Code = "IPDCFTOB03")]
        public IHttpActionResult UpdateAPI(Guid visitId, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, "A01_006_080721_V");
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (form.ConfirmBy != null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, "A01_006_080721_V", request["Datas"]);

            form.Note = request["Note"]?.ToString();
            form.Comment = request["Comment"]?.ToString();
            unitOfWork.EIOFormRepository.Update(form);

            UpdateVisit(visit, "IPD");

            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [HttpGet]
        [Route("api/ConsentForTransfusionOfBlood/IPD/GetInfo/{visitId}")]
        public IHttpActionResult IPDGetGetInfo(Guid visitId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.IPD_NOT_FOUND);
            var CheckFormLocked = IPDIsBlock(visit, "A01_006_080721_V");
            return Content(HttpStatusCode.OK, CheckFormLocked);
        }

        private dynamic FormatOutput(EIOForm fprm, dynamic visit)
        {
            List<MasterDataValue> datas_Part2 = null;
            IPD ipd = (IPD)visit;
            var part2_Id = ipd.IPDMedicalRecord?.IPDMedicalRecordPart2Id;
            if (part2_Id != null)
                datas_Part2 = unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                              .Where(d => !d.IsDeleted && d.IPDMedicalRecordPart2Id == part2_Id)
                              .Select(d => new MasterDataValue
                              {
                                  Code = d.Code,
                                  Value = d.Value
                              }).ToList();
            var fullnameCreatedby = "";
            if (fprm.CreatedBy.Count()> 1)
            {
                var user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == fprm.CreatedBy);
                if(user != null)
                {
                    fullnameCreatedby = user.Fullname;
                }
            }
            return new
            {
                fprm.Id,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, "A01_006_080721_V"),
                fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = fprm.Id,
                fprm.Note,
                fprm.Comment,
                UpdatedAt = fprm.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConfirmInfos = GetFormConfirms(fprm.Id),
                ICD10 = getValueFromMasterDatas("IPDMRPTICDCANS", datas_Part2),
                ChanDoanBenhChinh = getValueFromMasterDatas("IPDMRPTCDBCANS", datas_Part2),
                ICDOptions = getValueFromMasterDatas("IPDMRPTICDPANS", datas_Part2),
                ChanDoanBenhKemTheo = getValueFromMasterDatas("IPDMRPTCDKTANS", datas_Part2),
                IsFormLocked = IPDIsBlock(ipd, "A01_006_080721_V"),
                Specialty = ipd.Specialty,
                FullNameCreatedBy = fullnameCreatedby,
                CustomerFullName = ipd.Customer.Fullname
            };

        }
    }
}
