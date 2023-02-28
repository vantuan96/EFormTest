using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.FormModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.MedicationAdministrationRecordModel;
using DataAccess.Models.OPDModel;
using DataAccess.Models.PrescriptionModel;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DataAccess.Repository
{
    public class EfUnitOfWork : DbContext, IUnitOfWork
    {
        private EmergencyDepartmentContext context = new EmergencyDepartmentContext();

        #region General EfGenericRepository
        private readonly EfGenericRepository<Models.Action> _actionRepo;
        private readonly EfGenericRepository<AdminRole> _adminRoleRepo;
        private readonly EfGenericRepository<Clinic> _clinicRepo;
        private readonly EfGenericRepository<Customer> _customerRepo;
        private readonly EfGenericRepository<EDStatus> _edStatusRepo;
        private readonly EfGenericRepository<Form> _formRepo;
        private readonly EfGenericRepository<HumanResourceAssessment> _humanResourceAssessmentRepo;
        private readonly EfGenericRepository<HumanResourceAssessmentPosition> _humanResourceAssessmentPositionRepo;
        private readonly EfGenericRepository<HumanResourceAssessmentShift> _humanResourceAssessmentShiftRepo;
        private readonly EfGenericRepository<HumanResourceAssessmentStaff> _humanResourceAssessmentStaffRepo;
        private readonly EfGenericRepository<ICD10> _ICD10Repo;
        private readonly EfGenericRepository<ICDSpecialty> _ICDSpecialtyRepo;
        private readonly EfGenericRepository<Log> _logRepo;
        private readonly EfGenericRepository<LogTmp> _logTmpRepo;
        private readonly EfGenericRepository<LogInFail> _logInFailRepo;
        private readonly EfGenericRepository<MasterData> _masterDataRepo;
        private readonly EfGenericRepository<MedicationMasterdata> _medicationMasterdataRepo;
        private readonly EfGenericRepository<Notification> _notificationRepo;
        private readonly EfGenericRepository<Order> _orderRepo;
        private readonly EfGenericRepository<Position> _positionRepo;
        private readonly EfGenericRepository<PositionUser> _positionUserRepo;
        private readonly EfGenericRepository<Role> _roleRepo;
        private readonly EfGenericRepository<RoleAction> _roleActionRepo;
        private readonly EfGenericRepository<RoleSpecialty> _roleSpecialtyRepo;
        private readonly EfGenericRepository<Room> _roomRepo;
        private readonly EfGenericRepository<Site> _siteRepo;
        private readonly EfGenericRepository<Specialty> _specialtyRepo;
        private readonly EfGenericRepository<StandingOrderMasterData> _standingOrderMasterDataRepo;
        private readonly EfGenericRepository<SystemConfig> _systemConfigRepo;
        private readonly EfGenericRepository<SystemNotification> _systemNotificationRepo;
        private readonly EfGenericRepository<UnlockFormToUpdate> _unlockFormToUpdateRepo;
        private readonly EfGenericRepository<User> _userRepo;
        private readonly EfGenericRepository<UserAdminRole> _userAdminRoleRepo;
        private readonly EfGenericRepository<UserClinic> _userClinicRepo;
        private readonly EfGenericRepository<UserRole> _userRoleRepo;
        private readonly EfGenericRepository<UserSpecialty> _userSpecialtyRepo;
        private readonly EfGenericRepository<VisitTypeGroup> _visitTypeGroupRepo;
        private readonly EfGenericRepository<Service> _serviceRepo;
        private readonly EfGenericRepository<ServiceGroup> _serviceGroupRepo;
        private readonly EfGenericRepository<CpoeOrderable> _cpoeOrderableRepo;
        private readonly EfGenericRepository<LabOrderableRef> _labOrderableRefRepo;
        private readonly EfGenericRepository<ChargeItem> _chargeItemRepo;
        private readonly EfGenericRepository<ChargeVisit> _chargeVisitRepo;
        private readonly EfGenericRepository<Charge> _chargeRepo;
        private readonly EfGenericRepository<ChargeDraft> _chargeDraftRepo;
        private readonly EfGenericRepository<ChargeItemPathology> _chargeItemPathologyRepo;
        private readonly EfGenericRepository<ChargeItemMicrobiology> _chargeItemMicrobiologyRepo;
        private readonly EfGenericRepository<ChargePackage> _chargePackageRepo;
        private readonly EfGenericRepository<ChargePackageService> _chargePackageServiceRepo;
        private readonly EfGenericRepository<ChargePackageUser> _chargePackageUserRepo;
        private readonly EfGenericRepository<RadiologyProcedurePlanRef> _radiologyProcedurePlanRefRepo;
        private readonly EfGenericRepository<TableData> _tableDataRepo;
        private readonly EfGenericRepository<DietCode> _dietCodeRepo;
        private readonly EfGenericRepository<MedicationAdministrationRecordModel> _medicationAdministrationRecordRepo;
        private readonly EfGenericRepository<ICD10Visit> _ICD10VisitRepo;
        private readonly EfGenericRepository<StillBirth> _StillBirthRepo;
        private readonly EfGenericRepository<SendMailNotification> _SendMailNotificationRepo;
        private readonly EfGenericRepository<PROMForCoronaryDisease> _PROMForCoronaryDiseaseRepo;
        private readonly EfGenericRepository<PROMForheartFailure> _PROMForheartFailureRepo;
        private readonly EfGenericRepository<UploadImage> _UploadImageRepo;
        private readonly EfGenericRepository<AppVersion> _AppVersionRepo;
        #endregion

        #region ED EfGenericRepository
        private readonly EfGenericRepository<ED> _edRepo;
        private readonly EfGenericRepository<EDVerbalOrder> _eDEDVerbalOrderRepo;
        private readonly EfGenericRepository<EDAmbulanceRunReport> _eDAmbulanceRunReportRepo;
        private readonly EfGenericRepository<EDAmbulanceRunReportData> _eDAmbulanceRunReportDataRepo;
        private readonly EfGenericRepository<EDAmbulanceTransferPatientsRecord> _eDAmbulanceTransferPatientsRecordRepo;
        private readonly EfGenericRepository<EDArterialBloodGasTest> _eDArterialBloodGasTestRepo;
        private readonly EfGenericRepository<EDArterialBloodGasTestData> _eDArterialBloodGasTestDataRepo;
        private readonly EfGenericRepository<EDChemicalBiologyTest> _eDChemicalBiologyTestRepo;
        private readonly EfGenericRepository<EDChemicalBiologyTestData> _eDChemicalBiologyTestDataRepo;
        private readonly EfGenericRepository<EDConsultationDrugWithAnAsteriskMark> _eDConsultationDrugWithAnAsteriskMarkRepo;
        private readonly EfGenericRepository<EDDischargeInformation> _dischargeInformationRepo;
        private readonly EfGenericRepository<EDDischargeInformationData> _dischargeInformationDataRepo;
        private readonly EfGenericRepository<EDEmergencyRecord> _emergencyRecordRepo;
        private readonly EfGenericRepository<EDEmergencyRecordData> _emergencyRecordDataRepo;
        private readonly EfGenericRepository<EDEmergencyTriageRecord> _emergencyTriageRecordRepo;
        private readonly EfGenericRepository<EDEmergencyTriageRecordData> _emergencyTriageRecordDataRepo;
        private readonly EfGenericRepository<EDHandOverCheckList> _handOverCheckListRepo;
        private readonly EfGenericRepository<EDHandOverCheckListData> _handOverCheckListDataRepo;
        private readonly EfGenericRepository<EDInjuryCertificate> _eDInjuryCertificateRepo;
        private readonly EfGenericRepository<EDMonitoringChartAndHandoverForm> _monitoringChartAndHandoverFormRepo;
        private readonly EfGenericRepository<EDMonitoringChartAndHandoverFormData> _monitoringChartAndHandoverFormDataRepo;
        private readonly EfGenericRepository<EDObservationChart> _eDObservationChartRepo;
        private readonly EfGenericRepository<EDObservationChartData> _eDObservationChartDataRepo;
        private readonly EfGenericRepository<EDPatientProgressNote> _patientProgressNoteRepo;
        private readonly EfGenericRepository<EDPatientProgressNoteData> _patientProgressNoteDataRepo;

        private readonly EfGenericRepository<EDPointOfCareTestingMasterData> _eDPointOfCareTestingMasterDataRepo;
        private readonly EfGenericRepository<EDSelfHarmRiskScreeningTool> _eDSelfHarmRiskScreeningToolRepo;
        private readonly EfGenericRepository<EDFallRickScreennings> _eDFallRickScreenningRepo;
        #endregion

        #region OPD EfGenericRepository
        private readonly EfGenericRepository<OPD> _oPDRepo;
        private readonly EfGenericRepository<OPDFallRiskScreening> _oPDFallRiskScreeningRepo;
        private readonly EfGenericRepository<OPDFallRiskScreeningData> _oPDFallRiskScreeningDataRepo;
        private readonly EfGenericRepository<OPDHandOverCheckList> _oPDHandOverCheckListRepo;
        private readonly EfGenericRepository<OPDHandOverCheckListData> _oPDHandOverCheckListDataRepo;
        private readonly EfGenericRepository<OPDInitialAssessmentForOnGoing> _oPDInitialAssessmentForOnGoingRepo;
        private readonly EfGenericRepository<OPDInitialAssessmentForOnGoingData> _oPDInitialAssessmentForOnGoingDataRepo;
        private readonly EfGenericRepository<OPDInitialAssessmentForShortTerm> _oPDInitialAssessmentForShortTermRepo;
        private readonly EfGenericRepository<OPDInitialAssessmentForShortTermData> _oPDInitialAssessmentForShortTermDataRepo;
        private readonly EfGenericRepository<OPDInitialAssessmentForTelehealth> _oPDInitialAssessmentForTelehealthRepo;
        private readonly EfGenericRepository<OPDInitialAssessmentForTelehealthData> _oPDInitialAssessmentForTelehealthDataRepo;
        private readonly EfGenericRepository<OPDObservationChart> _oPDObservationChartRepo;
        private readonly EfGenericRepository<OPDObservationChartData> _oPDObservationChartDataRepo;
        private readonly EfGenericRepository<OPDOutpatientExaminationNote> _oPDOutpatientExaminationNoteRepo;
        private readonly EfGenericRepository<OPDOutpatientExaminationNoteData> _oPDOutpatientExaminationNoteDataRepo;
        private readonly EfGenericRepository<OPDPatientProgressNote> _oPDPatientProgressNoteRepo;
        private readonly EfGenericRepository<OPDPatientProgressNoteData> _oPDPatientProgressNoteDataRepo;
        private readonly EfGenericRepository<OPDNCCNBROV1> _OPDNCCNBROV1Repo;
        private readonly EfGenericRepository<OPDRiskAssessmentForCancer> _OPDRiskAssessmentForCancerRepo;
        private readonly EfGenericRepository<OPDClinicalBreastExamNote> _OPDClinicalBreastExamNoteRepo;
        private readonly EfGenericRepository<OPDGENBRCA> _OPDGENBRCARepo;
        private readonly EfGenericRepository<OPDPreAnesthesiaHandOverCheckList> _OPDPreAnesthesiaHandOverCheckListRepo;

        #endregion

        #region IPD EfGenericRepository
        private readonly EfGenericRepository<IPD> _iPDRepo;
        private readonly EfGenericRepository<IPDDischargeMedicalReport> _iPDDischargeMedicalReportRepo;
        private readonly EfGenericRepository<IPDFallRiskAssessmentForAdult> _iPDFallRiskAssessmentForAdultRepo;
        private readonly EfGenericRepository<IPDFallRiskAssessmentForAdultData> _iPDFallRiskAssessmentForAdultDataRepo;
        private readonly EfGenericRepository<IPDFallRiskAssessmentForObstetric> _iPDFallRiskAssessmentForObstetricRepo;
        private readonly EfGenericRepository<IPDFallRiskAssessmentForObstetricData> _iPDFallRiskAssessmentForObstetricDataRepo;
        private readonly EfGenericRepository<IPDGuggingSwallowingScreen> _iPDGuggingSwallowingScreenRepo;
        private readonly EfGenericRepository<IPDGuggingSwallowingScreenData> _iPDGuggingSwallowingScreenDataRepo;
        private readonly EfGenericRepository<IPDHandOverCheckList> _iPDHandOverCheckListRepo;
        private readonly EfGenericRepository<IPDHandOverCheckListData> _iPDHandOverCheckListDataRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentForAdult> _iPDInitialAssessmentForAdultRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentForAdultData> _iPDInitialAssessmentForAdultDataRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentForChemotherapy> _iPDInitialAssessmentForChemotherapyRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentForChemotherapyData> _iPDInitialAssessmentForChemotherapyDataRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentForFrailElderly> _iPDInitialAssessmentForFrailElderlyRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentForFrailElderlyData> _iPDInitialAssessmentForFrailElderlyDataRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentSpecialRequest> _iPDInitialAssessmentSpecialRequestRepo;
        private readonly EfGenericRepository<IPDConsultationDrugWithAnAsteriskMark> _iPDConsultationDrugWithAnAsteriskMarkRepo;
        private readonly EfGenericRepository<IPDMedicalRecord> _iPDMedicalRecordRepo;
        private readonly EfGenericRepository<IPDMedicalRecordData> _iPDMedicalRecordDataRepo;
        private readonly EfGenericRepository<IPDMedicalRecordPart1> _iPDMedicalRecordPart1Repo;
        private readonly EfGenericRepository<IPDMedicalRecordPart1Data> _iPDMedicalRecordPart1DataRepo;
        private readonly EfGenericRepository<IPDMedicalRecordPart2> _iPDMedicalRecordPart2Repo;
        private readonly EfGenericRepository<IPDMedicalRecordPart2Data> _iPDMedicalRecordPart2DataRepo;
        private readonly EfGenericRepository<IPDMedicalRecordPart3> _iPDMedicalRecordPart3Repo;
        private readonly EfGenericRepository<IPDMedicalRecordPart3Data> _iPDMedicalRecordPart3DataRepo;
        private readonly EfGenericRepository<IPDPatientProgressNote> _iPDPatientProgressNoteRepo;
        private readonly EfGenericRepository<IPDPatientProgressNoteData> _iPDPatientProgressNoteDataRepo;
        private readonly EfGenericRepository<IPDPlanOfCare> _iPDPlanOfCareRepo;
        private readonly EfGenericRepository<IPDReferralLetter> _iPDReferralLetterRepo;
        private readonly EfGenericRepository<IPDTransferLetter> _iPDTransferLetterRepo;
        private readonly EfGenericRepository<IPDTakeCareOfPatientsWithCovid19Recomment> _IPDTakeCareOfPatientsWithCovid19RecommentRepo;
        private readonly EfGenericRepository<IPDTakeCareOfPatientsWithCovid19Assessment> _IPDTakeCareOfPatientsWithCovid19AssessmentRepo;
        private readonly EfGenericRepository<IPDDischargePreparationChecklist> _IPDDischargePreparationChecklistRepo;
        private readonly EfGenericRepository<IPDDischargePreparationChecklistData> _IPDDischargePreparationChecklistDataRepo;
        private readonly EfGenericRepository<IPDAdultVitalSignMonitor> _IPDAdultVitalSignMonitorRepo;
        private readonly EfGenericRepository<IPDAdultVitalSignMonitorData> _IPDAdultVitalSignMonitorDataRepo;
        private readonly EfGenericRepository<IPDConfirmDischarge> _IPDConfirmDischargeWithoutDirectRepo;
        private readonly EfGenericRepository<VitalSignForAdult> _IPDVitalSignForAdultRepo;
        private readonly EfGenericRepository<IPDInjuryCertificate> _IPDInjuryCertificateRepo;
        private readonly EfGenericRepository<BradenScale> _IPDBradenScaleRepo;
        private readonly EfGenericRepository<IPDSurgeryCertificate> _IPDSurgeryCertificateRepo;
        private readonly EfGenericRepository<IPDSurgeryCertificateData> _IPDSurgeryCertificateDataRepo;
        private readonly EfGenericRepository<IPDSummaryOf15DayTreatment> _IPDSummayOf15DayTreatmentRepo;
        private readonly EfGenericRepository<IPDMedicationHistory> _IPDMedicationHistoryRepo;
        private readonly EfGenericRepository<IPDSetupMedicalRecord> _IPDSetupMedicalRecordRepo;
        private readonly EfGenericRepository<IPDMedicalRecordOfPatients> _IPDMedicalRecordOfPatientRepo;
        private readonly EfGenericRepository<IPDInitialAssessmentForNewborns> _IPDInitialAssessmentForNewbornsRepo;
        private readonly EfGenericRepository<IPDThrombosisRiskFactorAssessment> _IPDThrombosisRiskFactorAssessmentRepo;
        private readonly EfGenericRepository<IPDThrombosisRiskFactorAssessmentData> _IPDThrombosisRiskFactorAssessmentDataRepo;
        private readonly EfGenericRepository<IPDSibling> _IPDSiblingRepo;
        private readonly EfGenericRepository<IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs> _IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepo;
        private readonly EfGenericRepository<VitalSignForPregnantWoman> _IPDVitalSignForPregnantWomanRepo;
        private readonly EfGenericRepository<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient> _IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepo;
        private readonly EfGenericRepository<IPDCoronaryIntervention> _IPDCoronaryInterventionRepo;
        private readonly EfGenericRepository<IPDPainRecord> _IPDPainRecordRepo;
        private readonly EfGenericRepository<IPDGlamorgan> _IPDGlamorganRepo;
        private readonly EfGenericRepository<IPDGlamorganData> _IPDGlamorganDataRepo;
        private readonly EfGenericRepository<IPDNeonatalObservationChart> _IPDNeonatalObservationChartRepo;
        private readonly EfGenericRepository<IPDVitalSignForPediatrics> _IPDVitalSignForPediatricsRepo;
        private readonly EfGenericRepository<IPDMedicalRecordExtenstion> _IPDMedicalRecordExtenstionRepo;
        private readonly EfGenericRepository<IPDScaleForAssessmentOfSuicideIntent> _IPDScaleForAssessmentOfSuicideIntentRepo;
        #endregion

        #region PrescriptionNote
        private readonly EfGenericRepository<PrescriptionNoteModel> _PrescriptionNoteRepo;
        private readonly EfGenericRepository<PresciptionRoundInfoModel> _PrescriptionRoundInfoRepo;
        #endregion

        #region EIO EfGenericRepository

        private readonly EfGenericRepository<HighlyRestrictedAntimicrobialConsult> _highlyRestrictedAntimicrobialConsultRepo;
        private readonly EfGenericRepository<HighlyRestrictedAntimicrobialConsultAntimicrobialOrder> _highlyRestrictedAntimicrobialConsultAntimicrobialOrderRepo;
        private readonly EfGenericRepository<HighlyRestrictedAntimicrobialConsultMicrobiologicalResults> _highlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepo;

        private readonly EfGenericRepository<EIOTestCovid2Confirmation> _eIOTestCovid2ConfirmationRepo;
        private readonly EfGenericRepository<ComplexOutpatientCaseSummary> _complexOutpatientCaseSummaryRepo;
        private readonly EfGenericRepository<EIOAssessmentForRetailServicePatient> _eDAssessmentForRetailServicePatientRepo;
        private readonly EfGenericRepository<EIOAssessmentForRetailServicePatientData> _eDAssessmentForRetailServicePatientDataRepo;
        private readonly EfGenericRepository<EIOBloodProductData> _eIOBloodProductDataRepo;
        private readonly EfGenericRepository<EIOBloodRequestSupplyAndConfirmation> _eIOBloodRequestSupplyAndConfirmationRepo;
        private readonly EfGenericRepository<EIOBloodSupplyData> _eIOBloodSupplyDataRepo;
        private readonly EfGenericRepository<EIOBloodTransfusionChecklist> _eIOBloodTransfusionChecklistRepo;
        private readonly EfGenericRepository<EIOBloodTransfusionChecklistData> _eIOBloodTransfusionChecklistDataRepo;
        private readonly EfGenericRepository<EIOCardiacArrestRecord> _eIOCardiacArrestRecordRepo;
        private readonly EfGenericRepository<EIOCardiacArrestRecordData> _eIOCardiacArrestRecordDataRepo;
        private readonly EfGenericRepository<EIOCardiacArrestRecordTable> _eIOCardiacArrestRecordTableRepo;
        private readonly EfGenericRepository<EIOExternalTransportationAssessment> _eIOExternalTransportationAssessmentRepo;
        private readonly EfGenericRepository<EIOExternalTransportationAssessmentData> _eIOExternalTransportationAssessmentDataRepo;
        private readonly EfGenericRepository<EIOExternalTransportationAssessmentEquipment> _eIOExternalTransportationAssessmentEquipmentRepo;
        private readonly EfGenericRepository<EIOJointConsultationForApprovalOfSurgery> _eIOJointConsultationForApprovalOfSurgeryRepo;
        private readonly EfGenericRepository<EIOJointConsultationForApprovalOfSurgeryData> _eIOJointConsultationForApprovalOfSurgeryDataRepo;
        private readonly EfGenericRepository<EIOJointConsultationGroupMinutes> _eIOJointConsultationGroupMinutesRepo;
        private readonly EfGenericRepository<EIOJointConsultationGroupMinutesData> _eIOJointConsultationGroupMinutesDataRepo;
        private readonly EfGenericRepository<EIOJointConsultationGroupMinutesMember> _eIOJointConsultationGroupMinutesMemberRepo;
        private readonly EfGenericRepository<EIOMortalityReport> _eIOMortalityReportRepo;
        private readonly EfGenericRepository<EIOMortalityReportMember> _eIOMortalityReportMemberRepo;
        private readonly EfGenericRepository<EIOPatientOwnMedicationsChart> _eIOPatientOwnMedicationsChartRepo;
        private readonly EfGenericRepository<EIOPatientOwnMedicationsChartData> _eIOPatientOwnMedicationsChartDataRepo;
        private readonly EfGenericRepository<EIOPreOperativeProcedureHandoverChecklist> _preOperativeProcedureHandoverChecklistRepo;
        private readonly EfGenericRepository<EIOPreOperativeProcedureHandoverChecklistData> _preOperativeProcedureHandoverChecklistDataRepo;
        private readonly EfGenericRepository<EIOProcedureSummary> _eIOProcedureSummaryRepo;
        private readonly EfGenericRepository<ProcedureSummaryV2> _ProcedureSummaryV2Repo;
        private readonly EfGenericRepository<EIOProcedureSummaryData> _eIOProcedureSummaryDataRepo;
        private readonly EfGenericRepository<EIOSkinTestResult> _eIOSkinTestResultRepo;
        private readonly EfGenericRepository<EIOSkinTestResultData> _eIOSkinTestResultDataRepo;
        private readonly EfGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheet> _spongeSharpsAndInstrumentsCountsSheetRepo;
        private readonly EfGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheetData> _spongeSharpsAndInstrumentsCountsSheetDataRepo;
        private readonly EfGenericRepository<EIOStandingOrderForRetailService> _eDStandingOrderForRetailServiceRepo;
        private readonly EfGenericRepository<EIOSurgicalProcedureSafetyChecklist> _eIOSurgicalProcedureSafetyChecklistRepo;
        private readonly EfGenericRepository<EIOSurgicalProcedureSafetyChecklistData> _eIOSurgicalProcedureSafetyChecklistDataRepo;
        private readonly EfGenericRepository<EIOSurgicalProcedureSafetyChecklistSignIn> _eIOSurgicalProcedureSafetyChecklistSignInRepo;
        private readonly EfGenericRepository<EIOSurgicalProcedureSafetyChecklistSignOut> _eIOSurgicalProcedureSafetyChecklistSignOutRepo;
        private readonly EfGenericRepository<EIOSurgicalProcedureSafetyChecklistTimeOut> _eIOSurgicalProcedureSafetyChecklistTimeOutRepo;
        private readonly EfGenericRepository<PatientAndFamilyEducation> _patientAndFamilyEducationRepo;
        private readonly EfGenericRepository<PatientAndFamilyEducationContent> _patientAndFamilyEducationContentRepo;
        private readonly EfGenericRepository<PatientAndFamilyEducationContentData> _patientAndFamilyEducationContentDataRepo;
        private readonly EfGenericRepository<PatientAndFamilyEducationData> _patientAndFamilyEducationDataRepo;
        private readonly EfGenericRepository<Translation> _translationRepo;
        private readonly EfGenericRepository<TranslationData> _translationDataRepo;
        private readonly EfGenericRepository<EIOPhysicianNote> _eIOPhysicianNoteRepo;
        private readonly EfGenericRepository<EIOCareNote> _eIOCareNoteRepo;
        private readonly EfGenericRepository<PreAnesthesiaConsultation> _PreAnesthesiaConsultationRepo;
        private readonly EfGenericRepository<SurgeryAndProcedureSummaryV3> _SurgeryAndProcedureSummaryV3Repo;
        private readonly EfGenericRepository<EIOConstraintNewbornAndPregnantWoman> _EIOConstraintNewbornAndPregnantWomanRepo;
        #endregion
        #region ECO EfGenericRepository
        private readonly EfGenericRepository<EOC> _ecoRepo;
        private readonly EfGenericRepository<EOCTransfer> _ecoCTransferRepo;
        private readonly EfGenericRepository<EOCFallRiskScreening> _ecoFallRiskScreeningRepo;
        private readonly EfGenericRepository<EOCInitialAssessmentForOnGoing> _ecoInitialAssessmentForOnGoinRepo;
        private readonly EfGenericRepository<EOCInitialAssessmentForShortTerm> _ecoInitialAssessmentForShortTermRepo;
        private readonly EfGenericRepository<EOCOutpatientExaminationNote> _ecoEOCOutpatientExaminationNoteRepo;
        private readonly EfGenericRepository<EOCHandOverCheckList> _ecoEOCHandOverCheckListRepo;

        #endregion
        private readonly EfGenericRepository<FormDatas> _FormDatasRepo;
        private readonly EfGenericRepository<EIOForm> _EIOFormRepo;
        private readonly EfGenericRepository<EIOFormConfirm> _EIOFormConfirmRepo;
        private readonly EfGenericRepository<UnlockVip> _UnlockVipRepo;
        private readonly EfGenericRepository<AppConfig> _AppConfigRepo;
        private readonly EfGenericRepository<FormOfPatient> _FormOfPatientRepo;
        private readonly EfGenericRepository<DiagnosticReporting> _DiagnosticReportingRepo;

        public EfUnitOfWork()
        {
            #region General constructor
            _actionRepo = new EfGenericRepository<Models.Action>(context);
            _adminRoleRepo = new EfGenericRepository<AdminRole>(context);
            _clinicRepo = new EfGenericRepository<Clinic>(context);
            _complexOutpatientCaseSummaryRepo = new EfGenericRepository<ComplexOutpatientCaseSummary>(context);
            _customerRepo = new EfGenericRepository<Customer>(context);
            _edStatusRepo = new EfGenericRepository<EDStatus>(context);
            _formRepo = new EfGenericRepository<Form>(context);
            _humanResourceAssessmentRepo = new EfGenericRepository<HumanResourceAssessment>(context);
            _humanResourceAssessmentPositionRepo = new EfGenericRepository<HumanResourceAssessmentPosition>(context);
            _humanResourceAssessmentShiftRepo = new EfGenericRepository<HumanResourceAssessmentShift>(context);
            _humanResourceAssessmentStaffRepo = new EfGenericRepository<HumanResourceAssessmentStaff>(context);
            _ICD10Repo = new EfGenericRepository<ICD10>(context);
            _ICDSpecialtyRepo = new EfGenericRepository<ICDSpecialty>(context);
            _logRepo = new EfGenericRepository<Log>(context);
            _logTmpRepo = new EfGenericRepository<LogTmp>(context);
            _logInFailRepo = new EfGenericRepository<LogInFail>(context);
            _masterDataRepo = new EfGenericRepository<MasterData>(context);
            _medicationMasterdataRepo = new EfGenericRepository<MedicationMasterdata>(context);
            _notificationRepo = new EfGenericRepository<Notification>(context);
            _orderRepo = new EfGenericRepository<Order>(context);
            _positionRepo = new EfGenericRepository<Position>(context);
            _positionUserRepo = new EfGenericRepository<PositionUser>(context);
            _roleRepo = new EfGenericRepository<Role>(context);
            _roleActionRepo = new EfGenericRepository<RoleAction>(context);
            _roleSpecialtyRepo = new EfGenericRepository<RoleSpecialty>(context);
            _roomRepo = new EfGenericRepository<Room>(context);
            _siteRepo = new EfGenericRepository<Site>(context);
            _specialtyRepo = new EfGenericRepository<Specialty>(context);
            _standingOrderMasterDataRepo = new EfGenericRepository<StandingOrderMasterData>(context);
            _systemConfigRepo = new EfGenericRepository<SystemConfig>(context);
            _systemNotificationRepo = new EfGenericRepository<SystemNotification>(context);
            _unlockFormToUpdateRepo = new EfGenericRepository<UnlockFormToUpdate>(context);
            _userRepo = new EfGenericRepository<User>(context);
            _userAdminRoleRepo = new EfGenericRepository<UserAdminRole>(context);
            _userClinicRepo = new EfGenericRepository<UserClinic>(context);
            _userRoleRepo = new EfGenericRepository<UserRole>(context);
            _userSpecialtyRepo = new EfGenericRepository<UserSpecialty>(context);
            _visitTypeGroupRepo = new EfGenericRepository<VisitTypeGroup>(context);
            _serviceRepo = new EfGenericRepository<Service>(context);
            _serviceGroupRepo = new EfGenericRepository<ServiceGroup>(context);
            _cpoeOrderableRepo = new EfGenericRepository<CpoeOrderable>(context);
            _labOrderableRefRepo = new EfGenericRepository<LabOrderableRef>(context);
            _chargeItemRepo = new EfGenericRepository<ChargeItem>(context);

            _chargeVisitRepo = new EfGenericRepository<ChargeVisit>(context);
            _chargeRepo = new EfGenericRepository<Charge>(context);
            _chargeDraftRepo = new EfGenericRepository<ChargeDraft>(context);
            _chargeItemPathologyRepo = new EfGenericRepository<ChargeItemPathology>(context);
            _chargePackageRepo = new EfGenericRepository<ChargePackage>(context);
            _chargePackageServiceRepo = new EfGenericRepository<ChargePackageService>(context);
            _chargePackageUserRepo = new EfGenericRepository<ChargePackageUser>(context);

            _chargeItemMicrobiologyRepo = new EfGenericRepository<ChargeItemMicrobiology>(context);
            _radiologyProcedurePlanRefRepo = new EfGenericRepository<RadiologyProcedurePlanRef>(context);
            _tableDataRepo = new EfGenericRepository<TableData>(context);
            _dietCodeRepo = new EfGenericRepository<DietCode>(context);
            _medicationAdministrationRecordRepo = new EfGenericRepository<MedicationAdministrationRecordModel>(context);
            _ICD10VisitRepo = new EfGenericRepository<ICD10Visit>(context);
            _StillBirthRepo = new EfGenericRepository<StillBirth>(context);
            _SendMailNotificationRepo = new EfGenericRepository<SendMailNotification>(context);
            _PROMForCoronaryDiseaseRepo = new EfGenericRepository<PROMForCoronaryDisease>(context);
            _PROMForheartFailureRepo = new EfGenericRepository<PROMForheartFailure>(context);
            _UploadImageRepo = new EfGenericRepository<UploadImage>(context);
            _AppVersionRepo = new EfGenericRepository<AppVersion>(context);
            #endregion

            #region ED constructor
            _edRepo = new EfGenericRepository<ED>(context);
            _eDEDVerbalOrderRepo = new EfGenericRepository<EDVerbalOrder>(context);
            _eDAmbulanceRunReportRepo = new EfGenericRepository<EDAmbulanceRunReport>(context);
            _eDAmbulanceRunReportDataRepo = new EfGenericRepository<EDAmbulanceRunReportData>(context);
            _eDAmbulanceTransferPatientsRecordRepo = new EfGenericRepository<EDAmbulanceTransferPatientsRecord>(context);
            _eDArterialBloodGasTestRepo = new EfGenericRepository<EDArterialBloodGasTest>(context);
            _eDArterialBloodGasTestDataRepo = new EfGenericRepository<EDArterialBloodGasTestData>(context);
            _eDChemicalBiologyTestRepo = new EfGenericRepository<EDChemicalBiologyTest>(context);
            _eDChemicalBiologyTestDataRepo = new EfGenericRepository<EDChemicalBiologyTestData>(context);
            _eDConsultationDrugWithAnAsteriskMarkRepo = new EfGenericRepository<EDConsultationDrugWithAnAsteriskMark>(context);
            _dischargeInformationRepo = new EfGenericRepository<EDDischargeInformation>(context);
            _dischargeInformationDataRepo = new EfGenericRepository<EDDischargeInformationData>(context);
            _emergencyRecordRepo = new EfGenericRepository<EDEmergencyRecord>(context);
            _emergencyRecordDataRepo = new EfGenericRepository<EDEmergencyRecordData>(context);
            _emergencyTriageRecordRepo = new EfGenericRepository<EDEmergencyTriageRecord>(context);
            _emergencyTriageRecordDataRepo = new EfGenericRepository<EDEmergencyTriageRecordData>(context);
            _handOverCheckListRepo = new EfGenericRepository<EDHandOverCheckList>(context);
            _handOverCheckListDataRepo = new EfGenericRepository<EDHandOverCheckListData>(context);
            _eDInjuryCertificateRepo = new EfGenericRepository<EDInjuryCertificate>(context);
            _monitoringChartAndHandoverFormRepo = new EfGenericRepository<EDMonitoringChartAndHandoverForm>(context);
            _monitoringChartAndHandoverFormDataRepo = new EfGenericRepository<EDMonitoringChartAndHandoverFormData>(context);
            _eDObservationChartRepo = new EfGenericRepository<EDObservationChart>(context);
            _eDObservationChartDataRepo = new EfGenericRepository<EDObservationChartData>(context);
            _patientProgressNoteRepo = new EfGenericRepository<EDPatientProgressNote>(context);
            _patientProgressNoteDataRepo = new EfGenericRepository<EDPatientProgressNoteData>(context);
            _eDPointOfCareTestingMasterDataRepo = new EfGenericRepository<EDPointOfCareTestingMasterData>(context);
            _eDSelfHarmRiskScreeningToolRepo = new EfGenericRepository<EDSelfHarmRiskScreeningTool>(context);
            _eDFallRickScreenningRepo = new EfGenericRepository<EDFallRickScreennings>(context);
            #endregion

            #region OPD constructor
            _oPDRepo = new EfGenericRepository<OPD>(context);
            _oPDFallRiskScreeningRepo = new EfGenericRepository<OPDFallRiskScreening>(context);
            _oPDFallRiskScreeningDataRepo = new EfGenericRepository<OPDFallRiskScreeningData>(context);
            _oPDHandOverCheckListRepo = new EfGenericRepository<OPDHandOverCheckList>(context);
            _oPDHandOverCheckListDataRepo = new EfGenericRepository<OPDHandOverCheckListData>(context);
            _oPDInitialAssessmentForOnGoingRepo = new EfGenericRepository<OPDInitialAssessmentForOnGoing>(context);
            _oPDInitialAssessmentForOnGoingDataRepo = new EfGenericRepository<OPDInitialAssessmentForOnGoingData>(context);
            _oPDInitialAssessmentForShortTermRepo = new EfGenericRepository<OPDInitialAssessmentForShortTerm>(context);
            _oPDInitialAssessmentForShortTermDataRepo = new EfGenericRepository<OPDInitialAssessmentForShortTermData>(context);
            _oPDInitialAssessmentForTelehealthRepo = new EfGenericRepository<OPDInitialAssessmentForTelehealth>(context);
            _oPDInitialAssessmentForTelehealthDataRepo = new EfGenericRepository<OPDInitialAssessmentForTelehealthData>(context);
            _oPDObservationChartRepo = new EfGenericRepository<OPDObservationChart>(context);
            _oPDObservationChartDataRepo = new EfGenericRepository<OPDObservationChartData>(context);
            _oPDOutpatientExaminationNoteRepo = new EfGenericRepository<OPDOutpatientExaminationNote>(context);
            _oPDOutpatientExaminationNoteDataRepo = new EfGenericRepository<OPDOutpatientExaminationNoteData>(context);
            _oPDPatientProgressNoteRepo = new EfGenericRepository<OPDPatientProgressNote>(context);
            _oPDPatientProgressNoteDataRepo = new EfGenericRepository<OPDPatientProgressNoteData>(context);
            _OPDNCCNBROV1Repo = new EfGenericRepository<OPDNCCNBROV1>(context);
            _OPDRiskAssessmentForCancerRepo = new EfGenericRepository<OPDRiskAssessmentForCancer>(context);
            _OPDClinicalBreastExamNoteRepo = new EfGenericRepository<OPDClinicalBreastExamNote>(context);
            _OPDGENBRCARepo = new EfGenericRepository<OPDGENBRCA>(context);
            _OPDPreAnesthesiaHandOverCheckListRepo = new EfGenericRepository<OPDPreAnesthesiaHandOverCheckList>(context);
            #endregion

            #region IPD constructor
            _iPDRepo = new EfGenericRepository<IPD>(context);
            _iPDDischargeMedicalReportRepo = new EfGenericRepository<IPDDischargeMedicalReport>(context);
            _iPDFallRiskAssessmentForAdultRepo = new EfGenericRepository<IPDFallRiskAssessmentForAdult>(context);
            _iPDFallRiskAssessmentForAdultDataRepo = new EfGenericRepository<IPDFallRiskAssessmentForAdultData>(context);
            _iPDFallRiskAssessmentForObstetricRepo = new EfGenericRepository<IPDFallRiskAssessmentForObstetric>(context);
            _iPDFallRiskAssessmentForObstetricDataRepo = new EfGenericRepository<IPDFallRiskAssessmentForObstetricData>(context);
            _iPDGuggingSwallowingScreenRepo = new EfGenericRepository<IPDGuggingSwallowingScreen>(context);
            _iPDGuggingSwallowingScreenDataRepo = new EfGenericRepository<IPDGuggingSwallowingScreenData>(context);
            _iPDHandOverCheckListRepo = new EfGenericRepository<IPDHandOverCheckList>(context);
            _iPDHandOverCheckListDataRepo = new EfGenericRepository<IPDHandOverCheckListData>(context);
            _iPDInitialAssessmentForAdultRepo = new EfGenericRepository<IPDInitialAssessmentForAdult>(context);
            _iPDInitialAssessmentForAdultDataRepo = new EfGenericRepository<IPDInitialAssessmentForAdultData>(context);
            _iPDInitialAssessmentForChemotherapyRepo = new EfGenericRepository<IPDInitialAssessmentForChemotherapy>(context);
            _iPDInitialAssessmentForChemotherapyDataRepo = new EfGenericRepository<IPDInitialAssessmentForChemotherapyData>(context);
            _iPDInitialAssessmentForFrailElderlyRepo = new EfGenericRepository<IPDInitialAssessmentForFrailElderly>(context);
            _iPDInitialAssessmentForFrailElderlyDataRepo = new EfGenericRepository<IPDInitialAssessmentForFrailElderlyData>(context);
            _iPDInitialAssessmentSpecialRequestRepo = new EfGenericRepository<IPDInitialAssessmentSpecialRequest>(context);
            _iPDConsultationDrugWithAnAsteriskMarkRepo = new EfGenericRepository<IPDConsultationDrugWithAnAsteriskMark>(context);
            _iPDMedicalRecordRepo = new EfGenericRepository<IPDMedicalRecord>(context);
            _iPDMedicalRecordDataRepo = new EfGenericRepository<IPDMedicalRecordData>(context);
            _iPDMedicalRecordPart1Repo = new EfGenericRepository<IPDMedicalRecordPart1>(context);
            _iPDMedicalRecordPart1DataRepo = new EfGenericRepository<IPDMedicalRecordPart1Data>(context);
            _iPDMedicalRecordPart2Repo = new EfGenericRepository<IPDMedicalRecordPart2>(context);
            _iPDMedicalRecordPart2DataRepo = new EfGenericRepository<IPDMedicalRecordPart2Data>(context);
            _iPDMedicalRecordPart3Repo = new EfGenericRepository<IPDMedicalRecordPart3>(context);
            _iPDMedicalRecordPart3DataRepo = new EfGenericRepository<IPDMedicalRecordPart3Data>(context);
            _iPDPatientProgressNoteRepo = new EfGenericRepository<IPDPatientProgressNote>(context);
            _iPDPatientProgressNoteDataRepo = new EfGenericRepository<IPDPatientProgressNoteData>(context);
            _iPDPlanOfCareRepo = new EfGenericRepository<IPDPlanOfCare>(context);
            _iPDReferralLetterRepo = new EfGenericRepository<IPDReferralLetter>(context);
            _iPDTransferLetterRepo = new EfGenericRepository<IPDTransferLetter>(context);
            _IPDTakeCareOfPatientsWithCovid19RecommentRepo = new EfGenericRepository<IPDTakeCareOfPatientsWithCovid19Recomment>(context);
            _IPDTakeCareOfPatientsWithCovid19AssessmentRepo = new EfGenericRepository<IPDTakeCareOfPatientsWithCovid19Assessment>(context);
            _IPDDischargePreparationChecklistRepo = new EfGenericRepository<IPDDischargePreparationChecklist>(context);
            _IPDDischargePreparationChecklistDataRepo = new EfGenericRepository<IPDDischargePreparationChecklistData>(context);
            _IPDAdultVitalSignMonitorRepo = new EfGenericRepository<IPDAdultVitalSignMonitor>(context);
            _IPDAdultVitalSignMonitorDataRepo = new EfGenericRepository<IPDAdultVitalSignMonitorData>(context);
            _IPDConfirmDischargeWithoutDirectRepo = new EfGenericRepository<IPDConfirmDischarge>(context);
            _IPDVitalSignForAdultRepo = new EfGenericRepository<VitalSignForAdult>(context);
            _IPDInjuryCertificateRepo = new EfGenericRepository<IPDInjuryCertificate>(context);
            _IPDBradenScaleRepo = new EfGenericRepository<BradenScale>(context);
            _IPDSurgeryCertificateRepo = new EfGenericRepository<IPDSurgeryCertificate>(context);
            _IPDSurgeryCertificateDataRepo = new EfGenericRepository<IPDSurgeryCertificateData>(context);
            _IPDSummayOf15DayTreatmentRepo = new EfGenericRepository<IPDSummaryOf15DayTreatment>(context);
            _IPDMedicationHistoryRepo = new EfGenericRepository<IPDMedicationHistory>(context);
            _IPDSetupMedicalRecordRepo = new EfGenericRepository<IPDSetupMedicalRecord>(context);
            _IPDMedicalRecordOfPatientRepo = new EfGenericRepository<IPDMedicalRecordOfPatients>(context);
            _IPDInitialAssessmentForNewbornsRepo = new EfGenericRepository<IPDInitialAssessmentForNewborns>(context);
            _IPDThrombosisRiskFactorAssessmentRepo = new EfGenericRepository<IPDThrombosisRiskFactorAssessment>(context);
            _IPDThrombosisRiskFactorAssessmentDataRepo = new EfGenericRepository<IPDThrombosisRiskFactorAssessmentData>(context);
            _IPDSiblingRepo = new EfGenericRepository<IPDSibling>(context);
            _IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepo = new EfGenericRepository<IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs>(context);
            _IPDVitalSignForPregnantWomanRepo = new EfGenericRepository<VitalSignForPregnantWoman>(context);
            _IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepo = new EfGenericRepository<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient>(context);
            _IPDCoronaryInterventionRepo = new EfGenericRepository<IPDCoronaryIntervention>(context);
            _IPDPainRecordRepo = new EfGenericRepository<IPDPainRecord>(context);
            _IPDGlamorganRepo = new EfGenericRepository<IPDGlamorgan>(context);
            _IPDGlamorganDataRepo = new EfGenericRepository<IPDGlamorganData>(context);
            _IPDNeonatalObservationChartRepo = new EfGenericRepository<IPDNeonatalObservationChart>(context);
            _IPDVitalSignForPediatricsRepo = new EfGenericRepository<IPDVitalSignForPediatrics>(context);
            _IPDMedicalRecordExtenstionRepo = new EfGenericRepository<IPDMedicalRecordExtenstion>(context);
            _IPDScaleForAssessmentOfSuicideIntentRepo = new EfGenericRepository<IPDScaleForAssessmentOfSuicideIntent>(context);
            #endregion

            #region EIO constructor
            _highlyRestrictedAntimicrobialConsultAntimicrobialOrderRepo = new EfGenericRepository<HighlyRestrictedAntimicrobialConsultAntimicrobialOrder>(context);
            _highlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepo = new EfGenericRepository<HighlyRestrictedAntimicrobialConsultMicrobiologicalResults>(context);
            _highlyRestrictedAntimicrobialConsultRepo = new EfGenericRepository<HighlyRestrictedAntimicrobialConsult>(context);
            _eIOTestCovid2ConfirmationRepo = new EfGenericRepository<EIOTestCovid2Confirmation>(context);
            _complexOutpatientCaseSummaryRepo = new EfGenericRepository<ComplexOutpatientCaseSummary>(context);
            _eDAssessmentForRetailServicePatientRepo = new EfGenericRepository<EIOAssessmentForRetailServicePatient>(context);
            _eDAssessmentForRetailServicePatientDataRepo = new EfGenericRepository<EIOAssessmentForRetailServicePatientData>(context);
            _eIOBloodProductDataRepo = new EfGenericRepository<EIOBloodProductData>(context);
            _eIOBloodRequestSupplyAndConfirmationRepo = new EfGenericRepository<EIOBloodRequestSupplyAndConfirmation>(context);
            _eIOBloodSupplyDataRepo = new EfGenericRepository<EIOBloodSupplyData>(context);
            _eIOBloodTransfusionChecklistRepo = new EfGenericRepository<EIOBloodTransfusionChecklist>(context);
            _eIOBloodTransfusionChecklistDataRepo = new EfGenericRepository<EIOBloodTransfusionChecklistData>(context);
            _eIOCardiacArrestRecordRepo = new EfGenericRepository<EIOCardiacArrestRecord>(context);
            _eIOCardiacArrestRecordDataRepo = new EfGenericRepository<EIOCardiacArrestRecordData>(context);
            _eIOCardiacArrestRecordTableRepo = new EfGenericRepository<EIOCardiacArrestRecordTable>(context);
            _eIOExternalTransportationAssessmentRepo = new EfGenericRepository<EIOExternalTransportationAssessment>(context);
            _eIOExternalTransportationAssessmentDataRepo = new EfGenericRepository<EIOExternalTransportationAssessmentData>(context);
            _eIOExternalTransportationAssessmentEquipmentRepo = new EfGenericRepository<EIOExternalTransportationAssessmentEquipment>(context);
            _eIOJointConsultationForApprovalOfSurgeryRepo = new EfGenericRepository<EIOJointConsultationForApprovalOfSurgery>(context);
            _eIOJointConsultationForApprovalOfSurgeryDataRepo = new EfGenericRepository<EIOJointConsultationForApprovalOfSurgeryData>(context);
            _eIOJointConsultationGroupMinutesRepo = new EfGenericRepository<EIOJointConsultationGroupMinutes>(context);
            _eIOJointConsultationGroupMinutesDataRepo = new EfGenericRepository<EIOJointConsultationGroupMinutesData>(context);
            _eIOJointConsultationGroupMinutesMemberRepo = new EfGenericRepository<EIOJointConsultationGroupMinutesMember>(context);
            _eIOMortalityReportRepo = new EfGenericRepository<EIOMortalityReport>(context);
            _eIOMortalityReportMemberRepo = new EfGenericRepository<EIOMortalityReportMember>(context);
            _eIOPatientOwnMedicationsChartRepo = new EfGenericRepository<EIOPatientOwnMedicationsChart>(context);
            _eIOPatientOwnMedicationsChartDataRepo = new EfGenericRepository<EIOPatientOwnMedicationsChartData>(context);
            _preOperativeProcedureHandoverChecklistRepo = new EfGenericRepository<EIOPreOperativeProcedureHandoverChecklist>(context);
            _preOperativeProcedureHandoverChecklistDataRepo = new EfGenericRepository<EIOPreOperativeProcedureHandoverChecklistData>(context);
            _eIOProcedureSummaryRepo = new EfGenericRepository<EIOProcedureSummary>(context);
            _ProcedureSummaryV2Repo = new EfGenericRepository<ProcedureSummaryV2>(context);
            _eIOProcedureSummaryDataRepo = new EfGenericRepository<EIOProcedureSummaryData>(context);
            _eIOSkinTestResultRepo = new EfGenericRepository<EIOSkinTestResult>(context);
            _eIOSkinTestResultDataRepo = new EfGenericRepository<EIOSkinTestResultData>(context);
            _spongeSharpsAndInstrumentsCountsSheetRepo = new EfGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheet>(context);
            _spongeSharpsAndInstrumentsCountsSheetDataRepo = new EfGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheetData>(context);
            _eDStandingOrderForRetailServiceRepo = new EfGenericRepository<EIOStandingOrderForRetailService>(context);
            _eIOSurgicalProcedureSafetyChecklistRepo = new EfGenericRepository<EIOSurgicalProcedureSafetyChecklist>(context);
            _eIOSurgicalProcedureSafetyChecklistDataRepo = new EfGenericRepository<EIOSurgicalProcedureSafetyChecklistData>(context);
            _eIOSurgicalProcedureSafetyChecklistSignInRepo = new EfGenericRepository<EIOSurgicalProcedureSafetyChecklistSignIn>(context);
            _eIOSurgicalProcedureSafetyChecklistSignOutRepo = new EfGenericRepository<EIOSurgicalProcedureSafetyChecklistSignOut>(context);
            _eIOSurgicalProcedureSafetyChecklistTimeOutRepo = new EfGenericRepository<EIOSurgicalProcedureSafetyChecklistTimeOut>(context);
            _patientAndFamilyEducationRepo = new EfGenericRepository<PatientAndFamilyEducation>(context);
            _patientAndFamilyEducationContentRepo = new EfGenericRepository<PatientAndFamilyEducationContent>(context);
            _patientAndFamilyEducationContentDataRepo = new EfGenericRepository<PatientAndFamilyEducationContentData>(context);
            _patientAndFamilyEducationDataRepo = new EfGenericRepository<PatientAndFamilyEducationData>(context);
            _translationRepo = new EfGenericRepository<Translation>(context);
            _translationDataRepo = new EfGenericRepository<TranslationData>(context);
            _eIOPhysicianNoteRepo = new EfGenericRepository<EIOPhysicianNote>(context);
            _eIOCareNoteRepo = new EfGenericRepository<EIOCareNote>(context);
           _SurgeryAndProcedureSummaryV3Repo = new EfGenericRepository<SurgeryAndProcedureSummaryV3>(context);
            #endregion
            _ecoRepo = new EfGenericRepository<EOC>(context);
            _ecoCTransferRepo = new EfGenericRepository<EOCTransfer>(context);
            _ecoFallRiskScreeningRepo = new EfGenericRepository<EOCFallRiskScreening>(context);
            _ecoInitialAssessmentForOnGoinRepo = new EfGenericRepository<EOCInitialAssessmentForOnGoing>(context);
            _ecoInitialAssessmentForShortTermRepo = new EfGenericRepository<EOCInitialAssessmentForShortTerm>(context);
            _ecoEOCOutpatientExaminationNoteRepo = new EfGenericRepository<EOCOutpatientExaminationNote>(context);
            _ecoEOCHandOverCheckListRepo = new EfGenericRepository<EOCHandOverCheckList>(context);

            #region EIO constructor

            #endregion
            _FormDatasRepo = new EfGenericRepository<FormDatas>(context);
            _EIOFormRepo = new EfGenericRepository<EIOForm>(context);
            _EIOFormConfirmRepo = new EfGenericRepository<EIOFormConfirm>(context);
            _UnlockVipRepo = new EfGenericRepository<UnlockVip>(context);
            _AppConfigRepo = new EfGenericRepository<AppConfig>(context);
            _FormOfPatientRepo = new EfGenericRepository<FormOfPatient>(context);
            _DiagnosticReportingRepo = new EfGenericRepository<DiagnosticReporting>(context);
            _PreAnesthesiaConsultationRepo = new EfGenericRepository<PreAnesthesiaConsultation>(context);
            _EIOConstraintNewbornAndPregnantWomanRepo = new EfGenericRepository<EIOConstraintNewbornAndPregnantWoman>(context);
            #region PrescriptionNote constructor
            _PrescriptionNoteRepo = new EfGenericRepository<PrescriptionNoteModel>(context);
            _PrescriptionRoundInfoRepo = new EfGenericRepository<PresciptionRoundInfoModel>(context);
            #endregion
        }

        #region General IGenericRepository
        public IGenericRepository<Models.Action> ActionRepository => _actionRepo;
        public IGenericRepository<AdminRole> AdminRoleRepository => _adminRoleRepo;
        public IGenericRepository<Clinic> ClinicRepository => _clinicRepo;
        public IGenericRepository<Customer> CustomerRepository => _customerRepo;
        public IGenericRepository<EDStatus> EDStatusRepository => _edStatusRepo;
        public IGenericRepository<Form> FormRepository => _formRepo;
        public IGenericRepository<HumanResourceAssessment> HumanResourceAssessmentRepository => _humanResourceAssessmentRepo;
        public IGenericRepository<HumanResourceAssessmentPosition> HumanResourceAssessmentPositionRepository => _humanResourceAssessmentPositionRepo;
        public IGenericRepository<HumanResourceAssessmentShift> HumanResourceAssessmentShiftRepository => _humanResourceAssessmentShiftRepo;
        public IGenericRepository<HumanResourceAssessmentStaff> HumanResourceAssessmentStaffRepository => _humanResourceAssessmentStaffRepo;
        public IGenericRepository<ICD10> ICD10Repository => _ICD10Repo;
        public IGenericRepository<ICDSpecialty> ICDSpecialtyRepository => _ICDSpecialtyRepo;
        public IGenericRepository<Log> LogRepository => _logRepo;
        public IGenericRepository<LogTmp> LogTmpRepository => _logTmpRepo;
        public IGenericRepository<LogInFail> LogInFailRepository => _logInFailRepo;
        public IGenericRepository<MasterData> MasterDataRepository => _masterDataRepo;
        public IGenericRepository<MedicationMasterdata> MedicationMasterdataRepository => _medicationMasterdataRepo;
        public IGenericRepository<Notification> NotificationRepository => _notificationRepo;
        public IGenericRepository<Order> OrderRepository => _orderRepo;
        public IGenericRepository<Position> PositionRepository => _positionRepo;
        public IGenericRepository<PositionUser> PositionUserRepository => _positionUserRepo;
        public IGenericRepository<Role> RoleRepository => _roleRepo;
        public IGenericRepository<RoleAction> RoleActionRepository => _roleActionRepo;
        public IGenericRepository<RoleSpecialty> RoleSpecialtyRepository => _roleSpecialtyRepo;
        public IGenericRepository<Room> RoomRepository => _roomRepo;
        public IGenericRepository<Site> SiteRepository => _siteRepo;
        public IGenericRepository<Specialty> SpecialtyRepository => _specialtyRepo;
        public IGenericRepository<StandingOrderMasterData> StandingOrderMasterDataRepository => _standingOrderMasterDataRepo;
        public IGenericRepository<SystemConfig> SystemConfigRepository => _systemConfigRepo;
        public IGenericRepository<SystemNotification> SystemNotificationRepository => _systemNotificationRepo;
        public IGenericRepository<UnlockFormToUpdate> UnlockFormToUpdateRepository => _unlockFormToUpdateRepo;
        public IGenericRepository<User> UserRepository => _userRepo;
        public IGenericRepository<UserAdminRole> UserAdminRoleRepository => _userAdminRoleRepo;
        public IGenericRepository<UserClinic> UserClinicRepository => _userClinicRepo;
        public IGenericRepository<UserRole> UserRoleRepository => _userRoleRepo;
        public IGenericRepository<UserSpecialty> UserSpecialtyRepository => _userSpecialtyRepo;
        public IGenericRepository<VisitTypeGroup> VisitTypeGroupRepository => _visitTypeGroupRepo;
        public IGenericRepository<Service> ServiceRepository => _serviceRepo;
        public IGenericRepository<ServiceGroup> ServiceGroupRepository => _serviceGroupRepo;
        public IGenericRepository<CpoeOrderable> CpoeOrderableRepository => _cpoeOrderableRepo;

        public IGenericRepository<LabOrderableRef> LabOrderableRefRepository => _labOrderableRefRepo;

        public IGenericRepository<ChargeVisit> ChargeVistRepository => _chargeVisitRepo;
        public IGenericRepository<Charge> ChargeRepository => _chargeRepo;
        public IGenericRepository<ChargeDraft> ChargeDraftRepository => _chargeDraftRepo;

        public IGenericRepository<ChargeItem> ChargeItemRepository => _chargeItemRepo;
        public IGenericRepository<ChargeItemMicrobiology> ChargeItemMicrobiologyRepository => _chargeItemMicrobiologyRepo;
        public IGenericRepository<ChargeItemPathology> ChargeItemPathologyRepository => _chargeItemPathologyRepo;
        public IGenericRepository<ChargePackage> ChargePackageRepository => _chargePackageRepo;
        public IGenericRepository<ChargePackageService> ChargePackageServiceRepository => _chargePackageServiceRepo;
        public IGenericRepository<ChargePackageUser> ChargePackageUserRepository => _chargePackageUserRepo;
        public IGenericRepository<RadiologyProcedurePlanRef> RadiologyProcedurePlanRefRepository => _radiologyProcedurePlanRefRepo;
        public IGenericRepository<TableData> TableDataRepository => _tableDataRepo;
        public IGenericRepository<DietCode> DietCodeRepository => _dietCodeRepo;
        public IGenericRepository<MedicationAdministrationRecordModel> MedicationAdministrationRecordRepository => _medicationAdministrationRecordRepo;
        public IGenericRepository<ICD10Visit> ICD10VisitRepository => _ICD10VisitRepo;
        public IGenericRepository<StillBirth> StillBirthRepository => _StillBirthRepo;
        public IGenericRepository<SendMailNotification> SendMailNotificationRepository => _SendMailNotificationRepo;
        public IGenericRepository<PROMForCoronaryDisease> PROMForCoronaryDiseaseRepository => _PROMForCoronaryDiseaseRepo;
        public IGenericRepository<PROMForheartFailure> PROMForheartFailureRepository => _PROMForheartFailureRepo;
        public IGenericRepository<UploadImage> UploadImageRepository => _UploadImageRepo;
        public IGenericRepository<AppVersion> AppVersionRepository => _AppVersionRepo;
        #endregion

        #region ED IGenericRepository
        public IGenericRepository<ED> EDRepository => _edRepo;
        public IGenericRepository<EDAmbulanceRunReport> EDAmbulanceRunReportRepository => _eDAmbulanceRunReportRepo;
        public IGenericRepository<EDVerbalOrder> EDVerbalOrderRepository => _eDEDVerbalOrderRepo;
        public IGenericRepository<EDAmbulanceRunReportData> EDAmbulanceRunReportDataRepository => _eDAmbulanceRunReportDataRepo;
        public IGenericRepository<EDAmbulanceTransferPatientsRecord> EDAmbulanceTransferPatientsRecordRepository => _eDAmbulanceTransferPatientsRecordRepo;
        public IGenericRepository<EDArterialBloodGasTest> EDArterialBloodGasTestRepository => _eDArterialBloodGasTestRepo;
        public IGenericRepository<EDArterialBloodGasTestData> EDArterialBloodGasTestDataRepository => _eDArterialBloodGasTestDataRepo;
        public IGenericRepository<EDChemicalBiologyTest> EDChemicalBiologyTestRepository => _eDChemicalBiologyTestRepo;
        public IGenericRepository<EDChemicalBiologyTestData> EDChemicalBiologyTestDataRepository => _eDChemicalBiologyTestDataRepo;
        public IGenericRepository<EDConsultationDrugWithAnAsteriskMark> EDConsultationDrugWithAnAsteriskMarkRepository => _eDConsultationDrugWithAnAsteriskMarkRepo;
        public IGenericRepository<EDDischargeInformation> DischargeInformationRepository => _dischargeInformationRepo;
        public IGenericRepository<EDDischargeInformationData> DischargeInformationDataRepository => _dischargeInformationDataRepo;
        public IGenericRepository<EDEmergencyRecord> EmergencyRecordRepository => _emergencyRecordRepo;
        public IGenericRepository<EDEmergencyRecordData> EmergencyRecordDataRepository => _emergencyRecordDataRepo;
        public IGenericRepository<EDEmergencyTriageRecord> EmergencyTriageRecordRepository => _emergencyTriageRecordRepo;
        public IGenericRepository<EDEmergencyTriageRecordData> EmergencyTriageRecordDataRepository => _emergencyTriageRecordDataRepo;
        public IGenericRepository<EDHandOverCheckList> HandOverCheckListRepository => _handOverCheckListRepo;
        public IGenericRepository<EDHandOverCheckListData> HandOverCheckListDataRepository => _handOverCheckListDataRepo;
        public IGenericRepository<EDInjuryCertificate> EDInjuryCertificateRepository => _eDInjuryCertificateRepo;
        public IGenericRepository<EDMonitoringChartAndHandoverForm> MonitoringChartAndHandoverFormRepository => _monitoringChartAndHandoverFormRepo;
        public IGenericRepository<EDMonitoringChartAndHandoverFormData> MonitoringChartAndHandoverFormDataRepository => _monitoringChartAndHandoverFormDataRepo;
        public IGenericRepository<EDObservationChart> EDObservationChartRepository => _eDObservationChartRepo;
        public IGenericRepository<EDObservationChartData> EDObservationChartDataRepository => _eDObservationChartDataRepo;
        public IGenericRepository<EDPatientProgressNote> PatientProgressNoteRepository => _patientProgressNoteRepo;
        public IGenericRepository<EDPatientProgressNoteData> PatientProgressNoteDataRepository => _patientProgressNoteDataRepo;

        public IGenericRepository<EDPointOfCareTestingMasterData> EDPointOfCareTestingMasterDataRepository => _eDPointOfCareTestingMasterDataRepo;
        public IGenericRepository<EDSelfHarmRiskScreeningTool> EDSelfHarmRiskScreeningToolRepository => _eDSelfHarmRiskScreeningToolRepo;
        public IGenericRepository<EDFallRickScreennings> EDFallRickScreenningRepository => _eDFallRickScreenningRepo;
        #endregion
       

        #region OPD IGenericRepository
        public IGenericRepository<OPD> OPDRepository => _oPDRepo;
        public IGenericRepository<OPDFallRiskScreening> OPDFallRiskScreeningRepository => _oPDFallRiskScreeningRepo;
        public IGenericRepository<OPDFallRiskScreeningData> OPDFallRiskScreeningDataRepository => _oPDFallRiskScreeningDataRepo;
        public IGenericRepository<OPDHandOverCheckList> OPDHandOverCheckListRepository => _oPDHandOverCheckListRepo;
        public IGenericRepository<OPDHandOverCheckListData> OPDHandOverCheckListDataRepository => _oPDHandOverCheckListDataRepo;
        public IGenericRepository<OPDInitialAssessmentForOnGoing> OPDInitialAssessmentForOnGoingRepository => _oPDInitialAssessmentForOnGoingRepo;
        public IGenericRepository<OPDInitialAssessmentForOnGoingData> OPDInitialAssessmentForOnGoingDataRepository => _oPDInitialAssessmentForOnGoingDataRepo;
        public IGenericRepository<OPDInitialAssessmentForShortTerm> OPDInitialAssessmentForShortTermRepository => _oPDInitialAssessmentForShortTermRepo;
        public IGenericRepository<OPDInitialAssessmentForShortTermData> OPDInitialAssessmentForShortTermDataRepository => _oPDInitialAssessmentForShortTermDataRepo;
        public IGenericRepository<OPDInitialAssessmentForTelehealth> OPDInitialAssessmentForTelehealthRepository => _oPDInitialAssessmentForTelehealthRepo;
        public IGenericRepository<OPDInitialAssessmentForTelehealthData> OPDInitialAssessmentForTelehealthDataRepository => _oPDInitialAssessmentForTelehealthDataRepo;
        public IGenericRepository<OPDObservationChart> OPDObservationChartRepository => _oPDObservationChartRepo;
        public IGenericRepository<OPDObservationChartData> OPDObservationChartDataRepository => _oPDObservationChartDataRepo;
        public IGenericRepository<OPDOutpatientExaminationNote> OPDOutpatientExaminationNoteRepository => _oPDOutpatientExaminationNoteRepo;
        public IGenericRepository<OPDOutpatientExaminationNoteData> OPDOutpatientExaminationNoteDataRepository => _oPDOutpatientExaminationNoteDataRepo;
        public IGenericRepository<OPDPatientProgressNote> OPDPatientProgressNoteRepository => _oPDPatientProgressNoteRepo;
        public IGenericRepository<OPDPatientProgressNoteData> OPDPatientProgressNoteDataRepository => _oPDPatientProgressNoteDataRepo;
        public IGenericRepository<OPDNCCNBROV1> OPDNCCNBROV1Repository => _OPDNCCNBROV1Repo;
        public IGenericRepository<OPDRiskAssessmentForCancer> OPDRiskAssessmentForCancerRepository => _OPDRiskAssessmentForCancerRepo;
        public IGenericRepository<OPDClinicalBreastExamNote> OPDClinicalBreastExamNoteRepository => _OPDClinicalBreastExamNoteRepo;
        public IGenericRepository<OPDGENBRCA> OPDGENBRCARepository => _OPDGENBRCARepo;
        public IGenericRepository<OPDPreAnesthesiaHandOverCheckList> OPDPreAnesthesiaHandOverCheckListRepository => _OPDPreAnesthesiaHandOverCheckListRepo;

        #endregion

        #region IPD IGenericRepository
        public IGenericRepository<IPD> IPDRepository => _iPDRepo;
        public IGenericRepository<IPDDischargeMedicalReport> IPDDischargeMedicalReportRepository => _iPDDischargeMedicalReportRepo;
        public IGenericRepository<IPDFallRiskAssessmentForAdult> IPDFallRiskAssessmentForAdultRepository => _iPDFallRiskAssessmentForAdultRepo;
        public IGenericRepository<IPDFallRiskAssessmentForAdultData> IPDFallRiskAssessmentForAdultDataRepository => _iPDFallRiskAssessmentForAdultDataRepo;
        public IGenericRepository<IPDFallRiskAssessmentForObstetric> IPDFallRiskAssessmentForObstetricRepository => _iPDFallRiskAssessmentForObstetricRepo;
        public IGenericRepository<IPDFallRiskAssessmentForObstetricData> IPDFallRiskAssessmentForObstetricDataRepository => _iPDFallRiskAssessmentForObstetricDataRepo;
        public IGenericRepository<IPDGuggingSwallowingScreen> IPDGuggingSwallowingScreenRepository => _iPDGuggingSwallowingScreenRepo;
        public IGenericRepository<IPDGuggingSwallowingScreenData> IPDGuggingSwallowingScreenDataRepository => _iPDGuggingSwallowingScreenDataRepo;
        public IGenericRepository<IPDHandOverCheckList> IPDHandOverCheckListRepository => _iPDHandOverCheckListRepo;
        public IGenericRepository<IPDHandOverCheckListData> IPDHandOverCheckListDataRepository => _iPDHandOverCheckListDataRepo;
        public IGenericRepository<IPDInitialAssessmentForAdult> IPDInitialAssessmentForAdultRepository => _iPDInitialAssessmentForAdultRepo;
        public IGenericRepository<IPDInitialAssessmentForAdultData> IPDInitialAssessmentForAdultDataRepository => _iPDInitialAssessmentForAdultDataRepo;
        public IGenericRepository<IPDInitialAssessmentForChemotherapy> IPDInitialAssessmentForChemotherapyRepository => _iPDInitialAssessmentForChemotherapyRepo;
        public IGenericRepository<IPDInitialAssessmentForChemotherapyData> IPDInitialAssessmentForChemotherapyDataRepository => _iPDInitialAssessmentForChemotherapyDataRepo;
        public IGenericRepository<IPDInitialAssessmentForFrailElderly> IPDInitialAssessmentForFrailElderlyRepository => _iPDInitialAssessmentForFrailElderlyRepo;
        public IGenericRepository<IPDInitialAssessmentForFrailElderlyData> IPDInitialAssessmentForFrailElderlyDataRepository => _iPDInitialAssessmentForFrailElderlyDataRepo;
        public IGenericRepository<IPDInitialAssessmentSpecialRequest> IPDInitialAssessmentSpecialRequestRepository => _iPDInitialAssessmentSpecialRequestRepo;
        public IGenericRepository<IPDMedicalRecord> IPDMedicalRecordRepository => _iPDMedicalRecordRepo;
        public IGenericRepository<IPDMedicalRecordData> IPDMedicalRecordDataRepository => _iPDMedicalRecordDataRepo;
        public IGenericRepository<IPDMedicalRecordPart1> IPDMedicalRecordPart1Repository => _iPDMedicalRecordPart1Repo;
        public IGenericRepository<IPDMedicalRecordPart1Data> IPDMedicalRecordPart1DataRepository => _iPDMedicalRecordPart1DataRepo;
        public IGenericRepository<IPDMedicalRecordPart2> IPDMedicalRecordPart2Repository => _iPDMedicalRecordPart2Repo;
        public IGenericRepository<IPDMedicalRecordPart2Data> IPDMedicalRecordPart2DataRepository => _iPDMedicalRecordPart2DataRepo;
        public IGenericRepository<IPDMedicalRecordPart3> IPDMedicalRecordPart3Repository => _iPDMedicalRecordPart3Repo;
        public IGenericRepository<IPDMedicalRecordPart3Data> IPDMedicalRecordPart3DataRepository => _iPDMedicalRecordPart3DataRepo;
        public IGenericRepository<IPDPatientProgressNote> IPDPatientProgressNoteRepository => _iPDPatientProgressNoteRepo;
        public IGenericRepository<IPDPatientProgressNoteData> IPDPatientProgressNoteDataRepository => _iPDPatientProgressNoteDataRepo;
        public IGenericRepository<IPDPlanOfCare> IPDPlanOfCareRepository => _iPDPlanOfCareRepo;
        public IGenericRepository<IPDReferralLetter> IPDReferralLetterRepository => _iPDReferralLetterRepo;
        public IGenericRepository<IPDTransferLetter> IPDTransferLetterRepository => _iPDTransferLetterRepo;
        public IGenericRepository<IPDTakeCareOfPatientsWithCovid19Recomment> IPDTakeCareOfPatientsWithCovid19RecommentRepository => _IPDTakeCareOfPatientsWithCovid19RecommentRepo;
        public IGenericRepository<IPDTakeCareOfPatientsWithCovid19Assessment> IPDTakeCareOfPatientsWithCovid19AssessmentRepository => _IPDTakeCareOfPatientsWithCovid19AssessmentRepo;
        public IGenericRepository<IPDConsultationDrugWithAnAsteriskMark> IPDConsultationDrugWithAnAsteriskMarkRepository => _iPDConsultationDrugWithAnAsteriskMarkRepo;
        public IGenericRepository<IPDConfirmDischarge> IPDConfirmDischargeWithoutDirectRepository => _IPDConfirmDischargeWithoutDirectRepo;
        public IGenericRepository<VitalSignForAdult> IPDVitalSignForAdultRespository => _IPDVitalSignForAdultRepo;
        public IGenericRepository<IPDInjuryCertificate> IPDInjuryCertificateRepository => _IPDInjuryCertificateRepo;
        public IGenericRepository<BradenScale> IPDBradenScaleRepository => _IPDBradenScaleRepo;
        public IGenericRepository<IPDSurgeryCertificate> IPDSurgeryCertificateRepository => _IPDSurgeryCertificateRepo;
        public IGenericRepository<IPDSurgeryCertificateData> IPDSurgeryCertificateDataRepository => _IPDSurgeryCertificateDataRepo;
        public IGenericRepository<IPDSummaryOf15DayTreatment> IPDSummayOf15DayTreatmentRepository => _IPDSummayOf15DayTreatmentRepo;
        public IGenericRepository<IPDMedicationHistory> IPDMedicationHistoryRepository => _IPDMedicationHistoryRepo;
        public IGenericRepository<IPDSetupMedicalRecord> IPDSetupMedicalRecordRepository => _IPDSetupMedicalRecordRepo;
        public IGenericRepository<IPDMedicalRecordOfPatients> IPDMedicalRecordOfPatientRepository => _IPDMedicalRecordOfPatientRepo;
        public IGenericRepository<IPDInitialAssessmentForNewborns> IPDInitialAssessmentForNewbornsRepository => _IPDInitialAssessmentForNewbornsRepo;
        public IGenericRepository<IPDThrombosisRiskFactorAssessment> IPDThrombosisRiskFactorAssessmentRepository => _IPDThrombosisRiskFactorAssessmentRepo;
        public IGenericRepository<IPDThrombosisRiskFactorAssessmentData> IPDThrombosisRiskFactorAssessmentDataRepository => _IPDThrombosisRiskFactorAssessmentDataRepo;
        public IGenericRepository<IPDSibling> IPDSiblingRepository => _IPDSiblingRepo;
        public IGenericRepository<IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs> IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository => _IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepo;
        public IGenericRepository<VitalSignForPregnantWoman> IPDVitalSignForPregnantWomanRepository => _IPDVitalSignForPregnantWomanRepo;
        public IGenericRepository<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient> IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepository => _IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepo;
        public IGenericRepository<IPDCoronaryIntervention> IPDCoronaryInterventionRepository => _IPDCoronaryInterventionRepo;
        public IGenericRepository<IPDPainRecord> IPDPainRecordRepository => _IPDPainRecordRepo;
        public IGenericRepository<IPDGlamorgan> IPDGlamorganRepository => _IPDGlamorganRepo;
        public IGenericRepository<IPDGlamorganData> IPDGlamorganDataRepository => _IPDGlamorganDataRepo;
        public IGenericRepository<IPDNeonatalObservationChart> IPDNeonatalObservationChartRepository => _IPDNeonatalObservationChartRepo;
        public IGenericRepository<IPDVitalSignForPediatrics> IPDVitalSignForPediatricsReponsitory => _IPDVitalSignForPediatricsRepo;
        public IGenericRepository<IPDMedicalRecordExtenstion> IPDMedicalRecordExtenstionReponsitory => _IPDMedicalRecordExtenstionRepo;
        public IGenericRepository<IPDScaleForAssessmentOfSuicideIntent> IPDScaleForAssessmentOfSuicideIntentReponsitory => _IPDScaleForAssessmentOfSuicideIntentRepo;
        #endregion

        #region EIO IGenericRepository
        public IGenericRepository<HighlyRestrictedAntimicrobialConsult> HighlyRestrictedAntimicrobialConsultRepository => _highlyRestrictedAntimicrobialConsultRepo;
        public IGenericRepository<HighlyRestrictedAntimicrobialConsultMicrobiologicalResults> HighlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepository => _highlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepo;
        public IGenericRepository<HighlyRestrictedAntimicrobialConsultAntimicrobialOrder> HighlyRestrictedAntimicrobialConsultAntimicrobialOrderRepository => _highlyRestrictedAntimicrobialConsultAntimicrobialOrderRepo;
        public IGenericRepository<EIOTestCovid2Confirmation> EIOTestCovid2ConfirmationRepository => _eIOTestCovid2ConfirmationRepo;
        public IGenericRepository<ComplexOutpatientCaseSummary> ComplexOutpatientCaseSummaryRepository => _complexOutpatientCaseSummaryRepo;
        public IGenericRepository<EIOAssessmentForRetailServicePatient> EIOAssessmentForRetailServicePatientRepository => _eDAssessmentForRetailServicePatientRepo;
        public IGenericRepository<EIOAssessmentForRetailServicePatientData> EIOAssessmentForRetailServicePatientDataRepository => _eDAssessmentForRetailServicePatientDataRepo;
        public IGenericRepository<EIOBloodProductData> EIOBloodProductDataRepository => _eIOBloodProductDataRepo;
        public IGenericRepository<EIOBloodRequestSupplyAndConfirmation> EIOBloodRequestSupplyAndConfirmationRepository => _eIOBloodRequestSupplyAndConfirmationRepo;
        public IGenericRepository<EIOBloodSupplyData> EIOBloodSupplyDataRepository => _eIOBloodSupplyDataRepo;
        public IGenericRepository<EIOBloodTransfusionChecklist> EIOBloodTransfusionChecklistRepository => _eIOBloodTransfusionChecklistRepo;
        public IGenericRepository<EIOBloodTransfusionChecklistData> EIOBloodTransfusionChecklistDataRepository => _eIOBloodTransfusionChecklistDataRepo;
        public IGenericRepository<EIOCardiacArrestRecord> EIOCardiacArrestRecordRepository => _eIOCardiacArrestRecordRepo;
        public IGenericRepository<EIOCardiacArrestRecordData> EIOCardiacArrestRecordDataRepository => _eIOCardiacArrestRecordDataRepo;
        public IGenericRepository<EIOCardiacArrestRecordTable> EIOCardiacArrestRecordTableRepository => _eIOCardiacArrestRecordTableRepo;
        public IGenericRepository<EIOExternalTransportationAssessment> EIOExternalTransportationAssessmentRepository => _eIOExternalTransportationAssessmentRepo;
        public IGenericRepository<EIOExternalTransportationAssessmentData> EIOExternalTransportationAssessmentDataRepository => _eIOExternalTransportationAssessmentDataRepo;
        public IGenericRepository<EIOExternalTransportationAssessmentEquipment> EIOExternalTransportationAssessmentEquipmentRepository => _eIOExternalTransportationAssessmentEquipmentRepo;
        public IGenericRepository<EIOJointConsultationForApprovalOfSurgery> EIOJointConsultationForApprovalOfSurgeryRepository => _eIOJointConsultationForApprovalOfSurgeryRepo;
        public IGenericRepository<EIOJointConsultationForApprovalOfSurgeryData> EIOJointConsultationForApprovalOfSurgeryDataRepository => _eIOJointConsultationForApprovalOfSurgeryDataRepo;
        public IGenericRepository<EIOJointConsultationGroupMinutes> EIOJointConsultationGroupMinutesRepository => _eIOJointConsultationGroupMinutesRepo;
        public IGenericRepository<EIOJointConsultationGroupMinutesData> EIOJointConsultationGroupMinutesDataRepository => _eIOJointConsultationGroupMinutesDataRepo;
        public IGenericRepository<EIOJointConsultationGroupMinutesMember> EIOJointConsultationGroupMinutesMemberRepository => _eIOJointConsultationGroupMinutesMemberRepo;
        public IGenericRepository<EIOMortalityReport> EIOMortalityReportRepository => _eIOMortalityReportRepo;
        public IGenericRepository<EIOMortalityReportMember> EIOMortalityReportMemberRepository => _eIOMortalityReportMemberRepo;
        public IGenericRepository<EIOPatientOwnMedicationsChart> EIOPatientOwnMedicationsChartRepository => _eIOPatientOwnMedicationsChartRepo;
        public IGenericRepository<EIOPatientOwnMedicationsChartData> EIOPatientOwnMedicationsChartDataRepository => _eIOPatientOwnMedicationsChartDataRepo;
        public IGenericRepository<EIOPreOperativeProcedureHandoverChecklist> EIOPreOperativeProcedureHandoverChecklistRepository => _preOperativeProcedureHandoverChecklistRepo;
        public IGenericRepository<EIOPreOperativeProcedureHandoverChecklistData> EIOPreOperativeProcedureHandoverChecklistDataRepository => _preOperativeProcedureHandoverChecklistDataRepo;
        public IGenericRepository<EIOProcedureSummary> EIOProcedureSummaryRepository => _eIOProcedureSummaryRepo;
        public IGenericRepository<ProcedureSummaryV2> ProcedureSummaryV2Repository => _ProcedureSummaryV2Repo;
        public IGenericRepository<EIOProcedureSummaryData> EIOProcedureSummaryDataRepository => _eIOProcedureSummaryDataRepo;
        public IGenericRepository<EIOSkinTestResult> EIOSkinTestResultRepository => _eIOSkinTestResultRepo;
        public IGenericRepository<EIOSkinTestResultData> EIOSkinTestResultDataRepository => _eIOSkinTestResultDataRepo;
        public IGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheet> EIOSpongeSharpsAndInstrumentsCountsSheetRepository => _spongeSharpsAndInstrumentsCountsSheetRepo;
        public IGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheetData> EIOSpongeSharpsAndInstrumentsCountsSheetDataRepository => _spongeSharpsAndInstrumentsCountsSheetDataRepo;
        public IGenericRepository<EIOStandingOrderForRetailService> EIOStandingOrderForRetailServiceRepository => _eDStandingOrderForRetailServiceRepo;
        public IGenericRepository<EIOSurgicalProcedureSafetyChecklist> EIOSurgicalProcedureSafetyChecklistRepository => _eIOSurgicalProcedureSafetyChecklistRepo;
        public IGenericRepository<EIOSurgicalProcedureSafetyChecklistData> EIOSurgicalProcedureSafetyChecklistDataRepository => _eIOSurgicalProcedureSafetyChecklistDataRepo;
        public IGenericRepository<EIOSurgicalProcedureSafetyChecklistSignIn> EIOSurgicalProcedureSafetyChecklistSignInRepository => _eIOSurgicalProcedureSafetyChecklistSignInRepo;
        public IGenericRepository<EIOSurgicalProcedureSafetyChecklistSignOut> EIOSurgicalProcedureSafetyChecklistSignOutRepository => _eIOSurgicalProcedureSafetyChecklistSignOutRepo;
        public IGenericRepository<EIOSurgicalProcedureSafetyChecklistTimeOut> EIOSurgicalProcedureSafetyChecklistTimeOutRepository => _eIOSurgicalProcedureSafetyChecklistTimeOutRepo;
        public IGenericRepository<PatientAndFamilyEducation> PatientAndFamilyEducationRepository => _patientAndFamilyEducationRepo;
        public IGenericRepository<PatientAndFamilyEducationContent> PatientAndFamilyEducationContentRepository => _patientAndFamilyEducationContentRepo;
        public IGenericRepository<PatientAndFamilyEducationContentData> PatientAndFamilyEducationContentDataRepository => _patientAndFamilyEducationContentDataRepo;
        public IGenericRepository<PatientAndFamilyEducationData> PatientAndFamilyEducationDataRepository => _patientAndFamilyEducationDataRepo;
        public IGenericRepository<Translation> TranslationRepository => _translationRepo;
        public IGenericRepository<TranslationData> TranslationDataRepository => _translationDataRepo;
        public IGenericRepository<EIOPhysicianNote> EIOPhysicianNoteRepository => _eIOPhysicianNoteRepo;
        public IGenericRepository<PreAnesthesiaConsultation> PreAnesthesiaConsultationRepository => _PreAnesthesiaConsultationRepo;
        public IGenericRepository<SurgeryAndProcedureSummaryV3> SurgeryAndProcedureSummaryV3Repository => _SurgeryAndProcedureSummaryV3Repo;
        public IGenericRepository<EIOConstraintNewbornAndPregnantWoman> EIOConstraintNewbornAndPregnantWomanRepository => _EIOConstraintNewbornAndPregnantWomanRepo;
        #endregion

        #region EOC IGenericRepository
        public IGenericRepository<EOC> EOCRepository => _ecoRepo;
        public IGenericRepository<EOCTransfer> EOCTransferRepository => _ecoCTransferRepo;
        public IGenericRepository<EOCFallRiskScreening> EOCFallRiskScreeningRepository => _ecoFallRiskScreeningRepo;
        public IGenericRepository<EOCInitialAssessmentForOnGoing> EOCInitialAssessmentForOnGoingRepository => _ecoInitialAssessmentForOnGoinRepo;
        public IGenericRepository<EOCInitialAssessmentForShortTerm> EOCInitialAssessmentForShortTermRepository => _ecoInitialAssessmentForShortTermRepo;
        public IGenericRepository<EOCOutpatientExaminationNote> EOCOutpatientExaminationNoteRepository => _ecoEOCOutpatientExaminationNoteRepo;
        public IGenericRepository<EOCHandOverCheckList> EOCHandOverCheckListRepository => _ecoEOCHandOverCheckListRepo;

        #endregion

        #region PrescriptionNote IGenericRepository
        public IGenericRepository<PrescriptionNoteModel> PrescriptionNoteRepository => _PrescriptionNoteRepo;
        public IGenericRepository<PresciptionRoundInfoModel> PrescriptionRoundInfoRepository => _PrescriptionRoundInfoRepo;
        #endregion

        public IGenericRepository<FormDatas> FormDatasRepository => _FormDatasRepo;
        public IGenericRepository<AppConfig> AppConfigRepository => _AppConfigRepo;
        public IGenericRepository<EIOForm> EIOFormRepository => _EIOFormRepo;
        public IGenericRepository<EIOFormConfirm> EIOFormConfirmRepository => _EIOFormConfirmRepo;
        public IGenericRepository<UnlockVip> UnlockVipRepository => _UnlockVipRepo;

        public IGenericRepository<EIOCareNote> EIOCareNoteRepository => _eIOCareNoteRepo;

        public IGenericRepository<IPDDischargePreparationChecklist> IPDDischargePreparationChecklistRepository => _IPDDischargePreparationChecklistRepo;

        public IGenericRepository<IPDDischargePreparationChecklistData> IPDDischargePreparationChecklistDataRepository => _IPDDischargePreparationChecklistDataRepo;

        public IGenericRepository<IPDAdultVitalSignMonitor> IPDAdultVitalSignMonitorRepository => _IPDAdultVitalSignMonitorRepo;

        public IGenericRepository<IPDAdultVitalSignMonitorData> IPDAdultVitalSignMonitorDataRepository => _IPDAdultVitalSignMonitorDataRepo;
        public IGenericRepository<FormOfPatient> FormOfPatientRepository => _FormOfPatientRepo;
        public IGenericRepository<DiagnosticReporting> DiagnosticReportingRepository => _DiagnosticReportingRepo;

        public void Commit(string request_id = "")
        {
            context.SaveChanges();
        }
        /// <summary>
        /// Execute DataTable
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="param">params</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string procedureName, Dictionary<string, object> param = null)
        {
            DataSet retVal = new DataSet();
            string strCmd = $"[dbo].[{procedureName}]";
            SqlConnection sqlConn = (SqlConnection)context.Database.Connection;
            SqlCommand cmd = new SqlCommand(strCmd, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            using (cmd)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    foreach (var key in param.Keys)
                    {
                        cmd.Parameters.Add(new SqlParameter(key, param[key]));
                    }
                }
                da.Fill(retVal);
            }
            return retVal?.Tables[0];
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="param">params</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string procedureName, Dictionary<string, object> param = null)
        {
            DataSet retVal = new DataSet();
            string strCmd = $"[dbo].[{procedureName}]";
            SqlConnection sqlConn = (SqlConnection)context.Database.Connection;
            SqlCommand cmd = new SqlCommand(strCmd, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            using (cmd)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    foreach (var key in param.Keys)
                    {
                        cmd.Parameters.Add(new SqlParameter(key, param[key]));
                    }
                }
                da.Fill(retVal);
            }
            return retVal;
        }
    }
}
