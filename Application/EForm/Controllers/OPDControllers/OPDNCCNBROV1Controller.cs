using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDNCCNBROV1Controller : BaseApiController
    {
        private readonly string visit_type = "OPD";

        [HttpGet]
        [Route("api/OPD/NCCNBROV1/Info/{type}/{visitId}")]
        [Permission(Code = "NCCNBROV1GET")]
        public IHttpActionResult GetInfo(Guid visitId, string type = "A01_201_201119_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var user = GetUser();
            return Content(HttpStatusCode.OK, new
            {
                IsLocked24h = Is24hLocked(visit.CreatedAt, visitId, type, user.Username)
            });
        }

        [HttpGet]
        [Route("api/OPD/NCCNBROV1/{type}/{visitId}")]
        [Permission(Code = "NCCNBROV1GET")]
        public IHttpActionResult GetNCCNBROV1(Guid visitId, string type = "A01_201_201119_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var form = GetForm(visitId);
            var user = GetUser();
            if (form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú không tồn tại",
                    EnMessage = "NCCN BR/OV - 1 does not exist",
                    IsLocked24h = Is24hLocked(visit.CreatedAt, visitId, type, user.Username)
                });           
            return Content(HttpStatusCode.OK, FormatOutput(type, visit, form));
        }

        [HttpPost]
        [Route("api/OPD/NCCNBROV1/Create/{type}/{visitId}")]
        [Permission(Code = "NCCNBROV1POST")]
        public IHttpActionResult CreateNCCNBROV1(Guid visitId,string type = "A01_201_201119_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);

            var user = GetUser();
            if (Is24hLocked(visit.CreatedAt, visitId, type, user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            var form = GetForm(visitId);
            if (form != null)
                return Content(HttpStatusCode.BadRequest,
                    new
                    {
                        ViMessage = "Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú đã tồn tại",
                        EnMessage = "NCCN BR/OV - 1 already exists"
                    });

            var form_data = new OPDNCCNBROV1()
            {
                VisitId = visitId
            };
            unitOfWork.OPDNCCNBROV1Repository.Add(form_data);
            CreateOrUpdateFormForSetupOfAdmin(visitId, form_data.Id, type);
            UpdateVisit(visit, visit_type);
            //var idForm = form_data.Id;
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }

        [HttpPost]
        [Route("api/OPD/NCCNBROV1/Update/{type}/{visitId}")]
        [Permission(Code = "NCCNBROV1POST")]
        public IHttpActionResult UpdateNCCNBROV1(Guid visitId,[FromBody] JObject request,string type = "A01_201_201119_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);
            var user = GetUser();
            var form = GetForm(visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú không tồn tại",
                    EnMessage = "NCCN BR/OV - 1 does not exist",
                });
            
            if (Is24hLocked(visit.CreatedAt, visitId, type, user.Username, form.Id))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);
            //var user = GetUser();
            //if (user.Username != form.CreatedBy)
            //    return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);
            if (form.DoctorConfirmId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            HandleUpdateOrCreateTableFormData((Guid)form.VisitId, form.Id, type, request["Datas"]);
            unitOfWork.OPDNCCNBROV1Repository.Update(form);
            CreateOrUpdateFormForSetupOfAdmin(visitId, form.Id, type);
            UpdateVisit(visit, visit_type);
            //var formId = form.Id;
            //CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }

        [HttpPost]
        [Route("api/OPD/NCCNBROV1/Confirm/{type}/{visitId}")]
        [Permission(Code = "IPDGENASS3")]
        public IHttpActionResult ConfirmAPI(Guid visitId, [FromBody] JObject request, string type = "A01_201_201119_V")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.OPD_NOT_FOUND);

            var form = GetForm(visitId);
            if (form == null)
                return Content(HttpStatusCode.NotFound,new {
                    ViMessage = "Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú không tồn tại",
                    EnMessage = "NCCN BR/OV - 1 does not exist",
                });           
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString());
            if (successConfirm)
            {
                UpdateVisit(visit, visit_type);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        private OPDNCCNBROV1 GetForm(Guid visit_id)
        {
            return unitOfWork.OPDNCCNBROV1Repository.Find(e => !e.IsDeleted && e.VisitId == visit_id).FirstOrDefault();
        }
        private dynamic FormatOutput(string formCode, OPD opd, OPDNCCNBROV1 fprm)
        {
            var DoctorConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.DoctorConfirmId);
            var FullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.CreatedBy)?.Fullname;
            var FullNameUpdate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.UpdatedBy)?.Fullname;
            var user = GetUser();
            return new
            {
                ID = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCode),
                CreatedBy = fprm.CreatedBy,
                FullNameCreate = FullNameCreate,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                FullNameUpdate = FullNameUpdate,
                UpdatedAt = fprm.UpdatedAt,
                Is24hLocked = Is24hLocked(opd.CreatedAt, opd.Id, formCode, user.Username),
                 Confirm = new
                {
                    Doctor = new
                    {
                        UserName = DoctorConfirm?.Username,
                        FullName = DoctorConfirm?.Fullname,
                        ConfirmAt = fprm.DoctorConfirmAt,
                    }
                }
            };
        }
        private bool ConfirmUser(OPDNCCNBROV1 opdNCCNBROV, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (kind.ToUpper() == "DOCTOR" &&  positions.Contains("DOCTOR") && opdNCCNBROV.DoctorConfirmId == null)
            {
                opdNCCNBROV.DoctorConfirmAt = DateTime.Now;
                opdNCCNBROV.DoctorConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.OPDNCCNBROV1Repository.Update(opdNCCNBROV);
            unitOfWork.Commit();
            return true;
        }
        
        protected void HandleUpdateOrCreateTableFormData(Guid VisitId, Guid FormId, string formCode, JToken request)
        {
            List<FormDatas> listInsert = new List<FormDatas>();
            List<FormDatas> listUpdate = new List<FormDatas>();
            var allergy_dct = new Dictionary<string, string>();

            var visit_type = GetCurrentVisitType();
            if (request != null)
            {
                foreach (var item in request)
                {
                    var code = item["Code"]?.ToString();
                    if (string.IsNullOrEmpty(code)) continue;
                    var value = item["Value"]?.ToString();
                    CreateOrUpdateTableFormData(VisitId, FormId, formCode, code, value, visit_type, ref listInsert, ref listUpdate);
                }
                if (listInsert.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Adds(listInsert);
                }
                if (listUpdate.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Updates(listUpdate);
                }
            }
        }
        protected void CreateOrUpdateTableFormData(Guid visitId, Guid formId, string formCode, string code, string value, string visit_type, ref List<FormDatas> listInsert, ref List<FormDatas> listUpdate)
        {
            var finded = unitOfWorkDapper.FormDatasRepository.FirstOrDefault(e =>
            e.IsDeleted == false &&
            e.VisitId == visitId &&
            e.FormCode == formCode &&
            e.FormId == formId &&
            e.Code == code);
            if (finded == null)
            {
                listInsert.Add(new FormDatas
                {
                    Code = code,
                    Value = value,
                    FormId = formId,
                    VisitId = visitId,
                    FormCode = formCode,
                    VisitType = visit_type,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });
            }
            else
            {
                finded.Value = value;
                listUpdate.Add(finded);
            }
        }
    }
}