using DataAccess.Models;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EForm.Models.DiagnosticReporting;
using EForm.Models.IPDModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDMedicalRecordPart3Controller : IPDMedicalRecordController
    {
        #region Part 3
        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part3/Create/{Type}/{id}")]
        [Permission(Code = "IMRPE1")]
        public IHttpActionResult CreateIPDMedicalRecordPart3API(string Type, Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record == null)
            {
                medical_record = new IPDMedicalRecord();
                medical_record.Version = 2;
                unitOfWork.IPDMedicalRecordRepository.Add(medical_record);
                ipd.IPDMedicalRecordId = medical_record.Id;
                unitOfWork.IPDRepository.Update(ipd);
            }

            int currentVersion = getVersionOfMedicalrecord(formCode: Type, visitId: id, setCurrentVersion: 2);

            var medicalRecordPart3Id = medical_record.IPDMedicalRecordPart3Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart3Id);

            if (medical_record.IPDMedicalRecordPart3Id != null && medicalRecordOfPatient != null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPE_EXIST);

            var recordOfPatient = new IPDMedicalRecordOfPatients();
            if (medicalRecordOfPatient == null || medicalRecordOfPatient.IsDeleted)
            {
                recordOfPatient.FormCode = Type;
                recordOfPatient.VisitId = id;
                recordOfPatient.CreatedBy = GetUser().Username;
                recordOfPatient.CreatedAt = DateTime.Now;
                recordOfPatient.UpdatedAt = recordOfPatient.CreatedAt;
                recordOfPatient.Id = new Guid();
                recordOfPatient.IsDeleted = false;
                recordOfPatient.Version = currentVersion;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Add(recordOfPatient);
            }


            if (medical_record.IPDMedicalRecordPart3Id == null)
            {
                var part_3 = new IPDMedicalRecordPart3();
                unitOfWork.IPDMedicalRecordPart3Repository.Add(part_3);
                medical_record.IPDMedicalRecordPart3Id = part_3.Id;
                unitOfWork.IPDMedicalRecordRepository.Update(medical_record);
                recordOfPatient.FormId = part_3.Id;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(recordOfPatient);
            }
            if (medicalRecordOfPatient == null)
            {
                recordOfPatient.FormId = medical_record.IPDMedicalRecordPart3Id;
                unitOfWork.IPDMedicalRecordOfPatientRepository.Update(recordOfPatient);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/IPD/MedicalRecord/Part3/{Type}/{id}")]
        [Permission(Code = "IMRPE2")]
        public IHttpActionResult GetIPDMedicalRecordPart3API(string Type, Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var setupMedicalRecord = GetSetupMedicalRecord(Type, ipd.SpecialtyId);
            //if (setupMedicalRecord == null || setupMedicalRecord.IsDeploy == false)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD);

            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var medical_record = ipd.IPDMedicalRecord;
            var medicalRecordPart3Id = medical_record.IPDMedicalRecordPart3Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart3Id);
            if (medical_record.IPDMedicalRecordPart3Id == null || medicalRecordOfPatient == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPE_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuildMedicalRecordPart3Result(Type, ipd, medical_record));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part3/{Type}/{id}")]
        [Permission(Code = "IMRPE3")]
        public IHttpActionResult UpdateIPDMedicalRecordPart3API(string Type, Guid id, [FromBody] JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            //var setupMedicalRecord = GetSetupMedicalRecord(Type, ipd.SpecialtyId);
            //if (setupMedicalRecord == null || setupMedicalRecord.IsDeploy == false)
            //    return Content(HttpStatusCode.NotFound, Message.NOT_FOUND_MEDICAL_RECORD);

            if (IPDIsBlock(ipd, Type))
                return Content(HttpStatusCode.NotFound, Message.IPD_TIME_FORBIDDEN);
            if (!IsSuperman() && ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);

            var user = GetUser();
            //if (ipd.PrimaryDoctorId != user.Id)
            //    return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            var medical_record = ipd.IPDMedicalRecord;
            var medicalRecordPart3Id = medical_record.IPDMedicalRecordPart3Id;
            var medicalRecordOfPatient = GetMedicalRecordOfPatients(Type, id, medicalRecordPart3Id);
            if (medical_record.IPDMedicalRecordPart3Id == null || medicalRecordOfPatient == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPE_NOT_FOUND);

            medicalRecordOfPatient.UpdatedAt = DateTime.Now;
            medicalRecordOfPatient.UpdatedBy = GetUser().Username;

            var part_3 = medical_record.IPDMedicalRecordPart3;
            HandleIPDMedicalRecordPart3Datas(ipd, part_3, request["Datas"]);
            HandleIPDMedicalRecordDatas(medical_record, request["GeneralDatas"]);
            var status_code = request["Status"]["Code"].ToString();

            CreateSignature(status_code, ipd);

            bool isUseHandOverCheckList = false;

            try
            {
                isUseHandOverCheckList = Convert.ToBoolean(request["IsUseHandOverCheckList"]);
            }
            catch (Exception)
            {
                isUseHandOverCheckList = false;
            }

            if (!Constant.InHospital.Contains(status_code) && !Constant.WaitingResults.Contains(status_code) && !Constant.Nonhospitalization.Contains(status_code))
            {
                var uncomleted_fields = GetUncompleteField(
                    medical_record.Id,
                    medical_record.IPDMedicalRecordPart2Id,
                    medical_record.IPDMedicalRecordPart3Id,
                    request["GeneralDatas"],
                    status_code,
                    medical_record.Version,
                    Type
                );

                if (isUseHandOverCheckList == false)
                {
                    var item = uncomleted_fields.FirstOrDefault(e => e.Code == "IPDMRPTLDCKANS");
                    if (item != null)
                    {
                        uncomleted_fields.Remove(item);
                    }

                }
                if (uncomleted_fields.Count > 0)
                    return Content(HttpStatusCode.OK, new { Error = uncomleted_fields });
            }
            if (Type == "BMTIMMACH")
            {
                var part3_data = medical_record.IPDMedicalRecordPart3.IPDMedicalRecordPart3Datas;
                var YES = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1106")?.Value;
                if (!string.IsNullOrEmpty(YES) && bool.Parse(YES) == true)
                {
                    var OT = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1109");
                    if (OT != null && bool.Parse(OT?.Value.ToString()) == true)
                    {
                        var OTDesc = part3_data.FirstOrDefault(e => e.Code == "IPDMRPE1110");
                        if (OTDesc == null || string.IsNullOrEmpty(OTDesc?.Value))
                        {
                            List<MedicalReportDataModels> error = new List<MedicalReportDataModels>();
                            error.Add(new MedicalReportDataModels
                            {
                                Code = "IPDMRPE1110",
                                ViName = "Phẫu thuật/ Thủ thuật khác (mô tả cụ thể)",
                                EnName = "Other Surgery/ Procedure",
                                Value = "",
                                Clinic = null,
                                Order = 1109
                            });
                            return Content(HttpStatusCode.OK, new { Error = error });
                        }
                    }
                }
            }

            var status_str = new List<string> {
                "IPDIHT", // Chuyển viện
                "IPDTF", // Chuyển Khoa
                "IPDDD", // Tử vong
                "IPDUDT", // Chuyển tuyến
                "IPDDC" // Ra viện
            };

            if (ipd.IsDraft && string.IsNullOrWhiteSpace(ipd.VisitCode))
            {
                var new_status = request["Status"]["Id"].ToString();
                Guid new_status_id = new Guid(new_status);
                var status_in_db = unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.Id == new_status_id).FirstOrDefault();
                if (ipd.EDStatusId != new_status_id && status_str.Contains(status_in_db.Code))
                {
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        NeedSyncVisitCode = true,
                        Message = new
                        {
                            EnMessage = "Vui lòng đồng bộ PID và Lượt Khám",
                            ViMessage = "Vui lòng đồng bộ PID và Lượt Khám"
                        }
                    });
                }
            }
            var is_error = HandleUpdateStatus(ipd, request["Status"], request["GeneralDatas"], isUseHandOverCheckList);
            if (is_error != null)
                return Content(is_error.Code, is_error.Message);



            var chronic_util = new ChronicUtil(unitOfWork, ipd.Customer);
            chronic_util.UpdateChronic();
            return Content(HttpStatusCode.OK, new { ipd.Customer.IsChronic });
        }
        private void HandleDischargeDate(IPD ipd)
        {
            ipd.DischargeDate = GetDischargeDate(ipd);
            unitOfWork.Commit();
        }
        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part3/SyncHistoryOfPatientIllnessAndAssessment/{Type}")]
        [Permission(Code = "IMRPE4")]
        public IHttpActionResult SyncHistoryOfPatientIllnessAndAssessmentAPI(string Type, [FromBody] JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);
            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record.IPDMedicalRecordPart2Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPT_NOT_FOUND);
            //if (medical_record.IPDMedicalRecordPart3Id == null)
            //    return Content(HttpStatusCode.NotFound, Message.IPD_MRPE_NOT_FOUND);

            var part_2 = medical_record.IPDMedicalRecordPart2;
            if (Type == "A01_039_050919_V")
            {
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_039_050919_V, part_2.Id));
            }
            if (Type == "A01_040_050919_V")
            {
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_040_050919_V, part_2.Id));
            }
            if (Type == "A01_195_050919_V")
            {
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(Constant.IPD_HISTORY_MR_A01_195_050919_V, part_2.Id));
            }

            if (Type == "A01_035_050919_V")
            {
                string[] codeMaster = Constant.IPD_HISTORY_MR_A01_035_050919_V;
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(codeMaster, part_2.Id));
            }

            var code = new List<string>()
            {
                "IPDMRPTTTYTANS", "IPDMRPTTUHOANS", "IPDMRPTTUHOANS", "IPDMRPTHOHAANS",
                "IPDMRPTTIHOANS", "IPDMRPTTTNSANS", "IPDMRPTTHKIANS", "IPDMRPTCOXKANS",
                "IPDMRPTTAMHANS", "IPDMRPTRAHMANS", "IPDMRPTMMATANS", "IPDMRPTNTDDANS",
                "IPDMRPTCXNCANS", "IPDMRPTTTBAANS", "IPDMRPTCACQ", "IPDMRPTQTBLANS"
            };

            var datas = (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
                            e => !e.IsDeleted &&
                            !string.IsNullOrEmpty(e.Code) &&
                            code.Contains(e.Code)
                        )
                         join data_sql in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable().Where(
                             e => !e.IsDeleted &&
                             e.IPDMedicalRecordPart2Id != null &&
                             e.IPDMedicalRecordPart2Id == part_2.Id &&
                             !string.IsNullOrEmpty(e.Code) &&
                             code.Contains(e.Code)
                         )
                         on master.Code equals data_sql.Code into data_list
                         from data_sql in data_list.DefaultIfEmpty()
                         select new
                         {
                             master.Code,
                             data_sql.Value,
                             master.ViName,
                             master.EnName,
                             master.Order
                         }).OrderBy(e => e.Order).ToList();

            if (Type == "A01_038_050919_V")
            {
                var part2Id = medical_record.IPDMedicalRecordPart2Id;
                var toanThan = (from t in unitOfWork.MasterDataRepository.AsQueryable()
                                where !t.IsDeleted && t.Code == "IPDMRPT104"
                                select new
                                {
                                    t.Code,
                                    Value = "",
                                    t.ViName,
                                    t.EnName,
                                    t.Order
                                }).FirstOrDefault();

                var lableTt = (from t in unitOfWork.MasterDataRepository.AsQueryable()
                               where !t.IsDeleted && t.Code == "IPDMRPT133"
                               select new
                               {
                                   t.Code,
                                   Value = "",
                                   t.ViName,
                                   t.EnName,
                                   t.Order
                               }).FirstOrDefault();

                var hoHap = (from h in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                             join m in unitOfWork.MasterDataRepository.AsQueryable()
                             on h.Code equals m.Code
                             where !h.IsDeleted && h.Code == "IPDMRPTHOHAANS" && h.IPDMedicalRecordPart2Id == part2Id
                             select new
                             {
                                 h.Code,
                                 h.Value,
                                 m.ViName,
                                 m.EnName,
                                 m.Order
                             }).FirstOrDefault();
                var xetNghiemCanLam = datas.Where(d => d.Code == "IPDMRPTCXNCANS" || d.Code == "IPDMRPTTTBAANS");
                var hoiBenh = (from h in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                               join m in unitOfWork.MasterDataRepository.AsQueryable()
                               on h.Code equals m.Code
                               where h.Code == "IPDMRPT61" && h.IPDMedicalRecordPart2Id == part2Id
                               select new
                               {
                                   h.Code,
                                   h.Value,
                                   m.ViName,
                                   m.EnName,
                                   m.Order
                               }).FirstOrDefault();
                var getFromMasterData = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                                         join d in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                                         on m.Code equals d.Code
                                         where !m.IsDeleted && !d.IsDeleted &&
                                         m.Order >= 644 && m.Order <= 678 && m.Form == "IPDMRPT"
                                         && m.Level == 2 && d.IPDMedicalRecordPart2Id == part2Id
                                         orderby m.Order
                                         select new
                                         {
                                             Code = m.Code,
                                             Value = d.Value,
                                             ViName = m.ViName,
                                             EnName = m.EnName,
                                             Order = m.Order
                                         }).Distinct().ToList();
                //var statusNewbornInSpecialty = (from m in unitOfWork.MasterDataRepository.AsQueryable()
                //                                join d in unitOfWork.IPDMedicalRecordPart2DataRepository.AsQueryable()
                //                                on m.Code equals d.Code
                //                                where !m.IsDeleted && !d.IsDeleted &&
                //                                m.Order >= 34 && m.Order <= 47 && m.Form == "IPDMRPT"
                //                                && m.Level == 2 && d.IPDMedicalRecordPart2Id == part2Id
                //                                orderby m.Order
                //                                select new
                //                                {
                //                                    Code = m.Code,
                //                                    Value = d.Value,
                //                                    ViName = m.ViName,
                //                                    EnName = m.EnName,
                //                                    Order = m.Order
                //                                }).ToList();

                var removeVd = (from r in getFromMasterData
                                where r.Code == "IPDMRPT110"
                                select new
                                {
                                    r.Code,
                                    r.Value,
                                    r.ViName,
                                    r.EnName,
                                    r.Order
                                }).FirstOrDefault();


                getFromMasterData.Insert(0, hoiBenh);
                getFromMasterData.Insert(1, toanThan);
                getFromMasterData.Add(hoHap);
                getFromMasterData.Add(lableTt);
                getFromMasterData.AddRange(xetNghiemCanLam);
                getFromMasterData.Remove(removeVd);
                //List<dynamic> getMasterdataCode = new List<dynamic>();
                //foreach (var item in getFromMasterData)
                //{
                //    if (item.Value == null || item.Value == "False" || item.Value == "") continue;
                //    getMasterdataCode.Add(item);
                //}

                return Content(HttpStatusCode.OK, getFromMasterData);
            }

            if (Type == "A01_035_050919_V")
            {
                string[] codeMaster = Constant.IPD_HISTORY_MR_A01_035_050919_V;
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(codeMaster, part_2.Id));
            }

            if (Type == "A01_036_050919_V")
            {
                string[] codes = Constant.IPD_HISTORY_MR_A01_036_050919_V;
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(codes, part_2.Id));
            }

            if (Type == "A01_196_050919_V")
            {
                string[] codes = Constant.IPD_HISTORY_MR_A01_196_050919_V;
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(codes, part_2.Id));
            }

            if (Type == "A01_041_050919_V")
            {
                var nguyennhan = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(d => !d.IsDeleted && d.Code == "IPDMRPT1054" && d.IPDMedicalRecordPart2Id == part_2.Id);
                if (string.IsNullOrEmpty(nguyennhan?.Value))
                {
                    string[] codes_v1 = Constant.IPD_HISTORY_MR_A01_041_050919_V;
                    return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(codes_v1, part_2.Id));
                }
                string[] codes_v2 = Constant.IPD_HISTORY_MR_A01_041_050919_V_VESION2;
                return Content(HttpStatusCode.OK, GetHistoryMedicalRecord(codes_v2, part_2.Id));
            }
            if (Type == "BMTIMMACH")
            {
                string[] codes = Constant.IPD_HISTORY_MR_BMTIMMACH;
                List<MappingData> datasHis = GetHistoryMedicalRecord(codes, part_2.Id);


                datasHis.Add(new MappingData
                {
                    ViName = "+ Bản thân",
                    EnName = "+ Personal medical history",
                    Code = "IPDMRPTBATHANS",
                    Value = GetPersonalHistory(ipd),
                    Group = "",
                    DataType = "",
                    Order = 8888
                });
                return Content(HttpStatusCode.OK, datasHis);
            }

            return Content(HttpStatusCode.OK, datas);
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/MedicalRecord/Part3/SyncPrincipalTest/{Type}")]
        [Permission(Code = "IMRPE5")]
        public IHttpActionResult SyncPrincipalTestAPI(string Type, [FromBody] JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var customer = ipd.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.CUSTOMER_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            if (ipd.IPDMedicalRecordId == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MERE_NOT_FOUND);
            var medical_record = ipd.IPDMedicalRecord;
            if (medical_record.IPDMedicalRecordPart3Id == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_MRPE_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            dynamic lab_result;
            dynamic xray_result;
            if (site_code == "times_city")
            {
                lab_result = EHosClient.GetLabResults(customer.PID);
                xray_result = EHosClient.GetXrayResults(customer.PID);
            }
            else
            {
                var api_code = GetSiteAPICode();
                //if (ipd.IsEhos == true)
                //{
                //    var afterLab = OHClient.GetLabResults(customer.PID, api_code);
                //    var afterXray = OHClient.GetXrayResultsByPID(customer.PID);
                //    lab_result = afterLab.Concat(EHosClient.GetLabResults(customer.PIDEhos));
                //    xray_result = afterXray.Concat(EHosClient.GetXrayResults(customer.PIDEhos));
                //}
                //else
                //{
                //    lab_result = OHClient.GetLabResults(customer.PID, api_code);
                //    xray_result = OHClient.GetXrayResultsByPID(customer.PID);
                //}
                lab_result = OHClient.GetLabResults(customer.PID, api_code);
                xray_result = OHClient.GetXrayResultsByPID(customer.PID);
            }
            return Content(HttpStatusCode.OK, new
            {
                XetNghiem = lab_result,
                CDHA = xray_result,
                DiagnosticReporting = GroupDiagnosticReportingByPid(customer.Id)
            });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/MedicalRecord/Part3/Sync/{Type}/{id}")]
        [Permission(Code = "IMRPE6")]
        public IHttpActionResult SyncIPDMedicalRecordPart3API(string Type, Guid id)
        {
            var current_ipd = GetIPD(id);
            if (current_ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (Type == "A01_196_050919_V")
            {
                string IdOldOncologyMedicalRecord = null;
                var results = GetAllInfoCustomerInAreIPD(current_ipd);
                if (results.Count > 1)
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].Id == current_ipd.Id)
                            continue;
                        Guid oldIpdIdtmp = (Guid)results[i].Id;
                        var ipdOld = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == oldIpdIdtmp);
                        if (ipdOld != null)
                        {
                            var MedicalRecordOfPatient = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(x => x.VisitId == oldIpdIdtmp && x.FormCode == "A01_196_050919_V");
                            if (MedicalRecordOfPatient != null)
                            {
                                IdOldOncologyMedicalRecord = oldIpdIdtmp.ToString();
                                break;
                            }
                        }
                    };
                }

                if (string.IsNullOrEmpty(IdOldOncologyMedicalRecord))
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                var opd_ipd = GetIPD(Guid.Parse(IdOldOncologyMedicalRecord));
                if (opd_ipd == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                if (opd_ipd.IPDMedicalRecordId == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                var medical_record = opd_ipd.IPDMedicalRecord;
                if (medical_record.IPDMedicalRecordPart3Id == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                return Content(HttpStatusCode.OK, BuildMedicalRecordPart3Result(Type, opd_ipd, medical_record, current_ipd));
            }
            else
            {
                if (current_ipd.TransferFromId == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                var ipd = unitOfWork.IPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.HandOverCheckListId != null &&
                    e.HandOverCheckListId == current_ipd.TransferFromId
                );
                if (ipd == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);

                if (ipd.IPDMedicalRecordId == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                var medical_record = ipd.IPDMedicalRecord;
                if (medical_record.IPDMedicalRecordPart3Id == null)
                    return Content(HttpStatusCode.NotFound, Message.SYNC_NOT_FOUND);
                return Content(HttpStatusCode.OK, BuildMedicalRecordPart3Result(Type, ipd, medical_record, current_ipd));
            }
        }

        [HttpGet]
        [Route("api/IPD/MedicalRecord/Part3/SyncInfoNewborn/{visitId}")]
        public IHttpActionResult GetInforNewbornFromInitAssessment(Guid visitId)
        {
            IPD visit = GetIPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, new { NewbornFromInitialAsseesment = SynsVitalSigFromInitialAsseesment(visit) });
        }
        #endregion

        private void HandleUpdateOrCreateHandOverCheckList(IPDHandOverCheckList hand_over_check_list, JToken request_datas)
        {
            var user = GetUser();
            var hocl_datas = hand_over_check_list.HandOverCheckListDatas.Where(e => !e.IsDeleted).ToList();
            foreach (var item in request_datas)
            {
                var code = item.Value<string>("Code");
                if (code == null)
                    continue;
                var value = item.Value<string>("Value");
                var hocl = hocl_datas.FirstOrDefault(e => e.Code == code);
                if (hocl == null)
                    CreateHandOverCheckListData(hand_over_check_list.Id, code, value);
                else if (hocl.Value != value)
                    UpdateHandOverCheckListData(hocl, value);
            }
            hand_over_check_list.HandOverNurseId = user.Id;
            hand_over_check_list.UpdatedBy = user.Username;
            unitOfWork.IPDHandOverCheckListRepository.Update(hand_over_check_list);
            unitOfWork.Commit();
        }

        private void CreateHandOverCheckListData(Guid hocl_id, string code, string value)
        {
            IPDHandOverCheckListData new_hocl_data = new IPDHandOverCheckListData
            {
                HandOverCheckListId = hocl_id,
                Code = code,
                Value = value
            };
            unitOfWork.IPDHandOverCheckListDataRepository.Add(new_hocl_data);
        }

        private void UpdateHandOverCheckListData(IPDHandOverCheckListData hocl_data, string value)
        {
            hocl_data.Value = value;
            unitOfWork.IPDHandOverCheckListDataRepository.Update(hocl_data);
        }
    }
}
