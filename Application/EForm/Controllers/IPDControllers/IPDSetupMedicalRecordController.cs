using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.MedicalRecordModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDSetupMedicalRecordController : BaseIPDApiController
    {
        public class IPDMedicalRecordType
        {
            public string Type { get; set; }
            public string FormCode { get; set; }

        }
        ////public List<IPDMedicalRecordType> GetMedicalRecords(Guid ipdId)
        ////{
        ////    IPD visit = GetIPD(ipdId);
        ////    if (visit == null)
        ////    {
        ////        return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
        ////    }
        ////    var specialtyId = visit.SpecialtyId;
        ////    var medicalRecord = visit.IPDMedicalRecordId;
        ////    var listMedicalRecordIsDeploy = (from m in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
        ////                                     where m.SpecialityId == specialtyId && m.IsDeploy == true
        ////                                     select new IPDMedicalRecordType()
        ////                                     {
        ////                                         Type = m.Type,
        ////                                         FormCode = m.Formcode
        ////                                     }).ToList();



        ////    if (medicalRecord == null)
        ////    {
        ////        if (listMedicalRecordIsDeploy.Count == 0)
        ////        {
        ////            return null;
        ////        }

        ////        return listMedicalRecordIsDeploy;
        ////    }
        ////    else
        ////    {
        ////        var formMedicalRecord = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
        ////                                    .Where(m => m.VisitId == ipdId && !m.IsDeleted)
        ////                                    .Select(m => new
        ////                                    {
        ////                                        FormCode = m.FormCode
        ////                                    }).ToList();

        ////        foreach(var item in formMedicalRecord)
        ////        {
        ////            var setupMedical = (from s in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
        ////                                where s.SpecialityId == specialtyId && !s.IsDeploy && s.Formcode == item.FormCode
        ////                                select new IPDMedicalRecordType()
        ////                                {
        ////                                    Type = s.Type,
        ////                                    FormCode = s.Formcode
        ////                                }).FirstOrDefault();

        ////            listMedicalRecordIsDeploy.Add(setupMedical);
        ////        }
        ////        return listMedicalRecordIsDeploy;
        ////    }
        ////}

        //[HttpPost]
        //[Route("api/IPD/CreateIPDMedicalRecordOfPatient/{ipdId}/{formCode}")]
        //public IHttpActionResult CreateIPDMedicalRecordOfPatient(Guid ipdId, string formCode)
        //{
        //    IPD visit = GetIPD(ipdId);
        //    if (visit == null)
        //    {
        //        return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
        //    }
        //    var medicalRecodOfPatient =from me in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
        //                               where me.
        //    var ipdMedicalrecordOfPatien = new IPDMedicalRecordOfPatients();
        //    if (medicalRecodId == null)
        //    {
        //        ipdMedicalrecordOfPatien.IPDMedicalRecordId = new Guid();
        //        ipdMedicalrecordOfPatien.FormCode = formCode;
        //        ipdMedicalrecordOfPatien.VisitId = (Guid)ipdId;
        //        ipdMedicalrecordOfPatien.CreatedAt = DateTime.Now;
        //        ipdMedicalrecordOfPatien.CreatedBy = GetUser().Username;
        //        unitOfWork.IPDMedicalRecordOfPatientRepository.Add(ipdMedicalrecordOfPatien);
        //    }
        //    else
        //    {
        //        var medicalRecordType = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
        //                               .Where(m => m.IPDMedicalRecordId == medicalRecodId && m.FormCode == formCode && !m.IsDeleted)
        //                               .FirstOrDefault();
        //        if (medicalRecordType == null)
        //        {
        //            ipdMedicalrecordOfPatien.IPDMedicalRecordId = (Guid)medicalRecodId;
        //            ipdMedicalrecordOfPatien.FormCode = formCode;
        //            ipdMedicalrecordOfPatien.VisitId = (Guid)ipdId;
        //            ipdMedicalrecordOfPatien.CreatedAt = DateTime.Now;
        //            ipdMedicalrecordOfPatien.CreatedBy = GetUser().Username;
        //            unitOfWork.IPDMedicalRecordOfPatientRepository.Add(ipdMedicalrecordOfPatien);
        //        }
        //    }
        //    unitOfWork.Commit();
        //    return Content(HttpStatusCode.OK, Message.SUCCESS);
        //}
    }
}
