using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOSpongeSharpsAndInstrumentsCountsSheetController : BaseApiController
    {
        protected EIOSpongeSharpsAndInstrumentsCountsSheet CreateSpongeSharpsAndInstrumentsCountsSheet(Guid visit_id, string visit_type)
        {
            var ssaic = new EIOSpongeSharpsAndInstrumentsCountsSheet
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
            };
            unitOfWork.EIOSpongeSharpsAndInstrumentsCountsSheetRepository.Add(ssaic);
            unitOfWork.Commit();
            return ssaic;
        }

        protected EIOSpongeSharpsAndInstrumentsCountsSheet GetSpongeSharpsAndInstrumentsCountsSheet(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOSpongeSharpsAndInstrumentsCountsSheetRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == visit_type
            );
        }

        protected dynamic BuildSpongeSharpsAndInstrumentsCountsSheet(EIOSpongeSharpsAndInstrumentsCountsSheet ssaic, int app_version, bool? IsLocked = false)
        {
            return new
            {
                IsLocked,
                ssaic.Id,
                DateTimeSheet = ssaic.DateTimeSheet?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ssaic.ScrubNurse,
                ssaic.CirculatingNurse,
                ssaic.Surgeon,
                Datas = ssaic.SpongeSharpsAndInstrumentsCountsSheetDatas.Where(e => !e.IsDeleted)
                .Select(phcd => new { phcd.Id, phcd.Code, phcd.Value })
                .ToList(),
                Version = app_version,
                ssaic.CreatedAt,
                ssaic.CreatedBy,
                ssaic.UpdatedBy,
                ssaic.UpdatedAt
            };
        }

        protected void HandleUpdateSpongeSharpsAndInstrumentsCountsSheet(EIOSpongeSharpsAndInstrumentsCountsSheet ssaic, JObject request)
        {
            UpdateSpongeSharpsAndInstrumentsCountsSheets(ssaic, request);

            var ssaic_datas = ssaic.SpongeSharpsAndInstrumentsCountsSheetDatas.Where(e => !e.IsDeleted).ToList();
            var request_ssaic_data = request["Datas"];
            foreach (var item in request_ssaic_data)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");
                var ssaic_data = ssaic_datas.FirstOrDefault(e => e.Code == code);
                if (ssaic_data == null)
                    CreateSpongeSharpsAndInstrumentsCountsSheetDatas(ssaic.Id, code, value);
                else if (ssaic_data.Value != value)
                    UpdateSpongeSharpsAndInstrumentsCountsSheetDatas(ssaic_data, code, value);

                ssaic.UpdatedBy = GetUser().Username;
                unitOfWork.EIOSpongeSharpsAndInstrumentsCountsSheetRepository.Update(ssaic);
            }
            unitOfWork.Commit();
        }
        
        private void UpdateSpongeSharpsAndInstrumentsCountsSheets(EIOSpongeSharpsAndInstrumentsCountsSheet ssaic, JObject request)
        {
            bool is_change = false;
            DateTime date_time_sheet = DateTime.ParseExact(request["DateTimeSheet"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            if (ssaic.DateTimeSheet != date_time_sheet)
            {
                ssaic.DateTimeSheet = date_time_sheet;
                is_change = true;
            }
            var ward_nurse = request["ScrubNurse"]?.ToString();
            if (ssaic.ScrubNurse != ward_nurse)
            {
                ssaic.ScrubNurse = ward_nurse;
                is_change = true;
            }
            var escort_nurse = request["CirculatingNurse"]?.ToString();
            if (ssaic.CirculatingNurse != escort_nurse)
            {
                ssaic.CirculatingNurse = escort_nurse;
                is_change = true;
            }
            var receiving_nurse = request["Surgeon"]?.ToString();
            if (ssaic.Surgeon != receiving_nurse)
            {
                ssaic.Surgeon = receiving_nurse;
                is_change = true;
            }
            if (is_change)
            {
                unitOfWork.EIOSpongeSharpsAndInstrumentsCountsSheetRepository.Update(ssaic);
                unitOfWork.Commit();
            }
        }
        private void UpdateSpongeSharpsAndInstrumentsCountsSheetDatas(EIOSpongeSharpsAndInstrumentsCountsSheetData ssaic_data, string code, string value)
        {
            ssaic_data.Code = code;
            ssaic_data.Value = value;
            unitOfWork.EIOSpongeSharpsAndInstrumentsCountsSheetDataRepository.Update(ssaic_data);
        }
        private void CreateSpongeSharpsAndInstrumentsCountsSheetDatas(Guid ssaic_id, string code, string value)
        {
            EIOSpongeSharpsAndInstrumentsCountsSheetData ssaic_data = new EIOSpongeSharpsAndInstrumentsCountsSheetData
            {
                SpongeSharpsAndInstrumentsCountsSheetId = ssaic_id,
                Code = code,
                Value = value
            };
            unitOfWork.EIOSpongeSharpsAndInstrumentsCountsSheetDataRepository.Add(ssaic_data);
        }
    }
}
