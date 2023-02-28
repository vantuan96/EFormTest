using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Models;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class EIORequestNoCardiopulmonaryResuscitationController : BaseApiController
    {
        protected const string formCode = "A01_032_050919_VE";
        private readonly string[] codeDatasForm = { "IsCustormer", "NameOfFamily", "DateTimeOfPatient", "DateTimeOfPhysician", "Picture", "Picture1" };
        protected void CreateForm(Guid visitId, string visitTypeCode)
        {
            
            EIOForm new_form = new EIOForm()
            {
                VisitId = visitId,
                VisitTypeGroupCode = visitTypeCode,
                FormCode = formCode
            };
            unitOfWork.EIOFormRepository.Add(new_form);
            unitOfWork.Commit();
        }

        protected EIOForm GetFormByVisitId(Guid visitId, string visitTypeCode)
        {
            var form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.VisitTypeGroupCode == visitTypeCode && e.FormCode == formCode);
            return form;
        }

        protected void CreateOrUpdateDatasForm(EIOForm form, JToken req)
        {
            var datas = GetListDatasForm(form);
            foreach (var item in codeDatasForm)
            {
                var data = datas.FirstOrDefault(e => e.Code.ToUpper() == item.ToUpper());
                if (data == null)
                {
                    FormDatas new_obj = new FormDatas()
                    {
                        VisitId = form.VisitId,
                        FormId = form.Id,
                        VisitType = form.VisitTypeGroupCode,
                        FormCode = form.FormCode,
                        Code = item.ToUpper(),
                        Value = req[item]?.ToString()
                    };
                    unitOfWork.FormDatasRepository.Add(new_obj);
                }
                else
                {
                    data.Value = req[item]?.ToString();
                    unitOfWork.FormDatasRepository.Update(data);
                }
            }
            unitOfWork.EIOFormRepository.Update(form);
            unitOfWork.Commit();
        }

        protected List<FormDatas> GetListDatasForm(EIOForm form)
        {
            var datas = (from d in unitOfWork.FormDatasRepository.AsQueryable()
                         where !d.IsDeleted && d.FormId == form.Id
                         && d.FormCode == form.FormCode
                         select d).ToList();
            return datas;
        }

        protected string GetAndFormatDiagnosis(Guid visitId, string visitType, ED ed = null)
        {
            StringBuilder builder = new StringBuilder();
            DiagnosisAndICDModel getcd = new DiagnosisAndICDModel();
            if (visitType == "ED" && ed != null)
                getcd = GetVisitDiagnosisAndICD(ed);
            else
                getcd = GetVisitDiagnosisAndICD(visitId, visitType, false);

            builder.Append($"{(string.IsNullOrEmpty(getcd.Diagnosis) ? "" : getcd.Diagnosis)}");
            builder.Append($"{(string.IsNullOrEmpty(getcd.DiagnosisOption) ? "" : (string.IsNullOrEmpty(getcd.Diagnosis) ? getcd.DiagnosisOption : "/ " + getcd.DiagnosisOption))}");

            string[] array_Icd = { getcd.ICD, getcd.ICDOption };
            builder.Append(GetAndFormatICD10(array_Icd));

            return builder.ToString();
        }

        private DiagnosisAndICDModel GetVisitDiagnosisAndICD(ED edVisit)
        {
            if(edVisit != null)
            {
                var mergencyRecordDatas = edVisit.EmergencyRecord.EmergencyRecordDatas;
                return new DiagnosisAndICDModel
                {
                    ICD = mergencyRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0ICD102")?.Value,
                    Diagnosis = mergencyRecordDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0ID0ANS")?.Value
                };
            }
            return new DiagnosisAndICDModel();
        }

        private string GetAndFormatICD10(string[] texts)
        {
            string result = String.Empty;

            foreach (var text in texts)
            {
                string str_text = text;
                if (text == null || text == $"\"\"")
                    str_text = "";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(str_text);
                string _str = String.Empty;
                if (objs != null)
                {
                    int lengthOfobjs = objs.Count;
                    for (int j = 0; j < lengthOfobjs; j++)
                    {
                        var codeIcd10 = objs[j]["code"]?.ToString();
                        if (j == 0)
                            _str += codeIcd10;
                        else
                            _str += $", {codeIcd10}";
                    }

                    if (!string.IsNullOrEmpty(result))
                        result += "/ " + _str;
                    else
                        result += _str;
                }
            }

            if (string.IsNullOrEmpty(result))
                return "";

            string format = $" ({result})";
            return format;
        }
    }
}
