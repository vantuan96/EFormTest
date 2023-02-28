-- thời gian deploy version 2 sàng lọc ngã
INSERT INTO AppConfigs(Id, [Key], [Label], [Value], IsDeleted)
VALUES(NEWID(), 'UPDATE_VERSION2_A02_007_220321_VE', N'Cập nhật tính năng mới cho đánh giá ngã với các bệnh nhân được tiếp sau khi deploy', FORMAT(GETDATE(), 'yyyy/MM/dd HH:mm tt zzz'), 0)

-- phân quyền version 2 sàng lọc ngã
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Xem] Sàng lọc ngã Version 2' , N'XEMSLN', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Tạo Mới] Sàng lọc ngã Version 2' , N'TAOSLN', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Chỉnh Sửa] Sàng lọc ngã Version 2' , N'SUASLN', (select id from VisitTypeGroups where Code = 'ED'));

-- PHÂN QUYỀN giấy xác nhận bệnh tật
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Giấy xác nhận tình trạng bệnh tật' , N'OPDXEMGXNBT', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][BÁC SĨ XÁC NHẬN] Giấy xác nhận tình trạng bệnh tật' , N'OPDBSXNGXNBT', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][BỆNH VIỆN XÁC NHẬN] Giấy xác nhận tình trạng bệnh tật' , N'OPDBVXNGXNBT', (select id from VisitTypeGroups where Code = 'OPD'));


--Update tiếng anh bệnh án răng hàm mặt
update MasterDatas set EnName = N'The Dental- Maxillofacial Medical Record'
where Form = 'A01_040_050919_V'
update IPDSetupMedicalRecords set EnName = N'The Dental- Maxillofacial Medical Record'
where Formcode = 'A01_040_050919_V'

--Sét lại IsDelete cho unlock form biên bản hc dấu sao bị ông nào sửa lại làm sai mất
update Forms set IsDeleted = 0
where Code = 'IPDBBHCTDS'
update Forms set IsDeleted = 1
where Code = 'IPDEXTA' and Name = N'Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao (*)'

--setup Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu đánh giá tiêu chuẩn chỉ định xét nghiệm GEN BRCA',N'Criteria Assesssment for GEN BRCA Test already',N'A03_119_201119_V',N'OPDSANPHUKHOA',N'A03_119_201119_V',N'',N'',N'',N'OPD',N'',N'',N'', '');

-- Update EnName Bệnh án sản
update MasterDatas set EnName = N'Obstetric medical record'
where Form = 'A01_035_050919_V'
update IPDSetupMedicalRecords set EnName = N'Obstetric medical record'
where Formcode = 'A01_035_050919_V'