ALTER PROCEDURE [dbo].[prc_VitalSignAdult_SearchByVisitId]
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
	--prc_VitalSignAdult_SearchByVisitId NULL, '', '', '', 1, 15, NULL, NULL, 0
     
	SET NOCOUNT ON;      
	DECLARE @sqlMain  NVARCHAR(MAX);
	DECLARE @sql  NVARCHAR(MAX);
	DECLARE @sqlCount NVARCHAR(MAX);
	DECLARE @sqlF NVARCHAR(MAX);

	CREATE TABLE #temp(id int)

	CREATE TABLE #tmpVital
	(
		FormId uniqueidentifier, 
		VisitId uniqueidentifier, 
		Code nvarchar(max), 
		Value nvarchar(max)
	)

	IF @VISIT_ID IS NULL
		INSERT INTO #tmpVital 
		SELECT FormId, VisitId, Code, Value 
		FROM FormDatas
		WHERE FormCode = 'A02_031_220321_V'
	ELSE
		INSERT INTO #tmpVital 
		SELECT FormId, VisitId, Code, Value 
		FROM FormDatas
		WHERE VisitId = @VISIT_ID
	
	
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
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS01'') AS BreathRate, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS04'') AS SPO2, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS06'') AS LowBP, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS07'') AS HighBP, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS09'') AS Pulse, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS11'') AS Temperature, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS14'') AS Sense, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS16'') AS RespiratorySuport, ';
	
	SET @sql = @sql + ' (SELECT STUFF((SELECT ''-'' + ISNULL([Value], '' '') FROM #tmpVital WHERE  VisitId=A.VISIT_ID AND FormId=A.Id AND Code IN (''IPDMEWS20'',''IPDMEWS21'') ORDER BY Code ASC FOR XML PATH('''')), 1, 1, '''')) AS PainScore, ';

	SET @sql = @sql + ' (
							SELECT STUFF((SELECT ''-'' + ISNULL([Value], '' '') FROM #tmpVital WHERE  VisitId=A.VISIT_ID AND FormId=A.Id AND Code IN (''IPDMEWS23'',''IPDMEWS24'') ORDER BY Code ASC FOR XML PATH('''')), 1, 1, '''')
						) AS CapillaryBlood, ';
	
	SET @sql = @sql + 'LTRIM((SELECT STUFF((
											SELECT ''; '' + ISNULL(X.VALUE, '' '') 
											FROM(
												SELECT (CASE Code WHEN ''IPDMEWS32'' THEN ''T('''+'+[Value]+'+'''ml)''
																WHEN ''IPDMEWS34'' THEN ''P('''+'+[Value]+'+'''ml)''
																WHEN ''IPDMEWS36'' THEN ''M('''+'+[Value]+'+'''ml)''
																WHEN ''IPDMEWS38'' THEN ''S('''+'+[Value]+'+'''ml)''
																WHEN ''IPDMEWS40'' THEN ''AN('''+'+[Value]+'+'''ml)''
																WHEN ''IPDMEWS42'' THEN ''D('''+'+[Value]+'+'''ml)'' END ) AS Value, Code
												FROM #tmpVital 
												WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId=A.VISIT_ID AND FormId=A.Id 
													AND Code IN (''IPDMEWS32'',''IPDMEWS34'', ''IPDMEWS36'', ''IPDMEWS38'', ''IPDMEWS40'', ''IPDMEWS42'')
											) AS X ORDER BY X.Code FOR XML PATH('''')
									), 1, 1,'''' ) )) AS FluidIn,';
	
	SET @sql = @sql + ' (SELECT [Value] AS Value FROM #tmpVital WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId=A.VISIT_ID AND FormId=A.Id AND Code IN (''IPDMEWS44'')) AS TotalFluidIn,';
	
	SET @sql = @sql + 'LTRIM((SELECT STUFF((
											SELECT ''; '' + ISNULL(X.VALUE, '' '') 
											FROM(
												SELECT (CASE Code WHEN ''IPDMEWS47'' THEN ''N('''+'+[Value]+'+'''ml)''
																  WHEN ''IPDMEWS49'' THEN ''Ph('''+'+[Value]+'+'''ml)''
																  WHEN ''IPDMEWS51'' THEN ''NT('''+'+[Value]+'+'''ml)''
																  WHEN ''IPDMEWS53'' THEN ''DL('''+'+[Value]+'+'''ml)'' END ) AS Value, Code
												FROM #tmpVital 
												WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId=A.VISIT_ID AND FormId=A.Id 
													AND Code IN (''IPDMEWS47'',''IPDMEWS49'', ''IPDMEWS51'', ''IPDMEWS53'')
											) AS X ORDER BY X.Code FOR XML PATH('''')
									), 1, 1,'''' ) )) AS FluidOut,';

	SET @sql = @sql + ' (SELECT [Value] AS Value FROM #tmpVital WHERE (Value <> '''' AND Value IS NOT NULL) AND VisitId=A.VISIT_ID AND FormId=A.Id AND Code IN (''IPDMEWS55'')) AS TotalFluidOut,';	

	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS57'') AS TotalBilan, ';
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS18'') AS TotalMews, ';	
	SET @sql = @sql + ' (SELECT [Value] FROM #tmpVital WHERE VisitId=A.VISIT_ID AND FormId=A.Id AND Code=''IPDMEWS27'') AS VIPScore, ';
	
	SET @sql = @sql + ' TRANS_DATE AS TransactionDate,CreatedBy, CreatedAt ';
	SET @sql = @sql + ' FROM IPD_VITALSIGN_ADULT AS A ';
	SET @sql = @sql + ' WHERE 1=1 '
    SET @sql = @sql + ' AND  A.IsDeleted = 0'
	
	IF (@VISIT_ID IS NOT NULL)
	BEGIN
		SET @sql = @sql + ' AND A.VISIT_ID = ''' + @VISIT_ID + ''''
	END

	IF (@USER_NAME IS NOT NULL AND @USER_NAME <> '')
	BEGIN
		SET @sql = @sql + ' AND A.CreatedBy = ''' + @USER_NAME + ''''
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
	DROP TABLE #tmpVital
End