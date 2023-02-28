--Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch
-- Insert MasterData
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch',N'Surgical/ Procedure safety checklist (Operating room and Cathlab)',N'ProcedureSafetyChecklist',N'PTTT',N'NHOMBIEUMAU',N'2',N'3',N'Label',N'',N'0',N'',N'', '1');

-- Insert Bảng form
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], VisitTypeGroupCode, Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch', 'A02_053_OR_201022_V', 'IPD',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch', 'A02_053_OR_201022_V', 'ED',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch', 'A02_053_OR_201022_V', 'OPD',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch', 'A02_053_OR_201022_V', 'EOC',1)
-- Insert phân quyền
-- Khu vực IPD
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Tạo mới] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICRIPDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Chỉnh sửa] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIUDIPDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xem] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIGDTIPDA02_053_OR_201022_V');
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xác nhận] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICFIPDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'IPD'));

-- Khu vực OPD
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][Tạo mới] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICROPDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][Chỉnh sửa] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIUDOPDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][Xem] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIGDTOPDA02_053_OR_201022_V');
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][Xác nhận] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICFOPDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'OPD'));

-- Khu vực ED
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Tạo mới] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICREDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Chỉnh sửa] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIUDEDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Xem] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIGDTEDA02_053_OR_201022_V');
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Xác nhận] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICFEDA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'ED'));

-- Khu vực EOC
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][Tạo mới] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICREOCA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][Chỉnh sửa] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIUDEOCA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][Xem] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APIGDTEOCA02_053_OR_201022_V');
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][Xác nhận] Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch' , N'APICFEOCA02_053_OR_201022_V', (select id from VisitTypeGroups where Code = 'EOC'));



INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], VisitTypeGroupCode) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Giấy ra viện', 'A01_146_290922_V', 'IPD')
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], VisitTypeGroupCode) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Giấy ra viện', 'A01_146_290922_V', 'ED')



INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần gây mê', 'A02_053_OR_201022_V_TAB1_GM', 'OPD',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 điều dưỡng dụng cụ kiểm tra', 'A02_053_OR_201022_V_TAB1_DCKT', 'OPD',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần điều dưỡng chạy ngoài thực hiện', 'A02_053_OR_201022_V_TAB1_CNTH', 'OPD',1)


INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần gây mê', 'A02_053_OR_201022_V_TAB1_GM', 'IPD',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 điều dưỡng dụng cụ kiểm tra', 'A02_053_OR_201022_V_TAB1_DCKT', 'IPD',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần điều dưỡng chạy ngoài thực hiện', 'A02_053_OR_201022_V_TAB1_CNTH', 'IPD',1)


INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần gây mê', 'A02_053_OR_201022_V_TAB1_GM', 'ED',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 điều dưỡng dụng cụ kiểm tra', 'A02_053_OR_201022_V_TAB1_DCKT', 'ED',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần điều dưỡng chạy ngoài thực hiện', 'A02_053_OR_201022_V_TAB1_CNTH', 'ED',1)

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần gây mê', 'A02_053_OR_201022_V_TAB1_GM', 'EOC',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 điều dưỡng dụng cụ kiểm tra', 'A02_053_OR_201022_V_TAB1_DCKT', 'EOC',1)
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code],VisitTypeGroupCode,Version) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bảng kiểm an toàn Phẫu thuật/ Phủ thuật trong phòng Mổ và phòng Can thiệp tim mạch_Tab1 phần điều dưỡng chạy ngoài thực hiện', 'A02_053_OR_201022_V_TAB1_CNTH', 'EOC',1)