using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EForm.Utils
{
    public class TranslationUtil
    {
        private IUnitOfWork unitOfWork;
        private Guid? VisitId;
        private string VisitTypeGroupCode;
        private string EnName;

        public TranslationUtil(IUnitOfWork unitOfWork, Guid? visit_id, string visit_type_group_code, string en_name)
        {
            this.unitOfWork = unitOfWork;
            this.VisitId = visit_id;
            this.VisitTypeGroupCode = visit_type_group_code;
            this.EnName = en_name;
        }

        public dynamic GetList(Guid? formId = null)
        {
            if (formId == null || formId == Guid.Empty)
            {
                return unitOfWork.TranslationRepository.Find(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == this.VisitId &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode.Equals(this.VisitTypeGroupCode) &&
                !string.IsNullOrEmpty(e.EnName) &&
                e.EnName.Equals(this.EnName)
            ).Select(e => new { e.Id, e.ToLanguage, e.FromLanguage, e.Status }).ToList();
            }
            else
            {
                return unitOfWork.TranslationRepository.Find(
               e => !e.IsDeleted &&
               e.VisitId != null &&
               e.VisitId == this.VisitId &&
               !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
               e.VisitTypeGroupCode.Equals(this.VisitTypeGroupCode) &&
               !string.IsNullOrEmpty(e.EnName) &&
               e.EnName.Equals(this.EnName) && e.FormId == formId)
                .Select(e => new { e.Id, e.ToLanguage, e.FromLanguage, e.Status }).ToList();
            }

        }
    }
}