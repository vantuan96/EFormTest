---Phan quyen thang danh gia tu sat
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BS TẠO MỚI]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSIC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BS CHỈNH SỬA]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSIU', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][BS XAC NHAN]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSICF', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM]Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát' , N'IPDSFAOSICV', (select id from VisitTypeGroups where Code = 'IPD'));



--Luu bang form
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)  values (NEWID(), 0, N'Thang đánh giá ý tưởng tự sát và mức độ ý tưởng tự sát', 'A01_221_210121_V', 'IPD')


--Phan quyen tham khao ho so ben an
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM TIỀN SỬ BỆNH] Tham khảo tiền sử bệnh từ bệnh án nội trú' , N'IPDOB09', (select id from VisitTypeGroups where Code = 'IPD'));


