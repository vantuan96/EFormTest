using DataAccess.Models;
using DataAccess.Models.EIOModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models;
using EForm.Utils;
using EMRModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOJointConsultationGroupMinutesController : BaseApiController
    {
        #region Joint Consultation Group Minutes
        protected List<EIOJointConsultationGroupMinutes> GetListJointConsultationGroupMinutes(Guid visit_id)
        {
            return unitOfWork.EIOJointConsultationGroupMinutesRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id
            ).OrderBy(e => e.CreatedAt).ToList();
        }
        protected EIOJointConsultationGroupMinutes GetEIOJointConsultationGroupMinutes(Guid id)
        {
            return unitOfWork.EIOJointConsultationGroupMinutesRepository.GetById(id);
        }
        protected void CreateEIOJointConsultationGroupMinutes(Guid visit_id, string visit_type, Guid? specialty_id)
        {
            var jscm = new EIOJointConsultationGroupMinutes()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
                SpecialtyId = specialty_id
            };
            unitOfWork.EIOJointConsultationGroupMinutesRepository.Add(jscm);
            unitOfWork.Commit();
        }
        protected dynamic GetOrUpdateNewestEIOJointConsultationGroupMinutesData(EIOJointConsultationGroupMinutes jscm, dynamic visit)
        {
            var datas = jscm.EIOJointConsultationGroupMinutesDatas.Where(e => !e.IsDeleted).ToList();
            var admitted_date = datas.FirstOrDefault(e => e.Code == "EIOJCGMADATANS");
            var chairman_title = datas.FirstOrDefault(e => e.Code == "EIOJCGMCHMAANS1");
            var secretary_title = datas.FirstOrDefault(e => e.Code == "EIOJCGMSETAANS1");
            if (!jscm.ChairmanConfirm && !jscm.SecretaryConfirm && !jscm.MemberConfirm)
            {
                if (admitted_date == null)
                {
                    admitted_date = new EIOJointConsultationGroupMinutesData()
                    {
                        EIOJointConsultationGroupMinutesId = jscm.Id,
                        Code = "EIOJCGMADATANS",
                        Value = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
                    };
                    unitOfWork.EIOJointConsultationGroupMinutesDataRepository.Add(admitted_date);
                    datas.Add(admitted_date);
                }
                else
                {
                    admitted_date.Value = visit.AdmittedDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND);
                    unitOfWork.EIOJointConsultationGroupMinutesDataRepository.Update(admitted_date, is_anonymous: true);
                }

            }
            var customer = visit.Customer;
            var SpecialtyInfo = new {
                visit.Specialty.ViName,
                visit.Specialty.EnName,
                visit.Specialty.Id
            };
            var gender = new CustomerUtil(customer).GetGender();
            var doctor = jscm.Doctor;
            var chairman = jscm.Chairman;
            var secretary = jscm.Secretary;
            var members = jscm.EIOJointConsultationGroupMinutesMembers
                .Where(e => !e.IsDeleted && e.IsConfirm)
                .Select(e => new { e.Member?.Id, e.Member?.Fullname, e.Member?.Username });
            var visit_type = jscm.VisitTypeGroupCode;
            var IsLocked = false;
            switch (visit_type)
            {
                case "IPD":
                    IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BienBanHoiChan, jscm.Id);
                    break;
                case "OPD":
                    var user = GetUser();
                    IsLocked = Is24hLocked(visit.CreatedAt, (Guid)jscm.VisitId, "OPDJCFM", user.Username, jscm.Id);
                    break;
            }
            return new
            {
                jscm.Id,
                Specialty = new { jscm.Specialty?.ViName, jscm.Specialty?.EnName },
                Doctor = new { doctor?.Id, doctor?.Fullname, doctor?.Username, doctor?.DisplayName, doctor?.Title },
                Chairman = new { chairman?.Id, chairman?.Fullname, chairman?.Username, chairman?.DisplayName, Title = chairman_title?.Value },
                jscm.ChairmanConfirm,
                Secretary = new { secretary?.Id, secretary?.Fullname, secretary?.Username, secretary?.DisplayName, Title = secretary_title?.Value },
                jscm.SecretaryConfirm,
                Members = members,
                jscm.MemberConfirm,
                Customer = new CustomerViewModel()
                {
                    Fullname = customer.Fullname,
                    PID = customer.PID,
                    Gender = gender,
                    DateOfBirth = customer.DateOfBirth?.ToString(Constant.DATE_FORMAT),
                    EthnicGroup = visit_type == "IPD" ? customer.MOHEthnic : customer.Fork,
                    Profession = visit_type == "IPD" ? customer.MOHJob : customer.Job
                },               
                Datas = datas.Select(e => new { e.Id, e.Code, e.Value }),
                SpecialtyInfo,
                Lock24h = IsLocked
            };
        }
        protected EIOJointConsultationGroupMinutes GetEIOJointConsultationGroupMinutes(Guid visit_id, string visit_type)
        {
            return unitOfWork.EIOJointConsultationGroupMinutesRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type
            );
        }
        protected void HandleJointConsultationGroupMinutesData(EIOJointConsultationGroupMinutes jscm, JToken request)
        {
            var jscm_data = jscm.EIOJointConsultationGroupMinutesDatas.Where(e => !e.IsDeleted && !string.IsNullOrEmpty(e.Code)).ToList();
            foreach (var item in request)
            {
                var code = item.Value<string>("Code");
                var value = item.Value<string>("Value");
                var data = GetOrCreateJointConsultationGroupMinutesData(code, jscm.Id, jscm_data);
                if (data != null)
                {
                    UpdateJointConsultationGroupMinutesData(data, item);
                    if (code == "EIOJCGMCHMAANS")
                    {
                        var chairman = unitOfWork.UserRepository.FirstOrDefault(
                            e => !e.IsDeleted &&
                            !string.IsNullOrEmpty(e.Username) &&
                            e.Username == value
                        );
                        jscm.ChairmanId = chairman?.Id;
                    }
                    else if (code == "EIOJCGMSETAANS")
                    {
                        var secretary = unitOfWork.UserRepository.FirstOrDefault(
                            e => !e.IsDeleted &&
                            !string.IsNullOrEmpty(e.Username) &&
                            e.Username == value
                        );
                        jscm.SecretaryId = secretary?.Id;
                    }
                    else if (code == "EIOJCGMPBPDANS")
                    {
                        var doctor = unitOfWork.UserRepository.FirstOrDefault(
                            e => !e.IsDeleted &&
                            !string.IsNullOrEmpty(e.Username) &&
                            e.Username == value
                        );
                        jscm.DoctorId = doctor?.Id;
                    }
                }
            }
            unitOfWork.Commit();
        }
        protected EIOJointConsultationGroupMinutesData GetOrCreateJointConsultationGroupMinutesData(string code, Guid jscm_id, List<EIOJointConsultationGroupMinutesData> jscm_data)
        {
            var data = jscm_data.FirstOrDefault(e => e.Code == code);
            if (data != null) return data;

            data = new EIOJointConsultationGroupMinutesData()
            {
                Code = code,
                EIOJointConsultationGroupMinutesId = jscm_id
            };
            unitOfWork.EIOJointConsultationGroupMinutesDataRepository.Add(data);
            return data;
        }
        protected void UpdateJointConsultationGroupMinutesData(EIOJointConsultationGroupMinutesData data, JToken item)
        {
            data.Value = item.Value<string>("Value");
            unitOfWork.EIOJointConsultationGroupMinutesDataRepository.Update(data);
        }
        protected bool ConfirmJointConsultationGroupMinutes(EIOJointConsultationGroupMinutes jscm, User user, string kind)
        {
            if (kind == "Chairman" && !jscm.ChairmanConfirm && jscm.ChairmanId != null && jscm.ChairmanId == user.Id)
                jscm.ChairmanConfirm = true;
            else if (kind == "Secretary" && !jscm.SecretaryConfirm && jscm.SecretaryId != null && jscm.SecretaryId == user.Id)
                jscm.SecretaryConfirm = true;
            else if (kind == "Member")
            {
                var member = jscm.EIOJointConsultationGroupMinutesMembers.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.IsConfirm &&
                    e.MemberId != null &&
                    e.MemberId == user.Id
                );
                if (member != null)
                    return false;

                member = new EIOJointConsultationGroupMinutesMember()
                {
                    EIOJointConsultationGroupMinutesId = jscm.Id,
                    MemberId = user.Id,
                    IsConfirm = true,
                };
                unitOfWork.EIOJointConsultationGroupMinutesMemberRepository.Add(member);

                jscm.MemberConfirm = true;
            }
            else
                return false;
            unitOfWork.EIOJointConsultationGroupMinutesRepository.Update(jscm);
            unitOfWork.Commit();
            return true;
        }
        #endregion
    }
}
