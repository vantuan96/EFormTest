
delete from MasterDatas where Code = 'TRANSLATEPDIAGNOSIS1'
delete from MasterDatas where Code = 'TRANSLATEPDIAGNOSIS1ANS'
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chuẩn đoán',N'Chuẩn đoán',N'TRANSLATEPDIAGNOSIS1',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEPDIAGNOSIS1ANS',N'TRANSLATEPDIAGNOSIS1',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Diagnosis',N'Diagnosis',N'TRANSLATEPDIAGNOSIS1',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'ED_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEPDIAGNOSIS1ANS',N'TRANSLATEPDIAGNOSIS1',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'ED_INJURY CERTIFICATE_EN', '1');

delete from MasterDatas where Code = 'TRANSLATETREATMENT1'
delete from MasterDatas where Code = 'TRANSLATETREATMENT1ANS'
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Điều trị',N'Điều trị',N'TRANSLATETREATMENT1',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETREATMENT1ANS',N'TRANSLATETREATMENT1',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'ED_INJURY CERTIFICATE_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Treatment',N'Treatment',N'TRANSLATETREATMENT1',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'ED_INJURY CERTIFICATE_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETREATMENT1ANS',N'TRANSLATETREATMENT1',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'ED_INJURY CERTIFICATE_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'BMI',N'BMI',N'TRANSLATEBMI',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEBMIANS',N'TRANSLATEBMI',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'BMI',N'BMI',N'TRANSLATEBMI',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEBMIANS',N'TRANSLATEBMI',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Trên 19 tuổi',N'Trên 19 tuổi',N'TRANSLATEOVER19',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEOVER19ANS',N'TRANSLATEOVER19',N'TRANSLATEVI',N'2',N'21',N'Radio',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Over 19 years old',N'Over 19 years old',N'TRANSLATEOVER19',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEOVER19ANS',N'TRANSLATEOVER19',N'TRANSLATEEN',N'2',N'21',N'Radio',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'5-19 tuổi',N'5-19 tuổi',N'TRANSLATE519',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATE519ANS',N'TRANSLATE519',N'TRANSLATEVI',N'2',N'21',N'Radio',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'5-19 years old',N'5-19 years old',N'TRANSLATE519',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATE519ANS',N'TRANSLATE519',N'TRANSLATEEN',N'2',N'21',N'Radio',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dưới 5 tuổi',N'Dưới 5 tuổi',N'TRANSLATEUNDER5',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEUNDER5ANS',N'TRANSLATEUNDER5',N'TRANSLATEVI',N'2',N'21',N'Radio',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Under 5 years',N'Under 5 years',N'TRANSLATEUNDER5',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEUNDER5ANS',N'TRANSLATEUNDER5',N'TRANSLATEEN',N'2',N'21',N'Radio',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhận thức, tương tác xã hội',N'Nhận thức, tương tác xã hội',N'TRANSLATENTTTXH',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATENTTTXHANS',N'TRANSLATENTTTXH',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cognition, social interaction',N'Cognition, social interaction',N'TRANSLATENTTTXH',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATENTTTXHANS',N'TRANSLATENTTTXH',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hành vi, tập trung chú ý',N'Hành vi, tập trung chú ý',N'TRANSLATEHVTTCY',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEHVTTCYANS',N'TRANSLATEHVTTCY',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Behavior, attention and concentration',N'Behavior, attention and concentration',N'TRANSLATEHVTTCY',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEHVTTCYANS',N'TRANSLATEHVTTCY',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cảm xúc, cảm giác',N'Cảm xúc, cảm giác',N'TRANSLATECXCG',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECXCGANS',N'TRANSLATECXCG',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Emotion, sensation',N'Emotion, sensation',N'TRANSLATECXCG',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECXCGANS',N'TRANSLATECXCG',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

