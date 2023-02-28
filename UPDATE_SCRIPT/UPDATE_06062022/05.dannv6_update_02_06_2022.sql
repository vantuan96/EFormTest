-- phân quyền y lệnh miệng

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Phiếu ghi nhận thuốc y lệnh miệng' , N'IPDVBO', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI, CHỈNH SỬA] Phiếu ghi nhận thuốc y lệnh miệng' , N'IPDVBO1', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BÁC SỸ XÁC NHẬN] Phiếu ghi nhận thuốc y lệnh miệng' , N'IPDVBO2', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][ĐIỀU DƯỠNG XÁC NHẬN] Phiếu ghi nhận thuốc y lệnh miệng' , N'IPDVBO3', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XÓA] Phiếu ghi nhận thuốc y lệnh miệng' , N'IPDVBO4', (select id from VisitTypeGroups where Code = 'IPD'));

-- Unlock form y lệnh miệng

INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Phiếu ghi nhận thuốc y lệnh miệng','A03_030_290321_VE','IPD',0)

-- setup bệnh án ung bướu

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh án ung bướu',N'The Oncology Medical Record',N'MedicalRecordOncology',N'MedicalRecords',N'A01_196_050919_V',N'',N'',N'',N'',N'0',N'',N'', '1');

-- Unlock form bệnh án ung bướu

INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Bệnh án ung bướu','A01_196_050919_V','IPD',0)

-- Phân quyền và mở khóa form, cập nhật FormType cho đánh giá ngã người lớn
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][ĐD CHỈNH SỬA] Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em' , N'IPDDDCS', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em' , N'IPDXEM', (select id from VisitTypeGroups where Code = 'IPD'));

UPDATE IPDFallRiskAssessmentForAdults set FormType = 'A02_048_301220_VE'

INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Đánh giá nguy cơ ngã ở trẻ em','A02_047_301220_VE','IPD',0)

--setup biểu mẫu tim mạch
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Biểu mẫu Tim mạch',N'Cardiovascular Form',N'CardiovascularForm',N'MedicalRecords',N'BMTIMMACH',N'',N'',N'',N'',N'',N'',N'', '');