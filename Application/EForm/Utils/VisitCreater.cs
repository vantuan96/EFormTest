using DataAccess.Models.EDModel;
using DataAccess.Models.EOCModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using DataAccess.Repository;
using EForm.Models;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace EForm.Utils
{
    public class VisitCreater
    {
        private IUnitOfWork unitOfWork;
        private Guid? in_hospital_status_id;
        private Guid? customer_id;
        private Guid? transfer_id;
        private Guid? group_id;
        private string visit_code;
        private Guid? site_id;
        private Guid? specialty_id;
        private Guid? user_id;
        private string visit_type;
        private bool isAnesthesia;
        private int version = 1;
        public VisitCreater(
            IUnitOfWork unitOfWork,
            Guid? in_hospital_status_id,
            Guid? customer_id,
            Guid? transfer_id,
            Guid? group_id,
            string visit_code,
            Guid? site_id,
            Guid? specialty_id,
            Guid? user_id,
            bool IsAnesthesia = false)
        {
            this.unitOfWork = unitOfWork;
            this.in_hospital_status_id = in_hospital_status_id;
            this.customer_id = customer_id;
            this.transfer_id = transfer_id;
            this.group_id = group_id;
            this.visit_code = visit_code;
            this.site_id = site_id;
            this.specialty_id = specialty_id;
            this.user_id = user_id;
            this.isAnesthesia = IsAnesthesia;
            UpdateVersionVisit();
        }

        private void UpdateVersionVisit()
        {
            int? version_system = this.unitOfWork.AppVersionRepository.AsQueryable().OrderByDescending(e => e.Order).Select(e => e.Version).FirstOrDefault();
            if (version_system == null)
                return;
            this.version = (int)version_system;
        }

        public void SetCustomerId(Guid customer_id)
        {
            this.customer_id = customer_id;
        }

        public ED CreateNewED(CustomerEDParameterModel request = null)
        {
            EDEmergencyTriageRecord etr = new EDEmergencyTriageRecord();
            etr.Version = 2;
            etr.TriageDateTime = DateTime.Now;
            unitOfWork.EmergencyTriageRecordRepository.Add(etr);
            EDEmergencyRecord er = new EDEmergencyRecord();
            unitOfWork.EmergencyRecordRepository.Add(er);
            EDObservationChart oc = new EDObservationChart();
            unitOfWork.EDObservationChartRepository.Add(oc);
            EDPatientProgressNote ppn = new EDPatientProgressNote();
            unitOfWork.PatientProgressNoteRepository.Add(ppn);
            EDDischargeInformation di = new EDDischargeInformation();
            unitOfWork.DischargeInformationRepository.Add(di);

            ED ed = new ED();
            ed.SiteId = this.site_id;
            ed.SpecialtyId = this.specialty_id;
            ed.CustomerId = this.customer_id;
            if (this.transfer_id != null)
            {
                ed.IsTransfer = true;
                ed.TransferFromId = this.transfer_id;
            }
            ed.EmergencyTriageRecordId = etr.Id;
            ed.AdmittedDate = etr.TriageDateTime;
            ed.EmergencyRecordId = er.Id;
            ed.ObservationChartId = oc.Id;
            ed.PatientProgressNoteId = ppn.Id;
            ed.EDStatusId = this.in_hospital_status_id;
            ed.DischargeInformationId = di.Id;
            if (request != null) { 
                ed.HealthInsuranceNumber = request.HealthInsuranceNumber;
                ed.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
                ed.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;
            }
            ed.PrimaryNurseId = this.user_id; // thêm điều dưỡng chính theo yêu cầu ai tiếp nhận thì là điều dưỡng chính
            ed.Version = this.version;
            unitOfWork.EDRepository.Add(ed);
            unitOfWork.Commit();

            GenerateRecordCode(ed);
            return ed;
        }

        
        public OPD CreateNewOPD(CustomerEDParameterModel request = null)
        {
            var now = DateTime.Now;
            OPDInitialAssessmentForOnGoing iafog = new OPDInitialAssessmentForOnGoing();
            iafog.AdmittedDate = now;
            iafog.Version = this.version;
            unitOfWork.OPDInitialAssessmentForOnGoingRepository.Add(iafog);
            OPDInitialAssessmentForShortTerm iafst = new OPDInitialAssessmentForShortTerm();
            iafst.Version = this.version >= 7 ? this.version : 2;
            iafst.AdmittedDate = now;
            unitOfWork.OPDInitialAssessmentForShortTermRepository.Add(iafst);
            OPDFallRiskScreening frs = new OPDFallRiskScreening();
            unitOfWork.OPDFallRiskScreeningRepository.Add(frs);
            OPDOutpatientExaminationNote oen = new OPDOutpatientExaminationNote();
            oen.IsConsultation = null;
            unitOfWork.OPDOutpatientExaminationNoteRepository.Add(oen);
            OPDPatientProgressNote ppn = new OPDPatientProgressNote();
            unitOfWork.OPDPatientProgressNoteRepository.Add(ppn);
            OPDObservationChart oc = new OPDObservationChart();
            unitOfWork.OPDObservationChartRepository.Add(oc);

            OPD opd = new OPD();
            opd.PrimaryNurseId = this.user_id;
            opd.VisitCode = this.visit_code;
            opd.SiteId = this.site_id;
            opd.SpecialtyId = this.specialty_id;
            opd.CustomerId = this.customer_id;
            if (this.transfer_id != null)
            {
                opd.IsTransfer = true;
                opd.TransferFromId = this.transfer_id;
            }
            if (this.group_id != null)
            {
                opd.GroupId = this.group_id;
            }
            opd.EDStatusId = this.in_hospital_status_id;
            opd.AdmittedDate = now;
            opd.OPDInitialAssessmentForOnGoingId = iafog.Id;
            opd.OPDInitialAssessmentForShortTermId = iafst.Id;
            //opd.OPDFallRiskScreeningId = frs.Id;
            opd.OPDOutpatientExaminationNoteId = oen.Id;
            opd.OPDPatientProgressNoteId = ppn.Id;
            opd.OPDObservationChartId = oc.Id;

            if (request != null)
            {
                opd.HealthInsuranceNumber = request.HealthInsuranceNumber;
                opd.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
                opd.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;
                opd.VisitType = request.VisitType;
            }

            opd.IsAnesthesia = this.isAnesthesia;
            opd.Version = this.version;
            unitOfWork.OPDRepository.Add(opd);
            unitOfWork.Commit();

            GenerateRecordCode(opd);
            return opd;
        }
        public IPD CreateNewIPD()
        {
            IPDPatientProgressNote ppn = new IPDPatientProgressNote();
            unitOfWork.IPDPatientProgressNoteRepository.Add(ppn);

            IPD ipd = new IPD();
            ipd.VisitCode = this.visit_code;
            ipd.SiteId = this.site_id;
            ipd.SpecialtyId = this.specialty_id;
            ipd.CustomerId = this.customer_id;
            if (this.transfer_id != null)
            {
                ipd.IsTransfer = true;
                ipd.TransferFromId = this.transfer_id;
            }
            ipd.AdmittedDate = DateTime.Now;
            ipd.EDStatusId = this.in_hospital_status_id;
            ipd.IPDPatientProgressNoteId = ppn.Id;
            ipd.PermissionForVisitor = true;
            ipd.Version = this.version;
            unitOfWork.IPDRepository.Add(ipd);
            unitOfWork.Commit();

            GenerateRecordCode(ipd);
            return ipd;
        }
        public IPD CreateDraftIPD()
        {
            IPDPatientProgressNote ppn = new IPDPatientProgressNote();
            unitOfWork.IPDPatientProgressNoteRepository.Add(ppn);

            IPD ipd = new IPD();
            ipd.VisitCode = this.visit_code;
            ipd.IsDraft = true;
            ipd.SiteId = this.site_id;
            ipd.SpecialtyId = this.specialty_id;
            ipd.CustomerId = this.customer_id;
            ipd.AdmittedDate = DateTime.Now;
            ipd.EDStatusId = this.in_hospital_status_id;
            ipd.IPDPatientProgressNoteId = ppn.Id;
            ipd.PermissionForVisitor = true;
            ipd.Version = this.version;
            unitOfWork.IPDRepository.Add(ipd);
            unitOfWork.Commit();

            GenerateRecordCode(ipd);
            return ipd;
        }
        public EOC CreateNewEOC(CustomerEDParameterModel request = null, string from_visit_type = null)
        {
            EOC eoc = new EOC();
            eoc.SiteId = this.site_id;
            eoc.SpecialtyId = this.specialty_id;
            eoc.CustomerId = this.customer_id;
            eoc.VisitCode = this.visit_code;
            eoc.AdmittedDate = DateTime.Now;
            eoc.StatusId = this.in_hospital_status_id;
            if (this.transfer_id != null)
            {
                eoc.IsTransfer = true;
                eoc.TransferFromId = this.transfer_id;
                eoc.TransferFromType = from_visit_type;
            }

            OPDPatientProgressNote ppn = new OPDPatientProgressNote();
            unitOfWork.OPDPatientProgressNoteRepository.Add(ppn);
            OPDObservationChart oc = new OPDObservationChart();
            unitOfWork.OPDObservationChartRepository.Add(oc);

            eoc.OPDPatientProgressNoteId = ppn.Id;
            eoc.OPDObservationChartId = oc.Id;

            if (request != null)
            {
                eoc.HealthInsuranceNumber = request.HealthInsuranceNumber;
                eoc.StartHealthInsuranceDate = request.ConvertedStartHealthInsuranceDate;
                eoc.ExpireHealthInsuranceDate = request.ConvertedExpireHealthInsuranceDate;
            }
            eoc.Version = this.version;
            unitOfWork.EOCRepository.Add(eoc);
            unitOfWork.Commit();

            GenerateRecordCode(eoc);
            return eoc;
        }
        private void GenerateRecordCode(dynamic visit)
        {
            var visit_type_name = ObjectContext.GetObjectType(visit.GetType()).Name;

            var site_code = unitOfWork.SiteRepository.GetById(visit.SiteId)?.ApiCode;
            if (!string.IsNullOrEmpty(site_code))
                site_code = site_code.Substring(1, 2);

            var pid = unitOfWork.CustomerRepository.GetById(visit.CustomerId)?.PID;

            var created_at = visit.AdmittedDate?.ToString("HHmmssfffddMMyy");

            var specialty_no = unitOfWork.SpecialtyRepository.GetById(visit.SpecialtyId)?.SpecialtyNo;
            string no = specialty_no?.ToString("D3");

            visit.RecordCode = string.Format("{0}.{1}.{2}{3}-{4}", site_code, visit_type_name, pid, created_at, no);
            if (visit_type_name == "ED") unitOfWork.EDRepository.Update(visit);
            else if (visit_type_name == "OPD") unitOfWork.OPDRepository.Update(visit);
            else if (visit_type_name == "EOC") unitOfWork.EOCRepository.Update(visit);
            unitOfWork.Commit();
        }
    }
}