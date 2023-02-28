insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[CLS] Danh sách hàng chờ chỉ định' , N'DR000001', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[CLS] Lựa chọn chỉ định' , N'DR000002', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[CLS] Danh sách chỉ định đã chọn' , N'DR000003', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[CLS] Xác nhận/ Chỉnh sửa kết quả' , N'DR000004', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[CLS] Chi tiết kết quả dịnh vụ CLS' , N'DR000005', NULL);
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ADMIN] Mở khóa CLS Chẩn đoán' , N'DR000006', NULL);

insert into AppConfigs (Id, [Key], [Label], [Value], IsDeleted ) Values (NEWID(), 'ORDERSETMASTER', N'Mã gói dịch vụ chia sẻ cho tất cả bác sĩ (MÃ + Mã gói)', 'DRALL', 0)
insert into AppConfigs (Id, [Key], [Label], [Value], IsDeleted ) Values (NEWID(), 'MAXQTYCHARGNITEM', N'Giới hạn 1 lần chỉ định của 1 dịch vụ trên 1 lần chỉ định', '200', 0)


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'SiblingData',N'SiblingData',N'IPDIAAUSIBLINGDATA',N'IPDIAAU',N'IPDIAAU',N'1',N'425',N'Text',N'',N'0',N'',N'CHILDREN', '1');
update  MasterDatas set code='IPDIAAUHYGIOTHANS' where Code='IPDIAAUHYGIOTH' and Form='IPDIAAU' and DataType='Text'
update  MasterDatas set code='IPDIAAUTEETOTHANS' where Code='IPDIAAUTEETOTH' and Form='IPDIAAU' and DataType='Text'
update  MasterDatas set code='IPDIAAUCOLOOTHANS' where Code='IPDIAAUCOLOOTH' and Form='IPDIAAU' and DataType='Text'
update MasterDatas set DataType = 'Checkbox' where code = 'IPDIAAUPIFCA1'
