-- thangdc3


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[fuChuyenCoDauThanhKhongDau]
(
      @strInput NVARCHAR(4000)
)
RETURNS NVARCHAR(4000)
AS
BEGIN  
    IF @strInput IS NULL RETURN @strInput
    IF @strInput = '' RETURN @strInput
    DECLARE @RT NVARCHAR(4000)
    DECLARE @SIGN_CHARS NCHAR(136)
    DECLARE @UNSIGN_CHARS NCHAR (136)
    SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế
                  ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý
                  ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ
                  ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ'
                  +NCHAR(272)+ NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee
                  iiiiiooooooooooooooouuuuuuuuuuyyyyy
                  AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII
                  OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
    SET @COUNTER = 1
    WHILE (@COUNTER <=LEN(@strInput))
    BEGIN
      SET @COUNTER1 = 1
      --Tìm trong chuỗi mẫu
       WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1)
       BEGIN
     IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1))
            = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
     BEGIN        
          IF @COUNTER=1
              SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1)
              + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1)
          ELSE
              SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1)
              +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1)
              + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)
              BREAK
               END
             SET @COUNTER1 = @COUNTER1 +1
       END
      --Tìm tiếp
       SET @COUNTER = @COUNTER +1
    END
    SET @strInput = replace(@strInput,' ',' ')
    RETURN @strInput
END


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Proc_CreateOrUpdateICD10]
(
	@ViName NVARCHAR(MAX),
	@EnName NVARCHAR(MAX),
	@Code VARCHAR(100)
	--@GroupCode NVARCHAR(MAX)
)
AS
BEGIN 
	IF((SELECT COUNT(Id) FROM ICD10 WHERE Code = @Code) > 0)
	BEGIN 
		UPDATE ICD10 SET ViName = @ViName, EnName = @EnName
								,ViNameWithoutSign =(select [dbo].[fuChuyenCoDauThanhKhongDau](@ViName))
								+ ' ' + @EnName
								, UpdatedAt = GETDATE()
						 
					 WHERE Code = @Code
	END
	ELSE
	BEGIN
		INSERT INTO ICD10(Id, IsDeleted, CreatedAt, UpdatedAt, ViName, EnName, ViNameWithoutSign, Code)
		VALUES
		(NEWID(), 0, GETDATE(), GETDATE(), @ViName, @EnName
		,(SELECT [dbo].[fuChuyenCoDauThanhKhongDau](@ViName)) + ' ' + @EnName  , @Code)

	END
END
update MasterDatas set ViName = N', sớm hơn nếu các triệu chứng diễn tiến xấu hơn hoặc bệnh nhân có bất kỳ mối quan tâm hoặc thắc mắc nào.', EnName = N', sớm hơn nếu các triệu chứng diễn tiến xấu hơn hoặc bệnh nhân có bất kỳ mối quan tâm hoặc thắc mắc nào.' where code = 'OPDOEN261002'

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh án ngoại khoa',N'The surgical medical record)',N'TheSurgicalMedicalRecord',N'MedicalRecords',N'A01_195_050919_V',N'',N'',N'',N'',N'0',N'',N'', '1');


INSERT INTO [dbo].[Forms]([Id], [IsDeleted], [CreatedAt], [UpdatedAt], [Name], [Code], [VisitTypeGroupCode]) VALUES (NEWID(), 0, '2022-03-03 00:00:00.000', '2022-03-03 00:00:00.000',N'Bệnh án ngoại khoa', 'A01_195_050919_V', 'IPD')
update MasterDatas set DataType = 'Checkbox' where code = 'TFTEOCANS'
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh ngoại khoa',N'Bệnh ngoại khoa',N'IPDMRPTBNK0001',N'IPDMRPT',N'IPDMRPT',N'1',N'2000',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bệnh ngoại khoa',N'Bệnh ngoại khoa',N'IPDMRPTBNK00010001',N'IPDMRPTBNK0001',N'IPDMRPT',N'2',N'2000',N'Text',N'',N'0',N'',N'', '1');


