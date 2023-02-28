using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Models.IPDModels;
using EForm.Models.MedicalRecordModels;
using EForm.Utils;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using static EForm.Models.OrdersResponse;

namespace EForm.Controllers.BaseControllers
{
    public class BaseMedicalRecodeApiController : BaseApiController
    {
        #region IPD
        protected List<MedicalRecordViewModel> GetFormsIPD(IPD ipd)
        {
            var formWithStatusPatient = GetFormWithStatusPatientIPD(ipd);
            var forms = new List<MedicalRecordViewModel>() {
                //GetInitialAssessmentForAdult(ipd),
                GetInitialAssessmentForChemotherapyIPD(ipd),
                GetInitialAssessmentForFrailElderlyIPD(ipd),
                GetFallRiskAssessmentForAdultIPD(ipd.Id),
                GetFallRiskAssessmentForObstetricIPD(ipd.Id),
                //GetMedicalRecord(ipd),
                GetProgressNoteIPD(ipd),
                GetPatientAndFamilyEducationFormIPD(ipd.Id),
                GetCardiacArrestRecordIPD(ipd.Id),
                GetPlanOfCareIPD(ipd.Id),
                GetGuggingSwallowingScreenIPD(ipd.Id),
                GetJointConsultationForApprovalOfSurgeryIPD(ipd.Id),
                GetJointConsultationGroupMinutesIPD(ipd.Id),
                GetTakeCareOfPatientsWithCovid19IPD(ipd),
                GetCareNote(ipd.Id),
                GetPhysicianNote(ipd.Id),
                GetPatientOwnMedicationsChartIPD(ipd.Id),
                GetBloodRequestSupplyAndConfirmationIPD(ipd.Id),
                GetBloodTransfusionChecklistIPD(ipd.Id),
                GetPreOperativeProcedureHandoverChecklistIPD(ipd.Id),
                GetConsultationDrugWithAnAsteriskMarkIPD(ipd),
                GetSurgicalProcedureSafetyChecklistIPD(ipd.Id),
                GetExternalTransportationAssessmentIPD(ipd.Id),
                GetIPDDischargePreparationChecklistRepositoryIPD(ipd.Id),
                GetIPDConfirmDischargeWithoutDirectIPD(ipd.Id),
                GetIPDVitalSignForAdultIPD(ipd.Id),
                GetIPDMortalityReportIPD(ipd.Id),
                GetIPDBradenScaleIPD(ipd.Id),
                GetIPDSurgeryCertificateIPD(ipd.Id),
                //GetProcedureSummaryIPD(ipd.Id),
                GetProcedureSummaryV2IPD(ipd.Id),
                GetIPDSumaryOf15DayTreatmentIPD(ipd.Id),
                GetIPDMedicationHistoryIPD(ipd.Id),
                GetIPDThrombosisRiskFactorAssessmentIPD(ipd.Id),
                GetEIOHighlyRestrictedAntimicrobialConsultIPD(ipd.Id),
                GetIPDInitAssessmentNewbornIPD(ipd.Id),
                GetVitalSignsForPregnantWomanIPD(ipd.Id),
                GetInitialAssementForWomenIPD(ipd.Id),
                IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugIPD(ipd.Id),
                GetIPDVerBalOrderIPD(ipd.Id),
                GetFormIPDThromBosisRickIPD(ipd.Id),
                GetFormCoronaryInterventionIPD(ipd.Id),
                GetFallRiskAssessmentForPediatricIPD(ipd.Id),
                //GetPreAnesthesiaConsultationIPD(ipd.Id),
                GetGlamorganIPD(ipd.Id),
                //GetStillBirthIPD(ipd.Id),
                GetNeonatalObservationChartIPD(ipd.Id),
                GetFormStillBirthOfPatientIPD(ipd.Id),
                //GetScaleForAssessmentOfSuicideIntentIPD(ipd.Id)
                GetIPDMedicationHistoryPediaatricPatient(ipd.Id),
                GetIPDPointOfCareTesting(ipd.Id),
                GetIPDScaleForAssessmentOfSuicideIntent(ipd.Id),
                GetChemicalBiologyTestIPD(ipd.Id),
                GetSurgeryAndProcedureSummaryV3(ipd.Id),
                GetStandingOrderIPD(ipd.Id),
                GetIPDPROMForheartFailure(ipd.Id),
                GetIPDPROMForCoronaryDisease(ipd.Id),
                IPDGetRequestNoCardiopulmonaryResuscitation(ipd.Id),
                GetConsentForOperationOrrProcedure(ipd.Id),
                IPDGetHIVTestingConsent(ipd.Id),
                GetConsentForTransfusionOfBlood(ipd.Id),
                IPDGetCartridgeCelite(ipd.Id),
                IPDCartridgeKaolinACT(ipd.Id),
                IPDAskThePatientToPrepareBeforeSurgery(ipd.Id),
                IPDTakeVegetablesIllegally(ipd.Id),
                IPDUploadImage(ipd.Id),
                IPDForUterineLife2Hours(ipd.Id),
                IPDProcedureSafetyChecklist(ipd.Id),
                EIOPointOfCaseTestingBloodGasAnalysis(ipd.Id, "IPD"),
                GetFormInEIOForm(ipd.Id, "A02_006_221221_V" ,"Đánh giá nguy cơ vi khuẩn đa kháng (MDRO)", "Multidrug-resistant bacteria risk assessment (MDRO)", "MultidrugResistant")
            };
            forms.AddRange(formWithStatusPatient);
            var listMedicalRecordOfpatient = GetListMedicalRecordOfPatientIPD(ipd);
            if (listMedicalRecordOfpatient != null && listMedicalRecordOfpatient.Count > 0)
            {
                forms.AddRange(listMedicalRecordOfpatient);
            }
            forms.AddRange(GetListVitalSignPediatricsIPD(ipd.Id));

            if (ipd.HandOverCheckList?.IsUseHandOverCheckList == false)
            {
                forms.RemoveAll(e => e.Type == "HandOverCheckList");
            }
            var index = forms.FindIndex(x => x.Type.ToUpper() == "MedicalRecord".ToUpper());
            if (index > -1)
            {
                var formCode = unitOfWork.FormRepository.FirstOrDefault(x => x.Name.ToUpper().Trim() == "Bệnh án nội khoa".ToUpper())?.Code;
                if (!string.IsNullOrEmpty(formCode))
                {
                    for (int i = 0; i < forms[index].Datas.Count; i++)
                    {
                        forms[index].Datas[i].Version = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId == ipd.Id && x.FormCode == formCode)?.Version.ToString();
                    }
                }

            }
            return forms;
        }

        private MedicalRecordViewModel EIOPointOfCaseTestingBloodGasAnalysis(Guid visitId, string visitType)
        {
            var form = (from f in unitOfWork.EIOFormRepository.AsQueryable()
                        where !f.IsDeleted && f.VisitId == visitId
                        && f.FormCode == "A03_165_061222_V"
                        && f.VisitTypeGroupCode == visitType
                        orderby f.UpdatedAt descending
                        select f).FirstOrDefault();
            if (form == null)
                return new MedicalRecordViewModel(
                   "Xét nghiệm tại chỗ - Khí máu và điện giải EG7+",
                   "Point of case testing - Blood gas analysis (Cartridge EG7+)",
                   "A03_165_061222_V"
               );

            return new MedicalRecordViewModel(
                "Xét nghiệm tại chỗ - Khí máu và điện giải EG7+",
                "Point of case testing - Blood gas analysis (Cartridge EG7+)",
                "A03_165_061222_V",
                form
            );
        }
        private MedicalRecordViewModel IPDForUterineLife2Hours(Guid visitId)
        {
            var form = (from e in unitOfWork.IPDInitialAssessmentForNewbornsRepository.AsQueryable()
                        where !e.IsDeleted && e.VisitId == visitId && e.Version == 2
                        && e.DataType.ToUpper() == "ForUterineLife2Hours_Obstetrics".ToUpper()
                        orderby e.UpdatedAt descending
                        select e).FirstOrDefault();

            if (form == null)
                return new MedicalRecordViewModel(
                        "Đánh giá tình trạng trẻ trong 2 giờ sau sinh",
                        "Assessment of infant status during the first 2 hours of extrauterine life",
                        "ForUterineLife2Hours_Obstetrics"
                    );

            return new MedicalRecordViewModel(
                        "Đánh giá tình trạng trẻ trong 2 giờ sau sinh",
                        "Assessment of infant status during the first 2 hours of extrauterine life",
                        "ForUterineLife2Hours_Obstetrics",
                        form
                    );
        }
        private MedicalRecordViewModel IPDTakeVegetablesIllegally(Guid visitId)
        {
            //var form = (from f in unitOfWork.EIOFormRepository.AsQueryable()
            //            where !f.IsDeleted && f.VisitId == visitId
            //            && f.FormCode == "A01_159_050919_VE"
            //            && f.VisitTypeGroupCode == "IPD"
            //            select f).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            var form = unitOfWork.EIOFormRepository.Find(f => !f.IsDeleted && f.VisitId == visitId
                        && f.FormCode == "A01_159_050919_VE").OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (form != null)
                return new MedicalRecordViewModel(
                        "Bản cam kết xin lấy bánh rau không theo quy định quản lý của Bệnh viện",
                        "Commitment paper to take the placenta out of the hospital",
                        "IPD_A01_159_050919_VE",
                        form
                    );

