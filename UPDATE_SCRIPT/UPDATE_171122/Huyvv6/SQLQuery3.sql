
Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic = 'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLU'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_VI,IPD_MEDICAL REPORT_VI,IPD_REFERRAL LETTER_VI,ED_DISCHARGE MEDICAL REPORT_VI,ED_REFERRAL LETTER_VI'
where Form = 'TRANSLATEVI' and Code = 'TRANSLATECLINEVOLUANS'

Update MasterDatas
set Clinic =  'IPD_DISCHARGE MEDICAL REPORT_EN,IPD_MEDICAL REPORT_EN,IPD_REFERRAL LETTER_EN,ED_DISCHARGE MEDICAL REPORT_EN,ED_REFERRAL LETTER_EN'
where Form = 'TRANSLATEEN' and Code = 'TRANSLATECLINEVOLUANS'



Update MasterDatas
SET ViName = 'Address',EnName = 'Address'
Where Code = 'TRANSLATEADDFOOTER' and Form = 'TRANSLATEEN'


Update MasterDatas
SET ViName = 'Co morbidities',EnName = 'Co morbidities'
Where Code = 'TRANSLATECOMORBIDITIES' and Form = 'TRANSLATEEN'

Update MasterDatas
SET ViName = 'Condition at discharge',EnName = 'Condition at discharge'
Where Code = 'TRANSLATECONDITIONDISCHARGE' and Form = 'TRANSLATEEN'

Update MasterDatas
SET ViName = 'Lab test and paraclinical test result',EnName = 'Lab test and paraclinical test result'
Where Code = 'TRANSLATELABANDSUBRESULT' and Form = 'TRANSLATEEN'


Update MasterDatas
SET ViName = 'Method, procedure, technique, medication applied during treatment',EnName = 'Method, procedure, technique, medication applied during treatment'
Where Code = 'TRANSLATEMPTDRUGUSETREATMENT' and Form = 'TRANSLATEEN'

Update MasterDatas
SET ViName = 'Patient’s condition upon transfer',EnName = 'Patient’s condition upon transfer'
Where Code = 'TRANSLATESATATUSREFERAL' and Form = 'TRANSLATEEN'


Update MasterDatas
SET ViName = 'Name, title, professional qualification of escort',EnName = 'Name, title, professional qualification of escort'
Where Code = 'TRANSLATEPERTRANSPORT' and Form = 'TRANSLATEEN'


Update MasterDatas
SET ViName = 'Treatment',EnName = 'Treatment'
Where Code = 'TRANSLATETREATMENT' and Form = 'TRANSLATEEN'


Update MasterDatas
SET ViName = 'Injury condition upon admission',EnName = 'Injury condition upon admission'
Where Code = 'TRANSLATESTATUSADMITTED' and Form = 'TRANSLATEEN'

Update MasterDatas
SET ViName = 'Injury condition upon discharge',EnName = 'Injury condition upon discharge'
Where Code = 'TRANSLATESTATUSINJURYDISCHARGE' and Form = 'TRANSLATEEN'

Update MasterDatas
SET ViName = 'Gender',EnName = 'Gender'
Where Code = 'TRANSLATEGENVI' and Form = 'TRANSLATEEN'

Update MasterDatas
SET ViName = 'Giới tính',EnName = 'Giới tính'
Where Code = 'TRANSLATEGENVI' and Form = 'TRANSLATEVI'