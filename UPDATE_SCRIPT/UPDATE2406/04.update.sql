UPDATE IPDFallRiskAssessmentForAdults set FormType = 'A02_048_301220_VE'

Delete masterDatas where code = 'IPDIAAHUMPTYLBCNCD1' and [order] = 936
Delete masterDatas where code = 'IPDIAAHUMPTYLBCNCD2'and [order] = 937
Delete masterDatas where code = 'IPDIAAHUMPTYLBCNCD3' and [order] = 938

UPDATE MasterDatas SET Note = 'IPD' WHERE [Group] = 'PromissoryNote' OR [Group] = 'MedicalRecords'


INSERT INTO FormOfPatients(ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order])
VALUES(N'Giấy ra viện', N'Hospital discharge record', N'Discharged', 'IPD', N'Ra viện', N'Discharged', 46)



--ket thuc dieu tri
INSERT INTO FormOfPatients(ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order])
VALUES(N'Giấy ra viện', N'Hospital discharge record', N'Discharged', 'IPD', N'Kết thúc điều trị', N'Complete treatment', 47)





-- báo cáo y tế
INSERT INTO FormOfPatients(ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order])
VALUES(N'Báo cáo y tế ra viện', N'Discharge Medical Report', N'DischargeMedicalReport', 'IPD', N'Kết thúc điều trị', N'Complete treatment', 48)



INSERT INTO FormOfPatients(ViName, EnName, TypeName, Area, ViStatusPatient, EnStatusPatient, [Order])
VALUES(N'Báo cáo y tế', N'Medical Report', N'MedicalReport', 'IPD', N'Kết thúc điều trị', N'Complete treatment', 49)