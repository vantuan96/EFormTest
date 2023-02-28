using DataAccess.Models;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using static Antlr.Runtime.Tree.TreeWizard;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDGlamorganController : BaseApiController
    {
        private readonly string visit_type = "IPD";

        [HttpGet]
        [Route("api/IPD/Glamorgan/{type}/All")]
        [Permission(Code = "IPDGLAMORGANGET")]
        public IHttpActionResult GetGlamorgan([FromUri] IPDGlamorganParam request, string type = "A02_066_050919_VE")
        {
            var visit = GetVisit(request.VisitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            string lastUpdatedBy;
            DateTime? lastUpdatedAt;
            List<IPDGlamorgan> glamorgans = FillterSearch(request, visit.Id, out lastUpdatedBy, out lastUpdatedAt);
            int numberPage = 0;
            if (glamorgans.Count > 0)
            {
                int totalGlamorgan = glamorgans.Count;
                numberPage = totalGlamorgan % request.PageSize == 0 ? totalGlamorgan / request.PageSize : totalGlamorgan / request.PageSize + 1;
                glamorgans = glamorgans.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).OrderByDescending(x=>x.StartingAssessment).ToList();
            }
            var IsLocked = IPDIsBlock(visit, type);
            List<dynamic> dynamic_glamorgans = new List<dynamic>();
            foreach(var e in glamorgans)
            {
                bool islock = IPDIsBlock(visit, type, e.Id);
                dynamic_glamorgans.Add(new
                {
                    Id = e.Id,
                    CreatedAt = e.CreatedAt,
                    CreatedBy = e.CreatedBy,
                    DeletedAt = e.DeletedAt,
                    DeletedBy = e.DeletedBy,
                    Intervention = e.Intervention,
                    InterventionOther = e.InterventionOther,
                    IsDeleted = e.IsDeleted,
                    Level = e.Level,
                    Number = e.Number,
                    NurseConfirmAt = e.NurseConfirmAt,
                    NurseConfirmId = e.NurseConfirmId,
                    StartingAssessment = e.StartingAssessment,
                    Total = e.Total,
                    UpdatedAt = e.UpdatedAt,
                    UpdatedBy = e.UpdatedBy,
                    VisitId = e.VisitId,
                    IsLocked = islock,
                });
            }    
            
            return Content(HttpStatusCode.OK, new
            {
                Glamorgans = dynamic_glamorgans,
                Paging = new { numberPage, request.PageNumber },
                IsLocked,
                visit.Version,
                UpdatedAt = lastUpdatedAt,
                UpdatedBy = lastUpdatedBy
            });
        }

        [HttpGet]
        [Route("api/IPD/Glamorgan/{type}/{id}")]
        [Permission(Code = "IPDGLAMORGANGET")]
        public IHttpActionResult GetGlamorgan(Guid id, string type = "A02_066_050919_VE")
        {
            var form = GetForm(type, id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_GLAMORGAN_NOT_FOUND);            
            var visit = GetVisit((Guid)form.VisitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            return Content(HttpStatusCode.OK, FormatOutput(type, visit, form));
        }

        [HttpPost]
        [Route("api/IPD/Glamorgan/Create/{type}/{visitId}")]
        [Permission(Code = "IPDGLAMORGANPOST")]
        public IHttpActionResult CreateGlamorgan(Guid visitId, [FromBody] JObject request, string type = "A02_066_050919_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);

            var form_data = new IPDGlamorgan()
            {
                VisitId = visitId,
                Level = request["Level"].ToString(),
                Total = request["Total"].ToString(),
                Intervention = request["Intervention"].ToString(),
                InterventionOther = request["InterventionOther"].ToString(),
                StartingAssessment = string.IsNullOrEmpty(request["StartingAssessment"]?.ToString()) == true ? DateTime.ParseExact(DateTime.Now.ToString("HH:mm dd/MM/yyyy"), "HH:mm dd/MM/yyyy", new CultureInfo("en-US")) : DateTime.ParseExact(request["StartingAssessment"].ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US")),
                Number = unitOfWork.IPDGlamorganRepository.Find(x => x.VisitId == visitId).ToList().Count() + 1
            };
            unitOfWork.IPDGlamorganRepository.Add(form_data);
            unitOfWork.Commit();
            UpdateVisit(visit, visit_type);
            HandleUpdateOrCreateGlamorganData(form_data, request, type);
            var idForm = form_data.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, idForm);
            return Content(HttpStatusCode.OK, new
            {
                form_data.Id,
                form_data.VisitId,
                form_data.CreatedBy,
                form_data.CreatedAt
            });
        }

        [HttpPost]
        [Route("api/IPD/Glamorgan/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "IPDGLAMORGANPUT")]
        public IHttpActionResult UpdateGlamorgan(Guid visitId, Guid id, [FromBody] JObject request, string type = "A02_066_050919_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(type, id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));
            var IsLocked = IPDIsBlock(visit, type, id);
            if (IsLocked)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            form.Total = string.IsNullOrEmpty(request["Total"].ToString()) == true ? form.Total : request["Total"].ToString();
            form.Level = string.IsNullOrEmpty(request["Level"].ToString()) == true ? form.Level : request["Level"].ToString();
            form.StartingAssessment = string.IsNullOrEmpty(request["StartingAssessment"].ToString()) == true ? form.StartingAssessment : DateTime.ParseExact(request["StartingAssessment"].ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            form.Intervention = string.IsNullOrEmpty(request["Intervention"].ToString()) == true ? form.Intervention : request["Intervention"].ToString();
            form.InterventionOther = string.IsNullOrEmpty(request["InterventionOther"].ToString()) == true ? form.Intervention : request["InterventionOther"].ToString();
            if (form.NurseConfirmId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            unitOfWork.IPDGlamorganRepository.Update(form);
            HandleUpdateOrCreateGlamorganData(form, request, type);
            UpdateVisit(visit, visit_type);
            var formId = form.Id;
            CreateOrUpdateIPDIPDInitialAssessmentToByFormType(visit, type, formId);
            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                form.VisitId,
                form.UpdatedAt
            });
        }

        [HttpPost]
        [Route("api/IPD/Glamorgan/Confirm/{type}/{visitId}/{Id}")]
        //[Permission(Code = "IPDGLAMORGANCF")]
        public IHttpActionResult ConfirmAPI(Guid visitId, Guid Id, [FromBody] JObject request, string type = "A02_066_050919_VE")
        {
            var visit = GetVisit(visitId, visit_type);
            if (visit == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_NOT_FOUND);
            var form = GetForm(type, Id);
            if (form == null)
                return Content(HttpStatusCode.NotFound, NotFoundData(visit, type));
            var IsLocked = IPDIsBlock(visit, type, Id);
            if (IsLocked)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);
            var successConfirm = ConfirmUser(form, user, request["TypeConfirm"].ToString());
            if (successConfirm)
            {
                UpdateVisit(visit, visit_type);
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
            }
        }
        private IPDGlamorgan GetForm(string type, Guid visit_id)
        {
            return unitOfWork.IPDGlamorganRepository.Find(e => !e.IsDeleted && e.Id == visit_id).FirstOrDefault();
        }
        private dynamic FormatOutput(string formCode, IPD ipd, IPDGlamorgan fprm)
        {
            var NurseConfirm = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Id == fprm.NurseConfirmId);
            var FullNameCreate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.CreatedBy)?.Fullname;
            var FullNameUpdate = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.Username == fprm.UpdatedBy)?.Fullname;
            var datas = unitOfWork.IPDGlamorganDataRepository.Find(e =>
                    e.IsDeleted == false &&
                    e.IPDGlamorganId == fprm.Id &&
                    e.FormCode == formCode
            ).Select(f => new { Id = f.Id, Code = f.Code, Value = f.Value }).ToList();
            return new
            {
                ID = fprm.Id,
                VisitId = fprm.VisitId,
                Datas = datas,
                CreatedBy = fprm.CreatedBy,
                FullNameCreate = FullNameCreate,
                CreatedAt = fprm.CreatedAt,
                UpdateBy = fprm.UpdatedBy,
                FullNameUpdate = FullNameUpdate,
                UpdatedAt = fprm.UpdatedAt,
                IsLocked = IPDIsBlock(ipd, formCode, fprm.Id),
                Confirm = new
                {
                    Nurse = new
                    {
                        UserName = NurseConfirm?.Username,
                        FullName = NurseConfirm?.Fullname,
                        ConfirmAt = fprm.NurseConfirmAt,
                    }
                },
                InterventionOther = fprm.InterventionOther,
                fprm.StartingAssessment,
            };
        }
        private bool ConfirmUser(IPDGlamorgan ipdGlamorgan, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName.ToUpper());
            if (kind.ToUpper() == "NURSE" && positions.Contains("NURSE") && ipdGlamorgan.NurseConfirmId == null)
            {
                ipdGlamorgan.NurseConfirmAt = DateTime.Now;
                ipdGlamorgan.NurseConfirmId = user?.Id;
            }
            else
            {
                return false;
            }
            unitOfWork.IPDGlamorganRepository.Update(ipdGlamorgan);
            unitOfWork.Commit();
            return true;
        }

        protected void CreateGlamorganDatas(Guid formId, string formCode, JToken request)
        {
            foreach (var item in request)
            {
                var glamorgan = new IPDGlamorganData
                {
                    Code = item["Code"]?.ToString(),
                    Value = item["Value"]?.ToString(),
                    IPDGlamorganId = formId,
                    FormCode = formCode,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                unitOfWork.IPDGlamorganDataRepository.Add(glamorgan);
            }
            unitOfWork.Commit();
        }
        protected void UpdateGlamorganDatas(Guid formId, string formCode, JToken request)
        {
            foreach (var item in request)
            {
                var finded = unitOfWork.IPDGlamorganDataRepository.FirstOrDefault(e =>
                                                                                    !e.IsDeleted &&
                                                                                    e.FormCode == formCode &&
                                                                                    e.IPDGlamorganId == formId &&
                                                                                    e.Code == item["Code"].ToString());
                if (finded != null)
                {
                    finded.Value = item["Value"]?.ToString();
                    unitOfWork.IPDGlamorganDataRepository.Update(finded);
                }
            }
            unitOfWork.Commit();
        }
        private List<IPDGlamorgan> FillterSearch(IPDGlamorganParam reques, Guid visitId, out string lastUpdatedBy, out DateTime? lastUpdatedAt)
        {

            List<IPDGlamorgan> glamorgans = new List<IPDGlamorgan>();
            glamorgans = unitOfWork.IPDGlamorganRepository.Find(e => !e.IsDeleted &&
                                                                  e.VisitId == visitId).ToList();

            var last = glamorgans.OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            lastUpdatedBy = last?.UpdatedBy;
            lastUpdatedAt= last?.UpdatedAt;

            DateTime? fromDate = null;
            DateTime? toDate = null;
            string assessor = reques.Assessor;

            if (reques.FromDate != null)
            {
                fromDate = DateTime.ParseExact(reques.FromDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }

            if (reques.ToDate != null)
            {
                toDate = DateTime.ParseExact(reques.ToDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }

            if (fromDate != null)
                glamorgans = glamorgans.Where(x => x.StartingAssessment >= fromDate).ToList();

            if (toDate != null)
                glamorgans = glamorgans.Where(x => x.StartingAssessment <= toDate).ToList();

            if (assessor != null)
                glamorgans = glamorgans.Where(x => x.CreatedBy.ToUpper() == assessor.ToUpper()).ToList();

            return glamorgans.OrderByDescending(x => x.CreatedAt).ToList();
        }
        private void HandleUpdateOrCreateGlamorganData(IPDGlamorgan glamorgan, JObject request, string formCode)
        {
            var datas = unitOfWork.IPDGlamorganDataRepository.Find(e => !e.IsDeleted && e.IPDGlamorganId == glamorgan.Id).ToList();

            foreach (var item in request["Datas"])
            {
                var code = item["Code"]?.ToString();
                if (string.IsNullOrEmpty(code)) continue;
                var value = item["Value"]?.ToString();
                var data = GetOrCreateGlamorganData(code, value, glamorgan.Id, datas, formCode);
                if (data == null) continue;

                UpdateGlamorganDataData(item["Value"]?.ToString(), data);
            }
            unitOfWork.Commit();
        }
        private IPDGlamorganData GetOrCreateGlamorganData(string code, string value, Guid form_id, List<IPDGlamorganData> datas, string formCode)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data != null) return data;

            data = new IPDGlamorganData
            {
                FormCode = formCode,
                Code = code,
                Value = value,
                IPDGlamorganId = form_id
            };
            unitOfWork.IPDGlamorganDataRepository.Add(data);
            return data;
        }
        private void UpdateGlamorganDataData(string value, IPDGlamorganData data)
        {
            data.Value = value;
            unitOfWork.IPDGlamorganDataRepository.Update(data);
        }
        protected List<dynamic> GetGlamorganDataGData(Guid formId, string formCode)
        {
            var data = unitOfWork.IPDGlamorganDataRepository.Find(e =>
                    e.IsDeleted == false &&
                    e.IPDGlamorganId == formId &&
                    e.FormCode == formCode
            ).Select(f => new { Id = f.Id, Code = f.Code, Value = f.Value, FormCode = f.FormCode });
            return (List<dynamic>)data;
        }
        public class IPDGlamorganParam : PagingParameterModel
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Assessor { get; set; }
            public Guid VisitId { get; set; }
        }
    }
}