delete from MasterDatas where Code = 'TRANSLATEKNTPV'
delete from MasterDatas where Code = 'TRANSLATEKNTPVANS'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kỹ năng tự phục vụ',N'Kỹ năng tự phục vụ',N'TRANSLATEKNTPV',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKNTPVANS',N'TRANSLATEKNTPV',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Self-care skills',N'Self-care skills',N'TRANSLATEKNTPV',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKNTPVANS',N'TRANSLATEKNTPV',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các cơ quan khác (Tim, phổi, tiêu hóa, …)',N'Các cơ quan khác (Tim, phổi, tiêu hóa, …)',N'TRANSLATECCQKTPTH',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECCQKTPTHANS',N'TRANSLATECCQKTPTH',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Other organs (Heart, lungs, digestion, ...)',N'Other organs (Heart, lungs, digestion, ...)',N'TRANSLATECCQKTPTH',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECCQKTPTHANS',N'TRANSLATECCQKTPTH',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám ý thức',N'Khám ý thức',N'TRANSLATEKYT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKYTANS',N'TRANSLATEKYT',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Consciousness assessment',N'Consciousness assessment',N'TRANSLATEKYT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKYTANS',N'TRANSLATEKYT',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám cảm giác, tri giác',N'Khám cảm giác, tri giác',N'TRANSLATEKCGTG',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKCGTGANS',N'TRANSLATEKCGTG',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sensation and perception assessment',N'Sensation and perception assessment',N'TRANSLATEKCGTG',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKCGTGANS',N'TRANSLATEKCGTG',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám tư duy',N'Khám tư duy',N'TRANSLATEKTD',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKTDANS',N'TRANSLATEKTD',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thinking skills assessment',N'Thinking skills assessment',N'TRANSLATEKTD',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKTDANS',N'TRANSLATEKTD',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám cảm xúc',N'Khám cảm xúc',N'TRANSLATEKCX',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKCXANS',N'TRANSLATEKCX',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Emotional assessment',N'Emotional assessment',N'TRANSLATEKCX',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKCXANS',N'TRANSLATEKCX',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám hoạt động',N'Khám hoạt động',N'TRANSLATEKHD',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKHDANS',N'TRANSLATEKHD',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Physical activity assessment',N'Physical activity assessment',N'TRANSLATEKHD',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKHDANS',N'TRANSLATEKHD',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám hoạt động',N'Khám hoạt động',N'TRANSLATEKCY',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKCYANS',N'TRANSLATEKCY',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Physical activity assessment',N'Physical activity assessment',N'TRANSLATEKCY',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKCYANS',N'TRANSLATEKCY',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám trí nhớ',N'Khám trí nhớ',N'TRANSLATEKTN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKTNANS',N'TRANSLATEKTN',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Memory assessment',N'Memory assessment',N'TRANSLATEKTN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKTNANS',N'TRANSLATEKTN',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám trí tuệ',N'Khám trí tuệ',N'TRANSLATEKKTT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKKTTANS',N'TRANSLATEKKTT',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Intellectual assessment',N'Intellectual assessment',N'TRANSLATEKKTT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKKTTANS',N'TRANSLATEKKTT',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khám nội khoa (tim, phổi ...)',N'Khám nội khoa (tim, phổi ...)',N'TRANSLATEKNTP',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKNTPANS',N'TRANSLATEKNTP',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Medical examination (heart, lung, digestion, ...)',N'Medical examination (heart, lung, digestion, ...)',N'TRANSLATEKNTP',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKNTPANS',N'TRANSLATEKNTP',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cảm xúc',N'Cảm xúc',N'TRANSLATECXTE',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECXTEANS',N'TRANSLATECXTE',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Emotional',N'Emotional',N'TRANSLATECXTE',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECXTEANS',N'TRANSLATECXTE',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các cơ quan khác',N'Các cơ quan khác',N'TRANSLATECCQK',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECCQKANS',N'TRANSLATECCQK',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Other organs',N'Other organs',N'TRANSLATECCQK',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECCQKANS',N'TRANSLATECCQK',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ý thức',N'Ý thức',N'TRANSLATEYT',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEYTANS',N'TRANSLATEYT',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Awareness',N'Awareness',N'TRANSLATEYT',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEYTANS',N'TRANSLATEYT',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giấc ngủ',N'Giấc ngủ',N'TRANSLATEGNNL',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEGNNLANS',N'TRANSLATEGNNL',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sleep',N'Sleep',N'TRANSLATEGNNL',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEGNNLANS',N'TRANSLATEGNNL',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các cơ quan khác',N'Các cơ quan khác',N'TRANSLATECCQKNL',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECCQKNLANS',N'TRANSLATECCQKNL',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Other organs',N'Other organs',N'TRANSLATECCQKNL',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECCQKNLANS',N'TRANSLATECCQKNL',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhận thức',N'Nhận thức',N'TRANSLATENTBN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATENTBNANS',N'TRANSLATENTBN',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cognition',N'Cognition',N'TRANSLATENTBN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATENTBNANS',N'TRANSLATENTBN',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');



Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Ngôn ngữ',N'Ngôn ngữ',N'TRANSLATENNBN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATENNBNANS',N'TRANSLATENNBN',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cognition',N'Cognition',N'TRANSLATENNBN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATENNBNANS',N'TRANSLATENNBN',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');

