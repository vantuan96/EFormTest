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
using System;

namespace DataAccess.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        #region General
        IGenericRepository<Models.Action> ActionRepository { get; }
        IGenericRepository<AdminRole> AdminRoleRepository { get; }
        IGenericRepository<Clinic> ClinicRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<EDStatus> EDStatusRepository { get; }
        IGenericRepository<Form> FormRepository { get; }
        IGenericRepository<HumanResourceAssessment> HumanResourceAssessmentRepository { get; }
        IGenericRepository<HumanResourceAssessmentPosition> HumanResourceAssessmentPositionRepository { get; }
        IGenericRepository<HumanResourceAssessmentShift> HumanResourceAssessmentShiftRepository { get; }
        IGenericRepository<HumanResourceAssessmentStaff> HumanResourceAssessmentStaffRepository { get; }
        IGenericRepository<ICD10> ICD10Repository { get; }
        IGenericRepository<ICDSpecialty> ICDSpecialtyRepository { get; }
        IGenericRepository<Log> LogRepository { get; }
        IGenericRepository<LogTmp> LogTmpRepository { get; }
        IGenericRepository<LogInFail> LogInFailRepository { get; }
        IGenericRepository<MasterData> MasterDataRepository { get; }
        IGenericRepository<MedicationMasterdata> MedicationMasterdataRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<Position> PositionRepository { get; }
        IGenericRepository<PositionUser> PositionUserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<RoleAction> RoleActionRepository { get; }
        IGenericRepository<RoleSpecialty> RoleSpecialtyRepository { get; }
        IGenericRepository<Room> RoomRepository { get; }
        IGenericRepository<Site> SiteRepository { get; }
        IGenericRepository<Specialty> SpecialtyRepository { get; }
        IGenericRepository<StandingOrderMasterData> StandingOrderMasterDataRepository { get; }
        IGenericRepository<SystemConfig> SystemConfigRepository { get; }
        IGenericRepository<SystemNotification> SystemNotificationRepository { get; }
        IGenericRepository<UnlockFormToUpdate> UnlockFormToUpdateRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<UserAdminRole> UserAdminRoleRepository { get; }
        IGenericRepository<UserClinic> UserClinicRepository { get; }
        IGenericRepository<UserRole> UserRoleRepository { get; }
        IGenericRepository<UserSpecialty> UserSpecialtyRepository { get; }
        IGenericRepository<VisitTypeGroup> VisitTypeGroupRepository { get; }
        IGenericRepository<Service> ServiceRepository { get; }
        IGenericRepository<ServiceGroup> ServiceGroupRepository { get; }
        IGenericRepository<CpoeOrderable> CpoeOrderableRepository { get; }
        IGenericRepository<LabOrderableRef> LabOrderableRefRepository { get; }
        IGenericRepository<ChargeItem> ChargeItemRepository { get; }
        IGenericRepository<ChargeItemPathology> ChargeItemPathologyRepository { get; }
        IGenericRepository<ChargeItemMicrobiology> ChargeItemMicrobiologyRepository { get; }
        IGenericRepository<Charge> ChargeRepository { get; }
        IGenericRepository<ChargeDraft> ChargeDraftRepository { get; }
        IGenericRepository<ChargeVisit> ChargeVistRepository { get; }
        IGenericRepository<ChargePackage> ChargePackageRepository { get; }
        IGenericRepository<ChargePackageService> ChargePackageServiceRepository { get; }
        IGenericRepository<ChargePackageUser> ChargePackageUserRepository { get; }
        IGenericRepository<RadiologyProcedurePlanRef> RadiologyProcedurePlanRefRepository { get; }
        IGenericRepository<TableData> TableDataRepository { get; }
        IGenericRepository<DietCode> DietCodeRepository { get; }
        IGenericRepository<MedicationAdministrationRecordModel> MedicationAdministrationRecordRepository { get; }
        IGenericRepository<ICD10Visit> ICD10VisitRepository { get; }
        IGenericRepository<StillBirth> StillBirthRepository { get; }
        IGenericRepository<SendMailNotification> SendMailNotificationRepository { get; }
        IGenericRepository<PROMForCoronaryDisease> PROMForCoronaryDiseaseRepository { get; }
        IGenericRepository<PROMForheartFailure> PROMForheartFailureRepository { get; }
        IGenericRepository<UploadImage> UploadImageRepository { get; }
        IGenericRepository<AppVersion> AppVersionRepository { get; }
        #endregion

        #region ED
        IGenericRepository<ED> EDRepository { get; }
        IGenericRepository<EDAmbulanceRunReport> EDAmbulanceRunReportRepository { get; }
        IGenericRepository<EDVerbalOrder> EDVerbalOrderRepository { get; }
        IGenericRepository<EDAmbulanceRunReportData> EDAmbulanceRunReportDataRepository { get; }
        IGenericRepository<EDAmbulanceTransferPatientsRecord> EDAmbulanceTransferPatientsRecordRepository { get; }
        IGenericRepository<EDArterialBloodGasTest> EDArterialBloodGasTestRepository { get; }
        IGenericRepository<EDArterialBloodGasTestData> EDArterialBloodGasTestDataRepository { get; }
        IGenericRepository<EDChemicalBiologyTest> EDChemicalBiologyTestRepository { get; }
        IGenericRepository<EDChemicalBiologyTestData> EDChemicalBiologyTestDataRepository { get; }
        IGenericRepository<EDConsultationDrugWithAnAsteriskMark> EDConsultationDrugWithAnAsteriskMarkRepository { get; }
        IGenericRepository<EDDischargeInformation> DischargeInformationRepository { get; }
        IGenericRepository<EDDischargeInformationData> DischargeInformationDataRepository { get; }
        IGenericRepository<EDEmergencyRecord> EmergencyRecordRepository { get; }
        IGenericRepository<EDEmergencyRecordData> EmergencyRecordDataRepository { get; }
        IGenericRepository<EDEmergencyTriageRecord> EmergencyTriageRecordRepository { get; }
        IGenericRepository<EDEmergencyTriageRecordData> EmergencyTriageRecordDataRepository { get; }
        IGenericRepository<EDHandOverCheckList> HandOverCheckListRepository { get; }
        IGenericRepository<EDHandOverCheckListData> HandOverCheckListDataRepository { get; }
        IGenericRepository<EDInjuryCertificate> EDInjuryCertificateRepository { get; }
        IGenericRepository<EDMonitoringChartAndHandoverForm> MonitoringChartAndHandoverFormRepository { get; }
        IGenericRepository<EDMonitoringChartAndHandoverFormData> MonitoringChartAndHandoverFormDataRepository { get; }
        IGenericRepository<EDObservationChart> EDObservationChartRepository { get; }
        IGenericRepository<EDObservationChartData> EDObservationChartDataRepository { get; }
        IGenericRepository<EDPatientProgressNote> PatientProgressNoteRepository { get; }
        IGenericRepository<EDPatientProgressNoteData> PatientProgressNoteDataRepository { get; }
        IGenericRepository<EDPointOfCareTestingMasterData> EDPointOfCareTestingMasterDataRepository { get; }
        IGenericRepository<EDSelfHarmRiskScreeningTool> EDSelfHarmRiskScreeningToolRepository { get; }
        IGenericRepository<EDFallRickScreennings> EDFallRickScreenningRepository { get; }

        #endregion

        #region OPD
        IGenericRepository<OPD> OPDRepository { get; }
        IGenericRepository<OPDFallRiskScreening> OPDFallRiskScreeningRepository { get; }
        IGenericRepository<OPDFallRiskScreeningData> OPDFallRiskScreeningDataRepository { get; }
        IGenericRepository<OPDHandOverCheckList> OPDHandOverCheckListRepository { get; }
        IGenericRepository<OPDHandOverCheckListData> OPDHandOverCheckListDataRepository { get; }
        IGenericRepository<OPDInitialAssessmentForOnGoing> OPDInitialAssessmentForOnGoingRepository { get; }
        IGenericRepository<OPDInitialAssessmentForOnGoingData> OPDInitialAssessmentForOnGoingDataRepository { get; }
        IGenericRepository<OPDInitialAssessmentForShortTerm> OPDInitialAssessmentForShortTermRepository { get; }
        IGenericRepository<OPDInitialAssessmentForShortTermData> OPDInitialAssessmentForShortTermDataRepository { get; }
        IGenericRepository<OPDInitialAssessmentForTelehealth> OPDInitialAssessmentForTelehealthRepository { get; }
        IGenericRepository<OPDInitialAssessmentForTelehealthData> OPDInitialAssessmentForTelehealthDataRepository { get; }
        IGenericRepository<OPDObservationChart> OPDObservationChartRepository { get; }
        IGenericRepository<OPDObservationChartData> OPDObservationChartDataRepository { get; }
        IGenericRepository<OPDOutpatientExaminationNote> OPDOutpatientExaminationNoteRepository { get; }
        IGenericRepository<OPDOutpatientExaminationNoteData> OPDOutpatientExaminationNoteDataRepository { get; }
        IGenericRepository<OPDPatientProgressNote> OPDPatientProgressNoteRepository { get; }
        IGenericRepository<OPDPatientProgressNoteData> OPDPatientProgressNoteDataRepository { get; }
        IGenericRepository<OPDNCCNBROV1> OPDNCCNBROV1Repository { get; }
        IGenericRepository<OPDRiskAssessmentForCancer> OPDRiskAssessmentForCancerRepository { get; }
        IGenericRepository<OPDClinicalBreastExamNote> OPDClinicalBreastExamNoteRepository { get; }
        IGenericRepository<OPDGENBRCA> OPDGENBRCARepository { get; }
        IGenericRepository<OPDPreAnesthesiaHandOverCheckList> OPDPreAnesthesiaHandOverCheckListRepository { get; }
        #endregion

        #region IPD
        IGenericRepository<IPD> IPDRepository { get; }
        IGenericRepository<IPDDischargeMedicalReport> IPDDischargeMedicalReportRepository { get; }
        IGenericRepository<IPDFallRiskAssessmentForAdult> IPDFallRiskAssessmentForAdultRepository { get; }
        IGenericRepository<IPDFallRiskAssessmentForAdultData> IPDFallRiskAssessmentForAdultDataRepository { get; }
        IGenericRepository<IPDFallRiskAssessmentForObstetric> IPDFallRiskAssessmentForObstetricRepository { get; }
        IGenericRepository<IPDFallRiskAssessmentForObstetricData> IPDFallRiskAssessmentForObstetricDataRepository { get; }
        IGenericRepository<IPDGuggingSwallowingScreen> IPDGuggingSwallowingScreenRepository { get; }
        IGenericRepository<IPDGuggingSwallowingScreenData> IPDGuggingSwallowingScreenDataRepository { get; }
        IGenericRepository<IPDHandOverCheckList> IPDHandOverCheckListRepository { get; }
        IGenericRepository<IPDHandOverCheckListData> IPDHandOverCheckListDataRepository { get; }
        IGenericRepository<IPDInitialAssessmentForAdult> IPDInitialAssessmentForAdultRepository { get; }
        IGenericRepository<IPDInitialAssessmentForAdultData> IPDInitialAssessmentForAdultDataRepository { get; }
        IGenericRepository<IPDInitialAssessmentForChemotherapy> IPDInitialAssessmentForChemotherapyRepository { get; }
        IGenericRepository<IPDInitialAssessmentForChemotherapyData> IPDInitialAssessmentForChemotherapyDataRepository { get; }
        IGenericRepository<IPDInitialAssessmentForFrailElderly> IPDInitialAssessmentForFrailElderlyRepository { get; }
        IGenericRepository<IPDInitialAssessmentForFrailElderlyData> IPDInitialAssessmentForFrailElderlyDataRepository { get; }
        IGenericRepository<IPDInitialAssessmentSpecialRequest> IPDInitialAssessmentSpecialRequestRepository { get; }
        IGenericRepository<IPDConsultationDrugWithAnAsteriskMark> IPDConsultationDrugWithAnAsteriskMarkRepository { get; }
        IGenericRepository<IPDMedicalRecord> IPDMedicalRecordRepository { get; }
        IGenericRepository<IPDMedicalRecordData> IPDMedicalRecordDataRepository { get; }
        IGenericRepository<IPDMedicalRecordPart1> IPDMedicalRecordPart1Repository { get; }
        IGenericRepository<IPDMedicalRecordPart1Data> IPDMedicalRecordPart1DataRepository { get; }
        IGenericRepository<IPDMedicalRecordPart2> IPDMedicalRecordPart2Repository { get; }
        IGenericRepository<IPDMedicalRecordPart2Data> IPDMedicalRecordPart2DataRepository { get; }
        IGenericRepository<IPDMedicalRecordPart3> IPDMedicalRecordPart3Repository { get; }
        IGenericRepository<IPDMedicalRecordPart3Data> IPDMedicalRecordPart3DataRepository { get; }
        IGenericRepository<IPDPatientProgressNote> IPDPatientProgressNoteRepository { get; }
        IGenericRepository<IPDPatientProgressNoteData> IPDPatientProgressNoteDataRepository { get; }
        IGenericRepository<IPDPlanOfCare> IPDPlanOfCareRepository { get; }
        IGenericRepository<IPDReferralLetter> IPDReferralLetterRepository { get; }
        IGenericRepository<IPDTransferLetter> IPDTransferLetterRepository { get; }
        IGenericRepository<IPDTakeCareOfPatientsWithCovid19Recomment> IPDTakeCareOfPatientsWithCovid19RecommentRepository { get; }
        IGenericRepository<IPDTakeCareOfPatientsWithCovid19Assessment> IPDTakeCareOfPatientsWithCovid19AssessmentRepository { get; }
        IGenericRepository<IPDDischargePreparationChecklist> IPDDischargePreparationChecklistRepository { get; }
        IGenericRepository<IPDDischargePreparationChecklistData> IPDDischargePreparationChecklistDataRepository { get; }
        IGenericRepository<IPDAdultVitalSignMonitor> IPDAdultVitalSignMonitorRepository { get; }
        IGenericRepository<IPDAdultVitalSignMonitorData> IPDAdultVitalSignMonitorDataRepository { get; }
        IGenericRepository<IPDConfirmDischarge> IPDConfirmDischargeWithoutDirectRepository { get; }
        IGenericRepository<VitalSignForAdult> IPDVitalSignForAdultRespository { get; }
        IGenericRepository<IPDInjuryCertificate> IPDInjuryCertificateRepository { get; }
        IGenericRepository<BradenScale> IPDBradenScaleRepository { get; }
        IGenericRepository<IPDSurgeryCertificate> IPDSurgeryCertificateRepository { get; }
        IGenericRepository<IPDSurgeryCertificateData> IPDSurgeryCertificateDataRepository { get;}
        IGenericRepository<IPDSummaryOf15DayTreatment> IPDSummayOf15DayTreatmentRepository { get; }
        IGenericRepository<IPDMedicationHistory> IPDMedicationHistoryRepository { get; }
        IGenericRepository<IPDSetupMedicalRecord> IPDSetupMedicalRecordRepository { get; }
        IGenericRepository<IPDMedicalRecordOfPatients> IPDMedicalRecordOfPatientRepository { get; }
        IGenericRepository<IPDInitialAssessmentForNewborns> IPDInitialAssessmentForNewbornsRepository { get; }
        IGenericRepository<IPDThrombosisRiskFactorAssessment> IPDThrombosisRiskFactorAssessmentRepository { get; }
        IGenericRepository<IPDThrombosisRiskFactorAssessmentData> IPDThrombosisRiskFactorAssessmentDataRepository { get; }
        IGenericRepository<IPDSibling> IPDSiblingRepository { get; }
        IGenericRepository<IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs> IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugsRepository { get; }
        IGenericRepository<VitalSignForPregnantWoman> IPDVitalSignForPregnantWomanRepository { get; }
        IGenericRepository<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient> IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatientRepository { get; }
        IGenericRepository<IPDCoronaryIntervention> IPDCoronaryInterventionRepository { get; }
        IGenericRepository<IPDPainRecord> IPDPainRecordRepository { get; }
        IGenericRepository<IPDGlamorgan> IPDGlamorganRepository { get; }
        IGenericRepository<IPDGlamorganData> IPDGlamorganDataRepository { get; }
        IGenericRepository<IPDNeonatalObservationChart> IPDNeonatalObservationChartRepository { get; }
        IGenericRepository<IPDVitalSignForPediatrics> IPDVitalSignForPediatricsReponsitory { get; }
        IGenericRepository<IPDMedicalRecordExtenstion> IPDMedicalRecordExtenstionReponsitory { get; }
        IGenericRepository<IPDScaleForAssessmentOfSuicideIntent> IPDScaleForAssessmentOfSuicideIntentReponsitory { get; }
        #endregion

        #region EIO
        IGenericRepository<HighlyRestrictedAntimicrobialConsult> HighlyRestrictedAntimicrobialConsultRepository { get; }
        IGenericRepository<HighlyRestrictedAntimicrobialConsultMicrobiologicalResults> HighlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepository { get; }
        IGenericRepository<HighlyRestrictedAntimicrobialConsultAntimicrobialOrder> HighlyRestrictedAntimicrobialConsultAntimicrobialOrderRepository { get; }
        IGenericRepository<EIOTestCovid2Confirmation> EIOTestCovid2ConfirmationRepository { get; }
        IGenericRepository<ComplexOutpatientCaseSummary> ComplexOutpatientCaseSummaryRepository { get; }
        IGenericRepository<EIOAssessmentForRetailServicePatient> EIOAssessmentForRetailServicePatientRepository { get; }
        IGenericRepository<EIOAssessmentForRetailServicePatientData> EIOAssessmentForRetailServicePatientDataRepository { get; }
        IGenericRepository<EIOBloodProductData> EIOBloodProductDataRepository { get; }
        IGenericRepository<EIOBloodRequestSupplyAndConfirmation> EIOBloodRequestSupplyAndConfirmationRepository { get; }
        IGenericRepository<EIOBloodSupplyData> EIOBloodSupplyDataRepository { get; }
        IGenericRepository<EIOBloodTransfusionChecklist> EIOBloodTransfusionChecklistRepository { get; }
        IGenericRepository<EIOBloodTransfusionChecklistData> EIOBloodTransfusionChecklistDataRepository { get; }
        IGenericRepository<EIOCardiacArrestRecord> EIOCardiacArrestRecordRepository { get; }
        IGenericRepository<EIOCardiacArrestRecordData> EIOCardiacArrestRecordDataRepository { get; }
        IGenericRepository<EIOCardiacArrestRecordTable> EIOCardiacArrestRecordTableRepository { get; }
        IGenericRepository<EIOExternalTransportationAssessment> EIOExternalTransportationAssessmentRepository { get; }
        IGenericRepository<EIOExternalTransportationAssessmentData> EIOExternalTransportationAssessmentDataRepository { get; }
        IGenericRepository<EIOExternalTransportationAssessmentEquipment> EIOExternalTransportationAssessmentEquipmentRepository { get; }
        IGenericRepository<EIOJointConsultationForApprovalOfSurgery> EIOJointConsultationForApprovalOfSurgeryRepository { get; }
        IGenericRepository<EIOJointConsultationForApprovalOfSurgeryData> EIOJointConsultationForApprovalOfSurgeryDataRepository { get; }
        IGenericRepository<EIOJointConsultationGroupMinutes> EIOJointConsultationGroupMinutesRepository { get; }
        IGenericRepository<EIOJointConsultationGroupMinutesData> EIOJointConsultationGroupMinutesDataRepository { get; }
        IGenericRepository<EIOJointConsultationGroupMinutesMember> EIOJointConsultationGroupMinutesMemberRepository { get; }
        IGenericRepository<EIOMortalityReport> EIOMortalityReportRepository { get; }
        IGenericRepository<EIOMortalityReportMember> EIOMortalityReportMemberRepository { get; }
        IGenericRepository<EIOPatientOwnMedicationsChart> EIOPatientOwnMedicationsChartRepository { get; }
        IGenericRepository<EIOPatientOwnMedicationsChartData> EIOPatientOwnMedicationsChartDataRepository { get; }
        IGenericRepository<EIOPreOperativeProcedureHandoverChecklist> EIOPreOperativeProcedureHandoverChecklistRepository { get; }
        IGenericRepository<EIOPreOperativeProcedureHandoverChecklistData> EIOPreOperativeProcedureHandoverChecklistDataRepository { get; }
        IGenericRepository<EIOProcedureSummary> EIOProcedureSummaryRepository { get; }
        IGenericRepository<ProcedureSummaryV2> ProcedureSummaryV2Repository { get; }
        IGenericRepository<EIOProcedureSummaryData> EIOProcedureSummaryDataRepository { get; }
        IGenericRepository<EIOSkinTestResult> EIOSkinTestResultRepository { get; }
        IGenericRepository<EIOSkinTestResultData> EIOSkinTestResultDataRepository { get; }
        IGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheet> EIOSpongeSharpsAndInstrumentsCountsSheetRepository { get; }
        IGenericRepository<EIOSpongeSharpsAndInstrumentsCountsSheetData> EIOSpongeSharpsAndInstrumentsCountsSheetDataRepository { get; }
        IGenericRepository<EIOStandingOrderForRetailService> EIOStandingOrderForRetailServiceRepository { get; }
        IGenericRepository<EIOSurgicalProcedureSafetyChecklist> EIOSurgicalProcedureSafetyChecklistRepository { get; }
        IGenericRepository<EIOSurgicalProcedureSafetyChecklistData> EIOSurgicalProcedureSafetyChecklistDataRepository { get; }
        IGenericRepository<EIOSurgicalProcedureSafetyChecklistSignIn> EIOSurgicalProcedureSafetyChecklistSignInRepository { get; }
        IGenericRepository<EIOSurgicalProcedureSafetyChecklistSignOut> EIOSurgicalProcedureSafetyChecklistSignOutRepository { get; }
        IGenericRepository<EIOSurgicalProcedureSafetyChecklistTimeOut> EIOSurgicalProcedureSafetyChecklistTimeOutRepository { get; }
        IGenericRepository<PatientAndFamilyEducation> PatientAndFamilyEducationRepository { get; }
        IGenericRepository<PatientAndFamilyEducationContent> PatientAndFamilyEducationContentRepository { get; }
        IGenericRepository<PatientAndFamilyEducationContentData> PatientAndFamilyEducationContentDataRepository { get; }
        IGenericRepository<PatientAndFamilyEducationData> PatientAndFamilyEducationDataRepository { get; }
        IGenericRepository<Translation> TranslationRepository { get; }
        IGenericRepository<TranslationData> TranslationDataRepository { get; }
        IGenericRepository<EIOPhysicianNote> EIOPhysicianNoteRepository { get; }
        IGenericRepository<EIOCareNote> EIOCareNoteRepository { get; }
        IGenericRepository<PreAnesthesiaConsultation> PreAnesthesiaConsultationRepository { get; }
        IGenericRepository<SurgeryAndProcedureSummaryV3> SurgeryAndProcedureSummaryV3Repository { get; }
        IGenericRepository<EIOConstraintNewbornAndPregnantWoman> EIOConstraintNewbornAndPregnantWomanRepository { get; }
        #endregion

        #region EOC
        IGenericRepository<EOC> EOCRepository { get; }
        IGenericRepository<EOCTransfer> EOCTransferRepository { get; }
        IGenericRepository<EOCFallRiskScreening> EOCFallRiskScreeningRepository { get; }
        IGenericRepository<EOCInitialAssessmentForOnGoing> EOCInitialAssessmentForOnGoingRepository { get; }
        IGenericRepository<EOCInitialAssessmentForShortTerm> EOCInitialAssessmentForShortTermRepository { get; }
        IGenericRepository<EOCOutpatientExaminationNote> EOCOutpatientExaminationNoteRepository { get; }
        IGenericRepository<EOCHandOverCheckList> EOCHandOverCheckListRepository { get; }
        #endregion

        #region Prescription
        IGenericRepository<PrescriptionNoteModel> PrescriptionNoteRepository { get; }
        IGenericRepository<PresciptionRoundInfoModel> PrescriptionRoundInfoRepository { get; }
        #endregion
        IGenericRepository<FormDatas> FormDatasRepository { get; }
        IGenericRepository<EIOForm> EIOFormRepository { get; }
        IGenericRepository<EIOFormConfirm> EIOFormConfirmRepository { get; }
        IGenericRepository<UnlockVip> UnlockVipRepository { get; }
        IGenericRepository<AppConfig> AppConfigRepository { get; }
        IGenericRepository<DiagnosticReporting> DiagnosticReportingRepository { get; }
        IGenericRepository<FormOfPatient> FormOfPatientRepository { get; }
        void Commit(string request_id = "");

    }
}
