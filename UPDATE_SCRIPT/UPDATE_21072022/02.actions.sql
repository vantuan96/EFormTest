insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Xem] Sàng lọc ngã Version 2' , N'XEMSLN', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Tạo Mới] Sàng lọc ngã Version 2' , N'TAOSLN', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Chỉnh Sửa] Sàng lọc ngã Version 2' , N'SUASLN', (select id from VisitTypeGroups where Code = 'ED'));

-- PHÂN QUYỀN giấy xác nhận bệnh tật
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Giấy xác nhận tình trạng bệnh tật' , N'OPDXEMGXNBT', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][BÁC SĨ XÁC NHẬN] Giấy xác nhận tình trạng bệnh tật' , N'OPDBSXNGXNBT', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][BỆNH VIỆN XÁC NHẬN] Giấy xác nhận tình trạng bệnh tật' , N'OPDBVXNGXNBT', (select id from VisitTypeGroups where Code = 'OPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD] [VIEW] Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA 1/2' , N'GENBRCAGET', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][BS/ĐD CHỈNH SỬA] Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA 1/2' , N'GENBRCAPOST', (select id from VisitTypeGroups where Code = 'OPD'));
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA', 'A03_119_201119_V', 'OPD')


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDPOST', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDGET', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDPUT', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] [XÁC NHẬN] Khai thác tiền sử dùng thuốc  - Nhi' , N'MEDHISFORCHILDCFIRM', (select id from VisitTypeGroups where Code = 'IPD'));

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu khai thác tiền sử dùng thuốc - Nhi', 'A03_124_120421_VE', 'IPD')