delete from MasterDatas where Code = 'TRANSLATEKNTPVBN'
delete from MasterDatas where Code = 'TRANSLATEKNTPVBNANS'
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kĩ năng tự phục vụ',N'Kĩ năng tự phục vụ',N'TRANSLATEKNTPVBN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATEKNTPVBNANS',N'TRANSLATEKNTPVBN',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Self-care skills',N'Self-care skills',N'TRANSLATEKNTPVBN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATEKNTPVBNANS',N'TRANSLATEKNTPVBN',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các cơ quan khác',N'Các cơ quan khác',N'TRANSLATECCQKBN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATECCQKBNANS',N'TRANSLATECCQKBN',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Other organs',N'Other organs',N'TRANSLATECCQKBN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATECCQKBNANS',N'TRANSLATECCQKBN',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Test tâm lý cần thiết ',N'Test tâm lý cần thiết ',N'TRANSLATETTLCTBN',N'TRANSLATEVI',N'TRANSLATEVI',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Dịch tại đây',N'Dịch tại đây',N'TRANSLATETTLCTBNANS',N'TRANSLATETTLCTBN',N'TRANSLATEVI',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nescessary psychological tests',N'Nescessary psychological tests',N'TRANSLATETTLCTBN',N'TRANSLATEEN',N'TRANSLATEEN',N'1',N'20',N'Label',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Translate here',N'Translate here',N'TRANSLATETTLCTBNANS',N'TRANSLATETTLCTBN',N'TRANSLATEEN',N'2',N'21',N'Text',N'',N'0',N'',N'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN', '1');



Update MasterDatas
set CreatedAt =  '2022-10-30 05:55:00.033',UpdatedAt =  '2022-10-30 05:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set  CreatedAt =  '2022-10-30 05:55:00.033',UpdatedAt =  '2022-10-30 05:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set  CreatedAt =  '2022-10-30 05:55:00.033',UpdatedAt =  '2022-10-30 05:55:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 05:55:00.033',UpdatedAt =  '2022-10-30 05:55:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAT'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAT'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENATANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:00:00.033',UpdatedAt =  '2022-10-30 06:00:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENATANS'



Update MasterDatas
set CreatedAt =  '2022-10-31 01:25:00.033',UpdatedAt =  '2022-10-31 01:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:25:00.033',UpdatedAt =  '2022-10-31 01:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORT'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:25:00.033',UpdatedAt =  '2022-10-31 01:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTANS'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:25:00.033',UpdatedAt =  '2022-10-31 01:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTANS'



Update MasterDatas
set CreatedAt =  '2022-10-31 01:30:00.033',UpdatedAt =  '2022-10-31 01:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDER'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:30:00.033',UpdatedAt =  '2022-10-31 01:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDER'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:30:00.033',UpdatedAt =  '2022-10-31 01:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATESENDERANS'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:30:00.033',UpdatedAt =  '2022-10-31 01:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATESENDERANS'





Update MasterDatas
set CreatedAt =  '2022-10-31 01:20:00.033',UpdatedAt =  '2022-10-31 01:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:20:00.033',UpdatedAt =  '2022-10-31 01:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTED'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:20:00.033',UpdatedAt =  '2022-10-31 01:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'

Update MasterDatas
set  CreatedAt =  '2022-10-31 01:20:00.033',UpdatedAt =  '2022-10-31 01:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENAMEMETHODCONTACTEDANS'


Update MasterDatas
set CreatedAt =  '2022-10-30 01:05:00.033',UpdatedAt =  '2022-10-30 01:05:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:05:00.033',UpdatedAt =  '2022-10-30 01:05:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:05:00.033',UpdatedAt =  '2022-10-30 01:05:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVIANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 01:05:00.033',UpdatedAt =  '2022-10-30 01:05:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENVIANS'


Update MasterDatas
set  ViName = 'Giới tính', EnName = 'Giới tính'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENVI'

