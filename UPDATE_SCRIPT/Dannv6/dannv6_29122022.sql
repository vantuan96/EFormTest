-- xét nghiệm tại chỗ - Khí máu và điện giải EG7+
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'A03_165_061222_V', 'ED')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'A03_165_061222_V', 'IPD')

Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xem xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIGDTEDA03_165_061222_V', NULL)
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Tạo mới xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICREDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'ED'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Chỉnh sửa xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIUDEDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'ED'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICFEDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'ED'))

Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xem xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIGDTIPDA03_165_061222_V', NULL)
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Tạo mới xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICRIPDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'IPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Chỉnh sửa xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIUDIPDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'IPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xác nhận xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICFIPDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'IPD'))

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'XNTC - Khí máu và điện giải EG7+',N'POCT - Blood gas analysis (Cartridge EG7+)',N'A03_165_061222_V',N'KQXN',N'NHOMBIEUMAU',N'2',N'25',N'Label',N'',N'',N'',N'', '');

--kiểm thảo tử vong ver 2 type KHSBA nhóm HSBA
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Trích biên bản kiểm thảo tử vong V2',N'Trích biên bản kiểm thảo tử vong V2',N'MortalityReportV2',N'KHAC',N'NHOMBIEUMAU',N'2',N'7',N'Label',N'',N'',N'',N'', '');