using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models.IPDModels;
using EForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers
{
    public class BaseIPDApiController : BaseApiController
    {
        protected TransferInfoModel GetFirstIpd(IPD ipd)
        {
            var spec = ipd.Specialty;
            var current_doctor = ipd.PrimaryDoctor;

            var transfers = new IPDTransfer(ipd).GetListInfo();
            TransferInfoModel first_ipd = null;
            if (transfers.Count() > 0)
            {
                first_ipd = transfers.FirstOrDefault(e => e.CurrentType == "ED" || e.CurrentType == "IPD" && (string.IsNullOrEmpty(e.CurrentSpecialtyCode) || !e.CurrentSpecialtyCode.Contains("PTTT")));
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
            if (first_ipd == null)
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
        protected void UpdateVisit(IPD visit)
        {
            unitOfWork.IPDRepository.Update(visit);
            unitOfWork.Commit();
        }
        protected List<MasterData> GetSpecialtySetupForm(Guid specialtyId)
        {
            IQueryable<MasterData> list_temp = (from spec in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                                                .Where(e => !e.IsDeleted && e.SpecialityId == specialtyId && e.IsDeploy && e.FormType == "MedicalRecords")
                                                join md in unitOfWork.MasterDataRepository.AsQueryable()
                                                .Where(e => !e.IsDeleted && e.Group == "MedicalRecords" && e.Note == "IPD")
                                                on spec.Formcode equals md.Form
                                                select md);

            IQueryable<MasterData> form_group = (from md in list_temp
                                                join e in unitOfWork.MasterDataRepository.AsQueryable()
                                                .Where(e => !e.IsDeleted && e.Note == "IPD")
                                                on md.Form equals e.Group
                                                select e);
            List<MasterData> result = list_temp.Concat(form_group).Where(e => e.Level > 1).ToList();
            return result;
        }
        protected List<dynamic> GetMedicalRecordsSavedOrSetup(Guid visitId, List<MasterData> specialty_setupForm)
        {
            List<MasterData> forms_md = GetFormPatienSaved(visitId);

            List<MasterData> list_temp = forms_md.Concat(specialty_setupForm).GroupBy(e => new { e.Code, e.Form }).Select(e => e.FirstOrDefault()).Where(e => e.Level == 2).OrderBy(e => e.Order).ToList();
            List<dynamic> forms_result = new List<dynamic>();
            foreach (var item in list_temp)
            {
                if (item.DataType == "BENHAN")
                {
                    var part2 = new { FormCode = item.Form, ViName = item.ViName, EnName = item.EnName, Type = item.Code + "/Part2", Role = new int[] { 1, 3 } };

                    var part1 = new { FormCode = item.Form, ViName = item.ViName, EnName = item.EnName, Type = item.Code + "/Part1", Role = new int[] { 2 } };

                    var print = new { FormCode = item.Form, ViName = item.ViName, EnName = item.EnName, Type = item.Code + "/Print", Role = new int[] { 4 } };

                    forms_result.Add(part2);
                    forms_result.Add(part1);
                    forms_result.Add(print);
                }
                else
                {
                    var obj = new { FormCode = item.Form, ViName = item.ViName, EnName = item.EnName, Type = item.Code, Role = new int[] { } };

                    forms_result.Add(obj);
                }
            }
            return forms_result;
        }

        protected List<MasterData> GetFormPatienSaved(Guid visitId)
        {
            List<MasterData> forms_md = (from e in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                               .Where(e => !e.IsDeleted && e.VisitId == visitId)
                                               join md in unitOfWork.MasterDataRepository.AsQueryable()
                                               .Where(e => !e.IsDeleted && e.Note == "IPD" && e.DataType != null && (e.DataType.ToUpper() == "BENHAN" || e.DataType.ToUpper() == "PromissoryNote".ToUpper()))
                                               on e.FormCode equals md.Form
                                               select md).ToList();
            return forms_md;
        }
    }
}
