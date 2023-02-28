--Phiếu khám gây mê
-- Phân quyền 
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] Phiếu khám gây mê' , N'OPDPACS', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][SỬA] Phiếu khám gây mê' , N'PACPUT', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Phiếu khám gây mê' , N'OPDPACGM');
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XÁC NHẬN] Phiếu khám gây mê' , N'APIPACCF', (select id from VisitTypeGroups where Code = 'OPD'));

-- insert bảng form
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode], Version, Time, Ispermission) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Phiếu khám gây mê', 'A03_034_200520_VE', 'OPD', 1, 1, 0)

--Insert MasterData
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu khám gây mê ',N'Pre-Anesthesia Consultation (PAC)',N'PreAnesthesiaConsultation',N'OPDBAHC',N'OPDNHOMBIEUMAU',N'2',N'3',N'Label',N'',N'0',N'',N'', '1');

-- chỉ định chung
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[Xem] Xem lịch sử chỉ định chung' , N'VIEWALSER');


