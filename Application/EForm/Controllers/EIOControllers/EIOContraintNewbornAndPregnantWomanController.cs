using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class EIOContraintNewbornAndPregnantWomanController : BaseApiController
    {
        [HttpGet]
        [Route("api/EIO/ContraintNewbornAndPregnantWoman/GetList/{visitId}/{visitTypeCode}/{formCode}")]
        public IHttpActionResult GetListFormId(Guid visitId, string visitTypeCode, string formCode)
        {
            var visit = GetVisit(visitId, visitTypeCode);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var tabs = GetListTabNewborn(visitId, visitTypeCode, formCode);

            return Content(HttpStatusCode.OK, tabs);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EIO/ContraintNewbornAndPregnantWoman/Created/{visitId}/{visitTypeCode}/{formCode}")]
        public IHttpActionResult CreatedTabNewborn(Guid visitId, string visitTypeCode, string formCode)
        {
            var visit = GetVisit(visitId, visitTypeCode);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            EIOConstraintNewbornAndPregnantWoman form = new EIOConstraintNewbornAndPregnantWoman()
            {
                VisitId = visitId,
                NewbornCustomerId = null,
                VisitTypeCode = visitTypeCode,
                PregnantWomanCustomerId = visit.CustomerId,
                FormCode = formCode
            };
            unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.Add(form);
            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, new 
            {
                Id = form.Id,
                form.CreatedAt,
                form.CreatedBy,
                form.UpdatedAt,
                form.UpdatedBy
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EIO/ContraintNewbornAndPregnantWoman/Updated/{id}/{*PID}")]
        public IHttpActionResult CreatedTabNewborn(Guid id, string PID)
        {
            var form = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (!string.IsNullOrEmpty(PID))
            {
                var customer_newborn = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.PID == PID && e.PID != null);
                var customer_update = UpdateCustomerFromOHToEform(PID, customer_newborn);
                if (customer_update == null)
                    return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
                if (string.IsNullOrEmpty(customer_update.PID))
                    return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);

                var check_unique = (from e in unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.AsQueryable()
                                    where !e.IsDeleted && e.VisitId == form.VisitId && e.FormCode == form.FormCode
                                    && e.Id != form.Id && e.NewbornCustomerId != null && e.NewbornCustomerId == customer_update.Id
                                    select e).FirstOrDefault();
                if (check_unique != null)
                    return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_IS_EXIST);

                form.NewbornCustomerId = customer_update.Id;
                unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.Update(form);
                unitOfWork.Commit();
            }
            else
            {
                form.NewbornCustomerId = null;
                unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.Update(form);
                unitOfWork.Commit();
            }    
           
            return Content(HttpStatusCode.OK, new
            {
                Id = form.Id,
                NewbornCustomerId = form.NewbornCustomerId,
                FullName = form.NewbornCustomer?.Fullname,
                Gender = form.NewbornCustomer?.Gender,
                DateOfBirth = form.NewbornCustomer?.DateOfBirth,
                PID = form.NewbornCustomer?.PID,
                form.CreatedAt,
                form.CreatedBy,
                form.UpdatedAt,
                form.UpdatedBy
            });
        }

        public List<dynamic> GetListTabNewborn(Guid visitId, string visitTypeCode, string formCode)
        {
            var tab = (from e in unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.AsQueryable()
                       where !e.IsDeleted && e.VisitId == visitId
                       && e.VisitTypeCode == visitTypeCode && e.FormCode == formCode
                       orderby e.CreatedAt
                       select new
                       {
                           A = e,
                           B = e.NewbornCustomer
                       }).ToList();

            var render = (from t in tab
                          orderby t.A.CreatedAt
                          select new
                          {
                              Id = t.A.Id,
                              FullNameNewborn = t.B == null ? null : t.B.Fullname,
                              Gender = t.B == null ? null : t.B.Gender,
                              DateOfBirth = t.B == null ? null : t.B.DateOfBirth,
                              PID = t.B == null ? null : t.B.PID,
                              t.A.CreatedAt,
                              t.A.CreatedBy,
                              t.A.UpdatedBy,
                              t.A.UpdatedAt,
                              AgeFormated = t.B == null ? null : t.B.AgeFormated
                          }).ToList();

            return new List<dynamic>(render);
        }

        private Customer CommitCustomerUpdate(List<dynamic> list_ohCustomer, string pId, Customer curren_customer)
        {
            if ((list_ohCustomer == null || list_ohCustomer.Count == 0) && curren_customer == null)
                return new Customer();
            var customer = list_ohCustomer.FirstOrDefault(e => e.PID == pId);
            if (customer != null)
            {
                Customer new_customerEform = new Customer()
                {
                    Fullname = customer.Fullname,
                    PID = customer.PID,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Fork = customer.Fork,
                    Gender = customer.Gender,
                    Job = customer.Job,
                    WorkPlace = customer.WorkPlace,
                    Relationship = customer.Relationship,
                    RelationshipContact = customer.RelationshipContact,
                    RelationshipAddress = customer.RelationshipAddress,
                    RelationshipKind = customer.RelationshipKind,
                    RelationshipID = customer.RelationshipID,
                    DateOfBirth = string.IsNullOrEmpty(customer.DateOfBirth) ? null : DateTime.ParseExact(customer.DateOfBirth, "dd/MM/yyyy", null),
                    IdentificationCard = customer.IdentificationCard,
                    HealthInsuranceNumber = customer.HealthInsuranceNumber,
                    Nationality = customer.Nationality,
                    IssueDate = string.IsNullOrEmpty(customer.IssueDate) ? null : DateTime.ParseExact(customer.IssueDate, "dd/MM/yyyy", null),
                    IssuePlace = customer.IssuePlace,
                    IsVip = customer.IsVip,
                    MOHProvince = customer.Province,
                    MOHDistrict = customer.District,
                };
                if (curren_customer == null)
                {
                    unitOfWork.CustomerRepository.Add(new_customerEform);
                    unitOfWork.Commit();
                    return new_customerEform;
                }
                else
                {
                    curren_customer.Fullname = new_customerEform.Fullname;
                    curren_customer.PID = new_customerEform.PID;
                    curren_customer.Address = new_customerEform.Address;
                    curren_customer.Phone = new_customerEform.Phone;
                    curren_customer.Fork = new_customerEform.Fork;
                    curren_customer.Gender = new_customerEform.Gender;
                    curren_customer.Job = new_customerEform.Job;
                    curren_customer.WorkPlace = new_customerEform.WorkPlace;
                    curren_customer.Relationship = new_customerEform.Relationship;
                    curren_customer.RelationshipContact = new_customerEform.RelationshipContact;
                    curren_customer.RelationshipAddress = new_customerEform.RelationshipAddress;
                    curren_customer.RelationshipKind = new_customerEform.RelationshipKind;
                    curren_customer.RelationshipID = new_customerEform.RelationshipID;
                    curren_customer.DateOfBirth = new_customerEform.DateOfBirth;
                    curren_customer.IdentificationCard = new_customerEform.IdentificationCard;
                    curren_customer.HealthInsuranceNumber = new_customerEform.HealthInsuranceNumber;
                    curren_customer.Nationality = new_customerEform.Nationality;
                    curren_customer.IssueDate = new_customerEform.IssueDate;
                    curren_customer.IssuePlace = new_customerEform.IssuePlace;
                    curren_customer.IsVip = new_customerEform.IsVip;
                    curren_customer.MOHProvince = new_customerEform.MOHProvince;
                    curren_customer.MOHDistrict = new_customerEform.MOHDistrict;
                    unitOfWork.CustomerRepository.Update(curren_customer);
                    unitOfWork.Commit();
                    return curren_customer;
                }

            }

            if (curren_customer != null)
                return curren_customer;

            return new Customer();
        }

        protected Customer UpdateCustomerFromOHToEform(string pId, Customer curren_customer)
        {
            var site_code = GetSiteCode();
            if (site_code == null)
                return null;
            else if (site_code == "times_city")
            {
                if (!string.IsNullOrEmpty(pId))
                    return CommitCustomerUpdate(EHosClient.searchPatienteHos(new SearchParameter { PID = pId }), pId, curren_customer);

                return new Customer();
            }
            else
            {
                if (!string.IsNullOrEmpty(pId))
                    return CommitCustomerUpdate(OHClient.searchPatienteOh(new SearchParameter { PID = pId }), pId, curren_customer);

                return new Customer();
            }
        }

        protected void CoppyNewbornCustomerForPIDIsEmpty(EIOConstraintNewbornAndPregnantWoman form, string pId)
        {
            if (form == null)
                return;
            if (form.NewbornCustomer == null)
                return;

            if (string.IsNullOrEmpty(pId) && !string.IsNullOrEmpty(form.NewbornCustomer.PID))
            {
                Customer customer_emptyPid = new Customer()
                {
                    Fullname = form.NewbornCustomer.Fullname,
                    PID = null,
                    Address = form.NewbornCustomer.Address,
                    Phone = form.NewbornCustomer.Phone,
                    Fork = form.NewbornCustomer.Fork,
                    Gender = form.NewbornCustomer.Gender,
                    Job = form.NewbornCustomer.Job,
                    WorkPlace = form.NewbornCustomer.WorkPlace,
                    Relationship = form.NewbornCustomer.Relationship,
                    RelationshipContact = form.NewbornCustomer.RelationshipContact,
                    RelationshipAddress = form.NewbornCustomer.RelationshipAddress,
                    RelationshipKind = form.NewbornCustomer.RelationshipKind,
                    RelationshipID = form.NewbornCustomer.RelationshipID,
                    DateOfBirth = form.NewbornCustomer.DateOfBirth,
                    IdentificationCard = form.NewbornCustomer.IdentificationCard,
                    HealthInsuranceNumber = form.NewbornCustomer.HealthInsuranceNumber,
                    Nationality = form.NewbornCustomer.Nationality,
                    IssueDate = form.NewbornCustomer.IssueDate,
                    IssuePlace = form.NewbornCustomer.IssuePlace,
                    IsVip = form.NewbornCustomer.IsVip,
                    MOHProvince = form.NewbornCustomer.MOHProvince,
                    MOHDistrict = form.NewbornCustomer.MOHDistrict,
                };
                unitOfWork.CustomerRepository.Add(customer_emptyPid);
                form.NewbornCustomerId = customer_emptyPid.Id;
                unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.Update(form);
                unitOfWork.Commit();
            }    
        }
    }
}
