select * from MasterDatas where Clinic like '%IPD_MEDICAL REPORT_VI%'
select * from MasterDatas where Form = 'VIIPDTRANDMR' order by [Order]

select * from Translations where VisitId = 'B13AC44D-9D10-4D7D-9798-00295DB1FCC7'


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCHC'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCHC'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCHCANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCHCANS'


-- ly do vao vien
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCLE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCLE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCLEANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCLEANS'

--ket qua can lam sang

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRRPT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRRPT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRRPTANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRRPTANS'

--chuẩn đoán

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'

--Phương pháp điều trị

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRTAP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRTAP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRTAPANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRTAPANS'

--Kế hoạch điều trị tiếp theo

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRFCP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRFCP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRFCPANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRFCPANS'


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh hiện tại',N'Tình trạng người bệnh hiện tại',N'IPDTRANDMRCAD',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANDMRCADANS',N'IPDTRANDMRCAD',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Current status',N'Current status',N'IPDTRANDMRCAD',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANDMRCADANS',N'IPDTRANDMRCAD',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_MEDICAL REPORT_EN', '1');

-- giay ra vien gioi tinh
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRGENANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRGENANS'



-- giay ra vien dia chi
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRADDANS'


-- giay ra vien chuan doan
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'



-- giay ra vien phương pháp điều trị

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRTAP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRTAP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRTAPANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRTAPANS'




Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ghi chú',N'Ghi chú',N'IPDMRPEHDTV',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'25',N'Label',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDMRPEHDTVANS',N'IPDMRPEHDTV',N'TRANSLATEVI',N'2',N'26',N'Text',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Note',N'Note',N'IPDMRPEHDTV',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'25',N'Label',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDMRPEHDTVANS',N'IPDMRPEHDTV',N'TRANSLATEEN',N'2',N'26',N'Text',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_EN', '1');


-- giay chuyen vien gioi tinh
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRGENANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRGENANS'



-- giay chuyen vien ly do nhap viee
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCHC'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCHC'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCHCANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCHCANS'


-- giấy chuyển viện Quá trình bệnh lý và diễn biến lâm sàng
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCLE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCLEANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRCLEANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRCHCANS'

-- giấy chuyển viện Kết quả cận lâm sàng
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRRPT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRRPT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRRPTANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRRPTANS'


-- giấy chuyển viện Chẩn đoán
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'

-- giấy chuyển viện Phương pháp điều trị

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRTAP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRTAP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRTAPANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRTAPANS'
-- giấy chuyển viện Các thuốc chính đã dùng

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRSIM'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRSIM'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRSIMANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRSIMANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh lúc chuyển viện',N'Tình trạng người bệnh lúc chuyển viện',N'TRANSLATESTATUSPATIENTTRANSFER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'21',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESTATUSPATIENTTRANSFERANS',N'TRANSLATESTATUSPATIENTTRANSFER',N'TRANSLATEVI',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Condition at discharge',N'Condition at discharge',N'TRANSLATESTATUSPATIENTTRANSFER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'21',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESTATUSPATIENTTRANSFERANS',N'TRANSLATESTATUSPATIENTTRANSFER',N'TRANSLATEEN',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');

-- giấy chuyển viện Kế hoạch điều trị tiếp theo

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRFCP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRFCP'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRFCPANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRFCPANS'




-- giấy chuyển tuyến Giới tính

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRGENANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRGENANS'


-- giấy chuyển tuyến Địa chỉ

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRADDANS'

-- giấy chuyển tuyến

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRADDANS'
-- giấy chuyển tuyến Quốc tịch

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRNAT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRNAT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRNATANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRNATANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nghề nghiệp',N'Nghề nghiệp',N'IPDTRANSFERJOB',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'7',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANSFERJOBANS',N'IPDTRANSFERJOB',N'TRANSLATEVI',N'2',N'8',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Job',N'Job',N'IPDTRANSFERJOB',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'7',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANSFERJOBANS',N'IPDTRANSFERJOB',N'TRANSLATEEN',N'2',N'8',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nơi làm việc',N'Nơi làm việc',N'IPDTRANSFERWORKPLACE',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANSFERWORKPLACEANS',N'IPDTRANSFERWORKPLACE',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Workplace',N'Workplace',N'IPDTRANSFERWORKPLACE',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANSFERWORKPLACEANS',N'IPDTRANSFERWORKPLACE',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kết quả xét nghiệm, cận lâm sàng',N'Kết quả xét nghiệm, cận lâm sàng',N'IPDTRANDMRRPT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'11',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANDMRRPTANS',N'IPDTRANDMRRPT',N'TRANSLATEVI',N'2',N'12',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kết quả xét nghiệm, cận lâm sàng',N'Kết quả xét nghiệm, cận lâm sàng',N'IPDTRANDMRRPT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'11',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANDMRRPTANS',N'IPDTRANDMRRPT',N'TRANSLATEEN',N'2',N'12',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');


-- giấy chuyển tuyến Chuẩn đoán

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI'
where Form = 'VIIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN'
where Form = 'ENIPDTRANDMR' and Code = 'IPDTRANDMRDIAANS'

Update MasterDatas
set Form = 'TRANSLATEEN'
where Form = 'ENIPDTRANDMR'

Update MasterDatas
set Form = 'TRANSLATEVI'
where Form = 'VIIPDTRANDMR'



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hướng điều trị:',N'Hướng điều trị:',N'IPDTRANDMRFCP',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'24',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANDMRFCPANS',N'IPDTRANDMRFCP',N'TRANSLATEVI',N'2',N'25',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hướng điều trị:',N'Hướng điều trị:',N'IPDTRANDMRFCP',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'24',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANDMRFCPANS',N'IPDTRANDMRFCPANS',N'TRANSLATEEN',N'2',N'25',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương tiện vận chuyển',N'Phương tiện vận chuyển',N'IPDTRANSFERTRANSPORT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANSFERTRANSPORTANS',N'IPDTRANSFERTRANSPORT',N'TRANSLATEVI',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương tiện vận chuyển',N'Phương tiện vận chuyển',N'IPDTRANSFERTRANSPORT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANSFERTRANSPORTANS',N'IPDTRANSFERTRANSPORT',N'TRANSLATEEN',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh, trình độ chuyên môn của người hộ tống',N'Họ tên, chức danh, trình độ chuyên môn của người hộ tống',N'IPDTRANSFERPERTRANSPORT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'28',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANSFERPERTRANSPORTANS',N'IPDTRANSFERPERTRANSPORT',N'TRANSLATEVI',N'2',N'29',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh, trình độ chuyên môn của người hộ tống',N'Họ tên, chức danh, trình độ chuyên môn của người hộ tống',N'IPDTRANSFERPERTRANSPORT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'28',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANSFERPERTRANSPORTANS',N'IPDTRANSFERPERTRANSPORT',N'TRANSLATEEN',N'2',N'29',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');



-- Giấy xác nhận thương tích ly do vao vien

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRCHC' 

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRCHC'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRCHCANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRCHCANS'


-- Giấy xác nhận thương tích chuẩn đoán

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRDIA' 

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRDIA'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRDIAANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRDIAANS'



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Điều trị',N'Điều trị',N'IPDTREATMENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'17',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTREATMENTANS',N'IPDTREATMENT',N'TRANSLATEVI',N'2',N'18',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Điều trị',N'Điều trị',N'IPDTREATMENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'17',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTREATMENTANS',N'IPDTREATMENT',N'TRANSLATEEN',N'2',N'18',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN', '1');


-- Giấy chứng nhận phẫu thuật giới tính

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRGEN' 

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRGENANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRGENANS'


-- Giấy chứng nhận phẫu thuật quốc tịch

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRNAT' 

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRNAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRNATANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRNATANS'

-- Giấy chứng nhận phẫu thuật địa chỉ

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRADD' 

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'IPDTRANDMRADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'IPDTRANDMRADDANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán trước mổ',N'Chẩn đoán trước mổ',N'IPDPREDIA',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'7',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDPREDIAANS',N'IPDPREDIA',N'TRANSLATEVI',N'2',N'8',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Preoperative diagnosis',N'Preoperative diagnosis',N'IPDPREDIA',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'7',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDPREDIAANS',N'IPDPREDIA',N'TRANSLATEEN',N'2',N'8',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán sau mổ',N'Chẩn đoán sau mổ',N'IPDPOSTOPDIA',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDPOSTOPDIAANS',N'IPDPOSTOPDIA',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Postoperative diagnosis',N'Postoperative diagnosis',N'IPDPOSTOPDIA',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDPOSTOPDIAANS',N'IPDPOSTOPDIA',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp phẫu thuật',N'Phương pháp phẫu thuật',N'IPDPROPER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'11',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDPROPERANS',N'IPDPROPER',N'TRANSLATEVI',N'2',N'12',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Procedure performed',N'Procedure performed',N'IPDPROPER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'11',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDPROPERANS',N'IPDPROPER',N'TRANSLATEEN',N'2',N'12',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp vô cảm',N'Phương pháp vô cảm',N'IPDMETANE',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'13',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDMETANEANS',N'IPDMETANE',N'TRANSLATEVI',N'2',N'14',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Method of anesthesia',N'Method of anesthesia',N'IPDMETANE',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'13',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDMETANEANS',N'IPDMETANE',N'TRANSLATEEN',N'2',N'14',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');




select * from TranslationDatas 
select * from MasterDatas where Code = 'OPDTRANSGENANS'

