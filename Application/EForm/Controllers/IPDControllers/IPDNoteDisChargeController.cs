using DataAccess.Models.EDModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using System;
using System.Net;
using System.Web.Http;
using System.Linq;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDNoteDisChargeController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/IPD/IPDNoteDisCharge/SyncDataFromIPDMedicalRecord/{visitId}")]
        public IHttpActionResult GetDataFromIPDMedicalRecords(Guid visitId)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildDataAutofill(visit));
        }

        private DateTime? GetAdmitedDate(IPD visit)
        {
            var tranferId = visit.TransferFromId;
            if (tranferId == null)
                return visit.CreatedAt;

            ED ed_visit = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.HandOverCheckListId == tranferId);
            if (ed_visit != null)
                return ed_visit.CreatedAt;

            return visit.CreatedAt;
        }

        private object BuildDataAutofill(IPD visit)
        {
            var data2 = visit.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas;
            var data3 = visit.IPDMedicalRecord?.IPDMedicalRecordPart3?.IPDMedicalRecordPart3Datas;

            var diagnosis = GetVisitDiagnosisAndICD(visit.Id, "IPD", getForPrescription: true);
            string dischargeReason = GetStatusDischargeReason(visit);

            object obj = new
            {
                AdmitedDate = GetAdmitedDate(visit),
                Specialty = new
                {
                    visit.Specialty.ViName,
                    visit.Specialty.EnName
                },
                ChiefComplaints = data2?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTLDVVANS")?.Value,
                History = data2?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTQTBLANS")?.Value,
                Diagnosis = diagnosis,
                TreatmentsAndProcedures = data3?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPEPPDTANS")?.Value,
                PatientStatusAtDischarge = data3?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPETTNBANS")?.Value,
                DischargeReason = dischargeReason
            };
            return obj;
        }

        public string GetStatusDischargeReason(IPD visit)
        {
            var status = visit.EDStatus;
            var dataForm = visit.IPDMedicalRecord?.IPDMedicalRecordDatas;
            if (status?.EnName?.ToUpper() == "Inter-hospital transfer".ToUpper())
                return $"{{Chuyển viện}} {dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDRH0ANS")?.Value}";

            if (status?.EnName?.ToUpper() == "Discharged".ToUpper() && status?.Code == "IPDDC")
            {
                if (dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTHTRVXIV")?.Value?.ToUpper() == "True".ToUpper())
                {
                    if (dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPGRV02")?.Value?.ToUpper() == "True".ToUpper())
                        return "Xin về - Chuyên môn";
                    else if (dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPGRV03")?.Value?.ToUpper() == "True".ToUpper())
                        return "Xin về - Kinh tế";
                    else if (dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPGRV04")?.Value?.ToUpper() == "True".ToUpper())
                        return $"Xin về - {dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPGRV05")?.Value}";
                    else
                        return null;
                }
                if (dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTHTRVBOV")?.Value?.ToUpper() == "True".ToUpper())
                    return "Bỏ về";
                if (dataForm?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTHTRVRVE")?.Value?.ToUpper() == "True".ToUpper())
                    return "Đưa về";
            }
            return null;
        }
    }
}