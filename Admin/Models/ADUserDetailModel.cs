using Admin.Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class ADUserDetailModel
    {
        public string UserId { get; set; }
        public String Department { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public String LoginName { get; set; }
        public String LoginNameWithDomain { get; set; }
        public String StreetAddress { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
        public String HomePhone { get; set; }
        public String Extension { get; set; }
        public String Mobile { get; set; }
        public String Fax { get; set; }
        public String EmailAddress { get; set; }
        public String Title { get; set; }
        public string Description { get; set; }
        public String Company { get; set; }
        public ADUserDetailModel Manager
        {
            get
            {
                if (!String.IsNullOrEmpty(this.ManagerName))
                {
                    ActiveDirectoryHelper ad = new ActiveDirectoryHelper();
                    return ad.GetUserByFullName(this.ManagerName);
                }
                return null;
            }
        }
        public String ManagerName { get; set; }

        public ADUserDetailModel() { }
        private ADUserDetailModel(DirectoryEntry directoryUser)
        {
            String domainAddress;
            String domainName;
            this.FirstName = GetProperty(directoryUser, ADProperties.FIRSTNAME);
            this.MiddleName = GetProperty(directoryUser, ADProperties.MIDDLENAME);
            this.LastName = GetProperty(directoryUser, ADProperties.LASTNAME);
            this.FullName = this.LastName + " " + this.FirstName;
            this.DisplayName = GetProperty(directoryUser, ADProperties.DISPLAYNAME);
            this.LoginName = GetProperty(directoryUser, ADProperties.LOGINNAME);
            this.UserId = this.LoginName;
            String userPrincipalName = GetProperty(directoryUser, ADProperties.USERPRINCIPALNAME);
            if (!string.IsNullOrEmpty(userPrincipalName))
            {
                domainAddress = userPrincipalName.Split('@')[1];
            }
            else
            {
                domainAddress = String.Empty;
            }
            if (!string.IsNullOrEmpty(domainAddress))
            {
                domainName = domainAddress.Split('.').First();
            }
            else
            {
                domainName = String.Empty;
            }
            this.LoginNameWithDomain = String.Format(@"{0}\{1}", domainName, LoginName);
            this.StreetAddress = GetProperty(directoryUser, ADProperties.STREETADDRESS);
            this.City = GetProperty(directoryUser, ADProperties.CITY);
            this.State = GetProperty(directoryUser, ADProperties.STATE);
            this.PostalCode = GetProperty(directoryUser, ADProperties.POSTALCODE);
            this.Country = GetProperty(directoryUser, ADProperties.COUNTRY);
            this.Company = GetProperty(directoryUser, ADProperties.COMPANY);
            this.Department = GetProperty(directoryUser, ADProperties.DEPARTMENT);
            this.HomePhone = GetProperty(directoryUser, ADProperties.HOMEPHONE);
            this.Extension = GetProperty(directoryUser, ADProperties.EXTENSION);
            this.Mobile = GetProperty(directoryUser, ADProperties.MOBILE);
            this.Fax = GetProperty(directoryUser, ADProperties.FAX);
            this.EmailAddress = GetProperty(directoryUser, ADProperties.EMAILADDRESS);
            this.Title = GetProperty(directoryUser, ADProperties.TITLE);
            this.Description = GetProperty(directoryUser, ADProperties.DESCRIPTION);
            this.ManagerName = GetProperty(directoryUser, ADProperties.MANAGER);
            if (!String.IsNullOrEmpty(ManagerName))
            {
                String[] managerArray = this.ManagerName.Split(',');
                this.ManagerName = managerArray[0].Replace("CN=", "");
            }
        }
        private static String GetProperty(DirectoryEntry userDetail, String propertyName)
        {
            if (userDetail.Properties.Contains(propertyName))
            {
                return userDetail.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public static ADUserDetailModel GetUser(DirectoryEntry directoryUser)
        {
            return new ADUserDetailModel(directoryUser);
        }
    }
}