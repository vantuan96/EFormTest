using AutoMapper;
using Bussiness.HisService;
using Clients.HisClient;
using Common;
using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Helper;
using EForm.Models;
using EForm.Services;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers
{
    [SessionAuthorize]
    public class DoctorPlaceDiagnosticsOrderController : BaseApiController
    {
        private readonly int MaximumNumberOfItemPerRequest = ConfigurationManager.AppSettings["MaximumNumberOfItemPerRequest"] != null ? int.Parse(ConfigurationManager.AppSettings["MaximumNumberOfItemPerRequest"].ToString()) : 20;
        private readonly string DrsPrefix = ConfigurationManager.AppSettings["DRS_PREFIX"] != null ? ConfigurationManager.AppSettings["DRS_PREFIX"].ToString() : "DRS";
        public readonly static string[] LABORATORY_CODE = {
            "FB.01", "FB.02", "FB.03", "FB.04",
            "FB.05", "FB.06", "FB.07", "FB.08",
            "FB.09", "FB.10", "FB.11"
        };
        public readonly static string[] RADIOLOGY_CODE =
        {
            "FA.01", "FA.02", "FA.03", "FA.04",
            "FA.05", "FA.06", "FA.07", "FA.08"
        };
        public readonly static string[] ALLIED_CODE =
        {
            "GA", "GB", "GC", "GD",
            "GG", "GI", "GK", "GM", "GN", "GP"
        };
        [Route("api/DoctorPlaceDiagnosticsOrder/Customer/")]
        // [Permission(Code = "DRS0001")]
        public async Task<IHttpActionResult> GetCustomerAsync([FromUri] HISCustomerParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var customer = GetUpdateOrCreateHisCustomerByPid(request.PID);
            if (customer == null)
            {
                return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);
            }
            var Visit = await HisClient.GetOpenVisitCodeAsync(request.PID);
            var current_username = getUsername();
            var isDoctor = Visit.Where(e => IsStringEquals(current_username, e.DoctorAD)).Count() > 0;
            if (customer.IsVip && (!IsVIPMANAGE() || !IsUnlockVipByPid(request.PID, UnlockVipType.PlaceDiagnosticsOrder)))
            {
                Visit = Visit.Where(e => IsStringEquals(current_username, e.DoctorAD)).ToList();
            }
            return Content(HttpStatusCode.OK, new
            {
                Visit,
                request.PID,
                Customer = customer
            });
        }
        [Route("api/DoctorPlaceDiagnosticsOrder/CustomerVisitInfo/")]
        // [Permission(Code = "DRS0002")]
        public async Task<IHttpActionResult> GetCustomerVisitInfoAsync([FromUri] HISCustomerParameterModel request)
        {
            if (!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var current_username = getUsername();
            var customer = GetLocalOrHisCustomerByPid(request.PID);

            var Diagnosis = new DiagnosisAndICDModel
            {
            };
            if (request.VisitId != null)
            {
                Diagnosis = GetVisitDiagnosis((Guid)request.VisitId, request.VisitType);
            }
            var MicrobiologyService01 = GetAppConfig("MICROBIOLOGY_SERVICE_01");
            var MicrobiologyService02 = GetAppConfig("MICROBIOLOGY_SERVICE_02");
            var PID = customer.PID;
            var Visit = await HisClient.GetOpenVisitCodeAsync(PID);
            int? maxqty = IntTryParse(GetAppConfig("MAXQTYCHARGEITEM"));
            if (Visit.Count == 0)
            {
                return Content(HttpStatusCode.OK, new
                {
                    PID,
                    Customer = customer,
                    Diagnosis,
                    MICROBIOLOGY_SERVICE_01 = MicrobiologyService01,
                    MICROBIOLOGY_SERVICE_02 = MicrobiologyService02,
                    AUTO_FOCUS_PXN_KHOI_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_KHOI_TE_BAO"].ToString(),
                    AUTO_FOCUS_PXN_MO_BENH_HOC = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_MO_BENH_HOC"].ToString(),
                    AUTO_FOCUS_PXN_SINH_THIET_LANH = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_SINH_THIET_LANH"].ToString(),
                    AUTO_FOCUS_PXN_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_TE_BAO"].ToString(),
                    AUTO_FOCUS_PXN_PHU_KHOA = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_PHU_KHOA"].ToString(),
                    maxqty = maxqty == null ? 200 : maxqty
                });
            };
            foreach (var data in Visit)
            {
                if (
                    data.VisitCode == request.VisitCode &&
                    (string.IsNullOrEmpty(request.PatientLocationCode) || request.PatientLocationCode == data.PatientLocationCode) &&
                    (IsVIPMANAGE() || IsUnlockVipByPid(request.PID, UnlockVipType.PlaceDiagnosticsOrder) || (!customer.IsVip || IsStringEquals(current_username, data.DoctorAD)))
                )
                {
                    var exitDraft = unitOfWork.ChargeDraftRepository.Find(e => !e.IsDeleted && e.PID == request.PID && e.VisitCode == request.VisitCode).FirstOrDefault();
                    return Content(HttpStatusCode.OK, new
                    {
                        Visit = data,
                        PID,
                        Customer = customer,
                        Draft = exitDraft,
                        Diagnosis,
                        MICROBIOLOGY_SERVICE_01 = MicrobiologyService01,
                        MICROBIOLOGY_SERVICE_02 = MicrobiologyService02,
                        AUTO_FOCUS_PXN_KHOI_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_KHOI_TE_BAO"].ToString(),
                        AUTO_FOCUS_PXN_MO_BENH_HOC = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_MO_BENH_HOC"].ToString(),
                        AUTO_FOCUS_PXN_SINH_THIET_LANH = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_SINH_THIET_LANH"].ToString(),
                        AUTO_FOCUS_PXN_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_TE_BAO"].ToString(),
                        AUTO_FOCUS_PXN_PHU_KHOA = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_PHU_KHOA"].ToString(),
                        maxqty = maxqty == null ? 200 : maxqty
                    });
                }
            }
            return Content(HttpStatusCode.OK, new
            {
                PID,
                Customer = customer,
                Diagnosis,
                MICROBIOLOGY_SERVICE_01 = MicrobiologyService01,
                MICROBIOLOGY_SERVICE_02 = MicrobiologyService02,
                AUTO_FOCUS_PXN_KHOI_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_KHOI_TE_BAO"].ToString(),
                AUTO_FOCUS_PXN_MO_BENH_HOC = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_MO_BENH_HOC"].ToString(),
                AUTO_FOCUS_PXN_SINH_THIET_LANH = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_SINH_THIET_LANH"].ToString(),
                AUTO_FOCUS_PXN_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_TE_BAO"].ToString(),
                AUTO_FOCUS_PXN_PHU_KHOA = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_PHU_KHOA"].ToString(),
                maxqty = maxqty == null ? 200 : maxqty
            });

        }
        [Route("api/DoctorPlaceDiagnosticsOrder/ServicesGroup/")]
        // [Permission(Code = "DRS0003")]
        public IHttpActionResult GetServiceGroupAPI([FromUri] ServicesParameterModel request)
        {
            var query = unitOfWork.ServiceGroupRepository.Find(e => !e.IsDeleted);

            if (request.Search != null)
            {
                query = query.Where(e =>
                   ((e.Code != null && e.Code.Contains(request.ConvertedSearch))
                   || (e.ViName != null && e.ViName.ToLower().Contains(request.ConvertedSearch))
                   )
                );
            }
            if (request.Type != null)
            {
                List<string> codemap = new List<string> { "FB", "FA", "G" };
                var code = codemap[(int)request.Type];
                var parrent = unitOfWork.ServiceGroupRepository.Find(e => !e.IsDeleted && e.Code == code).FirstOrDefault()?.HISId;
                query = query.Where(e => e.HISParentId == parrent);
            }
            var items = query.OrderByDescending(m => m.Code).Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            return Ok(new { items });
        }
        [Route("api/DoctorPlaceDiagnosticsOrder/Services/")]
        // [Permission(Code = "DRS0004")]
        public IHttpActionResult GetServicesAPI([FromUri] ServicesParameterModel request = null)
        {

            string ServiceGroupCodeAllowSetupQty = GetAppConfig("ServiceGroupCodeAllowSetupQty");
            // ConfigurationManager.AppSettings.Get("ServiceGroupCodeAllowSetupQty")
            string ServiceCodeAllowSetupQty = GetAppConfig("ServiceCodeAllowSetupQty");

            var query = (from sql in unitOfWork.ServiceRepository.AsQueryable().Where(
                            e => !e.IsDeleted && e.IsActive && e.Type == "SRV"
                         )

                         select new ServicesModel
                         {
                             Id = sql.Id,
                             ViName = sql.ViName,
                             EnName = sql.EnName,
                             Code = sql.Code,
                             CombinedName = sql.CombinedName,
                             ServiceGroupCode = sql.ServiceGroup.Code,
                             HISId = sql.HISId,
                             GroupId = sql.ServiceGroupId,
                             RootServiceGroupCode = sql.RootServiceGroupCode,
                             RootServiceGroupId = sql.RootServiceGroupId
                         });
            // query = query.Where(e => LABORATORY_CODE.Any(x => e.ServiceGroupCode.Equals(x)) || RADIOLOGY_CODE.Any(x => e.ServiceGroupCode.Equals(x)) || ALLIED_CODE.Any(x => e.ServiceGroupCode.Equals(x)));
            if (request.Search != null)
            {
                query = query.Where(e => e.CombinedName != null && e.CombinedName.Contains(request.ConvertedSearch));
            }
            if (request.GroupId != null)
            {
                var groups = getListGroupId((Guid)request.GroupId);
                query = query.Where(e => groups.Contains((Guid)e.GroupId));
            }
            else
            {
                if (request.Type != null)
                {
                    if (request.Type == 0)
                        query = query.Where(e => e.RootServiceGroupCode.StartsWith("F\\FB"));
                    if (request.Type == 1)
                        query = query.Where(e => e.RootServiceGroupCode.StartsWith("F\\FA"));
                    if (request.Type == 2)
                        query = query.Where(e => !e.RootServiceGroupCode.StartsWith("F\\FA") && !e.RootServiceGroupCode.StartsWith("F\\FB"));
                }
            }
            if (!string.IsNullOrEmpty(request.GroupCode))
            {
                query = query.Where(e => e.ServiceGroupCode == request.GroupCode);
            }
            var items = query.OrderBy(m => m.Code).Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            int count = query.Count();
            return Ok(new
            {
                count,
                items = items.Select(e => new ServicesModel
                {
                    Id = e.Id,
                    ViName = e.ViName,
                    EnName = e.EnName,
                    Code = e.Code,
                    CombinedName = e.CombinedName,
                    ServiceGroupCode = e.ServiceGroupCode,
                    ItemType = GetItemType(e.ServiceGroupCode),
                    HISId = e.HISId,
                    ServiceType = GetServiceType(e.RootServiceGroupCode),
                    AllowSetupQty = IsAllowSetupQty(e.ServiceGroupCode, e.Code, ServiceGroupCodeAllowSetupQty, ServiceCodeAllowSetupQty),
                    Qty = 1
                })
            });
        }
        [HttpGet]
        [Route("api/DoctorPlaceDiagnosticsOrder/Services/LastItemChargeByDoctor/")]
        // [Permission(Code = "DRS0005")]
        public IHttpActionResult LastItemChargeByDoctor([FromUri] ServicesParameterModel request = null)
        {
            if (GetAppConfig("ISLOCKLastItemChargeByDoctor") == "TRUE")
            {
                return Ok(new
                {
                    items = new List<ServicesModel>()
                });
            }
            using (var txn = GetNewReadUncommittedScope())
            {
                var username = getUsername();

                var charge_id = (from charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable().Where(
                                e => e.CreatedBy == username && e.ServiceType == request.Type
                             )
                                 orderby charge_item_sql.CreatedAt descending
                                 select charge_item_sql.ServiceId).Take(30).ToListNoLock();

                var query = (from sql in unitOfWork.ServiceRepository.AsQueryable().Where(
                                e => !e.IsDeleted && e.IsActive && e.Type == "SRV" && charge_id.Contains(e.Id)
                             )
                             select new ServicesModel
                             {
                                 Id = sql.Id,
                                 ViName = sql.ViName,
                                 EnName = sql.EnName,
                                 Code = sql.Code,
                                 CombinedName = sql.CombinedName,
                                 ServiceGroupCode = sql.ServiceGroup.Code,
                                 HISId = sql.HISId,
                                 GroupId = sql.ServiceGroupId,
                                 RootServiceGroupCode = sql.RootServiceGroupCode,
                                 RootServiceGroupId = sql.RootServiceGroupId
                             });

                var items = query.ToListNoLock().ToList();

                string ServiceGroupCodeAllowSetupQty = GetAppConfig("ServiceGroupCodeAllowSetupQty");
                string ServiceCodeAllowSetupQty = GetAppConfig("ServiceCodeAllowSetupQty");

                return Ok(new
                {
                    items = items.Select(e => new ServicesModel
                    {
                        Id = e.Id,
                        ViName = e.ViName,
                        EnName = e.EnName,
                        Code = e.Code,
                        CombinedName = e.CombinedName,
                        ItemType = GetItemType(e.ServiceGroupCode),
                        HISId = e.HISId,
                        ServiceType = GetServiceType(e.RootServiceGroupCode),
                        AllowSetupQty = IsAllowSetupQty(e.ServiceGroupCode, e.Code, ServiceGroupCodeAllowSetupQty, ServiceCodeAllowSetupQty)
                    })
                });
            }
        }
        [HttpGet]
        [Route("api/DoctorPlaceDiagnosticsOrder/Services/LastItemChargeByPID/")]
        // [Permission(Code = "DRS0006")]
        public IHttpActionResult LastItemChargeByPID([FromUri] ServicesParameterModel request = null)
        {
            var items = new List<ServicesModel>();
            return Ok(new
            {
                items
            });
            //var customer = unitOfWork.CustomerRepository.Find(e => !e.IsDeleted && e.PID == request.PID).FirstOrDefault();
            //if (customer == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);

            //DateTime end = DateTime.Now;
            //DateTime start = end.AddDays(-1);

            //var query = (from sql in unitOfWork.ChargeItemRepository.AsQueryable().Where(
            //                e => !e.IsDeleted && e.CustomerId == customer.Id && e.CreatedAt >= start
            //             )
            //             select new ServicesModel
            //             {
            //                 Code = sql.ServiceCode,
            //                 CreatedAt = sql.CreatedAt,
            //                 CreatedBy = sql.CreatedBy,
            //                 Status = sql.Status
            //             });

            //var items = query.OrderByDescending(e => e.CreatedAt).Skip((request.PageNumber - 1) * request.PageSize)
            //    .Take(request.PageSize)
            //    .ToList();
            //return Ok(new
            //{
            //    items = items.Where(e => e.Status == Constant.ChargeItemStatus.Placed).Select(e => new ServicesModel
            //    {
            //        Id = e.Id,
            //        Code = e.Code,
            //        CreatedAt = e.CreatedAt,
            //        CreatedBy = e.CreatedBy,
            //        Status = e.Status
            //    })
            //});
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/ChargeV2/")]
        [Permission(Code = "DRS0007")]
        public async Task<IHttpActionResult> SubmitChargeV2Async([FromBody] ChargeModel request)
        {
            // return Content(HttpStatusCode.BadRequest, Message.PLACING_ORDER_VISIT_IS_CLOSED);
            var is_open = await IsVisitOpenV2Async(request.Charge.PatientVisitId.Value);
            if (is_open)
            {
                List<ChargeItem> listLabChargeItem = new List<ChargeItem>();
                List<ChargeItem> listRadChargeItem = new List<ChargeItem>();
                List<ChargeItem> listAllChargeItem = new List<ChargeItem>();

                Customer customer = unitOfWork.CustomerRepository.FirstOrDefault(c => !c.IsDeleted && c.PID == request.PID);

                if (customer == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);

                request.Visit.CustomerId = customer.Id;

                ChargeVisit local_charge_visit = GetOrCreateChargeVisit(request.Visit);

                request.Charge.ChargeVisitId = local_charge_visit.Id;

                request.Charge.Status = 0;

                unitOfWork.ChargeRepository.Add(request.Charge);
                unitOfWork.Commit();

                var list_service_group_allow_quantity = GetAppConfig("ServiceGroupCodeAllowSetupQuantity");
                var list_service_allow_quantity = GetAppConfig("ServiceCodeAllowSetupQuantity").Split(',').ToList();

                var ef_drs_ver = GetAppConfig("ef_drs_ver");

                var list_for_check = list_service_allow_quantity.Concat(getAllServiceCodeByServiceGroupCode(list_service_group_allow_quantity)).ToList();
                int ind = 1;
                foreach (var data in request.ChargeItems)
                {
                    //Common information
                    data.ChargeId = request.Charge.Id;
                    data.CustomerId = customer.Id;
                    data.Status = Constant.ChargeItemStatus.Placing;
                    data.DoctorAD = request.Charge.DoctorAD;
                    data.VisitCode = request.Charge.VisitCode;
                    data.VisitType = request.Charge.VisitType;
                    data.HospitalCode = request.Charge.HospitalCode;
                    data.InitialDiagnosis = request.Charge.Diagnosis;

                    data.ChargeItemType = getChargeItemType(data.ServiceType);

                    if (string.IsNullOrEmpty(data.Priority)) data.Priority = request.Charge.Priority;
                    if (string.IsNullOrEmpty(data.Reason)) data.Reason = request.Charge.Reason;

                    //If Item has Procedure => Radiology
                    if (data.RadiologyProcedureId != null)
                    {
                        data.ChargeItemType = Constant.ChargeItemType.Rad;
                        if (data.Qty != null && data.Qty > 1)
                        {
                            for (int i = 0; i < data.Qty; i++)
                            {
                                ChargeItem newItem = new ChargeItem();
                                CopyObjProperties(data, newItem);
                                listRadChargeItem.Add(newItem);
                            }
                        }
                        else
                        {
                            listRadChargeItem.Add(data);
                        }
                    }
                    else
                    {
                        //Check if Item is Lab
                        var LabOrderableRefData = getLabOrderableRef(data.ItemId);
                        if (LabOrderableRefData != null)
                        {
                            var ServiceDepartmentId = await getServiceDepartmentAsync(data.PatientLocationId, data.ItemId);
                            if (ServiceDepartmentId != null)
                            {
                                data.ServiceDepartmentId = (Guid)ServiceDepartmentId;
                            }
                            data.PlacerOrderableId = LabOrderableRefData.CpoeOrderableId;
                            data.ChargeItemType = Constant.ChargeItemType.Lab;

                            if (data.Qty != null && data.Qty > 1)
                            {
                                for (int i = 0; i < data.Qty; i++)
                                {
                                    ChargeItem newItem = new ChargeItem();
                                    CopyObjProperties(data, newItem);
                                    listLabChargeItem.Add(newItem);
                                }
                            }
                            else
                            {
                                listLabChargeItem.Add(data);
                            }
                        }
                        else
                        {
                            data.ChargeItemType = Constant.ChargeItemType.Allies;
                            if (data.Qty != null && data.Qty > 1)
                            {
                                if (hasSetupQuantity(data.ServiceCode, list_for_check))
                                {
                                    data.Quantity = data.Qty.ToString();
                                    listAllChargeItem.Add(data);
                                }
                                else
                                {
                                    for (int i = 0; i < data.Qty; i++)
                                    {
                                        ChargeItem newItem = new ChargeItem();
                                        CopyObjProperties(data, newItem);
                                        listAllChargeItem.Add(newItem);
                                    }
                                }
                            }
                            else
                            {
                                listAllChargeItem.Add(data);
                            }
                        }
                    }
                    if (data.Microbiology != null)
                    {
                        unitOfWork.ChargeItemMicrobiologyRepository.Add(data.Microbiology);
                        data.ChargeItemMicrobiologyId = data.Microbiology.Id;
                    }
                    if (data.Pathology != null)
                    {
                        unitOfWork.ChargeItemPathologyRepository.Add(data.Pathology);
                        data.ChargeItemPathologyId = data.Pathology.Id;
                    }
                    ind++;
                }
               
                unitOfWork.Commit();
                
                var allData = listAllChargeItem.Concat(listLabChargeItem).Concat(listRadChargeItem);
                var itemForInsert = new List<ChargeItemDto>();
                
                if (string.IsNullOrEmpty(ef_drs_ver))
                {
                    foreach (var item in allData)
                    {
                        itemForInsert.Add(Mapper.Map<ChargeItem, ChargeItemDto>(item));
                    }
                    unitOfWorkDapper.ChargeItemDtoRepository.Adds(itemForInsert);
                } else
                {
                    foreach (var item in allData)
                    {
                        unitOfWork.ChargeItemRepository.Add(item);
                    }
                    unitOfWork.Commit();
                }

                return Ok(new
                {
                    ChargeId = request.Charge.Id
                });

            }
            else
            {
                return Content(HttpStatusCode.BadRequest, Message.PLACING_ORDER_VISIT_IS_CLOSED);
            }

        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/ChargeV2/{ChargeId}")]
        [Permission(Code = "DRS0007")]
        public async Task<IHttpActionResult> SubmitChargeV2DrsAsync(Guid ChargeId)
        {
            var charge = unitOfWork.ChargeRepository.Find(c => c.Id == ChargeId).FirstOrDefault();

            if (charge == null) return Content(HttpStatusCode.BadRequest, Message.DATA_NOT_FOUND);
            // return Content(HttpStatusCode.BadRequest, Message.PLACING_ORDER_VISIT_IS_CLOSED);
            var is_open = await IsVisitOpenV2Async(charge.PatientVisitId.Value);
            if (is_open)
            {
                var Oh_Service = new OHService(unitOfWork);
                var rsLab = new OHServiceResult();
                var rsRad = new OHServiceResult();
                var rsAll = new OHServiceResult();
                // var listAllChargeItems = unitOfWork.ChargeItemRepository.Find(c => c.ChargeId == ChargeId).ToList();

                rsRad = await Oh_Service.PlaceRadOrderAsync(new List<Guid>().ToArray(), ChargeId);
                rsAll = await Oh_Service.PlaceAlliedOrderAsync(new List<Guid>().ToArray(), ChargeId);
                rsLab = await Oh_Service.PlaceLabOrderAsync(new List<Guid>().ToArray(), ChargeId);

                charge.Status = 1;
                unitOfWork.Commit();

                return Ok(new
                {
                    Total = rsRad.Total + rsAll.Total + rsLab.Total,
                    OK = rsAll.OK + rsLab.OK + rsRad.OK,
                    Failed = rsAll.Failed + rsLab.Failed + rsRad.Failed
                });
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, Message.PLACING_ORDER_VISIT_IS_CLOSED);
            }

        }

        //[HttpPost]
        //[Route("api/DoctorPlaceDiagnosticsOrder/ChargeV3/{ChargeId}")]
        //public async Task<IHttpActionResult> SubmitChargeV3Drs(Guid ChargeId)
        //{
        //    var charge = unitOfWork.ChargeRepository.Find(c => c.Id == ChargeId).FirstOrDefault();
            
        //    if (charge == null) return Content(HttpStatusCode.BadRequest, Message.DATA_NOT_FOUND);
        //    // return Content(HttpStatusCode.BadRequest, Message.PLACING_ORDER_VISIT_IS_CLOSED);
        //    if (IsVisitOpen(charge.PatientVisitId.Value))
        //    {
        //        var Oh_Service = new OHService();
        //        var rsLab = new OHServiceResult();
        //        var rsRad = new OHServiceResult();
        //        var rsAll = new OHServiceResult();
        //        // var listAllChargeItems = unitOfWork.ChargeItemRepository.Find(c => c.ChargeId == ChargeId).ToList();
        //        rsRad = await PlaceRadOrder(Oh_Service, ChargeId);
        //        rsAll = await PlaceAlliedOrder(Oh_Service, ChargeId);
        //        rsLab = await PlaceLabOrder(Oh_Service, ChargeId);  
        //        charge.Status = 1;
        //        unitOfWork.Commit();

        //        return Ok(new
        //        {
        //            Total = rsRad.Total + rsAll.Total + rsLab.Total,
        //            OK = rsAll.OK + rsLab.OK + rsRad.OK,
        //            Failed = rsAll.Failed + rsLab.Failed + rsRad.Failed
        //        });
        //    }
        //    else
        //    {
        //        return Content(HttpStatusCode.BadRequest, Message.PLACING_ORDER_VISIT_IS_CLOSED);
        //    }
        //}
        //public async Task<OHServiceResult> PlaceRadOrder(OHService Oh_Service, Guid ChargeId)
        //{
        //    Task<OHServiceResult> rsRad = Oh_Service.PlaceRadOrderV3(new List<Guid>().ToArray(), ChargeId);

        //    return await rsRad;
        //}
        //public async Task<OHServiceResult> PlaceAlliedOrder(OHService Oh_Service, Guid ChargeId)
        //{
        //    var rsAllied = Oh_Service.PlaceAlliedOrderV3(new List<Guid>().ToArray(), ChargeId);           

        //    return await rsAllied;
        //}
        //public async Task<OHServiceResult> PlaceLabOrder(OHService Oh_Service, Guid ChargeId)
        //{
        //    var rsLab = Oh_Service.PlaceLabOrderV3(new List<Guid>().ToArray(), ChargeId);           
        //    return await rsLab;
        //}
        private bool hasSetupQuantity(string serviceCode, List<string> list_service_group_allow_quantity)
        {
            return list_service_group_allow_quantity.Contains(serviceCode);
        }
        private List<string> getAllServiceCodeByServiceGroupCode(string ServiceGroupCodes)
        {
            var list_service_code = new List<string>();
            var serviceGroupCode = ServiceGroupCodes.Split(',');
            for (int i = 0; i < serviceGroupCode.Length; i++)
            {
                var code = "\\" + serviceGroupCode[i].ToString() + "\\";
                var codes = unitOfWork.ServiceRepository.Find(e => !e.IsDeleted && ("\\" + e.RootServiceGroupCode + "\\").Contains(code)).Select(e => e.Code).ToList();
                list_service_code.AddRange(codes);
            }
            return list_service_code;
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrderCharge/Cancel/")]
        [Permission(Code = "DRS0008")]
        public async Task<IHttpActionResult> CancelChargeAsync([FromBody] ChargeModel request)
        {
            var Oh_Service = new OHService(unitOfWork);
            var listChargeItem = GetChargeItemsByIds(request.Ids);
            var first_item = listChargeItem.First();
            var username = getUsername();
            bool isOpen = IsVisitOpen(first_item.PatientVisitId);
            bool isAuth = first_item.CreatedBy == username;
            if (isOpen || isAuth)
            {
                var notAllowCancel = 0;
                listChargeItem = await UpdatedChargeFromOHAsync(listChargeItem);
                List<ChargeItem> listLabChargeItem = new List<ChargeItem>();
                List<ChargeItem> listRadChargeItem = new List<ChargeItem>();
                List<ChargeItem> listAllChargeItem = new List<ChargeItem>();
                var rsLab = new OHServiceResult();
                var rsRad = new OHServiceResult();
                var rsAll = new OHServiceResult();
                foreach (var i in listChargeItem)
                {
                    if (i.AllowCancel && !IsDiagnosticReported(i.Id))
                    {
                        i.DeletedBy = GetUser().Username;
                        if (i.ChargeItemType == Constant.ChargeItemType.Lab)
                        {
                            listLabChargeItem.Add(i);
                        }
                        else if (i.ChargeItemType == Constant.ChargeItemType.Rad)
                        {
                            listRadChargeItem.Add(i);
                        }
                        else
                        {
                            listAllChargeItem.Add(i);
                        }
                    }
                    else
                    {
                        notAllowCancel++;
                    }
                }
                
                if (listRadChargeItem.Count > 0)
                {
                    rsRad = await Oh_Service.CancelRadOrderAsync(listRadChargeItem, getUsername());
                }
                if (listAllChargeItem.Count > 0)
                {
                    rsAll = await Oh_Service.CancelAlliedOrderAsync(listAllChargeItem, getUsername());
                }
                if (listLabChargeItem.Count > 0)
                {
                    rsLab = await Oh_Service.CancelLabOrderAsync(listLabChargeItem, getUsername());
                }
                unitOfWork.Commit();
                return Ok(new
                {
                    Total = listChargeItem.Count,
                    OK = rsAll.OK + rsLab.OK + rsRad.OK,
                    Failed = rsAll.Failed + rsLab.Failed + rsRad.Failed,
                    NotAllowCancel = notAllowCancel,
                    Items = listChargeItem
                });
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, Message.CANCELLING_ORDER_VISIT_IS_CLOSED);
            }
        }
        private bool IsUnlockForCancelCharge(string visitCode, string patientId)
        {
            var RecordCode = string.Format("{0}-{1}", visitCode, patientId);
            var now = DateTime.Now;
            var username = getUsername();
            return unitOfWork.UnlockFormToUpdateRepository.Find(e => !e.IsDeleted && e.FormCode == "DRS" && e.RecordCode == RecordCode && e.Username == username && e.ExpiredAt >= now).Count() > 0;
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/GetUpdate/")]
        // [Permission(Code = "DRS0009")]
        public async Task<IHttpActionResult> GetUpdateChargeAsync([FromBody] ChargeModel request)
        {
            if(request == null || request.ChargeId == null)
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }    
            var charge = unitOfWork.ChargeRepository.Find(e => e.Id == request.ChargeId).FirstOrDefault();

            if (charge == null) return Content(HttpStatusCode.BadRequest, Message.CONTENT_NOT_FOUND);

            List<ChargeItem> listChargeItems = GetChargeItems(request.ChargeId);
            List<ChargeItem> listChargeItemUpdated = new List<ChargeItem>();
            if (charge.Status == 0)
            {
                foreach (var u in listChargeItems)
                {
                    u.Status = Constant.ChargeItemStatus.DRSFailed;
                    u.FailedReason = "Chỉ định đến OH bị lỗi, vui lòng chỉ định lại!";
                }
                charge.Status = 1;
                unitOfWork.Commit();
                listChargeItemUpdated = listChargeItems;
            } else
            {
                listChargeItemUpdated = await UpdatedChargeFromOHAsync(listChargeItems);
            }

            var first_item = listChargeItemUpdated.FirstOrDefault();
            if (first_item != null)
            {
                var userc = unitOfWork.UserRepository.Find(e => !e.IsDeleted && e.Username == first_item.CreatedBy).FirstOrDefault();
                return Ok(new
                {
                    ChargeItem = listChargeItemUpdated,
                    User = new { userc?.Username, userc?.Fullname }
                });
            }
            else
            {
                return Ok(new
                {
                    ChargeItem = listChargeItemUpdated,
                    User = new { Username = "", Fullname = "" }
                });
            }
        }
        private async Task<List<ChargeItem>> UpdatedChargeFromOHAsync(List<ChargeItem> listChargeItems)
        {
            listChargeItems = await GetUpdateChargesFromOHAsync(listChargeItems);
            return listChargeItems;
        }
        private async Task<List<ChargeItem>> GetUpdateChargesFromOHAsync(List<ChargeItem> listChargeItems)
        {
            if (listChargeItems.Count > 0)
            {
                var first_re = listChargeItems.FirstOrDefault();
                var visit_code = first_re.VisitCode;
                var visit_type = first_re.VisitType;
                List<string> listLabChargeItem = new List<string>();
                List<string> listRadChargeItem = new List<string>();
                List<string> listAllChargeItem = new List<string>();
                foreach (var i in listChargeItems)
                {
                    if (i.Status != Constant.ChargeItemStatus.Failed)
                    {
                        if (i.ChargeItemType == Constant.ChargeItemType.Lab)
                        {
                            listLabChargeItem.Add(string.Format("{0}{1}", DrsPrefix, i.PlacerIdentifyNumber.ToString()));
                        }
                        else if (i.ChargeItemType == Constant.ChargeItemType.Rad)
                        {
                            listRadChargeItem.Add(string.Format("{0}{1}", DrsPrefix, i.PlacerIdentifyNumber.ToString()));
                        }
                        else
                        {
                            // if(!string.IsNullOrEmpty(i.ChargeDetailId.ToString()))
                            listAllChargeItem.Add(string.Format("{0}{1}", DrsPrefix, i.PlacerIdentifyNumber.ToString()));
                        }
                    }
                }
                var listUpdatedCharge = await HisClient.getUpdateChargeAsync(
                                            string.Join(";", listAllChargeItem),
                                            string.Join(";", listLabChargeItem),
                                            string.Join(";", listRadChargeItem), visit_code, visit_type);
                if (listUpdatedCharge.Count > 0)
                {
                    foreach (var u in listUpdatedCharge)
                    {
                        var item = listChargeItems.Where(i => i.ChargeDetailId == u.ChargeDetailId || u.PlacerOrderNumber.Contains(i.PlacerIdentifyNumber.ToString())).FirstOrDefault();
                        if (item != null)
                        {
                            item.ChargeDetailId = u.ChargeDetailId;
                            item.PlacerOrderStatus = u.PlacerOrderStatus;
                            item.RadiologyScheduledStatus = u.RadiologyScheduledStatus;
                            item.PaymentStatus = u.PaymentStatus;
                            item.SpecimenStatus = u.SpecimenStatus;
                            item.FailedReason = null;
                            if (u.FillerOrderNumber != null) item.Filler = u.FillerOrderNumber;
                            if (item.ChargeItemType == Constant.ChargeItemType.Rad)
                            {
                                if ((u.PlacerOrderStatus == CpoePlacerOrderStatus.Verified || u.PlacerOrderStatus == CpoePlacerOrderStatus.ResultsPublished) && (item.Status == Constant.ChargeItemStatus.Placing || item.Status == Constant.ChargeItemStatus.DRSFailed))
                                {
                                    item.Status = Constant.ChargeItemStatus.Placed;
                                }

                                if (u.PlacerOrderStatus == CpoePlacerOrderStatus.Cancelled)
                                {
                                    item.Status = Constant.ChargeItemStatus.Cancelled;
                                }
                            }
                            else
                            {
                                if (item.ChargeDetailId != null)
                                {
                                    item.Status = Constant.ChargeItemStatus.Placed;
                                }
                            }
                            //Move charge
                            if (u.NewChargeId != null)
                            {
                                item.ChargeDetailId = u.NewChargeId;
                                if (u.ChargeDeletedDate != null)
                                {
                                    item.Status = Constant.ChargeItemStatus.Cancelled;
                                    item.CancelComment = "[Cancelled from EF]" + item.CancelComment;
                                }
                                else
                                {
                                    item.Status = Constant.ChargeItemStatus.Placed;
                                }
                            }
                            else
                            {
                                if (u.ChargeDeletedDate != null)
                                {
                                    item.Status = Constant.ChargeItemStatus.Cancelled;
                                    item.CancelComment = "[Cancelled from EF]" + item.CancelComment;
                                }
                            }
                            // unitOfWork.ChargeItemRepository.Update(item);
                        }
                    }

                    
                    unitOfWork.Commit();
                }
                //foreach (var u in listChargeItems)
                //{
                //    var item = listUpdatedCharge.Where(i => i.ChargeDetailId == u.ChargeDetailId || i.PlacerOrderNumber.Contains(u.PlacerIdentifyNumber.ToString())).FirstOrDefault();
                //    if (item == null)
                //    {
                //        u.Status = Constant.ChargeItemStatus.Failed;
                //        u.FailedReason = "Chỉ định đến OH bị lỗi, vui lòng chỉ định lại!";
                //    }
                //}
            }
            return listChargeItems;
        }
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/ChargeVisit/History/")]
        // [Permission(Code = "DRS0010")]
        public IHttpActionResult GetChargeVisitHistory([FromUri] HISCustomerParameterModel request)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                var customer = unitOfWork.CustomerRepository.FirstOrDefault(c => !c.IsDeleted && c.PID == request.PID);
                if (customer == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);
                var current_username = getUsername();
                var isVIPmanage = IsVIPMANAGE();
                var IsUnlockVip = IsUnlockVipByPid(request.PID, UnlockVipType.PlaceDiagnosticsOrder);
                var charge = unitOfWork.ChargeVistRepository.Find(e => !e.IsDeleted && e.VisitGroupType != Constant.VMHC_CODE && e.CustomerId == customer.Id && (IsUnlockVip || isVIPmanage || !customer.IsVip || current_username.ToLower() == e.DoctorAD.ToLower() || current_username.ToLower() == e.CreatedBy.ToLower())).OrderByDescending(e => e.UpdatedAt).ToList().Select(e => new
                {
                    e.Id,
                    e.HospitalCode,
                    e.DoctorAD,
                    e.VisitCode,
                    e.VisitType,
                    e.CreatedAt,
                    e.CreatedBy,
                    e.ActualVisitDate,
                    e.AreaName,
                    e.PatientLocationId,
                    e.PatientVisitId,
                    IsSelected = false
                });
                string HospitalCode = charge.FirstOrDefault()?.HospitalCode;
                var Site = unitOfWork.SiteRepository.Find(e => !e.IsDeleted && e.ApiCode == HospitalCode).FirstOrDefault();
                return Ok(new { Items = charge, Customer = customer, Site });
            }
        }
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/Charge/History/{id}")]
        // [Permission(Code = "DRS0011")]
        public IHttpActionResult GetChargeHistory(Guid id)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                var charge = unitOfWork.ChargeRepository.Find(e => !e.IsDeleted && e.ChargeVisitId == id).ToList().OrderByDescending(e => e.CreatedAt).Select(e => new
                {
                    e.Id,
                    e.HospitalCode,
                    e.VisitCode,
                    e.VisitId,
                    e.VisitType,
                    e.Priority,
                    e.Reason,
                    e.Room,
                    e.Bed,
                    e.CreatedAt,
                    e.CreatedBy,
                    e.Diagnosis,
                    e.PatientVisitId,
                    e.ChargeVisit.CustomerId,
                    Items = GetChargeItems(e.Id)
                });
                var first_item = charge.FirstOrDefault();
                string HospitalCode = first_item?.HospitalCode;
                var Site = new Site();
                if (HospitalCode == "HHN")
                {
                    Site = unitOfWork.SiteRepository.Find(e => !e.IsDeleted && (e.ApiCode == HospitalCode || e.ApiCode == "HTC")).FirstOrDefault();
                }
                else
                {
                    Site = unitOfWork.SiteRepository.Find(e => !e.IsDeleted && e.ApiCode == HospitalCode).FirstOrDefault();
                }
                var user = unitOfWork.UserRepository.Find(e => !e.IsDeleted && e.Username == first_item.CreatedBy).FirstOrDefault();
                return Ok(new
                {
                    Items = charge,
                    Site = new
                    {
                        Site.Location,
                        Site.Address,
                        Site.ApiCode,
                        Site.Code,
                        Site.EnName,
                        Site.ViName,
                        Site.Hotline,
                        Site.PhoneNumber,
                        Site.Province,
                        Site.Emergency
                    },
                    User = new { user?.Username, user?.Fullname }
                });
            }
        }
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/Services/GetDetailService/{id}")]
        // [Permission(Code = "DRS0012")]
        public IHttpActionResult GetDetailService(Guid id, [FromUri] ServicesGetPriceParameterModel request)
        {
            var service = unitOfWork.ServiceRepository.Find(e => !e.IsDeleted && e.IsActive && e.Id == id).FirstOrDefault();

            // var LabOrderableRefData = getLabOrderableRef(service.HISId);
            bool hasLabDepartment = true;
            //if (LabOrderableRefData != null)
            //{
            //    var ServiceDepartment = getServiceDepartment(request.PatientLocationId, service.HISId);
            //    if (ServiceDepartment == null)
            //    {
            //        hasLabDepartment = false;
            //    }
            //}

            return Ok(new ServicesModel
            {
                Id = service.Id,
                ViName = service.ViName,
                EnName = service.EnName,
                Code = service.Code,
                CombinedName = service.CombinedName,
                ServiceGroupCode = service.ServiceGroup.Code,
                ItemType = GetItemType(service.ServiceGroup.Code),
                ServiceType = GetServiceType(service.RootServiceGroupCode),
                HISId = service.HISId,
                Price = 0,
                hasLabDepartment = hasLabDepartment
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/Services/GetDetailService/")]
        // [Permission(Code = "DRS0013")]
        public IHttpActionResult GetDetailService([FromBody] ServicesGetPriceParameterModel request)
        {
            var query = (from sql in unitOfWork.ServiceRepository.AsQueryable().Where(
                        e => !e.IsDeleted && e.IsActive && request.Ids.Contains(e.Id)
                        )
                         select new ServicesModel
                         {
                             Id = sql.Id,
                             ViName = sql.ViName,
                             EnName = sql.EnName,
                             Code = sql.Code,
                             ServiceGroupCode = sql.ServiceGroup.Code,
                             RootServiceGroupCode = sql.RootServiceGroupCode,
                             HISId = sql.HISId
                         });

            var items = query.OrderByDescending(m => m.Code).ToList();

            string ServiceGroupCodeAllowSetupQty = GetAppConfig("ServiceGroupCodeAllowSetupQty");
            string ServiceCodeAllowSetupQty = GetAppConfig("ServiceCodeAllowSetupQty");

            return Ok(new
            {
                items = items.Select(e => new ServicesModel
                {
                    Id = e.Id,
                    ViName = e.ViName,
                    EnName = e.EnName,
                    Code = e.Code,
                    CombinedName = e.CombinedName,
                    ServiceGroupCode = e.ServiceGroupCode,
                    ItemType = GetItemType(e.ServiceGroupCode),
                    ServiceType = GetServiceType(e.RootServiceGroupCode),
                    RootServiceGroupCode = e.RootServiceGroupCode,
                    HISId = e.HISId,
                    RadiologyProcedurePlanRef = getRadiologyProcedure(e.Code),
                    hasLabDepartment = true,
                    AllowSetupQty = IsAllowSetupQty(e.ServiceGroupCode, e.Code, ServiceGroupCodeAllowSetupQty, ServiceCodeAllowSetupQty)
                })
            });
        }
        //private bool isSetupLabDep(Guid HISId, ServicesGetPriceParameterModel request)
        //{
        //    var LabOrderableRefData = getLabOrderableRef(HISId);
        //    bool hasLabDepartment = true;
        //    if (LabOrderableRefData != null)
        //    {
        //        var ServiceDepartment = getServiceDepartmentAsync(request.PatientLocationId, HISId);
        //        if (ServiceDepartment == null)
        //        {
        //            hasLabDepartment = false;
        //        }
        //    }
        //    return hasLabDepartment;
        //}
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/Services/GetPrice/")]
        // [Permission(Code = "DRS0014")]
        public IHttpActionResult GetPriceServices([FromBody] ServicesGetPriceParameterModel request)
        {
            var serice = OHClient.getServicePrice(request.Code, request.PatientVisitId);
            return Ok(new
            {
                Items = serice
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/ChargeDraft/")]
        // [Permission(Code = "DRS0015")]
        public IHttpActionResult ChargeDraft([FromBody] ChargeDraft request)
        {
            var exitDraft = unitOfWork.ChargeDraftRepository.Find(e => !e.IsDeleted && e.PID == request.PID && e.VisitCode == request.VisitCode).FirstOrDefault();
            if (exitDraft == null)
            {
                unitOfWork.ChargeDraftRepository.Add(request);
                unitOfWork.Commit();
                return Ok(new
                {
                    request = request
                });
            }
            else
            {
                if (request.IsDeleted)
                {
                    unitOfWork.ChargeDraftRepository.Delete(exitDraft);
                }
                else
                {
                    exitDraft.Title = request.Title;
                    exitDraft.Note = request.Note;
                    exitDraft.RawData = request.RawData;
                    unitOfWork.ChargeDraftRepository.Update(exitDraft);
                }
                unitOfWork.Commit();
                return Ok(new
                {
                    request = exitDraft
                });
            }
        }

        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/OrderSets/List/")]
        [Permission(Code = "DRS0016")]
        public IHttpActionResult GetOrderSets([FromUri] ServicesParameterModel request)
        {
            var current_user = GetUser();
            var current_username = current_user.Username;
            var current_userId = current_user.Id;
            var is_admin = hasAction("VIEWALLORDERSET");
            var MASTERCode = GetAppConfig("ORDERSETMASTER");
            var query = unitOfWork.ChargePackageRepository.Find(e => !e.IsDeleted);
            if (request.Search != null)
            {
                query = query.Where(e => (e.Name != null && e.Name.Contains(request.ConvertedSearch2)) || (e.Code != null && e.Code.Contains(request.ConvertedSearch2)));
            }
            if (request.Type != null)
            {
                switch (request.Type)
                {
                    case 1:
                        query = query.Where(e => e.CreatedBy == current_username);
                        break;
                    case 2:
                        query = query.Where(e => e.ChargePackageUser.Any(f => !f.IsDeleted && f.UserId == current_userId) || MASTERCode.Contains(e.Code));
                        break;
                    case 3:
                        if (is_admin)
                        {
                            if (request.FilterType == null)
                            {
                                // pass
                            }
                            else if (request.FilterType == 1)
                                query = query.Where(e => e.CreatedBy == current_username);
                            else
                                query = query.Where(e => e.ChargePackageUser.Any(f => !f.IsDeleted && f.UserId == current_userId) || MASTERCode.Contains(e.Code));
                        }
                        else
                        {
                            if (request.FilterType == null)
                                query = query.Where(e => e.CreatedBy == current_username || e.ChargePackageUser.Any(f => !f.IsDeleted && f.UserId == current_userId) || MASTERCode.Contains(e.Code));
                            else if (request.FilterType == 1)
                                query = query.Where(e => e.CreatedBy == current_username);
                            else
                                query = query.Where(e => e.ChargePackageUser.Any(f => !f.IsDeleted && f.UserId == current_userId) || MASTERCode.Contains(e.Code));
                        }
                        break;
                    default:
                        query = query.Where(e => e.CreatedBy == current_username);
                        break;
                }
            }
            else
            {
                query = query.Where(e => e.CreatedBy == current_username);
            }

            int count = query.Count();

            var items = query.OrderByDescending(m => m.Code).Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            return Ok(new
            {
                items = items.Select(e => new {
                    e.Id,
                    e.Code,
                    e.Name,
                    e.CreatedAt,
                    e.CreatedBy,
                    UserIds = e.ChargePackageUser.Select(f => f.UserId).ToList(),
                    Users = e.ChargePackageUser.Where(f => !f.IsDeleted).Select(f => new
                    {
                        f.User.Id,
                        f.User.Fullname,
                        FullShort = f.User.Fullname,
                        f.User.Username,
                    }).ToList(),
                    ServiceIds = e.ChargePackageService.Select(f => f.ServiceId).ToList(),
                    Services = e.ChargePackageService.Select(f => new {
                        f.Service.Id,
                        f.Service.Code,
                        f.Service.ViName,
                    }).ToList()
                }),
                count
            });
        }
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/OrderSets/Detail/{id}")]
        [Permission(Code = "DRS0016")]
        public IHttpActionResult GetOrderSets(Guid id)
        {
            var data = unitOfWork.ChargePackageRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (data == null) return Content(HttpStatusCode.NotFound, Message.FORMAT_INVALID);
            return Ok(new
            {
                data
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/OrderSets/Create/")]
        [Permission(Code = "DRS0017")]
        public IHttpActionResult CreateOrUpdateOrderSets([FromBody] OrderSetsModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Name)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var exitData = unitOfWork.ChargePackageRepository.Find(e => !e.IsDeleted && ((request.Id != null && e.Id != request.Id && (e.Code == request.Code)) || (request.Id == null && (e.Code == request.Code)))).FirstOrDefault();
            if (exitData != null) return Content(HttpStatusCode.BadRequest, new { ViMessage = "Mã gói đã tồn tại", EnMessage = "Format is NOT correct" });
            exitData = unitOfWork.ChargePackageRepository.Find(e => !e.IsDeleted && ((request.Id != null && e.Id != request.Id && (e.Name == request.Name)) || (request.Id == null && (e.Name == request.Name)))).FirstOrDefault();
            if (exitData != null) return Content(HttpStatusCode.BadRequest, new { ViMessage = "Tên gói đã tồn tại", EnMessage = "Format is NOT correct" });

            var data = new ChargePackage() { };
            if (request.Id != null)
            {
                data = unitOfWork.ChargePackageRepository.Find(e => !e.IsDeleted && e.Id == request.Id).FirstOrDefault();
                if (data == null) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
                data.Code = request.Code;
                data.Name = request.Name;

                unitOfWork.ChargePackageUserRepository.HardDeleteRange(unitOfWork.ChargePackageUserRepository.Find(e => e.ChargePackageId == data.Id).AsQueryable());
                unitOfWork.ChargePackageServiceRepository.HardDeleteRange(unitOfWork.ChargePackageServiceRepository.Find(e => e.ChargePackageId == data.Id).AsQueryable());

                unitOfWork.Commit();
            }
            else
            {
                data = new ChargePackage()
                {
                    Code = request.Code,
                    Name = request.Name
                };
                unitOfWork.ChargePackageRepository.Add(data);
                unitOfWork.Commit();
            }
            if (request.UserIds != null)
            {
                foreach (var id in request.UserIds)
                {
                    unitOfWork.ChargePackageUserRepository.Add(new ChargePackageUser
                    {
                        UserId = id,
                        ChargePackageId = data.Id
                    });
                }
            }
            if (request.ServiceIds != null)
            {
                foreach (var id in request.ServiceIds)
                {
                    unitOfWork.ChargePackageServiceRepository.Add(new ChargePackageService
                    {
                        ServiceId = id,
                        ChargePackageId = data.Id
                    });
                }
            }
            unitOfWork.Commit();
            return Ok(new
            {
                data.Id
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/OrderSets/Delete/{id}")]
        [Permission(Code = "DRS0018")]
        public IHttpActionResult DeleteOrderSets(Guid id)
        {
            var data = unitOfWork.ChargePackageRepository.GetById(id);
            if (data == null) return Content(HttpStatusCode.NotFound, Message.FORMAT_INVALID);
            if (data.CreatedBy != getUsername()) return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            unitOfWork.ChargePackageRepository.Delete(data);
            unitOfWork.Commit();
            return Ok(new
            {
                data.Id
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/DoctorPlaceDiagnosticsOrder/OrderSets/DeleteShare/{id}")]
        [Permission(Code = "DRS0018")]
        public IHttpActionResult DeleteShareOrderSets(Guid id)
        {
            var data = unitOfWork.ChargePackageRepository.GetById(id);
            var current_user = GetUser();
            if (data == null) return Content(HttpStatusCode.NotFound, Message.FORMAT_INVALID);
            var item = unitOfWork.ChargePackageUserRepository.Find(e => !e.IsDeleted && e.UserId == current_user.Id && e.ChargePackageId == id).FirstOrDefault();
            if (item == null) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            unitOfWork.ChargePackageUserRepository.HardDelete(item);
            unitOfWork.Commit();
            return Ok(new
            {
                data.Id
            });
        }

        private CpoeOrderableModel getLabOrderableRef(Guid ItemHisId)
        {
            var query = (from sql in unitOfWork.LabOrderableRefRepository.AsQueryable().Where(
                       e => !e.IsDeleted && e.ItemId == ItemHisId && e.ActiveStatus == "A"
                       )
                         join cpoeOrderables_tbl in unitOfWork.CpoeOrderableRepository.AsQueryable()
                         on sql.LabOrderableRid equals cpoeOrderables_tbl.LabOrderableRid

                         select new CpoeOrderableModel
                         {
                             CpoeOrderableId = cpoeOrderables_tbl.CpoeOrderableId

                         });


            return query.FirstOrDefault();
        }
        private Customer CreateCustomer(dynamic request)
        {
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
                IdentificationCard = request.IdentificationCard
            };
            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();
            return customer;
        }

        private List<ChargeItem> GetChargeItemsByIds(List<Guid> ids)
        {
            return unitOfWork.ChargeItemRepository.AsQueryable().Where(c => ids.Contains(c.Id)).ToList();
        }
        private List<RadiologyProcedurePlanRef> getRadiologyProcedure(string service_code)
        {
            return unitOfWork.RadiologyProcedurePlanRefRepository.Find(e => e.ActiveStatus == "A" && e.ShortCode.StartsWith(service_code)).ToList();
        }
        private ChargeVisit GetOrCreateChargeVisit(ChargeVisit Visit)
        {
            // var sitecode = GetSiteAPICode();
            var charge_visit = unitOfWork.ChargeVistRepository.Find(e =>
                e.VisitCode == Visit.VisitCode &&
                e.VisitGroupType == Visit.VisitGroupType &&
                e.VisitType == Visit.VisitType &&
                e.HospitalCode == Visit.HospitalCode &&
                e.PatientVisitId == Visit.PatientVisitId &&
                e.DoctorAD == Visit.DoctorAD &&
                e.PatientLocationCode == Visit.PatientLocationCode
            ).FirstOrDefault();
            if (charge_visit == null)
            {
                unitOfWork.ChargeVistRepository.Add(Visit);
                unitOfWork.Commit();
                return Visit;
            }
            unitOfWork.ChargeVistRepository.Update(charge_visit);
            unitOfWork.Commit();
            return charge_visit;
        }
        private List<ChargeItem> GetChargeItems(Guid ChargeId)
        {
            return unitOfWork.ChargeItemRepository.Find(item => !item.IsDeleted && ChargeId == item.ChargeId).ToList().OrderBy(e => e.CreatedAt).Select(e => foE(e)).ToList();
        }
        private ChargeItem foE(ChargeItem e)
        {
            e.DiagnosticReported = IsDiagnosticReported(e.Id);
            return e;
        }
        private string getChargeItemType(int itemType)
        {
            if (itemType == 0) return Constant.ChargeItemType.Lab;
            if (itemType == 1) return Constant.ChargeItemType.Rad;
            return Constant.ChargeItemType.Allies;
        }

        private async Task<Guid?> getServiceDepartmentAsync(Guid patientLocationId, Guid HISId)
        {
            var data_from_hist = await OHAPIService.getServiceDepartmentIdAsync(patientLocationId, HISId);
            var first_item = data_from_hist.FirstOrDefault();
            if (first_item != null) return first_item.ServiceDepartmentId;
            return null;
        }
        private DiagnosisAndICDModel GetVisitDiagnosis(Guid visit_id, string visit_type)
        {
            if (visit_type == "ED")
            {
                ED visit = GetED(visit_id);
                if (visit != null)
                {
                    var data_di = visit.EmergencyRecord.EmergencyRecordDatas;
                    var data_etr = visit.EmergencyTriageRecord.EmergencyTriageRecordDatas;
                    return new DiagnosisAndICDModel
                    {
                        Diagnosis = data_di.FirstOrDefault(e => e.Code == "ER0ID0ANS")?.Value,
                        Reason = data_etr.FirstOrDefault(e => e.Code == "ETRCC0ANS")?.Value,
                        VisitType = "ED"
                    };
                }
            }
            if (visit_type == "OPD")
            {
                OPD visit = GetOPD(visit_id);
                if (visit != null)
                {
                    var data_eon = visit.OPDOutpatientExaminationNote.OPDOutpatientExaminationNoteDatas;
                    return new DiagnosisAndICDModel
                    {
                        Diagnosis = data_eon.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value,
                        Reason = data_eon.FirstOrDefault(e => e.Code == "OPDOENCC0ANS")?.Value,
                        VisitType = "OPD"
                    };
                }
            }
            if (visit_type == "IPD")
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
                                DiagnosisOption = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTCDKTANS")?.Value,
                                Reason = data_eon.FirstOrDefault(e => e.Code == "IPDMRPTLDVVANS")?.Value,
                                VisitType = "IPD"
                            };
                            return returnData;
                        }
                    }
                }
            }
            return new DiagnosisAndICDModel { };
        }
        private int GetItemType(string group_code)
        {
            if (group_code.StartsWith("FB.05")) return 0;
            if (group_code.StartsWith("FB.01")) return 1;
            return 2;
        }
        private int GetServiceType(string group_code)
        {
            if (group_code.StartsWith("F\\FB")) return 0;
            if (group_code.StartsWith("F\\FA")) return 1;
            return 2;
        }
        private List<Guid> getListGroupId(Guid parrent_id)
        {
            var parrent = unitOfWork.ServiceGroupRepository.GetById(parrent_id);
            return unitOfWork.ServiceGroupRepository.Find(e => !e.IsDeleted && (e.KeyStruct.Contains(parrent.HISId.ToString()) || e.Id == parrent_id)).Select(e => e.Id).ToList();
        }
        private Customer UpdateCustomer(dynamic request, Customer local_customer)
        {
            DateTime? dob = null;
            if (!string.IsNullOrEmpty(request.DateOfBirth))
            {
                dob = DateTime.ParseExact(request.DateOfBirth, Constant.DATE_FORMAT, null);
            }

            local_customer.Fullname = request.Fullname;
            local_customer.DateOfBirth = dob;
            local_customer.Phone = request.Phone;
            local_customer.Gender = request.Gender;
            local_customer.Job = request.Job;
            local_customer.Address = request.Address;

            unitOfWork.Commit();
            return local_customer;
        }
        private bool IsAllowSetupQty(string group_code, string code, string serviceGroupCodeAllowSetupQty = null, string serviceCodeAllowSetupQty = null)
        {
            string ServiceGroupCodeAllowSetupQty = string.IsNullOrEmpty(serviceGroupCodeAllowSetupQty) ? GetAppConfig("ServiceGroupCodeAllowSetupQty") : serviceGroupCodeAllowSetupQty;
            string ServiceCodeAllowSetupQty = string.IsNullOrEmpty(serviceCodeAllowSetupQty) ? GetAppConfig("ServiceCodeAllowSetupQty") : serviceCodeAllowSetupQty;
            
            return ServiceGroupCodeAllowSetupQty.Split(',').Contains(group_code) || ServiceCodeAllowSetupQty.Split(',').Contains(code);
        }
        private bool IsVisitOpen(Guid patientVisitId)
        {
            var isOpen = false;
            var visit = OHClient.GetVisitDetails(patientVisitId);
            if (visit != null && visit.Count > 0 && visit.First().ClosureDate == null)
            {
                isOpen = true;
            }
            return isOpen;
        }
        private async Task<bool> IsVisitOpenV2Async(Guid patientVisitId)
        {
            var isOpen = false;
            var visit_from_oh = await HisClient.GetVisitDetailsV2Async(patientVisitId);
            var visit = visit_from_oh.FirstOrDefault();
            if (visit_from_oh != null && visit_from_oh.Count > 0 && visit.ClosureDate == null)
            {
                isOpen = true;
            }
            return isOpen;
        }
    }
}