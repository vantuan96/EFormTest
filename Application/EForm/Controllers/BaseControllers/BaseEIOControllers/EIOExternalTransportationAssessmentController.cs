using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Models.EIOModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOExternalTransportationAssessmentController : BaseApiController
    {
        protected EIOExternalTransportationAssessment CreateEIOExternalTransportationAssessment(Guid visit_id, string visit_type)
        {
            var et_assessment = new EIOExternalTransportationAssessment()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type
            };
            unitOfWork.EIOExternalTransportationAssessmentRepository.Add(et_assessment);

            CreateEIOExternalTransportationAssessmentEquipment("Bình oxy", et_assessment.Id);
            CreateEIOExternalTransportationAssessmentEquipment("Hệ thống các ống, dẫn lưu cần cố định và đã được kiểm tra bởi bác sỹ", et_assessment.Id);
            CreateEIOExternalTransportationAssessmentEquipment("Hệ thống đường truyền và dịch truyền đầy đủ", et_assessment.Id);
            CreateEIOExternalTransportationAssessmentEquipment("Thuốc cấp cứu, an thần, giảm đau đầy đủ", et_assessment.Id);
            CreateEIOExternalTransportationAssessmentEquipment("Thiết bị y tế (ghi cụ thể trong “Bảng kiểm bàn giao dụng cụ vận chuyển ngoại viện”)", et_assessment.Id);

            unitOfWork.Commit();
            return et_assessment;
        }
        private void CreateEIOExternalTransportationAssessmentEquipment(string name, Guid form_id)
        {
            var equip = new EIOExternalTransportationAssessmentEquipment()
            {
                EIOExternalTransportationAssessmentId = form_id,
                Name = name
            };
            unitOfWork.EIOExternalTransportationAssessmentEquipmentRepository.Add(equip);
        }
        protected EIOExternalTransportationAssessment GetEIOExternalTransportationAssessment(Guid visit_id, Guid itemId, string visit_type)
        {
            return unitOfWork.EIOExternalTransportationAssessmentRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                e.Id == itemId &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }

        protected List<EIOExternalTransportationAssessment> GetListEIOExternalTransportationAssessment(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOExternalTransportationAssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            ).OrderBy(e => e.CreatedAt).ToList();
        }
        protected dynamic GetExternalTransportationAssessmentResult(EIOExternalTransportationAssessment ex_assessment)
        {
            var datas = ex_assessment.EIOExternalTransportationAssessmentDatas
                .Where(e => !e.IsDeleted)
                .Select(e => new { e.Id, e.Code, e.Value});

            var equipments = ex_assessment.EIOExternalTransportationAssessmentEquipments
                .Where(e => !e.IsDeleted).OrderBy(e=> e.CreatedAt)
                .Select(e => new { e.Id, e.Name, e.IsNeeded, e.Note });

            var doctor = ex_assessment.Doctor;
            var nurse = ex_assessment.Nurse;

            bool isLocked = false;
            if(ex_assessment.VisitTypeGroupCode == "IPD")
            {
                var visit = GetIPD((Guid)ex_assessment.VisitId);
                isLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangDanhGiaNhuCauTrangThietBi, ex_assessment.Id);
            }          
            return new
            {
                ex_assessment.Id,
                DoctorTime = ex_assessment.DoctorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Doctor = new { doctor?.Id, doctor?.Username, doctor?.Fullname, doctor?.DisplayName, doctor?.Title },
                NurseTime = ex_assessment.NurseTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Nurse = new { nurse?.Id, nurse?.Username, nurse?.Fullname, nurse?.DisplayName, nurse?.Title },
                Datas = datas,
                Equipments = equipments,
                IsLocked = isLocked
            };
        }

        protected dynamic GetListExternalTransportationAssessmentResult(List<EIOExternalTransportationAssessment> listEIOExternalTransportationAssessment)
        {
            List<EIOExternalTransportationAssessmentItemModel> listItems = new List<EIOExternalTransportationAssessmentItemModel>();
            if (listEIOExternalTransportationAssessment.Count > 0)
            {
                foreach (var item in listEIOExternalTransportationAssessment)
                {
                    var doctor = item.Doctor;
                    var nurse = item.Nurse;

                    var datas = item.EIOExternalTransportationAssessmentDatas
                        .Where(e => !e.IsDeleted)
                        .Select(e => new { e.Id, e.Code, e.Value }).ToList();

                    var equipments = item.EIOExternalTransportationAssessmentEquipments
                        .Where(e => !e.IsDeleted).OrderBy(e => e.CreatedAt)
                        .Select(e => new { e.Id, e.Name, e.IsNeeded, e.Note });

                    bool isLocked = false;
                    if (item.VisitTypeGroupCode == "IPD")
                    {
                        var visit = GetIPD((Guid)item.VisitId);
                        isLocked = IPDIsBlock(visit, Constant.IPDFormCode.BangDanhGiaNhuCauTrangThietBi);
                    }

                    EIOExternalTransportationAssessmentItemModel result = new EIOExternalTransportationAssessmentItemModel
                    {
                        Id = item.Id,
                        DoctorTime = item.DoctorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                        Doctor = new User { Username = doctor?.Username,  Fullname = doctor?.Fullname, DisplayName = doctor?.DisplayName, Title = doctor?.Title },
                        NurseTime = item.NurseTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                        Nurse = new User { Username = nurse?.Username, Fullname =  nurse?.Fullname, DisplayName = nurse?.DisplayName, Title = nurse?.Title },
                        Datas = datas,
                        Equipments = equipments,
                        IsLocked = isLocked
                    };

                    listItems.Add(result);
                }

                
            }

            return listItems;
        }
        protected void HandleUpdateOrCreateExternalTransportationAssessment(EIOExternalTransportationAssessment ex_assessment, List<string> position, JObject request, dynamic visit)
        {
            var need_notify = false;
            var datas = ex_assessment.EIOExternalTransportationAssessmentDatas.Where(e => !e.IsDeleted).ToList();
            if (request["Datas"] != null)
            {
                foreach (var item in request["Datas"])
                {
                    var code = item["Code"]?.ToString();
                    if (string.IsNullOrEmpty(code)) continue;

                    var data = GetOrCreateEIOExternalTransportationAssessmentData(code, ex_assessment.Id, datas);
                    if (data == null) continue;

                    var value = item["Value"]?.ToString();
                    var notify = UpdateEIOExternalTransportationAssessmentData(data, code, value, position);
                    need_notify |= notify;
                }
            }
            


            if (position.Contains("Nurse"))
            {
                var equipments = ex_assessment.EIOExternalTransportationAssessmentEquipments.Where(e => !e.IsDeleted).ToList();
                if (request["Equipments"] != null)
                {
                    foreach (var item in request["Equipments"])
                    {
                        var data = GetOrCreateEIOExternalTransportationAssessmentEquipment(item["Id"]?.ToString(), ex_assessment.Id, equipments);
                        if (data == null) continue;
                        UpdateEIOExternalTransportationAssessmentEquipment(data, item);
                    }
                }
                
            }

            unitOfWork.Commit();
            if (need_notify)
            {
                if(ex_assessment.VisitTypeGroupCode == "ED")
                {
                    PushEDNotification(visit);
                }
                else if (ex_assessment.VisitTypeGroupCode == "IPD")
                {
                    PushIPDNotification(visit);
                }
            }
            
        }
        private EIOExternalTransportationAssessmentData GetOrCreateEIOExternalTransportationAssessmentData(string code, Guid form_id, List<EIOExternalTransportationAssessmentData> datas)
        {
            var data = datas.FirstOrDefault(
                e => !string.IsNullOrEmpty(e.Code) &&
                e.Code == code
            );
            if (data != null) return data;

            data = new EIOExternalTransportationAssessmentData()
            {
                EIOExternalTransportationAssessmentId = form_id,
                Code = code,
            };
            unitOfWork.EIOExternalTransportationAssessmentDataRepository.Add(data);
            return data;
        }
        private bool UpdateEIOExternalTransportationAssessmentData(EIOExternalTransportationAssessmentData data, string code, string value, List<string> position)
        {
            var is_change = data.Value != value;
            if (((position.Contains("Doctor") && Constant.EIO_AEFET_DOCTOR_CODE.Contains(code)) || (position.Contains("Nurse") && !Constant.EIO_AEFET_DOCTOR_CODE.Contains(code))) && is_change)
            { 
                data.Value = value;
                unitOfWork.EIOExternalTransportationAssessmentDataRepository.Update(data);
            }

            if (
                !position.Contains("Nurse") || 
                !Constant.EIO_AEFET_NURSE_RED_CODE.Contains(code) ||
                !is_change || 
                value.ToLower() != "true"
            ) 
                return false;

            return true;
        }
        private EIOExternalTransportationAssessmentEquipment GetOrCreateEIOExternalTransportationAssessmentEquipment(string id, Guid form_id, List<EIOExternalTransportationAssessmentEquipment> datas)
        {
            if (string.IsNullOrEmpty(id))
            {
                var new_data = new EIOExternalTransportationAssessmentEquipment()
                {
                    EIOExternalTransportationAssessmentId = form_id
                };
                unitOfWork.EIOExternalTransportationAssessmentEquipmentRepository.Add(new_data);
                return new_data;
            };

            var data_id = new Guid(id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateEIOExternalTransportationAssessmentEquipment(EIOExternalTransportationAssessmentEquipment data, JToken item)
        {
            data.Name = item["Name"]?.ToString();
            var is_needed = item["IsNeeded"]?.ToString();
            if(string.IsNullOrEmpty(is_needed))
                data.IsNeeded = null;
            else
                data.IsNeeded = item["IsNeeded"]?.ToObject<bool>();
            data.Note = item["Note"]?.ToString();
            unitOfWork.EIOExternalTransportationAssessmentEquipmentRepository.Update(data);
        }
        protected dynamic ConfirmExternalTransportationAssessment(EIOExternalTransportationAssessment et_assessment, JObject request)
        {
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Message.INFO_INCORRECT; 
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (kind == "Doctor" && positions.Contains("Doctor") && et_assessment.DoctorId == null)
            {
                et_assessment.DoctorTime = DateTime.Now;
                et_assessment.DoctorId = user.Id;
            }
            else if (kind == "Nurse" && positions.Contains("Nurse") && et_assessment.NurseId == null)
            {
                et_assessment.NurseTime = DateTime.Now;
                et_assessment.NurseId = user.Id;
            }
            else
                return Message.FORBIDDEN;

            unitOfWork.EIOExternalTransportationAssessmentRepository.Update(et_assessment);
            unitOfWork.Commit();
            return null;
        }

        private void PushEDNotification(ED ed)
        {
            var di = ed.DischargeInformation;
            if (di.CreatedAt == di.UpdatedAt)
                return;

            var user = GetUser();
            var spec = ed.Specialty;
            var customer = ed.Customer;
            var noti_creator = new NotificationCreator(
                unitOfWork: unitOfWork,
                from_user: user?.Username,
                to_user: di?.UpdatedBy,
                priority: 7,
                vi_message: $"<b>[ED-{spec?.ViName}]</b> Bạn nhận được yêu cầu tái đánh giá Bảng đánh giá nhu cầu trang thiết bị/nhân lực vận chuyển ngoại viện của bệnh nhân <b>{customer.Fullname}</b> từ điều dưỡng <b>{ed.CurrentNurse.Fullname}</b>",
                en_message: $"<b>[ED-{spec?.ViName}]</b> Bạn nhận được yêu cầu tái đánh giá Bảng đánh giá nhu cầu trang thiết bị/nhân lực vận chuyển ngoại viện của bệnh nhân <b>{customer.Fullname}</b> từ điều dưỡng <b>{ed.CurrentNurse.Fullname}</b>",
                spec_id: spec?.Id,
                visit_id: ed.Id,
                group_code: "ED",
                form_frontend: "ExternalTransportationAssessment"
            );
            noti_creator.Create();
        }

        private void PushIPDNotification(IPD ipd)
        {
            var user = GetUser();
            var spec = ipd.Specialty;
            var customer = ipd.Customer;
            if (ipd != null && ipd.PrimaryDoctor != null)
            {
                var noti_creator = new NotificationCreator(
                    unitOfWork: unitOfWork,
                    from_user: user?.Username,
                    to_user: ipd?.PrimaryDoctor.Username,
                    priority: 7,
                    vi_message: $"<b>[IPD-{spec?.ViName}]</b> Bạn nhận được yêu cầu tái đánh giá Bảng đánh giá nhu cầu trang thiết bị/nhân lực vận chuyển ngoại viện của bệnh nhân <b>{customer.Fullname}</b> từ điều dưỡng <b>{user.Fullname}</b>",
                    en_message: $"<b>[IPD-{spec?.ViName}]</b> Bạn nhận được yêu cầu tái đánh giá Bảng đánh giá nhu cầu trang thiết bị/nhân lực vận chuyển ngoại viện của bệnh nhân <b>{customer.Fullname}</b> từ điều dưỡng <b>{user.Fullname}</b>",
                    spec_id: spec?.Id,
                    visit_id: ipd.Id,
                    group_code: "IPD",
                    form_frontend: "ExternalTransportationAssessment"
                );
                noti_creator.Create();
            }
            
        }
    }
}
