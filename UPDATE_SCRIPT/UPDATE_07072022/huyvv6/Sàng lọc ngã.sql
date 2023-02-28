Update	FormDatas
SET FormCode = 'A02_007_220321_VE'
where 	 FormCode = 'OPDFRSFOP'	 and VisitType = 'EOC'

Update OPDFallRiskScreenings
SET OPDFallRiskScreenings.VisitId = 
(SELECT opds.id FROM opds  WHERE opds.OPDFallRiskScreeningId = OPDFallRiskScreenings.Id)
 
 
 Update  OPDFallRiskScreenings
 Set Version = 1
