using System;
using System.Globalization;
using System.Linq;
using DataAccess.Models;
using DataAccess.Models.EDModel;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Utils;

namespace EForm.Controllers.BaseControllers
{
    public class BaseEDApiController: BaseApiController
    {
        protected void UpdateVisit(ED visit)
        {
            unitOfWork.EDRepository.Update(visit);
            unitOfWork.Commit();
        }
        protected void UpdatePrimaryDoctor(ED visit, Guid? primary_doctor_id = null)
        {
            visit.PrimaryDoctorId = primary_doctor_id;
        }
        protected void CreatedEDChangingNotification(User from_user, string to_user,  ED ed, string form_name, string form_code)
        {
            var spec = ed.Specialty;
            var customer = ed.Customer;
            var vi_mes = string.Format(
                "<b>[ED - {0}] {1}</b> của bệnh nhân <b>{2}</b> đã được chỉnh sửa bởi <b>{3}</b> ({4})",
                spec?.ViName,
                form_name,
                customer?.Fullname,
                from_user.Fullname,
                from_user.Title
            );
            var en_mes = string.Format(
                "<b>[ED - {0}] {1}</b> của bệnh nhân <b>{2}</b> đã được chỉnh sửa bởi <b>{3}</b> ({4})",
                spec?.ViName,
                form_name,
                customer?.Fullname,
                from_user.Fullname,
                from_user.Title
            );

            var noti_creator = new NotificationCreator(
                unitOfWork: unitOfWork,
                from_user: from_user.Username,
                to_user: to_user,
                priority: 1,
                vi_message: vi_mes,
                en_message: en_mes,
                spec_id: spec?.Id,
                visit_id: ed.Id,
                group_code: "ED",
                form_frontend: form_code
            );
            noti_creator.Create();
        }  
    }
}