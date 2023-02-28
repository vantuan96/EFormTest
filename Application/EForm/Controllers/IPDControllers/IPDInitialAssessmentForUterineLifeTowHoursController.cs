using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    public class IPDInitialAssessmentForUterineLifeTowHoursController : BaseIPDApiController
    {
        private const string formCodeVersion2 = "A02_016_061022_VE";
        private readonly string visit_type = "IPD";
        private const string timeUpdate_version2 = "HIDE_A02_016_050919_VE";

        [HttpGet]
        [Route("api/IPD/InitialAssessment/ForUterineLife2HoursV2/{type}/{ipdId}/{tab}/{*id}")]
        [Permission(Code = "IPDVIEW2HOURSV2")]
        public IHttpActionResult GetInitialAssessmentForNewborns(string type, Guid ipdId, string tab, Guid? id)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            IPDInitialAssessmentForNewborns form = null;
            if (IsVisitLastTimeUpdate(visit, timeUpdate_version2))
                if (id != null)
                    form = GetForm((Guid)id);
                else
                    form = GetForm(ipdId, tab, 2);
            else
                form = GetForm(tab, ipdId);

            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Constant.IPDFormCode.DanhGiaBanDauChoTreVuaSinh));
            return Content(HttpStatusCode.OK, FormatOutput(type, tab, visit, form));
        }

        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForUterineLife2HoursV2/Create/{type}/{ipdId}/{tab}/{*formIdNewborn}")]
        [Permission(Code = "IPDCREATE2HOURSV2")]
        public IHttpActionResult CreateInitialAssessmentForNewborns(string type, Guid ipdId, string tab, Guid? formIdNewborn)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            int version = 1;
            if (IsVisitLastTimeUpdate(visit, timeUpdate_version2))
                version = 2;

            var form_data = new IPDInitialAssessmentForNewborns()
            {
                DataType = tab,
                VisitId = ipdId,
                Version = version,
                EIOConstraintNewbornAndPregnantWomanId = formIdNewborn
            };
            unitOfWork.IPDInitialAssessmentForNewbornsRepository.Add(form_data);
            UpdateVisit(visit, visit_type);
            var idForm = form_data.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, idForm);      
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }

        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForUterineLife2HoursV2/Update/{type}/{ipdId}/{tab}/{*id}")]
        [Permission(Code = "IPDUPDATE2HOURSV2")]
        public IHttpActionResult UpdateAPI(string type, Guid ipdId, string tab, [FromBody] JObject request, Guid? id)
        {
            string formCodeInFormDatas = type + '_' + tab;
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            IPDInitialAssessmentForNewborns form = null;
            if (IsVisitLastTimeUpdate(visit, timeUpdate_version2))
                if (id != null)
                    form = GetForm((Guid)id);
                else
                    form = GetForm(ipdId, tab, 2);
            else
                form = GetForm(tab, ipdId);

            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Constant.IPDFormCode.DanhGiaBanDauChoTreVuaSinh));
            //HandleTime(visit, request["Time"]);
            if (!string.IsNullOrEmpty(request["Time"]["AdmittedFrom"].ToString()))
            {
                form.DateOfAdmission = DateTime.ParseExact(request["Time"]["AdmittedFrom"].ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
            else
            {
                form.DateOfAdmission = null;
            }
            HandleUpdateOrCreateTableFormData((Guid)form.VisitId, form.Id, formCodeInFormDatas, request["Datas"]);

            if (IsVisitLastTimeUpdate(visit, timeUpdate_version2) && form.Version == 2)
            {
                string _str_form_newbornId = request["NewbornCustomerId"]?.ToString();
                if (_str_form_newbornId == null)
                    return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
                Guid form_newbornId = new Guid(_str_form_newbornId);
                form.EIOConstraintNewbornAndPregnantWomanId = form_newbornId;
            }

            unitOfWork.IPDInitialAssessmentForNewbornsRepository.Update(form);
            UpdateVisit(visit, visit_type);
            var formId = form.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }

        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForUterineLife2HoursV2/Confirm/{type}/{ipdId}/{tab}/{*id}")]
        [Permission(Code = "IPDCONFIRM2HOURSV2")]
        public IHttpActionResult ConfirmAPI(string type, Guid ipdId, string tab, [FromBody] JObject request, Guid? id)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            IPDInitialAssessmentForNewborns form = null;
            if (IsVisitLastTimeUpdate(visit, timeUpdate_version2))
                if (id != null)
                    form = GetForm((Guid)id);
                else
                    form = GetForm(ipdId, tab, 2);
            else
                form = GetForm(tab, ipdId);

            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString(), "IPDCONFIRM2HOURSV2");
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


        [HttpGet]
        [Route("api/IPD/InitialAssessment/ForUterineLife2HoursV2/GetList/{visitId}")]
        [Permission(Code = "IPDVIEW2HOURSV2")]
        public IHttpActionResult GetListFormVersion2(Guid visitId)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(visit, formCodeVersion2),
                Forms = GetListForms(visit.Id)
            });
        }

        private IPDInitialAssessmentForNewborns GetForm(string type, Guid visit_id)
        {
            return unitOfWork.IPDInitialAssessmentForNewbornsRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.DataType == type && e.Version != 2 && e.EIOConstraintNewbornAndPregnantWomanId == null).FirstOrDefault();
        }
        private dynamic FormatOutput(string formCode, string tab, IPD ipd, IPDInitialAssessmentForNewborns fprm)
        {
            string formCodeinFormDatas = formCode + '_' + tab;
            var MedicalStaff1Confirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.MedicalStaff1ConfirmId);
            var MedicalStaff2Confirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.MedicalStaff2ConfirmId);
            var FullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.CreatedBy)?.Fullname;
            var FullNameUpdate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.UpdatedBy)?.Fullname;
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
                IsLocked = IPDIsBlock(ipd, formCodeVersion2, fprm.Id),
                Confirm = new
                {
                    MedicalStaff1 = new
                    {
                        UserName = MedicalStaff1Confirm?.Username,
                        FullName = MedicalStaff1Confirm?.Fullname,
                        ConfirmAt = fprm.MedicalStaff1ConfirmAt,
                    },
                    MedicalStaff2 = new
                    {
                        UserName = MedicalStaff2Confirm?.Username,
                        FullName = MedicalStaff2Confirm?.Fullname,
                        ConfirmAt = fprm.MedicalStaff2ConfirmAt,
                    },
                },
                DataPara = GetParaFromInitialAssessment(ipd.Id, Constant.IPD_COPPY_PARA_FROM_A02_069_080121_VE),
                fprm?.DateOfAdmission,
                fprm.Version,
                NewbornCustomer = GetNewbornCustomerByFormId(fprm.EIOConstraintNewbornAndPregnantWomanId),
                SpecialtyEnName = ipd.Specialty?.EnName
            };
        }
        private bool ConfirmUser(IPDInitialAssessmentForNewborns ipdInitialAssessmentForNewborns, User user, string kind, string actionCode)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (kind.ToUpper() == "MEDICALSTAFF1" && !string.IsNullOrEmpty(GetActionOfUser(user, actionCode)) && ipdInitialAssessmentForNewborns.MedicalStaff1ConfirmId == null)
            {
                ipdInitialAssessmentForNewborns.MedicalStaff1ConfirmAt = DateTime.Now;
                ipdInitialAssessmentForNewborns.MedicalStaff1ConfirmId = user?.Id;
            }
            else if (kind.ToUpper() == "MEDICALSTAFF2" && !string.IsNullOrEmpty(GetActionOfUser(user, actionCode)) && ipdInitialAssessmentForNewborns.MedicalStaff2ConfirmId == null)
            {
                ipdInitialAssessmentForNewborns.MedicalStaff2ConfirmAt = DateTime.Now;
                ipdInitialAssessmentForNewborns.MedicalStaff2ConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.IPDInitialAssessmentForNewbornsRepository.Update(ipdInitialAssessmentForNewborns);
            unitOfWork.Commit();
            return true;
        }

        public void HandleTime(IPD visit, JToken data_request)
        {
            if (data_request != null)
            {
                if (!string.IsNullOrEmpty(data_request["AdmittedFrom"].ToString()))
                {
                    try
                    {
                        var data = visit.IPDInitialAssessmentForAdult?.IPDInitialAssessmentForAdultDatas?.FirstOrDefault(x => x.Code == "IPDIAAUARTIANS");
                        if (data != null)
                        {
                            data.Value = DateTime.ParseExact(data_request["AdmittedFrom"].ToString(), "yyyy-MM-dd HH:mm:ss.fff", null).ToString("HH:mm dd/MM/yyyy");
                            unitOfWork.Commit();
                        }
                        visit.AdmittedDate = DateTime.ParseExact(data_request["AdmittedFrom"].ToString(), "yyyy-MM-dd HH:mm:ss.fff", null);
                        unitOfWork.IPDRepository.Update(visit);
                        unitOfWork.Commit();
                    }
                    catch (Exception ex) { }
                }
            }
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

        private dynamic GetParaFromInitialAssessment(Guid visit_id, string[] code)
        {
            var initial = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                           .Where(
                                d => !d.IsDeleted
                                && d.VisitId == visit_id
                                && d.FormCode == "A02_069_080121_VE"
                            ).FirstOrDefault();
            if (initial == null)
                return null;
            var initial_Id = initial.FormId;
            var master = (from mas in unitOfWork.MasterDataRepository.AsQueryable()
                          where !mas.IsDeleted
                          && !string.IsNullOrEmpty(mas.Code)
                          && code.Contains(mas.Code)
                          join data in unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                          .Where(
                                  d => !d.IsDeleted && d.IPDInitialAssessmentForAdultId == initial_Id
                                  && !string.IsNullOrEmpty(d.Code) && code.Contains(d.Code)
                                 )
                          on mas.Code equals data.Code into tempData
                          from datas in tempData.DefaultIfEmpty()
                          select new
                          {
                              mas.ViName,
                              mas.EnName,
                              mas.Code,
                              datas.Value,
                              mas.Order
                          }).OrderBy(o => o.Order).ToList();

            var timeDate = unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                          .Where(
                                t => !t.IsDeleted
                                && t.IPDInitialAssessmentForAdultId == initial_Id
                                && t.Code == "IPDIAAUSPPIES2"
                            )
                          .FirstOrDefault();

            return new { Datas = master, UpdateBy = initial.UpdatedBy, UpdateAt = initial.UpdatedAt.Value.ToString("HH:mm"), Year = timeDate?.Value };
        }

        private List<dynamic> GetListForms(Guid visitId)
        {
            const string formCode = "ForUterineLife2Hours_Obstetrics";
            var forms = (from e in unitOfWork.IPDInitialAssessmentForNewbornsRepository.AsQueryable()
                         join n in unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.AsQueryable()
                         on e.EIOConstraintNewbornAndPregnantWomanId equals n.Id
                         where !e.IsDeleted && e.VisitId == visitId && e.DataType.ToUpper() == formCode.ToUpper()
                         && n.VisitId == visitId && n.FormCode.ToUpper() == formCode.ToUpper()
                         && e.EIOConstraintNewbornAndPregnantWomanId != null
                         orderby e.CreatedAt
                         select new
                         {
                             Id = e.Id,
                             e.CreatedAt,
                             e.CreatedBy,
                             e.UpdatedAt,
                             e.UpdatedBy,
                             NewbornCustomer = new
                             {
                                 Id = n.Id,
                                 FullName = n.NewbornCustomer == null ? null : n.NewbornCustomer.Fullname,
                                 Gender = n.NewbornCustomer == null ? null : n.NewbornCustomer.Gender,
                                 DateOfBirth = n.NewbornCustomer == null ? null : n.NewbornCustomer.DateOfBirth,
                                 PID = n.NewbornCustomer == null ? null : n.NewbornCustomer.PID
                             }
                         }).ToList();

            return new List<dynamic>(forms);
        }
        private dynamic GetNewbornCustomerByFormId(Guid? eioContraintNewbornId)
        {
            if (eioContraintNewbornId == null)
                return null;

            var newbornCustomer = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository
                                 .FirstOrDefault(e => !e.IsDeleted && e.Id == eioContraintNewbornId);
            if (newbornCustomer == null)
                return null;

            return new
            {
                newbornCustomer.Id,
                newbornCustomer.NewbornCustomer?.Fullname,
                newbornCustomer.NewbornCustomer?.Gender,
                newbornCustomer.NewbornCustomer?.DateOfBirth,
                newbornCustomer.NewbornCustomer?.PID
            };
        }
        private IPDInitialAssessmentForNewborns GetForm(Guid formId)
        {
            return unitOfWork.IPDInitialAssessmentForNewbornsRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
        }
        private IPDInitialAssessmentForNewborns GetForm(Guid visit_id, string type, int version)
        {
            return unitOfWork.IPDInitialAssessmentForNewbornsRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.DataType == type && e.Version == version && e.EIOConstraintNewbornAndPregnantWomanId == null).FirstOrDefault();
        }
    }
}
