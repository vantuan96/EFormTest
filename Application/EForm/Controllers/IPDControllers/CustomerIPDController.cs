using DataAccess.Models;
using DataAccess.Models.IPDModel;
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
using EForm.Controllers.GeneralControllers;
using System.Linq;
using static EForm.Controllers.IPDControllers.IPDSetupMedicalRecordController;
using System.Text.RegularExpressions;
using System.Text;
using EForm.Models.IPDModels;
using DataAccess.Models.OPDModel;
using DataAccess.Models.EDModel;
using EMRModels;
using Helper;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class CustomerIPDController : BaseIPDApiController
    {
        private InHospital in_hospital = new InHospital();

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CustomerIPD")]
        [Permission(Code = "ICIPD1")]
        public IHttpActionResult CreateCustomerAPI([FromBody] CustomerEDParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var Specialty = GetSpecialty();
            if (Specialty.VisitTypeGroup.Code != "IPD")
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_ACCOUNT_SPE_INVALID);
            var in_hospital_status_id = this.in_hospital.GetStatus("IPD").Id;
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital_status_id,
                null,
                null,
                null,
                request.VisitCode,
                GetSiteId(),
                Specialty.Id,
                GetUser().Id
            );

            var local_customer = GetLocalCustomer(request);
            if (local_customer == null)
            {
                var customer = CreateNewCustomer(request, in_hospital_status_id);
                creater.SetCustomerId(customer.Id);
                var new_ipd = creater.CreateNewIPD();
                SyncDoctor(new_ipd);
                SyncInfoFromHis(customer);
                return Content(HttpStatusCode.OK, new { new_ipd.Id });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(request.PID))
                {
                    UpdateCustomer(local_customer, request, in_hospital_status_id);
                    SyncInfoFromHis(local_customer);
                }
            }

            var has_draft_in_hospital = GetDraftByPid(local_customer.Id);
            if (has_draft_in_hospital != null) return Content(HttpStatusCode.BadRequest, has_draft_in_hospital);

            this.in_hospital.SetState(local_customer.Id, null, null, null);
            var in_hospital_visit = this.in_hospital.GetVisit();
            if (in_hospital_visit != null)
                return Content(HttpStatusCode.BadRequest, this.in_hospital.BuildErrorMessage(in_hospital_visit));

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
            var ipd = creater.CreateNewIPD();
            SyncDoctor(ipd);
            // UpdateManualCustomer(ipd.Customer, ipd, MapCustomerInformationFromHIS(his_customer, ipd));
            return Content(HttpStatusCode.OK, new { ipd.Id });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CustomerIPD/Draft")]
        [Permission(Code = "ICIPD1DRAFT")]
        public IHttpActionResult CreateDraftCustomerAPI([FromBody] CustomerEDParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var Specialty = GetSpecialty();
            if (Specialty.VisitTypeGroup.Code != "IPD")
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_ACCOUNT_SPE_INVALID);


            var in_hospital_status_id = this.in_hospital.GetStatus("IPD").Id;
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital_status_id,
                null,
                null,
                null,
                request.VisitCode,
                GetSiteId(),
                Specialty.Id,
                GetUser().Id
            );

            var local_customer = GetLocalCustomer(request);
            if (local_customer == null)
            {
                var customer = CreateNewCustomer(request, in_hospital_status_id);
                var has_draft_in_hospital1 = GetDraftByPid(customer.Id);
                if (has_draft_in_hospital1 != null) return Content(HttpStatusCode.BadRequest, has_draft_in_hospital1);
                creater.SetCustomerId(customer.Id);
                var new_ipd = creater.CreateDraftIPD();
                SyncDoctor(new_ipd);
                SyncInfoFromHis(customer);
                return Content(HttpStatusCode.OK, new { new_ipd.Id });
            }
            else
            {
                UpdateCustomer(local_customer, request, in_hospital_status_id);
                SyncInfoFromHis(local_customer);
            }

            this.in_hospital.SetState(local_customer.Id, null, null, null);
            var in_hospital_visit = this.in_hospital.GetVisit();
            if (in_hospital_visit != null)
                return Content(HttpStatusCode.BadRequest, this.in_hospital.BuildErrorMessage(in_hospital_visit));

            var has_draft_in_hospital = GetDraftByPid(local_customer.Id);
            if (has_draft_in_hospital != null) return Content(HttpStatusCode.BadRequest, has_draft_in_hospital);
            creater.SetCustomerId(local_customer.Id);
            var ipd = creater.CreateDraftIPD();
            SyncDoctor(ipd);
            return Content(HttpStatusCode.OK, new { ipd.Id });
        }

        private object GetDraftByPid(Guid id)
        {
            var draft = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.IsDraft && e.CustomerId == id).FirstOrDefault();
            if (draft == null) return null;
            return new
            {
                ViMessage = $"Bệnh nhân đang có hồ sơ nháp tại {draft.Specialty?.ViName?.ToLower()} {draft.Specialty?.Site?.Name}",
                EnMessage = $"Bệnh nhân đang có hồ sơ nháp tại {draft.Specialty?.ViName?.ToLower()} {draft.Specialty?.Site?.Name}",
                NeedShowMsg = true
            };
        }
        private object GetOtherDraftByPid(Guid id, Guid VisitId)
        {
            var draft = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.IsDraft && e.CustomerId == id && e.Id != VisitId).FirstOrDefault();
            if (draft == null) return null;
            return new
            {
                ViMessage = $"Bệnh nhân đang có hồ sơ nháp tại {draft.Specialty?.ViName?.ToLower()} {draft.Specialty?.Site?.Name}",
                EnMessage = $"Bệnh nhân đang có hồ sơ nháp tại {draft.Specialty?.ViName?.ToLower()} {draft.Specialty?.Site?.Name}",
                NeedShowMsg = true
            };
        }
        [HttpGet]
        [Route("api/IPD/CustomerIPD/{id}/ext")]
        [Permission(Code = "ICIPD2")]
        public IHttpActionResult DetailCustomerAPIEXT(Guid id, [FromUri]CustomerRequetDetailModel request)
        {
            //SynsIPDMedicalRecordOfPatient();
            var ipd = GetIPDData(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var vip_info = GetIPDVipInfo(ipd);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }

            if (IsDoctor() && request.readonlyview != true) {
                var is_waiting_accept_error_message = GetWaitingDoctorAcceptMessage(ipd);
                if (is_waiting_accept_error_message != null)
                {
                    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);
                }
            }
            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            
            return Content(HttpStatusCode.OK, new
            {
                customer.Id,
                customer.IsVip,
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
                IPDId = ipd.Id,
                ipd.RecordCode,
                AdmittedDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ipd.VisitCode,
                ipd.HealthInsuranceNumber,
                StartHealthInsuranceDate = ipd.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = ipd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                customer.MOHJob,
                customer.MOHJobCode,
                customer.MOHEthnic,
                customer.MOHEthnicCode,
                customer.MOHNationality,
                customer.MOHNationalityCode,
                MOHAddress = string.IsNullOrWhiteSpace(customer.MOHAddress) ? customer.Address : customer.MOHAddress,
                customer.MOHProvince,
                customer.MOHProvinceCode,
                customer.MOHDistrict,
                customer.MOHDistrictCode,
                customer.MOHObject,
                customer.MOHObjectOther,
                Link = new List<dynamic>(),
                customer.Height,
                customer.Weight,
                ipd.IsDraft,
                customer.RelationshipAddress,
                UserNameReceive = ipd.CreatedBy,
                ipd.Version
            }) ;
        }
        [HttpGet]
        [Route("api/IPD/CustomerIPD/{id}")]
        [Permission(Code = "ICIPD2")]
        public IHttpActionResult DetailCustomerAPI(Guid id, [FromUri] CustomerRequetDetailModel request)
        {
            //SynsIPDMedicalRecordOfPatient();
            var ipd = GetIPDData(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var vip_info = GetIPDVipInfo(ipd);
            if (vip_info != null)
            {
                return Content(HttpStatusCode.Forbidden, vip_info);
            }

            if (IsDoctor() && request.readonlyview != true)
            {
                var is_waiting_accept_error_message = GetWaitingDoctorAcceptMessage(ipd);
                if (is_waiting_accept_error_message != null)
                {
                    return Content(HttpStatusCode.OK, is_waiting_accept_error_message);
                }
            }
            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            var site = ipd.Site;
            var DiagnosisAndICD = GetVisitDiagnosisAndICD(id, "IPD", false);

            var forms_setup = GetSpecialtySetupForm((Guid)ipd.SpecialtyId);
            var list_form_final = GetMedicalRecordsSavedOrSetup(id, forms_setup);

            var specialty = ipd.Specialty;
            var isVisitLastUpdate = IsForNeonatalMaternityV2((DateTime)ipd.CreatedAt, GetAppConfig("HIDE_A02_016_050919_VE")); // check thời gian tiếp nhận visit sau cập nhật

            return Content(HttpStatusCode.OK, new
            {
                site?.Location,
                Site = site?.Name,
                DiagnosisAndICD,
                site?.Province,
                site?.LocationUnit,
                customer.Id,
                customer.IsVip,
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
                IPDId = ipd.Id,
                ipd.RecordCode,
                AdmittedDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ipd.VisitCode,
                ipd.HealthInsuranceNumber,
                StartHealthInsuranceDate = ipd.StartHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                ExpireHealthInsuranceDate = ipd.ExpireHealthInsuranceDate?.ToString(Constant.DATE_FORMAT),
                customer.MOHJob,
                customer.MOHJobCode,
                customer.MOHEthnic,
                customer.MOHEthnicCode,
                customer.MOHNationality,
                customer.MOHNationalityCode,
                MOHAddress = string.IsNullOrWhiteSpace(customer.MOHAddress) ? customer.Address : customer.MOHAddress,
                customer.MOHProvince,
                customer.MOHProvinceCode,
                customer.MOHDistrict,
                customer.MOHDistrictCode,
                customer.MOHObject,
                customer.MOHObjectOther,
                Allergy = EMRVisitAllergy.GetIPDVisitAllergy(ipd),
                IsLocked = IPDIsBlock(ipd, ""),
                Link = new List<dynamic>(),
                Age = CaculatorAgeCustormer(customer?.DateOfBirth),
                Status = new { EnName = ipd.EDStatus.EnName, ViName = ipd.EDStatus.ViName, Code = ipd.EDStatus.Code },
                MedicalRecordType = list_form_final,
                customer.Height,
                customer.Weight,
                Danhgiabandau = CheckPromissoryNote(ipd.Id, forms_setup),
                ipd.IsDraft,
                Specialty = new { ViName = specialty?.ViName, EnName = specialty?.EnName },
                customer.RelationshipAddress,
                DischargeDate = (Constant.InHospital.Contains(ipd.EDStatus.Code) || Constant.WaitingResults.Contains(ipd.EDStatus.Code)) ? "" : ipd.DischargeDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                DoctorUserName = ipd.PrimaryDoctor?.Username,
                NurseUserName = ipd.PrimaryNurse?.Username,
                DiagnosisDischagre = GetVisitDiagnosisAndICD(ipd.Id, "IPD", true),
                SiteCode = ipd.Site?.ApiCode,
                UserNameReceive = ipd.CreatedBy,
                HideFormNewborn = isVisitLastUpdate, // ẩn form đánh giá ban đầu trẻ vừa sinh visit mới
                ipd.Version
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/CustomerIPD/ManualUpdate/{id}")]
        [Permission(Code = "ICIPD3")]
        public IHttpActionResult UpdateManualCustomerAPI(Guid id, [FromBody] CustomerManualUpdateParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            customer.Fork = request.MOHEthnic;
            customer.Job = request.MOHJob;

            UpdateDataCustomer(customer, ipd, request);
            SyncDoctor(ipd);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/CustomerIPD/SyncVisitCode/{id}")]
        [Permission(Code = "ICIPD4")]
        public IHttpActionResult SyncVisitcodeAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetVisitCode(customer.PID, "IPD"));
            return Content(HttpStatusCode.OK, OHClient.GetVisitCode(customer.PID, "IPD"));
        }

        [HttpGet]
        [Route("api/IPD/CustomerIPD/SyncVisitCodeByPID/{pid}")]
        [Permission(Code = "ICIPD6")]
        public IHttpActionResult SyncVisitcodeByPIDAPI(string pid)
        {
            if (string.IsNullOrEmpty(pid))
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Content(HttpStatusCode.OK, EHosClient.GetVisitCode(pid, "IPD"));
            return Content(HttpStatusCode.OK, OHClient.GetVisitCode(pid, "IPD"));
        }

        [HttpGet]
        [Route("api/IPD/CustomerIPD/HisCustomer/{id}/{pid}")]
        public IHttpActionResult SyncCustomerInfo(Guid id, string pid)
        {
            if (string.IsNullOrEmpty(pid)) Content(HttpStatusCode.BadRequest, Message.PID_INVALID);
            var his_customers = OHClient.searchPatienteOh(new SearchParameter { PID = pid });

            if (his_customers.Count == 0) return Content(HttpStatusCode.BadRequest, Message.PID_INVALID);

            var his_customer = his_customers[0];

            var cus = GetOrCreateCustomerByPid(his_customer, pid);

            this.in_hospital.SetState(cus.Id, id, null, null);
            var in_hospital_visit = this.in_hospital.GetVisit();
            if (in_hospital_visit != null)
                return Content(HttpStatusCode.BadRequest, this.in_hospital.BuildErrorMessage(in_hospital_visit));

            var has_draft_in_hospital = GetOtherDraftByPid(cus.Id, id);
            if (has_draft_in_hospital != null) return Content(HttpStatusCode.BadRequest, has_draft_in_hospital);


            if (his_customers.Count == 0) return Content(HttpStatusCode.BadRequest, Message.PID_INVALID);

            return Content(HttpStatusCode.OK, new List<dynamic> { his_customer });
        }
        [HttpGet]
        [Route("api/IPD/CustomerIPD/SyncInfo/{id}")]
        public IHttpActionResult SyncCustomerInfo(Guid id, [FromUri] SearchParameter request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            string pid = customer.PID;

            if (ipd.IsDraft)
            {
                pid = request.PID;
            }

            var his_customers = new List<dynamic>();

            if (string.IsNullOrEmpty(pid)) Content(HttpStatusCode.BadRequest, Message.PID_INVALID);

            if (!string.IsNullOrEmpty(pid))
                his_customers = OHClient.searchPatienteOh(new SearchParameter { PID = pid });

            if (his_customers.Count == 0) return Content(HttpStatusCode.BadRequest, Message.PID_INVALID);

            var his_customer = his_customers[0];

            if (request.PID != customer.PID && ipd.IsDraft)
            {
                var cus = GetOrCreateCustomerByPid(his_customer, request.PID);

                this.in_hospital.SetState(cus.Id, id, null, null);
                var in_hospital_visit = this.in_hospital.GetVisit();
                if (in_hospital_visit != null)
                    return Content(HttpStatusCode.BadRequest, this.in_hospital.BuildErrorMessage(in_hospital_visit));

                var has_draft_in_hospital = GetOtherDraftByPid(cus.Id, id);
                if (has_draft_in_hospital != null) return Content(HttpStatusCode.BadRequest, has_draft_in_hospital);

                ipd.Customer = cus;
                ipd.CustomerId = cus.Id;
                unitOfWork.Commit();
            }

            UpdateManualCustomer(ipd.Customer, ipd, MapCustomerInformationFromHIS(his_customer, ipd));
            return Content(HttpStatusCode.OK, new { });
        }
        private object[] CheckPromissoryNote(Guid visitId, List<MasterData> list_setup)
        {
            List<MasterData> forms_saved = GetFormPatienSaved(visitId);

            object[] result = list_setup.Concat(forms_saved).GroupBy(e => new { e.Code, e.Form }).Select(e => e.FirstOrDefault()).Where(e => e.Level == 3).OrderBy(o => o.Order).Select(e => new { e.ViName, e.Code, e.Form, Status = true }).ToArray();
            return result;
        }
        private Customer GetLocalCustomer(CustomerEDParameterModel request)
        {
            if (request.PID == null)
                return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Fullname == request.Fullname && e.DateOfBirth == request.ConvertedDateOfBirth && e.Gender == request.Gender);
            return unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.PID == request.PID);
        }

        private Customer CreateNewCustomer(CustomerEDParameterModel request, Guid in_hospital_status_id)
        {
            Customer customer = new Customer
            {
                PID = request.PID,
                Fullname = request.Fullname,
                DateOfBirth = request.ConvertedDateOfBirth,
                Phone = request.Phone,
                Gender = request.ConvertedGender,
                Job = request.Job,
                WorkPlace = request.WorkPlace,
                Relationship = request.Relationship,
                RelationshipContact = request.RelationshipContact,
                Address = request.Address,
                Nationality = request.Nationality,
                IsVip = request.IsVip,
                EDStatusId = in_hospital_status_id
            };
            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();

            return customer;
        }

        private Customer UpdateCustomer(Customer customer, CustomerEDParameterModel request, Guid in_hospital_status_id)
        {
            var current_customer = JsonConvert.SerializeObject(new
            {
                customer.PID,
                customer.Fullname,
                customer.DateOfBirth,
                customer.Phone,
                customer.Gender,
                customer.Job,
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.Address,
                customer.Nationality,
                customer.EDStatusId,
                customer.MOHJob,
                customer.MOHJobCode,
                customer.MOHEthnic,
                customer.MOHEthnicCode,
                customer.MOHNationality,
                customer.MOHNationalityCode,
                customer.MOHProvince,
                customer.MOHProvinceCode,
                customer.MOHDistrict,
                customer.MOHDistrictCode,
                customer.MOHObject,
                customer.MOHObjectOther,
                customer.IsVip,
                customer.IdentificationCard
            });
            var update_customer = JsonConvert.SerializeObject(new
            {
                request.PID,
                request.Fullname,
                DateOfBirth = request.ConvertedDateOfBirth,
                request.Phone,
                Gender = request.ConvertedGender,
                request.Job,
                request.WorkPlace,
                request.Relationship,
                request.RelationshipContact,
                request.Address,
                request.Nationality,
                EDStatusId = in_hospital_status_id,
                request.MOHJob,
                request.MOHJobCode,
                request.MOHEthnic,
                request.MOHEthnicCode,
                request.MOHNationality,
                request.MOHNationalityCode,
                request.MOHProvince,
                request.MOHProvinceCode,
                request.MOHDistrict,
                request.MOHDistrictCode,
                request.MOHObject,
                request.MOHObjectOther,
                request.IsVip,
                request.IdentificationCard
            });
            if (!current_customer.Equals(update_customer))
            {
                customer.PID = request.PID;
                customer.Fullname = request.Fullname;
                customer.DateOfBirth = request.ConvertedDateOfBirth;
                customer.Phone = request.Phone;
                customer.Gender = request.ConvertedGender;
                customer.Job = request.Job;
                customer.WorkPlace = request.WorkPlace;
                customer.Relationship = request.Relationship;
                customer.RelationshipContact = request.RelationshipContact;
                customer.Address = request.Address;
                customer.Nationality = request.Nationality;
                customer.EDStatusId = in_hospital_status_id;
                customer.MOHJob = request.MOHJob;
                customer.MOHJobCode = request.MOHJobCode;
                customer.MOHEthnic = request.MOHEthnic;
                customer.MOHEthnicCode = request.MOHEthnicCode;
                customer.MOHNationality = request.MOHNationality;
                customer.MOHNationalityCode = request.MOHNationalityCode;
                customer.MOHProvince = request.MOHProvince;
                customer.MOHProvinceCode = request.MOHProvinceCode;
                customer.MOHDistrict = request.MOHDistrict;
                customer.MOHDistrictCode = request.MOHDistrictCode;
                customer.MOHObject = request.MOHObject;
                customer.MOHObjectOther = request.MOHObjectOther;
                customer.IsVip = request.IsVip;
                customer.IdentificationCard = request.IdentificationCard;
                customer.IssueDate = request.ConvertedIssueDate;
                customer.IssuePlace = request.IssuePlace;
                unitOfWork.CustomerRepository.Update(customer);
                unitOfWork.Commit();
                UpdateRecordCodeOfCustomer(customer.Id);
            }
            return customer;
        }
        private void UpdateDataCustomer(Customer customer, IPD ipd, CustomerManualUpdateParameterModel request)
        {
            //customer.MOHJob = request.MOHJob;
            //customer.MOHJobCode = request.MOHJobCode;
            //customer.MOHEthnic = request.MOHEthnic;
            //customer.MOHEthnicCode = request.MOHEthnicCode;
            customer.MOHNationality = request.MOHNationality;
            customer.MOHNationalityCode = request.MOHNationalityCode;
            customer.MOHObject = request.MOHObject;
            customer.MOHObjectOther = request.MOHObjectOther;

            unitOfWork.CustomerRepository.Update(customer);

            ipd.VisitCode = request.VisitCode;
            ipd.HealthInsuranceNumber = request.HealthInsuranceNumber;
            ipd.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
            ipd.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;

            if (ipd.IsDraft && !string.IsNullOrWhiteSpace(request.VisitCode))
            {
                ipd.IsDraft = false;
            }
            unitOfWork.IPDRepository.Update(ipd);
            unitOfWork.Commit();
            // customer.MOHAddress = string.IsNullOrWhiteSpace(request.MOHAddress) ? request.Address : request.MOHAddress;
        }
        private void UpdateManualCustomer(Customer customer, IPD ipd, CustomerManualUpdateParameterModel request)
        {
            customer.Fullname = request.Fullname;
            customer.DateOfBirth = request.ConvertedDateOfBirth;
            customer.Phone = request.Phone;
            customer.Gender = request.ConvertedGender;
            customer.Fork = request.Fork;
            customer.Job = request.Job;
            customer.WorkPlace = request.WorkPlace;
            customer.Relationship = request.Relationship;
            customer.RelationshipContact = request.RelationshipContact;
            customer.RelationshipAddress = request.RelationshipAddress;
            customer.RelationshipKind = request.RelationshipKind;
            customer.Address = request.Address;
            customer.Nationality = request.Nationality;
            customer.IdentificationCard = request.IdentificationCard;
            customer.IssueDate = request.ConvertedIssueDate;
            customer.IssuePlace = request.IssuePlace;
            customer.MOHAddress = request.MOHAddress;
            customer.MOHProvince = request.MOHProvince;
            customer.MOHProvinceCode = request.MOHProvinceCode;
            customer.MOHDistrict = request.MOHDistrict;
            customer.MOHDistrictCode = request.MOHDistrictCode;

            customer.MOHJob = request.MOHJob;
            customer.MOHJobCode = request.MOHJobCode;

            customer.MOHEthnic = request.MOHEthnic;
            customer.MOHEthnicCode = request.MOHEthnicCode;

            customer.IsVip = request.IsVip;
            unitOfWork.CustomerRepository.Update(customer);
            unitOfWork.Commit();
            UpdateRecordCodeOfCustomer(customer.Id);

            ipd.VisitCode = request.VisitCode;
            ipd.HealthInsuranceNumber = string.IsNullOrEmpty(ipd.HealthInsuranceNumber) ? request.HealthInsuranceNumber : ipd.HealthInsuranceNumber;
            ipd.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
            ipd.ExpireHealthInsuranceDate = string.IsNullOrEmpty(ipd.HealthInsuranceNumber?.ToString()) ? request.ConvertedExpireHealthInsuranceDate : ipd.ExpireHealthInsuranceDate;
            unitOfWork.IPDRepository.Update(ipd);
            unitOfWork.Commit();

            if (ipd.IsDraft && !string.IsNullOrWhiteSpace(request.VisitCode))
            {
                ipd.IsDraft = false;
                unitOfWork.IPDRepository.Update(ipd);
                unitOfWork.Commit();
            }
            SyncDoctor(ipd);
        }

        private void MergeAnonymousCustomer(IPD ipd, Guid local_customer_id, Customer current_customer)
        {
            ipd.CustomerId = local_customer_id;
            unitOfWork.IPDRepository.Update(ipd);

            current_customer.IsDeleted = true;
            unitOfWork.CustomerRepository.Update(current_customer);
            unitOfWork.Commit(); ;
        }
    }
}
