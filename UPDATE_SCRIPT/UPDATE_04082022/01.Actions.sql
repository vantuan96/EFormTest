insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BS Xác nhận] Điều trị can thiệp động mạch vành' , N'DTCTDMV3', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Xác nhận phiếu xét nghiệm sinh hóa máu' , N'IPOCT10', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Tạo phiếu xét nghiệm sinh hóa máu' , N'IPOCT6', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Chỉnh sửa phiếu xét nghiệm sinh hóa máu' , N'IPOCT9', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Xem chi tiết giấy XN sinh hóa máu' , N'IPOCT8', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Xem phiếu xét nghiệm sinh hóa máu' , N'IPOCT7', NULL);


INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+', 'A03_039_080322_V', 'IPD')
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Xét nghiệm tại chỗ - Sinh hóa máu Catridge CHEM8+', 'A03_039_080322_V', 'ED')






---Phan quyen thang danh gia tu sat
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BS TẠO MỚI]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSIC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BS CHỈNH SỬA]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSIU', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BS XAC NHAN]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSICF', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSICV', (select id from VisitTypeGroups where Code = 'IPD'));



--Luu bang form
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)  values (NEWID(), 0, N'Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát', 'A01_221_210121_V', 'IPD')


--Phan quyen tham khao ho so ben an
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM TIỀN SỬ BỆNH] Tham khảo tiền sử bệnh từ bệnh án nội trú' , N'IPDOB09', (select id from VisitTypeGroups where Code = 'IPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] Tóm tắt thủ thuật V2' , N'EOCPSV21A01084', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][TẠO MỚI] Tóm tắt thủ thuật V2' , N'EOCPSV22A01084', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][UPDATE] Tóm tắt thủ thuật V2' , N'EOCPSV23A01084', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XÁC NHẬN] Tóm tắt thủ thuật V2' , N'EOCPSV24A01084', (select id from VisitTypeGroups where Code = 'EOC'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM] Tóm tắt thủ thuật V2' , N'EDPSV21A01084', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][TẠO MỚI] Tóm tắt thủ thuật V2' , N'EDPSV22A01084', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][UPDATE] Tóm tắt thủ thuật V2' , N'EDPSV23A01084', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XÁC NHẬN] Tóm tắt thủ thuật V2' , N'EDPSV24A01084', (select id from VisitTypeGroups where Code = 'ED'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Tóm tắt thủ thuật V2' , N'IPDPSV21A01084', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Tóm tắt thủ thuật V2' , N'IPDPSV22A01084', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][UPDATE] Tóm tắt thủ thuật V2' , N'IPDPSV23A01084', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XÁC NHẬN] Tóm tắt thủ thuật V2' , N'IPDPSV24A01084', (select id from VisitTypeGroups where Code = 'IPD'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Tóm tắt thủ thuật V2' , N'OPDPSV21A01084', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] Tóm tắt thủ thuật V2' , N'OPDPSV22A01084', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][UPDATE] Tóm tắt thủ thuật V2' , N'OPDPSV23A01084', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XÁC NHẬN] Tóm tắt thủ thuật V2' , N'OPDPSV24A01084', (select id from VisitTypeGroups where Code = 'OPD'));





insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu tóm tắt thủ thuật', 'IPDPSV2', 'IPD')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu tóm tắt thủ thuật', 'OPDPSV2', 'OPD')



insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] danh sách phiếu GDSK NB và thân nhân' , N'OPFEF1', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] chi tiết phiếu GDSK NB và thân nhân' , N'OPFEF2', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] phiếu GDSK NB và thân nhân' , N'OPFEF3', (select id from VisitTypeGroups where Code = 'OPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] danh sách phiếu GDSK NB và thân nhân' , N'EOPFEF1', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] chi tiết phiếu GDSK NB và thân nhân' , N'EOPFEF2', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA] phiếu GDSK NB và thân nhân' , N'EOPFEF3', (select id from VisitTypeGroups where Code = 'EOC'));

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu GDSK cho NB và thân nhân', 'A03_045_290422_VE', 'OPD')
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu GDSK cho NB và thân nhân', 'A03_045_290422_VE', 'EOC')
 
 
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Xét nghiệm tại chỗ - Khí máu Cartridge CG4' , N'XEMXNTC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][SỬA] Xét nghiệm tại chỗ - Khí máu Cartridge CG4' , N'SUAXNTC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Tạo Mới] Xét nghiệm tại chỗ - Khí máu Cartridge CG4' , N'TAOXNTC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xác Nhận] Xét nghiệm tại chỗ - Khí máu Cartridge CG4' , N'XACNHANXNTC', (select id from VisitTypeGroups where Code = 'IPD'));
