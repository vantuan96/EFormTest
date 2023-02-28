using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class IPDProcedureSummaryController : EIOProcedureSummaryController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/ProcedureSummary/Create/{id}")]
        [Permission(Code = "IPRSU1")]
        public IHttpActionResult CreateProcedureSummaryAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            CreateProcedureSummary(id, "IPD");
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        [HttpGet]
        [Route("api/IPD/ProcedureSummary/CheckFormLocked/{id}")]
        [Permission(Code = "IPRSU2")]
        public IHttpActionResult CheckFormLockedAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);     
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = IPDIsBlock(ipd, "A01_085_120522_VE")
            });
        }
        
        [HttpGet]
        [Route("api/IPD/ProcedureSummary/List/{id}")]
        [Permission(Code = "IPRSU3")]
        public IHttpActionResult GetListProcedureSummaryAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            bool IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.TomTatThuThuat);
            var procedures = GetListProcedureSummary(id, "IPD");
            if (procedures.Count > 0)
            {
                return Content(HttpStatusCode.OK, procedures.Select(e => new {
                    e.CreatedBy,
                    e.CreatedAt,
                    e.Id,
                    IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.TomTatThuThuat),
                    Version = e.Version,
                    e.EIOProcedureSummaryDatas,
                    UpdateAt = e.UpdatedAt,
                    UpdateBy = e.UpdatedBy
                }).ToList());
            }
            else
            {
                return Content(HttpStatusCode.OK, new
                {
                    IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.TomTatThuThuat),
                    Count = 0
                });
            }
        }
        
        [HttpGet]
        [Route("api/IPD/ProcedureSummary/{id}")]
        [Permission(Code = "IPRSU4")]
        public IHttpActionResult GetDetailProcedureSummaryAPI(Guid id)
        {
            EIOProcedureSummary procedure = GetProcedureSummary(id);
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Message.EIO_PRSU_NOT_FOUND);
            var ipd = GetIPD(procedure.VisitId.Value);
            var doctor = procedure.ProcedureDoctor;
            var head_of_dept = procedure.HeadOfDepartment;
            var director = procedure.Director;            
            var data = procedure.EIOProcedureSummaryDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Code, e.Value, e.EnValue }).OrderBy(e => e.Code).ToList();

            Guid surgeryCertificateId = Guid.Empty;
            try
            {
                surgeryCertificateId = unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => e.FormId == id).Id;
            }
            catch (Exception)
            {

            }

            if(surgeryCertificateId == null || surgeryCertificateId == Guid.Empty)
            {
                var form = CreateSurgeryByAPIUpdateProcedure(procedure);
                if (form != null)
                    surgeryCertificateId = form.Id;
            }

            return Content(HttpStatusCode.OK, new
            {
                procedure.Id,
                ProcedureDoctor = new { doctor?.Username, doctor?.Fullname, doctor?.DisplayName, doctor?.Title },
                ProcedureTime = procedure.ProcedureTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfDepartment = new { head_of_dept?.Username, head_of_dept?.Fullname, head_of_dept?.DisplayName, head_of_dept?.Title },
                HeadOfDepartmentTime = procedure.HeadOfDepartmentTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.DisplayName, director?.Title },
                DirectorTime = procedure.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.TomTatThuThuat, id),
                Datas = data,
                Version = procedure.Version,
                SurgeryCertificateId = surgeryCertificateId
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/ProcedureSummary/{id}")]
        [Permission(Code = "IPRSU5")]
        public IHttpActionResult UpdateDetailProcedureSummaryAPI(Guid id, [FromBody]JObject request)
        {
            EIOProcedureSummary procedure = GetProcedureSummary(id);
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Message.EIO_PRSU_NOT_FOUND);
            var ipd = GetIPD(procedure.VisitId.Value);
            var islock24h = IPDIsBlock(ipd, Constant.IPDFormCode.TomTatThuThuat, id);     
            if (islock24h)
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);           
           
            if (procedure.ProcedureDoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateProcedureSummary(procedure, request["Datas"]);
            UpdateSurgeryCertificateByProdure(procedure);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/ProcedureSummary/Confirm/{id}")]
        [Permission(Code = "IPRSU6")]
        public IHttpActionResult ConfirmDetailProcedureSummaryAPI(Guid id, [FromBody]JObject request)
        {
            EIOProcedureSummary procedure = GetProcedureSummary(id);
            if (procedure == null)
                return Content(HttpStatusCode.BadRequest, Message.EIO_PRSU_NOT_FOUND);            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);           
            var success = ConfirmProcedureSummary(procedure, user, kind);
            if (success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);            
            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
        }

        private IPDSurgeryCertificate CreateSurgeryByAPIUpdateProcedure(EIOProcedureSummary pro)
        {
            var sugery = (from f in unitOfWork.IPDSurgeryCertificateRepository.AsQueryable()
                          where !f.IsDeleted && f.VisitId == pro.VisitId
                          && f.VisitTypeGroupCode == pro.VisitTypeGroupCode
                          select f).FirstOrDefault();

            if (sugery != null)
                return null;

            var new_surgery = new IPDSurgeryCertificate()
            {
                VisitId = pro.VisitId,
                VisitTypeGroupCode = pro.VisitTypeGroupCode,
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

        private void UpdateSurgeryCertificateByProdure(EIOProcedureSummary pro)
        {
            var surgery = unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => !e.IsDeleted && e.FormId == pro.Id);
            if(surgery != null)
            {
                surgery.UpdatedAt = pro.UpdatedAt;
                surgery.UpdatedBy = pro.UpdatedBy;
                unitOfWork.IPDSurgeryCertificateRepository.Update(surgery, is_anonymous : true, is_time_change : false);
                unitOfWork.Commit();
            }
        }
    }
}
