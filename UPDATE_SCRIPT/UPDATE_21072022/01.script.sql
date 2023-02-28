INSERT INTO AppConfigs(Id, [Key], [Label], [Value], IsDeleted)
VALUES(NEWID(), 'UPDATE_VERSION2_A02_007_220321_VE', N'Cập nhật tính năng mới cho đánh giá ngã với các bệnh nhân được tiếp sau khi deploy', FORMAT(GETDATE(), 'yyyy/MM/dd HH:mm tt zzz'), 0)

update Forms set IsDeleted = 0
where Code = 'IPDBBHCTDS'
update Forms set IsDeleted = 1
where Code = 'IPDEXTA' and Name = N'Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)'


Update IPDMedicationHistories
SET FormCode = 'A03_120_120421_VE'

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu khai thác tiền sử dùng thuốc - Nhi', 'A03_124_120421_VE', 'IPD')


Update	FormDatas
SET FormCode = 'A02_007_220321_VE'
where 	 FormCode = 'OPDFRSFOP'	 and VisitType = 'EOC'

Update OPDFallRiskScreenings
SET OPDFallRiskScreenings.VisitId = 
(SELECT opds.id FROM opds  WHERE opds.OPDFallRiskScreeningId = OPDFallRiskScreenings.Id)
 
 
 Update  OPDFallRiskScreenings
 Set Version = 1

-- Update EnName Bệnh án sản
update MasterDatas set EnName = N'Obstetric medical record'
where Form = 'A01_035_050919_V'
update IPDSetupMedicalRecords set EnName = N'Obstetric medical record'
where Formcode = 'A01_035_050919_V'


select * from MasterDatas where code = 'PFEFPCM'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Yêu cầu của người bệnh/ thân nhân về phương pháp tư vấn',N'Preferred counseling method',N'PFEFPCM',N'PFEF',N'PFEF',N'1',N'10',N'',N'',N'',N'',N'PFEFEN0PM0,PFEFEN0MU0,PFEFEN0CP0,PFEFEN0NC0,PFEFEN0HHI,PFEFEN0ROM,PFEFEN0ROA,PFEFEN0REH,PFEFEN0PDC,PFEFEN0OTH', '2');


delete from MasterDatas where Code = 'A01_201_201119_V'
delete from IPDSetupMedicalRecords where Formcode = 'A01_201_201119_V'