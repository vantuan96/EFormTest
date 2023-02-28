--Giấy chuyển tuyến xác nhận
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'IPD')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'ED')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'EOC')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển tuyến', 'A01_167_180220_VE', 'OPD')
update Actions set Name = N'[IPD] Xác nhận giấy chuyển tuyến', VisitTypeGroupId = (select Id from VisitTypeGroups where Code = 'IPD') where Code = 'ITFLE2'
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[OPD] Xác nhận giấy chuyển tuyến', 'OPDXACNHANGCT', (select Id from VisitTypeGroups where Code = 'OPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận giấy chuyển tuyến', 'EDXACNHANGCT', (select Id from VisitTypeGroups where Code = 'ED'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[EOC] Xác nhận giấy chuyển tuyến', 'EOCXACNHANGCT', (select Id from VisitTypeGroups where Code = 'EOC'))

--GDSK người bệnh và nhân thân
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Phiếu GDSK cho NB và thân nhân', 'A03_045_290422_VE', 'ED')

Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[EOC] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'EOCXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'EOC'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'IPDXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'IPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[OPD] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'OPDXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'OPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận phiếu GDSK cho người bệnh và nhân thân', 'EDXACNHANGDSK', (select Id from VisitTypeGroups where Code = 'ED'))

--Phiếu điều trị
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Phiếu điều trị', 'A01_066_050919_VE', 'ED')
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD] Xác nhận phiếu điều trị', 'IPDXACNHANPDT', (select Id from VisitTypeGroups where Code = 'IPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận phiếu điều trị', 'EDXACNHANPDT', (select Id from VisitTypeGroups where Code = 'ED'))

--Giấy chuyển viện
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'OPD')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'IPD')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'EOC')
Insert Into Forms(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupCode)Values(NEWID(), GETDATE(), GETDATE(), 0, N'Giấy chuyển viện', 'A01_145_050919_VE', 'ED')
update Actions set Name = N'[IPD] Xác nhận giấy chuyển viện', VisitTypeGroupId = (select Id from VisitTypeGroups where Code = 'IPD') where Code = 'IRELE2'
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[EOC] Xác nhận giấy chuyển viện', 'EOCXACNHANGCV', (select Id from VisitTypeGroups where Code = 'EOC'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[OPD] Xác nhận giấy chuyển viện', 'OPDXACNHANGCV', (select Id from VisitTypeGroups where Code = 'OPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED] Xác nhận giấy chuyển viện', 'EDXACNHANGCV', (select Id from VisitTypeGroups where Code = 'ED'))

--Phiếu chăm sóc
update Forms set Code = 'A02_062_050919_V' where Code = 'IPDCT' and VisitTypeGroupCode = 'ED'
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[IPD]Phiếu chăm sóc', 'IPDXACNHANPCS', (select Id from VisitTypeGroups where Code = 'IPD'))
Insert Into Actions(Id, CreatedAt, UpdatedAt, IsDeleted, Name, Code, VisitTypeGroupId)Values(NEWID(), GETDATE(), GETDATE(), 0, N'[ED]Phiếu chăm sóc', 'EDXACNHANPCS', (select Id from VisitTypeGroups where Code = 'ED'))
