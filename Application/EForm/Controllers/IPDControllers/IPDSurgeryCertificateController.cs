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
    public class IPDSurgeryCertificateController : EIOProcedureSummaryController
    {
        //[HttpPost]
        //[CSRFCheck]
        //[Route("api/IPD/SurgeryCertificate/Create/{visitId}")]
        //[Permission(Code = "IPDSURCER01")]
        //public IHttpActionResult CreateProcedureSummaryAPI(Guid visitId)
        //{
        //    var ipd = GetIPD(visitId);
        //    if (ipd == null)
        //        return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

        //    CreateSurgeryCertificate(visitId, "IPD");
        //    return Content(HttpStatusCode.OK, Message.SUCCESS);
        //}

        //[HttpGet]
        //[Route("api/IPD/ListAllProcedureSummary/{visitId}")]
        //[Permission(Code = "IPDSURCER02")]

        [HttpGet]
        [Route("api/IPD/SurgeryCertificate/{visitId}")]
        [Permission(Code = "IPDSURCER03")]
        public IHttpActionResult GetDetailSurgeryCertificateAPIByVisit(Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            var IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.IPDSurgeryCertificate);
            var certificate = GetListSurgeryCertificateByVisitId(visitId);
            if (certificate == null || certificate.Count == 0)
                return Content(HttpStatusCode.NotFound, new
                {
                    Message.IPD_SURCER_NOT_FOUND,
                    IsLocked,
                });

            var form = certificate.OrderBy(c => c.CreatedAt).FirstOrDefault();
            if (form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    Message.IPD_SURCER_NOT_FOUND,
                    IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.IPDSurgeryCertificate),
                });
            if (form.FormId != null)
                return Content(HttpStatusCode.OK, new { Version = "2" });

            var doctor = form.ProcedureDoctor;
            var dean = form.Dean;
            var director = form.Director;
            var data = form.IPDSurgeryCertificateDatas.Where(e => !e.IsDeleted)
                .Select(e => new { e.Code, e.Value, e.EnValue }).OrderBy(e => e.Code).ToList();

            string ngayVaoVien = "";
            var first_ipd = GetFirstIpd(ipd);
            ngayVaoVien = first_ipd?.CurrentDate;
            var khoa = ipd.Specialty.ViName;
            Customer customerInfo = ipd.Customer ?? null;

            return Content(HttpStatusCode.OK, new
            {
                form.Id,
                CustomerInfo = customerInfo,
                Khoa = khoa,
                ProcedureDoctor = new { doctor?.Username, doctor?.Fullname, doctor?.DisplayName, doctor?.Title },
                ProcedureTime = form.ProcedureTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Dean = new { dean?.Username, dean?.Fullname, dean?.DisplayName, dean?.Title },
                DeanConfirmTime = form.DeanConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Director = new { director?.Username, director?.Fullname, director?.DisplayName, director?.Title },
                DirectorTime = form.DirectorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                NgayVaoVien = ngayVaoVien,
                NgayRaVien = ipd.DischargeDate != null ? Convert.ToDateTime(ipd.DischargeDate).ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) : null,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.IPDSurgeryCertificate),
                Datas = data,
                CreateAt = form.CreatedAt,
                Version = "1"
            });
        }

        [HttpGet]
        [Route("api/IPD/ListAllProcedureSummary/{visitId}")]
        [Permission(Code = "IPDSURCER02")]
        public IHttpActionResult GetListProcedureSummaryAPI(Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            bool IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.TomTatThuThuat);
            var procedures = GetListProcedureSummary(visitId, "IPD");
            return Content(HttpStatusCode.OK, procedures.Select(e => new
            {
                e.CreatedBy,
                e.CreatedAt,
                e.Id,
                ChanDoanTruocPhauThuat = e.EIOProcedureSummaryDatas.FirstOrDefault(item => item.Code == "EIOPS12")?.Value,
                ChanDoanSauPhauThuat = e.EIOProcedureSummaryDatas.FirstOrDefault(item => item.Code == "EIOPS10")?.Value,
                PhuongPhapPhauThuat = e.EIOProcedureSummaryDatas.FirstOrDefault(item => item.Code == "EIOPS18")?.Value,
                PhuongPhapVoCam = e.EIOProcedureSummaryDatas.FirstOrDefault(item => item.Code == "EIOPS20")?.Value,
                PhauThuanVien = e.EIOProcedureSummaryDatas.FirstOrDefault(item => item.Code == "EIOPS42")?.Value,
                BacSiGayMe = e.EIOProcedureSummaryDatas.FirstOrDefault(item => item.Code == "EIOPS02")?.Value,
            }).ToList());
        }

        [HttpGet]
        [Route("api/IPD/SurgeryCertificate/All/{visitId}")]
        [Permission(Code = "IPDSURCER03")]
        public IHttpActionResult GetAll(Guid visitId)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var forms = GetListSurgeryCertificateByVisitId(ipd.Id);
            foreach (var form in forms)
            {
                if (form.FormId == null)
                {
                    form.Version = "1";
                }
                else
                if (form != null && form.Version == null)
                {
                    form.Version = "2";
                }
                else
                {
                    form.Version = "3";
                }
            }
            var forms_result = forms.Select(e => new
            {
                e.Id,
                e.CreatedAt,
                e.CreatedBy,
                e.UpdatedAt,
                e.UpdatedBy,
                Version = e.Version
            }).OrderBy(c => c.CreatedAt).ToList();

            return Content(HttpStatusCode.OK, new
            {
                forms_result,
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.IPDSurgeryCertificate)
            });
        }

        [HttpGet]
        [Route("api/IPD/SurgeryCertificate/DetailById/{visitId}/{formId}")]
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

            var data = certificate.IPDSurgeryCertificateDatas.Where(e => !e.IsDeleted)
                .Select(e => new MappingData() { Code = e.Code, Value = e.Value, EnValue = e.EnValue }).OrderBy(e => e.Code).ToList();
            if (certificate.FormId != null)
            {
                Dictionary<string, string> codes = new Dictionary<string, string>()
                        {
                            {"IPDSURCER08", "EIOPS12"},
                            {"IPDSURCER10", "EIOPS60"},
                            {"IPDSURCER04", "EIOPS26"},
                            {"IPDSURCER22", "EIOPS10"},
                            {"IPDSURCER12", "EIOPS62"},
                            {"IPDSURCER14", "EIOPS18"},
                            {"IPDSURCER16", "EIOPS20"},
                            {"IPDSURCER18", "EIOPS42"},
                            {"IPDSURCER20", "EIOPS02"},

                     };

                Guid? procedureId = certificate.FormId;
                var procedure = GetProcedureSummary(procedureId);
                data = AutofillFromProcedure(data, procedure, codes);

                data = FormatString(data);
            }

            string ngayVaoVien = "";
            var first_visit = GetFirstIpd(ipd);
            if (first_visit.CurrentType == "OPD")
                ngayVaoVien = first_visit.TransferDate;

            ngayVaoVien = first_visit.CurrentDate;
            string ngayRaVien = "";

            //IPDInitialAssessmentForAdult initialAssessmentForAdult = ipd.IPDInitialAssessmentForAdult;
            //if (initialAssessmentForAdult != null)
            //{
            //    ICollection<IPDInitialAssessmentForAdultData> initialAssessmentForAdultDatas = initialAssessmentForAdult.IPDInitialAssessmentForAdultDatas;
            //    if (initialAssessmentForAdultDatas != null)
            //    {
            //        ngayVaoVien = initialAssessmentForAdultDatas.FirstOrDefault(e => e.Code == "IPDIAAUARTIANS")?.Value;
            //    }
            //}

            var medicalRecord = ipd.IPDMedicalRecord;
            //var medicalRecordPart2 = ipd.IPDMedicalRecord.IPDMedicalRecordPart2;
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

            //string ngayVaoVien = "";
            //var first_ipd = GetFirstIpd(ipd);
            //ngayVaoVien = first_ipd.CurrentDate;
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
                Translations = translations
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgeryCertificate/{formId}")]
        [Permission(Code = "IPDSURCER04")]
        public IHttpActionResult UpdateDetailProcedureSummaryAPI(Guid formId, [FromBody] JObject request)
        {
            IPDSurgeryCertificate certificate = GetSurgeryCertificateById(formId);
            //EIOProcedureSummary procedure = GetProcedureSummary(id);
            if (certificate == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_SURCER_NOT_FOUND);
            var ipd = GetIPD(certificate.VisitId.Value);
            var islock24h = IPDIsBlock(ipd, Constant.IPDFormCode.IPDGiayChungNhanPhauThuat, formId);
            if (islock24h)
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            if (certificate.ProcedureDoctorId != null)
                return Content(HttpStatusCode.NotFound, Message.OWNER_FORBIDDEN);

            HandleUpdateIPDSurgeryCertificate(certificate, request["Datas"]);

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/SurgeryCertificate/Confirm/{formId}")]
        [Permission(Code = "IPDSURCER05")]
        public IHttpActionResult ConfirmDetailIPDSurgeryCertificateAPI(Guid formId, [FromBody] JObject request)
        {
            IPDSurgeryCertificate certificate = GetSurgeryCertificateById(formId);
            if (certificate == null)
                return Content(HttpStatusCode.BadRequest, Message.IPD_SURCER_NOT_FOUND);
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var success = ConfirmSurgeryCertificate(certificate, user, kind);
            if (success)
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);
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
        private IPDSurgeryCertificate CreateSurgeryCertificate(Guid visit_id, string visit_type)
        {
            var certificate = new IPDSurgeryCertificate
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
            };
            unitOfWork.IPDSurgeryCertificateRepository.Add(certificate);
            unitOfWork.Commit();
            return certificate;
        }

        protected EIOProcedureSummary GetProcedureSummary(Guid? id)
        {
            return unitOfWork.EIOProcedureSummaryRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
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

        protected List<MappingData> AutofillFromProcedure(List<MappingData> datas, EIOProcedureSummary procedure, Dictionary<string, string> codes)
        {
            var codeKeys = codes.Keys.ToList();
            var codeValues = codes.Values.ToList();
            var procedureId = procedure?.Id;
            var dataProcedure = (from d in unitOfWork.EIOProcedureSummaryDataRepository.AsQueryable()
                                 where !d.IsDeleted && d.EIOProcedureSummaryId == procedureId
                                 && codeValues.Contains(d.Code)
                                 select d).ToList();

            foreach (var item in codeKeys)
            {
                var check = datas.FirstOrDefault(e => e.Code == item);
                if (check == null)
                {
                    MappingData _new = new MappingData()
                    {
                        Code = item,
                        Value = null,
                        EnValue = null
                    };
                    datas.Add(_new);
                }
            }

            var dataResult = (from d in datas
                              select new MappingData()
                              {
                                  Code = d.Code,
                                  Value = ChangeValue(d, dataProcedure, codes).Value,
                                  EnValue = ChangeValue(d, dataProcedure, codes).EnValue,
                              }).ToList();

            return dataResult;
        }

        protected MappingData ChangeValue(MappingData data, List<EIOProcedureSummaryData> dataProcedure, Dictionary<string, string> codes)
        {
            var codeKeys = codes.Keys.ToList();
            if (codeKeys.Contains(data.Code))
            {
                string key = data.Code;
                data.Value = dataProcedure.FirstOrDefault(e => e.Code == codes[key]) == null ? "" : dataProcedure.FirstOrDefault(e => e.Code == codes[key]).Value;
                data.EnValue = dataProcedure.FirstOrDefault(e => e.Code == codes[key]) == null ? "" : dataProcedure.FirstOrDefault(e => e.Code == codes[key]).EnValue;
            }
            return data;
        }

        protected IPDSurgeryCertificate GetSurgeryCertificateById(Guid id)
        {
            return unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
        }

        protected List<IPDSurgeryCertificate> GetListSurgeryCertificateByVisitId(Guid id)
        {
            return unitOfWork.IPDSurgeryCertificateRepository.AsQueryable().Where(e => !e.IsDeleted && e.VisitId == id).ToList();
        }

        protected void HandleUpdateIPDSurgeryCertificate(IPDSurgeryCertificate certificate, JToken request)
        {
            var certificate_datas = certificate.IPDSurgeryCertificateDatas.Where(i => !i.IsDeleted).ToList();
            foreach (var item in request)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;

                var value = item.Value<string>("Value");
                var data = GetOrCreateSurgeryCertificateData(certificate_datas, certificate.Id, code);
                if (data != null)
                    UpdateSurgeryCertificateData(data, value);
            }
            var user = GetUser();
            certificate.UpdatedBy = user.Username;
            unitOfWork.IPDSurgeryCertificateRepository.Update(certificate);
            unitOfWork.Commit();
        }

        private IPDSurgeryCertificateData GetOrCreateSurgeryCertificateData(List<IPDSurgeryCertificateData> list_data, Guid certificate_id, string code)
        {
            IPDSurgeryCertificateData data = list_data.FirstOrDefault(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && e.Code == code);
            if (data != null)
                return data;

            data = new IPDSurgeryCertificateData
            {
                IPDSurgeryCertificateId = certificate_id,
                Code = code,
            };
            unitOfWork.IPDSurgeryCertificateDataRepository.Add(data);
            return data;
        }

        private void UpdateSurgeryCertificateData(IPDSurgeryCertificateData data, string value)
        {
            data.Value = value;
            unitOfWork.IPDSurgeryCertificateDataRepository.Update(data);
        }

        private bool ConfirmSurgeryCertificate(IPDSurgeryCertificate certificate, User user, string kind)
        {
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (kind == "Surgoen" && positions.Contains("Doctor") && certificate.ProcedureDoctorId == null)
            {
                certificate.ProcedureDoctorId = user?.Id;
                certificate.ProcedureTime = DateTime.Now;
            }
            else if (kind == "Head Of Department" && positions.Contains("Head Of Department") && certificate.DeanId == null)
            {
                certificate.DeanId = user?.Id;
                certificate.DeanConfirmTime = DateTime.Now;
            }
            else if (kind == "Director" && positions.Contains("Director") && certificate.DirectorId == null)
            {
                certificate.DirectorId = user?.Id;
                certificate.DirectorTime = DateTime.Now;
            }
            else
                return false;

            unitOfWork.IPDSurgeryCertificateRepository.Update(certificate);
            unitOfWork.Commit();
            return true;
        }

    }
}