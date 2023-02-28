using DataAccess.Repository;
using EForm.Common;
using EForm.Models;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Utils
{
    public class ClosestVisitIn24H
    {
        private IUnitOfWork _unitOfWork;
        private VisitIdModel Visit;
        private string NewestEDForm;
        private string NewestOPDForm;
        private string NewestIPDForm;

        public ClosestVisitIn24H(Guid? customer_id, DateTime admitted_date, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.Visit = GetClosestVisitIn24H(customer_id, admitted_date);
        }

        private VisitIdModel GetClosestVisitIn24H(Guid? customer_id, DateTime admitted_date)
        {
            var visit = new List<VisitIdModel>();
            var time = DateTime.Now.AddDays(-1);

            List<VisitIdModel> ed = _unitOfWork.EDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate >= time &&
                e.AdmittedDate < admitted_date
            ).OrderByDescending(e => e.AdmittedDate)
            .Select(e => new VisitIdModel
            {
                Object = e,
                AdmittedDate = e.AdmittedDate,
                VisitType = "ED"
            })
            .ToList();
            if (ed.Count > 0)
                visit.Add(ed[0]);

            List<VisitIdModel> opd = _unitOfWork.OPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate >= time &&
                e.AdmittedDate < admitted_date
            ).OrderByDescending(e => e.AdmittedDate)
            .Select(e => new VisitIdModel
            {
                Object = e,
                AdmittedDate = e.AdmittedDate,
                VisitType = "OPD"
            })
            .ToList();
            if (opd.Count > 0)
                visit.Add(opd[0]);

            List<VisitIdModel> ipd = _unitOfWork.IPDRepository.Find(
                e => !e.IsDeleted &&
                e.CustomerId != null &&
                e.CustomerId == customer_id &&
                e.AdmittedDate != null &&
                e.AdmittedDate >= time &&
                e.AdmittedDate < admitted_date
            ).OrderByDescending(e => e.AdmittedDate)
            .Select(e => new VisitIdModel
            {
                Object = e,
                AdmittedDate = e.AdmittedDate,
                VisitType = "IPD"
            })
            .ToList();
            if (ipd.Count > 0)
                visit.Add(ipd[0]);

            if (visit.Count > 0)
                return visit.OrderByDescending(e => e.AdmittedDate).ToList()[0];

            return null;
        }

        private string GetNewestEDForm()
        {
            if (string.IsNullOrEmpty(this.NewestEDForm)) {
                var etr = this.Visit.Object.EmergencyTriageRecord;
                var retail = this.Visit.Object.EDAssessmentForRetailServicePatient;
                if (retail != null && retail.UpdatedAt > etr.UpdatedAt)
                {
                    this.NewestEDForm = "Retail";
                    return this.NewestEDForm;
                }
                this.NewestEDForm = "EmergencyTriageRecord";
                return this.NewestEDForm;
            }
            return this.NewestEDForm;
        }
        private string GetNewestOPDForm()
        {
            if (string.IsNullOrEmpty(this.NewestOPDForm))
            {
                var ia = this.Visit.Object.OPDInitialAssessmentForShortTerm;
                var telehealth = this.Visit.Object.OPDInitialAssessmentForTelehealth;
                var retail = this.Visit.Object.EIOAssessmentForRetailServicePatient;

                if (telehealth != null && retail != null)
                {
                    if (ia.UpdatedAt >= telehealth.UpdatedAt)
                    {
                        if (ia.UpdatedAt >= retail.UpdatedAt)
                        {
                            this.NewestOPDForm = "InitialAssessment";
                            return this.NewestOPDForm;
                        }
                        this.NewestOPDForm = "Retail";
                        return this.NewestOPDForm;
                    }
                    if (telehealth.UpdatedAt >= retail.UpdatedAt)
                    {
                        this.NewestOPDForm = "Telehealth";
                        return this.NewestOPDForm;
                    }
                    this.NewestOPDForm = "Retail";
                    return this.NewestOPDForm;
                }

                if (telehealth != null && telehealth.UpdatedAt >= ia.UpdatedAt)
                {
                    this.NewestOPDForm = "Telehealth";
                    return this.NewestOPDForm;
                }
                if (retail != null && retail.UpdatedAt >= ia.UpdatedAt)
                {
                    this.NewestOPDForm = "Retail";
                    return this.NewestOPDForm;
                }
                this.NewestOPDForm = "InitialAssessment";
                return this.NewestOPDForm;
            }
            return this.NewestOPDForm;
        }
        private string GetNewestIPDForm()
        {
            if (string.IsNullOrEmpty(this.NewestIPDForm))
                this.NewestIPDForm = "InitialAssessment";
            return this.NewestIPDForm;
        }

        public dynamic GetVitalSign()
        {
            if (this.Visit == null)
                return null;

            if (this.Visit.VisitType == "ED")
                return GetEDVitalSign();

            if (this.Visit.VisitType == "OPD")
                return GetOPDVitalSign();

            if (this.Visit.VisitType == "IPD")
                return GetIPDVitalSign();

            return null;
        }

        private dynamic GetEDVitalSign()
        {
            var form = GetNewestEDForm();
            if(form == "Retail")
                return GetEDVitalSignInRetail();
            return GetEDVitalSignInInitialAssessment();
        }
        private dynamic GetEDVitalSignInInitialAssessment()
        {
            Guid? vital_sign_id = this.Visit.Object.ObservationChartId;
            var vital_sign = _unitOfWork.EDObservationChartDataRepository.Find(
                e => !e.IsDeleted &&
                e.ObservationChartId != null &&
                e.ObservationChartId == vital_sign_id
            ).OrderByDescending(e => e.NoteAt).ToList();
            if (vital_sign.Count < 1)
                return null;

            var sign = vital_sign[0];

            return new List<dynamic> {
                new { Code = "OPDIAFSTOPPULANS", ViName = "Mạch", EnName = "Pulse", Value = sign.Pulse },
                new { Code = "OPDIAFSTOPBP0ANS", ViName = "Huyết áp", EnName = "BP", Value = $"{sign.DiaBP} / {sign.SysBP}" },
                new { Code = "OPDIAFSTOPTEMANS", ViName = "", EnName = "T", Value = sign.Temperature },
                new { Code = "OPDIAFSTOPRR0ANS", ViName = "RR", EnName = "Nhịp thở", Value = sign.Resp }
            };
        }
        private dynamic GetEDVitalSignInRetail()
        {
            string[] list_code = new string[] {
                "EDAFRSPPULANS", "EDAFRSPBP0ANS", "EDAFRSPTEMANS", "EDAFRSPRR0ANS"
            };
            Guid? vital_sign_id = this.Visit.Object.EDAssessmentForRetailServicePatientId;
            return from data_sql in _unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.AsQueryable()
                   .Where(
                        e => !e.IsDeleted &&
                        e.EDAssessmentForRetailServicePatientId != null &&
                        e.EDAssessmentForRetailServicePatientId == vital_sign_id &&
                        list_code.Contains(e.Code)
                    )
                   join master_sql in _unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on data_sql.Code equals master_sql.Code into mtlist
                   from master_sql in mtlist.DefaultIfEmpty()
                   select new
                   {
                       master_sql.ViName,
                       master_sql.EnName,
                       master_sql.Code,
                       data_sql.Value,
                   };
        }

        private dynamic GetOPDVitalSign()
        {
            var form = GetNewestOPDForm();
            if(form == "Retail")
                return GetOPDVitalSignInRetail();
            if (form == "Telehealth")
                return GetOPDVitalSignInTelehealth();
            return GetOPDVitalSignInInitialAssessment();
        }
        private dynamic GetOPDVitalSignInInitialAssessment()
        {
            string[] list_code = new string[] {
                "OPDIAFSTOPPULANS", "OPDIAFSTOPBP0ANS", "OPDIAFSTOPTEMANS", "OPDIAFSTOPRR0ANS"
            };
            Guid? vital_sign_id = this.Visit.Object.OPDInitialAssessmentForShortTermId;
            return from data_sql in _unitOfWork.OPDInitialAssessmentForShortTermDataRepository.AsQueryable()
                   .Where(
                        e => !e.IsDeleted &&
                        e.OPDInitialAssessmentForShortTermId != null &&
                        e.OPDInitialAssessmentForShortTermId == vital_sign_id &&
                        list_code.Contains(e.Code)
                    )
                   join master_sql in _unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on data_sql.Code equals master_sql.Code into mtlist
                   from master_sql in mtlist.DefaultIfEmpty()
                   select new
                   {
                       master_sql.ViName,
                       master_sql.EnName,
                       master_sql.Code,
                       data_sql.Value,
                   };
        }
        private dynamic GetOPDVitalSignInTelehealth()
        {
            string[] list_code = new string[] {
                "OPDIAFTPPULANS", "OPDIAFTPBP0ANS", "OPDIAFTPTEMANS", "OPDIAFTPRR0ANS"
            };
            Guid? vital_sign_id = this.Visit.Object.OPDInitialAssessmentForTelehealthId;
            return from data_sql in _unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.AsQueryable()
                   .Where(
                        e => !e.IsDeleted &&
                        e.OPDInitialAssessmentForTelehealthId != null &&
                        e.OPDInitialAssessmentForTelehealthId == vital_sign_id &&
                        list_code.Contains(e.Code)
                    )
                   join master_sql in _unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on data_sql.Code equals master_sql.Code into mtlist
                   from master_sql in mtlist.DefaultIfEmpty()
                   select new
                   {
                       master_sql.ViName,
                       master_sql.EnName,
                       master_sql.Code,
                       data_sql.Value,
                   };
        }
        private dynamic GetOPDVitalSignInRetail()
        {
            string[] list_code = new string[] {
                "EDAFRSPPULANS", "EDAFRSPBP0ANS", "EDAFRSPTEMANS", "EDAFRSPRR0ANS"
            };
            Guid? vital_sign_id = this.Visit.Object.EIOAssessmentForRetailServicePatientId;
            return from data_sql in _unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.AsQueryable()
                   .Where(
                        e => !e.IsDeleted &&
                        e.EDAssessmentForRetailServicePatientId != null &&
                        e.EDAssessmentForRetailServicePatientId == vital_sign_id &&
                        list_code.Contains(e.Code)
                    )
                   join master_sql in _unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on data_sql.Code equals master_sql.Code into mtlist
                   from master_sql in mtlist.DefaultIfEmpty()
                   select new
                   {
                       master_sql.ViName,
                       master_sql.EnName,
                       master_sql.Code,
                       data_sql.Value,
                   };
        }

        private dynamic GetIPDVitalSign()
        {
            string[] list_code = new string[] {
                "IPDIAAUPULSANS", "IPDIAAUBLPRANS", "IPDIAAUTEMPANS", "IPDIAAURERAANS"
            };
            Guid? vital_sign_id = this.Visit.Object.IPDInitialAssessmentForAdultId;
            return from data_sql in _unitOfWork.IPDInitialAssessmentForAdultDataRepository.AsQueryable()
                   .Where(
                        e => !e.IsDeleted &&
                        e.IPDInitialAssessmentForAdultId != null &&
                        e.IPDInitialAssessmentForAdultId == vital_sign_id &&
                        list_code.Contains(e.Code)
                    )
                   join master_sql in _unitOfWork.MasterDataRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on data_sql.Code equals master_sql.Code into mtlist
                   from master_sql in mtlist.DefaultIfEmpty()
                   select new
                   {
                       master_sql.ViName,
                       master_sql.EnName,
                       master_sql.Code,
                       data_sql.Value,
                   };
        }


        public dynamic GetDiseasesScreening()
        {
            if (this.Visit == null)
                return null;

            if (this.Visit.VisitType == "ED")
                return GetEDDiseasesScreening();

            if (this.Visit.VisitType == "OPD")
                return GetOPDDiseasesScreening();

            if (this.Visit.VisitType == "IPD")
                return GetIPDDiseasesScreening();

            return null;
        }
        private dynamic GetEDDiseasesScreening()
        {
            var form = GetNewestEDForm();
            if (form == "Retail")
            {
                var retail = this.Visit.Object.EDAssessmentForRetailServicePatient;
                return new
                {
                    retail.Version,
                    Name = "EDAssessmentForRetailServicePatient",
                    Code = "EDAFRSP",
                    Datas = GetEDDiseasesScreeningInRetailService(retail.Version),
                };
            }

            var etr = this.Visit.Object.EmergencyTriageRecord;
            return new
            {
                etr.Version,
                Name = "EmergencyTriageRecord",
                Code = "ETR",
                Datas = GetEDDiseasesScreeningInETR(etr.Version),
            };
        }
        private dynamic GetEDDiseasesScreeningInETR(int version)
        {
            if (version >= 2)
            {
                version = 2;
            }
            string[] list_code = Constant.DISEASES_SCREENING[version]["EDEmergencyTriageRecord"];
            Guid? diseases_screening_id = this.Visit.Object.EmergencyTriageRecordId;
            return _unitOfWork.EmergencyTriageRecordDataRepository.Find(
                        e => !e.IsDeleted &&
                        e.EmergencyTriageRecordId != null &&
                        e.EmergencyTriageRecordId == diseases_screening_id &&
                        list_code.Contains(e.Code)
                    ).Select(e => new { e.Code, e.Value });
        }
        private dynamic GetEDDiseasesScreeningInRetailService(int version)
        {
            if (version >= 2)
            {
                version = 2;
            }
            string[] list_code = Constant.DISEASES_SCREENING[version]["EDRetailService"];
            Guid? diseases_screening_id = this.Visit.Object.EDAssessmentForRetailServicePatientId;
            return _unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.Find(
                        e => !e.IsDeleted &&
                        e.EDAssessmentForRetailServicePatientId != null &&
                        e.EDAssessmentForRetailServicePatientId == diseases_screening_id &&
                        list_code.Contains(e.Code)
                    ).Select(e => new { e.Code, e.Value });
        }

        private dynamic GetOPDDiseasesScreening()
        {
            var form = this.GetNewestOPDForm();

            if (form == "Retail")
            {
                var retail = this.Visit.Object.EIOAssessmentForRetailServicePatient;
                return new
                {
                    retail.Version,
                    Name = "OPDAssessmentForRetailServicePatient",
                    Code = "EDAFRSP",
                    Datas = GetOPDDiseasesScreeningInRetailService(retail.Version),
                };
            }

            if (form== "Telehealth")
            {
                var telehealth = this.Visit.Object.OPDInitialAssessmentForTelehealth;
                return new
                {
                    telehealth.Version,
                    Name = "OPDInitialAssessmentForTelehealth",
                    Code = "OPDIAFTP",
                    Datas = GetOPDDiseasesScreeningInTelehealth(telehealth.Version),
                };
            }

            var ia = this.Visit.Object.OPDInitialAssessmentForShortTerm;
            return new
            {
                ia.Version,
                Name = "OPDInitialAssessmentForShortTerm",
                Code = "OPDIAFSTOP",
                Datas = GetOPDDiseasesScreeningInInitialAssessment(ia.Version),
            };
        }
        private dynamic GetOPDDiseasesScreeningInInitialAssessment(int version)
        {
            if(version >= 2)
            {
                version = 2;
            }
            string[] list_code = Constant.DISEASES_SCREENING[version]["OPDShortTerm"];
            Guid? diseases_screening_id = this.Visit.Object.OPDInitialAssessmentForShortTermId;
            return _unitOfWork.OPDInitialAssessmentForShortTermDataRepository.Find(
                        e => !e.IsDeleted &&
                        e.OPDInitialAssessmentForShortTermId != null &&
                        e.OPDInitialAssessmentForShortTermId == diseases_screening_id &&
                        list_code.Contains(e.Code)
                    ).Select(e => new { e.Code, e.Value });
        }
        private dynamic GetOPDDiseasesScreeningInTelehealth(int version)
        {
            if (version >= 2)
            {
                version = 2;
            }
            string[] list_code = Constant.DISEASES_SCREENING[version]["OPDTelehealth"];
            Guid? diseases_screening_id = this.Visit.Object.OPDInitialAssessmentForTelehealthId;
            return _unitOfWork.OPDInitialAssessmentForTelehealthDataRepository.Find(
                        e => !e.IsDeleted &&
                        e.OPDInitialAssessmentForTelehealthId != null &&
                        e.OPDInitialAssessmentForTelehealthId == diseases_screening_id &&
                        list_code.Contains(e.Code)
                    ).Select(e => new { e.Code, e.Value });
        }
        private dynamic GetOPDDiseasesScreeningInRetailService(int version)
        {
            if (version >= 2)
            {
                version = 2;
            }
            string[] list_code = Constant.DISEASES_SCREENING[version]["EDRetailService"];
            Guid? diseases_screening_id = this.Visit.Object.EIOAssessmentForRetailServicePatientId;
            return _unitOfWork.EIOAssessmentForRetailServicePatientDataRepository.Find(
                        e => !e.IsDeleted &&
                        e.EDAssessmentForRetailServicePatientId != null &&
                        e.EDAssessmentForRetailServicePatientId == diseases_screening_id &&
                        list_code.Contains(e.Code)
                    ).Select(e => new { e.Code, e.Value });
        }

        private dynamic GetIPDDiseasesScreening()
        {
            var ia = this.Visit.Object.IPDInitialAssessmentForAdult;
            if (ia == null)
                return null;
            int version = ia.Version;
            if (version >= 2)
            {
                version = 2;
            }
            string[] list_code = Constant.DISEASES_SCREENING[version]["IPDAdult"];
            Guid? diseases_screening_id = this.Visit.Object.IPDInitialAssessmentForAdultId;
            var datas = _unitOfWork.IPDInitialAssessmentForAdultDataRepository.Find(
                        e => !e.IsDeleted &&
                        e.IPDInitialAssessmentForAdultId != null &&
                        e.IPDInitialAssessmentForAdultId == diseases_screening_id &&
                        list_code.Contains(e.Code)
                    ).Select(e => new { e.Code, e.Value });
            return new
            {
                ia.Version,
                Name = "IPDInitialAssessmentForAdult",
                Code = "IPDIAAU",
                Datas = datas,
            };
        }
    }
}