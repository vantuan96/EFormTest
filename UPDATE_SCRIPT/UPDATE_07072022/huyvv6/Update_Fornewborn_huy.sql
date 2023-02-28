UPDATE IPDInitialAssessmentForNewborns 
SET DateOfAdmission = (SELECT IPDS.AdmittedDate FROM IPDS  WHERE IPDInitialAssessmentForNewborns.VisitId = IPDS.ID)