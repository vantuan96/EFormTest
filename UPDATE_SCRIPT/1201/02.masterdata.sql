--- Deploy 12/01/2023
update MasterDatas set Code = 'INITIALASSESSMENTSHORTCONFIRM_RS_USERRECIVED' 
where Code = 'INITIALASSESSMENTSHORTCONFIRM_RS_USERCREATED'

--- Bảng kiểm chuẩn bị ra viện
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đã bàn giao đúng chủng loại, đủ số lượng thuốc cho người bệnh theo đơn ra viện?',N'Have handover of correct types and quantity of medication to patient been completed based on discharge prescription?',N'IPDDPCN117',N'IPDDPCNDIF',N'A03_046_050919_VE',N'1',N'130',N'Label',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Rồi/Có',N'Completed/Yes',N'IPDDPCN118',N'IPDDPCN117',N'A03_046_050919_VE',N'2',N'131',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Không áp dụng',N'NA',N'IPDDPCN119',N'IPDDPCN117',N'A03_046_050919_VE',N'2',N'132',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ghi chú (nếu có)',N'Notes (if any)',N'IPDDPCN120',N'IPDDPCN117',N'A03_046_050919_VE',N'2',N'133',N'Text',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đã hoàn thiện GDSK tất cả các thuốc ra viện? (Tham chiếu “Phiếu GDSK cho người bệnh và thân nhân”)',N'Complete education on home medication (Refer to “Patient and Family Education Form”)',N'IPDDPCN121',N'IPDDPCNDIF',N'A03_046_050919_VE',N'1',N'134',N'Label',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Rồi/Có',N'Completed/Yes',N'IPDDPCN122',N'IPDDPCN121',N'A03_046_050919_VE',N'2',N'135',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Không áp dụng',N'NA',N'IPDDPCN123',N'IPDDPCN121',N'A03_046_050919_VE',N'2',N'136',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ghi chú (nếu có)',N'Notes (if any)',N'IPDDPCN124',N'IPDDPCN121',N'A03_046_050919_VE',N'2',N'137',N'Text',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chức danh người trực tiếp thực hiện',N'Chức danh người trực tiếp thực hiện',N'IPDDPCN125',N'IPDDPCNDIF',N'A03_046_050919_VE',N'1',N'138',N'Label',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bác sĩ',N'Doctor',N'IPDDPCN126',N'IPDDPCN125',N'A03_046_050919_VE',N'2',N'139',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Điều dưỡng',N'Nurse',N'IPDDPCN127',N'IPDDPCN125',N'A03_046_050919_VE',N'2',N'140',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dược sỹ',N'Pharmacist',N'IPDDPCN128',N'IPDDPCN125',N'A03_046_050919_VE',N'2',N'141',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chức danh người trực tiếp thực hiện',N'Chức danh người trực tiếp thực hiện',N'IPDDPCN129',N'IPDDPCNDIF',N'A03_046_050919_VE',N'1',N'142',N'Label',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bác sĩ',N'Doctor',N'IPDDPCN130',N'IPDDPCN129',N'A03_046_050919_VE',N'2',N'143',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Điều dưỡng',N'Nurse',N'IPDDPCN131',N'IPDDPCN129',N'A03_046_050919_VE',N'2',N'144',N'Radio',N'',N'',N'',N'', '2');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dược sỹ',N'Pharmacist',N'IPDDPCN132',N'IPDDPCN129',N'A03_046_050919_VE',N'2',N'145',N'Radio',N'',N'',N'',N'', '2');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nơi giới thiệu',N'Referred by',N'OPDOENPT603',N'OPDOEN',N'OPDOEN',N'1',N'603',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cơ quan y tế',N'Hospitals',N'OPDOENPT604',N'OPDOENPT603',N'OPDOEN',N'2',N'604',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tự đến',N'By themselves',N'OPDOENPT605',N'OPDOENPT603',N'OPDOEN',N'2',N'605',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cơ quan y tế',N'Hospitals',N'OPDOENPT606',N'OPDOEN',N'OPDOEN',N'1',N'606',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán của nơi giới thiệu',N'Referral center',N'OPDOENPT607',N'OPDOENPT606',N'OPDOEN',N'2',N'607',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chẩn đoán của nơi giới thiệu',N'Referral center',N'OPDOENPT608',N'OPDOENPT606',N'OPDOEN',N'2',N'608',N'Text',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Không khai thác được',N'Nil',N'OPDOENPT609',N'OPDOENPT606',N'OPDOEN',N'2',N'609',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đã xử lý (thuốc, chăm sóc)',N'Treatment (medication, care)',N'OPDOENPT610',N'OPDOEN',N'OPDOEN',N'1',N'610',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Không',N'No',N'OPDOENPT611',N'OPDOENPT610',N'OPDOEN',N'2',N'611',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Có',N'Yes',N'OPDOENPT612',N'OPDOENPT610',N'OPDOEN',N'2',N'612',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đã xử lý (thuốc, chăm sóc)',N'Treatment (medication, care)',N'OPDOENPT613',N'OPDOENPT610',N'OPDOEN',N'2',N'613',N'Text',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh ra viện',N'Patient status at discharge',N'OPDOENPT614',N'OPDOEN',N'OPDOEN',N'1',N'614',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khỏi',N'Cured',N'OPDOENPT615',N'OPDOENPT614',N'OPDOEN',N'2',N'615',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đỡ, giảm',N'Improved',N'OPDOENPT616',N'OPDOENPT614',N'OPDOEN',N'2',N'616',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chưa thay đổi',N'Unchanged',N'OPDOENPT617',N'OPDOENPT614',N'OPDOEN',N'2',N'617',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nặng hơn',N'Worsened',N'OPDOENPT618',N'OPDOENPT614',N'OPDOEN',N'2',N'618',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khác',N'Others',N'OPDOENPT619',N'OPDOENPT614',N'OPDOEN',N'2',N'619',N'Radio',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng người bệnh ra viện',N'Patient status at discharge',N'OPDOENPT620',N'OPDOENPT614',N'OPDOEN',N'2',N'620',N'Text',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tiền sử bệnh',N'Tiền sử bệnh',N'OPDOENPT621',N'OPDOEN',N'OPDOEN',N'1',N'621',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Gia đình',N'Family history',N'OPDOENPT622',N'OPDOENPT621',N'OPDOEN',N'2',N'622',N'Text',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Báo bác sĩ điều trị',N'Inform attending physician',N'IPDIAAUITV8IAPNEW',N'IPDIAAUITV8',N'IPDIAAU',N'2',N'541',N'Checkbox',N'',N'',N'',N'', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giám đốc',N'Giám đốc',N'OUTPATIENT_DIR',N'A01_252_221222_V',N'A01_252_221222_V',N'1',N'1',N'ConfirmForm',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bác sỹ khám bệnh',N'Bác sỹ khám bệnh',N'OUTPATIENT_PHY',N'A01_252_221222_V',N'A01_252_221222_V',N'1',N'2',N'ConfirmForm',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bác sỹ điều trị',N'Bác sỹ điều trị',N'OUTPATIENT_PHY_SUMMARY',N'OUTPATIENT_PHY_SUMMARY',N'OUTPATIENT_PHY_SUMMARY',N'1',N'1',N'ConfirmForm',N'',N'',N'',N'', '1');



