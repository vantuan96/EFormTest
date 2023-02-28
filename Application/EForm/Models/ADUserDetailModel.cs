using EForm.Helper;
using System;
using System.DirectoryServices;
using System.Linq;

namespace EForm.Models
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

    public static class ADProperties
    {
        public const String OBJECTCLASS = "objectClass";
        public const String CONTAINERNAME = "cn";
        public const String LASTNAME = "sn";
        public const String COUNTRYNOTATION = "c";
        public const String CITY = "l";
        public const String STATE = "st";
        public const String TITLE = "title";
        public const String DESCRIPTION = "description";
        public const String POSTALCODE = "postalCode";
        public const String PHYSICALDELIVERYOFFICENAME = "physicalDeliveryOfficeName";
        public const String FIRSTNAME = "givenName";
        public const String MIDDLENAME = "initials";
        public const String DISTINGUISHEDNAME = "distinguishedName";
        public const String INSTANCETYPE = "instanceType";
        public const String WHENCREATED = "whenCreated";
        public const String WHENCHANGED = "whenChanged";
        public const String DISPLAYNAME = "displayName";
        public const String USNCREATED = "uSNCreated";
        public const String MEMBEROF = "memberOf";
        public const String USNCHANGED = "uSNChanged";
        public const String COUNTRY = "co";
        public const String DEPARTMENT = "department";
        public const String COMPANY = "company";
        public const String PROXYADDRESSES = "proxyAddresses";
        public const String STREETADDRESS = "streetAddress";
        public const String DIRECTREPORTS = "directReports";
        public const String NAME = "name";
        public const String OBJECTGUID = "objectGUID";
        public const String USERACCOUNTCONTROL = "userAccountControl";
        public const String BADPWDCOUNT = "badPwdCount";
        public const String CODEPAGE = "codePage";
        public const String COUNTRYCODE = "countryCode";
        public const String BADPASSWORDTIME = "badPasswordTime";
        public const String LASTLOGOFF = "lastLogoff";
        public const String LASTLOGON = "lastLogon";
        public const String PWDLASTSET = "pwdLastSet";
        public const String PRIMARYGROUPID = "primaryGroupID";
        public const String OBJECTSID = "objectSid";
        public const String ADMINCOUNT = "adminCount";
        public const String ACCOUNTEXPIRES = "accountExpires";
        public const String LOGONCOUNT = "logonCount";
        public const String LOGINNAME = "sAMAccountName";
        public const String SAMACCOUNTTYPE = "sAMAccountType";
        public const String SHOWINADDRESSBOOK = "showInAddressBook";
        public const String LEGACYEXCHANGEDN = "legacyExchangeDN";
        public const String USERPRINCIPALNAME = "userPrincipalName";
        public const String EXTENSION = "ipPhone";
        public const String SERVICEPRINCIPALNAME = "servicePrincipalName";
        public const String OBJECTCATEGORY = "objectCategory";
        public const String DSCOREPROPAGATIONDATA = "dSCorePropagationData";
        public const String LASTLOGONTIMESTAMP = "lastLogonTimestamp";
        public const String EMAILADDRESS = "mail";
        public const String MANAGER = "manager";
        public const String MOBILE = "mobile";
        public const String PAGER = "pager";
        public const String FAX = "facsimileTelephoneNumber";
        public const String HOMEPHONE = "homePhone";
        public const String MSEXCHUSERACCOUNTCONTROL = "msExchUserAccountControl";
        public const String MDBUSEDEFAULTS = "mDBUseDefaults";
        public const String MSEXCHMAILBOXSECURITYDESCRIPTOR = "msExchMailboxSecurityDescriptor";
        public const String HOMEMDB = "homeMDB";
        public const String MSEXCHPOLICIESINCLUDED = "msExchPoliciesIncluded";
        public const String HOMEMTA = "homeMTA";
        public const String MSEXCHRECIPIENTTYPEDETAILS = "msExchRecipientTypeDetails";
        public const String MAILNICKNAME = "mailNickname";
        public const String MSEXCHHOMESERVERNAME = "msExchHomeServerName";
        public const String MSEXCHVERSION = "msExchVersion";
        public const String MSEXCHRECIPIENTDISPLAYTYPE = "msExchRecipientDisplayType";
        public const String MSEXCHMAILBOXGUID = "msExchMailboxGuid";
        public const String NTSECURITYDESCRIPTOR = "nTSecurityDescriptor";
    }
}