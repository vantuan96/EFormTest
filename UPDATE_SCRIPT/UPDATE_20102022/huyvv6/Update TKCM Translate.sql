DECLARE @Id uniqueidentifier
DECLARE @VisitId uniqueidentifier
DECLARE @VisitTypeGroupCode nvarchar(max)
DECLARE @SpecId uniqueidentifier

DECLARE cursorProduct CURSOR FOR  
SELECT Id,VisitId, VisitTypeGroupCode FROM Translations     

OPEN cursorProduct                

FETCH NEXT FROM cursorProduct     
      INTO @Id,@VisitId, @VisitTypeGroupCode

WHILE @@FETCH_STATUS = 0          
BEGIN
                                  
    IF @VisitTypeGroupCode = 'IPD' 
		BEGIN
			SELECT @SpecId = SpecialtyId FROM IPDs WHERE Id = @VisitId
			PRINT @SpecId
			UPDATE Translations 
			SET SpecialtyId = @SpecId
			WHERE Id = @Id
		END
	ELSE  IF @VisitTypeGroupCode = 'OPD' 
		BEGIN
			SELECT @SpecId = SpecialtyId FROM OPDs WHERE Id = @VisitId
			PRINT @SpecId
			UPDATE Translations 
			SET SpecialtyId = @SpecId
			WHERE Id = @Id
		END
	ELSE  IF @VisitTypeGroupCode = 'ED' 
		BEGIN
			SELECT @SpecId = SpecialtyId FROM EDs WHERE Id = @VisitId
			PRINT @SpecId
			UPDATE Translations 
			SET SpecialtyId = @SpecId
			WHERE Id = @Id
		END
	ELSE  IF @VisitTypeGroupCode = 'EOC' 
		BEGIN
			SELECT @SpecId = SpecialtyId FROM EOCs WHERE Id = @VisitId
			PRINT @SpecId
			UPDATE Translations 
			SET SpecialtyId = @SpecId
			WHERE Id = @Id
		END
   

    FETCH NEXT FROM cursorProduct 
          INTO @Id,@VisitId, @VisitTypeGroupCode
END

CLOSE cursorProduct             
DEALLOCATE cursorProduct         