--hunglq

Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bảng phẫu thuật',N'Bảng phẫu thuật',N'IPDPHUTBL1',N'IPDPHUTBL',N'IPDPHUTBL',N'1',N'1',N'Label',N'',N'0',N'',N'Pediatric', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Giờ, Ngày',N'Giờ, Ngày',N'IPDPHUTBL2',N'IPDPHUTBL1',N'IPDPHUTBL',N'2',N'2',N'',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Phương pháp phẫu thuật/vô cảm',N'Phương pháp phẫu thuật/vô cảm',N'IPDPHUTBL3',N'IPDPHUTBL1',N'IPDPHUTBL',N'2',N'3',N'TextArea',N'',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bác sỹ phẫu thuật',N'Bác sỹ phẫu thuật',N'IPDPHUTBL4',N'IPDPHUTBL1',N'IPDPHUTBL',N'2',N'4',N'Text',N'getUser',N'',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bác sỹ gây mê',N'Bác sỹ gây mê',N'IPDPHUTBL5',N'IPDPHUTBL1',N'IPDPHUTBL',N'2',N'5',N'Text',N'getUser',N'',N'',N'', '1');


Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'3. Tiền sử sản phụ khoa:',N'3. Tiền sử sản phụ khoa:',N'IPDMRPT900',N'IPDMRPT',N'IPDMRPT',N'1',N'900',N'Label',N'',N'0',N'',N'Pediatric', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bắt đầu thấy kinh năm',N'Bắt đầu thấy kinh năm',N'IPDMRPT901',N'IPDMRPT',N'IPDMRPT',N'1',N'901',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Bắt đầu thấy kinh năm',N'Bắt đầu thấy kinh năm',N'IPDMRPT902',N'IPDMRPT901',N'IPDMRPT',N'2',N'902',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tuổi',N'Tuổi',N'IPDMRPT903',N'IPDMRPT',N'IPDMRPT',N'1',N'903',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tuổi',N'Tuổi',N'IPDMRPT904',N'IPDMRPT903',N'IPDMRPT',N'2',N'904',N'Datetime',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tính chất kinh nguyệt',N'Tính chất kinh nguyệt',N'IPDMRPT905',N'IPDMRPT',N'IPDMRPT',N'1',N'905',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tính chất kinh nguyệt',N'Tính chất kinh nguyệt',N'IPDMRPT906',N'IPDMRPT905',N'IPDMRPT',N'2',N'906',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chu kỳ',N'Chu kỳ',N'IPDMRPT907',N'IPDMRPT',N'IPDMRPT',N'1',N'907',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Chu kỳ',N'Chu kỳ',N'IPDMRPT908',N'IPDMRPT907',N'IPDMRPT',N'2',N'908',N'Datetime',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Số ngày thấy kinh',N'Số ngày thấy kinh',N'IPDMRPT909',N'IPDMRPT',N'IPDMRPT',N'1',N'909',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Số ngày thấy kinh',N'Số ngày thấy kinh',N'IPDMRPT910',N'IPDMRPT909',N'IPDMRPT',N'2',N'910',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lượng kinh',N'Lượng kinh',N'IPDMRPT911',N'IPDMRPT',N'IPDMRPT',N'1',N'911',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lượng kinh',N'Lượng kinh',N'IPDMRPT912',N'IPDMRPT911',N'IPDMRPT',N'2',N'912',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kinh lần cuối ngày',N'Kinh lần cuối ngày',N'IPDMRPT913',N'IPDMRPT',N'IPDMRPT',N'1',N'913',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Kinh lần cuối ngày',N'Kinh lần cuối ngày',N'IPDMRPT914',N'IPDMRPT913',N'IPDMRPT',N'2',N'914',N'Datetime',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đau bụng',N'Đau bụng',N'IPDMRPT915',N'IPDMRPT',N'IPDMRPT',N'1',N'915',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Đau bụng',N'Đau bụng',N'IPDMRPT916',N'IPDMRPT915',N'IPDMRPT',N'2',N'916',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'1. Trước',N'1. Trước',N'IPDMRPT917',N'IPDMRPT950',N'IPDMRPT',N'2',N'917',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'2. Trong',N'2. Trong',N'IPDMRPT918',N'IPDMRPT950',N'IPDMRPT',N'2',N'918',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'3. Sau',N'3. Sau',N'IPDMRPT919',N'IPDMRPT950',N'IPDMRPT',N'2',N'919',N'Checkbox',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lấy chồng năm',N'Lấy chồng năm',N'IPDMRPT920',N'IPDMRPT',N'IPDMRPT',N'1',N'920',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Lấy chồng năm',N'Lấy chồng năm',N'IPDMRPT921',N'IPDMRPT920',N'IPDMRPT',N'2',N'921',N'Datetime',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tuổi',N'Tuổi',N'IPDMRPT922',N'IPDMRPT920',N'IPDMRPT',N'2',N'922',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hết kinh năm',N'Hết kinh năm',N'IPDMRPT923',N'IPDMRPT',N'IPDMRPT',N'1',N'923',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hết kinh năm',N'Hết kinh năm',N'IPDMRPT924',N'IPDMRPT923',N'IPDMRPT',N'2',N'924',N'Datetime',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tuổi',N'Tuổi',N'IPDMRPT925',N'IPDMRPT923',N'IPDMRPT',N'2',N'925',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Những bệnh phụ khoa đã điều trị',N'Những bệnh phụ khoa đã điều trị',N'IPDMRPT926',N'IPDMRPT',N'IPDMRPT',N'1',N'926',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Những bệnh phụ khoa đã điều trị',N'Những bệnh phụ khoa đã điều trị',N'IPDMRPT927',N'IPDMRPT926',N'IPDMRPT',N'2',N'927',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'4. Tiền sử sản khoa',N'4. Tiền sử sản khoa',N'IPDMRPT928',N'IPDMRPT',N'IPDMRPT',N'1',N'928',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Da niêm mạc',N'Da niêm mạc',N'IPDMRPT929',N'IPDMRPTTTYT2',N'IPDMRPT',N'2',N'929',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Hạch',N'Hạch',N'IPDMRPT930',N'IPDMRPTTTYT2',N'IPDMRPT',N'2',N'930',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Vú',N'Vú',N'IPDMRPT931',N'IPDMRPTTTYT2',N'IPDMRPT',N'2',N'931',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Khác',N'Khác',N'IPDMRPT932',N'IPDMRPTCACQ2',N'IPDMRPT',N'2',N'932',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'3. Khám chuyên khoa',N'3. Khám chuyên khoa',N'IPDMRPT933',N'IPDMRPT',N'IPDMRPT',N'1',N'933',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'a. Khám ngoài',N'a. Khám ngoài',N'IPDMRPT934',N'IPDMRPT',N'IPDMRPT',N'1',N'934',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các dấu hiệu sinh dục thứ phát',N'Các dấu hiệu sinh dục thứ phát',N'IPDMRPT935',N'IPDMRPT934',N'IPDMRPT',N'2',N'935',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Môi lớn',N'Môi lớn',N'IPDMRPT936',N'IPDMRPT934',N'IPDMRPT',N'2',N'936',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Môi bé',N'Môi bé',N'IPDMRPT937',N'IPDMRPT934',N'IPDMRPT',N'2',N'937',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Âm vật',N'Âm vật',N'IPDMRPT938',N'IPDMRPT934',N'IPDMRPT',N'2',N'938',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Âm hộ',N'Âm hộ',N'IPDMRPT939',N'IPDMRPT934',N'IPDMRPT',N'2',N'939',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Màng trinh',N'Màng trinh',N'IPDMRPT940',N'IPDMRPT934',N'IPDMRPT',N'2',N'940',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Tầng sinh môn',N'Tầng sinh môn',N'IPDMRPT941',N'IPDMRPT934',N'IPDMRPT',N'2',N'941',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'b. Khám trong',N'b. Khám trong',N'IPDMRPT942',N'IPDMRPT',N'IPDMRPT',N'1',N'942',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thân tử cung',N'Thân tử cung',N'IPDMRPT943',N'IPDMRPT',N'IPDMRPT',N'1',N'943',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thân tử cung',N'Thân tử cung',N'IPDMRPT944',N'IPDMRPT943',N'IPDMRPT',N'2',N'944',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các túi cùng',N'Các túi cùng',N'IPDMRPT945',N'IPDMRPT',N'IPDMRPT',N'1',N'945',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Các túi cùng',N'Các túi cùng',N'IPDMRPT946',N'IPDMRPT945',N'IPDMRPT',N'2',N'946',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'4. Các xét nghiệm cận lâm sàng cần làm',N'4. Các xét nghiệm cận lâm sàng cần làm',N'IPDMRPT947',N'IPDMRPT',N'IPDMRPT',N'1',N'947',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'4. Các xét nghiệm cận lâm sàng cần làm',N'4. Các xét nghiệm cận lâm sàng cần làm',N'IPDMRPT948',N'IPDMRPT947',N'IPDMRPT',N'2',N'948',N'Text',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'Thời gian',N'Thời gian',N'IPDMRPT949',N'IPDMRPT',N'IPDMRPT',N'1',N'949',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'1. Toàn thân',N'1. Toàn thân',N'IPDMRPTTTYT2',N'IPDMRPT',N'IPDMRPT',N'1',N'950',N'Label',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'2.Trong 48 giờ vào viện',N'2.Trong 48 giờ vào viện',N'IPDMRPT951',N'IPDMRPTTTTV',N'IPDMRPG',N'2',N'951',N'Radio',N'',N'0',N'',N'', '1');
Insert into MasterDatas  (Id, CreatedAt, UpdatedAt, IsDeleted,ViName, EnName, Code, [Group], Form, [Level], [Order], DataType, Note, IsReadOnly,Data, Clinic, [Version]) values (NEWID(), GETDATE(), GETDATE(), 'False', N'3. Trong 72 giờ vào viện',N'3. Trong 72 giờ vào viện',N'IPDMRPT952',N'IPDMRPTTTTV',N'IPDMRPG',N'2',N'952',N'Radio',N'',N'0',N'',N'', '1');


--haulv