Update MasterDatas
set CreatedAt =  '2022-10-30 06:25:00.033',UpdatedAt =  '2022-10-30 06:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:25:00.033',UpdatedAt =  '2022-10-30 06:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:25:00.033',UpdatedAt =  '2022-10-30 06:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:25:00.033',UpdatedAt =  '2022-10-30 06:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'



Update MasterDatas
set Clinic = 'ED_EMERGENCY CONFIRMATION_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic = 'ED_EMERGENCY CONFIRMATION_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADD'

Update MasterDatas
set Clinic =  'ED_EMERGENCY CONFIRMATION_VI,OPD_MEDICAL REPORT_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,IPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDANS'

Update MasterDatas
set Clinic =   'ED_EMERGENCY CONFIRMATION_EN,OPD_MEDICAL REPORT_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,IPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDANS'


Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTER'

Update MasterDatas
set Clinic =  'IPD_INJURY CERTIFICATE_VI,IPD_DISCHARGE CERTIFICATE_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_INJURY CERTIFICATE_VI,IPD_DISCHARGE MEDICAL REPORT_VI,IPD_SURGERY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEADDFOOTERANS'

Update MasterDatas
set Clinic =   'IPD_INJURY CERTIFICATE_EN,IPD_DISCHARGE CERTIFICATE_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_INJURY CERTIFICATE_EN,IPD_DISCHARGE MEDICAL REPORT_EN,IPD_SURGERY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEADDFOOTERANS'



Update MasterDatas
set CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDHLSANS'

Update MasterDatas
set  CreatedAt =  '2022-10-30 06:30:00.033',UpdatedAt =  '2022-10-30 06:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDHLSANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 13:56:00.033',UpdatedAt =  '2022-10-01 13:56:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:56:00.033',UpdatedAt =  '2022-10-01 13:56:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLAN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:56:00.033',UpdatedAt =  '2022-10-01 13:56:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:56:00.033',UpdatedAt =  '2022-10-01 13:56:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTER'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETRANSPORTFOOTERANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETRANSPORTFOOTERANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:00:00.033',UpdatedAt =  '2022-11-01 10:00:00.033'
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

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYANS'




----
Update MasterDatas
set CreatedAt =  '2022-11-01 10:20:00.033',UpdatedAt =  '2022-11-01 10:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:20:00.033',UpdatedAt =  '2022-11-01 10:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTT'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:20:00.033',UpdatedAt =  '2022-11-01 10:20:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:20:00.033',UpdatedAt =  '2022-11-01 10:20:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTTANS'
-------

Update MasterDatas
set CreatedAt =  '2022-11-01 10:25:00.033',UpdatedAt =  '2022-11-01 10:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTM'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:25:00.033',UpdatedAt =  '2022-11-01 10:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTM'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:25:00.033',UpdatedAt =  '2022-11-01 10:25:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:25:00.033',UpdatedAt =  '2022-11-01 10:25:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:30:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKHH'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:30:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKHH'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:30:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKHHANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:30:00.033',UpdatedAt =  '2022-11-01 10:30:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKHHANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:35:00.033',UpdatedAt =  '2022-11-01 10:35:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCK'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:35:00.033',UpdatedAt =  '2022-11-01 10:35:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCK'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:35:00.033',UpdatedAt =  '2022-11-01 10:35:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCKANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:35:00.033',UpdatedAt =  '2022-11-01 10:35:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCKANS'



Update MasterDatas
set CreatedAt =  '2022-11-01 10:40:00.033',UpdatedAt =  '2022-11-01 10:40:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:40:00.033',UpdatedAt =  '2022-11-01 10:40:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPK'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:40:00.033',UpdatedAt =  '2022-11-01 10:40:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKCBPKANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:40:00.033',UpdatedAt =  '2022-11-01 10:40:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKCBPKANS'



Update MasterDatas
set CreatedAt =  '2022-11-01 10:37:00.033',UpdatedAt =  '2022-11-01 10:37:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMHH'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:37:00.033',UpdatedAt =  '2022-11-01 10:37:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMHH'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:37:00.033',UpdatedAt =  '2022-11-01 10:37:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMHHANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:37:00.033',UpdatedAt =  '2022-11-01 10:37:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMHHANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,EOC_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,EOC_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENTANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:05:00.033',UpdatedAt =  '2022-11-01 10:05:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:05:00.033',UpdatedAt =  '2022-11-01 10:05:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENT'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:05:00.033',UpdatedAt =  '2022-11-01 10:05:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYOFPRESENTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:05:00.033',UpdatedAt =  '2022-11-01 10:05:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYOFPRESENTANS'


Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_VI,ED_EMERGENCY CONFIRMATION_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTM'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_EMERGENCY CONFIRMATION_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTM'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_VI,ED_EMERGENCY CONFIRMATION_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKTMANS'

