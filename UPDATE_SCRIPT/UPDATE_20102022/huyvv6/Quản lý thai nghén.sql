INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [DeletedBy], [DeletedAt], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, NULL, NULL, NULL, '2021-10-26 00:00:00.000', NULL, '2021-10-26 00:00:00.000', N'Quản lý thai nghén', 'A01_067_050919_VE', 'OPD')

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD] [BS TẠO MỚI] Quản lý thai nghén' , N'APICROPDA01_067_050919_VE', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD] [BS CHỈNH SỬA] Quản lý thai nghén' , N'APIUDOPDA01_067_050919_VE', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD] [BS XÁC NHẬN] Quản lý thai nghén' , N'APICFOPDA01_067_050919_VE', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'OPD] [VIEW] Quản lý thai nghén' , N'APIGDTOPDA01_067_050919_VE', (select id from VisitTypeGroups where Code = 'OPD'));


