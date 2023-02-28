using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDInitialAssessmentController : BaseMedicalRecodeApiController
    {
        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/Delete/{id}")]
        [Permission(Code = "IINAS1")]
        public IHttpActionResult DeleteVisitAPI(Guid id, [FromBody]DeleteMedicalRecord request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            
            var user = GetUser();
            if (ipd.IPDInitialAssessmentForAdult == null || GetMedicalRecordOfPatients("A02_013_220321_VE", ipd.Id, ipd.IPDInitialAssessmentForAdultId)?.CreatedBy != user.Username)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            if (string.IsNullOrWhiteSpace(request.Note)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            
            // is Hồ sơ nháp
            // chưa hoàn thành BA
            //
     
            if (IsHasForm(ipd))
            {
                return Content(HttpStatusCode.BadRequest, Message.VISIT_HAS_FORM);
            }
            if (ipd.IsTransfer && ipd.TransferFromId != null)
            {
                bool IsAccepted = !revertHanoverChecklis((Guid)ipd.TransferFromId);
                if (IsAccepted) return Content(HttpStatusCode.BadRequest, Message.VISIT_FORBIDDEN);
            }
            unitOfWork.IPDRepository.Delete(ipd);
            try
            {
                unitOfWork.IPDInitialAssessmentForAdultRepository.Delete(ipd.IPDInitialAssessmentForAdult);
            }
            catch (Exception) { }
            try
            {
                unitOfWork.IPDInitialAssessmentForChemotherapyRepository.Delete(ipd.IPDInitialAssessmentForChemotherapy);
            }
            catch (Exception) { }
            try
            {
                unitOfWork.IPDInitialAssessmentForFrailElderlyRepository.Delete(ipd.IPDInitialAssessmentForFrailElderly);
            }
            catch (Exception) { }
            unitOfWork.Commit();
            setLog(new Log
            {
                Action = "DELETE IPD",
                URI = id.ToString(),
                Name = "DELETE IPD",
                Request = id.ToString(),
                Reason = request.Note,
            });
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private bool IsHasForm(IPD ipd)
        {
            var listform = GetFormsIPD(ipd);
            return listform.Any(e => e.Type != "InitialAssessmentForAudult" && e.Datas != null && e.Datas.Count() > 0);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/InitialAssessment/ForAdult/Create/{Type}/{id}")]
        [Permission(Code = "IIAAU1")]
        public IHttpActionResult CreateIPDInitialAssessmentForAdultAPI(string Type, Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var checkMedical = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                               .Where(m => m.VisitId == id && m.FormCode == Type && !m.IsDeleted)
                               .FirstOrDefault();
            if(checkMedical != null && ipd.IPDInitialAssessmentForAdultId != null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IAAU_EXIST);

            if(ipd.IPDInitialAssessmentForAdultId == null)
                CreateIPDInitialAssessmentForAdult(ipd);

            var initialAssessmentId = ipd.IPDInitialAssessmentForAdultId;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(ipd, Type, initialAssessmentId);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/InitialAssessment/ForAdult/{Type}/{id}")]
        [Permission(Code = "IIAAU2")]
        public IHttpActionResult GetIPDInitialAssessmentForAdultAPI(string Type, Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var is_block_24h = IPDIsBlock(ipd, Type);
            var ia = ipd.IPDInitialAssessmentForAdult;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Đánh giá ban đầu người bệnh ngoại trú thông thường không tồn tại",
                    EnMessage = "Initial assessment for adult inpatient is not found",
                    IsLocked = is_block_24h
                });
            var checkRecord = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                             .Where(r => r.VisitId == id && r.FormCode == Type && !r.IsDeleted)
                             .FirstOrDefault();
          
            if (checkRecord == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Đánh giá ban đầu người bệnh ngoại trú thông thường không tồn tại",
                    EnMessage = "Initial assessment for adult inpatient is not found",
                    IsLocked = is_block_24h
                }); 

            var datas = ia.IPDInitialAssessmentForAdultDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });

            return Content(HttpStatusCode.OK, BuildInitialAssessmentResult(ipd, ia, datas, Type));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForAdult/{Type}/{id}")]
        [Permission(Code = "IIAAU3")]
        public IHttpActionResult UpdateIPDInitialAssessmentForAdultAPI(string Type, Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForAdult;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IAAU_NOT_FOUND);

            var ipdId = ipd.Id;
            var recordOfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(m => !m.IsDeleted && m.VisitId == ipdId && m.FormCode == Type);

            if (recordOfPatient == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IAAU_NOT_FOUND);

            var user = GetUser();
            if (user == null || user.Username != recordOfPatient.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            var formId = recordOfPatient.FormId;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(ipd, Type, formId);
            HandleDeleteSibling(request["SiblingDelete"]);
            HandleUpdateOrCreateSibling(ipd.Id, request["Siblings"]);
            HandleUpdateOrCreateInitialAssessmentForAdultData(ipd, ia, Type, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForAdult/SyncVitalSign/{id}")]
        [Permission(Code = "IIAAU4")]
        public IHttpActionResult SyncVitalSignAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForAdult;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IAAU_NOT_FOUND);

            var closest_visit = new ClosestVisitIn24H(ipd.CustomerId, ipd.AdmittedDate, unitOfWork);
            var vital_sign = closest_visit.GetVitalSign();
            if (vital_sign != null)
                return Content(HttpStatusCode.OK, vital_sign);
            return Content(HttpStatusCode.OK, Message.DATA_NOT_FOUND);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForAdult/SyncDiseasesScreening/{id}")]
        [Permission(Code = "IIAAU5")]
        public IHttpActionResult SyncDiseasesScreeningAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForAdult;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IAAU_NOT_FOUND);

            var closest_visit = new ClosestVisitIn24H(ipd.CustomerId, ipd.AdmittedDate, unitOfWork);
            var diseases_screening = closest_visit.GetDiseasesScreening();
            if (diseases_screening != null)
                return Content(HttpStatusCode.OK, diseases_screening);
            return Content(HttpStatusCode.OK, Message.DATA_NOT_FOUND);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForAdult/Sync/{id}")]
        [Permission(Code = "IIAAU6")]
        public IHttpActionResult SyncIPDInitialAssessmentForAdultAPI(Guid id)
        {
            var current_ipd = GetIPD(id);
            if (current_ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if(current_ipd.TransferFromId == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.HandOverCheckListId != null &&
                e.HandOverCheckListId == current_ipd.TransferFromId
            );
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForAdult;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var datas = ia.IPDInitialAssessmentForAdultDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });

            return Content(HttpStatusCode.OK, BuildInitialAssessmentResult(ipd, ia, datas));
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/InitialAssessment/ForChemotherapy/Create/{id}")]
        [Permission(Code = "IIACP1")]
        public IHttpActionResult CreateIPDInitialAssessmentForChemotherapyAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (ipd.IPDInitialAssessmentForChemotherapyId != null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IACP_EXIST);

            CreateIPDInitialAssessmentForChemotherapy(ipd);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/InitialAssessment/ForChemotherapy/{id}")]
        [Permission(Code = "IIACP2")]
        public IHttpActionResult GetIPDInitialAssessmentForChemotherapyAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForChemotherapy;
            string formCode = "A02_011_080121_VE";            
            if (ia == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Đánh giá ban đầu người bệnh truyền hóa chất không tồn tại",
                    EnMessage = "Initial assessment for chemotherapy patient is not found",
                    IsLocked = IPDIsBlock(ipd, formCode)
                });

            var datas = ia.IPDInitialAssessmentForChemotherapyDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });
           
         
            return Content(HttpStatusCode.OK, BuildInitialAssessmentResult(ipd, ia, datas, formCode));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForChemotherapy/{id}")]
        [Permission(Code = "IIACP3")]
        public IHttpActionResult UpdateIPDInitialAssessmentForChemotherapyAPI(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForChemotherapy;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IACP_NOT_FOUND);

            var user = GetUser();
            if (user == null || user.Username != ia.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateInitialAssessmentForChemotherapyData(ipd, ia, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForChemotherapy/SynsGeneralAssessment/{id}")]
        [Permission(Code = "IIACP4")]
        public IHttpActionResult SyncGeneralAssessmentAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForChemotherapy;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IACP_NOT_FOUND);

            var general_assessment = GetClosestGeneralAssessment(ipd.CustomerId, ipd.AdmittedDate);
            if (general_assessment != null)
                return Content(HttpStatusCode.OK, general_assessment);
            return Content(HttpStatusCode.OK, Message.DATA_NOT_FOUND);
        }


        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForChemotherapy/Sync/{id}")]
        [Permission(Code = "IIACP5")]
        public IHttpActionResult SyncIPDInitialAssessmentForChemotherapyAPI(Guid id)
        {
            var current_ipd = GetIPD(id);
            if (current_ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (current_ipd.TransferFromId == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.HandOverCheckListId != null &&
                e.HandOverCheckListId == current_ipd.TransferFromId
            );
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForChemotherapy;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var datas = ia.IPDInitialAssessmentForChemotherapyDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });

            return Content(HttpStatusCode.OK, BuildInitialAssessmentResult(ipd, ia, datas));
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/InitialAssessment/ForFrailElderly/Create/{id}")]
        [Permission(Code = "IIAFE1")]
        public IHttpActionResult CreateIPDInitialAssessmentForFrailElderlyAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (ipd.IPDInitialAssessmentForFrailElderlyId != null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IAFE_EXIST);

            CreateIPDInitialAssessmentForFrailElderly(ipd);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/InitialAssessment/ForFrailElderly/{id}")]
        [Permission(Code = "IIAFE2")]
        public IHttpActionResult GetIPDInitialAssessmentForForFrailElderlyAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForFrailElderly;
            string formCode = "A03_012_080121_VE";            
            if (ia == null)
                return Content(HttpStatusCode.NotFound, new
                    {
                        ViMessage = "Đánh giá ban đầu người bệnh cao tuổi, già yếu/cuối đời không tồn tại",
                        EnMessage = "Initial assessment for frail elderly/ end-of-life patient is not found",
                        IsLocked = IPDIsBlock(ipd, formCode)
                    }
                );

            var datas = ia.IPDInitialAssessmentForFrailElderlyDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });
            
             
            return Content(HttpStatusCode.OK, BuildInitialAssessmentResult(ipd, ia, datas, formCode));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForFrailElderly/{id}")]
        [Permission(Code = "IIAFE3")]
        public IHttpActionResult UpdateIPDInitialAssessmentForFrailElderlyAPI(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForFrailElderly;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_IAFE_NOT_FOUND);

            var user = GetUser();
            if (user == null || user.Username != ia.CreatedBy)
                return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateOrCreateInitialAssessmentForFrailElderlyData(ipd, ia, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/InitialAssessment/ForFrailElderly/Sync/{id}")]
        [Permission(Code = "IIAFE4")]
        public IHttpActionResult SyncIPDInitialAssessmentForFrailElderlyAPI(Guid id)
        {
            var current_ipd = GetIPD(id);
            if (current_ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (current_ipd.TransferFromId == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.HandOverCheckListId != null &&
                e.HandOverCheckListId == current_ipd.TransferFromId
            );
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var ia = ipd.IPDInitialAssessmentForFrailElderly;
            if (ia == null)
                return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

            var datas = ia.IPDInitialAssessmentForFrailElderlyDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });

            return Content(HttpStatusCode.OK, BuildInitialAssessmentResult(ipd, ia, datas));
        }

        private dynamic BuildInitialAssessmentResult(IPD ipd, dynamic ia, dynamic datas, string formCode = null)
        {
            string username = ia.CreatedBy;
            string CreatedBy = ia.CreatedBy;
            var user = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username == username
            );
            var spec = ipd.Specialty;
            var ipdId = ipd.Id;
            IPDMedicalRecordOfPatients record = null;
            User userUpdate = null;
            if (formCode != null)
            {
                record = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                         .Where(i => i.VisitId == ipdId && i.FormCode == formCode)
                         .FirstOrDefault();

                string userName = record?.UpdatedBy;
                userUpdate = unitOfWork.UserRepository.FirstOrDefault(
                    u => !u.IsDeleted && !string.IsNullOrEmpty(u.Username)
                    && u.Username == userName
                    );
            }
            if (record != null)
            {
                CreatedBy = record?.CreatedBy;
            }
            string updatedByForFrailElderly = ipd?.IPDInitialAssessmentForFrailElderly?.UpdatedBy;
            User userUpdateForFrailElderly = null;
            if (!string.IsNullOrEmpty(updatedByForFrailElderly))
            {
                userUpdateForFrailElderly = unitOfWork.UserRepository.FirstOrDefault(
                    u => !u.IsDeleted && !string.IsNullOrEmpty(u.Username)
                    && u.Username == updatedByForFrailElderly
                    );
            }
            Guid? formId = null;
            if(ipd.Version >= 11)
            {
                switch (formCode)
                {
                    case "A03_012_080121_VE":
                        formId = ipd?.IPDInitialAssessmentForFrailElderlyId;
                        break;
                    case "A02_011_080121_VE":
                        formId = ipd?.IPDInitialAssessmentForChemotherapyId;
                        break;
                    default:
                        formId = record?.Id;
                        break;
                }
            }
            
            bool isLock24h = IPDIsBlock(ipd, string.IsNullOrEmpty(formCode) ? Constant.IPDFormCode.DanhGiaBanDau : formCode, formId: formId);
            return new
            {
                ia.Version,
                CreatedBy,
                Specialty = new { spec?.ViName, spec?.EnName },
                ipd.Customer.Gender,
                UpdatedBy = new { userUpdate?.Username, userUpdate?.Fullname, userUpdate?.DisplayName, userUpdate?.Title },
                UpdatedAt = record?.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Datas = datas,
                IsNew = IsNew(ia.CreatedAt, ia.UpdatedAt),
                IsLocked = isLock24h,
                Siblings = unitOfWork.IPDSiblingRepository.Find(e => e.VisitId == ipd.Id && !e.IsDeleted).OrderBy(x=>x.Order).Select(s => new
                {
                    s.Id,
                    s.Age,
                    s.Gender,
                    s.Order
                }).ToList(),
                DataPara = GetDataParaFromInitialNewborn(ipd.Id, Constant.IPD_COPPY_PARA_FROM_A02_016_050919_VE),
                VisitType = ipd.VisitType,
                IsExitTheOncologyMedicalRecord = IsExitA01_196_050919_V(spec, ipd),
                Id = record?.Id,
                InfoForFrailElderly = new
                {
                    FormId = ipd?.IPDInitialAssessmentForFrailElderlyId,
                    CreatedAt = ipd.IPDInitialAssessmentForFrailElderly?.CreatedAt,
                    CreatedBy = ipd.IPDInitialAssessmentForFrailElderly?.CreatedBy,
                    UpdatedAt = ipd.IPDInitialAssessmentForFrailElderly?.UpdatedAt,
                    UpdatedBy = ipd.IPDInitialAssessmentForFrailElderly?.UpdatedBy,
                    Version = ipd.IPDInitialAssessmentForFrailElderly?.Version,
                },
                InfoForChemotherapy = new
                {
                    FormId = ipd?.IPDInitialAssessmentForChemotherapyId,
                    CreatedAt = ipd.IPDInitialAssessmentForChemotherapy?.CreatedAt,
                    CreatedBy = ipd.IPDInitialAssessmentForChemotherapy?.CreatedBy,
                    UpdatedAt = ipd.IPDInitialAssessmentForChemotherapy?.UpdatedAt,
                    UpdatedBy = ipd.IPDInitialAssessmentForChemotherapy?.UpdatedBy,
                    Version = ipd.IPDInitialAssessmentForChemotherapy?.Version,
                },
                VersionApp = ipd.Version
            };
        }

        private bool IsExitA01_196_050919_V(Specialty specialty, IPD visit)
        {
            const string formCode = "A01_196_050919_V";
            var getSetup = (from s in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                            where s.SpecialityId == specialty.Id && s.Formcode == formCode
                            && s.IsDeploy == true
                            select s).FirstOrDefault();
            if (getSetup != null)
                return true;

            var getOfpatient = (from p in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                where !p.IsDeleted && p.VisitId == visit.Id
                                && p.FormCode == formCode
                                select p).FirstOrDefault();
            if (getOfpatient != null)
                return true;

            return false;
        }

        private dynamic GetDataParaFromInitialNewborn(Guid visit_Id, string[] codes)
        {
            var getForm = unitOfWork.IPDInitialAssessmentForNewbornsRepository
                         .FirstOrDefault(
                                s => !s.IsDeleted
                                && s.VisitId == visit_Id
                                && s.DataType == "ForNeonatalMaternity"
                          );
            if (getForm == null)
                return null;
            var datas_master = (from mas in unitOfWork.MasterDataRepository.AsQueryable()
                                where !mas.IsDeleted && !string.IsNullOrEmpty(mas.Code)
                                      && codes.Contains(mas.Code)
                                join data in unitOfWork.FormDatasRepository.AsQueryable()
                                .Where(
                                      d => !d.IsDeleted
                                      && !string.IsNullOrEmpty(d.Code)
                                      && codes.Contains(d.Code)
                                      && d.FormId == getForm.Id
                                 )
                                on mas.Code equals data.Code into tempDatas
                                from datas in tempDatas.DefaultIfEmpty()
                                select new
                                {
                                    mas.ViName,
                                    mas.EnName,
                                    mas.Code,
                                    datas.Value,
                                    mas.Order
                                }).OrderBy(o => o.Order).ToList();

            return new { Datas = datas_master, UpdateBy = getForm.UpdatedBy, UpdateAt = getForm.UpdatedAt.Value.ToString("HH:mm") };
        }
        #region Adult 
        private void CreateIPDInitialAssessmentForAdult(IPD ipd)
        {
            var ia = new IPDInitialAssessmentForAdult();
            ia.Version = ipd.Version >= 9 ? ipd.Version : 2;
            unitOfWork.IPDInitialAssessmentForAdultRepository.Add(ia);

            CreateAdmittedFrom(ipd, ia.Id);

            var customer = ipd.Customer;
            if (customer != null)
            {
                CreateIPDInitialAssessmentForAdultData(ia.Id, "IPDIAAUPTCIANS", customer.Relationship);
                CreateIPDInitialAssessmentForAdultData(ia.Id, "IPDIAAUPHONANS", customer.RelationshipContact);

                if (customer.IsAllergy)
                {
                    CreateIPDInitialAssessmentForAdultData(ia.Id, "IPDIAAUALLEYES", "True");
                    CreateIPDInitialAssessmentForAdultData(ia.Id, "IPDIAAUALLEANS", customer.Allergy);
                    CreateIPDInitialAssessmentForAdultData(ia.Id, "IPDIAAUALLEKOA", customer.KindOfAllergy);
                }
                else
                {
                    CreateIPDInitialAssessmentForAdultData(ia.Id, "IPDIAAUALLENOO", "True");
                }
            }

            CreateIPDInitialAssessmentForAdultData(ia.Id, "IPDIAAUARTIANS", ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND));

            ipd.IPDInitialAssessmentForAdultId = ia.Id;
            ipd.PrimaryNurseId = GetUser()?.Id;
            unitOfWork.IPDRepository.Update(ipd);
            unitOfWork.Commit();
        }

        private void CreateAdmittedFrom(IPD ipd, Guid ia_id)
        {
            if (ipd.IsTransfer && ipd.TransferFromId != null)
            {
                var hocl_ed = unitOfWork.HandOverCheckListRepository.GetById((Guid)ipd.TransferFromId);
                if (hocl_ed != null)
                {
                    CreateIPDInitialAssessmentForAdultData(ia_id, "IPDIAAUADFRAAE", "True");
                    return;
                }

                var hocl_opd = unitOfWork.OPDHandOverCheckListRepository.GetById((Guid)ipd.TransferFromId);
                if (hocl_opd != null)
                {
                    CreateIPDInitialAssessmentForAdultData(ia_id, "IPDIAAUADFRCLI", "True");
                    return;
                }

                var hocl_ipd = unitOfWork.IPDHandOverCheckListRepository.GetById((Guid)ipd.TransferFromId);
                if (hocl_ipd != null)
                {
                    var spec = hocl_ipd.HandOverUnitPhysician;
                    var spec_name = spec?.ViName;
                    CreateIPDInitialAssessmentForAdultData(ia_id, "IPDIAAUADFROTH", "True");
                    CreateIPDInitialAssessmentForAdultData(ia_id, "IPDIAAUADFRANS", spec_name);
                    return;
                }
            }
            
            CreateIPDInitialAssessmentForAdultData(ia_id, "IPDIAAUADFROTH", "True");
        }
        private void HandleUpdateOrCreateInitialAssessmentForAdultData(IPD ipd, IPDInitialAssessmentForAdult ia,string type, JToken request)
        {
            string formCode = "IPDIAAU";
            if (type.ToUpper() == Constant.IPDFormCode.DanhGiaBanDauSanPhuChuyenDa.ToUpper())
            {
                formCode = "IPDIAAUSPP";
            }
            var datas = (from master_sql in unitOfWork.MasterDataRepository.AsQueryable().Where(e => !e.IsDeleted && e.Form == formCode.ToUpper())
                         join data_sql in unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                            .Where(e => !e.IsDeleted && e.IPDInitialAssessmentForAdultId == ia.Id)
                            on master_sql.Code equals data_sql.Code 
                            into d_list from data_sql in d_list.DefaultIfEmpty()
                         select new IPDIAMasterForAdultData
                         {
                            Data = data_sql,
                            Code = master_sql.Code,
                            Group = master_sql.Group,
                            Order = master_sql.Order,
                            Note = master_sql.Note,
                            ViName = master_sql.ViName,
                            EnName = master_sql.EnName,
                            Value = data_sql.Value,
                            DataType = master_sql.DataType
                         }).ToList();

            var special_requests = unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd.Id
            ).ToList();

            var customer_util = new CustomerUtil(unitOfWork, ipd.Customer);
            var allergy_dct = new Dictionary<string, string>();
            foreach (var item in request)
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code))continue;
                var value = item["Value"]?.ToString();

                if (Constant.IPD_IAAU_ALLERGIC_CODE.Contains(code))
                    allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;

                var ia_master = GetOrCreateIPDInitialAssessmentForAdultData(ia.Id, code, datas);
                if (ia_master == null) continue;

                UpdateIPDInitialAssessmentForAdultData(ipd, customer_util, ia_master, value, special_requests);
            }
            var visit_util = new VisitAllergy(ipd);
            visit_util.UpdateAllergy(allergy_dct);
            unitOfWork.IPDInitialAssessmentForAdultRepository.Update(ia);
            unitOfWork.Commit();
        }
        private void CreateIPDInitialAssessmentForAdultData(Guid form_id, string code, string value)
        {
            var data = new IPDInitialAssessmentForAdultData
            {
                IPDInitialAssessmentForAdultId = form_id,
                Code = code,
                Value = value
            };
            unitOfWork.IPDInitialAssessmentForAdultDataRepository.Add(data);
        }
        private IPDIAMasterForAdultData GetOrCreateIPDInitialAssessmentForAdultData(Guid form_id, string code, List<IPDIAMasterForAdultData> datas)
        {
            var ia_master = datas.FirstOrDefault(e => e.Code == code);
            if (ia_master == null || ia_master.Data != null) return ia_master;


            IPDInitialAssessmentForAdultData new_data = new IPDInitialAssessmentForAdultData
            {
                IPDInitialAssessmentForAdultId = form_id,
                Code = code,
            };
            unitOfWork.IPDInitialAssessmentForAdultDataRepository.Add(new_data);
            ia_master.Data = new_data;
            return ia_master;
        }
        private void UpdateIPDInitialAssessmentForAdultData(IPD ipd, CustomerUtil customer_util, IPDIAMasterForAdultData ia_master, string value, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            var data = ia_master.Data;
            data.Value = value;
            unitOfWork.IPDInitialAssessmentForAdultDataRepository.Update(data);

            switch (ia_master.Code)
            {
                case "IPDIAAUARTIANS":
                    if (!string.IsNullOrEmpty(value))
                        UpdateAdmittedDate(ipd, value);
                    break;

                case "IPDIAAUPTCIANS":
                    if (!string.IsNullOrEmpty(value))
                        customer_util.UpdateRelationship(value);
                    break;

                case "IPDIAAUPHONANS":
                    if (!string.IsNullOrEmpty(value))
                        customer_util.UpdateRelationshipContact(value);
                    break;

                case "IPDIAAUHEIGANS":
                    if (!string.IsNullOrEmpty(value))
                        customer_util.UpdateHeight(value);
                    break;

                case "IPDIAAUWEIGANS":
                    if (!string.IsNullOrEmpty(value))
                        customer_util.UpdateWeight(value);
                    break;

                case "IPDIAAUEDEMYES":
                case "IPDIAAUREASDIS":
                case "IPDIAAUCOLOCYA":
                case "IPDIAAUMESTDRO":
                case "IPDIAAUMESTUNC":
                case "IPDIAAUPSASUNC":
                case "IPDIAAUDIETSPE":
                case "IPDIAAUSWALIMFL":
                case "IPDIAAUSWALIMSO":
                case "IPDIAAUFEROTUF":
                case "IPDIAAUBOHAINC":
                case "IPDIAAUBOHACON":
                case "IPDIAAUURASURC":
                case "IPDIAAUURASINC":
                    HandleSpecialRequestChooseKey(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUITV10IAS":
                case "IPDIAAUNEIDANS":
                    HandleSpecialRequestTextKey(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUMESTRTC":
                case "IPDIAAUMESTRTP":
                case "IPDIAAUMESTOTH":
                    HandleSpecialRequestChooseOption(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUMESTANS":
                case "IPDIAAUDIETANS":
                case "IPDIAAUEDEMIYP":
                case "IPDIAAUDIETOTH":
                    HandleSpecialRequestTextOption(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUEADRANS4":
                case "IPDIAAUHEARANS4":
                case "IPDIAAUTALKANS4":
                case "IPDIAAUVISIANS4":
                case "IPDIAAUDRESANS4":
                case "IPDIAAUPEHYANS4":
                case "IPDIAAUWALKANS4":
                case "IPDIAAUTAMEANS4":
                    HandleSpecialRequestChooseLevel4(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUPASCANS":
                    HandleSpecialRequestTextRange(value, 3, ia_master, ipd.Id, special_requests);
                        DeleteSpecialRequest(ia_master.Code, special_requests);
                    break;

                case "IPDIAAUPASCSEL":
                    HandleSpecialRequestSelectOption(value, "IPDIAAUPASCSEL", ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUALLEYES":
                    HandleSpecialRequestChooseKey(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUALLEKOA":
                    HandleSpecialRequestSelectOption(value, "IPDIAAUALLEKOA", ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUALLEANS":
                    HandleSpecialRequestTextOption(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUWLOGYES":
                    HandleSpecialRequestChooseKey(value, "IPDIAAUAAAA", ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAURESOUNK":
                    HandleSpecialRequestChooseOption(value, "IPDIAAUAAAA", ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUHOMAANS":
                case "IPDIAAURESOANS":
                    HandleSpecialRequestTextOption(value, "IPDIAAUAAAA", ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAAUNBDYYES":
                    if (!string.IsNullOrEmpty(value) && value.ToLower().Equals("true"))
                        UpdatePermissionForVisitor(ipd, true);
                    break;

                case "IPDIAAUNBDYNOO":
                    if (!string.IsNullOrEmpty(value) && value.ToLower().Equals("true"))
                        UpdatePermissionForVisitor(ipd, false);
                    break;

                default:
                    break;
            }
        }
        private void UpdatePermissionForVisitor(IPD ipd, bool value)
        {
            ipd.PermissionForVisitor = value;
            unitOfWork.IPDRepository.Update(ipd);
        }
        private void UpdateAdmittedDate(IPD ipd, string s_date)
        {
            try
            {
                ipd.AdmittedDate = DateTime.ParseExact(s_date, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                unitOfWork.IPDRepository.Update(ipd);
            }
            catch (Exception) { }
        }
        #endregion

        #region Chemotherapy
        private void CreateIPDInitialAssessmentForChemotherapy(IPD ipd)
        {
            var ia = new IPDInitialAssessmentForChemotherapy();
            unitOfWork.IPDInitialAssessmentForChemotherapyRepository.Add(ia);

            ipd.IPDInitialAssessmentForChemotherapyId = ia.Id;
            unitOfWork.IPDRepository.Update(ipd);
            unitOfWork.Commit();
        }
        private void HandleUpdateOrCreateInitialAssessmentForChemotherapyData(IPD ipd, IPDInitialAssessmentForChemotherapy ia, JToken request)
        {
            var datas = (from master_sql in unitOfWork.MasterDataRepository.AsQueryable().Where(e => !e.IsDeleted && e.Form == "IPDIACP")
                         join data_sql in unitOfWork.IPDInitialAssessmentForChemotherapyDataRepository.AsQueryable()
                            .Where(e => !e.IsDeleted && e.IPDInitialAssessmentForChemotherapyId == ia.Id)
                            on master_sql.Code equals data_sql.Code
                            into d_list
                         from data_sql in d_list.DefaultIfEmpty()
                         select new IPDIAMasterForChemotherapyData
                         {
                             Data = data_sql,
                             Code = master_sql.Code,
                             Group = master_sql.Group,
                             Order = master_sql.Order,
                             Note = master_sql.Note,
                             ViName = master_sql.ViName,
                             EnName = master_sql.EnName,
                             Value = data_sql.Value,
                             DataType = master_sql.DataType
                         }).ToList();

            var special_requests = unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd.Id
            ).ToList();

            foreach (var item in request)
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateIPDInitialAssessmentForChemotherapyData(ia.Id, code, datas);
                if (data == null) continue;

                UpdateIPDInitialAssessmentForChemotherapyData(ipd, data, item["Value"]?.ToString(), special_requests);
            }
            unitOfWork.IPDInitialAssessmentForChemotherapyRepository.Update(ia);
            unitOfWork.Commit();
        }
        private IPDIAMasterForChemotherapyData GetOrCreateIPDInitialAssessmentForChemotherapyData(Guid form_id, string code, List<IPDIAMasterForChemotherapyData> datas)
        {
            var ia_master = datas.FirstOrDefault(e => e.Code == code);
            var x = ia_master.Data;
            if (ia_master == null || ia_master.Data != null) return ia_master;


            IPDInitialAssessmentForChemotherapyData new_data = new IPDInitialAssessmentForChemotherapyData
            {
                IPDInitialAssessmentForChemotherapyId = form_id,
                Code = code,
            };
            unitOfWork.IPDInitialAssessmentForChemotherapyDataRepository.Add(new_data);
            ia_master.Data = new_data;
            return ia_master;
        }
        private void UpdateIPDInitialAssessmentForChemotherapyData(IPD ipd, IPDIAMasterForChemotherapyData ia_master, string value, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            var data = ia_master.Data;
            data.Value = value;
            unitOfWork.IPDInitialAssessmentForChemotherapyDataRepository.Update(data);

            switch (ia_master.Code)
            {
                case "IPDIACPEATTNOO":
                case "IPDIACPCONDNES":
                case "IPDIACPCONDCOD":
                case "IPDIACPSKCOABN":
                case "IPDIACPHEENABN":
                case "IPDIACPTWDIYES":
                case "IPDIACPHBTCYES":
                case "IPDIACPANORYES":
                case "IPDIACPCTPTYES":
                case "IPDIACPDIARYES":
                case "IPDIACPDMOMYES":
                case "IPDIACPNAUSYES":
                case "IPDIACPVOMIYES":
                case "IPDIACPALHLYES":
                case "IPDIACPNRTXYES":
                case "IPDIACPNTPNYES":
                    HandleSpecialRequestChooseKey(value, ia_master, ipd.Id, special_requests);
                    break;

                default:
                    break;
            }
        }
        private object GetClosestGeneralAssessment(Guid? customer_id, DateTime admitted_date)
        {
            var ipd = GetClosestIPD(customer_id, admitted_date);
            if (ipd == null) return null;

            string[] list_code = new string[] {
                "IPDIACPONDIANS",
                "IPDIACPDAODANS",
                "IPDIACPOCIAANS",
            };
            return unitOfWork.IPDInitialAssessmentForChemotherapyDataRepository.Find(
                e => !e.IsDeleted &&
                e.IPDInitialAssessmentForChemotherapyId != null &&
                e.IPDInitialAssessmentForChemotherapyId == ipd.IPDInitialAssessmentForChemotherapyId &&
                list_code.Contains(e.Code)
            ).Select(e => new { e.Code, e.Value });
        }
        private IPD GetClosestIPD(Guid? customer_id, DateTime admitted_date)
        {
            var ipd = unitOfWork.IPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.IPDInitialAssessmentForChemotherapyId != null &&
                e.AdmittedDate != null &&
                e.AdmittedDate < admitted_date
            ).OrderByDescending(e => e.AdmittedDate)
            .ToList();
            if (ipd.Count > 0)
                return ipd[0];

            return null;
        }
        #endregion

        #region Frail Elderly 
        private void CreateIPDInitialAssessmentForFrailElderly(IPD ipd)
        {
            var ia = new IPDInitialAssessmentForFrailElderly();
            unitOfWork.IPDInitialAssessmentForFrailElderlyRepository.Add(ia);

            ipd.IPDInitialAssessmentForFrailElderlyId = ia.Id;
            unitOfWork.IPDRepository.Update(ipd);
            unitOfWork.Commit();
        }
        private void HandleUpdateOrCreateInitialAssessmentForFrailElderlyData(IPD ipd, IPDInitialAssessmentForFrailElderly ia, JToken request)
        {

            var datas = (from master_sql in unitOfWork.MasterDataRepository.AsQueryable().Where(e => !e.IsDeleted && e.Form == "IPDIAFE")
                         join data_sql in unitOfWork.IPDInitialAssessmentForFrailElderlyDataRepository.AsQueryable()
                            .Where(e => !e.IsDeleted && e.IPDInitialAssessmentForFrailElderlyId == ia.Id)
                            on master_sql.Code equals data_sql.Code
                            into d_list
                         from data_sql in d_list.DefaultIfEmpty()
                         select new IPDIAMasterForFrailElderlyData
                         {
                             Data = data_sql,
                             Code = master_sql.Code,
                             Group = master_sql.Group,
                             Order = master_sql.Order,
                             Note = master_sql.Note,
                             ViName = master_sql.ViName,
                             EnName = master_sql.EnName,
                             Value = data_sql.Value,
                             DataType = master_sql.DataType
                         }).ToList();

            var special_requests = unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd.Id
            ).ToList();

            foreach (var item in request)
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;

                var data = GetOrCreateIPDInitialAssessmentForFrailElderlyData(ia.Id, code, datas);
                if (data == null) continue;

                UpdateIPDInitialAssessmentForFrailElderlyData(ipd, data, item["Value"]?.ToString(), special_requests);
            }
            unitOfWork.IPDInitialAssessmentForFrailElderlyRepository.Update(ia);
            unitOfWork.Commit();
        }
        private IPDIAMasterForFrailElderlyData GetOrCreateIPDInitialAssessmentForFrailElderlyData(Guid form_id, string code, List<IPDIAMasterForFrailElderlyData> datas)
        {
            var ia_master = datas.FirstOrDefault(e => e.Code == code);
            if (ia_master == null || ia_master.Data != null) return ia_master;

            IPDInitialAssessmentForFrailElderlyData new_data = new IPDInitialAssessmentForFrailElderlyData
            {
                IPDInitialAssessmentForFrailElderlyId = form_id,
                Code = code,
            };
            unitOfWork.IPDInitialAssessmentForFrailElderlyDataRepository.Add(new_data);
            ia_master.Data = new_data;
            return ia_master;
        }
        private void UpdateIPDInitialAssessmentForFrailElderlyData(IPD ipd, IPDIAMasterForFrailElderlyData ia_master, string value, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            var data = ia_master.Data;
            data.Value = value;
            unitOfWork.IPDInitialAssessmentForFrailElderlyDataRepository.Update(data);

            switch (ia_master.Code)
            {
                case "IPDIAFEITV1ANS":
                case "IPDIAFEITV2ANS":
                    HandleSpecialRequestTextKey(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAFEHASNYSN":
                case "IPDIAFEHAFCYSN":
                case "IPDIAFEDICFYSN":
                case "IPDIAFEAFPTYSN":
                case "IPDIAFEAAICYSN":
                    HandleSpecialRequestChooseKey(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAFEDICFCAV":
                case "IPDIAFEDICFPSY":
                case "IPDIAFEDICFINF":
                case "IPDIAFEDICFOTH":
                    HandleSpecialRequestChooseOption(value, ia_master, ipd.Id, special_requests);
                    break;

                case "IPDIAFEHASNNOT":
                case "IPDIAFEHAFCNOT":
                case "IPDIAFEDICFANS":
                case "IPDIAFEAFPTNOT":
                case "IPDIAFEAAICNOT":
                    HandleSpecialRequestTextOption(value, ia_master, ipd.Id, special_requests);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Special request
        private IPDInitialAssessmentSpecialRequest GetSpecialRequest(string code, List<IPDInitialAssessmentSpecialRequest> datas)
        {
            return datas.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
        }
        private IPDInitialAssessmentSpecialRequest GetOfCreateSpecialRequest(string code, string group, int order, string note, bool is_key, string data_type, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> datas)
        {
            var data = datas.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null) return data;

            JObject json = JObject.Parse(note);
            data = new IPDInitialAssessmentSpecialRequest
            {
                IPDId = ipd_id,
                Code = code,
                Group = group,
                Order = order,
                ViName = json["ViName"]?.ToString(),
                EnName = json["EnName"]?.ToString(),
                IsKey = is_key,
                DataType = data_type,
            };
            unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Add(data);
            return data;
        }
        private void DeleteSpecialRequest(IPDInitialAssessmentSpecialRequest special_request)
        {
            unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Delete(special_request);
        }
        private void DeleteSpecialRequest(string code, List<IPDInitialAssessmentSpecialRequest> datas)
        {
            var spec_req = datas.Where( e=> code.Contains(e.Code));
            foreach (var spec in spec_req)
                unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Delete(spec);
        }
        private void DeleteGroupSpecialRequest(string group, List<IPDInitialAssessmentSpecialRequest> datas)
        {
            var spec_req = datas.Where(e => group.Contains(e.Group));
            foreach (var spec in spec_req)
                unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Delete(spec);
        }
        private void UpdateSpecialRequest(IPDInitialAssessmentSpecialRequest special_request, string value)
        {
            special_request.ViValue = value;
            special_request.EnValue = value;
            unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Update(special_request);
        }
        private void UpdateSpecialRequest(IPDInitialAssessmentSpecialRequest special_request, string vi_value, string en_value)
        {
            special_request.ViValue = vi_value;
            special_request.EnValue = en_value;
            unitOfWork.IPDInitialAssessmentSpecialRequestRepository.Update(special_request);
        }
        private void HandleSpecialRequestChooseKey(string value, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (!string.IsNullOrEmpty(value) && value.ToLower().Equals("true"))
            {
                var spec_req = GetOfCreateSpecialRequest(
                    ia_master.Code,
                    ia_master.Group,
                    (int)ia_master.Order,
                    ia_master.Note,
                    true,
                    ia_master.DataType,
                    ipd_id,
                    special_requests
                );
                UpdateSpecialRequest(spec_req, ia_master.ViName, ia_master.EnName);
            }
            else
                DeleteSpecialRequest(ia_master.Code, special_requests);
        }
        private void HandleSpecialRequestChooseKey(string value, string group, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (!string.IsNullOrEmpty(value) && value.ToLower().Equals("true"))
            {
                var spec_req = GetOfCreateSpecialRequest(
                    ia_master.Code,
                    group,
                    (int)ia_master.Order,
                    ia_master.Note,
                    true,
                    ia_master.DataType,
                    ipd_id,
                    special_requests
                );
                UpdateSpecialRequest(spec_req, ia_master.ViName, ia_master.EnName);
            }
            else
                DeleteSpecialRequest(ia_master.Code, special_requests);
        }
        private void HandleSpecialRequestTextKey(string value, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (string.IsNullOrEmpty(value))
            {
                DeleteSpecialRequest(ia_master.Code, special_requests);
                return;
            }

            var spec_req = GetOfCreateSpecialRequest(
                ia_master.Code,
                ia_master.Group,
                (int)ia_master.Order,
                ia_master.Note,
                true,
                ia_master.DataType,
                ipd_id,
                special_requests
            );
            UpdateSpecialRequest(spec_req, value);
        }
        private void HandleSpecialRequestTextKey(string value, string group, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (string.IsNullOrEmpty(value))
            {
                DeleteSpecialRequest(ia_master.Code, special_requests);
                return;
            }

            var spec_req = GetOfCreateSpecialRequest(
                ia_master.Code,
                group,
                (int)ia_master.Order,
                ia_master.Note,
                true,
                ia_master.DataType,
                ipd_id,
                special_requests
            );
            UpdateSpecialRequest(spec_req, value);
        }
        private void HandleSpecialRequestChooseOption(string value, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (!string.IsNullOrEmpty(value) && value.ToLower().Equals("true"))
            {
                var spec_req = GetOfCreateSpecialRequest(
                    ia_master.Code,
                    ia_master.Group,
                    (int)ia_master.Order,
                    ia_master.Note,
                    false,
                    ia_master.DataType,
                    ipd_id,
                    special_requests
                );
                UpdateSpecialRequest(spec_req, ia_master.ViName, ia_master.EnName);
            }
            else
                DeleteSpecialRequest(ia_master.Code, special_requests);
        }
        private void HandleSpecialRequestChooseOption(string value, string group, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (!string.IsNullOrEmpty(value) && value.ToLower().Equals("true"))
            {
                var spec_req = GetOfCreateSpecialRequest(
                    ia_master.Code,
                    group,
                    (int)ia_master.Order,
                    ia_master.Note,
                    false,
                    ia_master.DataType,
                    ipd_id,
                    special_requests
                );
                UpdateSpecialRequest(spec_req, ia_master.ViName, ia_master.EnName);
            }
            else
                DeleteSpecialRequest(ia_master.Code, special_requests);
        }
        private void HandleSpecialRequestTextOption(string value, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (string.IsNullOrEmpty(value))
            {
                DeleteSpecialRequest(ia_master.Code, special_requests);
                return;
            }

            var spec_req = GetOfCreateSpecialRequest(
                ia_master.Code,
                ia_master.Group,
                (int)ia_master.Order,
                ia_master.Note,
                false,
                ia_master.DataType,
                ipd_id,
                special_requests
            );
            UpdateSpecialRequest(spec_req, value);
        }
        private void HandleSpecialRequestTextOption(string value, string group, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (string.IsNullOrEmpty(value))
            {
                DeleteSpecialRequest(ia_master.Code, special_requests);
                return;
            }
            if (ia_master.Code == "IPDIAAUHOMAANS")
                value = $"{value} kg";

            var spec_req = GetOfCreateSpecialRequest(
                ia_master.Code,
                group,
                (int)ia_master.Order,
                ia_master.Note,
                false,
                ia_master.DataType,
                ipd_id,
                special_requests
            );
            UpdateSpecialRequest(spec_req, value);
        }
        private void HandleSpecialRequestChooseLevel4(string value, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (!string.IsNullOrEmpty(value) && value.ToLower().Equals("true"))
            {
                var spec_req = GetOfCreateSpecialRequest(
                    ia_master.Code,
                    "IPDIAAUSSSS",
                    (int)ia_master.Order,
                    ia_master.Note,
                    true,
                    ia_master.DataType,
                    ipd_id,
                    special_requests
                );
                UpdateSpecialRequest(spec_req, $"{ia_master.ViName}(Level 4)", $"{ia_master.EnName}(Level 4)");
            }
            else
                DeleteSpecialRequest(ia_master.Code, special_requests);
        }
        private void HandleSpecialRequestSelectOption(string value, string code, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (string.IsNullOrEmpty(value)) {
                DeleteSpecialRequest(ia_master.Code, special_requests);
                return;
            }

            var kind = unitOfWork.MasterDataRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                e.Code == code
            ).Data;
            var json = JArray.Parse(kind);
            var list_value = value.Split(',').ToList();
            foreach (var item in json)
                for(int i = 0; i < list_value.Count(); i++)
                    if (item["value"]?.ToString() == list_value[i])
                        list_value[i] = item["label"]?.ToString();

            var spec_req = GetOfCreateSpecialRequest(
                ia_master.Code, 
                ia_master.Group, 
                (int)ia_master.Order, 
                ia_master.Note, 
                false,
                ia_master.DataType,
                ipd_id, 
                special_requests
            );
            UpdateSpecialRequest(spec_req,  string.Join(",", list_value));
        }
        private void HandleSpecialRequestTextRange(string value, int range, dynamic ia_master, Guid ipd_id, List<IPDInitialAssessmentSpecialRequest> special_requests)
        {
            if (string.IsNullOrEmpty(value))
            {
                DeleteSpecialRequest(ia_master.Code, special_requests);
                return;
            }

            if (int.Parse(value) > range)
            {
                var spec_req = GetOfCreateSpecialRequest(
                    ia_master.Code,
                    ia_master.Group,
                    (int)ia_master.Order,
                    ia_master.Note,
                    true,
                    ia_master.DataType,
                    ipd_id,
                    special_requests
                );
                UpdateSpecialRequest(spec_req, value);
            }
            else
                DeleteSpecialRequest(ia_master.Code, special_requests);
        }
        private void HandleUpdateOrCreateSibling(Guid visit_id, JToken request_datas)
        { if(request_datas!=null)
            {
                foreach (var item in request_datas)
                {
                    string item_id = item["Id"]?.ToString();
                    if (string.IsNullOrEmpty(item_id))
                    {
                        CreateSibling(visit_id, item);
                        unitOfWork.Commit();
                    }
                    else
                    {
                        Guid sibling_id = new Guid(item_id);
                        IPDSibling ipdSibling = unitOfWork.IPDSiblingRepository.GetById(sibling_id);
                        UpdateSibling(ipdSibling, item);
                        unitOfWork.Commit();
                    }
                }
               
            }
            
        }
        protected void CreateSibling(Guid visit_id, JToken item)
        {
            IPDSibling sibling = new IPDSibling();
            sibling.VisitId = visit_id;
            sibling.Age = item["Age"]?.ToString(); ;
            sibling.Gender = item["Gender"]?.ToString();
            unitOfWork.IPDSiblingRepository.Add(sibling);
        }
        protected void UpdateSibling(IPDSibling sibling, JToken item)
        {
            var old = new
            {
                sibling.Age,
                sibling.Gender,

            };
            var _new = new
            {
                Age = item["Age"]?.ToString(),
                Gender =item["Gender"]?.ToString(),
            };

            if (JsonConvert.SerializeObject(old) != JsonConvert.SerializeObject(_new))
            {
                sibling.Age = _new.Age;
                sibling.Gender = _new.Gender;

                unitOfWork.IPDSiblingRepository.Update(sibling);
            }
        }
        private void HandleDeleteSibling(JToken request_datas)
        {
            if(request_datas!= null)
            {
                foreach (var item in request_datas)
                {
                    Guid idsibling = (Guid)item["Id"];
                    IPDSibling sibling = unitOfWork.IPDSiblingRepository.FirstOrDefault(x => x.Id == idsibling);
                    unitOfWork.IPDSiblingRepository.Delete(sibling);
                }
                unitOfWork.Commit();
            }
           
        }
        #endregion
    }
}
