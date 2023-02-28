using EForm.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web.Http;
using EForm.Common;
using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using System.Linq;
using System.Collections.Generic;
using DataAccess.Models.GeneralModel;
using EForm.Utils;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class EMRFormController : BaseApiController
    {
        [HttpGet]
        [Route("api/GetList/{type}/{formcode}/{visitId}")]
        [Permission(Code = "APIGDT")]
        public IHttpActionResult GetList(string type, string formcode, Guid visitId)
        {
            var formcodeRepo = ConvertFormCode(formcode);
            var checkfrom = unitOfWork.FormRepository.FirstOrDefault(e => e.VisitTypeGroupCode == type && e.Code == formcodeRepo);
            if (checkfrom == null) return Content(HttpStatusCode.NotFound, new { ViName = "Không tìm thấy formcode! ", EnName = "Formcode is not found!" });
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.VISIT_NOT_FOUND);
            bool islock = false;

            if (type == "IPD")
            {
                islock = IPDIsBlock(visit, formcodeRepo, form_timeToBlock: checkfrom.TimeToLockForm);
            }
            if (type == "OPD")
            {
                var user = GetUser();
                islock = Is24hLocked(visit.CreatedAt, visit.Id, formcodeRepo, user.Username);
            }
            var forms = GetForms(visitId, formcode, type);
            if (forms.Count == 0)
                return Content(HttpStatusCode.NotFound, new
                {
                    visitId = visitId,
                    IsLock24h = islock,
                    Common.Message.FORM_NOT_FOUND
                });
            var datas = forms.Select(form => new
            {
                form?.Id,
                form?.CreatedAt,
                form?.CreatedBy,
                form?.UpdatedAt,
                form?.UpdatedBy
            }).OrderBy(e => e.CreatedAt).ToList();
            var lastUpdate = datas.OrderByDescending(x => x.UpdatedAt).FirstOrDefault();
            return Content(HttpStatusCode.OK, new
            {
                VisitId = visitId,
                Datas = datas,
                LastInfo = new
                {
                    lastUpdate?.CreatedAt,
                    lastUpdate?.CreatedBy,
                    lastUpdate?.UpdatedAt,
                    lastUpdate?.UpdatedBy
                },
                IsLock24h = islock,
                visit.Version
            });
        }
        [HttpGet]
        [Route("api/GetDetail/{type}/{formcode}/{visitId}/{id}")]
        [Permission(Code = "APIGDT")]
        public IHttpActionResult GetFormByVisitId(string type, string formcode, Guid visitId, Guid id)
        {
            var formcodeRepo = ConvertFormCode(formcode);
            var checkfrom = unitOfWork.FormRepository.FirstOrDefault(e => e.VisitTypeGroupCode == type && e.Code == formcodeRepo);
            if (checkfrom == null) return Content(HttpStatusCode.NotFound, new { ViName = "Không tìm thấy formcode! ", EnName = "Formcode is not found!" });
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.VISIT_NOT_FOUND);

            var form = unitOfWork.EIOFormRepository.GetById(id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Common.Message.FORM_NOT_FOUND);
           
            
            bool IsLock24h = false;
            var isUnLockConfirm = unitOfWork.UnlockFormToUpdateRepository
                                            .Find(e => e.FormCode == formcodeRepo && e.FormId != null && e.FormId == form.Id && e.ExpiredAt >= DateTime.Now)
                                            .OrderByDescending(e => e.UpdatedAt)?.FirstOrDefault() != null ? true : false;
            if (type == "IPD")
            {
                IsLock24h = IPDIsBlock(visit, formcodeRepo, id, form_timeToBlock: checkfrom.TimeToLockForm);
            }
            if (type == "OPD")
            {
                var user = GetUser();
                if (formcode == "A01_067_050919_VE_TAB1")
                {
                    if (isUnLockConfirm)
                    {
                        IsLock24h = false;
                    }else
                    {
                        IsLock24h = Is24hLocked(visit.CreatedAt, visit.Id, formcodeRepo, user.Username, id);
                    }    
                }
                else
                {
                    IsLock24h = Is24hLocked(visit.CreatedAt, visit.Id, formcodeRepo, user.Username, id);
                }
               
            }
            var data = FormatOutput(form, visit, isUnLockConfirm);
            return Content(HttpStatusCode.OK, new { data, IsLock24h = IsLock24h });
        }
        [HttpPost]
        [Route("api/CreateForm/{type}/{formcode}/{visitId}")]
        [Permission(Code = "APICR")]
        public IHttpActionResult CreateForm(string type, string formcode, Guid visitId)
        {
            var formcodeRepo = ConvertFormCode(formcode);
            var checkfrom = unitOfWork.FormRepository.FirstOrDefault(e => e.VisitTypeGroupCode == type && e.Code == formcodeRepo);
            if (checkfrom == null) return Content(HttpStatusCode.NotFound, new { ViName = "Không tìm thấy formcode! ", EnName = "Formcode is not found!" });
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.VISIT_NOT_FOUND);

            if (Validate24hLocked(visit, type, formcodeRepo, timeToBlock: checkfrom.TimeToLockForm) && !formcode.Contains("UPLOADFILE"))
                    return Content(HttpStatusCode.BadRequest, Common.Message.FORM_IS_LOCKED);
            
            if (checkfrom.Time == 1)
            {
                var form = unitOfWork.EIOFormRepository.FirstOrDefault(e => e.FormCode == formcode && e.VisitTypeGroupCode == type && e.VisitId == visitId);
                if (form != null) return Content(HttpStatusCode.BadRequest, Common.Message.FORM_EXIST);
            }
            var form_data = new EIOForm
            {
                VisitId = visitId,
                Version = visit.Version >= 7 ? visit.Version : checkfrom?.Version,
                VisitTypeGroupCode = type,
                FormCode = formcode
            };
            unitOfWork.EIOFormRepository.Add(form_data);
            unitOfWork.Commit();
            UpdateVisit(visit, type);
            SaveFormSetup(form_data); 
            return Content(HttpStatusCode.OK, new { form_data.Id, form_data.CreatedAt, form_data.CreatedBy, form_data.VisitId });
        }

        [HttpPost]
        [Route("api/UpdateForm/{type}/{formcode}/{visitId}/{id}")]
        [Permission(Code = "APIUD")]
        public IHttpActionResult UpdateForm(string type, string formcode, Guid visitId, Guid id, [FromBody] JObject request)
        {
            var formcodeRepo = ConvertFormCode(formcode);
            var checkfrom = unitOfWork.FormRepository.FirstOrDefault(e => e.VisitTypeGroupCode == type && e.Code == formcodeRepo);
            if (checkfrom == null) return Content(HttpStatusCode.NotFound, new { ViName = "Không tìm thấy formcode! ", EnName = "Formcode is not found!" });
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.VISIT_NOT_FOUND);

            var form = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == formcode && e.Id == id).FirstOrDefault(); ;
            if (form == null)
                return Content(HttpStatusCode.NotFound, Common.Message.FORM_NOT_FOUND);

            if (formcode == "A01_067_050919_VE_TAB1")
            {
                var isUnLockConfirm = unitOfWork.UnlockFormToUpdateRepository
                                                .Find(e => e.FormCode == formcodeRepo && e.FormId != null && e.FormId == form.Id && e.ExpiredAt >= DateTime.Now)
                                                .OrderByDescending(e => e.UpdatedAt)?.FirstOrDefault() != null ? true : false;
                if(isUnLockConfirm)
                {
                    if (GetFormConfirms(form.Id).Count > 0)
                    {
                        return Content(HttpStatusCode.Forbidden, Common.Message.OWNER_FORBIDDEN);
                    }
                }
                else
                {
                    if (Validate24hLocked(visit, type, formcodeRepo, form.Id, timeToBlock: checkfrom.TimeToLockForm))
                        return Content(HttpStatusCode.BadRequest, Common.Message.FORM_IS_LOCKED);
                }
            }
            else
            {
                if (Validate24hLocked(visit, type, formcodeRepo, form.Id, timeToBlock: checkfrom.TimeToLockForm))
                    return Content(HttpStatusCode.BadRequest, Common.Message.FORM_IS_LOCKED);

                if (checkfrom.Ispermission)
                {
                    var user = GetUser();
                    if (user.Username != form.CreatedBy && !IsCheckConfirm(form.Id))
                    {
                        return Content(HttpStatusCode.Forbidden, Common.Message.OWNER_FORBIDDEN);
                    }
                }
            }
            if(formcodeRepo == "A02_053_OR_201022_V")
            {
                HandleUpdateOrCreateFormDatasProcedureSafetyChecklist((Guid)form.VisitId, form.Id, formcode, request["Datas"]);
            }
            else
            {
                HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formcode, request["Datas"]);
            }
            form.Note = request["Note"]?.ToString();
            form.Comment = request["Comment"]?.ToString();
            unitOfWork.EIOFormRepository.Update(form);

            UpdateVisit(visit, type);
            SaveFormSetup(form);
            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [HttpPost]
        [Route("api/ConfirmForm/{type}/{formcode}/{visitId}/{idform}")]
        [Permission(Code = "APICF")]
        public IHttpActionResult ConfirmForm(string type, string formcode, Guid visitId, Guid idform, [FromBody] JObject request)
        {
            var formcodeRepo = ConvertFormCode(formcode);
            var checkfrom = unitOfWork.FormRepository.FirstOrDefault(e => e.VisitTypeGroupCode == type && e.Code == formcodeRepo);
            if (checkfrom == null) return Content(HttpStatusCode.NotFound, new { ViName = "Không tìm thấy formcode! ", EnName = "Formcode is not found!" });
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, "Không tồn tại");

            var form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == idform);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Common.Message.FORM_NOT_FOUND);
            if (formcode == "A01_067_050919_VE_TAB1")
            {
                var isUnLockConfirm = unitOfWork.UnlockFormToUpdateRepository
                                                .Find(e => e.FormCode == formcodeRepo && e.FormId != null && e.FormId == form.Id && e.ExpiredAt >= DateTime.Now)
                                                .OrderByDescending(e => e.UpdatedAt)?.FirstOrDefault() != null ? true : false;
                if (isUnLockConfirm)
                {
                    if (GetFormConfirms(form.Id).Count > 0)
                    {
                        return Content(HttpStatusCode.Forbidden, Common.Message.OWNER_FORBIDDEN);
                    }
                }
                else
                {
                    if (Validate24hLocked(visit, type, formcodeRepo, form.Id, timeToBlock: checkfrom.TimeToLockForm))
                        return Content(HttpStatusCode.BadRequest, Common.Message.FORM_IS_LOCKED);
                }
            }
            else
            {
                if (Validate24hLocked(visit, type, formcodeRepo, form.Id, timeToBlock: checkfrom.TimeToLockForm))
                    return Content(HttpStatusCode.BadRequest, Common.Message.FORM_IS_LOCKED);
            }


            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Common.Message.INFO_INCORRECT);

            if (!ConfirmOfUserCreated(form.CreatedBy, kind, user.Username))
                return Content(HttpStatusCode.Forbidden, Common.Message.FORBIDDEN);

            var PermissionCode = "APICF" + type + formcodeRepo;
            var ischeckpermission = IsCheckPermission(username, PermissionCode);
            if (!ischeckpermission)
                return Content(HttpStatusCode.Forbidden, Common.Message.OWNER_FORBIDDEN);
            Guid? formid = form.Id;
            var getconfirm = GetFormConfirms(form.Id);
            if (getconfirm.Count > 0)
            {
                foreach (var item in getconfirm)
                {
                    if (kind != item.ConfirmType)
                    {
                        SaveConfirm(username, kind, formid);
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, Common.Message.INFO_INCORRECT);
                    }
                }
            }
            else
            {
                SaveConfirm(username, kind, formid);
            }
            unitOfWork.EIOFormRepository.Update(form);
            UpdateVisit(visit, type);

            return Content(HttpStatusCode.OK, Common.Message.SUCCESS);

        }

        [HttpGet]
        [Route("api/IsCheckGetDetail/{type}/{formcode}/{visitId}")]
        public IHttpActionResult IsCheckGetDetail(string type, string formcode, Guid visitId)
        {
            var formcodeRepo = ConvertFormCode(formcode);
            var checkfrom = unitOfWork.FormRepository.FirstOrDefault(e => e.VisitTypeGroupCode == type && e.Code == formcodeRepo);
            if (checkfrom == null) return Content(HttpStatusCode.NotFound, new { ViName = "Không tìm thấy formcode! ", EnName = "Formcode is not found!" });
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.VISIT_NOT_FOUND);
            bool islock = false;

            if (type == "IPD")
            {
                islock = IPDIsBlock(visit, formcodeRepo, form_timeToBlock: checkfrom.TimeToLockForm);
            }
            if (type == "OPD")
            {
                var user = GetUser();
                islock = Is24hLocked(visit.CreatedAt, visit.Id, formcodeRepo, user.Username);
            }
            var form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == formcode);
            if (form == null)
                return Content(HttpStatusCode.OK, new { IsCheckGetForm = true, IsLock24h = islock });           
            return Content(HttpStatusCode.OK, new { IsCheckGetForm = false, Id = form.Id, IsLock24h = islock });
        }

        //Tách riêng phần update form BẢNG KIỂM AN TOÀN PHẪU THUẬT/ THỦ THUẬT TRONG PHÒNG MỔ VÀ PHÒNG CAN THIỆP TIM MẠCH

        protected void HandleUpdateOrCreateFormDatasProcedureSafetyChecklist(Guid VisitId, Guid FormId, string formCode, JToken request, dynamic visit = null, bool ischeck = false)
        {
            List<FormDatas> listInsert = new List<FormDatas>();
            List<FormDatas> listUpdate = new List<FormDatas>();
            var allergy_dct = new Dictionary<string, string>();

            var visit_type = GetCurrentVisitType();
            List<FormDatas> current_data = null;
            current_data = unitOfWorkDapper.FormDatasRepository.Find(e =>
                 e.IsDeleted == false &&
                 e.FormId == FormId).ToList();
            var listConfirm = unitOfWork.EIOFormConfirmRepository.Find(e =>
                        e.IsDeleted == false &&
                        e.FormId == FormId
                ).ToList();
            if (request != null)
            {
                foreach (var item in request)
                {
                    var code = item["Code"]?.ToString();
                    if (string.IsNullOrEmpty(code)) continue;
                    if(formCode == "A02_053_OR_201022_V_TAB11")
                    {
                       var listConfirm1 = listConfirm.Where(e => !e.IsDeleted && e.Note == "A02_053_OR_201022_V_TAB1_GM").ToList();
                       if (listConfirm1 != null && listConfirm1.Any() && "PMVPCTTM05,PMVPCTTM08,PMVPCTTM11,PMVPCTTM14,PMVPCTTM16,PMVPCTTM189".Contains(code)) continue;
                       
                        var listConfirm2 = listConfirm.Where(e => !e.IsDeleted && e.Note == "A02_053_OR_201022_V_TAB1_DCKT").ToList();
                        if (listConfirm2 != null && listConfirm2.Any() && "PMVPCTTM22,PMVPCTTM25,PMVPCTTM28,PMVPCTTM31".Contains(code)) continue;
                       
                        var listConfirm3 = listConfirm.Where(e => !e.IsDeleted && e.Note == "A02_053_OR_201022_V_TAB1_CNTH").ToList();
                        if (listConfirm3 != null && listConfirm3.Any() && "PMVPCTTM35,PMVPCTTM37PMVPCTTM39".Contains(code)) continue;
                    }
                    else if(listConfirm != null && listConfirm.Any()) continue;                   
                    
                    var value = item["Value"]?.ToString();
                    if (Constant.OPD_IAFST_ALLERGIC_CODE.Contains(code))
                        allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;
                    CreateOrUpdateFormData(VisitId, FormId, formCode, code, value, visit_type, ref listInsert, ref listUpdate, current_data, ischeck);
                }
                if (listInsert.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Adds(listInsert);
                }
                if (listUpdate.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Updates(listUpdate);
                }
                if (visit != null)
                {
                    if (allergy_dct.Count() > 0)
                    {
                        var visit_util = new VisitAllergy(visit);
                        visit_util.UpdateAllergy(allergy_dct);
                        unitOfWork.Commit();
                    }
                }
            }
        }
        private bool Validate24hLocked(dynamic visit, string type, string formCode, Guid? formId = null, int? timeToBlock = null)
        {
            if (type == "OPD")
            {
                var user_login = GetUser();
                return Is24hLocked(visit.CreatedAt, visit.Id, formCode, user_login?.Username, formId);
            }
            else if (type == "IPD")
                return IPDIsBlock(visit, formCode, form_timeToBlock: timeToBlock, formId: formId);
            else
                return false;
        }

        #region dannv 6
        [HttpGet]
        [Route("api/EIO/EMRForm/GetVersionByVisit/{visitId}/{visitTypeCode}")]
        public IHttpActionResult GetLastAppVersionForVisit(Guid visitId, string visitTypeCode)
        {
            var visit = GetVisit(visitId, visitTypeCode);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.VISIT_NOT_FOUND);
            return Content(HttpStatusCode.OK, new { Version = visit.Version, ReceivingDate = visit.CreatedAt });
        }
        private bool ConfirmOfUserCreated(string user_createdForm, string kind, string userName)
        {
            if (!kind.ToUpper().Contains("UserCreated".ToUpper()))
                return true;

            if (user_createdForm.ToLower() == userName.ToLower())
                return true;

            return false;
        }
        private void SaveFormSetup(EIOForm form)
        {
            if(form != null)
            {
                switch(form.FormCode)
                {
                    case "A01_159_050919_VE":
                        CreateOrUpdateFormForSetupOfAdmin(form.VisitId, form.Id, form.FormCode);
                        return;
                    case "A02_052_050919_V":
                        CreateOrUpdateFormForSetupOfAdmin(form.VisitId, form.Id, form.FormCode);
                        return;
                    default: return;
                }
            }
        }
        #endregion
    }
}