using DataAccess.Models.EIOModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Controllers.EIOControllers;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDFormController : EIOFormController
    {
        // GET: EIOForm
        private readonly string visti_type = "ED";

        [Route("api/ED/MasterDataForm/{id}")]
        [Permission(Code = "FORDEV01")]
        public IHttpActionResult Get(Guid id)
        {
            return GetAPI(id, visti_type, "x");
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/MasterDataForm/Create/{id}")]
        [Permission(Code = "FORDEV01")]
        public IHttpActionResult Create(Guid id)
        {
            return CreateAPI(id, visti_type);
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/MasterDataForm/Update/{id}")]
        [Permission(Code = "FORDEV01")]
        public IHttpActionResult Update(Guid id, [FromBody] JObject request)
        {
            return UpdateAPI(id, request, visti_type);
        }
        [CSRFCheck]
        [HttpPost]
        [Route("api/ED/MasterDataForm/Confirm/{id}")]
        [Permission(Code = "FORDEV01")]
        public IHttpActionResult Confirm(Guid id, [FromBody] JObject request)
        {
            return ConfirmAPI(id, request, visti_type);
        }
        // for multi form
        //[Route("api/ED/MasterDataForm/{id}")]
        //[Permission(Code = "ED05102101")]
        //public IHttpActionResult List(Guid id)
        //{
        //    return GetsAPI(id, visti_type);
        //}
    }
}