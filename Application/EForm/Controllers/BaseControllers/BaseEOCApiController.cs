using DataAccess.Models.EOCModel;
using DataAccess.Repository;
using EForm.BaseControllers;
using EForm.Common;
using System;
using System.Collections.Generic;

namespace EForm.Controllers.BaseControllers
{
    public class BaseEOCApiController: BaseApiController
    {
        protected readonly string clinic_code = "Normal";
        protected void SetPrimaryDoctorAndAdmittedDate(EOC visit, dynamic request)
        {
            if (request["PrimaryDoctor"] != null && request["PrimaryDoctor"]["Id"] != null)
                visit.PrimaryDoctorId = request["PrimaryDoctor"]["Id"];
            if (request["AdmittedDate"] != null)
            {
                var admitted_date = request["AdmittedDate"].ToString();
                DateTime request_admitted_date = DateTime.ParseExact(admitted_date, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                visit.AdmittedDate = request_admitted_date;
            }
        }
        protected void UpdateVisit(EOC visit)
        {
            if (IsDoctor()) visit.LastUpdatedAtByDoctor = DateTime.Now;
            unitOfWork.EOCRepository.Update(visit);
        }
        protected List<dynamic> GetListEOCForm (Guid visit_id)
        {
            var forms = new List<dynamic> () {
                unitOfWork.EOCInitialAssessmentForShortTermRepository,
                unitOfWork.EOCInitialAssessmentForOnGoingRepository,
                unitOfWork.EOCFallRiskScreeningRepository,
                unitOfWork.EOCOutpatientExaminationNoteRepository
            };
            foreach (var form in forms)
            {
                var t = form;
            }
            return forms;
        }
        protected void DeleteVisit(EOC visit)
        {

            unitOfWork.EOCRepository.Delete(visit);

            var oen = GetOutpatientExaminationNote(visit.Id);
            if (oen != null) unitOfWork.EOCOutpatientExaminationNoteRepository.Delete(oen);

            var short_term = GetEOCInitialAssessmentForShortTerm(visit.Id);
            if (short_term != null) unitOfWork.EOCInitialAssessmentForShortTermRepository.Delete(short_term);

            unitOfWork.Commit();
        }
        protected EOCOutpatientExaminationNote GetOutpatientExaminationNote(Guid VisitId)
        {
            var form = unitOfWork.EOCOutpatientExaminationNoteRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
        protected EOCInitialAssessmentForShortTerm GetEOCInitialAssessmentForShortTerm(Guid VisitId)
        {
            var form = unitOfWork.EOCInitialAssessmentForShortTermRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == VisitId);
            return form;
        }
    }
}