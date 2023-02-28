using DataAccess.Models;
using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOPatientOwnMedicationsChartController : BaseApiController
    {
        protected EIOPatientOwnMedicationsChart GetEIOPatientOwnMedicationsChart(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOPatientOwnMedicationsChartRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }

        protected EIOPatientOwnMedicationsChart CreateEIOPatientOwnMedicationsChart(Guid visit_id, string visit_type)
        {
            var med_chart = new EIOPatientOwnMedicationsChart()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type
            };
            unitOfWork.EIOPatientOwnMedicationsChartRepository.Add(med_chart);
            unitOfWork.Commit();
            return med_chart;
        }

        protected dynamic GetPatientOwnMedicationsChartResult(EIOPatientOwnMedicationsChart med_chart, Customer customer, bool IsLocked = false)
        {
            var specialty = GetSpecialty();
            var head_of_department = med_chart.HeadOfDepartment;
            var head_of_pharmacy = med_chart.HeadOfPharmacy;
            var doctor = med_chart.Doctor;

            var datas = med_chart.EIOPatientOwnMedicationsChartDatas.Where(e => !e.IsDeleted).OrderBy(e => e.CreatedAt).Select(e => new
            {
                e.Id,
                e.DrugName,
                e.Unit,
                e.Quantity,
                e.LotNo,
                ExpiryDate = e.ExpiryDate?.ToString(Constant.DATE_FORMAT),
                e.StorageOrigin,
                e.CreatedBy,
                e.Note
            });

            return new
            {
                med_chart.Id,
                med_chart.FirstTotal,
                med_chart.DoctorOpinion,
                med_chart.ClinicalPharmacistReview,
                med_chart.StorageDrugsAtPharmacy,
                med_chart.SecondTotal,
                med_chart.Upload,
                Specialty = new { specialty?.ViName, specialty?.EnName },
                Customer = new
                {
                    customer?.Fullname,
                    DateOfBirth = customer?.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    customer?.PID,
                },
                HeadOfDepartmentTime = med_chart.HeadOfDepartmentTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfDepartment = new { head_of_department?.Id, head_of_department?.Username, head_of_department?.Fullname, head_of_department?.DisplayName, head_of_department?.Title },
                HeadOfPharmacyTime = med_chart.HeadOfPharmacyTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfPharmacy = new { head_of_pharmacy?.Id, head_of_pharmacy?.Username, head_of_pharmacy?.Fullname, head_of_pharmacy?.DisplayName, head_of_pharmacy?.Title },
                DoctorTime = med_chart.DoctorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Doctor = new { doctor?.Id, doctor?.Username, doctor?.Fullname, doctor?.DisplayName, doctor?.Title },
                Datas = datas,
                IsLocked
            };
        }

        protected void HandleUpdateOrCreatePatientOwnMedicationsChart(EIOPatientOwnMedicationsChart med_chart, JObject request)
        {
            if (med_chart.HeadOfDepartmentId == null && med_chart.HeadOfPharmacyId == null && med_chart.DoctorId == null)
            {
                var user = GetUser();
                med_chart.FirstTotal = request["FirstTotal"]?.ToString();
                med_chart.DoctorOpinion = request["DoctorOpinion"]?.ToString();
                med_chart.ClinicalPharmacistReview = request["ClinicalPharmacistReview"]?.ToString();
                med_chart.StorageDrugsAtPharmacy = request["StorageDrugsAtPharmacy"]?.ToObject<bool?>();
                med_chart.SecondTotal = request["SecondTotal"]?.ToString();
                var datas = med_chart.EIOPatientOwnMedicationsChartDatas.Where(e => !e.IsDeleted).ToList();
                foreach (var item in request["Datas"])
                {
                    var str_id = item.Value<string>("Id");
                    var data = GetOrCreateEIOPatientOwnMedicationsChartData(item.Value<string>("Id"), med_chart.Id, datas);
                    if (data != null && user.Username == data.CreatedBy)
                        UpdateEIOPatientOwnMedicationsChartData(data, item);
                }
            }
            med_chart.Upload = request["Upload"]?.ToString();
            unitOfWork.EIOPatientOwnMedicationsChartRepository.Update(med_chart);
            unitOfWork.Commit();
        }

        private EIOPatientOwnMedicationsChartData GetOrCreateEIOPatientOwnMedicationsChartData(string str_id, Guid med_id, List<EIOPatientOwnMedicationsChartData> datas)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                var data = new EIOPatientOwnMedicationsChartData()
                {
                    EIOPatientOwnMedicationsChartId = med_id,
                };
                unitOfWork.EIOPatientOwnMedicationsChartDataRepository.Add(data);
                return data;
            }

            Guid data_id = new Guid(str_id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }

        private void UpdateEIOPatientOwnMedicationsChartData(EIOPatientOwnMedicationsChartData data, JToken item)
        {
            data.DrugName = item.Value<string>("DrugName");
            data.Unit = item.Value<string>("Unit");
            data.Quantity = item.Value<string>("Quantity");
            data.LotNo = item.Value<string>("LotNo");
            var time = item.Value<string>("ExpiryDate");
            if (!string.IsNullOrEmpty(time))
                data.ExpiryDate = DateTime.ParseExact(time, Constant.DATE_FORMAT, null);
            else
                data.ExpiryDate = null;
            data.StorageOrigin = item.Value<string>("StorageOrigin");
            data.Note = item.Value<string>("Note");
            unitOfWork.EIOPatientOwnMedicationsChartDataRepository.Update(data);
        }

        protected dynamic ConfirmPatientOwnMedicationsChart(EIOPatientOwnMedicationsChart med_chart, JObject request)
        {
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var kind = request["kind"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null || string.IsNullOrEmpty(kind))
                return Message.INFO_INCORRECT;            
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);            
            if (kind == "HeadOfDepartment" && positions.Contains("Head Of Department") && med_chart.HeadOfDepartmentId == null)
            {
                med_chart.HeadOfDepartmentTime = DateTime.Now;
                med_chart.HeadOfDepartmentId = user.Id;
            }
            else if (kind == "HeadOfPharmacy" && positions.Contains("Head Of Department") && med_chart.HeadOfPharmacyId == null)
            {
                med_chart.HeadOfPharmacyTime = DateTime.Now;
                med_chart.HeadOfPharmacyId = user.Id;
            }
            else if (kind == "Doctor" && positions.Contains("Doctor") && med_chart.HeadOfPharmacyId == null)
            {
                med_chart.DoctorTime = DateTime.Now;
                med_chart.DoctorId = user.Id;
            }
            else
                return Message.FORBIDDEN;

            unitOfWork.EIOPatientOwnMedicationsChartRepository.Update(med_chart);
            unitOfWork.Commit();
            return null;
        }
    }
}
