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

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh lúc chuyển viện',N'Tình trạng người bệnh lúc chuyển viện',N'IPDTRANDMRCAD',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'21',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'IPDTRANDMRCADANS',N'IPDTRANDMRCAD',N'TRANSLATEVI',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Condition at discharge',N'Condition at discharge',N'IPDTRANDMRCAD',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'21',N'Label',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'IPDTRANDMRCADANS',N'IPDTRANDMRCAD',N'TRANSLATEEN',N'2',N'22',N'Text',N'',N'0',N'',N'IPD_REFERRAL LETTER_EN', '1');

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

Update MasterDatas 
SET Code = 'TRANSLATETRANSPORTANS'
Where Code in ('IPDTRANSFERTRANSPORTANS') and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI')
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
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =      'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'
-----------------------------------

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
SET Code = 'TRANSLATESTATUSPATIENTTRANSFER'
WHERE ViName Like N'Tình trạng người bệnh lúc chuyển viện' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )
update MasterDatas
SET Code = 'TRANSLATESTATUSPATIENTTRANSFERANS'
WHERE ViName Like N'Tình trạng người bệnh lúc chuyển viện' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI' )

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
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATESTATUSINJURYDISCHARGEANS',N'TRANSLATESTATUSADMITTEDANS',N'TRANSLATESTATUSADMITTED',N'2',N'20',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False',  N'Tình trạng thương tích lúc vào viện',N'Tình trạng thương tích lúc vào viện',N'TRANSLATESTATUSADMITTED',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'19',N'Label',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATESTATUSINJURYDISCHARGEANS',N'TRANSLATESTATUSADMITTEDANS',N'TRANSLATESTATUSADMITTED',N'2',N'20',N'Text',N'',N'0',N'',N'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN', '1');

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

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,ED_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,ED_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'

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
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT'

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
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set Clinic =    'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set Clinic =  'IPD_INJURY CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANS'

Update MasterDatas
set Clinic =    'IPD_INJURY CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANS'



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


Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =    'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =  'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic =   'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'

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
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set Clinic =   'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,EOC_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set Clinic =     'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,EOC_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'

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


select * from MasterDatas where Code = 'select * from MasterDatas where Form = 'TRANSLATEVI''

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
SET [Group] = 'TRANSLATETRANSPORT'
where Code = 'TRANSLATETRANSPORTANS' and (Form = 'TRANSLATEEN' or Form = 'TRANSLATEVI') 

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

select * from MasterDatas where Code = 'TRANSLATETRANSPORT'

update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Code = 'TRANSLATETRANSPORT' and  Form = 'TRANSLATEVI'
update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Code = 'TRANSLATETRANSPORT' and  Form = 'TRANSLATEEN'
update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_VI,OPD_TRANSFER LETTER_VI'
where Code = 'TRANSLATETRANSPORTANS' and  Form = 'TRANSLATEVI'
update MasterDatas
SET Clinic = 'IPD_TRANSFER LETTER_EN,OPD_TRANSFER LETTER_EN'
where Code = 'TRANSLATETRANSPORTANS' and  Form = 'TRANSLATEEN'

----

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