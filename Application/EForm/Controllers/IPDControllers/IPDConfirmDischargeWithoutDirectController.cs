using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDConfirmDischargeWithoutDirectController : BaseApiController
    {
        [CSRFCheck]
        [HttpGet]
        [Route("api/IPD/IPDConfirmDischargeWithoutDirect/{id}")]
        [Permission(Code = "IPDCDWD02")]
        public IHttpActionResult GetIPDConfirmDischargeWithoutDirect(Guid id)
        {
            var visit = GetIPD(id);
            
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            }

            IPDConfirmDischarge confirmDischarge = GetConfirmDischargeWithoutDirectByVisitId(visit.Id);

            Customer customer = visit.Customer;
            string doctorUserName = "";
            string doctorFullName = "";

            if (confirmDischarge == null || confirmDischarge.DoctorFullName == null)
            {
                if (visit.PrimaryDoctor != null)
                {
                    doctorUserName = visit.PrimaryDoctor.Username;
                }
            }
            else
            {
                doctorUserName = confirmDischarge.DoctorUsername;
            }
            
            if (doctorUserName != "")
            {
                var userFullname = unitOfWork.UserRepository.FirstOrDefault(user => user.Username == doctorUserName)?.Fullname;
                if (userFullname == null)
                {
                    var displayName = unitOfWork.UserRepository.FirstOrDefault(user => user.Username == doctorUserName)?.DisplayName;
                    if (displayName != null)
                    {
                        if (displayName != "" && displayName.Contains('(') && (displayName.Trim().IndexOf('(') != 0))
                        {

                            string name = displayName.Substring(0, displayName.IndexOf("("));
                            doctorFullName = name;
                        }
                        else
                        {
                            doctorFullName = displayName;
                        }
                    }
                }
                else
                {
                    doctorFullName = userFullname;
                }
            }    

            string reasonDischarge = "";

            if (confirmDischarge == null || confirmDischarge.ReasonDischarge == null)
            {
                reasonDischarge = GetReasonDischarge(id);
            }
            else
            {
                reasonDischarge = confirmDischarge.ReasonDischarge;
            }

            string customerName = "";
            if (confirmDischarge == null || confirmDischarge.CustomerName == null)
            {
                customerName = visit.Customer.Fullname;
            }
            else
            {
                customerName = confirmDischarge.CustomerName;
            }

            var medicalRecordDatas = GetMedicalRecordData(id);
            DateTime timeDischarge = DateTime.Today;
            if (medicalRecordDatas != null)
            {
                string ngayGioRaVien = medicalRecordDatas.FirstOrDefault(e => e.Code == "IPDMRPTCDRVANS")?.Value;
                if (ngayGioRaVien != null && ngayGioRaVien != "")
                {
                    timeDischarge = DateTime.ParseExact(ngayGioRaVien.Replace('/', '-'), "HH:mm dd-MM-yyyy", CultureInfo.InvariantCulture);
                }   
            }

            DiagnosisAndICDModel diagnosisInfo = GetVisitDiagnosisAndICD(id, "IPD", true);
            Specialty specialty = visit.Specialty;

            bool IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.GiayXacNhanRaVienKhongTheoChiDinh);


            return Content(HttpStatusCode.OK, new
            {
                Customer = customer,
                Room = visit.Room,
                DoctorUserName = doctorUserName,
                DischargeType = reasonDischarge,
                FormData = confirmDischarge,
                TimeDischarge = timeDischarge, 
                Dianosis = diagnosisInfo,
                Specialty = specialty,
                CustomerName = customerName,
                DoctorFullName = doctorFullName,
                IsLocked = IsLocked
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/IPDConfirmDischargeWithoutDirect/Update/{id}")]
        [Permission(Code = "IPDCDWD03")]
        public IHttpActionResult UpdateDischargeWithoutDirect(Guid id, JObject request)
        {
            var visit = GetIPD(id);
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);
            }

            if ((string)request["Room"] != null)
            {
                visit.Room = (string)request["Room"];
                visit.Customer.Fullname = (string)request["Customer"]["Fullname"];
                unitOfWork.IPDRepository.Update(visit);
                unitOfWork.Commit();
            }

            IPDConfirmDischarge confirmDischarge = GetConfirmDischargeWithoutDirectByVisitId(visit.Id);

            if (confirmDischarge == null)
            {
                confirmDischarge = new IPDConfirmDischarge();
                confirmDischarge.DoctorUsername = (string)request["DoctorUserName"];
                confirmDischarge.DoctorFullName = (string)request["DoctorFullName"];
                confirmDischarge.VisitId = visit.Id;
                confirmDischarge.IsSignToConfirm = (bool)request["FormData"]["IsSignToConfirm"];
                confirmDischarge.Witness = (string)request["FormData"]["Witness"];
                confirmDischarge.ReasonDischarge = (string)request["DischargeType"];
                confirmDischarge.CustomerName = (string)request["CustomerName"];
                confirmDischarge.ImageUrl = (string)request["FormData"]["ImageUrl"];
                unitOfWork.IPDConfirmDischargeWithoutDirectRepository.Add(confirmDischarge);
            }
            else
            {
                confirmDischarge.DoctorUsername = (string)request["DoctorUserName"];
                confirmDischarge.VisitId = visit.Id;
                confirmDischarge.IsSignToConfirm = (bool)request["FormData"]["IsSignToConfirm"];
                confirmDischarge.Witness = (string)request["FormData"]["Witness"];
                confirmDischarge.ReasonDischarge = (string)request["DischargeType"];
                confirmDischarge.CustomerName = (string)request["CustomerName"];
                confirmDischarge.DoctorFullName = (string)request["DoctorFullName"];
                confirmDischarge.ImageUrl = (string)request["FormData"]["ImageUrl"];
                unitOfWork.IPDConfirmDischargeWithoutDirectRepository.Update(confirmDischarge);
            }

            unitOfWork.Commit();
            
            return Content(HttpStatusCode.OK, Message.SUCCESS);

        }
    
        private IPDConfirmDischarge GetConfirmDischargeWithoutDirectByVisitId(Guid visitId)
        {
            IPDConfirmDischarge confirmDischargeWithoutDirect = unitOfWork.IPDConfirmDischargeWithoutDirectRepository.FirstOrDefault(e => e.VisitId == visitId);

            return confirmDischargeWithoutDirect;
        }

        private IPDConfirmDischarge CreateConfirmDischargeWithoutDirect(Guid visitId, bool isSignToConfirm, string doctorUserName)
        {
            IPDConfirmDischarge confirmDischargeWithoutDirect = new IPDConfirmDischarge();

            var visit = GetIPD(visitId);

            var customer = visit.Customer;
            var room = visit.Room;
            var witness = unitOfWork.IPDConfirmDischargeWithoutDirectRepository.FirstOrDefault(e => e.VisitId == visit.Id);

            DiagnosisAndICDModel diagnosisInfo = new DiagnosisAndICDModel();
            diagnosisInfo = GetVisitDiagnosisAndICD(visitId, "IPD", true);
            string timeDischarge = "";
            string reasonDischarge = "";
            
            var doctor = visit.PrimaryDoctor;
            var medicalRecordDatas = GetMedicalRecordData(visitId);
            reasonDischarge = GetReasonDischarge(visitId);

            if (medicalRecordDatas != null)
            {
                timeDischarge = medicalRecordDatas.FirstOrDefault(e => e.Code == "IPDMRPTCDRVANS")?.Value;
            }    
                

            confirmDischargeWithoutDirect.DoctorUsername = doctorUserName;
            confirmDischargeWithoutDirect.IsSignToConfirm = isSignToConfirm;
            confirmDischargeWithoutDirect.VisitId = visitId;

            return confirmDischargeWithoutDirect;
        }

        //Lấy lý do ra viện
        private string GetReasonDischarge(Guid id)
        {
            string reasonDischarge = "";
            var visit = GetIPD(id);
            var medical_record = visit.IPDMedicalRecord;
            if (medical_record != null)
            {
                var medicalRecordDatas = medical_record.IPDMedicalRecordDatas;
                if (medicalRecordDatas != null)
                {
                    
                    var hinhThucRaVien = medicalRecordDatas.FirstOrDefault(e => e.Code.Contains("IPDMRPTHTRV") && e.Value == "True");
                    if (hinhThucRaVien != null)
                    {
                        if (hinhThucRaVien.Code == "IPDMRPTHTRVXIV")
                        {
                            var lyDoXinVe = medicalRecordDatas.Where(e => e.Code.Contains("IPDMRPGRV")).ToList();
                            reasonDischarge = "Xin về";

                            foreach (var item in lyDoXinVe)
                            {
                                if (item.Value == "True" && item.Code == "IPDMRPGRV04")
                                {
                                    reasonDischarge += $": Khác - {lyDoXinVe.FirstOrDefault(e => e.Code == "IPDMRPGRV05").Value}";
                                    break;
                                }
                                else if (item.Value == "True" && item.Code == "IPDMRPGRV02")
                                {
                                    reasonDischarge += ": Chuyên môn";
                                    break;
                                }
                                else if (item.Value == "True" && item.Code == "IPDMRPGRV03")
                                {
                                    reasonDischarge += ": Kinh tế";
                                    break;
                                }

                            }
                        }
                        else if (hinhThucRaVien.Code == "IPDMRPTHTRVBOV")
                        {
                            reasonDischarge = "Bỏ về (trốn viện)";
                        }
                    }
                }
            }

            return reasonDischarge;
        }
    
        private List<IPDMedicalRecordData> GetMedicalRecordData(Guid id)
        {
            List<IPDMedicalRecordData> medicalRecordDatas = new List<IPDMedicalRecordData>();

            var visit = GetIPD(id);
            var medical_record = visit.IPDMedicalRecord;
            if (medical_record != null)
            {
                medicalRecordDatas = medical_record.IPDMedicalRecordDatas.ToList();
            }

            return medicalRecordDatas;
        }
    }
}
