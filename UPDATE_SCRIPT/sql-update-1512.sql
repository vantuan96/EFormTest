
-- update Actions set Name = N'[IPD] Xác nhận giấy chuyển tuyến', VisitTypeGroupId = (select Id from VisitTypeGroups where Code = 'IPD') where Code = 'ITFLE2'
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[OPD] Xác nhận giấy chuyển tuyến', 'OPDXACNHANGCT', (select Id from VisitTypeGroups where Code = 'OPD'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận giấy chuyển tuyến', 'EDXACNHANGCT', (select Id from VisitTypeGroups where Code = 'ED'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[EOC] Xác nhận giấy chuyển tuyến', 'EOCXACNHANGCT', (select Id from VisitTypeGroups where Code = 'EOC'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[EOC] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'EOCXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'EOC'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'IPDXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'IPD'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[OPD] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'OPDXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'OPD'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'EDXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'ED'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xác nhận phiếu điều trị', 'IPDXACNHANPDT', (select Id from VisitTypeGroups where Code = 'IPD'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận phiếu điều trị', 'EDXACNHANPDT', (select Id from VisitTypeGroups where Code = 'ED'))
-- update Actions set Name = N'[IPD] Xác nhận giấy chuyển viện', VisitTypeGroupId = (select Id from VisitTypeGroups where Code = 'IPD') where Code = 'IRELE2'
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[EOC] Xác nhận giấy chuyển viện', 'EOCXACNHANGCV', (select Id from VisitTypeGroups where Code = 'EOC'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[OPD] Xác nhận giấy chuyển viện', 'OPDXACNHANGCV', (select Id from VisitTypeGroups where Code = 'OPD'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận giấy chuyển viện', 'EDXACNHANGCV', (select Id from VisitTypeGroups where Code = 'ED'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD]Phiếu chăm sóc', 'IPDXACNHANPCS', (select Id from VisitTypeGroups where Code = 'IPD'))
-- Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED]Phiếu chăm sóc', 'EDXACNHANPCS', (select Id from VisitTypeGroups where Code = 'ED'))

--Giấy chuyển tuyến xác nhận


Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'IPD')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'ED')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'EOC')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'OPD')

--GDSK người bệnh và nhân thân
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Phiếu GDSK cho NB và thân nhân', 'A03_045_290422_VE', 'ED')

--Phiếu điều trị
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Phiếu điều trị', 'A01_066_050919_VE', 'ED')

--Giấy chuyển viện
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'OPD')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'IPD')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'EOC')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'ED')

--Phiếu chăm sóc
update Forms set Code = 'A02_062_050919_V' where Code = 'IPDCT' and VisitTypeGroupCode = 'ED'


--- masterdata
--- Deploy 15/12/2022
--- Telehealth Examination Note
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu khám Telehealth',N'Telehealth Examination Note',N'TelehealthExaminationNote',N'BAHC',N'NHOMBIEUMAU',N'2',N'3',N'Label',N'',N'0',N'',N'', '1');

--- REFERRAL LETTER (Giấy chuyển viện)
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'BÁC SỸ ĐIỀU TRỊ',N'PHYSICIAN-IN-CHARGE',N'REFERRALLETTER_PIC',N'REFERRALLETTERCONFIRM',N'REFERRALLETTERCONFIRM',N'1',N'1',N'ConfirmForm',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'GIÁM ĐỐC BỆNH VIỆN',N'DIRECTOR',N'REFERRALLETTER_DIR',N'REFERRALLETTERCONFIRM',N'REFERRALLETTERCONFIRM',N'1',N'2',N'ConfirmForm',N'',N'',N'',N'', '1');

--- TRANSFERLETTERCONFIRM (Giấy chuyển tuyến)
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Y, BÁC SĨ KHÁM, ĐIỀU TRỊ',N'PHYSICIAN-IN-CHARGE',N'TRANSFERLETTER_PIC',N'TRANSFERLETTERCONFIRM',N'TRANSFERLETTERCONFIRM',N'1',N'1',N'ConfirmForm',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'NGƯỜI CÓ THẨM QUYỀN CHUYỂN TUYẾN',N'TRANSFER AUTHORITY',N'TRANSFERLETTER_TAU',N'TRANSFERLETTERCONFIRM',N'TRANSFERLETTERCONFIRM',N'1',N'2',N'ConfirmForm',N'',N'',N'',N'', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khai thác thông tin thuốc đã dùng tại nhà (nếu có)',N'Khai thác thông tin thuốc đã dùng tại nhà (nếu có)',N'OPDOENVH600',N'OPDOEN',N'OPDOEN',N'1',N'600',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'NB không có thuốc đang dùng',N'NB không có thuốc đang dùng',N'OPDOENVH601',N'OPDOENVH600',N'OPDOEN',N'2',N'601',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'NB có thuốc đang dùng',N'NB có thuốc đang dùng',N'OPDOENVH602',N'OPDOENVH600',N'OPDOEN',N'2',N'602',N'Radio',N'',N'',N'',N'', '1');

