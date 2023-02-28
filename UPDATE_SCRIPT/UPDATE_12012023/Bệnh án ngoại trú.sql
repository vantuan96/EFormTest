insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD]Xác nhận bệnh án ngoại trú' , N'APICFA01_252_221222_V', (select id from VisitTypeGroups where Code ='OPD'));
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bệnh án ngoại trú', 'A01_252_221222_V', 'OPD')

--Bệnh án ngoại trú
insert into FormOfPatients (ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD', N'Chuyển tuyến',N'Upstream/Downstream transfer',55)
insert into FormOfPatients (ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Chuyển viện',N'Inter-hospital transfer',56)
insert into FormOfPatients ( ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Chuyển cấp cứu',N'Transfer to ED',57)
insert into FormOfPatients ( ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Nhập nội trú',N'Admitted',58)
insert into FormOfPatients ( ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Hoàn thành khám',N'Discharged',59)