delete MasterDatas where [Form] = 'IPDMRPO' and [order] >= 202 and [order] <= 214
delete MasterDatas where [Form] = 'IPDMRPT' and [order] >= 601 and [order] <= 684

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Không',N'No',N'IPDMRPTCDKTNO',N'IPDMRPTCDKT',N'IPDMRPT',N'2',N'682',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Có',N'Yes',N'IPDMRPTCDKTYES',N'IPDMRPTCDKT',N'IPDMRPT',N'2',N'681',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Không',N'No',N'IPDMRPTCDBCNO',N'IPDMRPTCDBC',N'IPDMRPT',N'2',N'680',N'Radio',N'',N'',N'',N'', '');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Có',N'Yes',N'IPDMRPTCDBCYES',N'IPDMRPTCDBC',N'IPDMRPT',N'2',N'679',N'Radio',N'',N'',N'',N'', '');

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sinh ngày:',N'Sinh ngày:',N'IPDMRPOS3',N'IPDMRPO',N'IPDMRPO',N'1',N'202',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sinh ngày:',N'Sinh ngày:',N'IPDMRPOS4',N'IPDMRPOS3',N'IPDMRPO',N'2',N'203',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đẻ lần mấy:',N'Đẻ lần mấy:',N'IPDMRPOS5',N'IPDMRPO',N'IPDMRPO',N'1',N'204',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đẻ lần mấy:',N'Đẻ lần mấy:',N'IPDMRPOS6',N'IPDMRPOS5',N'IPDMRPO',N'2',N'205',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sinh ngày:',N'Sinh ngày:',N'IPDMRPOS7',N'IPDMRPO',N'IPDMRPO',N'1',N'206',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sinh ngày:',N'Sinh ngày:',N'IPDMRPOS8',N'IPDMRPOS7',N'IPDMRPO',N'2',N'207',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'9. Nhóm máu mẹ:',N'9. Nhóm máu mẹ:',N'IPDMRPOS9',N'IPDMRPO',N'IPDMRPO',N'1',N'208',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'9. Nhóm máu mẹ:',N'9. Nhóm máu mẹ:',N'IPDMRPOS10',N'IPDMRPOS9',N'IPDMRPO',N'2',N'209',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'10. Tiền thai (Para)',N'10. Tiền thai (Para)',N'IPDMRPOS11',N'IPDMRPO',N'IPDMRPO',N'1',N'210',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sinh (đủ tháng)',N'Sinh (đủ tháng)',N'IPDMRPOS12',N'IPDMRPOS11',N'IPDMRPO',N'2',N'211',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sớm (thiếu tháng)',N'Sớm (thiếu tháng)',N'IPDMRPOS13',N'IPDMRPOS11',N'IPDMRPO',N'2',N'212',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sẩy (nạo, hút)',N'Sẩy (nạo, hút)',N'IPDMRPOS14',N'IPDMRPOS11',N'IPDMRPO',N'2',N'213',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Sống',N'Sống',N'IPDMRPOS15',N'IPDMRPOS11',N'IPDMRPO',N'2',N'214',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'II. Hỏi bệnh: (diễn biến của bệnh sơ sinh)',N'II. Hỏi bệnh: (diễn biến của bệnh sơ sinh)',N'IPDMRPT61',N'IPDMRPT60',N'IPDMRPT',N'2',N'601',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'a. Tình hình sản phụ trong khi đẻ',N'a. Tình hình sản phụ trong khi đẻ',N'IPDMRPT62',N'IPDMRPT',N'IPDMRPT',N'1',N'602',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Vỡ ối',N'- Vỡ ối',N'IPDMRPT63',N'IPDMRPT',N'IPDMRPT',N'1',N'603',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Vỡ ối',N'- Vỡ ối',N'IPDMRPT64',N'IPDMRPT63',N'IPDMRPT',N'2',N'604',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Mầu sắc:',N'- Mầu sắc:',N'IPDMRPT65',N'IPDMRPT',N'IPDMRPT',N'1',N'605',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Mầu sắc:',N'- Mầu sắc:',N'IPDMRPT66',N'IPDMRPT65',N'IPDMRPT',N'2',N'606',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cách đẻ:',N'Cách đẻ:',N'IPDMRPT67',N'IPDMRPT',N'IPDMRPT',N'1',N'607',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'1. Đẻ thường',N'1. Đẻ thường',N'IPDMRPT68',N'IPDMRPT67',N'IPDMRPT',N'2',N'608',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'2. Can thiệp',N'2. Can thiệp',N'IPDMRPT69',N'IPDMRPT67',N'IPDMRPT',N'2',N'609',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lúc',N'Lúc',N'IPDMRPT70',N'IPDMRPT',N'IPDMRPT',N'1',N'610',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lúc',N'Lúc',N'IPDMRPT71',N'IPDMRPT70',N'IPDMRPT',N'2',N'611',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lý do can thiệp',N'Lý do can thiệp',N'IPDMRPT72',N'IPDMRPT',N'IPDMRPT',N'1',N'612',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lý do can thiệp',N'Lý do can thiệp',N'IPDMRPT73',N'IPDMRPT72',N'IPDMRPT',N'2',N'613',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'b. Tình trạng sơ sinh khi ra đời:',N'b. Tình trạng sơ sinh khi ra đời:',N'IPDMRPT74',N'IPDMRPT',N'IPDMRPT',N'1',N'614',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'1. Khóc ngay',N'1. Khóc ngay',N'IPDMRPT75',N'IPDMRPT74',N'IPDMRPT',N'2',N'615',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'2. Ngạt',N'2. Ngạt',N'IPDMRPT76',N'IPDMRPT74',N'IPDMRPT',N'2',N'616',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'3. Khác',N'3. Khác',N'IPDMRPT77',N'IPDMRPT74',N'IPDMRPT',N'2',N'617',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh người đỡ đẻ, phẫu thuật',N'Họ tên, chức danh người đỡ đẻ, phẫu thuật',N'IPDMRPT78',N'IPDMRPT',N'IPDMRPT',N'1',N'618',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh người đỡ đẻ, phẫu thuật',N'Họ tên, chức danh người đỡ đẻ, phẫu thuật',N'IPDMRPT79',N'IPDMRPT78',N'IPDMRPT',N'2',N'619',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Apgar',N'Apgar',N'IPDMRPT80',N'IPDMRPT',N'IPDMRPT',N'1',N'620',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Apgar',N'Apgar',N'IPDMRPT81',N'IPDMRPT80',N'IPDMRPT',N'2',N'621',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'1 phút',N'1 phút',N'IPDMRPT82',N'IPDMRPT',N'IPDMRPT',N'1',N'622',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'điểm',N'điểm',N'IPDMRPT83',N'IPDMRPT82',N'IPDMRPT',N'2',N'623',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'điểm',N'điểm',N'IPDMRPT84',N'IPDMRPT82',N'IPDMRPT',N'2',N'624',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'5 phút',N'5 phút',N'IPDMRPT85',N'IPDMRPT',N'IPDMRPT',N'1',N'625',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'điểm',N'điểm',N'IPDMRPT86',N'IPDMRPT85',N'IPDMRPT',N'2',N'626',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'điểm',N'điểm',N'IPDMRPT87',N'IPDMRPT85',N'IPDMRPT',N'2',N'627',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'10 phút',N'10 phút',N'IPDMRPT88',N'IPDMRPT',N'IPDMRPT',N'1',N'628',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'điểm',N'điểm',N'IPDMRPT89',N'IPDMRPT88',N'IPDMRPT',N'2',N'629',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'điểm',N'điểm',N'IPDMRPT90',N'IPDMRPT88',N'IPDMRPT',N'2',N'630',N'',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng dinh dưỡng sau sinh:',N'Tình trạng dinh dưỡng sau sinh:',N'IPDMRPT91',N'IPDMRPT',N'IPDMRPT',N'1',N'631',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tình trạng dinh dưỡng sau sinh:',N'Tình trạng dinh dưỡng sau sinh:',N'IPDMRPT92',N'IPDMRPT91',N'IPDMRPT',N'2',N'632',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'c. Phương pháp hồi sinh ngay sau đẻ:',N'c. Phương pháp hồi sinh ngay sau đẻ:',N'IPDMRPT93',N'IPDMRPT',N'IPDMRPT',N'1',N'633',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hút dịch',N'Hút dịch',N'IPDMRPT94',N'IPDMRPT93',N'IPDMRPT',N'2',N'634',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Xoa bóp tim',N'Xoa bóp tim',N'IPDMRPT96',N'IPDMRPT93',N'IPDMRPT',N'2',N'635',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thở O2',N'Thở O2',N'IPDMRPT97',N'IPDMRPT93',N'IPDMRPT',N'2',N'636',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đặt nội khí quản',N'Đặt nội khí quản',N'IPDMRPT98',N'IPDMRPT93',N'IPDMRPT',N'2',N'637',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bóp bóng O2',N'Bóp bóng O2',N'IPDMRPT99',N'IPDMRPT93',N'IPDMRPT',N'2',N'638',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khác',N'Khác',N'IPDMRPT100',N'IPDMRPT93',N'IPDMRPT',N'2',N'639',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khác',N'Khác',N'IPDMRPT101',N'IPDMRPT93',N'IPDMRPT',N'2',N'640',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh người chuyển sơ sinh',N'Họ tên, chức danh người chuyển sơ sinh',N'IPDMRPT102',N'IPDMRPT',N'IPDMRPT',N'1',N'641',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Họ tên, chức danh người chuyển sơ sinh',N'Họ tên, chức danh người chuyển sơ sinh',N'IPDMRPT103',N'IPDMRPT102',N'IPDMRPT',N'2',N'642',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'1. Toàn thân:',N'1. Toàn thân:',N'IPDMRPT104',N'IPDMRPT',N'IPDMRPT',N'1',N'643',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Dị tật bẩm sinh:',N'- Dị tật bẩm sinh:',N'IPDMRPT105',N'IPDMRPT104',N'IPDMRPT',N'2',N'644',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Có hậu môn:',N'- Có hậu môn:',N'IPDMRPT106',N'IPDMRPT104',N'IPDMRPT',N'2',N'645',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cụ thể dị tật:',N'Cụ thể dị tật:',N'IPDMRPT107',N'IPDMRPT',N'IPDMRPT',N'1',N'646',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Cụ thể dị tật:',N'Cụ thể dị tật:',N'IPDMRPT108',N'IPDMRPT107',N'IPDMRPT',N'2',N'647',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Tình hình sơ sinh khi vào khoa',N'- Tình hình sơ sinh khi vào khoa',N'IPDMRPT109',N'IPDMRPT',N'IPDMRPT',N'1',N'648',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Vòng đầu: ',N'Vòng đầu: ',N'IPDMRPT110',N'IPDMRPT109',N'IPDMRPT',N'2',N'649',N'Text',N'cm',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Tình hình sơ sinh khi vào khoa',N'- Tình hình sơ sinh khi vào khoa',N'IPDMRPT111',N'IPDMRPT109',N'IPDMRPT',N'2',N'650',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Tình trạng toàn thân:',N'+ Tình trạng toàn thân:',N'IPDMRPT112',N'IPDMRPT',N'IPDMRPT',N'1',N'651',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Tình trạng toàn thân:',N'+ Tình trạng toàn thân:',N'IPDMRPT113',N'IPDMRPT112',N'IPDMRPT',N'2',N'652',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Mầu sắc da:',N'+ Mầu sắc da:',N'IPDMRPT114',N'IPDMRPT',N'IPDMRPT',N'1',N'653',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'1. Hồng hào',N'1. Hồng hào',N'IPDMRPT115',N'IPDMRPT114',N'IPDMRPT',N'2',N'654',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'2. Xanh tái',N'2. Xanh tái',N'IPDMRPT116',N'IPDMRPT114',N'IPDMRPT',N'2',N'655',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'3. Vàng',N'3. Vàng',N'IPDMRPT117',N'IPDMRPT114',N'IPDMRPT',N'2',N'656',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'4. Tím',N'4. Tím',N'IPDMRPT118',N'IPDMRPT114',N'IPDMRPT',N'2',N'657',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'5. Khác',N'5. Khác',N'IPDMRPT119',N'IPDMRPT114',N'IPDMRPT',N'2',N'658',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Màu sắc da:',N'- Màu sắc da:',N'IPDMRPT120',N'IPDMRPT114',N'IPDMRPT',N'2',N'659',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Nghe phổi:',N'+ Nghe phổi:',N'IPDMRPT121',N'IPDMRPT',N'IPDMRPT',N'1',N'660',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Nghe phổi:',N'+ Nghe phổi:',N'IPDMRPT122',N'IPDMRPT121',N'IPDMRPT',N'2',N'661',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Chỉ số Silverman:',N'+ Chỉ số Silverman:',N'IPDMRPT123',N'IPDMRPT',N'IPDMRPT',N'1',N'662',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Chỉ số Silverman:',N'+ Chỉ số Silverman:',N'IPDMRPT124',N'IPDMRPT123',N'IPDMRPT',N'2',N'663',N'Text',N'Điểm',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Tim mạch:',N'- Tim mạch:',N'IPDMRPT125',N'IPDMRPT',N'IPDMRPT',N'1',N'664',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Tim mạch:',N'- Tim mạch:',N'IPDMRPT126',N'IPDMRPT125',N'IPDMRPT',N'2',N'665',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Bụng:',N'- Bụng:',N'IPDMRPT127',N'IPDMRPT',N'IPDMRPT',N'1',N'666',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Bụng:',N'- Bụng:',N'IPDMRPT128',N'IPDMRPT127',N'IPDMRPT',N'2',N'667',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Cơ quan sinh dục ngoài:',N'- Cơ quan sinh dục ngoài:',N'IPDMRPT129',N'IPDMRPT',N'IPDMRPT',N'1',N'668',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Cơ quan sinh dục ngoài:',N'- Cơ quan sinh dục ngoài:',N'IPDMRPT130',N'IPDMRPT129',N'IPDMRPT',N'2',N'669',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Xương khớp',N'- Xương khớp',N'IPDMRPT131',N'IPDMRPT',N'IPDMRPT',N'1',N'670',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Xương khớp',N'- Xương khớp',N'IPDMRPT132',N'IPDMRPT131',N'IPDMRPT',N'2',N'671',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Thần kinh:',N'- Thần kinh:',N'IPDMRPT133',N'IPDMRPT',N'IPDMRPT',N'1',N'672',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Phản xạ:',N'+ Phản xạ:',N'IPDMRPT134',N'IPDMRPT',N'IPDMRPT',N'1',N'673',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'- Thần kinh , phản xạ',N'- Thần kinh , phản xạ',N'IPDMRPT135',N'IPDMRPT134',N'IPDMRPT',N'2',N'674',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Trương lực cơ:',N'+ Trương lực cơ:',N'IPDMRPT136',N'IPDMRPT',N'IPDMRPT',N'1',N'675',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'+ Trương lực cơ:',N'+ Trương lực cơ:',N'IPDMRPT137',N'IPDMRPT136',N'IPDMRPT',N'2',N'676',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'5. Chỉ định theo dõi:',N'5. Chỉ định theo dõi:',N'IPDMRPT138',N'IPDMRPT',N'IPDMRPT',N'1',N'677',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'5. Chỉ định theo dõi:',N'5. Chỉ định theo dõi:',N'IPDMRPT139',N'IPDMRPT138',N'IPDMRPT',N'2',N'678',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'3. Khác',N'3. Khác',N'IPDMRPT140',N'IPDMRPT74',N'IPDMRPT',N'2',N'679',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'II. Hỏi bệnh: (diễn biến của bệnh sơ sinh)',N'II. Hỏi bệnh: (diễn biến của bệnh sơ sinh)',N'IPDMRPT60',N'IPDMRPT',N'IPDMRPT',N'1',N'600',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhịp thở:',N'Nhịp thở:',N'IPDMRPT141',N'IPDMRPT',N'IPDMRPT',N'1',N'681',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhịp thở:',N'Nhịp thở:',N'IPDMRPT142',N'IPDMRPT141',N'IPDMRPT',N'2',N'682',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhịp tim:',N'Nhịp tim:',N'IPDMRPT143',N'IPDMRPT',N'IPDMRPT',N'1',N'683',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Nhịp tim:',N'Nhịp tim:',N'IPDMRPT144',N'IPDMRPT143',N'IPDMRPT',N'2',N'684',N'Text',N'',N'0',N'',N'', '1');
