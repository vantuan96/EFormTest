using AutoMapper;
using Bussiness;
using DataAccess.Models;
using DataAccess.Models.DTOs;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using EForm.Client;
using EForm.Common;
using EForm.Helper;
using EForm.Models;
using EForm.Models.DiagnosticReporting;
using EForm.Models.EDModels;
using EForm.Models.EOCModel;
using EForm.Models.IPDModels;
using EForm.Models.MedicalRecordModels;
using EForm.Models.MedicationAdministrationRecordModels;
using EForm.Models.OPDModels;
using EForm.Utils;
using EMRModels;
using Helper;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EForm.BaseControllers
{
    public class BaseApiController : ApiController
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();
        protected DataAccess.Dapper.Repository.IUnitOfWork unitOfWorkDapper = new DataAccess.Dapper.Repository.UnitOfWork();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
        protected EOC GetEOC(Guid id)
        {
            var visit = unitOfWork.EOCRepository.GetById(id);

            if (visit == null || visit.IsDeleted)
                return null;
            //var current_username = getUsername();
            //if (visit.Customer.IsVip && !IsVIPMANAGE())
            //{
            //    if (visit.UnlockFor == null) return null;
            //    if (visit.UnlockFor == "ALL") return visit;
            //    if (!("," + visit.UnlockFor + ",").Contains("," + current_username + ",")) return null;
            //}
            return visit;
        }
        protected ED GetED(Guid id)
        {
            // var current_username = getUsername();
            var ed = unitOfWork.EDRepository.GetById(id);

            if (ed == null || ed.IsDeleted)
                return null;
            //if (ed.Customer.IsVip && !IsVIPMANAGE())
            //{
            //    if (ed.UnlockFor == null) return null;
            //    if (ed.UnlockFor == "ALL") return ed;
            //    if (!("," + ed.UnlockFor + ",").Contains("," + current_username + ",")) return null;
            //}
            return ed;
        }
        protected OPD GetOPD(Guid id)
        {
            var opd = unitOfWork.OPDRepository.GetById(id);

            if (opd == null || opd.IsDeleted)
                return null;
            //var current_username = getUsername();
            //if (opd.Customer.IsVip && !IsVIPMANAGE())
            //{
            //    if (opd.UnlockFor == null) return null;
            //    if (opd.UnlockFor == "ALL") return opd;
            //    if (!("," + opd.UnlockFor + ",").Contains("," + current_username + ",")) return null;
            //}
            return opd;
        }
        protected IPD GetIPD(Guid id)
        {
            var ipd = unitOfWork.IPDRepository.GetById(id);

            if (ipd == null || ipd.IsDeleted)
                return null;
            //var current_username = getUsername();
            //if (ipd.Customer.IsVip && !IsVIPMANAGE())
            //{
            //    if (ipd.UnlockFor == null) return null;
            //    if (ipd.UnlockFor == "ALL") return ipd;
            //    if (!("," + ipd.UnlockFor + ",").Contains("," + current_username + ",")) return null;
            //}
            return ipd;
        }
        protected VisitInfoModel GetLastestVisitInfoModelIn24H(Guid? customer_id, Guid current_visit_id, DateTime? current_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            var opd_list = unitOfWork.OPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "OPD",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var ed_list = unitOfWork.EDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "ED",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var ipd_list = unitOfWork.IPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "IPD",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var eoc_list = unitOfWork.EOCRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "EOC",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var visit_list = opd_list.Concat(ed_list).Concat(ipd_list).Concat(eoc_list);
            if (visit_list.Count() > 0)
                return visit_list.OrderByDescending(e => e.AdmittedDate).FirstOrDefault();
            return null;
        }
        protected List<VisitInfoModel> GetVisitIn24H(Guid? customer_id, Guid current_visit_id, DateTime? current_admitted_date)
        {
            var time = DateTime.Now.AddDays(-1);
            var opd_list = unitOfWork.OPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "OPD",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var ed_list = unitOfWork.EDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "ED",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var ipd_list = unitOfWork.IPDRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "IPD",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var eoc_list = unitOfWork.EOCRepository.Find(
                    e => !e.IsDeleted &&
                    e.Id != current_visit_id &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id &&
                    e.AdmittedDate != null &&
                    e.AdmittedDate >= time &&
                    e.AdmittedDate < current_admitted_date
                ).Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "EOC",
                    AdmittedDate = e.AdmittedDate,
                    Specialty = e.Specialty?.ViName
                }).ToList();
            var visit_list = opd_list.Concat(ed_list).Concat(ipd_list).Concat(eoc_list).OrderByDescending(e => e.AdmittedDate).ToList();
            return visit_list;
        }
        #region RequestInformation
        protected string GetIp()
        {
            if (Request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (Request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)Request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
        protected User GetUser()
        {
            try
            {
                var ip = GetIp();
                if (!IsProInv() && ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    return GetUserDev(ip);
                }
                var identity = (ClaimsIdentity)User.Identity;
                var username = identity?.Name;
                return unitOfWork.UserRepository.FirstOrDefault(m => !m.IsDeleted && m.Username == username);
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected String getUsername()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var usertname = identity?.Name;
                return string.IsNullOrWhiteSpace(usertname) ? (IsProInv() ? "" : "") : usertname;
            }
            catch (Exception)
            {
                return IsProInv() ? "" : "";
            }
        }
        protected bool IsProInv()
        {
            return ConfigurationManager.AppSettings["HiddenError"].Equals("true");
        }
        private User GetUserDev(string ip)
        {
            if (IsProInv()) return null;
            string[] userNames = ConfigurationManager.AppSettings.Get("DEV_ACCOUNT").Split(',');
            var user_name_from_webconfig = userNames.FirstOrDefault();
            if (ip == "10.115.88.239") return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == "hunglq25");
            if (ip == "10.115.88.70") return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == "ducdv11");
            if (ip == "10.115.50.133") return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == "thangdc3");
            if (ip == "10.115.90.32") return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == "thanhnt135");
            if (ip == "10.115.88.157") return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == "haulv4");
            if (ip == "10.115.88.74") return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == "halt63");
            if (ip == "10.115.88.229") return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == "tungpa1");
            if (ip == "::1")
            {
                // đọc code xem config từ chỗ nào di mấy bố, có 7 dòng thôi, đừng sửa chỗ này nữa
                return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == user_name_from_webconfig);
            }
            return unitOfWork.UserRepository.FirstOrDefault(u => user_name_from_webconfig == u.Username);
        }
        protected Site GetSite()
        {

            try
            {
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    return unitOfWork.SiteRepository.FirstOrDefault(s => s.Id == user.CurrentSiteId);
                }
                var identity = (ClaimsIdentity)User.Identity;
                var site_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "Site").Value);
                var site = unitOfWork.SiteRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == site_id);
                return site;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Cần sửa
        protected string GetSiteCode()
        {
            try
            {
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    var sitex = unitOfWork.SiteRepository.FirstOrDefault(s => s.Id == user.CurrentSiteId);
                    return sitex?.Code;
                }
                var identity = (ClaimsIdentity)User.Identity;
                var site_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "Site").Value);
                var site = unitOfWork.SiteRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == site_id);
                return site?.Code;
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected Guid? GetSiteId()
        {
            try
            {
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    return user.CurrentSiteId;
                }
                var identity = (ClaimsIdentity)User.Identity;
                var site_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "Site").Value);
                return site_id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
        protected string GetSiteAPICode()
        {
            try
            {
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    var sitex = unitOfWork.SiteRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == user.CurrentSiteId);
                    return sitex.ApiCode;
                }
                var identity = (ClaimsIdentity)User.Identity;
                var site_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "Site").Value);
                var site = unitOfWork.SiteRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == site_id);
                return site?.ApiCode;
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected Specialty GetSpecialty()
        {
            try
            {
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    return unitOfWork.SpecialtyRepository.FirstOrDefault(s => s.Id == user.CurrentSpecialtyId);
                }
                var identity = (ClaimsIdentity)User.Identity;
                var specialty_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "SpecialtyId").Value);
                return unitOfWork.SpecialtyRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == specialty_id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected Guid GetSpecialtyId()
        {
            try
            {
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    var spec = unitOfWork.SpecialtyRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == user.CurrentSpecialtyId);
                    return spec.Id;
                }
                var identity = (ClaimsIdentity)User.Identity;
                var specialty_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "SpecialtyId").Value);
                return specialty_id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
        protected bool IsCovidSpecialty()
        {
            var specialty = GetSpecialty();
            return !string.IsNullOrEmpty(specialty.Code) && specialty.Code.Contains("EDCOVID19");
            // return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["CovidSpecialty"]) && ConfigurationManager.AppSettings["CovidSpecialty"].Contains(specialty.SpecialtyNo.ToString());
        }
        protected string GetSpecialtyName()
        {
            try
            {
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    var spec = unitOfWork.SpecialtyRepository.FirstOrDefault(s => s.Id == user.CurrentSpecialtyId);
                    return string.Format("{0} ({1} - {2})", spec?.Site.Name, spec?.VisitTypeGroup.Code, spec?.ViName);
                }
                var identity = (ClaimsIdentity)User.Identity;
                var specialty_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "SpecialtyId").Value);
                var specialty = unitOfWork.SpecialtyRepository.FirstOrDefault(s => !s.IsDeleted && s.Id == specialty_id);
                return string.Format("{0} ({1} - {2})", specialty?.Site.Name, specialty?.VisitTypeGroup.Code, specialty?.ViName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region In wating accept
        protected dynamic GetWaitingAcceptMessageByPosition(dynamic visit, string position_name)
        {
            if (position_name.Equals("Nurse"))
                return GetWaitingNurseAcceptMessage(visit);
            else if (position_name.Equals("Doctor"))
                return GetWaitingDoctorAcceptMessage(visit);
            return null;
        }
        protected dynamic GetWaitingDoctorAcceptMessage(dynamic visit)
        {
            if (visit.IsTransfer && visit.TransferFromId != null)
            {
                var transfer = GetTransfer(visit);
                if (transfer != null && !transfer.IsAcceptPhysician)
                    return new
                    {
                        transfer.Id,
                        transfer.VisitId,
                        transfer.ViMessage,
                        transfer.EnMessage,
                        transfer.VisitTypeGroupCode,
                        ErrorPopup = true,
                        Datas = new List<dynamic>(),
                        IsUseHandOverCheckList = transfer.IsUseHandOverCheckList,
                        HandOverPhysician = transfer.HandOverPhysician.Username,
                        HandOverTimePhysician = transfer.HandOverTimePhysician,
                        HandOverUnitPhysician = transfer.HandOverUnitPhysician
                    };
            }
            return null;
        }
        protected dynamic GetWaitingNurseAcceptMessage(dynamic visit)
        {
            if (visit.IsTransfer && visit.TransferFromId != null)
            {
                var transfer = GetTransfer(visit);
                if (transfer != null && !transfer.IsAcceptNurse)
                    return new
                    {
                        transfer.Id,
                        transfer.VisitId,
                        transfer.ViMessage,
                        transfer.EnMessage,
                        transfer.VisitTypeGroupCode,
                        ErrorPopup = true,
                        Datas = new List<dynamic>(),
                    };
            }
            return null;
        }
        protected dynamic BuildInWaitingAccpetErrorMessage(dynamic from_specialty, dynamic to_specialty)
        {
            return new
            {
                ViMessage = string.Format("Bệnh nhân chuyển từ {0} đến {1} đang chờ xác nhận", from_specialty?.ViName?.ToLower(), to_specialty?.ViName?.ToLower()),
                EnMessage = string.Format("The patient is transfered from {0} to {1}", from_specialty?.EnName?.ToLower(), to_specialty?.EnName?.ToLower()),
            };
        }
        protected dynamic GetInWaitingAcceptPatientById(Guid customer_id)
        {
            var in_transfer = unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                Constant.Transfer.Contains(e.Code)
            ).Select(e => e.Id).ToList();

            var ed = unitOfWork.EDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            );

            if (ed != null) return ed;

            var opd = unitOfWork.OPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.OPDHandOverCheckListId != null &&
                (!e.OPDHandOverCheckList.IsAcceptNurse || !e.OPDHandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            );

            if (opd != null) return opd;

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            );

            if (ipd != null) return ipd;

            return null;
        }
        protected dynamic GetInWaitingAcceptPatientById(Guid customer_id, Guid visit_id, Guid? group_id = null)
        {
            var in_transfer = unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                Constant.Transfer.Contains(e.Code)
            ).Select(e => e.Id).ToList();

            var ed = unitOfWork.EDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Id != visit_id &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            );
            if (ed != null) return ed;

            if (group_id == null)
            {
                var opd = unitOfWork.OPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id != visit_id &&
                    e.EDStatus != null &&
                    in_transfer.Contains((Guid)e.EDStatusId) &&
                    e.OPDHandOverCheckListId != null &&
                    (!e.OPDHandOverCheckList.IsAcceptNurse || !e.OPDHandOverCheckList.IsAcceptPhysician) &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id
                );
                if (opd != null) return opd;
            }
            else
            {
                var group_opd = unitOfWork.OPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id != visit_id &&
                    (e.GroupId == null || e.GroupId != group_id) &&
                    e.EDStatus != null &&
                    in_transfer.Contains((Guid)e.EDStatusId) &&
                    e.OPDHandOverCheckListId != null &&
                    (!e.OPDHandOverCheckList.IsAcceptNurse || !e.OPDHandOverCheckList.IsAcceptPhysician) &&
                    e.CustomerId != null &&
                    e.CustomerId == customer_id
                );
                if (group_opd != null) return group_opd;
            }

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Id != visit_id &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            );
            if (ipd != null) return ipd;

            return null;
        }
        protected dynamic GetInWaitingAcceptPatientByPID(string pid)
        {
            var in_transfer = unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                (Constant.Transfer.Contains(e.Code) || Constant.Admitted.Contains(e.Code))
            ).Select(e => e.Id).ToList();

            var ed = unitOfWork.EDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.Customer.PID != null &&
                e.Customer.PID.Equals(pid)
            );

            if (ed != null) return ed;

            var opd = unitOfWork.OPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.OPDHandOverCheckListId != null &&
                (!e.OPDHandOverCheckList.IsAcceptNurse || !e.OPDHandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.Customer.PID != null &&
                e.Customer.PID.Equals(pid)
            );

            if (opd != null) return opd;

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.Customer.PID != null &&
                e.Customer.PID.Equals(pid)
            );

            if (ipd != null) return ipd;

            return null;
        }
        protected dynamic GetInWaitingAcceptPatientByPID(string pid, Guid visit_id, Guid? group_id = null)
        {
            var in_transfer = unitOfWork.EDStatusRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                Constant.Transfer.Contains(e.Code)
            ).Select(e => e.Id).ToList();

            var ed = unitOfWork.EDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Id != visit_id &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.Customer.PID != null &&
                e.Customer.PID.Equals(pid)
            );
            if (ed != null) return ed;

            if (group_id == null)
            {
                var opd = unitOfWork.OPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id != visit_id &&
                    e.EDStatus != null &&
                    in_transfer.Contains((Guid)e.EDStatusId) &&
                    e.OPDHandOverCheckListId != null &&
                    (!e.OPDHandOverCheckList.IsAcceptNurse || !e.OPDHandOverCheckList.IsAcceptPhysician) &&
                    e.CustomerId != null &&
                    e.Customer.PID != null &&
                    e.Customer.PID.Equals(pid)
                );
                if (opd != null) return opd;
            }
            else
            {
                var opd = unitOfWork.OPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id != visit_id &&
                    (e.GroupId == null || e.GroupId != group_id) &&
                    e.EDStatus != null &&
                    in_transfer.Contains((Guid)e.EDStatusId) &&
                    e.OPDHandOverCheckListId != null &&
                    (!e.OPDHandOverCheckList.IsAcceptNurse || !e.OPDHandOverCheckList.IsAcceptPhysician) &&
                    e.CustomerId != null &&
                    e.Customer.PID != null &&
                    e.Customer.PID.Equals(pid)
                );
                if (opd != null) return opd;
            }

            var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Id != visit_id &&
                e.EDStatus != null &&
                in_transfer.Contains((Guid)e.EDStatusId) &&
                e.HandOverCheckListId != null &&
                (!e.HandOverCheckList.IsAcceptNurse || !e.HandOverCheckList.IsAcceptPhysician) &&
                e.CustomerId != null &&
                e.Customer.PID != null &&
                e.Customer.PID.Equals(pid)
            );
            if (ed != null) return ed;

            return null;
        }
        protected dynamic GetTransfer(dynamic visit)
        {
            if (visit.TransferFromId == null)
            {
                return null;
            }
            var hocl = GetHandOverCheckListByTransferFromId((Guid)visit.TransferFromId);
            if (hocl != null)
            {
                var to_specialty = hocl.ReceivingUnitPhysician;
                var from_specialty = hocl.HandOverUnitPhysician;
                Guid hocl_id = hocl.Id;
                string type_name = ObjectContext.GetObjectType(hocl.GetType()).Name;
                string visit_type_group_code = string.Empty;
                Guid? visit_id = null;
                if (type_name.Contains("OPD"))
                {
                    visit_type_group_code = "OPD";
                    var opd = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.OPDHandOverCheckListId != null && e.OPDHandOverCheckListId == hocl_id);
                    visit_id = opd?.Id;
                }
                else if (type_name.Contains("IPD"))
                {
                    visit_type_group_code = "IPD";
                    var ipd = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.HandOverCheckListId != null && e.HandOverCheckListId == hocl_id);
                    visit_id = ipd?.Id;
                }
                else if (type_name.Contains("EOC"))
                {
                    visit_type_group_code = "EOC";
                    var eoc_hocl = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == hocl_id);
                    visit_id = eoc_hocl?.VisitId;
                }
                else
                {
                    visit_type_group_code = "ED";
                    var ed = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.HandOverCheckListId != null && e.HandOverCheckListId == hocl_id);
                    visit_id = ed?.Id;
                }
                return new
                {
                    hocl.Id,
                    hocl.IsAcceptNurse,
                    hocl.IsAcceptPhysician,
                    VisitTypeGroupCode = visit_type_group_code,
                    VisitId = visit_id,
                    ViMessage = string.Format("Bệnh nhân chuyển từ {0} đến {1} đang chờ xác nhận", from_specialty?.ViName?.ToLower(), to_specialty?.ViName?.ToLower()),
                    EnMessage = string.Format("The patient is transfered from {0} to {1}", from_specialty?.EnName?.ToLower(), to_specialty?.EnName?.ToLower()),
                    IsUseHandOverCheckList = hocl.IsUseHandOverCheckList,
                    HandOverPhysician = hocl.HandOverPhysician,
                    HandOverTimePhysician = hocl.HandOverTimePhysician,
                    HandOverUnitPhysician = hocl.HandOverUnitPhysician
                };
            }
            return null;
        }
        private dynamic GetHandOverCheckListByTransferFromId(Guid transfer_id)
        {
            var hocl = unitOfWork.HandOverCheckListRepository.GetById(transfer_id);
            if (hocl != null) return hocl;

            var opd_hocl = unitOfWork.OPDHandOverCheckListRepository.GetById(transfer_id);
            if (opd_hocl != null) return opd_hocl;

            var ipd_hocl = unitOfWork.IPDHandOverCheckListRepository.GetById(transfer_id);
            if (ipd_hocl != null) return ipd_hocl;

            var eoc_hocl = unitOfWork.EOCHandOverCheckListRepository.GetById(transfer_id);
            if (eoc_hocl != null)
            {
                return eoc_hocl;
            }

            return null;
        }
        protected dynamic GetHandOverCheckListByVisit(dynamic visit)
        {
            string type_name = ObjectContext.GetObjectType(visit.GetType()).Name;
            if (type_name.Equals("ED"))
                return visit.HandOverCheckList;
            else if (type_name.Equals("OPD"))
                return visit.OPDHandOverCheckList;
            else if (type_name.Equals("IPD"))
                return visit.HandOverCheckList;
            return null;
        }
        private dynamic GetHandOverCheckListById(Guid id)
        {
            var ed_hocl = unitOfWork.HandOverCheckListRepository.GetById(id);
            if (ed_hocl != null) return ed_hocl;

            var opd_hocl = unitOfWork.OPDHandOverCheckListRepository.GetById(id);
            if (opd_hocl != null) return opd_hocl;

            var ipd_hocl = unitOfWork.IPDHandOverCheckListRepository.GetById(id);
            if (ipd_hocl != null) return ipd_hocl;

            var eoc_hocl = unitOfWork.EOCHandOverCheckListRepository.GetById(id);
            if (eoc_hocl != null) return eoc_hocl;

            return null;
        }
        protected dynamic GetVisitHandOverCheckList(Guid id)
        {
            dynamic hocl = null;
            hocl = GetHandOverCheckListById(id);
            Guid hocl_id = Guid.Empty;
            if (hocl != null)
            {
                hocl_id = hocl.Id;
                var ed = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.HandOverCheckListId != null && e.HandOverCheckListId == hocl_id);
                if (ed != null) return new { hocl, visit = ed };
                var opd = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.OPDHandOverCheckListId != null && e.OPDHandOverCheckListId == hocl_id);
                if (opd != null) return new { hocl, visit = opd };
                var ipd = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.HandOverCheckListId != null && e.HandOverCheckListId == hocl_id);
                if (ipd != null) return new { hocl, visit = ipd };
            }
            else
            {
                hocl = unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.GetById(id);
                if (hocl != null)
                {
                    hocl_id = hocl.VisitId;
                    var opd2 = unitOfWork.OPDRepository.FirstOrDefault(e => !e.IsDeleted && e.IsAnesthesia && e.Id == hocl_id);
                    if (opd2 != null) return new { hocl, visit = opd2 };
                }
            }
            if (IsPropertyExist(hocl, "Visit") && hocl.Visit != null) return new { hocl, visit = hocl.Visit };
            return null;
        }
        protected static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
        protected List<Guid?> GetListSpecicalty(User user)
        {
            return user.UserSpecialties.Where(e => !e.IsDeleted).Select(e => e.SpecialtyId).ToList();
        }
        #endregion

        #region Block update form
        protected bool IsBlockAfter24h(DateTime? created_at, Guid? formId = null)
        {
            //Tạm ngưng khóa hồ sơ sau 24h (THEO YÊU CẦU CỦA NA)
            // return false;
            if (formId != null && IsCheckConfirm((Guid)formId))
            {
                return false;
            }
            //if (IsSuperman()) return false;           
            var now = DateTime.Now;
            double timeToBlock = Convert.ToDouble(GetAppConfig("TIME_TO_BLOCK"));
            return created_at?.AddDays(timeToBlock) <= now;
        }
        protected bool IPDIsBlock(IPD ipd, string formCode, Guid? formId = null, int? form_timeToBlock = null)
        {
            // if (IsSuperman()) return false;
            if (ipd.DischargeDate == null) return false;

            if (formId != null && IsCheckConfirm((Guid)formId))
            {
                return false;
            }
            var user = GetUser();
            var now = DateTime.Now;
            var unlock = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(
                s => !s.IsDeleted && s.ExpiredAt >= now &&
                s.VisitId != null && s.VisitId == ipd.Id &&
                !string.IsNullOrEmpty(s.Username) && s.Username == user.Username &&
                !string.IsNullOrEmpty(s.RecordCode) && s.RecordCode == ipd.RecordCode &&
                s.FormId == null &&
                s.FormCode.ToUpper() == formCode.ToUpper());
            double timeToBlock = form_timeToBlock == null ? Convert.ToDouble(GetAppConfig("TIME_TO_BLOCK")) : Convert.ToDouble(form_timeToBlock);
            return unlock == null && ipd.DischargeDate?.AddDays(timeToBlock) <= now;
        }
        protected bool IsSuperman()
        {
            try
            {
                return ConfigurationManager.AppSettings["HiddenError"].Equals("false") && getUsername() == "thangdc3";
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected bool Is24hLocked(DateTime? created_at, Guid visit_id, string form_code, string username, Guid? formId = null)
        {
            if (IsSuperman()) return false;
            return IsBlockAfter24h(created_at, formId) && !HasUnlockPermission(visit_id, form_code, username, formId);
        }
        protected bool HasUnlockPermission(Guid visit_id, string form_code, string username, Guid? formId = null)
        {
            if (formId != null && IsCheckConfirm((Guid)formId))
            {
                return true;
            }
            var unlock = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username.Equals(username) &&
                !string.IsNullOrEmpty(e.FormCode) &&
                    e.FormId == null &&
                e.FormCode.Equals(form_code) &&
                e.ExpiredAt >= DateTime.Now
            );

            return unlock != null;
        }
        #region unlock confirm
        protected bool IsUnlockConfirm(Guid formId)
        {
            var username = getUsername();
            var unlock = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.FormId != null &&
                e.FormId == formId &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username.Equals(username) &&
                !string.IsNullOrEmpty(e.FormCode) &&
                e.ExpiredAt >= DateTime.Now
            );
            return unlock != null;
        }
        protected bool IsCheckConfirm(Guid formId)
        {
            //var user = GetUser();
            var unlock = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(
                e => e.VisitId != null &&
                e.FormId != null &&
                !e.IsDeleted &&
                e.FormId == formId &&
                !string.IsNullOrEmpty(e.Username) &&
                !string.IsNullOrEmpty(e.FormCode) &&
                e.ExpiredAt >= DateTime.Now);
            return unlock != null;
        }
        //protected void LockConfirm(Guid formId)
        //{
        //    var getid = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(e => !e.IsDeleted &&
        //       e.VisitId != null &&
        //       e.FormId != null &&
        //       e.FormId == formId);

        //    if (getid != null)
        //    {
        //        unitOfWork.UnlockFormToUpdateRepository.Delete(getid);
        //        unitOfWork.Commit();
        //    }
        //}
        #endregion
        protected bool HasUnlockPermission(Guid visit_id, string[] form_code_list, string username)
        {
            var unlock = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.Username) &&
                e.Username.Equals(username) &&
                !string.IsNullOrEmpty(e.FormCode) &&
                form_code_list.Contains(e.FormCode) &&
                e.ExpiredAt >= DateTime.Now
            );

            return unlock != null;
        }
        #endregion

        #region Patient medication list
        protected dynamic GetLastestPatientMedicationList(Guid visit_id, DateTime? visit_admitted, Guid? customer_id)
        {
            var lastest_visit = GetLastestVisit(visit_id, visit_admitted, customer_id);
            if (lastest_visit != null)
            {
                return unitOfWork.OrderRepository.Find(
                    i => !i.IsDeleted &&
                    i.VisitId != null &&
                    i.VisitId == lastest_visit.Id &&
                    !string.IsNullOrEmpty(i.OrderType) &&
                    i.OrderType.Equals(lastest_visit.Type)
                )
                .OrderBy(o => o.CreatedAt).Select(o => new
                {
                    o.Drug,
                    o.Dosage,
                    o.Route,
                    LastDoseDate = o.LastDoseDate?.ToString(Constant.DATE_FORMAT),
                }).ToList();
            }
            return new List<dynamic>();
        }
        private VisitOrder GetLastestVisit(Guid visit_id, DateTime? visit_admitted_date, Guid? customer_id)
        {
            var visit_list = new List<dynamic>();

            var ed_list = unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.Id != visit_id &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate < visit_admitted_date
            )
            .Select(e => new VisitOrder
            {
                Id = e.Id,
                AdmittedDate = e.AdmittedDate,
                Type = Constant.ED_PATIENT_MEDICATION_LIST
            })
            .ToList();
            visit_list.AddRange(ed_list);

            var opd_list = unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.Id != visit_id &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate < visit_admitted_date
            )
            .Select(e => new VisitOrder
            {
                Id = e.Id,
                AdmittedDate = e.AdmittedDate,
                Type = Constant.OPD_PATIENT_MEDICATION_LIST
            })
            .ToList();
            visit_list.AddRange(opd_list);

            visit_list = visit_list.OrderByDescending(e => e.AdmittedDate).ToList();
            if (visit_list.Count() > 0)
                return visit_list[0];
            return null;
        }
        #endregion

        #region Record Code
        protected void UpdateRecordCodeOfCustomer(Guid customer_id)
        {
            //var visits = new List<dynamic>();
            //var eds = unitOfWork.EDRepository.Find(e => !e.IsDeleted && e.CustomerId != null && e.CustomerId == customer_id);
            //visits.AddRange(eds);
            //var opds = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.CustomerId != null && e.CustomerId == customer_id);
            //visits.AddRange(opds);
            //var ipds = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.CustomerId != null && e.CustomerId == customer_id);
            //visits.AddRange(ipds);

            //foreach (var vs in visits) GenerateRecordCode(vs);
        }
        protected void GenerateRecordCode(dynamic visit)
        {
            var visit_type_name = ObjectContext.GetObjectType(visit.GetType()).Name;

            var site_code = unitOfWork.SiteRepository.GetById(visit.SiteId)?.ApiCode;
            if (!string.IsNullOrEmpty(site_code))
                site_code = site_code.Substring(1, 2);

            var pid = unitOfWork.CustomerRepository.GetById(visit.CustomerId)?.PID;

            var created_at = visit.AdmittedDate?.ToString("HHmmssddMMyy");

            var specialty_no = unitOfWork.SpecialtyRepository.GetById(visit.SpecialtyId)?.SpecialtyNo;
            string no = specialty_no?.ToString("D3");

            visit.RecordCode = string.Format("{0}.{1}.{2}{3}-{4}", site_code, visit_type_name, pid, created_at, no);
            if (visit_type_name == "ED") unitOfWork.EDRepository.Update(visit);
            else if (visit_type_name == "OPD") unitOfWork.OPDRepository.Update(visit);

            unitOfWork.Commit();
        }
        protected void GenerateRecordCode(ED visit)
        {
            var site_code = visit.Site.ApiCode;
            if (!string.IsNullOrEmpty(site_code))
                site_code = site_code.Substring(1, 2);

            var pid = visit.Customer.PID;

            var created_at = visit.AdmittedDate.ToString("HHmmssddMMyy");

            var specialty_no = visit.Specialty.SpecialtyNo;
            string no = specialty_no.ToString("D3");

            visit.RecordCode = string.Format("{0}.{1}.{2}{3}-{4}", site_code, "ED", pid, created_at, no);
            unitOfWork.EDRepository.Update(visit);
            unitOfWork.Commit();
        }
        #endregion

        #region Check Owner
        protected bool IsUserCreateFormManual(string username, string created_by)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(created_by) && username.Equals(created_by))
                return true;

            return false;
        }
        protected bool IsUserCreateFormAuto(string username, string updated_by, DateTime? created_at, DateTime? updated_at)
        {
            if (IsNew(created_at, updated_at))
                return true;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(updated_by) && username.Equals(updated_by))
                return true;

            return false;
        }
        #endregion

        protected Customer GetCustomerByPid(string PID)
        {
            return unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == PID).FirstOrDefault();
        }
        protected Customer GetCustomerById(Guid id)
        {
            return unitOfWork.CustomerRepository.GetById(id);
        }
        protected bool IsNew(DateTime? create_at, DateTime? update_at)
        {
            var is_new = false;
            if (create_at != null && update_at != null && create_at == update_at)
                is_new = true;
            return is_new;
        }
        protected string GetUserPositions()
        {
            var ip = GetIp();
            if (ConfigurationManager.AppSettings["HiddenError"].Equals("false") && ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
            {
                return "Administrator,Nurse";
            }
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var specialty_id = new Guid(identity.Claims.FirstOrDefault(c => c.Type == "SpecialtyId").Value);
                var positions = identity.Claims.FirstOrDefault(c => c.Type == "Positions").Value;

                return positions;
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected bool IsDoctor()
        {
            var positions = GetUserPositions();
            return positions != null ? positions.Contains("Doctor") : false;
        }
        protected bool IsDoctor(string username)
        {
            if (string.IsNullOrEmpty(username)) return false;
            var positions = GetUserByUsername(username).PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            return positions != null ? positions.Contains("Doctor") : false;
        }
        protected User GetAcceptUser(string userName, string password)
        {
            string[] userNames = ConfigurationManager.AppSettings.Get("DEV_ACCOUNT").Split(',');
            if (ConfigurationManager.AppSettings["HiddenError"].Equals("false") && userNames.Contains(userName))
                return unitOfWork.UserRepository.FirstOrDefault(s => s.Username.Equals(userName));

            bool isValidAdAccount = false;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                isValidAdAccount = context.ValidateCredentials(userName, password);
            }
            if (isValidAdAccount)
            {
                return unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username.Equals(userName));
            }
            return null;
        }

        protected UserModel GetUserInfo(User user)
        {
            var fullname = user.DisplayName;
            if (string.IsNullOrEmpty(fullname))
                fullname = user.Fullname;
            return new UserModel
            {
                Username = user.Username,
                Fullname = fullname,
                FullShortName = user.Fullname,
                Department = user.Department,
                Title = user.Title,
                Mobile = user.Mobile
            };
        }
        protected User GetUserInfo(Guid id)
        {
            return unitOfWork.UserRepository.GetById(id);
        }
        protected User GetUserByUsername(string user_name)
        {
            return unitOfWork.UserRepository.Find(e => e.Username == user_name).FirstOrDefault();
        }
        protected void HandleUpdateOrCreateFormDatas(Guid VisitId, Guid FormId, string formCode, JToken request, dynamic visit = null, bool ischeck = false)
        {
            List<FormDatas> listInsert = new List<FormDatas>();
            List<FormDatas> listUpdate = new List<FormDatas>();
            var allergy_dct = new Dictionary<string, string>();

            var visit_type = GetCurrentVisitType();
            List<FormDatas> current_data = null;
            if (formCode.Contains("PreProcedureRiskAssessmentForCardiacCatheterization"))
            {
                current_data = unitOfWork.FormDatasRepository.Find(e =>
                e.IsDeleted == false &&
                e.FormCode.Contains("PreProcedureRiskAssessmentForCardiacCatheterization") &&
                e.VisitId == VisitId).ToList();
            }
            else
            {
                current_data = unitOfWorkDapper.FormDatasRepository.Find(e =>
                e.IsDeleted == false &&
                e.FormId == FormId).ToList();
            }
            if (request != null)
            {
                foreach (var item in request)
                {
                    var code = item["Code"]?.ToString();
                    if (string.IsNullOrEmpty(code)) continue;
                    var value = item["Value"]?.ToString();
                    if (Constant.OPD_IAFST_ALLERGIC_CODE.Contains(code))
                        allergy_dct[Constant.CUSTOMER_ALLERGY_SWITCH[code]] = value;
                    CreateOrUpdateFormData(VisitId, FormId, formCode, code, value, visit_type, ref listInsert, ref listUpdate, current_data, ischeck);
                }
                if (listInsert.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Adds(listInsert);
                }
                if (listUpdate.Count > 0)
                {
                    unitOfWorkDapper.FormDatasRepository.Updates(listUpdate);
                }
                if (visit != null)
                {
                    if (allergy_dct.Count() > 0)
                    {
                        var visit_util = new VisitAllergy(visit);
                        visit_util.UpdateAllergy(allergy_dct);
                        unitOfWork.Commit();
                    }
                }
            }
        }
        protected void SyncDoctor(IPD visit)
        {
            try
            {
                if (!string.IsNullOrEmpty(visit.Customer.PID) && !string.IsNullOrEmpty(visit.VisitCode))
                {
                    string doctor_username = "";
                    var site_code = GetSiteCode();
                    if (site_code == "times_city")
                    {
                        doctor_username = EHosClient.GetBsIPD(visit.Customer.PID, visit.VisitCode);
                        if (!string.IsNullOrEmpty(doctor_username))
                        {
                            var user = unitOfWork.UserRepository.Find(e => e.Username == doctor_username).FirstOrDefault();
                            visit.PrimaryDoctorId = user?.Id;
                            unitOfWork.Commit();
                        }
                    }
                    else
                    {
                        doctor_username = OHClient.GetBsIPD(visit.Customer.PID, visit.VisitCode);
                        if (!string.IsNullOrEmpty(doctor_username))
                        {
                            var user = GetUserByUsername(doctor_username);
                            visit.PrimaryDoctorId = user?.Id;
                            unitOfWork.Commit();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        protected dynamic GetVisit(Guid id)
        {
            dynamic visit = GetED(id);
            if (visit != null) return visit;
            visit = GetIPD(id);
            if (visit != null) return visit;
            visit = GetOPD(id);
            if (visit != null) return visit;
            visit = GetEOC(id);
            if (visit != null) return visit;
            return null;
        }
        protected void UpdateVisit(dynamic visit, string type)
        {
            if (type == "ED")
                unitOfWork.EDRepository.Update(visit);
            if (type == "OPD")
                unitOfWork.OPDRepository.Update(visit);
            if (type == "IPD")
                unitOfWork.IPDRepository.Update(visit);
            if (type == "EOC")
                unitOfWork.EOCRepository.Update(visit);
            unitOfWork.Commit();
        }
        protected dynamic GetVisit(Guid id, string type)
        {
            dynamic visit = null;
            if (type == "ED")
                visit = GetED(id);
            if (type == "IPD")
                visit = GetIPD(id);
            if (type == "OPD")
                visit = GetOPD(id);
            if (type == "EOC")
                visit = GetEOC(id);
            return visit;
        }

        protected bool CreateEOCTranfer(Guid visit_id, JToken request, string username, string visit_type, Guid customer_id)
        {
            var site_id = GetSiteId();
            var eoc_in_hopspital = unitOfWork.SpecialtyRepository.FirstOrDefault(e => e.SiteId == site_id && e.VisitTypeGroup.Code == "EOC");
            if (eoc_in_hopspital == null)
            {
                return false;
            }

            var specialty_id = GetSpecialtyId();
            var from_md_data = request.FirstOrDefault(d => d.Value<string>("Code") == "TFTEOCANS");
            bool is_tranfer_eoc = from_md_data != null ? from_md_data.Value<string>("Value") == "True" : false;
            var finded_tranfer = unitOfWork.EOCTransferRepository.FirstOrDefault(e => e.FromVisitId == visit_id);
            if (finded_tranfer != null && finded_tranfer.AcceptBy != null)
                return true;

            if (finded_tranfer != null)
            {
                finded_tranfer.IsDeleted = !is_tranfer_eoc;
                finded_tranfer.TransferBy = username;
                unitOfWork.EOCTransferRepository.Update(finded_tranfer);
            }
            else
            {
                if (is_tranfer_eoc == true)
                {
                    unitOfWork.EOCTransferRepository.Add(new EOCTransfer
                    {
                        FromVisitType = visit_type,
                        FromVisitId = visit_id,
                        TransferBy = username,
                        TransferAt = DateTime.Now,
                        SpecialtyId = specialty_id,
                        SiteId = site_id,
                        CustomerId = customer_id,
                        IsDeleted = false
                    });
                }
            }
            unitOfWork.Commit();
            return true;
        }
        protected void CreateOrUpdateFormData(Guid visitId, Guid formId, string formCode, string code, string value, string visit_type, ref List<FormDatas> listInsert, ref List<FormDatas> listUpdate, List<FormDatas> current_data, bool ischeck = false)
        {

            var finded = current_data.FirstOrDefault(e => e.Code == code);
            if (ischeck && (code == "OPDOENRECANS" || code == "OPDOENREC2ANS"))
            {
            }
            else
            {
                if (finded == null)
                {
                    if (!string.IsNullOrEmpty(value))
                        listInsert.Add(new FormDatas
                        {
                            Code = code,
                            Value = value,
                            FormId = formId,
                            VisitId = visitId,
                            FormCode = formCode,
                            VisitType = visit_type
                        });
                }
                else
                {
                    if (value != finded.Value)
                    {
                        finded.Value = value;
                        listUpdate.Add(finded);
                    }
                }
            }
        }

        protected string GetCurrentVisitType()
        {
            var specialty = GetSpecialty();
            return specialty.VisitTypeGroup.Code;
        }

        protected List<FormDataValue> GetFormData(Guid visitId, Guid formId, string formCode)
        {
            if (formCode.Contains("PreProcedureRiskAssessmentForCardiacCatheterization"))
            {
                return unitOfWork.FormDatasRepository.Find(e =>
                    e.IsDeleted == false &&
                    e.VisitId == visitId &&
                    e.FormCode.Contains("PreProcedureRiskAssessmentForCardiacCatheterization")
                ).Select(f => new FormDataValue { Id = f.Id, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();
            }
            return unitOfWorkDapper.FormDatasRepository.Find(e =>
                    e.IsDeleted == false &&
                    e.VisitId == visitId &&
                    e.FormCode == formCode &&
                    e.FormId == formId
            ).ToList().Select(f => new FormDataValue { Id = f.Id, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();
        }
        protected List<EIOFormConfirm> GetFormConfirms(Guid formId)
        {
            return unitOfWork.EIOFormConfirmRepository.Find(e =>
                    e.IsDeleted == false &&
                    e.FormId == formId
            ).ToList();
        }
        protected void SaveConfirm(string username, string kind, Guid? formId)
        {
            var savconfirm = new EIOFormConfirm
            {
                FormId = formId,
                ConfirmType = kind,
                ConfirmBy = username,
                ConfirmAt = DateTime.Now
            };
            unitOfWork.EIOFormConfirmRepository.Add(savconfirm);
            unitOfWork.Commit();

        }
        protected bool IsCheckPermission(string username, string code)
        {
            var ischeck = (from user in unitOfWork.UserRepository.AsQueryable()
                           join ur in unitOfWork.UserRoleRepository.AsQueryable()
                           on user.Id equals ur.UserId
                           join ra in unitOfWork.RoleActionRepository.AsQueryable()
                           on ur.RoleId equals ra.RoleId
                           join a in unitOfWork.ActionRepository.AsQueryable()
                           on ra.ActionId equals a.Id
                           where user.Username == username && a.Code == code
                           && !ur.IsDeleted && !ra.IsDeleted && !a.IsDeleted && !user.IsDeleted
                           select a.Code).FirstOrDefault();

            if (ischeck != null && ischeck.Any())
                return true;
            return false;

        }
        protected EIOForm GetForm(Guid visit_id, string formCode)
        {
            return unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.FormCode == formCode).FirstOrDefault();
        }
        protected List<EIOForm> GetForms(Guid visit_id, string form_code, string visit_type_group)
        {
            return unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.FormCode == form_code && e.VisitTypeGroupCode == visit_type_group).ToList();
        }
        protected dynamic FormatOutput(EIOForm fprm, dynamic visit = null, bool isUnLockConfirm = false)
        {
            var confirminfo = GetFormConfirms(fprm.Id);
            if (confirminfo.Count() > 0)
            {
                foreach (var con in confirminfo)
                {
                    var fullname = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == con.ConfirmBy).Fullname;
                    con.Note = fullname;
                }
            }
            var fullnameupdatedby = "";
            var fullnamecreatedby = "";
            if (!string.IsNullOrEmpty(fprm.UpdatedBy))
            {
                fullnameupdatedby = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == fprm.UpdatedBy).Fullname;
            }
            if (!string.IsNullOrEmpty(fprm.CreatedBy))
            {
                fullnamecreatedby = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == fprm.CreatedBy).Fullname;
            }
            Guid specialtyId = Guid.NewGuid();
            specialtyId = visit.SpecialtyId;
            var site = unitOfWork.SpecialtyRepository.FirstOrDefault(e => e.Id == specialtyId);
            var customer = GetCustomerInfoInVisit((Guid)fprm.VisitId, fprm.VisitTypeGroupCode);
            var formCode = ConvertFormCode(fprm.FormCode);
            return new
            {
                fprm.Id,
                Datas = GetFormData((Guid)fprm.VisitId, fprm.Id, fprm.FormCode),
                fprm.CreatedBy,
                CreatedAt = fprm.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = fprm.VisitId,
                fprm.Note,
                fprm.Comment,
                IsNew = fprm.CreatedAt < fprm.UpdatedAt ? false : true,
                UpdatedAt = fprm.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConfirmInfos = confirminfo,
                FullNameNB = customer?.Fullname,
                PID = customer?.PID,
                DateOfBirth = customer?.DateOfBirth,
                Gender = customer?.Gender,
                UpdatedBy = fprm.UpdatedBy,
                Specialty = new
                {
                    ViName = site?.ViName,
                    EnName = site?.EnName,
                    Code = site?.Code,
                },
                FullNameUpdatedBy = fullnameupdatedby,
                FullNameCreatedBy = fullnamecreatedby,
                isUnLockConfirm = isUnLockConfirm == true && confirminfo.Count == 0 ? true : false,
                Version = visit?.Version
            };
        }
        protected ValidateUserModel validateConfirmForm(JObject request)
        {
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return new ValidateUserModel
                {
                    ErrorMsg = Common.Message.INFO_INCORRECT
                };

            var kind = request["kind"]?.ToString();
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains(kind))
                return new ValidateUserModel
                {
                    ErrorMsg = Common.Message.FORBIDDEN
                };
            return new ValidateUserModel
            {
                Id = user.Id,
                ErrorMsg = null
            };
        }
        protected CustomerModel GetCustomerInfoInVisit(Guid visitId, string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            var customer = new CustomerModel() { };
            if (visitType == "ED")
                customer = getCustomerInfoED(visit);
            if (visitType == "IPD")
                customer = getCustomerInfoIPD(visit);
            //if (visitType == "OPD")
            //    customer = getCustomerInfoED(visit);
            if (visitType == "EOC")
                customer = getCustomerInfoEOC(visit);
            return customer;
        }
        protected CustomerModel GetCustomerInfoInVisit(dynamic visit, string visitType)
        {
            var customer = new CustomerModel() { };
            if (visitType == "ED")
                customer = getCustomerInfoED(visit);
            if (visitType == "IPD")
                customer = getCustomerInfoIPD(visit);
            //if (visitType == "OPD")
            //    customer = getCustomerInfoED(visit);
            if (visitType == "EOC")
                customer = getCustomerInfoEOC(visit);
            return customer;
        }
        protected string GetMdValueInVisit(Guid visit_id, string md_coce)
        {
            var data = unitOfWork.FormDatasRepository.Find(e => e.Code == md_coce && e.VisitId == visit_id)
                .Select(e => new MasterDataValue
                {
                    Code = e.Code,
                    Value = e.Value,
                }).ToList();
            return getValueFromMasterDatas(md_coce, data);
        }
        protected CustomerModel getCustomerInfoEOC(EOC visit)
        {
            var etrMasterDataCode = new string[] {
                "OPDIAFSTOPPULANS", "OPDIAFSTOPBP0ANS", "OPDIAFSTOPTEMANS", "OPDIAFSTOPSPOANS", "OPDIAFSTOPRR0ANS", "OPDIAFSTOPHEIANS", "OPDIAFSTOPWEIANS", "OPDIAFSTOPNOTANS",
                "OPDIAFSTOPALLYES", "OPDIAFSTOPALLNOO", "OPDIAFSTOPALLNPA", "OPDIAFSTOPALLKOA", "OPDIAFSTOPALLANS", "OPDOENICDANS", "OPDOENDD0ANS", "OPDOENICDOPT"
            };
            var data = unitOfWork.FormDatasRepository.Find(e => e.VisitId == visit.Id && etrMasterDataCode.Contains(e.Code)).ToList()
                .Select(e => new MasterDataValue
                {
                    Code = e.Code,
                    Value = e.Value,
                }).ToList();
            var customer = new CustomerModel()
            {
                IsAllergy = getValueFromMasterDatas("OPDIAFSTOPALLYES", data) == "True",
                IsAllergyNone = getValueFromMasterDatas("OPDIAFSTOPALLNPA", data) == "True",
                KindOfAllergy = getValueFromMasterDatas("OPDIAFSTOPALLKOA", data),
                Allergy = getValueFromMasterDatas("OPDIAFSTOPALLANS", data),

                Pulse = getValueFromMasterDatas("OPDIAFSTOPPULANS", data),
                BP = getValueFromMasterDatas("OPDIAFSTOPBP0ANS", data),
                T = getValueFromMasterDatas("OPDIAFSTOPTEMANS", data),
                SpO2 = getValueFromMasterDatas("OPDIAFSTOPSPOANS", data),
                RR = getValueFromMasterDatas("OPDIAFSTOPRR0ANS", data),
                VitalSignsNote = getValueFromMasterDatas("OPDIAFSTOPNOTANS", data),

                Weight = getValueFromMasterDatas("OPDIAFSTOPWEIANS", data),
                Height = getValueFromMasterDatas("OPDIAFSTOPHEIANS", data),
                ICD10 = getValueFromMasterDatas("OPDOENICDANS", data),
                ICDOptions = getValueFromMasterDatas("OPDOENICDOPT", data),
                Diagnosis = getValueFromMasterDatas("OPDOENDD0ANS", data),
                Fullname = visit.Customer?.Fullname,
                Gender = visit.Customer?.Gender,
                PID = visit.Customer?.PID,
                DateOfBirth = visit.Customer?.DateOfBirth != null ? visit.Customer?.DateOfBirth?.ToString(Constant.DATE_FORMAT) : "",

                Specialty = new
                {
                    visit.Specialty?.ViName,
                    visit.Specialty?.EnName,
                    visit.Specialty?.Id,
                },
                Site = new
                {
                    visit.Site?.Name,
                    visit.Site?.Id,
                }
            };
            return customer;
        }
        protected CustomerModel getCustomerInfoED(ED visit)
        {
            var etrMasterDataCode = new string[] { "ETRALLYES", "ETRALLNO", "ETRALLNPA", "ETRALLKOA", "ETRALLANS", "ETRHEIANS", "ETRWEIANS" };
            var dataEtr = unitOfWork.EmergencyTriageRecordDataRepository
                .Find(e => etrMasterDataCode.Contains(e.Code) && e.EmergencyTriageRecordId == visit.EmergencyTriageRecordId)
                .Select(e => new MasterDataValue
                {
                    Code = e.Code,
                    Value = e.Value,
                }).ToList();
            var di0MasterDataCode = new string[] { "DI0DIAOPT", "DI0DIAANS", "DI0DIAICD" };

            var dataDi0 = unitOfWork.DischargeInformationDataRepository
                .Find(e => di0MasterDataCode.Contains(e.Code) && e.DischargeInformationId == visit.DischargeInformationId)
                .Select(e => new MasterDataValue
                {
                    Code = e.Code,
                    Value = e.Value,
                }).ToList();
            var customer = new CustomerModel()
            {
                IsAllergy = getValueFromMasterDatas("ETRALLYES", dataEtr) == "True",
                IsAllergyNone = getValueFromMasterDatas("ETRALLNPA", dataEtr) == "True",
                KindOfAllergy = getValueFromMasterDatas("ETRALLKOA", dataEtr),
                Allergy = getValueFromMasterDatas("ETRALLANS", dataEtr),
                Weight = getValueFromMasterDatas("ETRWEIANS", dataEtr),
                Height = getValueFromMasterDatas("ETRHEIANS", dataEtr),
                ICD10 = getValueFromMasterDatas("DI0DIAICD", dataDi0),
                Diagnosis = getValueFromMasterDatas("DI0DIAANS", dataDi0),
                Fullname = visit.Customer?.Fullname,
                Gender = visit.Customer?.Gender,
                PID = visit.Customer?.PID,
                DateOfBirth = visit.Customer?.DateOfBirth != null ? visit.Customer?.DateOfBirth?.ToString(Constant.DATE_FORMAT) : "",
                MasterDataValue = dataEtr.Concat(dataDi0).ToList(),
                Specialty = new
                {
                    visit.Specialty?.ViName,
                    visit.Specialty?.EnName,
                    visit.Specialty?.Id,
                },
                Site = new
                {
                    visit.Site?.Name,
                    visit.Site?.Id,
                },
                ICDOptions = getValueFromMasterDatas("DI0DIAOPT", dataDi0)
            };
            return customer;
        }
        protected CustomerModel getCustomerInfoIPD(IPD visit)
        {
            var etrMasterDataCode = new string[] { "IPDIAAUALLEYES", "IPDIAAUALLENOO", "IPDIAAUALLENPA", "IPDIAAUALLEKOA", "IPDIAAUALLEANS", "IPDIAAUHEIGANS", "IPDIAAUWEIGANS" };
            var dataEtr = unitOfWork.IPDInitialAssessmentForAdultDataRepository
                .Find(e => etrMasterDataCode.Contains(e.Code) && e.IPDInitialAssessmentForAdultId == visit.IPDInitialAssessmentForAdultId)
                .Select(e => new MasterDataValue
                {
                    Code = e.Code,
                    Value = e.Value,
                }).ToList();

            List<MasterDataValue> datas_Part2 = null;
            var part2_Id = visit.IPDMedicalRecord?.IPDMedicalRecordPart2Id;
            if (part2_Id != null)
                datas_Part2 = unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                            .Where(d => !d.IsDeleted && d.IPDMedicalRecordPart2Id == part2_Id)
                            .Select(d => new MasterDataValue
                            {
                                Code = d.Code,
                                Value = d.Value
                            }).ToList();

            var customer = new CustomerModel()
            {
                Fullname = visit.Customer?.Fullname,
                Gender = visit.Customer?.Gender,
                PID = visit.Customer?.PID,
                DateOfBirth = visit.Customer?.DateOfBirth != null ? visit.Customer?.DateOfBirth?.ToString(Constant.DATE_FORMAT) : "",
                IsAllergy = getValueFromMasterDatas("IPDIAAUALLEYES", dataEtr) == "True",
                IsAllergyNone = getValueFromMasterDatas("IPDIAAUALLENPA", dataEtr) == "True",
                KindOfAllergy = getValueFromMasterDatas("IPDIAAUALLEKOA", dataEtr),
                Allergy = getValueFromMasterDatas("IPDIAAUALLEANS", dataEtr),
                Weight = getValueFromMasterDatas("IPDIAAUWEIGANS", dataEtr),
                Height = getValueFromMasterDatas("IPDIAAUHEIGANS", dataEtr),
                MasterDataValue = dataEtr,
                Specialty = new
                {
                    visit.Specialty?.ViName,
                    visit.Specialty?.EnName,
                    visit.Specialty?.Id,
                },
                Site = new
                {
                    visit.Site?.Name,
                    visit.Site?.Id,
                },
                ICD10 = getValueFromMasterDatas("IPDMRPTICDCANS", datas_Part2),
                ICDOptions = getValueFromMasterDatas("IPDMRPTICDPANS", datas_Part2),
                Diagnosis = getValueFromMasterDatas("IPDMRPTCDBCANS", datas_Part2)
            };
            return customer;
        }
        protected string getValueFromMasterDatas(string code, List<MasterDataValue> datas)
        {
            if (datas == null) return "";
            return datas.FirstOrDefault(e => e.Code == code)?.Value;
        }

        protected string GetValueFromFormValueDatas(string code, List<FormDataValue> datas)
        {
            if (datas == null) return "";
            return datas.FirstOrDefault(e => e.Code == code)?.Value;
        }

        protected EOCInfo getEOCInfo(Guid visit_id, string type)
        {
            var site_id = GetSiteId();
            var eoc_in_hopspital = unitOfWork.SpecialtyRepository.FirstOrDefault(e => !e.IsDeleted && e.IsPublish && e.SiteId == site_id && e.VisitTypeGroup.Code == "EOC");
            if (eoc_in_hopspital == null)
            {
                return new EOCInfo
                {
                    NoEOC = true
                };
            }
            var finded = unitOfWork.EOCTransferRepository.FirstOrDefault(e => !e.IsDeleted && e.AcceptBy != null && e.FromVisitId == visit_id && e.FromVisitType == type);
            if (finded == null || finded.ToVisitId == null)
            {
                return new EOCInfo
                {
                    NoEOC = false
                };
            }
            var to_visit = GetEOC((Guid)finded.ToVisitId);
            if (to_visit == null)
            {
                return new EOCInfo
                {
                    NoEOC = false
                };
            }
            return new EOCInfo
            {
                AcceptBy = finded.AcceptBy,
                TransferAt = finded.TransferAt,
                NoEOC = false,
                VisitId = finded.ToVisitId,
                IsDone = to_visit.Status.Id != GetInHospitalStatusId("EOC"),
                Status = to_visit.Status
            };
        }
        protected Guid GetInHospitalStatusId(string visit_type_group = "ED")
        {
            return unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                Constant.InHospital.Contains(e.Code) &&
                e.VisitTypeGroupId != null &&
                e.VisitTypeGroup.Code == visit_type_group
            ).Id;
        }
        protected Guid GetStatusIdByCode(string code)
        {
            return unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.Code == code
            ).Id;
        }
        protected EDStatus GetStatusById(Guid id)
        {
            return unitOfWork.EDStatusRepository.GetById(id);
        }
        protected ValidateCustomInHospital ValidaterCustomInHopital(Guid customer_id, Guid? visit_id = null)
        {
            InHospital in_hospital = new InHospital();
            in_hospital.SetState(customer_id, visit_id, null, null);
            var in_hospital_visit = in_hospital.GetVisit();
            if (in_hospital_visit != null)
                return new ValidateCustomInHospital
                {
                    IsValidate = false,
                    Msg = in_hospital.BuildErrorMessage(in_hospital_visit)
                };
            return new ValidateCustomInHospital
            {
                IsValidate = true,
                Msg = null
            };
        }
        protected Customer CreateCustomer(CustomerEDParameterModel request, Guid? in_hospital_status_id = null)
        {
            if (!string.IsNullOrEmpty(request.PID))
            {
                Customer exitCustomer = GetCustomerByPid(request.PID);
                if (exitCustomer != null) return exitCustomer;
            }
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
                EDStatusId = in_hospital_status_id,
                Fork = request.Fork,
                IdentificationCard = request.IdentificationCard,
                IsVip = request.IsVip
            };
            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();
            return customer;
        }
        protected Customer UpdateCustomer(Customer customer, CustomerEDParameterModel request, Guid? in_hospital_status_id)
        {
            var current_customer = JsonConvert.SerializeObject(new
            {
                customer.PID,
                customer.Fullname,
                customer.DateOfBirth,
                customer.Phone,
                customer.Gender,
                customer.WorkPlace,
                customer.Relationship,
                customer.RelationshipContact,
                customer.Address,
                customer.Nationality,
                customer.Fork,
                customer.IdentificationCard,
                customer.EDStatusId,
                customer.IsVip
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
                request.Fork,
                request.IdentificationCard,
                EDStatusId = in_hospital_status_id,
                request.IsVip
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
                customer.Fork = request.Fork;
                customer.IdentificationCard = request.IdentificationCard;
                unitOfWork.CustomerRepository.Update(customer);
                unitOfWork.Commit();
                if (customer.PID != request.PID)
                    UpdateRecordCodeOfCustomer(customer.Id);
            }
            return customer;
        }
        protected DiagnosisAndICDModel GetVisitDiagnosisAndICD(Guid visit_id, string visit_type, bool getForPrescription, bool getForAnesthesia = false)
        {
            if (visit_type == "ED")
            {
                ED visit = GetED(visit_id);
                if (visit != null)
                {
                    var data_di = visit.DischargeInformation.DischargeInformationDatas;
                    return new DiagnosisAndICDModel
                    {
                        ICD = data_di.FirstOrDefault(e => e.Code == "DI0DIAICD")?.Value,
                        Diagnosis = data_di.FirstOrDefault(e => e.Code == "DI0DIAANS")?.Value,
                        ICDOption = data_di.FirstOrDefault(e => e.Code == "DI0DIAOPT")?.Value,
                        DiagnosisOption = data_di.FirstOrDefault(e => e.Code == "DI0DIAOPT2")?.Value
                    };
                }
            }
            if (visit_type == "OPD")
            {
                OPD visit = GetOPD(visit_id);
                if (visit != null)
                {
                    var data_eon = visit.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas;
                    if (!visit.IsAnesthesia)
                        return new DiagnosisAndICDModel
                        {
                            ICD = data_eon.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value, // OPDOEN662 back mã mới, phiếu khám ngoại trú back lại như cũ
                            Diagnosis = data_eon.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value,
                            ICDOption = data_eon.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value
                        };
                    else
                        return new DiagnosisAndICDModel
                        {
                            ICD = data_eon.FirstOrDefault(e => e.Code == "PRANCO4")?.Value, // Phiếu khám gây mê
                            Diagnosis = data_eon.FirstOrDefault(e => e.Code == "PRANCO5")?.Value,
                        };
                }
            }
            if (visit_type == "EOC")
            {
                EOC visit = GetEOC(visit_id);
                if (visit != null)
                {
                    CustomerModel customer = getCustomerInfoEOC(visit);
                    if (customer != null)
                    {
                        return new DiagnosisAndICDModel
                        {
                            ICD = customer.ICD10,
                            Diagnosis = customer.Diagnosis,
                            ICDOption = customer.ICDOptions
                        };
                    }
                }
            }

            if (visit_type == "IPD" && getForPrescription == false)
            {
                IPD visit = GetIPD(visit_id);
                if (visit != null)
                {
                    var medical_record = visit.IPDMedicalRecord;
                    if (medical_record != null)
                    {
                        var part_2 = visit.IPDMedicalRecord.IPDMedicalRecordPart2;
                        if (part_2 != null)
                        {
                            var data_eon = visit.IPDMedicalRecord.IPDMedicalRecordPart2.IPDMedicalRecordPart2Datas;
                            var returnData = new DiagnosisAndICDModel
                            {
                                ICD = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTICDCANS")?.Value,
                                Diagnosis = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTCDBCANS")?.Value,
                                ICDOption = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTICDPANS")?.Value,
                                DiagnosisOption = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTCDKTANS")?.Value
                            };
                            return returnData;
                        }
                    }
                }
            }
            else if (visit_type == "IPD" && getForPrescription == true)
            {
                IPD visit = GetIPD(visit_id);
                if (visit != null)
                {
                    var medical_record = visit.IPDMedicalRecord;
                    if (medical_record != null)
                    {
                        var part_3 = visit.IPDMedicalRecord.IPDMedicalRecordPart3;
                        if (part_3 != null)
                        {
                            var data_eon = visit.IPDMedicalRecord.IPDMedicalRecordPart3.IPDMedicalRecordPart3Datas;
                            if (data_eon != null)
                            {
                                var returnData = new DiagnosisAndICDModel
                                {
                                    ICD = data_eon.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value,
                                    Diagnosis = data_eon.FirstOrDefault(e => e.Code == "IPDMRPECDBCANS")?.Value,
                                    ICDOption = data_eon.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value,
                                    DiagnosisOption = data_eon.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value
                                };
                                return returnData;
                            }
                        }
                    }
                }
            }
            return new DiagnosisAndICDModel { };
        }
        protected TransferVisitInfoModel GetTransferVisitInfo(EOC visit)
        {
            var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.Id);
            var data_di = unitOfWork.FormDatasRepository.Find(e =>
                    !e.IsDeleted &&
                    e.VisitId == visit.Id &&
                    e.FormCode == "OPDOEN" &&
                    e.FormId == form.Id
            ).Select(f => new FormDataValue { Id = f.Id, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();
            return new TransferVisitInfoModel
            {
                ICD = data_di.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value,
                Diagnosis = data_di.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value,
                ResonForTransfer = data_di.FirstOrDefault(e => e.Code == "OPDOENRFTANS")?.Value,
                SpecialtyName = visit.Specialty?.ViName
            };
        }
        protected TransferVisitInfoModel GetTransferVisitInfo(ED visit)
        {
            var data_di = visit.DischargeInformation.DischargeInformationDatas;
            return new TransferVisitInfoModel
            {
                ICD = data_di.FirstOrDefault(e => e.Code == "DI0DIAICD")?.Value,
                Diagnosis = data_di.FirstOrDefault(e => e.Code == "DI0DIAANS")?.Value,
                ResonForTransfer = data_di.FirstOrDefault(e => e.Code == "DI0RFAANS")?.Value,
                SpecialtyName = visit.Specialty?.ViName
            };
        }
        protected TransferVisitInfoModel GetTransferVisitInfo(OPD visit)
        {
            var data_eon = visit.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas;
            return new TransferVisitInfoModel
            {
                ICD = data_eon.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value,
                Diagnosis = data_eon.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value,
                ResonForTransfer = data_eon.FirstOrDefault(e => e.Code == "OPDOENRFTANS")?.Value,
                SpecialtyName = visit.Specialty?.ViName
            };
        }
        protected TransferVisitInfoModel GetTransferVisitInfo(IPD visit)
        {
            var medical_record = visit.IPDMedicalRecord;
            if (medical_record != null)
            {
                var part_3 = visit.IPDMedicalRecord.IPDMedicalRecordPart3;
                var medicalRecord = visit.IPDMedicalRecord;
                if (part_3 != null && medicalRecord != null)
                {
                    var data_eon = part_3.IPDMedicalRecordPart3Datas;
                    var data_med = medicalRecord.IPDMedicalRecordDatas;
                    return new TransferVisitInfoModel
                    {
                        ICD = data_eon.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value,
                        Diagnosis = data_eon.FirstOrDefault(e => e.Code == "IPDMRPECDBCANS")?.Value,
                        ResonForTransfer = data_med.FirstOrDefault(e => e.Code == "IPDMRPTLDCKANS")?.Value,
                        SpecialtyName = visit.Specialty?.ViName
                    };
                }
            }
            return new TransferVisitInfoModel
            {
            };
        }
        protected DateTime? GetDischargeDate(IPD ipd)
        {
            return ipd.DischargeDate;
        }
        protected DateTime? GetDischargeDate(EOC eoc)
        {
            var status_code = eoc.Status.Code;
            var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == eoc.Id);
            if (status_code == "EOCUD")
            {
                var oen_data = GetFormData(eoc.Id, form.Id, "OPDOEN");
                var tranfer_time = oen_data.FirstOrDefault(e => e.Code == "OPDOENTD0ANS");
                if (tranfer_time != null && !string.IsNullOrEmpty(tranfer_time.Value))
                {
                    DateTime request_assessment_datetime = DateTime.ParseExact(tranfer_time.Value, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                    return request_assessment_datetime;
                }
            }
            if (status_code == "EOCIHT")
            {
                var oen_data = GetFormData(eoc.Id, form.Id, "OPDOEN");
                var tranfer_time = oen_data.FirstOrDefault(e => e.Code == "OPDOENTOTANS");
                if (tranfer_time != null && !string.IsNullOrEmpty(tranfer_time.Value))
                {
                    DateTime request_assessment_datetime = DateTime.ParseExact(tranfer_time.Value, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                    return request_assessment_datetime;
                }
            }
            return form.ExaminationTime;
        }
        protected DateTime? GetDischargeDate(ED ed)
        {
            var status_code = ed.EDStatus.Code;
            if (status_code == "EDUDT")
            {
                try
                {
                    var tranfer_time = ed.DischargeInformation.DischargeInformationDatas.FirstOrDefault(e => e.Code == "DI0TD0ANS");
                    if (tranfer_time != null && !string.IsNullOrEmpty(tranfer_time.Value))
                    {
                        DateTime request_assessment_datetime = DateTime.ParseExact(tranfer_time.Value, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                        return request_assessment_datetime;
                    }
                }
                catch
                {
                    return ed.DischargeInformation.AssessmentAt;
                }
            }
            return ed.DischargeInformation.AssessmentAt;
        }
        protected DateTime? GetDischargeDate(OPD opd)
        {
            var status_code = opd.EDStatus.Code;
            if (status_code == "OPDUDT")
            {
                try
                {
                    var tranfer_time = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == "OPDOENTD0ANS");
                    if (tranfer_time != null && !string.IsNullOrEmpty(tranfer_time.Value))
                    {
                        DateTime request_assessment_datetime = DateTime.ParseExact(tranfer_time.Value, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                        return request_assessment_datetime;
                    }
                }
                catch
                {
                    return opd.OPDOutpatientExaminationNote.ExaminationTime;
                }
            }
            if (status_code == "OPDIHT")
            {
                try
                {
                    var tranfer_time = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == "OPDOENTOTANS");
                    if (tranfer_time != null && !string.IsNullOrEmpty(tranfer_time.Value))
                    {
                        DateTime request_assessment_datetime = DateTime.ParseExact(tranfer_time.Value, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                        return request_assessment_datetime;
                    }
                }
                catch
                {
                    return opd.OPDOutpatientExaminationNote.ExaminationTime;
                }
            }
            return opd.OPDOutpatientExaminationNote.ExaminationTime;
        }
        protected MedicalRecordViewModel GetCareNote(Guid visit_id)
        {
            var form = unitOfWork.EIOCareNoteRepository.Find(e => e.VisitId == visit_id).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            var formNewborn = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.FormCode == "A02_062_050919_V")
                             .OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (form != null)
            {
                if (formNewborn != null && formNewborn?.UpdatedAt > form.UpdatedAt)
                    return new MedicalRecordViewModel(
                     "Phiếu chăm sóc",
                     "Nurse's Note",
                     "CareNote",
                     formNewborn
                    );
                return new MedicalRecordViewModel(
                   "Phiếu chăm sóc",
                   "Nurse's Note",
                   "CareNote",
                   form
               );
            }
            if (formNewborn != null)
                return new MedicalRecordViewModel(
                    "Phiếu chăm sóc",
                    "Nurse's Note",
                    "CareNote",
                    formNewborn
                   );
            return new MedicalRecordViewModel(
                "Phiếu chăm sóc",
                "Nurse's Note",
                "CareNote"
            );
        }
        protected MedicalRecordViewModel GetPhysicianNote(Guid visit_id)
        {
            var form = unitOfWork.EIOPhysicianNoteRepository.Find(e => e.VisitId == visit_id).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            var formNewborn = unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.AsQueryable()
                            .Where(e => !e.IsDeleted && e.VisitId == visit_id && e.FormCode == "A01_066_050919_VE")
                            .OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (form != null)
            {
                if (formNewborn != null && formNewborn?.UpdatedAt > form.UpdatedAt)
                    return new MedicalRecordViewModel(
                      "Phiếu điều trị",
                      "Physician Note",
                      "PhysicianNote",
                      formNewborn
                      );
                return new MedicalRecordViewModel(
                    "Phiếu điều trị",
                    "Physician Note",
                    "PhysicianNote",
                    form
                );
            }
            if (formNewborn != null)
                return new MedicalRecordViewModel(
                      "Phiếu điều trị",
                      "Physician Note",
                      "PhysicianNote",
                      formNewborn
                      );
            return new MedicalRecordViewModel(
                "Phiếu điều trị",
                "Physician Note",
                "PhysicianNote"
            );
        }
        protected DateTime? HandleDatetimeField(string request_assessment)
        {
            if (!string.IsNullOrEmpty(request_assessment))
            {
                DateTime request_assessment_datetime = DateTime.ParseExact(request_assessment, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                return request_assessment_datetime;
            }
            return null;
        }
        
        protected VisitAllergyModel ConvertAllergy(Dictionary<string, string> all_dct)
        {
            try
            {
                var vam = new VisitAllergyModel() { };
                if (all_dct.ContainsKey("YES") && all_dct["YES"] != null && all_dct["YES"].Trim().ToLower() == "true")
                {
                    vam.IsAllergy = true;
                    vam.KindOfAllergy = all_dct["KOA"];
                    vam.Allergy = all_dct["ANS"];
                }
                else if (all_dct.ContainsKey("NOO") && all_dct["NOO"] != null && all_dct["NOO"].Trim().ToLower() == "true")
                {
                    vam.IsAllergy = false;
                    vam.KindOfAllergy = "";
                    vam.Allergy = "Không";
                }
                else if (all_dct.ContainsKey("NPA") && all_dct["NPA"] != null && all_dct["NPA"].Trim().ToLower() == "true")
                {
                    vam.IsAllergy = false;
                    vam.KindOfAllergy = "";
                    vam.Allergy = "Không xác định";
                }

                return vam;
            }
            catch (Exception)
            {
                return new VisitAllergyModel() { };
            }
        }

        protected void CopyProperties(IPDPrescriptionModel source, IPDPrescriptionModel destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentNullException("source");
            }
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }

                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }
        protected void CopyObjProperties(dynamic source, dynamic destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentNullException("source");
            }
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }

                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }
        protected Customer GetLocalOrHisCustomerByPid(string pid)
        {
            var customerLocal = unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == pid).FirstOrDefault();

            if (customerLocal != null)
            {
                return customerLocal;
            }
            else
            {
                var hisCustomers = OHClient.searchPatienteOh(new SearchParameter { PID = pid });
                if (hisCustomers.Count == 0)
                {
                    return null;
                }
                var customer = hisCustomers.First();
                var new_customer = CreateNewCustomer(customer);
                return new_customer;
            }
        }
        protected Customer GetUpdateOrCreateHisCustomerByPid(string pid)
        {
            var hisCustomers = OHClient.searchPatienteOhByPid(pid);
            if (hisCustomers.Count == 0)
            {
                return null;
            }
            var his_customer = hisCustomers.First();
            var customerLocal = unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == pid).FirstOrDefault();
            if (customerLocal != null)
            {
                return UpdateCustomer(his_customer, customerLocal);
            }
            else
            {
                return CreateNewCustomer(his_customer);
            }
        }
        protected Customer GetOrCreateCustomerByPid(dynamic his_customer, string pid)
        {
            var customerLocal = unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == pid).FirstOrDefault();
            if (customerLocal != null)
            {
                return customerLocal;
            }
            else
            {
                return CreateNewCustomer(his_customer);
            }
        }
        private Customer CreateNewCustomer(dynamic request)
        {
            Customer exitCustomer = GetCustomerByPid(request.PID);
            if (exitCustomer != null) return exitCustomer;
            DateTime? dob = null;
            if (!string.IsNullOrEmpty(request.DateOfBirth))
            {
                dob = DateTime.ParseExact(request.DateOfBirth, Constant.DATE_FORMAT, null);
            }
            Customer customer = new Customer
            {
                PID = request.PID,
                Fullname = request.Fullname,
                DateOfBirth = dob,
                Phone = request.Phone,
                Gender = request.Gender,
                Job = request.Job,
                WorkPlace = request.WorkPlace,
                Relationship = request.Relationship,
                RelationshipContact = request.RelationshipContact,
                Address = request.Address,
                Nationality = request.Nationality,
                Fork = request.Fork,
                IdentificationCard = request.IdentificationCard,
                RelationshipID = request.RelationshipID,
                IsVip = request.IsVip,
                HealthInsuranceNumber = request.HealthInsuranceNumber
            };
            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();
            return customer;
        }
        private Customer UpdateCustomer(dynamic his_customer, Customer local_customer)
        {
            DateTime? dob = null;
            if (!string.IsNullOrEmpty(his_customer.DateOfBirth))
            {
                dob = DateTime.ParseExact(his_customer.DateOfBirth, Constant.DATE_FORMAT, null);
            }
            local_customer.PID = his_customer.PID;
            local_customer.Fullname = his_customer.Fullname;
            local_customer.DateOfBirth = dob;
            local_customer.Phone = his_customer.Phone;
            local_customer.Gender = his_customer.Gender;
            local_customer.Job = his_customer.Job;
            local_customer.WorkPlace = his_customer.WorkPlace;
            local_customer.Relationship = his_customer.Relationship;
            local_customer.RelationshipContact = his_customer.RelationshipContact;
            local_customer.Address = his_customer.Address;
            local_customer.Nationality = his_customer.Nationality;
            local_customer.Fork = his_customer.Fork;
            local_customer.IdentificationCard = his_customer.IdentificationCard;
            local_customer.RelationshipID = his_customer.RelationshipID;
            local_customer.IsVip = his_customer.IsVip;
            local_customer.HealthInsuranceNumber = his_customer.HealthInsuranceNumber;
            unitOfWork.Commit();
            return local_customer;
        }

        protected string CrawlHtml(string htmlData)
        {
            if (string.IsNullOrEmpty(htmlData)) return "";
            string finalString = "";

            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlData);

                //var listFontNodes = doc.DocumentNode.SelectNodes("//font[@xmlns='http://www.w3.org/TR/xhtml1/strict']").ToList();
                List<string> listChanDoan = new List<string>();
                var listPNodes = doc.DocumentNode.SelectNodes("//p").ToList();

                foreach (var pNode in listPNodes)
                {
                    string chanDoan = "";
                    if (pNode.InnerText.Contains("\\n") || pNode.InnerText.Contains("\\r"))
                    {
                        chanDoan = "- " + pNode.InnerText.Replace("\\n", "").Replace("\\r", "").Trim();
                    }
                    else
                    {
                        chanDoan = "- " + pNode.InnerText.Replace("\n", "").Replace("\r", "").Trim();
                    }
                    Regex trimmer = new Regex(@"\s\s+");
                    chanDoan = trimmer.Replace(chanDoan, " ");
                    listChanDoan.Add(chanDoan);
                }
                foreach (var item in listChanDoan)
                {
                    finalString += $"{item}\n";
                }
                return finalString;
            }
            catch
            {
                return "Không lấy được dữ liệu";
            }
        }
        protected List<string> GetCurrentUserAction()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Roles = identity.Claims.FirstOrDefault(c => c.Type == "Roles").Value;
                return Roles.Split(',').ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected bool hasAction(string action_code)
        {
            var list_action = GetCurrentUserAction();
            return list_action != null && list_action.Contains(action_code);
        }
        protected void setLog(Log log)
        {
            log.Id = Guid.NewGuid();
            var user = GetUser();
            log.Username = user.Username;
            unitOfWork.LogRepository.Add(log);
            unitOfWork.Commit();
        }
        protected bool revertHanoverChecklis(Guid id)
        {
            var ed_hand = unitOfWork.HandOverCheckListRepository.GetById(id);
            if (ed_hand != null)
            {
                if (ed_hand.IsAcceptNurse && ed_hand.IsAcceptPhysician) return false;
                ed_hand.IsAcceptNurse = false;
                ed_hand.IsAcceptPhysician = false;

                ed_hand.ReceivingNurseId = null;
                ed_hand.ReceivingPhysicianId = null;

                unitOfWork.Commit();
                return true;
            }
            var opd_hand = unitOfWork.OPDHandOverCheckListRepository.GetById(id);
            if (opd_hand != null)
            {
                if (opd_hand.IsAcceptNurse && opd_hand.IsAcceptPhysician) return false;
                opd_hand.IsAcceptNurse = false;
                opd_hand.IsAcceptPhysician = false;

                opd_hand.ReceivingNurseId = null;
                opd_hand.ReceivingPhysicianId = null;

                unitOfWork.Commit();
                return true;
            }
            var ipd_hand = unitOfWork.IPDHandOverCheckListRepository.GetById(id);
            if (ipd_hand != null)
            {
                if (ipd_hand.IsAcceptNurse && ipd_hand.IsAcceptPhysician) return false;
                ipd_hand.IsAcceptNurse = false;
                ipd_hand.IsAcceptPhysician = false;

                ipd_hand.ReceivingNurseId = null;
                ipd_hand.ReceivingPhysicianId = null;

                unitOfWork.Commit();
                return true;
            }
            var eoc_hand = unitOfWork.EOCHandOverCheckListRepository.GetById(id);
            if (eoc_hand != null)
            {
                if (eoc_hand.IsAcceptNurse && eoc_hand.IsAcceptPhysician) return false;
                eoc_hand.IsAcceptNurse = false;
                eoc_hand.IsAcceptPhysician = false;

                eoc_hand.ReceivingNurseId = null;
                eoc_hand.ReceivingPhysicianId = null;

                unitOfWork.Commit();
                return true;
            }
            return true;
        }
        protected string getOPDTreatmentPlans(OPDOutpatientExaminationNote oen)
        {
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            return oen.IsConsultation == true ? TreatmentPlansInConsultation(oen_datas) : oen_datas.FirstOrDefault(e => e.Code == "OPDOENTP0ANS")?.Value;
        }
        private string TreatmentPlansInConsultation(List<OPDOutpatientExaminationNoteData> oen_datas)
        {
            List<string> principal_test = new List<string>();
            var OPDOEN251001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN251001")?.Value;
            var OPDOEN251002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN251002")?.Value;

            if (IsTrueMDValue(OPDOEN251001))
            {
                principal_test.Add(string.Format("{0}: {1}", "Tiêm chủng", OPDOEN251002));
            }

            var OPDOEN252001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN252001")?.Value;

            if (IsTrueMDValue(OPDOEN252001))
            {
                principal_test.Add(string.Format("{0}", "Tiếp tục liệu pháp hiện tại."));
            }

            var OPDOEN253001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN253001")?.Value;
            var OPDOEN253002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN253002")?.Value;

            if (IsTrueMDValue(OPDOEN253001))
            {
                principal_test.Add(string.Format("{0}: {1}", "Những thay đổi mới đối với liệu pháp", OPDOEN253002));
            }

            var OPDOEN254001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN254001")?.Value;

            if (IsTrueMDValue(OPDOEN254001))
            {
                principal_test.Add(string.Format("{0}", "Các tác dụng phụ có thể xảy ra, tương tác, hiệu quả thay đổi, kỳ vọng và rủi ro đã được thảo luận. Bệnh nhân sẽ báo cáo / phản hồi nếu có sự cố."));
            }

            var OPDOEN255001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN255001")?.Value;
            if (IsTrueMDValue(OPDOEN255001))
            {
                principal_test.Add(string.Format("{0}", "Các lựa chọn điều trị và tầm quan trọng của việc theo dõi được trao đổi với bệnh nhân / người đại diện."));
            }

            var OPDOEN256001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN256001")?.Value;
            if (IsTrueMDValue(OPDOEN256001))
            {
                principal_test.Add(string.Format("{0}", "Kế hoạch được hiểu và đồng ý."));
            }

            var OPDOEN257001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN257001")?.Value;
            if (IsTrueMDValue(OPDOEN257001))
            {
                var t = string.Format("{0}", "Tất cả các câu hỏi đã được trả lời.");
                var OPDOEN258002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN258002")?.Value;
                List<string> r = new List<string>();
                if (IsTrueMDValue(OPDOEN258002))
                {
                    r.Add("Bệnh nhân");
                }
                var OPDOEN258003 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN258003")?.Value;
                if (IsTrueMDValue(OPDOEN258003))
                {
                    r.Add("Người nhà bệnh nhân");
                }
                principal_test.Add(string.Format("{0} {1} {2}", t, string.Join(", ", r), " hài lòng với đánh giá và điều trị"));
            }

            var OPDOEN259001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN259001")?.Value;
            if (IsTrueMDValue(OPDOEN259001))
            {
                principal_test.Add(string.Format("{0}", "Cần khám và điều trị với bác sĩ chuyên khoa."));
            }

            var OPDOEN260001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN260001")?.Value;
            if (IsTrueMDValue(OPDOEN260001))
            {
                principal_test.Add(string.Format("{0}", "Cần chuyển đến cơ sở y tế khác theo đúng phạm vi chuyên môn."));
            }

            var OPDOEN261001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN261001")?.Value;
            if (IsTrueMDValue(OPDOEN261001))
            {
                var OPDOEN261002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN261002")?.Value;
                principal_test.Add(string.Format("Kiểm tra lại nếu không tốt hơn trong {0}, sớm hơn nếu các triệu chứng diễn tiến xấu hơn hoặc bệnh nhân có bất kỳ mối quan tâm hoặc thắc mắc nào. ", OPDOEN261002));
            }

            return string.Join("\n", principal_test);
        }
        protected string getOPDPrincipalTest(OPDOutpatientExaminationNote oen)
        {
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            return oen.IsConsultation == true ? PrincipalTestInConsultation(oen_datas) : oen_datas.FirstOrDefault(e => e.Code == "OPDOENPT0ANS")?.Value;
        }
        private string PrincipalTestInConsultation(List<OPDOutpatientExaminationNoteData> oen_datas)
        {
            List<string> principal_test = new List<string>();
            var OPDOEN262001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN262001")?.Value;
            var OPDOEN262002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN262002")?.Value;

            if (IsTrueMDValue(OPDOEN262001))
            {
                principal_test.Add(string.Format("{0}: {1}", "Xét nghiệm", OPDOEN262002));
            }

            var OPDOEN263001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN263001")?.Value;
            var OPDOEN263002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN263002")?.Value;

            if (IsTrueMDValue(OPDOEN263001))
            {
                principal_test.Add(string.Format("{0}: {1}", "Chẩn đoán hình ảnh", OPDOEN263002));
            }

            var OPDOEN264001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN264001")?.Value;
            var OPDOEN264002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN264002")?.Value;

            if (IsTrueMDValue(OPDOEN264001))
            {
                principal_test.Add(string.Format("{0}: {1}", "Thăm dò chức năng", OPDOEN264002));
            }

            var OPDOEN265001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN265001")?.Value;
            var OPDOEN265002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN265002")?.Value;

            if (IsTrueMDValue(OPDOEN265001))
            {
                principal_test.Add(string.Format("{0}: {1}", "Giải phẫu bệnh lý", OPDOEN265002));
            }

            var OPDOEN266001 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN266001")?.Value;
            var OPDOEN266002 = oen_datas.FirstOrDefault(e => e.Code == "OPDOEN266002")?.Value;
            if (IsTrueMDValue(OPDOEN266001))
            {
                principal_test.Add(string.Format("{0}: {1}", "Khác", OPDOEN266002));
            }
            return string.Join("\n", principal_test);
        }
        protected bool hsClinicalExamination(OPDOutpatientExaminationNote oen)
        {
            return (oen.IsConsultation == true && IsTrueMDValue(oen.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => e.Code == "OPDOEN25002")?.Value)) || oen.IsConsultation == false;
        }
        protected bool IsTrueMDValue(string val)
        {
            return !string.IsNullOrEmpty(val) && val.Trim().ToUpper() == "TRUE";
        }
        protected bool IsStringEquals(string a, string b)
        {
            return string.Equals(a, b, StringComparison.CurrentCultureIgnoreCase);
        }
        protected Guid? StringToGuid(string a)
        {
            if (string.IsNullOrWhiteSpace(a)) return null;
            return new Guid(a);
        }
        protected bool IsVIPMANAGE()
        {
            return hasAction("VIPMANAGE");
        }
        protected bool IsUnlockVipByPid(string PID, string type = null)
        {
            if (IsVIPMANAGE()) return true;
            var current_user = getUsername();
            var now = DateTime.Now;
            var unlockfor = unitOfWork.UnlockVipRepository.Find(e =>
                !e.IsDeleted &&
                e.PID == PID &&
                e.ExpiredAt >= now &&
                ("," + e.Username + ",").Contains("," + current_user + ",") &&
                (type == null || (e.Type != null && e.Type.Contains(type)))
            ).Count();
            return unlockfor > 0;
        }
        protected bool HasEFORMViSitOpen(string PID)
        {
            var current_user = getUsername();
            var visit = GetVisitByPid(PID);
            return visit.Count(e => e.UnlockFor == "ALL" || ("," + e.UnlockFor + ",").Contains("," + current_user + ",")) > 0;
        }
        protected List<VisitInfoModel> GetVisitByPid(string PID)
        {
            var list = new List<VisitInfoModel>();
            var ed_visit = unitOfWork.EDRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID)
                .Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "ED",
                    UnlockFor = e.UnlockFor
                }).ToList();

            if (ed_visit.Count() > 0) list.AddRange(ed_visit);

            var opd_visit = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID)
                .Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "OPD",
                    UnlockFor = e.UnlockFor
                }).ToList();

            if (opd_visit.Count() > 0) list.AddRange(opd_visit);

            var ipd_visit = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID)
                .Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "IPD",
                    UnlockFor = e.UnlockFor
                }).ToList();

            if (ipd_visit.Count() > 0) list.AddRange(ipd_visit);

            var eoc_visit = unitOfWork.EOCRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID)
                .Select(e => new VisitInfoModel
                {
                    Id = e.Id,
                    VisitType = "EOC",
                    UnlockFor = e.UnlockFor
                }).ToList();

            if (eoc_visit.Count() > 0) list.AddRange(eoc_visit);
            return list;
        }
        protected MsgModel GetEOCVipInfo(EOC visit)
        {
            var current_username = getUsername();
            if (visit.Customer.IsVip)
            {
                if (visit.UnlockFor == "ALL" || IsVIPMANAGE()) return null;
                if (visit.UnlockFor == null || !("," + visit.UnlockFor + ",").Contains("," + current_username + ","))
                {
                    return new MsgModel()
                    {
                        ViMessage = "Bạn không được phép truy cập hồ sơ này",
                        EnMessage = "Bạn không được phép truy cập hồ sơ này"
                    };
                };
            }
            return null;
        }
        protected MsgModel GetEDVipInfo(ED ed)
        {
            var current_username = getUsername();
            if (ed.Customer.IsVip)
            {
                if (ed.UnlockFor == "ALL" || IsVIPMANAGE()) return null;
                if (ed.UnlockFor == null || !("," + ed.UnlockFor + ",").Contains("," + current_username + ","))
                {
                    return new MsgModel()
                    {
                        ViMessage = "Bạn không được phép truy cập hồ sơ này",
                        EnMessage = "Bạn không được phép truy cập hồ sơ này"
                    };
                };
            }
            return null;
        }
        protected MsgModel GetIPDVipInfo(IPD ipd)
        {
            var current_username = getUsername();
            if (ipd.Customer.IsVip)
            {
                if (ipd.UnlockFor == "ALL" || IsVIPMANAGE()) return null;
                if (ipd.UnlockFor == null || !("," + ipd.UnlockFor + ",").Contains("," + current_username + ","))
                {
                    return new MsgModel()
                    {
                        ViMessage = "Bạn không được phép truy cập hồ sơ này",
                        EnMessage = "Bạn không được phép truy cập hồ sơ này"
                    };
                };
            }
            return null;
        }
        protected MsgModel GetOPDVipInfo(OPD opd)
        {
            var current_username = getUsername();
            if (opd.Customer.IsVip)
            {
                if (opd.UnlockFor == "ALL" || IsVIPMANAGE()) return null;
                if (opd.UnlockFor == null || !("," + opd.UnlockFor + ",").Contains("," + current_username + ","))
                {
                    return new MsgModel()
                    {
                        ViMessage = "Bạn không được phép truy cập hồ sơ này",
                        EnMessage = "Bạn không được phép truy cập hồ sơ này"
                    };
                };
            }
            return null;
        }
        protected ED GetEDData(Guid id)
        {
            var ed = unitOfWork.EDRepository.GetById(id);
            if (ed == null || ed.IsDeleted)
                return null;
            return ed;
        }
        protected EOC GetEOCData(Guid id)
        {
            var visit = unitOfWork.EOCRepository.GetById(id);
            if (visit == null || visit.IsDeleted)
                return null;
            return visit;
        }
        protected OPD GetOPDData(Guid id)
        {
            var opd = unitOfWork.OPDRepository.GetById(id);
            if (opd == null || opd.IsDeleted)
                return null;
            return opd;
        }
        protected IPD GetIPDData(Guid id)
        {
            var ipd = unitOfWork.IPDRepository.GetById(id);
            if (ipd == null || ipd.IsDeleted)
                return null;
            return ipd;
        }
        protected dynamic GetVisitIdByRecodeCode(string recode_code)
        {
            var ed = unitOfWork.EDRepository.Find(e => !e.IsDeleted && e.RecordCode == recode_code).FirstOrDefault();
            if (ed != null)
                return ed;
            var opd = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.RecordCode == recode_code).FirstOrDefault();
            if (opd != null)
                return opd;
            var ipd = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.RecordCode == recode_code).FirstOrDefault();
            if (ipd != null)
                return ipd;
            var eoc = unitOfWork.EOCRepository.Find(e => !e.IsDeleted && e.RecordCode == recode_code).FirstOrDefault();
            if (eoc != null)
                return eoc;
            return null;
        }
        protected bool HasInHospitalVisitIn24hByPid(string PID)
        {
            var now = DateTime.Now;
            double timeToBlock = 1;
            var blockto = now.AddDays(timeToBlock);
            var current_user_id = GetUser().Id;

            var ed = unitOfWork.EDRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID && e.PrimaryDoctorId == current_user_id && (e.DischargeDate == null || e.DischargeDate <= blockto)).FirstOrDefault();
            if (ed != null) return true;

            var opd = unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID && (e.PrimaryDoctorId == current_user_id || e.AuthorizedDoctorId == current_user_id) && (e.DischargeDate == null || e.DischargeDate <= blockto)).FirstOrDefault();
            if (opd != null) return true;

            var ipd = unitOfWork.IPDRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID && e.PrimaryDoctorId == current_user_id && (e.DischargeDate == null || e.DischargeDate <= blockto)).FirstOrDefault();
            if (ipd != null) return true;

            var eoc = unitOfWork.EOCRepository.Find(e => !e.IsDeleted && e.Customer.PID == PID && e.PrimaryDoctorId == current_user_id && (e.DischargeDate == null || e.DischargeDate <= blockto)).FirstOrDefault();
            if (eoc != null) return true;
            return false;
        }
        protected void HandleUpdateOrCreateOrder(Guid visit_id, string type, JToken request_datas)
        {
            List<Order> listInsert = new List<Order>();
            List<Order> listUpdate = new List<Order>();
            foreach (var item in request_datas)
            {
                string item_id = item["Id"]?.ToString();
                if (string.IsNullOrEmpty(item_id))
                {
                    CreateOrder(visit_id, type, item, ref listInsert);
                }
                else
                {
                    Guid order_id = new Guid(item_id);
                    Order order = unitOfWork.OrderRepository.GetById(order_id);
                    UpdateOrder(order, item, ref listUpdate);
                }
            }
            if (listInsert.Count > 0)
            {
                unitOfWorkDapper.OrderDtoRepository.Adds(Mapper.Map<List<OrderDto>>(listInsert));
            }
            if (listUpdate.Count > 0)
            {
                unitOfWorkDapper.OrderDtoRepository.Updates(Mapper.Map<List<OrderDto>>(listUpdate));
            }
        }
        protected void CreateOrder(Guid visit_id, string type, JToken item, ref List<Order> listInsert)
        {
            Order order = new Order();
            order.VisitId = visit_id;
            order.OrderType = type;
            order.Drug = item["Drug"]?.ToString();
            order.Dosage = item["Dosage"]?.ToString();
            order.Route = item["Route"]?.ToString();
            order.Note = item["Note"]?.ToString();
            order.MedicationPlan = item["MedicationPlan"]?.ToString();
            var last_dose_date = item["LastDoseDate"]?.ToString();
            if (!string.IsNullOrEmpty(last_dose_date))
                order.LastDoseDate = DateTime.ParseExact(item["LastDoseDate"]?.ToString(), Constant.DATE_FORMAT, null);
            else
                order.LastDoseDate = null;
            listInsert.Add(order);
        }
        protected void UpdateOrder(Order order, JToken item, ref List<Order> listUpdate)
        {
            var old = new
            {
                order.Drug,
                order.Dosage,
                order.Route,
                LastDoseDate = order.LastDoseDate?.ToString(Constant.DATE_FORMAT),
                order.Note,
                order.MedicationPlan
            };
            var _new = new
            {
                Drug = item["Drug"]?.ToString(),
                Dosage = item["Dosage"]?.ToString(),
                Route = item["Route"]?.ToString(),
                LastDoseDate = item["LastDoseDate"]?.ToString(),
                Note = item["Note"]?.ToString(),
                MedicationPlan = item["MedicationPlan"]?.ToString(),
                IsDeleted = item.Value<bool>("IsDeleted")
            };

            if (JsonConvert.SerializeObject(old) != JsonConvert.SerializeObject(_new))
            {
                order.IsDeleted = _new.IsDeleted;
                order.Drug = _new.Drug;
                order.Dosage = _new.Dosage;
                order.Route = _new.Route;
                if (!string.IsNullOrEmpty(_new.LastDoseDate))
                    order.LastDoseDate = DateTime.ParseExact(_new.LastDoseDate, Constant.DATE_FORMAT, null);
                else
                    order.LastDoseDate = null;
                order.Note = _new.Note;
                order.MedicationPlan = _new.MedicationPlan;
                listUpdate.Add(order);
            }
        }
        protected dynamic GetPatientMedicationList(Guid visit_id, string type)
        {
            var listDto = unitOfWorkDapper.OrderDtoRepository.Find(
                i => i.IsDeleted == false &&
                i.VisitId == visit_id &&
                i.OrderType == type
            ).ToList().OrderBy(o => o.CreatedAt).Select(o => new
            {
                o.Id,
                o.Drug,
                o.Dosage,
                o.Route,
                LastDoseDate = o.LastDoseDate?.ToString(Constant.DATE_FORMAT),
                o.Note,
                o.MedicationPlan
            });
            return listDto.Distinct();
        }
        protected object NotFoundData(IPD ipd, string formCode)
        {
            return new { ViMessage = "Form không tồn tại", EnMessage = "Form is not found", IsLocked = IPDIsBlock(ipd, formCode) };
        }
        protected string GetAppConfig(string key)
        {
            var config_in_db = unitOfWork.AppConfigRepository.Find(e => !e.IsDeleted && e.Key == key).FirstOrDefault();
            var isTest = (config_in_db != null && !string.IsNullOrEmpty(config_in_db?.Value));
            var config = ConfigurationManager.AppSettings[key]?.ToString();
            return isTest ? config_in_db?.Value : (string.IsNullOrEmpty(config) ? "" : config);
        }
        protected IPDSetupMedicalRecord GetSetupMedicalRecord(string formCode, Guid? specialityId)
        {
            return unitOfWork.IPDSetupMedicalRecordRepository.FirstOrDefault(x => !x.IsDeleted && x.Formcode.ToUpper() == formCode.ToUpper() && x.SpecialityId == specialityId);
        }
        protected IPDMedicalRecordOfPatients GetMedicalRecordOfPatients(string formCode, Guid? visitId, Guid? formId)
        {
            if (string.IsNullOrEmpty(formCode) || visitId == null || formId == null) return null;
            return unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(x => !x.IsDeleted && x.FormCode.ToUpper() == formCode.ToUpper() && x.VisitId == visitId && x.FormId == formId);
        }
        protected List<DiagnosticReportingCharge> GetDiagnosticReportingByPid(Guid cus_id)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                var query = (from dia_sql in unitOfWork.DiagnosticReportingRepository.AsQueryable().Where(
                                e => !e.IsDeleted && e.Status == 2
                                )
                             join charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable()
                             on dia_sql.ChargeItemId equals charge_item_sql.Id

                             join charge_sql in unitOfWork.ChargeRepository.AsQueryable()
                             on charge_item_sql.ChargeId equals charge_sql.Id
                             join charge_visit_sql in unitOfWork.ChargeVistRepository.AsQueryable()
                             on charge_sql.ChargeVisitId equals charge_visit_sql.Id
                             join ser_sql in unitOfWork.ServiceRepository.AsQueryable()
                             on charge_item_sql.ServiceId equals ser_sql.Id
                             where charge_item_sql.CustomerId == cus_id
                             select new DiagnosticReportingCharge
                             {
                                 PID = charge_item_sql.PatientId,
                                 CreatedAt = charge_item_sql.CreatedAt,
                                 CreatedBy = charge_item_sql.CreatedBy,
                                 ChargeItemId = charge_item_sql.Id,
                                 AreaName = charge_visit_sql.AreaName,
                                 ServiceCode = ser_sql.Code,
                                 ServiceName = ser_sql.ViName,
                                 ExamCompleted = dia_sql.ExamCompleted,
                                 Status = dia_sql.Status,
                                 Technique = dia_sql.Technique,
                                 Findings = dia_sql.Findings,
                                 Impression = dia_sql.Impression,
                                 VisitGroupType = charge_item_sql.VisitGroupType,
                                 VisitCode = charge_item_sql.VisitCode,
                                 PickupAt = dia_sql.UpdatedAt,
                                 PickupBy = dia_sql.CreatedBy,
                                 Id = dia_sql.Id,
                                 NguoiTraKQ = dia_sql.UpdatedBy
                             });
                return query.ToListNoLock()
                    .OrderByDescending(m => m.PickupAt).ToList();
            }
        }
        protected List<DiagnosticReportingCharge> GetDiagnosticReportingByPid(Guid cus_id, string VisitCode)
        {
            var query = (from dia_sql in unitOfWork.DiagnosticReportingRepository.AsQueryable().Where(
                            e => !e.IsDeleted && e.Status == 2
                            )
                         join charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable()
                         on dia_sql.ChargeItemId equals charge_item_sql.Id
                         join charge_sql in unitOfWork.ChargeRepository.AsQueryable()
                         on charge_item_sql.ChargeId equals charge_sql.Id
                         join charge_visit_sql in unitOfWork.ChargeVistRepository.AsQueryable()
                         on charge_sql.ChargeVisitId equals charge_visit_sql.Id
                         join ser_sql in unitOfWork.ServiceRepository.AsQueryable()
                         on charge_item_sql.ServiceId equals ser_sql.Id
                         where charge_item_sql.CustomerId == cus_id
                         select new DiagnosticReportingCharge
                         {
                             PID = charge_item_sql.PatientId,
                             CustomerId = charge_item_sql.CustomerId,
                             CreatedAt = charge_item_sql.CreatedAt,
                             CreatedBy = charge_item_sql.CreatedBy,
                             ChargeItemId = charge_item_sql.Id,
                             AreaName = charge_visit_sql.AreaName,
                             ServiceCode = ser_sql.Code,
                             ServiceName = ser_sql.ViName,
                             ExamCompleted = dia_sql.ExamCompleted,
                             Status = dia_sql.Status,
                             Technique = dia_sql.Technique,
                             Findings = dia_sql.Findings,
                             Impression = dia_sql.Impression,
                             VisitGroupType = charge_item_sql.VisitGroupType,
                             VisitCode = charge_item_sql.VisitCode,
                             PickupAt = dia_sql.UpdatedAt,
                             PickupBy = dia_sql.CreatedBy,
                             Id = dia_sql.Id,
                         });
            return query.ToListNoLock()
                .Where(e => e.VisitCode == VisitCode).OrderBy(m => m.ExamCompleted).ToList();
        }
        //protected dynamic GroupDiagnosticReportingByPid(string PID)
        //{
        //    List<DiagnosticReportingCharge> DiagnosticReporting = GetDiagnosticReportingByPid(PID);
        //    return DiagnosticReporting.GroupBy(e => e.ExamCompleted.Value.Date).Select(e => new
        //    {
        //        RawDate = e.Key,
        //        Date = e.Key.ToString(Constant.DATE_FORMAT),
        //        Datas = e.ToList()
        //    });
        //}
        protected dynamic GroupDiagnosticReportingByPid(Guid customer_id)
        {
            List<DiagnosticReportingCharge> DiagnosticReporting = GetDiagnosticReportingByPid(customer_id);
            return DiagnosticReporting.GroupBy(e => e.ExamCompleted.Value.Date).Select(e => new
            {
                RawDate = e.Key,
                Date = e.Key.ToString(Constant.DATE_FORMAT),
                Datas = e.ToList()
            });
        }
        protected int? IntTryParse(string val)
        {
            int outValue;
            return int.TryParse(val, out outValue) ? (int?)outValue : null;
        }

        protected float? FloatTryParse(string val)
        {
            float outValue;
            return float.TryParse(val, out outValue) ? (float?)outValue : null;
        }

        protected void CreateOrUpdateIPDIPDInitialAssessmentToByFormType(IPD visit, string Type, Guid? FormId)
        {
            if (visit == null)
                return;
            var checkMedical = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                .Where(m => m.VisitId == visit.Id && m.FormCode == Type && m.FormId == FormId && !m.IsDeleted)
                                .FirstOrDefault();
            if (checkMedical == null)
            {
                IPDMedicalRecordOfPatients medicalRecord = new IPDMedicalRecordOfPatients()
                {
                    VisitId = visit.Id,
                    FormCode = Type,
                    FormId = FormId,
                };
                unitOfWork.IPDMedicalRecordOfPatientRepository.Add(medicalRecord);
            }
            else
            {
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(checkMedical);
            }
            unitOfWork.Commit();
        }
        protected bool IsDiagnosticReported(Guid charge_item_id)
        {
            return unitOfWork.DiagnosticReportingRepository.Find(e => !e.IsDeleted && e.ChargeItemId == charge_item_id && e.Status != 0).Any();
        }
        protected List<VisitModel> GetAllInfoCustomerInAreIPD(IPD ipd)
        {
            var results = new List<VisitModel>();
            var ipds = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted && !e.IsDraft && string.IsNullOrEmpty(e.DeletedBy) &&
                        e.CustomerId != null &&
                        e.CustomerId == ipd.CustomerId
                        )
                        join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                            on visit.SpecialtyId equals spec.Id into spec_list
                        from specialty in spec_list.DefaultIfEmpty()
                        join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                            on visit.EDStatusId equals stt_sql.Id
                        join mere in unitOfWork.IPDMedicalRecordRepository.AsQueryable()
                            on visit.IPDMedicalRecordId equals mere.Id into mere_list
                        from mere in mere_list.DefaultIfEmpty()
                        join di in unitOfWork.IPDMedicalRecordPart2Repository.AsQueryable()
                            on mere.IPDMedicalRecordPart2Id equals di.Id into di_list
                        from di in di_list.DefaultIfEmpty()
                        join doc in unitOfWork.UserRepository.AsQueryable()
                            on visit.PrimaryDoctorId equals doc.Id into doctor_list
                        from doctor in doctor_list.DefaultIfEmpty()
                        select new VisitModel
                        {
                            Id = visit.Id,
                            ExaminationTime = visit.AdmittedDate,
                            VisitCode = visit.VisitCode,
                            RecordCode = visit.RecordCode,
                            EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                            StatusId = visit.EDStatusId,
                            Status = new { stt_sql.EnName, stt_sql.ViName, stt_sql.Code },
                            Type = "IPD",
                            SpecialtyId = visit.SpecialtyId,
                            Specialty = new { specialty.ViName, specialty.EnName, Site = specialty.Site.Name },
                            Fullname = doctor.Username,
                            CreatedAt = di.CreatedAt,
                            UpdatedAt = di.UpdatedAt,
                            NurseUsername = visit.CreatedBy,
                            DoctorUsername = doctor.Username,
                            UnlockFor = visit.UnlockFor,
                            VisitTypeGroup = visit.Specialty.VisitTypeGroup.Code
                        }).ToList();
            results.AddRange(ipds);
            results = results.OrderByDescending(e => e.ExaminationTime).ToList();
            return results;
        }
        protected IPDMedicalRecordOfPatients GetLastIPDMedicalRecordOfPatients(Guid VisitId)
        {
            var MedicalCode = (from ipd_sql in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable().Where(
                        e => e.FormType == "MedicalRecords")
                               select ipd_sql.Formcode).ToList();

            var IPDMedicalRecordOfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                .Where(r => r.VisitId == VisitId && MedicalCode.Contains(r.FormCode) && !r.IsDeleted)
                                .OrderByDescending(e => e.UpdatedAt)
                                .FirstOrDefault();
            return IPDMedicalRecordOfPatient;
        }
        protected List<string> GetListObjAction(Guid user_id)
        {
            var actions = (from user_role in unitOfWork.UserRoleRepository.AsQueryable()
                            .Where(
                                e => !e.IsDeleted &&
                                e.UserId != null &&
                                e.UserId == user_id &&
                                e.RoleId != null
                            )
                           join role_action in unitOfWork.RoleActionRepository.AsQueryable() on user_role.RoleId equals role_action.RoleId
                           where !role_action.IsDeleted
                           select role_action.Action.Code
                            ).Distinct().ToList();
            return actions;
        }
        protected List<string> GetListObjAction()
        {

            try
            {
                var ip = GetIp();
                if (!IsProInv() && ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    var user = GetUserDev(ip);
                    return GetListObjAction(user.Id);
                }
                var identity = (ClaimsIdentity)User.Identity;
                var roles = identity.Claims.FirstOrDefault(c => c.Type == "Roles")?.Value;
                if (string.IsNullOrEmpty(roles)) return new List<string>();
                return roles.Split(',').ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        protected void ConvertedJsonToIC10Table(Guid VisitId, Guid FormId, string MasterDataCode, string jsonString)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            List<ICDJsonModel> routes_list = json_serializer.Deserialize<List<ICDJsonModel>>(jsonString);
            List<ICD10Visit> icd10_list = Mapper.Map<List<ICDJsonModel>, List<ICD10Visit>>(routes_list);

            List<ICD10Visit> icd10VisitDBs = unitOfWork.ICD10VisitRepository.Find(x => x.VisitId == VisitId && x.FormId == FormId && x.MasterDataCode == x.MasterDataCode && !x.IsDeleted).ToList();
            foreach (var icd10 in icd10_list)
            {
                var data = icd10VisitDBs.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == icd10.Code);

                if (data != null)
                {
                    data.EnName = icd10.EnName;
                    data.Name = icd10.Name;
                    unitOfWork.ICD10VisitRepository.Update(data);
                }
                else
                {
                    icd10.VisitId = VisitId;
                    icd10.FormId = FormId;
                    icd10.MasterDataCode = MasterDataCode;
                    unitOfWork.ICD10VisitRepository.Add(icd10);
                }
            }
            unitOfWork.Commit();
        }

        protected dynamic GetPromissoryNote(Guid visitId, Guid specialtyId, List<listMedicalRecordIsDeploy> getPromissoryNoteBySetup)
        {
            var getPromissoryNoteByPatient = (from p in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                              where !p.IsDeleted && p.VisitId == visitId
                                              join mas in unitOfWork.MasterDataRepository.AsQueryable()
                                              .Where(m => !m.IsDeleted && m.Note == "OPD")
                                              on p.FormCode equals mas.Form
                                              select new listMedicalRecordIsDeploy()
                                              {
                                                  ViName = mas.ViName,
                                                  EnName = mas.EnName,
                                                  Type = mas.Code,
                                                  FormCode = mas.Form
                                              }).ToList().GroupBy(e => e.Type).Select(e => e.FirstOrDefault()).ToList();

            var result = getPromissoryNoteByPatient.Concat(getPromissoryNoteBySetup);

            return result.GroupBy(e => e.Type).Select(e => e.FirstOrDefault()).ToList();
        }
        protected void CreateOrUpdateOPDInitialAssessmentToByFormType(OPD visit, string Type, Guid? FormId)
        {
            if (visit == null)
                return;
            var checkMedical = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                .Where(m => m.VisitId == visit.Id && m.FormCode == Type && m.FormId == FormId && !m.IsDeleted)
                                .FirstOrDefault();
            if (checkMedical == null)
            {
                IPDMedicalRecordOfPatients medicalRecord = new IPDMedicalRecordOfPatients()
                {
                    VisitId = visit.Id,
                    FormCode = Type,
                    FormId = FormId,
                };
                unitOfWork.IPDMedicalRecordOfPatientRepository.Add(medicalRecord);
            }
            else
            {
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(checkMedical);
            }
            unitOfWork.Commit();
        }
        protected object NotFoundDataOPD(OPD opd, string formCode)
        {
            return new { ViMessage = "Form không tồn tại", EnMessage = "Form is not found" };
        }

        protected void CreateOrUpdateFormForSetupOfAdmin(Guid? visitId, Guid? formId, string formCode, bool create_more = true)
        {
            if(create_more)
            {
                if (visitId == null || formId == null || string.IsNullOrEmpty(formCode))
                    return;
                var formOfPatient = (from f in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                     where !f.IsDeleted && f.VisitId == visitId && f.FormId == formId
                                     && f.FormCode == formCode
                                     select f).FirstOrDefault();
                if (formOfPatient == null)
                {
                    IPDMedicalRecordOfPatients medicalRecord = new IPDMedicalRecordOfPatients()
                    {
                        VisitId = (Guid)visitId,
                        FormCode = formCode,
                        FormId = formId
                    };
                    unitOfWork.IPDMedicalRecordOfPatientRepository.Add(medicalRecord);
                }
                else
                {
                    unitOfWork.IPDMedicalRecordOfPatientRepository.Update(formOfPatient);
                }
                unitOfWork.Commit();
            }
            else
            {
                var formOfPatient = (from f in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                     where !f.IsDeleted && f.VisitId == visitId 
                                     && f.FormCode == formCode
                                     select f).FirstOrDefault();
                if (formOfPatient == null)
                {
                    IPDMedicalRecordOfPatients medicalRecord = new IPDMedicalRecordOfPatients()
                    {
                        VisitId = (Guid)visitId,
                        FormCode = formCode,
                        FormId = formId
                    };
                    unitOfWork.IPDMedicalRecordOfPatientRepository.Add(medicalRecord);
                    unitOfWork.Commit();
                }
            }    
        }
        protected string FormatUnicodeString(string s_input)
        {
            return StringHelper.FormatUnicodeString(s_input);
        }
        protected CustomerManualUpdateParameterModel MapCustomerInformationFromHIS(dynamic his_customer, IPD ipd)
        {
            var Province = MapStringFromHis(his_customer.Province, "GENLPCG");
            var District = MapDistrictFromHis(his_customer.District, Province);
            var Ethnic = MapStringFromHis(his_customer.Fork, "GENETHN");
            var Job = MapStringFromHis(his_customer.Job, "GENJOBB");

            var result = new CustomerManualUpdateParameterModel
            {
                PID = his_customer.PID,
                VisitCode = ipd.VisitCode,
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
        protected MasterDataValue MapStringFromHis(string str, string masterDataCode)
        {
            var result = new MasterDataValue()
            {
                ViName = str,
                Code = "",
                Note = ""
            };
            if (string.IsNullOrWhiteSpace(str)) return result;

            var list_data = unitOfWork.MasterDataRepository.Find(s => s.Level == 1 && s.Form == masterDataCode).OrderByDescending(e => e.CreatedAt).ToList();
            var finded_value = list_data.FirstOrDefault(s => FormatUnicodeString(s.Data).Contains(FormatUnicodeString(str)));

            if (finded_value == null) return result;

            result.ViName = finded_value.ViName;
            result.Code = finded_value.Code;
            result.Note = finded_value.Note;

            return result;
        }
        private MasterDataValue MapDistrictFromHis(string district, MasterDataValue province)
        {
            var result = new MasterDataValue()
            {
                ViName = district,
                Code = "",
                Note = ""
            };

            if (string.IsNullOrWhiteSpace(district)) return result;

            if (province == null || string.IsNullOrWhiteSpace(province.Code)) return result;

            var listDistrict = unitOfWork.MasterDataRepository.Find(s => s.Level == 2 && s.Form == "GENLPCG").ToList();
            var District = listDistrict.Where(s => s.Group == province.Code).FirstOrDefault(s => FormatUnicodeString(s.Data).Contains(FormatUnicodeString(district)));
            if (District == null) return result;

            result.ViName = District.ViName;
            result.Code = District.Code;
            result.Note = District.Note;

            return result;
        }

        protected CustomerManualUpdateParameterModel MapCustomerInformationFromHIS(HisCustomer his_customer)
        {
            var Province = MapStringFromHis(his_customer.Province, "GENLPCG");
            var District = MapDistrictFromHis(his_customer.District, Province);
            var Ethnic = MapStringFromHis(his_customer.Fork, "GENETHN");
            var Job = MapStringFromHis(his_customer.Job, "GENJOBB");

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
                MOHObject = !string.IsNullOrEmpty(his_customer.HealthInsuranceNumber) ? "1" : his_customer.Object,
                IsVip = his_customer.IsVip
            };
            return result;
        }


        protected Customer UpdateOHDataForCustomer(Customer customer, CustomerManualUpdateParameterModel his_customer)
        {
            customer.PID = his_customer.PID;
            customer.Fullname = his_customer.Fullname;
            customer.DateOfBirth = his_customer.ConvertedDateOfBirth;
            customer.Phone = his_customer.Phone;
            customer.Gender = his_customer.ConvertedGender;
            // customer.Job = his_customer.Job;
            customer.WorkPlace = his_customer.WorkPlace;
            customer.Relationship = his_customer.Relationship;
            customer.RelationshipContact = his_customer.RelationshipContact;
            customer.Address = his_customer.Address;
            customer.Nationality = his_customer.Nationality;
            //customer.MOHJob = his_customer.MOHJob;
            //customer.MOHJobCode = his_customer.MOHJobCode;
            //customer.MOHEthnic = his_customer.MOHEthnic;
            //customer.MOHEthnicCode = his_customer.MOHEthnicCode;
            //customer.MOHNationality = his_customer.MOHNationality;
            //customer.MOHNationalityCode = his_customer.MOHNationalityCode;
            customer.MOHProvince = his_customer.MOHProvince;
            customer.MOHProvinceCode = his_customer.MOHProvinceCode;
            customer.MOHDistrict = his_customer.MOHDistrict;
            customer.MOHDistrictCode = his_customer.MOHDistrictCode;
            customer.MOHObject = his_customer.MOHObject;
            customer.MOHObjectOther = his_customer.MOHObjectOther;
            customer.MOHAddress = his_customer.MOHAddress;
            customer.IsVip = his_customer.IsVip;
            customer.IdentificationCard = his_customer.IdentificationCard;
            customer.IssueDate = his_customer.ConvertedIssueDate;
            customer.IssuePlace = his_customer.IssuePlace;
            unitOfWork.CustomerRepository.Update(customer);
            unitOfWork.Commit();
            return customer;
        }
        protected Customer CreateOHDataForNewCustomer(CustomerManualUpdateParameterModel his_customer)
        {
            var exit_cus = GetCustomerByPid(his_customer.PID);
            if (exit_cus != null) return exit_cus;

            Customer customer = new Customer();

            customer.PID = his_customer.PID;
            customer.Fullname = his_customer.Fullname;
            customer.DateOfBirth = his_customer.ConvertedDateOfBirth;
            customer.Phone = his_customer.Phone;
            customer.Gender = his_customer.ConvertedGender;
            customer.Job = his_customer.Job;
            customer.WorkPlace = his_customer.WorkPlace;
            customer.Relationship = his_customer.Relationship;
            customer.RelationshipContact = his_customer.RelationshipContact;
            customer.Address = his_customer.Address;
            customer.Nationality = his_customer.Nationality;
            customer.MOHJob = his_customer.MOHJob;
            customer.MOHJobCode = his_customer.MOHJobCode;
            customer.MOHEthnic = his_customer.MOHEthnic;
            customer.MOHEthnicCode = his_customer.MOHEthnicCode;
            customer.MOHNationality = his_customer.MOHNationality;
            customer.MOHNationalityCode = his_customer.MOHNationalityCode;
            customer.MOHProvince = his_customer.MOHProvince;
            customer.MOHProvinceCode = his_customer.MOHProvinceCode;
            customer.MOHDistrict = his_customer.MOHDistrict;
            customer.MOHDistrictCode = his_customer.MOHDistrictCode;
            customer.MOHObject = his_customer.MOHObject;
            customer.MOHAddress = his_customer.MOHAddress;
            customer.MOHObjectOther = his_customer.MOHObjectOther;
            customer.IsVip = his_customer.IsVip;
            customer.IdentificationCard = his_customer.IdentificationCard;
            customer.IssueDate = his_customer.ConvertedIssueDate;
            customer.IssuePlace = his_customer.IssuePlace;
            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();
            return customer;
        }

        protected bool IsVisitLastTimeUpdate(IPD visit, string key)
        {
            string timeStartUpdate = GetAppConfig(key);
            DateTime timeVersion;
            bool succes = DateTime.TryParseExact(timeStartUpdate, "yyyy/MM/dd HH:mm tt zzz", new CultureInfo("en-Us"), DateTimeStyles.None, out timeVersion);
            if (!succes)
                return false;
            if (visit.CreatedAt < timeVersion)
                return false;

            return true;
        }
        protected bool IsForNeonatalMaternityV2(DateTime CreatedAt, string timeStartUpdate)
        {
            DateTime timeVersion;
            bool succes = DateTime.TryParseExact(timeStartUpdate, "yyyy/MM/dd HH:mm tt zzz", new CultureInfo("en-Us"), DateTimeStyles.None, out timeVersion);
            if (!succes)
                return false;
            if (CreatedAt < timeVersion)
                return false;

            return true;
        }

        protected bool IsVisitLastTimeUpdate(ED visit, string key)
        {
            string timeStartUpdate = GetAppConfig(key);
            DateTime timeVersion;
            bool succes = DateTime.TryParseExact(timeStartUpdate, "yyyy/MM/dd HH:mm tt zzz", new CultureInfo("en-Us"), DateTimeStyles.None, out timeVersion);
            if (!succes)
                return false;
            if (visit.CreatedAt < timeVersion)
                return false;
            return true;
        }

        protected string GetActionOfUser(User user, string code)
        {
            var action = (from u in unitOfWork.UserRoleRepository.AsQueryable()
                          join r in unitOfWork.RoleRepository.AsQueryable()
                          on u.RoleId equals r.Id
                          join ra in unitOfWork.RoleActionRepository.AsQueryable()
                          on r.Id equals ra.RoleId
                          join ac in unitOfWork.ActionRepository.AsQueryable()
                          on ra.ActionId equals ac.Id
                          where u.UserId == user.Id && ac.Code == code
                          && !u.IsDeleted && !r.IsDeleted && !ra.IsDeleted
                          && !ac.IsDeleted
                          select ac.Code).FirstOrDefault();
            return action;
        }
        protected Customer SyncInfoFromHis(string pid)
        {
            if (string.IsNullOrWhiteSpace(pid)) return null;

            try
            {
                var hisCustomers = OHClient.searchPatientByPid(pid);

                var customerLocal = unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == pid).FirstOrDefault();

                var his_customer = hisCustomers.FirstOrDefault();

                if (his_customer != null)
                {
                    if (customerLocal != null)
                    {
                        customerLocal = UpdateOHDataForCustomer(customerLocal, MapCustomerInformationFromHIS(his_customer));
                    }
                    else
                    {
                        customerLocal = CreateOHDataForNewCustomer(MapCustomerInformationFromHIS(his_customer));
                    }
                }
                return customerLocal;
            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Info(string.Format("<GET his_customer> Error: {0}", ex));
                return null;
            }

        }
        protected Customer SyncInfoFromHis(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.PID)) return null;

            try
            {
                var hisCustomers = OHClient.searchPatientByPid(customer.PID);

                var customerLocal = customer;

                var his_customer = hisCustomers.FirstOrDefault();

                if (his_customer != null)
                {
                    customerLocal = UpdateOHDataForCustomer(customerLocal, MapCustomerInformationFromHIS(his_customer));
                }
                return customerLocal;
            }
            catch (Exception ex)
            {
                CustomLog.intervaljoblog.Info(string.Format("<GET his_customer> Error: {0}", ex));
                return null;
            }

        }

        protected List<string> GetListPosotionUserByUserName(string userName)
        {
            var user = GetUserByUsername(userName);
            if (user == null)
                return new List<string>() { };
            var poision = user.PositionUsers.Where(p => !p.IsDeleted).Select(e => e.Position?.EnName).ToList();

            return poision;
        }
        protected string GetTreatmentsLast(Guid visitId, Guid? part3Id, List<IPDMedicalRecordPart3Data> part3Datas)
        {
            string treatments_and_procedures = "";
            if (part3Datas == null)  // Api bị 5 lít
                return treatments_and_procedures;

            string formCode = GetFormCodeLastMedicalRecordUpdate(visitId);
            if (!string.IsNullOrEmpty(formCode))
            {
                switch (formCode)
                {
                    case "A01_196_050919_V":
                        var formCodeMedical = unitOfWork.IPDMedicalRecordOfPatientRepository.Find(x => x.VisitId == visitId).OrderByDescending(x => x.UpdatedAt).FirstOrDefault();
                        if (formCodeMedical != null)
                        {
                            treatments_and_procedures = GetTreatmentsAndProceduresUB(part3Id);
                        }
                        break;
                    case "BMTIMMACH":

                        string KHDT = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1102")?.Value;
                        string TVCK = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1104")?.Value;
                        string PT = "";
                        string PTO = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1110")?.Value;

                        string YES = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1106")?.Value;
                        string NO = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1107")?.Value;
                        string GHEPTIM = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1108")?.Value;
                        string PTOD = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1109")?.Value;
                        string OT = part3Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPE1110")?.Value;
                        if (!string.IsNullOrEmpty(YES) && !string.IsNullOrEmpty(NO))
                        {
                            if (bool.Parse(YES) == true && bool.Parse(NO) == false)
                            {
                                if (string.IsNullOrEmpty(GHEPTIM) && string.IsNullOrEmpty(PTOD))
                                {
                                    PT = "Có";
                                }
                                else
                                {
                                    bool flag = false;
                                    if (!string.IsNullOrEmpty(GHEPTIM) && bool.Parse(GHEPTIM) == true)
                                    {
                                        PT = "Ghép tim";
                                        flag = true;
                                    }
                                    if (!string.IsNullOrEmpty(PTOD) && bool.Parse(PTOD) == true)
                                    {
                                        if (!string.IsNullOrEmpty(OT))
                                        {
                                            PT = OT;
                                        }
                                        else
                                        {
                                            PT = "";
                                        }
                                        flag = true;

                                    }
                                    if (!string.IsNullOrEmpty(GHEPTIM) && !string.IsNullOrEmpty(PTOD) && bool.Parse(GHEPTIM) == true && bool.Parse(PTOD) == true)
                                    {
                                        PT = "Ghép tim. " + OT;
                                        flag = true;
                                    }
                                    if (flag == false)
                                    {
                                        PT = "Có";
                                    }
                                }
                            }
                            if (bool.Parse(YES) == false && bool.Parse(NO) == true)
                            {
                                PT = "Không";
                            }
                        }

                        treatments_and_procedures += !string.IsNullOrEmpty(KHDT) ? "+ Kế hoạch điều trị và chăm sóc: " + KHDT + "\n" : "";
                        treatments_and_procedures += !string.IsNullOrEmpty(TVCK) ? "+ Tư vấn chuyên khoa: " + TVCK + "\n" : "";
                        treatments_and_procedures += !string.IsNullOrEmpty(PT) ? "+ Phẫu thuật/ Thủ thuật: " + PT + "\n" : "";

                        break;
                    default:
                        // part3Datas có thể null => part3Datas.FirstOrDefault toang
                        treatments_and_procedures = part3Datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPEPPDTANS")?.Value;
                        break;
                }
            }
            return treatments_and_procedures;
        }
        public string GetTreatmentsAndProceduresUB(Guid? part3_id)
        {
            string value = "";
            // Thêm điều kiện IPDMedicalRecordPart3Id != null, vì part3_id có thể null
            var part3Data = unitOfWork.IPDMedicalRecordPart3DataRepository.AsQueryable().Where(x => x.IPDMedicalRecordPart3Id != null && x.IPDMedicalRecordPart3Id == part3_id);
            var data = (from part3 in part3Data
                        join m in unitOfWork.MasterDataRepository.AsQueryable()
                            on part3.Code equals m.Code
                        select new
                        {
                            Code = part3.Code,
                            Value = part3.Value,
                            ViName = m.ViName,
                        }).ToList();
            var DD = data.Where(x => (x.Code == "IPDMRPE1002" || x.Code == "IPDMRPE1003") && x.Value.ToUpper() == "TRUE").ToList();
            if (DD.Count == 1)
            {

                value += DD[0].ViName.ToString().Contains(":") ? DD[0].ViName.ToString().Trim() + "\n" : DD[0].ViName.ToString().Trim() + ":\n";
            }
            string[] codes1 = new string[] { "IPDMRPE1005", "IPDMRPE1009", "IPDMRPE1013", "IPDMRPE1015", "IPDMRPE1019", "IPDMRPE1021" };
            foreach (var code in codes1)
            {
                var obj = data.FirstOrDefault(x => x.Code == code);
                if (obj != null)
                {
                    switch (code)
                    {
                        case "IPDMRPE1005":
                            var th = data.FirstOrDefault(x => x.Code == "IPDMRPE1007");
                            if (th != null && (!string.IsNullOrEmpty(obj.Value) || !string.IsNullOrEmpty(th.Value)))
                            {
                                value += obj.ViName.ToString().Contains(":") ? obj.ViName.ToString().Trim() + "&emsp;&emsp;tại u: " + obj.Value.Trim() + " Gy&emsp;&emsp;" : obj.ViName.ToString().Trim() + "&emsp;&emsp;tại u: " + obj.Value.Trim() + " Gy&emsp;&emsp;";
                                value += th.ViName.ToString().Contains(":") ? th.ViName.ToString().Trim() + " " + th.Value.Trim() + "\n" : th.ViName.ToString().Trim() + ": " + th.Value.Trim() + "\n";
                            }
                            break;
                        case "IPDMRPE1009":
                            th = data.FirstOrDefault(x => x.Code == "IPDMRPE1011");
                            if (th != null && (!string.IsNullOrEmpty(obj.Value) || !string.IsNullOrEmpty(th.Value)))
                            {
                                value += obj.ViName.ToString().Contains(":") ? obj.ViName.ToString().Trim() + "&emsp;&emsp;tại u: " + obj.Value.Trim() + " Gy&emsp;&emsp;" : obj.ViName.ToString().Trim() + "&emsp;&emsp;tại u: " + obj.Value.Trim() + " Gy&emsp;&emsp;";
                                value += th.ViName.ToString().Contains(":") ? th.ViName.ToString().Trim() + " " + th.Value.Trim() + "\n" : th.ViName.ToString().Trim() + ": " + th.Value.Trim() + "\n";
                            }
                            break;
                        case "IPDMRPE1015":
                            th = data.FirstOrDefault(x => x.Code == "IPDMRPE1017");
                            if (th != null && (!string.IsNullOrEmpty(obj.Value) || !string.IsNullOrEmpty(th.Value)))
                            {
                                value += obj.ViName.ToString().Contains(":") ? obj.ViName.ToString().Trim() + "&emsp;&emsp;tại u: " + obj.Value.Trim() + " Gy&emsp;&emsp;" : obj.ViName.ToString().Trim() + "&emsp;&emsp;tại u: " + obj.Value.Trim() + " Gy&emsp;&emsp;";
                                value += th.ViName.ToString().Contains(":") ? th.ViName.ToString().Trim() + " " + th.Value.Trim() + "\n" : th.ViName.ToString().Trim() + ": " + th.Value.Trim() + "\n";
                            }
                            break;
                        default:
                            if (!string.IsNullOrEmpty(obj.Value))
                            {
                                value += obj.ViName.ToString().Contains(":") ? obj.ViName.ToString().Trim() + " " + obj.Value.Trim() + "\n" : obj.ViName.ToString().Trim() + ": " + obj.Value.Trim() + "\n";
                            }
                            break;
                    }

                    //else
                    //{
                    //    value += obj.ViName.ToString().Contains(":") ? obj.ViName.ToString().Trim() + "&emsp;&emsp;tại u:&emsp;&emsp;" : obj.ViName.ToString().Trim() + ":&emsp;&emsp;tại u:&emsp;&emsp;";
                    //}

                }
            }
            var DAPUNG = data.Where(x => (x.Code == "IPDMRPE1023" || x.Code == "IPDMRPE1024" || x.Code == "IPDMRPE1025") && x.Value.ToUpper() == "TRUE").ToList();
            if (DAPUNG.Count == 1)
            {

                if (!string.IsNullOrEmpty(DAPUNG[0].ViName))
                {
                    var dapungText = data.FirstOrDefault(x => x.Code == "IPDMRPE1026");
                    if (dapungText != null)
                    {
                        value += dapungText.ViName.ToString().Contains(":") ? dapungText.ViName.ToString().Trim() + " " : dapungText.ViName.ToString().Trim() + ": ";
                        value += DAPUNG[0].ViName.ToString().Contains(":") ? DAPUNG[0].ViName.ToString().Trim() : DAPUNG[0].ViName.ToString().Trim() + ": ";
                        value += dapungText?.Value.ToString().Trim() + "\n";
                    }
                }

            }
            string[] codes2 = new string[] { "IPDMRPE1028" };
            foreach (var code in codes2)
            {
                var obj = data.FirstOrDefault(x => x.Code == code);
                if (obj != null && !string.IsNullOrEmpty(obj.Value))
                {
                    value += obj.ViName.ToString().Contains(":") ? obj.ViName.ToString().Trim() + " " + obj.Value + "\n" : obj.ViName.ToString().Trim() + ": " + obj.Value.ToString().Trim() + "\n";
                }

            }
            return value;
        }

        public string GetFormCodeLastMedicalRecordUpdate(Guid visitId)
        {
            var ipdMedicalRecordOfPatients = unitOfWork.IPDMedicalRecordOfPatientRepository.Find(x => !x.IsDeleted && x.VisitId == visitId).OrderByDescending(x => x.UpdatedAt).ToList();
            var forms = unitOfWork.FormRepository.Find(x => !x.IsDeleted && x.Name.Contains("Bệnh án")).ToList();
            string formCode = default;
            foreach (var item in ipdMedicalRecordOfPatients)
            {
                foreach (var form in forms)
                {
                    if (item.FormCode == form.Code)
                    {
                        formCode = item.FormCode;
                        break;
                    }

                }
                if (!string.IsNullOrEmpty(formCode))
                {
                    break;
                }
            }
            return formCode;
        }
        public string GetPersonalHistory(IPD ipd)
        {
            string formCode = GetFormCodeLastMedicalRecordUpdate(ipd.Id);
            string personalHistory = "";
            var part2_data = ipd?.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas;
            if (!string.IsNullOrEmpty(formCode))
            {
                switch (formCode)
                {
                    case "BMTIMMACH":
                        var YESDTD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3001")?.Value;
                        if (!string.IsNullOrEmpty(YESDTD) && bool.Parse(YESDTD) == true)
                        {
                            var valueDTD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3016")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueDTD) ? "" : "+ Đái tháo đường: " + valueDTD + "\n";
                        }
                        var YESTHA = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3002")?.Value;
                        if (!string.IsNullOrEmpty(YESTHA) && bool.Parse(YESTHA) == true)
                        {
                            var valueTHA = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3017")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueTHA) ? "" : "+ Tăng huyết áp: " + valueTHA + "\n";
                        }
                        var YESRLLM = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3003")?.Value;
                        if (!string.IsNullOrEmpty(YESRLLM) && bool.Parse(YESRLLM) == true)
                        {
                            var valueRLLM = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3018")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueRLLM) ? "" : "+ Rối loạn lipid máu: " + valueRLLM + "\n";
                        }
                        var YESKRTICD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3004")?.Value;
                        if (!string.IsNullOrEmpty(YESKRTICD) && bool.Parse(YESKRTICD) == true)
                        {
                            var valueRTICD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3019")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueRTICD) ? "" : "+ Đã đặt máy khử rung tim ICD: " + valueRTICD + "\n";
                        }
                        var YESDBCTCRT = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3005")?.Value;
                        if (!string.IsNullOrEmpty(YESDBCTCRT) && bool.Parse(YESDBCTCRT) == true)
                        {
                            var valueDBCTCRT = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3020")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueDBCTCRT) ? "" : "+ Đã đặt máy tái đồng bộ cơ tim CRT: " + valueDBCTCRT + "\n";
                        }
                        var YESDBCTCRTD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3006")?.Value;
                        if (!string.IsNullOrEmpty(YESDBCTCRTD) && bool.Parse(YESDBCTCRTD) == true)
                        {
                            var valueDBCTCRTD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3021")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueDBCTCRTD) ? "" : "+ Máy tạo nhịp tim tái đồng bộ có khử rung CRT-D: " + valueDBCTCRTD + "\n";
                        }
                        var YESLVAD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3007")?.Value;
                        if (!string.IsNullOrEmpty(YESLVAD) && bool.Parse(YESLVAD) == true)
                        {
                            var valueLVAD = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3022")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueLVAD) ? "" : "+ Đã đặt thiết bị hỗ trợ thất trái LVAD: " + valueLVAD + "\n";
                        }
                        var YESDCGT = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3008")?.Value;
                        if (!string.IsNullOrEmpty(YESDCGT) && bool.Parse(YESDCGT) == true)
                        {
                            var valueDCGT = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3023")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueDCGT) ? "" : "+ Đã cấy ghép tim: " + valueDCGT + "\n";
                        }
                        var YESOT = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3009")?.Value;
                        if (!string.IsNullOrEmpty(YESOT) && bool.Parse(YESOT) == true)
                        {
                            var valueOT = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT3024")?.Value;
                            personalHistory += string.IsNullOrEmpty(valueOT) ? "" : "+ Khác: " + valueOT + "\n";
                        }
                        break;
                    case "A01_041_050919_V":
                        var valueDU = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT02")?.Value;
                        if (!string.IsNullOrEmpty(valueDU))
                        {
                            personalHistory += string.IsNullOrEmpty(valueDU) ? "" : "Dị ứng: " + valueDU + "\n";
                        }
                        var valueTM = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT1058")?.Value;
                        if (!string.IsNullOrEmpty(valueTM))
                        {
                            personalHistory += string.IsNullOrEmpty(valueTM) ? "" : "Tại mắt: " + valueTM + "\n";
                        }
                        var valueTT = part2_data.FirstOrDefault(x => x.Code == "IPDMRPT1645")?.Value;
                        if (!string.IsNullOrEmpty(valueTT))
                        {
                            personalHistory += string.IsNullOrEmpty(valueTT) ? "" : "Toàn thân: " + valueTT + "\n";
                        }
                        break;
                    default:
                        var valueBT = part2_data?.FirstOrDefault(x => x.Code == "IPDMRPTBATHANS")?.Value;
                        personalHistory = string.IsNullOrEmpty(valueBT) ? "" : valueBT;
                        break;
                }
            }
            return personalHistory;
        }
        public List<VisitModel> GetInfoAllAreaInCustomerId(Guid customerId)
        {
            var results = new List<VisitModel>();
            results.AddRange(getEDVisitByCustomerId(customerId));
            results.AddRange(getIPDVisitByCustomerId(customerId));
            results.AddRange(getOPDVisitByCustomerId(customerId));
            results.AddRange(getEDCVisitByCustomerId(customerId));
            return results;
        }
        public List<VisitModel> GetInfoAllAreaInCustomerIdForProm(Guid customerId, DateTime visit_time)
        {
            var results = new List<VisitModel>();
            //results.AddRange(getIPDVisitByCustomerIdForProm(customerId,visit_time));
            results.AddRange(getOPDVisitByCustomerIdForProm(customerId, visit_time));
            return results;
        }
        protected List<VisitModel> getEDVisitByCustomerId(Guid customer_id)
        {
            var results = (from visit in unitOfWork.EDRepository.AsQueryable().Where(
                        e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id
                        )
                           join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                           from specialty in spec_list.DefaultIfEmpty()
                           join spmr in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                           on visit.SpecialtyId equals spmr.SpecialityId into spmr_list
                           from setup in spmr_list.DefaultIfEmpty()
                           join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                               on visit.EDStatusId equals stt_sql.Id
                           join di in unitOfWork.DischargeInformationRepository.AsQueryable()
                               on visit.DischargeInformationId equals di.Id
                           join doc in unitOfWork.UserRepository.AsQueryable()
                               on di.UpdatedBy equals doc.Username into doctor_list
                           from doctor in doctor_list.DefaultIfEmpty()
                           join doct in unitOfWork.UserRepository.AsQueryable()
                               on visit.PrimaryDoctorId equals doct.Id into doctort_list
                           from doctort in doctort_list.DefaultIfEmpty()
                           join nur_sql in unitOfWork.UserRepository.AsQueryable()
                               on visit.CurrentNurseId equals nur_sql.Id into nlist
                           from nur_sql in nlist.DefaultIfEmpty()
                           join chart in unitOfWork.EDObservationChartDataRepository.AsQueryable()
                            on visit.ObservationChartId equals chart.ObservationChartId into chart_data
                           select new VisitModel
                           {
                               Id = visit.Id,
                               ExaminationTime = visit.AdmittedDate,
                               DischargeDate = visit.DischargeDate,
                               VisitCode = visit.VisitCode,
                               RecordCode = visit.RecordCode,
                               EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                               StatusId = visit.EDStatusId,
                               Status = new { stt_sql.EnName, stt_sql.ViName, stt_sql.Code },
                               CheckStatus = stt_sql.EnName,
                               Type = "ED",
                               SpecialtyId = visit.SpecialtyId,
                               Specialty = new { specialty.ViName, specialty.EnName, Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode },
                               SpecialtySite = new { Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode, EnName = specialty.Site.EnName, ViName = specialty.Site.ViName },
                               Fullname = doctor.Username,
                               CreatedAt = di.CreatedAt,
                               UpdatedAt = di.UpdatedAt,
                               NurseUsername = visit.CreatedBy,
                               DoctorUsername = doctort.Username,
                               UnlockFor = visit.UnlockFor,
                               IsTransfer = visit.IsTransfer,
                               TransferFromId = visit.TransferFromId,
                               HandOverCheckListId = visit.HandOverCheckListId,
                               CustomerIsAllergy = (bool)visit.IsAllergy,
                               CustomerAllergy = visit.Allergy,
                               CustomerKindOfAllergy = visit.KindOfAllergy,
                               CreatedBy = visit.CreatedBy,
                               VitalSigns = new VitalSigns
                               {
                                   Pulse = chart_data.AsQueryable().OrderByDescending(x => x.NoteAt).FirstOrDefault().Pulse,
                                   BP = chart_data.AsQueryable().OrderByDescending(x => x.NoteAt).FirstOrDefault().SysBP + "/" + chart_data.AsQueryable().OrderByDescending(x => x.NoteAt).FirstOrDefault().DiaBP,
                                   To = chart_data.AsQueryable().OrderByDescending(x => x.NoteAt).FirstOrDefault().Temperature,
                                   RR = chart_data.AsQueryable().OrderByDescending(x => x.NoteAt).FirstOrDefault().Resp,
                                   Height = null,
                                   Weight = null,
                                   Spo2 = chart_data.AsQueryable().OrderByDescending(x => x.NoteAt).FirstOrDefault().SpO2
                               },
                               Recept = visit.CreatedAt
                           }).ToListNoLock().Distinct().ToList();
            return results;
        }
        protected List<VisitModel> getOPDVisitByCustomerId(Guid customer_id)
        {
            var results = (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted &&
                        e.CustomerId == customer_id
                        )
                           join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                           from specialty in spec_list.DefaultIfEmpty()
                           join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                               on visit.EDStatusId equals stt_sql.Id
                           join di in unitOfWork.OPDOutpatientExaminationNoteRepository.AsQueryable()
                               on visit.OPDOutpatientExaminationNoteId equals di.Id
                           join doc in unitOfWork.UserRepository.AsQueryable()
                               on visit.PrimaryDoctorId equals doc.Id into doctor_list
                           from doctor in doctor_list.DefaultIfEmpty()
                           join doc2 in unitOfWork.UserRepository.AsQueryable()
                               on visit.AuthorizedDoctorId equals doc2.Id into doctor2_list
                           from doctor2 in doctor2_list.DefaultIfEmpty()
                           join nur_sql in unitOfWork.UserRepository.AsQueryable()
                           on visit.PrimaryNurseId equals nur_sql.Id into nlist
                           from nur_sql in nlist.DefaultIfEmpty()
                           join shorterm in unitOfWork.OPDInitialAssessmentForShortTermDataRepository.AsQueryable()
                           on visit.OPDInitialAssessmentForShortTermId equals shorterm.OPDInitialAssessmentForShortTermId into shorterm_data
                           select new VisitModel
                           {
                               Id = visit.Id,
                               ExaminationTime = visit.AdmittedDate,
                               DischargeDate = visit.DischargeDate,
                               VisitCode = visit.VisitCode,
                               RecordCode = visit.RecordCode,
                               EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                               StatusId = visit.EDStatusId,
                               Status = new { stt_sql.EnName, stt_sql.ViName, stt_sql.Code },
                               CheckStatus = stt_sql.EnName,
                               Type = "OPD",
                               SpecialtyId = visit.SpecialtyId,
                               Specialty = new { specialty.ViName, specialty.EnName, Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode },
                               SpecialtySite = new { Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode, EnName = specialty.Site.EnName, ViName = specialty.Site.ViName },
                               Fullname = doctor.Username,
                               CreatedAt = di.CreatedAt,
                               UpdatedAt = di.UpdatedAt,
                               NurseUsername = nur_sql.Username,
                               DoctorUsername = doctor.Username,
                               AuthorizedDoctorUsername = doctor2.Username,
                               UnlockFor = visit.UnlockFor,
                               IsTransfer = visit.IsTransfer,
                               TransferFromId = visit.TransferFromId,
                               HandOverCheckListId = visit.OPDHandOverCheckListId,
                               CustomerIsAllergy = (bool)visit.IsAllergy,
                               CustomerAllergy = visit.Allergy,
                               CustomerKindOfAllergy = visit.KindOfAllergy,
                               IsPreAnesthesia = visit.IsAnesthesia,
                               CreatedBy = visit.CreatedBy,
                               VitalSigns = new VitalSigns
                               {
                                   Pulse = shorterm_data.FirstOrDefault(x => x.Code == "OPDIAFSTOPPULANS" && !x.IsDeleted).Value,
                                   BP = shorterm_data.FirstOrDefault(x => x.Code == "OPDIAFSTOPBP0ANS" && !x.IsDeleted).Value,
                                   To = shorterm_data.FirstOrDefault(x => x.Code == "OPDIAFSTOPTEMANS" && !x.IsDeleted).Value,
                                   RR = shorterm_data.FirstOrDefault(x => x.Code == "OPDIAFSTOPRR0ANS" && !x.IsDeleted).Value,
                                   Height = shorterm_data.FirstOrDefault(x => x.Code == "OPDIAFSTOPHEIANS" && !x.IsDeleted).Value,
                                   Weight = shorterm_data.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIANS" && !x.IsDeleted).Value,
                                   Spo2 = shorterm_data.FirstOrDefault(x => x.Code == "OPDIAFSTOPSPOANS" && !x.IsDeleted).Value
                               },
                               Recept = visit.CreatedAt
                           }).ToListNoLock().Distinct().ToList();
            return results;
        }
        protected List<VisitModel> getOPDVisitByCustomerIdForProm(Guid customer_id, DateTime visit_time)
        {
            var no_examination = unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.EnName) &&
                Constant.Admitted.Contains(e.Code) &&
                e.VisitTypeGroupId != null &&
                e.VisitTypeGroup.Code == "OPD"
            );
            return (from visit in unitOfWork.OPDRepository.AsQueryable().Where(
                        e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id &&
                        e.AdmittedDate <= visit_time &&
                        e.EDStatusId != null &&
                        e.EDStatusId == no_examination.Id
                                                                                )
                    select new VisitModel
                    {
                        Id = visit.Id,
                        Type = "OPD",
                        ExaminationTime = visit.AdmittedDate
                    }
                        ).ToList();

        }
        protected List<VisitModel> getIPDVisitByCustomerId(Guid customer_id)
        {

            var results = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                            e => !e.IsDeleted && !e.IsDraft && string.IsNullOrEmpty(e.DeletedBy) &&
                            e.CustomerId != null &&
                            e.CustomerId == customer_id
                        )
                           join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                           from specialty in spec_list.DefaultIfEmpty()
                           join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                               on visit.EDStatusId equals stt_sql.Id
                           join spec1 in unitOfWork.SpecialtyRepository.AsQueryable()
                           on visit.SpecialtyId equals spec1.Id
                           join mere in unitOfWork.IPDMedicalRecordRepository.AsQueryable()
                               on visit.IPDMedicalRecordId equals mere.Id into mere_list
                           from mere in mere_list.DefaultIfEmpty()
                           join di in unitOfWork.IPDMedicalRecordPart2Repository.AsQueryable()
                               on mere.IPDMedicalRecordPart2Id equals di.Id into di_list
                           from di in di_list.DefaultIfEmpty()
                           join doc in unitOfWork.UserRepository.AsQueryable()
                               on visit.PrimaryDoctorId equals doc.Id into doctor_list
                           from doctor in doctor_list.DefaultIfEmpty()
                           join nur_sql in unitOfWork.UserRepository.AsQueryable()
                           on visit.PrimaryNurseId equals nur_sql.Id into nlist
                           from nur_sql in nlist.DefaultIfEmpty()
                           join hpi in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                               on new { mere.IPDMedicalRecordPart2Id, Code = "IPDMRPTQTBLANS" } equals new { hpi.IPDMedicalRecordPart2Id, hpi.Code } into hpi_list
                           from history_of_present_illness in hpi_list.DefaultIfEmpty()
                           join initAssess in unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                            on visit.IPDInitialAssessmentForAdultId equals initAssess.IPDInitialAssessmentForAdultId into adult_list
                           select new VisitModel
                           {
                               Id = visit.Id,
                               ExaminationTime = visit.AdmittedDate,
                               DischargeDate = visit.DischargeDate,
                               VisitCode = visit.VisitCode,
                               RecordCode = visit.RecordCode,
                               EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                               StatusId = visit.EDStatusId,
                               StatusCode = stt_sql.Code,
                               CheckStatus = stt_sql.EnName,
                               Status = new { stt_sql.EnName, stt_sql.ViName, stt_sql.Code },
                               Type = "IPD",
                               SpecialtyId = visit.SpecialtyId,
                               Specialty = new { specialty.ViName, specialty.EnName, Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode },
                               SpecialtySite = new { Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode, EnName = specialty.Site.EnName, ViName = specialty.Site.ViName },
                               Fullname = doctor.Username,
                               CreatedAt = di.CreatedAt,
                               UpdatedAt = di.UpdatedAt,
                               NurseUsername = nur_sql.Username,
                               DoctorUsername = doctor.Username,
                               UnlockFor = visit.UnlockFor,
                               IsTransfer = visit.IsTransfer,
                               TransferFromId = visit.TransferFromId,
                               HandOverCheckListId = visit.HandOverCheckListId,
                               HistoryOfPresentIllness = history_of_present_illness.Value,
                               CustomerIsAllergy = (bool)visit.IsAllergy,
                               CustomerAllergy = visit.Allergy,
                               CustomerKindOfAllergy = visit.KindOfAllergy,
                               CreatedBy = visit.CreatedBy,
                               VitalSigns = new VitalSigns
                               {
                                   Pulse = adult_list.AsQueryable().FirstOrDefault(x => x.Code == "IPDIAAUPULSANS" && !x.IsDeleted).Value,
                                   BP = adult_list.FirstOrDefault(x => x.Code == "IPDIAAUBLPRANS" && !x.IsDeleted).Value,
                                   To = adult_list.FirstOrDefault(x => x.Code == "IPDIAAUTEMPANS" && !x.IsDeleted).Value,
                                   RR = adult_list.FirstOrDefault(x => x.Code == "IPDIAAURERAANS" && !x.IsDeleted).Value,
                                   Height = adult_list.FirstOrDefault(x => x.Code == "IPDIAAUHEIGANS" && !x.IsDeleted).Value,
                                   Weight = adult_list.FirstOrDefault(x => x.Code == "IPDIAAUWEIGANS" && !x.IsDeleted).Value,
                               },
                               Recept = visit.CreatedAt
                           }).Where(e => e.StatusCode != "IPDNOEX").ToListNoLock().Distinct().ToList();
            return results;
        }
        protected List<VisitModel> getEDCVisitByCustomerId(Guid customer_id)
        {
            var results = (from visit in unitOfWork.EOCRepository.AsQueryable().Where(
                        e => !e.IsDeleted && string.IsNullOrEmpty(e.DeletedBy) &&
                        e.CustomerId != null &&
                        e.CustomerId == customer_id
                        )
                           join spec in unitOfWork.SpecialtyRepository.AsQueryable()
                               on visit.SpecialtyId equals spec.Id into spec_list
                           from specialty in spec_list.DefaultIfEmpty()
                           join stt_sql in unitOfWork.EDStatusRepository.AsQueryable()
                               on visit.StatusId equals stt_sql.Id
                           join hocl in unitOfWork.EOCHandOverCheckListRepository.AsQueryable()
                               on visit.Id equals hocl.VisitId into hocl_list
                           from hoc in hocl_list.DefaultIfEmpty()
                           join doc in unitOfWork.UserRepository.AsQueryable()
                               on visit.PrimaryDoctorId equals doc.Id into doctor_list
                           from doctor in doctor_list.DefaultIfEmpty()
                           join nur_sql in unitOfWork.UserRepository.AsQueryable()
                           on visit.PrimaryNurseId equals nur_sql.Id into nlist
                           from nur_sql in nlist.DefaultIfEmpty()
                           select new VisitModel
                           {
                               Id = visit.Id,
                               ExaminationTime = visit.AdmittedDate,
                               DischargeDate = visit.DischargeDate,
                               VisitCode = visit.VisitCode,
                               RecordCode = visit.RecordCode,
                               EHOSVisitCode = doctor.EHOSAccount + visit.VisitCode,
                               StatusId = visit.StatusId,
                               Status = new { stt_sql.EnName, stt_sql.ViName, stt_sql.Code },
                               CheckStatus = stt_sql.EnName,
                               Type = "EOC",
                               SpecialtyId = visit.SpecialtyId,
                               Specialty = new { specialty.ViName, specialty.EnName, Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode },
                               SpecialtySite = new { Site = specialty.Site.Name, ApiCode = specialty.Site.ApiCode, EnName = specialty.Site.EnName, ViName = specialty.Site.ViName },
                               Fullname = doctor.Username,
                               CreatedAt = visit.CreatedAt,
                               UpdatedAt = visit.UpdatedAt,
                               NurseUsername = nur_sql.Username,
                               DoctorUsername = doctor.Username,
                               UnlockFor = visit.UnlockFor,
                               IsTransfer = visit.IsTransfer,
                               TransferFromId = visit.TransferFromId,
                               HandOverCheckListId = hoc.Id,
                               CustomerIsAllergy = (bool)visit.IsAllergy,
                               CustomerAllergy = visit.Allergy,
                               CustomerKindOfAllergy = visit.KindOfAllergy,
                               CreatedBy = visit.CreatedBy,
                           }).ToListNoLock().Distinct().ToList();
            return results;
        }
        protected List<VisitModel> getIPDVisitByCustomerIdForProm(Guid customer_id, DateTime visit_time)
        {
            var results = (from visit in unitOfWork.IPDRepository.AsQueryable().Where(
                            e => !e.IsDeleted && !e.IsDraft && string.IsNullOrEmpty(e.DeletedBy) &&
                            e.CustomerId != null &&
                            e.CustomerId == customer_id
                            && e.AdmittedDate <= visit_time
                            )
                           select new VisitModel
                           {
                               Id = visit.Id,
                               Type = "IPD",
                               ExaminationTime = visit.AdmittedDate
                           }).ToList();
            return results;
        }

        protected string GetAndFormatDiagnosis(Guid? visitId, string visitType)
        {
            if (visitId == null)
                return "";

            StringBuilder builder = new StringBuilder();
            var getcd = GetVisitDiagnosisAndICD((Guid)visitId, visitType, true);
            builder.Append($"{(string.IsNullOrEmpty(getcd.Diagnosis) ? "" : getcd.Diagnosis)}");
            builder.Append($"{(string.IsNullOrEmpty(getcd.DiagnosisOption) ? "" : (string.IsNullOrEmpty(getcd.Diagnosis) ? getcd.DiagnosisOption : ", " + getcd.DiagnosisOption))}");

            string[] array_Icd = { getcd.ICD, getcd.ICDOption };
            builder.Append(GetAndFormatICD10(array_Icd));

            return builder.ToString();
        }


        private string GetAndFormatICD10(string[] texts)
        {
            string result = String.Empty;

            foreach (var text in texts)
            {
                string str_text = text;
                if (text == null || text == $"\"\"")
                    str_text = "";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(str_text);
                string _str = String.Empty;
                if (objs != null)
                {
                    int lengthOfobjs = objs.Count;
                    for (int j = 0; j < lengthOfobjs; j++)
                    {
                        var codeIcd10 = objs[j]["code"]?.ToString();
                        if (j == 0)
                            _str += codeIcd10;
                        else
                            _str += $", {codeIcd10}";
                    }

                    if (!string.IsNullOrEmpty(result))
                        result += "/ " + _str;
                    else
                        result += _str;
                }
            }

            if (string.IsNullOrEmpty(result))
                return "";

            string format = $" ({result})";
            return format;
        }

        protected int? CaculatorAgeCustormer(DateTime? dOb_custormer)
        {
            if (dOb_custormer == null)
                return null;

            var now = DateTime.Now;

            TimeSpan totalAge = now - (DateTime)dOb_custormer;
            int age = (int)totalAge.TotalDays / 365;

            return age;
        }

        protected TransferInfoModel GetFirstIpdInVisitTypeIPD(IPD ipd)
        {
            if (ipd == null)
                return new TransferInfoModel();

            var spec = ipd.Specialty;
            var current_doctor = ipd.PrimaryDoctor;

            var transfers = new IPDTransfer(ipd).GetListInfo();
            TransferInfoModel first_ipd = null;
            if (transfers.Count() > 0)
            {
                first_ipd = transfers.FirstOrDefault(e => e.CurrentType == "IPD");
            }
            else
            {
                first_ipd = new TransferInfoModel()
                {
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentSpecialty = new { spec?.ViName, spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                };
            }
            if (first_ipd == null)
            {
                first_ipd = new TransferInfoModel()
                {
                    CurrentRawDate = ipd.AdmittedDate,
                    CurrentSpecialty = new { spec?.ViName, spec?.EnName },
                    CurrentDoctor = new { current_doctor?.Username, current_doctor?.Fullname, current_doctor?.DisplayName },
                    CurrentDate = ipd.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                };
            }
            return first_ipd;
        }
        public string ConvertFormCode(string formcode)
        {
            if (formcode.Contains("TAB"))
            {
                int index = formcode.IndexOf("_TAB");
                int lengthSub = formcode.Substring(index).Length;
                formcode = formcode.Substring(0, formcode.Length - lengthSub);
            }
            return formcode;
        }
        public dynamic PreAnesthesiaModel(OPD opd)
        {
            var check = (from op in unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.AsQueryable()
                        .Where(e => !e.IsDeleted && (e.IsAcceptNurse || e.IsAcceptPhysician))
                         join or in unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.Id == opd.Id).AsQueryable()
                         on op.Id equals or.TransferFromId
                         join oo in unitOfWork.OPDRepository.Find(e => !e.IsDeleted && e.CustomerId == opd.CustomerId).AsQueryable()
                         on op.VisitId equals oo.Id
                         join sp in unitOfWork.SpecialtyRepository.Find(e => !e.IsDeleted && e.Id == opd.SpecialtyId).AsQueryable()
                         on op.ReceivingUnitNurseId equals sp.Id
                         join cus in unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.Id == opd.CustomerId).AsQueryable()
                         on oo.CustomerId equals cus.Id
                         select new VisitPreAnesModel { Id = op.Id, VisitCode = oo.VisitCode, CustomerId = cus.Id, IsAcceptByNurse = op.IsAcceptNurse, IsAcceptPhysician = op.IsAcceptPhysician, StatusId = oo.EDStatusId }
                        ).FirstOrDefault();
            return check;
        }
        public dynamic PreAnesthesiaCustomerModel(Specialty specialty, string PID)
        {
            var check = (from op in unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.AsQueryable()
                        .Where(e => !e.IsDeleted && !e.IsAcceptNurse && !e.IsAcceptPhysician)
                         join oo in unitOfWork.OPDRepository.AsQueryable()
                         on op.VisitId equals oo.Id
                         join sp in unitOfWork.SpecialtyRepository.AsQueryable().Where(s => s.Id == specialty.Id)
                         on op.ReceivingUnitNurseId equals sp.Id
                         join ssp in unitOfWork.SpecialtyRepository.AsQueryable()
                         on op.HandOverUnitNurseId equals ssp.Id
                         join cus in unitOfWork.CustomerRepository.AsQueryable().Where(u => !u.IsDeleted && u.PID == PID)
                         on oo.CustomerId equals cus.Id
                         select new VisitPreAnesModel
                         {
                             Id = op.Id,
                             VisitCode = oo.VisitCode,
                             CustomerId = cus.Id,
                             Fullname = cus.Fullname,
                             PID = cus.PID,
                             DateOfBirth = cus.DateOfBirth,
                             SpecialtyName = ssp.ViName,
                             IsAcceptByNurse = op.IsAcceptNurse,
                             IsAcceptPhysician = op.IsAcceptPhysician,
                             StatusId = oo.EDStatusId
                         }
                        ).FirstOrDefault();
            return check;
        }
        public OPDPreAnesthesiaHandOverCheckList GetAnesthesia(Specialty specialty, string PID)
        {
            var check = (from op in unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.AsQueryable()
                            .Where(e => !e.IsDeleted && !e.IsAcceptNurse && !e.IsAcceptPhysician)

                         join oo in unitOfWork.OPDRepository.AsQueryable()
                         on op.VisitId equals oo.Id

                         join sp in unitOfWork.SpecialtyRepository.AsQueryable().Where(s => s.Id == specialty.Id)
                         on op.ReceivingUnitNurseId equals sp.Id

                         join cus in unitOfWork.CustomerRepository.AsQueryable().Where(u => !u.IsDeleted && u.PID == PID)
                         on oo.CustomerId equals cus.Id
                         select op
                        ).FirstOrDefault();
            return check;
        }
        private bool IsConfirmEOCStandingOrder(Guid opd_id)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == opd_id &&
                e.OrderType.Equals(Constant.ED_STANDING_ORDER) &&
                !e.IsConfirm
            );
            if (order != null)
                return false;
            return true;
        }
        protected bool IsConfirmStandingOrder(Guid visitId)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visitId &&
                e.OrderType.Equals(Constant.ED_STANDING_ORDER) &&
                !e.IsConfirm
            );
            if (order != null)
                return false;
            return true;
        }
        protected bool IsConfirmOrder(Guid visitId)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visitId &&
                e.OrderType.Equals(Constant.ED_ORDER) &&
                !e.IsConfirm
            );
            if (order != null)
                return false;
            return true;
        }
        protected bool IsConfirmSkinTest(Guid visitId)
        {
            var skin_test = unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId == visitId);
            if (skin_test != null && skin_test.ConfirmDoctorId == null)
                return false;
            return true;
        }

        protected EIOFormConfirmModel GetEIOFormConfirmByFormId(Guid form_id, string kind = null)
        {
            return (from c in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                    where !c.IsDeleted && c.FormId == form_id && (kind == null || c.ConfirmType == kind)
                    join use in unitOfWork.UserRepository.AsQueryable()
                    .Where(m => !m.IsDeleted)
                    on c.ConfirmBy equals use.Username
                    select new EIOFormConfirmModel()
                    {
                        Id = c.Id,
                        ConfirmAt = c.ConfirmAt,
                        ConfirmBy = c.ConfirmBy,
                        Fullname = use.Fullname,
                        Title = use.Title,
                        ConfirmType = c.ConfirmType,
                        FormId = c.FormId,
                        Note = c.Note,
                        Department = use.Department
                    }).FirstOrDefault();
        }
        protected Site getSiteByApiCode(string site_code)
        {
            return unitOfWork.SiteRepository.FirstOrDefault(e => e.ApiCode == site_code);
        }
        protected string getAppId()
        {
            return ConfigurationManager.AppSettings["APP_ID"] != null ? ConfigurationManager.AppSettings["APP_ID"].ToString() : "NONE";
        }
        protected List<MappingData> GenAutofillFromProcedure(IPDSurgeryCertificate certificate)
        {
            var data = certificate.IPDSurgeryCertificateDatas.Where(e => !e.IsDeleted)
                .Select(e => new MappingData() { Code = e.Code, Value = e.Value }).OrderBy(e => e.Code).ToList();
            if (certificate.FormId != null)
            {

                Dictionary<string, string> codesv3 = new Dictionary<string, string>()
                        {
                            {"IPDSURCER08", "SAPSNEW6"},
                            {"IPDSURCER10", "SAPSNEW4"},
                            {"IPDSURCER04", "SAPSNEW2"},
                            {"IPDSURCER22", "SAPSNEW10"},
                            {"IPDSURCER12", "SAPSNEW8"},
                            {"IPDSURCER14", "SAPSNEW12"},
                            {"IPDSURCER16", "SAPSNEW26"},
                            {"IPDSURCER18", "SAPSNEW28"},
                            {"IPDSURCER20", "SAPSNEW32"},

                     };
                Dictionary<string, string> codesv4 = new Dictionary<string, string>()
                        {
                            {"IPDSURCER08", "SSNEW6"},
                            {"IPDSURCER10", "SSNEW4"},
                            {"IPDSURCER04", "SSNEW2"},
                            {"IPDSURCER22", "SSNEW10"},
                            {"IPDSURCER12", "SSNEW8"},
                            {"IPDSURCER14", "SSNEW12"},
                            {"IPDSURCER16", "SSNEW26"},
                            {"IPDSURCER18", "SSNEW28"},
                            {"IPDSURCER20", "SSNEW32"},

                     };


                Guid? procedureId = certificate.FormId;
                var procedure = unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(e => !e.IsDeleted && e.Id == procedureId);
                if (certificate.Version == "3")
                {
                    data = AutofillFromProcedure(data, procedure, codesv3);
                }
                if (certificate.Version == "4")
                {
                    data = AutofillFromProcedure(data, procedure, codesv4);
                }
                data = FormatString(data);
            }
            return data;
        }
        protected List<MappingData> AutofillFromProcedure(List<MappingData> datas, SurgeryAndProcedureSummaryV3 procedure, Dictionary<string, string> codes)
        {
            var codeKeys = codes.Keys.ToList();
            var codeValues = codes.Values.ToList();
            var procedureId = procedure?.Id;
            var procedureVisit = procedure?.VisitId;
            var dataProcedure = unitOfWorkDapper.FormDatasRepository.Find(e =>
                    e.IsDeleted == false &&
                    e.VisitId == procedureVisit &&
                    e.FormId == procedureId
            ).Select(f => new FormDataValue { Id = f.Id, Code = f.Code, Value = f.Value, FormId = f.FormId, FormCode = f.FormCode }).ToList();


            foreach (var item in codeKeys)
            {
                var check = datas.FirstOrDefault(e => e.Code == item);
                if (check == null)
                {
                    MappingData _new = new MappingData()
                    {
                        Code = item,
                        Value = null
                    };
                    datas.Add(_new);
                }
            }

            var dataResult = (from d in datas
                              select new MappingData()
                              {
                                  Code = d.Code,
                                  Value = ChangeValue(d, dataProcedure, codes).Value,
                              }).ToList();

            return dataResult;
        }
        protected List<MappingData> FormatString(List<MappingData> datas)
        {
            int lengthOfdatas = datas.Count;
            for (int i = 0; i < lengthOfdatas; i++)
            {
                if (datas[i].Code == "IPDSURCER08")
                {
                    var stringObject = datas.FirstOrDefault(o => o.Code == "IPDSURCER10");
                    string jsonText = stringObject?.Value;
                    if (jsonText == null || jsonText == $"\"\"")
                        jsonText = "";
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(jsonText);
                    string _str = String.Empty;
                    if (objs != null)
                    {
                        int lengthOfobjs = objs.Count;
                        for (int j = 0; j < lengthOfobjs; j++)
                        {
                            var codeIcd10 = objs[j]["code"]?.ToString();
                            if (j == 0)
                                _str += codeIcd10;
                            else
                                _str += $" ,{codeIcd10}";
                        }
                        datas[i].Value = datas[i].Value + $" ({_str})";
                    }
                }
                if (datas[i].Code == "IPDSURCER22")
                {
                    var stringObject = datas.FirstOrDefault(o => o.Code == "IPDSURCER12");
                    string jsonText = stringObject?.Value;
                    if (jsonText == null || jsonText == $"\"\"")
                        jsonText = "";
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(jsonText);
                    string _str = String.Empty;
                    if (objs != null)
                    {
                        int lengthOfobjs = objs.Count;
                        for (int j = 0; j < lengthOfobjs; j++)
                        {
                            var codeIcd10 = objs[j]["code"]?.ToString();
                            if (j == 0)
                                _str += codeIcd10;
                            else
                                _str += $" ,{codeIcd10}";
                        }
                        datas[i].Value = datas[i].Value + $" ({_str})";
                    }
                }
            }

            return datas.OrderBy(o => o.Code).ToList();
        }
        protected MappingData ChangeValue(MappingData data, List<FormDataValue> dataProcedure, Dictionary<string, string> codes)
        {
            var codeKeys = codes.Keys.ToList();
            if (codeKeys.Contains(data.Code))
            {
                string key = data.Code;
                data.Value = dataProcedure.FirstOrDefault(e => e.Code == codes[key]) == null ? "" : dataProcedure.FirstOrDefault(e => e.Code == codes[key]).Value;
                //DateTime createdDay = new DateTime(2022, 9, 15);
                //if (data.Value.Count() > 0 && (key == "IPDSURCER18" || key == "IPDSURCER20") && procedure.CreatedAt >= createdDay)
                //{
                //    var obj = new JavaScriptSerializer().Deserialize<dynamic>(data.Value);
                //    string fullname = string.Empty;
                //    foreach (var item in obj)
                //    {
                //        var username = (string)item;
                //        var name = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == username);
                //        if(name != null)
                //        {
                //            var full_name = name.Fullname;
                //            fullname = fullname + ", " + full_name;
                //        }                        
                //    }
                //    data.Value = fullname;
                //}
            }
            return data;
        }
        protected List<MappingData> GetHistoryMedicalRecord(string[] code, Guid part2_Id)
        {

            var datas = (from master in unitOfWork.MasterDataRepository.AsQueryable()
                         where !master.IsDeleted && !string.IsNullOrEmpty(master.Code)
                         && code.Contains(master.Code)

                         join data_code in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                         .Where(d =>
                              !d.IsDeleted
                              && d.IPDMedicalRecordPart2Id != null
                              && d.IPDMedicalRecordPart2Id == part2_Id
                              && !string.IsNullOrEmpty(d.Code)
                              && code.Contains(d.Code)
                              )
                         on master.Code equals data_code.Code into datas_query
                         from choice_data in datas_query.DefaultIfEmpty()
                         select new MappingData
                         {
                             Code = master.Code,
                             ViName = master.ViName,
                             EnName = master.EnName,
                             Value = choice_data.Value,
                             Order = master.Order,
                             Group = master.Group,
                             DataType = master.DataType
                         }).ToList();
            List<MappingData> datas_choices = new List<MappingData>();
            int length = code.Length;
            for (int i = 0; i < length; i++)
            {
                var data = (from d in datas
                            where d.Code == code[i]
                            select new MappingData
                            {
                                ViName = d.ViName,
                                EnName = d.EnName,
                                Code = d.Code,
                                Value = d.Value,
                                Group = d.Group,
                                DataType = d.DataType,
                                Order = i
                            }).FirstOrDefault();
                if (data != null)
                    datas_choices.Add(data);
            }
            return datas_choices;
        }

        protected EIOForm CreateIdForForm(Guid visitId, string formCode, string visitType, int app_version, Guid? formid = null, bool isTimeChage = false, bool isAnonymus = true)
        {
            var form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == formCode && e.VisitTypeGroupCode == visitType.ToUpper());
            if (form == null)
            {
                form = new EIOForm()
                {
                    VisitId = visitId,
                    VisitTypeGroupCode = visitType,
                    FormCode = formCode,
                    Version = app_version,
                    FormId = formid
                };
                unitOfWork.EIOFormRepository.Add(form);
            }
            else
                unitOfWork.EIOFormRepository.Update(form, is_time_change: isTimeChage, is_anonymous: isAnonymus);
            unitOfWork.Commit();
            return form;
        }
        protected dynamic formatPastMedicalHistory(VisitModel mdel)
        {
            return new
            {
                ExaminationTime = mdel.ExaminationTime.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Clinic = new { mdel.ViName, mdel.EnName, mdel.ClinicCode },
                PrimaryDoctor = mdel.Username,
                mdel.Fullname,
                mdel.Username,
                Value = PastMedicalHistoryValue(mdel),
                mdel.Type,
                mdel.Sk_OPDOENTSSKANS,
                mdel.Sk_OPDOENTSKNANS,
                mdel.Sk_OPDOENTSKANS,
                mdel.ClinicCode
            };
        }
        private string PastMedicalHistoryValue(VisitModel mdel)
        {
            if (mdel.ClinicCode == "FreeTextOnly-000")
            {
                return string.Format("- Tiền sử sản khoa: {0}\n- Tiền sử kinh nguyệt: {1}\n- Tiền sử khác: {2}", mdel.Sk_OPDOENTSSKANS, mdel.Sk_OPDOENTSKNANS, mdel.Sk_OPDOENTSKANS);
            }
            return mdel.PastMedicalHistory;
        }
        protected string GetDiagnosisED(List<EDDischargeInformationData> discharge_info_datas, List<EDEmergencyRecordData> emer_datas, int visitVersion, string typeReport = null)
        {
            string resultDiagnosis = "", mainDiagnosis = "", mainCodes = "", comorbidityDiagnosis = "", comorbidityCodes = "", icd10Main = "";

            if (typeReport == "EMERGENCY CONFIRMATION")
            {
                mainDiagnosis = emer_datas?.FirstOrDefault(e => !e.IsDeleted && e.Code == "ER0ID0ANS")?.Value;
                if (mainDiagnosis != null)
                {
                    icd10Main = emer_datas?.FirstOrDefault(e => e.Code == "ER0ICD102")?.Value;
                    List<DiagnosisModel> mainDiagnosisCodes = new List<DiagnosisModel>();
                    if(!string.IsNullOrEmpty(icd10Main))
                    {
                        mainDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Main);
                        if (mainDiagnosisCodes != null)
                        {
                            mainCodes = String.Join(", ", mainDiagnosisCodes?.Select(e => e.code).ToArray());
                        }
                    }    
                }
                if (string.IsNullOrEmpty(mainDiagnosis))
                {
                    mainDiagnosis = "";
                }
                if (string.IsNullOrEmpty(mainCodes))
                {
                    mainCodes = "";
                }
                resultDiagnosis = $"{mainDiagnosis} ({mainCodes})";
                if(visitVersion >= 10)
                {
                    resultDiagnosis = $"{mainDiagnosis} ({mainCodes})";
                }
                else
                {
                    resultDiagnosis = mainDiagnosis ?? "";
                }
                if (resultDiagnosis.Contains("()"))
                {
                    resultDiagnosis = resultDiagnosis.Replace(" ()", "");
                }
                return resultDiagnosis;
            }
            if (discharge_info_datas != null && discharge_info_datas.Count > 0)
            {
                // lấy chuẩn đoán từ đánh giá kết thúc
                //Chẩn đoán bệnh chính
                mainDiagnosis = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
                if (typeReport == "DISCHARGE MEDICAL REPORT")
                {
                    return $"{mainDiagnosis}";
                }
                if (mainDiagnosis != null)
                {
                    icd10Main = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0DIAICD")?.Value;
                    if (!string.IsNullOrEmpty(icd10Main))
                    {
                        List<DiagnosisModel> mainDiagnosisCodes = new List<DiagnosisModel>();
                        mainDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Main);
                        if (mainDiagnosisCodes != null)
                        {
                            mainCodes = String.Join(", ", mainDiagnosisCodes.Select(e => e.code).ToArray());
                        }
                    }

                }
                //Chẩn đoán bệnh kèm theo
                comorbidityDiagnosis = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0DIAOPT2")?.Value;

                if (comorbidityDiagnosis != null)
                {
                    string icd10Comorbidity = discharge_info_datas.FirstOrDefault(e => e.Code == "DI0DIAOPT")?.Value;
                    if (!string.IsNullOrEmpty(icd10Comorbidity))
                    {
                        List<DiagnosisModel> optionDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Comorbidity);
                        if (optionDiagnosisCodes != null)
                        {
                            comorbidityCodes = String.Join(", ", optionDiagnosisCodes.Select(e => e.code).ToArray());
                        }
                    }

                }
            }
            string diagnosis = "";
            if (!string.IsNullOrEmpty(mainDiagnosis) && !string.IsNullOrEmpty(comorbidityDiagnosis))
            {
                diagnosis = mainDiagnosis + "/ " + comorbidityDiagnosis;
            }
            else if (!string.IsNullOrEmpty(mainDiagnosis) && string.IsNullOrEmpty(comorbidityDiagnosis))
            {
                diagnosis = mainDiagnosis;
            }
            else if (string.IsNullOrEmpty(mainDiagnosis) && !string.IsNullOrEmpty(comorbidityDiagnosis))
            {
                diagnosis = comorbidityDiagnosis;
            }
            else
            {
                diagnosis = "";
            }
            string icdCode = "";
            if (!string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
            {
                icdCode = mainCodes + "/ " + comorbidityCodes;
            }
            else if (!string.IsNullOrEmpty(mainCodes) && string.IsNullOrEmpty(comorbidityCodes))
            {
                icdCode = mainCodes;
            }
            else if (string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
            {
                icdCode = comorbidityCodes;
            }
            else
            {
                icdCode = "";
            }

            resultDiagnosis = $"{diagnosis} ({icdCode})";

            if (visitVersion >= 10)
            {
                resultDiagnosis = resultDiagnosis + "";
            }
            else
            {
                switch (typeReport)
                {
                    case "JOIN CONSULTATION FOR APPROVAL OF SURGERY":
                        resultDiagnosis = resultDiagnosis + "";
                        break;
                    case "MEDICAL REPORT":
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                    case "INJURY CERTIFICATE":
                        resultDiagnosis = resultDiagnosis + "";
                        break;
                    default:
                        resultDiagnosis = mainDiagnosis ?? "";
                        break;
                }
                if (typeReport != "INJURY CERTIFICATE")
                {
                    resultDiagnosis = resultDiagnosis.Replace("/", ",");
                }
            }
            if (resultDiagnosis.Contains("()"))
            {
                resultDiagnosis = resultDiagnosis.Replace(" ()", "");
            }
            return resultDiagnosis;
        }
        protected string GetDiagnosisIPD(List<IPDMedicalRecordPart3Data> part_3_datas, int visitVersion, string typeReport = null)
        {
            string resultDiagnosis = "", mainDiagnosis = "", mainCodes = "", comorbidityDiagnosis = "", comorbidityCodes = "", icd10Main = "";
            if (part_3_datas != null && part_3_datas.Count > 0)
            {
                // lấy chuẩn đoán từ tab 3 của bệnh án bất kì
                //Chẩn đoán bệnh chính
                mainDiagnosis = part_3_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPECDBCANS")?.Value;
                if (typeReport == "DISCHARGE MEDICAL REPORT")
                {
                    return $"{mainDiagnosis}";
                }

                if (mainDiagnosis != null)
                {
                    icd10Main = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEICDCANS")?.Value;
                    if (!string.IsNullOrEmpty(icd10Main))
                    {
                        List<DiagnosisModel> mainDiagnosisCodes = new List<DiagnosisModel>();
                        mainDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Main);
                        if (mainDiagnosisCodes != null)
                        {
                            mainCodes = String.Join(", ", mainDiagnosisCodes.Select(e => e.code).ToArray());
                        }
                    }

                }
                //Chẩn đoán bệnh kèm theo
                var yes = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPECDKTYES");
                if (yes != null && !string.IsNullOrEmpty(yes?.Value) && Convert.ToBoolean(yes?.Value) == true)
                {
                    comorbidityDiagnosis = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPECDKTANS")?.Value;
                    if (comorbidityDiagnosis != null)
                    {
                        string icd10Comorbidity = part_3_datas.FirstOrDefault(e => e.Code == "IPDMRPEICDPANS")?.Value;
                        if (!string.IsNullOrEmpty(icd10Comorbidity))
                        {
                            List<DiagnosisModel> optionDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Comorbidity);
                            if (optionDiagnosisCodes != null)
                            {
                                comorbidityCodes = String.Join(", ", optionDiagnosisCodes.Select(e => e.code).ToArray());
                            }
                        }
                    }
                }
                string diagnosis = "";
                if (!string.IsNullOrEmpty(mainDiagnosis) && !string.IsNullOrEmpty(comorbidityDiagnosis))
                {
                    diagnosis = mainDiagnosis + "/ " + comorbidityDiagnosis;
                }
                else if (!string.IsNullOrEmpty(mainDiagnosis) && string.IsNullOrEmpty(comorbidityDiagnosis))
                {
                    diagnosis = mainDiagnosis;
                }
                else if (string.IsNullOrEmpty(mainDiagnosis) && !string.IsNullOrEmpty(comorbidityDiagnosis))
                {
                    diagnosis = comorbidityDiagnosis;
                }
                else
                {
                    diagnosis = "";
                }
                string icdCode = "";
                if (!string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = mainCodes + "/ " + comorbidityCodes;
                }
                else if (!string.IsNullOrEmpty(mainCodes) && string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = mainCodes;
                }
                else if (string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = comorbidityCodes;
                }
                else
                {
                    icdCode = "";
                }

                resultDiagnosis = $"{diagnosis} ({icdCode})";
            }

            if (visitVersion >= 10)
            {
                resultDiagnosis = resultDiagnosis + "";
            }
            else
            {
                switch (typeReport)
                {
                    case "MEDICAL REPORT":
                        resultDiagnosis = resultDiagnosis + "";
                        break;
                    //case "DISCHARGE MEDICAL REPORT":
                    //    resultDiagnosis = mainDiagnosis + "";
                    //    break;
                    case "REFERRAL LETTER":
                        resultDiagnosis = mainDiagnosis ?? "";
                        break;
                    case "INJURY CERTIFICATE":
                        resultDiagnosis = resultDiagnosis + "";
                        break;
                    case "TRANSFER LETTER":
                        resultDiagnosis = resultDiagnosis + "";
                        break;
                    case "DISCHARGE CERTIFICATE":
                        resultDiagnosis = resultDiagnosis + "";
                        break;
                    default:
                        resultDiagnosis = mainDiagnosis ?? "";
                        break;
                }
                resultDiagnosis = resultDiagnosis.Replace("/", ",");
            }
            if (resultDiagnosis.Contains("()"))
            {
                resultDiagnosis = resultDiagnosis.Replace(" ()", "");
            }
            return resultDiagnosis;
        }
        protected string GetDiagnosisOPD(List<OPDOutpatientExaminationNoteData> oen_datas, int visitVersion, string typeReport = null)
        {
            string resultDiagnosis = "", mainDiagnosis = "", mainCodes = "", comorbidityDiagnosis = "", comorbidityCodes = "", icd10Main = "", icdCode = "";
            if (oen_datas != null && oen_datas.Count > 0)
            {
                // lấy chuẩn đoán từ từ phiếu khám ngoại trú
                //Chẩn đoán bệnh chính
                mainDiagnosis = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENDD0ANS")?.Value;
                if (typeReport == "MEDICAL REPORT" || typeReport == "TRANSFER LETTER" || typeReport == "PATIENT HANDOVER RECORD")
                {
                    return $"{mainDiagnosis}";
                }
                if (mainDiagnosis != null)
                {
                    icd10Main = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
                    if (!string.IsNullOrEmpty(icd10Main))
                    {
                        List<DiagnosisModel> mainDiagnosisCodes = new List<DiagnosisModel>();
                        mainDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Main);
                        if (mainDiagnosisCodes != null)
                        {
                            mainCodes = String.Join(", ", mainDiagnosisCodes.Select(e => e.code).ToArray());
                        }
                    }

                }
                //Chẩn đoán bệnh kèm theo
                comorbidityDiagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value;

                if (comorbidityDiagnosis != null)
                {
                    string icd10Comorbidity = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
                    if (!string.IsNullOrEmpty(icd10Comorbidity))
                    {
                        List<DiagnosisModel> optionDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Comorbidity);
                        if (optionDiagnosisCodes != null)
                        {
                            comorbidityCodes = String.Join(", ", optionDiagnosisCodes.Select(e => e.code).ToArray());
                        }
                    }

                }
                string diagnosis = "";
                if (!string.IsNullOrEmpty(mainDiagnosis) && !string.IsNullOrEmpty(comorbidityDiagnosis))
                {
                    diagnosis = mainDiagnosis + "/ " + comorbidityDiagnosis;
                }
                else if (!string.IsNullOrEmpty(mainDiagnosis) && string.IsNullOrEmpty(comorbidityDiagnosis))
                {
                    diagnosis = mainDiagnosis;
                }
                else if (string.IsNullOrEmpty(mainDiagnosis) && !string.IsNullOrEmpty(comorbidityDiagnosis))
                {
                    diagnosis = comorbidityDiagnosis;
                }
                else
                {
                    diagnosis = "";
                }

                if (!string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = mainCodes + "/ " + comorbidityCodes;
                }
                else if (!string.IsNullOrEmpty(mainCodes) && string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = mainCodes;
                }
                else if (string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = comorbidityCodes;
                }
                else
                {
                    icdCode = "";
                }

                resultDiagnosis = $"{diagnosis} ({icdCode})";
            }

            if (visitVersion >= 10)
            {
                switch (typeReport)
                {
                    case "MEDICAL CERTIFICATE":
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                    case "ILLNESS CERTIFICATE":
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                    case "REFERRAL LETTER":
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                    case "PATIENT HANDOVER RECORD":
                        resultDiagnosis = mainDiagnosis ?? "";
                        break;
                    default:
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                }
            }
            else
            {
                switch (typeReport)
                {
                    case "ILLNESS CERTIFICATE":
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                    case "REFERRAL LETTER":
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                    case "MEDICAL CERTIFICATE":
                        resultDiagnosis = $"{mainDiagnosis} ({icdCode})";
                        break;
                    default:
                        resultDiagnosis = mainDiagnosis ?? "";
                        break;
                }
                resultDiagnosis = resultDiagnosis.Replace("/", ",");
            }
            if (resultDiagnosis.Contains("()"))
            {
                resultDiagnosis = resultDiagnosis.Replace(" ()", "");
            }
            return resultDiagnosis;
        }
        protected string GetDiagnosisEOC(List<FormDataValue> oen_datas, int visitVersion, string typeReport = null)
        {
            string resultDiagnosis = "", mainDiagnosis = "", mainCodes = "", comorbidityDiagnosis = "", comorbidityCodes = "", icd10Main = "", icdCode = "";
            if (oen_datas != null && oen_datas.Count > 0)
            {
                // lấy chuẩn đoán từ từ phiếu khám ngoại trú
                //Chẩn đoán bệnh chính
                mainDiagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENDD0ANS")?.Value;
                if (typeReport == "MEDICAL REPORT" || typeReport == "TRANSFER LETTER" || typeReport == "PATIENT HANDOVER RECORD")
                {
                    return $"{mainDiagnosis}";
                }

                if (mainDiagnosis != null)
                {
                    icd10Main = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDANS")?.Value;
                    if (!string.IsNullOrEmpty(icd10Main))
                    {
                        List<DiagnosisModel> mainDiagnosisCodes = new List<DiagnosisModel>();
                        mainDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Main);
                        if (mainDiagnosisCodes != null)
                        {
                            mainCodes = String.Join(", ", mainDiagnosisCodes.Select(e => e.code).ToArray());
                        }
                    }
                }
                //Chẩn đoán bệnh kèm theo
                //comorbidityDiagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value;

                if (comorbidityDiagnosis != null)
                {
                    string icd10Comorbidity = oen_datas.FirstOrDefault(e => e.Code == "OPDOENICDOPT")?.Value;
                    if (!string.IsNullOrEmpty(icd10Comorbidity))
                    {
                        List<DiagnosisModel> optionDiagnosisCodes = JsonConvert.DeserializeObject<List<DiagnosisModel>>(icd10Comorbidity);
                        if (optionDiagnosisCodes != null)
                        {
                            comorbidityCodes = String.Join(", ", optionDiagnosisCodes.Select(e => e.code).ToArray());
                        }
                    }
                }
                string diagnosis = "";
                if (!string.IsNullOrEmpty(mainDiagnosis))
                {
                    diagnosis = mainDiagnosis;
                }
                else
                {
                    diagnosis = "";
                }

                if (!string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = mainCodes + "/ " + comorbidityCodes;
                }
                else if (!string.IsNullOrEmpty(mainCodes) && string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = mainCodes;
                }
                else if (string.IsNullOrEmpty(mainCodes) && !string.IsNullOrEmpty(comorbidityCodes))
                {
                    icdCode = comorbidityCodes;
                }
                else
                {
                    icdCode = "";
                }

                resultDiagnosis = $"{diagnosis} ({icdCode})";
            }

            if (visitVersion >= 10)
            {
                return resultDiagnosis;
            }
            else
            {
                resultDiagnosis = resultDiagnosis.Replace("/", ",");
            }
            if (resultDiagnosis.Contains("()"))
            {
                resultDiagnosis = resultDiagnosis.Replace(" ()", "");
            }
            return resultDiagnosis;
        }
        protected dynamic GetInfoConfirm(Guid id)
        {
            dynamic Confirmcreated = new
            {
                ConfirmAt = String.Empty,
                ConfirmBy = string.Empty,
                ConfirmType = String.Empty,
                IsUnlockConfirm = false,
            };

            var confirm = GetConfirm(id);
            if (confirm != null)
            {
                Confirmcreated = new
                {
                    ConfirmAt = confirm.IsDeleted ? null : confirm.ConfirmAt,
                    ConfirmBy = confirm.IsDeleted ? null : confirm.ConfirmBy,
                    ConfirmType = confirm.IsDeleted ? null : confirm.ConfirmType,
                    IsUnlockConfirm = confirm.IsDeleted
                };
            }
            return Confirmcreated;
        }
        protected EIOFormConfirm GetConfirm(Guid id)
        {
            return unitOfWork.EIOFormConfirmRepository.Find(e => e.FormId == id).OrderByDescending(e => e.CreatedAt).FirstOrDefault();
        }
        public static System.Transactions.TransactionScope GetNewReadUncommittedScope()
        {
            var timeout_config = ConfigurationManager.AppSettings["TRANSACTION_TIMEOUT"];
            var timeout = TimeSpan.FromMinutes(int.Parse(string.IsNullOrEmpty(timeout_config) ? "10" : timeout_config.ToString()));
            return new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.RequiresNew,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                    Timeout = timeout
                });
        }
        protected EDStatus GetStatusByCode(string code)
        {
            return unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted && e.Code == code
            );
        }
        protected async Task<Customer> SyncHisCustomerAsync(string pid)
        {
            var his_patient_info = new SyncOHPatientInfo(unitOfWork);
            return await his_patient_info.SyncCustomerAsync(pid);
        }
        public static string GetStringClinicCodeUsed(OPD visit)
        {
            List<string> list_result = new List<string>();
            var code_setup = visit?.Clinic?.SetUpClinicDatas;
            var data_form = visit.OPDOutpatientExaminationNote?.OPDOutpatientExaminationNoteDatas;
            if (visit?.OPDOutpatientExaminationNote?.Version == 1)
                return visit?.Clinic?.Code;

            if (data_form != null)
            {
                var codeInData = (from d in data_form
                                  where !d.IsDeleted
                                  && d.Code == "SETUPCLINICS"
                                  select d.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(codeInData))
                    return codeInData;
            }

            return code_setup;
        }
        public AppVersion getAppCurrentVersion()
        {
            return unitOfWork.AppVersionRepository.Find(e => !e.IsDeleted).ToList().OrderByDescending(e => e.Order).FirstOrDefault();
        }
        //Lấy tiền sử gia đình, bản thân
        public VisitHistoryModel OPDFamilyPastMedicalHistory(OPD opd)
        {
            var data = opd.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENPT622")?.Value;
            if (data == null) return null;
            var spec = unitOfWork.SpecialtyRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == opd.SpecialtyId);
            var user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == opd.PrimaryDoctorId);
            var result = new VisitHistoryModel
            {
                ExaminationTime = opd.AdmittedDate,
                DoctorExam = user?.Username,
                SpecialtyVi = spec?.ViName,
                SpecialtyEn = spec?.EnName,
                PastMedicalHistory = data,
                UpdateAt = opd.CreatedAt.Value
            };
            return result;
        }
        public VisitHistoryModel IPDFamilyPastMedicalHistory(IPD ipd)
        {
            var data = ipd.IPDMedicalRecord?.IPDMedicalRecordPart2?.IPDMedicalRecordPart2Datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "IPDMRPTGIDIANS")?.Value;
            if (data == null) return null;
            var spec = unitOfWork.SpecialtyRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == ipd.SpecialtyId);            
            //var user = unitOfWork.UserRepository.FirstOrDefault(e => !e.IsDeleted && e.Username == username);
            var result = new VisitHistoryModel
            {
                ExaminationTime = ipd.AdmittedDate,
                DoctorExam = ipd.IPDMedicalRecord.IPDMedicalRecordPart2?.UpdatedBy,
                SpecialtyVi = spec?.ViName,
                SpecialtyEn = spec?.EnName,
                PastMedicalHistory = data,
                UpdateAt = ipd.CreatedAt.Value
            };
            return result;
        }
    }
}
