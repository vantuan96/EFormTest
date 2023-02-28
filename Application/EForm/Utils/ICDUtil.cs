using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Utils
{
    public class ICDUtil
    {
        private IUnitOfWork unitOfWork;

        public ICDUtil(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<string> GetICDByCustomer(Guid customer_id)
        {
            var list_icd = new List<string>();

            var dies = unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).Select(e => e.DischargeInformation).ToList();
            foreach (var di in dies)
            {
                var icd_raw = di.DischargeInformationDatas.Where(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    "DI0DIAICD,DI0DIAOPT".Contains(e.Code)
                ).Select(e => e.Value);
                foreach (var i in icd_raw)
                {
                    var icd = ICDConvert.Operate(i).Select(e => e.Code).ToList();
                    list_icd.AddRange(icd);
                }
            }


            var oens = unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).Select(e => e.OPDOutpatientExaminationNote).ToList();
            foreach (var oen in oens)
            {
                var icd_raw = oen.OPDOutpatientExaminationNoteDatas.Where(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    "OPDOENICDANS,OPDOENICDOPT".Contains(e.Code)
                ).Select(e => e.Value);
                foreach (var i in icd_raw)
                {
                    var icd = ICDConvert.Operate(i).Select(e => e.Code).ToList();
                    list_icd.AddRange(icd);
                }
            }

            var mere_ids = unitOfWork.IPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).Select(e => e.IPDMedicalRecordId).ToList();
            var part_2 = unitOfWork.IPDMedicalRecordRepository.Find(
                e => !e.IsDeleted &&
                mere_ids.Contains(e.Id) &&
                e.IPDMedicalRecordPart2 != null
            ).Select(e => e.IPDMedicalRecordPart2).ToList();
            foreach (var medical in part_2)
            {
                var icd_raw = medical.IPDMedicalRecordPart2Datas.Where(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    "IPDMRPTICDCANS,IPDMRPTICDPANS".Contains(e.Code)
                ).Select(e => e.Value);
                foreach (var i in icd_raw)
                {
                    var icd = ICDConvert.Operate(i).Select(e => e.Code).ToList();
                    list_icd.AddRange(icd);
                }
            }
            var eoc_ids = unitOfWork.EOCRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).ToList();
            foreach (var eoc in eoc_ids)
            {
                var icd_raw = unitOfWork.FormDatasRepository.Find(
                    e => !e.IsDeleted && e.FormCode == "OPDOEN" && e.VisitId == eoc.Id && "OPDOENICDANS,OPDOENICDOPT".Contains(e.Code)
                    ).ToList().Select(e => e.Value).ToList();
                foreach (var i in icd_raw)
                {
                    var icd = ICDConvert.Operate(i).Select(e => e.Code).ToList();
                    list_icd.AddRange(icd);
                }
            }
            return list_icd.Distinct().ToList();
        }
    }
}