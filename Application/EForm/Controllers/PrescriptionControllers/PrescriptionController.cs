using Common;
using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.OPDModel;
using DataAccess.Models.PrescriptionModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EForm.Models.MedicationAdministrationRecordModels;
using EForm.Models.PrescriptionModels;
using EMRModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using static EForm.Common.Constant;

namespace EForm.Controllers.PrescriptionControllers
{
    [SessionAuthorize]
    public class PrescriptionController : BaseApiController
    {
        [HttpGet]
        [Route("api/Prescription/{pid}")]
        [CSRFCheck]
        [Permission(Code = "ALLPRE")]
        public IHttpActionResult GetAllPrescriptionsByPID(string pid, Nullable<DateTime> fromDate, Nullable<DateTime> toDate, int pageNumber, int pageSize)
        {
            if (fromDate != null)
            {
                fromDate = fromDate.Value.Date;
            }

            var cus = GetCustomerByPid(pid);
            if (cus == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);

            List<PrescriptionModel> prescriptions = GetPrescriptions(pid, fromDate, toDate);

            if (prescriptions != null)
            {
                if (cus.IsVip && !IsVIPMANAGE() && !IsUnlockVipByPid(pid, UnlockVipType.Prescription) && !HasEFORMViSitOpen(pid))
                {
                    return Content(HttpStatusCode.Forbidden, new MsgModel()
                    {
                        ViMessage = "Bạn không có quyền truy cập hồ sơ này",
                        EnMessage = "Bạn không có quyền truy cập hồ sơ này"
                    });
                }
                prescriptions = prescriptions.OrderByDescending(m => m.CreatedDate).ToList();
                prescriptions = prescriptions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return Content(HttpStatusCode.OK, prescriptions);
            }
            else
            {
                if (!IsVIPMANAGE() && cus.IsVip)
                {
                    return Content(HttpStatusCode.Forbidden, new MsgModel()
                    {
                        ViMessage = "Bạn không có quyền truy cập hồ sơ này",
                        EnMessage = "Bạn không có quyền truy cập hồ sơ này"
                    });
                }
                prescriptions = new List<PrescriptionModel>();
                return Content(HttpStatusCode.OK, prescriptions);
            }
        }

        [HttpGet]
        [Route("api/Prescription/Detail/{prescriptionId}/{visitType}")]
        [CSRFCheck]
        [Permission(Code = "DETAILPRE")]
        public IHttpActionResult GetDetailPrescription(string prescriptionId, [FromUri] HISCustomerParameterModel request)
        {
            List<DetailPharmacyModel> listPharmacies = new List<DetailPharmacyModel>();
            if (prescriptionId != null)
            {
                listPharmacies = GetPharmaciesOfPrescription(prescriptionId);
            }

            try
            {
                if (listPharmacies.Count > 0)
                {
                    string pid = listPharmacies[0].PID;
                    var cus = GetCustomerByPid(pid);
                    string visitCode = listPharmacies[0].VisitCode;

                    MedicationInfoModel medicationInfo = new MedicationInfoModel();

                    if (request.VisitType.Trim().ToUpper() == "VMHC")
                    {
                        medicationInfo = GetMedicalInfo(visitCode, listPharmacies[0].PrescriberAD, listPharmacies[0].LocationCode, request.VisitId, true, pid, cus.Id);
                    }
                    else
                    {
                        medicationInfo = GetMedicalInfo(visitCode, listPharmacies[0].PrescriberAD, listPharmacies[0].LocationCode, request.VisitId, false, pid, cus.Id);
                    }


                    medicationInfo.HospitalInfo = GetHospitalInfo(listPharmacies[0].HospitalCode);

                    List<PrescriptionNoteModel> listNotes = new List<PrescriptionNoteModel>();
                    var notes = unitOfWork.PrescriptionNoteRepository.Find(e => e.PrescriptionId.ToString() == prescriptionId);
                    if (notes != null)
                    {
                        listNotes = notes.ToList();
                    }

                    if (listNotes.Count == 0)
                    {
                        //var user = GetUser();

                        //if (user.Username.ToUpper() == listPharmacies[0].PrescriberAD.ToUpper())
                        //{

                        //}
                        var prescriptionTypes = listPharmacies.GroupBy(e => e.DType).ToList();
                        foreach (var item in prescriptionTypes)
                        {
                            listNotes.Add(new PrescriptionNoteModel
                            {
                                PrescriptionId = Guid.Parse(prescriptionId),
                                PrescriptionType = item.Key,
                                Note = medicationInfo.NoteOfDoctor
                            });
                        }
                    }

                    List<PresciptionRoundInfoModel> listRoundInfo = new List<PresciptionRoundInfoModel>();
                    var roundInfo = unitOfWork.PrescriptionRoundInfoRepository.Find(e => e.PrescriptionId.ToString() == prescriptionId);
                    if (roundInfo != null)
                    {
                        listRoundInfo = roundInfo.OrderByDescending(item => item.CreatedAt).ToList();
                    }
                    if (cus.IsVip && !IsVIPMANAGE() && !IsUnlockVipByPid(pid, UnlockVipType.Prescription) && !HasEFORMViSitOpen(pid))
                    {
                        return Content(HttpStatusCode.Forbidden, new MsgModel()
                        {
                            ViMessage = "Bạn không có quyền truy cập hồ sơ này",
                            EnMessage = "Bạn không có quyền truy cập hồ sơ này"
                        });
                    }
                    return Ok(new
                    {
                        ListPharmacies = listPharmacies,
                        Notes = listNotes,
                        MedicationInfo = medicationInfo,
                        RoundInfo = listRoundInfo
                    });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, Message.NOTI_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.ToString());
            }

        }

