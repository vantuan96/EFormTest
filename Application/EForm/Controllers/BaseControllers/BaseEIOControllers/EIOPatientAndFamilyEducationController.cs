using DataAccess.Models;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Security;
using System.Web.Http;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOPatientAndFamilyEducationController : BaseApiController
    {
        [HttpPost]
        [Route("api/EIO/PatientAndFamilyEducation/Created/{visitId}/{visitType}")]
        public IHttpActionResult EIOCreatedForm(Guid visitId, string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            PatientAndFamilyEducation edu_form = GetOrCreatePatientAndFamilyEducationForm(visitId, visitType, null, app_version: visit.Version);

            return Content(HttpStatusCode.OK, new { Id = edu_form.Id, Version = edu_form.Version });
        }

        protected dynamic GetListPatientAndFamilyEducation(Guid visit_id, string visit_type_group_code)
        {
            var result = new List<dynamic>();
            var pfer = GetPatientAndFamilyEducationIdByVisitId(visit_id, visit_type_group_code);
            foreach (var pf in pfer)
                result.Add(GetPersonToBeAccessed(pf));
            return result;
        }
        private dynamic GetPatientAndFamilyEducationIdByVisitId(Guid visit_id, string visit_type_group_code)
        {
            return unitOfWork.PatientAndFamilyEducationRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode.Equals(visit_type_group_code)
            ).OrderBy(e => e.CreatedAt).ToList();
        }
        private dynamic GetPersonToBeAccessed(PatientAndFamilyEducation p)
        {
            var patient = unitOfWork.PatientAndFamilyEducationDataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.PatientAndFamilyEducationId != null &&
                    e.PatientAndFamilyEducationId == p.Id &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals("PFEFNPTBAPAI") &&
                    !string.IsNullOrEmpty(e.Value) &&
                    e.Value.Equals("true", StringComparison.OrdinalIgnoreCase)
                );
            if (patient != null)
                return new { Id = p.Id, ViName = "Người bệnh", EnName = "Patient", patient?.CreatedAt, patient?.UpdatedAt, patient?.CreatedBy, patient?.UpdatedBy, p?.Version };

            var family = unitOfWork.PatientAndFamilyEducationDataRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.PatientAndFamilyEducationId != null &&
                e.PatientAndFamilyEducationId == p.Id &&
                !string.IsNullOrEmpty(e.Code) &&
                e.Code.Equals("PFEFNPTBAANS")
            );
            if (family != null)
                return new { Id = p.Id, ViName = family.Value, EnName = family.Value, family?.CreatedAt, family?.UpdatedAt, family?.CreatedBy, family?.UpdatedBy, p?.Version };

            return new { Id = p.Id, ViName = "", EnName = "", p?.CreatedAt, p?.UpdatedAt, p?.CreatedBy, p?.UpdatedBy, p?.Version };
        }

        protected dynamic BuildPatientAndFamilyEducationFormData(Guid edu_form_id, string fomCode, string visit_group)
        {
            bool isLocked = false;
            var form = unitOfWork.PatientAndFamilyEducationRepository.GetById(edu_form_id);
            var groupContent = new Dictionary<string, dynamic>();
            switch (visit_group)
            {
                case "IPD":
                    {

                        var visit = GetIPD((Guid)form.VisitId);
                        isLocked = IPDIsBlock(visit, Constant.IPDFormCode.GDSKchoNBvaThanNhan);
                        groupContent = BuildGroupContentsOfEducationForm(edu_form_id, visit_group, visit);
                        break;
                    }
                case "OPD":
                    {
                        var user = GetUser();
                        var visit = GetOPD((Guid)form.VisitId);
                        isLocked = Is24hLocked(visit.CreatedAt, visit.Id, fomCode, user.Username);
                        groupContent = BuildGroupContentsOfEducationForm(edu_form_id, visit_group, visit, user?.Username);
                        break;
                    }
                default:
                    groupContent = BuildGroupContentsOfEducationForm(edu_form_id, visit_group, visit: null);
                    break;
            }
            
            return new
            {
                IsLocked = isLocked,
                Id = edu_form_id,
                Informations = BuildInformationsOfEducationForm(edu_form_id),
                GroupContents = groupContent,
                form.Version,
                form.UpdatedAt,
                form.UpdatedBy,
                form.CreatedAt,
                form.CreatedBy
            };
        }     
        private Dictionary<string, dynamic> BuildGroupContentsOfEducationForm(Guid edu_form_id, string visitTypeGroup, dynamic visit, string userName = null)
        {
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> education_need_group_content = new Dictionary<string, dynamic>();
            foreach (var edu_form_content in GetEducationContentList(edu_form_id))
            {
                var edu_form_content_formated = BuildEducationContentData(edu_form_content, visitTypeGroup, visit, userName);

                string edu_need_code = edu_form_content.EducationalNeedCode;
                if (!education_need_group_content.ContainsKey(edu_need_code))
                {
                    response[edu_need_code] = new Dictionary<string, dynamic> {
                        {"ViName",  edu_form_content.ViName },
                        {"EnName",  edu_form_content.ViName },
                    };
                    education_need_group_content[edu_need_code] = new List<dynamic>();
                }
                education_need_group_content[edu_need_code].Add(edu_form_content_formated);


            }

            foreach (var item in education_need_group_content)
            {
                var lastPatientAndFamilyEducationContents = ((List<dynamic>)item.Value).OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();
                response[item.Key]["LastUpdatedBy"] = lastPatientAndFamilyEducationContents?.UpdatedBy?.ToString();
                response[item.Key]["LastUpdatedAt"] = lastPatientAndFamilyEducationContents?.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
                response[item.Key]["Contents"] = item.Value;
            }


            return response;
        }
        private dynamic GetEducationContentList(Guid edu_form_id)
        {
            return (from pfec_sql in unitOfWork.PatientAndFamilyEducationContentRepository.AsQueryable().Where(
                       e => !e.IsDeleted &&
                       e.PatientAndFamilyEducationId != null &&
                       e.PatientAndFamilyEducationId == edu_form_id
                   )
                    join mtd_sql in unitOfWork.MasterDataRepository.AsQueryable() on pfec_sql.EducationalNeedCode equals mtd_sql.Code into ulist
                    from mtd_sql in ulist.DefaultIfEmpty()
                    select new
                    {
                        pfec_sql.Id,
                        pfec_sql.UpdatedAt,
                        pfec_sql.UpdatedBy,
                        pfec_sql.CreatedAt,
                        pfec_sql.CreatedBy,
                        pfec_sql.EducationalNeedCode,
                        mtd_sql.ViName,
                        mtd_sql.EnName,
                    }).OrderBy(e => e.CreatedAt).ToList();
        }
        private dynamic BuildEducationContentData(dynamic edu_form_content, string visitTypeGroup, dynamic visit, string userName = null)
        {
            Guid edu_form_content_id = edu_form_content.Id;
            var datas = unitOfWork.PatientAndFamilyEducationContentDataRepository.Find(
                e => !e.IsDeleted &&
                e.PatientAndFamilyEducationContentId != null &&
                e.PatientAndFamilyEducationContentId == edu_form_content_id
            ).Select(e => new { e.Id, e.Code, e.Value, e.EnValue }).ToList();

            var confirm_edu = (from e in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                               where !e.IsDeleted && e.FormId == edu_form_content_id
                               select e).FirstOrDefault();

            bool isLocked = false;
            if (visitTypeGroup == "OPD" && visit != null)
                isLocked = Is24hLocked(visit.CreatedAt, (Guid)visit.Id, "A03_045_290422_VE", userName, edu_form_content_id);
            else if (visitTypeGroup == "IPD" && visit != null)
                isLocked = IPDIsBlock((IPD)visit, "IPDGDSK", edu_form_content_id);

            return new
            {
                edu_form_content.Id,
                edu_form_content.CreatedBy,
                CreatedAt = edu_form_content.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                UpdatedAt = edu_form_content.UpdatedAt,
                edu_form_content.UpdatedBy,
                edu_form_content.EducationalNeedCode,
                Datas = datas,
                ConfirmCreated = new
                {
                    confirm_edu?.FormId,
                    confirm_edu?.Note,
                    confirm_edu?.ConfirmType,
                    confirm_edu?.ConfirmBy,
                    confirm_edu?.ConfirmAt
                },
                IsLocked = isLocked
            };
        }
        private dynamic BuildInformationsOfEducationForm(Guid edu_form_id)
        {
            return unitOfWork.PatientAndFamilyEducationDataRepository.Find(
                e => !e.IsDeleted &&
                e.PatientAndFamilyEducationId != null &&
                e.PatientAndFamilyEducationId == edu_form_id
            ).Select(e => new { e.Id, e.Value, e.Code }).ToList();
        }

        protected Guid GetOrCreatePatientAndFamilyEducationFormId(Guid visit_id, string visit_type_group_code, string form_str_id)
        {
            if (string.IsNullOrEmpty(form_str_id))
            {
                PatientAndFamilyEducation education_form = new PatientAndFamilyEducation()
                {
                    VisitId = visit_id,
                    VisitTypeGroupCode = visit_type_group_code,
                };
                unitOfWork.PatientAndFamilyEducationRepository.Add(education_form);
                unitOfWork.Commit();
                return education_form.Id;
            }
            return new Guid(form_str_id);
        }
        protected PatientAndFamilyEducation GetOrCreatePatientAndFamilyEducationForm(Guid visit_id, string visit_type_group_code, string form_str_id, int app_version)
        {
            if (string.IsNullOrEmpty(form_str_id))
            {
                PatientAndFamilyEducation education_form = new PatientAndFamilyEducation()
                {
                    VisitId = visit_id,
                    VisitTypeGroupCode = visit_type_group_code,
                    Version = app_version >= 7 ? app_version : 2
                };
                unitOfWork.PatientAndFamilyEducationRepository.Add(education_form);
                unitOfWork.Commit();
                return education_form;
            }

            var id = new Guid(form_str_id);
            return unitOfWork.PatientAndFamilyEducationRepository.GetById(id);
        }

        protected void HandleUpdateOrCreatePatientFamilyEducationInformations(PatientAndFamilyEducation edu_form, JToken request_datas)
        {
            var info_datas = edu_form.PatientAndFamilyEducationDatas.Where(e => !e.IsDeleted).ToList();

            foreach (var item in request_datas)
            {
                string code = item.Value<string>("Code");
                if (string.IsNullOrEmpty(item.Value<string>("Code"))) continue;

                string value = item.Value<string>("Value");
                var info = GetOrCreatePatientFamilyEducationInformation(info_datas, edu_form.Id, code, value);
                if (info != null/* && IsUserCreateFormManual(user.Username, info.CreatedBy)*/)
                    UpdatePatientFamilyEducationInformation(info, value);
            }
            var user = GetUser();
            edu_form.UpdatedBy = user.Username;
            unitOfWork.PatientAndFamilyEducationRepository.Update(edu_form);
            unitOfWork.Commit();
        }
        private PatientAndFamilyEducationData GetOrCreatePatientFamilyEducationInformation(List<PatientAndFamilyEducationData> info_datas, Guid edu_form_id, string code, string value)
        {
            PatientAndFamilyEducationData edu_info = info_datas.FirstOrDefault(
                    e => !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals(code)
                );
            if (edu_info == null)
            {
                edu_info = new PatientAndFamilyEducationData()
                {
                    PatientAndFamilyEducationId = edu_form_id,
                    Code = code,
                    Value = value,
                };
                unitOfWork.PatientAndFamilyEducationDataRepository.Add(edu_info);
            }
            return edu_info;
        }
        private bool UpdatePatientFamilyEducationInformation(PatientAndFamilyEducationData info, string value)
        {
            if (info.Value == value)
                return false;
            info.Value = value;
            unitOfWork.PatientAndFamilyEducationDataRepository.Update(info);
            return true;
        }

        protected void HandleUpdateOrCreatePatientFamilyEducationGroupContents(PatientAndFamilyEducation edu_form, JToken request_datas)
        {
            var user = GetUser();
            var group_content = request_datas.ToObject<Dictionary<string, dynamic>>();
            foreach (KeyValuePair<string, dynamic> group in group_content)
            {
                foreach (var content in group.Value.Contents)
                {
                    PatientAndFamilyEducationContent content_obj = GetOrCreatePatientFamilyEducationContent(content.Id?.ToString(), edu_form.Id, group.Key);
                    //if (IsUserCreateFormManual(user.Username, content_obj.CreatedBy))
                    HandleUpdateOrCreatePatientFamilyEducationContentDatas(content_obj, content.Datas);
                }
            }
            edu_form.UpdatedBy = user.Username;
            //unitOfWork.PatientAndFamilyEducationRepository.Update(edu_form);
            //unitOfWork.Commit();
        }
        private PatientAndFamilyEducationContent GetOrCreatePatientFamilyEducationContent(string content_str_id, Guid edu_form_id, string code)
        {
            if (string.IsNullOrEmpty(content_str_id))
            {
                var user = GetUser();
                var user_info = string.Format("{0} ({1})", user.Fullname, user.Username);
                PatientAndFamilyEducationContent edu_content = new PatientAndFamilyEducationContent()
                {
                    PatientAndFamilyEducationId = edu_form_id,
                    EducationalNeedCode = code,
                    UpdatedInfo = user_info,
                    CreatedInfo = user_info,
                };
                unitOfWork.PatientAndFamilyEducationContentRepository.Add(edu_content);
                unitOfWork.Commit();
                return edu_content;
            }
            return unitOfWork.PatientAndFamilyEducationContentRepository.GetById(new Guid(content_str_id));
        }
        private void HandleUpdateOrCreatePatientFamilyEducationContentDatas(PatientAndFamilyEducationContent edu_content, dynamic request_datas)
        {
            var content_datas = edu_content.PatientAndFamilyEducationContentDatas.Where(e => !e.IsDeleted).ToList();
            var is_change = false;
            foreach (var item in request_datas)
            {
                string code = item.Value<string>("Code");
                if (string.IsNullOrEmpty(code)) continue;

                string value = item.Value<string>("Value");
                var isnew = false;
                var content_data = GetOrCreatePatientFamilyEducationContentData(content_datas, edu_content.Id, code, value, ref isnew);
                if (isnew)
                {
                    is_change = true;
                }
                else if (isnew == false && content_data != null)
                {
                    var item_is_change = UpdatePatientFamilyEducationContentData(content_data, value);
                    if (!is_change) is_change = item_is_change;
                }

            }

            if (is_change)
            {
                var user = GetUser();
                edu_content.UpdatedBy = user.Username;
                unitOfWork.PatientAndFamilyEducationContentRepository.Update(edu_content);
                unitOfWork.Commit();
            }
        }
        private PatientAndFamilyEducationContentData GetOrCreatePatientFamilyEducationContentData(List<PatientAndFamilyEducationContentData> content_datas, Guid edu_content_id, string code, string value, ref bool isnew)
        {
            PatientAndFamilyEducationContentData content_data = content_datas.FirstOrDefault(
                    e => !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals(code)
                );
            if (content_data == null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    content_data = new PatientAndFamilyEducationContentData()
                    {
                        PatientAndFamilyEducationContentId = edu_content_id,
                        Code = code,
                        Value = value,
                    };
                    unitOfWork.PatientAndFamilyEducationContentDataRepository.Add(content_data);
                    unitOfWork.Commit();
                    isnew = true;
                }
            }
            return content_data;
        }
        private bool UpdatePatientFamilyEducationContentData(PatientAndFamilyEducationContentData edu_content_data, string value)
        {
            if (edu_content_data.Value == value)
                return false;
            edu_content_data.Value = value;
            unitOfWork.PatientAndFamilyEducationContentDataRepository.Update(edu_content_data);
            unitOfWork.Commit();
            return true;
        }
    }
}
