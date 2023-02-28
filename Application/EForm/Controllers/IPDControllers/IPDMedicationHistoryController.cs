using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models.IPDModels;
using EForm.Models.PrescriptionModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Helper;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDMedicationHistoryController : BaseApiController
    {
        private readonly string visit_type = "IPD";
        private readonly string formCode = "IPDMEDHIS";
        private readonly string orderTypeMedicationHistory = "IPD-MedicationHistory";
        private readonly string orderTypeTPCN = "IPD-TPCN";
        [HttpGet]
        [Route("api/IPD/MedicationHistory/Info/{type}/{visitId}")]
        [Permission(Code = "MEDHIS2")]
        public IHttpActionResult GetInfo(Guid visitId, string type = "A03_120_120421_VE")
        {
            IPD visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var user = GetUser();
            dynamic IsHasUnlockPermission = null;
            if (user != null)
            {
                IsHasUnlockPermission = HasUnlockPermission(visitId, formCode, user.Username);
            }
            var dateOfAdmission = visit.AdmittedDate;
            
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
                ICD10MainDisease = iCD10MainDisease == "\"\"" ? "" : iCD10MainDisease,
                MainDisease = mainDisease == "\"\"" ? "" : mainDisease,
                IDC10IncludingDisease = iDC10IncludingDiseas == "\"\"" ? "" : iDC10IncludingDiseas,
                IncludingDisease = includingDisease == "\"\"" ? "" : includingDisease,
                CreateBy = createBy,
                UpdateAt = visit.IPDMedicalRecord?.IPDMedicalRecordPart2?.UpdatedAt
            };
            var allergyMasterData = unitOfWork.MasterDataRepository.AsQueryable().Where(x => x.Group == "IPDIAAUALLE").Select(x => new { x.ViName,x.EnName,x.Code,x.Order,x.Data,x.DataType }).ToList();
            var roomId = visit.Room;
            //var diseasehistory = visit.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas?.FirstOrDefault(x => x.Code == "IPDMRPTBATHANS")?.Value;
            var currentDrug = GetPatientMedication(visit.Id,"IPD-PatientMedicationList");
            IPDQueryModel edmodel = new IPDQueryModel();
            var visitAllergy = EMRVisitAllergy.GetIPDVisitAllergy(visit);
            return Content(HttpStatusCode.OK, new
            {
                DateOfAdmission = dateOfAdmission,
                Diagnostic = diagnostic,
                Allergy = new {
                VisitAllergy = visitAllergy,
                MasterData = allergyMasterData
                },
                RoomId = visit.Room,
                DiseaseHistory = GetPersonalHistory(visit),
                CurrentDrug = currentDrug,
                IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.PhieuKhaiThacTienSuDungThuoc),
                IsHasUnlockPermission
        });;
        }
        [HttpGet]
        [Route("api/IPD/MedicationHistory/{type}/{visitId}")]
        [Permission(Code = "MEDHIS2")]
        public IHttpActionResult GetMedication(Guid visitId, string type = "A03_120_120421_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var form = GetForm(visitId,type);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Constant.IPDFormCode.PhieuKhaiThacTienSuDungThuoc));            
            return Content(HttpStatusCode.OK, FormatOutput(visit, form));
        }
        // GET: MedicationHistory
        [HttpPost]
        [Route("api/IPD/MedicationHistory/Create/{type}/{visitId}")]
        [Permission(Code = "MEDHIS1")]
        public IHttpActionResult CreateMedicationHistory(Guid visitId, string type = "A03_120_120421_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, type);
            if (form != null)
                return Content(HttpStatusCode.BadRequest, Message.FORM_EXIST);

            var form_data = new IPDMedicationHistory
            {
                VisitId = visitId,
                FormCode = type,
                Version = 1
            };
            unitOfWork.IPDMedicationHistoryRepository.Add(form_data);

            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, new { 
                                                    Id=form_data.Id,
                                                    CreateBy= form_data?.CreatedBy,
                                                    CreateAt=form_data.CreatedAt
                                                  });

        }
        [HttpPost]
        [Route("api/IPD/MedicationHistory/Update/{type}/{visitId}")]
        [Permission(Code = "MEDHIS3")]
        public IHttpActionResult UpdateMedicationHistory(Guid visitId, [FromBody]JObject request, string type = "A03_120_120421_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, type);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Constant.IPDFormCode.PhieuKhaiThacTienSuDungThuoc));
            if(form.PharmacistConfirmId != null || form.DoctorConfirmId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            HandleUpdateAllergy(visit.Id, request["ALLERGY"]);
            HandleUpdateOrCreateOrder(visit.Id, orderTypeMedicationHistory, request["MedicationHistory"]);
            HandleUpdateOrCreateOrder(visit.Id, orderTypeTPCN, request["TPCN"]);
            HandleUpdateOrCreateFormDatas((Guid)form.VisitId, form.Id, formCode, request["Datas"]);            
            unitOfWork.IPDMedicationHistoryRepository.Update(form);

            UpdateVisit(visit, visit_type);

            return Content(HttpStatusCode.OK, new { Id=form.Id,UpdateBy=form?.UpdatedBy,UpdateAt=form.UpdatedAt });
        }

        [HttpPost]
        [Route("api/IPD/MedicationHistory/Confirm/{type}/{visitId}")]
        [Permission(Code = "MEDHIS4")]
        public IHttpActionResult ConfirmAPI(Guid visitId, [FromBody] JObject request, string type = "A03_120_120421_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, type);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, Constant.IPDFormCode.PhieuKhaiThacTienSuDungThuoc));            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString());
            if(successConfirm)
            {
                UpdateVisit(visit, visit_type);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {

                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }   
        }

        private IPDMedicationHistory GetForm(Guid visit_id,string formCode)
        {
            return unitOfWork.IPDMedicationHistoryRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.FormCode == formCode).FirstOrDefault();
        }
        private dynamic FormatOutput(IPD ipd,IPDMedicationHistory fprm)
        {
            var datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCode);
           
            var medicalRecordPart2s = ipd.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas;
            var medicalRecordPart3s = ipd.IPDMedicalRecord?.IPDMedicalRecordPart3?.IPDMedicalRecordPart3Datas;

            var iCD10MainDisease = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTICDCANS")?.Value;
            var mainDisease = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTCDBCANS")?.Value;
            var iDC10IncludingDiseas = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTICDPANS")?.Value;
            var includingDisease = medicalRecordPart2s?.FirstOrDefault(x => x.Code == "IPDMRPTCDKTANS")?.Value;
            var doctorConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.DoctorConfirmId);
            var pharmacistConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.PharmacistConfirmId);
            var diagnostic = new
            {
                ICD10MainDisease = iCD10MainDisease == "\"\"" ? "" : iCD10MainDisease,
                MainDisease = mainDisease == "\"\"" ? "" : mainDisease,
                IDC10IncludingDisease = iDC10IncludingDiseas == "\"\"" ? "" : iDC10IncludingDiseas,
                IncludingDisease = includingDisease == "\"\"" ? "" : includingDisease,
                CreateBy = ipd.IPDMedicalRecord?.IPDMedicalRecordPart2?.CreatedBy,
                UpdateAt = ipd.IPDMedicalRecord?.IPDMedicalRecordPart2?.UpdatedAt
            };
            return new
            {
                ID = fprm.Id,
                VisitId = fprm.VisitId,
                RoomId = datas.Where(c => c.Code == "IPDDRUGHIS12")?.FirstOrDefault()?.Value,
                Customer = new
                {

                    PID = ipd.Customer?.PID,
                    FullName = ipd.Customer?.Fullname,
                    DateOfBirth = ipd.Customer?.DateOfBirth,
                    Gender = ipd.Customer?.Gender,
                    DateOfAdmission = ipd.AdmittedDate,
                    Diagnostic = diagnostic,
                    Department = new {
                        ViName = ipd.Specialty.ViName,
                        Enname = ipd.Specialty.EnName
                    },
                    IsAllergy = ipd.Customer.IsAllergy,
                    Allergy = ipd.Customer.Allergy
                    

                },
                MedicationHistory = GetPatientMedication(ipd.Id, orderTypeMedicationHistory),
                TPCN = GetPatientMedication((Guid)ipd.Id, orderTypeTPCN),
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, formCode),
                CreatedBy=fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                UpdatedAt = fprm.UpdatedAt,
                DoctorConfirm = new
                {
                    UserName = doctorConfirm?.Username,
                    FullName = doctorConfirm?.Fullname

                },
                DoctorConfirmAt = fprm.DoctorConfirmAt,
                PharmacistConfirm = new
                {
                    UserName = pharmacistConfirm?.Username,
                    FullName = pharmacistConfirm?.Fullname
                },
                PhysicianConfirmAt = fprm.PharmacistConfirmAt,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuKhaiThacTienSuDungThuoc)
            };
        }

        protected dynamic GetPatientMedication(Guid visit_id, string type)
        {
            return unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType == type
            )
            .OrderBy(o => o.CreatedAt).Select(o => new
            {
                o.Id,
                o.Drug,
                o.Dosage,
                o.LastDoseDate,
                o.MedicationPlan,
                o.Note,
                o.Route,
              
            }).Distinct().ToList();
        }
        protected IPDSurgeryCertificate GetSurgeryCertificateByVisitId(Guid id)
        {
            return unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => e.VisitId == id);
        }
        protected bool ConfirmUser(IPDMedicationHistory ipdMedicationHistory,User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());            
            if (kind.ToUpper() == "DOCTOR" && positions.Contains("DOCTOR") && ipdMedicationHistory.DoctorConfirmId == null)
            {
                ipdMedicationHistory.DoctorConfirmAt = DateTime.Now;
                ipdMedicationHistory.DoctorConfirmId = user?.Id;
            }
            else if (kind.ToUpper() == "PHARMACIST" && positions.Contains("PHARMACIST") && ipdMedicationHistory.PharmacistConfirmId == null)
            { 
                ipdMedicationHistory.PharmacistConfirmAt = DateTime.Now;
                ipdMedicationHistory.PharmacistConfirmId = user?.Id;
            }
            else
                return false;
            unitOfWork.IPDMedicationHistoryRepository.Update(ipdMedicationHistory);
            unitOfWork.Commit();
            return true;
        }
        protected void HandleUpdateAllergy(Guid visit_id,JToken request_datas)
        {
            if (request_datas!=null)
            {
                var visit = GetIPD(visit_id);
                var etr = visit.IPDInitialAssessmentForAdult;
                if (etr != null)
                {
                    foreach(var item in request_datas)
                    {
                        var data = etr.IPDInitialAssessmentForAdultDatas?.FirstOrDefault(x => x.Code == item["Code"].ToString());
                        data.Value = item["Value"].ToString();
                        unitOfWork.IPDInitialAssessmentForAdultDataRepository.Update(data);
                        unitOfWork.Commit();
                    }
                }
            }
        }

    }
}