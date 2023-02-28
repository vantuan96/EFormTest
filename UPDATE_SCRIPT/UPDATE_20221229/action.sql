
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xem xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIGDTEDA03_165_061222_V', NULL)
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Tạo mới xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICREDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'ED'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Chỉnh sửa xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIUDEDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'ED'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICFEDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'ED'))

Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xem xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIGDTIPDA03_165_061222_V', NULL)
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Tạo mới xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICRIPDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'IPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Chỉnh sửa xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APIUDIPDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'IPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xác nhận xét nghiệm tại chỗ - Khí máu và điện giải EG7+', 'APICFIPDA03_165_061222_V', (select Id from VisitTypeGroups where Code = 'IPD'))

