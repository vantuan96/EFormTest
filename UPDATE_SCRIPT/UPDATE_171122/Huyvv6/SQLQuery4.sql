

Update MasterDatas
SET Data = 'EOC' ,DefaultValue = 'TRANSLATEREASON'
Where Code = 'TRANSLATEREASONFORVISITANS'
-- ket qua can lam sang
-- ly do vao vien
Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATEREASON'
Where Code = 'TRANSLATEREASONANS'

-- ly do nhap vien 
Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATEREASON'
Where Code = 'TRANSLATECHIEFCOMANS'
-- ket qua can lam sang

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATESUBRESULT'
Where Code = 'TRANSLATESUBRESULTANS'
-- ket qua xet nhiem, can lam sang
Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATESUBRESULT'
Where Code = 'TRANSLATELABANDSUBRESULTANS'


---
Update MasterDatas
SET Data = 'EOC' ,DefaultValue = 'TRANSLATESUBRESULT'
Where Code = 'TRANSLATEPRINCIPALTESTANS'
------
--phuong phap dieu tri
Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATETREATMENTANDPROCEDURE'
Where Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATETREATMENTANDPROCEDURE'
Where Code = 'TRANSLATETREATMENTANS'


----
--Tinh trang nguoi benh hien tai
Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATCURRENTPATIENT'
Where Code = 'TRANSLATCURRENTPATIENTANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATCURRENTPATIENT'
Where Code = 'TRANSLATESATATUSREFERALANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATCURRENTPATIENT'
Where Code = 'TRANSLATECONDITIONDISCHARGEANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATCURRENTPATIENT'
Where Code = 'TRANSLATESTATUSPATIENTTRANSFERANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATCURRENTPATIENT'
Where Code = 'TRANSLATESTATUSINJURYDISCHARGEANS'
--------------

Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATECAREPLAN'
Where Code = 'TRANSLATECAREPLANANS'

Update MasterDatas
SET Data = 'EOC' ,DefaultValue = 'TRANSLATECAREPLAN'
Where Code = 'TRANSLATERECOMMENDATIONANDFOLLOWUPANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATECAREPLAN'
Where Code = 'TRANSLATEFOLLOWUPCAREPLANANS'


Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATECAREPLAN'
Where Code = 'TRANSLATETREANTMENTPLANANS'

Update MasterDatas
SET Data = 'EOC' ,DefaultValue = 'TRANSLATECAREPLAN'
Where Code = 'TRANSLATETREATMENTANDPROCEDUREANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATECAREPLAN'
Where Code = 'TRANSLATENOTEANS'

---

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATECAREPLAN'
Where Code = 'TRANSLATEFOLLOWUPCAREPLAN'

Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATEPDIAGNOSIS'
Where Code = 'TRANSLATEPDIAGNOSISANS'


--------
----Quá trình bệnh lý và diễn biến lâm sàng
Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATECLINEVOLU'
Where Code = 'TRANSLATECLINEVOLUANS'


-----
Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATEGEN'
Where Code = 'TRANSLATEGENANS'

Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATEGEN'
Where Code = 'TRANSLATEGENVI'
-------------
-----
Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATEADD'
Where Code = 'TRANSLATEADDANS'

Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATEADD'
Where Code = 'TRANSLATEADDFOOTERANS'
-------------
Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATEDRUGUSED'
Where Code = 'TRANSLATEDRUGUSEDANS'
----

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATCURRENTPATIENT'
Where Code = 'TRANSLATECONDITIONDISCHARGEANS'

Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATCURRENTPATIENT	'
Where Code = 'TRANSLATESTATUSPATIENTTRANSFERANS'

Update MasterDatas
SET Data = 'IPD,EOC' ,DefaultValue = 'TRANSLATCURRENTPATIENT	'
Where Code = 'TRANSLATESATATUSREFERALANS'

Update MasterDatas
SET Data = 'IPD' ,DefaultValue = 'TRANSLATEJOB'
Where Code = 'TRANSLATEJOBANS'


Update MasterDatas
SET Data = 'EOC' ,DefaultValue = 'TRANSLATEHISTORY'
Where Code = 'TRANSLATEHISTORYOFPRESENTANS'
Update MasterDatas
SET Data = 'EOC' ,DefaultValue = 'TRANSLATEHISTORY'
Where Code = 'TRANSLATEHISTORYANS'

Update MasterDatas
SET Data = 'EOC' ,DefaultValue = 'TRANSLATEKLS'
Where Code = 'TRANSLATEKLSANS'