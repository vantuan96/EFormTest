using DataAccess.Repository;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Utils
{
    public class IPDMedicalRecordAssessment
    {
        private IUnitOfWork unitOfWork = new EfUnitOfWork();
        private Guid? IPDMedicalRecordPart2Id;

        public IPDMedicalRecordAssessment(Guid? mere_id)
        {
            this.IPDMedicalRecordPart2Id = mere_id;
        }

        public dynamic GetList()
        {
            return (from master in unitOfWork.MasterDataRepository.AsQueryable()
                    .Where(
                        i => !i.IsDeleted &&
                        !string.IsNullOrEmpty(i.Code) &&
                        Constant.IPD_MEDICAL_RECORD_ASSESSMENT.Contains(i.Code)
                    )
                    join data in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                    .Where(
                        i => !i.IsDeleted &&
                        i.IPDMedicalRecordPart2Id == this.IPDMedicalRecordPart2Id
                    ) on master.Code equals data.Code into ulist
                    from data in ulist.DefaultIfEmpty()
                    select new { master.ViName, master.EnName, data.Value, master.Order, master.Code })
                    .OrderBy(e => e.Order)
                    .Select(e => new { e.ViName, e.EnName, e.Value, e.Code })
                    .ToList();
        }

        public string GetString()
        {
            var assess_lst = GetList();
            string result = string.Empty;
            foreach (var item in assess_lst)
                result += $" {item.ViName}: \n{item.Value}\n";
            return result;
        }

        public dynamic GetDatasByCodes(string[] code)
        {
            var listDatas = (from master in unitOfWork.MasterDataRepository.AsQueryable()
                            .Where(
                                i => !i.IsDeleted &&
                                !string.IsNullOrEmpty(i.Code) &&
                                code.Contains(i.Code)
                             )
                             join data in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                             .Where(
                                 i => !i.IsDeleted &&
                                 i.IPDMedicalRecordPart2Id == this.IPDMedicalRecordPart2Id
                             ) on master.Code equals data.Code into ulist
                             from data in ulist.DefaultIfEmpty()
                             select new { master.ViName, master.EnName, data.Value, master.Order, master.Code })
                            .OrderBy(e => e.Order)
                            .Select(e => new { e.ViName, e.EnName, e.Value, e.Code })
                            .ToList();

            List<dynamic> datas = new List<dynamic>();
            int lenghOfCode = code.Length;
            for (int i = 0; i < lenghOfCode; i++)
            {
                var indexData = listDatas.FirstOrDefault(d => d.Code == code[i]);
                if (indexData != null)
                    datas.Add(indexData);
            }
            return datas.ToList();
        }

        public dynamic EditDatasByCodes(dynamic datas, string[] codes_edit)
        {
            var data_choices = new List<dynamic>(datas);
            foreach(var code in codes_edit)
            {
                var getData = data_choices.FirstOrDefault(d => d.Code == code);
                if (getData.Value == "False")
                    datas.Remove(getData);
            }
            return datas;
        }

        public string ConverToStringFromDatas(dynamic datas)
        {
            string _string = string.Empty;
            foreach(var s in datas)
            {
                _string += $" {s.ViName}: \n{s.Value}\n";
            }
            return _string;
        }
        public string CheckFormCodeMedicalRecord(Guid ipdId)
        {
            var medicalOfpatient = (from m in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                    where !m.IsDeleted && m.VisitId == ipdId
                                    select m).ToList();
            var part3MedicalRecord = (from m in medicalOfpatient
                                      join part3 in unitOfWork.IPDMedicalRecordPart3Repository.AsQueryable()
                                      on m.FormId equals part3.Id
                                      orderby m.UpdatedAt descending
                                      select m).ToList();
            if (part3MedicalRecord == null || part3MedicalRecord.Count == 0) return null;
            var checkMedicalRecord = part3MedicalRecord[0];
            return checkMedicalRecord.FormCode;
        }
    }
}