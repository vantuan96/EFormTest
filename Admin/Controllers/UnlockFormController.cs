using Admin.Common;
using Admin.Common.HandlerLogs;
using Admin.Common.Model;
using Admin.CustomAuthen;
using Admin.Models;
using DataAccess.Models;
using DataAccess.Models.IPDModel;
using DataAccess.Repository;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static Antlr.Runtime.Tree.TreeWizard;

namespace Admin.Controllers
{
    [Authorize(Roles = Constant.AdminRoles.SuperAdmin + "," + Constant.AdminRoles.ManageUnlock)]
    public class UnlockFormController : Controller
    {
        protected IUnitOfWork unitOfWork = new EfUnitOfWork();

        // GET: Unlock
        public ActionResult Index()
        {
            var model = new UnlockFormViewModel();
            return View(model);
        }

        public ActionResult GetListUnlockForms(DataTablesQueryModel queryModel)
        {
            try
            {
                int totalResultsCount;
                var take = queryModel.length;
                var skip = queryModel.start;

                string sortBy = "";
                string sortDir = "";
                if (queryModel.order != null)
                {
                    sortBy = queryModel.columns[queryModel.order[0].column].data;
                    sortDir = queryModel.order[0].dir.ToLower();
                }

                var list = unitOfWork.UnlockFormToUpdateRepository.AsQueryable();
                var filterName = queryModel.columns.First(s => s.name == "RecordCode").search.value;
                if (!string.IsNullOrEmpty(filterName))
                    list = list.Where(s => s.RecordCode.ToLower().Contains(filterName.ToLower()));

                totalResultsCount = list.Count();

                var result = list.OrderBy(sortBy + (sortDir == "desc" ? " descending" : "")).Skip(skip).Take(take).ToList()
                    .Select(x => new UnlockFormViewModel
                    {
                        Id = x.Id,
                        Username = x.Username,
                        RecordCode = x.RecordCode,
                        FormName = x.FormName,
                        ExpiredAt = x.ExpiredAt?.ToString("HH:mm dd/MM/yyyy"),
                        IsDeleted = x.IsDeleted,
                        LockType = x.FormId == null ? "Khóa 24h" : "Khóa xác nhận"
                    });

                return Json(new
                {
                    queryModel.draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = totalResultsCount,
                    data = result
                });
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateUnlock(Guid id)
        {
            try
            {
                var unlock_form = unitOfWork.UnlockFormToUpdateRepository.GetById(id);
                if (unlock_form == null)
                    return Json(false);
                else
                {
                    unitOfWork.UnlockFormToUpdateRepository.Delete(unlock_form);
                    unitOfWork.Commit();
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateUnlock(Guid id)
        {
            try
            {
                var unlock_form = unitOfWork.UnlockFormToUpdateRepository.GetById(id);
                if (unlock_form == null)
                    return Json(false);
                else
                {
                    unlock_form.IsDeleted = false;
                }
                unitOfWork.Commit();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUnlock24H(string record_code, Guid visit_id, string unlock_user, string list_form)
        {
            dynamic data = null;
            try
            {
                if (string.IsNullOrEmpty(record_code) || visit_id == null || string.IsNullOrEmpty(unlock_user) || string.IsNullOrEmpty(list_form))
                {
                    data = new
                    {
                        IsUnlock = false,
                        Message = "Thông tin nhập không đúng"
                    };
                    return Json(data);
                }
                var json = new JavaScriptSerializer().Deserialize<dynamic>(list_form);
                foreach (var form in json)
                {
                    UnlockFormToUpdate unlock = new UnlockFormToUpdate()
                    {
                        RecordCode = record_code,
                        VisitId = visit_id,
                        FormName = form["Name"],
                        FormCode = form["Code"],
                        Username = unlock_user,
                        ExpiredAt = DateTime.Now.AddDays(1),
                    };
                    unitOfWork.UnlockFormToUpdateRepository.Add(unlock);
                }
                unitOfWork.Commit();
                data = new
                {
                    IsUnlock = true,
                    Message = "Mở khóa 24h thành công"
                };
                return Json(data);
            }
            catch (Exception)
            {
                data = new
                {
                    IsUnlock = true,
                    Message = "Có lỗi trong quá trình xử lý"
                };
                return Json(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckRecordCode(string record_code)
        {
            if (string.IsNullOrEmpty(record_code))
                return Json(new CheckRecordCodeModel { IsInvalidRecordCode = true });
            else if (record_code.Contains("ED"))
            {
                var visit = unitOfWork.EDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.RecordCode) &&
                    e.RecordCode.Equals(record_code)
                );

                if (visit == null)
                    return Json(new CheckRecordCodeModel { IsInvalidRecordCode = true });

                return Json(new CheckRecordCodeModel
                {
                    VisitId = visit.Id,
                    FormList = unitOfWork.FormRepository.Find(
                        e => !e.IsDeleted &&
                        e.VisitTypeGroupCode.Equals("ED")
                    ).Select(e => new { e.Id, e.Code, e.Name })
                    .OrderBy(e => e.Name)
                    .ToList(),
                });
            }
            else if (record_code.Contains("OPD"))
            {
                var visit = unitOfWork.OPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.RecordCode) &&
                    e.RecordCode.Equals(record_code)
                );

                if (visit == null)
                    return Json(new CheckRecordCodeModel { IsInvalidRecordCode = true });

                return Json(new CheckRecordCodeModel
                {
                    VisitId = visit.Id,
                    FormList = unitOfWork.FormRepository.Find(
                        e => !e.IsDeleted &&
                        e.VisitTypeGroupCode.Equals("OPD")
                    ).Select(e => new { e.Id, e.Code, e.Name })
                    .OrderBy(e => e.Name)
                    .ToList(),
                });
            }
            else if (record_code.Contains("IPD"))
            {
                var visit = unitOfWork.IPDRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.RecordCode) &&
                    e.RecordCode.Equals(record_code)
                );

                if (visit == null)
                    return Json(new CheckRecordCodeModel { IsInvalidRecordCode = true });

                return Json(new CheckRecordCodeModel
                {
                    VisitId = visit.Id,
                    FormList = unitOfWork.FormRepository.Find(
                        e => !e.IsDeleted &&
                        e.VisitTypeGroupCode.Equals("IPD")
                    ).Select(e => new { e.Id, e.Code, e.Name })
                    .OrderBy(e => e.Name)
                    .ToList(),
                });
            }
            else if (record_code.Contains("EOC"))
            {
                var visit = unitOfWork.EOCRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    !string.IsNullOrEmpty(e.RecordCode) &&
                    e.RecordCode.Equals(record_code)
                );

                if (visit == null)
                    return Json(new CheckRecordCodeModel { IsInvalidRecordCode = true });

                return Json(new CheckRecordCodeModel
                {
                    VisitId = visit.Id,
                    FormList = unitOfWork.FormRepository.Find(
                        e => !e.IsDeleted &&
                        e.VisitTypeGroupCode.Equals("EOC")
                    ).Select(e => new { e.Id, e.Code, e.Name })
                    .OrderBy(e => e.Name)
                    .ToList(),
                });
            }
            return Json(new CheckRecordCodeModel { IsInvalidRecordCode = true });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateExpire(Guid id)
        {
            try
            {
                var unlock = unitOfWork.UnlockFormToUpdateRepository.GetById(id);
                if (unlock != null)
                {
                    unlock.ExpiredAt = unlock.ExpiredAt?.AddDays(1);
                    unitOfWork.UnlockFormToUpdateRepository.Update(unlock);
                    unitOfWork.Commit();
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        #region Mở khóa xác nhận
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUnlockConfirm(string record_code, Guid visit_id, string list_form_id, string unlock_user, string list_form)
        {
            dynamic data = null;
            try
            {
                if (string.IsNullOrEmpty(record_code) || visit_id == null || string.IsNullOrEmpty(list_form_id) || string.IsNullOrEmpty(unlock_user) || string.IsNullOrEmpty(list_form))
                {
                    data = new
                    {
                        IsUnlock = false,
                        Message = "Thông tin nhập không đúng"
                    };
                    return Json(data);
                }
                var listForm = new JavaScriptSerializer().Deserialize<dynamic>(list_form);
                var strListFormId = new JavaScriptSerializer().Deserialize<string>(list_form_id);
                var listFormId = strListFormId.Split(',');
                foreach (var form in listForm)
                {
                    foreach (var formId in listFormId)
                    {
                        if (!string.IsNullOrEmpty(formId))
                        {
                            bool func1 = CheckIsIdFormMatchForm(visit_id, Guid.Parse(formId), form["Code"]);
                            bool func2 = CheckIsIdFormMatchToUnlockConfirm(visit_id, Guid.Parse(formId), form["Code"]);
                            if (!func1 && !func2)
                            {
                                data = new
                                {
                                    IsUnlock = false,
                                    Message = "ID form đã nhập không khớp phiếu đã chọn"
                                };
                                return Json(data);
                            }
                            UnlockFormToUpdate unlock = new UnlockFormToUpdate()
                            {
                                RecordCode = record_code,
                                VisitId = visit_id,
                                FormId = Guid.Parse(formId),
                                FormName = form["Name"],
                                FormCode = form["Code"],
                                Username = unlock_user,
                                ExpiredAt = DateTime.Now.AddHours(8)
                            };
                            ExecuteLog(visit_id, Guid.Parse(formId), form["Code"], unlock_user);
                            unitOfWork.UnlockFormToUpdateRepository.Add(unlock);
                        }
                    }
                }
                unitOfWork.Commit();
                data = new
                {
                    IsUnlock = true,
                    Message = "Mở khóa xác nhận thành công"
                };
                return Json(data);
            }
            catch (Exception)
            {
                data = new
                {
                    IsUnlock = false,
                    Message = "Có lỗi trong quá trình xử lý"
                };
                return Json(data);
            }
        }
        private bool CheckIsIdFormMatchForm(Guid visitId, Guid formId, string formCode)
        {
            dynamic form = null;
            bool is_anonymous = true;
            bool is_time_change = false;
            switch (formCode)
            {
                #region Khu vực IPD
                // ---------- Khu vực IPD ---------- //
                case "A01_145_050919_VE": // giấy chuyển viện version 1
                    form = unitOfWork.IPDReferralLetterRepository.FirstOrDefault(
                        e => !e.IsDeleted && e.Id == formId
                        );
                    if (form == null)
                        return false;
                    form.PhysicianInChargeId = null;
                    form.PhysicianInChargeTime = null;
                    form.DirectorId = null;
                    form.DirectorTime = null;
                    unitOfWork.Commit();
                    return true;
                case "A01_167_180220_VE": //Giấy chuyển tuyến version 1
                    form = unitOfWork.IPDTransferLetterRepository.FirstOrDefault(
                        e => !e.IsDeleted && e.Id == formId
                        );
                    if (form == null)
                        return false;
                    form.PhysicianInChargeId = null;
                    form.PhysicianInChargeTime = null;
                    form.DirectorId = null;
                    form.DirectorTime = null;
                    unitOfWork.Commit();
                    return true;
                //1 Phiếu dự trù máu
                case "IPDPDTM":
                    form = unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    var tab2IPD = from t2 in unitOfWork.EIOBloodSupplyDataRepository.AsQueryable()
                                  where !t2.IsDeleted && t2.EIOBloodRequestSupplyAndConfirmationId == formId
                                  select t2;
                    if (form != null)
                    {
                        //form.SpecialtyId = null; mở khóa ỏ đây bay luôn chuyên khoa 
                        form.HeadOfDeptId = null;
                        form.HeadOfDeptTime = null;
                        form.DoctorConfirmId = null;
                        form.DoctorConfirmTime = null;
                        unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(form, is_anonymous, is_time_change);
                        if (tab2IPD != null && tab2IPD.Count() > 0)
                        {
                            foreach (var item in tab2IPD)
                            {
                                item.CuratorTime = null;
                                item.CuratorUser = null;
                                item.NurseTime = null;
                                item.NurseUser = null;
                                item.ProviderTime = null;
                                item.ProviderUser = null;
                            }
                        }
                        unitOfWork.Commit();
                        return true;
                    }
                    else
                    {
                        var form2 = unitOfWork.EIOFormConfirmRepository.FirstOrDefault(e => !e.IsDeleted && e.FormId == formId);
                        if (form2 != null)
                        {
                            form2.IsDeleted = true;
                            unitOfWork.EIOFormConfirmRepository.Update(form2);
                            unitOfWork.Commit();
                            return true;
                        }
                    }
                    break;
                //2 Phiếu truyền máu
                case "IPDPTM":
                    form = unitOfWork.EIOBloodTransfusionChecklistRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.FirstTechnicianConfirmTime = null;
                        form.HeadOfLabConfirmTime = null;
                        form.NurseConfirmTime = null;
                        form.PhysicianConfirmTime = null;
                        form.SecondTechnicianConfirmTime = null;
                        form.FirstTechnicianId = null;
                        form.SecondTechnicianId = null;
                        form.PhysicianId = null;
                        form.NurseId = null;
                        form.HeadOfLabId = null;
                        unitOfWork.EIOBloodTransfusionChecklistRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //3 Bảng hồi sinh tim phổi
                case "IPDCAR":
                    form = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.FormCompletedTime = null;
                        form.TeamLeaderId = null;
                        form.TeamLeaderTime = null;
                        form.FormCompletedId = null;
                        unitOfWork.EIOCardiacArrestRecordRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //4 Tóm tắt thủ thuật can thiệp động mạch vành
                case "A01_076_050919_VE":
                    form = unitOfWork.IPDCoronaryInterventionRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        unitOfWork.IPDCoronaryInterventionRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //5 Bệnh án ung bướu
                case "A01_196_050919_V":
                    form = unitOfWork.IPDDischargeMedicalReportRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DirectorTime = null;
                        form.DeptHeadTime = null;
                        form.PhysicianInChargeTime = null;
                        unitOfWork.IPDDischargeMedicalReportRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //6 Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện
                case "IPDEXTA":
                    form = unitOfWork.EIOExternalTransportationAssessmentRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorTime = null;
                        form.DoctorId = null;
                        form.NurseTime = null;
                        form.NurseId = null;
                        unitOfWork.EIOExternalTransportationAssessmentRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //7 Bảng điểm GLAMORGAN sàng lọc loét do tỳ ép ở trẻ nhi và sơ sinh 
                case "A02_066_050919_VE":
                    form = unitOfWork.IPDGlamorganRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.NurseConfirmAt = null;
                        form.NurseConfirmId = null;
                        unitOfWork.IPDGlamorganRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //8 Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)
                case "IPDBBHCTDS":
                    form = unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.HospitalDirectorOrHeadDepartmentTime = null;
                        form.DoctorTime = null;
                        form.HospitalDirectorOrHeadDepartmentId = null;
                        form.DoctorId = null;
                        unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //9 Biên bản hội chẩn thông qua mổ
                case "IPDJCFAOS":
                    form = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.CMOId = null;
                        form.HeadOfDeptId = null;
                        form.AnesthetistId = null;
                        form.SurgeonId = null;
                        unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //10 Biên bản hội chẩn
                case "IPDJCGM":
                    var jscmIPD = unitOfWork.EIOJointConsultationGroupMinutesRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (jscmIPD != null)
                    {
                        jscmIPD.ChairmanConfirm = false;
                        //jscmIPD.ChairmanId = null;
                        jscmIPD.SecretaryConfirm = false;
                        //jscmIPD.SecretaryId = null;
                        jscmIPD.MemberConfirm = false;
                        var members = jscmIPD.EIOJointConsultationGroupMinutesMembers.Where(
                                   e => !e.IsDeleted &&
                                   e.IsConfirm &&
                                   e.MemberId != null &&
                                   e.EIOJointConsultationGroupMinutesId == jscmIPD.Id
                               ).ToList();
                        foreach (var member in members)
                        {
                            //member.MemberId = null;
                            member.IsConfirm = false;
                            unitOfWork.EIOJointConsultationGroupMinutesMemberRepository.Update(member, is_anonymous, is_time_change);
                        }
                        unitOfWork.EIOJointConsultationGroupMinutesRepository.Update(jscmIPD, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //11 Bệnh án nhi khoa (General)//5 Bệnh án nhi khoa
                case "A01_037_050919_V":
                    form = unitOfWork.IPDMedicalRecordExtenstionReponsitory.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        unitOfWork.IPDMedicalRecordExtenstionReponsitory.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    form = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                        return true;
                    break;
                //12 Phiếu khai thác tiền sử dùng thuốc
                case "IPDMEDHIS":
                    form = unitOfWork.IPDMedicationHistoryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        form.PharmacistConfirmAt = null;
                        form.PharmacistConfirmId = null;
                        unitOfWork.IPDMedicationHistoryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //13 Phiếu khai thác tiền sử dùng thuốc - Nhi
                case "A03_124_120421_VE":
                    form = unitOfWork.IPDMedicationHistoryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        form.PharmacistConfirmAt = null;
                        form.PharmacistConfirmId = null;
                        unitOfWork.IPDMedicationHistoryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //14 Biên bản kiểm thảo tử vong
                case "EDMORE":
                    var formMortalityIPD = unitOfWork.EIOMortalityReportRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (formMortalityIPD != null)
                    {
                        formMortalityIPD.ChairmanTime = null;
                        formMortalityIPD.SecretaryTime = null;
                        //formMortalityIPD.ChairmanId = null;
                        //formMortalityIPD.SecretaryId = null;
                        var members = formMortalityIPD.EDMortalityReportMembers.Where(e => !e.IsDeleted).ToList(); ;
                        if (members != null)
                        {
                            foreach (var member in members)
                            {
                                member.ConfirmTime = null;
                                //member.MemberId = null;
                                unitOfWork.EIOMortalityReportMemberRepository.Update(member, is_anonymous, is_time_change);
                            }
                        }
                        unitOfWork.EIOMortalityReportRepository.Update(formMortalityIPD, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //15 Phiếu ghi nhận sử dụng thuốc do BN mang vào
                case "IPDMK":
                    form = unitOfWork.EIOPatientOwnMedicationsChartRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.HeadOfPharmacyId = null;
                        form.HeadOfPharmacyTime = null;
                        form.DoctorId = null;
                        form.DoctorTime = null;
                        form.HeadOfDepartmentId = null;
                        form.HeadOfDepartmentTime = null;
                        unitOfWork.EIOPatientOwnMedicationsChartRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //16 Phiếu phẫu thuật/ thủ thuật
                case "IPDPS":
                    form = unitOfWork.EIOProcedureSummaryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureTime = null;
                        form.ProcedureDoctorId = null;
                        unitOfWork.EIOProcedureSummaryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //17 Phiếu tóm tắt thủ thuật
                case "IPDPSV2":
                    form = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureDoctorId = null;
                        form.ProcedureTime = null;
                        unitOfWork.ProcedureSummaryV2Repository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //18 Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát
                case "A01_221_210121_V":
                    form = unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmId = null;
                        form.DoctorConfirmAt = null;
                        unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //19 Phiếu sơ kết 15 ngày điều trị
                case "IPDSO15DT":
                    form = unitOfWork.IPDSummayOf15DayTreatmentRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.HeadOfDepartmentConfirmAt = null;
                        form.HeadOfDepartmentConfirmId = null;
                        form.PhysicianConfirmAt = null;
                        form.PhysicianConfirmId = null;
                        unitOfWork.IPDSummayOf15DayTreatmentRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //20 Giấy chứng nhận phẫu thuật
                case "IPDSURCER":
                    form = unitOfWork.IPDSurgeryCertificateRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureTime = null;
                        form.ProcedureDoctorId = null;
                        form.DeanConfirmTime = null;
                        form.DeanId = null;
                        form.DirectorTime = null;
                        form.DirectorId = null;
                        unitOfWork.IPDSurgeryCertificateRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //21 Phiếu ghi nhận thuốc y lệnh miệng
                case "A03_030_290321_VE":
                    form = unitOfWork.EDVerbalOrderRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                        return true;
                    break;
                //22 Đánh giá ban đầu cho trẻ vừa sinh
                case "A02_016_050919_VE":
                    form = unitOfWork.IPDInitialAssessmentForNewbornsRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.MedicalStaff1ConfirmAt = null;
                        form.MedicalStaff1ConfirmId = null;
                        form.MedicalStaff2ConfirmAt = null;
                        form.MedicalStaff2ConfirmId = null;
                        unitOfWork.IPDInitialAssessmentForNewbornsRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                // Đánh giá ban đầu trẻ sau 2h sinh (đánh giá ban đầu trẻ vừa sinh version2)
                case "A02_016_061022_VE":
                    form = unitOfWork.IPDInitialAssessmentForNewbornsRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.MedicalStaff1ConfirmAt = null;
                        form.MedicalStaff1ConfirmId = null;
                        form.MedicalStaff2ConfirmAt = null;
                        form.MedicalStaff2ConfirmId = null;
                        unitOfWork.IPDInitialAssessmentForNewbornsRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //23 Biên bản hội chẩn sử dụng kháng sinh cần ưu tiên quản lý
                case "IPDHRAC":
                    form = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ConfirmDate = null;
                        form.ConfirmDoctorId = null;
                        unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch
                case "A02_053_OR_201022_V":
                    form = unitOfWork.EIOFormRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.FormId == formId);
                    if (form != null)
                    {
                        var listConfirm = unitOfWork.EIOFormConfirmRepository.Find(e =>
                        e.IsDeleted == false &&
                        e.FormId == formId).ToList();
                        if (listConfirm != null && listConfirm.Any())
                        {
                            foreach (var confirm in listConfirm)
                            {
                                confirm.IsDeleted = true;
                                unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                            }
                        }
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                // Báo cáo y tế ra viện IPD
                case "A01_143_290922_VE":
                    form = unitOfWork.IPDDischargeMedicalReportRepository.Find(e => !e.IsDeleted && e.Id == formId);
                    if(form != null)
                    {
                        form.PhysicianInChargeId = null;
                        form.PhysicianInChargeTime = null;
                        form.DeptHeadId = null;
                        form.DeptHeadTime = null;
                        form.DirectorId = null;
                        form.DirectorTime = null;
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                #endregion Khu vực IPD
                #region Khu vực OPD
                // ---------- Khu vực OPD ---------- //
                //1 Bảng hồi sinh tim phổi
                case "OPDCAARRE":
                    form = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.FormCompletedTime = null;
                        form.TeamLeaderId = null;
                        form.TeamLeaderTime = null;
                        form.FormCompletedId = null;
                        unitOfWork.EIOCardiacArrestRecordRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //2 Phiếu khám lâm sàng vú
                case "A03_116_200520_V":
                    form = unitOfWork.OPDClinicalBreastExamNoteRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        unitOfWork.OPDClinicalBreastExamNoteRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //3 OPDDiseasesCertificationController
                //case "A03_116_200520_V":
                //    form = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(
                //    e => !e.IsDeleted &&
                //    e.VisitId != null &&
                //    e.Id == formId);
                //    if (form != null)
                //        return true;
                //    break;
                //4 Biên bản hội chẩn
                case "OPDJCFM":
                    var jscmOPD = unitOfWork.EIOJointConsultationGroupMinutesRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (jscmOPD != null)
                    {
                        jscmOPD.ChairmanConfirm = false;
                        //jscmOPD.ChairmanId = null;
                        jscmOPD.SecretaryConfirm = false;
                        //jscmOPD.SecretaryId = null;
                        jscmOPD.MemberConfirm = false;
                        var members = jscmOPD.EIOJointConsultationGroupMinutesMembers.Where(
                                   e => !e.IsDeleted &&
                                   e.IsConfirm &&
                                   e.MemberId != null &&
                                   e.EIOJointConsultationGroupMinutesId == jscmOPD.Id
                               ).ToList();
                        foreach (var member in members)
                        {
                            //member.MemberId = null;
                            member.IsConfirm = false;
                            unitOfWork.EIOJointConsultationGroupMinutesMemberRepository.Update(member, is_anonymous, is_time_change);
                        }
                        unitOfWork.EIOJointConsultationGroupMinutesRepository.Update(jscmOPD, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //5 Phiếu phẫu thuật/ thủ thuật
                case "OPDPRSU":
                    form = unitOfWork.EIOProcedureSummaryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureTime = null;
                        form.ProcedureDoctorId = null;
                        unitOfWork.EIOProcedureSummaryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //6 Phiếu tóm tắt thủ thuật
                case "OPDPSV2":
                    form = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureDoctorId = null;
                        form.ProcedureTime = null;
                        unitOfWork.ProcedureSummaryV2Repository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //7 Kết quả test da
                case "OSKTR":
                    form = unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ConfirmDoctorId = null;
                        form.ConfirmDate = null;
                        unitOfWork.EIOSkinTestResultRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //8 Ghi nhận thực hiện thuốc
                case "OPDSTOR":
                    form = unitOfWork.OrderRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                        return true;
                    break;
                //9 Ghi nhận thực hiện thuốc NB dịch vụ lẻ
                case "OPDSORS":
                    form = unitOfWork.OrderRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                        return true;
                    break;
                //10 Thông tin khách hàng đánh giá nguy cơ ung thư
                case "A03_115_200520_V":
                    form = unitOfWork.OPDRiskAssessmentForCancerRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        unitOfWork.OPDRiskAssessmentForCancerRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //11 Biên bản hội chẩn thông qua mổ
                case "A01_059_090822_VE":
                    form = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.CMOId = null;
                        form.HeadOfDeptId = null;
                        form.AnesthetistId = null;
                        form.SurgeonId = null;
                        unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //PHIẾU KHÁM GÂY MÊ
                case "A03_034_200520_VE":
                    form = unitOfWork.OPDOutpatientExaminationNoteRepository.FirstOrDefault(
                        e => !e.IsDeleted && e.Id == formId);
                    if (form != null)
                    {
                        var listConfirm = unitOfWork.EIOFormConfirmRepository.Find(e =>
                       e.IsDeleted == false &&
                       e.FormId == formId
                            ).ToList();
                        if (listConfirm != null && listConfirm.Any())
                        {
                            foreach (var confirm in listConfirm)
                            {
                                confirm.IsDeleted = true;
                                unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                            }
                        }
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //đánh giá nguy cơ thuyên tắc mạch người bệnh nội trú
                case "IPDTRFA":
                    form = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.FirstOrDefault(e => !e.IsDeleted && e.FormCode == "IPDTRFA" && e.Id == formId);
                    if (form != null)
                    {
                        var listConfirm = unitOfWork.EIOFormConfirmRepository.Find(e =>
                       e.IsDeleted == false &&
                       e.FormId == formId
                            ).ToList();
                        if (listConfirm != null && listConfirm.Any())
                        {
                            foreach (var confirm in listConfirm)
                            {
                                confirm.IsDeleted = true;
                                unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                            }
                        }
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                case "A01_049_050919_VE":
                    form = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.FirstOrDefault(e => !e.IsDeleted && e.FormCode == "A01_049_050919_VE" && e.Id == formId);
                    if (form != null)
                    {
                        var listConfirm = unitOfWork.EIOFormConfirmRepository.Find(e =>
                       e.IsDeleted == false &&
                       e.FormId == formId
                            ).ToList();
                        if (listConfirm != null && listConfirm.Any())
                        {
                            foreach (var confirm in listConfirm)
                            {
                                confirm.IsDeleted = true;
                                unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                            }
                        }
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                #endregion Khu vực OPD
                #region Khu vực ED
                // ---------- Khu vực ED ---------- //
                //1 Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện
                case "A02_055_050919_V":
                    form = unitOfWork.EIOExternalTransportationAssessmentRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorTime = null;
                        form.DoctorId = null;
                        form.NurseTime = null;
                        form.NurseId = null;
                        unitOfWork.EIOExternalTransportationAssessmentRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //2 Phiếu truyền máu
                case "A02_74_050919_VE":
                    form = unitOfWork.EIOBloodTransfusionChecklistRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.FirstTechnicianConfirmTime = null;
                        form.HeadOfLabConfirmTime = null;
                        form.NurseConfirmTime = null;
                        form.PhysicianConfirmTime = null;
                        form.SecondTechnicianConfirmTime = null;
                        form.FirstTechnicianId = null;
                        form.SecondTechnicianId = null;
                        form.PhysicianId = null;
                        form.NurseId = null;
                        form.HeadOfLabId = null;
                        unitOfWork.EIOBloodTransfusionChecklistRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //3 Bảng hồi sinh tim phổi
                case "EDCAARRE":
                    form = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.FormCompletedTime = null;
                        form.TeamLeaderId = null;
                        form.TeamLeaderTime = null;
                        form.FormCompletedId = null;
                        unitOfWork.EIOCardiacArrestRecordRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //4 Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)
                case "A01_058_050919_VE":
                    form = unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.HospitalDirectorOrHeadDepartmentTime = null;
                        form.HospitalDirectorOrHeadDepartmentId = null;
                        form.DoctorTime = null;
                        form.DoctorId = null;
                        unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //5 Giấy chứng nhận thương tích
                case "EDEINCE":
                    form = unitOfWork.EDInjuryCertificateRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorId = null;
                        form.DoctorTime = null;
                        form.HeadOfDeptId = null;
                        form.HeadOfDeptTime = null;
                        form.DirectorId = null;
                        form.DirectorTime = null;
                        unitOfWork.EDInjuryCertificateRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //6 Biên bản hội chẩn thông qua mổ
                case "A01_059_120522_VE":
                    form = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.CMOId = null;
                        form.HeadOfDeptId = null;
                        form.AnesthetistId = null;
                        form.SurgeonId = null;
                        unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //7 Phiếu tóm tắt thủ thuật
                case "EDPSV2":
                    form = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureDoctorId = null;
                        form.ProcedureTime = null;
                        unitOfWork.ProcedureSummaryV2Repository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //8 Biên bản kiểm thảo tử vong
                case "IPDMORE":
                    var formMortalityED = unitOfWork.EIOMortalityReportRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (formMortalityED != null)
                    {
                        formMortalityED.ChairmanTime = null;
                        formMortalityED.SecretaryTime = null;
                        //formMortalityED.ChairmanId = null;
                        //formMortalityED.SecretaryId = null;
                        var members = formMortalityED.EDMortalityReportMembers.Where(e => !e.IsDeleted).ToList(); ;
                        if (members != null)
                        {
                            foreach (var member in members)
                            {
                                member.ConfirmTime = null;
                                //member.MemberId = null;
                                unitOfWork.EIOMortalityReportMemberRepository.Update(member, is_anonymous, is_time_change);
                            }
                        }
                        unitOfWork.EIOMortalityReportRepository.Update(formMortalityED, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //9 Phiếu ghi nhận sử dụng thuốc do BN mang vào
                case "A03_051_120421_VE":
                    form = unitOfWork.EIOPatientOwnMedicationsChartRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.HeadOfPharmacyId = null;
                        form.HeadOfPharmacyTime = null;
                        form.DoctorId = null;
                        form.DoctorTime = null;
                        form.HeadOfDepartmentId = null;
                        form.HeadOfDepartmentTime = null;
                        unitOfWork.EIOPatientOwnMedicationsChartRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //10 Kết quả test da
                case "ESKTR":
                    form = unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ConfirmDoctorId = null;
                        form.ConfirmDate = null;
                        unitOfWork.EIOSkinTestResultRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //11 Biên bản hội chẩn sử dụng kháng sinh cần ưu tiên quản lý
                case "A01_060_120421_VE":
                    form = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ConfirmDate = null;
                        form.ConfirmDoctorId = null;
                        unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //12 Phiếu dự trù máu
                case "EDPDTM":
                    form = unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    var tab2ED = from t2 in unitOfWork.EIOBloodSupplyDataRepository.AsQueryable()
                                 where !t2.IsDeleted && t2.EIOBloodRequestSupplyAndConfirmationId == formId
                                 select t2;
                    if (form != null)
                    {
                        //form.SpecialtyId = null; đức ơi là đức không tìm hiểu xem trường này nó làm gì à
                        form.HeadOfDeptId = null;
                        form.HeadOfDeptTime = null;
                        form.DoctorConfirmId = null;
                        form.DoctorConfirmTime = null;
                        unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.Update(form, is_anonymous, is_time_change);
                        if (tab2ED != null && tab2ED.Count() > 0)
                        {
                            foreach (var item in tab2ED)
                            {
                                item.CuratorTime = null;
                                item.CuratorUser = null;
                                item.NurseTime = null;
                                item.NurseUser = null;
                                item.ProviderTime = null;
                                item.ProviderUser = null;
                            }
                        }
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //13.Bệnh án cấp cứu ngoại viện
                case "A03_006_050919_VE":
                    form = unitOfWork.EDAmbulanceRunReportRepository.FirstOrDefault(e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.TransferTime = null;
                        form.TransferId = null;
                        form.AdmitTime = null;
                        form.AdmitId = null;
                        unitOfWork.EDAmbulanceRunReportRepository.Update(form, is_anonymous, is_time_change);
                    }
                    unitOfWork.Commit();
                    return true;
                    break;
                #endregion Khu vực ED
                #region Khu vực EOC
                // ---------- Khu vực EOC ---------- //
                //1 Bảng hồi sinh tim phổi
                case "EOCCAARRE":
                    form = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.FormCompletedTime = null;
                        form.TeamLeaderId = null;
                        form.TeamLeaderTime = null;
                        form.FormCompletedId = null;
                        unitOfWork.EIOCardiacArrestRecordRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //2 Phiếu tóm tắt thủ thuật
                case "EOCPSV2":
                    form = unitOfWork.ProcedureSummaryV2Repository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureDoctorId = null;
                        form.ProcedureTime = null;
                        unitOfWork.ProcedureSummaryV2Repository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //3 Kết quả test da
                case "EOCSKTR":
                    form = unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ConfirmDoctorId = null;
                        form.ConfirmDate = null;
                        unitOfWork.EIOSkinTestResultRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                #endregion Khu vực EOC
                #region General
                // ---------- General ---------- //
                //1 - 2 Biên bản phối hợp với bệnh nhân, gia đình xử lý thai chết lưu
                case "A01_152_100122_VE":
                    form = unitOfWork.StillBirthRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.HospitalLeadershipConfirmAt = null;
                        form.HospitalLeadershipConfirmId = null;
                        form.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmAt = null;
                        form.HeadOfDepartmentOrLeaderOfOnDutyTeamConfirmId = null;
                        unitOfWork.StillBirthRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //3 Bệnh án sơ sinh
                case "A01_038_050919_V":
                    form = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                        return true;
                    break;
                //4 Bệnh án sản khoa
                case "A01_035_050919_V":
                    form = unitOfWork.IPDMedicalRecordPart2DataRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                        return true;
                    break;
                //5 Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+ (IPD,ED)
                case "A03_039_080322_V":
                    form = unitOfWork.EDChemicalBiologyTestRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                    {
                        if (form.VisitTypeGroupCode == "IPD")
                        {
                            Guid? ipdId = form.VisitId;
                            var ipd = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == ipdId);
                            if (ipd?.Version >= 7)
                            {
                                form.ExecutionTime = null;
                                form.ExecutionUserId = null;
                            }
                        }
                        if (form.VisitTypeGroupCode == "ED")
                        {
                            Guid? edId = form.VisitId;
                            var ed = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == edId);
                            if (ed?.Version >= 7)
                            {
                                form.ExecutionTime = null;
                                form.ExecutionUserId = null;
                            }
                        }

                        //form.ExecutionUserId = null;
                        //form.ExecutionTime = null;
                        form.DoctorAcceptId = null;
                        form.AcceptTime = null;
                        unitOfWork.EDChemicalBiologyTestRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //6 Ghi nhận thực hiện thuốc standing order (IPD,OPD,ED,EOC)
                case "A03_029_050919_VE":
                    var formResult = unitOfWork.OrderRepository.AsQueryable().Where(
                    e => !e.IsDeleted &&
                    e.Id == formId).FirstOrDefault();
                    if (formResult != null)
                    {
                        formResult.DoctorConfirm = null;
                        formResult.IsConfirm = false;
                        formResult.DoctorId = null;
                        formResult.DoctorTime = null;
                        formResult.NurseId = null;
                        formResult.NurseTime = null;
                        unitOfWork.OrderRepository.Update(formResult, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //7 Biên bản hội chẩn (ED,EOC)
                case "A01_057_050919_VE":
                    var jscmEIO = unitOfWork.EIOJointConsultationGroupMinutesRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (jscmEIO != null)
                    {
                        jscmEIO.ChairmanConfirm = false;
                        //jscmEIO.ChairmanId = null;
                        jscmEIO.SecretaryConfirm = false;
                        //jscmEIO.SecretaryId = null;
                        jscmEIO.MemberConfirm = false;
                        var members = jscmEIO.EIOJointConsultationGroupMinutesMembers.Where(
                                   e => !e.IsDeleted &&
                                   e.IsConfirm &&
                                   e.MemberId != null &&
                                   e.EIOJointConsultationGroupMinutesId == jscmEIO.Id
                               ).ToList();
                        foreach (var member in members)
                        {
                            //member.MemberId = null;
                            member.IsConfirm = false;
                            unitOfWork.EIOJointConsultationGroupMinutesMemberRepository.Update(member, is_anonymous, is_time_change);
                        }
                        unitOfWork.EIOJointConsultationGroupMinutesRepository.Update(jscmEIO, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //8 Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú // Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú // Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú
                case "A01_201_201119_V":
                    form = unitOfWork.OPDGENBRCARepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.UserConfirmAt = null;
                        form.UserConfirmId = null;
                        unitOfWork.OPDGENBRCARepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    form = unitOfWork.OPDNCCNBROV1Repository.FirstOrDefault(
                        e => !e.IsDeleted &&
                        e.VisitId != null &&
                        e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        unitOfWork.OPDNCCNBROV1Repository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    form = unitOfWork.OPDRiskAssessmentForCancerRepository.FirstOrDefault(
                        e => !e.IsDeleted &&
                        e.VisitId != null &&
                        e.Id == formId);
                    if (form != null)
                    {
                        form.DoctorConfirmAt = null;
                        form.DoctorConfirmId = null;
                        unitOfWork.OPDRiskAssessmentForCancerRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //9 Tóm tắt phẫu thuật (IPD,OPD,ED,EOC)
                case "A01_085_120522_VE":
                    form = unitOfWork.SurgeryAndProcedureSummaryV3Repository.FirstOrDefault(
                        e => !e.IsDeleted &&
                        e.VisitId != null &&
                        e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureDoctorId = null;
                        form.ProcedureTime = null;
                        unitOfWork.SurgeryAndProcedureSummaryV3Repository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //10 Xét nghiệm tại chỗ - Khí máu Cartridge CG4 (IPD,ED)
                case "A03_038_080322_V":
                    form = unitOfWork.EDArterialBloodGasTestRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.Id == formId);
                    if (form != null)
                    {
                        if (form.VisitTypeGroupCode == "IPD")
                        {
                            Guid? ipdId = form.VisitId;
                            var ipd = unitOfWork.IPDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == ipdId);
                            if (ipd?.Version >= 7)
                            {
                                form.ExecutionTime = null;
                                form.ExecutionUserId = null;
                            }
                        }

                        if (form.VisitTypeGroupCode == "ED")
                        {
                            Guid? edId = form.VisitId;
                            var ed = unitOfWork.EDRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == edId);
                            if (ed?.Version >= 7)
                            {
                                form.ExecutionTime = null;
                                form.ExecutionUserId = null;
                            }
                        }
                        form.DoctorAcceptId = null;
                        form.AcceptTime = null;
                        unitOfWork.EDArterialBloodGasTestRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //11 Phiếu phẫu thuật/ thủ thuật (ED,EOC)
                case "A01_085_050919_VE":
                    form = unitOfWork.EIOProcedureSummaryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureTime = null;
                        form.ProcedureDoctorId = null;
                        unitOfWork.EIOProcedureSummaryRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //12 PROM bệnh nhân mạch vành (IPD,OPD)
                case "PROMFCD":
                    form = unitOfWork.PROMForCoronaryDiseaseRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.ProcedureConfirmId = null;
                        form.ProcedureConfirmTime = null;
                        unitOfWork.PROMForCoronaryDiseaseRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //13 PROM bệnh nhân suy tim (IPD,OPD)
                case "PROMFHF":
                    form = unitOfWork.PROMForheartFailureRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.Id == formId);
                    if (form != null)
                    {
                        form.UserConfirmAt = null;
                        form.UserConfirmId = null;
                        unitOfWork.PROMForheartFailureRepository.Update(form, is_anonymous, is_time_change);
                        unitOfWork.Commit();
                        return true;
                    }
                    break;
                //13 Giấy ra viện mở bằng VisitId
                case "A01_146_290922_V":
                    var listConfirmrv = unitOfWork.EIOFormConfirmRepository.Find(e =>
                       e.IsDeleted == false &&
                       e.FormId == formId &&
                       "IPDCONFIRMDISCHAGE,EDCONFIRMDISCHAGE".Contains(e.Note)
               ).ToList();
                    if (listConfirmrv != null && listConfirmrv.Any())
                    {
                        foreach (var confirm in listConfirmrv)
                        {
                            confirm.IsDeleted = true;
                            unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                        }
                        unitOfWork.Commit();
                        return true;
                    }

                    break;
                    #endregion General

            }
            return false;
        }
        private bool CheckIsIdFormMatchToUnlockConfirm(Guid visitId, Guid formId, string formCode)
        {
            bool is_anonymous = true;
            bool is_time_change = false;
            var formConfirm = unitOfWork.EIOFormRepository.FirstOrDefault(e =>
                    !e.IsDeleted &&
                    e.VisitId == visitId &&
                    e.Id == formId &&
                    e.FormCode.Contains(formCode));
            if (formConfirm != null)
            {
                var listConfirm = unitOfWork.EIOFormConfirmRepository.Find(e =>
                        e.IsDeleted == false &&
                        e.FormId == formId
                ).ToList();
                if (listConfirm != null && listConfirm.Any())
                {
                    foreach (var confirm in listConfirm)
                    {
                        confirm.IsDeleted = true;
                        unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                    }
                }
                unitOfWork.Commit();
                return true;
            }
            else
            {
                var listConfirm = unitOfWork.EIOFormConfirmRepository.Find(e =>
                        e.IsDeleted == false &&
                        e.FormId == formId &&
                        e.Note == formCode
                ).ToList();
                if (listConfirm != null && listConfirm.Any())
                {
                    foreach (var confirm in listConfirm)
                    {
                        confirm.IsDeleted = true;
                        unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                    }
                    unitOfWork.Commit();
                    return true;
                }
                var listConfirm2 = unitOfWork.EIOFormConfirmRepository.Find(e =>
                        e.IsDeleted == false &&
                        e.FormId == formId
                ).ToList();
                if (listConfirm2 != null && listConfirm2.Any())
                {
                    foreach (var confirm in listConfirm2)
                    {
                        confirm.IsDeleted = true;
                        unitOfWork.EIOFormConfirmRepository.Update(confirm, is_anonymous, is_time_change);
                    }
                    unitOfWork.Commit();
                    return true;
                }
            }

            return false;
        }
        protected void LockConfirm(Guid formId)
        {
            var getid = unitOfWork.UnlockFormToUpdateRepository.FirstOrDefault(e => !e.IsDeleted &&
               e.VisitId != null &&
               e.FormId != null &&
               e.FormId == formId);
            if (getid != null)
            {
                unitOfWork.UnlockFormToUpdateRepository.Delete(getid);
                unitOfWork.Commit();
            }
        }
        private void ExecuteLog(Guid visitId, Guid formId, string formCode, string userConfirm)
        {
            string action = $"UnlockConfirm FormCode: {formCode}, FormId: {formId.ToString().ToUpper()}";
            string content = $"Unlock For User: {userConfirm}";
            var fullUrl = this.Url.Action("UnlockForm", "AddUnlockConfirm", new { visit_id = visitId }, this.Request.Url.Scheme);
            LogHelper.WriteLogStatus(visitId, action, content, fullUrl, "", formId);
        }
        #endregion Mở khóa xác nhận

    }
}