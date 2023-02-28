-- đánh giá ban đầu ngoại trú dài hạn EOC
Insert Into Forms(Id, IsDeleted, CreatedAt, UpdatedAt, Name, Code, VisitTypeGroupCode)Values(NEWID(), 0, GETDATE(), GETDATE(), N'Đánh giá ban đầu NB ngoại trú dài hạn', 'A02_008_080121_VE', 'EOC')

--Sửa masterdata nhóm HSBA
UPDATE MasterDatas SET Form = 'NHOMBIEUMAU' WHERE Form LIKE '%NHOMBIEUMAU%'
UPDATE MasterDatas SET [Group] = 'NHOMBIEUMAU' WHERE [Group] LIKE '%IPDNHOMBIEUMAU%'
UPDATE MasterDatas SET Code = 'BAHC' WHERE Code = 'IPDBAHC' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'BMKTHSBA' WHERE Code = 'IPDBMKTHSBA' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'HCCK' WHERE Code = 'IPDHCCK' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'KHAC' WHERE Code = 'IPDKHAC' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'KQCDHA' WHERE Code = 'IPDKQCDHA' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'KQXN' WHERE Code = 'IPDKQXN' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'PTTT' WHERE Code = 'IPDPTTT' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'RAVIEN' WHERE Code = 'IPDRAVIEN'and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'TDDD' WHERE Code = 'IPDTDDD' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'YL' WHERE Code = 'IPDYL' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET Code = 'YLK' WHERE Code = 'IPDYLK' and Form = 'NHOMBIEUMAU'

UPDATE MasterDatas SET [Group] = 'BAHC' WHERE [Group] like '%BAHC%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'BMKTHSBA' WHERE [Group] like '%BMKTHSBA%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'HCCK' WHERE [Group] like '%HCCK%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'KHAC' WHERE [Group] like '%KHAC%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'KQCDHA' WHERE [Group] like '%KQCDHA%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'KQXN' WHERE [Group] like '%KQXN%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'PTTT' WHERE [Group] like '%PTTT%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'RAVIEN' WHERE [Group] like '%RAVIEN%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'TDDD' WHERE [Group] like '%TDDD%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'YL' WHERE [Group] like '%YL%' and Form = 'NHOMBIEUMAU'
UPDATE MasterDatas SET [Group] = 'YLK' WHERE [Group] like '%YLK%' and Form = 'NHOMBIEUMAU'

-- Xóa master data trùng thai chết lưu
DELETE FROM MasterDatas WHERE Code = 'IPDIAACOORDIUPLOADCD2' AND [Order] = 1082
DELETE FROM MasterDatas WHERE Code = 'IPDIAACOORDIUPLOADCD1' AND [Order] = 1081

--update version 2 XNTC_đông máu
update EIOForms set Version = 1 where FormCode = 'A03_041_080322_V'