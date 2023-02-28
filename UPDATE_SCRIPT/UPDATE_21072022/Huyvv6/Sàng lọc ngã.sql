Update	FormDatas
SET FormCode = 'A02_007_220321_VE'
where 	 FormCode = 'OPDFRSFOP'	 and VisitType = 'EOC'

Update OPDFallRiskScreenings
SET OPDFallRiskScreenings.VisitId = 
(SELECT opds.id FROM opds  WHERE opds.OPDFallRiskScreeningId = OPDFallRiskScreenings.Id)
 
 
 Update  OPDFallRiskScreenings
 Set Version = 1


Update Forms
SET Code = 'A02_007_220321_VE'
Where Code = 'OPDFRSFOP'

Update UnlockFormToUpdates
SET FormCode = 'A02_007_220321_VE'
Where FormCode = 'OPDFRSFOP'

Update Forms
SET Code = 'A02_007_220321_VE'
Where Code = 'OPDFRSFOP'