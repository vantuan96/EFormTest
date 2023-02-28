using DataAccess.Models;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models;
using EForm.Models.IPDModels;
using EForm.Models.PrescriptionModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDSurgeryCertificateV3Controller : EIOProcedureSummaryController
    {
        [HttpGet]
        [Route("api/IPD/SurgeryCertificateV3/DetailById/{visitId}/{formId}")]
        [Permission(Code = "IPDSURCER03")]
        public IHttpActionResult GetDetailSurgeryCertificateAPI(Guid visitId, Guid formId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            IPDSurgeryCertificate certificate = GetSurgeryCertificateById(formId);
            if (certificate == null)
                return Content(HttpStatusCode.BadRequest, new
                {
                    Message.EIO_PRSU_NOT_FOUND,
                    IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.IPDSurgeryCertificate),
                });

            var doctor = certificate.ProcedureDoctor;
            var dean = certificate.Dean;
            var director = certificate.Director;

            var data = GenAutofillFromProcedure(certificate);


            string ngayVaoVien = "";
            var first_visit = GetFirstIpd(ipd);
            if (first_visit.CurrentType == "OPD")
                ngayVaoVien = first_visit.TransferDate;

            ngayVaoVien = first_visit.CurrentDate;
            string ngayRaVien = "";

            var medicalRecord = ipd.IPDMedicalRecord;
            switch (ipd.EDStatus.ViName)
            {
                case "Ra viện":
                    if (medicalRecord != null)
                    {
                        var medicalRecordDatas = medicalRecord.IPDMedicalRecordDatas;
                        if (medicalRecordDatas != null)
                        {
                            ngayRaVien = medicalRecordDatas.FirstOrDefault(e => e.Code == "IPDMRPTCDRVANS")?.Value;
                        }
                    }
                    break;
                case "Chuyển viện":
                    if (medicalRecord != null)
                    {
                        var medicalRecordDatas = medicalRecord.IPDMedicalRecordDatas;
                        if (medicalRecordDatas != null)
                        {
                            ngayRaVien = medicalRecordDatas.FirstOrDefault(e => e.Code == "IPDMRPTCHVHANS")?.Value;
                        }
                    }
                    break;
                case "Chuyển tuyến":
                    //IPDTD0ANS
                    if (medicalRecord != null)
                    {
                        var medicalRecordDatas = medicalRecord.IPDMedicalRecordDatas;
                        if (medicalRecordDatas != null)
                        {
                            ngayRaVien = medicalRecordDatas.FirstOrDefault(e => e.Code == "IPDTD0ANS")?.Value;
                        }
                    }
                    break;
                case "Tử vong":
                    //
                    if (medicalRecord != null)
                    {
                        var medicalRecordDatas = medicalRecord.IPDMedicalRecordDatas;
                        if (medicalRecordDatas != null)
                        {
                            ngayRaVien = medicalRecordDatas.FirstOrDefault(e => e.Code == "IPDMRPTNGTVANS")?.Value;
                        }
                    }
                    break;
                case "Chuyển khoa":
                    ngayRaVien = Convert.ToDateTime(ipd.DischargeDate).ToString("hh:mm dd/MM/yyyy");
                    break;
            }
            var khoa = ipd.Specialty.ViName;
            Customer customerInfo = ipd.Customer ?? null;
            var translation_util = new TranslationUtil(unitOfWork, ipd.Id, "IPD", "Surgery Certificate");
            var translations = translation_util.GetList(formId);
            return Content(HttpStatusCode.OK, new
            {
                certificate.Id,
                CustomerInfo = customerInfo,
                Khoa = khoa,
                ProcedureDoctor = new { doctor?.Username, doctor?.Fullname, doctor?.DisplayName, doctor?.Title },
                ProcedureTime = certificate.ProcedureTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Dean = new { dean?.Username, dean?.Fullname, dean?.DisplayName, dean?.Title },
                DeanConfirmTime = certificate.DeanConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.DisplayName, director?.Title },
                DirectorTime = certificate.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                NgayVaoVien = ngayVaoVien,
                NgayRaVien = ipd.DischargeDate != null ? Convert.ToDateTime(ipd.DischargeDate).ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) : null,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.IPDSurgeryCertificate),
                Datas = data,
                CreateAt = certificate.CreatedAt,
                FormId = certificate.FormId?.ToString(),
                Translations = translations
            });
        }

        private TransferInfoModel GetFirstIpd(IPD ipd)
        {
            var spec = ipd.Specialty;
            var current_doctor = ipd.PrimaryDoctor;

            var transfers = new IPDTransfer(ipd).GetListInfo();
            TransferInfoModel first_ipd = null;
            if (transfers.Count() > 0)
            {
                first_ipd = transfers.FirstOrDefault(e => e.CurrentType == "ED" || e.CurrentType == "IPD" && (string.IsNullOrEmpty(e.CurrentSpecialtyCode) || !e.CurrentSpecialtyCode.Contains("PTTT")));
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

        protected SurgeryAndProcedureSummaryV3 GetProcedureSummary(Guid? id)
        {
            return unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
        }




        protected IPDSurgeryCertificate GetSurgeryCertificateById(Guid id)
        {
            return unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
        }


    }
}