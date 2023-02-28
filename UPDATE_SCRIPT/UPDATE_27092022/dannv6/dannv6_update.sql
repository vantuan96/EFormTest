
-- SUA nhom HSBA BIEN BAN HOI CHAN THONG MO
update MasterDatas set [Group] = 'EDBAHC' where Code = 'JointConsultationForApprovalOfSurgery' and Form = 'EDNHOMBIEUMAU'

update MasterDatas set [Group] = 'OPDBAHC' WHERE Code = 'JointConsultationForApprovalOfSurgery' and Form = 'OPDNHOMBIEUMAU'

update MasterDatas set [Group] = 'IPDBAHC' where Code = 'JointConsultationForApprovalOfSurgery' and Form = 'IPDNHOMBIEUMAU'

-- Seup Clinic
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chung',N'Chung',N'FreeTextOnly-001',N'SETUPCLINICS',N'CHUNG',N'1',N'1',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sản phụ khoa',N'Sản phụ khoa',N'FreeTextOnly-000',N'SETUPCLINICS',N'SANPHUKHOA',N'1',N'2',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dinh dưỡng',N'Dinh dưỡng',N'FreeTextOnly-002',N'SETUPCLINICS',N'DINHDUONG',N'1',N'3',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tâm lý',N'Tâm lý',N'MultiSelect-002',N'SETUPCLINICS',N'TAMLY',N'1',N'4',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhi/ Vaccine',N'Nhi/ Vaccine',N'FreeTextOnly-005',N'SETUPCLINICS',N'NHIVACCINE',N'1',N'5',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phục hồi chức năng',N'Phục hồi chức năng',N'MultiSelect-001',N'SETUPCLINICS',N'PHUCHOICHUCNANG',N'1',N'6',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Liên chuyên khoa/ Da liễu',N'Liên chuyên khoa/ Da liễu',N'RadioYesNo-001',N'SETUPCLINICS',N'LIENCHUYENKHOA',N'1',N'7',N'',N'',N'',N'',N'', '');

UPDATE Clinics SET SetUpClinicDatas = 'FreeTextOnly-000' WHERE Code = 'FreeTextOnly-000'
UPDATE Clinics SET SetUpClinicDatas = 'FreeTextOnly-005' WHERE Code = 'FreeTextOnly-005'
UPDATE Clinics SET SetUpClinicDatas = 'MultiSelect-001' WHERE Code = 'MultiSelect-001'
UPDATE Clinics SET SetUpClinicDatas = 'RadioYesNo-001' WHERE Code = 'RadioYesNo-001'
UPDATE Clinics SET SetUpClinicDatas = 'MultiSelect-002' WHERE Code = 'MultiSelect-002'
UPDATE Clinics SET SetUpClinicDatas = 'FreeTextOnly-001' WHERE Code = 'FreeTextOnly-001'
UPDATE Clinics SET SetUpClinicDatas = 'FreeTextOnly-002' WHERE Code = 'FreeTextOnly-002'
UPDATE Clinics SET SetUpClinicDatas = 'FreeTextOnly-001' WHERE SetUpClinicDatas IS NULL AND Code LIKE N'%FreeTextOnly-001%'
UPDATE Clinics SET SetUpClinicDatas = 'FreeTextOnly-001' WHERE Code NOT IN ('FreeTextOnly-000', 'FreeTextOnly-005', 'MultiSelect-001', 'RadioYesNo-001', 'MultiSelect-002', 'FreeTextOnly-001', 'FreeTextOnly-002') AND SetUpClinicDatas IS NULL OR Code IS NULL
----------------


-- xoa truong note masterdata phieu kham ngoai tru
update MasterDatas set Note = NULL where Code in ('OPDOEN510', 'OPDOEN511', 'OPDOEN512', 'OPDOEN513', 'OPDOEN514', 'OPDOEN515', 'OPDOEN516', 'OPDOEN517')

--update version 1 phiếu khám ngoại trú nếu sót version = 0
update OPDOutpatientExaminationNotes set Version = 1 where Version = 0 