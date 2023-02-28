insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3C', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3U', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3CF', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3C', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3U', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3CF', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3C', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3U', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3CF', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3C', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3U', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3CF', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));


insert into [EFORM_TEST].[dbo].[Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'ED')
insert into [EFORM_TEST].[dbo].[Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'EOC')
insert into [EFORM_TEST].[dbo].[Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'IPD')
insert into [EFORM_TEST].[dbo].[Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'OPD')


--OPD được quyền xem 3 khu vực còn lại
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));



--IPD được quyền xem 3 khu vực còn lại
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));

--ED được xem các khu vực còn lại

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));


--EOC được xem cái khu vực còn lại

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tóm tắt phẫu thuật',N'Surgery And Procedure Summary',N'A01_085_120522_VE',N'OPD',N'A01_085_120522_VE',N'',N'',N'',N'OPD',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tóm tắt phẫu thuật',N'Surgery And Procedure Summary',N'A01_085_120522_VE',N'IPD',N'A01_085_120522_VE',N'',N'',N'',N'IPD',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tóm tắt phẫu thuật',N'Surgery And Procedure Summary',N'A01_085_120522_VE',N'ED',N'A01_085_120522_VE',N'',N'',N'',N'ED',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tóm tắt phẫu thuật',N'Surgery And Procedure Summary',N'A01_085_120522_VE',N'EOC',N'A01_085_120522_VE',N'',N'',N'',N'EOC',N'',N'',N'', '');

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'IPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'OPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'ED')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'EOC')