        [HttpPost]
        [Route("api/Prescription/Note/Create")]
        [CSRFCheck]
        [Permission(Code = "PRECRENOTE")]
        public IHttpActionResult CreateNoteForPrescription(JObject request)
        {
            if (request != null)
            {
                Guid prescriptionId = (Guid)request["PrescriptionId"];
                string precriptionType = (string)request["PrescriptionType"];
                var note = unitOfWork.PrescriptionNoteRepository.FirstOrDefault(e => e.PrescriptionId == prescriptionId && e.PrescriptionType == precriptionType);

                if (note == null)
                {
                    var newNote = new PrescriptionNoteModel
                    {
                        PrescriptionId = (Guid)request["PrescriptionId"],
                        PrescriptionType = (string)request["PrescriptionType"],
                        Note = (string)request["Note"]
                    };

                    unitOfWork.PrescriptionNoteRepository.Add(newNote);
                    unitOfWork.Commit();
                }
                else
                {
                    note.Note = (string)request["Note"];
                    unitOfWork.PrescriptionNoteRepository.Update(note);
                    unitOfWork.Commit();
                }

                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            return null;
        }

        [HttpPost]
        [Route("api/Prescription/RoundInfo/Create")]
        [CSRFCheck]
        [Permission(Code = "PRECREROUNDINFO")]
        public IHttpActionResult CreateRoundInfoForPrescription(JObject request)
        {
            if (request != null)
            {
                DateTime? fromDate = null;
                DateTime? toDate = null;
                int? round = null;
                fromDate = (request["FromDate"].ToString() != "" && request["FromDate"].ToString() != "Invalid date") ? DateTime.ParseExact(request["FromDate"].ToString(), "dd/MM/yyyy", null) : (DateTime?)null;

                toDate = (request["ToDate"].ToString() != "" && request["ToDate"].ToString() != "Invalid date") ? DateTime.ParseExact(request["ToDate"].ToString(), "dd/MM/yyyy", null) : (DateTime?)null;
                round = request["Round"].ToString() != "" ? (int)request["Round"] : (int?)null;

                Guid prescriptionId = (Guid)request["PrescriptionId"];
                string precriptionType = (string)request["PrescriptionType"];

                var roundInfo = unitOfWork.PrescriptionRoundInfoRepository.FirstOrDefault(e => e.PrescriptionId == prescriptionId && e.PrescriptionType == precriptionType);

                if (roundInfo == null)
                {
                    var newRoundInfo = new PresciptionRoundInfoModel
                    {
                        PrescriptionId = (Guid)request["PrescriptionId"],
                        PrescriptionType = (string)request["PrescriptionType"],
                        Round = round,
                        FromDate = fromDate,
                        ToDate = toDate
                    };

                    unitOfWork.PrescriptionRoundInfoRepository.Add(newRoundInfo);
                    unitOfWork.Commit();
                }
                else
                {
                    roundInfo.Round = round;
                    roundInfo.FromDate = fromDate;
                    roundInfo.ToDate = toDate;
                    unitOfWork.PrescriptionRoundInfoRepository.Update(roundInfo);
                    unitOfWork.Commit();
                }
                return Content(HttpStatusCode.OK, Message.SUCCESS);
            }
            return null;
        }

        private List<PrescriptionModel> GetPrescriptions(string pid, Nullable<DateTime> fromDate, Nullable<DateTime> toDate)
        {
            string stringFromDate = "";
            string stringToDate = "";
            if (fromDate != null && toDate != null)
            {
                DateTime newFromDate = (DateTime)fromDate;
                stringFromDate = newFromDate.ToString("yyyy-MM-dd");
                DateTime newToDate = (DateTime)toDate;
                stringToDate = newToDate.AddDays(1).ToString("yyyy-MM-dd");
            }
            string urlPostfix = "";

            if (fromDate != null && toDate != null)
            {
                urlPostfix = string.Format("/EMRVinmecCom/1.0.0/getPrescriptionByPID?PID={0}&From={1}&To={2}", pid, stringFromDate, stringToDate);
            }
            else if (fromDate == null && toDate == null)
            {
                urlPostfix = string.Format("/EMRVinmecCom/1.0.0/getPrescriptionByPID?PID={0}", pid);
            }


            try
            {
                var response = HISClient.RequestAPI(urlPostfix, "Entries", "Entry");
                if (response != null)
                {
                    List<PrescriptionModel> prescriptions = JsonConvert.DeserializeObject<List<PrescriptionModel>>(response.ToString());
                    List<PrescriptionModel> finalPrescriptions = new List<PrescriptionModel>();
                    var groupPrescription = prescriptions.GroupBy(e => e.PrescriptionId);
                    foreach (var prescription in groupPrescription)
                    {
                        if (prescription.ToList().Count > 1)
                        {
                            foreach (var item in prescription)
                            {
                                if (item.PrimaryDoctorAD.ToUpper() == prescriptions[0].PrescriberAD.ToUpper())
                                {
                                    finalPrescriptions.Add(item);
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in prescription)
                            {

                                finalPrescriptions.Add(item);

                            }
                        }
                    }
                    return finalPrescriptions;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<DetailPharmacyModel> GetPharmaciesOfPrescription(string prescriptionId)
        {
            string urlPostfix = string.Format("/EMRVinmecCom/1.0.0/getPrescriptionDetails?PrescriptionId={0}", prescriptionId);
            try
            {
                var response = HISClient.RequestAPI(urlPostfix, "Entries", "Entry");
                if (response != null)
                {
                    List<DetailPharmacyModel> listPharmacies = JsonConvert.DeserializeObject<List<DetailPharmacyModel>>(response.ToString());
                    foreach (var item in listPharmacies)
                    {
                        if (item.DType == "N")
                        {
                            string[] arrString = item.Quantity.Split(' ');
                            var strQuantity = NumberToText(Convert.ToDouble(arrString[0]));
                            item.Quantity = strQuantity + " " + arrString[1];
                        }
                    }
                    return listPharmacies;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                return null;
            }
        }

        private MedicationInfoModel GetMedicalInfo(string visitCode, string primaryDoctorUserName, string locationCode, Guid? visitId, bool isFromViHC, string PID, Guid? customerId)
        {
            MedicationInfoModel medicationInfo = new MedicationInfoModel();

            PrescriptionVisitModel visit = GetVisitByVisitCodeAndDoctor(visitCode, primaryDoctorUserName, locationCode, visitId, customerId);

            if (visit != null)
            {
                // Đi từ đơn thuốc ở menu bệnh nhân
                if (visitId != null)
                {
                    medicationInfo = GetMedicationInfoFromVisit(visit);
                }
                else // Đi từ Đơn thuốc ở menu chính
                {
                    if (isFromViHC)
                    {
                        string urlPostfix = string.Format("/EMRVinmecCom/1.0.0/VihcGetDauHieuSinhTon?PID={0}&VisitCode={1}", PID, visitCode);
                        try
                        {
                            var response = HISClient.RequestAsyncAPIDauHieuSinhTon(urlPostfix, "Result", "ICD10s");
                            if (response != null)
                            {
                                DataFromViHCModel.Root root = JsonConvert.DeserializeObject<DataFromViHCModel.Root>(response.ToString());

                                if (root.Result != null)
                                {
                                    if (visit.VisitCode == root.Result.VisitCode && primaryDoctorUserName.ToUpper() == root.Result.DoctorAD.ToUpper())
                                    {
                                        // Lấy thông tin chiều cao
                                        medicationInfo.CustomerMedicationInfo.Height = root.Result.Height;

                                        //Lấy thông tin cân nặng
                                        medicationInfo.CustomerMedicationInfo.Weight = root.Result.Weight;

                                        // Thông tin chẩn đoán
                                        List<DataFromViHCModel.ICD10> listICD10s = root.Result.ICD10s.ICD10;
                                        string chanDoan = "";
                                        if (listICD10s != null && listICD10s.Count > 0)
                                        {
                                            foreach (var item in listICD10s)
                                            {
                                                chanDoan += $"{item.NameVN} ({item.Code}), ";
                                            }

                                            medicationInfo.CustomerMedicationInfo.DiagnosisFromViHC = chanDoan.Remove(chanDoan.LastIndexOf(','), 1);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            return medicationInfo;
                        }
                    }
                    else
                    {
                        medicationInfo = GetMedicationInfoFromVisit(visit);
                    }
                }
            }
            else
            {
                if (isFromViHC)
                {
                    string urlPostfix = string.Format("/EMRVinmecCom/1.0.0/VihcGetDauHieuSinhTon?PID={0}&VisitCode={1}", PID, visitCode);
                    try
                    {
                        var response = HISClient.RequestAsyncAPIDauHieuSinhTon(urlPostfix, "Result", "");
                        if (response != null)
                        {
                            DataFromViHCModel.Root root = JsonConvert.DeserializeObject<DataFromViHCModel.Root>(response.ToString());

                            if (root.Result != null && root.Result.VisitCode == visitCode && (root.Result.DoctorAD.ToUpper() == primaryDoctorUserName.ToUpper() || root.Result.ICD10s.ICD10[0].DoctorAD.ToUpper() == primaryDoctorUserName.ToUpper()))
                            {
                                // Lấy thông tin chiều cao
                                medicationInfo.CustomerMedicationInfo.Height = root.Result.Height;

                                //Lấy thông tin cân nặng
                                medicationInfo.CustomerMedicationInfo.Weight = root.Result.Weight;

                                // Thông tin chẩn đoán
                                List<DataFromViHCModel.ICD10> listICD10s = root.Result.ICD10s.ICD10;
                                string chanDoan = "";
                                if (listICD10s != null && listICD10s.Count > 0)
                                {
                                    foreach (var item in listICD10s)
                                    {
                                        chanDoan += $"{item.NameVN} ({item.Code}), ";
                                    }

                                    medicationInfo.CustomerMedicationInfo.DiagnosisFromViHC = chanDoan.Remove(chanDoan.LastIndexOf(','), 1);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return medicationInfo;
                    }
                }
                else
                {
                    medicationInfo.VisitCodeMessage = "Vui lòng tiếp nhận lượt khám vào EFORM!";
                }

            }
            return medicationInfo;
        }

        private Site GetHospitalInfo(string hospitalCode)
        {
            Site site = new Site();

            List<Site> sites = unitOfWork.SiteRepository.Find(e => !e.IsDeleted).ToList();
            if (sites.Count > 0)
            {
                site = sites.FirstOrDefault(e => e.ApiCode == "HTC");
                if (site != null)
                {
                    site = sites.FirstOrDefault(e => e.ApiCode == "HTC");
                }

                site = sites.FirstOrDefault(e => e.ApiCode == hospitalCode);
            }
            return site;
        }

        private PrescriptionVisitModel GetVisitByVisitCodeAndDoctor(string visitCode, string doctorUserName, string locationCode, Guid? visitId, Guid? customerId)
        {
            PrescriptionVisitModel visit = new PrescriptionVisitModel();
            List<PrescriptionVisitModel> listVisits = new List<PrescriptionVisitModel>();
            List<PrescriptionVisitModel> listIpdVisits = new List<PrescriptionVisitModel>();
            List<PrescriptionVisitModel> listEdVisits = new List<PrescriptionVisitModel>();
            List<PrescriptionVisitModel> listEocVisits = new List<PrescriptionVisitModel>();
            List<PrescriptionVisitModel> listOpdVisits = new List<PrescriptionVisitModel>();

            if (visitId == null)
            {
                // Cập nhật bỏ check trùng BS chính ở IPD và ED
                listIpdVisits = unitOfWork.IPDRepository
                //.Find(e => !e.IsDeleted && e.VisitCode.Contains(visitCode) && e.PrimaryDoctorId != null && e.PrimaryDoctor.Username.ToUpper() == doctorUserName.ToUpper())
                .Find(e => !e.IsDeleted && e.VisitCode.Contains(visitCode) && e.PrimaryDoctorId != null && e.CustomerId == customerId)
                .Select(e => new PrescriptionVisitModel
                {
                    VisitType = "IPD",
                    VisitId = e.Id,
                    Specialty = e.Specialty,
                    Customer = e.Customer,
                    IsAllergy = e.IsAllergy,
                    KindOfAllergy = e.KindOfAllergy,
                    Allergy = e.Allergy,
                    HealthInsuranceNumber = e.HealthInsuranceNumber,
                    ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate,
                    IPDInitialAssessmentForAdult = e.IPDInitialAssessmentForAdult,
                    VisitCode = e.VisitCode
                }).ToList();

                // Cập nhật bỏ check trùng BS chính ở IPD và ED
                listEdVisits = unitOfWork.EDRepository
                    //.Find(e => !e.IsDeleted && e.VisitCode.Contains(visitCode) && e.PrimaryDoctorId != null && e.PrimaryDoctor.Username.ToUpper() == doctorUserName.ToUpper())
                    .Find(e => !e.IsDeleted && e.VisitCode.Contains(visitCode) && e.PrimaryDoctorId != null && e.CustomerId == customerId)
                    .Select(e => new PrescriptionVisitModel
                    {
                        VisitType = "ED",
                        VisitId = e.Id,
                        Specialty = e.Specialty,
                        Customer = e.Customer,
                        IsAllergy = e.IsAllergy,
                        KindOfAllergy = e.KindOfAllergy,
                        Allergy = e.Allergy,
                        HealthInsuranceNumber = e.HealthInsuranceNumber,
                        ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate,
                        EmergencyTriageRecord = e.EmergencyTriageRecord,
                        VisitCode = e.VisitCode
                    }).ToList();

                listOpdVisits = unitOfWork.OPDRepository
                    .Find(e => !e.IsDeleted && e.VisitCode.Contains(visitCode) && e.PrimaryDoctorId != null && e.PrimaryDoctor.Username.ToUpper() == doctorUserName.ToUpper() && e.CustomerId == customerId)
                    .Select(e => new PrescriptionVisitModel
                    {
                        VisitType = "OPD",
                        VisitId = e.Id,
                        Specialty = e.Specialty,
                        Customer = e.Customer,
                        IsAllergy = e.IsAllergy,
                        KindOfAllergy = e.KindOfAllergy,
                        Allergy = e.Allergy,
                        HealthInsuranceNumber = e.HealthInsuranceNumber,
                        ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate,
                        OPDInitialAssessmentForShortTerm = e.OPDInitialAssessmentForShortTerm,
                        VisitCode = e.VisitCode
                    }).ToList();

                listEocVisits = unitOfWork.EOCRepository
                    .Find(e => !e.IsDeleted && e.VisitCode.Contains(visitCode) && e.PrimaryDoctorId != null && e.PrimaryDoctor.Username.ToUpper() == doctorUserName.ToUpper() && e.CustomerId == customerId)
                    .Select(e => new PrescriptionVisitModel
                    {
                        VisitType = "EOC",
                        VisitId = e.Id,
                        Specialty = e.Specialty,
                        Customer = e.Customer,
                        IsAllergy = e.IsAllergy,
                        KindOfAllergy = e.KindOfAllergy,
                        Allergy = e.Allergy,
                        HealthInsuranceNumber = e.HealthInsuranceNumber,
                        ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate,
                        VisitCode = e.VisitCode
                    }).ToList();


                listVisits.AddRange(listIpdVisits);
                listVisits.AddRange(listEdVisits);
                listVisits.AddRange(listOpdVisits);
                listVisits.AddRange(listEocVisits);
            }
            else
            {
                listIpdVisits = unitOfWork.IPDRepository
                    .Find(e => !e.IsDeleted && e.Id == visitId)
                    .Select(e => new PrescriptionVisitModel
                    {
                        VisitType = "IPD",
                        VisitId = e.Id,
                        Specialty = e.Specialty,
                        Customer = e.Customer,
                        IsAllergy = e.IsAllergy,
                        KindOfAllergy = e.KindOfAllergy,
                        Allergy = e.Allergy,
                        HealthInsuranceNumber = e.HealthInsuranceNumber,
                        ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate,
                        IPDInitialAssessmentForAdult = e.IPDInitialAssessmentForAdult
                    }).ToList();
                listEdVisits = unitOfWork.EDRepository
                    .Find(e => !e.IsDeleted && e.Id == visitId)
                    .Select(e => new PrescriptionVisitModel
                    {
                        VisitType = "ED",
                        VisitId = e.Id,
                        Specialty = e.Specialty,
                        Customer = e.Customer,
                        IsAllergy = e.IsAllergy,
                        KindOfAllergy = e.KindOfAllergy,
                        Allergy = e.Allergy,
                        HealthInsuranceNumber = e.HealthInsuranceNumber,
                        ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate,
                        EmergencyTriageRecord = e.EmergencyTriageRecord
                    }).ToList();
                listOpdVisits = unitOfWork.OPDRepository
                    .Find(e => !e.IsDeleted && e.Id == visitId)
                    .Select(e => new PrescriptionVisitModel
                    {
                        VisitType = "OPD",
                        VisitId = e.Id,
                        Specialty = e.Specialty,
                        Customer = e.Customer,
                        IsAllergy = e.IsAllergy,
                        KindOfAllergy = e.KindOfAllergy,
                        Allergy = e.Allergy,
                        HealthInsuranceNumber = e.HealthInsuranceNumber,
                        ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate,
                        OPDInitialAssessmentForShortTerm = e.OPDInitialAssessmentForShortTerm
                    }).ToList();
                listEocVisits = unitOfWork.EOCRepository
                    .Find(e => !e.IsDeleted && e.Id == visitId)
                    .Select(e => new PrescriptionVisitModel
                    {
                        VisitType = "EOC",
                        VisitId = e.Id,
                        Specialty = e.Specialty,
                        Customer = e.Customer,
                        IsAllergy = e.IsAllergy,
                        KindOfAllergy = e.KindOfAllergy,
                        Allergy = e.Allergy,
                        HealthInsuranceNumber = e.HealthInsuranceNumber,
                        ExpireHealthInsuranceDate = e.ExpireHealthInsuranceDate
                    }).ToList();

                listVisits.AddRange(listIpdVisits);
                listVisits.AddRange(listEdVisits);
                listVisits.AddRange(listOpdVisits);
                listVisits.AddRange(listEocVisits);
            }



            if (listVisits.Count == 1)
            {
                visit = listVisits.First();
            }
            else if (listVisits.Count > 1)
            {
                try
                {
                    visit = listVisits.FirstOrDefault(e => e.Specialty.LocationCode.Contains(locationCode) && e.VisitCode == visitCode);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                visit = null;
            }

            return visit;
        }

        private string GenerateAllergyString(string kindOfAllergy, string allergy)
        {
            string diUng = "";
            if (kindOfAllergy.Contains(','))
            {
                string[] allergyTypes = kindOfAllergy.Split(',');
                for (int i = 0; i < allergyTypes.Length; i++)
                {
                    switch (allergyTypes[i])
                    {
                        case "1":
                            diUng += "Thực phẩm/ Food, ";
                            break;
                        case "2":
                            diUng += "Thời tiết/ Weather, ";
                            break;
                        case "3":
                            diUng += "Thuốc/ Medicine, ";
                            break;
                        case "4":
                            diUng += "Khác/ Other, ";
                            break;
                    }
                }

                diUng = diUng.Remove(diUng.LastIndexOf(','), 1);
            }
            else
            {
                switch (kindOfAllergy)
                {
                    case "1":
                        diUng = "Thực phẩm/ Food";
                        break;
                    case "2":
                        diUng = "Thời tiết/ Weather";
                        break;
                    case "3":
                        diUng = "Thuốc/ Medicine";
                        break;
                    case "4":
                        diUng = "Khác/ Other";
                        break;
                }
            }

            diUng += $" - {allergy}";
            return diUng;
        }

        private string NumberToText(double inputNumber)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return FirstLetterToUpper(result); ;
        }

        private string FirstLetterToUpper(string str)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }

            return str.ToUpper();
        }

        private EOCOutpatientExaminationNote GetForm(Guid VisitId)
        {
            var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }

        //Lấy thông tin chiều cao, cân nặng, dị ứng của OPD - Đánh giá ban đầu DV lẻ
        private MedicationInfoModel GetMedicalInfoOPDDVLe(EIOAssessmentForRetailServicePatient danhGiaBanDauDVLe, PrescriptionVisitModel visit)
        {
            MedicationInfoModel medicationInfo = new MedicationInfoModel();

            if (danhGiaBanDauDVLe.EIOAssessmentForRetailServicePatientDatas != null && danhGiaBanDauDVLe.EIOAssessmentForRetailServicePatientDatas.Count > 0)
            {
                var height = danhGiaBanDauDVLe.EIOAssessmentForRetailServicePatientDatas.FirstOrDefault(e => e.Code == "EDAFRSPHEIANS");
                if (height != null)
                {
                    medicationInfo.CustomerMedicationInfo.Height = height?.Value;
                }
                else
                {
                    medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                    medicationInfo.CustomerMedicationInfo.Height = "";
                }

                var weight = danhGiaBanDauDVLe.EIOAssessmentForRetailServicePatientDatas.FirstOrDefault(e => e.Code == "EDAFRSPWEIANS");
                if (weight != null)
                {
                    medicationInfo.CustomerMedicationInfo.Weight = weight?.Value;
                }
                else
                {
                    medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                    medicationInfo.CustomerMedicationInfo.Weight = "";
                }

                // Dị ứng
                var iaDatas = danhGiaBanDauDVLe.EIOAssessmentForRetailServicePatientDatas;
                if (iaDatas != null && iaDatas.Count > 0)
                {
                    if (iaDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLNPA")?.Value.ToUpper() == "TRUE")
                    {
                        medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định/ N/A";
                    }
                    else if (iaDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLNOO")?.Value.ToUpper() == "TRUE")
                    {
                        medicationInfo.CustomerMedicationInfo.Allergy = "Không/ No";
                    }
                    else if (iaDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLYES")?.Value.ToUpper() == "TRUE")
                    {
                        medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(iaDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLKOA")?.Value, iaDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLANS")?.Value);
                    }
                    else
                    {
                        medicationInfo.CustomerMedicationInfo.Allergy = "";
                        medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                    }
                }
            }
            else
            {
                medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
            }

            return medicationInfo;
        }

        //Lấy thông tin chiều cao, cân nặng, dị ứng của OPD - Đánh giá ban đầu Telehealth
        private MedicationInfoModel GetMedicalInfoOPDTelehealth(OPDInitialAssessmentForTelehealth danhGiaBanDauTelehealth, PrescriptionVisitModel visit)
        {
            MedicationInfoModel medicationInfo = new MedicationInfoModel();

            if (danhGiaBanDauTelehealth.OPDInitialAssessmentForTelehealthDatas != null && danhGiaBanDauTelehealth.OPDInitialAssessmentForTelehealthDatas.Count > 0)
            {
                var height = danhGiaBanDauTelehealth.OPDInitialAssessmentForTelehealthDatas.FirstOrDefault(e => e.Code == "OPDIAFTPHEIANS");
                if (height != null)
                {
                    medicationInfo.CustomerMedicationInfo.Height = height?.Value;
                }

                var weight = danhGiaBanDauTelehealth.OPDInitialAssessmentForTelehealthDatas.FirstOrDefault(e => e.Code == "OPDIAFTPWEIANS");
                if (weight != null)
                {
                    medicationInfo.CustomerMedicationInfo.Weight = weight?.Value;
                }
            }

            var iaDatas = danhGiaBanDauTelehealth.OPDInitialAssessmentForTelehealthDatas;
            if (iaDatas != null && iaDatas.Count > 0)
            {
                if (iaDatas.FirstOrDefault(e => e.Code == "OPDIAFTPALLNPA")?.Value.ToUpper() == "TRUE")
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định/ N/A";
                }
                else if (iaDatas.FirstOrDefault(e => e.Code == "OPDIAFTPALLNOO")?.Value.ToUpper() == "TRUE")
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = "Không/ No";
                }
                else if (iaDatas.FirstOrDefault(e => e.Code == "OPDIAFTPALLYES")?.Value.ToUpper() == "TRUE")
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(iaDatas.FirstOrDefault(e => e.Code == "OPDIAFTPALLKOA")?.Value, iaDatas.FirstOrDefault(e => e.Code == "OPDIAFTPALLANS")?.Value);
                }
                else
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = "";
                    medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                }
            }

            return medicationInfo;
        }

        //Lấy thông tin chiều cao, cân nặng, dị ứng của OPD - Đánh giá ban đầu thông thường
        private MedicationInfoModel GetMedicalInfoOPDThongThuong(OPDInitialAssessmentForShortTerm danhGiaBanDauThongThuong, PrescriptionVisitModel visit)
        {
            MedicationInfoModel medicationInfo = new MedicationInfoModel();
            // Chiều cao OPDIAFSTOPHEIANS
            if (danhGiaBanDauThongThuong.OPDInitialAssessmentForShortTermDatas != null && danhGiaBanDauThongThuong.OPDInitialAssessmentForShortTermDatas.Count > 0)
            {
                var heightOPD = danhGiaBanDauThongThuong.OPDInitialAssessmentForShortTermDatas.FirstOrDefault(e => e.Code == "OPDIAFSTOPHEIANS");
                if (heightOPD != null)
                {
                    medicationInfo.CustomerMedicationInfo.Height = heightOPD?.Value;
                }
                else
                {
                    medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                    medicationInfo.CustomerMedicationInfo.Height = "";
                }

                // Cân nặng OPDIAFSTOPWEIANS
                var weightOPD = danhGiaBanDauThongThuong.OPDInitialAssessmentForShortTermDatas.FirstOrDefault(e => e.Code == "OPDIAFSTOPWEIANS");
                if (weightOPD != null)
                {
                    medicationInfo.CustomerMedicationInfo.Weight = weightOPD?.Value;
                }
                else
                {
                    medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                    medicationInfo.CustomerMedicationInfo.Weight = "";
                }

                // Dị ứng
                if (visit.IsAllergy == true)
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(visit.KindOfAllergy, visit.Allergy);
                }
                else
                {
                    var iaDatas = danhGiaBanDauThongThuong.OPDInitialAssessmentForShortTermDatas;
                    if (iaDatas != null)
                    {
                        if (iaDatas.FirstOrDefault(e => e.Code == "OPDIAFSTOPALLNPA")?.Value.ToUpper() == "TRUE")
                        {
                            medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định/ N/A";
                        }
                        else if (iaDatas.FirstOrDefault(e => e.Code == "OPDIAFSTOPALLNOO")?.Value.ToUpper() == "TRUE")
                        {
                            medicationInfo.CustomerMedicationInfo.Allergy = "Không/ No";
                        }
                        else
                        {
                            medicationInfo.CustomerMedicationInfo.Allergy = "";
                            medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                        }
                    }
                }
            }
            else
            {
                medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
            }

            return medicationInfo;
        }

        //Lấy thông tin chiều cao, cân nặng, dị ứng của ED - Phân loại cấp cứu
        private MedicationInfoModel GetMedicalInfoEDPhanLoaiCapCuu(EDEmergencyTriageRecord phanLoaiCapCuu, PrescriptionVisitModel visit)
        {
            MedicationInfoModel medicationInfo = new MedicationInfoModel();

            if (phanLoaiCapCuu.EmergencyTriageRecordDatas != null && phanLoaiCapCuu.EmergencyTriageRecordDatas.Count > 0)
            {
                var emergencyTriageRecordDatas = phanLoaiCapCuu.EmergencyTriageRecordDatas;
                if (emergencyTriageRecordDatas != null && emergencyTriageRecordDatas.Count > 0)
                {
                    // Chiều cao ETRHEIANS
                    var heightED = emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "ETRHEIANS");
                    if (heightED != null)
                    {
                        medicationInfo.CustomerMedicationInfo.Height = heightED?.Value;
                    }
                    else
                    {
                        medicationInfo.CustomerMedicationInfo.Height = "";
                        medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Phân loại cấp cứu.";
                    }

                    // Cân nặng ETRWEIANS
                    var weightED = emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "ETRWEIANS");
                    if (weightED != null)
                    {
                        medicationInfo.CustomerMedicationInfo.Weight = weightED?.Value;
                    }
                    else
                    {
                        medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Phân loại cấp cứu.";
                        medicationInfo.CustomerMedicationInfo.Weight = "";
                    }

                    // Dị ứng
                    if (visit.IsAllergy == true)
                    {
                        medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(visit.KindOfAllergy, visit.Allergy);
                    }
                    else
                    {
                        if (emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "ETRALLNPA")?.Value.ToUpper() == "TRUE")
                        {
                            medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định/ N/A";
                        }
                        else if (emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "ETRALLNO")?.Value.ToUpper() == "TRUE")
                        {
                            medicationInfo.CustomerMedicationInfo.Allergy = "Không/ No";
                        }
                        else
                        {
                            medicationInfo.CustomerMedicationInfo.Allergy = "";
                            medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Phân loại cấp cứu.";
                        }
                    }
                }
                else
                {
                    medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Phân loại cấp cứu.";
                    medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Phân loại cấp cứu.";
                    medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Phân loại cấp cứu.";
                }
            }
            else
            {
                medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Phân loại cấp cứu.";
                medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Phân loại cấp cứu.";
                medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Phân loại cấp cứu.";
            }

            return medicationInfo;
        }

        //Lấy thông tin chiều cao, cân nặng, dị ứng của ED - Dịch vụ lẻ EDAssessmentForRetailServicePatient
        private MedicationInfoModel GetMedicalInfoEDDVLe(EIOAssessmentForRetailServicePatient danhGiaBanDauDVLeED, PrescriptionVisitModel visit)
        {
            MedicationInfoModel medicationInfo = new MedicationInfoModel();

            var emergencyTriageRecordDatas = danhGiaBanDauDVLeED.EIOAssessmentForRetailServicePatientDatas;
            if (emergencyTriageRecordDatas != null && emergencyTriageRecordDatas.Count > 0)
            {
                // Chiều cao ETRHEIANS
                var heightED = emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "EDAFRSPHEIANS");
                if (heightED != null)
                {
                    medicationInfo.CustomerMedicationInfo.Height = heightED?.Value;
                }
                else
                {
                    medicationInfo.CustomerMedicationInfo.Height = "";
                    medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Phân loại cấp cứu.";
                }

                // Cân nặng ETRWEIANS
                var weightED = emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "EDAFRSPWEIANS");
                if (weightED != null)
                {
                    medicationInfo.CustomerMedicationInfo.Weight = weightED?.Value;
                }
                else
                {
                    medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Phân loại cấp cứu.";
                    medicationInfo.CustomerMedicationInfo.Weight = "";
                }

                // Dị ứng
                if (emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLNPA")?.Value.ToUpper() == "TRUE")
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định/ N/A";
                }
                else if (emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLNOO")?.Value.ToUpper() == "TRUE")
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = "Không/ No";
                }
                else if (emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLYES")?.Value.ToUpper() == "TRUE")
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLKOA")?.Value, emergencyTriageRecordDatas.FirstOrDefault(e => e.Code == "EDAFRSPALLANS")?.Value);
                }
                else
                {
                    medicationInfo.CustomerMedicationInfo.Allergy = "";
                    medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Phân loại cấp cứu.";
                }
            }
            else
            {
                medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Phân loại cấp cứu.";
                medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Phân loại cấp cứu.";
                medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Phân loại cấp cứu.";
            }

            return medicationInfo;
        }
        private MedicationInfoModel GetMedicationInfoFromVisit(PrescriptionVisitModel visit)
        {
            MedicationInfoModel medicationInfo = new MedicationInfoModel();

            #region Lấy thông tin chiều cao, cân nặng, dị ứng và lời dặn của bs

            switch (visit.VisitType)
            {
                #region IPD
                case "IPD":
                    if (visit.IPDInitialAssessmentForAdult != null)
                    {
                        var ia = visit.IPDInitialAssessmentForAdult;
                        if (ia != null && ia.IPDInitialAssessmentForAdultDatas != null && ia.IPDInitialAssessmentForAdultDatas.Count > 0)
                        {
                            //Chiều cao IPDIAAUHEIGANS
                            var heightIPD = ia.IPDInitialAssessmentForAdultDatas.FirstOrDefault(e => e.Code == "IPDIAAUHEIGANS");
                            if (heightIPD != null)
                            {
                                medicationInfo.CustomerMedicationInfo.Height = heightIPD?.Value;
                            }
                            else
                            {
                                medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                                medicationInfo.CustomerMedicationInfo.Height = "";
                            }

                            // Cân nặng IPDIAAUWEIGANS
                            var weightIPD = ia.IPDInitialAssessmentForAdultDatas.FirstOrDefault(e => e.Code == "IPDIAAUWEIGANS");
                            if (weightIPD != null)
                            {
                                medicationInfo.CustomerMedicationInfo.Weight = weightIPD?.Value;
                            }
                            else
                            {
                                medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                                medicationInfo.CustomerMedicationInfo.Weight = "";
                            }

                            //Dị ứng
                            if (visit.IsAllergy == true)
                            {
                                medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(visit.KindOfAllergy, visit.Allergy);
                            }
                            else
                            {
                                if (ia.IPDInitialAssessmentForAdultDatas.FirstOrDefault(e => e.Code == "IPDIAAUALLENOO")?.Value.ToUpper() == "TRUE")
                                {
                                    medicationInfo.CustomerMedicationInfo.Allergy = "Không/ No";
                                }
                                else if (ia.IPDInitialAssessmentForAdultDatas.FirstOrDefault(e => e.Code == "IPDIAAUALLENPA")?.Value.ToUpper() == "TRUE")
                                {
                                    medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định/ N/A";
                                }
                                else
                                {
                                    medicationInfo.CustomerMedicationInfo.Allergy = "";
                                    medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                                }
                            }
                        }
                        else
                        {
                            medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                            medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                            medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                        }
                    }
                    else
                    {
                        medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                        medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                        medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                    }

                    #region Lấy lời dặn của bác sĩ
                    var ipd = GetIPD(visit.VisitId);
                    if (ipd != null)
                    {
                        if (ipd.IPDMedicalRecord != null)
                        {
                            var part3 = ipd.IPDMedicalRecord.IPDMedicalRecordPart3;
                            if (part3 != null)
                            {
                                var partt3Datas = part3.IPDMedicalRecordPart3Datas;
                                if (partt3Datas != null)
                                {
                                    medicationInfo.NoteOfDoctor = partt3Datas.FirstOrDefault(e => e.Code == "IPDMRPEHDTVANS")?.Value;
                                }

                            }
                        }

                    }

                    #endregion

                    break;
                #endregion
                #region OPD
                case "OPD":
                    var opd = GetOPD(visit.VisitId);

                    var danhGiaBanDauDVLe = opd.EIOAssessmentForRetailServicePatient; // Đánh giá ban đầu người bệnh dịch vụ lẻ
                    var danhGiaBanDauTelehealth = opd.OPDInitialAssessmentForTelehealth; // Đánh giá ban đầu Telehealth
                    var danhGiaBanDauThongThuong = visit.OPDInitialAssessmentForShortTerm; // Đánh giá ban đầu người bệnh ngoại trú thường

                    List<AssessmentModel> listOPDAssessments = new List<AssessmentModel>();
                    if (danhGiaBanDauDVLe != null)
                    {
                        listOPDAssessments.Add(new AssessmentModel { FormName = "danhGiaBanDauDVLe", UpdateAt = (DateTime)danhGiaBanDauDVLe.UpdatedAt });
                    }

                    if (danhGiaBanDauTelehealth != null)
                    {
                        listOPDAssessments.Add(new AssessmentModel { FormName = "danhGiaBanDauTelehealth", UpdateAt = (DateTime)danhGiaBanDauTelehealth.UpdatedAt });
                    }

                    if (danhGiaBanDauThongThuong != null)
                    {
                        listOPDAssessments.Add(new AssessmentModel { FormName = "danhGiaBanDauThongThuong", UpdateAt = (DateTime)danhGiaBanDauThongThuong.UpdatedAt });
                    }

                    listOPDAssessments = listOPDAssessments.OrderByDescending(e => e.UpdateAt).ToList();

                    if (listOPDAssessments.Count > 0)
                    {
                        if (listOPDAssessments[0].FormName == "danhGiaBanDauDVLe")
                        {
                            medicationInfo = GetMedicalInfoOPDDVLe(danhGiaBanDauDVLe, visit);
                        }
                        else if (listOPDAssessments[0].FormName == "danhGiaBanDauTelehealth")
                        {
                            medicationInfo = GetMedicalInfoOPDTelehealth(danhGiaBanDauTelehealth, visit);

                        }
                        else if (listOPDAssessments[0].FormName == "danhGiaBanDauThongThuong")
                        {
                            medicationInfo = GetMedicalInfoOPDThongThuong(danhGiaBanDauThongThuong, visit);
                        }
                    }
                    else
                    {
                        medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                        medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                        medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                    }

                    #region Lấy lời dặn của bác sĩ
                    if (opd != null)
                    {
                        var oen = opd.OPDOutpatientExaminationNote;
                        if (oen != null)
                        {
                            var datas = oen.OPDOutpatientExaminationNoteDatas.Where(i => !i.IsDeleted).Select(e => new { e.Id, e.Code, e.Value }).ToList();
                            if (datas != null && datas.Count > 0)
                            {
                                medicationInfo.NoteOfDoctor = datas.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
                            }
                        }
                    }

                    #endregion

                    break;
                #endregion
                #region ED
                case "ED":
                    var ed = GetED(visit.VisitId);
                    var danhGiaBanDauDVLeED = ed.EDAssessmentForRetailServicePatient;
                    var phanLoaiCapCuu = ed.EmergencyTriageRecord;
                    List<AssessmentModel> listEDAssessments = new List<AssessmentModel>();
                    if (danhGiaBanDauDVLeED != null)
                    {
                        listEDAssessments.Add(new AssessmentModel { FormName = "danhGiaBanDauDVLeED", UpdateAt = (DateTime)danhGiaBanDauDVLeED.UpdatedAt });
                    }
                    if (phanLoaiCapCuu != null)
                    {
                        listEDAssessments.Add(new AssessmentModel { FormName = "phanLoaiCapCuu", UpdateAt = (DateTime)phanLoaiCapCuu.UpdatedAt });
                    }

                    listEDAssessments = listEDAssessments.OrderByDescending(e => e.UpdateAt).ToList();
                    if (listEDAssessments.Count > 0)
                    {
                        if (listEDAssessments[0].FormName == "phanLoaiCapCuu")
                        {
                            medicationInfo = GetMedicalInfoEDPhanLoaiCapCuu(phanLoaiCapCuu, visit);
                        }
                        else if (listEDAssessments[0].FormName == "danhGiaBanDauDVLeED")
                        {
                            medicationInfo = GetMedicalInfoEDDVLe(danhGiaBanDauDVLeED, visit);
                        }
                    }
                    else
                    {
                        medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Phân loại cấp cứu.";
                        medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Phân loại cấp cứu.";
                        medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Phân loại cấp cứu.";
                    }

                    #region Lấy lời dặn của bác sĩ
                    if (ed.DischargeInformation != null)
                    {
                        var datas = ed.DischargeInformation.DischargeInformationDatas;
                        if (datas != null && datas.Count > 0)
                        {
                            medicationInfo.NoteOfDoctor = datas.FirstOrDefault(e => e.Code == "DI0DR0ANS")?.Value;
                        }
                    }
                    #endregion

                    break;
                #endregion
                #region EOC
                case "EOC":
                    var form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.VisitId);
                    if (form != null)
                    {
                        List<FormDataValue> eocDatas = GetFormData(visit.VisitId, form.Id, "OPDIAFSTOP");
                        if (eocDatas.Count > 0)
                        {
                            // Chiều cao
                            var heightEOC = eocDatas.FirstOrDefault(e => e.Code == "OPDIAFSTOPHEIANS");
                            if (heightEOC != null)
                            {
                                medicationInfo.CustomerMedicationInfo.Height = heightEOC?.Value;
                            }
                            else
                            {
                                medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                                medicationInfo.CustomerMedicationInfo.Height = "";
                            }

                            // Cân nặng
                            var weightEOC = eocDatas.FirstOrDefault(e => e.Code == "OPDIAFSTOPWEIANS");
                            if (weightEOC != null)
                            {
                                medicationInfo.CustomerMedicationInfo.Weight = weightEOC?.Value;
                            }
                            else
                            {
                                medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                                medicationInfo.CustomerMedicationInfo.Weight = "";
                            }

                            // Dị ứng
                            if (visit.IsAllergy == true)
                            {
                                medicationInfo.CustomerMedicationInfo.Allergy = GenerateAllergyString(visit.KindOfAllergy, visit.Allergy);
                            }
                            else
                            {
                                var npa = eocDatas.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "OPDIAFSTOPALLNPA")?.Value;
                                if (!string.IsNullOrEmpty(npa) && npa.Trim().ToUpper() == "TRUE")
                                {
                                    medicationInfo.CustomerMedicationInfo.Allergy = "Không xác định/ N/A";
                                }
                                else if (eocDatas.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "OPDIAFSTOPALLNOO")?.Value.Trim().ToUpper() == "TRUE")
                                {
                                    medicationInfo.CustomerMedicationInfo.Allergy = "Không/ No";
                                }
                                else
                                {
                                    medicationInfo.CustomerMedicationInfo.Allergy = "";
                                    medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                                }
                            }
                        }
                        else
                        {
                            medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                            medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                            medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                        }
                    }
                    else
                    {
                        medicationInfo.HeightMessage = "Vui lòng bổ sung Chiều cao tại Đánh giá ban đầu người bệnh.";
                        medicationInfo.WeightMessage = "Vui lòng bổ sung Cân nặng tại Đánh giá ban đầu người bệnh.";
                        medicationInfo.AllergyMessage = "Vui lòng bổ sung Dị ứng tại Đánh giá ban đầu người bệnh.";
                    }

                    #region Lấy lời dặn của bác sĩ
                    var eocForm = GetForm(visit.VisitId);
                    if (eocForm != null)
                    {
                        var datas = GetFormData(visit.VisitId, eocForm.Id, "OPDOEN");
                        if (datas.Count > 0)
                        {
                            medicationInfo.NoteOfDoctor = datas.FirstOrDefault(e => e.Code == "OPDOENRFUANS")?.Value;
                        }
                    }

                    #endregion

                    break;
                #endregion 
                default:
                    break;
            }

