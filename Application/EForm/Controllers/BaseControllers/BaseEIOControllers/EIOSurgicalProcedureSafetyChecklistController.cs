using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOSurgicalProcedureSafetyChecklistController : BaseApiController
    {
        #region Surgical Procedure Safety Checklist
        protected EIOSurgicalProcedureSafetyChecklist GetSurgicalProcedureSafetyChecklist(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }
        protected IEnumerable<EIOSurgicalProcedureSafetyChecklistData> GetSurgicalProcedureSafetyChecklistData(Guid id, string visit_type)
        {
            return unitOfWork.EIOSurgicalProcedureSafetyChecklistDataRepository.Find(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistId != null &&
                e.EIOSurgicalProcedureSafetyChecklistId == id &&
                !string.IsNullOrEmpty(e.EIOSurgicalProcedureSafetyChecklistCode) &&
                e.EIOSurgicalProcedureSafetyChecklistCode == visit_type
            );
        }
        protected void HandleSurgicalProcedureSafetyChecklistData(Guid id, string visit_type, JToken request)
        {
            var datas = GetSurgicalProcedureSafetyChecklistData(id, visit_type);
            foreach (var item in request)
            {
                var code = item.Value<string>("Code");
                var data = GetOrCreateSurgicalProcedureSafetyChecklistData(code, id, visit_type, datas);
                if (data != null)
                    UpdateSurgicalProcedureSafetyChecklistData(data, item);
            }
            unitOfWork.Commit();
        }
        protected EIOSurgicalProcedureSafetyChecklistData GetOrCreateSurgicalProcedureSafetyChecklistData(string code, Guid id, string visit_type, IEnumerable<EIOSurgicalProcedureSafetyChecklistData> datas)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data != null) return data;

            data = new EIOSurgicalProcedureSafetyChecklistData()
            {
                Code = code,
                EIOSurgicalProcedureSafetyChecklistId = id,
                EIOSurgicalProcedureSafetyChecklistCode = visit_type,
            };
            unitOfWork.EIOSurgicalProcedureSafetyChecklistDataRepository.Add(data);
            return data;
        }
        protected void UpdateSurgicalProcedureSafetyChecklistData(EIOSurgicalProcedureSafetyChecklistData data, JToken item)
        {
            data.Value = item.Value<string>("Value");
            unitOfWork.EIOSurgicalProcedureSafetyChecklistDataRepository.Update(data);
        }
        protected bool HaveUncompletedSafetyChecklistData(Guid? spsc_id, string group_code)
        {
            var datas = unitOfWork.EIOSurgicalProcedureSafetyChecklistDataRepository.Find(
                e => !e.IsDeleted &&
                e.EIOSurgicalProcedureSafetyChecklistId != null &&
                e.EIOSurgicalProcedureSafetyChecklistId == spsc_id &&
                !string.IsNullOrEmpty(e.EIOSurgicalProcedureSafetyChecklistCode) &&
                e.EIOSurgicalProcedureSafetyChecklistCode == group_code
            ).ToList();
            if (datas.Count == 0)
                return true;


            var completed_data = datas.Where(e => !string.IsNullOrEmpty(e.Value)).Count();
            if (group_code == "SignIn")
                return completed_data < 12 ? true : false;

            return completed_data < 17 ? true : false;
        }
        #endregion
    }
}
