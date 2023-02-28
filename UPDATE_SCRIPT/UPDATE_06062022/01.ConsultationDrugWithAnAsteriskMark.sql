--Khu vực IPD
-- Bảng Thuốc dấu sao khu IPD
--Chạy cái này trước
-- Chạy toàn bộ từ trên xuống dưới đến hết scrips vẫn đc không cần chạy từng đoạn 
DECLARE @NumberRow INT;
DECLARE @IndexRow INT;

SET @NumberRow = (select count(*) from IPDs
					where IPDConsultationDrugWithAnAsteriskMarkId = IPDConsultationDrugWithAnAsteriskMarkId);
SET @IndexRow = 1;

WHILE @IndexRow <= @NumberRow
BEGIN
	UPDATE IPDConsultationDrugWithAnAsteriskMarks 
			SET VisitId = (
							SELECT A.Visit FROM (SELECT STT=ROW_NUMBER() OVER(ORDER BY I.Id ), I.Id[Visit], I.IPDConsultationDrugWithAnAsteriskMarkId, M.Id FROM IPDs I
							INNER JOIN IPDConsultationDrugWithAnAsteriskMarks M
							ON I.IPDConsultationDrugWithAnAsteriskMarkId = M.Id) AS A
							WHERE A.STT = @IndexRow)
			WHERE Id = (
						SELECT A.IPDConsultationDrugWithAnAsteriskMarkId FROM (SELECT STT=ROW_NUMBER() OVER(ORDER BY I.Id ), I.Id[Visit], I.IPDConsultationDrugWithAnAsteriskMarkId, M.Id FROM IPDs I
						INNER JOIN IPDConsultationDrugWithAnAsteriskMarks M
						ON I.IPDConsultationDrugWithAnAsteriskMarkId = M.Id) AS A
						WHERE A.STT = @IndexRow);
	SET @IndexRow = @IndexRow + 1;
END
GO

-- Bảng Orders khu IPD
-- chạy cái này sau
DECLARE @NumberRow INT;
DECLARE @IndexRow INT;

SET @NumberRow = (select COUNT(*) from Orders
					where VisitId IN (SELECT VisitId FROM IPDConsultationDrugWithAnAsteriskMarks)
					AND OrderType = 'IPD-DrugWithAnAsteriskMark');
SET @IndexRow = 1;

WHILE @IndexRow <= @NumberRow
BEGIN
	UPDATE Orders SET FormId =(SELECT (SELECT Id FROM IPDConsultationDrugWithAnAsteriskMarks WHERE VisitId = A.VisitId)[FORMID] 	
										FROM 
										(SELECT STT= ROW_NUMBER() OVER(ORDER BY Id), * from Orders
										where VisitId IN (SELECT VisitId FROM IPDConsultationDrugWithAnAsteriskMarks)
										AND OrderType = 'IPD-DrugWithAnAsteriskMark') AS A
										WHERE A.STT = @IndexRow)
				  WHERE Id = (SELECT Id 	
										FROM 
										(SELECT STT= ROW_NUMBER() OVER(ORDER BY Id), * from Orders
										where VisitId IN (SELECT VisitId FROM IPDConsultationDrugWithAnAsteriskMarks)
										AND OrderType = 'IPD-DrugWithAnAsteriskMark') AS A
										WHERE A.STT = @IndexRow) ;
	SET @IndexRow = @IndexRow + 1;
END
GO

-- Khu vực Ed 
-- Bảng thuốc dấu sao khu vực ED

--Chạy cái này trước
DECLARE @NumberRow INT;
DECLARE @IndexRow INT;

SET @NumberRow = (select count(*) from EDs
					where EDConsultationDrugWithAnAsteriskMarkId = EDConsultationDrugWithAnAsteriskMarkId);
SET @IndexRow = 1;

WHILE @IndexRow <= @NumberRow
BEGIN
	UPDATE EDConsultationDrugWithAnAsteriskMarks 
			SET VisitId = (
							SELECT A.Visit FROM (SELECT STT=ROW_NUMBER() OVER(ORDER BY E.Id ), E.Id[Visit], E.EDConsultationDrugWithAnAsteriskMarkId, M.Id FROM EDs E
							INNER JOIN EDConsultationDrugWithAnAsteriskMarks M
							ON E.EDConsultationDrugWithAnAsteriskMarkId = M.Id) AS A
							WHERE A.STT = @IndexRow)
			WHERE Id = (
						SELECT A.EDConsultationDrugWithAnAsteriskMarkId FROM (SELECT STT=ROW_NUMBER() OVER(ORDER BY E.Id ), E.Id[Visit], E.EDConsultationDrugWithAnAsteriskMarkId, M.Id FROM EDs E
						INNER JOIN EDConsultationDrugWithAnAsteriskMarks M
						ON E.EDConsultationDrugWithAnAsteriskMarkId = M.Id) AS A
						WHERE A.STT = @IndexRow);
	SET @IndexRow = @IndexRow + 1;
END
GO

-- Bảng Orders khu ED
-- chạy cái này sau
DECLARE @NumberRow INT;
DECLARE @IndexRow INT;

SET @NumberRow = (select COUNT(*) from Orders
					where VisitId IN (SELECT VisitId FROM EDConsultationDrugWithAnAsteriskMarks)
					AND OrderType = 'ED-DrugWithAnAsteriskMark');
SET @IndexRow = 1;

WHILE @IndexRow <= @NumberRow
BEGIN
	UPDATE Orders SET FormId =(SELECT (SELECT Id FROM EDConsultationDrugWithAnAsteriskMarks WHERE VisitId = A.VisitId)[FORMID] 	
										FROM 
										(SELECT STT= ROW_NUMBER() OVER(ORDER BY Id), * from Orders
										where VisitId IN (SELECT VisitId FROM EDConsultationDrugWithAnAsteriskMarks)
										AND OrderType = 'ED-DrugWithAnAsteriskMark') AS A
										WHERE A.STT = @IndexRow)
				  WHERE Id = (SELECT Id 	
										FROM 
										(SELECT STT= ROW_NUMBER() OVER(ORDER BY Id), * from Orders
										where VisitId IN (SELECT VisitId FROM EDConsultationDrugWithAnAsteriskMarks)
										AND OrderType = 'ED-DrugWithAnAsteriskMark') AS A
										WHERE A.STT = @IndexRow) ;
	SET @IndexRow = @IndexRow + 1;
END
GO
