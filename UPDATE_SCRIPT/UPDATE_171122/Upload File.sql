--Actions
--IPD
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xem] Xem file' , N'VIEWFILEDTIPDUPLOADFILE' );
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Sửa file] Sửa file' , N'UPLOADFILEUPDAIPDUPLOADFILE', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Upload file] Tạo file' , N'APICRIPDUPLOADFILE', (select id from VisitTypeGroups where Code = 'IPD'));
--OPD
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][Xem] Xem file' , N'VIEWFILEDTOPDUPLOADFILE' );
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][Sửa file] Sửa file' , N'UPLOADFILEUPDAOPDUPLOADFILE', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][Upload file] Tạo file' , N'APICROPDUPLOADFILE', (select id from VisitTypeGroups where Code = 'OPD'));
--ED
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Xem] Xem file' , N'VIEWFILEDTEDUPLOADFILE' );
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Sửa file] Sửa file' , N'UPLOADFILEUPDAEDUPLOADFILE', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Upload file] Tạo file' , N'APICREDUPLOADFILE', (select id from VisitTypeGroups where Code = 'ED'));
--EOC
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][Xem] Xem file' , N'VIEWFILEDTEOCUPLOADFILE' );
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][Sửa file] Sửa file' , N'UPLOADFILEUPDAEOCUPLOADFILE', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][Upload file] Tạo file' , N'APICREOCUPLOADFILE', (select id from VisitTypeGroups where Code = 'EOC'));
-- MasterData
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Upload File',N'Upload File',N'UploadImage',N'BMKTHSBA',N'NHOMBIEUMAU',N'2',N'3',N'Label',N'',N'0',N'',N'', '1');
-- Form
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Upload File', 'UPLOADFILE', 'IPD')





---Sửa status
update  EDStatus set ViName = N'Chờ KQ CLS', EnName = N'Waiting for result' where Code in ('OPDWR','EDWR', 'IPDWR', 'EOCWR')



-- Insert form Bệnh án cấp cứu ngoại viện để mở khoá form
Insert into Forms (Id, IsDeleted, Code, VisitTypeGroupCode, Name) Values (NEWID(), 0, N'A03_006_050919_VE', 'ED', N'Bệnh án cấp cứu ngoại viện')