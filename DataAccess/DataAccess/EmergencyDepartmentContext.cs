using DataAccess.Migrations;
using DataAccess.Models;
using DataAccess.Models.EDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Models.EIOModel;
using System.Data.Entity;
using DataAccess.Models.IPDModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.PrescriptionModel;
using DataAccess.Models.FormModel;
using DataAccess.Models.MedicationAdministrationRecordModel;
using System;

namespace DataAccess
{
    public class EmergencyDepartmentContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's
        // configuration file (App.config or Web.config). By default, this connection string targets the
        // 'DataAccess.Model1' database on your LocalDb instance.
        //
        // If you wish to target a different database and/or database provider, modify the 'Model1'
        // connection string in the application configuration file.
        public EmergencyDepartmentContext()
            : base("EmergencyDepartmentContext")
        {
            Database.CommandTimeout = 240;
            // Database.Log = s => EmergencyDepartmentContextLogger.Log("EFApp", s);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmergencyDepartmentContext, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        #region General
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EDStatus> EDStatus { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<HumanResourceAssessment> HumanResourceAssessments { get; set; }
        public DbSet<HumanResourceAssessmentPosition> HumanResourceAssessmentPositions { get; set; }
        public DbSet<HumanResourceAssessmentShift> HumanResourceAssessmentShifts { get; set; }
        public DbSet<HumanResourceAssessmentStaff> HumanResourceAssessmentStaffs { get; set; }
        public DbSet<ICD10> ICD10s { get; set; }
        public DbSet<ICDSpecialty> ICDSpecialties { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogTmp> LogTmps { get; set; }
        public DbSet<LogInFail> LogInFails { get; set; }
        public DbSet<MasterData> MasterDatas { get; set; }
        public DbSet<MedicationMasterdata> MedicationMasterdatas { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionUser> PositionUsers { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleAction> RoleAction { get; set; }
        public DbSet<RoleSpecialty> RoleSpecialties { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<StandingOrderMasterData> StandingOrderMasterDatas { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<SystemNotification> SystemNotifications { get; set; }
        public DbSet<UnlockFormToUpdate> UnlockFormToUpdates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAdminRole> UserAdminRoles { get; set; }
        public DbSet<UserClinic> UserClinics { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSpecialty> UserSpecialties { get; set; }
        public DbSet<VisitTypeGroup> VisitTypeGroups { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceGroup> ServiceGroups { get; set; }
        public DbSet<CpoeOrderable> CpoeOrderables { get; set; }
        public DbSet<LabOrderableRef> LabOrderableRefs { get; set; }
        public DbSet<ChargeVisit> ChargeVisit { get; set; }
        public DbSet<Charge> Charge { get; set; }

        public DbSet<ChargeItem> ChargeItems { get; set; }
        public DbSet<ChargeItemPathology> ChargeItemPathology { get; set; }
        public DbSet<ChargeItemMicrobiology> ChargeItemMicrobiology { get; set; }
        public DbSet<RadiologyProcedurePlanRef> RadiologyProcedurePlanRef { get; set; }
        public DbSet<ChargePackage> ChargePackage { get; set; }
        public DbSet<ChargePackageService> ChargePackageService { get; set; }
        public DbSet<ChargePackageUser> ChargePackageUser { get; set; }

        public DbSet<ChargeDraft> ChargeDraft { get; set; }
        public DbSet<DietCode> DietCode { get; set; }
        public DbSet<MedicationAdministrationRecordModel> MedicationAdministrationRecord { get; set; }
        public DbSet<StillBirth> StillBirths { get; set; }
        public DbSet<SendMailNotification> SendMailNotifications { get; set; }
        public DbSet<PROMForCoronaryDisease> PROMForCoronaryDiseases { get; set; }
        public DbSet<PROMForheartFailure> PROMForheartFailures { get; set; }
        public DbSet<UploadImage> UploadImages { get; set; }
        public DbSet<AppVersion> AppVersions { get; set; }
        #endregion

        #region ED
        public DbSet<ED> EDs { get; set; }
        public DbSet<EDSelfHarmRiskScreeningTool> EDSelfHarmRiskScreeningTools { get; set; }
        public DbSet<EDAmbulanceRunReport> EDAmbulanceRunReports { get; set; }
        public DbSet<EDVerbalOrder> EDVerbalOrders { get; set; }
        public DbSet<EDAmbulanceRunReportData> EDAmbulanceRunReportDatas { get; set; }
        public DbSet<EDAmbulanceTransferPatientsRecord> EDAmbulanceTransferPatientsRecords { get; set; }
        public DbSet<EDArterialBloodGasTest> EDArterialBloodGasTests { get; set; }
        public DbSet<EDArterialBloodGasTestData> EDArterialBloodGasTestDatas { get; set; }
        public DbSet<EDChemicalBiologyTest> EDChemicalBiologyTests { get; set; }
        public DbSet<EDChemicalBiologyTestData> EDChemicalBiologyTestDatas { get; set; }
        public DbSet<EDConsultationDrugWithAnAsteriskMark> EDConsultationDrugWithAnAsteriskMarks { get; set; }
        public DbSet<EDDischargeInformation> EDDischargeInformations { get; set; }
        public DbSet<EDDischargeInformationData> EDDischargeInformationDatas { get; set; }
        public DbSet<EDEmergencyRecord> EDEmergencyRecords { get; set; }
        public DbSet<EDEmergencyRecordData> EDEmergencyRecordDatas { get; set; }
        public DbSet<EDEmergencyTriageRecord> EDEmergencyTriageRecords { get; set; }
        public DbSet<EDEmergencyTriageRecordData> EDEmergencyTriageRecordDatas { get; set; }
        public DbSet<EDHandOverCheckList> EDHandOverCheckLists { get; set; }
        public DbSet<EDHandOverCheckListData> EDHandOverCheckListDatas { get; set; }
        public DbSet<EDInjuryCertificate> EDInjuryCertificates { get; set; }
        public DbSet<EDMonitoringChartAndHandoverForm> EDMonitoringChartAndHandoverForms { get; set; }
        public DbSet<EDMonitoringChartAndHandoverFormData> EDMonitoringChartAndHandoverFormDatas { get; set; }
        public DbSet<EDObservationChart> EDObservationCharts { get; set; }
        public DbSet<EDObservationChartData> EDObservationChartDatas { get; set; }
        public DbSet<EDPatientProgressNote> EDPatientProgressNotes { get; set; }
        public DbSet<EDPatientProgressNoteData> EDPatientProgressNoteDatas { get; set; }
        public DbSet<EDPointOfCareTestingMasterData> EDPointOfCareTestingMasterDatas { get; set; }
        public DbSet<EDFallRickScreennings> EDFallRickScreennings { get; set; }

        #endregion

        #region OPD
        public DbSet<OPD> OPDs { get; set; }
        public DbSet<OPDFallRiskScreening> OPDFallRiskScreenings { get; set; }
        public DbSet<OPDFallRiskScreeningData> OPDFallRiskScreeningDatas { get; set; }
        public DbSet<OPDHandOverCheckList> OPDHandOverCheckLists { get; set; }
        public DbSet<OPDHandOverCheckListData> OPDHandOverCheckListDatas { get; set; }
        public DbSet<OPDInitialAssessmentForOnGoing> OPDInitialAssessmentForOnGoings { get; set; }
        public DbSet<OPDInitialAssessmentForOnGoingData> OPDInitialAssessmentForOnGoingDatas { get; set; }
        public DbSet<OPDInitialAssessmentForShortTerm> OPDInitialAssessmentForShortTerms { get; set; }
        public DbSet<OPDInitialAssessmentForShortTermData> OPDInitialAssessmentForShortTermDatas { get; set; }
        public DbSet<OPDInitialAssessmentForTelehealth> OPDInitialAssessmentForTelehealths { get; set; }
        public DbSet<OPDInitialAssessmentForTelehealthData> OPDInitialAssessmentForTelehealthDatas { get; set; }
        public DbSet<OPDObservationChart> OPDObservationCharts { get; set; }
        public DbSet<OPDObservationChartData> OPDObservationChartDatas { get; set; }
        public DbSet<OPDOutpatientExaminationNote> OPDOutpatientExaminationNotes { get; set; }
        public DbSet<OPDOutpatientExaminationNoteData> OPDOutpatientExaminationNoteDatas { get; set; }
        public DbSet<OPDPatientProgressNote> OPDPatientProgressNotes { get; set; }
        public DbSet<OPDPatientProgressNoteData> OPDPatientProgressNoteDatas { get; set; }
        public DbSet<OPDNCCNBROV1> OPDNCCNBROV1s { get; set; }
        public DbSet<OPDRiskAssessmentForCancer> OPDRiskAssessmentForCancers { get; set; }
        public DbSet<OPDClinicalBreastExamNote> OPDClinicalBreastExamNotes { get; set; }
        public DbSet<OPDGENBRCA> OPDGENBRCAs { get; set; }
        public DbSet<OPDPreAnesthesiaHandOverCheckList> OPDPreAnesthesiaHandOverCheckLists { get; set; }
        #endregion

        #region IPD
        public DbSet<IPD> IPDs { get; set; }
        public DbSet<IPDDischargeMedicalReport> IPDDischargeMedicalReports { get; set; }
        public DbSet<IPDFallRiskAssessmentForAdult> IPDFallRiskAssessmentForAdults { get; set; }
        public DbSet<IPDFallRiskAssessmentForAdultData> IPDFallRiskAssessmentForAdultDatas { get; set; }
        public DbSet<IPDFallRiskAssessmentForObstetric> IPDFallRiskAssessmentForObstetrics { get; set; }
        public DbSet<IPDFallRiskAssessmentForObstetricData> IPDFallRiskAssessmentForObstetricDatas { get; set; }
        public DbSet<IPDGuggingSwallowingScreen> IPDGuggingSwallowingScreens { get; set; }
        public DbSet<IPDGuggingSwallowingScreenData> IPDGuggingSwallowingScreenDatas { get; set; }
        public DbSet<IPDHandOverCheckList> IPDHandOverCheckLists { get; set; }
        public DbSet<IPDHandOverCheckListData> IPDHandOverCheckListDatas { get; set; }
        public DbSet<IPDInitialAssessmentForAdult> IPDInitialAssessmentForAdults { get; set; }
        public DbSet<IPDInitialAssessmentForAdultData> IPDInitialAssessmentForAdultDatas { get; set; }
        public DbSet<IPDInitialAssessmentForChemotherapy> IPDInitialAssessmentForChemotherapys { get; set; }
        public DbSet<IPDInitialAssessmentForChemotherapyData> IPDInitialAssessmentForChemotherapyDatas { get; set; }
        public DbSet<IPDInitialAssessmentForFrailElderly> IPDInitialAssessmentForFrailElderlys { get; set; }
        public DbSet<IPDInitialAssessmentForFrailElderlyData> IPDInitialAssessmentForFrailElderlyDatas { get; set; }
        public DbSet<IPDInitialAssessmentSpecialRequest> IPDInitialAssessmentSpecialRequests { get; set; }
        public DbSet<IPDMedicalRecord> IPDMedicalRecords { get; set; }
        public DbSet<IPDMedicalRecordData> IPDMedicalRecordDatas { get; set; }
        public DbSet<IPDMedicalRecordPart1> IPDMedicalRecordPart1s { get; set; }
        public DbSet<IPDMedicalRecordPart1Data> IPDMedicalRecordPart1Datas { get; set; }
        public DbSet<IPDMedicalRecordPart2> IPDMedicalRecordPart2s { get; set; }
        public DbSet<IPDMedicalRecordPart2Data> IPDMedicalRecordPart2Datas { get; set; }
        public DbSet<IPDMedicalRecordPart3> IPDMedicalRecordPart3s { get; set; }
        public DbSet<IPDMedicalRecordPart3Data> IPDMedicalRecordPart3Datas { get; set; }
        public DbSet<IPDPatientProgressNote> IPDPatientProgressNotes { get; set; }
        public DbSet<IPDPatientProgressNoteData> IPDPatientProgressNoteDatas { get; set; }
        public DbSet<IPDPlanOfCare> IPDPlanOfCares { get; set; }
        public DbSet<IPDReferralLetter> IPDReferralLetters { get; set; }
        public DbSet<IPDTransferLetter> IPDTransferLetters { get; set; }
        public DbSet<IPDTakeCareOfPatientsWithCovid19Recomment> IPDTakeCareOfPatientsWithCovid19Recomment { get; set; }
        public DbSet<IPDTakeCareOfPatientsWithCovid19Assessment> IPDTakeCareOfPatientsWithCovid19Assessment { get; set; }
        public DbSet<IPDConsultationDrugWithAnAsteriskMark> IPDConsultationDrugWithAnAsteriskMarks { get; set; }
        public DbSet<IPDDischargePreparationChecklist> IPDDischargePreparationChecklists { get; set; }
        public DbSet<IPDDischargePreparationChecklistData> IPDDischargePreparationChecklistDatas { get; set; }
        public DbSet<IPDConfirmDischarge> IPDConfirmDischargeWithoutDirect { get; set; }
        public DbSet<VitalSignForAdult> IPDVitalSignForAdult { get; set; }
        public DbSet<IPDInjuryCertificate> IPDInjuryCertificate { get; set; }
        public DbSet<BradenScale> IPDBradenScale { get; set; }
        public DbSet<IPDSurgeryCertificate> IPDSurgeryCertificate { get; set; }
        public DbSet<IPDSurgeryCertificateData> IPDSurgeryCertificateData { get; set; }
        public DbSet<IPDSummaryOf15DayTreatment> IPDSummaryOf15DayTreatment { get; set; }
        public DbSet<IPDMedicationHistory> IPDMedicationHistory { get; set; }
        public DbSet<IPDSetupMedicalRecord> IPDSetupMedicalRecord { get; set; }
        public DbSet<IPDMedicalRecordOfPatients> IPDMedicalRecordOfPatients { get; set; }
        public DbSet<IPDInitialAssessmentForNewborns> IPDInitialAssessmentForNewborns { get; set; }
        public DbSet<IPDThrombosisRiskFactorAssessment> IPDThrombosisRiskFactorAssessment { get; set; }
        public DbSet<IPDThrombosisRiskFactorAssessmentData> IPDThrombosisRiskFactorAssessmentDatas { get; set; }
        public DbSet<IPDSibling> IPDSiblings { get; set; }
        public DbSet<IPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs> IPDIPDMonitoringSheetForPatientsWithExtravasationOfCancerDrugs { get; set; }
        public DbSet<VitalSignForPregnantWoman> IPDVitalSignForPregnantWoman { get; set; }
        public DbSet<IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatient> IPDThrombosisRiskFactorAssessmentForGeneralSurgeryPatients { get; set; }
        public DbSet<IPDCoronaryIntervention> IPDCoronaryInterventions { get; set; }
        public DbSet<IPDPainRecord> IPDPainRecords { get; set; }
        public DbSet<IPDGlamorgan> IPDGlamorgans { get; set; }
        public DbSet<IPDGlamorganData> IPDGlamorganDatas { get; set; }
        public DbSet<IPDNeonatalObservationChart> IPDNeonatalObservationChartsDatas { get; set; }
        public DbSet<IPDVitalSignForPediatrics> IPDVitalSignForPediatrics { get; set; }
        public DbSet<IPDMedicalRecordExtenstion> IPDMedicalRecordExtenstions { get; set; }
        public DbSet<IPDScaleForAssessmentOfSuicideIntent> IPDScaleForAssessmentOfSuicideIntents { get; set; }
        #endregion

        #region EIO

        public DbSet<HighlyRestrictedAntimicrobialConsult> HighlyRestrictedAntimicrobialConsult { get; set; }
        public DbSet<HighlyRestrictedAntimicrobialConsultMicrobiologicalResults> HighlyRestrictedAntimicrobialConsultMicrobiologicalResults { get; set; }
        public DbSet<HighlyRestrictedAntimicrobialConsultAntimicrobialOrder> HighlyRestrictedAntimicrobialConsultAntimicrobialOrder { get; set; }
        public DbSet<EIOTestCovid2Confirmation> EIOTestCovid2Confirmation { get; set; }
        public DbSet<ComplexOutpatientCaseSummary> ComplexOutpatientCaseSummarys { get; set; }
        public DbSet<EIOAssessmentForRetailServicePatient> EIOAssessmentForRetailServicePatients { get; set; }
        public DbSet<EIOAssessmentForRetailServicePatientData> EIOAssessmentForRetailServicePatientDatas { get; set; }
        public DbSet<EIOBloodProductData> EIOBloodProductDatas { get; set; }
        public DbSet<EIOBloodRequestSupplyAndConfirmation> EIOBloodRequestSupplyAndConfirmations { get; set; }
        public DbSet<EIOBloodSupplyData> EIOBloodSupplyDatas { get; set; }
        public DbSet<EIOBloodTransfusionChecklist> EIOBloodTransfusionChecklists { get; set; }
        public DbSet<EIOBloodTransfusionChecklistData> EIOBloodTransfusionChecklistDatas { get; set; }
        public DbSet<EIOCardiacArrestRecord> EIOCardiacArrestRecords { get; set; }
        public DbSet<EIOCardiacArrestRecordData> EIOCardiacArrestRecordDatas { get; set; }
        public DbSet<EIOCardiacArrestRecordTable> EIOCardiacArrestRecordTables { get; set; }
        public DbSet<EIOExternalTransportationAssessment> EIOExternalTransportationAssessments { get; set; }
        public DbSet<EIOExternalTransportationAssessmentData> EIOExternalTransportationAssessmentDatas { get; set; }
        public DbSet<EIOExternalTransportationAssessmentEquipment> EIOExternalTransportationAssessmentEquipments { get; set; }
        public DbSet<EIOJointConsultationForApprovalOfSurgery> EIOJointConsultationForApprovalOfSurgeries { get; set; }
        public DbSet<EIOJointConsultationForApprovalOfSurgeryData> EIOJointConsultationForApprovalOfSurgeryDatas { get; set; }
        public DbSet<EIOJointConsultationGroupMinutes> EIOJointConsultationGroupMinutes { get; set; }
        public DbSet<EIOJointConsultationGroupMinutesData> EIOJointConsultationGroupMinutesDatas { get; set; }
        public DbSet<EIOJointConsultationGroupMinutesMember> EIOJointConsultationGroupMinutesMembers { get; set; }
        public DbSet<EIOMortalityReport> EIOMortalityReports { get; set; }
        public DbSet<EIOMortalityReportMember> EIOMortalityReportMembers { get; set; }
        public DbSet<EIOPatientOwnMedicationsChart> EIOPatientOwnMedicationsCharts { get; set; }
        public DbSet<EIOPatientOwnMedicationsChartData> EIOPatientOwnMedicationsChartDatas { get; set; }
        public DbSet<EIOPreOperativeProcedureHandoverChecklist> EIOPreOperativeProcedureHandoverChecklists { get; set; }
        public DbSet<EIOPreOperativeProcedureHandoverChecklistData> EIOPreOperativeProcedureHandoverChecklistDatas { get; set; }
        public DbSet<EIOProcedureSummary> EIOProcedureSummaries { get; set; }
        public DbSet<EIOProcedureSummaryData> EIOProcedureSummaryDatas { get; set; }
        public DbSet<ProcedureSummaryV2> ProcedureSummaryV2s { get; set; }
        public DbSet<EIOSkinTestResult> EIOSkinTestResults { get; set; }
        public DbSet<EIOSkinTestResultData> EIOSkinTestResultDatas { get; set; }
        public DbSet<EIOSpongeSharpsAndInstrumentsCountsSheet> EIOSpongeSharpsAndInstrumentsCountsSheets { get; set; }
        public DbSet<EIOSpongeSharpsAndInstrumentsCountsSheetData> EIOSpongeSharpsAndInstrumentsCountsSheetDatas { get; set; }
        public DbSet<EIOStandingOrderForRetailService> EIOStandingOrderForRetailServices { get; set; }
        public DbSet<EIOSurgicalProcedureSafetyChecklist> EIOSurgicalProcedureSafetyChecklist { get; set; }
        public DbSet<EIOSurgicalProcedureSafetyChecklistData> EIOSurgicalProcedureSafetyChecklistDatas { get; set; }
        public DbSet<EIOSurgicalProcedureSafetyChecklistSignIn> EIOSurgicalProcedureSafetyChecklistSignIns { get; set; }
        public DbSet<EIOSurgicalProcedureSafetyChecklistSignOut> EIOSurgicalProcedureSafetyChecklistSignOuts { get; set; }
        public DbSet<EIOSurgicalProcedureSafetyChecklistTimeOut> EIOSurgicalProcedureSafetyChecklistTimeOuts { get; set; }
        public DbSet<PatientAndFamilyEducation> PatientAndFamilyEducations { get; set; }
        public DbSet<PatientAndFamilyEducationContent> PatientAndFamilyEducationContents { get; set; }
        public DbSet<PatientAndFamilyEducationContentData> PatientAndFamilyEducationContentDatas { get; set; }
        public DbSet<PatientAndFamilyEducationData> PatientAndFamilyEducationDatas { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<TranslationData> TranslationDatas { get; set; }
        public DbSet<EIOPhysicianNote> EIOPhysicianNotes { get; set; }
        public DbSet<EIOCareNote> EIOCareNotes { get; set; }
       public DbSet<SurgeryAndProcedureSummaryV3> SurgeryAndProcedureSummaryV3s { get; set; }
       public DbSet<EIOConstraintNewbornAndPregnantWoman> EIOConstraintNewbornAndPregnantWomans { get; set; }

        #endregion

        #region EOC
        public DbSet<EOC> EOCs { get; set; }
        public DbSet<EOCTransfer> EOCTransfers { get; set; }
        public DbSet<EOCFallRiskScreening> EOCFallRiskScreening { get; set; }
        public DbSet<EOCInitialAssessmentForOnGoing> EOCInitialAssessmentForOnGoing { get; set; }
        public DbSet<EOCInitialAssessmentForShortTerm> EOCInitialAssessmentForShortTerm { get; set; }
        public DbSet<EOCOutpatientExaminationNote> EOCOutpatientExaminationNote { get; set; }
        public DbSet<EOCHandOverCheckList> EOCHandOverCheckList { get; set; }
        public DbSet<PreAnesthesiaConsultation> PreAnesthesiaConsultations { get; set; }
        #endregion
        public DbSet<EIOForm> EIOForm { get; set; }
        public DbSet<EIOFormConfirm> EIOFormConfirm { get; set; }
        public DbSet<FormDatas> FormDatas { get; set; }
        public DbSet<FormOfPatient> FormOfPatients { get; set; }
        public DbSet<TableData> TableDatas { get; set; }
        public DbSet<ICD10Visit> ICD10Visit { get; set; }

        // public DbSet<VitalSignForAdultTemp> VitalSignForAdultTemp { get; set; }

        #region
        public DbSet<PrescriptionNoteModel> PrescriptionNoteModel { get; set; }
        public DbSet<PresciptionRoundInfoModel> PresciptionRoundInfoModel { get; set; }
        public DbSet<UnlockVip> UnlockVip { get; set; }
        public DbSet<DiagnosticReporting> DiagnosticReporting { get; set; }

        #endregion
    }

    public class EmergencyDepartmentContextLogger
    {
        public static void Log(string component, string message)
        {
            System.Diagnostics.Debug.WriteLine("Component: {0} Message: {1} ", component, message);
        }
    }
}