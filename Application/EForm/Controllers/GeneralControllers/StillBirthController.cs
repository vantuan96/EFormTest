using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class StillBirthController : BaseApiController
    {
        [HttpGet]
        [Route("api/{area}/StillBirth/Info/{type}/{visitId}/{Id}")]
        [Permission(Code = "STILLBIRTHGET")]
        public IHttpActionResult GetInfoStillBirth(string type, Guid visitId,Guid Id, string area = "IPD")
        {
            var IsLocked = false;
            var visit = GetVisit(visitId, area);
            
            if (visit == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            string specialtyName = "";
            string customerId = "";
            switch (area)
            {
                case "IPD":
                    specialtyName = visit?.Specialty?.ViName;
                    IsLocked = IPDIsBlock(visit, type);
                    customerId = visit?.CustomerId.ToString();
                    break;
                case "OPD":
                    specialtyName = visit?.Specialty?.ViName;
                    var user = GetUser();
                    IsLocked = Is24hLocked(visit.CreatedAt, visitId, type, user.Username);
                    customerId = visit?.CustomerId.ToString();
                    break;
            }
            var SpecialtyNoList = unitOfWork.AppConfigRepository.FirstOrDefault(x => x.Key == "STILL_BIRTH_CODE").Value.ToString().Split(',');
            Guid gCustomerId = Guid.Parse(customerId);
            var LastPreNatal = unitOfWork.OPDRepository.AsQueryable().Where(x => x.CustomerId == gCustomerId && SpecialtyNoList.Contains(x.Specialty.SpecialtyNo.ToString())).OrderByDescending(x => x.AdmittedDate).Select(x => new {
                SiteCode = x.Site.ApiCode,
                SpecialtyViName = x.Specialty.ViName,
                Area = "OPD",
                SpecialtyEnName = x.Specialty.EnName,
                AdmittedDate = x.AdmittedDate

            }).ToList();

            return Content(HttpStatusCode.OK, new
            {
                Specialty = specialtyName,
                IsLocked,
                LastPreNatal
            });
        }
        
        [HttpGet]
        [Route("api/{area}/StillBirth/{type}/{visitId}")]
        [Permission(Code = "STILLBIRTHGET")]
        public IHttpActionResult GetStillBirth(string type,Guid visitId, string area = "IPD")
        {
            var IsLocked = false;
            var visit = GetVisit(visitId, area);
            if (visit == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            var hracs = unitOfWork.StillBirthRepository.Find(x => !x.IsDeleted && x.VisitId == visitId).OrderBy(o => o.CreatedAt).Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                CreatedAt = form.CreatedAt,
                UpdatedAt = form.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
            }).ToList();

            switch(area)
            {
              case "IPD":
                    IsLocked = IPDIsBlock(visit, type);
                    break;
                case "OPD":
                    var user = GetUser();
                    IsLocked = Is24hLocked(visit.CreatedAt, visitId, type, user.Username);
                    break;
            }
            return Content(HttpStatusCode.OK, new
            {
                Datas = hracs,
                IsLocked
            });
        }

        [HttpGet]
        [Route("api/{area}/StillBirth/{type}/{visitId}/{id}")]
        [Permission(Code = "STILLBIRTHGET")]
        public IHttpActionResult Get(string type, Guid visitId, Guid id, string area = "ED")
        {
            var visit = GetVisit(visitId, area);
            if (visit == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            var stillBirth = unitOfWork.StillBirthRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId == visitId && x.Id == id);
            if (stillBirth == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            else
            {
                return Content(HttpStatusCode.OK, FormatOutput(type, area, visit, stillBirth));
            }
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/{area}/StillBirth/Create/{type}/{visitId}")]
        [Permission(Code = "STILLBIRTHPOST")]
        public IHttpActionResult Post(string type, Guid visitId, string area = "IPD")
        {

            dynamic visit = GetVisit(visitId, area);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            switch(area)
            {
                case "IPD":
                    if (IPDIsBlock((IPD)visit, type))
                        return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
                    break;
                case "OPD":
                    var user = GetUser();
                    if (Is24hLocked(visit.CreatedAt, visitId, type, user.Username))
                        return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
                    break;
            }
            var stillBirth = new StillBirth
            {
                VisitTypeGroupCode = area,
                VisitId = visitId
            };
            unitOfWork.StillBirthRepository.Add(stillBirth);
            CreateOrUpdateFormForSetupOfAdmin(visitId, stillBirth.Id, "A01_152_100122_VE");
            UpdateVisit(visit, area);
            //var idForm = stillBirth.Id;
            //CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, idForm);
            return Content(HttpStatusCode.OK, new
            {
                stillBirth.Id,
                stillBirth.VisitId,
                stillBirth.CreatedBy,
                stillBirth.CreatedAt
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/{area}/StillBirth/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "STILLBIRTHPOST")]
        public IHttpActionResult Update(string type, Guid visitId, Guid id, [FromBody] JObject request, string area = "IPD")
        {

            dynamic visit = GetVisit(visitId, area);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var stillBirth = unitOfWork.StillBirthRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId == visitId && x.Id == id);
            if (stillBirth == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            else
            {
                switch (area)
                {
                    case "IPD":
                        if (IPDIsBlock((IPD)visit, type, stillBirth.Id))
                            return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
                        break;
                    case "OPD":
                        var user = GetUser();
                        if (Is24hLocked(visit.CreatedAt, visitId, type, user.Username, stillBirth.Id))
                            return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
                        break;
                }
                if (stillBirth.HospitalLeadershipConfirmId != null || stillBirth.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmId != null)
                    return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
                
                HandleUpdateOrCreateFormDatas(visitId, stillBirth.Id, type + "_" + area, request["Datas"]);
                
                unitOfWork.StillBirthRepository.Update(stillBirth);
                CreateOrUpdateFormForSetupOfAdmin(visitId, stillBirth.Id, "A01_152_100122_VE");
                UpdateVisit(visit, area);
                //var formId = stillBirth.Id;
                //CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, formId);
                return Content(HttpStatusCode.OK, new
                {
                    stillBirth.Id,
                    stillBirth.VisitId,
                    stillBirth.UpdatedAt,
                    stillBirth.UpdatedBy
                });
            }
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/{area}/StillBirth/Confirm/{type}/{visitId}/{Id}")]
        [Permission(Code = "STILLBIRTHCFPOST")]
        public IHttpActionResult ConfirmAPI(string area,string type, Guid visitId, Guid Id, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, area);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(area, visitId, Id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                        ViMessage = "Biên bản phối hợp với bệnh nhân, gia đình xử lý thai chết lưu không tồn tại",
                        EnMessage = "Coordinating with the patient/family to deal with a stillbirth does not exist",
                });            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);            
            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString());
            if (successConfirm)
            {
                UpdateVisit(visit, area);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        private dynamic FormatOutput(string formCode, string area,dynamic visit, StillBirth fprm)
        {
            var IsLocked = false;
            string formCodeinFormDatas = formCode + "_" + area;
            var HospitalLeadership = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.HospitalLeadershipConfirmId);
            var PatientOrPatientIsFamily = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.PatientOrPatientIsFamilyConfirmId);
            var HeadOfDepartmentOrLeaderOfOnDutyTeam = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmId);
            var FullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.CreatedBy)?.Fullname;
            var FullNameUpdate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.UpdatedBy)?.Fullname;
            switch (area)
            {
                case "IPD":
                    IsLocked = IPDIsBlock(visit, formCode, fprm.Id);
                    break;
                case "OPD":
                    var user = GetUser();
                    IsLocked = Is24hLocked(visit.CreatedAt, visit.Id, formCode, user.Username, fprm.Id);
                    break;
            }
            return new
            {
                ID = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCodeinFormDatas),
                CreatedBy = fprm.CreatedBy,
                FullNameCreate = FullNameCreate,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                FullNameUpdate = FullNameUpdate,
                UpdatedAt = fprm.UpdatedAt,
                IsLocked,
                Confirm = new
                {
                    HospitalLeadership = new
                    {
                        UserName = HospitalLeadership?.Username,
                        FullName = HospitalLeadership?.Fullname,
                        ConfirmAt = fprm.HospitalLeadershipConfirmAt
                    },
                    HeadOfDepartmentOrLeaderOfOnDutyTeam = new
                    {
                        UserName = HeadOfDepartmentOrLeaderOfOnDutyTeam?.Username,
                        FullName = HeadOfDepartmentOrLeaderOfOnDutyTeam?.Fullname,
                        ConfirmAt = fprm.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmAt
                    },
                    PatientOrPatientIsFamily = new
                    {
                        UserName = PatientOrPatientIsFamily?.Username,
                        FullName = PatientOrPatientIsFamily?.Fullname,
                        ConfirmAt = fprm.PatientOrPatientIsFamilyConfirmAt
                    },
                }
            };
        }
        private StillBirth GetForm(string type, Guid visit_id, Guid id)
        {
            return unitOfWork.StillBirthRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.VisitTypeGroupCode == type && e.Id == id).FirstOrDefault();
        }
        private bool ConfirmUser(StillBirth stillBirth, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
           
            if (kind.ToUpper() == "HospitalLeadership".ToUpper() && positions.Contains("DOCTOR") && stillBirth.HospitalLeadershipConfirmId == null)
            {
                stillBirth.HospitalLeadershipConfirmAt = DateTime.Now;
                stillBirth.HospitalLeadershipConfirmId = user?.Id;
            }
            else if (kind.ToUpper() == "HeadOfDepartmentOrLeaderOfOnDutyTeam".ToUpper() && positions.Contains("DOCTOR") && stillBirth.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmId == null)
            {
                stillBirth.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmAt = DateTime.Now;
                stillBirth.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.StillBirthRepository.Update(stillBirth);
            unitOfWork.Commit();
            return true;
        }
    }
}
