insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[Tạo mới][IPD] Phiếu cam kết truyền máu và các chế phẩm máu' , N'IPDCFTOB01', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[Xem][IPD] Phiếu cam kết truyền máu và các chế phẩm máu' , N'IPDCFTOB02', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[Cập nhật][IPD] Phiếu cam kết truyền máu và các chế phẩm máu' , N'IPDCFTOB03', (select id from VisitTypeGroups where Code = 'IPD'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[Tạo mới][ED] Phiếu cam kết truyền máu và các chế phẩm máu' , N'EDCFTOB01', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[Xem][ED] Phiếu cam kết truyền máu và các chế phẩm máu' ,  N'EDCFTOB02', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[Cập nhật][ED] Phiếu cam kết truyền máu và các chế phẩm máu' , N'EDCFTOB03', (select id from VisitTypeGroups where Code = 'ED'));

--------------------------Phiếu đồng ý xét nghiệm HIV ------------------------------------
--Insert Action
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'IPDHIVTCC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'IPDHIVTCU', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Phiếu đồng ý xét nghiệm HIV' , N'IPDHIVTCG', NULL);

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'EDHIVTCC', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'EDHIVTCU', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM] Phiếu đồng ý xét nghiệm HIV' , N'EDHIVTCG', NULL);

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'EOCHIVTCC', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'EOCHIVTCU', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] Phiếu đồng ý xét nghiệm HIV' , N'EOCHIVTCG', NULL);

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'OPDHIVTCC', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'OPDHIVTCU', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Phiếu đồng ý xét nghiệm HIV' , N'OPDHIVTCG', NULL);

--Insert bảng form
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'IPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'OPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'ED')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'EOC')


-- insert Action
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'IPDCFOOPC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'IPDCFOOPU', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'IPDCFOOPG', NULL);

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EDCFOOPC', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][CHỈNH SỬA] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EDCFOOPU', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EDCFOOPG', NULL);

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EOCCFOOPC', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA]Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EOCCFOOPU', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EOCCFOOPG', NULL);

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'OPDCFOOPC', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'OPDCFOOPU', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'OPDCFOOPG', NULL);

--insert Form
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'IPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'OPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'ED')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'EOC')


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xem] yêu cầu không hồi sinh tim phổi' , N'IPDVIEWKHSTP', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Sửa] yêu cầu không hồi sinh tim phổi' , N'IPDUPDATEKHSTP', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Tạo] yêu cầu không hồi sinh tim phổi' , N'IPDCREATEKHSTP', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Xem] yêu cầu không hồi sinh tim phổi' , N'EDVIEWKHSTP', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Sửa] yêu cầu không hồi sinh tim phổi' , N'EDUPDATEKHSTP', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Tạo] yêu cầu không hồi sinh tim phổi' , N'EDCREATEkHSTP', (select id from VisitTypeGroups where Code = 'ED'));

-- Unlocked
INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted) VALUES(NEWID(),GETDATE(),GETDATE(),N'Yêu cầu không hồi sinh tim phổi','A01_032_050919_VE','IPD',0)


INSERT INTO [dbo].[Forms] ([Id], [IsDeleted],[DeletedBy],[DeletedAt],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt],[Name],[Code],[VisitTypeGroupCode])
     VALUES (NEWID(), 0, NULL, NULL, NULL, GETDATE(), NULL, GETDATE(), N'Phiếu cam kết truyền máu và các chế phẩm máu', 'A01_006_080721_V', 'IPD')                                            
                                                
INSERT INTO [dbo].[Forms] ([Id], [IsDeleted],[DeletedBy],[DeletedAt],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt],[Name],[Code],[VisitTypeGroupCode])
     VALUES (NEWID(), 0, NULL, NULL, NULL, GETDATE(), NULL, GETDATE(), N'Phiếu cam kết truyền máu và các chế phẩm máu', 'A01_006_080721_V', 'ED')


-- sửa quyền động mạch vành
update Actions set Name = N'[IPD][BS CHỈNH SỬA] Tóm tắt thủ thuật can thiệp động mạch vành', Code = 'DTCTDMV' where Name = N'[IPD][BS CHỈNH SỬA] Điều trị can thiệp động mạch vành'
update Actions set Name = N'[IPD][BS Xác nhận] Tóm tắt thủ thuật can thiệp động mạch vành' where Name = N'[IPD][BS Xác nhận] Điều trị can thiệp động mạch vành'