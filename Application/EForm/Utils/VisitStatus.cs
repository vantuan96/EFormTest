using DataAccess.Models;
using DataAccess.Repository;
using EForm.Common;
using System.Linq;

namespace EForm.Utils
{
    public class VisitStatus
    {
        private IUnitOfWork unitOfWork;
        private string VisitTypeGroup;

        public VisitStatus(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public VisitStatus(IUnitOfWork unitOfWork, string visit_type_group)
        {
            this.unitOfWork = unitOfWork;
            this.VisitTypeGroup = visit_type_group;
        }

        public EDStatus GetNoExaminationStatus()
        {
            if (string.IsNullOrEmpty(this.VisitTypeGroup))
                return null;

            return unitOfWork.EDStatusRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.Code) &&
                Constant.NoExamination.Contains(e.Code) &&
                e.VisitTypeGroupId != null &&
                e.VisitTypeGroup.Code == this.VisitTypeGroup
            );
        }
    }
}