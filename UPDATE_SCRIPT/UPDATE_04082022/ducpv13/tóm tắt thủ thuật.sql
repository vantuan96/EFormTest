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





insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu tóm tắt thủ thuật', 'IPDPSV2', 'IPD')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu tóm tắt thủ thuật', 'OPDPSV2', 'OPD')



