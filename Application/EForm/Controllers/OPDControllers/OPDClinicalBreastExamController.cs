using DataAccess.Models.OPDModel;
using EForm.BaseControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    public class OPDClinicalBreastExamController : BaseApiController
    {
        protected OPDClinicalBreastExamNote CreateClinicalBreastExamNote (Guid visit_id, string visit_type)
        {
            var procedure = new OPDClinicalBreastExamNote()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type
            };
            unitOfWork.OPDClinicalBreastExamNoteRepository.Add(procedure);
            unitOfWork.Commit();
            return procedure;
        }
        protected List<OPDClinicalBreastExamNote> GetListClinicalBreastExamNote (Guid visit_id, string visit_type)
        {
            return unitOfWork.OPDClinicalBreastExamNoteRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId == visit_id &&
                e.VisitTypeGroupCode == visit_type
            ).OrderBy(e => e.CreatedAt).ToList();
        }
        protected OPDClinicalBreastExamNote GetClinicalBreastExamNote(Guid id)
        {
            return unitOfWork.OPDClinicalBreastExamNoteRepository.GetById(id);
        }
    }
}
