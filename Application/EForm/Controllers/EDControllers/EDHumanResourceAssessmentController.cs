using DataAccess.Models.GeneralModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.EDControllers
{
    [SessionAuthorize]
    public class EDHumanResourceAssessmentController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/HumanResourceAssessment/List")]
        [Permission(Code = "EHURA1")]
        public IHttpActionResult ListEDHumanResourceAssessmentAPI([FromUri]HumanResourceListQueryModel request)
        {
            if(!request.Validate())
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var spec_id = GetSpecialtyId();

            var hrs = unitOfWork.HumanResourceAssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.SpecialtyId != null &&
                e.SpecialtyId == spec_id &&
                e.Date >= request.ConvertedStartDate &&
                e.Date <= request.ConvertedEndDate
            ).OrderBy(e => e.Date).Select( e=> new { e.Id, Date = e.Date?.ToString(Constant.DATE_FORMAT) });

            return Content(HttpStatusCode.OK, hrs);
        }

        [HttpGet]
        [Route("api/ED/HumanResourceAssessment/Shift")]
        [Permission(Code = "EHURA2")]
        public IHttpActionResult ListEDHumanResourceAssessmentShiftAPI()
        {
            var site_id = GetSiteId();
            var shifts = unitOfWork.HumanResourceAssessmentShiftRepository.Find(
                e => !e.IsDeleted &&
                e.SiteId != null &&
                e.SiteId == site_id
            ).OrderBy(e => e.ViName).Select(e => new { 
                e.Id, e.ViName, e.EnName, e.StartAt, e.EndAt
            });
            return Content(HttpStatusCode.OK, shifts);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/HumanResourceAssessment/Create")]
        [Permission(Code = "EHURA3")]
        public IHttpActionResult CreateEDHumanResourceAssessmentAPI([FromBody]JObject request)
        {
            var spec_id = GetSpecialtyId();
            var positions = unitOfWork.HumanResourceAssessmentPositionRepository.Find(
                e => !e.IsDeleted && 
                e.SpecialtyId != null &&
                e.SpecialtyId == spec_id
            ).OrderBy(e => e.Order);

            var site_id = GetSiteId();
            var shifts = unitOfWork.HumanResourceAssessmentShiftRepository.Find(
                e => !e.IsDeleted &&
                e.SiteId != null &&
                e.SiteId == site_id
            );

            var s_date = request["Date"]?.ToString();
            var date = DateTime.ParseExact(s_date, Constant.DATE_FORMAT, null);

            foreach(var shf in shifts)
            {
                var s_start_at = $"{shf.StartAt} {s_date}";
                var start_at = DateTime.ParseExact(s_start_at, Constant.TIME_DATE_FORMAT, null);

                var s_end_at = $"{shf.EndAt} {s_date}";
                var end_at = DateTime.ParseExact(s_end_at, Constant.TIME_DATE_FORMAT, null);
                if (start_at >= end_at)
                    end_at = end_at.AddDays(1);

                var hra = new HumanResourceAssessment
                {
                    ViName = shf.ViName,
                    EnName = shf.EnName,
                    Date = date,
                    SpecialtyId = spec_id,
                    StartAt = start_at,
                    EndAt = end_at
                };
                unitOfWork.HumanResourceAssessmentRepository.Add(hra);

                foreach (var pos in positions)
                    foreach (var type in Constant.HUMAN_RESOURCE_STAFF_TYPE)
                        CreateHumanResourceAssessmentStaff(pos.ViName, pos.EnName, pos.Order, type, hra.Id);
            }
            
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        [HttpGet]
        [Route("api/ED/HumanResourceAssessment/TV")]
        [Permission(Code = "EHURA4")]
        public IHttpActionResult GetEDHumanResourceAssessmentTVAPI()
        {
            List<Guid> hra_ids;
            hra_ids = GetListHumanResourceAssessmentId(DateTime.Now.AddMinutes(15));
            if (hra_ids == null)
                return Content(HttpStatusCode.BadRequest, Message.HUMAN_RESOURCE_NOT_FOUND);

            var staffs = GetHumanResourceAssessmentStaffModel(hra_ids);
            var datas = GetHumanResourceItem(staffs);

            return Content(HttpStatusCode.OK, datas);
        }

        [HttpGet]
        [Route("api/ED/HumanResourceAssessment/Detail")]
        [Permission(Code = "EHURA5")]
        public IHttpActionResult GetEDHumanResourceAssessmentAPI([FromUri]HumanResourceQueryModel request)
        {
            if(string.IsNullOrEmpty(request.Date))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var result = new List<dynamic>();
            List<HumanResourceAssessment> hras = GetHumanResourceAssessments(request.ConvertedDate);
            foreach(var hr in hras)
            {
                var staffs = GetHumanResourceAssessmentStaffModel(new List<Guid> { hr.Id });
                var datas = GetHumanResourceItem(staffs);
                result.Add(new
                {
                    hr.Id,
                    hr.ViName,
                    hr.EnName,
                    StartAt = hr.StartAt?.ToString(Constant.TIME_DATE_FORMAT),
                    EndAt = hr.EndAt?.ToString(Constant.TIME_DATE_FORMAT),
                    Datas = datas
                });
            }
            return Content(HttpStatusCode.OK, result);
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/HumanResourceAssessment/Update")]
        [Permission(Code = "EHURA6")]
        public IHttpActionResult UpdateHumanResourceAssessment([FromBody]JObject request)
        {
            var s_id = request["Id"]?.ToString();
            if (string.IsNullOrEmpty(s_id))
                return Content(HttpStatusCode.BadRequest, Message.HUMAN_RESOURCE_NOT_FOUND);

            var id = new Guid(s_id);
            var hra = unitOfWork.HumanResourceAssessmentRepository.GetById(id);
            if(hra == null)
                return Content(HttpStatusCode.BadRequest, Message.HUMAN_RESOURCE_NOT_FOUND);

            ClearHumanResourceAssessmentStaffs(hra);

            CreateListHumanResourceAssessmentStaff(hra.Id, request["Datas"]);

            unitOfWork.Commit();

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        private HumanResourceAssessmentStaff CreateHumanResourceAssessmentStaff(string vi_name, string en_name, int? order, string type, Guid form_id)
        {
            var staff = new HumanResourceAssessmentStaff
            {
                ViName = vi_name,
                EnName = en_name,
                Order = (int)order,
                Type = type,
                HumanResourceAssessmentId = form_id
            };
            unitOfWork.HumanResourceAssessmentStaffRepository.Add(staff);
            return staff;
        }
        private HumanResourceAssessmentStaff CreateHumanResourceAssessmentStaff(string vi_name, string en_name, int? order, string type, Guid form_id, Guid? user_id)
        {
            var staff = new HumanResourceAssessmentStaff
            {
                ViName = vi_name,
                EnName = en_name,
                Order = (int)order,
                Type = type,
                HumanResourceAssessmentId = form_id,
                UserId = user_id
            };
            unitOfWork.HumanResourceAssessmentStaffRepository.Add(staff);
            return staff;
        }

        private List<Guid> GetListHumanResourceAssessmentId(DateTime? now)
        {
            var spec_id = GetSpecialtyId();
            return unitOfWork.HumanResourceAssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.SpecialtyId != null &&
                e.SpecialtyId == spec_id &&
                e.StartAt < now && now < e.EndAt
            ).Select(e => e.Id).ToList();
        }
        private List<HumanResourceAssessment> GetHumanResourceAssessments(DateTime? date)
        {
            var spec_id = GetSpecialtyId();
            return unitOfWork.HumanResourceAssessmentRepository.Find(
                e => !e.IsDeleted &&
                e.SpecialtyId != null &&
                e.SpecialtyId == spec_id &&
                e.Date == date
            ).OrderBy(e => e.ViName).ToList();
        }
        private List<HumanResourceAssessmentStaffModel> GetHumanResourceAssessmentStaffModel(List<Guid> hra_ids)
        {
            return  (from staff_sql in unitOfWork.HumanResourceAssessmentStaffRepository.AsQueryable()
                        .Where(
                        e => !e.IsDeleted &&
                        e.HumanResourceAssessmentId != null &&
                        hra_ids.Contains((Guid)e.HumanResourceAssessmentId)
                    )
                    join user_sql in unitOfWork.UserRepository.AsQueryable() on staff_sql.UserId equals user_sql.Id
                    into u_list
                    from user_sql in u_list.DefaultIfEmpty()
                    select new HumanResourceAssessmentStaffModel
                    {
                        ViName = staff_sql.ViName,
                        EnName = staff_sql.EnName,
                        Order = staff_sql.Order,
                        Type = staff_sql.Type,
                        UserId = user_sql.Id,
                        Username = user_sql.Username,
                        Fullname = user_sql.DisplayName,
                        FullShort = user_sql.Fullname,
                    }).Distinct().OrderBy(e => e.Order).ToList();
        }
        private List<HumanResourceItem> GetHumanResourceItem(List<HumanResourceAssessmentStaffModel> staffs)
        {
            var result = new List<HumanResourceItem>();
            foreach (var stf in staffs)
            {
                var item = result.FirstOrDefault(e => e.ViName == stf.ViName);
                if (item == null)
                {
                    item = new HumanResourceItem
                    {
                        ViName = stf.ViName,
                        EnName = stf.EnName,
                        Order = stf.Order,
                        GroupStaffs = new List<HumanResourceItemGroupStaff>()
                    };
                    result.Add(item);
                }

                var g_staff = item.GroupStaffs.FirstOrDefault(e => e.Type == stf.Type);
                if (g_staff == null)
                {
                    g_staff = new HumanResourceItemGroupStaff
                    {
                        Type = stf.Type,
                        Users = new List<dynamic>()
                    };
                    item.GroupStaffs.Add(g_staff);
                }
                g_staff.Users.Add(new
                {
                    Id = stf.UserId,
                    stf.Username,
                    stf.Fullname,
                    stf.FullShort,
                });
            }
            return result;
        }

        private void ClearHumanResourceAssessmentStaffs(HumanResourceAssessment hra)
        {
            var staffs = hra.HumanResourceAssessmentStaffs.Where(e => !e.IsDeleted).ToList();
            foreach (var stf in staffs)
                unitOfWork.HumanResourceAssessmentStaffRepository.HardDelete(stf);
        }
        private void CreateListHumanResourceAssessmentStaff(Guid hra_id, JToken datas)
        {
            foreach (var item in datas)
            {
                string vi_name = item["ViName"]?.ToString();
                string en_name = item["EnName"]?.ToString();
                int? order = item["Order"]?.ToObject<int>();

                foreach (var group in item["GroupStaffs"])
                {
                    
                    if (group["Users"].Count() == 0)
                    {
                        foreach (var c_type in Constant.HUMAN_RESOURCE_STAFF_TYPE)
                            CreateHumanResourceAssessmentStaff(vi_name, en_name, order, c_type, hra_id);
                        continue;
                    }

                    string type = group["Type"]?.ToString();
                    foreach (var user in group["Users"])
                    {
                        string s_user_id = user["Id"]?.ToString();
                        Guid? user_id;
                        if (string.IsNullOrEmpty(s_user_id))
                            user_id = null;
                        else
                            user_id = new Guid(s_user_id);

                        CreateHumanResourceAssessmentStaff(vi_name, en_name, order, type, hra_id, user_id);
                    }
                }
            }
        }
    }
}
