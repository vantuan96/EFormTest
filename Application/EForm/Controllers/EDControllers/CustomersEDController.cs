using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DataAccess.Models;
using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Utils;
using EForm.Models;
using Newtonsoft.Json;
using System.Linq;
using EMRModels;
using System.Threading.Tasks;
using Helper;
namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class CustomersEDController : BaseEDApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CustomersED")]
        [Permission(Code = "ECUED1")]
        public IHttpActionResult CreateCustomerAPI([FromBody] CustomerEDParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            InHospital in_hospital = new InHospital();
            var in_hospital_status_id = in_hospital.GetStatus().Id;

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var Specialty = GetSpecialty();
            if (Specialty.VisitTypeGroup.Code != "ED")
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_ACCOUNT_SPE_INVALID);

            var creater = new VisitCreater(
                unitOfWork,
                in_hospital_status_id,
                null,
                null,
                null,
                null,
                GetSiteId(),
                Specialty.Id,
                GetUser().Id
            );

            var local_customer = GetLocalCustomer(request);
            if (local_customer == null)
            {
                var customer = CreateCustomer(request, in_hospital_status_id);
                creater.SetCustomerId(customer.Id);
                var new_ed = creater.CreateNewED(request);
                return Content(HttpStatusCode.OK, new { new_ed.Id });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(request.PID))
                {
                    UpdateCustomer(local_customer, request, in_hospital_status_id);
                }
            }

            in_hospital.SetState(local_customer.Id, null, null, null);
            var in_hospital_visit = in_hospital.GetVisit();
            if (in_hospital_visit != null && !IsSuperman())
            {
                var specialty = GetSpecialty();
                if (in_hospital_visit.SpecialtyId == specialty.Id)
                    return Content(HttpStatusCode.OK, new { in_hospital_visit.Id });
                return Content(HttpStatusCode.BadRequest, in_hospital.BuildErrorMessage(in_hospital_visit));
            }

            dynamic in_waiting_visit;
            if (!string.IsNullOrEmpty(local_customer.PID))
                in_waiting_visit = GetInWaitingAcceptPatientByPID(local_customer.PID);
            else
                in_waiting_visit = GetInWaitingAcceptPatientById(local_customer.Id);
            if (in_waiting_visit != null)
            {
                var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                return Content(HttpStatusCode.BadRequest, BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician));
            }

            creater.SetCustomerId(local_customer.Id);
            ED ed = creater.CreateNewED(request);

            return Content(HttpStatusCode.OK, new { ed.Id });
        }
        [HttpGet]
        [Route("api/ED/CustomersED/{id}/ext")]
        [Permission(Code = "ECUED2")]
        public IHttpActionResult DetailCustomerAPI_EXT(Guid id, [FromUri] CustomerRequetDetailModel request)
        {
            var ed = GetEDData(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var vip_info = GetEDVipInfo(ed);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }
            if (IsDoctor() && request.readonlyview != true)
            {
                var is_waiting_accept_error_message = GetWaitingDoctorAcceptMessage(ed);
                if (is_waiting_accept_error_message != null)
                    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);
            }
            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                customer.Id,
                customer.PID,
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.AgeFormated,
                customer.Phone,
                customer.Gender,
                customer.Fork,
                customer.Nationality,
                customer.Address,
                customer.Job,
                customer.IsVip,
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.IsChronic,
                customer.IdentificationCard,
                IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                customer.IssuePlace,
                EDId = ed.Id,
                ed.RecordCode,
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ed.VisitCode,
                ed.HealthInsuranceNumber,
                StartHealthInsuranceDate = ed.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = ed.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ed.CovidRiskGroup,
                ed.SelfHarmRiskScreeningResults,
                customer.RelationshipAddress,
                UserNameReceive = ed.CreatedBy,
                ed.Version
            });
        }
        [HttpGet]
        [Route("api/ED/CustomersED/{id}")]
        [Permission(Code = "ECUED2")]
        public IHttpActionResult DetailCustomerAPI(Guid id, [FromUri] CustomerRequetDetailModel request)
        {
            var ed = GetEDData(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            var vip_info = GetEDVipInfo(ed);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }
            if (IsDoctor() && request.readonlyview != true)
            {
                var is_waiting_accept_error_message = GetWaitingDoctorAcceptMessage(ed);
                if (is_waiting_accept_error_message != null)
                    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);
            }
            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            var specialty = ed.Specialty;
            var codeStatus = ed.EDStatus?.Code;

            return Content(HttpStatusCode.OK, new
            {
                customer.Id,
                customer.PID,
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.AgeFormated,
                customer.Phone,
                customer.Gender,
                customer.Fork,
                customer.Nationality,
                customer.Address,
                customer.Job,
                customer.IsVip,
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.IsChronic,
                customer.IdentificationCard,
                IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                customer.IssuePlace,
                EDId = ed.Id,
                ed.RecordCode,
                ATSScale = unitOfWork.MasterDataRepository.FirstOrDefault(m => !m.IsDeleted && m.Code == ed.ATSScale),
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ed.VisitCode,
                ed.HealthInsuranceNumber,
                StartHealthInsuranceDate = ed.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = ed.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ed.CovidRiskGroup,
                ed.SelfHarmRiskScreeningResults,
                IsCovidSpecialty = IsCovidSpecialty(),
                Link = new List<dynamic>(){
                    new { ed.Id, ViName="Phân loại cấp cứu", EnName="Emergency triage record", Type="ETR"},
                    new { ed.Id, ViName="Ghi nhận thực hiện thuốc", EnName="Standing Order", Type="StandingOrder"},
                    new { ed.Id, ViName="Ghi nhận thực hiện thuốc NB dịch vụ lẻ", EnName="Standing Order For Retail Service", Type="StandingOrderForRetailService"},
                    new { ed.Id, ViName="Bệnh án cấp cứu", EnName="Emergency record", Type="ER0"},
                    new { ed.Id, ViName="Thêm theo dõi", EnName="Add observation", Type="OS0"},
                    new { ed.Id, ViName="Đánh giá kết thúc", EnName="Discharge information", Type="DI0"},
                    new { ed.Id, ViName="Bản kiểm bàn giao người bệnh trước mổ", EnName="Pre-Operative/Procedure handover checklist", Type="PHC"},
                    new { ed.Id, ViName="Báo cáo y tế", EnName="Medical report", Type="KLM"},
                    new { ed.Id, ViName="Báo cáo y tế ra viện", EnName="Discharge medical report", Type="DMR"},
                    new { ed.Id, ViName="Giấy ra viện", EnName="DischargeCertificate", Type="NPQ"},
                    new { ed.Id, ViName="Giấy chuyển tuyến", EnName="Transfer Letter", Type="TransferLetter"},
                    new { ed.Id, ViName="Bàn giao vận chuyển", EnName="Hand over form for patients being transferred", Type="MCA"},
                    new { ed.Id, ViName="Biên bản bàn giao người bệnh chuyển khoa", EnName="Patient hand over record", Type="PHR"},
                    new { ed.Id, ViName="Giấy chuyển viện", EnName="ReferralLetter", Type="RL0"},
                    new { ed.Id, ViName="Phiếu GDSK cho NB và thân nhân", EnName="Patient and family education form", Type="PFEF"},
                    new { ed.Id, ViName="Giấy xác nhận BN cấp cứu", EnName="Emergency Confirmation", Type="EMCO"},
                    new { ed.Id, ViName="Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+", EnName="Point of care testing - Chemistry Catridge CHEM8+", Type="PointOfCareTesting/ChemicalBiologyTest"},
                    new { ed.Id, ViName="Xét nghiệm khí máu động mạch", EnName="Arterial blood gas test", Type="PointOfCareTesting/ArterialBloodGasTest"},
                    new { ed.Id, ViName="Kết quả test da", EnName="Skin test result", Type="SkinTestResult"},
                    new { ed.Id, ViName="Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)", EnName="Minutes of consultation for patient using drug with an asterisk mark(*)", Type="ConsultationDrugWithAnAsteriskMark"},
                    new { ed.Id, ViName="Biên bản hội chẩn thông qua mổ", EnName="Joint-Consultation for approval of surgery", Type="JointConsultationForApprovalOfSurgery"},
                },
                customer.RelationshipAddress,
                Specialty = new { ViName = specialty?.ViName, EnName = specialty?.EnName },
                DischargeDate = (Constant.InHospital.Contains(codeStatus) || Constant.WaitingResults.Contains(codeStatus)) ? "" : ed.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DoctorUserName = ed.PrimaryDoctor?.Username,
                NurseUserName = ed.PrimaryNurse?.Username,
                DiagnosisAndICD = GetVisitDiagnosisAndICD(ed.Id, "ED", true),
                Allergy = EMRVisitAllergy.GetEDVisitAllergy(ed),
                Age = CaculatorAgeCustormer(customer.DateOfBirth),
                UserNameReceive = ed.CreatedBy,
                SiteCode = ed.Site?.ApiCode,
                Site = ed.Site?.Name,
                ed.Version
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CustomersED/{id}")]
        [Permission(Code = "ECUED3")]
        public IHttpActionResult UpdateCustomerHISAPI(Guid id, [FromBody] CustomerEDParameterModel request)
        {
            //if (!request.Validate())
            //    return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            //var ed = GetED(id);
            //if (ed == null)
            //    return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            //var customer = ed.Customer;
            //if (customer == null || customer.IsDeleted)
            //    return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            //if (!string.IsNullOrEmpty(customer.PID))
            //    return Content(HttpStatusCode.BadRequest, Message.SYNCHRONIZED_ERROR);

            //InHospital in_hospital = new InHospital();
            //var local_customer = GetLocalCustomer(request);
            //var in_hospital_status_id = in_hospital.GetStatus().Id;
            //if (local_customer != null)
            //{
            //    if (local_customer.EDStatusId != in_hospital_status_id)
            //    {
            //        MergeAnonymousCustomer(ed, local_customer.Id, customer);
            //        return Content(HttpStatusCode.OK, Message.SUCCESS);
            //    }
            //    return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_IS_EXIST);
            //}

            //UpdateCustomer(customer, request, in_hospital_status_id);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CustomersED/ManualUpdate/{id}")]
        [Permission(Code = "ECUED4")]
        public async Task<IHttpActionResult> UpdateManualCustomerAPIAsync(Guid id, [FromBody] CustomerManualUpdateParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var ed = GetED(id);

            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            UpdatePrimaryDoctor(ed, request.BacSiAD);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            if (!string.IsNullOrEmpty(request.PID))
            {
                var pid_error = IsValidPID(request.PID, request.Fullname, request.DateOfBirth);
                if (pid_error != null)
                    return Content(HttpStatusCode.BadRequest, pid_error);

                await SyncHisCustomerAsync(request.PID);

                var local_customer = unitOfWork.CustomerRepository.FirstOrDefault(c => !c.IsDeleted && c.Id != customer.Id && !string.IsNullOrEmpty(c.PID) && c.PID == request.PID);
                if (local_customer != null)
                {
                    InHospital in_hospital = new InHospital();
                    in_hospital.SetState(local_customer.Id, null, null, null);
                    var in_hospital_visit = in_hospital.GetVisit();
                    if (in_hospital_visit != null)
                        return Content(HttpStatusCode.BadRequest, in_hospital.BuildErrorMessage(in_hospital_visit));

                    dynamic in_waiting_visit;
                    if (!string.IsNullOrEmpty(local_customer.PID))
                        in_waiting_visit = GetInWaitingAcceptPatientByPID(local_customer.PID);
                    else
                        in_waiting_visit = GetInWaitingAcceptPatientById(local_customer.Id);
                    if (in_waiting_visit != null)
                    {
                        var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                        return Content(HttpStatusCode.BadRequest, BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician));
                    }

                    MergeAnonymousCustomer(ed, local_customer.Id, customer);
                    GenerateRecordCode(ed);
                    UpdateManualCustomer(customer, ed, request);
                    return Content(HttpStatusCode.OK, Message.SUCCESS);
                }
            }

            UpdateManualCustomer(customer, ed, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void UpdatePrimaryDoctor(ED ed, string bacSiAD)
        {
            if (!string.IsNullOrWhiteSpace(bacSiAD))
            {
                Guid PrimaryDoctorId = Guid.Empty;
                User user = unitOfWork.UserRepository.FirstOrDefault(e => e.Username == bacSiAD);
                if (user != null)
                {
                    PrimaryDoctorId = user.Id;
                }
                if (PrimaryDoctorId != Guid.Empty)
                {
                    ed.PrimaryDoctorId = PrimaryDoctorId;
                }
                unitOfWork.EDRepository.Update(ed);
                unitOfWork.Commit();
            }
        }

        [HttpGet]
        [Route("api/ED/CustomersED/SyncVisitCode/{id}")]
        [Permission(Code = "ECUED5")]
        public IHttpActionResult SyncVisitcodeAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetVisitCode(customer.PID));
            return Content(HttpStatusCode.OK, OHClient.GetVisitCode(customer.PID));
        }

        private Customer GetLocalCustomer(CustomerEDParameterModel request)
        {
            if (request.PID == null)
                return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Fullname == request.Fullname && e.DateOfBirth == request.ConvertedDateOfBirth && e.Gender == request.Gender);
            return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.PID == request.PID);
        }


        private void UpdateManualCustomer(Customer customer, ED ed, CustomerManualUpdateParameterModel request)
        {
            var current_ed = JsonConvert.SerializeObject(new
            {
                ed.VisitCode,
                ed.HealthInsuranceNumber,
                ed.StartHealthInsuranceDate,
                ed.ExpireHealthInsuranceDate,
            });

            var update_ed = JsonConvert.SerializeObject(new
            {
                request.VisitCode,
                request.HealthInsuranceNumber,
                StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate,
                ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate,
            });

            if (!current_ed.Equals(update_ed))
            {
                ed.VisitCode = request.VisitCode;
                ed.HealthInsuranceNumber = request.HealthInsuranceNumber;
                ed.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
                ed.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;
                unitOfWork.EDRepository.Update(ed);
                unitOfWork.Commit();
            }
        }

        private void MergeAnonymousCustomer(ED ed, Guid local_customer_id, Customer current_customer)
        {
            ed.CustomerId = local_customer_id;
            unitOfWork.EDRepository.Update(ed);

            // current_customer.IsDeleted = true;
            // unitOfWork.CustomerRepository.Update(current_customer);
            unitOfWork.Commit();
        }

        private dynamic IsValidPID(string pid, string fullname, string dob)
        {
            var his_customers = OHClient.searchPatienteOh(new SearchParameter { PID = pid });

            if (his_customers.Count == 0)
                return Message.PID_INVALID;

            var his_customer = his_customers[0];
            if (his_customer.Fullname != fullname || his_customer.DateOfBirth != dob)
                return his_customer;

            return null;
        }
    }
}