Update MasterDatas
set Clinic = 'ED_TRANSFER LETTER_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN,ED_EMERGENCY CONFIRMATION_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKTMANS'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORY'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_VI,EOC_REFERRAL LETTER_VI,ED_MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHISTORYANS'

Update MasterDatas
set Clinic = 'OPD_REFERRAL LETTER_EN,EOC_REFERRAL LETTER_EN,ED_MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHISTORYANS'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCCCDH'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCCCDH'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCCCDHANS'

Update MasterDatas
set Clinic = ''
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCCCDHANS'




Update MasterDatas
set CreatedAt =  '2022-10-30 05:50:00.033',UpdatedAt =  '2022-10-30 05:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENGIOI'

Update MasterDatas
set  CreatedAt =  '2022-10-30 05:50:00.033',UpdatedAt =  '2022-10-30 05:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENGIOI'

Update MasterDatas
set CreatedAt =  '2022-10-30 05:50:00.033',UpdatedAt =  '2022-10-30 05:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEGENGIOIANS'

Update MasterDatas
set CreatedAt =  '2022-10-30 05:50:00.033',UpdatedAt =  '2022-10-30 05:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEGENGIOIANS'



Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_DISCHARGE CERTIFICATE_VI,IPD_REFERRAL LETTER_VI,IPD_TRANSFER LETTER_VI,IPD_INJURY CERTIFICATE_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI,OPD_ILLNESS CERTIFICATE_VI,ED_MEDICAL REPORT_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_DISCHARGE CERTIFICATE_VI,ED_REFERRAL LETTER_VI,ED_TRANSFER LETTER_VI,EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSISANS'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_DISCHARGE CERTIFICATE_EN,IPD_REFERRAL LETTER_EN,IPD_TRANSFER LETTER_EN,IPD_INJURY CERTIFICATE_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN,OPD_ILLNESS CERTIFICATE_EN,ED_MEDICAL REPORT_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_DISCHARGE CERTIFICATE_EN,ED_REFERRAL LETTER_EN,ED_TRANSFER LETTER_EN,EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSISANS'


Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT'

Update MasterDatas
set Clinic =  'IPD_INJURY CERTIFICATE_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENTANS'

Update MasterDatas
set Clinic = 'IPD_INJURY CERTIFICATE_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENTANS'




Update MasterDatas
set CreatedAt =  '2022-11-01 10:18:00.033',UpdatedAt =  '2022-11-01 10:18:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS1'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:18:00.033',UpdatedAt =  '2022-11-01 10:18:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS1'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:18:00.033',UpdatedAt =  '2022-11-01 10:18:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPDIAGNOSIS1ANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:18:00.033',UpdatedAt =  '2022-11-01 10:18:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPDIAGNOSIS1ANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:19:00.033',UpdatedAt =  '2022-11-01 10:19:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENT1'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:19:00.033',UpdatedAt =  '2022-11-01 10:19:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT1'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:19:00.033',UpdatedAt =  '2022-11-01 10:19:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETREATMENT1ANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:19:00.033',UpdatedAt =  '2022-11-01 10:19:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETREATMENT1ANS'



Update MasterDatas
set CreatedAt =  '2022-11-01 10:50:00.033',UpdatedAt =  '2022-11-01 10:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINCIPALTEST'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:50:00.033',UpdatedAt =  '2022-11-01 10:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINCIPALTEST'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:50:00.033',UpdatedAt =  '2022-11-01 10:50:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEPRINCIPALTESTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:50:00.033',UpdatedAt =  '2022-11-01 10:50:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEPRINCIPALTESTANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUP'

Update MasterDatas
set  CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUP'

Update MasterDatas
set CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 13:57:00.033',UpdatedAt =  '2022-11-01 13:57:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:12:00.033',UpdatedAt =  '2022-11-01 10:12:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:12:00.033',UpdatedAt =  '2022-11-01 10:12:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOM'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:12:00.033',UpdatedAt =  '2022-11-01 10:12:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECHIEFCOMANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:12:00.033',UpdatedAt =  '2022-11-01 10:12:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECHIEFCOMANS'


