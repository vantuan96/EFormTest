using DataAccess.Models.EDModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using EForm.Common;
using EForm.Models;
using EForm.Models.IPDModels;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Utils
{
    public class IPDTransfer
    {
        private IPD ipd;
        private IUnitOfWork unitOfWork = new EfUnitOfWork();

        public IPDTransfer(IPD ipd)
        {
            this.ipd = ipd;
        }

        public List<TransferInfoModel> GetListInfo()
        {
            var status_code = ipd.EDStatus?.Code;
            var transfers = new List<TransferInfoModel>();
            var current_doctor = ipd.PrimaryDoctor;
            var current_spec = ipd.Specialty;

            if (!Constant.Transfer.Contains(status_code) || ipd.HandOverCheckListId == null)
                transfers.Add(new TransferInfoModel
                {
                    CurrentTransferFromId = ipd.TransferFromId,
                    CurrentSpecialty = new { current_spec?.ViName, current_spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentType = "IPD",
                    CurrentSpecialtyCode = current_spec.Code
                });
            else
            {
                var hocl = ipd.HandOverCheckList;
                var transfer_specialty = hocl.ReceivingUnitPhysician;

                dynamic visit = null;
                string transfer_type = "ED";
                visit = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.TransferFromId != null && e.TransferFromId == hocl.Id);
                if (visit == null)
                {
                    visit = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.TransferFromId != null && e.TransferFromId == hocl.Id);
                    transfer_type = "OPD";
                }
                if (visit == null)
                {
                    visit = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.TransferFromId != null && e.TransferFromId == hocl.Id);
                    transfer_type = "IPD";
                }
                var transfer_doctor = visit?.PrimaryDoctor;
                DiagnosisAndICDModel icd_info = GetVisitDiagnosisAndICD(visit?.Id, transfer_type);
                transfers.Add(new TransferInfoModel
                {
                    TransferSpecialty = new { transfer_specialty?.ViName, transfer_specialty?.EnName },
                    TransferDoctor = new { transfer_doctor?.Username, transfer_doctor?.Fullname, transfer_doctor?.DisplayName },
                    TransferRawDate = visit?.AdmittedDate,
                    TransferType = transfer_type,
                    CurrentTransferFromId = ipd.TransferFromId,
                    CurrentSpecialty = new { current_spec?.ViName, current_spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentType = "IPD",
                    DischargeDate = visit?.DischargeDate,
                    CurrentDiagnosis = icd_info.Diagnosis,
                    CurrentICD = icd_info.ICD,
                    TransferSpecialtyCode = transfer_specialty.Code,
                    CurrentSpecialtyCode = current_spec.Code
                });
            }

            var results = GetTranferInfo(transfers).Distinct().OrderBy(e => e.CurrentRawDate).ToList();
            for (var i = 0; i < results.Count - 1; i++)
            {
                //results[i].TransferSpecialtyCode = results[i + 1].TransferSpecialtyCode;
                results[i].TransferSpecialty = results[i + 1].CurrentSpecialty;
                results[i].TransferRawDate = results[i + 1].CurrentRawDate;
                results[i].TransferDoctor = results[i + 1].CurrentDoctor;
                results[i].TransferType = results[i + 1].CurrentType;
            }

            return results.Select(e => new TransferInfoModel
            {
                TransferSpecialty = e.TransferSpecialty,
                TransferDoctor = e.TransferDoctor,
                TransferRawDate = e.TransferRawDate,
                TransferDate = e.TransferRawDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                TransferType = e.TransferType,
                CurrentTransferFromId = e.CurrentTransferFromId,
                CurrentSpecialty = e.CurrentSpecialty,
                CurrentDoctor = e.CurrentDoctor,
                CurrentRawDate = e.CurrentRawDate,
                CurrentDate = e.CurrentRawDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                CurrentType = e.CurrentType,
                CurrentDiagnosis = e.CurrentDiagnosis,
                CurrentICD = e.CurrentICD,
                DischargeDate = e.DischargeDate,
                TransferSpecialtyCode = e.TransferSpecialtyCode,
                CurrentSpecialtyCode = e.CurrentSpecialtyCode
            }).ToList();
        }
        protected DiagnosisAndICDModel GetVisitDiagnosisAndICD(Guid? visit_id, string visit_type)
        {
            if (visit_id == null)
            {
                return new DiagnosisAndICDModel
                {
                };
            }
            if (visit_type == "ED")
            {
                ED visit = GetED((Guid)visit_id);
                if (visit != null)
                {
                    var data_di = visit.DischargeInformation.DischargeInformationDatas;
                    return new DiagnosisAndICDModel
                    {
                        ICD = data_di.FirstOrDefault(e => e.Code == "DI0DIAICD")?.Value,
                        Diagnosis = data_di.FirstOrDefault(e => e.Code == "DI0DIAANS")?.Value
                    };
                }
            }
            if (visit_type == "OPD")
            {
                OPD visit = GetOPD((Guid)visit_id);
                if (visit != null)
                {
                    var data_eon = visit.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas;
                    return new DiagnosisAndICDModel
                    {
                        ICD = data_eon.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value,
                        Diagnosis = data_eon.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value
                    };
                }
            }
            if (visit_type == "IPD")
            {
                IPD visit = GetIPD((Guid)visit_id);
                if (visit != null)
                {
                    var medical_record = visit.IPDMedicalRecord;
                    if (medical_record != null)
                    {
                        var part_3 = visit.IPDMedicalRecord.IPDMedicalRecordPart3;
                        if (part_3 != null)
                        {
                            var data_eon = visit.IPDMedicalRecord.IPDMedicalRecordPart3.IPDMedicalRecordPart3Datas;
                            return new DiagnosisAndICDModel
                            {
                                ICD = data_eon.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value,
                                Diagnosis = data_eon.FirstOrDefault(e => e.Code == "IPDMRPECDBCANS")?.Value
                            };
                        }
                    }
                }
            }
            return new DiagnosisAndICDModel
            {
            };
        }
        protected ED GetED(Guid id)
        {
            var ed = unitOfWork.EDRepository.GetById(id);

            if (ed == null || ed.IsDeleted)
                return null;

            return ed;
        }

        protected OPD GetOPD(Guid id)
        {
            var opd = unitOfWork.OPDRepository.GetById(id);

            if (opd == null || opd.IsDeleted)
                return null;

            return opd;
        }

        protected IPD GetIPD(Guid id)
        {
            var ipd = unitOfWork.IPDRepository.GetById(id);

            if (ipd == null || ipd.IsDeleted)
                return null;

            return ipd;
        }
        private List<TransferInfoModel> GetTranferInfo(List<TransferInfoModel> old_transfers)
        {
            if (old_transfers.Count < 1)
                return old_transfers;

            var last_visit = old_transfers[0];
            if (last_visit.CurrentTransferFromId == null)
                return old_transfers;

            var transfers = new List<TransferInfoModel>();

            var ed = GetEDTransferInfo(last_visit.CurrentTransferFromId);
            if (ed != null)
            {
                transfers.Add(ed);
                transfers.AddRange(old_transfers);
                if (ed.CurrentTransferFromId != null)
                    transfers.AddRange(GetTranferInfo(transfers));
            }

            var opd = GetOPDTransferInfo(last_visit.CurrentTransferFromId);
            if (opd != null)
            {
                transfers.Add(opd);
                transfers.AddRange(old_transfers);
                if (opd.CurrentTransferFromId != null)
                    transfers.AddRange(GetTranferInfo(transfers));
            }

            var ipd = GetIPDTransferInfo(last_visit.CurrentTransferFromId);
            if (ipd != null)
            {
                transfers.Add(ipd);
                transfers.AddRange(old_transfers);
                if (ipd.CurrentTransferFromId != null)
                    transfers.AddRange(GetTranferInfo(transfers));
            }

            return transfers;
        }
        private TransferInfoModel GetEDTransferInfo(Guid? transfer_from_id)
        {
            return (from visit in unitOfWork.EDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.HandOverCheckListId != null &&
                        e.HandOverCheckListId == transfer_from_id
                    )
                    join di in unitOfWork.DischargeInformationRepository.AsQueryable()
                        on visit.DischargeInformationId equals di.Id
                    join doctor in unitOfWork.UserRepository.AsQueryable()
                        on di.UpdatedBy equals doctor.Username
                        into doclist
                    from doctor in doclist.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                        on visit.SpecialtyId equals spec.Id
                        into spelist
                    from spec in spelist.DefaultIfEmpty()
                    join diagnosis in unitOfWork.DischargeInformationDataRepository.AsQueryable().Where(e => e.Code == "DI0DIAANS")
                        on visit.DischargeInformationId equals diagnosis.DischargeInformationId
                        into diaglist
                    from diagnosis in diaglist.DefaultIfEmpty()
                    join icd in unitOfWork.DischargeInformationDataRepository.AsQueryable().Where(e => e.Code == "DI0DIAICD")
                        on visit.DischargeInformationId equals icd.DischargeInformationId
                        into ilist
                    from icd in ilist.DefaultIfEmpty()
                    select new TransferInfoModel
                    {
                        CurrentTransferFromId = visit.TransferFromId,
                        CurrentSpecialty = new { spec.ViName, spec.EnName },
                        CurrentSpecialtyCode = spec.Code,
                        CurrentDoctor = new { doctor.Username, doctor.Fullname, doctor.DisplayName },
                        CurrentRawDate = visit.AdmittedDate,
                        CurrentType = "ED",
                        CurrentDiagnosis = diagnosis.Value,
                        CurrentICD = icd.Value,
                    }).FirstOrDefault();
        }
        private TransferInfoModel GetOPDTransferInfo(Guid? transfer_from_id)
        {
            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.OPDHandOverCheckListId != null &&
                        e.OPDHandOverCheckListId == transfer_from_id
                    )
                    join doctor in unitOfWork.UserRepository.AsQueryable()
                        on visit.PrimaryDoctorId equals doctor.Id
                        into doclist
                    from doctor in doclist.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                        on visit.SpecialtyId equals spec.Id
                        into spelist
                    from spec in spelist.DefaultIfEmpty()
                    join diagnosis in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable().Where(e => e.Code == "OPDOENDD0ANS")
                        on visit.OPDOutpatientExaminationNoteId equals diagnosis.OPDOutpatientExaminationNoteId
                        into diaglist
                    from diagnosis in diaglist.DefaultIfEmpty()
                    join icd in unitOfWork.OPDOutpatientExaminationNoteDataRepository.AsQueryable().Where(e => e.Code == "OPDOENICDANS")
                        on visit.OPDOutpatientExaminationNoteId equals icd.OPDOutpatientExaminationNoteId
                        into ilist
                    from icd in ilist.DefaultIfEmpty()
                    select new TransferInfoModel
                    {
                        CurrentTransferFromId = visit.TransferFromId,
                        CurrentSpecialty = new { spec.ViName, spec.EnName },
                        CurrentDoctor = new { doctor.Username, doctor.Fullname, doctor.DisplayName },
                        CurrentRawDate = visit.AdmittedDate,
                        CurrentType = "OPD",
                        CurrentDiagnosis = diagnosis.Value,
                        CurrentSpecialtyCode = spec.Code,
                        CurrentICD = icd.Value,
                    }).FirstOrDefault();
        }
        private TransferInfoModel GetIPDTransferInfo(Guid? transfer_from_id)
        {
            return (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.HandOverCheckListId != null &&
                        e.HandOverCheckListId == transfer_from_id
                    )
                    join doctor in unitOfWork.UserRepository.AsQueryable()
                        on visit.PrimaryDoctorId equals doctor.Id
                        into doclist
                    from doctor in doclist.DefaultIfEmpty()
                    join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                        on visit.SpecialtyId equals spec.Id
                        into spelist
                    from spec in spelist.DefaultIfEmpty()
                    join mere in unitOfWork.IPDMedicalRecordRepository.AsQueryable()
                        on visit.IPDMedicalRecordId equals mere.Id
                    join diagnosis in unitOfWork.IPDMedicalRecordPart3DataRepository.AsQueryable().Where(e => e.Code == "IPDMRPECDBCANS")
                        on mere.IPDMedicalRecordPart3Id equals diagnosis.IPDMedicalRecordPart3Id
                        into diaglist
                    from diagnosis in diaglist.DefaultIfEmpty()
                    join icd in unitOfWork.IPDMedicalRecordPart3DataRepository.AsQueryable().Where(e => e.Code == "IPDMRPEICDCANS")
                        on mere.IPDMedicalRecordPart3Id equals icd.IPDMedicalRecordPart3Id
                        into ilist
                    from icd in ilist.DefaultIfEmpty()
                    select new TransferInfoModel
                    {
                        CurrentTransferFromId = visit.TransferFromId,
                        CurrentSpecialty = new { spec.ViName, spec.EnName },
                        CurrentSpecialtyCode = spec.Code,
                        CurrentDoctor = new { doctor.Username, doctor.Fullname, doctor.DisplayName },
                        CurrentRawDate = visit.AdmittedDate,
                        CurrentType = "IPD",
                        CurrentDiagnosis = diagnosis.Value,
                        CurrentICD = icd.Value
                    }).FirstOrDefault();
        }
    }
}