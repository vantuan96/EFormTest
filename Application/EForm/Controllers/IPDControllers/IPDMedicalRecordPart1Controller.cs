using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Utils;
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
    public class IPDMedicalRecordPart1Controller : IPDMedicalRecordController
    {
        #region Part 1
        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part1/Create/{Type}/{id}")]
        [Permission(Code = "IMRPO1")]
        public IHttpActionResult CreateIPDMedicalRecordPart1API(string Type,Guid id)
        {
            
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record == null)
            {
                medical_record = new IPDMedicalRecord();
                unitOfWork.IPDMedicalRecordRepository.Add(medical_record);
                ipd.IPDMedicalRecordId = medical_record.Id;
                unitOfWork.IPDRepository.Update(ipd);
            }

            int currentVersion = getVersionOfMedicalrecord(formCode:Type,visitId:id, setCurrentVersion: 2);

            var medicalRecordPart1Id = medical_record.IPDMedicalRecordPart1Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart1Id);
            if (medical_record.IPDMedicalRecordPart1Id != null && medicalRecordOfPatient != null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_EXIST);
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


            if (medical_record.IPDMedicalRecordPart1Id == null)
            {
                var part_1 = new IPDMedicalRecordPart1();
                unitOfWork.IPDMedicalRecordPart1Repository.Add(part_1);

                medical_record.IPDMedicalRecordPart1Id = part_1.Id;
                unitOfWork.IPDMedicalRecordRepository.Update(medical_record);
                recordOfPatient.FormId = part_1.Id;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(recordOfPatient);
            }
            if(medicalRecordOfPatient == null )
            {
                recordOfPatient.FormId = medical_record.IPDMedicalRecordPart1Id;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(recordOfPatient);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/MedicalRecord/Part1/{Type}/{id}")]
        [Permission(Code = "IMRPO2")]
        public new IHttpActionResult GetIPDMedicalRecordPart1API(string Type,Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);        

            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);
            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record.IPDMedicalRecordPart1Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_NOT_FOUND);

            var medicalRecordPart1Id = medical_record.IPDMedicalRecordPart1Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart1Id);
            if (medicalRecordOfPatient == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildMedicalRecordPart1Result(Type, ipd, medical_record));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part1/{Type}/{id}")]
        [Permission(Code = "IMRPO3")]
        public IHttpActionResult UpdateIPDMedicalRecordPart1API(string Type,Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var setupMedicalRecord = GetSetupMedicalRecord(Type, ipd.SpecialtyId);
            //if (setupMedicalRecord == null || setupMedicalRecord.IsDeploy == false)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD);

            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);
            if (IPDIsBlock(ipd,Type.ToUpper()))
                return Content(HttpStatusCode.NotFound, Message.IPD_TIME_FORBIDDEN);
            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record.IPDMedicalRecordPart1Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_NOT_FOUND);
            var medicalRecordPart1Id = medical_record.IPDMedicalRecordPart1Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart1Id);
            if (medicalRecordOfPatient == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPO_NOT_FOUND);

            medicalRecordOfPatient.UpdatedAt = DateTime.Now;
            medicalRecordOfPatient.UpdatedBy = GetUser().Username;

            var part_1 = medical_record.IPDMedicalRecordPart1;

            HandleIPDMedicalRecordPart1Datas(medical_record, part_1, request["Datas"]);
            
            return Content(HttpStatusCode.OK, new { ipd.Customer.IsChronic });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part1/Sync/{Type}/{id}")]
        [Permission(Code = "IMRPO4")]
        public IHttpActionResult SyncIPDMedicalRecordPart1API(string Type,Guid id)
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
                    for (int i = 0; i < results.Count ; i++)
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
                if (IdOldOncologyMedicalRecord == null)
                {
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                }
                var opd_ipd = GetIPD(Guid.Parse(IdOldOncologyMedicalRecord));
                if (opd_ipd == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                if (opd_ipd.IPDMedicalRecordId == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                var medical_record = opd_ipd.IPDMedicalRecord;
                if (medical_record.IPDMedicalRecordPart1Id == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                return Content(HttpStatusCode.OK, BuildMedicalRecordPart1Result(Type, opd_ipd, medical_record));
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
                if (medical_record.IPDMedicalRecordPart1Id == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                return Content(HttpStatusCode.OK, BuildMedicalRecordPart1Result(Type, ipd, medical_record));
            }
        }
        #endregion
    }
}
    