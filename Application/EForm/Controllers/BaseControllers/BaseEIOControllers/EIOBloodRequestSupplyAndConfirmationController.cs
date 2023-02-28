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
    public class EIOBloodRequestSupplyAndConfirmationController : BaseApiController
    {
        protected EIOBloodRequestSupplyAndConfirmation GetBloodRequestSupplyAndConfirmation(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }
        protected EIOBloodRequestSupplyAndConfirmation CreateBloodRequestSupplyAndConfirmation(Guid visit_id, string visit_type, Guid? specialty_id, int app_version)
        {
            var brsac = new EIOBloodRequestSupplyAndConfirmation()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
                SpecialtyId = specialty_id,
                Version = app_version >= 7 ? app_version : 2
            };
            unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Add(brsac);
            unitOfWork.Commit();
            return brsac;
        }
        protected dynamic GetBloodRequestSupplyAndConfirmationDatas(EIOBloodRequestSupplyAndConfirmation brsac, dynamic visit, Guid? form_id, string visit_type, bool IsLocked = false)
        {
            if (form_id != null)
            {
                brsac.Diagnosis = new Diagnosis(unitOfWork, form_id, visit_type).GetData();
                if (visit_type == "ED")
                    brsac.Diagnosis = brsac.Diagnosis + EDGetAndFormatICD10(form_id);
                unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac, is_anonymous: true, is_time_change: false);
                unitOfWork.Commit();
            }
            var customer = visit.Customer;
            var gender = new CustomerUtil(customer).GetGender();
            var dob = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT);

            var specialty = brsac.Specialty;
            var head_of_deft = brsac.HeadOfDept;
            var blood_request_datas = GetBloodRequestDatas(brsac.Id);
            var blood_supply_datas = GetBloodSupplyDatas(brsac.Id);
            var blood_trans_datas = GetBloodTransfusionConfirmationDatas(brsac.Id, (Guid)brsac.VisitId);
            var blood_trans_code = GetListCodeNameBloodTransfusionConfirmation(brsac.Id);

            return new
            {
                brsac.Id,
                brsac.Number,
                brsac.IsFrequently,
                brsac.Diagnosis,
                brsac.BloodTypeABO,
                brsac.BloodTypeRH,
                brsac.TransfusionTime,
                Customer = new { customer.Fullname, customer.PID, DateOfBirth = dob, Gender = gender },
                Specialty = new { specialty?.ViName, specialty?.EnName, specialty?.Id },
                HeadOfDept = new { head_of_deft?.Fullname, head_of_deft?.Username, head_of_deft?.DisplayName, head_of_deft?.Title },
                BloodRequestDatas = blood_request_datas,
                BloodSupplyDatas = blood_supply_datas,
                BloodTransDatas = blood_trans_datas,
                BloodTransListCode = blood_trans_code,
                IsLocked,
                Version = brsac.Version
            };
        }

        #region Purchase request
        protected dynamic GetBloodRequest(EIOBloodRequestSupplyAndConfirmation brsac, Guid? form_id, string visit_type, bool IsLocked = false)
        {
            if (form_id != null)
            {
                brsac.Diagnosis = new Diagnosis(unitOfWork, form_id, visit_type).GetData();
                if (visit_type == "ED")
                    brsac.Diagnosis = brsac.Diagnosis + EDGetAndFormatICD10(form_id);
                unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac, is_anonymous: true, is_time_change: false);
                unitOfWork.Commit();
            }
            var specialty = brsac.Specialty;
            var head_of_deft = brsac.HeadOfDept;
            var datas = GetBloodRequestDatas(brsac.Id);
            var doctorConfirm = brsac.DoctorConfirm;

            return new
            {
                brsac.Number,
                brsac.IsFrequently,
                brsac.Diagnosis,
                brsac.BloodTypeABO,
                brsac.BloodTypeRH,
                brsac.TransfusionTime,
                Specialty = new { specialty?.ViName, specialty?.EnName, specialty?.Id },
                HeadOfDeptTime = brsac.HeadOfDeptTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                HeadOfDept = new
                {
                    FullName = head_of_deft?.Fullname,
                    Username = head_of_deft?.Username,
                    DisplayName = head_of_deft?.DisplayName,
                    Title = head_of_deft?.Title,
                },
                Datas = datas,
                IsLocked,
                DoctorConfirm = new { doctorConfirm?.Fullname, doctorConfirm?.Username, doctorConfirm?.DisplayName, doctorConfirm?.Title },
                DoctorConfirmTime = brsac.DoctorConfirmTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Version = brsac.Version,
                brsac.CreatedBy,
                CreatedAt = brsac.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                brsac.UpdatedBy,
                UpdatedAt = brsac.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
            };
        }
        private dynamic GetBloodRequestDatas(Guid brsac_id)
        {
            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac_id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_PURCHASE
            );
            if (datas != null && datas.Count() > 0)
                if (IsCheckConfirm(brsac_id))
                {
                    foreach (var item in datas)
                        item.IsConfirm = false;
                    unitOfWork.Commit();
                }

            return datas.OrderBy(e => e.Time).Select(e => new
            {
                e.Id,
                e.Name,
                e.Quanlity,
                e.Capacity,
                IsConfirm = e.IsConfirm,
                Time = e.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.CreatedBy,
                TransmissionTime = e.TransmissionTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.UpdatedBy,
                UpdatedAt = e.UpdatedAt?.ToString("HH:mm dd/MM/yyyy")
            });
        }

        protected void HandleCreateOrUpdateBloodRequestData(EIOBloodRequestSupplyAndConfirmation brsac, JObject request, Customer customer)
        {
            brsac.Number = string.IsNullOrEmpty(request["Number"]?.ToString()) ? new Nullable<int>() : request["Number"].ToObject<int>();
            brsac.IsFrequently = string.IsNullOrEmpty(request["IsFrequently"].ToString()) ? false : request["IsFrequently"].ToObject<bool>();
            brsac.BloodTypeABO = request["BloodTypeABO"].ToString();
            brsac.BloodTypeRH = request["BloodTypeRH"].ToString();
            brsac.TransfusionTime = string.IsNullOrEmpty(request["TransfusionTime"]?.ToString()) ? new Nullable<int>() : request["TransfusionTime"].ToObject<int>();

            if (brsac.Version >= 2)
            {
                bool isEdit = request["IsEdited"].ToObject<bool>();
                if (isEdit)
                {
                    brsac.HeadOfDeptId = null;
                    brsac.HeadOfDeptTime = null;
                    brsac.DoctorConfirmId = null;
                    brsac.DoctorConfirmTime = null;
                }
            }

            unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac);

            customer.BloodTypeABO = request["BloodTypeABO"].ToString();
            customer.BloodTypeRH = request["BloodTypeRH"].ToString();
            unitOfWork.CustomerRepository.Update(customer);

            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac.Id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_PURCHASE
            ).ToList();
            var user = GetUser();
            foreach (var item in request["Datas"])
            {
                var str_id = item.Value<string>("Id");
                var data = GetOrCreateBloodRequestData(item.Value<string>("Id"), brsac, datas);

                // đống này để hùng làm xong rồi chặn sau (data.CreatedBy == user.Username)
                if (data != null && !data.IsConfirm && data.CreatedBy == user.Username)
                    UpdateBloodRequestData(data, item, brsac.Version);
            }
            unitOfWork.Commit();
        }
        private EIOBloodProductData GetOrCreateBloodRequestData(string str_id, EIOBloodRequestSupplyAndConfirmation brsac, List<EIOBloodProductData> datas)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                var data = new EIOBloodProductData()
                {
                    FormId = brsac.Id,
                    FormName = Constant.EIO_BLOOD_PURCHASE
                };
                unitOfWork.EIOBloodProductDataRepository.Add(data);

                brsac.HeadOfDeptId = null;
                brsac.DoctorConfirmId = null;
                brsac.HeadOfDeptTime = null;
                brsac.DoctorConfirmTime = null;
                unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac);

                return data;
            }

            Guid data_id = new Guid(str_id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateBloodRequestData(EIOBloodProductData data, JToken item, int version)
        {
            data.Name = item.Value<string>("Name");
            if (!string.IsNullOrEmpty(item.Value<string>("Quanlity")))
                data.Quanlity = item.Value<int>("Quanlity");
            if (!string.IsNullOrEmpty(item.Value<string>("Capacity")))
                data.Capacity = item.Value<int>("Capacity");
            var time = item.Value<string>("Time");
            if (!string.IsNullOrEmpty(time))
                data.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                data.Time = null;

            if (version >= 2)
            {
                var transmissionTime = item.Value<string>("TransmissionTime");
                if (!string.IsNullOrEmpty(transmissionTime))
                    data.TransmissionTime = DateTime.ParseExact(transmissionTime, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

                if (data.CreatedBy == GetUser()?.Username)
                    data.IsDeleted = item.Value<bool>("IsDeleted");
            }

            unitOfWork.EIOBloodProductDataRepository.Update(data);
        }
        protected void ConfirmBloodRequest(EIOBloodRequestSupplyAndConfirmation brsac, Guid user_id)
        {
            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac.Id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_PURCHASE &&
                !e.IsConfirm
            );
            foreach (var data in datas)
            {
                data.IsConfirm = true;
                unitOfWork.EIOBloodProductDataRepository.Update(data);
            }
            brsac.HeadOfDeptTime = DateTime.Now;
            brsac.HeadOfDeptId = user_id;
            unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac);
            unitOfWork.Commit();
        }
        #endregion

        #region Supply
        protected dynamic GetBloodSupply(EIOBloodRequestSupplyAndConfirmation brsac, bool IsLocked = false)
        {
            var blood_request_datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac.Id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_PURCHASE &&
                !string.IsNullOrEmpty(e.Name)
            ).Select(e => new { e.Name, e.Quanlity }).GroupBy(e => e.Name).Select(e => new { name = e.Key, value = e.Sum(s => s.Quanlity) });
            var blood_supply_datas = GetBloodSupplyDatas(brsac.Id);
            return new
            {
                brsac.Number,
                brsac.IsFrequently,
                ListName = blood_request_datas,
                Supplies = blood_supply_datas,
                IsLocked,
                Version = brsac.Version,
                brsac.CreatedBy,
                CreatedAt = brsac.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                brsac.UpdatedBy,
                UpdatedAt = brsac.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
            };
        }
        private List<EIOBloodSupplyViewModel> GetBloodSupplyDatas(Guid brsac_id)
        {
            var result = new List<EIOBloodSupplyViewModel>();

            var supplies = unitOfWork.EIOBloodSupplyDataRepository.Find(
                e => !e.IsDeleted &&
                e.EIOBloodRequestSupplyAndConfirmationId != null &&
                e.EIOBloodRequestSupplyAndConfirmationId == brsac_id
            ).OrderBy(e => e.CreatedAt);

            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac_id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_SUPPLY
            ).OrderBy(e => e.CreatedAt).ToList();

            // var isUnlockConfirm = IsUnlockConfirm(brsac_id);

            foreach (var sup in supplies)
            {
                result.Add(new EIOBloodSupplyViewModel()
                {
                    Id = sup.Id,
                    Name = sup.Name,
                    CreatedBy = sup.CreatedBy,
                    Quanlity = sup.Quanlity,
                    NurseTime = sup.NurseTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    NurseUser = sup.NurseUser,
                    CuratorTime = sup.CuratorTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    CuratorUser = sup.CuratorUser,
                    ProviderTime = sup.ProviderTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    ProviderUser = sup.ProviderUser,
                    Datas = datas.Where(e => e.GroupId == sup.Id).Select(e => new
                    {
                        e.Id,
                        e.Code,
                        e.BloodTypeABO,
                        e.BloodTypeRH,
                        e.Capacity,
                        e.CreatedAt
                    }),
                    CreatedAt = sup.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedAt = sup.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = sup.UpdatedBy

                });
            }

            return result;
        }
        protected void HandleCreateOrUpdateBloodSupplyData(EIOBloodRequestSupplyAndConfirmation brsac, JObject request)
        {
            brsac.Number = string.IsNullOrEmpty(request["Number"]?.ToString()) ? new Nullable<int>() : request["Number"].ToObject<int>();
            brsac.IsFrequently = string.IsNullOrEmpty(request["IsFrequently"]?.ToString()) ? false : request["IsFrequently"].ToObject<bool>();
            unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac);

            var supplies = unitOfWork.EIOBloodSupplyDataRepository.Find(
                e => !e.IsDeleted &&
                e.EIOBloodRequestSupplyAndConfirmationId != null &&
                e.EIOBloodRequestSupplyAndConfirmationId == brsac.Id
            ).ToList();

            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac.Id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_SUPPLY
            ).ToList();

            var user = GetUser();
            foreach (var item in request["Supplies"])
            {
                var str_id = item.Value<string>("Id");
                var is_removed = item.Value<bool>("Removed");
                var data = GetOrCreateBloodSupplyData(item.Value<string>("Id"), is_removed, brsac, supplies);

                if (data != null && data.CreatedBy == user.Username)
                    UpdateBloodSupplyData(data, item, datas, brsac);
            }
            unitOfWork.Commit();

        }
        private EIOBloodSupplyData GetOrCreateBloodSupplyData(string str_id, bool is_removed, EIOBloodRequestSupplyAndConfirmation brsac, List<EIOBloodSupplyData> supplies)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                if (is_removed)
                    return null;

                var new_data = new EIOBloodSupplyData()
                {
                    EIOBloodRequestSupplyAndConfirmationId = brsac.Id,
                };
                unitOfWork.EIOBloodSupplyDataRepository.Add(new_data);
                return new_data;
            }

            Guid data_id = new Guid(str_id);
            var data = supplies.FirstOrDefault(e => e.Id == data_id);
            if (data != null && is_removed)
            {
                unitOfWork.EIOBloodSupplyDataRepository.Delete(data);
                return null;
            }
            return supplies.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateBloodSupplyData(EIOBloodSupplyData supply, JToken item, List<EIOBloodProductData> datas, EIOBloodRequestSupplyAndConfirmation brsac)
        {
            if (!string.IsNullOrEmpty(supply.NurseUser) ||
                !string.IsNullOrEmpty(supply.CuratorUser) ||
                !string.IsNullOrEmpty(supply.ProviderUser)) return;

            supply.Name = item.Value<string>("Name");
            if (!string.IsNullOrEmpty(item.Value<string>("Quanlity")))
                supply.Quanlity = item.Value<int>("Quanlity");

            if (brsac.Version >= 2)
            {
                // tam thoi comment if(supply.CreatedBy == getUsername())
                supply.IsDeleted = item.Value<bool>("IsDeleted");
            }

            unitOfWork.EIOBloodSupplyDataRepository.Update(supply);
            foreach (var i in item["Datas"])
            {
                var str_id = i.Value<string>("Id");
                var is_removed = i.Value<bool>("Removed");
                var data = GetOrCreateBloodSupplyDataItem(str_id, is_removed, supply.EIOBloodRequestSupplyAndConfirmationId, supply.Id, datas);
                if (data == null) continue;
                if (is_removed)
                    unitOfWork.EIOBloodProductDataRepository.Delete(data);
                else
                    UpdateBloodRequestDataItem(data, i, supply.Name, brsac.Version);
            }
        }
        private EIOBloodProductData GetOrCreateBloodSupplyDataItem(string str_id, bool is_removed, Guid? brsac_id, Guid supply_id, List<EIOBloodProductData> datas)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                if (is_removed)
                    return null;

                var new_data = new EIOBloodProductData()
                {
                    FormId = brsac_id,
                    FormName = Constant.EIO_BLOOD_SUPPLY,
                    GroupId = supply_id,
                };
                unitOfWork.EIOBloodProductDataRepository.Add(new_data);
                return new_data;
            }

            Guid data_id = new Guid(str_id);
            var data = datas.FirstOrDefault(e => e.Id == data_id);
            if (data != null && is_removed)
            {
                unitOfWork.EIOBloodProductDataRepository.Delete(data);
                return null;
            }
            return data;
        }
        private void UpdateBloodRequestDataItem(EIOBloodProductData data, JToken item, string supply_name, int version)
        {
            data.Name = supply_name;
            data.Code = item.Value<string>("Code");
            data.BloodTypeABO = item.Value<string>("BloodTypeABO");
            data.BloodTypeRH = item.Value<string>("BloodTypeRH");
            if (!string.IsNullOrEmpty(item.Value<string>("Capacity")))
                data.Capacity = item.Value<int>("Capacity");

            if (version >= 2)
            {
                if (data.CreatedBy == GetUser()?.Username)
                    data.IsDeleted = item.Value<bool>("IsDeleted");
            }

            unitOfWork.EIOBloodProductDataRepository.Update(data);
        }
        protected bool ConfirmBloodSupply(EIOBloodSupplyData supply, string kind, string username)
        {
            if (kind == "Nurse" && string.IsNullOrEmpty(supply.NurseUser))
            {
                supply.NurseUser = username;
                supply.NurseTime = DateTime.Now;
            }
            else if (kind == "Curator" && string.IsNullOrEmpty(supply.CuratorUser))
            {
                supply.CuratorUser = username;
                supply.CuratorTime = DateTime.Now;
            }
            else if (kind == "Provider" && string.IsNullOrEmpty(supply.ProviderUser))
            {
                supply.ProviderUser = username;
                supply.ProviderTime = DateTime.Now;
            }
            else
                return false;
            unitOfWork.EIOBloodSupplyDataRepository.Update(supply);
            unitOfWork.Commit();
            return true;
        }
        #endregion

        #region Transfusion Confirmation
        private List<dynamic> GetConfirmCreated(Guid brsac_id, Guid visitId)
        {
            var formids = unitOfWork.EIOBloodProductDataRepository.AsQueryable()
                                        .Where(e => !e.IsDeleted && e.FormId == brsac_id && e.FormName == "EIOBloodTransfusionConfirmation").Select(e => e.Id);

            var list_confirm_created = (from c in unitOfWork.EIOFormConfirmRepository.AsQueryable()
                                        where !c.IsDeleted
                                        join id in formids on c.FormId equals id
                                        select new
                                        {
                                            c.ConfirmAt,
                                            c.ConfirmBy,
                                            c.ConfirmType,
                                            ObjectId = c.FormId
                                        }).ToList();

            return new List<dynamic>(list_confirm_created);
        }

        protected dynamic GetBloodTransfusionConfirmation(EIOBloodRequestSupplyAndConfirmation brsac, bool IsLocked = false)
        {
            var datas = GetBloodTransfusionConfirmationDatas(brsac.Id, (Guid)brsac.VisitId);
            var list_code = GetListCodeNameBloodTransfusionConfirmation(brsac.Id);
            return new
            {
                brsac.Number,
                brsac.IsFrequently,
                ListCode = list_code,
                Datas = datas,
                IsLocked,
                brsac.Version,
                brsac.CreatedBy,
                CreatedAt = brsac.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                brsac.UpdatedBy,
                UpdatedAt = brsac.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
            };
        }
        private dynamic GetBloodTransfusionConfirmationDatas(Guid brsac_id, Guid visitId)
        {
            var objects = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac_id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_TRANS
                );

            var Confirms = GetConfirmCreated(brsac_id, visitId);
            var result = (from obj in objects
                          join confirm in Confirms
                          on obj.Id equals confirm.ObjectId into temp
                          from res in temp.DefaultIfEmpty()
                          orderby obj.Time
                          select new
                          {
                              obj.Id,
                              obj.Name,
                              obj.Code,
                              obj.Quanlity,
                              obj.Capacity,
                              obj.Note,
                              Time = obj.Time?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                              obj.CreatedBy,
                              ConfirmCreated = res == null ? null : new
                              {
                                  res?.ConfirmAt,
                                  res?.ConfirmBy,
                                  res?.ConfirmType,
                                  res?.ObjectId
                              }
                          }).ToList();
            return result;
        }
        private dynamic GetListCodeNameBloodTransfusionConfirmation(Guid brsac_id)
        {
            var list_code = (from s in unitOfWork.EIOBloodSupplyDataRepository.AsQueryable()
                             join p in unitOfWork.EIOBloodProductDataRepository.AsQueryable()
                             on s.Id equals p.GroupId
                             where !s.IsDeleted && s.EIOBloodRequestSupplyAndConfirmationId == brsac_id
                             && !p.IsDeleted
                             select new
                             {
                                 p.Name,
                                 p.Code
                             }).ToList();

            return list_code;
        }
        protected List<Guid> HandleCreateOrUpdateBloodTransfusionConfirmationData(EIOBloodRequestSupplyAndConfirmation brsac, JObject request)
        {
            brsac.Number = string.IsNullOrEmpty(request["Number"]?.ToString()) ? new Nullable<int>() : request["Number"].ToObject<int>();
            brsac.IsFrequently = string.IsNullOrEmpty(request["IsFrequently"]?.ToString()) ? false : request["IsFrequently"].ToObject<bool>();
            unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(brsac);

            var datas = unitOfWork.EIOBloodProductDataRepository.Find(
                e => !e.IsDeleted &&
                e.FormId != null &&
                e.FormId == brsac.Id &&
                !string.IsNullOrEmpty(e.FormName) &&
                e.FormName == Constant.EIO_BLOOD_TRANS
            ).ToList();
            var user = GetUser();
            List<Guid> list_id = new List<Guid>();
            foreach (var item in request["Datas"])
            {
                var str_id = item.Value<string>("Id");
                var data = GetOrCreateBloodTransfusionConfirmationData(item.Value<string>("Id"), brsac, datas);
                if (data != null && data.CreatedBy == user.Username)
                    UpdateBloodTransfusionConfirmationData(data, item, brsac.Version);
                list_id.Add(data.Id);
            }
            unitOfWork.Commit();
            return list_id;

        }
        private EIOBloodProductData GetOrCreateBloodTransfusionConfirmationData(string str_id, EIOBloodRequestSupplyAndConfirmation brsac, List<EIOBloodProductData> datas)
        {
            if (string.IsNullOrEmpty(str_id))
            {
                var data = new EIOBloodProductData()
                {
                    FormId = brsac.Id,
                    FormName = Constant.EIO_BLOOD_TRANS
                };
                unitOfWork.EIOBloodProductDataRepository.Add(data);

                return data;
            }

            Guid data_id = new Guid(str_id);
            return datas.FirstOrDefault(e => e.Id == data_id);
        }
        private void UpdateBloodTransfusionConfirmationData(EIOBloodProductData data, JToken item, int version)
        {
            data.Name = item.Value<string>("Name");
            data.Code = item.Value<string>("Code");
            if (!string.IsNullOrEmpty(item.Value<string>("Quanlity")))
                data.Quanlity = item.Value<int>("Quanlity");
            if (!string.IsNullOrEmpty(item.Value<string>("Capacity")))
                data.Capacity = item.Value<int>("Capacity");
            data.Note = item.Value<string>("Note");
            var time = item.Value<string>("Time");
            if (!string.IsNullOrEmpty(time))
                data.Time = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                data.Time = null;

            if (version >= 2)
                data.IsDeleted = item.Value<bool>("IsDeleted");

            unitOfWork.EIOBloodProductDataRepository.Update(data);
        }

        private string EDGetAndFormatICD10(Guid? dischargeInfomationId)
        {
            string[] codes = { "DI0DIAICD", "DI0DIAOPT" };

            var string_json = (from d in unitOfWork.DischargeInformationDataRepository.AsQueryable()
                               where !d.IsDeleted && d.DischargeInformationId == dischargeInfomationId
                               && codes.Contains(d.Code)
                               select d).ToList();
            string result = String.Empty;

            foreach (var code in codes)
            {
                string text = string_json.FirstOrDefault(e => e.Code == code)?.Value;
                if (text == null || text == $"\"\"")
                    text = "";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<Dictionary<string, string>> objs = jss.Deserialize<List<Dictionary<string, string>>>(text);
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
                            _str += $", {codeIcd10}";
                    }

                    if (!string.IsNullOrEmpty(result))
                        result += "/ " + _str;
                    else
                        result += _str;
                }
            }

            if (string.IsNullOrEmpty(result))
                return "";

            string format = $" ({result})";
            return format;
        }
        #endregion
    }
}
