using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models.PrescriptionModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPatientOwnMedicationsChartControllerController : EIOPatientOwnMedicationsChartController
    {
        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/PatientOwnMedicationsChart/Create/{id}")]
        [Permission(Code = "IPOMC1")]
        public IHttpActionResult CreatePatientOwnMedicationsChartAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.PhieuGhiNhanSuDungThuocNguoiBenhMangVao))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            }
            var med_chart = GetEIOPatientOwnMedicationsChart(ipd.Id, "IPD");
            if (med_chart != null)
                return Content(HttpStatusCode.NotFound, Message.ED_POMC_EXIST);

            CreateEIOPatientOwnMedicationsChart(ipd.Id, "IPD");
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/PatientOwnMedicationsChart/{id}")]
        [Permission(Code = "IPOMC2")]
        public IHttpActionResult GetPatientOwnMedicationsChartAPI(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);            
            var med_chart = GetEIOPatientOwnMedicationsChart(ipd.Id, "IPD");
            // Chưa check med_chart != null => med_chart.Id toang
            bool IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangKiemAnToanPhauThuatThuThuat, med_chart?.Id);
            if (med_chart == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    ViMessage = "Phiếu ghi nhận sử dụng thuốc do người bệnh mang vào không tồn tại",
                    EnMessage = "Patient’s own medications chart is not found",
                    IsLocked
                });           
            return Content(HttpStatusCode.OK, GetPatientOwnMedicationsChartResult(med_chart, ipd.Customer, IsLocked));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/PatientOwnMedicationsChart/{id}")]
        [Permission(Code = "IPOMC3")]
        public IHttpActionResult UpdatePatientOwnMedicationsChartAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var med_chart = GetEIOPatientOwnMedicationsChart(ipd.Id, "IPD");
            if (med_chart == null)
                return Content(HttpStatusCode.NotFound, Message.ED_POMC_NOT_FOUND);
            var lock24h = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuGhiNhanSuDungThuocNguoiBenhMangVao, med_chart?.Id); 
            if (lock24h)            
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            if (med_chart.HeadOfDepartmentId != null || med_chart.HeadOfPharmacyId != null || med_chart.DoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);
            HandleUpdateOrCreatePatientOwnMedicationsChart(med_chart, request);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/PatientOwnMedicationsChart/Confirm/{id}")]
        [Permission(Code = "IPOMC4")]
        public IHttpActionResult ConfirmPatientOwnMedicationsChartAPI(Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);            
            var med_chart = GetEIOPatientOwnMedicationsChart(ipd.Id, "IPD");
            if (med_chart == null)
                return Content(HttpStatusCode.NotFound, Message.ED_POMC_NOT_FOUND);
            var lock24h = IPDIsBlock(ipd, Constant.IPDFormCode.PhieuGhiNhanSuDungThuocNguoiBenhMangVao, med_chart?.Id);
            if (lock24h)
                return Content(HttpStatusCode.BadRequest, Message.FORM_IS_LOCKED);
            var error = ConfirmPatientOwnMedicationsChart(med_chart, request);
            if (error != null)
                return Content(HttpStatusCode.NotFound, error);
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }
       
       
    }
}
