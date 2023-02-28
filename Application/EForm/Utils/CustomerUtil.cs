using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;

namespace EForm.Utils
{
    public class CustomerUtil
    {
        private IUnitOfWork unitOfWork;
        public Customer customer;

        public CustomerUtil(Customer customer)
        {
            this.customer = customer;
        }

        public CustomerUtil(IUnitOfWork unitOfWork, Customer customer)
        {
            this.unitOfWork = unitOfWork;
            this.customer = customer;
        }

        public string GetGender()
        {
            if (this.customer == null)
                return null;

            var gender = "Chưa xác định";
            if (this.customer.Gender == 0)
                gender = "Nữ";
            else if (this.customer.Gender == 1)
                gender = "Nam";
            return gender;
        }

        public void UpdateRelationship(string value)
        {
            customer.Relationship = value;
            unitOfWork.CustomerRepository.Update(customer);
        }
        public void UpdateRelationshipContact(string value)
        {
            customer.RelationshipContact = value;
            unitOfWork.CustomerRepository.Update(customer);
        }
        public void UpdateHeight(string value)
        {
            customer.Height = value;
            customer.LastUpdateHeight = DateTime.Now;
            unitOfWork.CustomerRepository.Update(customer);
        }
        public void UpdateWeight(string value)
        {
            customer.Weight = value;
            customer.LastUpdateWeight = DateTime.Now;
            unitOfWork.CustomerRepository.Update(customer);
        }

        public void UpdateAllergy_(Dictionary<string, string> all_dct)
        {
            if(all_dct.Count > 0)
            {
                if (all_dct["YES"].Trim().ToLower() == "true")
                {
                    customer.IsAllergy = true;
                    customer.KindOfAllergy = all_dct["KOA"];
                    customer.Allergy = all_dct["ANS"];
                }
                else if (all_dct["NOO"].Trim().ToLower() == "true")
                {
                    customer.IsAllergy = false;
                    customer.KindOfAllergy = "";
                    customer.Allergy = "Không";
                }
                else if (all_dct["NPA"].Trim().ToLower() == "true")
                {
                    customer.IsAllergy = false;
                    customer.KindOfAllergy = "";
                    customer.Allergy = "Không xác định";
                }
                customer.LastUpdateAllergy = DateTime.Now;
                unitOfWork.CustomerRepository.Update(customer);
            }
        }
    }
}