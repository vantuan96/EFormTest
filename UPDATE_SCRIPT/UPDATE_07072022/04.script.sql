update IPDMedicalRecordOfPatients SET Version = 1
INSERT INTO AppConfigs(Id,[Key],[Label],[Value],IsDeleted) VALUES (newid(),'STILL_BIRTH_CODE',N'Đây là Mã khoa ở Prod/ check SpecialtyNo','15,197,202,64,201,213,184',0)

UPDATE IPDInitialAssessmentForNewborns SET DateOfAdmission = (SELECT IPDS.AdmittedDate FROM IPDS  WHERE IPDInitialAssessmentForNewborns.VisitId = IPDS.ID)

update MasterDatas set Code = 'IPDIAAUITV1ANS1' where Code = 'IPDIAAUITV1ANS' and Clinic = 'CHILDREN'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'CĐHA',N'CĐHA',N'EDJCFAOSKQCDHA',N'EDJCFAOS',N'EDJCFAOS',N'1',N'590',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'CĐHA',N'CĐHA',N'EDJCFAOSKQCDHAANS',N'EDJCFAOSKQCDHA',N'EDJCFAOS',N'2',N'591',N'ReadonlyField',N'',N'',N'',N'', '1');
update MasterDatas set Note = 'FE.0028', Data = '[#]FE.0028[#]', DataType = 'AutoFill' where code = 'EDJCFAOSECGANS'

