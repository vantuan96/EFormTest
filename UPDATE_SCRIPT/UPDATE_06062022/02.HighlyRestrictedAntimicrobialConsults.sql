Update HighlyRestrictedAntimicrobialConsults
SET HighlyRestrictedAntimicrobialConsults.VisitId = CASE HighlyRestrictedAntimicrobialConsults.VisitTypeGroupCode
WHEN 'ED' THEN (
SELECT ed.Id FROM EDs ed WHERE ed.HighlyRestrictedAntimicrobialConsultId = HighlyRestrictedAntimicrobialConsults.Id)
WHEN 'IPD' THEN (
SELECT ipd.Id FROM IPDs ipd WHERE ipd.HighlyRestrictedAntimicrobialConsultId = HighlyRestrictedAntimicrobialConsults.Id)
END


update EDHandOverCheckLists set IsUseHandOverCheckList = 1
update OPDHandOverCheckLists set IsUseHandOverCheckList = 1
update EOCHandOverCheckLists set IsUseHandOverCheckList = 1
update IPDHandOverCheckLists set IsUseHandOverCheckList = 1