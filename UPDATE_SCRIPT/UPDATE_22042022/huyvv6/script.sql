update MasterDatas
set DataType = 'Checkbox'
where code = 'IPDIAAUPIFCA1'
update MasterDatas
set DataType ='Radio'
where code = 'IPDIAAUCOLOOTH'
update MasterDatas
set Code ='IPDIAAUDIETSPECANS'
where code = 'IPDIAAUDIETSPEC' and DataType = 'Text'
update MasterDatas
set ViName =N'Báo ngay bác sĩ điều trị nếu điểm an thần <= -4 và/hoặc điểm đau >= 4',
EnName = N'Inform attending physician if sedation score <= -4 and/or pain score >= 4'
where code = 'IPDIAAUNEEDIDDAUAP'
update MasterDatas
set ViName =N'Hướng xử trí hỗ trợ',
EnName = N'Needs identified'
where code = 'IPDIAAUNEEDIDDAU'

update MasterDatas set [order] = 415 where code = 'IPDIAAUHYGIOTHANS'
update MasterDatas set [order] = 414 where code = 'IPDIAAUHYGIOTH'
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Báo bác sĩ điều trị',N'Inform attending physician',N'IPDIAAUNIHTANS',N'IPDIAAUNIHT',N'IPDIAAU',N'2',N'540',N'Text',N'',N'0',N'',N'', '1');