-- Update Setup bệnh án biểu mẫu
update MasterDatas set DataType = 'BENHAN', Level = 2 where [Group] = 'MedicalRecords' and 
Form in('A01_036_050919_V', 'A01_196_050919_V', 'A01_037_050919_V', 'A01_041_050919_V', 'A01_038_050919_V', 'A01_039_050919_V', 'A01_195_050919_V', 'A01_040_050919_V', 'A01_034_050919_V', 'A01_035_050919_V')

update MasterDatas set DataType = 'Label', Level = 1 where [Group] = 'MedicalRecords' and Note = 'IPD' and Form in ('IPDBAICU', 'BMTIMMACH')

update MasterDatas set DataType = 'PromissoryNote', Level = 3, [Group] = 'A01_037_050919_V' where [Group] = 'PromissoryNote' and Form = 'A02_014_220321_VE'

update MasterDatas set DataType = 'PromissoryNote', Level = 3, [Group] = 'A01_038_050919_V' where Form = 'A02_015_220321_VE'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá ban đầu trẻ nội trú nhi',N'Initial Assessment For Pediatric Inpatient',N'InitialAssessmentForPediatricInPatient',N'A01_034_050919_V',N'A02_014_220321_VE',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em',N'Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em',N'A02_047_301220_VE',N'A01_037_050919_V',N'A02_047_301220_VE',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em',N'Thang điểm HUMPTY DUMPTY đánh giá nguy cơ ngã ở trẻ em',N'A02_047_301220_VE',N'A01_195_050919_V',N'A02_047_301220_VE',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng điểm GLAMORGAN sàng lọc loét do tỳ ép ở trẻ nhi và sơ sinh',N'Glamorgan pressure injury screening tool',N'A02_066_050919_VE',N'A01_037_050919_V',N'A02_066_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng điểm GLAMORGAN sàng lọc loét do tỳ ép ở trẻ nhi và sơ sinh',N'Glamorgan pressure injury screening tool',N'A02_066_050919_VE',N'A01_038_050919_V',N'A02_066_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng điểm GLAMORGAN sàng lọc loét do tỳ ép ở trẻ nhi và sơ sinh',N'Glamorgan pressure injury screening tool',N'A02_066_050919_VE',N'A01_195_050919_V',N'A02_066_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)',N'A02_036_080322_V',N'A01_037_050919_V',N'A02_036_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)',N'A02_036_080322_V',N'A01_195_050919_V',N'A02_036_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 3 tháng tuổi)',N'A02_036_080322_V',N'A01_034_050919_V',N'A02_036_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 3 đến 12 tháng tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 3 đến 12 tháng tuổi)',N'A02_035_080322_V',N'A01_037_050919_V',N'A02_035_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 3 đến 12 tháng tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 3 đến 12 tháng tuổi)',N'A02_035_080322_V',N'A01_195_050919_V',N'A02_035_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 3 đến 12 tháng tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 3 đến 12 tháng tuổi)',N'A02_035_080322_V',N'A01_034_050919_V',N'A02_035_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 4 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 4 tuổi)',N'A02_034_080322_V',N'A01_037_050919_V',N'A02_034_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 4 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 4 tuổi)',N'A02_034_080322_V',N'A01_195_050919_V',N'A02_034_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 4 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 1 đến dưới 4 tuổi)',N'A02_034_080322_V',N'A01_034_050919_V',N'A02_034_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 4 đến 12 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 4 đến 12 tuổi)',N'A02_033_080322_V',N'A01_037_050919_V',N'A02_033_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 4 đến 12 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 4 đến 12 tuổi)',N'A02_033_080322_V',N'A01_195_050919_V',N'A02_033_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 4 đến 12 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Từ 4 đến 12 tuổi)',N'A02_033_080322_V',N'A01_034_050919_V',N'A02_033_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Trên 12 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Trên 12 tuổi)',N'A02_032_080322_V',N'A01_037_050919_V',N'A02_032_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Trên 12 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Trên 12 tuổi)',N'A02_032_080322_V',N'A01_195_050919_V',N'A02_032_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi DHST dành cho trẻ nhi (Trên 12 tuổi)',N'Bảng theo dõi DHST dành cho trẻ nhi (Trên 12 tuổi)',N'A02_032_080322_V',N'A01_034_050919_V',N'A02_032_080322_V',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá nguy cơ thuyên tắc mạch (BN nội khoa)',N'Đánh giá nguy cơ thuyên tắc mạch (BN nội khoa)',N'IPDTRFA',N'A01_034_050919_V',N'IPDTRFA',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá nguy cơ thuyên tắc mạch (BN nội khoa)',N'Đánh giá nguy cơ thuyên tắc mạch (BN nội khoa)',N'IPDTRFA',N'BMTIMMACH',N'IPDTRFA',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ sơ sinh',N'Bảng theo dõi dấu hiệu sinh tồn dành cho trẻ sơ sinh',N'IPDNOC',N'A01_038_050919_V',N'IPDNOC',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá ban đầu cho trẻ vừa sinh',N'Đánh giá ban đầu cho trẻ vừa sinh',N'A02_016_050919_VE',N'A01_035_050919_V',N'A02_016_050919_VE',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá ban đầu cho trẻ vừa sinh',N'Đánh giá ban đầu cho trẻ vừa sinh',N'A02_016_050919_VE',N'A01_038_050919_V',N'A02_016_050919_VE',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'ĐGBĐ sản phụ chuyển dạ',N'ĐGBĐ sản phụ chuyển dạ',N'A02_069_080121_VE',N'A01_035_050919_V',N'A02_069_080121_VE',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng theo dõi dấu hiệu sinh tồn dành cho sản phụ',N'Bảng theo dõi dấu hiệu sinh tồn dành cho sản phụ',N'VSFPW',N'A01_035_050919_V',N'VSFPW',N'3',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Xin lấy bánh rau không theo quy định',N'Xin lấy bánh rau không theo quy định',N'A01_159_050919_VE',N'A01_035_050919_V',N'A01_159_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh án ngoại khoa',N'The surgical medical record',N'TheSurgicalMedicalRecord',N'BMTIMMACH',N'A01_195_050919_V',N'2',N'0',N'BENHAN',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá nguy cơ thuyên tắc mạch ngoại khoa',N'Đánh giá nguy cơ thuyên tắc mạch ngoại khoa',N'A01_049_050919_VE',N'A01_195_050919_V',N'A01_049_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá nguy cơ thuyên tắc mạch ngoại khoa',N'Đánh giá nguy cơ thuyên tắc mạch ngoại khoa',N'A01_049_050919_VE',N'BMTIMMACH',N'A01_049_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Yêu cầu người bệnh chuẩn bị trước khi phẫu thuật thủ thuật',N'Yêu cầu người bệnh chuẩn bị trước khi phẫu thuật thủ thuật',N'A02_052_050919_V',N'A01_195_050919_V',N'A02_052_050919_V',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phiếu theo dõi người bệnh thoát mạch thuốc điều trị ung thư',N'Phiếu theo dõi người bệnh thoát mạch thuốc điều trị ung thư',N'A02_041_050919_V',N'A01_196_050919_V',N'A02_041_050919_V',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tóm tắt thủ thuật can thiệp động mạch vành',N'Tóm tắt thủ thuật can thiệp động mạch vành',N'A01_076_050919_VE',N'BMTIMMACH',N'A01_076_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ghi nhận thực hiện thuốc standing order',N'Ghi nhận thực hiện thuốc standing order',N'A03_029_050919_VE',N'BMTIMMACH',N'A03_029_050919_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'PROM bệnh nhân mạch vành',N'PROM bệnh nhân mạch vành',N'PROMFCD',N'BMTIMMACH',N'PROMFCD',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'PROM bệnh nhân suy tim',N'PROM bệnh nhân suy tim',N'PROMFHF',N'BMTIMMACH',N'PROMFHF',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh án tim mạch',N'Cardiology medical record for inpatient',N'CardiovascularForm',N'BMTIMMACH',N'BMTIMMACH',N'2',N'0',N'BENHAN',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh án nội khoa',N'The General Internal Medical Record',N'MedicalRecord',N'IPDBAICU',N'A01_034_050919_V',N'2',N'0',N'BENHAN',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đánh giá tình trạng trẻ trong 2 giờ sau sinh',N'Assessment of infant status during the first 2 hours of extrauterine life',N'A02_016_061022_VE',N'A01_035_050919_V',N'A02_016_061022_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');

