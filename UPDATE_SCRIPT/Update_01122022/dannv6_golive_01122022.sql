--phiếu điều trị + phiếu chăm sóc
Insert Into Forms(Id, IsDeleted, CreatedAt, UpdatedAt, Name, Code, VisitTypeGroupCode)Values(NEWID(), 0, GETDATE(), GETDATE(), N'Phiếu chăm sóc', 'IPDCT', 'ED')
Insert Into Forms(Id, IsDeleted, CreatedAt, UpdatedAt, Name, Code, VisitTypeGroupCode)Values(NEWID(), 0, GETDATE(), GETDATE(), N'Phiếu điều trị', 'IPDTT', 'ED')

--Bệnh án cấp cứu ngoại viện
Insert Into Forms(Id, IsDeleted, CreatedAt, UpdatedAt, Name, Code, VisitTypeGroupCode)Values(NEWID(), 0, GETDATE(), GETDATE(), N'Bệnh án cấp cứu ngoại viện', 'A03_006_050919_VE', 'ED')

--Đánh giá sàng lọc ngã
Insert into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)values(NEWID(), GETDATE(), GETDATE(), 0, N'Đánh giá sàng lọc ngã', 'A02_007_220321_VE', 'ED')

--EOC DGBD thong thuong
Insert into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Đánh giá ban đầu NB ngoại trú thông thường', 'A02_007_080121_VE', 'EOC')

--Báo cáo y tế
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Báo cáo y tế', 'A01_144_050919_VE', 'ED')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Báo cáo y tế', 'A01_198_100120_VE', 'EOC')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Báo cáo y tế', 'A01_144_050919_VE', 'IPD')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Báo cáo y tế', 'A01_198_100120_VE', 'OPD')

--Bảng kiểm bàn giao NB trước pt
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Bảng kiểm chuẩn bị và bàn giao NB trước phẫu thuật', 'EDBKTPT', 'ED')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Bảng kiểm chuẩn bị và bàn giao NB trước phẫu thuật', 'EOCBKTPT', 'EOC')

Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận bảng kiểm chuẩn bị và bàn giao NB trước phẫu thuật', 'XACNHANBKTPT', NULL)

--Bảng điểm GLAMORGAN
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD][Xác nhận] Bảng điểm GLAMORGAN', 'XACNHANGLAMORGAN', (select Id from VisitTypeGroups where Code = 'IPD'))

--ĐGBĐ Ngoại trú TT
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận ĐGBĐ NB ngoại trú thông thường', 'XACNHANDGBDNBNTTT', NULL)

-- Sàng lọc nga
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[OPD] Xác nhận phiếu sàng lọc ngã', 'OPDXACNHANPSLN', (select Id from VisitTypeGroups where Code = 'OPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[EOC] Xác nhận phiếu sàng lọc ngã', 'EOCXACNHANPSLN', (select Id from VisitTypeGroups where Code = 'EOC'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận phiếu sàng lọc ngã', 'EDXACNHANPSLN', (select Id from VisitTypeGroups where Code = 'ED'))

-- Xác nhận Bệnh án CC
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận bệnh án cấp cứu', 'EDXACNHANBACC', (select Id from VisitTypeGroups where Code = 'ED'))

-- xác nhận giấy chứng nhận bệnh nhân cấp cứu
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận giấy chứng nhận bệnh nhân cấp cứu', 'EDXACNHANGCNBNCC', (select Id from VisitTypeGroups where Code = 'ED'))
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chứng nhận bệnh nhân cấp cứu', 'A01_155_050919_V', 'ED')

-- ED xác nhận báo cáo y tế ra viện
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận báo cáo y tế ra viện', 'EDXACNHANBCYTRV', (select Id from VisitTypeGroups where Code = 'ED'))

-- xác nhận báo cáo y tế
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận báo cáo y tế', 'XACNHANBCYT', NULL)

-- xác nhận giấy chuyển viện
update Actions set Name = N'Xác nhận giấy chuyển viện', VisitTypeGroupId = NULL  where Code = 'IRELE2'

-- giấy chuyển tuyến
update Actions set Name = N'Xác nhận giấy tuyến', VisitTypeGroupId = NULL  where Code = 'ITFLE2'

-- xác nhận đgbđ sản phụ chuyển dạ
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xác nhận ĐGBĐ sản phụ chuyển dạ', 'IPDXACNHANDGBDSPCD', (select Id from VisitTypeGroups where Code = 'IPD'))

-- xác nhận trẻ nội trú nhi
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận đánh giá ban đầu trẻ nội trú nhi', 'XACNHANDGBDNTN', (select Id from VisitTypeGroups where Code = 'IPD'))

-- XÁC NHẬN TRẺ SS NỘI TRÚ
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận đánh giá ban đầu trẻ sơ sinh nội trú', 'XACNHANDGBDSSNT', (select Id from VisitTypeGroups where Code = 'IPD'))

-- Xác nhận DGBD dài hạn EOC + OPD
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận đánh giá ban đầu NB ngoại trú dài hạn', 'OPDXACNHANDGBDNTDH', (select Id from VisitTypeGroups where Code = 'OPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận đánh giá ban đầu NB ngoại trú dài hạn', 'EOCXACNHANDGBDNTDH', (select Id from VisitTypeGroups where Code = 'EOC'))

-- xác nhân DGBD người bệnh chăm sóc từ xa
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xác nhận đánh giá ban đầu NB chăm sóc từ xa', 'OPDXACNHANDGBDNBCSTX', (select Id from VisitTypeGroups where Code = 'OPD'))