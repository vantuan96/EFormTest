CREATE PROCEDURE [dbo].[prc_VitalSignAdult_SearchByVisitId]
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
	--prc_VitalSignAdult_SearchByVisitId '25948E30-7B7F-495A-B3C2-9DF64070514A', '', '2022-01-07 16:20:00', '2022-01-07 16:50:00', 1, 15, NULL, NULL, 0
     
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
	
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS01'') AS BreathRate, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS04'') AS SPO2, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS06'') AS LowBP, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS07'') AS HighBP, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS09'') AS Pulse, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS11'') AS Temperature, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS14'') AS Sense, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS16'') AS RespiratorySuport, ';
	SET @sql = @sql + ' (SELECT STRING_AGG( ISNULL([Value], '' ''), ''-'') WITHIN GROUP (ORDER BY Code ASC) FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code IN (''IPDMEWS20'',''IPDMEWS21'')) AS PainScore, ';
	SET @sql = @sql + ' (SELECT STRING_AGG( ISNULL([Value], '' ''), ''-'') WITHIN GROUP (ORDER BY Code ASC) FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code IN (''IPDMEWS23'',''IPDMEWS24'')) AS CapillaryBlood, ';
	
	SET @sql = @sql + ' (SELECT STRING_AGG( ISNULL(X.Value, '' ''), ''; '') WITHIN GROUP (ORDER BY X.Code ASC) FROM (SELECT ( CASE code
									when ''IPDMEWS32'' then ''T('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS34'' then ''P('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS36'' then ''M('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS38'' then ''S('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS40'' then ''AN('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS42'' then ''D('''+'+[Value]+'+'''ml)'' END) AS Value, Code
						FROM FormDatas WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId = A.VISIT_ID AND FormId = A.Id AND Code IN (''IPDMEWS32'',''IPDMEWS34'', ''IPDMEWS36'', ''IPDMEWS38'', ''IPDMEWS40'', ''IPDMEWS42'')) X) AS FluidIn,';

	SET @sql = @sql + ' (SELECT [Value] AS Value FROM FormDatas WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId = A.VISIT_ID AND FormId = A.Id AND Code IN (''IPDMEWS44'')) AS TotalFluidIn,';

	SET @sql = @sql + ' (SELECT STRING_AGG( ISNULL(X.Value, '' ''), ''; '') WITHIN GROUP (ORDER BY X.Code ASC) FROM (SELECT ( CASE code
									when ''IPDMEWS47'' then ''N('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS49'' then ''Ph('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS51'' then ''NT('''+'+[Value]+'+'''ml)''
									when ''IPDMEWS53'' then ''DL('''+'+[Value]+'+'''ml)'' END) AS Value, Code
						FROM FormDatas WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId = A.VISIT_ID AND FormId = A.Id AND Code IN (''IPDMEWS47'',''IPDMEWS49'', ''IPDMEWS51'', ''IPDMEWS53'')) X) AS FluidOut,';
	
	SET @sql = @sql + ' (SELECT [Value] AS Value FROM FormDatas WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId = A.VISIT_ID AND FormId = A.Id AND Code IN (''IPDMEWS55'')) AS TotalFluidOut,';	

	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS57'') AS TotalBilan, ';
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS18'') AS TotalMews, ';
	
	SET @sql = @sql + ' (SELECT [Value] FROM FormDatas WHERE VisitId = A.VISIT_ID AND FormId = A.Id AND Code = ''IPDMEWS27'') AS VIPScore, ';
	
	SET @sql = @sql + ' TRANS_DATE AS TransactionDate,CreatedBy, CreatedAt ';
	SET @sql = @sql + ' FROM IPD_VITALSIGN_ADULT AS A ';
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
	FROM IPD_VITALSIGN_ADULT 
	WHERE VISIT_ID =  @VISIT_ID and IsDeleted = 0; 

	DROP TABLE #temp
End