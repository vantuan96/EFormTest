delete EDArterialBloodGasTestDatas where EDArterialBloodGasTestDatas.Id in (select e.Id from EDArterialBloodGasTestDatas e  inner join EDPointOfCareTestingMasterDatas d on e.EDPointOfCareTestingMasterDataId = d.Id where d.Form = 'A03_038_061222_V_XNTC')
delete EDPointOfCareTestingMasterDatas where Form = 'A03_038_061222_V_XNTC'

 

Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',1,'pH',N'Người trưởng thành',N'Người trưởng thành',7.45,7.35,7.59,7.21,null,4,0,'2022-12-20 10:27:37.943')

 


Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',1,'pH',N'Trẻ em',N'Trẻ em',null,null,7.59,7.21,null,4,0,'2022-12-20 11:27:37.943')

 


Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',2,'pCO2',N'Người trưởng thành',N'Người trưởng thành',45,35,67,19,'mmHg',4,0,'2022-12-20 12:27:37.943')
Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',2,'pCO2',N'Trẻ em',N'Trẻ em',null,null,66,21,'mmHg',4,0,'2022-12-20 13:27:37.943')

 

 

 

Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',3,'pO2',N'Người trưởng thành',N'Người trưởng thành',105,80,null,43,'mmHg',4,0,'2022-12-20 14:27:37.943')
Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',3,'pO2',N'Trẻ em',N'Trẻ em',null,null,124,45,'mmHg',4,0,'2022-12-20 15:27:37.943')
Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',3,'pO2',N'Trẻ sơ sinh',N'Trẻ sơ sinh',null,null,92,37,'mmHg',4,0,'2022-12-20 16:27:37.943')

 

 

 

Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',4,'BE',N'Người trưởng thành',N'Người trưởng thành',3,-2,null,null,'mmol/L',4,0,'2022-12-20 17:27:37.943')

 

 

 

Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',5,'HCO3',N'Người trưởng thành',N'Người trưởng thành',26,22,null,null,'mmol/L',4,0,'2022-12-20 18:27:37.943')

 

 

 

Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',6,'TCO2',N'Người trưởng thành',N'Người trưởng thành',27,23,null,null,'mmol/L',4,0,'2022-12-20 19:27:37.943')

 

 

 

Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',7,'SO2',N'Người trưởng thành',N'Người trưởng thành',98,95,null,null,'%',4,0,'2022-12-20 20:27:37.943')

 

 

 

Insert into EDPointOfCareTestingMasterDatas(Id,Form,[Order],[Name],ViAge,EnAge,HigherLimit,LowerLimit,HigherAlert,LowerAlert,Unit,[Version],IsDeleted,CreatedAt)
Values(NEWID(),'A03_038_061222_V_XNTC',8,'Lactat',N'Người trưởng thành',N'Người trưởng thành',1.25,0.36,null,null,'mmol/L',4,0,'2022-12-20 21:27:37.943')


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Xác nhận ĐGBĐ NB hóa trị' , N'XACNHANDGBDNBHT', (select id from VisitTypeGroups where Code = 'IPD'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Xác nhận ĐGBĐ NB nội trú thông thường' , N'XACNHANDGBDNTTT', (select id from VisitTypeGroups where Code = 'IPD'));

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode],[Ispermission]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'ĐGBĐ NB cao tuổi/ cuối đời', 'A03_012_080121_VE', 'IPD',0)
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD] Xác nhận ĐGBĐ NB cao tuổi/ cuối đời' , N'XACNHANDGBDNBCT', (select id from VisitTypeGroups where Code = 'IPD'));

INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode],[Ispermission]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'ĐGBĐ NB hóa trị', 'A02_011_080121_VE', 'IPD',0)