            return new MedicalRecordViewModel(
                        "Bản cam kết xin lấy bánh rau không theo quy định quản lý của Bệnh viện",
                        "Commitment paper to take the placenta out of the hospital",
                        "IPD_A01_159_050919_VE"
                    );
        }

        private MedicalRecordViewModel IPDAskThePatientToPrepareBeforeSurgery(Guid visitId)
        {
            var form = (from f in unitOfWork.EIOFormRepository.AsQueryable()
                        where !f.IsDeleted && f.VisitId == visitId
                        && f.FormCode == "A02_052_050919_V"
                        select f).FirstOrDefault();
            if (form != null)
                return new MedicalRecordViewModel(
                        "Yêu cầu người bệnh chuẩn bị trước khi phẫu thuật/ thủ thuật",
                        "Yêu cầu người bệnh chuẩn bị trước khi phẫu thuật/ thủ thuật",
                        "IPD_A02_052_050919_V",
                        form
                    );

            return new MedicalRecordViewModel(
                        "Yêu cầu người bệnh chuẩn bị trước khi phẫu thuật/ thủ thuật",
                        "Yêu cầu người bệnh chuẩn bị trước khi phẫu thuật/ thủ thuật",
                        "IPD_A02_052_050919_V"
                    );
        }

        private MedicalRecordViewModel IPDGetRequestNoCardiopulmonaryResuscitation(Guid visitId)
        {
            var form = unitOfWork.EIOFormRepository.Find(f => !f.IsDeleted && f.VisitId == visitId && f.FormCode == "A01_032_050919_VE").FirstOrDefault();
            if (form != null)
                return new MedicalRecordViewModel(
                        "Yêu cầu không hồi sinh tim phổi",
                        "Do not resuscitation order",
                        "RequestResuscitation",
                        form
                    );

            return new MedicalRecordViewModel(
                        "Yêu cầu không hồi sinh tim phổi",
                        "Do not resuscitation order",
                        "RequestResuscitation"
                    );
        }

        private MedicalRecordViewModel GetStandingOrderIPD(Guid visitId)
        {
            //var _order = (from order in unitOfWork.OrderRepository.AsQueryable()
            //              where !order.IsDeleted && order.VisitId == visitId
            //              && order.OrderType == Constant.IPD_STANDING_ORDER
            //              orderby order.UpdatedAt descending
            //              select order).FirstOrDefault();
            var _order = unitOfWork.OrderRepository.Find(order => !order.IsDeleted && order.VisitId == visitId && order.OrderType == Constant.IPD_STANDING_ORDER).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (_order != null)
                return new MedicalRecordViewModel(
                        "Ghi nhận thực hiện thuốc standing order",
                        "Record administration standing order medication",
                        "StandingOrder",
                        _order
                    );

            return new MedicalRecordViewModel(
                        "Ghi nhận thực hiện thuốc standing order",
                        "Record administration standing order medication",
                        "StandingOrder"
                    );
        }

        private MedicalRecordViewModel GetIPDPointOfCareTesting(Guid visitId)
        {
            //var form = (from f in unitOfWork.EDArterialBloodGasTestRepository.AsQueryable()
            //            where !f.IsDeleted && f.VisitId == visitId && f.VisitTypeGroupCode == "IPD"
            //            orderby f.UpdatedAt descending
            //            select f).FirstOrDefault();
            var form = unitOfWork.EDArterialBloodGasTestRepository.Find(f => !f.IsDeleted && f.VisitId == visitId).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (form != null)
                return new MedicalRecordViewModel(
                        "Xét nghiệm tại chỗ - Khí máu Cartridge CG4+",
                        "Point of care testing - Blood gas analysis Cartridge CG4+",
                        "ArterialBloodGasTest",
                        form
                    );

            return new MedicalRecordViewModel(
                     "Xét nghiệm tại chỗ - Khí máu Cartridge CG4+",
                     "Point of care testing - Blood gas analysis Cartridge CG4+",
                     "ArterialBloodGasTest"
                );
        }

        protected MedicalRecordViewModel GetFormStillBirthOfPatientIPD(Guid visitId)
        {
            //var formx = (from f in unitOfWork.StillBirthRepository.AsQueryable()
            //            where !f.IsDeleted && f.VisitId == visitId && f.VisitTypeGroupCode == "IPD"
            //            orderby f.UpdatedAt descending
            //            select f).FirstOrDefault();

            var form = unitOfWork.StillBirthRepository.Find(f => !f.IsDeleted && f.VisitId == visitId).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (form == null)
                return new MedicalRecordViewModel(
                        "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
                        "Coordinating with the patient/ family to deal with a stillbirth",
                        "CoordinatingWithThePatient"
                    );

            return new MedicalRecordViewModel(
                        "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
                        "Coordinating with the patient/ family to deal with a stillbirth",
                        "CoordinatingWithThePatient",
                        form,
                        "2"
                    );
        }

        protected MedicalRecordViewModel GetNeonatalObservationChartIPD(Guid visitId)
        {
            var forms = unitOfWork.IPDNeonatalObservationChartRepository.Find(e => e.VisitId == visitId && e.IsDeleted).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (forms == null)
                return new MedicalRecordViewModel(
                      "Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ sơ sinh",
                      "Neonatal Observation Chart",
                      "NeonatalObservationChart"
                    );

            return new MedicalRecordViewModel(
                      "Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ sơ sinh",
                      "Neonatal Observation Chart",
                      "NeonatalObservationChart",
                      forms
                    );
        }

        protected List<MedicalRecordViewModel> GetListVitalSignPediatricsIPD(Guid visitId)
        {
            Dictionary<string, string> dic_vital = new Dictionary<string, string>()
            {
                { "A02_036_080322_V", "VitalSignFor1To3"},
                { "A02_035_080322_V", "VitalSignFor3To12"},
                { "A02_034_080322_V", "VitalSignFor1To4"},
                { "A02_033_080322_V", "VitalSignFor4To12"},
                { "A02_032_220321_VE", "VitalSignForOver12"}
            };

            var forms_byVisit = unitOfWork.IPDVitalSignForPediatricsReponsitory.AsQueryable()
                        .Where(f => !f.IsDeleted && f.VisitId == visitId)
                        .ToList();

            List<MedicalRecordViewModel> listItem = new List<MedicalRecordViewModel>();
            foreach (KeyValuePair<string, string> item in dic_vital)
            {
                string formCode = item.Key;
                string type = item.Value;
                var forms = (from f in forms_byVisit
                             where f.FormType == formCode
                             orderby f.UpdatedAt descending
                             select f).ToList();
                MedicalRecordViewModel model;
                if (forms == null || forms.Count == 0)
                    model = new MedicalRecordViewModel(
                            "Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi",
                            "vital signs monitor for pediatric",
                            type
                        );
                else
                    model = new MedicalRecordViewModel(
                            "Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi",
                            "vital signs monitor for pediatric",
                            type,
                            forms[0]
                        );
                listItem.Add(model);
            }

            return listItem;
        }
        protected MedicalRecordViewModel GetFallRiskAssessmentForPediatricIPD(Guid visitId)
        {
            var forms = unitOfWork.IPDFallRiskAssessmentForAdultRepository.Find(
                            f => !f.IsDeleted &&
                            f.IPDId == visitId &&
                            f.FormType == "A02_047_301220_VE"
                        ).OrderByDescending(f => f.UpdatedAt).FirstOrDefault();

            if (forms == null)
                return new MedicalRecordViewModel(
                        "Đánh giá nguy cơ ngã người bệnh nội trú trẻ em",
                        "Fall risk assessment in pediatric inpatients",
                        "FallRiskAssessmentInPediatricInpatients"
                      );

            return new MedicalRecordViewModel(
                        "Đánh giá nguy cơ ngã người bệnh nội trú trẻ em",
                        "Fall risk assessment in pediatric inpatients",
                        "FallRiskAssessmentInPediatricInpatients",
                        forms
                      );
        }

        protected MedicalRecordViewModel GetFormCoronaryInterventionIPD(Guid visit_Id)
        {
            var forms = unitOfWork.IPDCoronaryInterventionRepository.Find(
                            e => !e.IsDeleted
                            && e.VisitId == visit_Id
                        ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (forms == null)
                return new MedicalRecordViewModel(
                        "Tóm tắt thủ thuật can thiệp động mạch vành",
                        "Coronary intervention summary",
                        "CoronaryIntervention"
                      );
            return new MedicalRecordViewModel(
                    "Tóm tắt thủ thuật can thiệp động mạch vành",
                    "Coronary intervention summary",
                    "CoronaryIntervention",
                    forms
                );
        }

        protected MedicalRecordViewModel GetFormIPDThromBosisRickIPD(Guid visit_Id)
        {
            var forms = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.Find(
                            thr => !thr.IsDeleted
                            && thr.IPDId == visit_Id
                            && thr.FormCode == "A01_049_050919_VE"
                        ).OrderByDescending(t => t.UpdatedAt).FirstOrDefault();
            if (forms == null)
                return new MedicalRecordViewModel(
                        "Đánh giá thuyên tắc mạch bệnh nhân ngoại khoa",
                        "Thrombosis risk factor assessment for general surgery patients",
                        "ThrombosisRiskFactorAssessmentFor"
                    );

            return new MedicalRecordViewModel(
                    "Đánh giá thuyên tắc mạch bệnh nhân ngoại khoa",
                    "Thrombosis risk factor assessment for general surgery patients",
                    "ThrombosisRiskFactorAssessmentFor",
                    forms
                );
        }

        protected MedicalRecordViewModel GetIPDVerBalOrderIPD(Guid visit_Id)
        {
            var ipdVerbal = unitOfWork.EDVerbalOrderRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit_Id);
            if (ipdVerbal == null)
                return new MedicalRecordViewModel(
                           "Phiếu ghi nhân thuốc y lệnh miệng",
                           "Verbal Order Form",
                           "VerbalOrderForm"
                        );

            return new MedicalRecordViewModel(
                        "Phiếu ghi nhân thuốc y lệnh miệng",
                        "Verbal Order Form",
                        "VerbalOrderForm",
                        ipdVerbal,
                        "3"
                );
        }

        protected List<MedicalRecordViewModel> GetListMedicalRecordOfPatientIPD(IPD ipd)
        {
            if (ipd == null) return null;
            var visitId = ipd.Id;
            var specialtyId = ipd.SpecialtyId;

            var medicalRecords = (from S in unitOfWork.IPDSetupMedicalRecordRepository.AsQueryable()
                                  join M in unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                                  on S.Formcode equals M.FormCode
                                  where M.VisitId == visitId && S.SpecialityId == specialtyId && !M.IsDeleted
                                  select new
                                  {
                                      ViName = S.ViName,
                                      EnName = S.EnName,
                                      Type = S.Type,
                                      FormId = M.VisitId,
                                      Formcode = M.FormCode
                                  }).ToList().Distinct().ToList();

            List<MedicalRecordViewModel> medicalRecordViewModels = new List<MedicalRecordViewModel>();
            foreach (var item in medicalRecords)
            {

                var times = unitOfWork.IPDMedicalRecordOfPatientRepository.AsQueryable()
                           .Where(m => m.FormCode == item.Formcode && m.VisitId == item.FormId).ToList()
                           .OrderBy(m => m.UpdatedAt == null ? m.CreatedAt : m.UpdatedAt).ToList();


                var medicalRecordViewModel = new MedicalRecordViewModel(item.ViName, item.EnName, item.Type, times[times.Count - 1]);
                medicalRecordViewModels.Add(medicalRecordViewModel);
            }
            return medicalRecordViewModels;
        }

        protected MedicalRecordViewModel IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugIPD(Guid visit_id)
        {
            var mon = unitOfWork.IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository
                      .FirstOrDefault(m => !m.IsDeleted && m.VisitId == visit_id);
            if (mon != null)
            {
                return new MedicalRecordViewModel(
                        "Phiếu theo dõi người bệnh thoát mạch thuốc điều trị ung thư",
                        "Monitoring sheet for patients with extravasation of cancer drugs",
                        "MonitoringSheetForPatientsWith",
                        mon
                    );
            }
            return new MedicalRecordViewModel(
                    "Phiếu theo dõi người bệnh thoát mạch thuốc điều trị ung thư",
                    "Monitoring sheet for patients with extravasation of cancer drugs",
                    "MonitoringSheetForPatientsWith"
                );
        }

        protected MedicalRecordViewModel GetInitialAssementForWomenIPD(Guid visit_id)
        {
            var initial = unitOfWork.IPDMedicalRecordOfPatientRepository.FirstOrDefault(
                    e => !e.IsDeleted
                    && e.VisitId == visit_id
                    && e.FormCode == "A02_069_080121_VE"
                );
            if (initial != null)
            {
                var viewModel = new MedicalRecordViewModel(
                                "Đánh giá ban đầu sản phụ chuyển dạ",
                                "Initial assessment for women in labour",
                                "InitialAssessmentForPregnantWomen",
                                initial
                             );
                return viewModel;
            }
            return new MedicalRecordViewModel(
                    "Đánh giá ban đầu sản phụ chuyển dạ",
                    "Initial assessment for women in labour",
                    "InitialAssessmentForPregnantWomen"
                );
        }

        protected MedicalRecordViewModel GetPreOperativeProcedureHandoverChecklistIPD(Guid visit_id)
        {
            var phc = unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId == visit_id
            );
            if (phc != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                    "Pre-operative verification and handover checklist",
                    "PHC",
                    phc
                );
            return new MedicalRecordViewModel(
                "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                "Pre-operative verification and handover checklist",
                "PHC"
            );
        }
        protected MedicalRecordViewModel GetBloodRequestSupplyAndConfirmationIPD(Guid visit_id)
        {
            var brsc = unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id
            );
            if (brsc == null)
                return new MedicalRecordViewModel(
                    "Phiếu dự trù, cung cấp và xác nhận thực hiện máu - Chế phẩm máu",
                    "Blood Request Supply And Confirmation",
                    "BloodRequestSupplyAndConfirmation"
            );

            var datas = new List<MedicalRecordDataViewModel> {
                new MedicalRecordDataViewModel{
                    ViName = "Dự trù và chế phẩm máu",
                    EnName = "Purchase Request",
                    FormId = brsc.Id,
                    UpdatedAt = brsc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = brsc.UpdatedBy,
                },
                new MedicalRecordDataViewModel{
                    ViName = "Cung cấp máu và chế phẩm máu",
                    EnName = "Supply",
                    FormId = brsc.Id,
                    UpdatedAt = brsc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = brsc.UpdatedBy,
                },
                new MedicalRecordDataViewModel{
                    ViName = "Xác nhận thực hiện truyền máu và chế phẩm máu",
                    EnName = "Transfusion Confirmation",
                    FormId = brsc.Id,
                    UpdatedAt = brsc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = brsc.UpdatedBy,
                },
            };

            return new MedicalRecordViewModel(
                "Phiếu dự trù, cung cấp và xác nhận thực hiện máu - Chế phẩm máu",
                "Blood Request Supply And Confirmation",
                "BloodRequestSupplyAndConfirmation",
                brsc,
                datas
            );
        }
        protected MedicalRecordViewModel GetBloodTransfusionChecklistIPD(Guid visit_id)
        {
            var btcs = unitOfWork.EIOBloodTransfusionChecklistRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id
            ).OrderByDescending(e => e.UpdatedAt).ToList();

            DateTime? created_at = null;
            DateTime? updated_at = null;
            string updated_by = null;
            var datas = new List<MedicalRecordDataViewModel>();
            foreach (var btc in btcs)
            {
                if (created_at == null)
                {
                    created_at = btc.CreatedAt;
                    updated_at = btc.UpdatedAt;
                    updated_by = btc.UpdatedBy;
                }
                datas.Add(new MedicalRecordDataViewModel
                {
                    ViName = "Phiếu truyền máu",
                    EnName = "Blood Transfusion Checklist",
                    FormId = btc.Id,
                    UpdatedAt = btc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = btc.UpdatedBy,
                });
            }

            return new MedicalRecordViewModel(
                "Phiếu truyền máu",
                "Blood Transfusion Checklist",
                "BloodTransfusionChecklist",
                new { CreatedAt = created_at, UpdatedAt = updated_at, UpdatedBy = updated_by },
                datas
            );
        }
        protected MedicalRecordViewModel GetPatientOwnMedicationsChartIPD(Guid visit_id)
        {
            var pom = unitOfWork.EIOPatientOwnMedicationsChartRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id
            );

            if (pom != null)
                return new MedicalRecordViewModel(
                    "Phiếu ghi nhận sử dụng thuốc do người bệnh mang vào",
                    "Patient Own Medications Chart",
                    "PatientOwnMedicationsChart",
                    pom
                );

            return new MedicalRecordViewModel(
                "Phiếu ghi nhận sử dụng thuốc do người bệnh mang vào",
                "Patient Own Medications Chart",
                "PatientOwnMedicationsChart"
            );
        }
        protected MedicalRecordViewModel GetJointConsultationGroupMinutesIPD(Guid visit_id)
        {
            var jcgm = unitOfWork.EIOJointConsultationGroupMinutesRepository.AsQueryable().Where(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "IPD").OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (jcgm != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn",
                    "Joint Consultation Group Minutes",
                    "JointConsultationGroupMinutes",
                    jcgm
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn",
                "Joint Consultation Group Minutes",
                "JointConsultationGroupMinutes"
            );
        }
        protected MedicalRecordViewModel GetInitialAssessmentForAdultIPD(IPD ipd)
        {
            if (ipd.IPDInitialAssessmentForAdultId != null)
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh nội trú thông thường",
                    "Initial assessment for adult inpatient",
                    "InitialAssessmentForAdult",
                    ipd.IPDInitialAssessmentForAdult,
                    "A02_013_220321_VE"
                );

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người bệnh nội trú thông thường",
                "Initial assessment for adult inpatient",
                "InitialAssessmentForAdult"
            );
        }
        protected MedicalRecordViewModel GetInitialAssessmentForChemotherapyIPD(IPD ipd)
        {
            if (ipd.IPDInitialAssessmentForChemotherapyId != null)
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh truyền hóa chất",
                    "Initial assessment for chemotherapy patient",
                    "InitialAssessmentForChemotherapy",
                    ipd.IPDInitialAssessmentForChemotherapy
                );

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người bệnh truyền hóa chất",
                "Initial assessment for chemotherapy patient",
                "InitialAssessmentForChemotherapy"
            );
        }
        protected MedicalRecordViewModel GetInitialAssessmentForFrailElderlyIPD(IPD ipd)
        {
            if (ipd.IPDInitialAssessmentForFrailElderlyId != null)
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh cao tuổi, già yếu/cuối đời",
                    "Initial assessment for frail elderly/ end-of-life patient",
                    "InitialAssessmentForFrailElderly",
                    ipd.IPDInitialAssessmentForFrailElderly
                );

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người bệnh cao tuổi, già yếu/cuối đời",
                "Initial assessment for frail elderly/ end-of-life patient",
                "InitialAssessmentForFrailElderly"
            );
        }
        protected MedicalRecordViewModel GetFallRiskAssessmentForAdultIPD(Guid ipd_id)
        {
            var fall_risk = unitOfWork.IPDFallRiskAssessmentForAdultRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd_id &&
                e.FormType == "A02_048_301220_VE"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (fall_risk != null)
                return new MedicalRecordViewModel(
                    "Đánh giá nguy cơ ngã nội trú người lớn",
                    "Fall Risk Assessment Adult",
                    "FallRiskAssessmentForAdult",
                    new
                    {
                        fall_risk.CreatedAt,
                        fall_risk.UpdatedAt,
                        fall_risk.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Đánh giá nguy cơ ngã nội trú người lớn",
                        EnName = "Fall Risk Assessment Adult",
                        FormId = ipd_id,
                    }}
                );

            return new MedicalRecordViewModel("Đánh giá nguy cơ ngã nội trú người lớn", "Fall Risk Assessment Adult", "FallRiskAssessmentForAdult");
        }
        protected MedicalRecordViewModel GetFallRiskAssessmentForObstetricIPD(Guid ipd_id)
        {
            var fall_risk = unitOfWork.IPDFallRiskAssessmentForObstetricRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd_id
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (fall_risk != null)
                return new MedicalRecordViewModel(
                    "Đánh giá nguy cơ ngã nội trú sản",
                    "Fall Risk Assessment Obstetric",
                    "FallRiskAssessmentForObstetric",
                    new
                    {
                        fall_risk.CreatedAt,
                        fall_risk.UpdatedAt,
                        fall_risk.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Đánh giá nguy cơ ngã nội trú sản",
                        EnName = "Fall Risk Assessment Obstetric",
                        FormId = ipd_id,
                    }}
                );

            return new MedicalRecordViewModel("Đánh giá nguy cơ ngã nội trú sản", "Fall Risk Assessment Obstetric", "FallRiskAssessmentForObstetric");
        }
        protected MedicalRecordViewModel GetMedicalRecordIPD(IPD ipd)
        {
            if (ipd.IPDMedicalRecordId != null)
                return new MedicalRecordViewModel(
                    "Bệnh án nội trú",
                    "Medical record",
                    "MedicalRecordPart1",
                    ipd.IPDMedicalRecord
                );

            return new MedicalRecordViewModel(
                "Bệnh án nội trú",
                "Medical record",
                "MedicalRecordPart1"
            );
        }

        protected MedicalRecordViewModel GetProgressNoteIPD(IPD ipd)
        {
            var progress_note = unitOfWork.IPDPatientProgressNoteDataRepository.Find(
                 e => !e.IsDeleted &&
                 e.IPDPatientProgressNoteId != null &&
                 e.IPDPatientProgressNoteId == ipd.IPDPatientProgressNoteId
             ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (progress_note != null)
                return new MedicalRecordViewModel(
                    "Thêm theo dõi",
                    "Patient Progress Note",
                    "PatientProgressNote",
                    new
                    {
                        progress_note.CreatedAt,
                        progress_note.UpdatedAt,
                        progress_note.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Thêm theo dõi",
                        EnName = "Patient Progress Note",
                        FormId = ipd.Id,
                    }}
                );

            return new MedicalRecordViewModel(
                "Thêm theo dõi",
                "Patient Progress Note",
                "PatientProgressNote"
            );
        }
        protected MedicalRecordViewModel GetTakeCareOfPatientsWithCovid19IPD(IPD ipd)
        {
            var all_data = new List<TrackingModel>();
            var assessment = unitOfWork.IPDTakeCareOfPatientsWithCovid19AssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == ipd.Id
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (assessment != null)
            {
                all_data.Add(new TrackingModel()
                {
                    Id = assessment.Id,
                    CreatedAt = assessment.CreatedAt,
                    UpdatedAt = assessment.UpdatedAt,
                    UpdatedBy = assessment.UpdatedBy
                });
            }
            var Recomment = unitOfWork.IPDTakeCareOfPatientsWithCovid19RecommentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == ipd.Id
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (Recomment != null)
            {
                all_data.Add(new TrackingModel()
                {
                    Id = Recomment.Id,
                    CreatedAt = Recomment.CreatedAt,
                    UpdatedAt = Recomment.UpdatedAt,
                    UpdatedBy = Recomment.UpdatedBy
                });
            }

            var first_item = all_data.OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (first_item != null)
                return new MedicalRecordViewModel(
                    "Phiếu chăm sóc người bệnh Covid-19",
                    "Phiếu chăm sóc người bệnh Covid-19",
                    "TakeCareOfPatientsWithCovid19",
                    new
                    {
                        first_item.CreatedAt,
                        first_item.UpdatedAt,
                        first_item.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Thêm theo dõi",
                        EnName = "Patient Progress Note",
                        FormId = ipd.Id,
                    }}
                );

            return new MedicalRecordViewModel(
                "Phiếu chăm sóc người bệnh Covid-19",
                "Phiếu chăm sóc người bệnh Covid-19",
                "TakeCareOfPatientsWithCovid19"
            );
        }

        protected MedicalRecordViewModel GetPatientAndFamilyEducationFormIPD(Guid ipd_id)
        {

            var gdsk = unitOfWork.PatientAndFamilyEducationRepository.Find(e => !e.IsDeleted &&
                                                                           e.VisitId != null &&
                                                                           !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                                                                           e.VisitId == ipd_id &&
                                                                           e.VisitTypeGroupCode.Equals("IPD")
                                                                           )
                                                                            .OrderByDescending(x => x.UpdatedAt)
                                                                            .FirstOrDefault();
            if (gdsk != null)
                return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF",
                    gdsk
                );

            return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF"
             );
        }

        protected MedicalRecordViewModel GetCardiacArrestRecordIPD(Guid ipd_id)
        {
            var car = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == ipd_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "IPD"
            );

            if (car != null)
                return new MedicalRecordViewModel(
                    "Bảng hồi sinh tim phổi",
                    "Cardiac Arrest Record",
                    "CardiacArrestRecord",
                    car
                );

            return new MedicalRecordViewModel(
                "Bảng hồi sinh tim phổi",
                "Cardiac Arrest Record",
                "CardiacArrestRecord"
            );
        }

        protected MedicalRecordViewModel GetPlanOfCareIPD(Guid ipd_id)
        {
            var plan_of_care = unitOfWork.IPDPlanOfCareRepository.Find(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd_id
            ).OrderBy(e => e.UpdatedAt).FirstOrDefault();

            if (plan_of_care != null)
                return new MedicalRecordViewModel(
                    "Kế hoạch điều trị và chăm sóc",
                    "Plan Of Care",
                    "PlanOfCare",
                    new
                    {
                        plan_of_care.CreatedAt,
                        plan_of_care.UpdatedAt,
                        plan_of_care.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Kế hoạch điều trị và chăm sóc",
                        EnName = "Plan Of Care",
                        FormId = ipd_id,
                    }}
                );

            return new MedicalRecordViewModel(
                "Kế hoạch điều trị và chăm sóc",
                "Plan Of Care",
                "PlanOfCare"
            );
        }

        protected MedicalRecordViewModel GetGuggingSwallowingScreenIPD(Guid ipd_id)
        {
            var guss = unitOfWork.IPDGuggingSwallowingScreenRepository.AsQueryable().OrderByDescending(x => x.UpdatedAt).FirstOrDefault(
                e => !e.IsDeleted &&
                e.IPDId != null &&
                e.IPDId == ipd_id
            );

            if (guss != null)
                return new MedicalRecordViewModel(
                    "Thang điểm GUSS đánh giá rối loạn nuốt cho người bệnh tại giường",
                    "Gugging swallowing screen",
                    "GuggingSwallowingScreen",
                    guss
                );

            return new MedicalRecordViewModel(
                "Thang điểm GUSS đánh giá rối loạn nuốt cho người bệnh tại giường",
                "Gugging swallowing screen",
                "GuggingSwallowingScreen"
            );
        }

        protected MedicalRecordViewModel GetJointConsultationForApprovalOfSurgeryIPD(Guid visit_id)
        {
            var jcfa = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.AsQueryable().Where(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == "IPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (jcfa != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn thông qua mổ",
                    "Joint-Consultation for approval of surgery",
                    "JointConsultationForApprovalOfSurgery",
                    jcfa
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn thông qua mổ",
                "Joint-Consultation for approval of surgery",
                "JointConsultationForApprovalOfSurgery"
            );
        }

        protected MedicalRecordViewModel GetProcedureSummaryIPD(Guid visit_id)
        {
            var procedure = unitOfWork.EIOProcedureSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == "IPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu phẫu thuật/ thủ thuật",
                    "Surgery and procedure Note",
                    "ProcedureSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu phẫu thuật/ thủ thuật",
                "Surgery and procedure Note",
                "ProcedureSummary"
            );
        }
        protected MedicalRecordViewModel GetProcedureSummaryV2IPD(Guid visit_id)
        {
            var procedure = unitOfWork.ProcedureSummaryV2Repository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitType == "IPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu tóm tắt thủ thuật",
                    "Procedure Summary",
                    "TrickSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu tóm tắt thủ thuật",
                "Procedure Summary",
                "TrickSummary"
            );
        }
        protected MedicalRecordViewModel GetConsultationDrugWithAnAsteriskMarkIPD(IPD ipd)
        {
            var forms = unitOfWork.IPDConsultationDrugWithAnAsteriskMarkRepository.AsQueryable()
                       .Where(
                        f => !f.IsDeleted
                        && f.VisitId == ipd.Id
                        ).OrderByDescending(f => f.UpdatedAt).FirstOrDefault();

            if (forms == null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)",
                    "Minutes of consultation for patient using drug with an asterisk mark(*)",
                    "ConsultationDrugWithAnAsteriskMark"
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)",
                "Minutes of consultation for patient using drug with an asterisk mark(*)",
                "ConsultationDrugWithAnAsteriskMark",
                forms
            );
        }
        protected MedicalRecordViewModel GetSurgicalProcedureSafetyChecklistIPD(Guid visit_id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "IPD"
            );

            if (spsc != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn phẫu thuật/thủ thuật",
                    "Surgical Procedure Safety Checklist",
                    "SurgicalProcedureSafetyChecklist",
                    spsc
                );

            return new MedicalRecordViewModel(
                "Bảng kiểm an toàn phẫu thuật/thủ thuật",
                "Surgical Procedure Safety Checklist",
                "SurgicalProcedureSafetyChecklist"
            );
        }

        protected MedicalRecordViewModel GetExternalTransportationAssessmentIPD(Guid visit_id)
        {
            var eta = unitOfWork.EIOExternalTransportationAssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "IPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện",
                    "External Transportation Assessment",
                    "ExternalTransportationAssessment",
                    eta
                );

            return new MedicalRecordViewModel(
                "Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện",
                "External Transportation Assessment",
                "ExternalTransportationAssessment"
            );
        }

        protected MedicalRecordViewModel GetIPDDischargePreparationChecklistRepositoryIPD(Guid visitId)
        {
            var eta = unitOfWork.IPDDischargePreparationChecklistRepository.Find(e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visitId).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm chuẩn bị ra viện",
                    "Hospital Discharge Checklist",
                    "DischargePreparationChecklist",
                    eta
                );

            return new MedicalRecordViewModel(
                "Bảng kiểm chuẩn bị ra viện",
                "Hospital Discharge Checklist",
                "DischargePreparationChecklist"
            );
        }

        protected MedicalRecordViewModel GetIPDConfirmDischargeWithoutDirectIPD(Guid visitId)
        {
            var eta = unitOfWork.IPDConfirmDischargeWithoutDirectRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visitId
            );

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Giấy xác nhận ra viện không theo chỉ định của bác sĩ",
                    "Discharge Againts Medical Advice",
                    "DischargeAgaintsMedicalAdvice",
                    eta
                );

            return new MedicalRecordViewModel(
                "Giấy xác nhận ra viện không theo chỉ định của bác sĩ",
                "Discharge Againts Medical Advice",
                "DischargeAgaintsMedicalAdvice"
            );
        }

        protected MedicalRecordViewModel GetIPDMortalityReportIPD(Guid visitId)
        {
            var eta = unitOfWork.EIOMortalityReportRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visitId
            );

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Biên bản - Trích biên bản kiểm thảo tử vong",
                    "Mortality Report",
                    "MortalityReport",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Biên bản - Trích biên bản kiểm thảo tử vong",
                    "Mortality Report",
                    "MortalityReport"
            );
        }


        protected MedicalRecordViewModel GetIPDVitalSignForAdultIPD(Guid visitId)
        {

            var eta = unitOfWork.IPDVitalSignForAdultRespository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == visitId
                );

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Bảng theo dõi dấu hiệu sinh tồn dành cho người lớn",
                    "Vital Signs For Adult Report",
                    "VitalSignsForAdult",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Bảng theo dõi dấu hiệu sinh tồn dành cho người lớn",
                    "Vital Signs For Adult Report",
                    "VitalSignsForAdult"
            );
        }

        protected MedicalRecordViewModel GetIPDBradenScaleIPD(Guid visitId)
        {

            var eta = unitOfWork.IPDBradenScaleRepository.Find(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == visitId
                ).OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Bảng điểm Braden đánh giá nguy cơ loét",
                    "Abbreviated Braden scale for predicting pressure score risk",
                    "BradenScale",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Bảng điểm Braden đánh giá nguy cơ loét",
                    "Abbreviated Braden scale for predicting pressure score risk",
                    "BradenScale"
            );
        }

        protected MedicalRecordViewModel GetIPDSurgeryCertificateIPD(Guid visitId)
        {

            var eta = unitOfWork.IPDSurgeryCertificateRepository.Find(e => !e.IsDeleted && e.VisitId == visitId).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Giấy chứng nhận phẫu thuật",
                    "Surgery Certificate",
                    "SurgeryCertificate",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Giấy chứng nhận phẫu thuật",
                    "Surgery Certificate",
                    "SurgeryCertificate"
            );
        }
        protected MedicalRecordViewModel GetIPDSumaryOf15DayTreatmentIPD(Guid visitId)
        {

            var eta = unitOfWork.IPDSummayOf15DayTreatmentRepository.Find(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == visitId
                ).OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu sơ kết 15 ngày điều trị",
                    "Sumary Of 15 Day Treatment",
                    "SumaryOf15DayTreatment",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu sơ kết 15 ngày điều trị",
                    "Sumary Of 15 Day Treatment",
                    "SumaryOf15DayTreatment"
            );
        }
        protected MedicalRecordViewModel GetIPDMedicationHistoryIPD(Guid visitId)
        {

            var eta = unitOfWork.IPDMedicationHistoryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == visitId &&
                    e.FormCode == "A03_120_120421_VE"
                );

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu khai thác tiền sử dùng thuốc - Người lớn",
                    "Medication history form - Adult patient",
                    "MedicationHistoryForm",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu khai thác tiền sử dùng thuốc - Người lớn",
                    "Medication history form - Adult patient",
                    "MedicationHistoryForm"
            );
        }
        protected MedicalRecordViewModel GetIPDMedicationHistoryPediaatricPatient(Guid visitId)
        {

            var eta = unitOfWork.IPDMedicationHistoryRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == visitId &&
                    e.FormCode == "A03_124_120421_VE"
                );

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu khai thác tiền sử dùng thuốc - Nhi",
                    "Medication history form - Pediatric patient",
                    "MedicationHistoryFormPediatricPatient",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu khai thác tiền sử dùng thuốc - Nhi",
                    "Medication history form - Pediatric patient",
                     "MedicationHistoryFormPediatricPatient"
            );
        }
        protected MedicalRecordViewModel GetIPDMedicalRecordPediatricIPD(IPD ipd)
        {
            if (ipd.IPDMedicalRecordId != null)
                return new MedicalRecordViewModel(
                     "Bệnh án nhi khoa",
                    "MedicalRecord Pediatric",
                    "MedicalRecordPediatric",
                    ipd.IPDMedicalRecord
                );


            return new MedicalRecordViewModel(
                    "Bệnh án nhi khoa",
                    "MedicalRecord Pediatric",
                    "MedicalRecordPediatric"
            );
        }
        protected List<MedicalRecordViewModel> GetFormWithStatusPatientIPD(IPD ipd)
        {
            List<MedicalRecordViewModel> forms = new List<MedicalRecordViewModel>();
            var statusPatient = ipd.EDStatus.EnName;
            if (statusPatient.Trim().ToUpper().Contains("In hospital".Trim().ToUpper())) return forms;
            var mdrcp2 = ipd?.IPDMedicalRecord?.IPDMedicalRecordPart2;
            dynamic form = mdrcp2;
            Guid id = Guid.NewGuid();
            var list_letter = unitOfWork.FormOfPatientRepository.Find(x => x.Area.ToUpper() == "IPD" && x.EnStatusPatient.ToUpper() == statusPatient.ToUpper()).ToList();
            //Báo cáo y tế 
            var bcyt = list_letter.Where(e => e.TypeName == "MedicalReport");
            if (bcyt != null && bcyt.Any())
            {
                MedicalRecordViewModel medicalReport = bcyt.Select(x => new MedicalRecordViewModel(
              x.ViName, x.EnName, x.TypeName, form
              )).FirstOrDefault();
                forms.Add(medicalReport);
            }

            // báo cáo y tế ra viện DischargeMedicalReport
            var bcytrv = list_letter.Where(e => e.EnName.Trim().ToUpper() == "Discharge Medical Report".Trim().ToUpper());
            if (bcytrv != null && bcytrv.Any())
            {
                var confirm_bcytrv = unitOfWork.IPDDischargeMedicalReportRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == ipd.IPDDischargeMedicalReportId &&
                (e.PhysicianInChargeTime != null || e.DirectorTime != null || e.DeptHeadTime != null));

                if (confirm_bcytrv != null && mdrcp2.UpdatedAt < confirm_bcytrv.UpdatedAt)
                {
                    form = confirm_bcytrv;
                }
                MedicalRecordViewModel dischargeMedicalReport = bcytrv.Select(x => new MedicalRecordViewModel(
                                    x.ViName, x.EnName, x.TypeName, form
                                    )).FirstOrDefault();
                forms.Add(dischargeMedicalReport);
            }

            //giấy ra viện
            var grv = list_letter.Where(e => e.EnName.Trim().ToUpper() == "Hospital discharge record".Trim().ToUpper());
            if (grv != null && grv.Any())
            {
                MedicalRecordViewModel rv = grv.Select(x => new MedicalRecordViewModel(
                   x.ViName, x.EnName, x.TypeName, form
                   )).FirstOrDefault();
                forms.Add(rv);
            }

            //chuyển viện
            if (statusPatient.Trim().ToUpper().Contains("Inter-hospital transfer".Trim().ToUpper()))
            {
                // giấy chuyển viện               

                var gcv = list_letter.Where(e => e.TypeName == "ReferralLetter");
                if (gcv != null && gcv.Any())
                {
                    if (ipd.IPDReferralLetterId != null)
                    {
                        id = ipd.IPDReferralLetterId.Value;
                        var confirm_gcv = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == id).ToList()
                                                                            .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                        if (confirm_gcv != null && mdrcp2.UpdatedAt < confirm_gcv.UpdatedAt)
                        {
                            form = confirm_gcv;
                        }
                    }
                    MedicalRecordViewModel referralLetter = gcv.Select(x => new MedicalRecordViewModel(
                        x.ViName, x.EnName, x.TypeName,
                         form
                        )).FirstOrDefault();
                    forms.Add(referralLetter);
                }
            }
            // chuyển tuyến
            if (statusPatient.Trim().ToUpper().Contains("Upstream/Downstream transfer".Trim().ToUpper()))
            {
                id = ipd.IPDTransferLetterId.Value;
                // giấy chuyển tuyến
                var gct = list_letter.Where(e => e.TypeName == "TransferLetter");
                if (gct != null && gct.Any())
                {
                    if (ipd.IPDTransferLetterId != null)
                    {
                        var confirm_grv = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == id).ToList()
                                                                       .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                        if (confirm_grv != null && mdrcp2.UpdatedAt < confirm_grv.UpdatedAt)
                        {
                            form = confirm_grv;
                        }
                    }
                    MedicalRecordViewModel referralLetter = gct.Select(x => new MedicalRecordViewModel(
                        x.ViName, x.EnName, x.TypeName,
                         form
                        )).FirstOrDefault();
                    forms.Add(referralLetter);
                }
            }


            //List<MedicalRecordViewModel> forms = unitOfWork.FormOfPatientRepository.Find(x => x.Area.ToUpper() == "IPD" && x.EnStatusPatient.ToUpper() == statusPatient.ToUpper()).Select(x => new MedicalRecordViewModel(
            //        x.ViName,
            //        x.EnName,
            //        x.TypeName,
            //        ipd?.IPDMedicalRecord?.IPDMedicalRecordPart2
            //)).ToList();

            //HandOverCheckList Biên bản bàn giao NB chuyển khoa
            //var bbbgnbck = list_letter.Where(e => e.TypeName == "HandOverCheckList");
            //MedicalRecordViewModel _handOverCheckList = bbbgnbck.Select(x => new MedicalRecordViewModel(
            //    x.ViName, x.EnName, x.TypeName, form
            //    )).FirstOrDefault();
            //forms.Add(_handOverCheckList);
            //if (forms.Count > 0)
            //{
            //    var handOverCheckList = ipd.HandOverCheckList;
            //    if (handOverCheckList == null || handOverCheckList.IsUseHandOverCheckList == false)
            //    {
            //        var item = forms.FirstOrDefault(e => e.Type == "HandOverCheckList");
            //        if (item != null)
            //        {
            //            forms.Remove(item);
            //        }
            //    }
            //}
            var yesValue = ipd.IPDMedicalRecord?.IPDMedicalRecordPart3?.IPDMedicalRecordPart3Datas?.FirstOrDefault(x => x.Code == "IPDMRPECOINYES")?.Value;
            var injury = unitOfWork?.IPDInjuryCertificateRepository?.FirstOrDefault(x => x.VisitId == ipd.Id);
            if (!string.IsNullOrEmpty(yesValue))
            {
                try
                {
                    if (Convert.ToBoolean(yesValue))
                    {
                        if (injury == null)
                        {
                            forms.Add(new MedicalRecordViewModel(
                                "Giấy chứng nhận thương tích",
                                "InjuryCertificate",
                                "InjuryCertificate",
                                ipd
                            ));
                        }
                        else
                        {
                            forms.Add(new MedicalRecordViewModel(
                                "Giấy chứng nhận thương tích",
                                "InjuryCertificate",
                                "InjuryCertificate",
                                 injury
                            ));
                        }
                    }
                }
                catch (Exception ex) { }

            }

            return forms;
        }
        protected MedicalRecordViewModel GetIPDInitialAssessmentForPediatricInpatientIPD(IPD ipd)
        {

            if (ipd.IPDInitialAssessmentForAdult != null)
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu nội trú nhi",
                    "Initial Assessment For Pediatric Inpatient",
                    "InitialAssessmentForPediatricInpatient",
                    ipd.IPDInitialAssessmentForAdult,
                    "A02_014_220321_VE"
                );

            return new MedicalRecordViewModel(
                    "Đánh giá ban đầu nội trú nhi",
                    "Initial Assessment For Pediatric Inpatient",
                    "InitialAssessmentForPediatricInpatient"
            );
        }

        protected MedicalRecordViewModel GetIPDThrombosisRiskFactorAssessmentIPD(Guid visitId)
        {

            var eta = unitOfWork.IPDThrombosisRiskFactorAssessmentRepository.FirstOrDefault(
                    e => !e.IsDeleted &&
                    e.IPDId != null &&
                    e.IPDId == visitId &&
                    e.FormCode == "IPDTRFA"
                );

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Đánh giá nguy cơ thuyên tắc mạch",
                    "Thrombosis Risk Factor Assessment",
                    "ThrombosisRiskFactorAssessment",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Đánh giá nguy cơ thuyên tắc mạch",
                    "Thrombosis Risk Factor Assessment",
                    "ThrombosisRiskFactorAssessment"
            );
        }

        protected MedicalRecordViewModel GetEIOHighlyRestrictedAntimicrobialConsultIPD(Guid visitId)
        {
            dynamic visit = GetVisit(visitId, "IPD");
            var hrac = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visitId
            ).OrderByDescending(x => x.UpdatedAt).FirstOrDefault();
            if (hrac != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn sử dụng kháng sinh cần ưu tiên quản lý",
                    "Highly Restricted Antimicrobial Consult",
                    "HighlyRestrictedAntimicrobialConsult",
                    hrac
                );

            return new MedicalRecordViewModel(
                    "Biên bản hội chẩn sử dụng kháng sinh cần ưu tiên quản lý",
                    "Highly Restricted Antimicrobial Consult",
                    "HighlyRestrictedAntimicrobialConsult"
            );
        }
        protected MedicalRecordViewModel GetIPDInitAssessmentNewbornIPD(Guid visitId)
        {

            var eta = unitOfWork.IPDInitialAssessmentForNewbornsRepository
                .Find(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId && x.DataType.ToUpper() != "ForUterineLife2Hours_Obstetrics".ToUpper())
                .OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (eta != null)
            {
                if (eta.Version != 2)
                    return new MedicalRecordViewModel(
                    "Đánh giá ban đầu cho trẻ vừa sinh",
                    "Initial Assessment For Neonatal Maternity",
                    "InitialAssessmentForNeonatalMaternity",
                    eta
                 );
                else if (eta.Version == 2)
                    return new MedicalRecordViewModel(
                     "Đánh giá ban đầu cho trẻ vừa sinh",
                     "Initial Assessment For Neonatal Maternity",
                      eta.DataType,
                      eta
                  );
            }

            return new MedicalRecordViewModel(
                    "Đánh giá ban đầu cho trẻ vừa sinh",
                    "Initial Assessment For Neonatal Maternity",
                    "InitialAssessmentForNeonatalMaternity"
            );
        }

        protected MedicalRecordViewModel GetVitalSignsForPregnantWomanIPD(Guid visitId)
        {
            var eta = unitOfWork.IPDVitalSignForPregnantWomanRepository.Find(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Bảng theo dõi dấu hiệu sinh tồn dành cho sản phụ",
                    "Vital Signs For Pregnant Woman",
                    "VitalSignsForPregnantWoman",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Bảng theo dõi dấu hiệu sinh tồn dành cho sản phụ",
                    "Vital Signs For Pregnant Woman",
                    "VitalSignsForPregnantWoman"
            );
        }
        protected MedicalRecordViewModel GetPreAnesthesiaConsultationIPD(Guid visitId)
        {
            var eta = unitOfWork.PreAnesthesiaConsultationRepository
                .AsQueryable().Where(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu khám gây mê",
                    "Pre Anesthesia Consultation",
                    "PreAnesthesiaConsultation",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu khám gây mê",
                    "Pre Anesthesia Consultation",
                    "PreAnesthesiaConsultation"
            );
        }
        protected MedicalRecordViewModel GetGlamorganIPD(Guid visitId)
        {
            var eta = unitOfWork.IPDGlamorganRepository
                .Find(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Bảng điểm GLAMORGAN sàng lọc loét do tỳ ép ở trẻ nhi và sơ sinh",
                    "Glamorgan pressure injury screening tool",
                    "GlamorganScaleForScreening",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Bảng điểm GLAMORGAN sàng lọc loét do tỳ ép ở trẻ nhi và sơ sinh",
                    "Glamorgan pressure injury screening tool",
                    "GlamorganScaleForScreening"
            );
        }
        protected MedicalRecordViewModel GetPainRecordIPD(Guid visitId)
        {
            var eta = unitOfWork.IPDPainRecordRepository
                .AsQueryable().Where(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Bệnh án đau",
                    "Pain Record",
                    "Pain Record",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Bệnh án đau",
                    "Pain Record",
                    "Pain Record"
            );
        }

        protected MedicalRecordViewModel GetStillBirthIPD(Guid visitId)
        {
            var eta = unitOfWork.StillBirthRepository.Find(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
                    "Report Coordinating with the patient/ family to deal with a stillbirth",
                    "CoordinatingWithThePatient",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
                    "Report Coordinating with the patient/ family to deal with a stillbirth",
                    "CoordinatingWithThePatient"
            );
        }
        protected MedicalRecordViewModel GetIPDScaleForAssessmentOfSuicideIntent(Guid visitId)
        {
            var eta = unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory.
                Find(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát",
                    "Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát",
                    "IPDScaleForAssessmentOfSuicideIntent",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát",
                    "Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát",
                    "IPDScaleForAssessmentOfSuicideIntent"
            );
        }
        //protected MedicalRecordViewModel GetScaleForAssessmentOfSuicideIntentIPD(Guid visitId)
        //{
        //    var eta = unitOfWork.IPDScaleForAssessmentOfSuicideIntentReponsitory
        //        .AsQueryable().Where(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
        //        .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

        //    if (eta != null)
        //        return new MedicalRecordViewModel(
        //            "Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát",
        //            "Rating scale and Degree of Suicide Intent",
        //            "RatingScaleAndDegreeOfSuicideIntent",
        //            eta
        //        );

        //    return new MedicalRecordViewModel(
        //            "Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát",
        //            "Rating scale and Degree of Suicide Intent",
        //            "RatingScaleAndDegreeOfSuicideIntent"
        //    );
        //}

        //protected MedicalRecordViewModel GetStillBirthIPD(Guid visitId)
        //{
        //    var eta = unitOfWork.StillBirthRepository
        //        .AsQueryable().Where(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
        //        .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

        //    if (eta != null)
        //        return new MedicalRecordViewModel(
        //            "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
        //            "Report Coordinating with the patient/ family to deal with a stillbirthn
        //            "ReportCoordinatingWithThePatientFamilyToDealWithAStillbirth",
        //            eta
        //        );

        //    return new MedicalRecordViewModel(
        //            "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
        //            "Report Coordinating with the patient/ family to deal with a stillbirth",
        //            "ReportCoordinatingWithThePatientFamilyToDealWithAStillbirth"
        //    );
        //}
        private MedicalRecordViewModel GetChemicalBiologyTestIPD(Guid visit_id)
        {
            var cbts = unitOfWork.EDChemicalBiologyTestRepository.Find(
                                                                        e => !e.IsDeleted &&
                                                                        e.VisitId != null &&
                                                                        e.VisitId == visit_id
                                                                       )
                                                                       .OrderByDescending(x => x.UpdatedAt)
                                                                       .FirstOrDefault();
            if (cbts != null)
            {
                return new MedicalRecordViewModel(
                                                  "Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+",
                                                  "Point of care testing - Chemistry Catridge CHEM8+",
                                                  "ChemicalBiologyTest",
                                                  cbts
                                                  );
            }
            return new MedicalRecordViewModel(
                                              "Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+",
                                              "Point of care testing - Chemistry Catridge CHEM8+",
                                              "ChemicalBiologyTest"
                                              );
        }
        protected MedicalRecordViewModel GetSurgeryAndProcedureSummaryV3(Guid visitId)
        {
            var procedure = unitOfWork.SurgeryAndProcedureSummaryV3Repository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            var procedurev2 = unitOfWork.EIOProcedureSummaryRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null && procedurev2 != null && procedure.UpdatedAt > procedurev2.UpdatedAt)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure != null && procedurev2 != null && procedure.UpdatedAt < procedurev2.UpdatedAt)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }
            if (procedure != null && procedurev2 == null)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure == null && procedurev2 != null)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }

            return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary"
            );


        }
        protected MedicalRecordViewModel GetIPDPROMForheartFailure(Guid visitId)
        {
            var procedure = unitOfWork.PROMForheartFailureRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "PROM bệnh nhân suy tim",
                    "PROM For Heart Failure",
                    "PROMForheartFailure",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "PROM bệnh nhân suy tim",
                    "PROM For Heart Failure",
                    "PROMForheartFailure"
            );
        }
        protected MedicalRecordViewModel GetIPDPROMForCoronaryDisease(Guid visitId)
        {
            var procedure = unitOfWork.PROMForCoronaryDiseaseRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "PROM bệnh nhân mạch vành",
                    "PROM for coronary disease",
                    "PROMForCoronaryDisease",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "PROM bệnh nhân mạch vành",
                    "PROM for coronary disease",
                    "PROMForCoronaryDisease"
            );
        }
        protected MedicalRecordViewModel GetConsentForOperationOrrProcedure(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.FormCode == "A01_001_080721_V"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure"
            );
        }
        protected MedicalRecordViewModel IPDGetHIVTestingConsent(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.FormCode == "A01_014_050919_VE"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent"
            );
        }

        protected MedicalRecordViewModel GetConsentForTransfusionOfBlood(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.FormCode == "A01_006_080721_V"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu cam kết truyền máu và các chế phẩm máu",
                    "Consent for transfusion of blood and/ or blood derived products",
                    "ConsentForTransfusionOfBlood",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu cam kết truyền máu và các chế phẩm máu",
                    "Consent for transfusion of blood and/ or blood derived products",
                    "ConsentForTransfusionOfBlood"
            );
        }
        protected MedicalRecordViewModel IPDGetCartridgeCelite(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.FormCode == "A03_040_080322_V"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - ACT Cartridge Celite",
                    "Point of case testing - ACT Cartridge Celite",
                    "CartridgeCelite",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - ACT Cartridge Celite",
                    "Point of case testing - ACT Cartridge Celite",
                    "CartridgeCelite"
            );
        }
        protected MedicalRecordViewModel IPDCartridgeKaolinACT(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.FormCode == "A03_041_080322_V"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT"
            );
        }

        protected MedicalRecordViewModel IPDUploadImage(Guid visitId)
        {
            var image = unitOfWork.UploadImageRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitType == "IPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            var form = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "IPD" &&
               e.FormCode == "UPLOADFILE"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (form != null && image != null)
            {
                if (image.UpdatedAt > form.UpdatedAt)
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   image
               );
                }
                else
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   form
               );
                }
            }
            if (form != null && image == null)
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    form
                );
            if (form == null && image != null)
            {
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    image
                );
            }
            return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage"
            );
        }
        protected MedicalRecordViewModel IPDProcedureSafetyChecklist(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "IPD" &&
                e.FormCode.Contains("A02_053_OR_201022_V_TAB")
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist"
            );
        }

        #endregion IPD

        #region ED
        public List<MedicalRecordViewModel> GetFormsED(ED ed)
        {
            var forms = new List<MedicalRecordViewModel>();

            var mandatory_forms = new List<MedicalRecordViewModel> {
                GetEmergencyTriageRecordED(ed),
                GetFallRiskScreenED(ed),
                GetEmergencyRecordED(ed),
                GetDischargeInformationED(ed),
            };
            forms.AddRange(mandatory_forms);

            var table_forms = new List<MedicalRecordViewModel> {
                GetOrderED(ed.Id),
                GetStandingOrderED(ed.Id),
                GetProgressNoteED(ed),
                GetComplexOutpatientCaseSummaryED(ed.CustomerId),
            };
            forms.AddRange(table_forms);
            var formWithStatusPatient = GetFormWithStatusPatientED(ed);
            var option_forms = new List<MedicalRecordViewModel> {
                GetAssessmentForRetailServicePatientED(ed),
                GetStandingOrderForRetailServiceED(ed),
                GetAmbulanceRunReportED(ed),
                GetPreOperativeProcedureHandoverChecklistED(ed.Id),
                GetMonitoringChartAndHandoverFormED(ed),
                //GetPatientHandOverRecordED(ed),
                GetPatientAndFamilyEducationFormED(ed.Id),
                GetChemicalBiologyTestED(ed.Id),
                GetArterialBloodGasTestED(ed.Id),
                GetSkinTestResultED(ed.Id),
                GetConsultationDrugWithAnAsteriskMarkED(ed),
                GetJointConsultationForApprovalOfSurgeryED(ed.Id),
                GetJointConsultationGroupMinutesED(ed.Id),
                GetBloodRequestSupplyAndConfirmationED(ed.Id),
                GetBloodTransfusionChecklistED(ed.Id),
                GetCardiacArrestRecordED(ed.Id),
                GetMortalityReportED(ed.Id, app_version: ed.Version),
                GetPatientOwnMedicationsChartED(ed.Id),
                GetExternalTransportationAssessmentED(ed.Id),
                //GetProcedureSummaryED(ed.Id),
                GetProcedureSummaryV2ED(ed.Id),
                GetHighlyRestrictedAntimicrobialED(ed),
                GetSelfHarmRiskScreeningToolED(ed),
                GetCareNote(ed.Id),
                GetPhysicianNote(ed.Id),
                //GetPreAnesthesiaConsultationED(ed.Id),
                GetEDSurgeryAndProcedureSummaryV3(ed.Id),
                EDGetRequestNoCardiopulmonaryResuscitation(ed.Id),
                EDGetConsentForOperationOrrProcedure(ed.Id),
                EDGetHIVTestingConsent(ed.Id),
                EDGetConsentForTransfusionOfBlood(ed.Id),
                EDGetCartridgeCelite(ed.Id),
                EDCartridgeKaolinACT(ed.Id),
                EDUploadImage(ed.Id),
                EDProcedureSafetyChecklist(ed.Id),
                EIOPointOfCaseTestingBloodGasAnalysis(ed.Id, "ED"),
                GetFormInEIOForm(ed.Id, "A02_006_221221_V" ,"Đánh giá nguy cơ vi khuẩn đa kháng (MDRO)", "Multidrug-resistant bacteria risk assessment (MDRO)", "MultidrugResistant")
            };
            forms.AddRange(option_forms.OrderBy(e => e.CreatedAt).ToList());
            if (formWithStatusPatient != null && formWithStatusPatient.Count > 0)
            {
                forms.AddRange(formWithStatusPatient);
            }
            if (ed.HandOverCheckList?.IsUseHandOverCheckList == false)
            {
                forms.RemoveAll(item => item.Type == "PatientHandOverRecord");
                forms.RemoveAll(item => item.Type == "HandOverCheckList");
            }
            return forms;
        }

        private MedicalRecordViewModel EDGetRequestNoCardiopulmonaryResuscitation(Guid visitId)
        {
            var form = (from f in unitOfWork.EIOFormRepository.AsQueryable()
                        where !f.IsDeleted && f.VisitId == visitId
                        && f.FormCode == "A01_032_050919_VE"
                        && f.VisitTypeGroupCode == "ED"
                        select f).FirstOrDefault();

            if (form != null)
                return new MedicalRecordViewModel(
                        "Yêu cầu không hồi sinh tim phổi",
                        "Do not resuscitation order",
                        "RequestResuscitation",
                        form
                    );

            return new MedicalRecordViewModel(
                        "Yêu cầu không hồi sinh tim phổi",
                        "Do not resuscitation order",
                        "RequestResuscitation"
                    );
        }
        protected MedicalRecordViewModel EDGetConsentForOperationOrrProcedure(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "ED" &&
               e.FormCode == "A01_001_080721_V"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure"
            );
        }
        private MedicalRecordViewModel GetSelfHarmRiskScreeningToolED(ED visit)
        {
            var forms = unitOfWork.EDSelfHarmRiskScreeningToolRepository.Find(e => e.VisitId == visit.Id)
                       .OrderByDescending(f => f.UpdatedAt).ToList();
            if (forms == null || forms.Count == 0)
                return new MedicalRecordViewModel(
                    "Bảng sàng lọc nguy cơ tự hại",
                    "Self-Harm Risk Screening Tool",
                    "SelfHarmRiskScreeningTool"
                );

            return new MedicalRecordViewModel(
                "Bảng sàng lọc nguy cơ tự hại",
                "Self-Harm Risk Screening Tool",
                "SelfHarmRiskScreeningTool",
                forms[0]
            );
        }
        private MedicalRecordViewModel GetHighlyRestrictedAntimicrobialED(ED visit)
        {
            var hrac = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.AsQueryable().OrderByDescending(x => x.UpdatedAt).FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit.Id
            );
            if (hrac != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn sử dụng kháng sinh cần ưu tiên quản lý",
                    "HIGHLY – RESTRICTED ANTIMICROBIAL CONSULT",
                    "HighlyRestrictedAntimicrobialConsult",
                    hrac
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn sử dụng kháng sinh cần ưu tiên quản lý",
                "HIGHLY – RESTRICTED ANTIMICROBIAL CONSULT",
                "AssessmentForRetailServicePatient"
            );
        }

        private MedicalRecordViewModel GetEmergencyTriageRecordED(ED ed)
        {
            var etr = ed.EmergencyTriageRecord;
            if (IsNew(etr.CreatedAt, etr.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Phân loại cấp cứu",
                    "Emergency triage record",
                    "EmergencyTriageRecord"
                );

            return new MedicalRecordViewModel(
                "Phân loại cấp cứu",
                "Emergency triage record",
                "EmergencyTriageRecord",
                etr
            );
        }
        private MedicalRecordViewModel GetFallRiskScreenED(ED ed)
        {
            const string timeUpdate_version2 = "UPDATE_VERSION2_A02_007_220321_VE";
            if (IsVisitLastTimeUpdate(ed, timeUpdate_version2))
            {
                var etr_version3 = (from f in unitOfWork.EDFallRickScreenningRepository.AsQueryable()
                                    where !f.IsDeleted && f.VisitId == ed.Id
                                    orderby f.UpdatedAt descending
                                    select f).FirstOrDefault();

                if (etr_version3 != null)
                    return new MedicalRecordViewModel(
                            "Phiếu sàng lọc nguy cơ ngã",
                            "Fall risk screening",
                            "FallRiskScreening",
                            etr_version3,
                            "3"
                        );

                return new MedicalRecordViewModel(
                            "Phiếu sàng lọc nguy cơ ngã",
                            "Fall risk screening",
                            "FallRiskScreening"
                        );
            }

            var etr = ed.EmergencyTriageRecord;
            if (IsNew(etr.CreatedAt, etr.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Phiếu sàng lọc nguy cơ ngã",
                    "Fall risk screening",
                    "FallRiskScreening"
                );

            return new MedicalRecordViewModel(
                "Phiếu sàng lọc nguy cơ ngã",
                "Fall risk screening",
                "FallRiskScreening",
                etr,
                "2"
            );
        }
        private MedicalRecordViewModel GetEmergencyRecordED(ED ed)
        {
            var emer = ed.EmergencyRecord;
            if (IsNew(emer.CreatedAt, emer.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Bệnh án cấp cứu",
                    "Emergency record",
                    "ER0"
                );

            return new MedicalRecordViewModel(
                "Bệnh án cấp cứu",
                "Emergency record",
                "ER0",
                emer
            );
        }
        private MedicalRecordViewModel GetDischargeInformationED(ED ed)
        {
            var di = ed.DischargeInformation;
            if (IsNew(di.CreatedAt, di.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Đánh giá kết thúc",
                    "Discharge information",
                    "DI0"
                );

            return new MedicalRecordViewModel(
                "Đánh giá kết thúc",
                "Discharge information",
                "DI0",
                di
            );
        }


        private MedicalRecordViewModel GetOrderED(Guid visit_id)
        {
            var order = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.ED_ORDER)
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (order != null)
                return new MedicalRecordViewModel(
                    "Phiếu ghi nhân thuốc y lệnh miệng",
                    "Verbal Order Form",
                    "VerbalOrderForm",
                    new
                    {
                        order.CreatedAt,
                        order.UpdatedAt,
                        order.UpdatedBy
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Phiếu ghi nhân thuốc y lệnh miệng",
                        EnName = "Verbal Order Form",
                        FormId = visit_id,
                    }}
                );

            return new MedicalRecordViewModel("Phiếu ghi nhân thuốc y lệnh miệng",
                           "Verbal Order Form",
                           "VerbalOrderForm");
        }
        private MedicalRecordViewModel GetStandingOrderED(Guid visit_id)
        {
            var standing_order = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.ED_STANDING_ORDER)
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (standing_order != null)
                return new MedicalRecordViewModel(
                    "Ghi nhận thực hiện thuốc standing order",
                    "Record administration standing order medication",
                    "StandingOrder",
                    new
                    {
                        standing_order.CreatedAt,
                        standing_order.UpdatedAt,
                        standing_order.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Ghi nhận thực hiện thuốc standing order",
                        EnName = "Record administration standing order medication",
                        FormId = visit_id,
                    }}
                );

            return new MedicalRecordViewModel("Ghi nhận thực hiện thuốc standing order", "Record administration standing order medication", "StandingOrder");
        }
        private MedicalRecordViewModel GetProgressNoteED(ED ed)
        {
            var ob_chart = unitOfWork.EDObservationChartDataRepository.Find(
                e => !e.IsDeleted &&
                e.ObservationChartId != null &&
                e.ObservationChartId == ed.ObservationChartId
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (ob_chart != null)
                return new MedicalRecordViewModel(
                    "Thêm theo dõi",
                    "Add observation",
                    "PatientProgressNote",
                    new
                    {
                        ob_chart.CreatedAt,
                        ob_chart.UpdatedAt,
                        ob_chart.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Thêm theo dõi",
                        EnName = "Add observation",
                        FormId = ed.Id,
                    }}
                );

            var progress_note = unitOfWork.PatientProgressNoteDataRepository.Find(
                e => !e.IsDeleted &&
                e.PatientProgressNoteId != null &&
                e.PatientProgressNoteId == ed.PatientProgressNoteId
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (progress_note != null)
                return new MedicalRecordViewModel(
                    "Thêm theo dõi",
                    "Add observation",
                    "PatientProgressNote",
                    new
                    {
                        progress_note.CreatedAt,
                        progress_note.UpdatedAt,
                        progress_note.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Thêm theo dõi",
                        EnName = "Add observation",
                        FormId = ed.Id,
                    }}
                );

            return new MedicalRecordViewModel("Thêm theo dõi", "Add observation", "PatientProgressNote");
        }
        private MedicalRecordViewModel GetComplexOutpatientCaseSummaryED(Guid? customer_id)
        {
            var complex = unitOfWork.ComplexOutpatientCaseSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).OrderByDescending(e => e.UpdatedAt).ToList();

            if (complex.Count > 0)
                return new MedicalRecordViewModel(
                    "Tóm tắt ca bệnh phức tạp",
                    "Complex Outpatient Case Summary",
                    "ComplexOutpatientCaseSummary",
                    new
                    {
                        complex[0].CreatedAt,
                        complex[0].UpdatedAt,
                        complex[0].UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Tóm tắt ca bệnh phức tạp",
                        EnName = "Complex Outpatient Case Summary",
                        FormId = customer_id,
                    }}
                );

            return new MedicalRecordViewModel(
                "Tóm tắt ca bệnh phức tạp",
                "Complex Outpatient Case Summary",
                "ComplexOutpatientCaseSummary"
            );
        }


        private MedicalRecordViewModel GetAssessmentForRetailServicePatientED(ED ed)
        {
            if (ed.EDAssessmentForRetailServicePatientId != null)
                return new MedicalRecordViewModel(
                    "Đánh giá người bệnh dịch vụ lẻ",
                    "Assessment For Retail Service Patient",
                    "AssessmentForRetailServicePatient",
                    ed.EDAssessmentForRetailServicePatient
                );

            return new MedicalRecordViewModel(
                "Đánh giá người bệnh dịch vụ lẻ",
                "Assessment For Retail Service Patient",
                "AssessmentForRetailServicePatient"
            );
        }
        private MedicalRecordViewModel GetAmbulanceRunReportED(ED ed)
        {
            if (ed.EDAmbulanceRunReportId != null)
                return new MedicalRecordViewModel(
                    "Bệnh án cấp cứu ngoại viện",
                    "Ambulance Run Report",
                    "AmbulanceRunReport",
                    ed.EDAmbulanceRunReport
                );

            return new MedicalRecordViewModel(
                "Bệnh án cấp cứu ngoại viện",
                "Ambulance Run Report",
                "AmbulanceRunReport"
            );
        }
        private MedicalRecordViewModel GetStandingOrderForRetailServiceED(ED ed)
        {
            if (ed.EDStandingOrderForRetailServiceId != null)
                return new MedicalRecordViewModel(
                    "Ghi nhận thực hiện thuốc NB sử dụng dịch vụ lẻ",
                    "Standing Order For Retail Service",
                    "StandingOrderForRetailService",
                    ed.EDStandingOrderForRetailService
                );

            return new MedicalRecordViewModel(
                "Ghi nhận thực hiện thuốc NB sử dụng dịch vụ lẻ",
                "Standing Order For Retail Service",
                "StandingOrderForRetailService"
            );
        }
        private MedicalRecordViewModel GetPreOperativeProcedureHandoverChecklistED(Guid visit_id)
        {
            var phc = unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitTypeGroupCode == "ED" &&
                e.VisitId == visit_id
            );
            if (phc != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                    "Pre-operative verification and handover checklist",
                    "PHC",
                    phc
                );
            return new MedicalRecordViewModel(
                "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                    "Pre-operative verification and handover checklist",
                    "PHC"
            );
        }
        private MedicalRecordViewModel GetMonitoringChartAndHandoverFormED(ED ed)
        {
            if (ed.MonitoringChartAndHandoverFormId != null)
            {
                var mchf = ed.MonitoringChartAndHandoverForm;
                if (!IsNew(mchf.CreatedAt, mchf.UpdatedAt))
                    return new MedicalRecordViewModel(
                        "Bàn giao vận chuyển",
                        "Hand over form for patients being transferred",
                        "MCA",
                        mchf
                    );
            }

            return new MedicalRecordViewModel(
                "Bàn giao vận chuyển",
                "Hand over form for patients being transferred",
                "MCA"
            );
        }
        private MedicalRecordViewModel GetPatientHandOverRecordED(ED ed)
        {
            if (ed.HandOverCheckListId != null)
                return new MedicalRecordViewModel(
                    "Biên bản bàn giao người bệnh chuyển khoa",
                    "Hand Over Check List",
                    "HandOverCheckList",
                    ed.HandOverCheckList
                );

            return new MedicalRecordViewModel(
                "Biên bản bàn giao người bệnh chuyển khoa",
                "Hand Over Check List",
                "HandOverCheckList"
            );
        }

        private MedicalRecordViewModel GetPatientAndFamilyEducationFormED(Guid visit_id)
        {
            var gdsk = unitOfWork.PatientAndFamilyEducationRepository.Find(e => !e.IsDeleted &&
                                                                          e.VisitId != null &&
                                                                          !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                                                                          e.VisitId == visit_id &&
                                                                          e.VisitTypeGroupCode.Equals("ED")
                                                                         )
                                                                          .ToList()
                                                                          .OrderByDescending(x => x.UpdatedAt)
                                                                          .FirstOrDefault();
            if (gdsk != null)
                return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF",
                    gdsk
                );

            return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF"
             );
        }
        private MedicalRecordViewModel GetChemicalBiologyTestED(Guid visit_id)
        {
            var cbts = unitOfWork.EDChemicalBiologyTestRepository.Find(
                                                                        e => !e.IsDeleted &&
                                                                        e.VisitId != null &&
                                                                        e.VisitId == visit_id &&
                                                                        !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                                                                        e.VisitTypeGroupCode.Equals("ED")
                                                                       ).ToList()
                                                                       .OrderByDescending(x => x.UpdatedAt)
                                                                       .FirstOrDefault();
            if (cbts != null)
            {
                return new MedicalRecordViewModel(
                                                  "Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+",
                                                  "Point of care testing - Chemistry Catridge CHEM8+",
                                                  "ChemicalBiologyTest",
                                                            cbts
                                                         );
            }
            return new MedicalRecordViewModel(
                                               "Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+",
                                               "Point of care testing - Chemistry Catridge CHEM8+",
                                               "ChemicalBiologyTest"
                                                          );
        }
        private MedicalRecordViewModel GetArterialBloodGasTestED(Guid visit_id)
        {
            var abgts = unitOfWork.EDArterialBloodGasTestRepository.AsQueryable()
                .Where(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode.Equals("ED")
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (abgts != null)
                return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Khí máu Cartridge CG4+",
                    "Point of care testing - Blood gas analysis Cartridge CG4+",
                    "ArterialBloodGasTest",
                    abgts
                );

            return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Khí máu Cartridge CG4+",
                    "Point of care testing - Blood gas analysis Cartridge CG4+",
                    "ArterialBloodGasTest"
                );
        }
        private MedicalRecordViewModel GetSkinTestResultED(Guid visit_id)
        {
            var skin_test = unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode.Equals("ED")
            );
            if (skin_test != null)
                return new MedicalRecordViewModel(
                    "Kết quả test da",
                    "Skin test result",
                    "SkinTestResult",
                    skin_test
                );

            return new MedicalRecordViewModel(
                "Kết quả test da",
                "Skin test result",
                "SkinTestResult"
            );
        }
        private MedicalRecordViewModel GetConsultationDrugWithAnAsteriskMarkED(ED ed)
        {
            var forms = unitOfWork.EDConsultationDrugWithAnAsteriskMarkRepository.AsQueryable()
                        .Where(f => !f.IsDeleted && f.VisitId == ed.Id)
                        .OrderByDescending(o => o.UpdatedAt).ToList();

            if (forms == null || forms.Count == 0)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)",
                    "Minutes of consultation for patient using drug with an asterisk mark(*)",
                    "ConsultationDrugWithAnAsteriskMark"
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)",
                "Minutes of consultation for patient using drug with an asterisk mark(*)",
                "ConsultationDrugWithAnAsteriskMark",
                forms[0]
            );
        }
        private MedicalRecordViewModel GetJointConsultationForApprovalOfSurgeryED(Guid visit_id)
        {
            var jcfa = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.AsQueryable()
                        .Where(
                            e => !e.IsDeleted &&
                            e.VisitId == visit_id &&
                            e.VisitTypeGroupCode == "ED"
                        ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (jcfa != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn thông qua mổ",
                    "Joint-Consultation for approval of surgery",
                    "JointConsultationForApprovalOfSurgery",
                    jcfa
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn thông qua mổ",
                "Joint-Consultation for approval of surgery",
                "JointConsultationForApprovalOfSurgery"
            );
        }
        private MedicalRecordViewModel GetJointConsultationGroupMinutesED(Guid visit_id)
        {
            var jcgm = unitOfWork.EIOJointConsultationGroupMinutesRepository.AsQueryable().Where(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED").OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (jcgm != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn",
                    "Joint Consultation Group Minutes",
                    "JointConsultationGroupMinutes",
                    jcgm
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn",
                "Joint Consultation Group Minutes",
                "JointConsultationGroupMinutes"
            );
        }
        private MedicalRecordViewModel GetBloodRequestSupplyAndConfirmationED(Guid visit_id)
        {
            var brsc = unitOfWork.EIOBloodRequestSupplyAndConfirmationRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED"
            );
            if (brsc == null)
                return new MedicalRecordViewModel(
                    "Phiếu dự trù, cung cấp và xác nhận thực hiện máu - Chế phẩm máu",
                    "Blood Request Supply And Confirmation",
                    "BloodRequestSupplyAndConfirmation"
            );

            var datas = new List<MedicalRecordDataViewModel> {
                new MedicalRecordDataViewModel{
                    ViName = "Dự trù và chế phẩm máu",
                    EnName = "Purchase Request",
                    FormId = brsc.Id,
                    UpdatedAt = brsc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = brsc.UpdatedBy,
                },
                new MedicalRecordDataViewModel{
                    ViName = "Cung cấp máu và chế phẩm máu",
                    EnName = "Supply",
                    FormId = brsc.Id,
                    UpdatedAt = brsc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = brsc.UpdatedBy,
                },
                new MedicalRecordDataViewModel{
                    ViName = "Xác nhận thực hiện truyền máu và chế phẩm máu",
                    EnName = "Transfusion Confirmation",
                    FormId = brsc.Id,
                    UpdatedAt = brsc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = brsc.UpdatedBy,
                },
            };

            return new MedicalRecordViewModel(
                "Phiếu dự trù, cung cấp và xác nhận thực hiện máu - Chế phẩm máu",
                "Blood Request Supply And Confirmation",
                "BloodRequestSupplyAndConfirmation",
                brsc,
                datas
            );
        }
        private MedicalRecordViewModel GetBloodTransfusionChecklistED(Guid visit_id)
        {
            var btcs = unitOfWork.EIOBloodTransfusionChecklistRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED"
            ).OrderByDescending(e => e.UpdatedAt);

            DateTime? created_at = null;
            DateTime? updated_at = null;
            string updated_by = null;
            var datas = new List<MedicalRecordDataViewModel>();
            foreach (var btc in btcs)
            {
                if (created_at == null)
                {
                    created_at = btc.CreatedAt;
                    updated_at = btc.UpdatedAt;
                    updated_by = btc.UpdatedBy;
                }
                datas.Add(new MedicalRecordDataViewModel
                {
                    ViName = "Phiếu truyền máu",
                    EnName = "Blood Transfusion Checklist",
                    FormId = btc.Id,
                    UpdatedAt = btc.UpdatedAt,
                    //?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    UpdatedBy = btc.UpdatedBy,
                });
            }

            return new MedicalRecordViewModel(
                "Phiếu truyền máu",
                "Blood Transfusion Checklist",
                "BloodTransfusionChecklist",
                new { CreatedAt = created_at, UpdatedAt = updated_at, UpdatedBy = updated_by },
                datas
            );
        }
        private MedicalRecordViewModel GetCardiacArrestRecordED(Guid visit_id)
        {
            var car = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED"
            );

            if (car != null)
                return new MedicalRecordViewModel(
                    "Bảng hồi sinh tim phổi",
                    "Cardiac Arrest Record",
                    "CardiacArrestRecord",
                    car
                );

            return new MedicalRecordViewModel(
                "Bảng hồi sinh tim phổi",
                "Cardiac Arrest Record",
                "CardiacArrestRecord"
            );
        }
        private MedicalRecordViewModel GetMortalityReportED(Guid visit_id, int app_version)
        {
            var mr = unitOfWork.EIOMortalityReportRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED"
            );

            string viname = "Kiểm thảo tử vong";
            string endname = "Mortality Report";
            string type = "MortalityReport";
            if (app_version >= 11)
            {
                type = "MortalityReportV2";
                viname = "Biên bản - Trích biên bản kiểm thảo tử vong";
                endname = "Minutes - Excerpt from the death audit report";
            }

            if (mr != null)
                return new MedicalRecordViewModel(
                    viname, endname,
                    type,
                    mr
                );

            return new MedicalRecordViewModel(
                viname, endname,
                type
            );
        }
        private MedicalRecordViewModel GetPatientOwnMedicationsChartED(Guid visit_id)
        {
            var pom = unitOfWork.EIOPatientOwnMedicationsChartRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED"
            );

            if (pom != null)
                return new MedicalRecordViewModel(
                    "Phiếu ghi nhận sử dụng thuốc do người bệnh mang vào",
                    "Patient Own Medications Chart",
                    "PatientOwnMedicationsChart",
                    pom
                );

            return new MedicalRecordViewModel(
                "Phiếu ghi nhận sử dụng thuốc do người bệnh mang vào",
                "Patient Own Medications Chart",
                "PatientOwnMedicationsChart"
            );
        }
        private MedicalRecordViewModel GetExternalTransportationAssessmentED(Guid visit_id)
        {
            var eta = unitOfWork.EIOExternalTransportationAssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "ED"
            ).OrderByDescending(e => e.UpdatedAt).ToList();

            if (eta == null || eta.Count == 0)
                return new MedicalRecordViewModel(
                    "Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện",
                    "External Transportation Assessment",
                    "ExternalTransportationAssessment"
                );

            return new MedicalRecordViewModel(
                "Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện",
                "External Transportation Assessment",
                "ExternalTransportationAssessment",
                eta[0]
            );
        }
        private MedicalRecordViewModel GetProcedureSummaryED(Guid visit_id)
        {
            var procedure = unitOfWork.EIOProcedureSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == "ED"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu phẫu thuật/ thủ thuật",
                    "Surgery and procedure Note",
                    "ProcedureSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu phẫu thuật/ thủ thuật",
                "Surgery and procedure Note",
                "ProcedureSummary"
            );
        }
        private MedicalRecordViewModel GetProcedureSummaryV2ED(Guid visit_id)
        {
            var procedure = unitOfWork.ProcedureSummaryV2Repository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitType == "ED"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu tóm tắt thủ thuật",
                    "Procedure Summary",
                    "TrickSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu tóm tắt thủ thuật",
                "Procedure Summary",
                "TrickSummary"
            );
        }
        private List<MedicalRecordViewModel> GetFormWithStatusPatientED(ED ed)
        {
            List<MedicalRecordViewModel> forms = new List<MedicalRecordViewModel>();
            var statusPatient = ed.EDStatus.ViName;
            if (!statusPatient.Trim().ToUpper().Contains("In hospital".Trim().ToUpper()))
            {
                var eddcif = ed?.DischargeInformation;
                dynamic form = eddcif;
                var list_letter = unitOfWork.FormOfPatientRepository.Find(x => x.Area.ToUpper() == "ED" && x.ViStatusPatient.ToUpper() == statusPatient.ToUpper()).ToList();
                //Báo cáo y tế 
                var bcyt = list_letter.Where(e => e.TypeName == "MedicalReport");
                if (bcyt != null && bcyt.Any())
                {
                    var _form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == ed.Id && e.FormCode == "A01_144_050919_VE" && e.VisitTypeGroupCode == "ED");
                    if (_form != null)
                    {
                        var confirm_bcyt = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == _form.Id).ToList()
                                                                        .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                        if (confirm_bcyt != null && eddcif.UpdatedAt < confirm_bcyt.UpdatedAt)
                        {
                            form = confirm_bcyt;
                        }
                    }
                    MedicalRecordViewModel medicalReport = bcyt.Select(x => new MedicalRecordViewModel(
                     x.ViName, x.EnName, x.TypeName, form
                     )).FirstOrDefault();
                    forms.Add(medicalReport);
                }
                form = eddcif;
                // báo cáo y tế ra viện DischargeMedicalReport
                var bcytrv = list_letter.Where(e => e.TypeName == "DischargeMedicalReport");
                if (bcytrv != null && bcytrv.Any())
                {
                    var confirm_bcytrv = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == eddcif.Id).ToList()
                                                                        .OrderByDescending(e => e.CreatedAt).FirstOrDefault();

                    if (confirm_bcytrv != null && eddcif.UpdatedAt < confirm_bcytrv.UpdatedAt)
                    {
                        form = confirm_bcytrv;
                    }
                    MedicalRecordViewModel dischargeMedicalReport = bcytrv.Select(x => new MedicalRecordViewModel(
                                        x.ViName, x.EnName, x.TypeName, form
                                        )).FirstOrDefault();
                    forms.Add(dischargeMedicalReport);
                }
                form = eddcif;
                //giấy ra viện
                var grv = list_letter.Where(e => e.TypeName == "Discharged");
                if (grv != null && grv.Any())
                {
                    var confirm_grv = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == ed.Id).ToList()
                                                                       .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                    if (confirm_grv != null && eddcif.UpdatedAt < confirm_grv.UpdatedAt)
                    {
                        form = confirm_grv;
                    }
                    MedicalRecordViewModel rv = grv.Select(x => new MedicalRecordViewModel(
                       x.ViName, x.EnName, x.TypeName, form
                       )).FirstOrDefault();
                    forms.Add(rv);
                }
                form = eddcif;
                //Biên bản bào giao NB chuyển khoa
                var hovcl = list_letter.Where(e => e.TypeName == "HandOverCheckList");
                if (hovcl != null && hovcl.Any())
                {
                    MedicalRecordViewModel ck = hovcl.Select(x => new MedicalRecordViewModel(
                       x.ViName, x.EnName, x.TypeName, form
                       )).FirstOrDefault();
                    forms.Add(ck);
                }
                form = eddcif;
                //chuyển viện
                if (statusPatient.Trim().ToUpper().Contains("Chuyển viện".Trim().ToUpper()))
                {
                    // giấy chuyển viện
                    var gcv = list_letter.Where(e => e.TypeName == "ReferralLetter");
                    if (gcv != null && gcv.Any())
                    {
                        var _form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == ed.Id && e.FormCode == "A01_145_050919_VE" && e.VisitTypeGroupCode == "ED");
                        if (_form != null)
                        {
                            var confirm_gcv = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == _form.Id).ToList()
                                                                            .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                            if (confirm_gcv != null && eddcif.UpdatedAt < confirm_gcv.UpdatedAt)
                            {
                                form = confirm_gcv;
                            }
                        }
                        MedicalRecordViewModel referralLetter = gcv.Select(x => new MedicalRecordViewModel(
                            x.ViName, x.EnName, x.TypeName,
                             form
                            )).FirstOrDefault();
                        forms.Add(referralLetter);
                    }
                }
                form = eddcif;
                // chuyển tuyến
                if (statusPatient.Trim().ToUpper().Contains("Chuyển tuyến".Trim().ToUpper()))
                {
                    // giấy chuyển tuyến
                    var gct = list_letter.Where(e => e.TypeName == "TransferLetter");
                    if (gct != null && gct.Any())
                    {
                        var _form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == ed.Id && e.FormCode == "A01_167_180220_VE" && e.VisitTypeGroupCode == "ED");
                        if (_form != null)
                        {
                            var confirm_gct = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == _form.Id).ToList()
                                                                            .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                            if (confirm_gct != null && eddcif.UpdatedAt < confirm_gct.UpdatedAt)
                            {
                                form = confirm_gct;
                            }
                        }
                        MedicalRecordViewModel referralLetter = gct.Select(x => new MedicalRecordViewModel(
                            x.ViName, x.EnName, x.TypeName,
                             form
                            )).FirstOrDefault();
                        forms.Add(referralLetter);
                    }
                }
            }
            //List<MedicalRecordViewModel> forms = unitOfWork.FormOfPatientRepository.Find(x => x.Area.ToUpper() == "ED" && x.ViStatusPatient.ToUpper() == statusPatient.ToUpper()).Select(x => new MedicalRecordViewModel(
            //        x.ViName,
            //        x.EnName,
            //        x.TypeName,
            //        ed?.DischargeInformation
            //)).ToList();

            var yesValueEmergency = ed.DischargeInformation?.DischargeInformationDatas?.FirstOrDefault(x => x.Code == "DI0COEMYES")?.Value;
            if (!String.IsNullOrEmpty(yesValueEmergency))
            {
                try
                {
                    if (Convert.ToBoolean(yesValueEmergency))
                    {
                        dynamic _ed = ed;
                        dynamic emergency = null;
                        emergency = ed?.EmergencyRecord;
                        var _form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == ed.Id && e.FormCode == "A01_155_050919_V" && e.VisitTypeGroupCode == "ED");
                        if (_form != null)
                        {
                            var eMCO = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == _form.Id).ToList()
                                                                            .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
                            if (eMCO != null && _ed.UpdatedAt < eMCO.UpdatedAt)
                            {
                                _ed = eMCO;
                            }
                            if (eMCO != null && emergency.UpdatedAt < eMCO.UpdatedAt)
                            {
                                emergency = eMCO;
                            }
                        }
                        if (emergency == null)
                        {
                            forms.Add(new MedicalRecordViewModel(
                               "Giấy xác nhận bệnh nhân cấp cứu",
                               "EMCO",
                               "EMCO",
                               _ed
                           ));
                        }
                        else
                        {
                            forms.Add(new MedicalRecordViewModel(
                              "Giấy xác nhận bệnh nhân cấp cứu",
                              "EMCO",
                              "EMCO",
                              emergency
                          ));
                        }

                    }
                }
                catch (Exception ex)
                {

                }

            }

            var yesValueInjury = ed.DischargeInformation?.DischargeInformationDatas?.FirstOrDefault(x => x.Code == "DI0COINYES")?.Value;
            if (!String.IsNullOrEmpty(yesValueInjury))
            {
                try
                {
                    if (Convert.ToBoolean(yesValueInjury))
                    {
                        var injury = unitOfWork?.EDInjuryCertificateRepository?.FirstOrDefault(x => x.VisitId == ed.Id);
                        if (injury == null)
                        {
                            forms.Add(new MedicalRecordViewModel(
                                "Giấy chứng nhận thương tích",
                                "InjuryCertificate",
                                "InjuryCertificate",
                                ed
                            ));
                        }
                        else
                        {
                            forms.Add(new MedicalRecordViewModel(
                               "Giấy chứng nhận thương tích",
                               "InjuryCertificate",
                               "InjuryCertificate",
                               injury
                           ));
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forms;
        }
        protected MedicalRecordViewModel GetPreAnesthesiaConsultationED(Guid visitId)
        {
            var eta = unitOfWork.PreAnesthesiaConsultationRepository
                .AsQueryable().Where(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu khám gây mê",
                    "Pre Anesthesia Consultation",
                    "PreAnesthesiaConsultation",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu khám gây mê",
                    "Pre Anesthesia Consultation",
                    "PreAnesthesiaConsultation"
            );
        }
        protected MedicalRecordViewModel GetEDSurgeryAndProcedureSummaryV3(Guid visitId)
        {
            var procedure = unitOfWork.SurgeryAndProcedureSummaryV3Repository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitType == "ED"
           ).ToList().OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            var procedurev2 = unitOfWork.EIOProcedureSummaryRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "ED"
           ).ToList().OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null && procedurev2 != null && procedure.UpdatedAt > procedurev2.UpdatedAt)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure != null && procedurev2 != null && procedure.UpdatedAt < procedurev2.UpdatedAt)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }
            if (procedure != null && procedurev2 == null)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure == null && procedurev2 != null)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }

            return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary"
            );
        }
        protected MedicalRecordViewModel EDGetHIVTestingConsent(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "ED" &&
               e.FormCode == "A01_014_050919_VE"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent"
            );
        }

        protected MedicalRecordViewModel EDGetConsentForTransfusionOfBlood(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "ED" &&
               e.FormCode == "A01_006_080721_V"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu cam kết truyền máu và các chế phẩm máu",
                    "Consent for transfusion of blood and/ or blood derived products",
                    "ConsentForTransfusionOfBlood",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu cam kết truyền máu và các chế phẩm máu",
                    "Consent for transfusion of blood and/ or blood derived products",
                    "ConsentForTransfusionOfBlood"
            );
        }
        protected MedicalRecordViewModel EDGetCartridgeCelite(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "ED" &&
                e.FormCode == "A03_040_080322_V"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - ACT Cartridge Celite",
                    "Point of case testing - ACT Cartridge Celite",
                    "CartridgeCelite",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - ACT Cartridge Celite",
                    "Point of case testing - ACT Cartridge Celite",
                    "CartridgeCelite"
            );
        }
        protected MedicalRecordViewModel EDCartridgeKaolinACT(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "ED" &&
                e.FormCode == "A03_041_080322_V"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT"
            );
        }
        protected MedicalRecordViewModel EDProcedureSafetyChecklist(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "ED" &&
                 e.FormCode.Contains("A02_053_OR_201022_V_TAB")
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist"
            );
        }
        protected MedicalRecordViewModel EDUploadImage(Guid visitId)
        {
            var image = unitOfWork.UploadImageRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitType == "ED"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            var form = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted &&
              e.VisitId == visitId &&
              e.VisitTypeGroupCode == "ED" &&
              e.FormCode == "UPLOADFILE"
          ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (form != null && image != null)
            {
                if (image.UpdatedAt > form.UpdatedAt)
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   image
               );
                }
                else
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   form
               );
                }
            }
            if (form != null && image == null)
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    form
                );
            if (form == null && image != null)
            {
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    image
                );
            }
            return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage"
            );
        }

        #endregion
        #region OPD
        public List<MedicalRecordViewModel> GetFormsOPD(OPD opd)
        {
            var forms = new List<MedicalRecordViewModel>();
            var mandatory_forms = new List<MedicalRecordViewModel> {
                GetInitialAssessmentForShortTermOPD(opd),
                GetInitialAssessmentForOnGoingOPD(opd),
                GetFallRiskScreeningOPD(opd),
                GetOutpatientExaminationNoteOPD(opd),
                GetOutpatientExaminationNoteOPDAnesthesia(opd)
            };
            forms.AddRange(mandatory_forms);

            var table_forms = new List<MedicalRecordViewModel> {
                GetStandingOrderOPD(opd.Id),
                GetProgressNoteOPD(opd),
                GetComplexOutpatientCaseSummaryOPD(opd.CustomerId),
            };
            forms.AddRange(table_forms);

            var option_forms = new List<MedicalRecordViewModel> {
                GetInitialAssessmentForTelehealthOPD(opd),
                //GetPatientHandOverRecord(opd),
                //GetProcedureSummaryOPD(opd.Id),
                GetProcedureSummaryV2OPD(opd.Id),
                GetSkinTestResultOPD(opd.Id),
                GetPreOperativeProcedureHandoverChecklistOPD(opd.Id),
                GetJointConsultationGroupMinutesOPD(opd.Id),
                GetSurgicalProcedureSafetyChecklistOPD(opd.Id),
                GetCardiacArrestRecordOPD(opd.Id),
                GetAssessmentForRetailServicePatientOPD(opd),
                GetStandingOrderForRetailServiceOPD(opd),
                GetStillBirthOPD(opd.Id),
                GetNCCNBROV1OPD(opd.Id),
                GetOPDGENBRCAROPD(opd.Id),
                GetClinicalBreastExamNoteOPD(opd.Id),
                GetRiskAssessmentForCancerOPD(opd.Id),
                GetIPDMedicationHistoryPediaatricPatient(opd.Id),
                GetOPDDiseasesCertification(opd),
                GetPatientAndFamilyEducationFormOPD(opd.Id),
                GetOPDSurgeryAndProcedureSummaryV3(opd.Id),
                GetOPDPROMForheartFailure(opd.Id),
                GetOPDPROMForCoronaryDisease(opd.Id),
                OPDGetConsentForOperationOrrProcedure(opd.Id),
                OPDGetHIVTestingConsent(opd.Id),
                GetJointConsultationForApprovalOfSurgeryOPD(opd.Id),
                OPDCartridgeKaolinACT(opd.Id),
                OPDPregnancyFollowUpRecord(opd.Id),
                OPDUploadImage(opd.Id),
                OPDRequesForPreSurgeryRespiratoryConsultation(opd.Id),
                OPDRequesForPreSurgeryCardiologyConsultation(opd.Id),
                OPDrocedureSafetyChecklist(opd.Id),
                OPDForChemotherapyPatient(opd.Id),
                GetFormInEIOForm(opd.Id, "A02_006_221221_V" ,"Đánh giá nguy cơ vi khuẩn đa kháng (MDRO)", "Multidrug-resistant bacteria risk assessment (MDRO)", "MultidrugResistant")
            };

            try
            {
                forms.AddRange(option_forms.OrderBy(e => e.CreatedAt).ToList());
            }
            catch
            {

            }

            var formWithStatusPatient = GetFormWithStatusPatientOPD(opd);
            if (formWithStatusPatient != null && formWithStatusPatient.Count > 0)
            {
                forms.AddRange(formWithStatusPatient);

            }
            if (opd.OPDHandOverCheckList?.IsUseHandOverCheckList == false)
            {
                forms.RemoveAll(e => e.Type == "HandOverCheckList");
            }
            return forms;
        }

        private MedicalRecordViewModel OPDRequesForPreSurgeryCardiologyConsultation(Guid visitId)
        {
            var form = (from f in unitOfWork.EIOFormRepository.AsQueryable()
                        where !f.IsDeleted && f.VisitId == visitId
                        && f.FormCode == "A01_204_030320_VE"
                        && f.VisitTypeGroupCode == "OPD"
                        select f).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (form != null)
                return new MedicalRecordViewModel(
                        "Phiếu yêu cầu khám chuyên khoa tim mạch tiền phẫu",
                        "Request For Pre Surgery Cardiology Consultation",
                        "OPD_A01_204_030320_VE",
                        form
                    );

            return new MedicalRecordViewModel(
                        "Phiếu yêu cầu khám chuyên khoa tim mạch tiền phẫu",
                        "Request For Pre Surgery Cardiology Consultation",
                        "OPD_A01_204_030320_VE"
                    );
        }

        private MedicalRecordViewModel OPDRequesForPreSurgeryRespiratoryConsultation(Guid visitId)
        {
            var form = (from f in unitOfWork.EIOFormRepository.AsQueryable()
                        where !f.IsDeleted && f.VisitId == visitId
                        && f.FormCode == "FORMOPDPYKKCKHHTP"
                        && f.VisitTypeGroupCode == "OPD"
                        select f).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (form != null)
                return new MedicalRecordViewModel(
                        "Phiếu yêu cầu khám chuyên khoa hô hấp tiền phẫu",
                        "Request For Pre Surgery Respiratory Consultation",
                        "OPDPYCKCKHHTP",
                        form
                    );

            return new MedicalRecordViewModel(
                        "Phiếu yêu cầu khám chuyên khoa hô hấp tiền phẫu",
                        "Request For Pre Surgery Respiratory Consultation",
                        "OPDPYCKCKHHTP"
                    );
        }

        private MedicalRecordViewModel GetOPDDiseasesCertification(OPD visit)
        {
            var outPatientExam = visit.OPDOutpatientExaminationNote;
            if (outPatientExam != null)
            {
                var data = outPatientExam.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted && e.Code == "OPDOENXNBTYES").FirstOrDefault();
                if (data?.Value?.ToUpper() == "TRUE")
                    return new MedicalRecordViewModel(
                            "Giấy xác nhận bệnh tật",
                            "Diseases certification",
                            "DiseasesCertification",
                            outPatientExam
                        );

            }

            return new MedicalRecordViewModel(
                    "Giấy xác nhận bệnh tật",
                    "Diseases certification",
                    "DiseasesCertification"
                );
        }

        private MedicalRecordViewModel GetInitialAssessmentForShortTermOPD(OPD opd)
        {
            var iafst = opd.OPDInitialAssessmentForShortTerm;
            if (IsNew(iafst.CreatedAt, iafst.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh ngoại trú thông thường",
                    "Initial assessment for short term",
                    "InitialAssessmentForShortTerm"
                );

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người bệnh ngoại trú thông thường",
                "Initial assessment for short term",
                "InitialAssessmentForShortTerm",
                iafst
            );
        }
        private MedicalRecordViewModel GetInitialAssessmentForOnGoingOPD(OPD opd)
        {
            if (opd?.OPDInitialAssessmentForOnGoingId != null)
            {
                var iafog = opd.OPDInitialAssessmentForOnGoing?.OPDInitialAssessmentForOnGoingDatas.ToList();
                if (iafog != null && iafog.Count > 0)
                {
                    return new MedicalRecordViewModel(
                      "Đánh giá ban đầu người bệnh ngoại trú dài hạn",
                      "Initial assessment for on-going",
                      "InitialAssessmentForOnGoing",
                      opd.OPDInitialAssessmentForOnGoing
                  );
                }
            }

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người bệnh ngoại trú dài hạn",
                "Initial assessment for on-going",
                "InitialAssessmentForOnGoing"

            );
        }
        private MedicalRecordViewModel GetFallRiskScreeningOPD(OPD opd)
        {
            var frs = unitOfWork.OPDFallRiskScreeningRepository.Find(x => !x.IsDeleted && x.VisitId == opd.Id).OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();
            //var frs = opd.OPDFallRiskScreening;
            if (frs == null)
                return new MedicalRecordViewModel(
                    "Phiếu sàng lọc nguy cơ ngã",
                    "Fall risk screening",
                    "FallRiskScreening"
                );

            return new MedicalRecordViewModel(
                "Phiếu sàng lọc nguy cơ ngã",
                "Fall risk screening",
                "FallRiskScreening",
                frs
            );
        }
        private MedicalRecordViewModel GetOutpatientExaminationNoteOPD(OPD opd)
        {
            var oen = opd.OPDOutpatientExaminationNote;
            if (opd?.IsAnesthesia == false)
            {
                if (IsNew(oen.CreatedAt, oen.UpdatedAt))
                {
                    if (oen.IsConsultation == true)
                        return new MedicalRecordViewModel(
                        "Phiếu tư vấn",
                        "Counseling",
                        "OutpatientExaminationNote"
                    );
                    if (opd.IsTelehealth == true)
                    {
                        return new MedicalRecordViewModel(
                       "Phiếu khám Telehealth",
                       "Telehealth Examination Note",
                       "TelehealthExaminationNote"
                       );
                    }

                    return new MedicalRecordViewModel(
                        "Phiếu khám ngoại trú",
                        "Outpatient examination note",
                        "OutpatientExaminationNote"
                    );
                }

                if (oen?.IsConsultation == true)
                    return new MedicalRecordViewModel(
                    "Phiếu tư vấn",
                    "Counseling",
                    "OutpatientExaminationNote",
                    oen
                );
                if (opd.IsTelehealth)
                {
                    return new MedicalRecordViewModel(
                     "Phiếu khám Telehealth",
                     "Telehealth Examination Note",
                    "TelehealthExaminationNote",
                    oen
                );
                }

                return new MedicalRecordViewModel(
                    "Phiếu khám ngoại trú",
                    "Outpatient examination note",
                    "OutpatientExaminationNote",
                    oen
                );
            }

            return new MedicalRecordViewModel(
                    "Phiếu khám ngoại trú",
                    "Outpatient examination note",
                    "OutpatientExaminationNote"
                );
        }
        private MedicalRecordViewModel GetOutpatientExaminationNoteOPDAnesthesia(OPD opd)
        {
            var oen = opd.OPDOutpatientExaminationNote;
            if (opd?.IsAnesthesia == true)
            {
                if (IsNew(oen.CreatedAt, oen.UpdatedAt))
                {
                    return new MedicalRecordViewModel(
                        "Phiếu khám gây mê",
                        "Pre-Anesthesia Consultation (PAC)",
                        "PreAnesthesiaConsultation"
                    );
                }
                return new MedicalRecordViewModel(
                     "Phiếu khám gây mê",
                        "Pre-Anesthesia Consultation (PAC)",
                        "PreAnesthesiaConsultation",
                    oen
                );
            }
            return new MedicalRecordViewModel(
                        "Phiếu khám gây mê",
                        "Pre-Anesthesia Consultation (PAC)",
                        "PreAnesthesiaConsultation"
                    );
        }

        private MedicalRecordViewModel GetStandingOrderOPD(Guid visit_id)
        {
            var standing_order = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.OPD_STANDING_ORDER)
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (standing_order != null)
                return new MedicalRecordViewModel(
                    "Ghi nhận thực hiện thuốc standing order",
                    "Record administration standing order medication",
                    "StandingOrder",
                    new
                    {
                        standing_order.CreatedAt,
                        standing_order.UpdatedAt,
                        standing_order.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Ghi nhận thực hiện thuốc standing order",
                        EnName = "Record administration standing order medication",
                        FormId = visit_id,
                    }}
                );

            return new MedicalRecordViewModel("Ghi nhận thực hiện thuốc standing order", "Record administration standing order medication", "StandingOrder");
        }
        private MedicalRecordViewModel GetProgressNoteOPD(OPD opd)
        {
            var progress_note = unitOfWork.OPDPatientProgressNoteDataRepository.Find(
                e => !e.IsDeleted &&
                e.OPDPatientProgressNoteId != null &&
                e.OPDPatientProgressNoteId == opd.OPDPatientProgressNoteId
            ).OrderByDescending(e => e.UpdatedAt).ToList();

            if (progress_note.Count > 0)
                return new MedicalRecordViewModel(
                    "Thêm theo dõi",
                    "Patient Progress Note",
                    "PatientProgressNote",
                    new
                    {
                        progress_note[0].CreatedAt,
                        progress_note[0].UpdatedAt,
                        progress_note[0].UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Thêm theo dõi",
                        EnName = "Patient Progress Note",
                        FormId = opd.Id,
                    }}
                );

            return new MedicalRecordViewModel(
                "Thêm theo dõi",
                "Patient Progress Note",
                "PatientProgressNote"
            );
        }
        private MedicalRecordViewModel GetComplexOutpatientCaseSummaryOPD(Guid? customer_id)
        {
            var complex = unitOfWork.ComplexOutpatientCaseSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).OrderByDescending(e => e.UpdatedAt).ToList();

            if (complex.Count > 0)
                return new MedicalRecordViewModel(
                    "Tóm tắt ca bệnh phức tạp",
                    "Complex Outpatient Case Summary",
                    "ComplexOutpatientCaseSummary",
                    new
                    {
                        complex[0].CreatedAt,
                        complex[0].UpdatedAt,
                        complex[0].UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Tóm tắt ca bệnh phức tạp",
                        EnName = "Complex Outpatient Case Summary",
                        FormId = customer_id,
                    }}
                );

            return new MedicalRecordViewModel(
                "Tóm tắt ca bệnh phức tạp",
                "Complex Outpatient Case Summary",
                "ComplexOutpatientCaseSummary"
            );
        }


        private MedicalRecordViewModel GetInitialAssessmentForTelehealthOPD(OPD opd)
        {
            if (opd.OPDInitialAssessmentForTelehealthId != null)
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người chăm sóc từ xa",
                    "Initial assessment for telehealth",
                    "InitialAssessmentForTelehealth",
                    opd.OPDInitialAssessmentForTelehealth
                );

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người chăm sóc từ xa",
                "Initial assessment for telehealth",
                "InitialAssessmentForTelehealth"
            );
        }
        private MedicalRecordViewModel GetPatientHandOverRecordOPD(OPD opd)
        {
            if (opd.OPDHandOverCheckListId != null || opd.OPDHandOverCheckList?.IsUseHandOverCheckList == true)
            {
                return new MedicalRecordViewModel(
                    "Biên bản bàn giao người bệnh chuyển khoa",
                    "Hand over check list",
                    "HandOverCheckList",
                    opd.OPDHandOverCheckList
                );
            }
            else
            {
                return new MedicalRecordViewModel(
                    "Biên bản bàn giao người bệnh chuyển khoa",
                    "Hand over check list",
                    "HandOverCheckList"
                );
            }


        }
        private MedicalRecordViewModel GetProcedureSummaryOPD(Guid visit_id)
        {
            var procedure = unitOfWork.EIOProcedureSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == "OPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu phẫu thuật/ thủ thuật",
                    "Surgery and procedure Note",
                    "ProcedureSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu phẫu thuật/ thủ thuật",
                "Surgery and procedure Note",
                "ProcedureSummary"
            );
        }
        private MedicalRecordViewModel GetProcedureSummaryV2OPD(Guid visit_id)
        {
            var procedure = unitOfWork.ProcedureSummaryV2Repository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitType == "OPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu tóm tắt thủ thuật",
                    "Procedure Summary",
                    "TrickSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu tóm tắt thủ thuật",
                "Procedure Summary",
                "TrickSummary"
            );
        }
        private MedicalRecordViewModel GetSkinTestResultOPD(Guid visit_id)
        {
            var skin_test = unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode.Equals("OPD")
            );
            if (skin_test != null)
                return new MedicalRecordViewModel(
                    "Kết quả test da",
                    "Skin test result",
                    "SkinTestResult",
                    skin_test
                );

            return new MedicalRecordViewModel(
                "Kết quả test da",
                "Skin test result",
                "SkinTestResult"
            );
        }
        private MedicalRecordViewModel GetJointConsultationGroupMinutesOPD(Guid visit_id)
        {
            var jcgm = unitOfWork.EIOJointConsultationGroupMinutesRepository.AsQueryable().Where(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "OPD").OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (jcgm != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn",
                    "Joint Consultation Group Minutes",
                    "JointConsultationGroupMinutes",
                    jcgm
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn",
                "Joint Consultation Group Minutes",
                "JointConsultationGroupMinutes"
            );
        }
        private MedicalRecordViewModel GetSurgicalProcedureSafetyChecklistOPD(Guid visit_id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "OPD"
            );

            if (spsc != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn phẫu thuật/thủ thuật",
                    "Surgical Procedure Safety Checklist",
                    "SurgicalProcedureSafetyChecklist",
                    spsc
                );

            return new MedicalRecordViewModel(
                "Bảng kiểm an toàn phẫu thuật/thủ thuật",
                "Surgical Procedure Safety Checklist",
                "SurgicalProcedureSafetyChecklist"
            );
        }
        private MedicalRecordViewModel GetCardiacArrestRecordOPD(Guid visit_id)
        {
            var car = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "OPD"
            );

            if (car != null)
                return new MedicalRecordViewModel(
                    "Bảng hồi sinh tim phổi",
                    "Cardiac Arrest Record",
                    "CardiacArrestRecord",
                    car
                );

            return new MedicalRecordViewModel(
                "Bảng hồi sinh tim phổi",
                "Cardiac Arrest Record",
                "CardiacArrestRecord"
            );
        }
        private MedicalRecordViewModel GetPreOperativeProcedureHandoverChecklistOPD(Guid visit_id)
        {
            var phc = unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitTypeGroupCode == "OPD" &&
                e.VisitId == visit_id
            );
            if (phc != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                    "Pre-operative verification and handover checklist",
                    "PHC",
                    phc
                );
            return new MedicalRecordViewModel(
                "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                    "Pre-operative verification and handover checklist",
                    "PHC"
            );
        }
        private MedicalRecordViewModel GetAssessmentForRetailServicePatientOPD(OPD visit)
        {
            if (visit.EIOAssessmentForRetailServicePatientId != null)
                return new MedicalRecordViewModel(
                    "Đánh giá người bệnh dịch vụ lẻ",
                    "Assessment For Retail Service Patient",
                    "AssessmentForRetailServicePatient",
                    visit.EIOAssessmentForRetailServicePatient
                );

            return new MedicalRecordViewModel(
                "Đánh giá người bệnh dịch vụ lẻ",
                "Assessment For Retail Service Patient",
                "AssessmentForRetailServicePatient"
            );
        }
        private MedicalRecordViewModel GetStandingOrderForRetailServiceOPD(OPD visit)
        {
            if (visit.EIOStandingOrderForRetailServiceId != null)
                return new MedicalRecordViewModel(
                    "Ghi nhận thực hiện thuốc NB sử dụng dịch vụ lẻ",
                    "Standing Order For Retail Service",
                    "StandingOrderForRetailService",
                    visit.EIOStandingOrderForRetailService
                );

            return new MedicalRecordViewModel(
                "Ghi nhận thực hiện thuốc NB sử dụng dịch vụ lẻ",
                "Standing Order For Retail Service",
                "StandingOrderForRetailService"
            );
        }
        private List<MedicalRecordViewModel> GetFormWithStatusPatientOPD(OPD opd)
        {
            dynamic form = null;
            var oen = opd?.OPDOutpatientExaminationNote;
            form = oen;
            var statusPatient = opd.EDStatus.ViName;
            List<MedicalRecordViewModel> forms = unitOfWork.FormOfPatientRepository.Find(x => x.Area.ToUpper() == "OPD" && x.TypeName != "MedicalRecordForOutpatient" && x.ViStatusPatient.ToUpper() == statusPatient.ToUpper()).Select(x => new MedicalRecordViewModel(
                    x.ViName,
                    x.EnName,
                    x.TypeName,
                    oen
            )).ToList();
            // bệnh án ngoại trú
            var confirm_nant = unitOfWork.EIOFormConfirmRepository.Find(e => !e.IsDeleted && e.FormId == oen.Id)
                                                                           .OrderByDescending(e => e.CreatedAt).FirstOrDefault();
            if (confirm_nant != null && oen.UpdatedAt < confirm_nant.UpdatedAt)
            {
                form = confirm_nant;
            }
            MedicalRecordViewModel form_bant = unitOfWork.FormOfPatientRepository.Find(x => x.Area.ToUpper() == "OPD" && x.TypeName == "MedicalRecordForOutpatient" && x.ViStatusPatient.ToUpper() == statusPatient.ToUpper()).Select(x => new MedicalRecordViewModel(
                   x.ViName,
                   x.EnName,
                   x.TypeName,
                   form
           )).FirstOrDefault();
            if (form_bant != null)
            {
                forms.Add(form_bant);
            }

            return forms;
        }
        protected MedicalRecordViewModel GetStillBirthOPD(Guid visitId)
        {
            var eta = unitOfWork.StillBirthRepository
                .AsQueryable().Where(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
                    "Coordinating with the patient/ family to deal with a stillbirth",
                    "CoordinatingWithThePatient",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu",
                    "Coordinating with the patient/ family to deal with a stillbirth",
                    "CoordinatingWithThePatient"
            );
        }
        protected MedicalRecordViewModel GetNCCNBROV1OPD(Guid visitId)
        {
            var eta = unitOfWork.OPDNCCNBROV1Repository.FirstOrDefault(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId);

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú",
                    "NCCN BR/OV-1",
                    "NCCNBROV1",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu đánh giá nguy cơ di truyền trong sàng lọc ung thư vú",
                    "NCCN BR/OV-1",
                    "NCCNBROV1"
            );
        }
        protected MedicalRecordViewModel GetRiskAssessmentForCancerOPD(Guid visitId)
        {
            var eta = unitOfWork.OPDRiskAssessmentForCancerRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId);

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu thông tin khách hàng - Đánh giá nguy cơ ung thư",
                    "Patient information - The risk assessment for cancer",
                    "PatientInformationTheRiskAssessmentForCancer",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu thông tin khách hàng - Đánh giá nguy cơ ung thư",
                    "Patient information- The risk assessment for cancer",
                    "PatientInformationTheRiskAssessmentForCancer"
            );
        }
        protected MedicalRecordViewModel GetOPDGENBRCAROPD(Guid visitId)
        {
            var eta = unitOfWork.OPDGENBRCARepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId);

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA 1/2",
                    "Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA 1/2",
                    "GENBRCA",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA 1/2",
                    "Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA 1/2",
                    "GENBRCA"
            );
        }
        protected MedicalRecordViewModel GetClinicalBreastExamNoteOPD(Guid visitId)
        {
            var eta = unitOfWork.OPDClinicalBreastExamNoteRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId);

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu khám lâm sàng vú",
                    "ClinicalBreastExamNote",
                    "CBE",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu khám lâm sàng vú",
                    "ClinicalBreastExamNote",
                    "CBE"
            );
        }
        protected MedicalRecordViewModel GetPatientAndFamilyEducationFormOPD(Guid ipd_id)
        {

            var gdsk = unitOfWork.PatientAndFamilyEducationRepository.Find(e => !e.IsDeleted &&
                                                                            e.VisitId != null &&
                                                                            !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                                                                            e.VisitId == ipd_id &&
                                                                            e.VisitTypeGroupCode.Equals("OPD")
                                                                           )
                                                                            .ToList()
                                                                            .OrderByDescending(x => x.UpdatedAt)
                                                                            .FirstOrDefault();
            if (gdsk != null)
                return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF",
                    gdsk
                );

            return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF"
             );
        }
        protected MedicalRecordViewModel GetOPDSurgeryAndProcedureSummaryV3(Guid visitId)
        {
            var procedure = unitOfWork.SurgeryAndProcedureSummaryV3Repository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitType == "OPD"
            ).ToList().OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            var procedurev2 = unitOfWork.EIOProcedureSummaryRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "OPD"
           ).ToList().OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null && procedurev2 != null && procedure.UpdatedAt > procedurev2.UpdatedAt)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure != null && procedurev2 != null && procedure.UpdatedAt < procedurev2.UpdatedAt)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }
            if (procedure != null && procedurev2 == null)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure == null && procedurev2 != null)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }

            return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary"
            );
        }
        protected MedicalRecordViewModel GetOPDPROMForheartFailure(Guid visitId)
        {
            var procedure = unitOfWork.PROMForheartFailureRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitType == "OPD"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "PROM bệnh nhân suy tim",
                    "PROM For Heart Failure",
                    "PROMForheartFailure",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "PROM bệnh nhân suy tim",
                    "PROM For Heart Failure",
                    "PROMForheartFailure"
            );
        }
        protected MedicalRecordViewModel GetOPDPROMForCoronaryDisease(Guid visitId)
        {
            var procedure = unitOfWork.PROMForCoronaryDiseaseRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitType == "OPD"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "PROM bệnh nhân mạch vành",
                    "PROM for coronary disease",
                    "PROMForCoronaryDisease",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "PROM bệnh nhân mạch vành",
                    "PROM for coronary disease",
                    "PROMForCoronaryDisease"
            );
        }
        protected MedicalRecordViewModel OPDGetConsentForOperationOrrProcedure(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "OPD" &&
               e.FormCode == "A01_001_080721_V"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure"
            );
        }
        protected MedicalRecordViewModel OPDGetHIVTestingConsent(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "OPD" &&
               e.FormCode == "A01_014_050919_VE"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent"
            );
        }
        private MedicalRecordViewModel GetJointConsultationForApprovalOfSurgeryOPD(Guid visit_id)
        {
            var jcfa = unitOfWork.EIOJointConsultationForApprovalOfSurgeryRepository.AsQueryable()
                        .Where(
                            e => !e.IsDeleted &&
                            e.VisitId == visit_id &&
                            e.VisitTypeGroupCode == "OPD"
                        ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (jcfa != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn thông qua mổ",
                    "Joint-Consultation for approval of surgery",
                    "JointConsultationForApprovalOfSurgery",
                    jcfa
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn thông qua mổ",
                "Joint-Consultation for approval of surgery",
                "JointConsultationForApprovalOfSurgery"
            );
        }
        protected MedicalRecordViewModel OPDCartridgeKaolinACT(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "OPD" &&
                e.FormCode == "A03_041_080322_V"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT"
            );
        }
        protected MedicalRecordViewModel OPDPregnancyFollowUpRecord(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "OPD" &&
                e.FormCode.Contains("A01_067_050919_VE")
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Quản lý thai nghén",
                    "Pregnancy follow up record",
                    "PregnancyFollowUpRecord",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Quản lý thai nghén",
                    "Pregnancy follow up record",
                    "PregnancyFollowUpRecord"
            );
        }
        protected MedicalRecordViewModel OPDUploadImage(Guid visitId)
        {
            var image = unitOfWork.UploadImageRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitType == "OPD"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            var form = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted &&
              e.VisitId == visitId &&
              e.VisitTypeGroupCode == "OPD" &&
              e.FormCode == "UPLOADFILE"
          ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            if (form != null && image != null)
            {
                if (image.UpdatedAt > form.UpdatedAt)
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   image
               );
                }
                else
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   form
               );
                }
            }
            if (form != null && image == null)
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    form
                );
            if (form == null && image != null)
            {
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    image
                );
            }

            return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage"
            );
        }
        protected MedicalRecordViewModel OPDrocedureSafetyChecklist(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "OPD" &&
                 e.FormCode.Contains("A02_053_OR_201022_V_TAB")
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist"
            );
        }
        protected MedicalRecordViewModel OPDForChemotherapyPatient(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "OPD" &&
                e.FormCode.Contains("NBDTHTTN")
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh điều trị hóa trị trong ngày",
                    "Initial assessment For Chemotherapy patient",
                    "ForChemotherapyPatient",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh điều trị hóa trị trong ngày",
                    "Initial assessment For Chemotherapy patient",
                    "ForChemotherapyPatient"
            );
        }

        #endregion
        #region EOC
        public List<MedicalRecordViewModel> GetFormsEOC(EOC eoc)
        {
            var forms = new List<MedicalRecordViewModel>();
            var mandatory_forms = new List<MedicalRecordViewModel> {
                GetInitialAssessmentForShortTermEOC(eoc.Id),
                GetInitialAssessmentForOnGoingEOC(eoc.Id),
                GetFallRiskScreeningEOC(eoc.Id),
                GetOutpatientExaminationNoteEOC(eoc.Id),
                GetComplexOutpatientCaseSummaryEOC(eoc.CustomerId)
            };
            forms.AddRange(mandatory_forms);

            var table_forms = new List<MedicalRecordViewModel> {
                GetStandingOrderEOC(eoc.Id),
                GetProgressNoteEOC(eoc),
                //GetComplexOutpatientCaseSummary(opd.CustomerId),
            };
            forms.AddRange(table_forms);

            var option_forms = new List<MedicalRecordViewModel> {
                // GetPatientHandOverRecord(visit),
                //GetProcedureSummaryEOC(eoc.Id),
                GetProcedureSummaryV2EOC(eoc.Id),
                GetSkinTestResultEOC(eoc.Id),
                GetPreOperativeProcedureHandoverChecklistEOC(eoc.Id),
                GetJointConsultationGroupMinutesEOC(eoc.Id),
                GetSurgicalProcedureSafetyChecklistEOC(eoc.Id),
                GetCardiacArrestRecordEOC(eoc.Id),
               // GetPreAnesthesiaConsultationEOC(eoc.Id),
                GetPatientAndFamilyEducationFormEOC(eoc.Id),
                GetEOCSurgeryAndProcedureSummaryV3(eoc.Id),
                EOCGetConsentForOperationOrrProcedure(eoc.Id),
                EOCGetHIVTestingConsent(eoc.Id),
                EOCCartridgeKaolinACT(eoc.Id),
                EOCUploadImage(eoc.Id),
                EOCrocedureSafetyChecklist(eoc.Id),
                GetFormInEIOForm(eoc.Id, "A02_006_221221_V" ,"Đánh giá nguy cơ vi khuẩn đa kháng (MDRO)", "Multidrug-resistant bacteria risk assessment (MDRO)", "MultidrugResistant")
            };
            forms.AddRange(option_forms.OrderBy(e => e.CreatedAt).ToList());
            var formWithStatusPatient = GetFormWithStatusPatientEOC(eoc);
            if (formWithStatusPatient != null && formWithStatusPatient.Count > 0)
            {
                forms.AddRange(formWithStatusPatient);
            }

            var hocl = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == eoc.Id);
            if (hocl != null && hocl.IsUseHandOverCheckList == false)
            {
                forms.RemoveAll(e => e.Type == "HandOverCheckList");
            }
            return forms;
        }

        private MedicalRecordViewModel GetInitialAssessmentForShortTermEOC(Guid VisitId)
        {
            var iafst = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            if (iafst == null || IsNew(iafst.CreatedAt, iafst.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh ngoại trú thông thường",
                    "Initial assessment for short term",
                    "InitialAssessmentForShortTerm"
                );

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người bệnh ngoại trú thông thường",
                "Initial assessment for short term",
                "InitialAssessmentForShortTerm",
                iafst
            );
        }
        private MedicalRecordViewModel GetInitialAssessmentForOnGoingEOC(Guid VisitId)
        {
            var iafog = unitOfWork.EOCInitialAssessmentForOnGoingRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            if (iafog == null || IsNew(iafog.CreatedAt, iafog.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Đánh giá ban đầu người bệnh ngoại trú dài hạn",
                    "Initial assessment for on-going",
                    "InitialAssessmentForOnGoing"
                );

            return new MedicalRecordViewModel(
                "Đánh giá ban đầu người bệnh ngoại trú dài hạn",
                "Initial assessment for on-going",
                "InitialAssessmentForOnGoing",
                iafog
            );
        }
        private MedicalRecordViewModel GetFallRiskScreeningEOC(Guid VisitId)
        {
            var frs = unitOfWork.EOCFallRiskScreeningRepository.Find(x => !x.IsDeleted && x.VisitId == VisitId).OrderByDescending(x => x.UpdatedAt).FirstOrDefault();
            if (frs != null)
                return new MedicalRecordViewModel(
                 "Phiếu sàng lọc nguy cơ ngã",
                 "Fall risk screening",
                 "FallRiskScreening",
                 frs
                );

            if (frs == null || IsNew(frs.CreatedAt, frs.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Phiếu sàng lọc nguy cơ ngã",
                    "Fall risk screening",
                    "FallRiskScreening"
                );

            return new MedicalRecordViewModel(
                "Phiếu sàng lọc nguy cơ ngã",
                "Fall risk screening",
                "FallRiskScreening",
                frs
            );
        }
        private MedicalRecordViewModel GetOutpatientExaminationNoteEOC(Guid VisitId)
        {
            var oen = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            if (oen == null || IsNew(oen.CreatedAt, oen.UpdatedAt))
                return new MedicalRecordViewModel(
                    "Phiếu khám bệnh",
                    "Patient Examination Note",
                    "OutpatientExaminationNote"
                );

            return new MedicalRecordViewModel(
                "Phiếu khám bệnh",
                "Patient Examination Note",
                "OutpatientExaminationNote",
                oen
            );
        }


        private MedicalRecordViewModel GetStandingOrderEOC(Guid visit_id)
        {
            var standing_order = unitOfWork.OrderRepository.Find(
                i => !i.IsDeleted &&
                i.VisitId != null &&
                i.VisitId == visit_id &&
                !string.IsNullOrEmpty(i.OrderType) &&
                i.OrderType.Equals(Constant.EOC_STANDING_ORDER)
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (standing_order != null)
                return new MedicalRecordViewModel(
                    "Ghi nhận thực hiện thuốc standing order",
                    "Record administration standing order medication",
                    "StandingOrder",
                    new
                    {
                        standing_order.CreatedAt,
                        standing_order.UpdatedAt,
                        standing_order.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Ghi nhận thực hiện thuốc standing order",
                        EnName = "Record administration standing order medication",
                        FormId = visit_id,
                    }}
                );

            return new MedicalRecordViewModel("Ghi nhận thực hiện thuốc standing order", "Record administration standing order medication", "StandingOrder");
        }
        private MedicalRecordViewModel GetProgressNoteEOC(EOC opd)
        {
            var progress_note = unitOfWork.OPDPatientProgressNoteDataRepository.Find(
                e => !e.IsDeleted &&
                e.OPDPatientProgressNoteId != null &&
                e.OPDPatientProgressNoteId == opd.OPDPatientProgressNoteId
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (progress_note != null)
                return new MedicalRecordViewModel(
                    "Theo dõi diến biến người bệnh",
                    "Patient Progress Note",
                    "PatientProgressNote",
                    new
                    {
                        progress_note.CreatedAt,
                        progress_note.UpdatedAt,
                        progress_note.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Theo dõi diến biến người bệnh",
                        EnName = "Patient Progress Note",
                        FormId = opd.Id,
                    }}
                );
            var ob_chart = unitOfWork.OPDObservationChartDataRepository.Find(
                e => !e.IsDeleted &&
                e.ObservationChartId != null &&
                e.ObservationChartId == opd.OPDObservationChartId
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (ob_chart != null)
                return new MedicalRecordViewModel(
                    "Theo dõi diến biến người bệnh",
                    "Add observation",
                    "PatientProgressNote",
                    new
                    {
                        ob_chart.CreatedAt,
                        ob_chart.UpdatedAt,
                        ob_chart.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Theo dõi diến biến người bệnh",
                        EnName = "Add observation",
                        FormId = opd.Id,
                    }}
                );
            return new MedicalRecordViewModel(
                "Theo dõi diến biến người bệnh",
                "Patient Progress Note",
                "PatientProgressNote"
            );
        }
        private MedicalRecordViewModel GetComplexOutpatientCaseSummaryEOC(Guid? customer_id)
        {
            var complex = unitOfWork.ComplexOutpatientCaseSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (complex != null)
                return new MedicalRecordViewModel(
                    "Tóm tắt ca bệnh phức tạp",
                    "Complex Outpatient Case Summary",
                    "ComplexOutpatientCaseSummary",
                    new
                    {
                        complex.CreatedAt,
                        complex.UpdatedAt,
                        complex.UpdatedBy,
                    },
                    new List<MedicalRecordDataViewModel>{ new MedicalRecordDataViewModel{
                        ViName = "Tóm tắt ca bệnh phức tạp",
                        EnName = "Complex Outpatient Case Summary",
                        FormId = customer_id,
                    }}
                );

            return new MedicalRecordViewModel(
                "Tóm tắt ca bệnh phức tạp",
                "Complex Outpatient Case Summary",
                "ComplexOutpatientCaseSummary"
            );
        }
        private MedicalRecordViewModel GetPatientHandOverRecordEOC(EOC visit)
        {
            var tranfer_status = new List<string> { "EOCA0", "EOCTE" };
            var hocl = unitOfWork.EOCHandOverCheckListRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visit.Id);

            if (hocl != null && tranfer_status.Contains(visit.Status.Code))
            {
                return new MedicalRecordViewModel(
                    "Biên bản bàn giao người bệnh chuyển khoa",
                    "Hand over check list",
                    "HandOverCheckList",
                    hocl
                );
            }
            else
            {
                return new MedicalRecordViewModel(
                    "Biên bản bàn giao người bệnh chuyển khoa",
                    "Hand over check list",
                    "HandOverCheckList"
                );
            }

        }
        private MedicalRecordViewModel GetProcedureSummaryEOC(Guid visit_id)
        {
            var procedure = unitOfWork.EIOProcedureSummaryRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == "EOC"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu phẫu thuật/ thủ thuật",
                    "Surgery and procedure Note",
                    "ProcedureSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu phẫu thuật/ thủ thuật",
                "Surgery and procedure Note",
                "ProcedureSummary"
            );
        }
        private MedicalRecordViewModel GetProcedureSummaryV2EOC(Guid visit_id)
        {
            var procedure = unitOfWork.ProcedureSummaryV2Repository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu tóm tắt thủ thuật",
                    "Procedure Summary",
                    "TrickSummary",
                    procedure
                );

            return new MedicalRecordViewModel(
                "Phiếu tóm tắt thủ thuật",
                "Procedure Summary",
                "TrickSummary"
            );
        }
        private MedicalRecordViewModel GetSkinTestResultEOC(Guid visit_id)
        {
            var skin_test = unitOfWork.EIOSkinTestResultRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode.Equals("EOC")
            );
            if (skin_test != null)
                return new MedicalRecordViewModel(
                    "Kết quả test da",
                    "Skin test result",
                    "SkinTestResult",
                    skin_test
                );

            return new MedicalRecordViewModel(
                "Kết quả test da",
                "Skin test result",
                "SkinTestResult"
            );
        }
        private MedicalRecordViewModel GetJointConsultationGroupMinutesEOC(Guid visit_id)
        {
            var jcgm = unitOfWork.EIOJointConsultationGroupMinutesRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId != null &&
               e.VisitId == visit_id &&
               !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
               e.VisitTypeGroupCode == "EOC").OrderByDescending(x => x.UpdatedAt).FirstOrDefault();

            if (jcgm != null)
                return new MedicalRecordViewModel(
                    "Biên bản hội chẩn",
                    "Joint Consultation Group Minutes",
                    "JointConsultationGroupMinutes",
                    jcgm
                );

            return new MedicalRecordViewModel(
                "Biên bản hội chẩn",
                "Joint Consultation Group Minutes",
                "JointConsultationGroupMinutes"
            );
        }
        private MedicalRecordViewModel GetSurgicalProcedureSafetyChecklistEOC(Guid visit_id)
        {
            var spsc = unitOfWork.EIOSurgicalProcedureSafetyChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "EOC"
            );

            if (spsc != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn phẫu thuật/thủ thuật",
                    "Surgical Procedure Safety Checklist",
                    "SurgicalProcedureSafetyChecklist",
                    spsc
                );

            return new MedicalRecordViewModel(
                "Bảng kiểm an toàn phẫu thuật/thủ thuật",
                "Surgical Procedure Safety Checklist",
                "SurgicalProcedureSafetyChecklist"
            );
        }
        private MedicalRecordViewModel GetCardiacArrestRecordEOC(Guid visit_id)
        {
            var car = unitOfWork.EIOCardiacArrestRecordRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == "EOC"
            );

            if (car != null)
                return new MedicalRecordViewModel(
                    "Bảng hồi sinh tim phổi",
                    "Cardiac Arrest Record",
                    "CardiacArrestRecord",
                    car
                );

            return new MedicalRecordViewModel(
                "Bảng hồi sinh tim phổi",
                "Cardiac Arrest Record",
                "CardiacArrestRecord"
            );
        }
        private MedicalRecordViewModel GetPreOperativeProcedureHandoverChecklistEOC(Guid visit_id)
        {
            var phc = unitOfWork.EIOPreOperativeProcedureHandoverChecklistRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                e.VisitTypeGroupCode == "EOC" &&
                e.VisitId == visit_id
            );
            if (phc != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                    "Pre-operative verification and handover checklist",
                    "PHC",
                    phc
                );
            return new MedicalRecordViewModel(
               "Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật",
                    "Pre-operative verification and handover checklist",
                    "PHC"
            );
        }
        private List<MedicalRecordViewModel> GetFormWithStatusPatientEOC(EOC eoc)
        {
            var statusPatient = eoc?.Status?.ViName;
            List<MedicalRecordViewModel> forms = new List<MedicalRecordViewModel>();
            if (!string.IsNullOrEmpty(statusPatient))
            {
                var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(x => x.VisitId == eoc.Id && !x.IsDeleted);
                if (form != null)
                {
                    forms = unitOfWork.FormOfPatientRepository.Find(x => x.Area.ToUpper() == "EOC" && x.ViStatusPatient.ToUpper() == statusPatient.ToUpper()).Select(x => new MedicalRecordViewModel(
                      x.ViName,
                      x.EnName,
                      x.TypeName,
                      form
                    )).ToList();
                }
            }
            return forms;
        }
        protected MedicalRecordViewModel GetPreAnesthesiaConsultationEOC(Guid visitId)
        {
            var eta = unitOfWork.PreAnesthesiaConsultationRepository
                .AsQueryable().Where(x => !x.IsDeleted && x.VisitId != null && x.VisitId == visitId)
                .OrderByDescending(x => x.UpdatedAt).ToList().FirstOrDefault();

            if (eta != null)
                return new MedicalRecordViewModel(
                    "Phiếu khám gây mê",
                    "Pre Anesthesia Consultation",
                    "PreAnesthesiaConsultation",
                    eta
                );

            return new MedicalRecordViewModel(
                    "Phiếu khám gây mê",
                    "Pre Anesthesia Consultation",
                    "PreAnesthesiaConsultation"
            );
        }
        protected MedicalRecordViewModel GetPatientAndFamilyEducationFormEOC(Guid ipd_id)
        {

            var gdsk = unitOfWork.PatientAndFamilyEducationRepository.Find(e => !e.IsDeleted &&
                                                                            e.VisitId != null &&
                                                                            !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                                                                            e.VisitId == ipd_id &&
                                                                            e.VisitTypeGroupCode.Equals("EOC")
                                                                           )
                                                                            .ToList()
                                                                            .OrderByDescending(x => x.UpdatedAt)
                                                                            .FirstOrDefault();
            if (gdsk != null)
                return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF",
                    gdsk
                );

            return new MedicalRecordViewModel(
                    "Phiếu GDSK cho NB và thân nhân",
                    "Patient and family education form",
                    "PFEF"
             );
        }
        protected MedicalRecordViewModel GetEOCSurgeryAndProcedureSummaryV3(Guid visitId)
        {
            var procedure = unitOfWork.SurgeryAndProcedureSummaryV3Repository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitType == "EOC"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            var procedurev2 = unitOfWork.EIOProcedureSummaryRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "EOC"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null && procedurev2 != null && procedure.UpdatedAt > procedurev2.UpdatedAt)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure != null && procedurev2 != null && procedure.UpdatedAt < procedurev2.UpdatedAt)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }
            if (procedure != null && procedurev2 == null)
                return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary",
                     procedure
                );
            if (procedure == null && procedurev2 != null)
            {
                return new MedicalRecordViewModel(
                   "Tóm tắt phẫu thuật",
                   "Surgery summary",
                   "SurgeryAndProcedureSummary",
                   procedurev2);
            }

            return new MedicalRecordViewModel(
                    "Tóm tắt phẫu thuật",
                    "Surgery summary",
                    "SurgeryAndProcedureSummary"
            );
        }
        protected MedicalRecordViewModel EOCGetConsentForOperationOrrProcedure(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "EOC" &&
               e.FormCode == "A01_001_080721_V"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu cam kết phẫu thuật/ thủ thuật/ điều trị có nguy cơ cao",
                    "Consent for Operation/ Procedure/ High risk treatment",
                    "ConsentForOperationOrrProcedure"
            );
        }

        protected MedicalRecordViewModel EOCGetHIVTestingConsent(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "EOC" &&
               e.FormCode == "A01_014_050919_VE"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Phiếu đồng ý xét nghiệm HIV",
                    "HIV Testing Consent Form",
                    "HIVTestingConsent"
            );
        }
        protected MedicalRecordViewModel EOCCartridgeKaolinACT(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "EOC" &&
                e.FormCode == "A03_041_080322_V"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Xét nghiệm tại chỗ - Đông máu ACT (Cartridge Kaolin ACT)",
                    "Point of case testing - Coagulation ACT (Catridge Kaolin ACT)",
                    "CatridgeKaolinACT"
            );
        }
        protected MedicalRecordViewModel EOCUploadImage(Guid visitId)
        {
            var image = unitOfWork.UploadImageRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitType == "EOC"
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();
            var form = unitOfWork.EIOFormRepository.Find(e => !e.IsDeleted &&
               e.VisitId == visitId &&
               e.VisitTypeGroupCode == "EOC" &&
               e.FormCode == "UPLOADFILE"
           ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (form != null && image != null)
            {
                if (image.UpdatedAt > form.UpdatedAt)
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   image
               );
                }
                else
                {
                    return new MedicalRecordViewModel(
                   "Upload File",
                   "Upload File",
                   "UploadImage",
                   form
               );
                }
            }
            if (form != null && image == null)
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    form
                );
            if (form == null && image != null)
            {
                return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage",
                    image
                );
            }
            return new MedicalRecordViewModel(
                    "Upload File",
                    "Upload File",
                    "UploadImage"
            );
        }
        protected MedicalRecordViewModel EOCrocedureSafetyChecklist(Guid visitId)
        {
            var procedure = unitOfWork.EIOFormRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visitId &&
                e.VisitTypeGroupCode == "EOC" &&
                e.FormCode.Contains("A02_053_OR_201022_V_TAB")
            ).OrderByDescending(e => e.UpdatedAt).FirstOrDefault();

            if (procedure != null)
                return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist",
                    procedure
                );

            return new MedicalRecordViewModel(
                    "Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch",
                    "Surgical/ Procedure safety checklist (Operating room and Cathlab)",
                    "ProcedureSafetyChecklist"
            );
        }
        #endregion

        #region Form dùng EIOForm
        private MedicalRecordViewModel GetFormInEIOForm(Guid visitId, string formCode, string viName, string enName, string type)
        {
            var form = (from e in unitOfWork.EIOFormRepository.AsQueryable()
                        where !e.IsDeleted && e.VisitId == visitId
                        && e.FormCode == formCode
                        orderby e.UpdatedAt descending
                        select e).FirstOrDefault();
            if (form != null)
                return new MedicalRecordViewModel(viName, enName, type, form);

            return new MedicalRecordViewModel(viName, enName, type);
        }
        #endregion
    }
}
