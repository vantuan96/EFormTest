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
using System.Linq;
using EForm.Models.OPDModels;
using System.Text;
using System.Web.Script.Serialization;
using EForm.Models.IPDModels;
using EForm.Models.EDModels;
using EMRModels;
using System.Threading.Tasks;
using Helper;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class CustomerOPDController : BaseOPDApiController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CustomerOPD")]
        [Permission(Code = "OCOPD1")]
        public async Task<IHttpActionResult> CreateCustomerAPIAsync([FromBody] CustomerEDParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            if (string.IsNullOrEmpty(request.VisitCode))
                return Content(HttpStatusCode.BadRequest, Message.VISIT_CODE_IS_MISSING);
            var Specialty = GetSpecialty();
            if (Specialty.VisitTypeGroup.Code != "OPD")
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_ACCOUNT_SPE_INVALID);
            var check = GetAnesthesia(Specialty, request.PID);
            bool isNewCus = false;
            var local_customer = GetLocalCustomer(request);
            if (local_customer == null)
            {
                isNewCus = true;
                local_customer = CreateCustomer(request);
            }
            if (check != null)
            {
                var user = GetUser();
                Guid id = check.Id;
                check.IsAcceptPhysician = true;
                check.IsAcceptNurse = true;
                check.ReceivingNurseId = user?.Id;

                unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(check);
                unitOfWork.Commit();

                Guid hand_over_check_list_id = id;
                var new_visit_id = CreateNewOPDFromHandOver(local_customer.Id, hand_over_check_list_id, request.VisitCode);

                return Content(HttpStatusCode.OK, new { Id = new_visit_id });
            }
            InHospital in_hospital = new InHospital();
            var in_hospital_status_id = in_hospital.GetStatus("OPD").Id;
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital_status_id,
                null,
                null,
                null,
                request.VisitCode,
                GetSiteId(),
                Specialty.Id,
                GetUser().Id,
                Specialty.IsAnesthesia
            );

            if (isNewCus)
            {
                var customer = CreateCustomer(request, in_hospital_status_id);
                creater.SetCustomerId(customer.Id);
                var new_opd = creater.CreateNewOPD();
                return Content(HttpStatusCode.OK, new { new_opd.Id });
            }
            else
            {
                UpdateCustomer(local_customer, request, in_hospital_status_id);
            }

            in_hospital.SetState(local_customer.Id, null, null, request.VisitCode);
            var in_hospital_visit = in_hospital.GetVisit();
            if (in_hospital_visit != null && !IsSuperman())
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

            SyncCustomer(local_customer, request);
            creater.SetCustomerId(local_customer.Id);
            OPD opd = creater.CreateNewOPD(request);
            
            if (!string.IsNullOrWhiteSpace(local_customer.PID))
            {
                await SyncHisCustomerAsync(local_customer.PID);
            }
            return Content(HttpStatusCode.OK, new { opd.Id });
        }

        [HttpGet]
        [Route("api/OPD/CustomerOPD/{id}/ext")]
        [Permission(Code = "OCOPD2")]
        public IHttpActionResult DetailCustomerAPIEXT(Guid id, [FromUri] CustomerRequetDetailModel request)
        {
            var opd = GetOPDData(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var vip_info = GetOPDVipInfo(opd);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }
            if (IsDoctor() && request.readonlyview != true)
            {
                var is_waiting_accept_error_message = GetWaitingDoctorAcceptMessage(opd);
                if (is_waiting_accept_error_message != null)
                {
                    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);
                }
            }
            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                customer.IsVip,
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
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.IsChronic,
                customer.IdentificationCard,
                IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                customer.IssuePlace,
                OPDId = opd.Id,
                opd.RecordCode,
                opd.IsTelehealth,
                AdmittedDate = opd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                opd.VisitCode,
                opd.HealthInsuranceNumber,
                StartHealthInsuranceDate = opd.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = opd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                // MedicalRecordType = GetSetUpMedicalRecords(opd.Id, "OPD"),
                customer.RelationshipAddress,
                UserNameReceive = opd.CreatedBy,
                opd.IsAnesthesia, // đánh dấu khoa gây mê
                opd.Version
            });
        }
        [HttpGet]
        [Route("api/OPD/CustomerOPD/{id}")]
        [Permission(Code = "OCOPD2")]
        public IHttpActionResult DetailCustomerAPI(Guid id, [FromUri] CustomerRequetDetailModel request)
        {
            var opd = GetOPDData(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var vip_info = GetOPDVipInfo(opd);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }
            if (IsDoctor() && request.readonlyview != true)
            {
                var is_waiting_accept_error_message = GetWaitingDoctorAcceptMessage(opd);
                if (is_waiting_accept_error_message != null)
                {
                    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);
                }
            }
            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            var getPromissoryNoteBySetup = (from m in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                                            where m.SpecialityId == opd.SpecialtyId && m.IsDeploy
                                            join mas in unitOfWork.MasterDataRepository.AsQueryable()
                                            .Where(m => !m.IsDeleted && m.Note == "OPD")
                                            on m.Formcode equals mas.Group
                                            select new listMedicalRecordIsDeploy()
                                            {
                                                ViName = mas.ViName,
                                                EnName = mas.EnName,
                                                Type = mas.Code,
                                                FormCode = mas.Form
                                            }).ToList();

            var specialty = opd.Specialty;
            var codestatus = opd.EDStatus.Code;

            return Content(HttpStatusCode.OK, new
            {
                PrimaryDoctor = opd.PrimaryDoctor?.Username,
                customer.IsVip,
                customer.Id,
                customer.PID,
                customer.Fullname,
                DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                Age = CaculatorAgeCustormer(customer.DateOfBirth),
                customer.AgeFormated,
                customer.Phone,
                customer.Gender,
                customer.Fork,
                customer.Nationality,
                customer.Address,
                customer.Job,
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.IsChronic,
                customer.IdentificationCard,
                customer.MOHJob,
                customer.MOHJobCode,
                customer.MOHEthnic,
                customer.MOHEthnicCode,
                customer.MOHNationality,
                customer.MOHNationalityCode,
                customer.MOHAddress,
                customer.MOHDistrict,
                customer.MOHDistrictCode,
                customer.MOHProvince,
                customer.MOHProvinceCode,
                customer.MOHObject,
                IssueDate = customer.IssueDate?.ToString(Constant.DATE_FORMAT),
                customer.IssuePlace,
                OPDId = opd.Id,
                opd.RecordCode,
                opd.IsTelehealth,
                AdmittedDate = opd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                opd.VisitCode,
                opd.HealthInsuranceNumber,
                StartHealthInsuranceDate = opd.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = opd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                IsLocked = CheckIsBlock(opd),
                Link = new List<dynamic>(){
                    new { opd.Id, ViName="Đánh giá ban đầu", EnName="Initial Assessment", Type="InitialAssessment"},
                    new { opd.Id, ViName="Thêm theo dõi", EnName="Patient Progress Note", Type="PatientProgressNote"},
                    new { opd.Id, ViName="Phiếu khám ngoại trú", EnName="Outpatient Examination Note", Type="OutpatientExaminationNote"},
                    new { opd.Id, ViName="Báo cáo y tế", EnName="Medical report", Type="MedicalReport"},
                    new { opd.Id, ViName="Giấy chuyển tuyến", EnName="Transfer Letter", Type="TransferLetter"},
                    new { opd.Id, ViName="Giấy chuyển viện", EnName="Referral Letter", Type="ReferralLetter"},
                    new { opd.Id, ViName="Biên bản bàn giao NB chuyển khoa", EnName="Hand Over Check List", Type="HandOverCheckList"},
                    new { opd.Id, ViName="Tóm tắt ca bệnh phức tạp", EnName="Complex Outpatient Case Summary", Type="ComplexOutpatientCaseSummary"},
                    new { opd.Id, ViName="Ghi nhận thực hiện thuốc", EnName="Standing Order", Type="StandingOrder"},
                },
                // MedicalRecordType = GetSetUpMedicalRecords(opd.Id, "OPD"),
                PromissoryNote = GetPromissoryNote(opd.Id, (Guid)opd.SpecialtyId, getPromissoryNoteBySetup),
                customer.RelationshipAddress,
                Specialty = new { ViName = specialty?.ViName, EnName = specialty?.EnName },
                DischargeDate = (Constant.InHospital.Contains(codestatus) || Constant.WaitingResults.Contains(codestatus)) ? "" : opd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DoctorUserName = opd.PrimaryDoctor?.Username,
                NurseUserName = opd.PrimaryNurse?.Username,
                DiagnosisAndICD = GetVisitDiagnosisAndICD(opd.Id, "OPD", true, getForAnesthesia: (opd.IsAnesthesia ? true : false)),
                Allergy = EMRVisitAllergy.GetOPDVisitAllergy(opd),
                UserNameReceive = opd.CreatedBy,
                SiteCode = opd.Site?.ApiCode,
                Site = opd.Site?.Name,
                LocationUnit = opd.Site?.LocationUnit,
                Location = opd.Site?.Location,
                Province = opd.Site?.Province,
                opd.IsAnesthesia, // đánh dấu khoa gây mê
                IsConsultation = opd.OPDOutpatientExaminationNote?.IsConsultation,
                opd.Version
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CustomerOPD/ManualUpdate/{id}")]
        [Permission(Code = "OCOPD3")]
        public async Task<IHttpActionResult> UpdateManualCustomerAPIAsync(Guid id, [FromBody] CustomerManualUpdateParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var opd = GetOPD(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var customer = opd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            
            if (!string.IsNullOrWhiteSpace(customer.PID))
            {
                await SyncHisCustomerAsync(customer.PID);
            }
            UpdateManualCustomer(opd, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/OPD/CustomerOPD/SyncVisitCode/{id}")]
        [Permission(Code = "OCOPD4")]
        public IHttpActionResult SyncVisitcodeAPI(Guid id)
        {
            var opd = GetOPD(id);
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

        //[HttpPost]
        //[CSRFCheck]
        //[Route("api/OPD/CustomerOPD/{id}")]
        //[Permission(Code = "OCOPD5")]
        //public IHttpActionResult UpdateCustomerHISAPI(Guid id, [FromBody] CustomerEDParameterModel request)
        //{
        //    if (!request.Validate())
        //        return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

        //    var opd = GetOPD(id);
        //    if (opd == null)
        //        return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

        //    var customer = opd.Customer;
        //    if (customer == null || customer.IsDeleted)
        //    {
        //        return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
        //    }
        //    if (!string.IsNullOrEmpty(customer.PID))
        //    {
        //        return Content(HttpStatusCode.BadRequest, Message.SYNCHRONIZED_ERROR);
        //    }
        //    var same_customer = GetLocalCustomer(request);
        //    InHospital in_hospital = new InHospital();
        //    var in_hospital_status_id = in_hospital.GetStatus("OPD").Id;
        //    if (same_customer != null)
        //    {
        //        if (same_customer.EDStatusId != in_hospital_status_id)
        //        {
        //            MergeAnonymousCustomer(opd, same_customer.Id, customer);
        //            return Content(HttpStatusCode.OK, Message.SUCCESS);
        //        }
        //        return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_IS_EXIST);
        //    }
        //    UpdateCustomer(customer, request, in_hospital_status_id);
        //    return Content(HttpStatusCode.OK, Message.SUCCESS);
        //}
        [HttpPost]
        [CSRFCheck]
        [Route("api/OPD/CustomerOPD/customer-form-anesthesia/{id}")]
        [Permission(Code = "EOC008")]
        public IHttpActionResult CreateCustomerFormOtherSpecialty(Guid id, [FromBody] CustomerEDParameterModel request)
        {
            var finded = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.GetById(id);
            if (finded == null || finded.IsAcceptNurse)
                return Content(HttpStatusCode.NotFound, Message.INFO_INCORRECT);

            var local_customer = GetCustomerByPid(request.PID);

            var user = GetUser();
            finded.IsAcceptPhysician = true;
            finded.IsAcceptNurse = true;
            finded.ReceivingNurseId = user?.Id;

            unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(finded);
            unitOfWork.Commit();

            Guid hand_over_check_list_id = id;
            var new_visit_id = CreateNewOPDFromHandOver(local_customer.Id, hand_over_check_list_id, request.VisitCode);

            return Content(HttpStatusCode.OK, new { Id = new_visit_id });
        }
        [HttpGet]
        [Route("api/OPD/CustomerOPD/SyncVisitCodeByPID/{pid}")]
        [Permission(Code = "OCOPD6")]
        public IHttpActionResult SyncVisitcodeByPIDAPI(string pid)
        {
            if (string.IsNullOrEmpty(pid))
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);
            var local_customer = GetCustomerByPid(pid);
            if (local_customer != null)
            {
                var specialty = GetSpecialty();
                var anesthesiaExit = PreAnesthesiaCustomerModel(specialty, pid);
                if (anesthesiaExit != null)
                    return Content(HttpStatusCode.BadGateway, new { ViMessage = "Yêu cầu tiếp nhận tồn tại", EnMessage = "Yêu cầu tiếp nhận tồn tại", TransferExit = true, TransferData = anesthesiaExit });
            }
            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetVisitCode(pid, "OPD"));
            return Content(HttpStatusCode.OK, OHClient.GetVisitCode(pid, "OPD"));
        }

        private Customer GetLocalCustomer(CustomerEDParameterModel request)
        {
            if (request.PID == null)
                return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Fullname == request.Fullname && e.DateOfBirth == request.ConvertedDateOfBirth && e.Gender == request.Gender);
            return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.PID == request.PID);
        }
        private void SyncCustomer(Customer customer, CustomerEDParameterModel request)
        {
            customer.Fullname = request.Fullname;
            customer.DateOfBirth = request.ConvertedDateOfBirth;
            customer.Gender = request.ConvertedGender;
            unitOfWork.CustomerRepository.Update(customer);
            unitOfWork.Commit();
        }

        private void UpdateManualCustomer(OPD opd, CustomerManualUpdateParameterModel request)
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
                if (opd.GroupId == null)
                {
                    opd.VisitCode = request.VisitCode;
                    opd.HealthInsuranceNumber = request.HealthInsuranceNumber;
                    opd.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
                    opd.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;
                    unitOfWork.OPDRepository.Update(opd);
                }
                else
                {
                    var group_visit = unitOfWork.OPDRepository.Find(
                        e => !e.IsDeleted &&
                        e.GroupId != null &&
                        e.GroupId == opd.GroupId
                    );
                    foreach (var opd_visit in group_visit)
                    {
                        opd_visit.VisitCode = request.VisitCode;
                        opd_visit.HealthInsuranceNumber = request.HealthInsuranceNumber;
                        opd_visit.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
                        opd_visit.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;
                        unitOfWork.OPDRepository.Update(opd_visit);
                    }
                }
                unitOfWork.Commit();
            }
        }

        //private void MergeAnonymousCustomer(OPD opd, Guid local_customer_id, Customer current_customer)
        //{
        //    opd.CustomerId = local_customer_id;
        //    unitOfWork.OPDRepository.Update(opd);

        //    current_customer.IsDeleted = true;
        //    unitOfWork.CustomerRepository.Update(current_customer);
        //    unitOfWork.Commit(); ;
        //}

       
        private bool CheckIsBlock(dynamic visit, Guid? formId = null)
        {
            var user = GetUser();
            var is_block_after_24h = IsBlockAfter24h(visit.CreatedAt, formId);
            var has_unlock_permission = HasUnlockPermission(visit.Id, "", user.Username, formId);
            if (!has_unlock_permission && is_block_after_24h)
                return true;
            return false;
        }
        protected Guid CreateNewOPDFromHandOver(Guid customer_id, Guid hand_over_check_list_id, string visitcode)
        {
            InHospital in_hospital = new InHospital();
            var specialty = GetSpecialty();
            bool isAnesthesia = specialty == null ? false : specialty.IsAnesthesia;
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital.GetStatus("OPD").Id,
                customer_id,
                hand_over_check_list_id,
                null,
                visitcode,
                GetSiteId(),
                GetSpecialtyId(),
                GetUser().Id,
                isAnesthesia
            );
            var opd = creater.CreateNewOPD();
            opd.TransferFromId = hand_over_check_list_id;
            unitOfWork.OPDRepository.Update(opd);
            unitOfWork.Commit();
            return opd.Id;
        }
        //protected bool GetOPDPreAnesthesiaHandOverVisit(Guid specialty_id, Guid Id)
        //{
        //    var ischeck = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.ReceivingUnitNurseId == specialty_id && e.Id == Id && !e.IsAcceptPhysician && !e.IsAcceptNurse);
        //    if (ischeck != null) return true;
        //    return false;
        //}

    }
}