Update MasterDatas 
SET Code = 'TRANSLATEGEN'
Where Code in ('IPDTRANDMRGEN','OPDTRANSGEN') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEGENANS'
Where Code in ('IPDTRANDMRGENANS','OPDTRANSGENANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
----
Update MasterDatas 
SET Code = 'TRANSLATEADD'
Where Code in ('IPDTRANDMRADD','OPDTRANSADD') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEADDANS'
Where Code in ('IPDTRANDMRADDANS','OPDTRANSADDANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
----
Update MasterDatas 
SET Code = 'TRANSLATENAT'
Where Code in ('IPDTRANDMRNAT') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATENATANS'
Where Code in ('IPDTRANDMRNATANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
-----
Update MasterDatas 
SET Code = 'TRANSLATEREASON'
Where Code in ('IPDTRANDMRCHC') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEREASONANS'
Where Code in ('IPDTRANDMRCHCANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
-------------------- ly do den khám
Update MasterDatas 
SET Code = 'TRANSLATEREASONFORVISIT'
Where Code in ('OPDTRANSRFV') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEREASONFORVISITANS'
Where Code in ('OPDTRANSRFVANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
----------------------- bệnh sử 

Update MasterDatas 
SET Code = 'TRANSLATEHISTORYOFPRESENT'
Where Code in ('OPDTRANSHPI') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEHISTORYOFPRESENTANS'
Where Code in ('OPDTRANSHPIANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
----------------------- kham lam sang

Update MasterDatas 
SET Code = 'TRANSLATECLINTEXAM'
Where Code in ('OPDTRANSCEF') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATECLINTEXAMANS'
Where Code in ('OPDTRANSCEFANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
---------------------------

----------------------- bao cao y te Các xét nghiệm, thăm dò chính

Update MasterDatas 
SET Code = 'TRANSLATEPRINTEST'
Where Code in ('OPDTRANSPT0') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEPRINTESTANS'
Where Code in ('OPDTRANSPT0ANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
------------------- bao cao y te chuan doan
Update MasterDatas 
SET Code = 'TRANSLATEPDIAGNOSIS'
Where Code in ('IPDTRANDMRDIA','OPDTRANSDIA') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEPDIAGNOSISANS'
Where Code in ('IPDTRANDMRDIAANS','OPDTRANSDIAANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
------------------- bao cao y te huong dieu tri
Update MasterDatas 
SET Code = 'TRANSLATETREANTMENTPLAN'
Where Code in ('IPDTRANDMRFCP','OPDTRANSTP0') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATETREANTMENTPLANANS'
Where Code in ('IPDTRANDMRFCPANS','OPDTRANSTP0ANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
---------------------------
------------------- bao cao y te dan do
Update MasterDatas 
SET Code = 'TRANSLATERECOMMEND'
Where Code in ('OPDTRANSRFU') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATERECOMMENDANS'
Where Code in ('OPDTRANSRFUANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')


---------
Update MasterDatas 
SET Code = 'TRANSLATECLINEVOLU'
Where Code in ('IPDTRANDMRCLE') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATECLINEVOLUANS'
Where Code in ('IPDTRANDMRCLEANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')


-------- ket qua can lam sang
Update MasterDatas 
SET Code = 'TRANSLATERESULTPRATEST'
Where Code in ('IPDTRANDMRRPT') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATERESULTPRATESTANS'
Where Code in ('IPDTRANDMRRPTANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
------------ phuong phap dieu tri

Update MasterDatas 
SET Code = 'TRANSLATETREATMENTANDPROCEDURE'
Where Code in ('IPDTRANDMRTAP') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATETREATMENTANDPROCEDUREANS'
Where Code in ('IPDTRANDMRTAPANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
------------
------------ cac thuoc chinh da dung

Update MasterDatas 
SET Code = 'TRANSLATEDRUGUSED'
Where Code in ('IPDTRANDMRSIM') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEDRUGUSEDANS'
Where Code in ('IPDTRANDMRSIMANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
------------------ tinh trang benh nhan luc chuyen vien
Update MasterDatas 
SET Code = 'TRANSLATECURRENTSATATUS'
Where Code in ('IPDTRANSFERSTATUS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATECURRENTSATATUSANS'
Where Code in ('IPDTRANSFERSTATUSANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
---------------- nghe nghiep
Update MasterDatas 
SET Code = 'TRANSLATEJOB'
Where Code in ('IPDTRANSFERJOB') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEJOBANS'
Where Code in ('IPDTRANSFERJOBANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
---------------- noi lam viec
Update MasterDatas 
SET Code = 'TRANSLATEWORKPLACE'
Where Code in ('IPDTRANSFERWORKPLACE') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEWORKPLACEANS'
Where Code in ('IPDTRANSFERWORKPLACEANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
--------------------
Update MasterDatas 
SET Code = 'TRANSLATETREATANDDRUG'
Where Code in ('IPDTRANSFERTREATANDDRUG') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATETREATANDDRUGANS'
Where Code in ('IPDTRANSFERTREATANDDRUGANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
---------------------
Update MasterDatas 
SET Code = 'TRANSLATETRANSPORT'
Where Code in ('IPDTRANSFERTRANSPORT') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

-----------------
Update MasterDatas 
SET Code = 'TRANSLATEPERTRANSPORT'
Where Code in ('IPDTRANSFERPERTRANSPORT') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

Update MasterDatas 
SET Code = 'TRANSLATEPERTRANSPORTANS'
Where Code in ('IPDTRANSFERPERTRANSPORTANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
--------------- ghi chu
Update MasterDatas 
SET Code = 'TRANSLATENOTE'
Where Code in ('IPDMRPEHDTV') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
Update MasterDatas 
SET Code = 'TRANSLATENOTEANS'
Where Code in ('IPDMRPEHDTVANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
--------------- tinh trang nguoi benh luc chuyen vien
Update MasterDatas 
SET Code = 'TRANSLATEPERCURRENTPATIENT'
Where Code in ('IPDTRANDMRCAD') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
Update MasterDatas 
SET Code = 'TRANSLATEPERCURRENTPATIENTANS'
Where Code in ('IPDTRANDMRCADANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
---------------
----
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN' 

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'
------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD' 

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'
---------------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD' 

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

----------------
Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONFORVISIT' 

Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONFORVISIT'

Update MasterDatas
set Clinic =   'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONFORVISITANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONFORVISITANS'
---------------------
Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set Clinic =   'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

-----------------------------------------
Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINTEXAM'

Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINTEXAM'

Update MasterDatas
set Clinic =   'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINTEXAMANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINTEXAMANS'
--------------------------------
Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINTEST'

Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINTEST'

Update MasterDatas
set Clinic =   'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINTESTANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINTESTANS'
--------------------

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'
----------------------------------------

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'
-----------
------------------
Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMEND'


Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMEND'

Update MasterDatas
set Clinic =    'OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDANS'
--------------------

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


---------------------------------------


Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'


-----------------------
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lý do nhập viện',N'Lý do nhập viện',N'TRANSLATECHIEFCOM',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'5',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECHIEFCOMANS',N'TRANSLATECHIEFCOM',N'TRANSLATEVI',N'2',N'6',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chief complaint',N'Chief complaint',N'TRANSLATECHIEFCOM',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'5',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECHIEFCOMANS',N'TRANSLATECHIEFCOM',N'TRANSLATEEN',N'2',N'6',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN', '1');

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'
--------------------
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATESTANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATESTANS'
----------------------
Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDURE'
--------
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'
------------------------
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS'
--------------------------------

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS'


---------------

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFOLLOWUPCAREPLAN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFOLLOWUPCAREPLAN'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kế hoạch điều trị và chăm sóc tiếp theo',N'Kế hoạch điều trị và chăm sóc tiếp theo',N'TRANSLATECAREPLAN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'23',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECAREPLANANS',N'TRANSLATECAREPLAN',N'TRANSLATEVI',N'2',N'24',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Follow-up care plan',N'Follow-up care plan',N'TRANSLATECAREPLAN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'23',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECAREPLANANS',N'TRANSLATECAREPLAN',N'TRANSLATEEN',N'2',N'24',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN', '1');
-----------------

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

--------------

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'
-----------------------------------
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'
-----------------
Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =     'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'
---------------------------
Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'
-------------------------------

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACEANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACEANS'
--------------
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATESTANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATESTANS'
--------------------
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'
select * from Translations where VisitId = 'B13AC44D-9D10-4D7D-9798-00295DB1FCC7'



----------------

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATANDDRUG'

Update MasterDatas
set Clinic =     'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATANDDRUG'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATANDDRUGANS'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATANDDRUGANS'

----
Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECURRENTSATATUS'

Update MasterDatas
set Clinic =     'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECURRENTSATATUS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECURRENTSATATUSANS'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECURRENTSATATUSANS'

-------------------
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'
--------------------------------

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic =      'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORTANS'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORTANS'

------------------------

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =      'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


----

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =      'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =      'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

-----
Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'
---------
Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =      'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'
select * from Translations where VisitId = 'B13AC44D-9D10-4D7D-9798-00295DB1FCC7'
select * from MasterDatas where Code = 'OPDTRANSRFV'



update MasterDatas 
Set Form = 'TRANSLATEVI'
Where Form = 'VIOPDTRANMR0'

update MasterDatas 
Set Form = 'TRANSLATEEN'
Where Form = 'ENOPDTRANMR0'

select * from TranslationDatas
----------------

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN' 
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS'

------------

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN' 
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATESTANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN' 
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATESTANS'
------------


Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng bệnh nhân hiện tại',N'Tình trạng bệnh nhân hiện tại',N'TRANSLATECURSTATUS',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'15',N'Label',N'',N'0',N'',N'ED_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECURSTATUSANS',N'TRANSLATECURSTATUS',N'TRANSLATEVI',N'2',N'16',N'Text',N'',N'0',N'',N'ED_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Current status',N'Current status',N'TRANSLATECURSTATUS',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'15',N'Label',N'',N'0',N'',N'ED_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECURSTATUSANS',N'TRANSLATECURSTATUS',N'TRANSLATEEN',N'2',N'16',N'Text',N'',N'0',N'',N'ED_MEDICAL REPORT_EN', '1');

select * from EDs
select * from Translations where VisitId = '95741525-A326-4F6D-A6CE-02A53341EECE'

select * from MasterDatas where Code = 'IPDPOSTOPDIA'





Update MasterDatas
set Clinic =    'OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic =   'OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic =  'OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLANANS'

Update MasterDatas
set Clinic =  'OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLANANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lời dặn của thầy thuốc',N'Lời dặn của thầy thuốc',N'TRANSLATEDOCTORRECOMENDATION',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'25',N'Label',N'',N'0',N'',N'ED_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDOCTORRECOMENDATIONANS',N'TRANSLATEDOCTORRECOMENDATION',N'TRANSLATEVI',N'2',N'26',N'Text',N'',N'0',N'',N'ED_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Doctor recommendations',N'Doctor recommendations',N'TRANSLATEDOCTORRECOMENDATION',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'25',N'Label',N'',N'0',N'',N'ED_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDOCTORRECOMENDATIONANS',N'TRANSLATEDOCTORRECOMENDATION',N'TRANSLATEEN',N'2',N'26',N'Text',N'',N'0',N'',N'ED_MEDICAL REPORT_EN', '1');






Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

----
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN' 
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN' 
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'

----
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN' 
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN' 
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

-----
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'  
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI' 
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS'


Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATESTANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATESTANS'

-----

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'





Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'
-------------

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS'

----
Update MasterDatas
set Clinic =  'OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic =   'OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic =  'OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLANANS'

Update MasterDatas
set Clinic =   'OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLANANS'
----
Update MasterDatas
set Clinic =  'ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATION'

Update MasterDatas
set Clinic =   'ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATION'

Update MasterDatas
set Clinic =  'ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

Update MasterDatas
set Clinic =   'ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

select * from MasterDatas where Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

----


---
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

--------

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'

------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'
------------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'




Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'




Update MasterDatas
set Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENOTE'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENOTE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENOTEANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENOTEANS'



Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

-----

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'

----

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASON'
Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS'




Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATEST'
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATEST'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERESULTPRATESTANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERESULTPRATESTANS'
----
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'





Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDURE'
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

----

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSED'
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS'


select * from MasterDatas where Code = 'TRANSLATESTATUSPATIENTTRANSFERANS'



update MasterDatas
SET Code = 'TRANSLATEDISCHARGEHOS'
WHERE ViName Like N'Tình trạng khi ra viện' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )
update MasterDatas
SET Code = 'TRANSLATEDISCHARGEHOSANS'
WHERE ViName Like N'Tình trạng khi ra viện' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1'
Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS'

Update MasterDatas
set Clinic =   'OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS'

update MasterDatas
Set Code = 'TRANSLATESTATUSPATIENTTRANSFER1' 
where  Code = 'TRANSLATECURRENTPATIENT' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

select * from MasterDatas where Code = 'TRANSLATECURSTATUSANS' 
select * from MasterDatas where ViName like N'Tình trạng bệnh nhân lúc chuyển viện'


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng bệnh nhân lúc chuyển viện',N'Tình trạng bệnh nhân lúc chuyển viện',N'TRANSLATESTATUSPATIENTTRANSFER1,',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'21',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESTATUSPATIENTTRANSFER1ANS',N'TRANSLATESTATUSPATIENTTRANSFER1',N'TRANSLATEVI',N'2',N'22',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Current status',N'Current status',N'TRANSLATESTATUSPATIENTTRANSFER1',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'21',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESTATUSPATIENTTRANSFER1ANS',N'TRANSLATESTATUSPATIENTTRANSFER1',N'TRANSLATEEN',N'2',N'22',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN', '1');


---
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False',  N'Tình trạng thương tích lúc vào viện',N'Tình trạng thương tích lúc vào viện',N'TRANSLATESTATUSADMITTED',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'19',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESTATUSADMITTEDANS',N'TRANSLATESTATUSADMITTED',N'TRANSLATEEN',N'2',N'20',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False',  N'Tình trạng thương tích lúc vào viện',N'Tình trạng thương tích lúc vào viện',N'TRANSLATESTATUSADMITTED',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'19',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESTATUSADMITTEDANS',N'TRANSLATESTATUSADMITTED',N'TRANSLATEVI',N'2',N'20',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng thương tích lúc ra viện',N'Tình trạng thương tích lúc ra viện',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'21',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESTATUSINJURYDISCHARGEANS',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEVI',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng thương tích lúc ra viện',N'Tình trạng thương tích lúc ra viện',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'21',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESTATUSINJURYDISCHARGEANS',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEEN',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');

 

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLAN'
Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLANANS'

Update MasterDatas
set Clinic =   'OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLANANS'
--------------
Update MasterDatas
set Clinic = 'ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATION'
Update MasterDatas
set Clinic = 'ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATION'

Update MasterDatas
set Clinic = 'ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

Update MasterDatas
set Clinic =   'ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

select * from Translations where VisitId = '95741525-A326-4F6D-A6CE-02A53341EECE'

--------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


----
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'


---
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'
--
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'
Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'
----

----------

update MasterDatas
SET Code = 'TRANSLATESUBRESULT'
WHERE ViName Like N'Kết quả cận lâm sàng' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )
update MasterDatas
SET Code = 'TRANSLATESUBRESULT'
WHERE ViName Like N'Result of paraclinical tests' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )

update MasterDatas
SET Code = 'TRANSLATELABANDSUBRESULT'
WHERE ViName Like N'Kết quả xét nghiệm, cận lâm sàng' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESUBRESULTANS',N'TRANSLATESUBRESULT',N'TRANSLATEVI',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESUBRESULTANS',N'TRANSLATESUBRESULT',N'TRANSLATEEN',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATELABANDSUBRESULTANS',N'TRANSLATELABANDSUBRESULT',N'TRANSLATEVI',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATELABANDSUBRESULTANS',N'TRANSLATELABANDSUBRESULT',N'TRANSLATEEN',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN', '1');


--
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'






select *  from MasterDatas where Form = 'TRANSLATEVI' order by [Order]


delete  from MasterDatas where Code = 'OPDTRANSGEN' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSGENANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSADD' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSADDANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính 
delete  from MasterDatas where Code = 'OPDTRANSDIA' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSDIAANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete from MasterDatas where Code = 'TRANSLATERESULTPRATESTANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )
delete from MasterDatas where Code = 'TRANSLATESATATUSREFERALANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )
delete  from MasterDatas where Code = 'OPDTRANSTP0' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSTP0ANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'TRANSLATETREANTMENTPLANANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'TRANSLATESTATUSINJURYDISCHARGEANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSRFV' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSRFVANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSHPI' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSHPIANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSPT0' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSPT0ANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSRFU' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSRFUANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSCEF' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSCEFANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDENDTATUSANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDPOSTOPDIA' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDPOSTOPDIAANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDPREDIA' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDPREDIAANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDTRANDMRCOM' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDTRANDMRCOMANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDPROPER' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDPROPERANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDMETANE' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDMETANEANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDSTARTSTATUS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDSTARTSTATUSANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDTRANDMRPAN' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDTRANDMRPANANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSNAM' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'OPDTRANSNAMANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDTRANSFERHOSANDPER' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính
delete  from MasterDatas where Code = 'IPDTRANSFERHOSANDPERANS' and (Form = 'TRANSLATEVI' or Form = 'TRANSLATEEN') -- giới tính


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị',N'Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị',N'TRANSLATEMPTDRUGUSETREATMENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEMPTDRUGUSETREATMENTANS',N'TRANSLATEMPTDRUGUSETREATMENT',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị',N'Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị',N'TRANSLATEMPTDRUGUSETREATMENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEMPTDRUGUSETREATMENTANS',N'TRANSLATEMPTDRUGUSETREATMENT',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh lúc chuyển tuyến',N'Tình trạng người bệnh lúc chuyển tuyến',N'TRANSLATESATATUSREFERAL',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'22',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESATATUSREFERALANS',N'TRANSLATESATATUSREFERAL',N'TRANSLATEVI',N'2',N'23',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh lúc chuyển tuyến',N'Tình trạng người bệnh lúc chuyển tuyến',N'TRANSLATESATATUSREFERAL',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'22',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESATATUSREFERALANS',N'TRANSLATESATATUSREFERAL',N'TRANSLATEEN',N'2',N'23',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN', '1');




select * from MasterDatas where ViName like N'%Hướng điều trị%'

select * from MasterDatas where  Code = 'TRANSLATETREANTMENTPLANANS'
---

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETREANTMENTPLANANS',N'TRANSLATETREANTMENTPLAN',N'TRANSLATEVI',N'2',N'23',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETREANTMENTPLANANS',N'TRANSLATETREANTMENTPLAN',N'TRANSLATEEN',N'2',N'23',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN', '1');


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'


----------
-----
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORTANS'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORTANS'
-----


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_EMERGENCY CONFIRMATION_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_EMERGENCY CONFIRMATION_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_EMERGENCY CONFIRMATION_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_EMERGENCY CONFIRMATION_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'
-------------

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_EMERGENCY CONFIRMATION_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_EMERGENCY CONFIRMATION_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_EMERGENCY CONFIRMATION_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_EMERGENCY CONFIRMATION_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'
-------------
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán lúc vào cấp cứu',N'Chẩn đoán lúc vào cấp cứu',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'7',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDOCTORRECOMENDATIONANS',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEVI',N'2',N'8',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán lúc vào cấp cứu',N'Chẩn đoán lúc vào cấp cứu',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'7',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDIAGNOSISBEFOREMERANS',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEEN',N'2',N'8',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');





Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


select *  from MasterDatas where Clinic Like '%ED_EMERGENCY CONFIRMATION_EN%' order by [Order]
select * from Translations where VisitId = '95741525-A326-4F6D-A6CE-02A53341EECE'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'




Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACEANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACEANS'


--
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

--

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS'


-----

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Code = 'TRANSLATETREATMENT'
where Code = 'IPDTREATMENT' and (Form = 'TRANSLATEEN' or  Form = 'TRANSLATEVI') 
Update MasterDatas
set Code = 'TRANSLATETREATMENTANS'
where Code = 'IPDTREATMENTANS' and (Form = 'TRANSLATEEN' or  Form = 'TRANSLATEVI') 
-------------
Update MasterDatas
set Clinic =   'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set Clinic =    'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set Clinic =  'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANS'

Update MasterDatas
set Clinic =    'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANS'
-----


Update MasterDatas
set Clinic =   'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set Clinic =    'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set Clinic =  'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'

Update MasterDatas
set Clinic =    'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'
----
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


select * from Translations where VisitId = '28BC086E-462F-4938-A40E-0063EF6087AF'


---
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lý do khám bệnh',N'Lý do khám bệnh',N'TRANSLATEREASONFORVISIT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'4',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEREASONFORVISITANS',N'TRANSLATEREASONFORVISIT',N'TRANSLATEVI',N'2',N'5',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Reason for visit',N'Reason for visit',N'TRANSLATEREASONFORVISIT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'4',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEREASONFORVISITANS',N'TRANSLATEREASONFORVISIT',N'TRANSLATEEN',N'2',N'5',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh sử',N'Bệnh sử',N'TRANSLATEHISTORYOFPRESENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'5',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEHISTORYOFPRESENTANS',N'TRANSLATEHISTORYOFPRESENT',N'TRANSLATEVI',N'2',N'6',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'History of present illness',N'History of present illness',N'TRANSLATEHISTORYOFPRESENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'5',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEHISTORYOFPRESENTANS',N'TRANSLATEHISTORYOFPRESENT',N'TRANSLATEEN',N'2',N'6',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');




Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các xét nghiệm, thăm dò chính',N'Các xét nghiệm, thăm dò chính',N'TRANSLATEPRINCIPALTEST',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'7',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEPRINCIPALTESTANS',N'TRANSLATEPRINCIPALTEST',N'TRANSLATEVI',N'2',N'8',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các xét nghiệm, thăm dò chính',N'Các xét nghiệm, thăm dò chính',N'TRANSLATEPRINCIPALTEST',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'7',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEPRINCIPALTESTANS',N'TRANSLATEPRINCIPALTEST',N'TRANSLATEEN',N'2',N'8',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');


Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'

-----
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'





select * from MasterDatas where Code = 'OPDTRANSTP0'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dặn dò',N'Dặn dò',N'TRANSLATERECOMMENDATIONANDFOLLOWUP',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'10',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATERECOMMENDATIONANDFOLLOWUPANS',N'TRANSLATERECOMMENDATIONANDFOLLOWUP',N'TRANSLATEVI',N'2',N'11',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Recommendation & Follow-up',N'Recommendation & Follow-up',N'TRANSLATERECOMMENDATIONANDFOLLOWUP',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'10',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATERECOMMENDATIONANDFOLLOWUPANS',N'TRANSLATERECOMMENDATIONANDFOLLOWUP',N'TRANSLATEEN',N'2',N'11',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');



---
Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


select * from Translations where VisitId = '28BC086E-462F-4938-A40E-0063EF6087AF'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lí do chuyển viện',N'Lí do chuyển viện',N'TRANSLATEREASONTRANSFER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'10',N'Label',N'',N'0',N'',N'EOC_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEREASONTRANSFERANS',N'TRANSLATEREASONTRANSFER',N'TRANSLATEVI',N'2',N'11',N'Text',N'',N'0',N'',N'EOC_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Reason for transfer',N'Reason for transfer',N'TRANSLATEREASONTRANSFER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'10',N'Label',N'',N'0',N'',N'EOC_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEREASONTRANSFERANS',N'TRANSLATEREASONTRANSFER',N'TRANSLATEEN',N'2',N'11',N'Text',N'',N'0',N'',N'EOC_REFERRAL LETTER_EN', '1');


---




select * from Translations  where VisitId = '28BC086E-462F-4938-A40E-0063EF6087AF'
--
Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

---

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'
----

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'


Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'
----


Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám lâm sàng',N'Khám lâm sàng',N'TRANSLATEKLS',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'6',N'Label',N'',N'0',N'',N'EOC_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKLSANS',N'TRANSLATEKLS',N'TRANSLATEVI',N'2',N'7',N'Text',N'',N'0',N'',N'EOC_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám lâm sàng',N'Khám lâm sàng',N'TRANSLATEKLS',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'6',N'Label',N'',N'0',N'',N'EOC_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKLSANS',N'TRANSLATEKLS',N'TRANSLATEEN',N'2',N'7',N'Text',N'',N'0',N'',N'EOC_REFERRAL LETTER_EN', '1');


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'
---------

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'


------------

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'




Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESATATUSREFERAL'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESATATUSREFERAL'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESATATUSREFERALANS'

Update MasterDatas
set Clinic =     'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESATATUSREFERALANS'
-----
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'


-------------Update MasterDatas


select *  from MasterDatas where Form = 'TRANSLATEVI' order by [Order]
select * from MasterDatas where Code = 'IPDENDTATUSANS'


select * from TranslationDatas 

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán sau mổ',N'Chẩn đoán sau mổ',N'TRANSLATEDIAGNOSISAFTERSERGERY',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'6',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDIAGNOSISAFTERSERGERYANS',N'TRANSLATEDIAGNOSISAFTERSERGERY',N'TRANSLATEVI',N'2',N'7',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán sau mổ',N'Chẩn đoán sau mổ',N'TRANSLATEDIAGNOSISAFTERSERGERY',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'6',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDIAGNOSISAFTERSERGERYANS',N'TRANSLATEDIAGNOSISAFTERSERGERY',N'TRANSLATEEN',N'2',N'7',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán trước mổ',N'Chẩn đoán trước mổ',N'TRANSLATEPREDIAGNOSIS',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'7',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEPREDIAGNOSISANS',N'TRANSLATEPREDIAGNOSIS',N'TRANSLATEVI',N'2',N'8',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán trước mổ',N'Chẩn đoán trước mổ',N'TRANSLATEPREDIAGNOSIS',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'7',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEPREDIAGNOSISANS',N'TRANSLATEPREDIAGNOSIS',N'TRANSLATEEN',N'2',N'8',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh kèm theo',N'Bệnh kèm theo',N'TRANSLATECOMORBIDITIES',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'15',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECOMORBIDITIESANS',N'TRANSLATECOMORBIDITIES',N'TRANSLATEVI',N'2',N'16',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh kèm theo',N'Bệnh kèm theo',N'TRANSLATECOMORBIDITIES',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'15',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECOMORBIDITIESANS',N'TRANSLATECOMORBIDITIES',N'TRANSLATEEN',N'2',N'16',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp phẫu thuật',N'Phương pháp phẫu thuật',N'TRANSLATEPPPT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'11',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEPPPTANS',N'TRANSLATEPPPT',N'TRANSLATEVI',N'2',N'12',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp phẫu thuật',N'Phương pháp phẫu thuật',N'TRANSLATEPPPT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'11',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEPPPTANS',N'TRANSLATEPPPT',N'TRANSLATEEN',N'2',N'12',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp vô cảm',N'Phương pháp vô cảm',N'TRANSLATEPPVC',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'13',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEPPVCANS',N'TRANSLATEPPVC',N'TRANSLATEVI',N'2',N'14',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp vô cảm',N'Phương pháp vô cảm',N'TRANSLATEPPVC',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'13',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEPPVCANS',N'TRANSLATEPPVC',N'TRANSLATEEN',N'2',N'14',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ, tên người bệnh',N'Họ, tên người bệnh',N'TRANSLATEFIRSTLASTNAMEPATIENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'1',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEFIRSTLASTNAMEPATIENTANS',N'TRANSLATEFIRSTLASTNAMEPATIENT',N'TRANSLATEVI',N'2',N'2',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ, tên người bệnh',N'Họ, tên người bệnh',N'TRANSLATEFIRSTLASTNAMEPATIENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'1',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEFIRSTLASTNAMEPATIENTANS',N'TRANSLATEFIRSTLASTNAMEPATIENT',N'TRANSLATEEN',N'2',N'2',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ông (Bà)',N'Ông (Bà)',N'TRANSLATEGRANDPARENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'3',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEGRANDPARENTANS',N'TRANSLATEGRANDPARENT',N'TRANSLATEVI',N'2',N'4',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ông (Bà)',N'Ông (Bà)',N'TRANSLATEGRANDPARENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'3',N'Label',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEGRANDPARENTANS',N'TRANSLATEGRANDPARENT',N'TRANSLATEEN',N'2',N'4',N'Text',N'',N'0',N'',N'IPD_SURGERY CERTIFICATE_EN', '1');
delete from  MasterDatas where Code = 'TRANSLATEPERCURRENTPATIENTANS' and Clinic = '' and [Order] = '22' and (Form ='TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from  MasterDatas where Code = 'TRANSLATEDISCHARGEHOSANS' and (Form ='TRANSLATEEN' or Form = 'TRANSLATEVI')

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng khi ra viện',N'Tình trạng khi ra viện',N'TRANSLATECONDITIONDISCHARGE',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECONDITIONDISCHARGEANS',N'TRANSLATECONDITIONDISCHARGE',N'TRANSLATEVI',N'2',N'4',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng khi ra viện',N'Tình trạng khi ra viện',N'TRANSLATECONDITIONDISCHARGE',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'3',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECONDITIONDISCHARGEANS',N'TRANSLATECONDITIONDISCHARGE',N'TRANSLATEEN',N'2',N'4',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN', '1');


delete from MasterDatas where Code = 'TRANSLATEFOLLOWUPCAREPLANANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATEFOLLOWUPCAREPLAN' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kế hoạch điều trị tiếp theo',N'Kế hoạch điều trị tiếp theo',N'TRANSLATEFOLLOWUPCAREPLAN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'3',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEFOLLOWUPCAREPLANANS',N'TRANSLATEFOLLOWUPCAREPLAN',N'TRANSLATEVI',N'2',N'4',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Follow-up care plan',N'Follow-up care plan',N'TRANSLATEFOLLOWUPCAREPLAN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'3',N'Label',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEFOLLOWUPCAREPLANANS',N'TRANSLATEFOLLOWUPCAREPLAN',N'TRANSLATEEN',N'2',N'4',N'Text',N'',N'0',N'',N'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thăm khám',N'Thăm khám',N'TRANSLATEASSESSMENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'3',N'Label',N'',N'0',N'',N'ED_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEASSESSMENTANS',N'TRANSLATEASSESSMENT',N'TRANSLATEVI',N'2',N'4',N'Text',N'',N'0',N'',N'ED_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment',N'Assessment',N'TRANSLATEASSESSMENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'3',N'Label',N'',N'0',N'',N'ED_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEASSESSMENTANS',N'TRANSLATEASSESSMENT',N'TRANSLATEEN',N'2',N'4',N'Text',N'',N'0',N'',N'ED_REFERRAL LETTER_EN', '1');

select * from MasterDatas where Code = 'TRANSLATEDISCHARGEHOSANS'



update TranslationDatas
SET Code = 'TRANSLATEADDANS'
where Code = 'IPDTRANDMRADDANS'

update TranslationDatas
SET Code = 'TRANSLATECONDITIONDISCHARGEANS'
where Code = 'IPDTRANDMRCADANS' 

update TranslationDatas
SET Code = 'TRANSLATEREASONANS'
where Code = 'IPDTRANDMRCHCANS'


update TranslationDatas
SET Code = 'TRANSLATECLINEVOLUANS'
where Code = 'IPDTRANDMRCLEANS'



update TranslationDatas
SET Code = 'TRANSLATECOMORBIDITIESANS'
where Code = 'IPDTRANDMRCOMANS'


update TranslationDatas
SET Code = 'TRANSLATEPDIAGNOSISANS'
where Code = 'IPDTRANDMRDIAANS'


update TranslationDatas
SET Code = 'TRANSLATEFOLLOWUPCAREPLANANS'
where Code = 'IPDTRANDMRFCPANS'



update TranslationDatas
SET Code = 'TRANSLATEGENANS'
where Code = 'IPDTRANDMRGENANS'

update TranslationDatas
SET Code = 'TRANSLATENATANS'
where Code = 'IPDTRANDMRNATANS'


update TranslationDatas
SET Code = 'TRANSLATESUBRESULTANS'
where Code = 'IPDTRANDMRRPTANS'


update TranslationDatas
SET Code = 'TRANSLATEDRUGUSEDANS'
where Code = 'IPDTRANDMRSIMANS'


update TranslationDatas
SET Code = 'TRANSLATETREATMENTANDPROCEDUREANS'
where Code = 'IPDTRANDMRTAPANS'



update TranslationDatas
SET Code = 'TRANSLATEADDANS'
where Code = 'OPDTRANSADDANS'



update TranslationDatas
SET Code = 'TRANSLATEKLSANS'
where Code = 'OPDTRANSCEFANS'

update TranslationDatas
SET Code = 'TRANSLATEPDIAGNOSISANS'
where Code = 'OPDTRANSDIAANS'


update TranslationDatas
SET Code = 'TRANSLATEGENANS'
where Code = 'OPDTRANSGENANS'



update TranslationDatas
SET Code = 'TRANSLATEHISTORYOFPRESENTANS'
where Code = 'OPDTRANSHPIANS'



update TranslationDatas
SET Code = 'TRANSLATEPRINCIPALTESTANS'
where Code = 'OPDTRANSPT0ANS'




update TranslationDatas
SET Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'
where Code = 'OPDTRANSRFUANS'


update TranslationDatas
SET Code = 'TRANSLATEREASONFORVISITANS'
where Code = 'OPDTRANSRFVANS'

select * from MasterDatas where Form = 'TRANSLATEVI' 

update TranslationDatas
SET Code = 'TRANSLATETREANTMENTPLANANS'
where Code = 'OPDTRANSTP0ANS'




update TranslationDatas
SET Code = 'TRANSLATEASSESSMENTANSANS'
where Code = 'EDTRANSASSANS' 


update TranslationDatas
SET Code = 'TRANSLATEREASONANS'
where Code = 'EDTRANSCC0ANS'



update TranslationDatas
SET Code = 'TRANSLATECURSTATUSANS'
where Code = 'EDTRANSCS0ANS'

select * from MasterDatas where Form = 'TRANSLATEVI' 

update TranslationDatas
SET Code = 'TRANSLATEPDIAGNOSISANS'
where Code = 'EDTRANSDIAANS'



update TranslationDatas
SET Code = 'TRANSLATEDOCTORRECOMENDATIONANS'
where Code = 'EDTRANSDR0ANS'



update TranslationDatas
SET Code = 'TRANSLATECAREPLANANS'
where Code = 'EDTRANSFCPANS'
select * from MasterDatas where Form = 'TRANSLATEVI' 

update TranslationDatas
SET Code = 'TRANSLATEHISTORYOFPRESENTANS'
where Code = 'EDTRANSHISANS'


update TranslationDatas
SET Code = 'TRANSLATESUBRESULTANS'
where Code = 'EDTRANSRPTANS'



update TranslationDatas
SET Code = 'TRANSLATEDRUGUSEDANS'
where Code = 'EDTRANSSM0ANS'




update TranslationDatas
SET Code = 'TRANSLATETREATMENTANDPROCEDUREANS'
where Code = 'EDTRANSTAPANS'

select * from MasterDatas where Form = 'TRANSLATEVI' 
select * from MasterDatas where ViName like ''
select Code from TranslationDatas Group by Code

update MasterDatas
SET [Group] = 'TRANSLATEGEN'
where Code = 'TRANSLATEGENANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

update MasterDatas
SET [Group] = 'TRANSLATEADD'
where Code = 'TRANSLATEADDANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

update MasterDatas
SET [Group] = 'TRANSLATEPDIAGNOSIS'
where Code = 'TRANSLATEPDIAGNOSISANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

----

update MasterDatas
SET [Group] = 'TRANSLATEPERTRANSPORT'
where Code = 'TRANSLATEPERTRANSPORTANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')


update MasterDatas
SET [Group] = 'TRANSLATEWORKPLACE'
where Code = 'TRANSLATEWORKPLACEANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

update MasterDatas
SET [Group] = 'TRANSLATEJOB'
where Code = 'TRANSLATEJOBANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 


update MasterDatas
SET [Group] = 'TRANSLATENAT'
where Code = 'TRANSLATENATANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

Update MasterDatas
SET  Code = 'TRANSLATESTATUSPATIENTTRANSFER1'
where Code = 'TRANSLATESTATUSPATIENTTRANSFER1,' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

update MasterDatas
SET [Group] = 'TRANSLATESTATUSPATIENTTRANSFER1'
where Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

update MasterDatas
SET [Group] = 'TRANSLATEDRUGUSED'
where Code = 'TRANSLATEDRUGUSEDANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

update MasterDatas
SET [Group] = 'TRANSLATETREATMENTANDPROCEDURE'
where Code = 'TRANSLATETREATMENTANDPROCEDUREANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 
update MasterDatas
SET [Group] = 'TRANSLATECLINEVOLU'
where Code = 'TRANSLATECLINEVOLUANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 


update MasterDatas
SET [Group] = 'TRANSLATENOTE'
where Code = 'TRANSLATENOTEANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 


update MasterDatas
SET [Group] = 'TRANSLATEREASON'
where Code = 'TRANSLATEREASONANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

update MasterDatas
SET [Group] = 'TRANSLATETREATMENT'
where Code = 'TRANSLATETREATMENTANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

update MasterDatas
SET [Group] = 'TRANSLATESTATUSADMITTED'
where Code = 'TRANSLATESTATUSADMITTEDANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

select * from MasterDatas where Clinic like '%OPD_ILLNESS CERTIFICATE_EN%'

select * from MasterDatas where Clinic like '%OPD_ILLNESS CERTIFICATE_VI%'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEEN'

--
update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEEN'
---
update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATESUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATESUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATESUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATESUBRESULTANS' and  Form = 'TRANSLATEEN'

---
update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATESUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATESUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATESUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATESUBRESULTANS' and  Form = 'TRANSLATEEN'

----
update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEEN'
----
update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_INJURY CERTIFICATE_VI'
where Code = 'TRANSLATEGRANDPARENT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_INJURY CERTIFICATE_EN'
where Code = 'TRANSLATEGRANDPARENT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_INJURY CERTIFICATE_VI'
where Code = 'TRANSLATEGRANDPARENTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_INJURY CERTIFICATE_EN'
where Code = 'TRANSLATEGRANDPARENTANS' and  Form = 'TRANSLATEEN'

----
update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Code = 'TRANSLATETREANTMENTPLAN' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Code = 'TRANSLATETREANTMENTPLAN' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Code = 'TRANSLATETREANTMENTPLANANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Code = 'TRANSLATETREANTMENTPLANANS' and  Form = 'TRANSLATEEN'

----
update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEEN'


---
update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'VIPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEEN'


---
update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI', ViName = N'Hướng điều trị'
where Code = 'TRANSLATETREANTMENTPLAN' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN' , ViName = N'Hướng điều trị'
where Code = 'TRANSLATETREANTMENTPLAN' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI' , ViName = N'Hướng điều trị'
where Code = 'TRANSLATETREANTMENTPLANANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN' , ViName = N'Hướng điều trị'
where Code = 'TRANSLATETREANTMENTPLANANS' and  Form = 'TRANSLATEEN'



update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Code = 'TRANSLATEFOLLOWUPCAREPLAN' and  Form = 'TRANSLATEVI'


update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Code = 'TRANSLATEFOLLOWUPCAREPLANANS' and  Form = 'TRANSLATEVI'

---


---
update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEEN'


delete from MasterDatas where Code = 'TRANSLATCURRENTPATIENT' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATCURRENTPATIENTANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATEPERCURRENTPATIENT' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATEPERCURRENTPATIENTANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')

delete from MasterDatas where Code = 'TRANSLATESTATUSADMITTED' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATESTATUSADMITTEDANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATESTATUSINJURYDISCHARGE' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATESTATUSINJURYDISCHARGEANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 
-------

delete from MasterDatas where Code = 'TRANSLATEDIAGNOSISBEFOREMER' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
delete from MasterDatas where Code = 'TRANSLATEDIAGNOSISBEFOREMERANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
-------
delete from MasterDatas where Code = 'TRANSLATEDOCTORRECOMENDATIONANS' and [Group] = 'TRANSLATEDIAGNOSISBEFOREMER' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')


update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEEN'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh hiện tại',N'Tình trạng người bệnh hiện tại',N'TRANSLATCURRENTPATIENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATCURRENTPATIENTANS',N'TRANSLATCURRENTPATIENT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Current status',N'Current status',N'TRANSLATCURRENTPATIENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATCURRENTPATIENTANS',N'TRANSLATCURRENTPATIENT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_MEDICAL REPORT_EN', '1');




update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEEN'


--



update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEEN'

select * from MasterDatas where Code = 'TRANSLATEMPTDRUGUSETREATMENT'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEMPTDRUGUSETREATMENTANS' and  Form = 'TRANSLATEEN'





update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATESUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATESUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATESUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATESUBRESULTANS' and  Form = 'TRANSLATEEN'




Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng thương tích lúc vào viện',N'Tình trạng thương tích lúc vào viện',N'TRANSLATESTATUSADMITTED',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESTATUSADMITTEDANS',N'TRANSLATESTATUSADMITTED',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng thương tích lúc vào viện',N'Tình trạng thương tích lúc vào viện',N'TRANSLATESTATUSADMITTED',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESTATUSADMITTEDANS',N'TRANSLATESTATUSADMITTED',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng thương tích lúc ra viện',N'Tình trạng thương tích lúc ra viện',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESTATUSINJURYDISCHARGEANS',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng thương tích lúc ra viện',N'Tình trạng thương tích lúc ra viện',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESTATUSINJURYDISCHARGEANS',N'TRANSLATESTATUSINJURYDISCHARGE',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');


update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Code = 'TRANSLATELABANDSUBRESULTANS' and  Form = 'TRANSLATEEN'

---
update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEPERTRANSPORT' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEPERTRANSPORT' and  Form = 'TRANSLATEEN'

update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Code = 'TRANSLATEPERTRANSPORTANS' and  Form = 'TRANSLATEVI'

update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Code = 'TRANSLATEPERTRANSPORTANS' and  Form = 'TRANSLATEEN'

select * from Translations




Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán lúc vào cấp cứu',N'Chẩn đoán lúc vào cấp cứu',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDIAGNOSISBEFOREMERANS',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán lúc vào cấp cứu',N'Chẩn đoán lúc vào cấp cứu',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDIAGNOSISBEFOREMERANS',N'TRANSLATEDIAGNOSISBEFOREMER',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');

select * from MasterDatas where Clinic like '%ED_EMERGENCY CONFIRMATION_VI%'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

		select * from MasterDatas where Code = 'TRANSLATEKLS'
Update MasterDatas
set Clinic =  'EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic =    'EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic =  'EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'

Update MasterDatas
set Clinic =   'EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS'

---
select * from MasterDatas where Code = 'TRANSLATEGEN'
Update MasterDatas
set CreatedAt =  '2022-10-30 01:00:00.033',UpdatedAt =  '2022-10-30 01:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:00:00.033',UpdatedAt =  '2022-10-30 01:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:00:00.033',UpdatedAt =  '2022-10-30 01:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:00:00.033',UpdatedAt =  '2022-10-30 01:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'
-----
select * from MasterDatas where Code = 'TRANSLATEGEN'

Update MasterDatas
set CreatedAt =  '2022-10-30 06:10:00.033',UpdatedAt =  '2022-10-30 06:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:10:00.033',UpdatedAt =  '2022-10-30 06:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:10:00.033',UpdatedAt =  '2022-10-30 06:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:10:00.033',UpdatedAt =  '2022-10-30 06:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'
----
select * from MasterDatas where Code = 'TRANSLATEGEN'
Update MasterDatas
set CreatedAt =  '2022-11-01 10:55:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:30:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:30:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOMANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:30:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOMANS'

---
Update MasterDatas
set CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS'
-----
Update MasterDatas
set CreatedAt =  '2022-11-01 05:00:00.033',UpdatedAt =  '2022-11-01 10:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASON'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:00:00.033',UpdatedAt =  '2022-11-01 10:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:00:00.033',UpdatedAt =  '2022-11-01 10:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:00:00.033',UpdatedAt =  '2022-11-01 10:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS'

---

Update MasterDatas
set CreatedAt =  '2022-11-01 11:00:00.033',UpdatedAt =  '2022-11-01 11:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:00:00.033',UpdatedAt =  '2022-11-01 11:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:00:00.033',UpdatedAt =  '2022-11-01 11:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:00:00.033',UpdatedAt =  '2022-11-01 11:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'
----

Update MasterDatas
set CreatedAt =  '2022-11-01 12:00:00.033',UpdatedAt =  '2022-11-01 12:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESUBRESULT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:00:00.033',UpdatedAt =  '2022-11-01 12:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESUBRESULT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:00:00.033',UpdatedAt =  '2022-11-01 12:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESUBRESULTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:00:00.033',UpdatedAt =  '2022-11-01 12:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESUBRESULTANS'



Update MasterDatas
set CreatedAt =  '2022-11-01 13:00:00.033',UpdatedAt =  '2022-11-01 13:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:00:00.033',UpdatedAt =  '2022-11-01 13:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:00:00.033',UpdatedAt =  '2022-11-01 13:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:00:00.033',UpdatedAt =  '2022-11-01 13:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'

--------------
Update MasterDatas
set CreatedAt =  '2022-11-01 14:00:00.033',UpdatedAt =  '2022-11-01 14:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:00:00.033',UpdatedAt =  '2022-11-01 14:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:00:00.033',UpdatedAt =  '2022-11-01 14:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:00:00.033',UpdatedAt =  '2022-11-01 14:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

select * from MasterDatas where Form = 'TRANSLATEVI'
----
Update MasterDatas
set CreatedAt =  '2022-11-01 15:00:00.033',UpdatedAt =  '2022-11-01 15:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATCURRENTPATIENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 15:00:00.033',UpdatedAt =  '2022-11-01 15:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATCURRENTPATIENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 15:00:00.033',UpdatedAt =  '2022-11-01 15:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATCURRENTPATIENTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 15:00:00.033',UpdatedAt =  '2022-11-01 15:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATCURRENTPATIENTANS'

-----
----
Update MasterDatas
set CreatedAt =  '2022-11-01 16:00:00.033',UpdatedAt =  '2022-11-01 16:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:00:00.033',UpdatedAt =  '2022-11-01 16:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:00:00.033',UpdatedAt =  '2022-11-01 16:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLANANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:00:00.033',UpdatedAt =  '2022-11-01 16:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLANANS'
----
Update MasterDatas
set CreatedAt =  '2022-11-01 17:00:00.033',UpdatedAt =  '2022-11-01 17:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFOLLOWUPCAREPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 17:00:00.033',UpdatedAt =  '2022-11-01 17:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFOLLOWUPCAREPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 17:00:00.033',UpdatedAt =  '2022-11-01 17:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFOLLOWUPCAREPLANANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 17:00:00.033',UpdatedAt =  '2022-11-01 17:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFOLLOWUPCAREPLANANS'

---------------------------------------------------------------------------
Update MasterDatas
set CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'
------------------------------------------------------------------------
Update MasterDatas
set CreatedAt =  '2022-11-01 17:30:00.033',UpdatedAt =  '2022-11-01 17:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPREDIAGNOSIS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 17:30:00.033',UpdatedAt =  '2022-11-01 17:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPREDIAGNOSIS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 17:30:00.033',UpdatedAt =  '2022-11-01 17:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPREDIAGNOSISANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 17:30:00.033',UpdatedAt =  '2022-11-01 17:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPREDIAGNOSISANS'
--------------------------------------------------------------------
------------------------------------------------------------------------
Update MasterDatas
set CreatedAt =  '2022-11-01 18:00:00.033',UpdatedAt =  '2022-11-01 18:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERY'

Update MasterDatas
set  CreatedAt =  '2022-11-01 18:00:00.033',UpdatedAt =  '2022-11-01 18:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERY'

Update MasterDatas
set  CreatedAt =  '2022-11-01 18:00:00.033',UpdatedAt =  '2022-11-01 18:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERYANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 18:00:00.033',UpdatedAt =  '2022-11-01 18:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERYANS'
-------------------------------
Update MasterDatas
set CreatedAt =  '2022-11-01 18:30:00.033',UpdatedAt =  '2022-11-01 18:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPPT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 18:30:00.033',UpdatedAt =  '2022-11-01 18:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPPT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 18:30:00.033',UpdatedAt =  '2022-11-01 18:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPPTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 18:30:00.033',UpdatedAt =  '2022-11-01 18:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPPTANS'
------------------------------
Update MasterDatas
set CreatedAt =  '2022-11-01 19:00:00.033',UpdatedAt =  '2022-11-01 19:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPVC'

Update MasterDatas
set  CreatedAt =  '2022-11-01 19:00:00.033',UpdatedAt =  '2022-11-01 19:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPVC'

Update MasterDatas
set  CreatedAt =  '2022-11-01 19:00:00.033',UpdatedAt =  '2022-11-01 19:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPVCANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 19:00:00.033',UpdatedAt =  '2022-11-01 19:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPVCANS'
------------------------------------
Update MasterDatas
set CreatedAt =  '2022-10-30 06:15:00.033',UpdatedAt =  '2022-10-30 06:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:15:00.033',UpdatedAt =  '2022-10-30 06:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:15:00.033',UpdatedAt =  '2022-10-30 06:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:15:00.033',UpdatedAt =  '2022-10-30 06:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'
----------------
Update MasterDatas
set CreatedAt =  '2022-10-30 06:20:00.033',UpdatedAt =  '2022-10-30 06:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:20:00.033',UpdatedAt =  '2022-10-30 06:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:20:00.033',UpdatedAt =  '2022-10-30 06:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACEANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:20:00.033',UpdatedAt =  '2022-10-30 06:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACEANS'
------------
Update MasterDatas
set CreatedAt =  '2022-11-01 13:30:00.033',UpdatedAt =  '2022-11-01 13:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:30:00.033',UpdatedAt =  '2022-11-01 13:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:30:00.033',UpdatedAt =  '2022-11-01 13:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:30:00.033',UpdatedAt =  '2022-11-01 13:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANS'
-----
Update MasterDatas
set CreatedAt =  '2022-11-01 14:15:00.033',UpdatedAt =  '2022-11-01 14:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSADMITTED'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:15:00.033',UpdatedAt =  '2022-11-01 14:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSADMITTED'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:15:00.033',UpdatedAt =  '2022-11-01 14:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSADMITTEDANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:15:00.033',UpdatedAt =  '2022-11-01 14:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSADMITTEDANS'
-----

Update MasterDatas
set CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:30:00.033',UpdatedAt =  '2022-11-01 14:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'
------





Update MasterDatas
set CreatedAt =  '2022-11-01 13:15:00.033',UpdatedAt =  '2022-11-01 13:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECOMORBIDITIES'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:30:00.033',UpdatedAt =  '2022-11-01 13:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECOMORBIDITIES'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:30:00.033',UpdatedAt =  '2022-11-01 13:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECOMORBIDITIESANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:30:00.033',UpdatedAt =  '2022-11-01 13:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECOMORBIDITIESANS'

--------------------

Update MasterDatas
set CreatedAt =  '2022-11-01 14:45:00.033',UpdatedAt =  '2022-11-01 14:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECONDITIONDISCHARGE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:45:00.033',UpdatedAt =  '2022-11-01 14:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECONDITIONDISCHARGE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:45:00.033',UpdatedAt =  '2022-11-01 14:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECONDITIONDISCHARGEANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:45:00.033',UpdatedAt =  '2022-11-01 14:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECONDITIONDISCHARGEANS'
----------------------------


Update MasterDatas
set CreatedAt =  '2022-10-31 01:00:00.033',UpdatedAt =  '2022-10-31 01:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:00:00.033',UpdatedAt =  '2022-10-31 01:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:00:00.033',UpdatedAt =  '2022-10-31 01:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFERANS'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:00:00.033',UpdatedAt =  '2022-10-31 01:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFERANS'
--------------------------

Update MasterDatas
set CreatedAt =  '2022-10-31 02:00:00.033',UpdatedAt =  '2022-10-31 02:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set  CreatedAt =  '2022-10-31 02:00:00.033',UpdatedAt =  '2022-10-31 02:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set  CreatedAt =  '2022-10-31 02:00:00.033',UpdatedAt =  '2022-10-31 02:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'

Update MasterDatas
set  CreatedAt =  '2022-10-31 02:00:00.033',UpdatedAt =  '2022-10-31 02:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'
-----------------------


Update MasterDatas
set CreatedAt =  '2022-10-31 03:00:00.033',UpdatedAt =  '2022-10-31 03:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set  CreatedAt =  '2022-10-31 03:00:00.033',UpdatedAt =  '2022-10-31 03:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set  CreatedAt =  '2022-10-31 03:00:00.033',UpdatedAt =  '2022-10-31 03:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set  CreatedAt =  '2022-10-31 03:00:00.033',UpdatedAt =  '2022-10-31 03:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'
--------------------

---------------

Update MasterDatas
set CreatedAt =  '2022-10-31 04:00:00.033',UpdatedAt =  '2022-10-31 04:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDER'

Update MasterDatas
set  CreatedAt =  '2022-10-31 04:00:00.033',UpdatedAt =  '2022-10-31 04:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDER'

Update MasterDatas
set  CreatedAt =  '2022-10-31 04:00:00.033',UpdatedAt =  '2022-10-31 04:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDERANS'

Update MasterDatas
set  CreatedAt =  '2022-10-31 04:00:00.033',UpdatedAt =  '2022-10-31 04:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDERANS'
--------------------


Update MasterDatas
set CreatedAt =  '2022-11-01 16:45:00.033',UpdatedAt =  '2022-11-01 16:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:45:00.033',UpdatedAt =  '2022-11-01 16:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:45:00.033',UpdatedAt =  '2022-11-01 16:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFERANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:45:00.033',UpdatedAt =  '2022-11-01 16:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFERANS'

--------------------


Update MasterDatas
set CreatedAt =  '2022-10-30 01:10:00.033',UpdatedAt =  '2022-10-30 01:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:10:00.033',UpdatedAt =  '2022-10-30 01:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:10:00.033',UpdatedAt =  '2022-10-30 01:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVIANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:10:00.033',UpdatedAt =  '2022-10-30 01:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVIANS'

-------------------------------

Update MasterDatas
set CreatedAt =  '2022-11-01 13:45:00.033',UpdatedAt =  '2022-11-01 13:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESATATUSREFERAL'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:45:00.033',UpdatedAt =  '2022-11-01 13:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESATATUSREFERAL'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:45:00.033',UpdatedAt =  '2022-11-01 13:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESATATUSREFERALANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:45:00.033',UpdatedAt =  '2022-11-01 13:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESATATUSREFERALANS'
---



Update MasterDatas
set CreatedAt =  '2022-11-01 13:35:00.033',UpdatedAt =  '2022-11-01 13:35:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:35:00.033',UpdatedAt =  '2022-11-01 13:35:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:35:00.033',UpdatedAt =  '2022-11-01 13:35:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:35:00.033',UpdatedAt =  '2022-11-01 13:35:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

---------


Update MasterDatas
set CreatedAt =  '2022-11-01 14:10:00.033',UpdatedAt =  '2022-11-01 14:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSADMITTED'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:10:00.033',UpdatedAt =  '2022-11-01 14:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSADMITTED'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:10:00.033',UpdatedAt =  '2022-11-01 14:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSADMITTEDANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:10:00.033',UpdatedAt =  '2022-11-01 14:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSADMITTEDANS'
----


Update MasterDatas
set CreatedAt =  '2022-11-01 14:20:00.033',UpdatedAt =  '2022-11-01 14:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:20:00.033',UpdatedAt =  '2022-11-01 14:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:20:00.033',UpdatedAt =  '2022-11-01 14:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:20:00.033',UpdatedAt =  '2022-11-01 14:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'
----


Update MasterDatas
set CreatedAt =  '2022-11-01 15:58:00.033',UpdatedAt =  '2022-11-01 15:58:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1'

Update MasterDatas
set  CreatedAt =  '2022-11-01 15:58:00.033',UpdatedAt =  '2022-11-01 15:58:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1'

Update MasterDatas
set  CreatedAt =  '2022-11-01 15:58:00.033',UpdatedAt =  '2022-11-01 15:58:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 15:58:00.033',UpdatedAt =  '2022-11-01 15:58:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS'
--------------

Update MasterDatas
set CreatedAt =  '2022-11-01 14:58:00.033',UpdatedAt =  '2022-11-01 14:58:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECURSTATUS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:58:00.033',UpdatedAt =  '2022-11-01 14:58:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECURSTATUS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:58:00.033',UpdatedAt =  '2022-11-01 14:58:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECURSTATUSANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:58:00.033',UpdatedAt =  '2022-11-01 14:58:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECURSTATUSANS'
----------------
Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'



-----

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'




Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'


-------

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set Clinic =    'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOMANS'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOMANS'


-----------

Update MasterDatas
set CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'
------------

Update MasterDatas
set CreatedAt =  '2022-11-01 12:30:00.033',UpdatedAt =  '2022-11-01 12:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:30:00.033',UpdatedAt =  '2022-11-01 12:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:30:00.033',UpdatedAt =  '2022-11-01 12:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLSANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:30:00.033',UpdatedAt =  '2022-11-01 12:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLSANS'
------------

Update MasterDatas
set CreatedAt =  '2022-11-01 12:35:00.033',UpdatedAt =  '2022-11-01 12:35:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:35:00.033',UpdatedAt =  '2022-11-01 12:35:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:35:00.033',UpdatedAt =  '2022-11-01 12:35:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:35:00.033',UpdatedAt =  '2022-11-01 12:35:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTTANS'
-----
Update MasterDatas
set CreatedAt =  '2022-11-01 12:40:00.033',UpdatedAt =  '2022-11-01 12:40:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTM'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:40:00.033',UpdatedAt =  '2022-11-01 12:40:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTM'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:40:00.033',UpdatedAt =  '2022-11-01 12:40:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:40:00.033',UpdatedAt =  '2022-11-01 12:40:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMANS'
--------

Update MasterDatas
set CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKHH'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKHH'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKHHANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:45:00.033',UpdatedAt =  '2022-11-01 12:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKHHANS'

---

Update MasterDatas
set CreatedAt =  '2022-11-01 12:50:00.033',UpdatedAt =  '2022-11-01 12:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCK'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:50:00.033',UpdatedAt =  '2022-11-01 12:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCK'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:50:00.033',UpdatedAt =  '2022-11-01 12:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCKANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:50:00.033',UpdatedAt =  '2022-11-01 12:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCKANS'
----


Update MasterDatas
set CreatedAt =  '2022-11-01 12:55:00.033',UpdatedAt =  '2022-11-01 12:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:55:00.033',UpdatedAt =  '2022-11-01 12:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:55:00.033',UpdatedAt =  '2022-11-01 12:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPKANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:55:00.033',UpdatedAt =  '2022-11-01 12:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPKANS'
-----
Update MasterDatas
set CreatedAt =  '2022-11-01 12:53:00.033',UpdatedAt =  '2022-11-01 12:53:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMHH'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:53:00.033',UpdatedAt =  '2022-11-01 12:53:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMHH'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:53:00.033',UpdatedAt =  '2022-11-01 12:53:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMHHANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 12:53:00.033',UpdatedAt =  '2022-11-01 12:53:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMHHANS'

------
Update MasterDatas
set CreatedAt =  '2022-11-01 13:50:00.033',UpdatedAt =  '2022-11-01 13:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:50:00.033',UpdatedAt =  '2022-11-01 13:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:50:00.033',UpdatedAt =  '2022-11-01 13:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:50:00.033',UpdatedAt =  '2022-11-01 13:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'
------------
----------
Update MasterDatas
set CreatedAt =  '2022-11-01 13:55:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:50:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:55:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTERANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:55:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTERANS'
------------


Update MasterDatas
set CreatedAt =  '2022-11-01 13:58:00.033',UpdatedAt =  '2022-11-01 13:58:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:58:00.033',UpdatedAt =  '2022-11-01 13:58:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:58:00.033',UpdatedAt =  '2022-11-01 13:58:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:58:00.033',UpdatedAt =  '2022-11-01 13:58:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORTANS'
------------
---


Update MasterDatas
set CreatedAt =  '2022-10-30 01:15:00.033',UpdatedAt =  '2022-10-30 01:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENGIOI'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:15:00.033',UpdatedAt =  '2022-10-30 01:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENGIOI'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:15:00.033',UpdatedAt =  '2022-10-30 01:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENGIOIANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:15:00.033',UpdatedAt =  '2022-10-30 01:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENGIOIANS'
----

Update MasterDatas
set CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'
---

Update MasterDatas
set CreatedAt =  '2022-11-01 14:50:00.033',UpdatedAt =  '2022-11-01 14:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENOTE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:50:00.033',UpdatedAt =  '2022-11-01 14:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENOTE'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:50:00.033',UpdatedAt =  '2022-11-01 14:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENOTEANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 14:50:00.033',UpdatedAt =  '2022-11-01 14:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENOTEANS'
---



Update MasterDatas
set CreatedAt =  '2022-11-01 06:10:00.033',UpdatedAt =  '2022-11-01 06:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONFORVISIT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:10:00.033',UpdatedAt =  '2022-11-01 06:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONFORVISIT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:10:00.033',UpdatedAt =  '2022-11-01 06:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONFORVISITANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:10:00.033',UpdatedAt =  '2022-11-01 06:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONFORVISITANS'
---

Update MasterDatas
set CreatedAt =  '2022-11-01 06:13:00.033',UpdatedAt =  '2022-11-01 06:13:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:13:00.033',UpdatedAt =  '2022-11-01 06:13:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:13:00.033',UpdatedAt =  '2022-11-01 06:13:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:13:00.033',UpdatedAt =  '2022-11-01 06:13:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

------------------------
Update MasterDatas
set CreatedAt =  '2022-11-01 11:25:00.033',UpdatedAt =  '2022-11-01 11:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:25:00.033',UpdatedAt =  '2022-11-01 11:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:25:00.033',UpdatedAt =  '2022-11-01 11:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:25:00.033',UpdatedAt =  '2022-11-01 11:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS'
-----
Update MasterDatas
set CreatedAt =  '2022-11-01 11:05:00.033',UpdatedAt =  '2022-11-01 11:05:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:05:00.033',UpdatedAt =  '2022-11-01 11:05:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:05:00.033',UpdatedAt =  '2022-11-01 11:05:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 11:05:00.033',UpdatedAt =  '2022-11-01 11:05:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYANS'
----
Update MasterDatas
set CreatedAt =  '2022-11-01 06:45:00.033',UpdatedAt =  '2022-11-01 06:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINCIPALTEST'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:45:00.033',UpdatedAt =  '2022-11-01 06:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINCIPALTEST'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:45:00.033',UpdatedAt =  '2022-11-01 06:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINCIPALTESTANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 06:45:00.033',UpdatedAt =  '2022-11-01 06:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINCIPALTESTANS'
-----
-------

-----
Update MasterDatas
set CreatedAt =  '2022-11-01 13:55:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUP'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:55:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUP'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:55:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:55:00.033',UpdatedAt =  '2022-11-01 13:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'
-----

-----
Update MasterDatas
set CreatedAt =  '2022-11-01 16:10:00.033',UpdatedAt =  '2022-11-01 16:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATION'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:10:00.033',UpdatedAt =  '2022-11-01 16:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATION'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:10:00.033',UpdatedAt =  '2022-11-01 16:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 16:10:00.033',UpdatedAt =  '2022-11-01 16:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'


--------------------
Update MasterDatas
set CreatedAt =  '2022-11-11 11:10:00.033',UpdatedAt =  '2022-11-11 11:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDIAGNOSISBEFOREMER'

Update MasterDatas
set  CreatedAt =  '2022-11-11 11:10:00.033',UpdatedAt =  '2022-11-11 11:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDIAGNOSISBEFOREMER'

Update MasterDatas
set  CreatedAt =  '2022-11-11 11:10:00.033',UpdatedAt =  '2022-11-11 11:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDIAGNOSISBEFOREMERANS'

Update MasterDatas
set  CreatedAt =  '2022-11-11 11:10:00.033',UpdatedAt =  '2022-11-11 11:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDIAGNOSISBEFOREMERANS'


Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS'
--------------

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =    'IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'



Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

----
select * from MasterDatas where Form = 'TRANSLATEVI'

select * from MasterDatas where Code = 'TRANSLATESTATUSPATIENTTRANSFER'
Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =    'IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_SURGERY CERTIFICATE_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_SURGERY CERTIFICATE_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'

------
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dấu hiệu lâm sàng',N'Dấu hiệu lâm sàng',N'TRANSLATEDHLS',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDHLSANS',N'TRANSLATEDHLS',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Current status',N'Current status',N'TRANSLATEDHLS',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDHLSANS',N'TRANSLATEDHLS',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');

select * from MasterDatas where Form = 'TRANSLATEVI'

select * from MasterDatas where Code = 'TRANSLATESTATUSPATIENTTRANSFER'
Update MasterDatas
set Clinic =  'IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'
----------------------------------------------


Update MasterDatas
set EnName = 'Postoperative diagnosis'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERY'

Update MasterDatas
set EnName = 'Postoperative diagnosis'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERY'

Update MasterDatas
set EnName = 'Postoperative diagnosis'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERYANS'

Update MasterDatas
set EnName = 'Postoperative diagnosis'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDIAGNOSISAFTERSERGERYANS'

-----------

Update MasterDatas
set EnName = 'Preoperative diagnosis'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPREDIAGNOSIS'

Update MasterDatas
set EnName = 'Preoperative diagnosis'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPREDIAGNOSIS'

Update MasterDatas
set EnName = 'Preoperative diagnosis'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPREDIAGNOSISANS'

Update MasterDatas
set EnName = 'Preoperative diagnosis'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPREDIAGNOSISANS'


----

-----------

Update MasterDatas
set EnName = 'Procedure performed'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPPT'

Update MasterDatas
set EnName = 'Procedure performed'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPPT'

Update MasterDatas
set EnName = 'Procedure performed'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPPTANS'

Update MasterDatas
set EnName = 'Procedure performed'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPPTANS'
----------


Update MasterDatas
set EnName = 'Method of anesthesia'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPVC'

Update MasterDatas
set EnName = 'Method of anesthesia'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPVC'

Update MasterDatas
set EnName = 'Method of anesthesia'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPPVCANS'

Update MasterDatas
set EnName = 'Method of anesthesia'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPPVCANS'

select * from MasterDatas where Form = 'TRANSLATEVI'

--------

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFIRSTLASTNAMEPATIENT'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFIRSTLASTNAMEPATIENT'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFIRSTLASTNAMEPATIENTANS'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFIRSTLASTNAMEPATIENTANS'


---
select * from MasterDatas where Form = 'TRANSLATEVI'
Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'

select * from MasterDatas where Code = 'TRANSLATEFOLLOWUPCAREPLAN'
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFOLLOWUPCAREPLAN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFOLLOWUPCAREPLAN'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFOLLOWUPCAREPLANANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFOLLOWUPCAREPLANANS'

---
Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLANANS'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLANANS'

----

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'

--------------
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'

------------------

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACEANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACEANS'
------------

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'
---------------



Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'


-----------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'


----
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'


----
Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGRANDPARENT'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGRANDPARENT'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGRANDPARENTANS'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGRANDPARENTANS'

----
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESUBRESULT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESUBRESULT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESUBRESULTANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESUBRESULTANS'

Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATCURRENTPATIENTANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATCURRENTPATIENTANS'



Update MasterDatas
set DefaultValue = 'TRANS1'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECONDITIONDISCHARGEANS'
Update MasterDatas
set DefaultValue = 'TRANS1'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECONDITIONDISCHARGEANS'

Update MasterDatas
set DefaultValue = 'TRANS2'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'
Update MasterDatas
set DefaultValue = 'TRANS2'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'


Update MasterDatas
set DefaultValue = 'TRANS3'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'
Update MasterDatas
set DefaultValue = 'TRANS3'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'


Update MasterDatas
set DefaultValue = 'TRANS3'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANS'
Update MasterDatas
set DefaultValue = 'TRANS3'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANS'


Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLANANS'
Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLANANS' 

Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'
Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS' 

Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENOTEANS'
Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENOTEANS'

Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEFOLLOWUPCAREPLANANS'
Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEFOLLOWUPCAREPLANANS'  

Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'
Update MasterDatas
set DefaultValue = 'TRANS4'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS' 


Update MasterDatas
set DefaultValue = 'TRANS5'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'
Update MasterDatas
set DefaultValue = 'TRANS5'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS' 

Update MasterDatas
set DefaultValue = 'TRANS5'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOMANS'
Update MasterDatas
set DefaultValue = 'TRANS5'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOMANS' 

Update MasterDatas
set DefaultValue = 'TRANS6'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'
Update MasterDatas
set DefaultValue = 'TRANS6'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS' 


Update MasterDatas
set DefaultValue = 'TRANS7'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESUBRESULTANS'
Update MasterDatas
set DefaultValue = 'TRANS7'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESUBRESULTANS' 

Update MasterDatas
set DefaultValue = 'TRANS7'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'
Update MasterDatas
set DefaultValue = 'TRANS7'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS' 

Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECONDITIONDISCHARGEANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECONDITIONDISCHARGEANS' 


Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECONDITIONDISCHARGEANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECONDITIONDISCHARGEANS' 

Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECURSTATUSANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECURSTATUSANS' 

Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFERANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFERANS' 

Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESATATUSREFERALANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESATATUSREFERALANS' 


Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSINJURYDISCHARGEANS' 




Update MasterDatas
set DefaultValue = 'TRANS9'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'
Update MasterDatas
set DefaultValue = 'TRANS9'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS' 

Update MasterDatas
set DefaultValue = 'TRANS9'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVIANS'
Update MasterDatas
set DefaultValue = 'TRANS9'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVIANS' 

Update MasterDatas
set DefaultValue = 'TRANS10'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'
Update MasterDatas
set DefaultValue = 'TRANS10'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS' 

Update MasterDatas
set DefaultValue = 'TRANS10'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'
Update MasterDatas
set DefaultValue = 'TRANS10'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS' 

Update MasterDatas
set DefaultValue = 'TRANS11'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'
Update MasterDatas
set DefaultValue = 'TRANS11'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS' 

Update MasterDatas
set DefaultValue = 'TRANS12'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'
Update MasterDatas
set DefaultValue = 'TRANS12'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS' 


Update MasterDatas
set DefaultValue = 'TRANS13'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'
Update MasterDatas
set DefaultValue = 'TRANS13'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS' 


Update MasterDatas
set DefaultValue = 'TRANS13'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTERANS'
Update MasterDatas
set DefaultValue = 'TRANS13'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTERANS' 


Update MasterDatas
set DefaultValue = 'TRANS14'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'
Update MasterDatas
set DefaultValue = 'TRANS14'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENTANS' 


Update MasterDatas
set DefaultValue = 'TRANS15'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'
Update MasterDatas
set DefaultValue = 'TRANS15'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS' 


Update MasterDatas
set DefaultValue = 'TRANS15'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENOTEANS'
Update MasterDatas
set DefaultValue = 'TRANS15'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENOTEANS' 

Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS'
Update MasterDatas
set DefaultValue = 'TRANS8'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS' 


Update MasterDatas
set DefaultValue = 'TRANS7'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINCIPALTESTANS'
Update MasterDatas
set DefaultValue = 'TRANS7'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINCIPALTESTANS' 

Update MasterDatas
set DefaultValue = 'TRANS16'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'
Update MasterDatas
set DefaultValue = 'TRANS16'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS' 


Update MasterDatas
set DefaultValue = 'TRANS5'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONFORVISITANS'
Update MasterDatas
set DefaultValue = 'TRANS5'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONFORVISITANS' 


Update MasterDatas 
set ViName = N'Lý do chuyển viện', EnName =  N'Reason for transfer'
where Form = 'TRANSLATEVI' and Code ='TRANSLATEREASONTRANSFER'

-----------------------

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFERANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFERANS'

---


Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'


------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh người đưa đi',N'Họ tên, chức danh người đưa đi',N'TRANSLATESENDER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESENDERANS',N'TRANSLATESENDER',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Name and title of medical escort',N'Name and title of medical escort',N'TRANSLATESENDER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESENDERANS',N'TRANSLATESENDER',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh người được liên hệ để nhận bệnh nhân',N'Họ tên, chức danh người được liên hệ để nhận bệnh nhân',N'TRANSLATENAMEMETHODCONTACTED',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATENAMEMETHODCONTACTEDANS',N'TRANSLATENAMEMETHODCONTACTED',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Name and title of staff being contacted from receiving hospital',N'Name and title of staff being contacted from receiving hospital',N'TRANSLATENAMEMETHODCONTACTED',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATENAMEMETHODCONTACTEDANS',N'TRANSLATENAMEMETHODCONTACTED',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nam/Nữ',N'Nam/Nữ',N'TRANSLATEGENVI',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEGENVIANS',N'TRANSLATEGENVI',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nam/Nữ',N'Nam/Nữ',N'TRANSLATEGENVI',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEGENVIANS',N'TRANSLATEGENVI',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương tiện vận chuyển',N'Phương tiện vận chuyển',N'TRANSLATETRANSPORT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETRANSPORTANS',N'TRANSLATETRANSPORT',N'TRANSLATEVI',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương tiện vận chuyển',N'Phương tiện vận chuyển',N'TRANSLATETRANSPORT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETRANSPORTANS',N'TRANSLATETRANSPORT',N'TRANSLATEEN',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương tiện vận chuyển',N'Phương tiện vận chuyển',N'TRANSLATETRANSPORTFOOTER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETRANSPORTFOOTERANS',N'TRANSLATETRANSPORTFOOTER',N'TRANSLATEVI',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương tiện vận chuyển',N'Phương tiện vận chuyển',N'TRANSLATETRANSPORTFOOTER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETRANSPORTFOOTERANS',N'TRANSLATETRANSPORTFOOTER',N'TRANSLATEEN',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Update MasterDatas
Set EnName = 'Occupation'
WHERE Code = 'TRANSLATEJOB' and  Form = 'TRANSLATEEN'




Update MasterDatas
set Clinic = 'OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic = 'OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'


Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'
--------------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTERANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTERANS'


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Địa chỉ',N'Địa chỉ',N'TRANSLATEADDFOOTER',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEADDFOOTERANS',N'TRANSLATEADDFOOTER',N'TRANSLATEVI',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Địa chỉ',N'Địa chỉ',N'TRANSLATEADDFOOTER',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'26',N'Label',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEADDFOOTERANS',N'TRANSLATEADDFOOTER',N'TRANSLATEEN',N'2',N'27',N'Text',N'',N'0',N'',N'IPD_DISCHARGE CERTIFICATE_EN', '1');


--------------
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

------------------
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'


---------------

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'


----------

---------------

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONFORVISIT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONFORVISIT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONFORVISITANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONFORVISITANS'

-----------------

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

------------
Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS'


------------------

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINCIPALTEST'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINCIPALTEST'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINCIPALTESTANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINCIPALTESTANS'
----------------


Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUP'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUP'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'



-------------------

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFERANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFERANS'

-------------------------

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'


----------------


Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'

-----------------------------

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORTANS'


---------------------------------------------------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLSANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLSANS'


------------------------

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'

----------------------------------------

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORTANS'
--------------------------------------------------------------------


Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDER'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDER'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,IPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDERANS'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,IPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDERANS'
---------------


Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'


-----------------



Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'



-----------


Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVIANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVIANS'




-------------------


Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'


---
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'

----
Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'


----
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLSANS'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLSANS'
------


----
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'



-----


Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'


------------

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATCURRENTPATIENT'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATCURRENTPATIENT'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATCURRENTPATIENTANS'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATCURRENTPATIENTANS'


---------------
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'


------------------

---------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'


-----------------------
---------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'


-----------


---------------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'



------

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'



----------------


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS'




------
Update MasterDatas
set Clinic = 'ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATION'

Update MasterDatas
set Clinic = 'ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATION'

Update MasterDatas
set Clinic = 'ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

Update MasterDatas
set Clinic = 'ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDOCTORRECOMENDATIONANS'

-------------


------
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECOMORBIDITIES'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECOMORBIDITIES'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECOMORBIDITIESANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECOMORBIDITIESANS'


-------------------------

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

-------------------------

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic ='IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'


---------


Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'



----

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACEANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACEANS'


--------
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


-----

--------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVIANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVIANS'





-------

--------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLSANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLSANS'

--------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'



----------

--------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTERANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTERANS'


-------


Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFERANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFERANS'



-----

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'

-----

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'


----

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDERANS'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDERANS'


----

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'


----

----

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'




----------------


Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

------------

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEASSESSMENT'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEASSESSMENT'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEASSESSMENTANS'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEASSESSMENTANS'

------------

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Triệu chứng cấp cứu điển hình',N'Triệu chứng cấp cứu điển hình',N'TRANSLATETCCCDH',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETCCCDHANS',N'TRANSLATETCCCDH',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Triệu chứng cấp cứu điển hình',N'Triệu chứng cấp cứu điển hình',N'TRANSLATETCCCDH',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETCCCDHANS',N'TRANSLATETCCCDH',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');
-------------------------------------

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVIANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVIANS'
-------



Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOB'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEJOBANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEJOBANS'


-----
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACE'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEWORKPLACEANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEWORKPLACEANS'



------

-----
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'


---

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASON'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_INJURY CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_INJURY CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONANS'


----


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'

------

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


---------

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

----

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS'


---
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'



-----
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'



-------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVIANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVIANS'
-------------------
Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


----
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEMPTDRUGUSETREATMENTANS'

----

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic ='IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'
-----------



Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'


-----------

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTERANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTERANS'


----

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPERTRANSPORTANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPERTRANSPORTANS'


----

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEREASONTRANSFERANS'

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEREASONTRANSFERANS'


----


Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'

---
Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'


--
Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDER'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDER'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDERANS'

Update MasterDatas
set Clinic =   'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDERANS'


-----

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set Clinic = 'IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOMANS'

Update MasterDatas
set Clinic =  'IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOMANS'

---


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'

----

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'

Update MasterDatas
set Clinic =  'OPD_MEDICAL REPORT_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS'


-----

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESUBRESULT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESUBRESULT'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESUBRESULTANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESUBRESULTANS'
------------



Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_INJURY CERTIFICATE_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_INJURY CERTIFICATE_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'


------

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDURE'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANDPROCEDUREANS'


----


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSED'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDRUGUSEDANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDRUGUSEDANS'

----


Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS'

Update MasterDatas
set Clinic =  'OPD_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESTATUSPATIENTTRANSFER1ANS'


----

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLAN'

Update MasterDatas
set Clinic = 'IPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECAREPLANANS'

Update MasterDatas
set Clinic =  'IPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECAREPLANANS'

----

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGEN'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_REFERRAL LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENANS'

Update MasterDatas
set Clinic =  'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_REFERRAL LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENANS'


-------


Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set Clinic = 'IPD_SURGERY CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set Clinic =  'IPD_SURGERY CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'

----------
Update MasterDatas 
SET ViName = N'Quá trình bệnh lý và diễn biến lâm sàng' , EnName = N'Quá trình bệnh lý và diễn biến lâm sàng'
WHERE Code = 'TRANSLATECLINEVOLU' and Form = 'TRANSLATEVI'
Update MasterDatas 
SET ViName = N'Quá trình bệnh lý và diễn biến lâm sàng' , EnName = N'Quá trình bệnh lý và diễn biến lâm sàng'
WHERE Code = 'TRANSLATECLINEVOLU' and Form = 'TRANSLATEEN'


Update MasterDatas 
SET ViName = 'Drugs used' , EnName = 'Drugs used'
WHERE Code = 'TRANSLATEDRUGUSED' and Form = 'TRANSLATEEN'
-------


Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULT'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATELABANDSUBRESULTANS'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATELABANDSUBRESULTANS'

----------
Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'
---


Update MasterDatas
set Clinic = 'ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEASSESSMENT'

Update MasterDatas
set Clinic = 'ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEASSESSMENT'

Update MasterDatas
set Clinic =  'ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEASSESSMENTANS'

Update MasterDatas
set Clinic =    'ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEASSESSMENTANS'



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giới',N'Giới',N'TRANSLATEGENGIOI',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEGENGIOIANS',N'TRANSLATEGENGIOI',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giới',N'Giới',N'TRANSLATEGENGIOI',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEGENGIOIANS',N'TRANSLATEGENGIOI',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_EMERGENCY CONFIRMATION_EN', '1');
-------------------------------------

Update MasterDatas 
SET ViName = N'Transportation method' , EnName = N'Transportation method'
WHERE Code = 'TRANSLATETRANSPORT' and Form = 'TRANSLATEEN'
Update MasterDatas 
SET ViName = 'Transportation method' , EnName = 'Transportation method'
WHERE Code = 'TRANSLATETRANSPORTFOOTER' and Form = 'TRANSLATEEN'
-------
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh sử',N'Bệnh sử',N'TRANSLATEHISTORY',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEHISTORYANS',N'TRANSLATEHISTORY',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'History',N'History',N'TRANSLATEHISTORY',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEHISTORYANS',N'TRANSLATEHISTORY',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_REFERRAL LETTER_EN', '1');
-------------------------------------


Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic = 'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas 
SET ViName = N'Clinical examination and findings' , EnName = N'Clinical examination and findings'
WHERE Code = 'TRANSLATEKLS' and Form = 'TRANSLATEEN'


Update MasterDatas 
SET ViName = 'Principal tests' , EnName = 'Principal tests'
WHERE Code = 'TRANSLATEPRINCIPALTEST' and Form = 'TRANSLATEEN'



--------



Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set Clinic =  'OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYANS'

Update MasterDatas
set Clinic =    'OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYANS'




Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,OPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic =    'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,OPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'


----
Update MasterDatas
set Clinic = 'ED_EMERGENCY CONFIRMATION_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'ED_EMERGENCY CONFIRMATION_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'ED_EMERGENCY CONFIRMATION_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI,IPD_SURGERY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =    'ED_EMERGENCY CONFIRMATION_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN,IPD_SURGERY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

---

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám toàn thân',N'Khám toàn thân',N'TRANSLATEKTT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKTTANS',N'TRANSLATEKTT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Full physical examination',N'Full physical examination',N'TRANSLATEKTT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKTTANS',N'TRANSLATEKTT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám tim mạch',N'Khám tim mạch',N'TRANSLATEKTM',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKTMANS',N'TRANSLATEKTM',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cadiovascular examination',N'Cadiovascular examination',N'TRANSLATEKTM',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKTMANS',N'TRANSLATEKTM',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám hô hấp',N'Khám hô hấp',N'TRANSLATEKHH',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKHHANS',N'TRANSLATEKHH',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Pulmonary examination',N'Pulmonary examination',N'TRANSLATEKHH',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKHHANS',N'TRANSLATEKHH',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám chuyên khoa',N'Khám chuyên khoa',N'TRANSLATEKCK',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKCKANS',N'TRANSLATEKCK',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Specialized examination',N'Specialized examination',N'TRANSLATEKCK',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKCKANS',N'TRANSLATEKCK',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám các bộ phận khác',N'Khám các bộ phận khác',N'TRANSLATEKCBPK',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKCBPKANS',N'TRANSLATEKCBPK',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Examination of other organs',N'Examination of other organs',N'TRANSLATEKCBPK',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKCBPKANS',N'TRANSLATEKCBPK',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
-------------------------------------
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám tim mạch, hô hấp',N'Khám tim mạch, hô hấp',N'TRANSLATEKTMHH',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKTMHHANS',N'TRANSLATEKTMHH',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Respiratory and cardiac function',N'Respiratory and cardiac function',N'TRANSLATEKTMHH',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKTMHHANS',N'TRANSLATEKTMHH',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'EOC_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám tim, phổi',N'Khám tim, phổi',N'TRANSLATEKTP',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKTPANS',N'TRANSLATEKTP',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cardiopulmonary examination',N'Cardiopulmonary examination',N'TRANSLATEKTP',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKTPANS',N'TRANSLATEKTP',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTT'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTT'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTTANS'

Update MasterDatas
set Clinic =    'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTTANS'

-----
Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCK'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCK'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCKANS'

Update MasterDatas
set Clinic =    'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCKANS'


Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPKANS'

Update MasterDatas
set Clinic =    'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPKANS'



-------------------------------------

delete from MasterDatas where Code = 'TRANSLATETREANTMENTPLAN'
delete from MasterDatas where Code = 'TRANSLATETREANTMENTPLANANS'

 Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hướng điều trị',N'Hướng điều trị',N'TRANSLATETREANTMENTPLAN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
 Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETREANTMENTPLANANS',N'TRANSLATETREANTMENTPLAN',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI', '1');
 Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Follow-up care plan',N'Follow-up care plan',N'TRANSLATETREANTMENTPLAN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
 Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETREANTMENTPLANANS',N'TRANSLATETREANTMENTPLAN',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN', '1');
-------------------------------------


-----------
Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set Clinic =  'OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYANS'

Update MasterDatas
set Clinic =    'OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYANS'

-----------
Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTT'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTT'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTTANS'

Update MasterDatas
set Clinic =    'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTTANS'

----
Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCK'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCK'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCKANS'

Update MasterDatas
set Clinic =    'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCKANS'


---
Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTM'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTM'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMANS'

Update MasterDatas
set Clinic =    'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMANS'


--
Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKHH'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKHH'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKHHANS'

Update MasterDatas
set Clinic =    'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKHHANS'



---
--
Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set Clinic =  'ED_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,OPD_MEDICAL REPORT_VI,ED_MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPKANS'

Update MasterDatas
set Clinic =   'ED_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,OPD_MEDICAL REPORT_EN,ED_MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPKANS'



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám các bộ phận khác (nếu có)',N'Khám các bộ phận khác (nếu có)',N'TRANSLATEKCBPKNC',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKCBPKNCANS',N'TRANSLATEKCBPKNC',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Examination of other related areas (if any)',N'Examination of other related areas (if any)',N'TRANSLATEKCBPKNC',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKCBPKNCANS',N'TRANSLATEKCBPKNC',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá mức độ mất chức năng của khu vực tổn thương',N'Đánh giá mức độ mất chức năng của khu vực tổn thương',N'TRANSLATEDGMDMCNCKVTT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGMDMCNCKVTTANS',N'TRANSLATEDGMDMCNCKVTT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of the function loss level of the lesion area',N'Assessment of the function loss level of the lesion area',N'TRANSLATEDGMDMCNCKVTT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGMDMCNCKVTTANS',N'TRANSLATEDGMDMCNCKVTT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh gía khả năng di chuyển trên mặt bằng phẳng',N'Đánh giá khả năng di chuyển trên mặt bằng phẳng',N'TRANSLATEDGKNDCTMBP',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNDCTMBPANS',N'TRANSLATEDGKNDCTMBP',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of the ability to move on a flat surface',N'Assessment of the ability to move on a flat surface',N'TRANSLATEDGKNDCTMBP',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNDCTMBPANS',N'TRANSLATEDGKNDCTMBP',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng thay đổi tư thế (nằm – ngồi – đứng)',N'Đánh giá khả năng thay đổi tư thế (nằm – ngồi – đứng)',N'TRANSLATEDGKNTDTTNND',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNTDTTNNDANS',N'TRANSLATEDGKNTDTTNND',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of the ability to change postures (lying - sitting - standing)',N'Assessment of the ability to change postures (lying - sitting - standing)',N'TRANSLATEDGKNTDTTNND',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNTDTTNNDANS',N'TRANSLATEDGKNTDTTNND',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng thực hiện các vận động phối hợp (vươn người bắt tay, cúi người nhặt dép)',N'Đánh giá khả năng thực hiện các vận động phối hợp (vươn người bắt tay, cúi người nhặt dép)',N'TRANSLATEDGKNTHCVDPPVNBTCNND',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNTHCVDPPVNBTCNNDANS',N'TRANSLATEDGKNTHCVDPPVNBTCNND',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of the ability to perform coordinated movements (reaching out to shake hands, bending to pick up sandals)',N'Assessment of the ability to perform coordinated movements (reaching out to shake hands, bending to pick up sandals)',N'TRANSLATEDGKNTHCVDPPVNBTCNND',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNTHCVDPPVNBTCNNDANS',N'TRANSLATEDGKNTHCVDPPVNBTCNND',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng PHCN và mức độ di chứng',N'Đánh giá khả năng PHCN và mức độ di chứng',N'TRANSLATEDGKNPVMDDC',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNPVMDDCANS',N'TRANSLATEDGKNPVMDDC',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of rehabilitation possibility and level of sequelae',N'Assessment of rehabilitation possibility and level of sequelae',N'TRANSLATEDGKNPVMDDC',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNPVMDDCANS',N'TRANSLATEDGKNPVMDDC',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá chức năng hô hấp (Dung tích sống, nhịp thở..)',N'Đánh giá chức năng hô hấp (Dung tích sống, nhịp thở..)',N'TRANSLATEDGKNCNHHDTSNT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNCNHHDTSNTANS',N'TRANSLATEDGKNCNHHDTSNT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of respiratory functions (Vital capacity, respiratory rate...)',N'Assessment of respiratory functions (Vital capacity, respiratory rate...)',N'TRANSLATEDGKNCNHHDTSNT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNCNHHDTSNTANS',N'TRANSLATEDGKNCNHHDTSNT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá sự cân đối và độ di động lồng ngực',N'Đánh giá sự cân đối và độ di động lồng ngực',N'TRANSLATEDGSCDVDDDLN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGSCDVDDDLNANS',N'TRANSLATEDGSCDVDDDLN',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of thoracic cage''s symmetry and mobility level',N'Assessment of thoracic cage''s symmetry and mobility level)',N'TRANSLATEDGSCDVDDDLN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGSCDVDDDLNANS',N'TRANSLATEDGSCDVDDDLN',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
-------------------------------------

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng gắng sức',N'Đánh giá khả năng gắng sức',N'TRANSLATEDGKNGS',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNGSANS',N'TRANSLATEDGKNGS',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of thoracic cage''s symmetry and mobility level',N'Assessment of thoracic cage''s symmetry and mobility level)',N'TRANSLATEDGKNGS',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNGSANS',N'TRANSLATEDGKNGS',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá mức độ thay đổi nhịp tim sau vận động',N'Đánh giá mức độ thay đổi nhịp tim sau vận động',N'TRANSLATEDGMDTDNTSVD',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGMDTDNTSVDANS',N'TRANSLATEDGMDTDNTSVD',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of the level of heart rate change after physical activities',N'Assessment of the level of heart rate change after physical activities',N'TRANSLATEDGMDTDNTSVD',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGMDTDNTSVDANS',N'TRANSLATEDGMDTDNTSVD',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng giao tiếp (tinh thần, phát âm…)',N'Đánh giá khả năng giao tiếp (tinh thần, phát âm…)',N'TRANSLATEDGKNGTTTPA',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNGTTTPAANS',N'TRANSLATEDGKNGTTTPA',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of the ability to communicate (spirit, pronunciation ...)',N'Assessment of the ability to communicate (spirit, pronunciation ...)',N'TRANSLATEDGKNGTTTPA',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNGTTTPAANS',N'TRANSLATEDGKNGTTTPA',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá tình trạng rối loạn nuốt',N'Đánh giá tình trạng rối loạn nuốt',N'TRANSLATEDGTTRLN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGTTRLNANS',N'TRANSLATEDGTTRLN',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of swallowing disorders',N'Assessment of swallowing disorders',N'TRANSLATEDGTTRLN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGTTRLNANS',N'TRANSLATEDGTTRLN',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng nhận thức không gian, thời gian',N'Đánh giá khả năng nhận thức không gian, thời gian',N'TRANSLATEDGKNNTKG',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNNTKGANS',N'TRANSLATEDGKNNTKG',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of spatial and temporal cognition',N'Assessment of spatial and temporal cognition',N'TRANSLATEDGKNNTKG',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNNTKGANS',N'TRANSLATEDGKNNTKG',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng điều vận (tầm, hướng, thăng bằng)',N'Đánh giá khả năng điều vận (tầm, hướng, thăng bằng)',N'TRANSLATEDGKNDVTHTB',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNDVTHTBANS',N'TRANSLATEDGKNDVTHTB',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of capacity of movement coordination (movements, direction, balance)',N' Assessment of capacity of movement coordination (movements, direction, balance)',N'TRANSLATEDGKNDVTHTBANS',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNDVTHTBANS',N'TRANSLATEDGKNDVTHTB',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá khả năng di chuyển độc lập ở nơi bằng phẳng',N'Đánh giá khả năng di chuyển độc lập ở nơi bằng phẳng',N'TRANSLATEDGKNDCDLONBP',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGKNDCDLONBPANS',N'TRANSLATEDGKNDCDLONBP',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of the ability to move independently in flat places',N'Assessment of the ability to move independently in flat places',N'TRANSLATEDGKNDCDLONBP',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGKNDCDLONBPANS',N'TRANSLATEDGKNDCDLONBP',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám dinh dưỡng',N'Khám dinh dưỡng',N'TRANSLATEKDD',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKDDANS',N'TRANSLATEKDD',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nutrition examination',N'Nutrition examination',N'TRANSLATEKDD',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKDDANS',N'TRANSLATEKDD',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá tiêm chủng',N'Đánh giá tiêm chủng',N'TRANSLATEDGTC',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGTCANS',N'TRANSLATEDGTC',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Immunization assessment',N'Immunization assessment',N'TRANSLATEDGTC',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGTCANS',N'TRANSLATEDGTC',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá phát triển vận động theo tuổi',N'Đánh giá phát triển vận động theo tuổi',N'TRANSLATEDGPTVDTT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGPTVDTTANS',N'TRANSLATEDGPTVDTT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of motor development by age',N'Assessment of motor development by age',N'TRANSLATEDGPTVDTT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGPTVDTTANS',N'TRANSLATEDGPTVDTT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá phát triển tinh thần theo tuổi',N'Đánh giá phát triển tinh thần theo tuổi',N'TRANSLATEDGPTTTTT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGPTTTTTANS',N'TRANSLATEDGPTTTTT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of motor development by age',N'Assessment of motor development by age',N'TRANSLATEDGPTVDTT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGPTTTTTANS',N'TRANSLATEDGPTTTTT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám tình trạng tăng hoặc giảm cân',N' Khám tình trạng tăng hoặc giảm cân',N'TRANSLATEKTTTHGC',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKTTTHGCANS',N'TRANSLATEKTTTHGC',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Examination of weight gain or weight loss',N'Examination of weight gain or weight loss',N'TRANSLATEKTTTHGC',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKTTTHGCANS',N'TRANSLATEKTTTHGC',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá cảm giác ngon miệng, nôn/trớ, đại tiểu tiện',N' Đánh giá cảm giác ngon miệng, nôn/trớ, đại tiểu tiện',N'TRANSLATEDGCGNMNTDTT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGCGNMNTDTTANS',N'TRANSLATEDGCGNMNTDTT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of appetite, vomiting/ spitting up, urination and defecation',N'Assessment of appetite, vomiting/ spitting up, urination and defecation',N'TRANSLATEDGCGNMNTDTT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGCGNMNTDTTANS',N'TRANSLATEDGCGNMNTDTT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá da, niêm mạc, tóc, móng, răng, giấc ngủ',N'Đánh giá da, niêm mạc, tóc, móng, răng, giấc ngủ',N'TRANSLATEDGDNMTMRGN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEDGDNMTMRGNANS',N'TRANSLATEDGDNMTMRGN',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Assessment of skin, mucosa, hair, nails, teeth, sleep',N'Assessment of skin, mucosa, hair, nails, teeth, sleep',N'TRANSLATEDGDNMTMRGN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEDGDNMTMRGNANS',N'TRANSLATEDGDNMTMRGN',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám lồng ngực, xương, khớp xương sọ, thóp',N'Khám lồng ngực, xương, khớp xương sọ, thóp',N'TRANSLATEKLNKXST',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKLNKXSTANS',N'TRANSLATEKLNKXST',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Examination of chest, bone, skull joints, fontanel',N'Examination of chest, bone, skull joints, fontanel',N'TRANSLATEKLNKXST',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKLNKXSTANS',N'TRANSLATEKLNKXST',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ngôn ngữ',N'Ngôn ngữ',N'TRANSLATENN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATENNANS',N'TRANSLATENN',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Language',N'Language',N'TRANSLATENN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATENNANS',N'TRANSLATENN',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhận thức',N'Nhận thức',N'TRANSLATENT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATENTANS',N'TRANSLATENT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cognition',N'Cognition',N'TRANSLATENT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATENTANS',N'TRANSLATENT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tương tác xã hội',N'Tương tác xã hội',N'TRANSLATETTXH',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETTXHANS',N'TRANSLATETTXH',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Social interaction',N'Social interaction',N'TRANSLATETTXH',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETTXHANS',N'TRANSLATETTXH',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hành vi',N'Hành vi',N'TRANSLATEHV',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEHVANS',N'TRANSLATEHV',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Behavior',N'Behavior',N'TRANSLATEHV',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEHVANS',N'TRANSLATEHV',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tập trung chú ý',N'Tập trung chú ý',N'TRANSLATETCCY',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETCCYANS',N'TRANSLATETCCY',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Behavior',N'Behavior',N'TRANSLATETCCY',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETCCYANS',N'TRANSLATETCCY',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cảm xúc',N'Cảm xúc',N'TRANSLATETCX',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETCXANS',N'TRANSLATETCX',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Emotion',N'Emotion',N'TRANSLATETCX',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETCXANS',N'TRANSLATETCX',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cảm giác',N'Cảm giác',N'TRANSLATETCG',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETCGANS',N'TRANSLATETCG',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sensation',N'Sensation',N'TRANSLATETCG',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETCGANS',N'TRANSLATETCG',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kĩ năng tự phục vụ',N'Kĩ năng tự phục vụ',N'TRANSLATEKNTPV',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKNTPVANS',N'TRANSLATEKNTPV',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Self-care skills',N'Self-care skills',N'TRANSLATEKNTPV',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKNTPVANS',N'TRANSLATEKNTPV',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Test tâm lý cần thiết',N'Test tâm lý cần thiết',N'TRANSLATETTLCT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETTLCTANS',N'TRANSLATETTLCT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nescessary psychological tests',N'Nescessary psychological tests',N'TRANSLATETTLCT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETTLCTANS',N'TRANSLATETTLCT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Biểu hiện chung',N'Biểu hiện chung',N'TRANSLATEBHC',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEBHCANS',N'TRANSLATEBHC',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'General expression',N'General expression',N'TRANSLATEBHC',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEBHCANS',N'TRANSLATEBHC',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tư duy',N'Tư duy',N'TRANSLATETD',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETDANS',N'TRANSLATETD',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thinking',N'Thinking',N'TRANSLATETD',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETDANS',N'TRANSLATETD',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tri giác',N'Tri giác',N'TRANSLATETG',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETGANS',N'TRANSLATETG',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sense',N'Sense',N'TRANSLATETG',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETGANS',N'TRANSLATETG',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Trí nhớ',N'Trí nhớ',N'TRANSLATETN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETNANS',N'TRANSLATETN',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Memory',N'Memory',N'TRANSLATETN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETNANS',N'TRANSLATETN',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Trí tuệ',N'Trí tuệ',N'TRANSLATETT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETTANS',N'TRANSLATETT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'IQ tests',N'IQ tests',N'TRANSLATETT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETTANS',N'TRANSLATETT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Trương lực cơ',N'Trương lực cơ',N'TRANSLATETLC',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETLCANS',N'TRANSLATETLC',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Muscular tone',N'Muscular tone',N'TRANSLATETLC',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETLCANS',N'TRANSLATETLC',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Vận động tinh',N'Vận động tinh',N'TRANSLATEVDT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEVDTANS',N'TRANSLATEVDT',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Fine motor skills',N'Fine motor skills',N'TRANSLATEVDT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEVDTANS',N'TRANSLATEVDT',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Vận động thô',N'Vận động thô',N'TRANSLATEVDTHO',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEVDTHOANS',N'TRANSLATEVDTHO',N'TRANSLATEVI',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Gross motor skills',N'Gross motor skills',N'TRANSLATEVDTHO',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'9',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEVDTHOANS',N'TRANSLATEVDTHO',N'TRANSLATEEN',N'2',N'10',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN', '1');