Update MasterDatas
set Clinic = 'EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic = 'EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLS'

Update MasterDatas
set Clinic = 'EOC_TRANSFER LETTER_VI,EOC_MEDICAL REPORT_VI,EOC_REFERRAL LETTER_VI,OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'

Update MasterDatas
set Clinic = 'EOC_TRANSFER LETTER_EN,EOC_MEDICAL REPORT_EN,EOC_REFERRAL LETTER_EN,OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS'





Update MasterDatas
set CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKLSANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKLSANS'
-----


Update MasterDatas
set CreatedAt =  '2022-11-01 10:06:00.033',UpdatedAt =  '2022-11-01 10:06:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:06:00.033',UpdatedAt =  '2022-11-01 10:06:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENN'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:06:00.033',UpdatedAt =  '2022-11-01 10:06:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENNANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:06:00.033',UpdatedAt =  '2022-11-01 10:06:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENNANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:07:00.033',UpdatedAt =  '2022-11-01 10:07:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEBHC'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:07:00.033',UpdatedAt =  '2022-11-01 10:07:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEBHC'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:07:00.033',UpdatedAt =  '2022-11-01 10:07:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEBHCANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:07:00.033',UpdatedAt =  '2022-11-01 10:07:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEBHCANS'



Update MasterDatas
set CreatedAt =  '2022-11-01 10:08:00.033',UpdatedAt =  '2022-11-01 10:08:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:08:00.033',UpdatedAt =  '2022-11-01 10:08:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENT'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:08:00.033',UpdatedAt =  '2022-11-01 10:08:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATENTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:08:00.033',UpdatedAt =  '2022-11-01 10:08:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATENTANS'

------
Update MasterDatas
set CreatedAt =  '2022-11-01 10:08:03.033',UpdatedAt =  '2022-11-01 10:08:03.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETTXH'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:08:03.033',UpdatedAt =  '2022-11-01 10:08:03.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETTXH'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:08:03.033',UpdatedAt =  '2022-11-01 10:08:03.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETTXHANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:08:03.033',UpdatedAt =  '2022-11-01 10:08:03.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETTXHANS'

---

Update MasterDatas
set CreatedAt =  '2022-11-01 10:10:00.033',UpdatedAt =  '2022-11-01 10:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETG'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:10:00.033',UpdatedAt =  '2022-11-01 10:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETG'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:10:00.033',UpdatedAt =  '2022-11-01 10:10:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETGANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:10:00.033',UpdatedAt =  '2022-11-01 10:10:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETGANS'

-------

Update MasterDatas
set CreatedAt =  '2022-11-01 10:11:00.033',UpdatedAt =  '2022-11-01 10:11:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETD'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:11:00.033',UpdatedAt =  '2022-11-01 10:11:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETD'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:11:00.033',UpdatedAt =  '2022-11-01 10:11:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETDANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:11:00.033',UpdatedAt =  '2022-11-01 10:11:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETDANS'


-----






----

Update MasterDatas
set CreatedAt =  '2022-11-01 10:14:00.033',UpdatedAt =  '2022-11-01 10:14:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHV'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:14:00.033',UpdatedAt =  '2022-11-01 10:14:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHV'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:14:00.033',UpdatedAt =  '2022-11-01 10:14:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEHVANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:14:00.033',UpdatedAt =  '2022-11-01 10:14:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEHVANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:10.033',UpdatedAt =  '2022-11-01 10:15:10.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCG'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:15:10.033',UpdatedAt =  '2022-11-01 10:15:10.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCG'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:10.033',UpdatedAt =  '2022-11-01 10:15:10.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCGANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:10.033',UpdatedAt =  '2022-11-01 10:15:10.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCGANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:05.033',UpdatedAt =  '2022-11-01 10:15:05.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCX'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:15:05.033',UpdatedAt =  '2022-11-01 10:15:05.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCX'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:05.033',UpdatedAt =  '2022-11-01 10:15:05.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCXANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:05.033',UpdatedAt =  '2022-11-01 10:15:05.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCXANS'

----
Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCCY'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCCY'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCCYANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:15:00.033',UpdatedAt =  '2022-11-01 10:15:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCCYANS'


