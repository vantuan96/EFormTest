update EDPointOfCareTestingMasterDatas
Set Version = 1
WHERE Version = 0;

DECLARE @id uniqueidentifier
DECLARE @Order int
DECLARE @flag6 int = 0;
DECLARE @flag4 int = 0;

DECLARE cursorProduct CURSOR FOR  -- khai báo con trỏ cursorProduct
SELECT Id, [Order] from  EDPointOfCareTestingMasterDatas where  Form = 'EDChemicalBiologyTest' and Version = 1  order by [Order]    -- dữ liệu trỏ tới

OPEN cursorProduct                -- Mở con trỏ

FETCH NEXT FROM cursorProduct     -- Đọc dòng đầu tiên
      INTO @id, @Order

WHILE @@FETCH_STATUS = 0          --vòng lặp WHILE khi đọc Cursor thành công
BEGIN
                                  --In kết quả hoặc thực hiện bất kỳ truy vấn
                                  --nào dựa trên kết quả đọc được
	
	WAITFOR DELAY '00:00:01';
	DECLARE @currentDate DateTime = GETDATE();
	Print @Order
	IF @Order < 6 
		BEGIN
			IF @Order = 4
			BEGIN
				IF @flag4 = 0
				BEGIN
					DECLARE @id4 uniqueidentifier
					
					DECLARE cursorProduct4 CURSOR FOR  -- khai báo con trỏ cursorProduct
					SELECT Id from  EDPointOfCareTestingMasterDatas where  Form = 'EDChemicalBiologyTest' and Version = 1  and [Order] = 4  order by HigherAlert    -- dữ liệu trỏ tới

					OPEN cursorProduct4                -- Mở con trỏ

					FETCH NEXT FROM cursorProduct4     -- Đọc dòng đầu tiên
						  INTO @id4
						  WHILE @@FETCH_STATUS = 0          --vòng lặp WHILE khi đọc Cursor thành công
					BEGIN
									
									WAITFOR DELAY '00:00:01'
									DECLARE @currentDate4 DateTime = GETDATE()
									Print @id4
									UPDATE EDPointOfCareTestingMasterDatas
									SET CreatedAt = @currentDate4,UpdatedAt = @currentDate4
									WHERE Id = @id4  and Form = 'EDChemicalBiologyTest' and Version = 1;
							FETCH NEXT FROM cursorProduct4 -- Đọc dòng tiếp
							INTO @id4
					END
					CLOSE cursorProduct4           -- Đóng Cursor
					DEALLOCATE cursorProduct4
					SET @flag4 = 1;
				END
			END
			ELSE
				BEGIN
				print @id
					UPDATE EDPointOfCareTestingMasterDatas
					SET CreatedAt = @currentDate,UpdatedAt = @currentDate
					WHERE Id = @id  and Form = 'EDChemicalBiologyTest' and Version = 1;
				END;
		END
	ELSE IF @Order =6
		BEGIN
			 IF @flag6 = 0 
				 BEGIN 
					 DECLARE @id6 uniqueidentifier
					
						DECLARE cursorProduct6 CURSOR FOR  -- khai báo con trỏ cursorProduct
						SELECT Id from  EDPointOfCareTestingMasterDatas where  Form = 'EDChemicalBiologyTest' and Version = 1  and [Order] = 6  order by HigherAlert    -- dữ liệu trỏ tới

						OPEN cursorProduct6               -- Mở con trỏ

						FETCH NEXT FROM cursorProduct6     -- Đọc dòng đầu tiên
							  INTO @id6
							  WHILE @@FETCH_STATUS = 0          --vòng lặp WHILE khi đọc Cursor thành công
						BEGIN
											WAITFOR DELAY '00:00:01'
											DECLARE @currentDate6 DateTime = GETDATE()
											UPDATE EDPointOfCareTestingMasterDatas
											SET CreatedAt = @currentDate6,UpdatedAt = @currentDate6
											WHERE Id = @id6  and Form = 'EDChemicalBiologyTest' and Version = 1;

										 FETCH NEXT FROM cursorProduct6 -- Đọc dòng tiếp
										 INTO @id6
						END
						CLOSE cursorProduct6              -- Đóng Cursor
						DEALLOCATE cursorProduct6
						SET @flag6 = 1;
				 END	
		END
	ELSE IF @Order > 6
		BEGIN
			print '>6'
											UPDATE EDPointOfCareTestingMasterDatas
											SET CreatedAt = @currentDate,UpdatedAt = @currentDate
											WHERE Id = @id and Form = 'EDChemicalBiologyTest' and Version = 1;
		END; 
	
    

    FETCH NEXT FROM cursorProduct -- Đọc dòng tiếp
          INTO @id, @Order
END
CLOSE cursorProduct              -- Đóng Cursor
DEALLOCATE cursorProduct  
