using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models;
using EForm.Models.EDModels;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDCareNoteController : EIOCareNoteController
    {
        private const string formCode = "A02_062_050919_V";
        private const string timeUpdate_version2 = "UPDATE_CARENOTE_VESION2";

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CareNote/Create/{id}")]
        [Permission(Code = "IPDPCC01")]
        public IHttpActionResult CreateIPDCareNoteAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            EIOConstraintNewbornAndPregnantWoman form = null;
            if (CheckVisitAndSetUp(ipd))
            {
                var formId_param = request["FormIdNewborn"]?.ToString();
                Guid? formId = null;
                if (!string.IsNullOrEmpty(formId_param))
                    formId = new Guid(formId_param);
                form = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
            }

            var phy = CreateEIOCareNote(ipd.Id, "IPD", request, form?.Id);
            return Content(HttpStatusCode.OK, new { phy.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CareNote/Update/{id}")]
        [Permission(Code = "IPDPCC02")]
        public IHttpActionResult UpdateIPDCareNoteAPI(Guid id, [FromBody] JObject request)
        {
            var note = GetEIOCareNote(id);
            if (note == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CN_NOT_FOUND);

            var user = GetUser();
            if (user.Username != note.CreatedBy)
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);

            var ipd = GetIPD((Guid)note.VisitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            EIOConstraintNewbornAndPregnantWoman form = null;
            if (CheckVisitAndSetUp(ipd))
            {
                var formId_param = request["FormIdNewborn"]?.ToString();
                Guid? formId = null;
                if (!string.IsNullOrEmpty(formId_param))
                    formId = new Guid(formId_param);
                form = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
            }

            var phy = UpdateEIOCareNote(id, request, form?.Id);
            return Content(HttpStatusCode.OK, new { phy.Id });
        }

        [HttpGet]
        [Route("api/IPD/CareNote/List/{id}")]
        [Permission(Code = "IPDPCC03")]
        public IHttpActionResult ListIPDCareNoteAPI(Guid id, [FromUri] EDParameterModel Parameter)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var list = GetListEIOCareNote(id, "IPD", Parameter.StartAt, Parameter.EndAt, Parameter.CreatedBy, Parameter.Sort, Parameter.FormIdNewborn);

            dynamic lastUpdate = (from e in unitOfWork.EIOCareNoteRepository.AsQueryable()
                                  where !e.IsDeleted && e.VisitId == id && e.FormId == Parameter.FormIdNewborn
                                  orderby e.UpdatedAt descending
                                  select e).FirstOrDefault();

            EIOConstraintNewbornAndPregnantWoman newbornForm = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == Parameter.FormIdNewborn);
            if (lastUpdate == null) // log tạo tab khi chưa có chăm sóc nào
                lastUpdate = newbornForm;


            DiagnosisAndICDModel diagnosis = new DiagnosisAndICDModel();
            if (Parameter.FormIdNewborn == null)
                diagnosis = GetVisitDiagnosisAndICD(ipd.Id, "IPD", false);

            var condition_version2 = CheckVisitAndSetUp(ipd);
            return Ok(new
            {
                ipd.Customer.AgeFormated,
                ipd.RecordCode,
                EDId = ipd.Id,
                Datas = list,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuChamSoc),
                UpdatedAt = lastUpdate?.UpdatedAt,
                UpdatedBy = lastUpdate?.UpdatedBy,
                Diagnosis2 = diagnosis,
                ConditionVersion2 = condition_version2,
                NewbornCustomer = new
                {
                    PID = newbornForm?.NewbornCustomer?.PID,
                    Address = newbornForm?.NewbornCustomer?.Address,
                    Gender = newbornForm?.NewbornCustomer?.Gender,
                    Fullname = newbornForm?.NewbornCustomer?.Fullname,
                    Id = newbornForm?.NewbornCustomer?.Id,
                    AgeFormated = newbornForm?.NewbornCustomer?.AgeFormated,
                    DateOfBirth = newbornForm?.NewbornCustomer?.DateOfBirth
                },
                ipd.Version,                
            });
        }

        [HttpGet]
        [Route("api/IPD/CareNote/Infor/{visitId}")]
        public IHttpActionResult GetVersion(Guid visitId)
        {
            var visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            bool condition = CheckVisitAndSetUp(visit);

            return Content(HttpStatusCode.OK, new { ConditionVersion2 = condition });
        }

        private bool CheckVisitAndSetUp(IPD visit)
        {
            var isVisitLastupdate = IsVisitLastTimeUpdate(visit, timeUpdate_version2);
            if (!isVisitLastupdate)
                return false;

            var specialty_id = visit.SpecialtyId;
            var setup = unitOfWork.IPDSetupMedicalRecordRepository.FirstOrDefault(e => !e.IsDeleted && e.SpecialityId == specialty_id && e.Formcode == "A01_035_050919_V" && e.FormType == "MedicalRecords" && e.IsDeploy == true);
            if (setup == null)
            {
                var check_form = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.Id && e.FormCode == formCode);
                if (check_form == null)
                    return false;

                return true;
            }

            return true;
        }
    }
}
