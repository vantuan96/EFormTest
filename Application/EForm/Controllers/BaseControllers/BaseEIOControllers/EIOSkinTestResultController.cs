using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOSkinTestResultController : BaseApiController
    {
        protected EIOSkinTestResult GetEIOSkinTestResult(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }


        protected dynamic GetSkinTestResultDetail(EIOSkinTestResult skin_test_result)
        {
            return new
            {
                skin_test_result.Id,
                skin_test_result.Conclusion,
                ConfirmDate = skin_test_result.ConfirmDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                skin_test_result.ConfirmDoctor?.Username,
                Datas = skin_test_result.EIOSkinTestResultDatas.Where(e => !e.IsDeleted).OrderBy(e => e.CreatedAt).Select(e => new {
                    e.Id,
                    e.Drug,
                    e.SkinDilutionConcentration,
                    e.SkinResult,
                    e.SkinPositive,
                    e.SkinNegative,
                    e.EndodermDilutionConcentration,
                    e.EndodermResult,
                    e.EndodermNegative,
                    e.CreatedBy,
                })
            };
        }


        protected EIOSkinTestResult CreateEIOSkinTestResult(Guid visit_id, string visit_type)
        {
            var skin_test = new EIOSkinTestResult()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type
            };
            unitOfWork.EIOSkinTestResultRepository.Add(skin_test);
            unitOfWork.Commit();
            return skin_test;
        }


        protected void UpdateEIOSkinTestResult(EIOSkinTestResult skin_test_result, JObject request)
        {
            string conclusion = request["Conclusion"]?.ToString();
            if (conclusion != skin_test_result.Conclusion)
            {
                skin_test_result.Conclusion = conclusion;
                unitOfWork.EIOSkinTestResultRepository.Update(skin_test_result);
            }

            foreach (var item in request["Datas"])
            {
                var str_id = item.Value<string>("Id");
                var data = GetOrCreateData(item.Value<string>("Id"), skin_test_result.Id);
                if (data != null)
                    UpdateData(data, item);
            }
            unitOfWork.Commit();
        }
        private EIOSkinTestResultData GetOrCreateData(string str_id, Guid skin_test_id)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                var data = new EIOSkinTestResultData()
                {
                    EDSkinTestResultId = skin_test_id
                };
                unitOfWork.EIOSkinTestResultDataRepository.Add(data);
                return data;
            }

            Guid data_id = new Guid(str_id);
            return unitOfWork.EIOSkinTestResultDataRepository.GetById(data_id);
        }
        private void UpdateData(EIOSkinTestResultData data, JToken item)
        {
            data.Drug = item.Value<string>("Drug");
            data.SkinDilutionConcentration = item.Value<string>("SkinDilutionConcentration");
            data.SkinResult = item.Value<string>("SkinResult");
            data.SkinPositive = item.Value<string>("SkinPositive");
            data.SkinNegative = item.Value<string>("SkinNegative");
            data.EndodermDilutionConcentration = item.Value<string>("EndodermDilutionConcentration");
            data.EndodermResult = item.Value<string>("EndodermResult");
            data.EndodermNegative = item.Value<string>("EndodermNegative");
            unitOfWork.EIOSkinTestResultDataRepository.Update(data);
        }


        protected void AcceptSkinTestResult(EIOSkinTestResult skin_test_result, Guid user_id)
        {
            skin_test_result.ConfirmDoctorId = user_id;
            skin_test_result.ConfirmDate = DateTime.Now;
            unitOfWork.EIOSkinTestResultRepository.Update(skin_test_result);
            unitOfWork.Commit();
        }


        protected void DeleteSkinTestResult(EIOSkinTestResult skin_test_result)
        {
            unitOfWork.EIOSkinTestResultRepository.Delete(skin_test_result);
            unitOfWork.Commit();
        }
    }
}