-- Mở khóa XN BCYT ra viện IPD
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Báo cáo y tế ra viện', 'A01_143_290922_VE', 'IPD')

-- Update form code DGBD + sàng lọc ngã 

update Forms set Code = 'OPD_A02_007_220321_VE' where Code = 'A02_007_220321_VE' and VisitTypeGroupCode = 'OPD'
update Forms set Code = 'ED_A02_007_220321_VE' where Code = 'A02_007_220321_VE' and VisitTypeGroupCode = 'ED'
update Forms set Code = 'EOC_A02_007_220321_VE' where Code = 'A02_007_220321_VE' and VisitTypeGroupCode = 'EOC'
update Forms set Code = 'EOC_A02_007_080121_VE' where Code = 'A02_007_080121_VE' and VisitTypeGroupCode = 'EOC'
update Forms set Code = 'OPD_A02_007_080121_VE' where Code = 'A02_007_080121_VE' and VisitTypeGroupCode = 'OPD'

-- Update masterdata 
update MasterDatas set Code = 'A03_030_290321_VE', Form = 'A03_030_290321_VE', ViName = N'Phiếu ghi nhân thuốc y lệnh miệng', EnName = 'Verbal Order Form' where Code = 'A03_029_050919_VE' and Form = 'A03_029_050919_VE'

