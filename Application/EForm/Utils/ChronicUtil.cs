using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Utils
{
    public class ChronicUtil
    {
        private IUnitOfWork unitOfWork;
        private Customer Customer;

        public ChronicUtil(IUnitOfWork unitOfWork, Customer customer)
        {
            this.unitOfWork = unitOfWork;
            this.Customer = customer;
        }

        public void UpdateChronic()
        {
            if (this.Customer == null)
                return;

            var icd_util = new ICDUtil(unitOfWork);
            var list_icd = icd_util.GetICDByCustomer(this.Customer.Id);
            var is_chronic = IsChronic(list_icd);
            if (this.Customer.IsChronic != is_chronic)
            {
                this.Customer.IsChronic = is_chronic;
                unitOfWork.CustomerRepository.Update(this.Customer);
                unitOfWork.Commit();
            }
        }

        private bool IsChronic(List<string> list_icd)
        {
            if (list_icd.Count() < 2)
                return false;
            var icd_chronic = unitOfWork.ICD10Repository.Find(
                e => !e.IsDeleted &&
                e.IsChronic &&
                list_icd.Contains(e.Code)
                && e.ICDSpecialtyId != null
            ).Select(e => e.ICDSpecialtyId).Distinct();
            if (icd_chronic.Count() > 1)
                return true;
            return false;
        }
    }
}