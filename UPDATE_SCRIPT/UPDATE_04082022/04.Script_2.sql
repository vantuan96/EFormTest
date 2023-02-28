

SELECT * from  EDPointOfCareTestingMasterDatas where Form = 'EDChemicalBiologyTest' and Version = 2 order by [Order]
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',1,N'Na+',N'Tất cả các lứa tuổi',N'Tất cả các lứa tuổi',146,138,160,120,NULL,N'mmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',2,N'K+',N'Tất cả các lứa tuổi',N'Tất cả các lứa tuổi',4.9,3.5,6,2.5,NULL,N'mmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',3,N'Cl-',N'Tất cả các lứa tuổi',N'Tất cả các lứa tuổi',109,98,NULL,NULL,NULL,N'mmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',4,N'Ion Calcium (iCa2+)',N'< 1 tuổi',N'< 1 tuổi',1.32,1.12,1.5,0.5,NULL,N'mmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',4,N'Ion Calcium (iCa2+)',N'Trẻ em',N'Child',1.32,1.12,1.625,0.75,NULL,N'mmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',5,N'Glucose',N'Tất cả các lứa tuổi',N'Tất cả các lứa tuổi',5.8,3.9,27.75,2.5,NULL,N'mmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',6,N'Ure',N'Tất cả các lứa tuổi',N'Tất cả các lứa tuổi',9.4,2.9,NULL,NULL,NULL,N'mmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',7,N'Creatinine',N'Người lớn',N'Adult',115,53,844,NULL,NULL,N'µmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',7,N'Creatinine',N'Trẻ em',N'Child',115,53,265,NULL,NULL,N'µmol/L',2);
WAITFOR DELAY '00:00:01'
Insert into EDPointOfCareTestingMasterDatas(Id,CreatedAt,UpdatedAt,IsDeleted,Form,[Order],[Name],Viage,EnAge,HigherLimit,LowerLimit,HigherAlert,Loweralert,Result,Unit,[Version]) values (NewId(),GetDate(),GetDate(),'false',N'EDChemicalBiologyTest',7,N'Creatinine',N'Sơ sinh',N'Infant',115,53,133,NULL,NULL,N'µmol/L',2);

Update MasterDatas set EnName = N'Theo yêu cầu của người bệnh hoặc người đại diện hợp pháp của người bệnh' where code = N'IPDRFT1LOG';
Update MasterDatas set EnName = N'Đủ điều kiện chuyển tuyến' where code = N'IPDRFT1SHT';


update EIOBloodRequestSupplyAndConfirmations set Version = 1


INSERT INTO EDPointOfCareTestingMasterDatas(Id, Form, [Order], Name, HigherLimit, LowerLimit, HigherAlert, LowerAlert, Unit, IsDeleted)
VALUES(NEWID(), 'A03_038_080322_V', 1, 'pH', 7.45, 7.35, 7.60, 7.20, '', 0)
,(NEWID(), 'A03_038_080322_V', 2, 'pCO2', 45, 35, 70, 20, 'mmHg', 0)
,(NEWID(), 'A03_038_080322_V', 3, 'pO2', 105, 80, NULL, 45, 'mmHg', 0)
,(NEWID(), 'A03_038_080322_V', 4, 'BE', 3, -2, NULL, NULL, 'mmol/L', 0)
,(NEWID(), 'A03_038_080322_V', 5, 'HCO3', 26, 22, NULL, NULL, 'mmol/L', 0)
,(NEWID(), 'A03_038_080322_V', 6, 'TCO2', 27, 23, NULL, NULL, 'mmol/L', 0)
,(NEWID(), 'A03_038_080322_V', 7, 'SO2', 98, 95, NULL, NULL, '%', 0)
,(NEWID(), 'A03_038_080322_V', 8, 'Lactat', 1.25, 0.36, NULL, NULL, 'mmol/L', 0)


INSERT INTO AppConfigs(Id, [Key], [Label], [Value], IsDeleted)
VALUES(NEWID(), 'UPDATE_VERSION2_A03_038_080322_V', N'Cập nhật tính năng mới cho Xét nghiệm tại chỗ - Khí máu Cartridge CG4 với các bệnh nhân được tiếp sau khi deploy', FORMAT(GETDATE(), 'yyyy/MM/dd HH:mm tt zzz'), 0)
INSERT INTO AppConfigs(Id, [Key], [Label], [Value], IsDeleted)
VALUES(NEWID(), 'UPDATE_VERSION2_A01_076_290422_VE', N'Cập nhật tính năng mới cho phiếu tóm tắt thủ thuật can thiệp động mạch vành với các bệnh nhân được tiếp sau khi deploy', FORMAT(GETDATE(), 'yyyy/MM/dd HH:mm tt zzz'), 0)


--- update sửa nhóm HSBA
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Biên bản hội chẩn',N'Biên bản hội chẩn',N'JointConsultationGroupMinutes',N'IPDBAHC',N'IPDNHOMBIEUMAU',N'2',N'30',N'Label',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Biên bản bào giao NB chuyển khoa',N'Biên bản bào giao NB chuyển khoa',N'HandOverCheckList',N'IPDBAHC',N'IPDNHOMBIEUMAU',N'2',N'31',N'Label',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá nguy cơ ngã NB nội trú trẻ em ',N'Đánh giá nguy cơ ngã NB nội trú trẻ em ',N'FallRiskAssessmentInPediatricInpatients',N'IPDTDDD',N'IPDNHOMBIEUMAU',N'2',N'20',N'Label',N'',N'',N'',N'', '');

delete from MasterDatas
where Code = 'BloodTransfusionChecklist'
and [Group] = 'EDTDDD'

update MasterDatas set [Group] = 'IPDKHAC'
where Code = 'BloodTransfusionChecklist'
and [Group] = 'IPDTDDD'

update MasterDatas set [Group] = 'IPDBMKTHSBA'
where Code = 'BloodRequestSupplyAndConfirmation'
and [Group] = 'IPDYL'

delete from MasterDatas 
where Code = 'JointConsultationGroupMinutes'
and [Group] = 'EOCPTTT'

update MasterDatas set Code = 'Discharged'
where Code = 'NPQ'

update MasterDatas set Code = 'DischargeMedicalReport'
where Code = 'DMR'

update MasterDatas set Code = 'MedicalReport'
where Code = 'KLM'

delete from MasterDatas
where Code = 'BloodRequestSupplyAndConfirmation'
and [Group] = 'EDYL'

delete from MasterDatas
where Code = 'AssessmentForRetailServicePatient'
and [Group] = 'EDBAHC'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giấy xác nhận bệnh tật',N'Giấy xác nhận bệnh tật',N'DiseasesCertification',N'OPDRAVIEN',N'OPDNHOMBIEUMAU',N'2',N'5',N'Label',N'',N'',N'',N'', '');

 Update EDChemicalBiologyTests
 SEt Version = 1  
 where Version = 0 
