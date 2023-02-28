using AutoMapper;
using DataAccess.Models;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class HISCustomerController : BaseApiController
    {
        [HttpGet]
        [Route("api/HISCustomer")]
        [Permission(Code = "GHISC1")]
        public IHttpActionResult GetCustomers([FromUri]HISCustomerParameterModel request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PID))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            //if (ConfigurationManager.AppSettings["HiddenError"].Equals("false") && IsSuperman() && !string.IsNullOrEmpty(request.PID))
            //{
            //    var customer = GetCustomerByPid(request.PID);
            //    return Content(HttpStatusCode.OK, EHosClient.FakeBuildShortResultV2(customer));
            //}HISCustomer

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
            {
                if (!string.IsNullOrEmpty(request.PID))
                    return Content(HttpStatusCode.OK, EHosClient.searchPatienteHos(new SearchParameter { PID = request.PID }));
                else if (!string.IsNullOrEmpty(request.DateOfBirth))
                    return Content(HttpStatusCode.OK, EHosClient.searchPatienteHos(new SearchParameter { TenBenhNhan = request.Fullname,GioiTinh = request.Gender.ToString(), NgaySinh = request.DateOfBirth }));
                else
                    return Content(HttpStatusCode.OK, EHosClient.searchPatienteHos(new SearchParameter { TenBenhNhan = request.Fullname}));
            }
            else
            {
                if (!string.IsNullOrEmpty(request.PID))
                    return Content(HttpStatusCode.OK, OHClient.searchPatienteOh(new SearchParameter { PID = request.PID }));
                else if (!string.IsNullOrEmpty(request.DateOfBirth))
                    return Content(HttpStatusCode.OK, OHClient.searchPatienteOh(new SearchParameter { TenBenhNhan = request.Fullname, GioiTinh = request.Gender.ToString(), NgaySinh = request.DateOfBirth }));
                else
                    return Content(HttpStatusCode.OK, OHClient.searchPatienteOh(new SearchParameter { TenBenhNhan = request.Fullname}));
            }
        }

        [HttpGet]
        [Route("api/HISCustomer/Sync/{id}")]
        [Permission(Code = "GHISC2")]
        public IHttpActionResult SynsCustomer(Guid id)
        {
            var ed = unitOfWork.EDRepository.GetById(id);
            if (ed == null || ed.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

            int gender = customer.Gender ?? 1;
            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
                return Ok(EHosClient.searchPatienteHos(new SearchParameter {
                    TenBenhNhan = customer.Fullname,
                    GioiTinh = gender.ToString(),
                    NgaySinh = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT)
                }));
            else
                return Ok(OHClient.searchPatienteOh(new SearchParameter
                {
                    TenBenhNhan = customer.Fullname,
                    GioiTinh = gender.ToString(),
                    NgaySinh = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT)
                }));
        }
        [HttpGet]
        [Route("api/HISCustomer/Sync/PID/{pid}")]
        public IHttpActionResult SynsCustomerWithPID(string pid)
        {
            List<dynamic> customers = new List<dynamic>();
            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            else if (site_code == "times_city")
            {
                customers = EHosClient.searchPatienteHos(new SearchParameter { PID = pid });
            }
            else
            {
                customers = OHClient.searchPatienteOh(new SearchParameter { PID = pid });
            }
            if(customers.Count != 1)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            var customerDb = unitOfWork.CustomerRepository.FirstOrDefault(x => x.PID == pid);
            if(customerDb == null)
            {
                customerDb = new Customer()
                {
                    PID = pid,
                };
                unitOfWork.CustomerRepository.Add(customerDb);
                unitOfWork.Commit();
            }

            Mapper.Map(MapCustomerInformationFromHIS(customers[0]), customerDb);
            unitOfWork.CustomerRepository.Update(customerDb);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Mapper.Map<CustomerDto>(customerDb));
        }
        private CustomerManualUpdateParameterModel MapCustomerInformationFromHIS(dynamic his_customer)
        {
            var listProvince = unitOfWork.MasterDataRepository.AsQueryable().Where(s => s.Level == 1 && s.Form == "GENLPCG").ToList();
            var Province = !string.IsNullOrEmpty(his_customer.Province) ? listProvince.FirstOrDefault(s => s.Data.ToLower().Contains(his_customer.Province.ToLower())) : null;
            var listDistrict = unitOfWork.MasterDataRepository.AsQueryable().Where(s => s.Level == 2 && s.Form == "GENLPCG").ToList();
            var District = string.IsNullOrEmpty(his_customer.District) ? null : Province != null ? listDistrict.Where(s => s.Group == Province.Code).FirstOrDefault(s => s.Data.ToLower().Contains(his_customer.District.ToLower())) :
                his_customer.District != null ? listDistrict.FirstOrDefault(s => s.Data.ToLower().Contains(his_customer.District.ToLower())) : null;
            var listEth = unitOfWork.MasterDataRepository.AsQueryable().Where(s => s.Form == "GENETHN").ToList();
            var Ethnic = !string.IsNullOrEmpty(his_customer.Fork) ? listEth.FirstOrDefault(s => s.Data.ToLower().Contains(his_customer.Fork.ToLower())) : null;
            var listJob = unitOfWork.MasterDataRepository.AsQueryable().Where(s => s.Form == "GENJOBB").ToList();
            var Job = !string.IsNullOrEmpty(his_customer.Job) ? listJob.FirstOrDefault(s => s.Data.ToLower().Contains(his_customer.Job.ToLower())) : null;

            var result = new CustomerManualUpdateParameterModel
            {
                PID = his_customer.PID,
                Fullname = his_customer.Fullname,
                DateOfBirth = his_customer.DateOfBirth,
                Address = his_customer.Address,
                HealthInsuranceNumber = his_customer.HealthInsuranceNumber,
                StartHealthInsuranceDate = his_customer.StartHealthInsuranceDate,
                ExpireHealthInsuranceDate = his_customer.ExpireHealthInsuranceDate,
                Fork = his_customer.Fork,
                Gender = his_customer.Gender,
                IdentificationCard = his_customer.IdentificationCard,
                IssueDate = his_customer.IssueDate,
                IssuePlace = his_customer.IssuePlace,
                Nationality = his_customer.Nationality,
                WorkPlace = his_customer.WorkPlace,
                Relationship = his_customer.Relationship,
                RelationshipAddress = his_customer.RelationshipAddress,
                RelationshipContact = his_customer.RelationshipContact,
                RelationshipKind = his_customer.RelationshipKind,
                Job = his_customer.Job,
                Phone = his_customer.Phone,
                MOHAddress = his_customer.SoNha,
                MOHDistrict = District?.ViName,
                MOHDistrictCode = District?.Note,
                MOHProvince = Province?.ViName,
                MOHProvinceCode = Province?.Note,
                MOHEthnic = Ethnic?.ViName,
                MOHEthnicCode = Ethnic?.Note,
                MOHJob = Job?.ViName,
                MOHJobCode = Job?.Note,
                MOHObject = !string.IsNullOrEmpty(his_customer.HealthInsuranceNumber) ? "1" : his_customer.Object
            };
            return result;
        }
    }
}
