using DataAccess.Repository;
using System;

namespace EForm.Utils
{
    public class Diagnosis
    {
        private IUnitOfWork unitOfWork;
        private Guid? FormId;
        private string VisitCode;

        public Diagnosis(IUnitOfWork unitOfWork, Guid? form_id, string visit_code)
        {
            this.unitOfWork = unitOfWork;
            this.FormId = form_id;
            this.VisitCode = visit_code;
        }

        public string GetData()
        {
            if (VisitCode == "ED")
            {
                return unitOfWork.DischargeInformationDataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.DischargeInformationId != null &&
                    e.DischargeInformationId == FormId &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code == "DI0DIAANS"
                )?.Value;
            }
            else if (VisitCode == "OPD")
            {
                return unitOfWork.OPDOutpatientExaminationNoteDataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.OPDOutpatientExaminationNoteId != null &&
                    e.OPDOutpatientExaminationNoteId == FormId &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code == "OPDOENDD0ANS"
                )?.Value;
            }
            else if (VisitCode == "IPD")
            {
                var mere = unitOfWork.IPDMedicalRecordRepository.FirstOrDefault(e => !e.IsDeleted && e.Id != null && e.Id == FormId);
                var part_3_id = mere?.IPDMedicalRecordPart2Id;
                return unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.IPDMedicalRecordPart2Id != null &&
                    e.IPDMedicalRecordPart2Id == part_3_id &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code == "IPDMRPTCDBCANS"
                )?.Value;
            }
            return string.Empty;
        }
    }
}