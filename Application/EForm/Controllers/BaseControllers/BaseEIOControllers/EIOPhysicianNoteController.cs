using DataAccess.Models.EDModel;
using DataAccess.Models.EIOModel;
using DataAccess.Models.IPDModel;
using DataAccess.Models.OPDModel;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOPhysicianNoteController : BaseApiController
    {
        protected dynamic GetListEIOPhysicianNote(dynamic visit, string visit_type, string from, string to, string createdBy, int? sort = null, Guid? formId = null)
        {

            var start = string.IsNullOrEmpty(from) ? (DateTime?)null : DateTime.ParseExact(from, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            var end = string.IsNullOrEmpty(to) ? (DateTime?)null : DateTime.ParseExact(to, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);

            var query = unitOfWork.EIOPhysicianNoteRepository.AsQueryable();
            if (!string.IsNullOrEmpty(createdBy))
                query = query.Where(e => e.CreatedBy == createdBy);
            if (start != null && end != null)
                query = query.Where(e => e.NoteTime != null && e.NoteTime >= start && e.NoteTime <= end);
            else if (start != null)
                query = query.Where(e => e.NoteTime != null && e.NoteTime >= start);
            else if (end != null)
                query = query.Where(e => e.NoteTime != null && e.NoteTime <= end);

            var visit_id = visit_type == "IPD" ? ((IPD)visit).Id : visit_type == "OPD" ? ((OPD)visit).Id : visit_type == "ED" ? ((ED)visit).Id : Guid.NewGuid();
            var isLocked = visit_type == "IPD" ? IPDIsBlock((IPD)visit, Constant.IPDFormCode.PhieuDieuTri) : false;

            query = query.Where(
                e => !e.IsDeleted &&
                e.VisitId != null &&
                e.VisitId == visit_id &&
                !string.IsNullOrEmpty(e.VisitTypeGroupCode) &&
                e.VisitTypeGroupCode == visit_type &&
                (string.IsNullOrEmpty(createdBy) || e.CreatedBy == createdBy)
                && e.FormId == formId
            );

            // lấy thêm full name user tạo form
            var result = from q in query
                         join u in unitOfWork.UserRepository.AsQueryable()
                         .Where(e => !e.IsDeleted)
                         on q.CreatedBy equals u.Username into temp
                         join newborn in unitOfWork.EIOConstraintNewbornAndPregnantWomanRepository.AsQueryable()
                         .Where(e => !e.IsDeleted)
                         on q.FormId equals newborn.Id into newborn_temp
                         from defaults in temp.DefaultIfEmpty()
                         from newborn in newborn_temp.DefaultIfEmpty()
                         select new
                         {
                             Form = q,
                             FullNameUser = defaults.Fullname,
                             FormContraintNewborn = newborn
                         };

            // thêm 2 kiểu sắp xếp 
            if (sort == null || sort == 1)
                result = result.OrderByDescending(e => e.Form.NoteTime);
            else
                result = result.OrderBy(e => e.Form.NoteTime);

            return result.ToList().Select(e => new
            {
                e.Form.Id,
                NoteTime = e.Form.NoteTime?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                e.Form.CreatedBy,
                e.Form.Examination,
                e.Form.Treatment,
                e.Form.VisitId,
                IsLocked = visit_type == "IPD" ? IPDIsBlock((IPD)visit, Constant.IPDFormCode.PhieuDieuTri, e.Form.Id) : false,
                FullName = e.FullNameUser,
                e.Form.CreatedAt,
                e.Form.UpdatedAt,
                e.Form.UpdatedBy,
                NewbornCustomer = new
                {
                    PID = e.FormContraintNewborn?.NewbornCustomer?.PID,
                    Address = e.FormContraintNewborn?.NewbornCustomer?.Address,
                    Gender = e.FormContraintNewborn?.NewbornCustomer?.Gender,
                    Fullname = e.FormContraintNewborn?.NewbornCustomer?.Fullname,
                    Id = e.FormContraintNewborn?.NewbornCustomer?.Id
                },
                ConfirmCreated = GetInfoConfirm(e.Form.Id)
            });
        }

        protected EIOPhysicianNote GetEIOPhysicianNote(Guid note_id)
        {
            return unitOfWork.EIOPhysicianNoteRepository.GetById(note_id);
        }

        protected EIOPhysicianNote CreateEIOPhysicianNote(Guid visit_id, string visit_type, JObject request, Guid? formId = null)
        {
            var pNote = new EIOPhysicianNote()
            {
                VisitId = visit_id,
                VisitTypeGroupCode = visit_type,
                Examination = request["Examination"]?.ToString(),
                Treatment = request["Treatment"]?.ToString()
            };

            var time = request["NoteTime"]?.ToString();
            if (!string.IsNullOrEmpty(time))
                pNote.NoteTime = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                pNote.NoteTime = null;

            pNote.FormId = formId;
            unitOfWork.EIOPhysicianNoteRepository.Add(pNote);
            unitOfWork.Commit();
            return pNote;
        }

        protected EIOPhysicianNote UpdateEIOPhysicianNote(Guid note_id, JObject request, Guid? formId = null)
        {
            var dbItem = unitOfWork.EIOPhysicianNoteRepository.GetById(note_id);
            dbItem.Examination = request["Examination"]?.ToString();
            dbItem.Treatment = request["Treatment"]?.ToString();
            var time = request["NoteTime"]?.ToString();
            if (!string.IsNullOrEmpty(time))
                dbItem.NoteTime = DateTime.ParseExact(time, Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            else
                dbItem.NoteTime = null;

            dbItem.FormId = formId;
            unitOfWork.EIOPhysicianNoteRepository.Update(dbItem); //update thoi gian chinh sua

            unitOfWork.Commit();
            return dbItem;
        }
       
    }
}