
-- nhóm HSBA
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu phẫu thuật/ thủ thuật',N'Phiếu phẫu thuật/ thủ thuật',N'SurgeryAndProcedureSummary',N'EDPTTT',N'EDNHOMBIEUMAU',N'2',N'6',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu cam kết truyền máu và các chế phẩm máu',N'Phiếu cam kết truyền máu và các chế phẩm máu',N'ConsentForTransfusionOfBlood',N'EDHCCK',N'EDNHOMBIEUMAU',N'2',N'2',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu cam kết truyền máu và các chế phẩm máu',N'Phiếu cam kết truyền máu và các chế phẩm máu',N'ConsentForTransfusionOfBlood',N'IPDHCCK',N'IPDNHOMBIEUMAU',N'2',N'3',N'',N'',N'',N'',N'', '');
----------------------------------------------------------------------------------------------------------------------------------------------
-- update version 1, 2 động mạch vành
declare @NumberRow INT;
declare @IndexRow INT;
set @NumberRow = (select count(C.Id) from IPDs  I
	join IPDCoronaryInterventions C on I.Id = C.VisitId
	where I.CreatedAt >= CONVERT(datetimeoffset(5), (select Value from AppConfigs where [Key] = 'UPDATE_VERSION2_A01_076_290422_VE'))
);
set @IndexRow = 1;

while @IndexRow <= @NumberRow
begin
	update IPDCoronaryInterventions set [Version] = 2
	where Id = (select Id from (select STT = ROW_NUMBER() over(order by C.Id), C.Id from IPDs  I
				join IPDCoronaryInterventions C on I.Id = C.VisitId
				where I.CreatedAt >= CONVERT(datetimeoffset(5), (select Value from AppConfigs where [Key] = 'UPDATE_VERSION2_A01_076_290422_VE'))) AS A
				where A.STT = @IndexRow)

	set @IndexRow = @IndexRow + 1;
end

update IPDCoronaryInterventions set [Version] = 1 where [Version] = 0
----------------------------------------------------------------------------------------------------------------

-- yêu cầu không hồi sinh tim phổi
-- phân quyền
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xem] yêu cầu không hồi sinh tim phổi' , N'IPDVIEWKHSTP', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Sửa] yêu cầu không hồi sinh tim phổi' , N'IPDUPDATEKHSTP', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Tạo] yêu cầu không hồi sinh tim phổi' , N'IPDCREATEKHSTP', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Xem] yêu cầu không hồi sinh tim phổi' , N'EDVIEWKHSTP', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Sửa] yêu cầu không hồi sinh tim phổi' , N'EDUPDATEKHSTP', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Tạo] yêu cầu không hồi sinh tim phổi' , N'EDCREATEkHSTP', (select id from VisitTypeGroups where Code = 'ED'));

-- Unlocked
INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Yêu cầu không hồi sinh tim phổi','A01_032_050919_VE','IPD',0)

-- nhóm hsba
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Yêu cầu không hồi sinh tim phổi',N'Yêu cầu không hồi sinh tim phổi',N'RequestResuscitation',N'IPDHCCK',N'IPDNHOMBIEUMAU',N'2',N'1',N'',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Yêu cầu không hồi sinh tim phổi',N'Yêu cầu không hồi sinh tim phổi',N'RequestResuscitation',N'EDHCCK',N'EDNHOMBIEUMAU',N'2',N'1',N'',N'',N'',N'',N'', '');

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- phiếu khám bệnh
update MasterDatas set ViName = N'Khám tim mạch, hô hấp', EnName = N'Respiratory and cardiac function' where Code = 'OPDOENKHH'
update MasterDatas set Code = 'GENRELIDIDUANS4V2' where Code = 'GENRELIDIDUANS4' and ViName like N'%Bột 150 ml%'
-----------------------------------------------------------------------------------------------------------------------------------------

-- tóm tắt thủ thuật version2
update ProcedureSummaryV2 set Version = '1'
---------------------------------------------------------------------------------------------------------------------------------------------

---- master data các trường mới phiếu khám bệnh
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám toàn thân',N'Body physical examination',N'OPDOENKTTV2',N'OPDOEN',N'OPDOEN',N'1',N'310',N'Label',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám toàn thân',N'Body physical examination',N'OPDOENKTTLV2',N'OPDOENKTTV2',N'OPDOEN',N'2',N'311',N'Text',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám chuyên khoa',N'Subspecialty examination',N'OPDOENKCKV2',N'OPDOEN',N'OPDOEN',N'1',N'312',N'Label',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám chuyên khoa',N'Subspecialty examination',N'OPDOENKCKLV2',N'OPDOENKCKV2',N'OPDOEN',N'2',N'313',N'Text',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám tim mạch, hô hấp',N'Respiratory and cardiac function',N'OPDOENKHHV2',N'OPDOEN',N'OPDOEN',N'1',N'314',N'Label',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám tim mạch, hô hấp',N'Respiratory and cardiac function',N'OPDOENKHHLV2',N'OPDOENKHHV2',N'OPDOEN',N'2',N'315',N'Text',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám các bộ phận khác (nếu có)',N'Examination of other related areas (if any)',N'OPDOENKBPK',N'OPDOEN',N'OPDOEN',N'1',N'316',N'Label',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám các bộ phận khác (nếu có)',N'Examination of other related areas (if any)',N'OPDOENKBPKLV2',N'OPDOENKBPK',N'OPDOEN',N'2',N'317',N'Text',N'',N'',N'',N'', '');
-----------------------------------

-- sửa quyền động mạch vành
update Actions set Name = N'[IPD][BS CHỈNH SỬA] Tóm tắt thủ thuật can thiệp động mạch vành', Code = 'DTCTDMV' where Name = N'[IPD][BS CHỈNH SỬA] Điều trị can thiệp động mạch vành'
update Actions set Name = N'[IPD][BS Xác nhận] Tóm tắt thủ thuật can thiệp động mạch vành' where Name = N'[IPD][BS Xác nhận] Điều trị can thiệp động mạch vành'