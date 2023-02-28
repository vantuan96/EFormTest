
--------------------------Phiếu đồng ý xét nghiệm HIV ------------------------------------
--Insert Action
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'IPDHIVTCC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'IPDHIVTCU', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Phiếu đồng ý xét nghiệm HIV' , N'IPDHIVTCG');

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'EDHIVTCC', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'EDHIVTCU', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM] Phiếu đồng ý xét nghiệm HIV' , N'EDHIVTCG');

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'EOCHIVTCC', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'EOCHIVTCU', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] Phiếu đồng ý xét nghiệm HIV' , N'EOCHIVTCG');

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] Phiếu đồng ý xét nghiệm HIV' , N'OPDHIVTCC', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] Phiếu đồng ý xét nghiệm HIV' , N'OPDHIVTCU', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Phiếu đồng ý xét nghiệm HIV' , N'OPDHIVTCG');

--Insert bảng form
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'IPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'OPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'ED')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu đồng ý xét nghiệm HIV', N'A01_014_050919_VE', 'EOC')

--insert Masterdatas
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu đồng ý xét nghiệm HIV',N'HIV testing consent form',N'HIVTestingConsent',N'EDHCCK',N'EDNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu đồng ý xét nghiệm HIV',N'HIV testing consent form',N'HIVTestingConsent',N'EOCHCCK',N'EOCNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu đồng ý xét nghiệm HIV',N'HIV testing consent form',N'HIVTestingConsent',N'IPDHCCK',N'IPDNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu đồng ý xét nghiệm HIV',N'HIV testing consent form',N'HIVTestingConsent',N'OPDHCCK',N'OPDNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');




--------------------------Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao ------------------------------------

-- insert Action
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'IPDCFOOPC', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'IPDCFOOPU', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'IPDCFOOPG', '');

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EDCFOOPC', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][CHỈNH SỬA] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EDCFOOPU', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EDCFOOPG', '');

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EOCCFOOPC', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA]Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EOCCFOOPU', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'EOCCFOOPG', '');

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'OPDCFOOPC', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'OPDCFOOPU', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao' , N'OPDCFOOPG', '');

--insert Form
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'IPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'OPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'ED')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao', N'A01_001_080721_V', 'EOC')

--Insert Masterdata
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao',N'Consent for operation/procedure/hight risk treatment',N'ConsentForOperationOrrProcedure',N'EDHCCK',N'EDNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao',N'Consent for operation/procedure/hight risk treatment',N'ConsentForOperationOrrProcedure',N'EOCHCCK',N'EOCNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao',N'Consent for operation/procedure/hight risk treatment',N'ConsentForOperationOrrProcedure',N'IPDHCCK',N'IPDNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu cam kết phẫu thuật/ thủ thuật điều trị nguy cơ cao',N'Consent for operation/procedure/hight risk treatment',N'ConsentForOperationOrrProcedure',N'OPDHCCK',N'OPDNHOMBIEUMAU',N'2',N'',N'',N'',N'0',N'',N'', '1');

