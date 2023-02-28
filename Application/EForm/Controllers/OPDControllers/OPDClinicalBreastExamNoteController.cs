using DataAccess.Models;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers
{
    [SessionAuthorize]
    public class OPDClinicalBreastExamNoteController : BaseApiController
    {
        private const string type = "A03_116_200520_V";
        private readonly string visit_type = "OPD";
        [HttpPost]
        [Route("api/OPD/ClinicalBreastExamNote/Create/{type}/{opdId}")]
        [Permission(Code = "CBENCOPD")]
        public IHttpActionResult CreateClinicalBreastExamNote(string type, Guid opdId)
        {
            var visit = GetVisit(opdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);
            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(visit.CreatedAt);
            var has_unlock_permission = HasUnlockPermission(visit.Id, "A03_116_200520_V", user.Username);
            if (is_block_after_24h && !has_unlock_permission)
                return Content(HttpStatusCode.BadRequest, Common.Message.TIME_FORBIDDEN);
            var form_data = new OPDClinicalBreastExamNote()
            {
                VisitId = opdId
            };
            unitOfWork.OPDClinicalBreastExamNoteRepository.Add(form_data);
            UpdateVisit(visit, visit_type);
            var idForm = form_data.Id;
            CreateOrUpdateOPDInitialAssessmentToByFormType(visit, type, idForm);
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }

        [HttpPost]
        [Route("api/OPD/ClinicalBreastExamNote/Update/{type}/{opdId}/{id}")]
        [Permission(Code = "CBENUUPD")]

        public IHttpActionResult UpdateClinicalBreastExamNote(string type, Guid opdId, Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(opdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);

            var form = GetForm(id);
            if (form == null)
                 return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));
            var user = GetUser();
            //if (user == null || user.Username != form.CreatedBy)
            //    return Content(HttpStatusCode.BadRequest, Common.Message.OWNER_FORBIDDEN);
            var is_block_after_24h = IsBlockAfter24h(visit.CreatedAt, id);
            var has_unlock_permission = HasUnlockPermission(visit.Id, "A03_116_200520_V", user.Username, id);
            if (is_block_after_24h && !has_unlock_permission)
                return Content(HttpStatusCode.BadRequest, Common.Message.TIME_FORBIDDEN);
            if (form.DoctorConfirmId != null)
                return Content(HttpStatusCode.NotFound, Common.Message.OWNER_FORBIDDEN);
            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, type, request["Datas"]);
            unitOfWork.OPDClinicalBreastExamNoteRepository.Update(form);
            UpdateVisit(visit, visit_type);
            var formId = form.Id;
            CreateOrUpdateOPDInitialAssessmentToByFormType(visit, type, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }

        [HttpPost]
        [Route("api/OPD/ClinicalBreastExamNote/Confirm/{type}/{opdId}/{id}")]
        [Permission(Code = "CBENUCFOPD")]

        public IHttpActionResult ConfirmAPI(string type, Guid opdId, Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(opdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);
            var form = GetForm(id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundDataOPD(visit, type));            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user);
            if (successConfirm)
            {
                UpdateVisit(visit, visit_type);
                return Content(HttpStatusCode.OK, Common.Message.SUCCESS);                             
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Common.Message.FORBIDDEN);
            }
        }
        [HttpGet]
        [Route("api/OPD/ClinicalBreastExamNote/{type}/{opdId}")]
        [Permission(Code = "CBENUGLOPD")]
        public IHttpActionResult GetClinicalBreastExamNotes(string type, Guid opdId)
        {
            var visit = GetVisit(opdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);
            var user = GetUser();
            bool is_block_after_24h = false;

            is_block_after_24h = Is24hLocked(visit.CreatedAt, opdId, "A03_116_200520_V", user.Username);
            
            var forms = unitOfWork.OPDClinicalBreastExamNoteRepository.Find(e => !e.IsDeleted && e.VisitId == opdId).OrderBy(o => o.Order).ToList().Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                CreatedAt = form.CreatedAt,
                UpdatedAt = form.UpdatedAt,
                Order = form.Order
            }).ToList();            
            return Content(HttpStatusCode.OK, new
            {
                Datas = forms,
                is_block_after_24h,
            });
        }
        [HttpGet]
        [Route("api/OPD/ClinicalBreastExamNote/{type}/{opdId}/{id}")]
        //[Permission(Code = "CBENUDT")]
        [Permission(Code = "CBENUGLOPD")]
        public IHttpActionResult GetClinicalBreastExamNote(string type, Guid opdId, Guid id)
        {
            var visit = GetVisit(opdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.OPD_NOT_FOUND);
            var user = GetUser();            
            var form = unitOfWork.OPDClinicalBreastExamNoteRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (form == null)
                return Content(HttpStatusCode.NotFound, Common.Message.FORM_NOT_FOUND_WITH_LOCKED);            
            return Content(HttpStatusCode.OK, FormatOutput(type, visit, form));
        }

        private OPDClinicalBreastExamNote GetForm(Guid id)
        {
            return unitOfWork.OPDClinicalBreastExamNoteRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
        }
        private dynamic FormatOutput(string type, OPD opd, OPDClinicalBreastExamNote fprm)
        {
            var user = GetUser();
            var datas = GetFormData((Guid)fprm.VisitId, fprm.Id, type);
            var doctor = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.DoctorConfirmId);

            bool is_block_after_24h = false;
            is_block_after_24h = Is24hLocked(opd.CreatedAt, opd.Id, "A03_116_200520_V", user.Username, fprm.Id);
            return new
            {
                Id = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = datas,
                CreatedBy = fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                UpdatedAt = fprm.UpdatedAt,
                DoctorConfirm = new
                {
                    UserName = doctor?.Username,
                    FullName = doctor?.Fullname,
                },
                DoctorConfirmAt = fprm.DoctorConfirmAt,
                is_block_after_24h
            };
        }
        private bool ConfirmUser(OPDClinicalBreastExamNote opdClinicalBreastExamNote, User user)
        {
            if (opdClinicalBreastExamNote.DoctorConfirmId == null)
            {
                opdClinicalBreastExamNote.DoctorConfirmAt = DateTime.Now;
                opdClinicalBreastExamNote.DoctorConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.OPDClinicalBreastExamNoteRepository.Update(opdClinicalBreastExamNote);
            unitOfWork.Commit();

            return true;
        }       
    }
}

