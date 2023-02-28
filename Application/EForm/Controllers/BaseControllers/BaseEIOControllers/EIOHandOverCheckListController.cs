using EForm.BaseControllers;
using EForm.Common;
using EForm.Utils;
using EForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EForm.Controllers.BaseControllers.BaseEIOControllers
{
    public class EIOHandOverCheckListController : BaseApiController
    {
        protected IQueryable<HandOverCheckListQueryModel> GetEDHandOverVisit(Guid specialty_id)
        {
            return from hocl_sql in unitOfWork.HandOverCheckListRepository.AsQueryable()
                    .Where(
                        e => !e.IsDeleted &&
                        e.HandOverTimePhysician != null &&
                        e.HandOverUnitPhysicianId != null &&
                        e.ReceivingUnitPhysicianId != null &&
                        e.ReceivingUnitPhysicianId == specialty_id
                    )
                   join visit_sql in unitOfWork.EDRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.Id equals visit_sql.HandOverCheckListId
                   join cus_sql in unitOfWork.CustomerRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on visit_sql.CustomerId equals cus_sql.Id
                   join h_unit_sql in unitOfWork.SpecialtyRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverUnitPhysicianId equals h_unit_sql.Id
                   join h_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverNurseId equals h_nurse_sql.Id into hnlist from h_nurse_sql in hnlist.DefaultIfEmpty()
                   join r_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingNurseId equals r_nurse_sql.Id into rnlist from r_nurse_sql in rnlist.DefaultIfEmpty()
                   join h_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverPhysicianId equals h_doctor_sql.Id into hdlist from h_doctor_sql in hdlist.DefaultIfEmpty()
                   join r_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingPhysicianId equals r_doctor_sql.Id into rdlist from r_doctor_sql in rdlist.DefaultIfEmpty()
                   select new HandOverCheckListQueryModel()
                   {
                       Id = hocl_sql.Id,
                       TransferDate = hocl_sql.HandOverTimePhysician,
                       HandOverNurseId = hocl_sql.HandOverNurseId,
                       HandOverNurseUsername = h_nurse_sql.Username,
                       ReceivingNurseId = hocl_sql.ReceivingNurseId,
                       ReceivingNurseUsername = r_nurse_sql.Username,
                       HandOverPhysicianId = hocl_sql.HandOverPhysicianId,
                       HandOverPhysicianUsername = h_doctor_sql.Username,
                       ReceivingPhysicianId = hocl_sql.ReceivingPhysicianId,
                       ReceivingPhysicianUsername = r_doctor_sql.Username,
                       HandOverUnit = h_unit_sql.ViName,
                       IsAcceptNurse = hocl_sql.IsAcceptNurse,
                       IsAcceptPhysician = hocl_sql.IsAcceptPhysician,
                       PID = cus_sql.PID,
                       Fullname = cus_sql.Fullname,
                       Phone = cus_sql.Phone,
                       DateOfBirth = cus_sql.DateOfBirth,
                       VisitCode = visit_sql.VisitCode,
                       VisitTypeGroupCode = "ED",
                       VisitId = visit_sql.Id,
                       IsUseHandOverCheckList = hocl_sql.IsUseHandOverCheckList
                   };
        }
        protected IQueryable<HandOverCheckListQueryModel> GetHandOverVisit(HandOverCheckListParameterModel request)
        {
            var specialty_id = GetSpecialtyId();
            var eoc_visit = GetEOCHandOverVisit(specialty_id);
            var ed_visit = GetEDHandOverVisit(specialty_id);
            var ipd_visit = GetIPDHandOverVisit(specialty_id);
            var opd_visit = GetOPDHandOverVisit(specialty_id);
            var opdPreAnesthesia_visit = GetOPDPreAnesthesiaHandOverVisit(specialty_id);

            var handover_visits = eoc_visit.Concat(ed_visit);
            handover_visits = handover_visits.Concat(ipd_visit);
            handover_visits = handover_visits.Concat(opd_visit);
            handover_visits = handover_visits.Concat(opdPreAnesthesia_visit);

            if (request.Search != null)
                handover_visits = FilterBySearch(handover_visits, request.ConvertedSearch);

            if (request.UserAccept != null)
                handover_visits = FilterByUserAccept(handover_visits, request.ConvertedUserAccept);

            if (request.Status != null)
                handover_visits = FilterByStatus(handover_visits, request.Status);

            if (request.StartDate != null && request.EndDate != null)
                handover_visits = FilterByDate(handover_visits, request.ConvertedStartDate, request.ConvertedEndDate);
            else if (request.StartDate != null)
                handover_visits = FilterByStartDate(handover_visits, request.ConvertedStartDate);
            else if (request.EndDate != null)
                handover_visits = FilterByEndDate(handover_visits, request.ConvertedEndDate);

            return handover_visits;
        }

        protected IQueryable<HandOverCheckListQueryModel> GetOPDHandOverVisit(Guid specialty_id)
        {
            return from hocl_sql in unitOfWork.OPDHandOverCheckListRepository.AsQueryable()
            .Where(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id
            )
                   join visit_sql in unitOfWork.OPDRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.Id equals visit_sql.OPDHandOverCheckListId
                   join cus_sql in unitOfWork.CustomerRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on visit_sql.CustomerId equals cus_sql.Id
                   join h_unit_sql in unitOfWork.SpecialtyRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverUnitPhysicianId equals h_unit_sql.Id
                   join h_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverNurseId equals h_nurse_sql.Id into hnlist
                   from h_nurse_sql in hnlist.DefaultIfEmpty()
                   join r_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingNurseId equals r_nurse_sql.Id into rnlist
                   from r_nurse_sql in rnlist.DefaultIfEmpty()
                   join h_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverPhysicianId equals h_doctor_sql.Id into hdlist
                   from h_doctor_sql in hdlist.DefaultIfEmpty()
                   join r_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingPhysicianId equals r_doctor_sql.Id into rdlist
                   from r_doctor_sql in rdlist.DefaultIfEmpty()
                   select new HandOverCheckListQueryModel()
                   {
                       Id = hocl_sql.Id,
                       TransferDate = hocl_sql.HandOverTimePhysician,
                       HandOverNurseId = hocl_sql.HandOverNurseId,
                       HandOverNurseUsername = h_nurse_sql.Username,
                       ReceivingNurseId = hocl_sql.ReceivingNurseId,
                       ReceivingNurseUsername = r_nurse_sql.Username,
                       HandOverPhysicianId = hocl_sql.HandOverPhysicianId,
                       HandOverPhysicianUsername = h_doctor_sql.Username,
                       ReceivingPhysicianId = hocl_sql.ReceivingPhysicianId,
                       ReceivingPhysicianUsername = r_doctor_sql.Username,
                       HandOverUnit = h_unit_sql.ViName,
                       IsAcceptNurse = hocl_sql.IsAcceptNurse,
                       IsAcceptPhysician = hocl_sql.IsAcceptPhysician,
                       PID = cus_sql.PID,
                       Fullname = cus_sql.Fullname,
                       Phone = cus_sql.Phone,
                       DateOfBirth = cus_sql.DateOfBirth,
                       VisitCode = visit_sql.VisitCode,
                       VisitTypeGroupCode = "OPD",
                       VisitId = visit_sql.Id,
                       IsUseHandOverCheckList = hocl_sql.IsUseHandOverCheckList
                   };
        }
        protected IQueryable<HandOverCheckListQueryModel> GetOPDPreAnesthesiaHandOverVisit(Guid specialty_id)
        {
            return from hocl_sql in unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.AsQueryable()
            .Where(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id
            )
                   join visit_sql in unitOfWork.OPDRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.VisitId equals visit_sql.Id
                   join cus_sql in unitOfWork.CustomerRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on visit_sql.CustomerId equals cus_sql.Id
                   join h_unit_sql in unitOfWork.SpecialtyRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverUnitPhysicianId equals h_unit_sql.Id
                   join h_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverNurseId equals h_nurse_sql.Id into hnlist
                   from h_nurse_sql in hnlist.DefaultIfEmpty()
                   join r_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingNurseId equals r_nurse_sql.Id into rnlist
                   from r_nurse_sql in rnlist.DefaultIfEmpty()
                   join h_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverPhysicianId equals h_doctor_sql.Id into hdlist
                   from h_doctor_sql in hdlist.DefaultIfEmpty()
                   join r_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingPhysicianId equals r_doctor_sql.Id into rdlist
                   from r_doctor_sql in rdlist.DefaultIfEmpty()
                   select new HandOverCheckListQueryModel()
                   {
                       Id = hocl_sql.Id,
                       TransferDate = hocl_sql.HandOverTimePhysician,
                       HandOverNurseId = hocl_sql.HandOverNurseId,
                       HandOverNurseUsername = h_nurse_sql.Username,
                       ReceivingNurseId = hocl_sql.ReceivingNurseId,
                       ReceivingNurseUsername = r_nurse_sql.Username,
                       HandOverPhysicianId = hocl_sql.HandOverPhysicianId,
                       HandOverPhysicianUsername = h_doctor_sql.Username,
                       ReceivingPhysicianId = hocl_sql.ReceivingPhysicianId,
                       ReceivingPhysicianUsername = r_doctor_sql.Username,
                       HandOverUnit = h_unit_sql.ViName,
                       IsAcceptNurse = hocl_sql.IsAcceptNurse,
                       IsAcceptPhysician = hocl_sql.IsAcceptPhysician,
                       PID = cus_sql.PID,
                       Fullname = cus_sql.Fullname,
                       Phone = cus_sql.Phone,
                       DateOfBirth = cus_sql.DateOfBirth,
                       VisitCode = visit_sql.VisitCode,
                       VisitTypeGroupCode = "OPDPreAnesthesia",
                       VisitId = visit_sql.Id,
                       IsUseHandOverCheckList = hocl_sql.IsUseHandOverCheckList
                   };
        }
        protected IQueryable<HandOverCheckListQueryModel> GetEOCHandOverVisit(Guid specialty_id)
        {
            return from hocl_sql in unitOfWork.EOCHandOverCheckListRepository.AsQueryable()
            .Where(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id
            )
                   join visit_sql in unitOfWork.EOCRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.VisitId equals visit_sql.Id
                   join cus_sql in unitOfWork.CustomerRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on visit_sql.CustomerId equals cus_sql.Id
                   join h_unit_sql in unitOfWork.SpecialtyRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverUnitPhysicianId equals h_unit_sql.Id
                   join h_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverNurseId equals h_nurse_sql.Id into hnlist
                   from h_nurse_sql in hnlist.DefaultIfEmpty()
                   join r_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingNurseId equals r_nurse_sql.Id into rnlist
                   from r_nurse_sql in rnlist.DefaultIfEmpty()
                   join h_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverPhysicianId equals h_doctor_sql.Id into hdlist
                   from h_doctor_sql in hdlist.DefaultIfEmpty()
                   join r_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingPhysicianId equals r_doctor_sql.Id into rdlist
                   from r_doctor_sql in rdlist.DefaultIfEmpty()
                   select new HandOverCheckListQueryModel()
                   {
                       Id = hocl_sql.Id,
                       TransferDate = hocl_sql.HandOverTimePhysician,
                       HandOverNurseId = hocl_sql.HandOverNurseId,
                       HandOverNurseUsername = h_nurse_sql.Username,
                       ReceivingNurseId = hocl_sql.ReceivingNurseId,
                       ReceivingNurseUsername = r_nurse_sql.Username,
                       HandOverPhysicianId = hocl_sql.HandOverPhysicianId,
                       HandOverPhysicianUsername = h_doctor_sql.Username,
                       ReceivingPhysicianId = hocl_sql.ReceivingPhysicianId,
                       ReceivingPhysicianUsername = r_doctor_sql.Username,
                       HandOverUnit = h_unit_sql.ViName,
                       IsAcceptNurse = hocl_sql.IsAcceptNurse,
                       IsAcceptPhysician = hocl_sql.IsAcceptPhysician,
                       PID = cus_sql.PID,
                       Fullname = cus_sql.Fullname,
                       Phone = cus_sql.Phone,
                       DateOfBirth = cus_sql.DateOfBirth,
                       VisitCode = visit_sql.VisitCode,
                       VisitTypeGroupCode = "EOC",
                       VisitId = visit_sql.Id,
                       IsUseHandOverCheckList = hocl_sql.IsUseHandOverCheckList
                   };
        }
        protected IQueryable<HandOverCheckListQueryModel> GetIPDHandOverVisit(Guid specialty_id)
        {
            return from hocl_sql in unitOfWork.IPDHandOverCheckListRepository.AsQueryable()
                .Where(
                    e => !e.IsDeleted &&
                    e.HandOverTimePhysician != null &&
                    e.HandOverUnitPhysicianId != null &&
                    e.ReceivingUnitPhysicianId != null &&
                    e.ReceivingUnitPhysicianId == specialty_id
                )
                   join visit_sql in unitOfWork.IPDRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.Id equals visit_sql.HandOverCheckListId
                   join cus_sql in unitOfWork.CustomerRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on visit_sql.CustomerId equals cus_sql.Id
                   join h_unit_sql in unitOfWork.SpecialtyRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverUnitPhysicianId equals h_unit_sql.Id
                   join h_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverNurseId equals h_nurse_sql.Id into hnlist
                   from h_nurse_sql in hnlist.DefaultIfEmpty()
                   join r_nurse_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingNurseId equals r_nurse_sql.Id into rnlist
                   from r_nurse_sql in rnlist.DefaultIfEmpty()
                   join h_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.HandOverPhysicianId equals h_doctor_sql.Id into hdlist
                   from h_doctor_sql in hdlist.DefaultIfEmpty()
                   join r_doctor_sql in unitOfWork.UserRepository.Find(e => !e.IsDeleted).AsQueryable()
                       on hocl_sql.ReceivingPhysicianId equals r_doctor_sql.Id into rdlist
                   from r_doctor_sql in rdlist.DefaultIfEmpty()
                   select new HandOverCheckListQueryModel()
                   {
                       Id = hocl_sql.Id,
                       TransferDate = hocl_sql.HandOverTimePhysician,
                       HandOverNurseId = hocl_sql.HandOverNurseId,
                       HandOverNurseUsername = h_nurse_sql.Username,
                       ReceivingNurseId = hocl_sql.ReceivingNurseId,
                       ReceivingNurseUsername = r_nurse_sql.Username,
                       HandOverPhysicianId = hocl_sql.HandOverPhysicianId,
                       HandOverPhysicianUsername = h_doctor_sql.Username,
                       ReceivingPhysicianId = hocl_sql.ReceivingPhysicianId,
                       ReceivingPhysicianUsername = r_doctor_sql.Username,
                       HandOverUnit = h_unit_sql.ViName,
                       IsAcceptNurse = hocl_sql.IsAcceptNurse,
                       IsAcceptPhysician = hocl_sql.IsAcceptPhysician,
                       PID = cus_sql.PID,
                       Fullname = cus_sql.Fullname,
                       Phone = cus_sql.Phone,
                       DateOfBirth = cus_sql.DateOfBirth,
                       VisitCode = visit_sql.VisitCode,
                       VisitTypeGroupCode = "IPD",
                       VisitId = visit_sql.Id,
                       IsUseHandOverCheckList = hocl_sql.IsUseHandOverCheckList
                   };
        }

        protected IQueryable<HandOverCheckListQueryModel> FilterBySearch(IQueryable<HandOverCheckListQueryModel> query, string search)
        {
            return query.Where(
                e => (e.PID != null && e.PID == search) ||
                (e.Fullname != null && e.Fullname.ToLower().Contains(search)) ||
                (e.Phone != null && e.Phone.Contains(search))
            );
        }
        protected IQueryable<HandOverCheckListQueryModel> FilterByUserAccept(IQueryable<HandOverCheckListQueryModel> query, List<Guid?> user_accept)
        {
            return query.Where(
                e => user_accept.Contains(e.HandOverNurseId) ||
                user_accept.Contains(e.HandOverPhysicianId) ||
                user_accept.Contains(e.ReceivingNurseId) ||
                user_accept.Contains(e.ReceivingPhysicianId)
            );
        }
        protected IQueryable<HandOverCheckListQueryModel> FilterByStatus(IQueryable<HandOverCheckListQueryModel> query, int? status)
        {
            if (status == 0)
                return query.Where(e => !e.IsAcceptNurse);
            else if (status == 1)
                return query.Where(e => e.IsAcceptNurse);
            return query;
        }
        protected IQueryable<HandOverCheckListQueryModel> FilterByStartDate(IQueryable<HandOverCheckListQueryModel> query, DateTime? date)
        {
            return query.Where(
                 e => e.TransferDate != null &&
                 e.TransferDate >= date
             );
        }
        protected IQueryable<HandOverCheckListQueryModel> FilterByEndDate(IQueryable<HandOverCheckListQueryModel> query, DateTime? date)
        {
            return query.Where(
                e => e.TransferDate != null &&
                e.TransferDate <= date
            );
        }
        protected IQueryable<HandOverCheckListQueryModel> FilterByDate(IQueryable<HandOverCheckListQueryModel> query, DateTime? start_date, DateTime? end_date)
        {
            return query.Where(
                e => e.TransferDate != null &&
                e.TransferDate >= start_date &&
                e.TransferDate <= end_date
            );
        }

        protected dynamic DataFormatted(IQueryable<HandOverCheckListQueryModel> query, int page_number, int page_size)
        {
            query = query.OrderByDescending(m => m.TransferDate);
            int count = query.Count();
            var items = query.Skip((page_number - 1) * page_size)
                .Take(page_size).ToList()
                .Select(e => new HandOverCheckListModel
                {
                    Id = e.Id,
                    PID = e.PID,
                    Phone = e.Phone,
                    Fullname = e.Fullname,
                    DateOfBirth = e.DateOfBirth.HasValue ? e.DateOfBirth.Value.ToString(Constant.DATE_FORMAT) : "",
                    VisitCode = e.VisitCode,
                    TransferDate = e.TransferDate.HasValue ? e.TransferDate.Value.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND) : "",
                    IsAcceptNurse = e.IsAcceptNurse,
                    HandOverNurseUsername = e.HandOverNurseUsername,
                    ReceivingNurseUsername = e.ReceivingNurseUsername,
                    IsAcceptPhysician = e.IsAcceptPhysician,
                    HandOverPhysicianUsername = e.HandOverPhysicianUsername,
                    ReceivingPhysicianUsername = e.ReceivingPhysicianUsername,
                    HandOverUnit = e.HandOverUnit,
                    VisitTypeGroupCode = e.VisitTypeGroupCode,
                    VisitId = e.VisitId,
                    IsUseHandOverCheckList = e.IsUseHandOverCheckList
                });
            return new { count, results = items };
        }


        protected Guid CreateNewEDFromHandOver(Guid customer_id, Guid hand_over_check_list_id)
        {
            InHospital in_hospital = new InHospital();
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital.GetStatus().Id,
                customer_id,
                hand_over_check_list_id,
                null,
                null,
                GetSiteId(),
                GetSpecialtyId(),
                GetUser().Id
            );
            var ed = creater.CreateNewED();
            return ed.Id;
        }
        protected Guid CreateNewOPDFromHandOver(Guid customer_id, Guid hand_over_check_list_id)
        {
            InHospital in_hospital = new InHospital();
            var specialty = GetSpecialty();
            bool isAnesthesia = specialty == null ? false : specialty.IsAnesthesia;
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital.GetStatus("OPD").Id,
                customer_id,
                hand_over_check_list_id,
                null,
                null,
                GetSiteId(),
                GetSpecialtyId(),
                GetUser().Id,
                isAnesthesia
            );
            var opd = creater.CreateNewOPD();
            return opd.Id;
        }
        protected Guid CreateNewIPDFromHandOver(Guid customer_id, Guid hand_over_check_list_id)
        {
            InHospital in_hospital = new InHospital();
            var creater = new VisitCreater(
                unitOfWork,
                in_hospital.GetStatus("IPD").Id,
                customer_id,
                hand_over_check_list_id,
                null,
                null,
                GetSiteId(),
                GetSpecialtyId(),
                GetUser().Id
            );
            var ipd = creater.CreateNewIPD();
            SyncDoctor(ipd);
            return ipd.Id;
        }

        protected void UpdateHandOverCheckList(string visit_type_group_hand_over_code, dynamic hand_over_check_list, bool isCheckPreAnes)
        {
            if (visit_type_group_hand_over_code.Equals("ED"))
                unitOfWork.HandOverCheckListRepository.Update(hand_over_check_list);
            else if (visit_type_group_hand_over_code.Equals("OPD") && !isCheckPreAnes)
                unitOfWork.OPDHandOverCheckListRepository.Update(hand_over_check_list);            
            else if (visit_type_group_hand_over_code.Equals("IPD"))
                unitOfWork.IPDHandOverCheckListRepository.Update(hand_over_check_list);
            else if (visit_type_group_hand_over_code.Equals("EOC"))
                unitOfWork.EOCHandOverCheckListRepository.Update(hand_over_check_list);
            else if(visit_type_group_hand_over_code.Equals("OPD") && isCheckPreAnes)
                unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Update(hand_over_check_list);
        }
        protected void UpdateGroupOPDVisit(Guid visit_id, Guid? group_id)
        {
            var group_visit = unitOfWork.OPDRepository.Find(
                    e => !e.IsBooked &&
                    e.Id != visit_id &&
                    e.GroupId != null &&
                    e.GroupId == group_id &&
                    e.OPDHandOverCheckListId != null &&
                    !e.OPDHandOverCheckList.IsAcceptNurse &&
                    !e.OPDHandOverCheckList.IsAcceptPhysician
                );
            foreach (var vs in group_visit)
            {
                var vs_hocl = vs.OPDHandOverCheckList;
                vs_hocl.IsAcceptNurse = true;
                vs_hocl.IsAcceptPhysician = true;
                unitOfWork.OPDHandOverCheckListRepository.Update(vs_hocl);
            }
        }

        protected dynamic IsCreateVisitError(dynamic visit) {
            Guid customer_id = visit.CustomerId;
            Guid? group_id = null;
            try{ group_id = visit.GroupId; }
            catch (Exception){}

            InHospital in_hospital = new InHospital();
            in_hospital.SetState(customer_id, null, group_id, null);
            var in_hospital_visit = in_hospital.GetVisit();
            if (in_hospital_visit != null)
                return in_hospital.BuildErrorMessage(in_hospital_visit);

            string customer_PID = visit.Customer.PID;
            dynamic in_waiting_visit;
            if (!string.IsNullOrEmpty(customer_PID))
                in_waiting_visit = GetInWaitingAcceptPatientByPID(pid: customer_PID, visit_id: visit.Id, group_id: group_id);
            else
                in_waiting_visit = GetInWaitingAcceptPatientById(customer_id: visit.CustomerId, visit_id: visit.Id, group_id: group_id);
            if (in_waiting_visit != null)
            {
                var transfer = GetHandOverCheckListByVisit(in_waiting_visit);
                return BuildInWaitingAccpetErrorMessage(transfer.HandOverUnitPhysician, transfer.ReceivingUnitPhysician);
            }
            return null;
        }

        protected int CountEDHandOverVisit(Guid specialty_id)
        {
            return unitOfWork.HandOverCheckListRepository.Find(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id &&
                !e.IsAcceptNurse
            ).Count();
        }
        protected int CountOPDHandOverVisit(Guid specialty_id)
        {
            return unitOfWork.OPDHandOverCheckListRepository.Find(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id &&
                !e.IsAcceptNurse
            ).Count();
        }
        protected int CountOPDPreAnesthesiaHandOverVisit(Guid specialty_id)
        {
            return unitOfWork.OPDPreAnesthesiaHandOverCheckListRepository.Find(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id &&
                !e.IsAcceptNurse
            ).Count();
        }
        protected int CountIPDHandOverVisit(Guid specialty_id)
        {
            return unitOfWork.IPDHandOverCheckListRepository.Find(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id &&
                !e.IsAcceptNurse
            ).Count();
        }
        protected int CountEOCHandOverVisit(Guid specialty_id)
        {
            return unitOfWork.EOCHandOverCheckListRepository.Find(
                e => !e.IsDeleted &&
                e.HandOverTimePhysician != null &&
                e.HandOverUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId != null &&
                e.ReceivingUnitPhysicianId == specialty_id &&
                !e.IsAcceptNurse
            ).Count();
        }
    }
}
