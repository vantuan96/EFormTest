using DataAccess.Models;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models.IPDModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDMedicalRecordPart2Controller : IPDMedicalRecordController
    {
        #region Part 2
        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part2/Create/{Type}/{id}")]
        [Permission(Code = "IMRPT1")]
        public IHttpActionResult CreateIPDMedicalRecordPart2API(string Type,Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record == null)
            {
                medical_record = new IPDMedicalRecord();
                medical_record.Version = 2;
                unitOfWork.IPDMedicalRecordRepository.Add(medical_record);
                ipd.IPDMedicalRecordId = medical_record.Id;
                unitOfWork.IPDRepository.Update(ipd);
            }

            int currentVersion = getVersionOfMedicalrecord(formCode: Type, visitId: id, setCurrentVersion: 2);

            var medicalRecordPart2Id = medical_record.IPDMedicalRecordPart2Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart2Id);
            if (medical_record.IPDMedicalRecordPart2Id != null && medicalRecordOfPatient != null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPT_EXIST);

            var recordOfPatient = new IPDMedicalRecordOfPatients();
            if (medicalRecordOfPatient == null || medicalRecordOfPatient.IsDeleted)
            {
             
                recordOfPatient.FormCode = Type;
                recordOfPatient.VisitId = id;
                recordOfPatient.CreatedBy = GetUser().Username;
                recordOfPatient.CreatedAt = DateTime.Now;
                recordOfPatient.UpdatedAt = recordOfPatient.CreatedAt;
                recordOfPatient.Id = new Guid();
                recordOfPatient.IsDeleted = false;
                recordOfPatient.Version = currentVersion;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Add(recordOfPatient);
            }


            if (medical_record.IPDMedicalRecordPart2Id == null)
            {
                var part_2 = new IPDMedicalRecordPart2();
                unitOfWork.IPDMedicalRecordPart2Repository.Add(part_2);
                medical_record.IPDMedicalRecordPart2Id = part_2.Id;
                unitOfWork.IPDMedicalRecordRepository.Update(medical_record);
                recordOfPatient.FormId = part_2.Id;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(recordOfPatient);
            }
            else
            {
                recordOfPatient.FormId = medical_record.IPDMedicalRecordPart2Id;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(recordOfPatient);
            }

            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/MedicalRecord/Part2/{Type}/{id}")]
        [Permission(Code = "IMRPT2")]
        public IHttpActionResult GetIPDMedicalRecordPart2API(string Type,Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);


            //var obj = GetVisitHandOverCheckList(id);
            //var setupMedicalRecord = GetSetupMedicalRecord(Type, ipd.SpecialtyId);
            //if (setupMedicalRecord == null || setupMedicalRecord.IsDeploy == false)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD);

            if (ipd.IPDMedicalRecordId == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);
            }


            var medical_record = ipd.IPDMedicalRecord;
            var medicalRecordPart2Id = medical_record.IPDMedicalRecordPart2Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart2Id);
            if (medical_record.IPDMedicalRecordPart2Id == null || medicalRecordOfPatient == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPT_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildMedicalRecordPart2Result(Type, ipd, medical_record));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part2/{Type}/{id}")]
        [Permission(Code = "IMRPT3")]
        public IHttpActionResult UpdateIPDMedicalRecordPart2API(string Type,Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var setupMedicalRecord = GetSetupMedicalRecord(Type, ipd.SpecialtyId);
            //if (setupMedicalRecord == null || setupMedicalRecord.IsDeploy == false)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD);

            if (IPDIsBlock(ipd, Type.ToUpper()))
                return Content(HttpStatusCode.NotFound, Message.IPD_TIME_FORBIDDEN);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            var medicalRecordPart2Id = medical_record.IPDMedicalRecordPart2Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart2Id);
            if (medical_record.IPDMedicalRecordPart2Id == null || medicalRecordOfPatient == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPT_NOT_FOUND);

            medicalRecordOfPatient.UpdatedAt = DateTime.Now;
            medicalRecordOfPatient.UpdatedBy = GetUser().Username;

            var part_2 = medical_record.IPDMedicalRecordPart2;
            HandleIPDMedicalRecordPart2Datas(medical_record, part_2, request["Datas"]);
            HandleIPDMedicalRecordDatas(medical_record, request["GeneralDatas"]);

            string dataWeight = request["Cannang"].ToString();
            SaveWeightFromInitialAssessment(ipd, "IPDIAAUWEIGANS", dataWeight);


            var status_code = request["Status"]["Code"].ToString();

            var handOverCheckList = ipd.HandOverCheckList;
            bool isUseHandOverCheckList = true;
            if (handOverCheckList != null)
            {
                isUseHandOverCheckList = handOverCheckList.IsUseHandOverCheckList;
            }
            if (!Constant.InHospital.Contains(status_code) && !Constant.WaitingResults.Contains(status_code))
            {
                var uncomleted_fields = GetUncompleteField(
                    medical_record.Id, 
                    medical_record.IPDMedicalRecordPart2Id, 
                    medical_record.IPDMedicalRecordPart3Id,
                    request["GeneralDatas"],
                    status_code,
                    medical_record.Version,
                    Type
                );

                if (isUseHandOverCheckList == false)
                {
                    var item = uncomleted_fields.FirstOrDefault(e => e.Code == "IPDMRPTLDCKANS");
                    if (item != null)
                    {
                        uncomleted_fields.Remove(item);
                    }

                }

                if (uncomleted_fields.Count > 0)
                    return Content(HttpStatusCode.OK, new { Error = uncomleted_fields });
            }

            
            
            var is_error = HandleUpdateStatus(ipd, request["Status"], request["GeneralDatas"], isUseHandOverCheckList);

            if (is_error != null)
                return Content(is_error.Code, is_error.Message);

            var chronic_util = new ChronicUtil(unitOfWork, ipd.Customer);
            chronic_util.UpdateChronic();
            return Content(HttpStatusCode.OK, new { ipd.Customer.IsChronic });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/MedicalRecord/Part2/SyncVisitHistory/{Type}")]
        [Permission(Code = "IMRPT4")]
        public IHttpActionResult SyncVisitHistoryAPI(string Type, [FromBody]JObject request)
        {
            if (string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }
            var id = new Guid(request["Id"]?.ToString());
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var setupMedicalRecord = GetSetupMedicalRecord(Type, ipd.SpecialtyId);
            //if (setupMedicalRecord == null || setupMedicalRecord.IsDeploy == false)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD);

            //var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, ipd.IPDMedicalRecord.Id);
            //if (medicalRecordOfPatient == null)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD_OF_PATIENT);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            List<VisitHistoryModel> familymedicalHistory = new List<VisitHistoryModel>();
            var list_visit_opd = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.CustomerId == customer.Id && e.Id != id && e.CreatedAt < ipd.CreatedAt).ToList();
            if (list_visit_opd != null && list_visit_opd.Count > 0)
            {
                foreach (var item in list_visit_opd)
                {
                    var data = OPDFamilyPastMedicalHistory(item);
                    if ((data != null && String.IsNullOrEmpty(data.PastMedicalHistory)) || data == null) continue;
                    familymedicalHistory.Add(data);
                }
            }
            var list_visit_ipd = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.CustomerId == customer.Id && e.CreatedAt < ipd.CreatedAt && e.Id != ipd.Id).ToList();
            if (list_visit_ipd != null && list_visit_ipd.Count > 0)
            {
                foreach (var item in list_visit_ipd)
                {
                    var data = IPDFamilyPastMedicalHistory(item);
                    if ((data != null && String.IsNullOrEmpty(data.PastMedicalHistory)) || data == null) continue;
                    familymedicalHistory.Add(data);
                }
            }
            familymedicalHistory = familymedicalHistory.OrderByDescending(e => e.UpdateAt).ToList();
            VisitHistory visit_history = VisitHistoryFactory.GetVisit("IPD", ipd, site_code);

            var visit_history_list = visit_history.GetHistory();
            return Content(HttpStatusCode.OK, new
            {
                HistoryOfPresentIllness = visit_history_list.Where(e => !string.IsNullOrEmpty(e.HistoryOfPresentIllness)).Select(e => new {
                    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    e.ViName,
                    e.EnName,
                    e.Fullname,
                    e.Username,
                    e.HistoryOfPresentIllness,
                    e.Type
                }),
                PastMedicalHistory = visit_history_list.Where(e => !string.IsNullOrEmpty(e.PastMedicalHistory)).Select(e => formatPastMedicalHistory(e)),
                //FamilyMedicalHistory = visit_history_list.Where(e => !string.IsNullOrEmpty(e.FamilyMedicalHistory)).Select(e => new {
                //    ExaminationTime = e.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                //    Clinic = new { e.ViName, e.EnName },
                //    PrimaryDoctor = e.Username,
                //    e.Fullname,
                //    e.Username,
                //    Value = e.FamilyMedicalHistory,
                //    e.Type
                //}),
                FamilyMedicalHistory = familymedicalHistory
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part2/Sync/{Type}/{id}")]
        [Permission(Code = "IMRPT5")]
        public IHttpActionResult SyncIPDMedicalRecordPart2API(string Type, Guid id)
        {
            var current_ipd = GetIPD(id);
            if (current_ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (Type == "A01_196_050919_V")
            {
                string IdOldOncologyMedicalRecord = null;
                var results = GetAllInfoCustomerInAreIPD(current_ipd);
                if (results.Count > 1)
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].Id == current_ipd.Id)
                            continue;
                        Guid oldIpdIdtmp = (Guid)results[i].Id;
                        var ipdOld = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == oldIpdIdtmp);
                        if (ipdOld != null)
                        {
                            var MedicalRecordOfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(x => x.VisitId == oldIpdIdtmp && x.FormCode == "A01_196_050919_V");
                            if (MedicalRecordOfPatient != null)
                            {
                                IdOldOncologyMedicalRecord = oldIpdIdtmp.ToString();
                                break;
                            }
                        }
                    };
                }
                if (string.IsNullOrEmpty(IdOldOncologyMedicalRecord))
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                var opd_ipd = GetIPD(Guid.Parse(IdOldOncologyMedicalRecord));
                if (opd_ipd == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                if (opd_ipd.IPDMedicalRecordId == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                var medical_record = opd_ipd.IPDMedicalRecord;
                if (medical_record.IPDMedicalRecordPart2Id == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                return Content(HttpStatusCode.OK, BuildMedicalRecordPart2Result(Type, opd_ipd, medical_record,current_ipd));
            }
            else
            {
                if (current_ipd.TransferFromId == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.HandOverCheckListId != null &&
                    e.HandOverCheckListId == current_ipd.TransferFromId
                );
                if (ipd == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                if (ipd.IPDMedicalRecordId == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                var medical_record = ipd.IPDMedicalRecord;
                if (medical_record.IPDMedicalRecordPart2Id == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                return Content(HttpStatusCode.OK, BuildMedicalRecordPart2Result(Type, ipd, medical_record, current_ipd));
            }
        }

        private void SaveWeightFromInitialAssessment(IPD ipd, string codeMasterData, string value)
        {
            var part2Id = ipd.IPDMedicalRecord.IPDMedicalRecordPart2Id;
            var checkPart2Data = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(
                                c => !c.IsDeleted && c.IPDMedicalRecordPart2Id == part2Id
                                && c.Code == codeMasterData
                                );
            if (checkPart2Data == null)
            {
                var data = new IPDMedicalRecordPart2Data()
                {
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = GetUser().Username,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = GetUser().Username,
                    Code = codeMasterData,
                    Value = value,
                    IPDMedicalRecordPart2Id = part2Id,
                    Id = new Guid()
                };
                unitOfWork.IPDMedicalRecordPart2DataRepository.Add(data);
            }
            else
            {
                checkPart2Data.UpdatedBy = GetUser().Username;
                checkPart2Data.UpdatedAt = DateTime.Now;
                checkPart2Data.Value = value;
                unitOfWork.IPDMedicalRecordPart2DataRepository.Update(checkPart2Data);
            }
            unitOfWork.Commit();
        }
        #endregion 
    }
}
