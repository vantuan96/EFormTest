insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[XEM]Timline hồ sơ bệnh nhân' , N'SHOWTIMELINE',    NULL);

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] PROM bệnh nhân suy tim' , N'IPDPFFCMM',    (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] PROM bệnh nhân suy tim' , N'IPDPFFUMM',    (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XÁC NHẬN] PROM bệnh nhân suy tim' , N'IPDPFFCMMF',    (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] PROM bệnh nhân suy tim' , N'IPDPFFGMM'   , (select id from VisitTypeGroups where Code = 'IPD'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] PROM bệnh nhân suy tim' , N'OPDPFFCMM',     (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] PROM bệnh nhân suy tim' , N'OPDPFFUMM',    (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XÁC NHẬN] PROM bệnh nhân suy tim' , N'OPDPFFCMMF',       (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] PROM bệnh nhân suy tim' , N'OPDPFFGMM',   (select id from VisitTypeGroups where Code = 'OPD'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3C', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3U', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3CF', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3C', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3U', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3CF', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));


insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3C', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3U', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3CF', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][TẠO MỚI] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3C', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][CHỈNH SỬA] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3U', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XÁC NHẬN] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3CF', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));



--OPD được quyền xem 3 khu vực còn lại
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'OPD'));



--IPD được quyền xem 3 khu vực còn lại
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'IPD'));

--ED được xem các khu vực còn lại

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GL', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[EOC][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EOCSAPSV3GD', (select id from VisitTypeGroups where Code = 'ED'));


--EOC được xem cái khu vực còn lại

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'OPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'IPDSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM DANH SÁCH] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GL', (select id from VisitTypeGroups where Code = 'EOC'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][XEM CHI TIẾT] TÓM TẮT PHẪU THUẬT' , N'EDSAPSV3GD', (select id from VisitTypeGroups where Code = 'EOC'));

insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XEM] PROM bệnh nhân mạch vành' , N'IPDPROMFCD1', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][TẠO MỚI] PROM bệnh nhân mạch vành' , N'IPDPROMFCD2', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][CHỈNH SỬA] PROM bệnh nhân mạch vành' , N'IPDPROMFCD3', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][XÁC NHẬN] PROM bệnh nhân mạch vành' , N'IPDPROMFCD4', (select id from VisitTypeGroups where Code = 'IPD'));
	
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XEM] PROM bệnh nhân mạch vành' , N'OPDPROMFCD1', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][TẠO MỚI] PROM bệnh nhân mạch vành' , N'OPDPROMFCD2', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][CHỈNH SỬA] PROM bệnh nhân mạch vành' , N'OPDPROMFCD3', (select id from VisitTypeGroups where Code = 'OPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[OPD][XÁC NHẬN] PROM bệnh nhân mạch vành' , N'OPDPROMFCD4', (select id from VisitTypeGroups where Code = 'OPD'));


-- phân quyền chỉ định cận lâm sàng
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xem]Lịch sử chỉ định cận lâm sàng' , N'XEMCLSBYPID');
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code) values (NEWID(), GETDATE(), GETDATE(), 'False',N'In Lịch sử cận lâm sàng' , N'ShowPrint');

-- phân quyền + Unlock ghi nhận thuốc standing order
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xem] Ghi nhận thực hiện thuốc Order Standing' , N'ORDERVIEW', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Tạo mới] Ghi nhận thực hiện thuốc Order Standing' , N'ORDERCREATE', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xác nhận] Ghi nhận thực hiện thuốc Order Standing' , N'ORDERCONFIRM', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xác nhận toàn bộ] Ghi nhận thực hiện thuốc Order Standing' , N'ORDERCONFIRMALL', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Xóa] Ghi nhận thực hiện thuốc Order Standing' , N'ORDERDELETE', (select id from VisitTypeGroups where Code = 'IPD'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[ED][Bác sỹ xác nhận] Phiếu dự trù và chế phẩm máu' , N'EDBSXNPDTM', (select id from VisitTypeGroups where Code = 'ED'));
insert into Actions  (Id, CreatedAt, UpdatedAt, IsDeleted, Name , Code, VisitTypeGroupId) values (NEWID(), GETDATE(), GETDATE(), 'False',N'[IPD][Bác sỹ xác nhận] Phiếu dự trù và chế phẩm máu' , N'BSXNPDTM', (select id from VisitTypeGroups where Code = 'IPD'));



INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'IPD Ghi nhận thực hiện thuốc standing order','A03_029_050919_VE','IPD',0)


 -- Unlock Bệnh án tim mạch
 INSERT INTO Forms(Id, CreatedAt, UpdatedAt, [Name], Code, VisitTypeGroupCode,IsDeleted)
VALUES(NEWID(),GETDATE(),GETDATE(),N'Bệnh án tim mạch','BMTIMMACH','IPD',0)

--phân quyền phiếu dự trù bác sĩ xác nhận ED+IPD


insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'IPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'OPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'ED')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'EOC')


insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'PROM bệnh nhân suy tim', N'PROMFHF', 'IPD')
insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'PROM bệnh nhân suy tim', N'PROMFHF', 'OPD')


insert into [Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'ED')
insert into [Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'EOC')
insert into [Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'IPD')
insert into [Forms] (Id,IsDeleted, Name, Code, VisitTypeGroupCode) values (NEWID(), 0, N'Tóm tắt phẫu thuật', N'A01_085_120522_VE', 'OPD')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'PROM bệnh nhân mạch vành', N'PROMFCD', 'IPD')


insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'PROM bệnh nhân mạch vành', N'PROMFCD', 'OPD')



insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Biên bản hội chẩn', N'A01_057_050919_VE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Biên bản hội chẩn', N'A01_057_050919_VE', 'EOC')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Bảng đánh giá nhu cầu trang thiết bị/ nhân lực vận chuyển ngoại viện', N'A02_055_050919_V', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu truyền máu', N'A02_74_050919_VE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Bảng hồi sinh tim phổi', N'EDCAARRE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Bảng hồi sinh tim phổi', N'EOCCAARRE', 'EOC')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Biên bản hội chẩn bệnh nhân sử dụng thuốc có dấu sao(*)', N'A01_058_050919_VE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Giấy chứng nhận thương tích', N'EDEINCE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Biên bản hội chẩn thông qua mổ', N'A01_059_120522_VE', 'ED')

Update Forms  
set Name = N'Ghi nhận thực hiện thuốc standing order' where Code = N'A03_029_050919_VE'

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Ghi nhận thực hiện thuốc standing order', N'A03_029_050919_VE', 'OPD')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Ghi nhận thực hiện thuốc standing order', N'A03_029_050919_VE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Ghi nhận thực hiện thuốc standing order', N'A03_029_050919_VE', 'EOC')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu tóm tắt thủ thuật', N'EDPSV2', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu tóm tắt thủ thuật', N'EOCPSV2', 'EOC')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Biên bản kiểm thảo tử vong', N'EDMORE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu ghi nhận sử dụng thuốc do BN mang vào', N'A03_051_120421_VE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Xét nghiệm tại chỗ - Khí máu Cartridge CG4', N'A03_038_080322_V', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu phẫu thuật/ thủ thuật', N'A01_085_050919_VE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu phẫu thuật/ thủ thuật', N'A01_085_050919_VE', 'EOC')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Kết quả test da', N'ESKTR', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Kết quả test da', N'EOCSKTR', 'EOC')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Biên bản hội chẩn sử dụng kháng sinh cần ưu tiên quản lý', N'A01_060_120421_VE', 'ED')

insert into Forms (Id,IsDeleted, Name, Code, VisitTypeGroupCode)
values (NEWID(), 0, N'Phiếu dự trù máu', N'EDPDTM', 'ED')