---
Update MasterDatas
set CreatedAt =  '2022-11-01 10:16:00.033',UpdatedAt =  '2022-11-01 10:16:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETN'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:16:00.033',UpdatedAt =  '2022-11-01 10:16:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETN'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:16:00.033',UpdatedAt =  '2022-11-01 10:16:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETNANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:16:00.033',UpdatedAt =  '2022-11-01 10:16:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETNANS'
-----

---
Update MasterDatas
set CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETT'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETTANS'

---
Update MasterDatas
set CreatedAt =  '2022-11-01 10:18:55.033',UpdatedAt =  '2022-11-01 10:18:55.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKNTPV'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:18:55.033',UpdatedAt =  '2022-11-01 10:18:55.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKNTPVANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:18:55.033',UpdatedAt =  '2022-11-01 10:18:55.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKNTPVANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:18:55.033',UpdatedAt =  '2022-11-01 10:18:55.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKNTPVANS'
-----
Update MasterDatas
set CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETTLCT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETTLCT'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETTLCTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:17:00.033',UpdatedAt =  '2022-11-01 10:17:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETTLCTANS'




Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCG'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCG'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCGANS'



Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCGANS'



Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCX'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCX'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI,OPD_TRANSFER LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATETCXANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN,OPD_TRANSFER LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATETCXANS'


Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTVDTT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTVDTT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTVDTTANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTVDTTANS'


Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTTTTT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTTTTT'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_VI,OPD_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTTTTTANS'

Update MasterDatas
set Clinic = 'OPD_MEDICAL REPORT_EN,OPD_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTTTTTANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:45:00.033',UpdatedAt =  '2022-11-01 10:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKDDANS'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:45:00.033',UpdatedAt =  '2022-11-01 10:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKDDANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:45:00.033',UpdatedAt =  '2022-11-01 10:45:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEKDDANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:45:00.033',UpdatedAt =  '2022-11-01 10:45:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEKDDANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:46:00.033',UpdatedAt =  '2022-11-01 10:46:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGTC'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:46:00.033',UpdatedAt =  '2022-11-01 10:46:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGTC'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:46:00.033',UpdatedAt =  '2022-11-01 10:46:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGTCANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:46:00.033',UpdatedAt =  '2022-11-01 10:46:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGTCANS'


Update MasterDatas
set CreatedAt =  '2022-11-01 10:47:00.033',UpdatedAt =  '2022-11-01 10:47:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTVDTT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:47:00.033',UpdatedAt =  '2022-11-01 10:47:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTVDTT'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:47:00.033',UpdatedAt =  '2022-11-01 10:47:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTVDTTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:47:00.033',UpdatedAt =  '2022-11-01 10:47:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTVDTTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTTTTT'

Update MasterDatas
set  CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTTTTT'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATEDGPTTTTTANS'

Update MasterDatas
set CreatedAt =  '2022-11-01 10:48:00.033',UpdatedAt =  '2022-11-01 10:48:00.033'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATEDGPTTTTTANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKTNANS'
WHERE Code = 'OPDOENKTNANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKLSANS'
WHERE Code = 'OPDOENCEFANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKTTANS'
WHERE Code = 'OPDOENKTTANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKCKANS'
WHERE Code = 'OPDOENKCKANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKTPANS'
WHERE Code = 'OPDOENKTP1ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKCBPKNCANS'
WHERE Code = 'OPDOENKCBPKANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEBMIANS'
WHERE Code = 'OPDOEN527'

Update MasterDatas
Set DefaultValue = 'TRANSLATEBMIANS'
WHERE Code = 'OPDOENBMIANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEOVER19ANS'
WHERE Code = 'OPDOENTUOI19'

Update MasterDatas
Set DefaultValue = 'TRANSLATE519ANS'
WHERE Code = 'OPDOENTUOI519'


Update MasterDatas
Set DefaultValue = 'TRANSLATEUNDER5ANS'
WHERE Code = 'OPDOENTUOI5'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKTTTHGCANS'
WHERE Code = 'OPDOENKTTTHGCANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEDGCGNMNTDTTANS'
WHERE Code = 'OPDOENDGCGNMANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEDGDNMTMRGNANS'
WHERE Code = 'OPDOENDGDNMANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKLNKXSTANS'
WHERE Code = 'OPDOENKLNANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKCBPKNCANS'
WHERE Code = 'OPDOENKCBPKANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKTMANS'
WHERE Code = 'OPDOENKTMANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKHHANS'
WHERE Code = 'OPDOENKHHANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATENNANS'
WHERE Code = 'OPDOENNGNGANS'