UPDATE FormOfPatients SET TypeName = 'DischargeMedicalReport' WHERE ViName = N'Báo cáo y tế ra viện' AND ViStatusPatient = N'Kết thúc điều trị'
UPDATE FormOfPatients SET TypeName = 'Discharged' WHERE ViName = N'Giấy ra viện' AND ViStatusPatient = N'Kết thúc điều trị'


--delete from MasterDatas where Code in('PFEFSPCHTEXTCD2', 'PFEFSPCHTEXTCD1', 'PFEFSPCHTEXT') and Form = 'PFEF'
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nếu chọn Rào cản ngôn ngữ',N'Nếu chọn Rào cản ngôn ngữ',N'PFEFSPCHTEXT',N'PFEF',N'PFEF',N'1',N'1047',N'Label',N'',N'0',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Rào cản ngôn ngữ',N'Speech or language barrier',N'PFEFSPCHTEXTCD1',N'PFEFSPCHTEXT',N'PFEF',N'2',N'1048',N'Text',N'',N'0',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Rào cản cảm xúc hoặc động lực',N'Emotional or motivation barrier',N'PFEFSPCHTEXTCD2',N'PFEFSPCHTEXT',N'PFEF',N'2',N'1049',N'Text',N'',N'0',N'',N'', '');
 ---
--delete from MasterDatas where Form in ('A03_045_290422_VE', 'IPDGDSK')
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Người thực hiện',N'Completed by',N'CONFIRMIPDGDSK',N'CONFIRMIPDGDSKCONFIRM',N'IPDGDSK',N'1',N'1',N'ConfirmForm',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Người thực hiện',N'Completed by',N'CONFIRMIPDGDSK',N'CONFIRMIPDGDSKCONFIRM',N'A03_045_290422_VE',N'1',N'1',N'ConfirmForm',N'',N'',N'',N'', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N' Chẩn đoán bệnh kèm theo (nếu có)',N' Chẩn đoán bệnh kèm theo (nếu có)',N'DI0Check',N'DI0',N'DI0',N'1',N'28',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Có',N'Yes',N'DI0Check1',N'DI0Check',N'DI0',N'2',N'29',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Không',N'No',N'DI0Check2',N'DI0Check',N'DI0',N'2',N'30',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hình thức ra viện',N'Discharge Type',N'DI0DT',N'DI0',N'DI0',N'1',N'31',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ra viện theo chỉ định',N'Discharge',N'DI0DT1',N'DI0DT',N'DI0',N'2',N'32',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Xin về',N'Request to leave',N'DI0DT2',N'DI0DT',N'DI0',N'2',N'33',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bỏ về (Trốn viện)',N'Leaves',N'DI0DT3',N'DI0DT',N'DI0',N'2',N'34',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đưa về',N'Be transferred home',N'DI0DT4',N'DI0DT',N'DI0',N'2',N'35',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Xin về',N'Request to leave',N'DI0Reason',N'DI0',N'DI0',N'1',N'36',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chuyên môn',N'Speciality',N'DI0Reason1',N'DI0Reason',N'DI0',N'2',N'37',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kinh tế',N'Economy',N'DI0Reason2',N'DI0Reason',N'DI0',N'2',N'38',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khác',N'Other',N'DI0Reason3',N'DI0Reason',N'DI0',N'2',N'39',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khác',N'Other',N'DI0Reason4',N'DI0Reason',N'DI0',N'2',N'40',N'Text',N'',N'',N'',N'', '1');
update MasterDatas set [Order]= 2  where Code = 'DI0DIAANS'
update MasterDatas set [Order]= 1 where Code = 'DI0DIAICD'
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán (Hiển thị trên báo cáo y tế)',N'Diagnosis',N'DI0DIANEW',N'DI0',N'DI0',N'1',N'41',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán bệnh chính (Hiển thị trên báo cáo y tế)',N'Primary diagnosis (Show in Medical Report)',N'DI0DIAANSNEW',N'DI0DIANEW',N'DI0',N'2',N'43',N'Text',N'',N'FALSE',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Mã ICD10 bệnh chính',N'ICD10 Primary diagnosis',N'DI0DIAICDNEW',N'DI0DIANEW',N'DI0',N'2',N'42',N'ICD10',N'',N'TRUE',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Mã ICD10 bệnh kèm theo (nếu có)',N'ICD10 Co-morbidity (if having)',N'DI0DIAOPTNEW',N'DI0DIANEW',N'DI0',N'2',N'44',N'ICD10',N'',N'FALSE',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán bệnh kèm theo (nếu có)',N'Chẩn đoán bệnh kèm theo (nếu có)',N'DI0DIAOPT2NEW',N'DI0DIANEW',N'DI0',N'2',N'45',N'Text',N'',N'FALSE',N'',N'', '1');

