insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDPOST', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDGET', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDPUT', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] [XÁC NHẬN] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDCFIRM', (select id from VisitTypeGroups where Code = 'IPD'));

Update IPDMedicationHistories
SET FormCode = 'A03_120_120421_VE'

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu khai thác tiền sử dùng thuốc - Nhi', 'A03_124_120421_VE', 'IPD')
