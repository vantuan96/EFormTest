insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Xem Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)' , N'IPDVIEW13NHI', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Chỉnh sửa Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)' , N'IPDEDIT13NHI', (select id from VisitTypeGroups where Code = 'IPD'));

INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)','A02_036_080322_V','IPD',0)
INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi (Từ 3 đến dưới 12 tháng tuổi)','A02_035_080322_V','IPD',0)
INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi (Từ 1 đến dưới 4 tuổi)','A02_034_080322_V','IPD',0)
INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi (Từ 4 đến 12 tuổi)','A02_033_080322_V','IPD',0)
INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ nhi (Trên 12 tuổi)','A02_032_220321_VE','IPD',0)

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][ĐD CHỈNH SỬA] Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em' , N'IPDDDCS', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em' , N'IPDXEM', (select id from VisitTypeGroups where Code = 'IPD'));

UPDATE IPDFallRiskAssessmentForAdults set FormType = 'A02_048_301220_VE'

INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Đánh giá nguy cơ ngã ở trẻ em','A02_047_301220_VE','IPD',0)

