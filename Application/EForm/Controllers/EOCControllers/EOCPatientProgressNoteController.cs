using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.EDModels;

using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class EOCPatientProgressNoteController : BaseOPDApiController
    {
        [HttpGet]
        [Route("api/EOC/PatientProgressNote/{id}")]
        [Permission(Code = "EOC037")]
        public IHttpActionResult GetPatientProgressNote(Guid id)
        {
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            var patient_progress_note = opd.OPDPatientProgressNote;
            if (patient_progress_note == null || patient_progress_note.IsDeleted)
            {
                OPDPatientProgressNote ppn = new OPDPatientProgressNote();
                unitOfWork.OPDPatientProgressNoteRepository.Add(ppn);
                OPDObservationChart oc = new OPDObservationChart();
                unitOfWork.OPDObservationChartRepository.Add(oc);

                opd.OPDPatientProgressNoteId = ppn.Id;
                opd.OPDObservationChartId = oc.Id;
                unitOfWork.EOCRepository.Update(opd);
                unitOfWork.Commit();
            }    

            return Ok(new
            {
                patient_progress_note.Id,
                opd.RecordCode,
                EDId = opd.Id,
                Datas = patient_progress_note.OPDPatientProgressNoteDatas.Where(ppnd => !ppnd.IsDeleted)
                .OrderByDescending(ppnd => ppnd.NoteAt)
                .Select(ppnd => new {
                    ppnd.Id,
                    NoteAt = ppnd.NoteAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    ppnd.Note,
                    ppnd.Interventions,
                    Username = ppnd.CreatedBy
                }),
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/EOC/PatientProgressNote/{id}")]
        [Permission(Code = "EOC038")]
        public IHttpActionResult UpdatePatientProgressNote(Guid id, [FromBody]JObject request)
        {
            var opd = GetEOC(id);
            if (opd == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var user = GetUser();

            var patient_progress_note = opd.OPDPatientProgressNote;
            if (patient_progress_note == null || patient_progress_note.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_PPN_NOT_FOUND);

            var ppnd = patient_progress_note.OPDPatientProgressNoteDatas.Where(e => !e.IsDeleted).ToList();

            if (IsBlockAfter24h(opd.CreatedAt) && !HasUnlockPermission(opd.Id, "OPDPPN", user.Username))
                return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            if (string.IsNullOrEmpty(request["Id"]?.ToString()))
            {
                CreatePatientProgressNoteData(patient_progress_note.Id, request);
            }
            else
            {
                OPDPatientProgressNoteData patient_progress_note_data = ppnd.FirstOrDefault(oc => oc.Id == new Guid(request["Id"]?.ToString()));
                if (patient_progress_note_data != null && patient_progress_note_data.CreatedBy == user.Username)
                    UpdatePatientProgressNoteData(patient_progress_note_data, request);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void CreatePatientProgressNoteData(Guid request_id, PatientProgressNoteParamModel request)
        {
            OPDPatientProgressNoteData ppnd = new OPDPatientProgressNoteData
            {
                OPDPatientProgressNoteId = request_id,
                NoteAt = request.ConvertedNoteAt,
                Note = request.Note,
                Interventions = request.Interventions
            };
            unitOfWork.OPDPatientProgressNoteDataRepository.Add(ppnd);
            unitOfWork.Commit();
        }

        private void CreatePatientProgressNoteData(Guid patient_progress_note_id, JObject request)
        {
            OPDPatientProgressNoteData patient_progress_note_data = new OPDPatientProgressNoteData
            {
                OPDPatientProgressNoteId = patient_progress_note_id,
                NoteAt = DateTime.ParseExact(request["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null),
                Note = request["Note"]?.ToString(),
                Interventions = request["Interventions"]?.ToString()
            };
            unitOfWork.OPDPatientProgressNoteDataRepository.Add(patient_progress_note_data);
        }

        private void UpdatePatientProgressNoteData(OPDPatientProgressNoteData patient_progress_note_data, JObject request)
        {
            patient_progress_note_data.NoteAt = DateTime.ParseExact(request["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            patient_progress_note_data.Note = request["Note"]?.ToString();
            patient_progress_note_data.Interventions = request["Interventions"]?.ToString();
            unitOfWork.OPDPatientProgressNoteDataRepository.Update(patient_progress_note_data);
        }
    }
}