Update MasterDatas
Set DefaultValue = 'TRANSLATENTTTXHANS'
WHERE Code = 'OPDOENNTTTXHANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEHVTTCYANS'
WHERE Code = 'OPDOENHVTTCYANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATECXCGANS'
WHERE Code = 'OPDOENCGXGANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKNTPV'
WHERE Code = 'OPDOENKTTPVANS'



Update MasterDatas
Set DefaultValue = 'TRANSLATETTLCTANS'
WHERE Code = 'OPDOENMSTTLCTANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATECCQKTPTHANS'
WHERE Code = 'OPDOENCCQKANS'



Update MasterDatas
Set DefaultValue = 'TRANSLATECCQKTPTHANS'
WHERE Code = 'OPDOENCCQKANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKDDANS'
WHERE Code = 'OPDOENKDDANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEDGTCANS'
WHERE Code = 'OPDOENTCANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEDGPTVDTTANS'
WHERE Code = 'OPDOENPTVDANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEDGPTTTTTANS'
WHERE Code = 'OPDOENPTTTANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKTMHHANS'
WHERE Code = 'OPDOENKTPANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEBHCANS'
WHERE Code = 'OPDOENBHCANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKYTANS'
WHERE Code = 'OPDOENKYTANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKCGTGANS'
WHERE Code = 'OPDOENKCGTGANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKTDANSANS'
WHERE Code = 'OPDOENKTDANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKCXANS'
WHERE Code = 'OPDOENKCXANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKHDANS'
WHERE Code = 'OPDOENKHDANS'



Update MasterDatas
Set DefaultValue = 'TRANSLATEKCYANS'
WHERE Code = 'OPDOENKCYANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKKTTANS'
WHERE Code = 'OPDOENKTRTANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATETTLCTANS'
WHERE Code = 'OPDOENTTLANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETTLCTBNANS'
WHERE Code = 'OPDOENTK0028ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETTLCTNLANS'
WHERE Code = 'OPDOENTK0027ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATECCQKBNANS'
WHERE Code = 'OPDOENTK0026ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEKNTPVBNANS'
WHERE Code = 'OPDOENTK0025ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEVDTHOANS'
WHERE Code = 'OPDOENTK0024ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEVDTANS'
WHERE Code = 'OPDOENTK0023ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATENNBNANS'
WHERE Code = 'OPDOENTK0022ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATENTBNANS'
WHERE Code = 'OPDOENTK0021ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKNTPANS'
WHERE Code = 'OPDOENKNKTANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETLCANS'
WHERE Code = 'OPDOENTK0020ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATECCQKNLANS'
WHERE Code = 'OPDOENTK0019ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETNTTNLANS'
WHERE Code = 'OPDOENTK0018ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEGNNANSLANS'
WHERE Code = 'OPDOENTK0017ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEHVNLANS'
WHERE Code = 'OPDOENTK0016ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATECXNLANS'
WHERE Code = 'OPDOENTK0015ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATETGANS'
WHERE Code = 'OPDOENTK0014ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETDANS'
WHERE Code = 'OPDOENTK0013ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEYTANS'
WHERE Code = 'OPDOENTK0012ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEBHCANS'
WHERE Code = 'OPDOENTK0011ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETTLCTANS'
WHERE Code = 'OPDOENTK0009ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATEKNTPVANS'
WHERE Code = 'OPDOENTK0008ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETCGANS'
WHERE Code = 'OPDOENTK0007ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATETCXANS'
WHERE Code = 'OPDOENTK0006ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETCCYANS'
WHERE Code = 'OPDOENTK0005ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATEHVANS'
WHERE Code = 'OPDOENTK0004ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATETTXHANS'
WHERE Code = 'OPDOENTK0003ANS'


Update MasterDatas
Set DefaultValue = 'TRANSLATENTANS'
WHERE Code = 'OPDOENTK0002ANS'

Update MasterDatas
Set DefaultValue = 'TRANSLATENNANS'
WHERE Code = 'OPDOENTK0001ANS'