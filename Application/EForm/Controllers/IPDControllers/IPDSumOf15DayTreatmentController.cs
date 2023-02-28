using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Models.PrescriptionModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDSumOf15DayTreatmentController : BaseApiController
    {
        private readonly string formCode = "IPDSO15DT";
        private readonly string visit_type = "IPD";

        [HttpGet]
        [Route("api/IPD/SumOf15DayTreatments/Info/{ipdId}")]
        [Permission(Code = "SO15DT2")]
        public IHttpActionResult GetInfo(Guid ipdId)
        {
            IPD visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var medicalRecordPart2s = visit.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas;
            var medicalRecordPart3s = visit.IPDMedicalRecord?.IPDMedicalRecordPart3?.IPDMedicalRecordPart3Datas;
            
            var iCD10MainDisease = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTICDCANS")?.Value;
            var mainDisease = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTCDBCANS")?.Value;
            var iDC10IncludingDiseas = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTICDPANS")?.Value;
            var includingDisease = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTCDKTANS")?.Value;
            var objIPDMedicalRecordPart2 = visit.IPDMedicalRecord?.IPDMedicalRecordPart2;
            var createBy = String.IsNullOrEmpty(objIPDMedicalRecordPart2?.UpdatedBy) == true ? objIPDMedicalRecordPart2?.CreatedBy : objIPDMedicalRecordPart2?.UpdatedBy;
            var diagnostic = new
            {
                ICD10MainDisease = iCD10MainDisease == "\"\"" ? "":iCD10MainDisease,
                MainDisease = mainDisease == "\"\"" ? "": mainDisease,
                IDC10IncludingDisease = iDC10IncludingDiseas == "\"\"" ? "" : iDC10IncludingDiseas,
                IncludingDisease = includingDisease == "\"\"" ? "" : includingDisease,
                CreateBy = createBy,
                UpdateAt = visit.IPDMedicalRecord?.IPDMedicalRecordPart2?.UpdatedAt   
            };

            var clinicalEvolution = medicalRecordPart3s?.FirstOrDefault(x => x.Code == "IPDMRPEQTBLANS")?.Value;
            var resultsOfPrincipleTest = medicalRecordPart3s?.FirstOrDefault(x => x.Code == "IPDMRPETTKQANS")?.Value;
            var methodTreatment = GetTreatmentsLast(ipdId, (Guid)visit?.IPDMedicalRecord?.IPDMedicalRecordPart3?.Id, medicalRecordPart3s.ToList());
            var mainDrugsUsed = medicalRecordPart3s?.FirstOrDefault(x => x.Code == "IPDMRPETCDDANS")?.Value;
            var resultsOfTreatment = medicalRecordPart3s?.FirstOrDefault(x => x.Code == "IPDMRPETTNBANS")?.Value;
            var continuousTreatmentAndPrognosis = medicalRecordPart3s?.FirstOrDefault(x => x.Code == "IPDMRPEHDTVANS")?.Value;
            
          //  var referContinuousTreatment = medicalRecordPart3s?.FirstOrDefault(x => x.Code == "IPD MRP EHD TVANS")?.Value;
            var totalMedical = new
            {
                ClinicalEvolution = clinicalEvolution == "\"\"" ? "" : clinicalEvolution,
                ResultsOfPrincipleTest = resultsOfPrincipleTest == "\"\"" ? "" : resultsOfPrincipleTest,
                Treatment = new
                {
                    MethodTreatment = methodTreatment == "\"\"" ? "" : methodTreatment,
                    MainDrugsUsed = mainDrugsUsed == "\"\"" ? "" : mainDrugsUsed,
                },
                ResultsOfTreatment = resultsOfTreatment == "\"\"" ? "" : resultsOfTreatment,
                ContinuousTreatmentAndPrognosis = continuousTreatmentAndPrognosis == "\"\"" ? "" : continuousTreatmentAndPrognosis,
                TreatmentResult = GetTreatmentResult(visit)
                // ReferContinuousTreatment = referContinuousTreatment == "\"\"" ? "" : referContinuousTreatment,
            };
            return Content(HttpStatusCode.OK, new
            {
                RoomId = visit.Room,
                Diagnostic = diagnostic,
                TotalMedical = totalMedical,
                IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.PhieuSoKet15NgayDieuTri)
            });
        }

        [HttpGet]
        [Route("api/IPD/SumOf15DayTreatments/{ipdId}/{id}")]
        [Permission(Code = "SO15DT2")]
        public IHttpActionResult GetIPDSummaryOf15DayTreatment(Guid ipdId, Guid id)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var form = unitOfWork.IPDSummayOf15DayTreatmentRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND_WITH_LOCKED);           
            return Content(HttpStatusCode.OK, FormatOutput(visit, form));
        }
        
        [HttpGet]
        [Route("api/IPD/SumOf15DayTreatments/{ipdId}")]
        [Permission(Code = "SO15DT2")]
        public IHttpActionResult GetIPDSummaryOf15DayTreatment(Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var forms = unitOfWork.IPDSummayOf15DayTreatmentRepository.Find(e => !e.IsDeleted && e.VisitId == ipdId).OrderBy(o => o.Order).ToList().Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                CreatedAt = form.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                UpdatedAt = form.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
            }).ToList();

            bool IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.PhieuSoKet15NgayDieuTri);

            return Content(HttpStatusCode.OK, new
            {
                Datas = forms,
                IsLocked
            });
        }
       

        [HttpPost]
        [Route("api/IPD/SumOf15DayTreatments/Create/{ipdId}")]
        [Permission(Code = "SO15DT1")]
        public IHttpActionResult CreateSumOf15DayTreatment(Guid ipdId)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form_data = new IPDSummaryOf15DayTreatment()
            {
                VisitId = ipdId
            };
            unitOfWork.IPDSummayOf15DayTreatmentRepository.Add(form_data);
            UpdateVisit(visit, visit_type);
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }
        [HttpPost]
        [Route("api/IPD/SumOf15DayTreatments/Update/{ipdId}/{id}")]
        [Permission(Code = "SO15DT3")]
        public IHttpActionResult UpdateAPI(Guid ipdId,Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var form = GetForm(id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Constant.IPDFormCode.PhieuSoKet15NgayDieuTri));
            if (form.HeadOfDepartmentConfirmId != null || form.PhysicianConfirmId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formCode, request["Datas"]);
            unitOfWork.IPDSummayOf15DayTreatmentRepository.Update(form);

            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, new { 
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }
        
        [HttpPost]
        [Route("api/IPD/SumOf15DayTreatments/Confirm/{ipdId}/{id}")]
       [Permission(Code = "SO15DT4")]
        public IHttpActionResult ConfirmAPI(Guid ipdId, Guid id, [FromBody] JObject request)
        {
            var visit = GetVisit(ipdId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var form = GetForm(id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Constant.IPDFormCode.PhieuSoKet15NgayDieuTri));            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString());
            if (successConfirm)
            {
                UpdateVisit(visit, visit_type);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }


        private IPDSummaryOf15DayTreatment GetForm(Guid id)
        {
            return unitOfWork.IPDSummayOf15DayTreatmentRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
        }
        private dynamic FormatOutput(IPD ipd,IPDSummaryOf15DayTreatment fprm)
        {
            var datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCode);
            var iCD10MainDisease = datas.Where(c => c.Code == "IPDSO15DTR05")?.FirstOrDefault()?.Value;
            var mainDisease = datas.Where(c => c.Code == "IPDSO15DTR07")?.FirstOrDefault()?.Value;
            var iDC10IncludingDisease = datas.Where(c => c.Code == "IPDSO15DTR09")?.FirstOrDefault()?.Value;
            var includingDisease = datas.Where(c => c.Code == "IPDSO15DTR11")?.FirstOrDefault()?.Value;
            var headOfDepartment = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.HeadOfDepartmentConfirmId);
            var physicianConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.PhysicianConfirmId);

            return new
            {
                Id = fprm.Id,
                VisitId = fprm.VisitId,
                RoomId = datas.Where(c => c.Code == "IPDSO15DTR02")?.FirstOrDefault()?.Value,
                Customer = new {
                    PID = ipd.Customer?.PID,
                    FullName = ipd.Customer?.Fullname,
                    DateOfBirth = ipd.Customer?.DateOfBirth,
                    Gender = ipd.Customer?.Gender
                        
                },
                Department = new
                {
                    ViName = ipd.Specialty?.ViName,
                    EnName = ipd.Specialty?.EnName
                },
                Diagnosis = new {
                    ICD10MainDisease = iCD10MainDisease == "\"\"" ? "" : iCD10MainDisease,
                    MainDisease = mainDisease == "\"\"" ? "" : mainDisease,
                    IDC10IncludingDisease = iDC10IncludingDisease == "\"\"" ? "" : iDC10IncludingDisease,
                    IncludingDisease = includingDisease == "\"\"" ? "" : includingDisease,
                },
                Datas = datas,
                CreatedBy = fprm.CreatedBy,
                UpdateBy = fprm.UpdatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfDepartmentConfirm = new { 
                    UserName = headOfDepartment?.Username,
                    FullName = headOfDepartment?.Fullname

                } ,
                HeadOfDepartmentConfirmAt = fprm.HeadOfDepartmentConfirmAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                PhysicianConfirm = new {
                    UserName = physicianConfirm?.Username,
                    FullName = physicianConfirm?.Fullname
                },
                PhysicianConfirmAt = fprm.PhysicianConfirmAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                UpdatedAt = fprm.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuSoKet15NgayDieuTri, fprm.Id)
                
            };
        }
        private bool ConfirmUser(IPDSummaryOf15DayTreatment ipdSummaryOf15DayTreatment, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());            
            if (kind.ToUpper() == "HEADOFDEPARTMENT" && positions.Contains("HEAD OF DEPARTMENT") && ipdSummaryOf15DayTreatment.HeadOfDepartmentConfirmId == null)
            {
                ipdSummaryOf15DayTreatment.HeadOfDepartmentConfirmAt = DateTime.Now;
                ipdSummaryOf15DayTreatment.HeadOfDepartmentConfirmId = user?.Id;
            }
            else if (kind.ToUpper() == "DOCTOR" && positions.Contains("DOCTOR") && ipdSummaryOf15DayTreatment.PhysicianConfirmId == null)
            {
                ipdSummaryOf15DayTreatment.PhysicianConfirmAt = DateTime.Now;
                ipdSummaryOf15DayTreatment.PhysicianConfirmId = user?.Id;
            }
            else
                return false;
            unitOfWork.IPDSummayOf15DayTreatmentRepository.Update(ipdSummaryOf15DayTreatment);
            unitOfWork.Commit();
            return true;
        }

        private string GetTreatmentResult(IPD ipd)
        {
            if (ipd == null)
                return null;

            var part3_id = ipd.IPDMedicalRecord?.IPDMedicalRecordPart3Id;
            if (part3_id == null)
                return null;

            string[] codes = new string[]
            {
                "IPDMRPE1057", "IPDMRPE1060", "IPDMRPE1062"
            };

            var data_part3 = (from d in unitOfWork.IPDMedicalRecordPart3DataRepository.AsQueryable()
                              where !d.IsDeleted && d.IPDMedicalRecordPart3Id == part3_id && codes.Contains(d.Code)
                              select d).ToList();
            if (data_part3.Count == 0)
                return null;

            var data1 = data_part3.FirstOrDefault(d => d.Code == "IPDMRPE1057");
            string str_data = "";
            if (data1 != null && !string.IsNullOrEmpty(data1.Value))
                str_data += $"- Nội khoa: {data1.Value}";

            var surgeryTable = (from table in unitOfWork.EIOFormRepository.AsQueryable()
                                where !table.IsDeleted && table.VisitId == ipd.Id && table.FormId == part3_id
                                && table.FormCode == "IPDMATTBL1"
                                select table).OrderBy(c => c.CreatedAt).ToList();
            if(surgeryTable.Count > 0)
            {
                string str = $"\n- Phẫu thuật:";
                foreach (var item in surgeryTable)
                {
                    str += $"\n\t";
                    var datas = (from d in unitOfWork.FormDatasRepository.AsQueryable()
                                 where !d.IsDeleted && d.FormId == item.Id && d.FormCode == item.FormCode
                                 select d).OrderBy(c => c.CreatedAt).ToList();
                    string[] codes_data = new string[] { "IPDMATTBL2", "IPDMATTBL4", "IPDMATTBL3" };

                    for(int i = 0; i < codes_data.Length; i++)
                    {
                        var data = datas.FirstOrDefault(d => d.Code == codes_data[i]);
                        if (i == 0)
                            str += $"+ {GetvalueFromCode(data)} ";
                        else
                            str += $"- {GetvalueFromCode(data)} ";
                    }    
                }
                str_data += str;
            } 

            var data2 = data_part3.FirstOrDefault(d => d.Code == "IPDMRPE1060");
            if (data2 != null && !string.IsNullOrEmpty(data2.Value))
                str_data += $"\n- Kết quả: {data2.Value} ";

            var data3 = data_part3.FirstOrDefault(d => d.Code == "IPDMRPE1062");
            if (data3 != null && !string.IsNullOrEmpty(data3.Value))
                str_data += $"\n- Biến chứng/ di chứng: {data3.Value} ";

            //var data4 = data_part3.FirstOrDefault(d => d.Code == "IPDMRPEHDTVANS");
            //if (data4 != null && !string.IsNullOrEmpty(data4.Value))
            //    str_data += $"\n- Hướng điều trị tiếp: {data4.Value} ";

            return str_data;
        }

        private string GetvalueFromCode(FormDatas data)
        {
            if (data == null)
                return " ";
            if (string.IsNullOrEmpty(data.Value))
                return " ";

            return data.Value;
        }

    }

}