using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.OPDModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOStandingOrderForRetailServiceController: BaseApiController
    {
        private InHospital in_hospital = new InHospital();

        protected EIOStandingOrderForRetailService GetRetailService(dynamic visit, string visit_type)
        {
            if (visit_type == "ED")
                return GetEDRetailService(visit);

            if (visit_type == "OPD")
                return GetOPDRetailService(visit);

            return null;
        }
        private EIOStandingOrderForRetailService GetEDRetailService(ED ed)
        {
            var sofrs = ed.EDStandingOrderForRetailService;
            if (sofrs == null || sofrs.IsDeleted)
                return null;

            var afrsp = ed.EDAssessmentForRetailServicePatient;
            var afrsp_data = afrsp.EIOAssessmentForRetailServicePatientDatas.Where(e => !e.IsDeleted);
            var pulse = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPPULANS")?.Value;
            var temparature = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPTEMANS")?.Value;
            var blood_pressure = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPBP0ANS")?.Value;
            var height = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPHEIANS")?.Value;
            var weight = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPWEIANS")?.Value;
            // var isTestCovid2 = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "IEOTESTSARCCOV2ANS")?.Value;
            var emer = ed.EmergencyRecord;
            var diagnosis = emer.EmergencyRecordDatas.FirstOrDefault((e => !string.IsNullOrEmpty(e.Code) && e.Code == "ER0ID0ANS"))?.Value;
            string doctor = string.Empty;
            if (!IsNew(emer.CreatedAt, emer.UpdatedAt))
                doctor = emer.UpdatedBy;

            var discharge_info = ed.DischargeInformation;
            var discharge_info_datas = discharge_info.DischargeInformationDatas;
            var discharge_diagnosis = discharge_info_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "DI0DIAANS")?.Value;
            if (!string.IsNullOrEmpty(discharge_diagnosis))
                diagnosis = discharge_diagnosis;

            if (!IsNew(discharge_info.CreatedAt, discharge_info.UpdatedAt))
                doctor = discharge_info.UpdatedBy;

            sofrs.Pulse = pulse;
            sofrs.BloodPressure = blood_pressure;
            sofrs.Temparature = temparature;
            sofrs.Height = height;
            sofrs.Weight = weight;
            if (string.IsNullOrEmpty(sofrs.Diagnosis))
                sofrs.Diagnosis = diagnosis;
            if (string.IsNullOrEmpty(sofrs.Doctor))
                sofrs.Doctor = doctor;
            unitOfWork.EIOStandingOrderForRetailServiceRepository.Update(sofrs);
            unitOfWork.Commit();
            return sofrs;
        }
        private EIOStandingOrderForRetailService GetOPDRetailService(OPD opd)
        {
            var sofrs = opd.EIOStandingOrderForRetailService;
            if (sofrs == null || sofrs.IsDeleted)
                return null;

            var afrsp = opd.EIOAssessmentForRetailServicePatient;
            var afrsp_data = afrsp.EIOAssessmentForRetailServicePatientDatas.Where(e => !e.IsDeleted);
            var pulse = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPPULANS")?.Value;
            var temparature = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPTEMANS")?.Value;
            var blood_pressure = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPBP0ANS")?.Value;
            var height = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPHEIANS")?.Value;
            var weight = afrsp_data.FirstOrDefault(e => !string.IsNullOrEmpty(e.Code) && e.Code == "EDAFRSPWEIANS")?.Value;

            var oen = opd.OPDOutpatientExaminationNote;
            var oen_datas = oen.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted).ToList();
            var initial_diagnosis = oen_datas.FirstOrDefault(e => e.Code == "OPDOENID0ANS")?.Value;
            var diagnosis = oen_datas.FirstOrDefault(e => !e.IsDeleted && e.Code == "OPDOENDD0ANS")?.Value;
            
            var doctor = opd.PrimaryDoctor?.Username;

            sofrs.Pulse = pulse;
            sofrs.BloodPressure = blood_pressure;
            sofrs.Temparature = temparature;
            sofrs.Height = height;
            sofrs.Weight = weight;
            if (string.IsNullOrEmpty(sofrs.Diagnosis))
                sofrs.Diagnosis = !string.IsNullOrEmpty(diagnosis) ? diagnosis : initial_diagnosis;
            if (string.IsNullOrEmpty(sofrs.Doctor))
                sofrs.Doctor = doctor;
            unitOfWork.EIOStandingOrderForRetailServiceRepository.Update(sofrs);
            unitOfWork.Commit();
            return sofrs;
        }

        protected dynamic GetDetailRetailService(dynamic visit, string order_type, EIOStandingOrderForRetailService sofrs, bool isLocked = false)
        {
            Guid visit_id = visit.Id;
            var orders = unitOfWork.OrderRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.OrderType) &&
                e.OrderType == order_type
            ).OrderBy(e => e.CreatedAt).Select(e => new
            {
                e.Id,
                e.MedicationMasterdataId,
                e.Drug,
                e.Route,
                e.Speed,
                e.Note,
                Nurse = e.Nurse?.Username,
                Doctor = e.Doctor?.Username,
                IsLocked = isLocked
            });

            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();

            var doctor = unitOfWork.UserRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Username) &&
                sofrs.Doctor == e.Username
            );
            // Guid EIOAssessmentForRetailServicePatientId = visit.EIOAssessmentForRetailServicePatientId;
            // var isTestCovid2 = unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.FirstOrDefault(e => e.EDAssessmentForRetailServicePatientId == EIOAssessmentForRetailServicePatientId && !string.IsNullOrEmpty(e.Code) && e.Code == "IEOTESTSARCCOV2ANS")?.Value;
            var status = visit.EDStatus;

            var statusss = Constant.InHospital.Concat(Constant.Discharged);

            return new
            {
                Customer = new
                {
                    customer?.Fullname,
                    DateOfBirth = customer?.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    Gender = gender,
                    customer?.PID,
                },
                visit.RecordCode,
                Status = new { status.Id, status.ViName, status.EnName, status.Code },
                ListStatus = unitOfWork.EDStatusRepository.Find(
                    e => !e.IsDeleted &&
                    e.VisitTypeGroupId != null &&
                    order_type.Contains(e.VisitTypeGroup.Code) &&
                    statusss.Contains(e.Code)
                ).OrderBy(e => e.CreatedAt).Select(st => new { st.Id, st.ViName, st.EnName, st.Code }),
                sofrs.Id,
                sofrs.Pulse,
                sofrs.BloodPressure,
                sofrs.Temparature,
                sofrs.Height,
                sofrs.Weight,
                sofrs.Diagnosis,
                sofrs.Doctor,
                DoctorFullname = doctor?.Fullname,
                Datas = orders,
                IsLocked = isLocked
            };
        }

        protected void UpdateRetailService(EIOStandingOrderForRetailService sofrs, JObject request)
        {
            sofrs.Diagnosis = request["Diagnosis"]?.ToString();
            sofrs.Doctor = request["Doctor"]?.ToString();
            unitOfWork.EIOStandingOrderForRetailServiceRepository.Update(sofrs);
            unitOfWork.Commit();
        }

        protected void HandleUpdateOrCreateOrderData(Guid visit_id, string order_type, JToken request_data)
        {
            var order_datas = unitOfWork.OrderRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.OrderType) &&
                e.OrderType == order_type
            ).ToList();
            foreach (var item in request_data)
            {
                Order order = GetOrCreateOrder(visit_id, order_type, order_datas, item);
                if (order != null)
                    UpdateOrder(order, item);
            }
            unitOfWork.Commit();
        }
        private Order GetOrCreateOrder(Guid visit_id, string order_type, List<Order> datas, JToken order_data)
        {
            var raw_id = order_data["Id"]?.ToString();
            Guid? id = CreateId(raw_id);
            Order order = null;
            if (id != null)
                order = datas.FirstOrDefault(e => e.Id == id);
            if (order == null)
            {
                order = new Order()
                {
                    VisitId = visit_id,
                    OrderType = order_type,
                };
                unitOfWork.OrderRepository.Add(order);
            }
            return order;
        }
        private void UpdateOrder(Order order, JToken order_data)
        {
            order.Drug = order_data.Value<string>("Drug");
            order.Route = order_data.Value<string>("Route");
            order.Speed = order_data.Value<string>("Speed");
            order.Note = order_data.Value<string>("Note");
            unitOfWork.OrderRepository.Update(order);
        }

        protected dynamic HandleStatus(dynamic visit, string visit_type, JObject request)
        {
            var status = request["Status"]["Id"].ToString();
            Guid status_id = new Guid(status);

            if (status_id == Guid.Empty) return null;

            var status_code = request["Status"]["Code"].ToString();
            if (visit.EDStatusId != status_id)
            {
                if (Constant.InHospital.Contains(status_code))
                {
                    Guid visit_id = visit.Id;
                    Guid customer_id = (Guid)visit.CustomerId;
                    string customer_PID = visit.Customer.PID;

                    this.in_hospital.SetState(customer_id, visit_id, null, null);
                    var in_hospital_visit = this.in_hospital.GetVisit();
                    if (in_hospital_visit != null)
                        return new
                        {
                            Code = HttpStatusCode.BadRequest,
                            Message = this.in_hospital.BuildErrorMessage(in_hospital_visit)
                        };

                    dynamic in_waiting_visit;
                    if (!string.IsNullOrEmpty(visit.Customer.PID))
                        in_waiting_visit = GetInWaitingAcceptPatientByPID(pid: customer_PID, visit_id: visit_id);
                    else
                        in_waiting_visit = GetInWaitingAcceptPatientById(customer_id: customer_id, visit_id: visit_id);
                    if (in_waiting_visit != null)
                    {
                        var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                        return BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician);
                    }
                }
                UpdateStatus(visit, visit_type, request["Status"]);
            }
            return null;
        }
        private void UpdateStatus(dynamic visit, string visit_type, JToken status)
        {
            var status_raw = status["Id"].ToString();
            Guid status_id = new Guid(status_raw);
            if (status_id != visit.EDStatusId)
            {
                visit.EDStatusId = status_id;
                var customer = visit.Customer;
                customer.EDStatusId = status_id;
                unitOfWork.CustomerRepository.Update(customer);
                if(visit_type == "ED")
                    unitOfWork.EDRepository.Update(visit);
                else if(visit_type == "OPD")
                    unitOfWork.OPDRepository.Update(visit);
            }
            unitOfWork.Commit();
        }
        private Guid? CreateId(string raw_id)
        {
            try
            {
                Guid id = new Guid(raw_id);
                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}