update MasterDatas set DataType='AutoFill', Data ='[#]702PTs[#]250PTs[#]H_22.1[#]H_22.1[#]H_22.1[#]700PT[#]250PTs[#]', version = 'update02072022' where Note = '702PTs'
update MasterDatas set DataType='AutoFill', Data ='[#]703PT%[#]252PT%[#]OH_%PT[#]OH_%PT[#]OH_%PT[#]702PT%[#]252PT%[#]', version = 'update02072022' where Note = '703PT%'
update MasterDatas set DataType='AutoFill', Data ='[#]704INR[#]253INR[#]OH_INR[#]OH_INR[#]OH_INR[#]703INR[#]253INR[#]', version = 'update02072022' where Note = '704INR'
update MasterDatas set DataType='AutoFill', Data ='[#]705FibYT1[#]254Fib[#]OH_Fibrinogen[#]OH_Fibrinogen[#]OH_Fibrinogen[#]707Fib[#]254Fib[#]', version = 'update02072022' where Note = '705FibYT1'
update MasterDatas set DataType='AutoFill', Data ='[#]7000[#]254APTTs[#]H_22.5[#]H_22.5[#]H_22.5[#]OH_APTT[#]254APTTs[#]', version = 'update02072022' where Note = '700APTTs'
update MasterDatas set DataType='AutoFill', Data ='[#]7173TT[#]273RTT[#]OH_rTT[#]OH_rTT[#]OH_rTT[#]7173TT[#]273RTT[#]', version = 'update02072022' where Note = '701APTTc'
update MasterDatas set DataType='AutoFill', Data ='[#]703PT%[#]252PT%[#]OH_%PT[#]OH_%PT[#]OH_%PT[#]702PT%[#]252PT%[#]', version = 'update02072022' where Note = '7020'
update MasterDatas set DataType='AutoFill', Data ='[#]7000[#]254APTTs[#]H_22.5[#]H_22.5[#]H_22.5[#]OH_APTT[#]254APTTs[#]', version = 'update02072022' where Note = '7000'
update MasterDatas set DataType='AutoFill', Data ='[#]002GLU[#]200GLU[#]OH_Glucose[#]OH_Glucose[#]OH_Glucose[#]002GLU[#]200GLU[#]', version = 'update02072022' where Note = '002GLU'
update MasterDatas set DataType='AutoFill', Data ='[#]001URE[#]201URE[#]OH_Urea_B[#]OH_Urea_B[#]OH_Urea_B[#]001URE[#]201URE[#]', version = 'update02072022' where Note = '001URE'
update MasterDatas set DataType='AutoFill', Data ='[#]002CRE[#]202CRE[#]OH_Creatinin[#]OH_Creatinin[#]OH_Creatinin[#]006CRE[#]202CRE[#]', version = 'update02072022' where Note = '002CRE'
update MasterDatas set DataType='AutoFill', Data ='[#]102Na[#]242Na[#]OH_Natri[#]OH_Natri[#]OH_Natri[#]102Na[#]242Na[#]', version = 'update02072022' where Note = '102Na'
update MasterDatas set DataType='AutoFill', Data ='[#]103Ka[#]243K[#]OH_Kali[#]OH_Kali[#]OH_Kali[#]103K[#]243K[#]', version = 'update02072022' where Note = '103Ka'
update MasterDatas set DataType='AutoFill', Data ='[#]104Cl[#]244Cl[#]OH_Clo[#]OH_Clo[#]OH_Clo[#]104Cl[#]244Cl[#]', version = 'update02072022' where Note = '104Cl'
update MasterDatas set DataType='AutoFill', Data ='[#]030GOT[#]204GOT[#]OH_ASAT (GOT)[#]OH_ASAT (GOT)[#]OH_ASAT (GOT)[#]030GOT[#]204GOT[#]', version = 'update02072022' where Note = '030GOT'
update MasterDatas set DataType='AutoFill', Data ='[#]031GPT[#]205GPT[#]OH_ALAT (GPT)[#]OH_ALAT (GPT)[#]OH_ALAT (GPT)[#]031GPT[#]205GPT[#]', version = 'update02072022' where Note = '031GPT'
update MasterDatas set DataType='AutoFill', Data ='[#]405GLU[#]405GLU[#]OH_Urine Glucose[#]OH_Urine Glucose[#]OH_Urine Glucose[#]405GLU[#]405GLU[#]', version = 'update02072022' where Note = '405GLU'
update MasterDatas set DataType='AutoFill', Data ='[#]403PRO[#]403PRO[#]OH_Protein[#]OH_Protein[#]OH_Protein[#]403PRO[#]403PRO[#]', version = 'update02072022' where Note = '403PRO'
update MasterDatas set DataType='AutoFill', Data ='[#]402LEU[#]402LEU[#]OH_Leucocyte[#]OH_Leucocyte[#]OH_Leucocyte[#]402LEU[#]402LEU[#]', version = 'update02072022' where Note = '402LEU'
update MasterDatas set DataType='AutoFill', Data ='[#]801RBC[#]013RBC[#]OH_RBC[#]OH_RBC[#]OH_RBC[#]801RBC[#]013RBC[#]', version = 'update02072022' where Note = '801RBC'
update MasterDatas set DataType='AutoFill', Data ='[#]800WBC[#]002WBC[#]OH_WBC_B[#]OH_WBC_B[#]OH_WBC_B[#]800WBC[#]002WBC[#]', version = 'update02072022' where Note = '800WBC'
update MasterDatas set DataType='AutoFill', Data ='[#]808PLT[#]020PLT[#]OH_PLT[#]OH_PLT[#]OH_PLT[#]808PLT[#]020PLT[#]', version = 'update02072022' where Note = '808PLT'
update MasterDatas set DataType='AutoFill', Data ='[#]802HGB[#]014HGB[#]OH_HGB[#]OH_HGB[#]OH_HGB[#]806MCHC[#]014HGB[#]', version = 'update02072022' where Note = '805MCH'
update MasterDatas set DataType='AutoFill', Data ='[#]803HCT[#]015HCT[#]OH_HCT[#]OH_HCT[#]OH_HCT[#]803HCT[#]015HCT[#]', version = 'update02072022' where Note = '803HCT'
update MasterDatas set DataType='AutoFill', Data ='[#]944fHIV[#]435HIV[#]OH_24.169[#]OH_24.169[#]OH_24.169[#]944fHIV[#]944fHIV[#]', version = 'update02072022' where Note = '944fHIV'
update MasterDatas set DataType='AutoFill', Data ='[#]900HBsAg[#]458THBSAG[#]OH_24.117[#]OH_24.117[#]OH_24.117[#]934fHBsAg[#]934fHBsAg[#]', version = 'update02072022' where Note = '900HBsAg'
update MasterDatas set DataType='AutoFill', Data ='[#]939fHCV[#]923HCV[#]OH_24.144[#]OH_24.144[#]OH_24.144[#]939fHCV[#]939fHCV[#]', version = 'update02072022' where Note = '939fHCV'



 
 INSERT INTO AppConfigs(Id,[Key],[Label],[Value],IsDeleted) VALUES (newid(),'IS_SEND_MAIL_APPOINTMENT',N'Có cho phép gửi mail thông báo Ngày hẹn trả KQ cho bác sĩ','YES',0)
 INSERT INTO AppConfigs(Id,[Key],[Label],[Value],IsDeleted) VALUES (newid(),'SUBJECT_SEND_MAIL_APPOINTMENT',N'EMR Nhắc hẹn trả kết quả cho khách hàng/ EMR Remind about test results to patients',N'EMR Nhắc hẹn trả kết quả cho khách hàng/ EMR Remind about test results to patients',0)
 INSERT INTO AppConfigs(Id,[Key],[Label],[Value],IsDeleted) VALUES (newid(),'BODY_SEND_MAIL_APPOINTMENT',N'Body mail thông báo Ngày hẹn trả KQ cho bác sĩ',N'<!DOCTYPE html> <html> <head> <meta charset="UTF-8"> </head> <style> ul{ margin:0px; list-style-type:none } </style> <body> <div> <table style="width:100%;border:1px solid black;border-collapse: collapse;"> <tr  style="border:1px solid black"> <th style="text-align:center">NỘI DUNG</th> <th style="text-align:center">CONTENT</th> </tr> <tr> <td style="border:1px solid black;padding: 0px 12px 12px 12px;width:50%"> <p style="margin:0px"><br/><b><i>Kính gửi BS {FullNameDoctor},</i></b><br/><br/> Kết quả CLS của KH {CustomerName} - PID {PID} - khám ngày {TimeNT} đã có trên hệ thống.<br/> KH sẽ được nhắc lịch để được tư vấn KQ, Bác sĩ vui lòng thực hiện:<br/> <p style="margin:0 0 0 1.7em" > - Gọi điện thoại cho khách hàng tư vấn kết quả và follow up quá trình điều trị.<br/> - Tư vấn cho KH nhận KQ qua My Vinmec/ email hoặc bản cứng nếu cần thiết. Thông tin tới HC khoa về việc KH yêu cầu email/ bản cứng KQ cho KH nếu có yêu cầu.<br/> - BS ghi nhận vào HSBA/ eForm về thời gian đã tư vấn KQ cho KH và các vấn đề liên quan.<br/> </p> Kính đề nghị BS thực hiện đầy đủ để đảm bảo việc trả KQ CLS cho KH, tránh việc gián đoạn trong điều trị, gia tăng hiệu quả trải nghiệm dịch vụ KH tại  Vinmec.<br/> <br/> <i>Đây là email nhắc việc tự động, anh chị vui lòng không trả lời lại email này.</i> <br/> <br/> Trân trọng, <br/> <br/></p> </td> <td style="border:1px solid black;padding: 0px 12px 12px 12px;width:50%"> <p style="margin:0px"><br/><b><i>Dear Dr {FullNameDoctor},</i></b><br/><br/> The ancillary results of {CustomerName} - PID {PID} - examined on {TimeNT} has been updated on the system.<br/> The patient will be sent a reminder about date & time of consultation. Please follow the instruction:<br/> <p style="margin:0 0 0 1.7em" > - Call the patient to consult them about the result and the follow up treatment process.<br/> - Inform the patient about methods to receive their results: via My Vinmec/email or the result report paper if necessary; contact the administrative team of the Department for an email/result report paper if the patient has such request.<br/> - Fill in time/date that the patients have been contacted for consultation and related information in the patients’ medical reports/eForm.<br/> </p> Please follow the above instruction to ensure the return of results to the patient, avoid interruption in treatment, and increase the patients’ satisfaction in experiencing Vinmec International Hospital’s services.<br/> <br/> <i>This is an automatically generated email. Please do not reply to this email.</i> <br/> <br/> Sincerely, <br/> <br/></p> </td> </tr> <tbody> </div> </body> </html>',0)
update MasterDatas set DataType = 'Checkbox' where code in ('OPDCARIAS63',
'OPDCARIAS64',
'OPDCARIAS65',
'OPDCARIAS66',
'OPDCARIAS67',
'OPDCARIAS68')


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giờ vào khoa của trẻ',N'Neonatal arrival time',N'IPDOAGIANM218',N'IPDOAGIANM',N'A02_016_050919_VE',N'1',N'218',N'Label',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giờ vào khoa của trẻ',N'Neonatal arrival time',N'IPDOAGIANM219',N'IPDOAGIANM218',N'A02_016_050919_VE',N'2',N'219',N'Text',N'',N'',N'',N'', '1');