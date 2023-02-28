using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOPreOperativeProcedureHandoverChecklistsController: BaseApiController
    {
        protected EIOPreOperativeProcedureHandoverChecklist CreatePreOperativeProcedureHandoverChecklist(dynamic visit, string visit_type)
        {
            var phc = new EIOPreOperativeProcedureHandoverChecklist
            {
                VisitId = visit.Id,
                VisitTypeGroupCode = visit_type,
            };
            unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.Add(phc);

            string diagnosis = "";
            if(visit_type == "ED")
                diagnosis = GetEDDiagnosis(visit.DischargeInformationId);
            else if (visit_type == "OPD")
                diagnosis = GetOPDDiagnosis(visit.OPDOutpatientExaminationNoteId);
            CreatePreOperativeProcedureHandoverChecklistDatas(phc.Id, "PHCPD0ANS", diagnosis);

            CreatePreOperativeProcedureHandoverChecklistDatas(phc.Id, "PHCAKAANS", visit.Allergy);

            unitOfWork.Commit();
            return phc;
        }

        private string GetAllergy(Customer customer)
        {
            if (customer.IsAllergy && string.IsNullOrEmpty(customer.Allergy) && !string.IsNullOrEmpty(customer.KindOfAllergy))
                return Constant.KIND_OF_ALLERGIC[customer.KindOfAllergy];

            return customer.Allergy;
        } 
        private string GetEDDiagnosis(Guid form_id)
        {
            var diagnosis = unitOfWork.DischargeInformationDataRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.DischargeInformationId == form_id &&
                e.Code == "DI0DIAANS"
            );
            return diagnosis?.Value;
        }
        private string GetOPDDiagnosis(Guid form_id)
        {
            var diagnosis = unitOfWork.OPDOutpatientExaminationNoteDataRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.OPDOutpatientExaminationNoteId == form_id &&
                e.Code == "OPDOENDD0ANS"
            );
            return diagnosis?.Value;
        }

        protected EIOPreOperativeProcedureHandoverChecklist GetPreOperativeProcedureHandoverChecklist(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == visit_type
            );
        }

        protected dynamic BuildPreOperativeProcedureHandoverChecklist(EIOPreOperativeProcedureHandoverChecklist phc, Customer customer, int app_version, bool IsLocked = false)
        {
            return new
            {
                FormIsLocked = IsLocked,
                IsLocked = IsLocked,
                phc.Id,
                DateTimeHandover = phc.DateTimeHandover?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                phc.WardNurse,
                phc.EscortNurse,
                phc.ReceivingNurse,
                Datas = phc.PreOperativeProcedureHandoverChecklistDatas.Where(e => !e.IsDeleted)
                .Select(phcd => new { phcd.Id, phcd.Code, phcd.Value })
                .ToList(),
                customer = new {
                    customer?.PID,
                    customer?.Fullname,
                    DateOfBirth = customer?.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    customer.Gender
                },
                phc.CreatedBy,
                phc.CreatedAt,
                phc.UpdatedAt,
                phc.UpdatedBy,
                Version = app_version
            };
        }

        protected void HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(EIOPreOperativeProcedureHandoverChecklist phc, JObject request)
        {
            UpdatePreOperativeProcedureHandoverChecklists(phc, request);

            var phc_datas = phc.PreOperativeProcedureHandoverChecklistDatas.Where(e => !e.IsDeleted).ToList();
            var request_phc_data = request["Datas"];
            foreach (var item in request_phc_data)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");
                var phc_data = phc_datas.FirstOrDefault(e => e.Code == code);
                if (phc_data == null)
                    CreatePreOperativeProcedureHandoverChecklistDatas(phc.Id, code, value);
                else if (phc_data.Value != value)
                    UpdatePreOperativeProcedureHandoverChecklistDatas(phc_data, code, value);
            }
            // phc.UpdatedBy = getUsername();
            unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.Update(phc);
            unitOfWork.Commit();
        }

        private void UpdatePreOperativeProcedureHandoverChecklists(EIOPreOperativeProcedureHandoverChecklist phc, JObject request)
        {
            bool is_change = false;
            DateTime date_time_handover;
            bool ret = DateTime.TryParseExact(request["DateTimeHandover"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date_time_handover);
            if (phc.DateTimeHandover != date_time_handover && ret)
            {
                phc.DateTimeHandover = date_time_handover;
                is_change = true;
            }
            var ward_nurse = request["WardNurse"]?.ToString();
            if (phc.WardNurse != ward_nurse)
            {
                phc.WardNurse = ward_nurse;
                is_change = true;
            }
            var escort_nurse = request["EscortNurse"]?.ToString();
            if (phc.EscortNurse != escort_nurse)
            {
                phc.EscortNurse = escort_nurse;
                is_change = true;
            }
            var receiving_nurse = request["ReceivingNurse"]?.ToString();
            if (phc.ReceivingNurse != receiving_nurse)
            {
                phc.ReceivingNurse = receiving_nurse;
                is_change = true;
            }
            if (is_change)
            {
                unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.Update(phc);
                unitOfWork.Commit();
            }
        }

        private void UpdatePreOperativeProcedureHandoverChecklistDatas(EIOPreOperativeProcedureHandoverChecklistData phc_data, string code, string value)
        {
            if (!"PHCEG0DT0,PHCFF0DT0".Contains(code) || Validator.ValidateTimeDateWithoutSecond(value))
            {
                phc_data.Value = value;
                unitOfWork.EIOPreOperativeProcedureHandoverChecklistDataRepository.Update(phc_data);
            }
        }

        private void CreatePreOperativeProcedureHandoverChecklistDatas(Guid phc_id, string code, string value)
        {
            if (!"PHCEG0DT0,PHCFF0DT0".Contains(code) || Validator.ValidateTimeDateWithoutSecond(value))
            {
                EIOPreOperativeProcedureHandoverChecklistData phc_data = new EIOPreOperativeProcedureHandoverChecklistData();
                phc_data.PreOperativeProcedureHandoverChecklistId = phc_id;
                phc_data.Code = code;
                phc_data.Value = value;
                unitOfWork.EIOPreOperativeProcedureHandoverChecklistDataRepository.Add(phc_data);
            }
        }
    }
}