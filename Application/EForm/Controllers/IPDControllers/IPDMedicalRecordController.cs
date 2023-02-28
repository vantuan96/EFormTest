using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.EIOControllers;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using Helper;
namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDMedicalRecordController : BaseApiController
    {
        [HttpPost]
        [Route("api/IPD/MedicalRecord/SyncMedicalRecordOncology/{type}/{visitId}")]
        public IHttpActionResult SyncSyncMedicalRecordOncology(string type, Guid visitId, [FromBody] JObject request)
        {
            var ipdNew = GetIPD(visitId);
            if (ipdNew == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var id = new Guid(request["VisitID"]?.ToString());
            var ipdOld = GetIPD(id);
            if (ipdOld == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var dataMedicalRecords = ipdOld.IPDMedicalRecord?.IPDMedicalRecordDatas.Where(e => !e.IsDeleted).ToList();
            var dataPart1s = ipdOld.IPDMedicalRecord?.IPDMedicalRecordPart1?.IPDMedicalRecordPart1Datas.Where(e => !e.IsDeleted).ToList();
            var dataPart2s = ipdOld.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted).ToList();
            var dataPart3s = ipdOld.IPDMedicalRecord?.IPDMedicalRecordPart3?.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted).ToList();

            var medical_record = ipdNew.IPDMedicalRecord;
            if (medical_record == null)
            {
                medical_record = new IPDMedicalRecord();
                unitOfWork.IPDMedicalRecordRepository.Add(medical_record);
                ipdNew.IPDMedicalRecordId = medical_record.Id;
                unitOfWork.IPDRepository.Update(ipdNew);
            }
            var medicalRecordId = (Guid)medical_record.Id;
            List<IPDMedicalRecordData> listMedicalRecordData = new List<IPDMedicalRecordData>();
            listMedicalRecordData = unitOfWork.IPDMedicalRecordDataRepository.Find(x => x.IPDMedicalRecordId == medicalRecordId).ToList();
            foreach (var dataMedicalRecord in dataMedicalRecords)
            {
                var data = GetOrCreateIPDMedicalRecordData(listMedicalRecordData, medicalRecordId, dataMedicalRecord.Code);
                if (data != null)
                    UpdateIPDMedicalRecordData(data, dataMedicalRecord.Value);
            }

            if (medical_record.IPDMedicalRecordPart1Id == null)
            {
                var part_1 = new IPDMedicalRecordPart1();
                unitOfWork.IPDMedicalRecordPart1Repository.Add(part_1);
                medical_record.IPDMedicalRecordPart1Id = part_1.Id;
                unitOfWork.IPDMedicalRecordRepository.Update(medical_record);
            }
            var medicalRecordPart1Id = (Guid)medical_record.IPDMedicalRecordPart1Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(type, visitId, medicalRecordPart1Id);
            //if (medicalRecordOfPatient != null)
            //    return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_EXIST);
            var recordOfPatient = new IPDMedicalRecordOfPatients();
            if (medicalRecordOfPatient == null || medicalRecordOfPatient.IsDeleted)
            {
                recordOfPatient.FormId = medicalRecordPart1Id;
                recordOfPatient.FormCode = type;
                recordOfPatient.VisitId = visitId;
                recordOfPatient.CreatedBy = GetUser().Username;
                recordOfPatient.CreatedAt = DateTime.Now;
                recordOfPatient.UpdatedAt = recordOfPatient.CreatedAt;
                recordOfPatient.Id = new Guid();
                recordOfPatient.IsDeleted = false;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Add(recordOfPatient);
            }
            List<IPDMedicalRecordPart1Data> listMedicalRecordPart1Data = new List<IPDMedicalRecordPart1Data>();
            listMedicalRecordPart1Data = unitOfWork.IPDMedicalRecordPart1DataRepository.Find(x => x.IPDMedicalRecordPart1Id == medicalRecordPart1Id).ToList();
            foreach (var dataPart1 in dataPart1s)
            {
                var data = GetOrCreateIPDMedicalRecordPart1Data(listMedicalRecordPart1Data, medicalRecordPart1Id, dataPart1.Code, dataPart1.Value);
                if (data != null)
                    UpdateIPDMedicalRecordPart1Data(data, dataPart1.Value);
            }

            if (medical_record.IPDMedicalRecordPart2Id == null)
            {
                var part_2 = new IPDMedicalRecordPart2();
                unitOfWork.IPDMedicalRecordPart2Repository.Add(part_2);
                medical_record.IPDMedicalRecordPart2Id = part_2.Id;
                unitOfWork.IPDMedicalRecordRepository.Update(medical_record);
            }
            var medicalRecordPart2Id = (Guid)medical_record.IPDMedicalRecordPart2Id;
            var medicalRecordOfPatientPart2 = GetMedicalRecordOfPatients(type, visitId, medicalRecordPart2Id);
            //if (medicalRecordOfPatientPart2 != null)
            //    return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_EXIST);
            var recordOfPatientPart2 = new IPDMedicalRecordOfPatients();
            recordOfPatientPart2.FormId = medicalRecordPart2Id;
            recordOfPatientPart2.FormCode = type;
            recordOfPatientPart2.VisitId = visitId;
            recordOfPatientPart2.CreatedBy = GetUser().Username;
            recordOfPatientPart2.CreatedAt = DateTime.Now;
            recordOfPatientPart2.UpdatedAt = recordOfPatientPart2.CreatedAt;
            recordOfPatientPart2.Id = new Guid();
            recordOfPatientPart2.IsDeleted = false;
            unitOfWork.IPDMedicalRecordOfPatientRepository.Add(recordOfPatientPart2);
            List<IPDMedicalRecordPart2Data> listMedicalRecordPart2Data = new List<IPDMedicalRecordPart2Data>();
            listMedicalRecordPart2Data = unitOfWork.IPDMedicalRecordPart2DataRepository.Find(x => x.IPDMedicalRecordPart2Id == medicalRecordPart2Id).ToList();
            foreach (var dataPart2 in dataPart2s)
            {
                var data = GetOrCreateIPDMedicalRecordPart2Data(listMedicalRecordPart2Data, medicalRecordPart2Id, dataPart2.Code, dataPart2.Value);
                if (data != null)
                    UpdateIPDMedicalRecordPart2Data(data, dataPart2.Value);
            }

            if (medical_record.IPDMedicalRecordPart3Id == null)
            {
                var part_3 = new IPDMedicalRecordPart3();
                unitOfWork.IPDMedicalRecordPart3Repository.Add(part_3);
                medical_record.IPDMedicalRecordPart3Id = part_3.Id;
                unitOfWork.IPDMedicalRecordRepository.Update(medical_record);
            }
            var medicalRecordPart3Id = (Guid)medical_record.IPDMedicalRecordPart3Id;
            var medicalRecordOfPatientPart3 = GetMedicalRecordOfPatients(type, visitId, medicalRecordPart3Id);
            //if (medicalRecordOfPatientPart3 != null)
            //    return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_EXIST);
            var recordOfPatientPart3 = new IPDMedicalRecordOfPatients();
            recordOfPatientPart3.FormId = medicalRecordPart3Id;
            recordOfPatientPart3.FormCode = type;
            recordOfPatientPart3.VisitId = visitId;
            recordOfPatientPart3.CreatedBy = GetUser().Username;
            recordOfPatientPart3.CreatedAt = DateTime.Now;
            recordOfPatientPart3.UpdatedAt = recordOfPatientPart3.CreatedAt;
            recordOfPatientPart3.Id = new Guid();
            recordOfPatientPart3.IsDeleted = false;
            unitOfWork.IPDMedicalRecordOfPatientRepository.Add(recordOfPatientPart3);
            List<IPDMedicalRecordPart3Data> listMedicalRecordPart3Data = new List<IPDMedicalRecordPart3Data>();
            listMedicalRecordPart3Data = unitOfWork.IPDMedicalRecordPart3DataRepository.Find(x => x.IPDMedicalRecordPart3Id == medicalRecordPart3Id).ToList();
            foreach (var dataPart3 in dataPart3s)
            {
                var data = GetOrCreateIPDMedicalRecordPart3Data(listMedicalRecordPart3Data, medicalRecordPart3Id, dataPart3.Code, dataPart3.Value);
                if (data != null)
                    UpdateIPDMedicalRecordPart3Data(data, dataPart3.Value);
            }

            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        [HttpGet]
        [Route("api/IPD/MedicalRecord/Info/Part2AndPart3/{visitId}")]
        public IHttpActionResult Part2AndPart3(Guid visitId)
        {
            IPD visit = GetVisit(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var part_2 = visit?.IPDMedicalRecord?.IPDMedicalRecordPart2;
            var part_3 = visit?.IPDMedicalRecord?.IPDMedicalRecordPart3;
            var fullNameCreatedPart2 = String.IsNullOrEmpty(part_2?.CreatedBy) == false ? unitOfWork.UserRepository.FirstOrDefault(x => x.Username == part_2.CreatedBy).Fullname : "";
            var fullNameUpdatedPart3 = String.IsNullOrEmpty(part_3?.UpdatedBy) == false ? unitOfWork.UserRepository.FirstOrDefault(x => x.Username == part_3.UpdatedBy).Fullname : "";
            return Content(HttpStatusCode.OK, new
            {
                FullNameCreatedPart2 = fullNameCreatedPart2,
                CreateByPart2 = part_2?.CreatedBy,
                CreateAtPart2 = part_2?.CreatedAt,

                FullNameUpdatedPart3 = fullNameUpdatedPart3,
                UpdateByPart3 = part_3?.UpdatedBy,
                UpdateAtPart3 = part_3?.UpdatedAt,
            });
        }
        [HttpGet]
        [Route("api/IPD/MedicalRecord/Info/{Type}/{id}")]
        [Permission(Code = "IMRPO2")]
        public IHttpActionResult GetIPDMedicalRecordPart1API(string Type, Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var setupMedicalRecord = GetSetupMedicalRecord(Type, ipd.SpecialtyId);
            //if (setupMedicalRecord == null || setupMedicalRecord.IsDeploy == false)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD);
            //var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, ipd.IPDMedicalRecord.Id);
            //if (medicalRecordOfPatient == null)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD_OF_PATIENT);
            var currentOncologyMedicalRecord = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(x => x.VisitId == ipd.Id && x.FormCode == "A01_196_050919_V");
            string IdOldOncologyMedicalRecord = null;
            if (currentOncologyMedicalRecord == null)
            {
                var results = GetAllInfoCustomerInAreIPD(ipd);
                if (results.Count > 1)
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].Id == id) continue;
                        Guid oldIpdIdtmp = (Guid)results[i].Id;
                        var ipdOld = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == oldIpdIdtmp);
                        if (ipdOld != null)
                        {
                            if (ipdOld.EDStatus.Code == "IPDCOTM")
                            {
                                IdOldOncologyMedicalRecord = null;
                                break;
                            }
                            else
                            {
                                var MedicalRecordOfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(x => x.VisitId == oldIpdIdtmp && x.FormCode == "A01_196_050919_V");
                                if (MedicalRecordOfPatient != null)
                                {
                                    IdOldOncologyMedicalRecord = oldIpdIdtmp.ToString();
                                    break;
                                }
                            }
                        }
                    }

                };
            }

            bool? isUsePreProcedure = null;
            var medicalRecord = ipd.IPDMedicalRecord;
            if (medicalRecord != null)
            {
                var medicalRecordPart2 = medicalRecord.IPDMedicalRecordPart2;
                if (medicalRecordPart2 != null)
                {
                    var medicalRecordPart2Datas = medicalRecordPart2.IPDMedicalRecordPart2Datas;
                    if (medicalRecordPart2Datas != null)
                    {
                        var value = medicalRecordPart2Datas.FirstOrDefault(e => e.Code == "IPDMRPT1681")?.Value;
                        if (string.IsNullOrEmpty(value))
                        {
                            isUsePreProcedure = null;
                        }
                        else
                        {
                            isUsePreProcedure = Convert.ToBoolean(value);
                        }
                    }
                }
            }
            int currentVersion = getVersionOfMedicalrecord(formCode: Type, visitId: id, setCurrentVersion: 2);

            var part2 = ipd?.IPDMedicalRecord?.IPDMedicalRecordPart2;
            bool isTab4 = false;
            if (part2 == null)
            {
                isTab4 = false;
            }
            else
            {
                var part2Id = part2.Id;
                var tab2 = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(x => x.FormId == part2Id && x.FormCode == Type);
                if (tab2 == null)
                {
                    isTab4 = false;
                }
                else
                {
                    isTab4 = true;
                }
            }
            return Content(HttpStatusCode.OK, new
            {
                ipd.Id,
                IsLocked = IPDIsBlock(ipd, Type),
                ipd.VisitCode,
                ipd.PrimaryDoctor?.Username,
                ipd.DischargeDate,
                MedicalRecordOncologyOld = new
                {
                    VisitId = IdOldOncologyMedicalRecord
                },
                IsUsePreProcedure = isUsePreProcedure,
                Version = currentVersion,
                IsTab4 = isTab4
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/MedicalRecord/SyncReadOnlySignificantMedications")]
        [Permission(Code = "IMERE1")]
        public IHttpActionResult SyncReadOnlySignificantMedicationsAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);
            if (ipd.VisitCode == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record.IPDMedicalRecordPart2Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPT_NOT_FOUND);
            if (medical_record.IPDMedicalRecordPart3Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPE_NOT_FOUND);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            List<dynamic> medicine;
            if (site_code == "times_city")
                medicine = EHosClient.GetSignificantMedications(customer.PID, ipd.VisitCode);
            else
                medicine = OHClient.GetSignificantMedications(customer.PID, ipd.VisitCode);

            return Content(HttpStatusCode.OK, medicine);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/MedicalRecord/SyncDiagnosisAndICD")]
        [Permission(Code = "IMERE2")]
        public IHttpActionResult SyncSyncDiagnosisAndICDAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);
            if (string.IsNullOrEmpty(ipd.VisitCode))
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record.IPDMedicalRecordPart2Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPT_NOT_FOUND);
            if (medical_record.IPDMedicalRecordPart3Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPE_NOT_FOUND);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            dynamic diagnosis;
            if (site_code == "times_city")
                diagnosis = EHosClient.GetDiagnosisAndICDIPD(customer.PID, ipd.VisitCode);
            else
                diagnosis = new { };

            return Content(HttpStatusCode.OK, diagnosis);
        }
        protected dynamic BuildMedicalRecordPart1Result(string formCode, IPD ipd, IPDMedicalRecord medical_record)
        {
            var general_datas = medical_record.IPDMedicalRecordDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });

            var part_1 = medical_record.IPDMedicalRecordPart1;
            var part_2 = medical_record.IPDMedicalRecordPart2;
            var part_3 = medical_record.IPDMedicalRecordPart3;

            var data_part_1 = part_1.IPDMedicalRecordPart1Datas.Where(e => !e.IsDeleted).Select(e => new { e.Id, e.Code, e.Value });

            var spec = ipd.Specialty;
            var current_doctor = ipd.PrimaryDoctor;

            var transfers = new IPDTransfer(ipd).GetListInfo();
            dynamic origin_visit = null;
            dynamic first_ed_opd = null;
            TransferInfoModel first_ipd = null;
            TransferInfoModel first_ipd2 = null;
            if (transfers.Count() > 0)
            {
                origin_visit = transfers[0];
                first_ed_opd = transfers.FirstOrDefault(e => e.CurrentType == "OPD" || e.CurrentType == "ED");
                first_ipd = transfers.FirstOrDefault(e => e.CurrentType == "ED" || e.CurrentType == "IPD" && (string.IsNullOrEmpty(e.CurrentSpecialtyCode) || !e.CurrentSpecialtyCode.Contains("PTTT")));
                first_ipd2 = transfers.FirstOrDefault(e => e.CurrentType == "IPD" && (string.IsNullOrEmpty(e.CurrentSpecialtyCode) || !e.CurrentSpecialtyCode.Contains("PTTT")));
            }
            else
            {
                origin_visit = new
                {
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    CurrentType = "IPD",
                };
                first_ipd = new TransferInfoModel()
                {
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentSpecialty = new { spec?.ViName, spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    CurrentType = "IPD"
                };
                first_ipd2 = new TransferInfoModel()
                {
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentSpecialty = new { spec?.ViName, spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    CurrentType = "IPD"
                };
            }


            dynamic in_hospital_days = null;
            if (ipd.DischargeDate != null)
            {
                DateTime admitted_date = (DateTime)first_ipd.CurrentRawDate;
                DateTime discharge_date = (DateTime)ipd.DischargeDate;
                in_hospital_days = (discharge_date.Date - admitted_date.Date).Days + 1;
            }

            var status = ipd.EDStatus;
            var part1_id = part_1.Id;
            var is_new = CheckIsNewByTab(part1_id, formCode);
            var customer = ipd.Customer;
            var site = ipd.Site;

            var visitId = ipd.Id;
            var getMedicalRecords = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                   .Where(m => m.VisitId == visitId && m.FormCode == formCode && !m.IsDeleted)
                                   .OrderBy(m => m.CreatedAt)
                                   .ToList();
            string userNameCreate = null;
            foreach (var item in getMedicalRecords)
            {
                var checkPart2 = unitOfWork.IPDMedicalRecordPart2Repository.AsQueryable()
                                .Where(m => m.Id == item.FormId)
                                .FirstOrDefault();
                if (checkPart2 != null)
                {
                    userNameCreate = item.CreatedBy;
                    break;
                }
            }

            User fullNameCreate = null;
            if (userNameCreate != null)
            {
                fullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => u.Username == userNameCreate);
            }
            var UserCreateAt = getMedicalRecords.Count == 0 ? null : getMedicalRecords[0]?.CreatedAt;
            string doctor = null;
            foreach (var item in getMedicalRecords)
            {
                var checkPart3 = unitOfWork.IPDMedicalRecordPart3Repository.AsQueryable()
                                 .Where(m => m.Id == item.FormId)
                                 .FirstOrDefault();
                if (checkPart3 != null)
                {
                    doctor = item.UpdatedBy;
                    break;
                }
            }
            User fullNameDoctor = null;
            if (doctor != null)
            {
                fullNameDoctor = unitOfWork.UserRepository.FirstOrDefault(d => d.Username == doctor);
            }

            var part2_Id = part_2?.Id;
            var part3_Id = part_3?.Id;

            string userNameCreated = GetSignature("PART2LASTUPDATEDBY", formCode, part2_Id);
            User fullNameCreated = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == userNameCreated);
            string timeCreated = GetSignature("PART2LASTUPDATEDAT", formCode, part2_Id);

            string userNameUpdated = GetSignature("PART3LASTUPDATEDBY", formCode, part3_Id);
            User fullNameUpdated = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == userNameUpdated);
            string timeUpdated = GetSignature("PART3LASTUPDATEDAT", formCode, part3_Id);


            string edSatatusCode = null;
            if (ipd.EDStatusId != null)
            {
                var edSatatus = unitOfWork.EDStatusRepository.FirstOrDefault(e => e.Id == ipd.EDStatusId);
                edSatatusCode = edSatatus.Code;
            }
            var medical = ipd.IPDMedicalRecordId;
            string timeUpdate = null;
            switch (edSatatusCode)
            {
                case "IPDDD":
                    var dead = unitOfWork.IPDMedicalRecordDataRepository.FirstOrDefault(d => d.IPDMedicalRecordId == medical && d.Code == "IPDMRPTNGTVANS");
                    var timeDead = dead?.Value;
                    timeUpdate = timeDead;
                    break;
                case "IPDUDT":
                    dead = unitOfWork.IPDMedicalRecordDataRepository.FirstOrDefault(d => d.IPDMedicalRecordId == medical && d.Code == "IPDTD0ANS");
                    timeDead = dead?.Value;
                    timeUpdate = timeDead;
                    break;
                case "IPDIHT":
                    dead = unitOfWork.IPDMedicalRecordDataRepository.FirstOrDefault(d => d.IPDMedicalRecordId == medical && d.Code == "IPDMRPTCHVHANS");
                    timeDead = dead?.Value;
                    timeUpdate = timeDead;
                    break;
                case "IPDDC":
                    dead = unitOfWork.IPDMedicalRecordDataRepository.FirstOrDefault(d => d.IPDMedicalRecordId == medical && d.Code == "IPDMRPTCDRVANS");
                    timeDead = dead?.Value;
                    timeUpdate = timeDead;
                    break;
                case "IPDTF":
                    dead = unitOfWork.IPDMedicalRecordDataRepository.FirstOrDefault(d => d.IPDMedicalRecordId == medical && d.Code == "IPDRH1ANS");
                    var timeTranfer = dead?.CreatedAt;
                    timeUpdate = timeTranfer?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
                    break;
                default:
                    break;
            }
            var checkIsDone = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                              .Where(m => !m.IsDeleted && m.VisitId == visitId && m.FormCode == formCode && m.FormId != null)
                              .ToList();
            bool isdone = false;
            if (checkIsDone.Count >= 3)
            {
                isdone = true;
            }
            string[] code = new string[]
            {
                 "IPDOAGIANM101","IPDOAGIANM103","IPDOAGIANM105"
            };
            var visit_Id = ipd.Id;
            var data_infor = AutoFillDataFromMasterData(code, visit_Id);
            float? vongDau = AutoFillFromInitialAssessment(ipd, "IPDIAAUROUNDHEADANS");
            string statusCode = ipd.EDStatus.Code.ToUpper();
            string moveIn = null;
            switch (statusCode)
            {
                case "IPDUDT":
                    moveIn = general_datas?.FirstOrDefault(x => x.Code == "IPDRH1ANS").Value.ToString();
                    break;
                case "IPDIHT":
                    moveIn = general_datas?.FirstOrDefault(x => x.Code == "IPDRH0ANS")?.Value.ToString();
                    break;

            }

            if (part_2 != null)
                SyncData(ipd, part_2);
            var form = GetForm(ipd.Id, "PreProcedureRiskAssessmentForCardiacCatheterization");
            List<FormDataValue> datas = new List<FormDataValue>();
            if (form != null)
                datas = GetFormData((Guid)form.VisitId, form.Id, "A01_034_290422_V_PreProcedureRiskAssessmentForCardiacCatheterization");
            int currentVersion = getVersionOfMedicalrecord(formCode: formCode, visitId: ipd.Id, setCurrentVersion: 2);
            return new
            {
                TranferInfo = is_new ? GetIPDTransferVisitInfo(ipd) : null,
                IsDone = isdone,
                SpecialtyName = ipd.Specialty?.ViName,
                site?.Location,
                Site = site?.Name,
                site?.Province,
                site?.LocationUnit,
                Specialty = new { spec?.ViName, spec?.EnName },
                IsNew = is_new,
                part_1.Id,
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                ListStatus = GetListStatus(ipd.IsDraft),
                DischargeDate = ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Customer = new
                {
                    customer.Id,
                    customer.PID,
                    customer.Fullname,
                    DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    customer.AgeFormated,
                    customer.Phone,
                    customer.Gender,
                    customer.Fork,
                    customer.IsVip,
                    customer.Nationality,
                    customer.Address,
                    customer.Job,
                    customer.WorkPlace,
                    customer.Relationship,
                    customer.RelationshipKind,
                    customer.RelationshipContact,
                    customer.RelationshipAddress,
                    customer.IsChronic,
                    customer.IdentificationCard,
                    IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                    customer.IssuePlace,
                    IPDId = ipd.Id,
                    ipd.RecordCode,
                    AdmittedDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    ipd.VisitCode,
                    ipd.HealthInsuranceNumber,
                    StartHealthInsuranceDate = ipd.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                    ExpireHealthInsuranceDate = ipd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                    customer.MOHJob,
                    customer.MOHJobCode,
                    customer.MOHEthnic,
                    customer.MOHEthnicCode,
                    customer.MOHNationality,
                    customer.MOHNationalityCode,
                    customer.MOHAddress,
                    customer.MOHProvince,
                    customer.MOHProvinceCode,
                    customer.MOHDistrict,
                    customer.MOHDistrictCode,
                    customer.MOHObject,
                    customer.MOHObjectOther
                },
                Datas = data_part_1,
                DatasPart2 = part_2 != null ? part_2.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }) : null,
                DatasPart3 = part_3 != null ? part_3.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }) : null,
                GeneralDatas = general_datas,
                IsLocked = IPDIsBlock(ipd, formCode.ToUpper()),
                PrimaryDoctor = new
                {
                    ipd.PrimaryDoctor?.Username,
                    ipd.PrimaryDoctor?.Fullname,
                    ipd.PrimaryDoctor?.Id,
                },
                ReadOnly = new
                {
                    Twelve = first_ipd?.CurrentDate,
                    Thirteen = first_ipd?.CurrentType,
                    Fifteen = new
                    {
                        first_ipd2?.CurrentSpecialty,
                        first_ipd2?.CurrentDoctor,
                        first_ipd2?.CurrentDate,
                    },
                    Sixteen = transfers.Where(e => !string.IsNullOrEmpty(e.TransferType) && e.CurrentType == "IPD").Select(e => new
                    {
                        e.TransferSpecialty,
                        e.TransferDate,
                        e.TransferDoctor,
                        e.DischargeDate,
                        e.TransferRawDate
                    }),
                    Eighteen = ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    Nineteen = in_hospital_days,
                    TwentyOne = new { first_ed_opd?.CurrentDiagnosis, first_ed_opd?.CurrentICD }
                },
                FullNameCreated = fullNameCreate?.Fullname,
                CreateBy = userNameCreate,
                CreateAt = UserCreateAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                FullNameUpdated = fullNameDoctor?.Fullname,
                UpdateBy = doctor,
                UpdateAt = ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Cannang = AutoFillFromInitialAssessment(ipd, "IPDIAAUWEIGANS"),
                VongDau = vongDau,
                NhipTho = AutoFillFromInitialAssessment(ipd, "IPDIAAURERAANS"),
                MoveIn = moveIn,
                DataInfor = data_infor,
                ipd.IsDraft,
                Signature = new
                {
                    CreatedBy = fullNameCreated?.Fullname,
                    CreatedAt = timeCreated,
                    UserNameCreated = userNameCreated,
                    UpdatedBy = fullNameUpdated?.Fullname,
                    UpdatedAt = timeUpdated,
                    UserNameUpdated = userNameUpdated
                },
                Version = ipd.Version > currentVersion ? ipd.Version : currentVersion,
                PreProcedureRiskAssessmentDatas = datas,
                PreProcedureRiskAssessmentCreatedBy = form?.CreatedBy
            };
        }

        protected dynamic BuildMedicalRecordPart2Result(string formCode, IPD ipd, IPDMedicalRecord medical_record, IPD ipdAsync = null)
        {
            string formCodeSan = "IPDSANTBL1";
            String oldFormId = null;
            String oldIpdId = null;
            var general_datas = medical_record.IPDMedicalRecordDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });

            var part_2 = medical_record.IPDMedicalRecordPart2;
            var status = ipd.EDStatus;

            var initial = ipd.IPDInitialAssessmentForAdultId;
            var spec = ipd.Specialty;
            IPDQueryModel edmodel = new IPDQueryModel();
            var visitAllergy = EMRVisitAllergy.GetIPDVisitAllergy(ipd);
            var fullNameCreated = String.IsNullOrEmpty(part_2?.CreatedBy) == false ? unitOfWork.UserRepository.FirstOrDefault(x => x.Username == part_2.CreatedBy)?.Fullname : "";
            var fullNameUpdated = String.IsNullOrEmpty(part_2?.UpdatedBy) == false ? unitOfWork.UserRepository.FirstOrDefault(x => x.Username == part_2.UpdatedBy)?.Fullname : "";

            float? vongDau = AutoFillFromInitialAssessment(ipd, "IPDIAAUROUNDHEADANS");
            bool isCreatedPreProcedureRiskAssessment = false;
            var preProcedureRiskAssessment = unitOfWork.IPDMedicalRecordExtenstionReponsitory.Find(e => !e.IsDeleted && e.VisitId == ipd.Id && e.Name == "PreProcedureRiskAssessmentForCardiacCatheterization").FirstOrDefault();
            if (preProcedureRiskAssessment != null)
            {
                isCreatedPreProcedureRiskAssessment = true;
            }
            else
            {
                isCreatedPreProcedureRiskAssessment = false;
            }
            int currentVersion = getVersionOfMedicalrecord(formCode: formCode, visitId: ipd.Id, setCurrentVersion: 2);
            List<VisitModel> specialistExaminationsOPD = new List<VisitModel>();
            if (formCode == "A01_195_050919_V")
            {
                var opdHistory = new OPDHistory();
                if (ipd?.CustomerId != null)
                {
                    var cusId = (Guid)ipd.CustomerId;
                    var admittedDate = (DateTime)ipd?.AdmittedDate;
                    specialistExaminationsOPD = opdHistory.GetOPDHistorySpecialistExamination(cusId, admittedDate).ToList();
                }

            }
            var isNew = CheckIsNewByTab(part_2.Id, formCode);
            // thực hiện đồng bộ tab2 huyvv6
            var oldDatasSanTablePT = new List<MasterdataForm>();
            var results = ipdAsync != null ? GetAllInfoCustomerInAreIPD(ipdAsync) : GetAllInfoCustomerInAreIPD(ipd);
            Guid id = ipdAsync != null ? ipdAsync.Id : ipd.Id;
            object specialtyOld = null;
            if (results.Count > 1)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].Id == id)
                        continue;
                    Guid oldIpdIdtmp = (Guid)results[i].Id;
                    oldIpdId = oldIpdIdtmp.ToString();
                    var forms = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == oldIpdIdtmp && e.FormCode == formCodeSan);
                    if (forms != null && forms.Count() > 0 && ipdAsync != null)
                    {
                        //oldFormId = forms.Id.ToString();
                        var formsCurrent = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == id && e.FormCode == formCodeSan && e.VisitTypeGroupCode == "IPD").ToList();
                        var isNewEIOForm = formsCurrent.Count > 0 ? false : true;
                        oldDatasSanTablePT = forms.OrderBy(x => x.CreatedAt).Select(form => FormatOutput(form, formCodeSan, isNewEIOForm)).ToList();
                        break;
                    }
                    specialtyOld = results[i].Specialty;

                }
            }
            List<PromResponse> promForCoronaryNews = new List<PromResponse>();
            List<PromResponse> promForHeartNews = new List<PromResponse>();
            IPDHistory his = new IPDHistory(ipd, null);
            string oldVisitGroup = null;
            bool havePromForCoronary = false;
            bool havePromHeart = false;
            Guid visitCurrentProm = Guid.Empty;
            havePromForCoronary = unitOfWork.PROMForCoronaryDiseaseRepository.Find(x => x.VisitId == ipd.Id && !x.IsDeleted).Any();
            havePromHeart = unitOfWork.PROMForheartFailureRepository.Find(x => x.VisitId == ipd.Id && !x.IsDeleted).Any();
            if (havePromForCoronary)
            {
                var proms = his.GetPromCurrentForCoronaryDiseases(ipd.Id).Select(x => new PromResponse
                {
                    VisitId = x.Id,
                    ViName = x.ViName,
                    EnName = x.EnName,
                    CreatedAt = x.CreatedAt,
                    VisitTypeGroup = x.VisitTypeGroup,
                    Username = x.Username,
                    VisitCode = x.VisitCode,
                    PromSumaries = x.PromSumarieObj.OrderBy(e => e.Order).ToList(),
                    HavePromForCoronary = true,
                    havePromHeart = false
                }).ToList();
                if (proms.Count > 0)
                {
                    promForCoronaryNews.AddRange(proms);
                }
            }
            if (havePromHeart == true)
            {
                var proms = his.GetPromCurrentForheartFailures(ipd.Id).Select(x => new PromResponse
                {
                    VisitId = x.Id,
                    ViName = x.ViName,
                    EnName = x.EnName,
                    CreatedAt = x.CreatedAt,
                    VisitTypeGroup = x.VisitTypeGroup,
                    Username = x.Username,
                    VisitCode = x.VisitCode,
                    PromSumaries = x.PromSumarieObj.OrderBy(e => e.Order).ToList(),
                    HavePromForCoronary = false,
                    havePromHeart = true
                }).ToList();
                if (proms.Count > 0)
                {
                    promForHeartNews.AddRange(proms);
                }
            }

            var resultInfos = GetInfoAllAreaInCustomerIdForProm((Guid)ipd.CustomerId, ipd.AdmittedDate).Select(e => new
            {
                Id = (Guid)e.Id,
                VisitType = e.Type,
                AdmittedDate = e.ExaminationTime,
            }).OrderByDescending(e => e.AdmittedDate).ToList();
            if (resultInfos.Count > 0)
            {
                oldVisitGroup = resultInfos[0]?.VisitType;
                if (!string.IsNullOrEmpty(oldVisitGroup) && oldVisitGroup == "OPD")
                {
                    Guid idOld = resultInfos[0].Id;
                    havePromForCoronary = unitOfWork.PROMForCoronaryDiseaseRepository.Find(x => x.VisitId == idOld && !x.IsDeleted).Any();
                    havePromHeart = unitOfWork.PROMForheartFailureRepository.Find(x => x.VisitId == idOld && !x.IsDeleted).Any();

                    if (havePromForCoronary)
                    {
                        var proms = his.GetPromCurrentForCoronaryDiseases(idOld).Select(x => new PromResponse
                        {
                            VisitId = x.Id,
                            ViName = x.ViName,
                            EnName = x.EnName,
                            CreatedAt = x.CreatedAt,
                            VisitTypeGroup = x.VisitTypeGroup,
                            Username = x.Username,
                            VisitCode = x.VisitCode,
                            PromSumaries = x.PromSumarieObj.OrderBy(e => e.Order).ToList(),
                            HavePromForCoronary = true,
                            havePromHeart = false
                        }).ToList();
                        if (proms.Count > 0)
                        {
                            promForCoronaryNews.AddRange(proms);
                        }
                    }
                    if (havePromHeart == true)
                    {
                        var proms = his.GetPromCurrentForheartFailures(idOld).Select(x => new PromResponse
                        {
                            VisitId = x.Id,
                            ViName = x.ViName,
                            EnName = x.EnName,
                            CreatedAt = x.CreatedAt,
                            VisitTypeGroup = x.VisitTypeGroup,
                            Username = x.Username,
                            VisitCode = x.VisitCode,
                            PromSumaries = x.PromSumarieObj.OrderBy(e => e.Order).ToList(),
                            HavePromForCoronary = false,
                            havePromHeart = true
                        }).ToList();
                        if (proms.Count > 0)
                        {
                            promForHeartNews.AddRange(proms);
                        }
                    }
                }
            }



            // thực hiện đồng bộ tab 2 huyvv6
            return new
            {
                Specialty = new { spec?.ViName, spec?.EnName },
                IsNew = isNew,
                IsLocked = IPDIsBlock(ipd, formCode.ToUpper()),
                part_2.Id,
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                Datas = SyncData(ipd, part_2),
                GeneralDatas = general_datas,
                ListStatus = GetListStatus(ipd.IsDraft),
                TranferInfo = GetIPDTransferVisitInfo(ipd),
                Version = ipd.Version > currentVersion ? ipd.Version : currentVersion,
                Allergy = visitAllergy.IsAllergy == true ? visitAllergy.Allergy : "",
                FullNameCreated = fullNameCreated,
                CreateBy = part_2?.CreatedBy,
                CreateAt = part_2?.CreatedAt,
                FullNameUpdated = fullNameUpdated,
                UpdateBy = part_2?.UpdatedBy,
                UpdateAt = part_2?.UpdatedAt,
                GrowthProcess = GetGrowthProcess(ipd.Customer.PID, ipd.CreatedAt),
                Cannang = AutoFillFromInitialAssessment(ipd, "IPDIAAUWEIGANS"),
                VongDau = vongDau,
                NhipTho = AutoFillFromInitialAssessment(ipd, "IPDIAAURERAANS"),
                InfoOldSan = new
                {
                    FormCode = formCodeSan,
                    FormId = oldFormId,
                    Forms = oldDatasSanTablePT,
                    VisitId = oldIpdId,
                    specialty = specialtyOld
                },
                IsUseHandOverCheckList = ipd.HandOverCheckList?.IsUseHandOverCheckList,
                IsCreatedPreProcedureRiskAssessment = isCreatedPreProcedureRiskAssessment,
                SpecialistExaminationsOPDHistories = specialistExaminationsOPD,
                Prom = new
                {
                    HavePromForCoronary = havePromForCoronary,
                    havePromHeart = havePromHeart,
                    PromHistoryForCoronaryDiseases = his.GetPromForCoronaryDiseasesHistory().Select(x => new PromResponse
                    {
                        VisitId = x.Id,
                        ViName = x.ViName,
                        EnName = x.EnName,
                        CreatedAt = x.CreatedAt,
                        VisitTypeGroup = x.VisitTypeGroup,
                        Username = x.Username,
                        VisitCode = x.VisitCode,
                        PromSumaries = x.PromSumarieObj.OrderBy(e => e.Order).ToList()
                    }),
                    PromHistoryForHeartFailure = his.GetPromForheartFailuresHistory().Select(x => new PromResponse
                    {
                        VisitId = x.Id,
                        ViName = x.ViName,
                        EnName = x.EnName,
                        CreatedAt = x.CreatedAt,
                        VisitTypeGroup = x.VisitTypeGroup,
                        Username = x.Username,
                        VisitCode = x.VisitCode,
                        PromSumaries = x.PromSumarieObj.OrderBy(e => e.Order).ToList()
                    }),
                    PromHistoryForCoronaryDiseasesNew = promForCoronaryNews.OrderByDescending(x => x.CreatedAt).ToList(),
                    PromHistoryForHeartFailureNew = promForHeartNews.OrderByDescending(x => x.CreatedAt).ToList(),
                    OldPromVisitGroup = oldVisitGroup,
                }
            };
        }
        public void GetCurrentProm(Guid visitId)
        {

        }
        protected float? AutoFillFromInitialAssessment(IPD ipd, string masterDatacode)
        {
            float? weight = null;
            var medical_record = ipd.IPDMedicalRecord;
            var part2_Id = medical_record.IPDMedicalRecordPart2Id;
            var initialAssessmentId = ipd.IPDInitialAssessmentForAdultId;
            if (initialAssessmentId == null)
            {
                var checkCannang = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(
                                    c => !c.IsDeleted && c.Code == "IPDIAAUWEIGANS" && c.IPDMedicalRecordPart2Id == part2_Id
                                    );
                if (checkCannang == null || checkCannang.Value == "" || checkCannang.Value == null) return weight;
                else
                {
                    weight = FloatTryParse(checkCannang.Value);
                    return weight;
                }
            }
            else
            {
                var checkInitialAssessmentData = unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                                            .Where(d => d.IsDeleted && d.IPDInitialAssessmentForAdultId == initialAssessmentId);
                if (checkInitialAssessmentData == null)
                {
                    var checkCannang = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(
                                   c => !c.IsDeleted && c.Code == "IPDIAAUWEIGANS" && c.IPDMedicalRecordPart2Id == part2_Id
                                   );
                    if (checkCannang == null || checkCannang.Value == "" || checkCannang.Value == null) return weight;
                    else
                    {
                        weight = FloatTryParse(checkCannang.Value);
                        return weight;
                    }
                }
                else
                {
                    var codeMasterData = unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                          .Where(w => !w.IsDeleted && w.IPDInitialAssessmentForAdultId == initialAssessmentId && w.Code == masterDatacode)
                          .Select(w => w.Value)
                          .FirstOrDefault();

                    if (codeMasterData == null || codeMasterData == "") return weight;
                    return FloatTryParse(codeMasterData);
                }
            }
        }

        protected dynamic BuildMedicalRecordPart3Result(string formCode, IPD ipd, IPDMedicalRecord medical_record, IPD ipdAsync = null)
        {
            string formCodeSan = "IPDSANTHPT1";
            String oldFormId = null;
            String oldIpdId = null;

            var general_datas = medical_record.IPDMedicalRecordDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });
            var part_1 = medical_record.IPDMedicalRecordPart1;
            IPDMedicalRecordPart2 part_2 = medical_record.IPDMedicalRecordPart2;
            var part_3 = medical_record.IPDMedicalRecordPart3;
            var datas = part_3.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });
            var status = ipd.EDStatus;

            var parrent_info = new List<MasterDataValue>();

            if (part_1 != null)
            {
                parrent_info = part_1.IPDMedicalRecordPart1Datas.Where(e => !e.IsDeleted && (e.Code == "IPDMRPO57" || e.Code == "IPDMRPO63")).Select(e => new MasterDataValue { Value = e.Value, Code = e.Code }).ToList();
            }

            var spec = ipd.Specialty;
            var fullNameCreated = String.IsNullOrEmpty(part_3?.CreatedBy) == false ? unitOfWork.UserRepository.FirstOrDefault(x => x.Username == part_3.CreatedBy)?.Fullname : "";
            var fullNameUpdated = String.IsNullOrEmpty(part_3?.UpdatedBy) == false ? unitOfWork.UserRepository.FirstOrDefault(x => x.Username == part_3.UpdatedBy)?.Fullname : "";
            string statusCode = ipd.EDStatus.Code.ToUpper();
            string moveIn = null;

            string[] code = new string[]
            {
                "IPDOAGIANM101","IPDOAGIANM103","IPDOAGIANM105"
            };

            var ipdId = ipd.Id;
            var dataInfor = AutoFillDataFromMasterData(code, ipdId);
            switch (statusCode)
            {
                case "IPDUDT":
                    moveIn = general_datas?.FirstOrDefault(x => x.Code == "IPDRH1ANS").Value.ToString();
                    break;
                case "IPDIHT":
                    moveIn = general_datas?.FirstOrDefault(x => x.Code == "IPDRH0ANS")?.Value.ToString();
                    break;
            }
            int currentVersion = getVersionOfMedicalrecord(formCode: formCode, visitId: ipd.Id, setCurrentVersion: 2);

            var isNew = ipdAsync != null ? CheckIsNewByTab((Guid)ipdAsync?.IPDMedicalRecord?.IPDMedicalRecordPart3Id, formCode) : CheckIsNewByTab(part_3.Id, formCode);

            // thực hiện đồng bộ tab 3 trường 5 (table) huyvv6
            var oldDatasSanTablePT = new List<MasterdataForm>();
            var results = ipdAsync != null ? GetAllInfoCustomerInAreIPD(ipdAsync) : GetAllInfoCustomerInAreIPD(ipd);
            Guid id = ipdAsync != null ? ipdAsync.Id : ipd.Id;
            object specialtyOld = null;
            if (results.Count > 1 && ipdAsync != null)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].Id == id)
                        continue;
                    Guid oldIpdIdtmp = (Guid)results[i].Id;
                    oldIpdId = oldIpdIdtmp.ToString();
                    var forms = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == oldIpdIdtmp && e.FormCode == formCodeSan && e.VisitTypeGroupCode == "IPD");
                    if (forms != null && forms.Count() > 0)
                    {
                        //oldFormId = forms.Id.ToString();

                        var formsCurrent = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == id && e.FormCode == formCodeSan && e.VisitTypeGroupCode == "IPD").ToList();

                        var isNewEIOForm = formsCurrent.Count > 0 ? false : true;
                        oldDatasSanTablePT = forms.OrderBy(x => x.CreatedAt).Select(form => FormatOutput(form, formCodeSan, isNewEIOForm)).ToList();
                        break;
                    }
                    specialtyOld = results[i].Specialty;

                }
            }
            // end thực hiện đồng bộ tab 3 trường 5 (table) huyvv6
            return new
            {
                Specialty = new { spec?.ViName, spec?.EnName },
                IsNew = isNew,
                IsLocked = IPDIsBlock(ipd, formCode.ToUpper()),
                part_3.Id,
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                Datas = datas,
                Part2 = part_2 != null ? part_2.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted).Select(e => new { e.Id, e.Code, e.Value, UpdatedAt = e.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND), e.UpdatedBy }) : null,
                GeneralDatas = general_datas,
                ListStatus = GetListStatus(ipd.IsDraft),
                Version = ipd.Version > currentVersion ? ipd.Version : currentVersion,
                FullNameCreated = fullNameCreated,
                CreateBy = part_3?.CreatedBy,
                CreateAt = part_3?.CreatedAt,
                FullNameUpdated = fullNameUpdated,
                UpdateBy = part_3?.UpdatedBy,
                UpdateAt = part_3?.UpdatedAt,
                MoveIn = moveIn,
                ParrentInfo = parrent_info,
                DataInfor = dataInfor,
                InfoOldSan = new
                {
                    FormCode = formCodeSan,
                    FormId = oldFormId,
                    Forms = oldDatasSanTablePT,
                    VisitId = oldIpdId,
                    specialty = specialtyOld
                },
                IsUseHandOverCheckList = ipd.HandOverCheckList?.IsUseHandOverCheckList,
                IsAcceptPhysician = ipd.HandOverCheckList?.IsAcceptPhysician,
                IsAcceptNurse = ipd.HandOverCheckList?.IsAcceptNurse,
                IsCheckTransfer = IsCheckTransfer(ipd)
            };
        }
        public List<dynamic> SynsVitalSigFromInitialAsseesment(IPD curren_visit)
        {
            var visitId = GetfirstVisit(curren_visit);

            var newborn_tab = new EIOContraintNewbornAndPregnantWomanController().GetListTabNewborn(visitId, "IPD", "A02_016_050919_VE_ForNeonatalMaternity_Obstetrics");
            string[] codes_masterdata = { "IPDOAGIANM101", "IPDOAGIANM103", "IPDOAGIANM105" };
            var forms_initia = (from form in unitOfWork.IPDInitialAssessmentForNewbornsRepository.AsQueryable()
                                .Where(e => !e.IsDeleted && e.VisitId == visitId && e.DataType != null && e.DataType.ToUpper() == "ForNeonatalMaternity_Obstetrics".ToUpper())
                                join data in unitOfWork.FormDatasRepository.AsQueryable()
                                .Where(e => !e.IsDeleted && e.FormCode.ToUpper() == "A02_016_050919_VE_ForNeonatalMaternity_Obstetrics".ToUpper() && codes_masterdata.Contains(e.Code) && e.VisitId == visitId)
                                on form.Id equals data.FormId into temp
                                select new DataNewbornViewModel
                                {
                                    VisitId = form.VisitId,
                                    FormId = form.Id,
                                    CreatedAt = form.CreatedAt,
                                    EIOConstraintNewbornAndPregnantWomanId = form.EIOConstraintNewbornAndPregnantWomanId,
                                    Datas = temp
                                }).ToList();

            SaveDataFormInitial(visitId, curren_visit, ref forms_initia, ref newborn_tab);

            var result = (from nb in newborn_tab
                          join data in forms_initia
                          on nb.Id equals data.EIOConstraintNewbornAndPregnantWomanId
                          where nb.PID != null
                          orderby data.CreatedAt
                          select new
                          {
                              Newborn = nb,
                              Datas = new
                              {
                                  VongDau = data.Datas.Where(e => e.Code == "IPDOAGIANM105").Select(e => e.Value).FirstOrDefault(),
                                  CanNang = data.Datas.Where(e => e.Code == "IPDOAGIANM101").Select(e => e.Value).FirstOrDefault(),
                                  ChieuCao = data.Datas.Where(e => e.Code == "IPDOAGIANM103").Select(e => e.Value).FirstOrDefault()
                              },
                              VisitId = visitId
                          }).ToList();
            return new List<dynamic>(result);
        }
        private void SaveDataFormInitial(Guid save_visitId, IPD visit_inProgress, ref List<DataNewbornViewModel> data_initial, ref List<dynamic> newborn_tab)
        {
            if (visit_inProgress.Version < 9)
                return;
            Guid? part3_id = visit_inProgress.IPDMedicalRecord?.IPDMedicalRecordPart3Id;
            if (part3_id == null)
                return;

            string[] code_md = { "IPDOAGIANM103", "IPDOAGIANM101", "IPDOAGIANM105" };
            string formCode = visit_inProgress.Id.ToString().ToUpper() + "_IPDPMPS";

            bool isNew = CheckIsNewByTab((Guid)part3_id, "A01_035_050919_V");
            if (!isNew)
            {
                var datas = unitOfWork.FormDatasRepository.AsQueryable().Where(e => !e.IsDeleted && e.FormCode == formCode).OrderByDescending(e => e.CreatedAt).ToList();
                Guid? visitId = datas.FirstOrDefault()?.VisitId;
                if (visitId != null && visitId != save_visitId)
                {
                    IPD ipd_save = GetIPD((Guid)visitId);
                    var ipdPm_ipdPs = ipd_save?.Specialty?.Code;
                    if (ipdPm_ipdPs == "IPDPM" || ipdPm_ipdPs == "IPDPS")
                    {
                        foreach (var item in data_initial)
                        {
                            var list_dataInitial = item.Datas.Where(e => code_md.Contains(e.Code)).ToList();
                            CreateOrUpdateDataNewborn(list_dataInitial, datas, formCode);
                        }
                        return;
                    }
                    else
                    {
                        newborn_tab.Clear();
                        newborn_tab = new EIOContraintNewbornAndPregnantWomanController().GetListTabNewborn((Guid)ipd_save?.Id, "IPD", "A02_016_050919_VE_ForNeonatalMaternity_Obstetrics");
                        data_initial.Clear();
                        data_initial = GetDataNewbornFromFormDatas(ipd_save?.Id, datas);
                        return;
                    }
                }
                foreach (var item in data_initial)
                {
                    var list_dataInitial = item.Datas.Where(e => code_md.Contains(e.Code)).ToList();
                    CreateOrUpdateDataNewborn(list_dataInitial, datas, formCode);
                }
            }
        }
        private void CreateOrUpdateDataNewborn(List<FormDatas> datas, List<FormDatas> curren_datas, string formCode)
        {
            foreach (var data in datas)
            {
                var find = curren_datas.FirstOrDefault(e => e.Code == data.Code && e.FormId == data.FormId);
                if (find == null)
                {
                    FormDatas new_data = new FormDatas()
                    {
                        VisitId = data.VisitId,
                        FormId = data.FormId,
                        Code = data.Code,
                        VisitType = data.VisitType,
                        FormCode = formCode,
                        Value = data.Value
                    };
                    unitOfWork.FormDatasRepository.Add(new_data);
                }
                else
                {
                    find.Value = data.Value;
                    find.VisitId = data.VisitId;
                    find.FormId = data.FormId;
                    unitOfWork.FormDatasRepository.Update(find);
                }
            }
            unitOfWork.Commit();
        }
        private List<DataNewbornViewModel> GetDataNewbornFromFormDatas(Guid? visitId_saved, List<FormDatas> datas_saved)
        {
            var datas = datas_saved.Where(e => e.VisitId == visitId_saved);
            var forms = (from e in unitOfWork.IPDInitialAssessmentForNewbornsRepository.AsQueryable()
                         where !e.IsDeleted && e.VisitId == visitId_saved && e.DataType != null
                         && e.DataType.ToUpper() == "ForNeonatalMaternity_Obstetrics".ToUpper()
                         select e).ToArray();

            var forms_initia =(from f in forms
                               join data in datas
                               on f.Id equals data.FormId into temp
                               select new DataNewbornViewModel
                               {
                                   VisitId = f.VisitId,
                                   FormId = f.Id,
                                   CreatedAt = f.CreatedAt,
                                   EIOConstraintNewbornAndPregnantWomanId = f.EIOConstraintNewbornAndPregnantWomanId,
                                   Datas = temp
                               }).ToList();

            return forms_initia;
        }
        private Guid GetfirstVisit(IPD curren_visit)
        {
            List<IPD> list_visit = new List<IPD>();
            list_visit.Add(curren_visit);
            Guid? custormerId = curren_visit.CustomerId;
            Guid? tranferId = curren_visit.TransferFromId;
            while (true)
            {
                var visit = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.HandOverCheckListId == tranferId && e.CustomerId == custormerId);
                if (visit == null)
                    break;
                list_visit.Add(visit);

                if (visit.TransferFromId == null || visit.TransferFromId == Guid.Empty || visit.TransferFromId == tranferId)
                    break;
                tranferId = visit.TransferFromId;
            }

            list_visit = list_visit.OrderBy(e => e.CreatedAt).ToList();

            if (curren_visit.Version < 9)
            {
                foreach (var visit in list_visit)
                {
                    var specialty_setup = unitOfWork.IPDSetupMedicalRecordRepository
                                        .FirstOrDefault(e => !e.IsDeleted && e.SpecialityId == visit.SpecialtyId && e.Formcode.ToUpper() == "A01_035_050919_V" && e.IsDeploy == true);
                    if (specialty_setup != null)
                        return visit.Id;

                    var formIsCreated = unitOfWork.IPDMedicalRecordOfPatientRepository
                                        .FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.Id && e.FormCode.ToUpper() == "A01_035_050919_V");
                    if (formIsCreated != null)
                        return visit.Id;
                }
                return curren_visit.Id;
            }
            else if (curren_visit.Version >= 9)
            {
                var last_pm_ps = list_visit.Where(e => e.Specialty.Code != null && (e.Specialty.Code == "IPDPM" || e.Specialty.Code == "IPDPS"))
                                .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                if (last_pm_ps == null)
                    return Guid.Empty;
                return last_pm_ps.Id;
            }
            else
                return Guid.Empty;
        }
        protected TransferInfoModel GetTranferInfo(Guid id)
        {
            var ipd = GetIPD(id);

            var transfers = new IPDTransfer(ipd).GetListInfo();
            var spec = ipd.Specialty;
            var current_doctor = ipd.PrimaryDoctor;
            TransferInfoModel first_ipd = new TransferInfoModel();
            if (transfers.Count() > 0)
            {
                first_ipd = transfers.FirstOrDefault(e => e.CurrentType == "IPD");
            }
            else
            {

                first_ipd = new TransferInfoModel()
                {
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentSpecialty = new { spec?.ViName, spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                };
            }
            return first_ipd;
        }
        private List<GrowthProcess> GetGrowthProcess(string pid, DateTime? cr)
        {
            if (string.IsNullOrWhiteSpace(pid)) return new List<GrowthProcess>();

            List<VisitSetupFormModel> ipds = (from ipd_sql in unitOfWork.IPDRepository.AsQueryable().Where(e => !e.IsDeleted && e.CreatedAt < cr)
                                              join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                                                  on ipd_sql.CustomerId equals cus_sql.Id
                                              join re_of in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                                 on ipd_sql.Id equals re_of.VisitId
                                              join re_us in unitOfWork.UserRepository.AsQueryable()
                                                  on ipd_sql.PrimaryDoctorId equals re_us.Id into nlist
                                              from re_us in nlist.DefaultIfEmpty()
                                              select new VisitSetupFormModel
                                              {
                                                  Id = ipd_sql.Id,
                                                  Code = re_of.FormCode,
                                                  IPDMedicalRecord = ipd_sql.IPDMedicalRecord,
                                                  PID = cus_sql.PID,
                                                  IPDMedicalRecordId = ipd_sql.IPDMedicalRecordId,
                                                  CreatedAt = ipd_sql.CreatedAt,
                                                  VisitCode = ipd_sql.VisitCode,
                                                  Doctor = re_us.Username,
                                                  FormId = re_of.FormId,
                                                  AdmittedDate = ipd_sql.AdmittedDate
                                              }).Where(e => e.Code == "A01_037_050919_V" && e.PID == pid && e.IPDMedicalRecordId != null).OrderBy(e => e.CreatedAt).Distinct().ToList();



            var data = new List<GrowthProcess>();
            var list_md_code = new List<string>() {
                "IPDMRPT07", "IPDMRPT10", "IPDMRPT12", "IPDMRPT14", "IPDMRPT16", "IPDMRPT18", "IPDMRPT19", "IPDMRPT20", "IPDMRPT21", "IPDMRPT22", "IPDMRPT23", "IPDMRPT24", "IPDMRPT26", "IPDMRPT28", "IPDMRPT29", "IPDMRPT30",
                "IPDMRPT32", "IPDMRPT34", "IPDMRPT36", "IPDMRPT38", "IPDMRPT39", "IPDMRPT40",  "IPDMRPT41", "IPDMRPT42", "IPDMRPT43",
                "IPDMRPT44", "IPDMRPT45", "IPDMRPT46", "IPDMRPT47", "IPDMRPT48", "IPDMRPT49", "IPDMRPT50", "IPDMRPT51", "IPDMRPT52", "IPDMRPT53", "IPDMRPT54", "IPDMRPT55"
            };
            foreach (var ipd in ipds)
            {
                // check part2 của Nhi có tồn tại
                IPDMedicalRecordPart2 part_2 = ipd.IPDMedicalRecord.IPDMedicalRecordPart2;
                if (part_2 != null && ipd.FormId == part_2.Id)
                {
                    var tranferInfo = GetTranferInfo(ipd.Id);
                    var data_part2 = (from ipd_md_sql in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(e => !e.IsDeleted && e.IPDMedicalRecordPart2Id == part_2.Id && list_md_code.Contains(e.Code))
                                      join re_of in unitOfWork.MasterDataRepository.AsQueryable()
                                      on ipd_md_sql.Code equals re_of.Code
                                      select new MasterDataValue
                                      {
                                          Code = ipd_md_sql.Code,
                                          Value = ipd_md_sql.Value,
                                          UpdatedBy = ipd_md_sql.UpdatedBy,
                                          CreatedBy = ipd_md_sql.CreatedBy,
                                          Label = re_of.ViName,
                                          ViName = re_of.ViName,
                                          EnName = re_of.EnName,
                                          GroupCode = re_of.Group,
                                          Lv = re_of.Level
                                      }
                                      ).OrderBy(e => e.Code).ToList();
                    data.Add(new GrowthProcess
                    {
                        Id = ipd.Id,
                        VisitCode = ipd.VisitCode,
                        RawVisitDate = ipd.AdmittedDate,
                        VisitDate = ipd.AdmittedDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                        Username = ipd.Doctor,
                        DoctorUsername = ipd.Doctor,
                        Data = data_part2
                    });
                    data = data.ToList().OrderByDescending(e => e.RawVisitDate).ToList();
                }
            }
            return data;
        }
        protected dynamic GetIPDTransferVisitInfo(IPD visit)
        {
            if (!visit.IsTransfer)
                return null;
            var transfer_from_id = visit.TransferFromId;
            var eocHandOver = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Id != null &&
                e.Id == transfer_from_id
            );

            if (eocHandOver != null)
            {
                var eoc = unitOfWork.EOCRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == eocHandOver.VisitId);
                if (eoc != null)
                    return GetTransferVisitInfo(eoc);
            }

            var ed = unitOfWork.EDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.HandOverCheckListId != null &&
                e.HandOverCheckListId == transfer_from_id
            );
            if (ed != null)
                return GetTransferVisitInfo(ed);

            var opd = unitOfWork.OPDRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               e.OPDHandOverCheckListId != null &&
               e.OPDHandOverCheckListId == transfer_from_id
            );
            if (opd != null)
                return GetTransferVisitInfo(opd);

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               e.HandOverCheckListId != null &&
               e.HandOverCheckListId == transfer_from_id
            );
            if (ipd != null)
                return GetTransferVisitInfo(ipd);
            return null;
        }
        protected dynamic SyncData(IPD ipd, IPDMedicalRecordPart2 part_2)
        {
            var vital_sign = GetVitalSign(
                ipd.IPDInitialAssessmentForAdultId,
                new string[] {
                    "IPDIAAUPULSANS", "IPDIAAUBLPRANS", "IPDIAAUTEMPANS", "IPDIAAURERAANS", "IPDIAAUHEIGANS", "IPDIAAUWEIGANS"
                }
            );

            var datas = part_2.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted).ToList();
            foreach (var sign in vital_sign)
            {
                switch (sign.Code)
                {
                    case "IPDIAAUPULSANS":
                        var pulse = GetOrCreateIPDMedicalRecordPart2Data(datas, part_2.Id, "IPDMRPTMACHANS", sign.Value);
                        if (pulse != null)
                            UpdateIPDMedicalRecordPart2Data(pulse, sign.Value);
                        break;
                    case "IPDIAAUTEMPANS":
                        var temp = GetOrCreateIPDMedicalRecordPart2Data(datas, part_2.Id, "IPDMRPTNHDOANS", sign.Value);
                        if (temp != null)
                            UpdateIPDMedicalRecordPart2Data(temp, sign.Value);
                        break;
                    case "IPDIAAUBLPRANS":
                        var bp = GetOrCreateIPDMedicalRecordPart2Data(datas, part_2.Id, "IPDMRPTHUAPANS", sign.Value);
                        if (bp != null)
                            UpdateIPDMedicalRecordPart2Data(bp, sign.Value);
                        break;
                    case "IPDIAAURERAANS":
                        var rr = GetOrCreateIPDMedicalRecordPart2Data(datas, part_2.Id, "IPDMRPTNHTHANS", sign.Value);
                        if (rr != null)
                            UpdateIPDMedicalRecordPart2Data(rr, sign.Value);
                        break;
                    case "IPDIAAUHEIGANS":
                        var height = GetOrCreateIPDMedicalRecordPart2Data(datas, part_2.Id, "IPDMRPTCICAANS", sign.Value);
                        if (height != null)
                            UpdateIPDMedicalRecordPart2Data(height, sign.Value);
                        break;
                    case "IPDIAAUWEIGANS":
                        var weight = GetOrCreateIPDMedicalRecordPart2Data(datas, part_2.Id, "IPDMRPTCANAANS", sign.Value);
                        if (weight != null)
                            UpdateIPDMedicalRecordPart2Data(weight, sign.Value);
                        break;

                    default:
                        break;
                }
            }
            unitOfWork.Commit();
            return part_2.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value });
        }

        protected void HandleIPDMedicalRecordDatas(IPDMedicalRecord mere, JToken request_data)
        {
            var datas = mere.IPDMedicalRecordDatas.Where(e => !e.IsDeleted).ToList();

            foreach (var item in request_data)
            {
                var code = item["Code"]?.ToString();
                if (code == null || "IPDMRPTNONHANS,IPDMRPTLDCKANS".Contains(code))
                    continue;

                var value = item["Value"]?.ToString();

                var data = GetOrCreateIPDMedicalRecordData(datas, mere.Id, code);
                if (data != null)
                    UpdateIPDMedicalRecordData(data, value);
            }
            mere.UpdatedBy = GetUser().Username;
            unitOfWork.IPDMedicalRecordRepository.Update(mere);
            unitOfWork.Commit();
        }
        protected void UpdateOrCreateIPDMedicalRecordData(Guid? form_id, string code, string value, bool ischeck)
        {
            if (!ischeck)
            {
                var data = unitOfWork.IPDMedicalRecordDataRepository.FirstOrDefault(
               e => !e.IsDeleted &&
               e.IPDMedicalRecordId != null &&
               e.IPDMedicalRecordId == form_id &&
               !string.IsNullOrEmpty(e.Code) &&
               e.Code == code
           );
                if (data != null)
                {
                    data.Value = value;
                    unitOfWork.IPDMedicalRecordDataRepository.Update(data);
                    return;
                }
                data = new IPDMedicalRecordData
                {
                    IPDMedicalRecordId = form_id,
                    Code = code,
                    Value = value
                };
                unitOfWork.IPDMedicalRecordDataRepository.Add(data);
            }
        }
        protected IPDMedicalRecordData GetOrCreateIPDMedicalRecordData(List<IPDMedicalRecordData> list_data, Guid form_id, string code)
        {
            var data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new IPDMedicalRecordData
            {
                IPDMedicalRecordId = form_id,
                Code = code,
            };
            unitOfWork.IPDMedicalRecordDataRepository.Add(data);
            return data;
        }
        protected void UpdateIPDMedicalRecordData(IPDMedicalRecordData data, string value)
        {
            data.Value = value;
            unitOfWork.IPDMedicalRecordDataRepository.Update(data);
        }

        protected void HandleIPDMedicalRecordPart1Datas(IPDMedicalRecord mere, IPDMedicalRecordPart1 part_1, JToken request_data)
        {
            var datas = part_1.IPDMedicalRecordPart1Datas.Where(e => !e.IsDeleted).ToList();

            foreach (var item in request_data)
            {
                var code = item["Code"]?.ToString();
                if (code == null)
                    continue;

                var value = item["Value"]?.ToString();

                var data = GetOrCreateIPDMedicalRecordPart1Data(datas, part_1.Id, code, value);
                if (data != null)
                    UpdateIPDMedicalRecordPart1Data(data, value);
            }
            unitOfWork.IPDMedicalRecordPart1Repository.Update(part_1);
            unitOfWork.IPDMedicalRecordRepository.Update(mere);
            unitOfWork.Commit();
        }
        protected IPDMedicalRecordPart1Data GetOrCreateIPDMedicalRecordPart1Data(List<IPDMedicalRecordPart1Data> list_data, Guid form_id, string code, string value)
        {
            var data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;
            if (string.IsNullOrWhiteSpace(value)) return null;
            data = new IPDMedicalRecordPart1Data
            {
                IPDMedicalRecordPart1Id = form_id,
                Code = code,
            };
            unitOfWork.IPDMedicalRecordPart1DataRepository.Add(data);
            return data;
        }
        protected void UpdateIPDMedicalRecordPart1Data(IPDMedicalRecordPart1Data data, string value)
        {
            data.Value = value;
            unitOfWork.IPDMedicalRecordPart1DataRepository.Update(data);
        }
        protected void HandleIPDMedicalRecordPart2Datas(IPDMedicalRecord mere, IPDMedicalRecordPart2 part_2, JToken request_data)
        {
            var datas = part_2.IPDMedicalRecordPart2Datas.Where(e => !e.IsDeleted).ToList();

            foreach (var item in request_data)
            {
                var code = item["Code"]?.ToString();
                if (code == null)
                    continue;

                var value = item["Value"]?.ToString();

                var data = GetOrCreateIPDMedicalRecordPart2Data(datas, part_2.Id, code, value);
                if (data != null)
                    UpdateIPDMedicalRecordPart2Data(data, value);
            }
            unitOfWork.IPDMedicalRecordPart2Repository.Update(part_2);
            unitOfWork.IPDMedicalRecordRepository.Update(mere);

            unitOfWork.Commit();
        }
        protected IPDMedicalRecordPart2Data GetOrCreateIPDMedicalRecordPart2Data(List<IPDMedicalRecordPart2Data> list_data, Guid form_id, string code, string value)
        {
            var data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;
            if (string.IsNullOrWhiteSpace(value)) return null;
            data = new IPDMedicalRecordPart2Data
            {
                IPDMedicalRecordPart2Id = form_id,
                Code = code,
            };
            unitOfWork.IPDMedicalRecordPart2DataRepository.Add(data);
            return data;
        }
        protected void UpdateIPDMedicalRecordPart2Data(IPDMedicalRecordPart2Data data, string value)
        {
            data.Value = value;
            unitOfWork.IPDMedicalRecordPart2DataRepository.Update(data);
        }

        protected void HandleIPDMedicalRecordPart3Datas(IPD ipd, IPDMedicalRecordPart3 part_3, JToken request_data)
        {
            var datas = part_3.IPDMedicalRecordPart3Datas.Where(e => !e.IsDeleted).ToList();

            foreach (var item in request_data)
            {
                var code = item["Code"]?.ToString();
                if (code == null)
                    continue;

                var value = item["Value"]?.ToString();

                var data = GetOrCreateIPDMedicalRecordPart3Data(datas, part_3.Id, code, value);
                if (data != null)
                    UpdateIPDMedicalRecordPart3Data(data, value);
            }

            var user = GetUser();
            part_3.UpdatedBy = user?.Username;
            unitOfWork.IPDMedicalRecordPart3Repository.Update(part_3);

            var mere = ipd.IPDMedicalRecord;
            mere.UpdatedBy = user?.Username;
            unitOfWork.IPDMedicalRecordRepository.Update(mere);

            // ipd.PrimaryDoctorId = user?.Id;
            unitOfWork.IPDRepository.Update(ipd);
            unitOfWork.Commit();
        }
        protected IPDMedicalRecordPart3Data GetOrCreateIPDMedicalRecordPart3Data(List<IPDMedicalRecordPart3Data> list_data, Guid form_id, string code, string value)
        {
            var data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;
            if (string.IsNullOrWhiteSpace(value)) return null;

            data = new IPDMedicalRecordPart3Data
            {
                IPDMedicalRecordPart3Id = form_id,
                Code = code,
            };
            unitOfWork.IPDMedicalRecordPart3DataRepository.Add(data);
            return data;
        }
        protected void UpdateIPDMedicalRecordPart3Data(IPDMedicalRecordPart3Data data, string value)
        {
            data.Value = value;
            unitOfWork.IPDMedicalRecordPart3DataRepository.Update(data);
        }

        private dynamic IPDValiDatePIDAndVisitCode(IPD visit, string status_code)
        {
            if (Constant.InHospital.Contains(status_code) || Constant.WaitingResults.Contains(status_code) || Constant.NoExamination.Contains(status_code) || Constant.Nonhospitalization.Contains(status_code))
                return null;

            string visitCode = visit.VisitCode;
            var custumer = visit.Customer;
            string pId = custumer?.PID;
            if (string.IsNullOrEmpty(visitCode) && string.IsNullOrEmpty(pId))
            {
                string en_err = "Please sync to the visit code and PID of the patient!";
                string vi_err = "Vui lòng đồng bộ lượt tiếp nhận và PID của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            if (string.IsNullOrEmpty(visitCode))
            {
                string en_err = "Please sync to the visit code of the patient!";
                string vi_err = "Vui lòng đồng bộ lượt tiếp nhận của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            if (string.IsNullOrEmpty(pId))
            {
                string en_err = "Please sync to the PID of the patient!";
                string vi_err = "Vui lòng đồng bộ PID của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            return null;
        }

        protected dynamic HandleUpdateStatus(IPD ipd, JToken request_status, JToken request_datas, bool isUseHandOverCheckList)
        {
            var new_status = request_status["Id"].ToString();
            Guid new_status_id = new Guid(new_status);
            if (new_status_id == Guid.Empty) return null;
            var current_status = ipd.EDStatus;
            var new_status_code = request_status["Code"].ToString();
            if (ipd.EDStatusId != new_status_id)
            {
                var error = HandleWhenStatusChange(ipd, request_status, current_status.Code);
                if (error != null)
                    return error;
            }

            DateTime? discharge_date = null;
            if (Constant.Transfer.Contains(new_status_code))
            {
                Guid? receiving_id = null;
                string receiving = request_datas.FirstOrDefault(d => d.Value<string>("Code") == "IPDMRPTNONHANS").Value<string>("Value");
                UpdateOrCreateIPDMedicalRecordData(ipd.IPDMedicalRecordId, "IPDMRPTNONHANS", receiving, IsCheckTransfer(ipd));

                receiving_id = new Guid(receiving);

                string reason = request_datas.FirstOrDefault(d => d.Value<string>("Code") == "IPDMRPTLDCKANS").Value<string>("Value");
                UpdateOrCreateIPDMedicalRecordData(ipd.IPDMedicalRecordId, "IPDMRPTLDCKANS", reason, false);

                discharge_date = CreateOrUpdateTransferForAnotherDepartment(ipd, receiving_id, reason, isUseHandOverCheckList);
            }
            else if (Constant.Discharged.Contains(new_status_code) || Constant.CompleteTreatment.Contains(new_status_code))
            {
                var discharge = request_datas.FirstOrDefault(d => d.Value<string>("Code") == "IPDMRPTCDRVANS").Value<string>("Value");
                if (!string.IsNullOrEmpty(discharge))
                    discharge_date = DateTime.ParseExact(discharge, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
            else if (Constant.InterHospitalTransfer.Contains(new_status_code))
            {
                var discharge = request_datas.FirstOrDefault(d => d.Value<string>("Code") == "IPDMRPTCHVHANS").Value<string>("Value");
                if (!string.IsNullOrEmpty(discharge))
                    discharge_date = DateTime.ParseExact(discharge, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
            else if (Constant.UpstreamDownstreamTransfer.Contains(new_status_code))
            {
                var discharge = request_datas.FirstOrDefault(d => d.Value<string>("Code") == "IPDTD0ANS").Value<string>("Value");
                if (!string.IsNullOrEmpty(discharge))
                    discharge_date = DateTime.ParseExact(discharge, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }
            else if (Constant.Dead.Contains(new_status_code))
            {
                var discharge = request_datas.FirstOrDefault(d => d.Value<string>("Code") == "IPDMRPTNGTVANS").Value<string>("Value");
                if (!string.IsNullOrEmpty(discharge))
                    discharge_date = DateTime.ParseExact(discharge, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            }

            ipd.DischargeDate = discharge_date;
            unitOfWork.IPDRepository.Update(ipd);
            unitOfWork.Commit();
            return null;
        }

        protected List<MedicalReportDataModels> GetUncompleteField(Guid? mere_id, Guid? part2_id, Guid? part3_id, JToken request_general, string status_code, int? version, string Type = null)
        {
            List<MedicalReportDataModels> uncomplete_field = new List<MedicalReportDataModels>();
            List<MedicalReportDataModels> part2_field = new List<MedicalReportDataModels>();
            List<MedicalReportDataModels> part3_field = new List<MedicalReportDataModels>();
            List<MedicalReportDataModels> gen_field = new List<MedicalReportDataModels>();

            var part2_data = GetMedicalRecordPart2Data(part2_id, Type);
            List<string> mdTMH = new List<string>() { "A01_039_050919_V", "A01_040_050919_V" };
            if (!mdTMH.Contains(Type))
            {
                part2_data = part2_data.Where(e => !mdTMH.Contains(e.Clinic)).ToList();
            }

            var empty_field_2 = GetEmptyTextField(Constant.IPD_MERE_PART_2_TEXT_CODE, part2_data);
            part2_field.AddRange(empty_field_2);

            var part3_data = GetMedicalRecordPart3Data(part3_id);
            var empty_field_3 = GetEmptyTextField(Constant.IPD_MERE_PART_3_TEXT_CODE, part3_data);
            part3_field.AddRange(empty_field_3);

            var kqdt_empty = GetEmptyChoiceField(Constant.IPD_MERE_PART_3_KQDT_CODE["QUES"], Constant.IPD_MERE_PART_3_KQDT_CODE["ANS"], part3_data);
            if (kqdt_empty != null)
                part3_field.Add(kqdt_empty);
            var general_data = GetMedicalRecordGeneralData(mere_id);

            if (Constant.Transfer.Contains(status_code))
            {
                var empty_transfer = GetEmptyTextField(Constant.IPD_MERE_GEN_TRANSFER_CODE, request_general);
                gen_field.AddRange(empty_transfer);
            }
            else if (Constant.Discharged.Contains(status_code))
            {
                var empty_discharged = GetEmptyTextField(Constant.IPD_MERE_GEN_DISCHARGE_CODE, general_data);
                gen_field.AddRange(empty_discharged);

                var htrv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_HTRV_CODE["QUES"], Constant.IPD_MERE_GEN_HTRV_CODE["ANS"], general_data);
                if (htrv_empty != null)
                {
                    gen_field.Add(htrv_empty);
                }
                var is_rv_ChoiceField = general_data.FirstOrDefault(
                        e => !string.IsNullOrEmpty(e.Code) &&
                        e.Code == "IPDMRPTHTRVXIV" &&
                        !string.IsNullOrEmpty(e.Value) &&
                        e.Value.ToLower() == "true"
                    );
                var is_rv_ChoiceFieldOther = general_data.FirstOrDefault(
                        e => !string.IsNullOrEmpty(e.Code) &&
                        e.Code == "IPDMRPGRV04" &&
                        !string.IsNullOrEmpty(e.Value) &&
                        e.Value.ToLower() == "true"
                    );
                var is_rv_ChoiceFieldOtherText = general_data.Where(
                        e => !string.IsNullOrEmpty(e.Code) &&
                        e.Code == "IPDMRPGRV05" &&
                        string.IsNullOrEmpty(e.Value)
                    ).ToList();
                if (is_rv_ChoiceField != null && version == 1)
                {
                    var htrv_rv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_HTRV2_CODE["QUES"], Constant.IPD_MERE_GEN_HTRV2_CODE["ANS"], general_data);
                    if (htrv_rv_empty != null)
                        gen_field.Add(htrv_rv_empty);
                    if (is_rv_ChoiceFieldOther != null && is_rv_ChoiceFieldOtherText.Count > 0)
                    {
                        gen_field.AddRange(is_rv_ChoiceFieldOtherText);
                    }
                }
            }
            else if (Constant.InterHospitalTransfer.Contains(status_code))
            {
                var empty_inter_transfer = GetEmptyTextField(Constant.IPD_MERE_GEN_INTER_CODE, general_data);
                gen_field.AddRange(empty_inter_transfer);
            }
            else if (Constant.Dead.Contains(status_code))
            {
                var empty_dead = GetEmptyTextField(Constant.IPD_MERE_GEN_DEAD_CODE, general_data);
                gen_field.AddRange(empty_dead);

                var ldtv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_LDTV_CODE["QUES"], Constant.IPD_MERE_GEN_LDTV_CODE["ANS"], general_data);
                if (ldtv_empty != null)
                    gen_field.Add(ldtv_empty);

                var tttv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_TTTV_CODE["QUES"], Constant.IPD_MERE_GEN_TTTV_CODE["ANS"], general_data);
                if (tttv_empty != null)
                    gen_field.Add(tttv_empty);
            }
            else if (Constant.UpstreamDownstreamTransfer.Contains(status_code))
            {
                var empty_upstream_downstream_transfer = GetEmptyTextField(Constant.IPD_MERE_GEN_UPDOWN_TRANSFER_CODE, general_data);
                gen_field.AddRange(empty_upstream_downstream_transfer);

                var cv_upstream_downstream_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_UD_CODE["QUES"], Constant.IPD_MERE_GEN_UD_CODE["ANS"], general_data);
                if (cv_upstream_downstream_empty != null)
                    gen_field.Add(cv_upstream_downstream_empty);

                var cv_upstream_downstream_empty2 = GetEmptyChoiceField(Constant.IPD_MERE_GEN_CV_CODE["QUES"], Constant.IPD_MERE_GEN_CV_CODE["ANS"], general_data);
                if (cv_upstream_downstream_empty2 != null)
                    gen_field.Add(cv_upstream_downstream_empty2);
            }

            //switch (status)
            //{
            //case "Transfer":
            //    var empty_transfer = GetEmptyTextField(Constant.IPD_MERE_GEN_TRANSFER_CODE, request_general);
            //    gen_field.AddRange(empty_transfer);
            //    break;
            //case "Discharged":
            //    var empty_discharged = GetEmptyTextField(Constant.IPD_MERE_GEN_DISCHARGE_CODE, general_data);
            //    gen_field.AddRange(empty_discharged);

            //    var htrv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_HTRV_CODE["QUES"], Constant.IPD_MERE_GEN_HTRV_CODE["ANS"], general_data);
            //    if (htrv_empty != null)
            //    {
            //        gen_field.Add(htrv_empty);
            //    }
            //    var is_rv_ChoiceField = general_data.FirstOrDefault(
            //            e => !string.IsNullOrEmpty(e.Code) &&
            //            e.Code == "IPDMRPTHTRVXIV" &&
            //            !string.IsNullOrEmpty(e.Value) &&
            //            e.Value.ToLower() == "true"
            //        );
            //    var is_rv_ChoiceFieldOther = general_data.FirstOrDefault(
            //            e => !string.IsNullOrEmpty(e.Code) &&
            //            e.Code == "IPDMRPGRV04" &&
            //            !string.IsNullOrEmpty(e.Value) &&
            //            e.Value.ToLower() == "true"
            //        );
            //    var is_rv_ChoiceFieldOtherText = general_data.Where(
            //            e => !string.IsNullOrEmpty(e.Code) &&
            //            e.Code == "IPDMRPGRV05" &&
            //            string.IsNullOrEmpty(e.Value)
            //        ).ToList();
            //    if (is_rv_ChoiceField != null && version == 1)
            //    {
            //        var htrv_rv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_HTRV2_CODE["QUES"], Constant.IPD_MERE_GEN_HTRV2_CODE["ANS"], general_data);
            //        if (htrv_rv_empty != null)
            //            gen_field.Add(htrv_rv_empty);
            //        if (is_rv_ChoiceFieldOther != null && is_rv_ChoiceFieldOtherText.Count > 0)
            //        {
            //            gen_field.AddRange(is_rv_ChoiceFieldOtherText);
            //        }
            //    }
            //    break;
            //case "Inter-hospital transfer":
            //    var empty_inter_transfer = GetEmptyTextField(Constant.IPD_MERE_GEN_INTER_CODE, general_data);
            //    gen_field.AddRange(empty_inter_transfer);
            //    break;
            //case "Dead":
            //    var empty_dead = GetEmptyTextField(Constant.IPD_MERE_GEN_DEAD_CODE, general_data);
            //    gen_field.AddRange(empty_dead);

            //    var ldtv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_LDTV_CODE["QUES"], Constant.IPD_MERE_GEN_LDTV_CODE["ANS"], general_data);
            //    if (ldtv_empty != null)
            //        gen_field.Add(ldtv_empty);

            //    var tttv_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_TTTV_CODE["QUES"], Constant.IPD_MERE_GEN_TTTV_CODE["ANS"], general_data);
            //    if (tttv_empty != null)
            //        gen_field.Add(tttv_empty);
            //    break;
            //case "Upstream/Downstream transfer":
            //    var empty_upstream_downstream_transfer = GetEmptyTextField(Constant.IPD_MERE_GEN_UPDOWN_TRANSFER_CODE, general_data);
            //    gen_field.AddRange(empty_upstream_downstream_transfer);

            //    var cv_upstream_downstream_empty = GetEmptyChoiceField(Constant.IPD_MERE_GEN_UD_CODE["QUES"], Constant.IPD_MERE_GEN_UD_CODE["ANS"], general_data);
            //    if (cv_upstream_downstream_empty != null)
            //        gen_field.Add(cv_upstream_downstream_empty);

            //    var cv_upstream_downstream_empty2 = GetEmptyChoiceField(Constant.IPD_MERE_GEN_CV_CODE["QUES"], Constant.IPD_MERE_GEN_CV_CODE["ANS"], general_data);
            //    if (cv_upstream_downstream_empty2 != null)
            //        gen_field.Add(cv_upstream_downstream_empty2);
            //    break;
            //default:
            //    break;
            //}
            part2_field = part2_field.OrderBy(e => e.Order).ToList();
            uncomplete_field.AddRange(part2_field);
            part3_field = part3_field.OrderBy(e => e.Order).ToList();
            uncomplete_field.AddRange(part3_field);
            gen_field = gen_field.OrderBy(e => e.Order).ToList();
            uncomplete_field.AddRange(gen_field);
            var uncomleted_fieldsInitialAssessment = GetUncompleteFieldInitialAssessment(part3_data);
            if (uncomleted_fieldsInitialAssessment.Count > 0)
            {
                uncomplete_field.AddRange(uncomleted_fieldsInitialAssessment);
            }
            if (Type == "A01_040_050919_V")
            {
                var bck_part2 = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT10024");
                if (bck_part2 == null || (string.IsNullOrEmpty(bck_part2?.Value)))
                {
                    uncomplete_field.Add(new MedicalReportDataModels()
                    {
                        Code = "IPDMRPT10024",
                        ViName = "Bệnh chuyên khoa",
                        EnName = "Bệnh chuyên khoa",
                        Value = "",
                        Order = 10024
                    });
                }
            }
            if (Type == "A01_039_050919_V")
            {
                var bck_part2 = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT10001");
                if (bck_part2 == null || (string.IsNullOrEmpty(bck_part2?.Value)))
                {
                    uncomplete_field.Add(new MedicalReportDataModels()
                    {
                        Code = "IPDMRPT10001",
                        ViName = "Bệnh chuyên khoa",
                        EnName = "Bệnh chuyên khoa",
                        Value = "",
                        Order = 10001
                    });
                }
            }
            if (mdTMH.Contains(Type))
            {
                var bck_part33 = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT10022");
                if (bck_part33 == null || (string.IsNullOrEmpty(bck_part33?.Value)))
                {
                    uncomplete_field.Add(new MedicalReportDataModels()
                    {
                        Code = "IPDMRPT10022",
                        ViName = "+ Da và mô dưới da",
                        EnName = "+ Da và mô dưới da",
                        Value = "",
                        Order = 10022
                    });
                }
            }
            if (Type == "A01_196_050919_V")
            {
                var checkValue1 = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1002");
                var checkValue2 = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1003");
                var codedt = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                              where !m.IsDeleted && m.Code == "IPDMRPE1001"
                              select new MedicalReportDataModels()
                              {
                                  Code = m.Code,
                                  ViName = m.ViName,
                                  EnName = m.EnName,
                                  Value = "",
                                  Order = m.Order
                              }).FirstOrDefault();
                if (checkValue1 != null && checkValue2 != null)
                {
                    if (!string.IsNullOrEmpty(checkValue1?.Value) && checkValue1?.Value == "False" && !string.IsNullOrEmpty(checkValue2?.Value) && checkValue2?.Value == "False")
                    {
                        uncomplete_field.Add(codedt);
                    }
                    else if (string.IsNullOrEmpty(checkValue1.Value) && string.IsNullOrEmpty(checkValue2.Value))
                    {
                        uncomplete_field.Add(codedt);
                    }
                }
                else if (checkValue1 == null && checkValue2 == null)
                {
                    uncomplete_field.Add(codedt);
                }
                var tomtatBA = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT1027");
                if (tomtatBA != null)
                {
                    if (string.IsNullOrEmpty(tomtatBA.Value))
                    {
                        var code = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                    where !m.IsDeleted && m.Code == "IPDMRPT1027"
                                    select new MedicalReportDataModels()
                                    {
                                        Code = m.Code,
                                        ViName = m.ViName,
                                        EnName = m.EnName,
                                        Value = "",
                                        Order = m.Order
                                    }).FirstOrDefault();

                        uncomplete_field.Add(code);
                    }
                    var sinhducBA = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT1001");
                    if (sinhducBA != null)
                    {
                        if (string.IsNullOrEmpty(tomtatBA.Value))
                        {
                            var code = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                        where !m.IsDeleted && m.Code == "IPDMRPT1001"
                                        select new MedicalReportDataModels()
                                        {
                                            Code = m.Code,
                                            ViName = m.ViName,
                                            EnName = m.EnName,
                                            Value = "",
                                            Order = m.Order
                                        }).FirstOrDefault();

                            uncomplete_field.Add(code);
                        }
                    }
                    var khacBA = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT831");
                    if (khacBA != null)
                    {
                        if (string.IsNullOrEmpty(tomtatBA.Value))
                        {
                            var code = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                        where !m.IsDeleted && m.Code == "IPDMRPT831"
                                        select new MedicalReportDataModels()
                                        {
                                            Code = m.Code,
                                            ViName = m.ViName,
                                            EnName = m.EnName,
                                            Value = "",
                                            Order = m.Order
                                        }).FirstOrDefault();
                            code.ViName = "+ Khác";
                            code.EnName = "+ Khác";
                            uncomplete_field.Add(code);
                        }
                    }
                    for (int i = 0; i < uncomplete_field.Count(); i++)
                    {
                        if (uncomplete_field[i].Code == "IPDMRPTTTNSANS")
                        {
                            uncomplete_field[i].ViName = "+ Tiết niệu";
                            uncomplete_field[i].EnName = "+ Tiết niệu";
                            break;
                        }
                        if (uncomplete_field[i].Code == "IPDMRPTTTBAANS")
                        {
                            uncomplete_field[i].ViName = "5. Tóm tắt bệnh án:";
                            uncomplete_field[i].EnName = "5. Tóm tắt bệnh án:";
                            break;
                        }

                    }
                }
                string[] codes = Constant.IPD_REMOVEVALIDATE_A01_196_050919_V;
                RemoveValidate(codes, ref uncomplete_field);
            }

            if (Type == "A01_038_050919_V")
            {
                string[] removeValidate = { "IPDMRPTTAMHANS", "IPDMRPTRAHMANS"
                                            ,"IPDMRPTMMATANS", "IPDMRPTNTDDANS"
                                            ,"IPDMRPETCDDANS", "IPDMRPTTAMHANS"
                                            ,"IPDMRPTVANTANS", "IPDMRPTQTBLANS"
                                            ,"IPDMRPTBATHANS", "IPDMRPTGIDIANS"
                                            ,"IPDMRPTTTYTANS", "IPDMRPTTUHOANS"
                                            ,"IPDMRPTTIHOANS", "IPDMRPTTTNSANS"
                                            ,"IPDMRPTTHKIANS", "IPDMRPTCOXKANS"
                                          };
                RemoveValidate(removeValidate, ref uncomplete_field);
            }
            if (Type == "A01_035_050919_V")
            {
                string[] codes =
                {
                    "IPDMRPTTHKIANS", "IPDMRPTCOXKANS", "IPDMRPTTAMHANS", "IPDMRPTRAHMANS",
                    "IPDMRPTMMATANS", "IPDMRPTNTDDANS", "IPDMRPTTTBAANS", "IPDMRPTQTBLANS"
                };
                RemoveValidate(codes, ref uncomplete_field);
            }
            if (Type == "A01_036_050919_V")
            {
                string[] codes = Constant.IPD_REMOVEVALIDATE_A01_036_050919_V;
                RemoveValidate(codes, ref uncomplete_field);
            }

            if (Type == "A01_040_050919_V")
            {
                var movadaBA = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT10022");
                if (movadaBA != null)
                {
                    if (string.IsNullOrEmpty(movadaBA.Value))
                    {
                        var code = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                    where !m.IsDeleted && m.Code == "IPDMRPT10022"
                                    select new MedicalReportDataModels()
                                    {
                                        Code = m.Code,
                                        ViName = m.ViName,
                                        EnName = m.EnName,
                                        Value = "",
                                        Order = m.Order
                                    }).FirstOrDefault();

                        uncomplete_field.Add(code);
                    }
                }
                string[] codes = Constant.IPD_REMOVEVALIDATE_A01_040_050919_V;
                RemoveValidate(codes, ref uncomplete_field);
            }

            if (Type == "A01_039_050919_V")
            {
                var movadaBA = part2_data.FirstOrDefault(e => e.Code == "IPDMRPT10022");
                if (movadaBA != null)
                {
                    if (string.IsNullOrEmpty(movadaBA.Value))
                    {
                        var code = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                    where !m.IsDeleted && m.Code == "IPDMRPT10022"
                                    select new MedicalReportDataModels()
                                    {
                                        Code = m.Code,
                                        ViName = m.ViName,
                                        EnName = m.EnName,
                                        Value = "",
                                        Order = m.Order
                                    }).FirstOrDefault();

                        uncomplete_field.Add(code);
                    }
                }
                string[] codes = Constant.IPD_REMOVEVALIDATE_A01_039_050919_V;
                RemoveValidate(codes, ref uncomplete_field);
            }

            if (Type == "A01_041_050919_V")
            {
                string[] codes = Constant.IPD_REMOVEVALIDATE_A01_041_050919_V;
                RemoveValidate(codes, ref uncomplete_field);
            }
            // xoa double
            int c = 0;
            for (int i = 0; i < uncomplete_field.Count; i++)
            {
                if (uncomplete_field[i].Code == "IPDMRPT10022")
                {

                    if (c >= 1)
                    {
                        uncomplete_field.Remove(uncomplete_field[i]);
                    }
                    c = c + 1;
                }
            }
            if (Type == "BMTIMMACH")
            {
                var YES = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1106")?.Value;
                if (!string.IsNullOrEmpty(YES) && bool.Parse(YES) == true)
                {
                    var OT = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1109");
                    if (OT != null && !string.IsNullOrEmpty(OT?.Value) && bool.Parse(OT?.Value.ToString()) == true)
                    {
                        var OTDesc = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1110");
                        if (!string.IsNullOrEmpty(OTDesc?.Value))
                        {
                            RemoveValidate(new string[] { "IPDMRPE1110" }, ref uncomplete_field);
                        }
                    }
                    else
                    {
                        RemoveValidate(new string[] { "IPDMRPE1110" }, ref uncomplete_field);
                    }
                }
                else
                {
                    RemoveValidate(new string[] { "IPDMRPE1110" }, ref uncomplete_field);
                }

                string[] codes = new string[] { "IPDMRPTBATHANS", "IPDMRPEPPDTANS" };
                RemoveValidate(codes, ref uncomplete_field);
            }
            else
            {
                RemoveValidate(new string[] { "IPDMRPE1110" }, ref uncomplete_field);
            }

            return uncomplete_field;
        }
        protected List<MedicalReportDataModels> GetMedicalRecordGeneralData(Guid? form_id)
        {
            return (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
                                e => !e.IsDeleted &&
                                !string.IsNullOrEmpty(e.Form) &&
                                e.Form == "IPDMRPG"
                              )
                    join data in unitOfWork.IPDMedicalRecordDataRepository.AsQueryable().Where(
                      e => !e.IsDeleted &&
                      e.IPDMedicalRecordId != null &&
                      e.IPDMedicalRecordId == form_id
                    ) on master.Code equals data.Code into dlist
                    from data in dlist.DefaultIfEmpty()
                    select new MedicalReportDataModels
                    {
                        Code = master.Code,
                        EnName = master.EnName,
                        ViName = master.ViName,
                        Value = data.Value,
                        Order = master.Order
                    }).ToList();
        }
        protected List<MedicalReportDataModels> GetMedicalRecordPart2Data(Guid? form_id, string Type = null)
        {
            List<string> mdTMH = new List<string>() { "A01_039_050919_V", "A01_040_050919_V" };
            return (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
                                e => !e.IsDeleted &&
                                !string.IsNullOrEmpty(e.Form) &&
                                e.Form == "IPDMRPT"
                                // && !mdTMH.Contains(e.Clinic) // bo qua TMH, RHM
                              )
                    join data in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(
                      e => !e.IsDeleted &&
                      e.IPDMedicalRecordPart2Id != null &&
                      e.IPDMedicalRecordPart2Id == form_id
                    ) on master.Code equals data.Code into dlist
                    from data in dlist.DefaultIfEmpty()
                    select new MedicalReportDataModels
                    {
                        Code = master.Code,
                        EnName = master.EnName,
                        ViName = master.ViName,
                        Value = data.Value,
                        Order = master.Order,
                        Clinic = master.Clinic
                    }).ToList();
        }
        protected List<MedicalReportDataModels> GetMedicalRecordPart3Data(Guid? form_id)
        {
            return (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
                                e => !e.IsDeleted &&
                                !string.IsNullOrEmpty(e.Form) &&
                                e.Form == "IPDMRPE"
                              )
                    join data in unitOfWork.IPDMedicalRecordPart3DataRepository.AsQueryable().Where(
                      e => !e.IsDeleted &&
                      e.IPDMedicalRecordPart3Id != null &&
                      e.IPDMedicalRecordPart3Id == form_id
                    ) on master.Code equals data.Code into dlist
                    from data in dlist.DefaultIfEmpty()
                    select new MedicalReportDataModels
                    {
                        Code = master.Code,
                        EnName = master.EnName,
                        ViName = master.ViName,
                        Value = data.Value,
                        Order = master.Order
                    }).ToList();
        }
        protected List<MedicalReportDataModels> GetEmptyTextField(string[] code, List<MedicalReportDataModels> datas)
        {
            return datas.Where(
                e => !string.IsNullOrEmpty(e.Code) &&
                code.Contains(e.Code) &&
                string.IsNullOrEmpty(e.Value)
            ).ToList();
        }
        protected List<MedicalReportDataModels> GetEmptyTextField(string[] request_code, JToken request_data)
        {
            var masters = unitOfWork.MasterDataRepository.Find(
                e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && request_code.Contains(e.Code)
            ).Select(e => new MedicalReportDataModels
            {
                Code = e.Code,
                ViName = e.ViName,
                EnName = e.EnName,
                Order = e.Order,
            }).ToList();

            List<MedicalReportDataModels> gen_field = new List<MedicalReportDataModels>();
            foreach (var item in request_data)
            {
                var code = item["Code"]?.ToString();
                if (code == null || !request_code.Contains(code))
                    continue;

                var value = item["Value"]?.ToString();
                if (!string.IsNullOrEmpty(value))
                    continue;

                var data_master = masters.FirstOrDefault(e => e.Code == code);
                if (data_master != null)
                    gen_field.Add(data_master);
            }
            return gen_field;
        }
        protected MedicalReportDataModels GetEmptyChoiceField(string group_code, string[] code, List<MedicalReportDataModels> datas)
        {
            var is_true = datas.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                code.Contains(e.Code) &&
                !string.IsNullOrEmpty(e.Value) &&
                e.Value.ToLower() == "true"
            );
            if (is_true != null)
                return null;

            return datas.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                group_code == e.Code
            );
        }

        protected dynamic HandleWhenStatusChange(IPD ipd, JToken request_status, string current_status_code)
        {
            if (Constant.Transfer.Contains(current_status_code))
            {
                var visit_transfer = new VisitTransfer(unitOfWork);
                if (visit_transfer.IsExist(ipd.HandOverCheckListId, ipd.Id))
                    return visit_transfer.BuildMessage();
            }

            var request_status_code = request_status["Code"].ToString();
            if (Constant.InHospital.Contains(request_status_code) || Constant.Transfer.Contains(request_status_code))
            {
                Guid visit_id = ipd.Id;
                Guid customer_id = (Guid)ipd.CustomerId;
                string customer_PID = ipd.Customer.PID;

                InHospital in_hospital = new InHospital();
                in_hospital.SetState(customer_id, visit_id, null, null);
                var in_hospital_visit = in_hospital.GetVisit();
                if (in_hospital_visit != null)
                    return new
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = in_hospital.BuildErrorMessage(in_hospital_visit)
                    };

                dynamic in_waiting_visit;
                if (!string.IsNullOrEmpty(ipd.Customer.PID))
                    in_waiting_visit = GetInWaitingAcceptPatientByPID(pid: customer_PID, visit_id: visit_id);
                else
                    in_waiting_visit = GetInWaitingAcceptPatientById(customer_id: customer_id, visit_id: visit_id);
                if (in_waiting_visit != null)
                {
                    var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                    return new
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician)
                    };
                }
            }

            if (!Constant.Transfer.Contains(request_status_code))
                RemoveTransferIfExist(ipd);

            var error = IPDValiDatePIDAndVisitCode(ipd, request_status_code);
            if (error != null)
                return error;

            UpdateStatus(ipd, request_status);
            return null;
        }
        protected void RemoveTransferIfExist(IPD ipd)
        {
            var hocl = ipd.HandOverCheckList;
            if (hocl != null)
            {
                unitOfWork.IPDHandOverCheckListRepository.Delete(hocl);
                ipd.HandOverCheckListId = null;
                unitOfWork.IPDRepository.Update(ipd);
                unitOfWork.Commit();
            }
        }
        protected void UpdateStatus(IPD ipd, JToken status)
        {
            var status_raw = status["Id"].ToString();
            Guid status_id = new Guid(status_raw);
            if (status_id != ipd.EDStatusId)
            {
                ipd.EDStatusId = status_id;
                ipd.DischargeDate = DateTime.Now;
                var customer = ipd.Customer;
                customer.EDStatusId = status_id;
                unitOfWork.IPDRepository.Update(ipd);
                unitOfWork.CustomerRepository.Update(customer);
            }
            unitOfWork.Commit();
        }
        protected DateTime? CreateOrUpdateTransferForAnotherDepartment(IPD ipd, Guid? receiving_id, string reason, bool isUseHandOverCheckList)
        {
            var specialty = ipd.Specialty;
            var receiving_unit = unitOfWork.SpecialtyRepository.GetById((Guid)receiving_id);
            if (ipd.HandOverCheckListId == null)
            {
                CreateHandOverCheckList(ipd, specialty, receiving_unit, reason, isUseHandOverCheckList);
                unitOfWork.Commit();
                return DateTime.Now;
            }
            var hocl = ipd.HandOverCheckList;
            if (hocl?.ReceivingPhysicianId == null && hocl?.ReceivingNurseId == null)
            {
                UpdateHandOverCheckList(hocl, specialty, receiving_unit, reason, isUseHandOverCheckList);
            }
            unitOfWork.Commit();
            return hocl.HandOverTimePhysician;
        }
        protected void CreateHandOverCheckList(IPD ipd, Specialty specialty, Specialty receiving, string reason, bool isUseHandOverCheckList)
        {
            IPDHandOverCheckList hocl = new IPDHandOverCheckList();
            hocl.HandOverTimePhysician = DateTime.Now;
            hocl.HandOverUnitNurseId = specialty.Id;
            hocl.HandOverUnitPhysicianId = specialty.Id;
            hocl.ReceivingUnitNurseId = receiving.Id;
            hocl.ReceivingUnitPhysicianId = receiving.Id;
            hocl.HandOverPhysicianId = GetUser()?.Id;
            hocl.ReasonForTransfer = reason;
            hocl.IsUseHandOverCheckList = isUseHandOverCheckList;

            if (!receiving.IsPublish)
            {
                hocl.IsAcceptNurse = true;
                hocl.IsAcceptPhysician = true;
            }
            unitOfWork.IPDHandOverCheckListRepository.Add(hocl);
            unitOfWork.Commit();
            ipd.HandOverCheckListId = hocl.Id;

            dynamic allery_data = GetAllergy(ipd.IPDInitialAssessmentForAdultId);
            dynamic vital_sign = GetVitalSign(
                ipd.IPDInitialAssessmentForAdultId,
                new string[] {
                    "IPDIAAUPULSANS", "IPDIAAUBLPRANS", "IPDIAAUTEMPANS", "IPDIAAURERAANS",
                }
            );

            foreach (var data in allery_data)
                UpdateAllergy(data.Code, data.Value, hocl);
            UpdateVitalSign(vital_sign, hocl.Id);
        }
        protected void UpdateHandOverCheckList(IPDHandOverCheckList hocl, Specialty specialty, Specialty receiving, string reason, bool isUseHandOverCheckList)
        {
            hocl.HandOverUnitNurseId = specialty.Id;
            hocl.HandOverUnitPhysicianId = specialty.Id;
            hocl.ReceivingUnitNurseId = receiving.Id;
            hocl.ReceivingUnitPhysicianId = receiving.Id;
            hocl.ReasonForTransfer = reason;

            if (!receiving.IsPublish)
            {
                hocl.IsAcceptNurse = true;
                hocl.IsAcceptPhysician = true;
            }
            else
            {
                if (hocl.ReceivingNurseId == null && hocl.ReceivingPhysicianId == null)
                {
                    hocl.IsAcceptNurse = false;
                    hocl.IsAcceptPhysician = false;
                }
            }
            hocl.IsUseHandOverCheckList = isUseHandOverCheckList;
            unitOfWork.IPDHandOverCheckListRepository.Update(hocl);
        }


        protected dynamic GetVitalSign(Guid? form_id, string[] vital_sign_code)
        {
            var vital_sign = (from data in unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                             .Where(
                                i => !i.IsDeleted &&
                                i.IPDInitialAssessmentForAdultId != null &&
                                i.IPDInitialAssessmentForAdultId == form_id &&
                                !string.IsNullOrEmpty(i.Code) &&
                                vital_sign_code.Contains(i.Code)
                             )
                              join master in unitOfWork.MasterDataRepository.AsQueryable() on data.Code equals master.Code into ulist
                              from master in ulist.DefaultIfEmpty()
                              select new { master.Code, master.EnName, master.ViName, data.Value, master.Note }).ToList();
            return vital_sign;
        }
        protected void UpdateVitalSign(dynamic data, Guid hoc_id)
        {
            string vital_sign = JoinVitalSign(data);
            var val = "True";
            if (string.IsNullOrEmpty(vital_sign)) val = "False";

            IPDHandOverCheckListData vs_yes = new IPDHandOverCheckListData
            {
                HandOverCheckListId = hoc_id,
                Code = "IPDHOCVS0YES",
                Value = val
            };
            unitOfWork.IPDHandOverCheckListDataRepository.Add(vs_yes);
            IPDHandOverCheckListData vs_no = new IPDHandOverCheckListData
            {
                HandOverCheckListId = hoc_id,
                Code = "IPDHOCVS0NOO",
                Value = "False"
            };
            unitOfWork.IPDHandOverCheckListDataRepository.Add(vs_no);
            IPDHandOverCheckListData vs_ans = new IPDHandOverCheckListData
            {
                HandOverCheckListId = hoc_id,
                Code = "IPDHOCVS0ANS",
                Value = vital_sign,
            };
            unitOfWork.IPDHandOverCheckListDataRepository.Add(vs_ans);
        }
        protected string JoinVitalSign(dynamic data)
        {
            string result = string.Empty;
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Value))
                    result += string.Format("{0}: {1} {2}, ", item.ViName, item.Value, item.Note);
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.TrimEnd();
                result = result.Remove(result.Length - 1);
            }
            return result;
        }
        protected dynamic GetAllergy(Guid? form_id)
        {
            return unitOfWork.IPDInitialAssessmentForAdultDataRepository.Find(
                e => !e.IsDeleted &&
                e.IPDInitialAssessmentForAdultId != null &&
                e.IPDInitialAssessmentForAdultId == form_id &&
                !string.IsNullOrEmpty(e.Code) &&
                e.Code.Contains("IPDIAAUALLE")
            ).Select(e => new { e.Code, e.Value }).ToList();
        }
        protected void UpdateAllergy(string code, string value, IPDHandOverCheckList hoc)
        {
            IPDHandOverCheckListData new_all = new IPDHandOverCheckListData
            {
                HandOverCheckListId = hoc.Id,
                Code = Constant.IPD_HOC_ALLERGIC_CODE_SWITCH[code],
                Value = value
            };
            unitOfWork.IPDHandOverCheckListDataRepository.Add(new_all);
        }


        protected dynamic GetListStatus(bool isDraft)
        {
            if (isDraft)
            {
                return unitOfWork.EDStatusRepository.Find(
                    e => !e.IsDeleted &&
                    e.VisitTypeGroupId != null &&
                    e.VisitTypeGroup.Code == "IPD"
                ).OrderBy(e => e.CreatedAt).Select(st => new { st.Id, st.ViName, st.EnName, st.Code });
            }
            return unitOfWork.EDStatusRepository.Find(
                    e => !e.IsDeleted &&
                    e.VisitTypeGroupId != null &&
                    e.VisitTypeGroup.Code == "IPD" &&
                    e.Code != "IPDNOEX"
                ).OrderBy(e => e.CreatedAt).Select(st => new { st.Id, st.ViName, st.EnName, st.Code });
        }

        protected List<MedicalReportDataModels> GetUncompleteFieldInitialAssessment(List<MedicalReportDataModels> datas)
        {
            //  List<MedicalReportDataModels> datas = request_data.ToObject<List<MedicalReportDataModels>>();
            List<MedicalReportDataModels> uncomplete_field = new List<MedicalReportDataModels>();
            string[] code = Constant.ED_DI0_XNTT_IPD_CODE["ANS"];
            var is_true = datas.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
               code.Contains(e.Code) &&
                !string.IsNullOrEmpty(e.Value) &&
                e.Value.ToLower() == "true"
            );
            if (is_true == null)
            {
                uncomplete_field.Add(new MedicalReportDataModels
                {
                    Code = "IPDMRPECOIN",
                    ViName = "Xác nhận thương tích (áp dụng Giấy chứng nhận thương tích)",
                    EnName = "Confirm injury (For Injury Certificate Form)"
                });
            }
            return uncomplete_field;
        }

        protected dynamic AutoFillDataFromMasterData(string[] code, Guid visit_Id)
        {
            var ipd = GetIPD(visit_Id);
            var form = unitOfWork.IPDInitialAssessmentForNewbornsRepository.FirstOrDefault(
                                                                                            f => !f.IsDeleted
                                                                                            && f.VisitId == visit_Id
                                                                                            && f.DataType == "ForNeonatalMaternity"
                                                                                            );

            if (form == null)
            {
                var datas_medicalRecord = (from mas in unitOfWork.MasterDataRepository.AsQueryable()
                                           where !mas.IsDeleted && !string.IsNullOrEmpty(mas.Code)
                                           && code.Contains(mas.Code)

                                           join data in unitOfWork.IPDMedicalRecordDataRepository.AsQueryable()
                                           .Where(data => !data.IsDeleted && !string.IsNullOrEmpty(data.Code)
                                                   && code.Contains(data.Code)
                                                   && data.IPDMedicalRecordId == ipd.IPDMedicalRecordId
                                           )
                                           on mas.Code equals data.Code into data_query
                                           from choices in data_query.DefaultIfEmpty()
                                           select new
                                           {
                                               mas.ViName,
                                               mas.EnName,
                                               mas.Code,
                                               choices.Value,
                                               Order = mas.Order
                                           }).OrderBy(o => o.Order).ToList();

                return datas_medicalRecord;
            }

            var dataInInitialAssessment = (from mas in unitOfWork.MasterDataRepository.AsQueryable()
                                           where !mas.IsDeleted && !string.IsNullOrEmpty(mas.Code)
                                           && code.Contains(mas.Code)

                                           join data in unitOfWork.FormDatasRepository.AsQueryable()
                                           .Where(data => !data.IsDeleted && !string.IsNullOrEmpty(data.Code)
                                           && code.Contains(data.Code) && data.VisitId == visit_Id
                                           && data.FormId == form.Id
                                           )
                                           on mas.Code equals data.Code into data_query
                                           from choices in data_query.DefaultIfEmpty()
                                           select new
                                           {
                                               mas.ViName,
                                               mas.EnName,
                                               mas.Code,
                                               choices.Value,
                                               Order = mas.Order
                                           }).OrderBy(o => o.Order).ToList();

            List<IPDMedicalRecordData> medical_datas = new List<IPDMedicalRecordData>();
            foreach (var item in dataInInitialAssessment)
            {
                medical_datas.Add(new IPDMedicalRecordData()
                {
                    Id = new Guid(),
                    Code = item.Code,
                    Value = item.Value,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = GetUser().Username,
                    UpdatedBy = GetUser().Username,
                    IsDeleted = false
                });
            }

            IPD lastIpd;
            bool success = SaveDatas(ipd, medical_datas, out lastIpd);

            return dataInInitialAssessment;
        }

        private bool SaveDatas(IPD ipd, List<IPDMedicalRecordData> datas, out IPD lastIpd)
        {
            lastIpd = null;
            var hanoverId = ipd.HandOverCheckListId;
            if (hanoverId == null)
                return false;

            lastIpd = unitOfWork.IPDRepository.FirstOrDefault(i => !i.IsDeleted && i.TransferFromId != null && i.TransferFromId == hanoverId);
            if (lastIpd == null)
                return false;

            Guid? medicalrecordId = lastIpd.IPDMedicalRecordId;
            if (medicalrecordId == null)
                return false;

            int lenght_datas = datas.Count;
            for (int i = 0; i < lenght_datas; i++)
            {
                string code = datas[i].Code;
                var checkCode = unitOfWork.IPDMedicalRecordDataRepository
                                .FirstOrDefault(
                                    e => !e.IsDeleted
                                          && e.IPDMedicalRecordId == medicalrecordId
                                          && e.Code == code
                                );
                if (checkCode == null)
                {
                    datas[i].IPDMedicalRecordId = lastIpd.IPDMedicalRecordId;
                    unitOfWork.IPDMedicalRecordDataRepository.Add(datas[i]);
                }
                else
                {
                    unitOfWork.IPDMedicalRecordDataRepository.Update(datas[i]);
                }

            }
            unitOfWork.Commit();

            return true;
        }

        private void RemoveValidate(string[] codes, ref List<MedicalReportDataModels> uncompletes)
        {
            foreach (var remove_code in codes)
            {
                var check_code = uncompletes.FirstOrDefault(u => u.Code == remove_code);
                if (check_code != null)
                    uncompletes.Remove(check_code);
            }
        }
        private List<FormDataValue> GetFormDatas(Guid visitId, Guid formId, string formCode, bool isNew)
        {
            var formDatas = unitOfWork.FormDatasRepository.Find(e =>
                    !e.IsDeleted &&
                    e.VisitId == visitId &&
                    e.FormCode == formCode &&
                    e.FormId == formId
            );
            if (isNew)
            {
                return formDatas.Select(f => new FormDataValue { Id = null, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();
            }
            return formDatas.Select(f => new FormDataValue { Id = f.Id, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();
        }
        private MasterdataForm FormatOutput(EIOForm fprm, string formCode, bool isNew)
        {
            return new MasterdataForm
            {
                Id = isNew == true ? null : fprm?.Id,
                Datas = GetFormDatas((Guid)fprm.VisitId, fprm.Id, formCode, isNew),
                IsDeleted = false
            };
        }

        protected bool CheckIsNewByTab(Guid id_part, string formCode)
        {
            var isPart = unitOfWork.IPDMedicalRecordOfPatientRepository
                         .FirstOrDefault(
                            p => !p.IsDeleted &&
                            p.FormId == id_part &&
                            p.FormCode == formCode
                        );
            if (isPart != null)
            {
                if (isPart.UpdatedAt == null)
                    return IsNew(isPart.CreatedAt, isPart.CreatedAt);

                string str_createAt = isPart.CreatedAt?.ToString("DD/MM/YY HH:mm:ss");
                DateTime createAt = DateTime.ParseExact(str_createAt, "DD/MM/YY HH:mm:ss", CultureInfo.InvariantCulture);
                string str_updateAt = isPart.UpdatedAt?.ToString("DD/MM/YY HH:mm:ss");
                DateTime updateAt = DateTime.ParseExact(str_updateAt, "DD/MM/YY HH:mm:ss", CultureInfo.InvariantCulture);
                return IsNew(createAt, updateAt);
            }
            return true;
        }

        protected string GetSignature(string code, string formCode, Guid? formId)
        {
            var data = (from d in unitOfWork.FormDatasRepository.AsQueryable()
                        where !d.IsDeleted && d.Code == code && d.FormCode == formCode
                        && d.FormId == formId
                        select d.Value).FirstOrDefault();
            if (data == null)
                return "";

            return data;
        }

        protected bool CreateSignature(string reqStatusCode, IPD visit)
        {
            if (!Constant.WaitingResults.Contains(reqStatusCode) && !Constant.InHospital.Contains(reqStatusCode))
            {
                EDStatus currenStatus = visit.EDStatus;
                if (currenStatus == null)
                    return false;

                var part2_Id = visit.IPDMedicalRecord?.IPDMedicalRecordPart2Id;
                var part3_Id = visit.IPDMedicalRecord?.IPDMedicalRecordPart3Id;

                if (reqStatusCode != currenStatus.Code)
                {
                    var medicalRecordOfpatien = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                                 where !m.IsDeleted && m.Group == "MedicalRecords" && m.Note == "IPD"
                                                 join f in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                                 .Where(f => !f.IsDeleted && f.VisitId == visit.Id)
                                                 on m.Form equals f.FormCode
                                                 select f).ToList();

                    string[] codes = new string[]
                    {
                        "PART2LASTUPDATEDBY", "PART2LASTUPDATEDAT",
                        "PART3LASTUPDATEDBY", "PART3LASTUPDATEDAT",
                    };

                    var getSignatures = (from m in unitOfWork.FormDatasRepository.AsQueryable()
                                         where !m.IsDeleted && m.FormId == part2_Id && codes.Contains(m.Code)
                                         || m.FormId == part3_Id && codes.Contains(m.Code)
                                         select m).ToList();

                    var lastUpdate_part2 = (from e in medicalRecordOfpatien
                                            where e.FormId == part2_Id
                                            select e).ToList();

                    var lastUpdate_part3 = (from e in medicalRecordOfpatien
                                            where e.FormId == part3_Id
                                            select e).ToList();

                    foreach (var item in lastUpdate_part2)
                    {
                        var checkUpdateBy = getSignatures.FirstOrDefault(e => e.FormId == item.FormId && e.FormCode == item.FormCode && e.Code == "PART2LASTUPDATEDBY");
                        var checkUpdateAt = getSignatures.FirstOrDefault(e => e.FormId == item.FormId && e.FormCode == item.FormCode && e.Code == "PART2LASTUPDATEDAT");
                        if (checkUpdateBy == null)
                        {
                            var dataLastUpdatedBy = new FormDatas()
                            {
                                VisitId = item.VisitId,
                                FormId = item.FormId,
                                FormCode = item.FormCode,
                                Code = "PART2LASTUPDATEDBY",
                                Value = item.UpdatedBy
                            };
                            unitOfWork.FormDatasRepository.Add(dataLastUpdatedBy);
                        }
                        else
                        {
                            checkUpdateBy.Value = item.UpdatedBy;
                            unitOfWork.FormDatasRepository.Update(checkUpdateBy);
                        }

                        if (checkUpdateAt == null)
                        {
                            var dataLastUpdateAt = new FormDatas()
                            {
                                VisitId = item.VisitId,
                                FormId = item.FormId,
                                FormCode = item.FormCode,
                                Code = "PART2LASTUPDATEDAT",
                                Value = item.UpdatedAt?.ToString("HH:mm dd/MM/yyyy")
                            };
                            unitOfWork.FormDatasRepository.Add(dataLastUpdateAt);
                        }
                        else
                        {
                            checkUpdateAt.Value = item.UpdatedAt?.ToString("HH:mm dd/MM/yyyy");
                            unitOfWork.FormDatasRepository.Update(checkUpdateAt);
                        }
                    }

                    foreach (var item in lastUpdate_part3)
                    {
                        var checkUpdateBy = getSignatures.FirstOrDefault(e => e.FormId == item.FormId && e.FormCode == item.FormCode && e.Code == "PART3LASTUPDATEDBY");
                        var checkUpdateAt = getSignatures.FirstOrDefault(e => e.FormId == item.FormId && e.FormCode == item.FormCode && e.Code == "PART3LASTUPDATEDAT");
                        if (checkUpdateBy == null)
                        {
                            var dataLastUpdatedBy = new FormDatas()
                            {
                                VisitId = item.VisitId,
                                FormId = item.FormId,
                                FormCode = item.FormCode,
                                Code = "PART3LASTUPDATEDBY",
                                Value = item.UpdatedBy
                            };
                            unitOfWork.FormDatasRepository.Add(dataLastUpdatedBy);
                        }
                        else
                        {
                            checkUpdateBy.Value = item.UpdatedBy;
                            unitOfWork.FormDatasRepository.Update(checkUpdateBy);
                        }

                        if (checkUpdateAt == null)
                        {
                            var dataLastUpdateAt = new FormDatas()
                            {
                                VisitId = item.VisitId,
                                FormId = item.FormId,
                                FormCode = item.FormCode,
                                Code = "PART3LASTUPDATEDAT",
                                Value = item.UpdatedAt?.ToString("HH:mm dd/MM/yyyy")
                            };
                            unitOfWork.FormDatasRepository.Add(dataLastUpdateAt);
                        }
                        else
                        {
                            checkUpdateAt.Value = item.UpdatedAt?.ToString("HH:mm dd/MM/yyyy");
                            unitOfWork.FormDatasRepository.Update(checkUpdateAt);
                        }
                    }
                }
                return true;
            }
            return false;
        }
        protected bool IsCheckTransfer(IPD ipd)
        {
            var value = unitOfWork.IPDMedicalRecordDataRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTNONHANS" && e.IPDMedicalRecordId == ipd.IPDMedicalRecordId);
            if (value == null)
                return false;
            bool isGuid = Guid.TryParse(value.Value, out Guid valueguid);
            if (!isGuid)
                return false;
            var checktransfer = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.SpecialtyId == valueguid && e.CustomerId == ipd.CustomerId && e.TransferFromId == ipd.HandOverCheckListId && e.IsTransfer == true);
            if (checktransfer != null) return true;

            return false;
        }

        private IPDMedicalRecordExtenstion GetForm(Guid visitId, string name)
        {
            return unitOfWork.IPDMedicalRecordExtenstionReponsitory.Find(e => !e.IsDeleted && e.VisitId == visitId && e.Name == name).FirstOrDefault();
        }
        public int getVersionOfMedicalrecord(string formCode, Guid visitId, int setCurrentVersion = 2)
        {
            var fistMedicalRecordOfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable().OrderByDescending(x => x.Version)?.FirstOrDefault(x => !x.IsDeleted && x.FormCode == formCode && x.VisitId == visitId);
            int currentVersion = fistMedicalRecordOfPatient == null ? setCurrentVersion : fistMedicalRecordOfPatient.Version;
            return currentVersion;
        }
        class DataNewbornViewModel
        {
            public Guid? VisitId { get; set; }
            public Guid FormId { get; set; }
            public DateTime? CreatedAt { get; set; }
            public Guid? EIOConstraintNewbornAndPregnantWomanId { get; set; }
            public IEnumerable<FormDatas> Datas { get; set; }
        }
    }
}