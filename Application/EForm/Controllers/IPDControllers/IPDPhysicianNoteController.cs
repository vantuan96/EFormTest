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
using EForm.Models.EDModels;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPhysicianNoteController : EIOPhysicianNoteController
    {
        private const string formCode = "A01_066_050919_VE";
        private const string timeUpdate_version2 = "UPDATE_PHYSICIAN_VESION2";

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PhysicianNote/Create/{id}")]
        [Permission(Code = "IPDPDT01")]
        public IHttpActionResult CreateIPDPhysicianNoteAPI(Guid id, [FromBody] JObject request)
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

            var phy = CreateEIOPhysicianNote(ipd.Id, "IPD", request, form?.Id);
            return Content(HttpStatusCode.OK, new { phy.Id });

        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PhysicianNote/Update/{id}")]
        [Permission(Code = "IPDPDT02")]
        public IHttpActionResult UpdateIPDPhysicianNoteAPI(Guid id, [FromBody] JObject request)
        {
            var note = GetEIOPhysicianNote(id);
            if (note == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PN_NOT_FOUND);

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

            var phy = UpdateEIOPhysicianNote(id, request, form?.Id);
            return Content(HttpStatusCode.OK, new { phy.Id });
        }

        [HttpGet]
        [Route("api/IPD/PhysicianNote/List/{id}")]
        [Permission(Code = "IPDPDT03")]
        public IHttpActionResult ListIPDPhysicianNoteAPI(Guid id, [FromUri] EDParameterModel Parameter)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var list = GetListEIOPhysicianNote(ipd, "IPD", Parameter.StartAt, Parameter.EndAt, Parameter.CreatedBy, Parameter.Sort, Parameter.FormIdNewborn);
            dynamic lastUpdated = unitOfWork.EIOPhysicianNoteRepository.AsQueryable().OrderByDescending(e => e.UpdatedAt).FirstOrDefault(e => !e.IsDeleted && e.VisitId == ipd.Id && e.FormId == Parameter.FormIdNewborn);

            EIOConstraintNewbornAndPregnantWoman newbornForm = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == Parameter.FormIdNewborn);
            if (lastUpdated == null) // log tạo tab khi chưa có chăm sóc nào
                lastUpdated = newbornForm;


            var condition_version2 = CheckVisitAndSetUp(ipd);
            return Ok(new
            {
                ipd.RecordCode,
                EDId = ipd.Id,
                Datas = list,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuDieuTri),
                UpdatedAt = lastUpdated?.UpdatedAt,
                UpdatedBy = lastUpdated?.UpdatedBy,
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
                ipd.Version
            });
        }

        [HttpGet]
        [Route("api/IPD/PhysicianNote/Infor/{visitId}")]
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