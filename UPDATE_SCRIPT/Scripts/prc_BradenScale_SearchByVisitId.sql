CREATE PROCEDURE [dbo].[prc_BradenScale_SearchByVisitId]
	@VISIT_ID		VARCHAR(50) = NULL,
	@USER_NAME 		VARCHAR(50) = NULL,
	@FROM_DATE		VARCHAR(20) = '',
	@TO_DATE 		VARCHAR(20) = '',
	@PAGE_INDEX 	INT = 1,
	@PAGE_SIZE  	INT = 5,
	@ORDER_BY 		VARCHAR(50) = 'A.TRANS_DATE',
	@DIRECTION_SORT VARCHAR(50) = 'DESC',
	@TOTAL_ROW 		INT  OUTPUT	
AS
BEGIN
	--prc_BradenScale_SearchByVisitId NULL, '', '', '', 1, 15, NULL, NULL, 0
     
	SET NOCOUNT ON;      
	DECLARE @sqlMain  NVARCHAR(4000);
	DECLARE @sql  NVARCHAR(4000);
	DECLARE @sqlCount NVARCHAR(4000);
	DECLARE @sqlF NVARCHAR(4000);

	CREATE TABLE #temp(id int)

	IF (@ORDER_BY IS NULL)
	BEGIN
		SET @ORDER_BY = 'A.TRANS_DATE'
	END

	IF (@DIRECTION_SORT IS NULL)
	BEGIN
		SET @DIRECTION_SORT = 'DESC'
	END
	  
	SET @sql = N'SELECT ROW_NUMBER() OVER (Order By ' + @ORDER_BY + ' ' + @DIRECTION_SORT +') AS ''RowNum'', ';
	SET @sql = @sql + ' A.Id, A.VISIT_ID, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDBRADEN43'') AS TotalScore, ';
	
	SET @sql = @sql + ' ( SELECT STRING_AGG (ISNULL(X. VALUE, '' ''), ''; '') WITHIN GROUP (ORDER BY X.Code ASC) FROM
							( SELECT (ISNULL(Code,'''')  '+'+''-''+' + 'ISNULL([Value],''''))  AS Value, Code FROM FormDatas 
							 WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId = A.VISIT_ID AND FormId = A.Id  AND Code IN (''IPDBRADEN45'',''IPDBRADEN46'', ''IPDBRADEN47'', ''IPDBRADEN48'', ''IPDBRADEN49'', ''IPDBRADEN50'', ''IPDBRADEN51'', ''IPDBRADEN52'', ''IPDBRADEN53'', ''IPDBRADEN54'')
							) X )AS Intervention,';

	
	SET @sql = @sql + ' TRANS_DATE AS TransactionDate,CreatedBy, CreatedAt ';
	SET @sql = @sql + ' FROM IPD_BRADEN_SCALE AS A ';
	SET @sql = @sql + ' WHERE 1=1 '
    SET @sql = @sql + ' AND  A.IsDeleted = 0'
	IF (@VISIT_ID IS NOT NULL)
	BEGIN
		SET @sql = @sql + ' AND  A.VISIT_ID = ''' + @VISIT_ID + ''''
	END

	IF (@USER_NAME IS NOT NULL AND @USER_NAME <> '')
	BEGIN
		SET @sql = @sql + ' AND  A.CreatedBy = ''' + @USER_NAME + ''''
	END
    
	IF (@FROM_DATE = '' AND @TO_DATE <> '')
	BEGIN
		SET @sql = @sql + ' AND (A.TRANS_DATE <= ''' + @TO_DATE + ''' )'     
	END
	
	IF (@FROM_DATE <> '' AND @TO_DATE = '')
	BEGIN
		SET @sql = @sql + ' AND (A.TRANS_DATE >= ''' + @FROM_DATE + ''' )'     
	END
	
	IF (@FROM_DATE <> '' AND @TO_DATE <> '')
	BEGIN
		SET @sql = @sql + ' AND (A.TRANS_DATE >=  ''' + @FROM_DATE + ''' AND A.TRANS_DATE <= ''' + @TO_DATE + ''' )'     
	END
		
	SET @sqlMain = N'SELECT * FROM('+ @sql +')a WHERE a.RowNum BETWEEN (' + CONVERT(VARCHAR(10),@PAGE_INDEX) + '-1) * ' + CONVERT(VARCHAR(10),@PAGE_SIZE) + '+1 AND (' + CONVERT(VARCHAR(10),@PAGE_INDEX) + ' * ' + CONVERT(VARCHAR(10),@PAGE_SIZE)+') '
    
	SET @sqlCount = N'INSERT INTO #temp SELECT COUNT(id) AS ''Total'' FROM('+@sql+')a'
	EXECUTE (@sqlCount)

	SET @TOTAL_ROW = (SELECT id FROM #temp);
	
	print @sqlMain   

	EXEC sp_executesql @sqlMain;
	
	SELECT @TOTAL_ROW AS TotalRow;

	SELECT COUNT(1) AS RowExisted 
	FROM IPD_BRADEN_SCALE 
	WHERE VISIT_ID =  @VISIT_ID and IsDeleted = 0; 

	DROP TABLE #temp
End