using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.Client;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.DiagnosticReporting;
using EForm.Models.IPDModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDDischargeInformationController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/DischargeInformation/{id}")]
        [Permission(Code = "EDIIN1")]
        public IHttpActionResult GetDischargeInformationAPI(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var dischare_info = ed.DischargeInformation;
            if (dischare_info == null || dischare_info.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_DI0_NOT_FOUND);

            var status = ed.EDStatus;
            var visit_transfer = get_visit_transfer(ed);
            var EOCInfo = getEOCInfo(ed.Id, "ED");
            return Ok(new
            {
                EOCInfo,
                ed.RecordCode,
                dischare_info.Id,
                visit_transfer,
                EDId = ed.Id,
                AdmittedDate = ed.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                AssessmentAt = dischare_info.AssessmentAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Datas = dischare_info.DischargeInformationDatas.Where(e => !e.IsDeleted).Select(etrd => new { dischare_info.Id, etrd.Code, etrd.Value }).ToList(),
                ListStatus = GetStatus(EOCInfo.AcceptBy != null),
                IsCovidSpecialty = IsCovidSpecialty(),
                IsUseHandOverCheckList = ed.HandOverCheckList?.IsUseHandOverCheckList,
                IsAcceptPhysician = ed.HandOverCheckList?.IsAcceptPhysician,
                IsAcceptNurse = ed.HandOverCheckList?.IsAcceptNurse,
                VersionApp = ed.Version
            });
        }

        private dynamic GetStatus(bool is_transfer)
        {
            if (is_transfer)
                return unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.VisitTypeGroupId != null && e.VisitTypeGroup.Code == "ED" && e.Code != "EDNE").OrderBy(e => e.CreatedAt).Select(st => new { st.Id, st.ViName, st.EnName, st.Code }).ToList();
            return unitOfWork.EDStatusRepository.Find(e => !e.IsDeleted && e.VisitTypeGroupId != null && e.VisitTypeGroup.Code == "ED").OrderBy(e => e.CreatedAt).Select(st => new { st.Id, st.ViName, st.EnName, st.Code }).ToList();
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/DischargeInformation/SyncReadOnlySignificantMedications")]
        [Permission(Code = "EDIIN2")]
        public IHttpActionResult SyncReadOnlySignificantMedicationsAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            else if (ed.VisitCode == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var discharge_info = ed.DischargeInformation;
            if (discharge_info == null || discharge_info.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_DI0_NOT_FOUND);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            List<dynamic> medicine;
            if (site_code == "times_city")
                medicine = EHosClient.GetSignificantMedications(customer.PID, ed.VisitCode);
            else
            {
                medicine = OHClient.GetSignificantMedications(customer.PID, ed.VisitCode);
            }


            return Content(HttpStatusCode.OK, medicine);
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/DischargeInformation/SyncReadOnlyResultOfParaclinicalTests")]
        [Permission(Code = "EDIIN3")]
        public IHttpActionResult SyncReadOnlyResultOfParaclinicalTestsAPI([FromBody] JObject request)
        {
            if (request == null || string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            }
            var id = new Guid(request["Id"]?.ToString());
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            //else if (ed.VisitCode == null)
            //    return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var discharge_info = ed.DischargeInformation;
            if (discharge_info == null || discharge_info.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_DI0_NOT_FOUND);

            var site_code = GetSiteCode();
            if (site_code == null)
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
                //if (ed.IsEhos == true)
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


        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/DischargeInformation/SyncDiagnosisAndICD")]
        [Permission(Code = "EDIIN4")]
        public IHttpActionResult SyncDiagnosisAndICDAPI([FromBody] JObject request)
        {
            var id = new Guid(request["Id"]?.ToString());
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            else if (ed.VisitCode == null)
                return Content(HttpStatusCode.NotFound, Message.VISIT_CODE_IS_MISSING);

            var customer = ed.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            var discharge_info = ed.DischargeInformation;
            if (discharge_info == null || discharge_info.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_DI0_NOT_FOUND);

            var site_code = GetSiteCode();
            if (site_code == null)
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var user = GetUser();
            if (string.IsNullOrEmpty(user.EHOSAccount))
                return Content(HttpStatusCode.NotFound, Message.EHOS_ACCOUNT_MISSING);
            dynamic result;
            if (site_code == "times_city")
                result = EHosClient.GetDiagnosisAndICD(customer.PID, ed.VisitCode, user.EHOSAccount);
            else
                result = new List<dynamic>();

            return Content(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/DischargeInformation/{id}")]
        [Permission(Code = "EDIIN5")]
        public IHttpActionResult UpdateDischargeInformationAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var discharge_info = ed.DischargeInformation;
            if (discharge_info == null && discharge_info.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_DI0_NOT_FOUND);

            var user = GetUser();

            bool isUseHandOverCheckList = false;

            try
            {
                isUseHandOverCheckList = Convert.ToBoolean(request["IsUseHandOverCheckList"]);
            }
            catch (Exception)
            {
                isUseHandOverCheckList = false;
            }

            var status_code = request["Status"]["Code"].ToString();
            var visit_transfer = get_visit_transfer(ed);
            if (visit_transfer != null && Constant.NoExamination.Contains(status_code))
            {
                return Content(visit_transfer.Code, visit_transfer.Message);
            }

            if (IsNew(discharge_info.CreatedAt, discharge_info.UpdatedAt))
                UpdateOwner(discharge_info);

            HandleAssessmentAt(discharge_info, request["AssessmentAt"]?.ToString());

            HandleUpdateOrCreateDischargeInformationData(ed, discharge_info, request["Datas"]);

            if (user.Username != discharge_info.CreatedBy)
                CreatedEDChangingNotification(user, discharge_info.CreatedBy, ed, "Đánh giá kết thúc", "DI0");

            if (!Constant.ED_UNCHECK_FIELD_STATUS.Contains(status_code))
            {
                var uncomleted_fields = GetUncompleteField(
                    discharge_info.Id,
                    request["Datas"],
                    status_code,
                    ed.Version
                );

                //Nếu bệnh nhân không sử dụng Biên bản bàn giao người bệnh chuyển khoa thì ko bắt buộc nhập Lý do chuyển
                if (isUseHandOverCheckList == false)
                {
                    //DI0RFAANS
                    var item = uncomleted_fields.FirstOrDefault(e => e.Code == "DI0RFAANS" || e.Code == "DI0RFT2ANS");
                    if (item != null)
                    {
                        uncomleted_fields.Remove(item);
                    }
                }
                if (uncomleted_fields.Count > 0)
                    return Content(HttpStatusCode.OK, new { Error = uncomleted_fields });
            }

            var error_mes = HandleStatus(ed, request, isUseHandOverCheckList);
            if (error_mes != null)
                return Content(error_mes.Code, error_mes.Message);

            // UpdatePrimaryDoctor(ed);
            HandleDischargeDate(ed);
            var chronic_util = new ChronicUtil(unitOfWork, ed.Customer);
            chronic_util.UpdateChronic();
            CreateEOCTranfer(ed.Id, request["Datas"], user.Username, "ED", (Guid)ed.CustomerId);
            return Content(HttpStatusCode.OK, new { ed.Customer.IsChronic });
        }
        private void UpdateOwner(EDDischargeInformation discharge_info)
        {
            discharge_info.CreatedBy = GetUser()?.Username;
            unitOfWork.DischargeInformationRepository.Update(discharge_info);
            unitOfWork.Commit();
        }
        private void UpdatePrimaryDoctor(ED ed)
        {
            ed.PrimaryDoctorId = GetUser()?.Id;
            unitOfWork.Commit();
        }

        private void HandleAssessmentAt(EDDischargeInformation dischare_info, string request_assessment)
        {
            if (!string.IsNullOrEmpty(request_assessment))
            {
                // String was not recognized as a valid DateTime
                bool isDateTime = DateTime.TryParseExact(request_assessment, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null, DateTimeStyles.None, out DateTime request_assessment_datetime);
                // DateTime request_assessment_datetime = DateTime.ParseExact(request_assessment, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                if (!isDateTime)
                    return;
                if (request_assessment_datetime != null && dischare_info.AssessmentAt != request_assessment_datetime)
                {
                    dischare_info.AssessmentAt = request_assessment_datetime;
                    unitOfWork.DischargeInformationRepository.Update(dischare_info);
                    unitOfWork.Commit();
                }
            }
        }

        private void HandleDischargeDate(ED ed)
        {
            ed.DischargeDate = GetDischargeDate(ed);
            unitOfWork.Commit();
        }
        private dynamic HandleStatus(ED ed, JObject request, bool isUseHandOverCheckList)
        {
            var new_status = request["Status"]["Id"].ToString();
            Guid new_status_id = new Guid(new_status);

            if (new_status_id == Guid.Empty) return null;

            var current_status = ed.EDStatus;

            var new_status_code = request["Status"]["Code"].ToString();

            var err = IsChangeTransferedStatus(ed, request["Datas"], current_status, new_status_code, ed.DischargeInformation.DischargeInformationDatas);
            if (err != null)
                return err;

            if (current_status.Id != new_status_id)
            {
                if (Constant.TransferToOPD.Contains(current_status.Code) || Constant.Admitted.Contains(current_status.Code))
                {
                    var visit_transfer = new VisitTransfer(unitOfWork);
                    if (visit_transfer.IsExist(ed.HandOverCheckListId, ed.Id))
                        return visit_transfer.BuildMessage();
                }

                //if (!IsConfirmAllOrder(ed))
                //    return new {
                //        Code = HttpStatusCode.BadRequest,
                //        Message = Message.ED_CONFIRM_PLS
                //    };
                string viMessage = "";
                string enMessage = "";
                if (!IsConfirmStandingOrder(ed.Id))
                {
                    viMessage += "Cần xác nhận ghi nhận thực hiện thuốc standing order" + "</br>";
                    enMessage += "You must confirm standing order form" + "</br>";
                }
                if (!IsConfirmOrder(ed.Id))
                {
                    viMessage += "Cần xác nhận ghi nhận y lệnh miệng" + "</br>";
                    enMessage += "You must confirm order form" + "</br>";
                }
                if (!IsConfirmSkinTest(ed.Id))
                {
                    viMessage += "Cần xác nhận kết quả test da" + "</br>";
                    enMessage += "You must confirm skin test form" + "</br>";
                }

                if (viMessage != "")
                {
                    return new
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = new
                        {
                            ViMessage = viMessage.Substring(0, viMessage.LastIndexOf("</br>")),
                            EnMessage = enMessage.Substring(0, enMessage.LastIndexOf("</br>"))
                        }
                    };
                }
                if (Constant.InHospital.Contains(new_status_code) || Constant.TransferToOPD.Contains(new_status_code) || Constant.Admitted.Contains(new_status_code))
                {
                    Guid visit_id = ed.Id;
                    Guid customer_id = (Guid)ed.CustomerId;
                    string customer_PID = ed.Customer.PID;

                    InHospital in_hospital = new InHospital();
                    in_hospital.SetState(customer_id, visit_id, null, null);
                    var in_hospital_visit = in_hospital.GetVisit();
                    if (in_hospital_visit != null)
                    {
                        return new
                        {
                            Code = HttpStatusCode.BadRequest,
                            Message = in_hospital.BuildErrorMessage(in_hospital_visit)
                        };
                    }

                    dynamic in_waiting_visit;
                    if (!string.IsNullOrEmpty(ed.Customer.PID))
                        in_waiting_visit = GetInWaitingAcceptPatientByPID(pid: customer_PID, visit_id: visit_id);
                    else
                        in_waiting_visit = GetInWaitingAcceptPatientById(customer_id: customer_id, visit_id: visit_id);
                    if (in_waiting_visit != null)
                    {
                        var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                        return new
                        {
                            Code = HttpStatusCode.BadRequest,
                            Message = BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician)
                        };
                    }
                }

                if ((!Constant.TransferToOPD.Contains(new_status_code) && !Constant.Admitted.Contains(new_status_code)))
                    RemoveTransferIfExist(ed);

                var erorr = EDValiDatePIDAndVisitCode(ed, new_status_code);
                if (erorr != null)
                    return erorr;

                UpdateStatus(ed, request["Status"]);
            }

            if (Constant.InterHospitalTransfer.Contains(new_status_code))
                HandleTransfered(ed);
            else if (Constant.TransferToOPD.Contains(new_status_code) || Constant.Admitted.Contains(new_status_code))
            {
                var error = HandleTransferForAnotherDepartment(ed, request["Datas"], new_status_code, isUseHandOverCheckList);
                if (error != null)
                    return new
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = error
                    };
            }
            return null;
        }

        private dynamic EDValiDatePIDAndVisitCode(ED visit, string status_code)
        {
            if (Constant.InHospital.Contains(status_code) || Constant.WaitingResults.Contains(status_code) || Constant.NoExamination.Contains(status_code) || Constant.Nonhospitalization.Contains(status_code))
                return null;

            string visitCode = visit.VisitCode;
            var custumer = visit.Customer;
            string pId = custumer?.PID;
            if (string.IsNullOrEmpty(visitCode) && string.IsNullOrEmpty(pId))
            {
                string en_err = "Please sync to the visit code and PID of the patient!";
                string vi_err = "Vui lòng đồng bộ lượt tiếp nhận và PID của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            if (string.IsNullOrEmpty(visitCode))
            {
                string en_err = "Please sync to the visit code of the patient!";
                string vi_err = "Vui lòng đồng bộ lượt tiếp nhận của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            if (string.IsNullOrEmpty(pId))
            {
                string en_err = "Please sync to the PID of the patient!";
                string vi_err = "Vui lòng đồng bộ PID của NB!";
                return new
                {
                    Code = HttpStatusCode.NotFound,
                    Message = new
                    {
                        IsErorr = true,
                        EnMessage = en_err,
                        ViMessage = vi_err
                    }
                };
            }

            return null;
        }

        private dynamic IsChangeTransferedStatus(ED ed, JToken jToken, EDStatus current_status, string new_status_code, ICollection<EDDischargeInformationData> dischargeInformationDatas)
        {
            string newStatus_code = new_status_code;
            string newTransfered = "";
            string oldStatus_code = current_status.Code;
            string oldTransfered = "";
            var visit_transfer = get_visit_transfer(ed);
            if (Constant.TransferToOPD.Contains(newStatus_code))
            {
                newTransfered = jToken.FirstOrDefault(d => d.Value<string>("Code") == "DI0REC2ANS").Value<string>("Value");
            }
            else
            {
                newTransfered = jToken.FirstOrDefault(d => d.Value<string>("Code") == "DI0REC1ANS").Value<string>("Value");
            }

            if (Constant.TransferToOPD.Contains(oldStatus_code))
            {
                oldTransfered = dischargeInformationDatas.FirstOrDefault(d => d.Code == "DI0REC2ANS")?.Value;
            }
            else
            {
                oldTransfered = dischargeInformationDatas.FirstOrDefault(d => d.Code == "DI0REC1ANS")?.Value;
            }

            if (visit_transfer != null)
            {
                if (oldStatus_code != newStatus_code) return visit_transfer;
                if (oldTransfered != newTransfered) return visit_transfer;
            }
            return null;
        }

        private dynamic get_visit_transfer(ED ed)
        {
            var visit_transfer = new VisitTransfer(unitOfWork);
            if (visit_transfer.IsExist(ed.HandOverCheckListId, ed.Id))
                return visit_transfer.BuildMessage();
            return null;
        }

        private bool IsConfirmAllOrder(ED visit)
        {
            var order = unitOfWork.OrderRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit.Id &&
                (e.OrderType.Equals(Constant.ED_ORDER) || e.OrderType.Equals(Constant.ED_STANDING_ORDER)) &&
                !e.IsConfirm
            );

            if (order != null)
                return false;

            var skin_test = visit.EDSkinTestResult;
            if (skin_test != null && skin_test.ConfirmDoctorId == null)
                return false;

            return true;
        }
        private void RemoveTransferIfExist(ED ed)
        {
            var hocl = ed.HandOverCheckList;
            if (hocl != null)
            {
                unitOfWork.HandOverCheckListRepository.Delete(hocl);
                ed.HandOverCheckListId = null;
                unitOfWork.EDRepository.Update(ed);
                unitOfWork.Commit();
            }
        }
        private void UpdateStatus(ED ed, JToken status)
        {
            var status_raw = status["Id"].ToString();
            Guid status_id = new Guid(status_raw);
            if (status_id != ed.EDStatusId)
            {
                ed.EDStatusId = status_id;
                var customer = ed.Customer;
                customer.EDStatusId = status_id;
                unitOfWork.CustomerRepository.Update(customer);
                unitOfWork.EDRepository.Update(ed);
            }
            unitOfWork.Commit();
        }
        private void HandleTransfered(ED ed)
        {
            if (ed.MonitoringChartAndHandoverFormId == null)
            {
                EDMonitoringChartAndHandoverForm mca = new EDMonitoringChartAndHandoverForm();
                unitOfWork.MonitoringChartAndHandoverFormRepository.Add(mca);

                ed.MonitoringChartAndHandoverFormId = mca.Id;
                unitOfWork.EDRepository.Update(ed);

                unitOfWork.Commit();
            }
        }
        private dynamic HandleTransferForAnotherDepartment(ED ed, JToken datas, string status_code, bool isUseHandOverCheckList)
        {
            Guid? receiving_id = null;
            string receiving = string.Empty;
            string reason = string.Empty;
            if (Constant.TransferToOPD.Contains(status_code))
            {
                receiving = datas.FirstOrDefault(d => d.Value<string>("Code") == "DI0REC2ANS").Value<string>("Value");
                UpdateOrCreateDischargeInformationData(ed.DischargeInformationId, "DI0REC2ANS", receiving);
                reason = datas.FirstOrDefault(d => d.Value<string>("Code") == "DI0RFT2ANS").Value<string>("Value");
                UpdateOrCreateDischargeInformationData(ed.DischargeInformationId, "DI0RFT2ANS", reason);
            }
            else
            {
                receiving = datas.FirstOrDefault(d => d.Value<string>("Code") == "DI0REC1ANS").Value<string>("Value");
                UpdateOrCreateDischargeInformationData(ed.DischargeInformationId, "DI0REC1ANS", receiving);
                reason = datas.FirstOrDefault(d => d.Value<string>("Code") == "DI0RFAANS").Value<string>("Value");
                UpdateOrCreateDischargeInformationData(ed.DischargeInformationId, "DI0RFAANS", reason);
            }

            try
            {
                receiving_id = new Guid(receiving);
            }
            catch (Exception)
            {
                return Message.TRANSFER_ERROR;
            }
            var specialty = ed.Specialty;
            var receiving_unit = unitOfWork.SpecialtyRepository.GetById((Guid)receiving_id);
            if (ed.HandOverCheckListId == null)
            {
                CreateHandOverCheckList(ed, specialty, receiving_unit, reason, isUseHandOverCheckList);
            }
            else
            {
                if (ed.HandOverCheckList?.ReceivingPhysicianId == null && ed.HandOverCheckList?.ReceivingNurseId == null)
                {
                    UpdateHandOverCheckList(ed.HandOverCheckList, specialty, receiving_unit, reason, isUseHandOverCheckList);
                }

            }
            unitOfWork.Commit();
            return null;
        }
        private void CreateHandOverCheckList(ED ed, Specialty specialty, Specialty receiving, string reason, bool isUseHandOverCheckList)
        {
            EDHandOverCheckList hocl = new EDHandOverCheckList();
            hocl.HandOverTimePhysician = DateTime.Now;
            hocl.HandOverUnitNurseId = specialty.Id;
            hocl.HandOverUnitPhysicianId = specialty.Id;
            hocl.ReceivingUnitNurseId = receiving.Id;
            hocl.ReceivingUnitPhysicianId = receiving.Id;
            hocl.HandOverPhysicianId = GetUser()?.Id;
            hocl.ReasonForTransfer = reason;
            hocl.IsUseHandOverCheckList = isUseHandOverCheckList;
            if (!receiving.IsPublish)
            {
                hocl.IsAcceptNurse = true;
                hocl.IsAcceptPhysician = true;
            }
            unitOfWork.HandOverCheckListRepository.Add(hocl);
            ed.HandOverCheckListId = hocl.Id;

            dynamic all_data;
            dynamic vital_sign;
            if (ed.IsRetailService && ed.EDAssessmentForRetailServicePatient?.UpdatedAt > ed.EmergencyTriageRecord?.UpdatedAt)
            {
                all_data = ed.EDAssessmentForRetailServicePatient.EIOAssessmentForRetailServicePatientDatas
                    .Where(e => !e.IsDeleted && e.Code.Contains("EDAFRSPALL"))
                    .Select(e => new { e.Code, e.Value })
                    .ToList();
                vital_sign = GetVitalSignDatasInRetailService(ed.EDAssessmentForRetailServicePatientId);
            }
            else
            {
                all_data = ed.EmergencyTriageRecord.EmergencyTriageRecordDatas
                    .Where(e => !e.IsDeleted && e.Code.Contains("ETRALL"))
                    .Select(e => new { e.Code, e.Value })
                    .ToList();
                vital_sign = GetVitalSignDatasInETR(ed.ObservationChartId);
            }

            foreach (var data in all_data)
                UpdateAllergy(data.Code, data.Value, hocl);
            UpdateVitalSign(vital_sign, hocl.Id);
        }
        private void UpdateHandOverCheckList(EDHandOverCheckList hocl, Specialty specialty, Specialty receiving, string reason, bool isHandOverCheckList)
        {
                hocl.HandOverUnitNurseId = specialty.Id;
                hocl.HandOverUnitPhysicianId = specialty.Id;
                hocl.ReceivingUnitNurseId = receiving.Id;
                hocl.ReceivingUnitPhysicianId = receiving.Id;
                hocl.HandOverPhysicianId = GetUser()?.Id;
                hocl.ReasonForTransfer = reason;
                if (!receiving.IsPublish)
                {
                    hocl.IsAcceptNurse = true;
                    hocl.IsAcceptPhysician = true;
                }
                else
                {
                    if (hocl.ReceivingNurseId == null && hocl.ReceivingPhysicianId == null)
                    {
                        hocl.IsAcceptNurse = false;
                        hocl.IsAcceptPhysician = false;
                    }
                }
            hocl.IsUseHandOverCheckList = isHandOverCheckList;
            unitOfWork.HandOverCheckListRepository.Update(hocl);
        }
        private void UpdateAllergy(string code, string value, EDHandOverCheckList hoc)
        {
            EDHandOverCheckListData new_all = new EDHandOverCheckListData
            {
                HandOverCheckListId = hoc.Id,
                Code = Constant.ED_HOC_ALLERGIC_CODE_SWITCH[code],
                Value = value
            };
            unitOfWork.HandOverCheckListDataRepository.Add(new_all);
        }


        private dynamic GetVitalSignDatasInETR(Guid? ia_id)
        {
            var vital_sign = new List<dynamic>();
            var observation_chart = unitOfWork.EDObservationChartDataRepository.Find(
                e => !e.IsDeleted &&
                e.ObservationChartId != null &&
                e.ObservationChartId == ia_id
            ).OrderByDescending(e => e.NoteAt).ToList();
            if (observation_chart.Count() < 1)
                return vital_sign;

            var data = observation_chart[0];
            vital_sign.Add(new { ViName = "Huyết áp", Value = string.Format("{0}/{1}", data.SysBP, data.DiaBP), Note = "mmHg" });
            vital_sign.Add(new { ViName = "Mạch", Value = data.Pulse, Note = "nhịp/phút" });
            vital_sign.Add(new { ViName = "Nhịp thở", Value = data.Resp, Note = "lần/phút" });
            vital_sign.Add(new { ViName = "SpO2", Value = data.SpO2, Note = "%" });
            vital_sign.Add(new { ViName = "Nhiệt độ", Value = data.Temperature, Note = "độ C" });
            return vital_sign;
        }
        private dynamic GetVitalSignDatasInRetailService(Guid? ia_id)
        {
            string accept_code = "EDAFRSPPULANS,EDAFRSPBP0ANS,EDAFRSPTEMANS,EDAFRSPSPOANS,EDAFRSPRR0ANS";
            var vital_sign = (from data in unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.AsQueryable()
                             .Where(
                                i => !i.IsDeleted &&
                                i.EDAssessmentForRetailServicePatientId != null &&
                                i.EDAssessmentForRetailServicePatientId == ia_id &&
                                !string.IsNullOrEmpty(i.Code) &&
                                accept_code.Contains(i.Code)
                             )
                              join master in unitOfWork.MasterDataRepository.AsQueryable() on data.Code equals master.Code into ulist
                              from master in ulist.DefaultIfEmpty()
                              select new { master.ViName, data.Value, master.Note }).ToList();
            return vital_sign;
        }
        private void UpdateVitalSign(dynamic data, Guid hoc_id)
        {
            string vital_sign = JoinVitalSign(data);
            var val = "True";
            if (string.IsNullOrEmpty(vital_sign)) val = "False";

            EDHandOverCheckListData vs_yes = new EDHandOverCheckListData
            {
                HandOverCheckListId = hoc_id,
                Code = "HOCVS0YES",
                Value = val
            };
            unitOfWork.HandOverCheckListDataRepository.Add(vs_yes);
            EDHandOverCheckListData vs_no = new EDHandOverCheckListData
            {
                HandOverCheckListId = hoc_id,
                Code = "HOCVS0NOO",
                Value = "False"
            };
            unitOfWork.HandOverCheckListDataRepository.Add(vs_no);
            EDHandOverCheckListData vs_ans = new EDHandOverCheckListData
            {
                HandOverCheckListId = hoc_id,
                Code = "HOCVS0ANS",
                Value = vital_sign,
            };
            unitOfWork.HandOverCheckListDataRepository.Add(vs_ans);
        }
        private string JoinVitalSign(dynamic data)
        {
            string result = string.Empty;
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Value))
                    result += string.Format("{0}: {1} {2}, ", item.ViName, item.Value, item.Note);
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.TrimEnd();
                result = result.Remove(result.Length - 1);
            }
            return result;
        }


        private void HandleUpdateOrCreateDischargeInformationData(ED ed, EDDischargeInformation dischare_info, JToken request_dischare_info_data)
        {
            var dischare_info_datas = dischare_info.DischargeInformationDatas.Where(e => !e.IsDeleted).ToList();
            foreach (var item in request_dischare_info_data)
            {
                var code = item.Value<string>("Code");
                if (code == null || "DI0REC2ANS,DI0RFT2ANS,DI0REC1ANS,DI0RFAANS".Contains(code))
                    continue;

                var value = item.Value<string>("Value");
                var di_data = dischare_info_datas.FirstOrDefault(e => e.Code == code);

                if (di_data == null)
                    CreateDischargeInformationData(ed, dischare_info.Id, code, value);
                else if (di_data.Value != value)
                    UpdateDischargeInformationData(ed, di_data, code, value);
            }

            var user = GetUser();
            var hocl = ed.HandOverCheckList;
            if (hocl != null)
            {
                hocl.HandOverPhysicianId = user.Id;
                unitOfWork.HandOverCheckListRepository.Update(hocl);
            }

            dischare_info.UpdatedBy = user.Username;
            unitOfWork.DischargeInformationRepository.Update(dischare_info);
            unitOfWork.Commit();
        }
        private void CreateDischargeInformationData(ED ed, Guid dischare_info_id, string code, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                EDDischargeInformationData new_di_data = new EDDischargeInformationData();
                new_di_data.DischargeInformationId = dischare_info_id;
                new_di_data.Code = code;
                new_di_data.Value = value;
                unitOfWork.DischargeInformationDataRepository.Add(new_di_data);
                if (code == "DI0DIAANS")
                    UpdateDiagnosis(ed, value);
            }
        }
        private void UpdateDischargeInformationData(ED ed, EDDischargeInformationData di_data, string code, string value)
        {
            di_data.Value = value;
            unitOfWork.DischargeInformationDataRepository.Update(di_data);
            if (code == "DI0DIAANS")
                UpdateDiagnosis(ed, value);
        }
        private void UpdateOrCreateDischargeInformationData(Guid? dischare_info_id, string code, string value)
        {
            var data = unitOfWork.DischargeInformationDataRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.DischargeInformationId == dischare_info_id &&
                e.Code == code
            );
            if (data != null)
            {
                data.Value = value;
                unitOfWork.DischargeInformationDataRepository.Update(data);
                return;
            }

            data = new EDDischargeInformationData
            {
                DischargeInformationId = dischare_info_id,
                Code = code,
                Value = value,
            };
            unitOfWork.DischargeInformationDataRepository.Add(data);
        }
        private void UpdateDiagnosis(ED ed, string value)
        {
            if (ed.PreOperativeProcedureHandoverChecklistId != null)
            {
                var phc_data = unitOfWork.EIOPreOperativeProcedureHandoverChecklistDataRepository.FirstOrDefault(e => !e.IsDeleted
                && e.PreOperativeProcedureHandoverChecklistId == ed.PreOperativeProcedureHandoverChecklistId
                && e.Code == "PHCPD0ANS");
                if (phc_data != null && string.IsNullOrEmpty(phc_data.Value))
                    UpdatePreOperativeProcedureHandoverChecklistDatas(phc_data, value);
                else
                    CreatePreOperativeProcedureHandoverChecklistDatas(ed.PreOperativeProcedureHandoverChecklistId, value);
            }
        }
        private void UpdatePreOperativeProcedureHandoverChecklistDatas(EIOPreOperativeProcedureHandoverChecklistData phc_data, string value)
        {
            phc_data.Value = value;
            unitOfWork.EIOPreOperativeProcedureHandoverChecklistDataRepository.Update(phc_data);
        }
        private void CreatePreOperativeProcedureHandoverChecklistDatas(Guid? phc_id, string value)
        {
            EIOPreOperativeProcedureHandoverChecklistData phc_data = new EIOPreOperativeProcedureHandoverChecklistData();
            phc_data.PreOperativeProcedureHandoverChecklistId = phc_id;
            phc_data.Code = "PHCPD0ANS";
            phc_data.Value = value;
            unitOfWork.EIOPreOperativeProcedureHandoverChecklistDataRepository.Add(phc_data);
        }


        private List<MedicalReportDataModels> GetUncompleteField(Guid? di_id, JToken request_general, string status_code, int visitVersion = 1)
        {
            List<MedicalReportDataModels> uncomplete_field = new List<MedicalReportDataModels>();

            var di_data = GetDischargeInformationData(di_id);

            var empty_field = GetEmptyTextField(Constant.ED_DI0_TEXT_CODE, di_data);
            uncomplete_field.AddRange(empty_field);
            var empty_xncc = GetEmptyChoiceField(Constant.ED_DI0_XNCC_CODE["QUES"], Constant.ED_DI0_XNCC_CODE["ANS"], di_data);
            if (empty_xncc != null)
                uncomplete_field.Add(empty_xncc);

            var empty_xntt = GetEmptyChoiceField(Constant.ED_DI0_XNTT_CODE["QUES"], Constant.ED_DI0_XNTT_CODE["ANS"], di_data);
            if (empty_xntt != null)
                uncomplete_field.Add(empty_xntt);

            if (Constant.Admitted.Contains(status_code))
            {
                var empty_admitted = GetEmptyTextField(Constant.ED_DI0_ADMITTED_CODE, request_general);
                uncomplete_field.AddRange(empty_admitted);
            }
            else if (Constant.TransferToOPD.Contains(status_code))
            {
                var empty_transfer_to_opd = GetEmptyTextField(Constant.ED_DI0_TRANTOPD_CODE, request_general);
                uncomplete_field.AddRange(empty_transfer_to_opd);
            }
            else if (Constant.InterHospitalTransfer.Contains(status_code))
            {
                var empty_inter_transfer_to_opd = GetEmptyTextField(Constant.ED_DI0_INHOTRAN_CODE, request_general);
                uncomplete_field.AddRange(empty_inter_transfer_to_opd);
            }
            else if (Constant.UpstreamDownstreamTransfer.Contains(status_code))
            {
                var empty_updo_transfer_to_opd = GetEmptyTextField(Constant.ED_DI0_UPDOTRAN_CODE, request_general);
                uncomplete_field.AddRange(empty_updo_transfer_to_opd);

                var refotran_empty = GetEmptyChoiceField(Constant.ED_DI0_REFOTRAN_CODE["QUES"], Constant.ED_DI0_REFOTRAN_CODE["ANS"], request_general);
                if (refotran_empty != null)
                    uncomplete_field.Add(refotran_empty);
            }
            else if (Constant.Discharged.Contains(status_code) && visitVersion >= 10)
            {
                var discharge_empty = GetEmptyChoiceField("DI0DT", new string[] { "DI0DT1", "DI0DT2", "DI0DT3", "DI0DT4" }, di_data);
                if (discharge_empty != null)
                {
                    uncomplete_field.Add(discharge_empty);
                }else
                {
                    var yes = di_data.FirstOrDefault(e => e.Code == "DI0DT2");
                    if (yes != null && !string.IsNullOrEmpty(yes?.Value) && Convert.ToBoolean(yes?.Value) == true)
                    {
                        var requestToLeave = GetEmptyChoiceField("DI0Reason", new string[] { "DI0Reason1", "DI0Reason2", "DI0Reason3" }, di_data);
                        if (requestToLeave != null)
                        {
                            uncomplete_field.Add(requestToLeave);
                        }
                        else
                        {
                            var yes3 = di_data.FirstOrDefault(e => e.Code == "DI0Reason3");
                            if (yes3 != null && !string.IsNullOrEmpty(yes3?.Value) && Convert.ToBoolean(yes3?.Value) == true)
                            {
                                var value4 = di_data?.FirstOrDefault(e => e.Code == "DI0Reason4")?.Value;
                                if (string.IsNullOrEmpty(value4))
                                {
                                    uncomplete_field.Add(di_data?.FirstOrDefault(e => e.Code == "DI0Reason4"));
                                }
                            }
                        }    
                            
                    }
                }    
                    
            }

            //switch (status_code)
            //{
            //    case "Admitted":
            //        var empty_admitted = GetEmptyTextField(Constant.ED_DI0_ADMITTED_CODE, request_general);
            //        uncomplete_field.AddRange(empty_admitted);
            //        break;

            //    case "Transfer to OPD":
            //        var empty_transfer_to_opd = GetEmptyTextField(Constant.ED_DI0_TRANTOPD_CODE, request_general);
            //        uncomplete_field.AddRange(empty_transfer_to_opd);
            //        break;

            //    case "Inter-hospital transfer":
            //        var empty_inter_transfer_to_opd = GetEmptyTextField(Constant.ED_DI0_INHOTRAN_CODE, request_general);
            //        uncomplete_field.AddRange(empty_inter_transfer_to_opd);
            //        break;

            //    case "Upstream/Downstream transfer":
            //        var empty_updo_transfer_to_opd = GetEmptyTextField(Constant.ED_DI0_UPDOTRAN_CODE, request_general);
            //        uncomplete_field.AddRange(empty_updo_transfer_to_opd);

            //        var refotran_empty = GetEmptyChoiceField(Constant.ED_DI0_REFOTRAN_CODE["QUES"], Constant.ED_DI0_REFOTRAN_CODE["ANS"], request_general);
            //        if (refotran_empty != null)
            //            uncomplete_field.Add(refotran_empty);
            //        break;

            //    default:
            //        break;
            //}
            if (visitVersion >= 10)
            {
                for (int i = 0; i < uncomplete_field.Count; i++)
                {
                    if (uncomplete_field[i].Code == "DI0DIAICD")
                    {
                        uncomplete_field[i].ViName = "Mã ICD10 bệnh chính";
                        uncomplete_field[i].EnName = "ICD10 Primary diagnosis";
                    }
                    if (uncomplete_field[i].Code == "DI0DIAANS")
                    {
                        uncomplete_field[i].ViName = "Chẩn đoán bệnh chính (Hiển thị trên báo cáo y tế)";
                        uncomplete_field[i].EnName = "Primary diagnosis (Show in Medical Report)";
                    }
                }
            }

            return uncomplete_field.OrderBy(e => e.Order).ToList();
        }

        private List<MedicalReportDataModels> GetDischargeInformationData(Guid? form_id)
        {
            return (from master in unitOfWork.MasterDataRepository.AsQueryable().Where(
                                e => !e.IsDeleted &&
                                !string.IsNullOrEmpty(e.Form) &&
                                e.Form == "DI0"
                              )
                    join data in unitOfWork.DischargeInformationDataRepository.AsQueryable().Where(
                      e => !e.IsDeleted &&
                      e.DischargeInformationId != null &&
                      e.DischargeInformationId == form_id
                    ) on master.Code equals data.Code into dlist
                    from data in dlist.DefaultIfEmpty()
                    select new MedicalReportDataModels
                    {
                        Code = master.Code,
                        EnName = master.EnName,
                        ViName = master.ViName,
                        Value = data.Value,
                        Order = master.Order
                    }).ToList();
        }
        private List<MedicalReportDataModels> GetEmptyTextField(string[] code, List<MedicalReportDataModels> datas)
        {
            return datas.Where(
                e => !string.IsNullOrEmpty(e.Code) &&
                code.Contains(e.Code) &&
                string.IsNullOrEmpty(e.Value)
            ).ToList();
        }
        private List<MedicalReportDataModels> GetEmptyTextField(string[] request_code, JToken request_data)
        {
            var masters = unitOfWork.MasterDataRepository.Find(
                e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && request_code.Contains(e.Code)
            ).Select(e => new MedicalReportDataModels
            {
                Code = e.Code,
                ViName = e.ViName,
                EnName = e.EnName,
                Order = e.Order,
            }).ToList();

            List<MedicalReportDataModels> gen_field = new List<MedicalReportDataModels>();
            foreach (var item in request_data)
            {
                var code = item["Code"]?.ToString();
                if (code == null || !request_code.Contains(code))
                    continue;

                var value = item["Value"]?.ToString();
                if (!string.IsNullOrEmpty(value))
                    continue;

                var data_master = masters.FirstOrDefault(e => e.Code == code);
                if (data_master != null)
                    gen_field.Add(data_master);
            }
            return gen_field;
        }


        private MedicalReportDataModels GetEmptyChoiceField(string group_code, string[] code, List<MedicalReportDataModels> datas)
        {
            var is_true = datas.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                code.Contains(e.Code) &&
                !string.IsNullOrEmpty(e.Value) &&
                e.Value.ToLower() == "true"
            );
            if (is_true != null)
                return null;

            return datas.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                group_code == e.Code
            );
        }
        private MedicalReportDataModels GetEmptyChoiceField(string group_code, string[] request_code, JToken request_data)
        {
            var masters = unitOfWork.MasterDataRepository.Find(
                e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code) && (group_code == e.Group || group_code == e.Code)
            ).Select(e => new MedicalReportDataModels
            {
                Code = e.Code,
                ViName = e.ViName,
                EnName = e.EnName,
                Order = e.Order,
            }).ToList();

            var is_empty = true;
            foreach (var item in request_data)
            {
                var code = item["Code"]?.ToString();
                if (code == null || !request_code.Contains(code))
                    continue;

                var value = item["Value"]?.ToString();
                if (string.IsNullOrEmpty(value) || value.Trim().ToLower() != "true")
                    continue;

                is_empty = false;
                break;
            }

            if (is_empty)
                return masters.FirstOrDefault(
                    e => !string.IsNullOrEmpty(e.Code) &&
                    group_code == e.Code
                );
            return null;
        }
    }
}
