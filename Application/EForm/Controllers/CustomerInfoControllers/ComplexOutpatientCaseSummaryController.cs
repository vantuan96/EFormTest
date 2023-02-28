using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.CustomerInfoControllers
{
    [SessionAuthorize]
    public class ComplexOutpatientCaseSummaryController : BaseApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/Customer/ComplexOutpatientCaseSummary/{id}")]
        [Permission(Code = "CCOCS1")]
        public IHttpActionResult CreateComplexOutpatientCaseSummaryAPI(Guid id, [FromBody]ComplexOutpatientCaseSummaryParameterModel request)
        {
            var customer = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            if (!customer.IsChronic)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_IS_NOT_CHRONIC);

            var user = GetUser();
            var visit = GetVisit(customer.Id, request.VisitId, request.VisitTypeGroupCode);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            //if (IsBlockAfter24h(visit.CreatedAt) && !HasUnlockPermission(visit.Id, new string[] { "EDCOCS", "OPDCOCS" }, user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (request.Id == null)
            {
                if (IsExistComplex(customer, request.VisitId))
                    return Content(HttpStatusCode.BadRequest, Message.COCS_IS_EXIST);

                if (!IsCorrectDoctor(user.Username, (Guid)request.VisitId, request.VisitTypeGroupCode))
                    return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

                CreateComplexOutpatientCaseSummary(user.Id, customer.Id, request);
            }
            else
            {
                var updated_complex = UpdateComplexOutpatientCaseSummary(request, user);
                if (updated_complex == null)
                    return Content(HttpStatusCode.BadRequest, Message.COCS_NOT_FOUND);
            }

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/Customer/ComplexOutpatientCaseSummary/Delete/{id}")]
        [Permission(Code = "CCOCS2")]
        public IHttpActionResult DeleteComplexOutpatientCaseSummaryAPI(Guid id)
        {
            var complex = unitOfWork.ComplexOutpatientCaseSummaryRepository.GetById(id);
            if (complex == null)
                return Content(HttpStatusCode.BadRequest, Message.COCS_NOT_FOUND);
            var user = GetUser();
            CreateConfirmNotification(user, complex);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [HttpGet]
        [Route("api/Customer/ComplexOutpatientCaseSummary/{id}")]
        [Permission(Code = "CCOCS3")]
        public IHttpActionResult GetComplexOutpatientCaseSummaryAPI(Guid id)
        {
            var customer = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            if (!customer.IsChronic)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_IS_NOT_CHRONIC);

            var results = customer.ComplexOutpatientCaseSummarys.Where(e => !e.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .Select(e=> new { 
                    e.Id, 
                    e.MainDiseases, 
                    e.Orders, 
                    e.PrimaryDoctor?.Fullname, 
                    e.PrimaryDoctor?.Username,
                    CreateAt = e.CreatedAt?.ToString(Constant.DATE_FORMAT),
                    e.VisitId,
                    e.VisitTypeGroupCode,
                });
            return Content(HttpStatusCode.OK, new
            {
                HistoryOfAllergies = GetHistoryOfAllergies(customer.Id),
                Allergies = GetAllergic(customer.Id),
                ICDVisits = GetICDAndDiagnosis(customer.Id),
                Datas = results
            }); ;
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/Customer/ComplexOutpatientCaseSummary/Sync/{id}")]
        [Permission(Code = "CCOCS4")]
        public IHttpActionResult SyncComplexOutpatientCaseSummaryAPI(Guid id, [FromBody]SyncComplexOutpatientCaseSummaryParameterModel request)
        {
            var customer = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            if (!customer.IsChronic)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_IS_NOT_CHRONIC);

            var visit = GetVisit(customer.Id, request.VisitId, request.VisitTypeGroupCode);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                MainDiseases = GetMainDisease(request.VisitId, request.VisitTypeGroupCode),
                Orders = GetOrder(request.VisitId, request.VisitTypeGroupCode),
            });
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/Customer/ComplexOutpatientCaseSummary/Update/{id}")]
        [Permission(Code = "CCOCS5")]
        public IHttpActionResult UpdateComplexOutpatientCaseSummaryAPI(Guid id, [FromBody]ComplexOutpatientCaseSummaryParameterModel request)
        {
            var customer = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            if (!customer.IsChronic)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_IS_NOT_CHRONIC);

            var user = GetUser();
            var visit = GetVisit(customer.Id, request.VisitId, request.VisitTypeGroupCode);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);

            if (request.Id != null)
            {
                var updated_complex = UpdateComplexOutpatientCaseSummary(request);
                if (updated_complex == null)
                    return Content(HttpStatusCode.BadRequest, Message.COCS_NOT_FOUND);
            }
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
        

        private ComplexOutpatientCaseSummary CreateComplexOutpatientCaseSummary(Guid primary_doctor_id, Guid customer_id, ComplexOutpatientCaseSummaryParameterModel request)
        {

            ComplexOutpatientCaseSummary complex = new ComplexOutpatientCaseSummary { 
                CustomerId = customer_id,
                VisitId = request.VisitId,
                VisitTypeGroupCode = request.VisitTypeGroupCode,
                MainDiseases = request.MainDiseases,
                Orders = request.Orders,
                PrimaryDoctorId = primary_doctor_id
            };
            unitOfWork.ComplexOutpatientCaseSummaryRepository.Add(complex);
            unitOfWork.Commit();
            return complex;
        }
        private ComplexOutpatientCaseSummary UpdateComplexOutpatientCaseSummary(ComplexOutpatientCaseSummaryParameterModel request)
        {
            var complex = unitOfWork.ComplexOutpatientCaseSummaryRepository.GetById((Guid)request.Id);
            if (complex != null && (complex.MainDiseases != request.MainDiseases || complex.Orders != request.Orders))
            {
                complex.MainDiseases = request.MainDiseases;
                complex.Orders = request.Orders;
                unitOfWork.ComplexOutpatientCaseSummaryRepository.Update(complex);
                unitOfWork.Commit();
            }
            return complex;
        }
        private ComplexOutpatientCaseSummary UpdateComplexOutpatientCaseSummary(ComplexOutpatientCaseSummaryParameterModel request, User user)
        {
            var complex = unitOfWork.ComplexOutpatientCaseSummaryRepository.GetById((Guid)request.Id);
            if (complex != null && complex.CreatedBy == user.Username && (complex.MainDiseases != request.MainDiseases || complex.Orders != request.Orders))
            {
                complex.MainDiseases = request.MainDiseases;
                complex.Orders = request.Orders;
                unitOfWork.ComplexOutpatientCaseSummaryRepository.Update(complex);
                unitOfWork.Commit();
            }
            return complex;
        }

        private dynamic GetVisit(Guid customer_id, Guid? visit_id, string visit_type_group_code)
        {
            if (visit_type_group_code.Equals("ED"))
            {
                var ed = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.CustomerId != null && e.CustomerId == customer_id && e.Id == visit_id);
                if (ed != null) return ed;
            }
            else if (visit_type_group_code.Equals("OPD"))
            {
                var opd = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.CustomerId != null && e.CustomerId == customer_id && e.Id == visit_id);
                if (opd != null) return opd;
            }
            else if (visit_type_group_code.Equals("EOC"))
            {
                var opd = unitOfWork.EOCRepository.FirstOrDefault(e => !e.IsDeleted && e.CustomerId != null && e.CustomerId == customer_id && e.Id == visit_id);
                if (opd != null) return opd;
            }
            return null;
        }

        private bool IsCorrectDoctor(string username, Guid visit_id, string visit_type_group_code)
        {
            string primary_doctor = string.Empty;
            try
            {
                if (visit_type_group_code.Equals("ED"))
                {
                    var ed = unitOfWork.EDRepository.GetById(visit_id);
                    //primary_doctor = ed.PrimaryDoctor.Username;
                    primary_doctor = ed.DischargeInformation.UpdatedBy;
                }
                else if (visit_type_group_code.Equals("OPD"))
                {
                    var opd = unitOfWork.OPDRepository.GetById(visit_id);
                    primary_doctor = opd.PrimaryDoctor.Username;
                }
                else if (visit_type_group_code.Equals("EOC"))
                {
                    var opd = unitOfWork.EOCRepository.GetById(visit_id);
                    primary_doctor = opd.PrimaryDoctor.Username;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return username.Equals(primary_doctor);
        }

        private bool IsExistComplex(Customer customer, Guid? visit_id)
        {
            var old_complex = customer.ComplexOutpatientCaseSummarys.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id
            );
            if (old_complex != null) return true;
            return false;
        }

        private dynamic GetHistoryOfAllergies(Guid customer_id)
        {
            var examination_notes = GetExaminationNotesOfCustomerForAllergy(customer_id);
            var eoc_examination_notes = GetEOCExaminationNotesOfCustomerForAllergy(customer_id);
            
            var history_of_allergies = new List<dynamic>();

            var opd_history_of_allergies =  GetHistoryOfAllergiesInExaminationNote(examination_notes);
            history_of_allergies.AddRange(opd_history_of_allergies);
            
            var eoc_history_of_allergies =  GetEOCHistoryOfAllergiesInExaminationNote(eoc_examination_notes);
            history_of_allergies.AddRange(eoc_history_of_allergies);
            
            return history_of_allergies;
        }
        private dynamic GetExaminationNotesOfCustomerForAllergy(Guid customer_id)
        {
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.OPDOutpatientExaminationNoteId != null
            ).OrderByDescending(e => e.OPDOutpatientExaminationNote.ExaminationTime)
            .Select(e => new {
                e.OPDOutpatientExaminationNoteId,
                ExaminationTime = e.OPDOutpatientExaminationNote.ExaminationTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.Specialty,
                e.PrimaryDoctor,
            }).ToList();
        }
        private dynamic GetEOCExaminationNotesOfCustomerForAllergy(Guid customer_id)
        {
            return unitOfWork.EOCOutpatientExaminationNoteRepository.Find(
                e => !e.IsDeleted &&
                e.Visit.CustomerId != null &&
                e.Visit.CustomerId == customer_id
            ).OrderByDescending(e => e.ExaminationTime)
            .Select(e => new {
                e.Id,
                ExaminationTime = e.ExaminationTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.Visit.Specialty,
                e.Visit.PrimaryDoctor,
                e.VisitId
            }).ToList();
        }
        private dynamic GetEOCHistoryOfAllergiesInExaminationNote(dynamic oens)
        {
            var history_of_allergies = new List<dynamic>();
            foreach (var item in oens)
            {
                Guid oen_id = item.Id;
                var history_of_allergies_value = unitOfWork.FormDatasRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.FormId == oen_id &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals("OPDOENHOAANS")
                )?.Value;

                if (!string.IsNullOrEmpty(history_of_allergies_value))
                    history_of_allergies.Add(new
                    {
                        item.ExaminationTime,
                        item.Specialty?.ViName,
                        item.Specialty?.EnName,
                        item.PrimaryDoctor?.Fullname,
                        item.PrimaryDoctor?.Username,
                        HistoryOfAllergies = history_of_allergies_value,
                    });
            }
            return history_of_allergies;
        }
        private dynamic GetHistoryOfAllergiesInExaminationNote(dynamic oens)
        {
            var history_of_allergies = new List<dynamic>();
            foreach (var item in oens)
            {
                Guid oen_id = item.OPDOutpatientExaminationNoteId;
                var history_of_allergies_value = unitOfWork.OPDOutpatientExaminationNoteDataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.OPDOutpatientExaminationNoteId != null &&
                    e.OPDOutpatientExaminationNoteId == oen_id &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals("OPDOENHOAANS")
                )?.Value;

                if (!string.IsNullOrEmpty(history_of_allergies_value))
                    history_of_allergies.Add(new
                    {
                        item.ExaminationTime,
                        item.Specialty?.ViName,
                        item.Specialty?.EnName,
                        item.PrimaryDoctor?.Fullname,
                        item.PrimaryDoctor?.Username,
                        HistoryOfAllergies = history_of_allergies_value,
                    });
            }
            return history_of_allergies;
        }

        private List<string> GetAllergic(Guid customer_id)
        {
            var allergies = new List<string>();

            var allergies_in_ED = GetAllergiesInED(customer_id);
            allergies.AddRange(allergies_in_ED);

            var allergies_in_OPD = GetAllergiesInOPD(customer_id);
            allergies.AddRange(allergies_in_OPD);

            var allergies_in_EOC = GetAllergiesInEOC(customer_id);
            allergies.AddRange(allergies_in_EOC);

            return allergies.Distinct().ToList();
        }
        private List<string> GetAllergiesInED(Guid customer_id)
        {
            var allergies = new List<string>();
            var etrs = GetEmergencyTriageRecordOfCustomer(customer_id);

            foreach (var etr in etrs)
            {
                var all = etr.EmergencyTriageRecordDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("ETRALLANS")
                )?.Value;
                if (!string.IsNullOrEmpty(all))
                {
                    allergies.Add(all);
                    continue;
                }

                var koa = etr.EmergencyTriageRecordDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("ETRALLKOA")
                )?.Value;
                if (!string.IsNullOrEmpty(koa))
                {
                    var koa_value = "";
                    foreach (var i in koa.Split(','))
                        koa_value += $"{Constant.KIND_OF_ALLERGIC[i]}, ";
                    allergies.Add(koa_value.Substring(0, koa_value.Length - 2));
                }
            }

            var afrs = GetAssessmentForRetailServicePatient(customer_id);
            foreach (var af in afrs)
            {
                var all = af.EIOAssessmentForRetailServicePatientDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("EDAFRSPALLANS")
                )?.Value;
                if (!string.IsNullOrEmpty(all))
                {
                    allergies.Add(all);
                    continue;
                }

                var koa = af.EIOAssessmentForRetailServicePatientDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("EDAFRSPALLKOA")
                )?.Value;
                if (!string.IsNullOrEmpty(koa))
                {
                    var koa_value = "";
                    foreach (var i in koa.Split(','))
                        koa_value += $"{Constant.KIND_OF_ALLERGIC[i]}, ";
                    allergies.Add(koa_value.Substring(0, koa_value.Length - 2));
                }
            }
            return allergies;
        }
        private List<string> GetAllergiesInOPD(Guid customer_id)
        {
            var allergies = new List<string>();

            var iafsts = GetOPDInitialAssessmentForShortTermOfCustomer(customer_id);
            foreach (var ia in iafsts)
            {
                var all = ia.OPDInitialAssessmentForShortTermDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("OPDIAFSTOPALLANS")
                )?.Value;
                if (!string.IsNullOrEmpty(all))
                {
                    allergies.Add(all);
                    continue;
                }

                var koa = ia.OPDInitialAssessmentForShortTermDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("OPDIAFSTOPALLKOA")
                )?.Value;
                if (!string.IsNullOrEmpty(koa))
                {
                    var koa_value = "";
                    foreach (var i in koa.Split(','))
                        koa_value += $"{Constant.KIND_OF_ALLERGIC[i]}, ";
                    allergies.Add(koa_value.Substring(0, koa_value.Length - 2));
                }
            }

            var iafths = GetOPDInitialAssessmentForTelehealthOfCustomer(customer_id);
            foreach (var ia in iafths)
            {
                var all = ia.OPDInitialAssessmentForTelehealthDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("OPDIAFTPALLANS")
                )?.Value;
                if (!string.IsNullOrEmpty(all))
                {
                    allergies.Add(all);
                    continue;
                }

                var koa = ia.OPDInitialAssessmentForTelehealthDatas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("OPDIAFTPALLKOA")
                )?.Value;
                if (!string.IsNullOrEmpty(koa))
                {
                    var koa_value = "";
                    foreach (var i in koa.Split(','))
                        koa_value += $"{Constant.KIND_OF_ALLERGIC[i]}, ";
                    allergies.Add(koa_value.Substring(0, koa_value.Length - 2));
                }
            }

            return allergies;
        }
        private List<string> GetAllergiesInEOC(Guid customer_id)
        {
            var allergies = new List<string>();
            var oens = GetEOCExaminationNotesOfCustomerForAllergy(customer_id);
            foreach (var item in oens)
            {
                var visit_id = (Guid)item.VisitId;
                var all = unitOfWork.FormDatasRepository.FirstOrDefault(
                     e => !e.IsDeleted &&
                     e.FormCode == "OPDIAFSTOP" &&
                     !string.IsNullOrEmpty(e.Code) &&
                     e.Code.Contains("OPDIAFSTOPALLANS") &&
                     e.VisitId == visit_id
                 )?.Value;
                if (!string.IsNullOrEmpty(all))
                {
                    allergies.Add(all);
                    continue;
                }

                var koa = unitOfWork.FormDatasRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.FormCode == "OPDIAFSTOP" &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Contains("OPDIAFSTOPALLKOA") &&
                    e.VisitId == visit_id
                )?.Value;
                if (!string.IsNullOrEmpty(koa))
                {
                    var koa_value = "";
                    foreach (var i in koa.Split(','))
                        koa_value += $"{Constant.KIND_OF_ALLERGIC[i]}, ";
                    allergies.Add(koa_value.Substring(0, koa_value.Length - 2));
                }
            }

            return allergies;
        }
        private List<EDEmergencyTriageRecord> GetEmergencyTriageRecordOfCustomer(Guid customer_id)
        {
            return unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.EmergencyTriageRecordId != null
            ).Select(e => e.EmergencyTriageRecord).ToList();
        }
        private List<EIOAssessmentForRetailServicePatient> GetAssessmentForRetailServicePatient(Guid customer_id)
        {
            return unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.IsRetailService &&
                e.EDAssessmentForRetailServicePatient != null
            ).Select(e => e.EDAssessmentForRetailServicePatient).ToList();
        }
        private List<OPDInitialAssessmentForShortTerm> GetOPDInitialAssessmentForShortTermOfCustomer(Guid customer_id)
        {
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.OPDInitialAssessmentForShortTermId != null
            ).Select(e => e.OPDInitialAssessmentForShortTerm).ToList();
        }
        private List<OPDInitialAssessmentForTelehealth> GetOPDInitialAssessmentForTelehealthOfCustomer(Guid customer_id)
        {
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.IsTelehealth &&
                e.OPDInitialAssessmentForTelehealthId != null
            ).Select(e => e.OPDInitialAssessmentForTelehealth).ToList();
        }

        private List<OrderModel> GetOrder(Guid? visit_id, string visit_type_group_code)
        {
            string order_type;
            if (visit_type_group_code.Equals("ED"))
                order_type = Constant.ED_ORDER;
            else if (visit_type_group_code.Equals("OPD"))
                order_type = Constant.OPD_STANDING_ORDER;
            else if (visit_type_group_code.Equals("EOC"))
                order_type = Constant.EOC_STANDING_ORDER;
            else
                return new List<OrderModel>();
            return GetOrderByVisitId(visit_id, order_type);
        }
        private List<OrderModel> GetOrderByVisitId(Guid? visit_id, string order_type)
        {
            var results = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(order_type)
            )
            .OrderBy(o => o.CreatedAt)
            .Select(o => new OrderModel {
                Id = o.Id,
                Drug = o.Drug,
                Dosage = o.Dosage,
                Route = o.Route,
                UsedAt = o.UsedAt?.ToString(Constant.DATE_FORMAT),
            }).ToList();
            return results;
        }

        private string GetMainDisease(Guid? visit_id, string visit_type_group_code)
        {
            if (visit_type_group_code.Equals("ED"))
                return GetEDMainDisease(visit_id);
            else if (visit_type_group_code.Equals("OPD"))
                return GetOPDMainDisease(visit_id);
            else if (visit_type_group_code.Equals("EOC"))
                return GetEOCMainDisease(visit_id);
            else
                return string.Empty;
        }
        private string GetEDMainDisease(Guid? visit_id)
        {
            var disease = string.Empty;
            try
            {
                var ed = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == visit_id);
                var discharge_info = ed.DischargeInformation;
                var discharge_info_datas = discharge_info.DischargeInformationDatas;
                var diagnosis = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
                var list_icd = new List<dynamic>();
                var icd_raw = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAICD")?.Value;
                list_icd.AddRange(ICDConvert.Operate(icd_raw));
                var icd_option_raw = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAOPT")?.Value;
                list_icd.AddRange(ICDConvert.Operate(icd_option_raw));
                var icd = string.Join(", ", list_icd.Select(e => e.Code));
                if (string.IsNullOrEmpty(icd)) 
                    disease = diagnosis;
                else 
                    disease = $"{diagnosis} ({icd})";
            }
            catch (Exception) {}
            return disease;
        }
        private string GetOPDMainDisease(Guid? visit_id)
        {
            var disease = string.Empty;
            try
            {
                var opd = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == visit_id);
                var oen = opd.OPDOutpatientExaminationNote;
                var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
                var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
                var list_icd = new List<dynamic>();
                var icd_raw = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENICDANS")?.Value;
                list_icd.AddRange(ICDConvert.Operate(icd_raw));
                var icd_option_raw = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENICDOPT")?.Value;
                list_icd.AddRange(ICDConvert.Operate(icd_option_raw));
                var icd = string.Join(", ", list_icd.Select(e => e.Code));
                if (string.IsNullOrEmpty(icd)) disease = diagnosis;
                else disease = string.Format("{0} ({1})", diagnosis, icd);
            }
            catch (Exception) { }
            return disease;
        }
        private string GetEOCMainDisease(Guid? visit_id)
        {
            var disease = string.Empty;
            try
            {
                var oen_datas = unitOfWork.FormDatasRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id).ToList();
                var diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
                var list_icd = new List<dynamic>();
                var icd_raw = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENICDANS")?.Value;
                list_icd.AddRange(ICDConvert.Operate(icd_raw));
                var icd_option_raw = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENICDOPT")?.Value;
                list_icd.AddRange(ICDConvert.Operate(icd_option_raw));
                var icd = string.Join(", ", list_icd.Select(e => e.Code));
                if (string.IsNullOrEmpty(icd)) disease = diagnosis;
                else disease = string.Format("{0} ({1})", diagnosis, icd);
            }
            catch (Exception) { }
            return disease;
        }
        private dynamic GetICDAndDiagnosis(Guid customer_id)
        {
            var list_icd = new List<ICDComplexOutpatientCaseSummary>();
            var icd_in_ed = GetICDAndDiagnosiInED(customer_id);
            list_icd.AddRange(icd_in_ed);
            var icd_in_opd = GetICDAndDiagnosisInOPD(customer_id);
            list_icd.AddRange(icd_in_opd);
            var icd_in_eoc = GetICDAndDiagnosisInEOC(customer_id);
            list_icd.AddRange(icd_in_eoc);
            return list_icd.OrderByDescending(e=> e.ExaminationDate)
                .Select(e => new ICDReturnComplexOutpatientCaseSummary{
                    PrimaryDoctor = e.PrimaryDoctor,
                    Specialty = e.Specialty,
                    ExaminationDate = e.ExaminationDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    Diagnosis = e.Diagnosis,
                    ICDs = e.ICDs,
                }).ToList();
        }
        private List<ICDComplexOutpatientCaseSummary> GetICDAndDiagnosiInED(Guid customer_id)
        {
            var list_icd = new List<ICDComplexOutpatientCaseSummary>();
            var visit = GetDischargeInformationOfCustomer(customer_id);
            foreach (var vs in visit)
            {
                string doctor_name = string.Empty;
                var discharge_infomation = vs.DI;
                if (discharge_infomation.UpdatedAt != discharge_infomation.CreatedAt)
                    doctor_name = discharge_infomation.UpdatedBy;

                ICollection<EDDischargeInformationData> discharge_info_datas = discharge_infomation.DischargeInformationDatas;
                var diagnosis = discharge_info_datas.FirstOrDefault(
                    e => !e.IsDeleted && 
                    e.Code == "DI0DIAANS"
                )?.Value;
                var icd_code_list = new List<string>();
                var icd_raw = discharge_info_datas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals("DI0DIAICD")
                )?.Value;
                icd_code_list.AddRange(ICDConvert.Operate(icd_raw).Select(e => e.Code));
                var icd_option_raw = discharge_info_datas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals("DI0DIAOPT")
                )?.Value;
                icd_code_list.AddRange(ICDConvert.Operate(icd_option_raw).Select(e => e.Code));

                var icd_master = unitOfWork.ICD10Repository.Find(
                    e => !e.IsDeleted
                    && !string.IsNullOrEmpty(e.Code)
                    && icd_code_list.Contains(e.Code)
                ).Select(e => new { e.Code, e.IsChronic, e.ViName, e.EnName });
                list_icd.Add(new ICDComplexOutpatientCaseSummary
                {
                    Diagnosis = diagnosis,
                    Specialty = vs.Specialty,
                    ExaminationDate = discharge_infomation.AssessmentAt,
                    PrimaryDoctor = doctor_name,
                    ICDs = icd_master
                });
            }
            return list_icd;
        }
        private List<ICDComplexOutpatientCaseSummary> GetICDAndDiagnosisInEOC(Guid customer_id)
        {
            var list_icd = new List<ICDComplexOutpatientCaseSummary>();
            var visit = unitOfWork.EOCRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).ToList();
            foreach (var vs in visit)
            {
                var oen = unitOfWork.EOCOutpatientExaminationNoteRepository.Find(e => !e.IsDeleted && e.VisitId == vs.Id).FirstOrDefault();
                if (oen != null) { 
                    List<FormDatas> oen_datas = unitOfWork.FormDatasRepository.Find(e => !e.IsDeleted && e.VisitId == vs.Id && e.FormCode == "OPDOEN").ToList();
                    var diagnosis = oen_datas.FirstOrDefault(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.Code)
                        && e.Code == "OPDOENDD0ANS"
                    )?.Value;
                    var icd_code_list = new List<string>();
                    var icd_raw = oen_datas.FirstOrDefault(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.Code) &&
                        e.Code.Equals("OPDOENICDANS")
                    )?.Value;
                    icd_code_list.AddRange(ICDConvert.Operate(icd_raw).Select(e => e.Code));
                    var icd_option_raw = oen_datas.FirstOrDefault(
                        e => !e.IsDeleted &&
                        !string.IsNullOrEmpty(e.Code) &&
                        e.Code.Equals("OPDOENICDOPT")
                    )?.Value;
                    icd_code_list.AddRange(ICDConvert.Operate(icd_option_raw).Select(e => e.Code));

                    var icd_master = unitOfWork.ICD10Repository.Find(
                        e => !e.IsDeleted
                        && !string.IsNullOrEmpty(e.Code)
                        && icd_code_list.Contains(e.Code)
                    ).Select(e => new { e.Code, e.IsChronic, e.ViName, e.EnName });
                    list_icd.Add(new ICDComplexOutpatientCaseSummary
                    {
                        Diagnosis = diagnosis,
                        Specialty = vs.Specialty,
                        ExaminationDate = oen.ExaminationTime,
                        PrimaryDoctor = vs.PrimaryDoctor?.Username,
                        ICDs = icd_master
                    });
                }
            }
            return list_icd;
        }
        private List<ICDComplexOutpatientCaseSummary> GetICDAndDiagnosisInOPD(Guid customer_id)
        {
            var list_icd = new List<ICDComplexOutpatientCaseSummary>();
            var visit = GetOPDOutpatientExaminationNoteOfCustomerForICDComplex(customer_id);
            foreach (var vs in visit)
            {
                ICollection<OPDOutpatientExaminationNoteData> oen_datas = vs.OEN.OPDOutpatientExaminationNoteDatas;
                var diagnosis = oen_datas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) 
                    && e.Code == "OPDOENDD0ANS"
                )?.Value;
                var icd_code_list = new List<string>();
                var icd_raw = oen_datas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals("OPDOENICDANS")
                )?.Value;
                icd_code_list.AddRange(ICDConvert.Operate(icd_raw).Select(e => e.Code));
                var icd_option_raw = oen_datas.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.Code) &&
                    e.Code.Equals("OPDOENICDOPT")
                )?.Value;
                icd_code_list.AddRange(ICDConvert.Operate(icd_option_raw).Select(e => e.Code));

                var icd_master = unitOfWork.ICD10Repository.Find(
                    e => !e.IsDeleted 
                    && !string.IsNullOrEmpty(e.Code) 
                    && icd_code_list.Contains(e.Code)
                ).Select(e => new { e.Code, e.IsChronic, e.ViName, e.EnName });
                list_icd.Add(new ICDComplexOutpatientCaseSummary
                {
                    Diagnosis = diagnosis,
                    Specialty = vs.Specialty,
                    ExaminationDate = vs.OEN.ExaminationTime,
                    PrimaryDoctor = vs.PrimaryDoctor,
                    ICDs = icd_master
                });
            }
            return list_icd;
        }
        private dynamic GetDischargeInformationOfCustomer(Guid customer_id)
        {
            return unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.DischargeInformationId != null
            ).Select(e => new { 
                DI = e.DischargeInformation,
                Specialty = new { e.Specialty?.ViName, e.Specialty?.EnName }
            }).ToList();
        }
        private dynamic GetOPDOutpatientExaminationNoteOfCustomerForICDComplex(Guid customer_id)
        {
            return unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).Select(e => new { 
                OEN = e.OPDOutpatientExaminationNote, 
                PrimaryDoctor = e.PrimaryDoctor?.Username,
                Specialty = new {e.Specialty?.ViName, e.Specialty?.EnName },
                VisitId = e.Id
            }).ToList();
        }

        private void CreateConfirmNotification(User from_user, ComplexOutpatientCaseSummary complex)
        {
            var visit = GetVisit((Guid)complex.CustomerId, complex.VisitId, complex.VisitTypeGroupCode);
            var primary_doctor = complex.PrimaryDoctor;
            if (primary_doctor != null)
            {
                var vi_mes = string.Format(
                    "Bạn nhận được YÊU CẦU XÓA THUỐC tại phiếu ca bệnh phức tạp của bệnh nhân <b>{0}</b> từ <b>{1}</b> ({2})",
                    complex.Customer?.Fullname,
                    from_user?.Fullname,
                    from_user?.Title
                );
                var en_mes = string.Format(
                    "Bạn nhận được YÊU CẦU XÓA THUỐC tại phiếu ca bệnh phức tạp của bệnh nhân <b>{0}</b> từ <b>{1}</b> ({2})",
                    complex.Customer?.Fullname,
                    from_user?.Fullname,
                    from_user?.Title
                );
                var noti_creator = new NotificationCreator(
                    unitOfWork: unitOfWork,
                    from_user: from_user?.Username,
                    to_user: primary_doctor.Username,
                    priority: 3,
                    vi_message: vi_mes,
                    en_message: en_mes,
                    spec_id: visit.SpecialtyId,
                    visit_id: complex.VisitId,
                    group_code: complex.VisitTypeGroupCode,
                    form_frontend: "ComplexOutpatientCaseSummary"
                );
                noti_creator.Create();
            }
        }
    }
}