            #endregion

            if (visit.Customer != null)
            {
                //Ngày tháng năm sinh
                if (visit.Customer.DateOfBirth != null)
                {
                    medicationInfo.CustomerMedicationInfo.DateOfBirth = Convert.ToDateTime(visit.Customer.DateOfBirth).ToString("dd/MM/yyyy") ?? "";
                }
                else
                {
                    medicationInfo.CustomerMedicationInfo.DateOfBirth = null;
                }
            }

            //Số thẻ BHYT
            medicationInfo.CustomerMedicationInfo.InsuranceCardNo = visit.HealthInsuranceNumber ?? "";

            //Thời hạn thẻ BHYT
            if (visit.ExpireHealthInsuranceDate != null)
            {
                medicationInfo.CustomerMedicationInfo.ExpireDate = Convert.ToDateTime(visit.ExpireHealthInsuranceDate).ToString("dd/MM/yyyy") ?? "";
            }
            else
            {
                visit.ExpireHealthInsuranceDate = null;
            }

            //Khoa bệnh nhân đang nằm
            medicationInfo.Specialty = $"{visit.Specialty.Site.ApiCode} - {visit.Specialty.ViName}";

            //Thông tin chẩn đoán
            DiagnosisAndICDModel diagnosisInfo = GetVisitDiagnosisAndICD(visit.VisitId, visit.VisitType, true);
            if (diagnosisInfo == null || diagnosisInfo.Diagnosis == null)
            {
                switch (visit.VisitType)
                {
                    case "ED":
                        medicationInfo.DiagnosisMessage = "Vui lòng bổ sung Chẩn đoán tại Đánh giá kết thúc.";
                        break;
                    case "OPD":
                        medicationInfo.DiagnosisMessage = "Vui lòng bổ sung Chẩn đoán tại Phiếu khám ngoại trú.";
                        break;
                    case "IPD":
                    case "EOC":
                        medicationInfo.DiagnosisMessage = "Vui lòng bổ sung Chẩn đoán tại Bệnh án.";
                        break;
                }
            }
            else
            {
                medicationInfo.CustomerMedicationInfo.DiagnosisInfo = diagnosisInfo;
            }


            return medicationInfo;
        }
    }
}
