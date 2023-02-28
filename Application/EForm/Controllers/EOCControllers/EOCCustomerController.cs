using DataAccess.Models;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Utils;
using EForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DataAccess.Models.EOCModel;
using System.Linq;
using EForm.Models.EOCModel;
using EForm.Models.EDModels;
using EForm.Models.OPDModels;
using DataAccess.Models.EDModel;
using EMRModels;
using System.Threading.Tasks;
using Helper;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class EOCCustomerController : BaseOPDApiController
    {
        [HttpGet]
        [Route("api/eoc/customer/{id}/ext")]
        [Permission(Code = "EOC007")]
        public IHttpActionResult EocCustomerEXT(Guid id)
        {
            var visit = GetEOCData(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var vip_info = GetEOCVipInfo(visit);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }

            var customer = visit.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);


            return Content(HttpStatusCode.OK, new
            {
                customer.Id,
                customer.PID,
                customer.Fullname,
                customer.IsVip,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.Phone,
                customer.Gender,
                customer.Fork,
                customer.AgeFormated,
                customer.Nationality,
                customer.Address,
                customer.Job,
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.IsChronic,
                customer.IdentificationCard,
                IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                customer.IssuePlace,
                VisitId = visit.Id,
                visit.RecordCode,
                AdmittedDate = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                visit.VisitCode,
                visit.HealthInsuranceNumber,
                StartHealthInsuranceDate = visit.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = visit.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                customer.RelationshipAddress,
                UserNameReceive = visit.CreatedBy,
                visit.Version
            });
        }
        [HttpGet]
        [Route("api/eoc/customer/{id}")]
        [Permission(Code = "EOC007")]
        public IHttpActionResult EocCustomer(Guid id)
        {
            var visit = GetEOCData(id);
            if (visit == null) return Content(HttpStatusCode.BadRequest, Message.VISIT_NOT_FOUND);
            var vip_info = GetEOCVipInfo(visit);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }

            var customer = visit.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            var specialty = visit.Specialty;
            var codeStatus = visit.Status?.Code;

            return Content(HttpStatusCode.OK, new
            {
                customer.Id,
                customer.PID,
                customer.Fullname,
                customer.IsVip,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                customer.Phone,
                customer.Gender,
                customer.Fork,
                customer.AgeFormated,
                customer.Nationality,
                customer.Address,
                customer.Job,
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.IsChronic,
                customer.IdentificationCard,
                IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                customer.IssuePlace,
                VisitId = visit.Id,
                visit.RecordCode,
                AdmittedDate = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                visit.VisitCode,
                visit.HealthInsuranceNumber,
                StartHealthInsuranceDate = visit.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = visit.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                Link = new List<dynamic>(){
                    new { visit.Id, ViName="Đánh giá ban đầu", EnName="Initial Assessment", Type="InitialAssessment"},
                    new { visit.Id, ViName="Thêm theo dõi", EnName="Patient Progress Note", Type="PatientProgressNote"},
                    new { visit.Id, ViName="Phiếu khám ngoại trú", EnName="Outpatient Examination Note", Type="OutpatientExaminationNote"},
                    new { visit.Id, ViName="Báo cáo y tế", EnName="Medical report", Type="MedicalReport"},
                    new { visit.Id, ViName="Giấy chuyển tuyến", EnName="Transfer Letter", Type="TransferLetter"},
                    new { visit.Id, ViName="Giấy chuyển viện", EnName="Referral Letter", Type="ReferralLetter"},
                    new { visit.Id, ViName="Biên bản bàn giao NB chuyển khoa", EnName="Hand Over Check List", Type="HandOverCheckList"},
                    new { visit.Id, ViName="Tóm tắt ca bệnh phức tạp", EnName="Complex Outpatient Case Summary", Type="ComplexOutpatientCaseSummary"},
                    new { visit.Id, ViName="Ghi nhận thực hiện thuốc", EnName="Standing Order", Type="StandingOrder"},
                },
                customer.RelationshipAddress,
                Specialty = new { ViName = specialty?.ViName, EnName = specialty?.EnName },
                DischargeDate = (Constant.InHospital.Contains(codeStatus) || Constant.WaitingResults.Contains(codeStatus)) ? "" : visit.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DoctorUserName = visit.PrimaryDoctor?.Username,
                NurseUserName = visit.PrimaryNurse?.Username,
                DiagnosisAndICD = GetVisitDiagnosisAndICD(visit.Id, "EOC", true),
                Allergy = EMRVisitAllergy.GetEOCVisitAllergy(visit),
                Age = CaculatorAgeCustormer(customer.DateOfBirth),
                UserNameReceive = visit.CreatedBy,
                SiteCode = visit.Site?.ApiCode,
                Site = visit.Site?.Name,
                visit.Version
            });
        }
        [HttpGet]
        [Route("api/eoc/customer/SyncVisitCodeByPID/{pid}")]
        [Permission(Code = "EOC007")]
        public IHttpActionResult SyncVisitcodeByPIDAPI(string pid, [FromUri]EOCTranfers request)
        {
            if (string.IsNullOrEmpty(pid))
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);
            if (!string.IsNullOrEmpty(request.checkTranfer))
            {
                var local_customer = GetLocalCustomerByPid(pid);
                if (local_customer != null)
                {
                    EOCTransfer in_waiting_visit = GetInWaiting(local_customer.Id);
                    if (in_waiting_visit != null)
                        return Content(HttpStatusCode.BadGateway, new { ViMessage = "Yêu cầu tiếp nhận tồn tại", EnMessage = "Yêu cầu tiếp nhận tồn tại", TransferExit = true, TransferData = GetCustomersTrandfer(in_waiting_visit.Id) });
                }
            }
            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetVisitCode(pid, "OPD"));
            return Content(HttpStatusCode.OK, OHClient.GetVisitCode(pid, "OPD"));
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/customer-form-other-specialty/{id}")]
        [Permission(Code = "EOC008")]
        public IHttpActionResult CreateCustomerFormOtherSpecialty(Guid id, [FromBody]CustomerEDParameterModel request)
        {
            var finded = unitOfWork.EOCTransferRepository.GetById(id);
            if (finded == null || finded.AcceptBy != null)
                return Content(HttpStatusCode.NotFound, Message.INFO_INCORRECT);

            var visit = GetVisit((Guid)finded.FromVisitId, finded.FromVisitType);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.INFO_INCORRECT);

            ValidateCustomInHospital validate_custome = ValidaterCustomInHopital(visit.CustomerId, visit.Id);
            if (!validate_custome.IsValidate)
                return Content(HttpStatusCode.BadGateway, validate_custome.Msg);

            var visit_id = NewVisit(visit.CustomerId, request, finded.FromVisitId, finded.FromVisitType, visit.VisitCode);

            finded.ToVisitId = visit_id;
            finded.AcceptBy = GetUser().Username;
            finded.AcceptAt = DateTime.Now;
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new { Id = visit_id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/customer")]
        [Permission(Code = "EOC009")]
        public IHttpActionResult Create([FromBody]CustomerEDParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var Specialty = GetSpecialty();
            if (Specialty.VisitTypeGroup.Code != "EOC")
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_ACCOUNT_SPE_INVALID);

            var local_customer = GetLocalCustomer(request);
            if (local_customer == null)
            {
                var customer = CreateCustomer(request, GetInHospitalStatusId("EOC"));
                var visit_id = NewVisit(customer.Id, request);
                return Content(HttpStatusCode.OK, new { Id = visit_id });
            } else
            {
                EOCTransfer in_waiting_visit = GetInWaiting(local_customer.Id);
                if (in_waiting_visit != null)
                    return Content(HttpStatusCode.BadGateway, new { ViMessage = "Yêu cầu tiếp nhận tồn tại", EnMessage = "Yêu cầu tiếp nhận tồn tại", TransferExit = true, TransferData = GetCustomersTrandfer(in_waiting_visit.Id) });

                UpdateCustomer(local_customer, request, GetInHospitalStatusId("EOC"));
            }

            ValidateCustomInHospital validate_custome = ValidaterCustomInHopital(local_customer.Id, null);
            if (!validate_custome.IsValidate)
                return Content(HttpStatusCode.BadGateway, validate_custome.Msg);

            var visit_id2 = NewVisit(local_customer.Id, request);
            
            return Content(HttpStatusCode.OK, new { Id = visit_id2 });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/eoc/Customer/ManualUpdate/{id}")]
        [Permission(Code = "EOC010")]
        public async Task<IHttpActionResult> UpdateManualCustomerAPIAsync(Guid id, [FromBody] CustomerManualUpdateParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            if (!string.IsNullOrWhiteSpace(customer.PID))
            {
               await SyncHisCustomerAsync(customer.PID);
            }
            UpdateManualCustomer(customer, opd, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/eoc/Customer/SyncVisitCode/{id}")]
        [Permission(Code = "EOC007")]
        public IHttpActionResult SyncVisitcodeAPI(Guid id)
        {
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetVisitCode(customer.PID, "OPD"));
            return Content(HttpStatusCode.OK, OHClient.GetVisitCode(customer.PID, "OPD"));
        }
        public dynamic GetCustomersTrandfer (Guid trandfer_id)
        {
            var site_id = GetSiteId();
            var specialty_id = GetSpecialtyId();
            var data = (from eoc_sql in unitOfWork.EOCTransferRepository.AsQueryable().Where(
                            e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) && string.IsNullOrEmpty(e.AcceptBy) && e.Id == trandfer_id
                         )
                         join cus_sql in unitOfWork.CustomerRepository.AsQueryable()
                            on eoc_sql.CustomerId equals cus_sql.Id into cEd
                         from cus_sql in cEd.DefaultIfEmpty()

                         join sp_sql in unitOfWork.SpecialtyRepository.AsQueryable()
                            on eoc_sql.SpecialtyId equals sp_sql.Id into cpOpd
                         from sp_sql in cpOpd.DefaultIfEmpty()

                         select new EOCTranfers
                         {
                             Id = eoc_sql.Id,
                             PID = cus_sql.PID,
                             Fullname = cus_sql.Fullname,
                             DateOfBirth = cus_sql.DateOfBirth,
                             SpecialtyName = sp_sql.ViName,
                             TransferBy = eoc_sql.TransferBy,
                             TransferAt = eoc_sql.TransferAt,
                             AcceptAt = eoc_sql.AcceptAt,
                             AcceptBy = eoc_sql.AcceptBy,
                             SpecialtyId = sp_sql.Id,
                             SiteId = eoc_sql.SiteId
                         }).Where(eoc => eoc.SiteId == site_id).FirstOrDefault();
            return new
            {
                Id = trandfer_id,
                data.PID,
                data.Fullname,
                data.DateOfBirth,
                data.SpecialtyName,
                data.TransferBy,
                data.TransferAt,
                data.AcceptAt,
                data.AcceptBy,
                data.SpecialtyId,
                data.SiteId
            };
        }
        private void AcceptWaitingTranfer(Guid id, Guid visit_id)
        {
            var waiting_tranfer = unitOfWork.EOCTransferRepository.GetById(id);
            waiting_tranfer.ToVisitId = visit_id;
            waiting_tranfer.AcceptBy = GetUser().Username;
            waiting_tranfer.AcceptAt = DateTime.Now;
            unitOfWork.Commit();
        }

        private EOCTransfer GetInWaiting(Guid customer_id)
        {
            var site_id = GetSiteId();
            return unitOfWork.EOCTransferRepository.FirstOrDefault(eoc => !eoc.IsDeleted && eoc.AcceptBy == null && eoc.SiteId == site_id && eoc.CustomerId == customer_id);
        }

        private Customer GetLocalCustomer(CustomerEDParameterModel request)
        {
            if (request.PID == null)
                return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Fullname == request.Fullname && e.DateOfBirth == request.ConvertedDateOfBirth && e.Gender == request.Gender);
            return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.PID == request.PID);
        }
        private Customer GetLocalCustomerByPid(string PID)
        {
            return unitOfWork.CustomerRepository.FirstOrDefault(e => e.PID == PID);
        }
        public Guid NewVisit(Guid customer_id, CustomerEDParameterModel request, Guid? transfer_id = null, string from_visit_type = null, string visit_code = null) {
            var creater = new VisitCreater(
                unitOfWork,
                GetStatusIdByCode("EOCIH"),
                customer_id,
                transfer_id,
                null,
                visit_code != null ? visit_code : request.VisitCode,
                GetSiteId(),
                GetSpecialtyId(),
                GetUser().Id
            );
            var eoc = creater.CreateNewEOC(request, from_visit_type);
            return eoc.Id;
        }
        private void UpdateManualCustomer(Customer customer, EOC opd, CustomerManualUpdateParameterModel request)
        {
            var current_opd = JsonConvert.SerializeObject(new
            {
                opd.VisitCode,
                opd.HealthInsuranceNumber,
                opd.StartHealthInsuranceDate,
                opd.ExpireHealthInsuranceDate,
            });

            var update_opd = JsonConvert.SerializeObject(new
            {
                request.VisitCode,
                request.HealthInsuranceNumber,
                StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate,
                ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate,
            });

            if (!current_opd.Equals(update_opd))
            {
                opd.VisitCode = request.VisitCode;
                opd.HealthInsuranceNumber = request.HealthInsuranceNumber;
                opd.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
                opd.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;
                unitOfWork.EOCRepository.Update(opd);
                unitOfWork.Commit();
            }
        }

        private void MergeAnonymousCustomer(EOC opd, Guid local_customer_id, Customer current_customer)
        {
            opd.CustomerId = local_customer_id;
            unitOfWork.EOCRepository.Update(opd);

            current_customer.IsDeleted = true;
            unitOfWork.CustomerRepository.Update(current_customer);
            unitOfWork.Commit(); ;
        }
    }
}
