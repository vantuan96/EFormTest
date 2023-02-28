using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers.BaseEIOControllers;
using EForm.Models.EDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDPhysicianNoteController : EIOPhysicianNoteController
    {
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/PhysicianNote/Create/{id}")]
        [Permission(Code = "EDPDT01")]
        public IHttpActionResult CreateEDPhysicianNoteAPI(Guid id, [FromBody] JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var phy = CreateEIOPhysicianNote(ed.Id, "ED", request);
            return Content(HttpStatusCode.OK, new { phy.Id });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/PhysicianNote/Update/{id}")]
        [Permission(Code = "EDPDT02")]
        public IHttpActionResult UpdateEDPhysicianNoteAPI(Guid id, [FromBody] JObject request)
        {
            var note = GetEIOPhysicianNote(id);
            if (note == null)
                return Content(HttpStatusCode.NotFound, Message.EIO_PN_NOT_FOUND);

            var user = GetUser();
            if (user.Username != note.CreatedBy)
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);

            var phy = UpdateEIOPhysicianNote(id, request);
            return Content(HttpStatusCode.OK, new { phy.Id });
        }

        [HttpGet]
        [Route("api/ED/PhysicianNote/List/{id}")]
        [Permission(Code = "EDPDT03")]
        public IHttpActionResult ListEDPhysicianNoteAPI(Guid id, [FromUri]EDParameterModel Parameter)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var list = GetListEIOPhysicianNote(ed, "ED", Parameter.StartAt, Parameter.EndAt, Parameter.CreatedBy, Parameter.Sort);

            var lastUpdated = unitOfWork.EIOPhysicianNoteRepository.AsQueryable().OrderByDescending(e => e.UpdatedAt).FirstOrDefault(e => !e.IsDeleted && e.VisitId == ed.Id);
            return Ok(new
            {
                ed.RecordCode,
                EDId = ed.Id,
                Datas = list,
                UpdatedAt = lastUpdated?.UpdatedAt,
                UpdatedBy = lastUpdated?.UpdatedBy,
                ed.Version
            });
        }
    }
}
