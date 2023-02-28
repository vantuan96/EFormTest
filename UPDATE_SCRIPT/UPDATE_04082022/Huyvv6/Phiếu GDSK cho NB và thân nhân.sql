insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] danh sách phiếu GDSK NB và thân nhân' , N'OPFEF1', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] chi tiết phiếu GDSK NB và thân nhân' , N'OPFEF2', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] phiếu GDSK NB và thân nhân' , N'OPFEF3', (select id from VisitTypeGroups where Code = 'OPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] danh sách phiếu GDSK NB và thân nhân' , N'EOPFEF1', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] chi tiết phiếu GDSK NB và thân nhân' , N'EOPFEF2', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA] phiếu GDSK NB và thân nhân' , N'EOPFEF3', (select id from VisitTypeGroups where Code = 'EOC'));


INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu GDSK cho NB và thân nhân', 'A03_045_290422_VE', 'OPD')
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Phiếu GDSK cho NB và thân nhân', 'A03_045_290422_VE', 'EOC')
 
 Update EDChemicalBiologyTests
 SEt Version = 1  
 where Version = 0 
