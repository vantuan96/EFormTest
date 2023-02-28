using DataAccess.Models.GeneralModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Models.IPDModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.MedicalRecordControllers
{
    [SessionAuthorize]
    public class MedicalRecordAlliedServiceController : BaseApiController
    {
        private readonly string[] codeIsRemove = { "E44.04", "E05.0036", "FB.09.04", "FB.09.05" };
        [HttpGet]
        [Route("api/AlliedService")]
        [Permission(Code = "VIEWALSER")]
        public IHttpActionResult AlliedService([FromUri] AlliedServiceParamesterModel param)
        {
            if (string.IsNullOrEmpty(param.PID))
                return Content(HttpStatusCode.BadRequest, new { ViName = "", EnName = "PID is null or empty!" });

            List<AlliedService> resultFromOh = OHClient.GetAlliedServiceFromOH(param.PID);
             var results = RemoveCLSService(resultFromOh);
            if (param.ServiceGroupId != null) {
                foreach (var item in results)
                {
                    var servicegroup = unitOfWork.ServiceRepository.FirstOrDefault(e => !e.IsDeleted && e.Code == item.ItemCode);
                    item.ServiceGroupId = servicegroup?.ServiceGroupId;
                }
                var groups = getListGroupId((Guid)param.ServiceGroupId);
                results = results.Where(e => groups.Contains((Guid)e.ServiceGroupId)).ToList();
            }
               
            if (!string.IsNullOrEmpty(param.VisitCode))
            {
                results = (from r in results where r.VisitCode == param.VisitCode select r).ToList();
            }
            if (!string.IsNullOrEmpty(param.FromDate))
            {
                var startDate = DateTime.ParseExact(param.FromDate, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                results = (from r in results where r.ServiceDate >= startDate select r).ToList();
            }

            if (!string.IsNullOrEmpty(param.ToDate))
            {
                var endDate = DateTime.ParseExact(param.ToDate, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
                results = (from r in results where r.ServiceDate <= endDate select r).ToList();
            }                
            if (results.Count() <= 0)
                return Content(HttpStatusCode.OK, new { ViName = "Không có thông tin! ", EnName = "No information!" });
            var responsive = results.OrderByDescending(e => e.ServiceDate).Select(e => new
            {
                ViName = e.ItemNameV,
                EnName = e.ItemNameE,
                Code = e.ItemCode,
                LocationViName = e.LocationNameV,
                LocationEnName = e.LocationNameE,
                ServiceBy = e.ServiceByAD,
                ServiceAt = e.ServiceDate,
                VisitCode = e.VisitCode,
                SiteCode = e.SiteCode,
                PID = e.PID
            });
            int count = responsive.Count();
            var items = responsive.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize)
            .ToList();
            return Content(HttpStatusCode.OK, new { Data = items, Count =  count });

            
        }
        private List<Guid> getListGroupId(Guid parrent_id)
        {
            var parrent = unitOfWork.ServiceGroupRepository.GetById(parrent_id);
            return unitOfWork.ServiceGroupRepository.Find(e => !e.IsDeleted && (e.KeyStruct.Contains(parrent.HISId.ToString()) || e.Id == parrent_id)).Select(e => e.Id).ToList();
        }
        private List<AlliedService> RemoveCLSService(List<AlliedService> listItem)
        {
            
            var checkCLSService = unitOfWork.ServiceRepository.AsQueryable().Where(e => e.IsDiagnosticReporting == true).Select(p => p.Code).ToArray();
            if(checkCLSService == null)
            {
                checkCLSService = codeIsRemove;
            }            
            var result = (from e in listItem
                          where (!checkCLSService.Contains(e.ItemCode.Trim()))                      
                          select e).ToList();           
            return result;
        }       
    }
}
