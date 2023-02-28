using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models.EDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDCareNoteController : EIOCareNoteController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CareNote/Create/{id}")]
        [Permission(Code = "EDPCC01")]
        public IHttpActionResult CreateEDCareNoteAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var phy = CreateEIOCareNote(ed.Id, "ED", request);
            return Content(HttpStatusCode.OK, new { phy.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/CareNote/Update/{id}")]
        [Permission(Code = "EDPCC02")]
        public IHttpActionResult UpdateEDCareNoteAPI(Guid id, [FromBody] JObject request)
        {
            var note = GetEIOCareNote(id);
            if (note == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_CN_NOT_FOUND);

            var user = GetUser();
            if (user.Username != note.CreatedBy)
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);

            var phy = UpdateEIOCareNote(id, request);
            return Content(HttpStatusCode.OK, new { phy.Id });
        }

        [HttpGet]
        [Route("api/ED/CareNote/List/{id}")]
        [Permission(Code = "EDPCC03")]
        public IHttpActionResult ListEDCareNoteAPI(Guid id, [FromUri] EDParameterModel Parameter)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var list = GetListEIOCareNote(id, "ED", Parameter.StartAt, Parameter.EndAt, Parameter.CreatedBy, Parameter.Sort);

            // thêm người update cuối 
            var lastUpdate = (from e in unitOfWork.EIOCareNoteRepository.AsQueryable()
                              where !e.IsDeleted && e.VisitId == id
                              orderby e.UpdatedAt descending
                              select e).FirstOrDefault();
            return Ok(new
            {
                ed.Customer.AgeFormated,
                ed.RecordCode,
                EDId = ed.Id,
                Datas = list,
                UpdatedAt = lastUpdate?.UpdatedAt,
                UpdatedBy = lastUpdate?.UpdatedBy,
                ed.Version
            });
        }
    }
}