-- thêm BA phụ khoa + thai chết lưu trong BA sản
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Biên bản phối hợp với bệnh nhân/ gia đình xử lý thai chết lưu',N'Report Coordinating with the patient/ family to deal with a stillbirth',N'A01_152_100122_VE',N'A01_035_050919_V',N'A01_152_100122_VE',N'2',N'0',N'PromissoryNote',N'IPD',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh án phụ khoa',N'Medical Record GyNecological',N'MedicalRecordGynecological',N'A01_035_050919_V',N'A01_036_050919_V',N'2',N'0',N'BENHAN',N'IPD',N'',N'',N'', '1');


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD]Xác nhận bệnh án ngoại trú' , N'APICFA01_252_221222_V', (select id from VisitTypeGroups where Code ='OPD'));
INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, GETDATE(), GETDATE(),N'Bệnh án ngoại trú', 'A01_252_221222_V', 'OPD')

--Bệnh án ngoại trú
insert into FormOfPatients (ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD', N'Chuyển tuyến',N'Upstream/Downstream transfer',55)
insert into FormOfPatients (ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Chuyển viện',N'Inter-hospital transfer',56)
insert into FormOfPatients ( ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Chuyển cấp cứu',N'Transfer to ED',57)
insert into FormOfPatients ( ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Nhập nội trú',N'Admitted',58)
insert into FormOfPatients ( ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order]) values 
( N'Bệnh án ngoại trú', N'Medical record for outpatient', N'MedicalRecordForOutpatient', 'OPD',N'Hoàn thành khám',N'Discharged',59)