using Bussiness.HisService;
using Common;
using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class SurgeryAndProcedureSummaryV3Controller : BaseApiController
    {
        private readonly string formCodeV4 = "A01_085_100822_VE";
        private readonly string formCodeV3 = "A01_085_120522_VE";

        protected IHttpActionResult GetListItemsByVisitId(Guid visitId, string visitType)
        {
            //var data = unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(e => e.VisitId == visitId && e.VisitType == visitType);
            
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    Message = "Không tồn tại",
                });
            }
            var listForms = unitOfWork.SurgeryAndProcedureSummaryV3Repository
                                                                    .Find(
                                                                        e => !e.IsDeleted &&
                                                                        e.VisitId != null &&
                                                                        e.VisitId == visitId &&
                                                                        e.VisitType == visitType
                                                                    ).OrderBy(x => x.CreatedAt)
                                                                    .Select(e => new ProcedureSumaryModel
                                                                    {
                                                                        Id = e.Id,
                                                                        VisitId = e.VisitId,
                                                                        CreateAt = e.CreatedAt,
                                                                        CreateBy = e.CreatedBy,
                                                                        UpdatedAt = e.UpdatedAt,
                                                                        VisitType = e.VisitType,
                                                                        TransactionDate = e.ProcedureTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                                                                        Version = e.Version,
                                                                    }).ToList();
            var dataV1 = unitOfWork.EIOProcedureSummaryRepository
                                                                   .Find(
                                                                   e => !e.IsDeleted &&
                                                                   e.VisitId == visitId &&
                                                                   e.VisitTypeGroupCode == visitType
                                                                   ).OrderBy(e => e.CreatedAt).ToList();
            var listFormsV1 = dataV1.Select(e => new ProcedureSumaryModel
            {

               EIOProcedureSummaryDatas =  e.EIOProcedureSummaryDatas,
                Id = e.Id,
                VisitId = e.VisitId,
                CreateAt = e.CreatedAt,
                CreateBy = e.CreatedBy,
                UpdatedAt = e.UpdatedAt,
                TransactionDate = e.ProcedureTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Version = e.Version

            }).ToList();          


            if (listFormsV1 != null)
            {
                listFormsV1.AddRange(listForms);

                return Content(HttpStatusCode.OK, new
                {
                    ListItems = listFormsV1

                });
            }else
            {
                return Content(HttpStatusCode.OK, new
                {
                    ListItems = listForms            
                });
            }


        }
        protected IHttpActionResult GetDetail(Guid visitId, Guid formId, string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            var IsLocked = false;            
            var form = unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);                
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            var ProcedureDoctorConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && form.ProcedureDoctorId != null && u.Id == form.ProcedureDoctorId);
            if (visitType == "IPD")
            {
              IsLocked = IPDIsBlock(visit, formCodeV3, formId);
            }
            if (visitType == "OPD")
            {
                var user = GetUser();
               IsLocked = Is24hLocked(visit.CreatedAt, visit.Id, "A01_085_120522_VE", user.Username, formId);
            }
            dynamic data = null;
            if (form.Version == "3")
            {
                 data = GetFormData(visitId, formId, formCodeV3);
            }
            if (form.Version == "4")
            {
                data = GetFormData(visitId, formId, formCodeV4);
            }            
            Guid surgeryCertificateId = Guid.Empty;
            if (visitType == "IPD")
            {
                var surgeryCertificate = unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => e.FormId == formId);
                if(surgeryCertificate != null)
                {
                    surgeryCertificateId = surgeryCertificate.Id;
                }    
                else
                {
                    var cer = CreateSurgeryByAPIUpdateProcedure(form);
                    if (form != null)
                        surgeryCertificateId = form.Id;
                }
            }
            return Content(HttpStatusCode.OK, new 
                {
                    formId,
                    Datas = data,
                    form.CreatedBy,
                    ProcedureTime = form.ProcedureTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    form.VisitType,
                    form.UpdatedBy,
                    UpdatedAt = form.UpdatedAt,
                    form.Version,
                    IsLocked,
                    DoctorConfirm = new
                    {
                        UserName = ProcedureDoctorConfirm?.Username,
                        FullName = ProcedureDoctorConfirm?.Fullname,
                    },
                    SurgeryCertificateId = surgeryCertificateId,
            });
            
        }
        protected IHttpActionResult CreateSurgeryAndProcedureSummaryV3(Guid visitId, string visitType)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tồn tại");
            }            
            var formData = new SurgeryAndProcedureSummaryV3
            {
                VisitId = visitId,
                VisitType = visitType,
                Version = "4"
            };
            unitOfWork.SurgeryAndProcedureSummaryV3Repository.Add(formData);
            unitOfWork.Commit();
            if(visitType == "IPD")
            {
                CreateSurgeryCertificateByProcedureSummary(formData);
            }           
            return Content(HttpStatusCode.OK, new { formData.Id , formData.Version });
        }
        protected IHttpActionResult Update(Guid visitId, Guid formId, String visitType, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tồn tại");
            }            
            var form = unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            if (request != null && request["Datas"] != null)
            {
                if(form.Version == "3")
                {
                    HandleUpdateOrCreateFormDatas(visitId, formId, formCodeV3, request["Datas"]);
                }
                if (form.Version == "4")
                {
                    HandleUpdateOrCreateFormDatas(visitId, formId, formCodeV4, request["Datas"]);
                }
                if (visitType == "IPD")
                {
                    UpdateSurgeryCertificateByProdure(form);                   
                }
               
                unitOfWork.SurgeryAndProcedureSummaryV3Repository.Update(form);
                unitOfWork.Commit();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Lỗi dữ liệu");
            }

            return Content(HttpStatusCode.OK, new { form.Id });
        }
        public IHttpActionResult ConfirmAPI(Guid visitId, Guid formId, String visitType, [FromBody] JObject request)
        {
            var visit = GetVisit(visitId, visitType);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, "Không tồn tại");

            var form = unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user);
            if (successConfirm)
            {
                UpdateVisit(visit, visitType);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        private bool ConfirmUser(SurgeryAndProcedureSummaryV3 form, User user)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (positions.Contains("DOCTOR") && form.ProcedureDoctorId == null)
            {
                form.ProcedureTime = DateTime.Now;
                form.ProcedureDoctorId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.SurgeryAndProcedureSummaryV3Repository.Update(form);
            unitOfWork.Commit();
            return true;
        }      
        private IPDSurgeryCertificate CreateSurgeryByAPIUpdateProcedure(SurgeryAndProcedureSummaryV3 pro)
        {
            var sugery = (from f in unitOfWork.IPDSurgeryCertificateRepository.AsQueryable()
                          where !f.IsDeleted && f.VisitId == pro.VisitId
                          && f.VisitTypeGroupCode == "IPD"
                          select f).FirstOrDefault();

            if (sugery != null)
                return null;

            var new_surgery = new IPDSurgeryCertificate()
            {
                VisitId = pro.VisitId,
                VisitTypeGroupCode = "IPD",
                FormId = pro.Id,
            };
            unitOfWork.IPDSurgeryCertificateRepository.Add(new_surgery);
            new_surgery.CreatedAt = pro.CreatedAt;
            new_surgery.CreatedBy = pro.CreatedBy;
            new_surgery.UpdatedAt = pro.UpdatedAt;
            new_surgery.UpdatedBy = pro.UpdatedBy;
            unitOfWork.IPDSurgeryCertificateRepository.Update(new_surgery, is_anonymous: true, is_time_change: false);
            unitOfWork.Commit();
            return new_surgery;
        }
        private void CreateSurgeryCertificateByProcedureSummary(SurgeryAndProcedureSummaryV3 procedure)
        {
            var checkNewForm = (from f in unitOfWork.IPDSurgeryCertificateRepository.AsQueryable()
                                where !f.IsDeleted && f.VisitId == procedure.VisitId
                                && f.VisitTypeGroupCode == "IPD"
                                && f.FormId == null
                                select f).FirstOrDefault();

            if (checkNewForm != null)
                return;

            var surgery = new IPDSurgeryCertificate()
            {
                VisitId = procedure.VisitId,
                VisitTypeGroupCode = procedure.VisitType,
                FormId = procedure.Id,
                Version = procedure.Version,
            };
            unitOfWork.IPDSurgeryCertificateRepository.Add(surgery);
            unitOfWork.Commit();
        }
        private void UpdateSurgeryCertificateByProdure(SurgeryAndProcedureSummaryV3 pro)
        {
            var surgery = unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => !e.IsDeleted && e.FormId == pro.Id);
            if (surgery != null)
            {
                surgery.UpdatedAt = pro.UpdatedAt;
                surgery.UpdatedBy = pro.UpdatedBy;
                unitOfWork.IPDSurgeryCertificateRepository.Update(surgery);
                unitOfWork.Commit();
            }
        }
        //kéo thông tin Ekip Mổ về tóm tắt phẫu thuật - 4 khu vực
        [HttpGet]
        [Route("api/IPD/SurgeryAndProcedureSummary/GetEkipFromOr/{type}/{visitId}")]
        //[Permission(Code = "IPDSAPSV3GL")]
        public async Task<IHttpActionResult> GetEkipFromOr(string type, Guid visitId)
        {
            var visit = GetVisit(visitId, type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Common.Message.VISIT_NOT_FOUND);
            Guid customerId = visit.CustomerId;
            var cus = unitOfWork.CustomerRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == customerId);
            if(cus == null || cus.PID == null) return Content(HttpStatusCode.NotFound, new { ViName = "Khách hàng không tồn tại" });
            var visitcode = visit.VisitCode;
            if(visitcode == null)
                return Content(HttpStatusCode.NotFound, new {ViName = "Chưa đồng bộ visitcode"});
            List<EkipFromOrModel> results_Or = await OHAPIService.SyncGetEkipFromOr(cus.PID, visitcode);
            results_Or = results_Or.Where(e => !e.IsDeleted).ToList();
            List<ListItemEkipFromOrModel> list_result = results_Or.Select(e => new ListItemEkipFromOrModel()
            {
                ThoiGianThucHien = e.ThoiGianThucHien.Value.ToString(Constants.DATE_FORMAT),
                ItemCode = e.ItemCode,
                ItemName = e.ItemName,
            }).ToList();
            foreach (var item in list_result)
            {
                item.ListItem = results_Or.Where(e => e.ItemCode == item.ItemCode).ToList();
            }
            return Content(HttpStatusCode.OK, list_result);
        }
    }
}
