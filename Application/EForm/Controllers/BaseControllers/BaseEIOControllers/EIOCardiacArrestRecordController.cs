using DataAccess.Models;
using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models.EIOModels;
using EForm.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOCardiacArrestRecordController : BaseApiController
    {
        protected EIOCardiacArrestRecord GetCardiacArrestRecord(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }

        protected EIOCardiacArrestRecord CreateCardiacArrestRecord(Guid visit_id, string visit_type, int version)
        {
            var car = new EIOCardiacArrestRecord()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
                Version = version
            };
            unitOfWork.EIOCardiacArrestRecordRepository.Add(car);
            unitOfWork.Commit();
            return car;
        }

        protected string ConfirmCardiacArrestRecord(EIOCardiacArrestRecord car, JObject request)
        {
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Message.INFO_INCORRECT;            
            var kind = request["kind"]?.ToString();
            if (car.Version == 1)
            {
                if (car.DoctorId != null)
                    return Message.OWNER_FORBIDDEN;

                var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
                if (!positions.Contains("Doctor"))
                    return Message.INFO_INCORRECT;

                car.DoctorComfirm = DateTime.Now;
                car.DoctorId = user.Id;
            }else if(car.Version == 2 && !string.IsNullOrEmpty(kind))
            {
                if (kind == "TeamLeader")
                {
                    if(car.TeamLeaderId != null)
                        return Message.OWNER_FORBIDDEN;

                    car.TeamLeaderTime = DateTime.Now;
                    car.TeamLeaderId = user.Id;
                }
                else if (kind == "Doctor")
                {
                    if (car.FormCompletedId != null)
                        return Message.OWNER_FORBIDDEN;

                    car.FormCompletedTime = DateTime.Now;
                    car.FormCompletedId = user.Id;
                }
            }
            unitOfWork.EIOCardiacArrestRecordRepository.Update(car);
            unitOfWork.Commit();
            return null;
        }

        protected dynamic GetCardiacArrestRecordInfo(EIOCardiacArrestRecord car, Guid? form_id, bool IsLocked = false)
        {
            if (car.Version == 1)
                return GetInfoVer1(car, form_id);
            if(car.Version == 2)
                return GetInfoVer2(car, form_id, IsLocked);
            return null;
        }
        private dynamic GetInfoVer1(EIOCardiacArrestRecord car, Guid? form_id)
        {
            var doctor = car.Doctor;
            var datas = GetCardiacArrestRecordInfoDatas(car.Id);
            if (doctor == null)
            {
                var diagnosis = new Diagnosis(unitOfWork, form_id, car.VisitTypeGroupCode).GetData();
                var car_diagnosis = datas.FirstOrDefault(e => e.Code == "EIOCAARREDIAANS");
                if (!string.IsNullOrEmpty(diagnosis) && car_diagnosis != null)
                {
                    car_diagnosis.Value = diagnosis;
                    unitOfWork.EIOCardiacArrestRecordDataRepository.Update(car_diagnosis);
                    unitOfWork.Commit();
                }
                else if (!string.IsNullOrEmpty(diagnosis) && car_diagnosis == null)
                {
                    car_diagnosis = new EIOCardiacArrestRecordData()
                    {
                        Code = "EIOCAARREDIAANS",
                        Value = diagnosis,
                        Type = Constant.EIO_CAARRE_IF,
                        FormId = car.Id
                    };

                    unitOfWork.EIOCardiacArrestRecordDataRepository.Add(car_diagnosis);
                    unitOfWork.Commit();
                    datas.Add(car_diagnosis);
                }
            }

            return new
            {
                car.Id,
                car.Version,
                DoctorComfirm = car.DoctorComfirm?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Doctor = new { doctor?.Id, doctor?.Fullname, doctor?.Username, doctor?.DisplayName, doctor?.Title },
                Datas = datas.Select(e => new { e.Value, e.Code, e.Id }),
                car.CreatedAt,
                car.CreatedBy,
                car.UpdatedAt,
                car.UpdatedBy
            };
        }
        private dynamic GetInfoVer2(EIOCardiacArrestRecord car, Guid? form_id, bool IsLocked = false)
        {
            var datas = GetCardiacArrestRecordInfoDatas(car.Id);
            if (car.TeamLeaderId == null && car.FormCompletedId == null)
            {
                var diagnosis = new Diagnosis(unitOfWork, form_id, car.VisitTypeGroupCode).GetData();
                var car_diagnosis = datas.FirstOrDefault(e => e.Code == "EIOCAARREDIAANS");
                if (!string.IsNullOrEmpty(diagnosis) && car_diagnosis != null)
                {
                    car_diagnosis.Value = diagnosis;
                    unitOfWork.EIOCardiacArrestRecordDataRepository.Update(car_diagnosis);
                    unitOfWork.Commit();
                }
                else if (!string.IsNullOrEmpty(diagnosis) && car_diagnosis == null)
                {
                    car_diagnosis = new EIOCardiacArrestRecordData()
                    {
                        Code = "EIOCAARREDIAANS",
                        Value = diagnosis,
                        Type = Constant.EIO_CAARRE_IF,
                        FormId = car.Id
                    };

                    unitOfWork.EIOCardiacArrestRecordDataRepository.Add(car_diagnosis);
                    unitOfWork.Commit();
                    datas.Add(car_diagnosis);
                }
            }

            var table_datas = car.EIOCardiacArrestRecordTables.Where(e => !e.IsDeleted).OrderBy(e => e.Time);
            var leader = car.TeamLeader;
            var completed = car.FormCompleted;
            return new
            {
                car.Id,
                car.Version,
                TeamLeaderTime = car.TeamLeaderTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                TeamLeader = new { leader?.Id, leader?.Fullname, leader?.Username, leader?.DisplayName, leader?.Title },
                FormCompletedTime = car.FormCompletedTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                FormCompleted = new { completed?.Id, completed?.Fullname, completed?.Username, completed?.DisplayName, completed?.Title },
                Datas = datas.Select(e => new { e.Value, e.Code, e.Id }),
                Table = table_datas.Select(e => new { 
                    e.Id, Time = e.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND), e.Rhythm, e.Defib, e.Andrenalin, e.Amiodarone, e.Other 
                }),
                IsLocked,
                car.CreatedAt,
                car.CreatedBy,
                car.UpdatedAt,
                car.UpdatedBy
            };
        }
        private List<EIOCardiacArrestRecordData> GetCardiacArrestRecordInfoDatas(Guid car_id)
        {
            return unitOfWork.EIOCardiacArrestRecordDataRepository.Find(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                !string.IsNullOrEmpty(e.Type) &&
                e.Type == Constant.EIO_CAARRE_IF &&
                e.FormId != null &&
                e.FormId == car_id
            ).ToList();
        }


        protected void HandleUpdateOrCreateCardiacArrestRecordInfoData(EIOCardiacArrestRecord car, JToken request)
        {
            var datas = GetCardiacArrestRecordInfoDatas(car.Id);
            foreach (var item in request["Datas"])
            {
                var code = item.Value<string>("Code");
                if (string.IsNullOrEmpty(code))
                    continue;

                var value = item.Value<string>("Value");
                var data = GetOrCreateCardiacArrestRecordInfoData(code, car.Id, datas);
                if (data != null)
                    UpdateCardiacArrestRecordInfoData(data, value);
            }
            if (car.Version == 2)
                HandleUpdateOrCreateTableDataVer2(car, request["Table"]);
            var user = GetUser();
            car.UpdatedBy = user.Username;
            unitOfWork.EIOCardiacArrestRecordRepository.Update(car);
            unitOfWork.Commit();
        }
        private EIOCardiacArrestRecordData GetOrCreateCardiacArrestRecordInfoData(string code, Guid car_id, List<EIOCardiacArrestRecordData> datas)
        {
            var data = datas.FirstOrDefault(e => e.Code == code);
            if (data == null)
            {
                data = new EIOCardiacArrestRecordData()
                {
                    Code = code,
                    Type = Constant.EIO_CAARRE_IF,
                    FormId = car_id
                };
                unitOfWork.EIOCardiacArrestRecordDataRepository.Add(data);
            }
            return data;
        }
        private void UpdateCardiacArrestRecordInfoData(EIOCardiacArrestRecordData data, string value)
        {
            data.Value = value;
            unitOfWork.EIOCardiacArrestRecordDataRepository.Update(data);
        }
        private void HandleUpdateOrCreateTableDataVer2(EIOCardiacArrestRecord car, JToken request)
        {
            var table_data = car.EIOCardiacArrestRecordTables.Where(e => !e.IsDeleted).ToList();
            var user = GetUser();
            foreach (var item in request)
            {
                var item_id = item.Value<string>("Id");
                var data = GetOrCreateTableDataVer2(item_id, car.Id, table_data);
                if (data == null) continue;

                UpdateTableDataVer2(data, item);
            }
            unitOfWork.Commit();
        }
        private EIOCardiacArrestRecordTable GetOrCreateTableDataVer2(string item_id, Guid form_id, List<EIOCardiacArrestRecordTable> data)
        {
            if (string.IsNullOrEmpty(item_id))
            {
                var new_data = new EIOCardiacArrestRecordTable()
                {
                    EIOCardiacArrestRecordId = form_id,
                };
                unitOfWork.EIOCardiacArrestRecordTableRepository.Add(new_data);
                return new_data;
            }
            var id = new Guid(item_id);
            return data.FirstOrDefault(e => e.Id == id);
        }
        private void UpdateTableDataVer2(EIOCardiacArrestRecordTable data, JToken item)
        {
            var time = item.Value<string>("Time");
            if (!string.IsNullOrEmpty(time))
                data.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                data.Time = null;
            data.Rhythm = item["Rhythm"]?.ToString();
            data.Defib = item["Defib"]?.ToString();
            data.Andrenalin = item["Andrenalin"]?.ToString();
            data.Amiodarone = item["Amiodarone"]?.ToString();
            data.Other = item["Other"]?.ToString();
            unitOfWork.EIOCardiacArrestRecordTableRepository.Update(data);
        }


        protected dynamic CreateFirstEIOCardiacArrestRecordTable(Guid car_id)
        {
            var cart = new EIOCardiacArrestRecordTable()
            {
                EIOCardiacArrestRecordId = car_id,
                Time = DateTime.Now
            };
            unitOfWork.EIOCardiacArrestRecordTableRepository.Add(cart);


            var vital_sign = new List<EIOCardiacArrestRecordData>()
            {
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_VS, "VS01","Mạch/Nhịp tim(Nhịp /Phút)",false, 0),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_VS, "VS02","Huyết áp(mmHg)", false, 1),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_VS, "VS03","Nhịp thở(Lần /Phút)", false, 2),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_VS, "VS04","SpO2(%)", false, 3),
            };
            var defib_energy = new List<EIOCardiacArrestRecordData>()
            {
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_DE, "DE01", "Loại (Mono/Bi)", false, 1),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_DE, "DE02", "Cường độ (J)", false, 2),
            };
            var medication_bolus = new List<EIOCardiacArrestRecordData>()
            {
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_MB, "MB01", "Adrenalin, 1mg/ml", false, 0),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_MB, "MB02", "Atropin sulphate, 0.25mg/ml", false, 1),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_MB, "MB03", "Cordarone Inj., 150mg/3ml", false, 2),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_MB, "MB04", "Adenosine, 20mg/2ml", false, 3),
            };
            var medication_infusion = new List<EIOCardiacArrestRecordData>()
            {
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_MI, "MI01", "Adrenalin, 1mg/ml", true, 0),
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_MI, "MI02", "Noradrenaline base, 1mg/ml", true, 1),
            };
            var other = new List<EIOCardiacArrestRecordData>()
            {
                CreateEIOCardiacArrestRecordTableData(car_id, cart.Id, Constant.EIO_CAARRE_OT, "OT01", "", true, 0),
            };

            unitOfWork.Commit();

            return new
            {
                cart.Id,
                Time = cart.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                cart.CreatedBy,
                VitalSign = vital_sign.Select(e => new { e.Id, e.Type, e.Code, e.Value }),
                DefibEnergy = defib_energy.Select(e => new { e.Id, e.Type, e.Code, e.Value }),
                MedicationBolus = medication_bolus.Select(e => new { e.Id, e.Type, e.Code, e.Value }),
                MedicationInfusion = medication_infusion.Select(e => new { e.Id, e.Type, e.Code, e.Value }),
                Other = other.Select(e => new { e.Id, e.Type, e.Code, e.Value }),
            };
        }


        protected dynamic GetEIOCardiacArrestRecordTable(EIOCardiacArrestRecord car)
        {
            if (car.Version == 1)
                return GetTableVer1(car);
            return null;
        }
        private dynamic GetTableVer1(EIOCardiacArrestRecord car)
        {
            var doctor = car.Doctor;

            var tables = car.EIOCardiacArrestRecordTables.Where(e => !e.IsDeleted).OrderBy(e => e.Time).ToList();

            var datas = car.EIOCardiacArrestRecordDatas.
                Where(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Type) && e.Type != Constant.EIO_CAARRE_IF && e.FormId != null).
                OrderBy(e => e.CreatedAt).ToList();

            var table_datas = new List<EIOCardiacArrestRecordTableModel>();

            foreach (var tab in tables)
            {
                table_datas.Add(new EIOCardiacArrestRecordTableModel()
                {
                    Id = tab.Id,
                    Time = tab.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    CreatedBy = tab.CreatedBy,
                    Remove = false,
                    VitalSign = datas.Where(e => e.Type == Constant.EIO_CAARRE_VS && e.FormId == tab.Id).
                        OrderBy(e => e.Order).
                        Select(e => new { e.Id, e.Type, e.Code, e.Value, e.Order, Remove = false, e.CreatedBy }),
                    DefibEnergy = datas.Where(e => e.Type == Constant.EIO_CAARRE_DE && e.FormId == tab.Id).
                        OrderBy(e => e.Order).
                        Select(e => new { e.Id, e.Type, e.Code, e.Value, e.Order, Remove = false, e.CreatedBy }),
                    MedicationBolus = datas.Where(e => e.Type == Constant.EIO_CAARRE_MB && e.FormId == tab.Id).
                        OrderBy(e => e.Order).
                        Select(e => new { e.Id, e.Type, e.Code, e.Value, e.Order, Remove = false, e.CreatedBy }),
                    MedicationInfusion = datas.Where(e => e.Type == Constant.EIO_CAARRE_MI && e.FormId == tab.Id).
                        OrderBy(e => e.Order).
                        Select(e => new { e.Id, e.Type, e.Code, e.Value, e.Order, Remove = false, e.CreatedBy }),
                    Other = datas.Where(e => e.Type == Constant.EIO_CAARRE_OT && e.FormId == tab.Id).
                        OrderBy(e => e.Order).
                        Select(e => new { e.Id, e.Type, e.Code, e.Value, e.Order, Remove = false, e.CreatedBy }),
                });
            }

            return new
            {
                car.Id,
                car.Note,
                car.Version,
                DoctorComfirm = car.DoctorComfirm?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Doctor = new { doctor?.Id, doctor?.Fullname, doctor?.Username, doctor?.DisplayName, doctor?.Title },
                Datas = table_datas
            };
        }

        protected void HandleUpdateOrCreateCardiacArrestRecordTableData(EIOCardiacArrestRecord car, JToken request)
        {
            if (car.Version == 1)
                HandleUpdateOrCreateTableDataVer1(car, request);
        }

        private void HandleUpdateOrCreateTableDataVer1(EIOCardiacArrestRecord car, JToken request)
        {
            car.Note = request["Note"]?.ToString();
            unitOfWork.EIOCardiacArrestRecordRepository.Update(car);

            var tables = car.EIOCardiacArrestRecordTables.Where(e => !e.IsDeleted).OrderBy(e => e.Time).ToList();

            var datas = car.EIOCardiacArrestRecordDatas.
                Where(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Type) && e.Type != Constant.EIO_CAARRE_IF && e.FormId != null).
                OrderBy(e => e.CreatedAt).ToList();

            var user = GetUser();
            foreach (var item in request["Datas"])
            {
                var item_id = item.Value<string>("Id");
                var is_remove = item.Value<bool>("Remove");
                var data = GetOrCreateCardiacArrestRecordTable(item_id, is_remove, car.Id, tables);
                if (data == null) continue;

                UpdateCardiacArrestRecordTable(data, item, datas, user);
            }
            unitOfWork.Commit();
        }
        private EIOCardiacArrestRecordTable GetOrCreateCardiacArrestRecordTable(string str_id, bool is_remove, Guid car_id, List<EIOCardiacArrestRecordTable> datas)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                if (is_remove)
                    return null;

                var data = new EIOCardiacArrestRecordTable()
                {
                    EIOCardiacArrestRecordId = car_id,
                };
                unitOfWork.EIOCardiacArrestRecordTableRepository.Add(data);
                return data;
            }
            Guid data_id = new Guid(str_id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateCardiacArrestRecordTable(EIOCardiacArrestRecordTable table, JToken item, List<EIOCardiacArrestRecordData> datas, User user)
        {
            if (table.CreatedBy == user.Username)
            {
                var time = item.Value<string>("Time");
                if (!string.IsNullOrEmpty(time))
                    table.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                else
                    table.Time = null;
            }
            UpdateUpdateCardiacArrestRecordTableItem(Constant.EIO_CAARRE_VS, item[Constant.EIO_CAARRE_VS], user, table, datas);
            UpdateUpdateCardiacArrestRecordTableItem(Constant.EIO_CAARRE_DE, item[Constant.EIO_CAARRE_DE], user, table, datas);
            UpdateUpdateCardiacArrestRecordTableItem(Constant.EIO_CAARRE_MB, item[Constant.EIO_CAARRE_MB], user, table, datas);
            UpdateUpdateCardiacArrestRecordTableItem(Constant.EIO_CAARRE_MI, item[Constant.EIO_CAARRE_MI], user, table, datas);
            UpdateUpdateCardiacArrestRecordTableItem(Constant.EIO_CAARRE_OT, item[Constant.EIO_CAARRE_OT], user, table, datas);
        }
        private void UpdateUpdateCardiacArrestRecordTableItem(string type, JToken item, User user, EIOCardiacArrestRecordTable table, List<EIOCardiacArrestRecordData> datas)
        {
            foreach (var i in item)
            {
                var i_id = i.Value<string>("Id");
                var is_remove = i.Value<bool>("Remove");
                var data = GetOrCreateCardiacArrestRecordTableData(i_id, is_remove, table.EIOCardiacArrestRecordId, table.Id, type, datas);
                if (data == null) continue;

                var is_anonymous = false;
                if (data.CreatedBy != user.Username)
                    is_anonymous = true;
                UpdateCardiacArrestRecordTableData(data, i, is_anonymous);
            }
        }
        private EIOCardiacArrestRecordData GetOrCreateCardiacArrestRecordTableData(string str_id, bool is_remove, Guid? car_id, Guid form_id, string type, List<EIOCardiacArrestRecordData> datas)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                if (is_remove)
                    return null;

                var data = new EIOCardiacArrestRecordData()
                {
                    EIOCardiacArrestRecordId = car_id,
                    FormId = form_id,
                    Type = type,
                };
                unitOfWork.EIOCardiacArrestRecordDataRepository.Add(data);
                return data;
            }
            Guid data_id = new Guid(str_id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateCardiacArrestRecordTableData(EIOCardiacArrestRecordData data, JToken item, bool is_anonymous)
        {
            if (!is_anonymous)
            {
                data.Value = item.Value<string>("Value");
                data.Order = item.Value<int>("Order");
            }
            data.Code = item.Value<string>("Code");
            unitOfWork.EIOCardiacArrestRecordDataRepository.Update(data, is_anonymous);
        }
        private EIOCardiacArrestRecordData CreateEIOCardiacArrestRecordTableData(Guid car_id, Guid cart_id, string type, string code, string label, bool editable, int order)
        {
            var data = new EIOCardiacArrestRecordData()
            {
                EIOCardiacArrestRecordId = car_id,
                FormId = cart_id,
                Type = type,
                Code = new JavaScriptSerializer().Serialize(new
                {
                    code,
                    label,
                    value = label,
                    editable,
                    inputs = new List<string>()
                }),
                Order = order,
            };
            unitOfWork.EIOCardiacArrestRecordDataRepository.Add(data);
            return data;
        }
    }
}
