using AutoMapper;
using DataAccess;
using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EForm.Models.DiagnosticReporting;
using EForm.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EForm.Helper;
using System.Web.Http;
using Clients.HisClient;

namespace EForm.Controllers
{
    [SessionAuthorize]
    public class PathologyMicrobiologyFormController : BaseApiController
    {
        private readonly string DrsPrefix = ConfigurationManager.AppSettings["DRS_PREFIX"] != null ? ConfigurationManager.AppSettings["DRS_PREFIX"].ToString() : "DRS";
        [HttpGet]
        [Route("api/PathologyMicrobiology/{PID}")]
        //[Permission(Code = "XEMCLSBYPID")]
        public IHttpActionResult GetListPathologyMicrobiology([FromUri] DiagnosticReportingParameterModel param, string PID)
        {
            
                if (string.IsNullOrEmpty(PID))
                return Content(HttpStatusCode.BadRequest, new
                {
                    ViName = "Không có PID",
                    EnName = "No PID"
                });
            var search = PID;
            Guid? search_by_pid = new Guid();


            var customer = GetCustomerByPid(search);
            if (customer == null)
            {
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            }
            search_by_pid = customer.Id;


            var MicrobiologyService01 = GetAppConfig("MICROBIOLOGY_SERVICE_01").Split(',');
            var MicrobiologyService02 = GetAppConfig("MICROBIOLOGY_SERVICE_02").Split(',');
            var MicrobiologyService = MicrobiologyService01.Concat(MicrobiologyService02);
            

            var query = from charge_item_sql in unitOfWork.ChargeItemRepository.AsQueryable().Where(
                          e => !e.IsDeleted &&
                          // e.Status == Constant.ChargeItemStatus.Placed &&
                          e.CustomerId == search_by_pid &&
                          e.VisitGroupType == Constant.VMHC_CODE
                          && (e.ItemType == 1 || MicrobiologyService.Contains(e.ServiceCode))
                     )

                        join ser_sql in unitOfWork.ServiceRepository.AsQueryable()
                             on charge_item_sql.ServiceId equals ser_sql.Id
                        select new PathologyMicrobiologyTemp
                        {
                            Id = charge_item_sql.Id,
                            VisitCode = charge_item_sql.VisitCode,
                            ServiceEnName = ser_sql.ViName,
                            ServiceViName = ser_sql.ViName,
                            CombinedName = ser_sql.CombinedName,
                            ServiceCode = charge_item_sql.ServiceCode,
                            ChargeId = charge_item_sql.ChargeId,
                            ItemType = charge_item_sql.ItemType,
                            CreatedAt = charge_item_sql.CreatedAt,
                            UpdatedAt = charge_item_sql.UpdatedAt,
                            ChargeItemMicrobiologyId = charge_item_sql.ChargeItemMicrobiologyId,
                            ChargeItemPathologyId = charge_item_sql.ChargeItemPathologyId,
                            SpecimenStatus = charge_item_sql.SpecimenStatus
                        };

            if (!string.IsNullOrEmpty(param.StartAt))
                query = query.Where(e => e.CreatedAt >= param.ConvertedStartAt);
            if (!string.IsNullOrEmpty(param.EndAt))
            {
                DateTime? endAt = param.ConvertedEndAt;
                endAt = endAt.Value.AddSeconds(59);
                query = query.Where(e => e.CreatedAt <= endAt);
            }

            // query = query.Where(e => e.ItemType == 1 || MicrobiologyService.Contains(e.ServiceCode));

            if (!string.IsNullOrEmpty(param.Search))
                query = query.Where(e => e.CombinedName.Contains(param.ConvertedSearch));

            if (!string.IsNullOrEmpty(param.VisitCode))
                query = query.Where(e => e.VisitCode == param.VisitCode);

            

                var items = query
                   .ToListNoLock().OrderByDescending(m => m.CreatedAt);
            // int count = items.Count();
            var current_username = getUsername();
            return Content(HttpStatusCode.OK, new
            {
                count = items.Count(),
                items = items.Select(e => DataFormatted(e, current_username)),
                AUTO_FOCUS_PXN_KHOI_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_KHOI_TE_BAO"].ToString(),
                AUTO_FOCUS_PXN_MO_BENH_HOC = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_MO_BENH_HOC"].ToString(),
                AUTO_FOCUS_PXN_SINH_THIET_LANH = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_SINH_THIET_LANH"].ToString(),
                AUTO_FOCUS_PXN_TE_BAO = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_TE_BAO"].ToString(),
                AUTO_FOCUS_PXN_PHU_KHOA = ConfigurationManager.AppSettings["AUTO_FOCUS_PXN_PHU_KHOA"].ToString(),
                MicrobiologyService01 = GetAppConfig("MICROBIOLOGY_SERVICE_01").Split(','),
                MicrobiologyService02 = GetAppConfig("MICROBIOLOGY_SERVICE_02").Split(','),
                Customer = customer
            });
        }
        // 618000402
        [HttpPost]
        [CSRFCheck]
        [Route("api/PathologyMicrobiology/Create/{type}/{id}")]
        [Permission(Code = "DRS0007")]
        public async Task<IHttpActionResult> CreateAsync(int type, Guid id)
        {
            ChargeItem chargeitem = await GetChargeItemAsync(id);
            if (chargeitem == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);
            if (!chargeitem.AllowEditWithSpecimenStatus) return Content(HttpStatusCode.BadRequest, Message.SPECIMEN_STATUS_ERROR);
            if (type == 0)
            {
                var microbiology = new ChargeItemMicrobiology() { };
                unitOfWork.ChargeItemMicrobiologyRepository.Add(microbiology);
                chargeitem.ChargeItemMicrobiologyId = microbiology.Id;
                unitOfWork.Commit();
            }
            if (type == 1)
            {
                var pathology = new ChargeItemPathology() { };
                unitOfWork.ChargeItemPathologyRepository.Add(pathology);
                chargeitem.ChargeItemPathologyId = pathology.Id;
                unitOfWork.Commit();
            }
            return Content(HttpStatusCode.OK, new { chargeitem.Id });
        }
        [HttpGet]
        [Route("api/PathologyMicrobiology/Detail/{type}/{id}")]
        //[Permission(Code = "DRS0007")]
        public async Task<IHttpActionResult> DetailAsync(string type, Guid id)
        {
            ChargeItem chargeItem = await GetChargeItemAsync(id);
            if (chargeItem == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);

            var current_username = getUsername();
            var charge = unitOfWork.ChargeRepository.FirstOrDefault(e => e.Id == chargeItem.ChargeId);
            var chargeVisit = charge.ChargeVisit;
            var Customer = GetCustomerById((chargeItem.CustomerId));
            if (type == "0")
            {
                var MicrobiologyDto = unitOfWork.ChargeItemMicrobiologyRepository.FirstOrDefault(e => e.Id == chargeItem.ChargeItemMicrobiologyId && !e.IsDeleted);
                if (MicrobiologyDto == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);
                return Content(HttpStatusCode.OK,
                    new
                    {
                        MicrobiologyDto,
                        type,
                        chargeItem.Id,
                        IsReadonly = !(chargeItem.AllowEditWithSpecimenStatus && current_username == MicrobiologyDto.CreatedBy),
                        User = new
                        {
                            Username = MicrobiologyDto.CreatedBy,
                            GetUserByUsername(MicrobiologyDto.CreatedBy)?.Fullname
                        },
                        Customer,
                        Site = getSiteByApiCode(chargeItem.HospitalCode),
                        data = new
                        {
                            charge.VisitId,
                            charge.Reason,
                            charge.Priority,
                            charge.Diagnosis,
                            charge.DoctorAD,
                            charge.PatientVisitId,
                            charge.PatientLocationId,
                            charge.VisitCode,
                            charge.VisitType,
                            charge.HospitalCode,
                            charge.Room,
                            charge.Bed,
                            Items = new List<ChargeItem>() { chargeItem }
                        },
                        ChargeData = new
                        {
                            chargeVisit.PatientLocationCode,
                            chargeVisit.VisitGroupType,
                            chargeVisit.VisitCode,
                            chargeVisit.VisitId,
                            chargeVisit.AreaName,
                            chargeVisit.VisitType,
                            chargeVisit.HospitalCode,
                            chargeVisit.DoctorAD,
                            chargeVisit.PatientLocationId,
                            chargeVisit.PatientVisitId,
                            chargeVisit.ActualVisitDate
                        }
                    });
            }
            if (type == "1")
            {
                var PathologyDto = unitOfWork.ChargeItemPathologyRepository.FirstOrDefault(e => e.Id == chargeItem.ChargeItemPathologyId && !e.IsDeleted);
                if (PathologyDto == null) return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);
                return Content(HttpStatusCode.OK, new
                {
                    PathologyDto,
                    type,
                    chargeItem.Id,
                    IsReadonly = !chargeItem.AllowEditWithSpecimenStatus || current_username != PathologyDto.CreatedBy,
                    User = new
                    {
                        Username = PathologyDto.CreatedBy,
                        GetUserByUsername(PathologyDto.CreatedBy)?.Fullname
                    },
                    Customer,
                    Site = getSiteByApiCode(chargeItem.HospitalCode),
                    data = new
                    {
                        charge.VisitId,
                        charge.Reason,
                        charge.Priority,
                        charge.Diagnosis,
                        charge.DoctorAD,
                        charge.PatientVisitId,
                        charge.PatientLocationId,
                        charge.VisitCode,
                        charge.VisitType,
                        charge.HospitalCode,
                        charge.Room,
                        charge.Bed,
                        Items = new List<ChargeItem>() { chargeItem }
                    },
                    ChargeData = new
                    {
                        chargeVisit.PatientLocationCode,
                        chargeVisit.VisitGroupType,
                        chargeVisit.VisitCode,
                        chargeVisit.VisitId,
                        chargeVisit.AreaName,
                        chargeVisit.VisitType,
                        chargeVisit.HospitalCode,
                        chargeVisit.DoctorAD,
                        chargeVisit.PatientLocationId,
                        chargeVisit.PatientVisitId,
                        chargeVisit.ActualVisitDate
                    }
                });
            }
            return Content(HttpStatusCode.BadRequest, Message.FORM_NOT_FOUND);
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/PathologyMicrobiology/Update/{type}/{id}")]
        [Permission(Code = "DRS0007")]
        public IHttpActionResult Update(int type, Guid id, [FromBody] PathologyMicrobiology chargeitem)
        {
            if (type == 0)
            {
                unitOfWorkDapper.ChargeItemMicrobiologyDtoRepository.Update(chargeitem.MicrobiologyDto);
                unitOfWork.Commit();
            }
            if (type == 1)
            {
                unitOfWorkDapper.ChargeItemPathologyDtoRepository.Update(chargeitem.PathologyDto);
                unitOfWork.Commit();
            }
            return Content(HttpStatusCode.OK, new { chargeitem.Id });
        }
        private async Task<ChargeItem> GetChargeItemAsync(Guid id)
        {
            var chargeItem = unitOfWork.ChargeItemRepository.Find(e => !e.IsDeleted && e.Id == id).FirstOrDefault();
            if (chargeItem == null) return null;
            var listUpdatedCharge = await Bussiness.HisService.OHAPIService.GetUpdateChargeByChargeDetailIdAsync(chargeItem.ChargeDetailId.ToString());
            if (listUpdatedCharge.Count > 0)
            {
                foreach (var u in listUpdatedCharge)
                {
                    if (chargeItem.ChargeDetailId == u.ChargeDetailId)
                    {
                        chargeItem.ChargeDetailId = u.ChargeDetailId;
                        chargeItem.PlacerOrderStatus = u.PlacerOrderStatus;
                        chargeItem.RadiologyScheduledStatus = u.RadiologyScheduledStatus;
                        chargeItem.PaymentStatus = u.PaymentStatus;
                        chargeItem.SpecimenStatus = u.SpecimenStatus;
                        chargeItem.FailedReason = null;
                        if (chargeItem.ChargeDetailId != null)
                        {
                            chargeItem.Status = Constant.ChargeItemStatus.Placed;
                        }
                        if (u.NewChargeId != null)
                        {
                            chargeItem.ChargeDetailId = u.NewChargeId;
                            if (u.ChargeDeletedDate != null)
                            {
                                chargeItem.Status = Constant.ChargeItemStatus.Cancelled;
                                chargeItem.CancelComment = "[Cancelled from EF]" + chargeItem.CancelComment;
                            }
                            else
                            {
                                chargeItem.Status = Constant.ChargeItemStatus.Placed;
                            }
                        }
                        else
                        {
                            if (u.ChargeDeletedDate != null)
                            {
                                chargeItem.Status = Constant.ChargeItemStatus.Cancelled;
                                chargeItem.CancelComment = "[Cancelled from EF]" + chargeItem.CancelComment;
                            }
                        }
                    }
                }
                unitOfWork.Commit();
            }
            return chargeItem;
        }
        private PathologyMicrobiology DataFormatted(PathologyMicrobiologyTemp item, string current_username)
        {
            var area = unitOfWork.ChargeRepository.Find(e => e.Id == item.ChargeId).FirstOrDefault();
            ChargeItemPathology pathology = null;
            ChargeItemMicrobiology microbiology = null;
            if (item.ItemType == 0)
            {
                microbiology = unitOfWork.ChargeItemMicrobiologyRepository.Find(e => e.Id == item.ChargeItemMicrobiologyId).FirstOrDefault();
            }
            if (item.ItemType == 1)
            {
                pathology = unitOfWork.ChargeItemPathologyRepository.Find(e => e.Id == item.ChargeItemPathologyId).FirstOrDefault();
            }
            var allowCancelSpecimenStatus = new List<string>() { SpecimenStatuses.Created, SpecimenStatuses.Deleted, SpecimenStatuses.Discarded, SpecimenStatuses.Rejected, SpecimenStatuses.MarkedForRecollection };
            return new PathologyMicrobiology()
            {
                Id = item.Id,
                ServiceCode = item.ServiceCode,
                ServiceViName = item.ServiceViName,
                ServiceEnName = item.ServiceEnName,
                ItemType = item.ItemType,
                VisitCode = item.VisitCode,
                Area = area?.ChargeVisit?.AreaName,
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
                Pathology = pathology,
                Microbiology = microbiology,
                SpecimenStatus = item.SpecimenStatus,
                IsReadonly = item.CreatedBy != current_username || (!string.IsNullOrEmpty(item.SpecimenStatus) && !allowCancelSpecimenStatus.Contains(item.SpecimenStatus))
            };
        }
    }
}