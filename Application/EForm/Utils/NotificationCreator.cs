using DataAccess.Models;
using DataAccess.Repository;
using System;

namespace EForm.Utils
{
    public class NotificationCreator
    {
        private IUnitOfWork unitOfWork;
        private string from_user;
        private string to_user;
        private int priority;
        private string vi_message;
        private string en_message;
        private Guid? spec_id;
        private Guid? visit_id;
        private string group_code;
        private string form_frontend;

        public NotificationCreator(
            IUnitOfWork unitOfWork,
            string from_user,
            string to_user,
            int priority,
            string vi_message,
            string en_message,
            Guid? spec_id,
            Guid? visit_id,
            string group_code,
            string form_frontend) 
        {
            this.unitOfWork = unitOfWork;
            this.from_user = from_user;
            this.to_user = to_user;
            this.priority = priority;
            this.vi_message = vi_message;
            this.en_message = en_message;
            this.spec_id = spec_id;
            this.visit_id = visit_id;
            this.group_code = group_code;
            this.form_frontend = form_frontend;
        }

        public void Create()
        {
            var old_noti = unitOfWork.NotificationRepository.FirstOrDefault(
                e => !e.IsDeleted &&
                !string.IsNullOrEmpty(e.ViMessage) &&
                e.ViMessage.Equals(vi_message) &&
                !string.IsNullOrEmpty(e.ToUser) &&
                e.ToUser == to_user &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !e.Seen
            );
            if (old_noti != null)
            {
                old_noti.CreatedAt = DateTime.Now;
                unitOfWork.NotificationRepository.Update(old_noti);
            }
            else
            {
                Notification noti = new Notification()
                {
                    FromUser = from_user,
                    ToUser = to_user,
                    Priority = priority,
                    ViMessage = vi_message,
                    EnMessage = en_message,
                    SpecialtyId = spec_id,
                    VisitId = visit_id,
                    VisitTypeGroupCode = group_code,
                    Form = form_frontend,
                };
                unitOfWork.NotificationRepository.Add(noti);
            }


            unitOfWork.Commit();
        }
    }
}