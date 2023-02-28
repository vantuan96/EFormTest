INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) 
VALUES (NEWID(), 0, '2021-11-09 00:00:00.000', '2021-11-09 00:00:00.000', N'Bảng theo dõi sinh tồn người lớn', 'IPDMEWS', 'IPD');

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) 
VALUES (NEWID(), 0, '2021-11-09 00:00:00.000', '2021-11-09 00:00:00.000', N'Giấy chứng nhận thương tích', 'IPDINCERT', 'IPD');

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) 
VALUES (NEWID(), 0, '2021-11-09 00:00:00.000', '2021-11-09 00:00:00.000', N'Braden đánh giá nguy cơ loét', 'IPDBRADENSCALE', 'IPD');

INSERT INTO Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) 
values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Bảng điểm Braden đánh giá nguy cơ loét' , N'IPDBRADEN_VW', (select id from VisitTypeGroups where Code = 'IPD'));

INSERT INTO Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) 
values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Bảng điểm Braden đánh giá nguy cơ loét' , N'IPDBRADEN_INS', (select id from VisitTypeGroups where Code = 'IPD'));

INSERT INTO Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) 
VALUES (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] Bảng điểm Braden đánh giá nguy cơ loét' , N'IPDBRADEN_INS', (select id from VisitTypeGroups where Code = 'IPD'));




