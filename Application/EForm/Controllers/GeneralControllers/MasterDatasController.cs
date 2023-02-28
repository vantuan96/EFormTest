using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using EForm.Authentication;
using EForm.BaseControllers;
using EMRModels;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class MasterDatasController : BaseApiController
    {
        [HttpGet]
        [Route("api/MasterDatas")]
        [Permission(Code = "GMADA1")]
        public IHttpActionResult GetMasterDatas([FromUri]MasterDataParameterModel request)
        {
            if(request == null)
                return BadRequest();

            if (request.Code != null)
                return Content(HttpStatusCode.OK, GetMasterDataByCode(request.Code));

            if (request.Group != null)
                return Content(HttpStatusCode.OK, GetMasterDataByGroup(request.Group));

            if (request.Form != null)
                return Content(HttpStatusCode.OK, GetMasterDataByForm(request.Form));

            if (request.Clinic != null)
                return Content(HttpStatusCode.OK, GetMasterDataByClinic(request.Clinic)); 

            return BadRequest();
        }

        private dynamic GetMasterDataByCode(string code)
        {
            var data = unitOfWork.MasterDataRepository.FirstOrDefault(md => !md.IsDeleted && md.Code == code);
            return new
            {
                data?.ViName, data?.EnName, data?.Code, data?.Group, data?.Order, data?.DataType,
                data?.Level, data?.IsReadOnly, data?.Note, data?.Data, data?.Clinic
            };
        }

        private dynamic GetMasterDataByGroup(string group)
        {
            return unitOfWork.MasterDataRepository.Find(md => !md.IsDeleted && md.Group == group)
                    .Select(md => new {
                        md.ViName, md.EnName, md.Code, md.Group, md.Order, md.DataType,
                        md.Level, md.IsReadOnly, md.Note, md.Data, md.Clinic
                    })
                    .OrderBy(md => md.Order)
                    .ToList();
        }

        private dynamic GetMasterDataByForm(string form)
        {

            var raw_data_level_3 = unitOfWork.MasterDataRepository.Find(md => md.Form == form && md.Level == 3)
            .Select(md => new {
                md.ViName,
                md.EnName,
                md.Code,
                md.Group,
                md.Order,
                md.DataType,
                md.Level,
                md.IsReadOnly,
                md.Note,
                md.Data,
                md.Clinic,
                md.DefaultValue,
                Value = "",
                Id = "",
            })
            .OrderBy(md => md.Order)
            .ToList();

            var raw_data_level_1 = unitOfWork.MasterDataRepository.Find(md => !md.IsDeleted && md.Form == form && md.Level == 1)
                .Select(md => new {
                    md.ViName, md.EnName, md.Code, md.Group, md.Order, md.DataType,
                    md.Data,
                    md.Level, md.Note, md.Clinic,
                    md.DefaultValue
                })
                .OrderBy(md => md.Order)
                .ToList();

            var raw_data_level_2 = unitOfWork.MasterDataRepository.Find(md => md.Form == form && md.Level == 2)
            .Select(md => new {
                md.ViName, md.EnName, md.Code, md.Group, md.Order, md.DataType,
                md.Level, md.IsReadOnly, md.Note, md.Data, md.Clinic,
                Value = "", Id = "",
                md.DefaultValue,
                Items = raw_data_level_3.Where(md2 => md2.Group == md.Code).ToList()
            })
            .OrderBy(md => md.Order)
            .ToList();
            
            List<dynamic> results = new List<dynamic>();
            foreach (var lv1 in raw_data_level_1)
            {
                var items = raw_data_level_2.Where(md => md.Group == lv1.Code);
                results.Add(new
                {
                    lv1.ViName, lv1.EnName, lv1.Code, lv1.Group, lv1.Order, lv1.DataType, 
                    lv1.Level, lv1.Note, lv1.Clinic, Items = items,
                    lv1.Data
                });
            }
            return results;
        }
        private dynamic GetMasterDataByClinic(string clinic)
        {
            var raw_data_level_3 = unitOfWork.MasterDataRepository.Find(md => md.Clinic.Contains(clinic) && md.Level == 3)
            .Select(md => new {
                md.ViName,
                md.EnName,
                md.Code,
                md.Group,
                md.Order,
                md.DataType,
                md.Level,
                md.IsReadOnly,
                md.Note,
                md.Data,
                md.Clinic,
                md.DefaultValue,
                Value = "",
                Id = "",
                md.CreatedAt,
            })
            .OrderBy(md => md.CreatedAt)
            .ToList();

            var raw_data_level_1 = unitOfWork.MasterDataRepository.Find(md => !md.IsDeleted && md.Clinic.Contains(clinic) && md.Level == 1)
                .Select(md => new {
                    md.ViName,
                    md.EnName,
                    md.Code,
                    md.Group,
                    md.Order,
                    md.DataType,
                    md.Data,
                    md.Level,
                    md.Note,
                    md.Clinic,
                    md.DefaultValue,
                    md.CreatedAt
                })
                .OrderBy(md => md.CreatedAt)
                .ToList();

            var raw_data_level_2 = unitOfWork.MasterDataRepository.Find(md => md.Clinic.Contains(clinic) && md.Level == 2)
            .Select(md => new {
                md.ViName,
                md.EnName,
                md.Code,
                md.Group,
                md.Order,
                md.DataType,
                md.Level,
                md.IsReadOnly,
                md.Note,
                md.Data,
                md.Clinic,
                Value = "",
                Id = "",
                md.DefaultValue,
                Items = raw_data_level_3.Where(md2 => md2.Group == md.Code).ToList(),
                md.CreatedAt
            })
            .OrderBy(md => md.CreatedAt)
            .ToList();

            List<dynamic> results = new List<dynamic>();
            foreach (var lv1 in raw_data_level_1)
            {
                var items = raw_data_level_2.Where(md => md.Group == lv1.Code);
                results.Add(new
                {
                    lv1.ViName,
                    lv1.EnName,
                    lv1.Code,
                    lv1.Group,
                    lv1.Order,
                    lv1.DataType,
                    lv1.Level,
                    lv1.Note,
                    lv1.Clinic,
                    Items = items,
                    lv1.Data,
                    lv1.CreatedAt
                });
            }
            return results;
        }
    }
}