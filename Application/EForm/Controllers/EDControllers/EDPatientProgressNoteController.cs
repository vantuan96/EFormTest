using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.EDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDPatientProgressNoteController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/PatientProgressNote/{id}")]
        [Permission(Code = "EPAPN1")]
        public IHttpActionResult GetPatientProgressNote(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var patient_progress_note = ed.PatientProgressNote;
            if (patient_progress_note == null || patient_progress_note.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_PPN_NOT_FOUND);

            return Ok(new
            {
                patient_progress_note.Id,
                ed.RecordCode,
                EDId = ed.Id,
                Datas = patient_progress_note.PatientProgressNoteDatas.Where(ppnd => !ppnd.IsDeleted)
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
        [Route("api/ED/PatientProgressNote/{id}")]
        [Permission(Code = "EPAPN2")]
        public IHttpActionResult UpdatePatientProgressNote(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            
            var patient_progress_note = ed.PatientProgressNote;
            if (patient_progress_note == null || patient_progress_note.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_PPN_NOT_FOUND);

            var ppnd = patient_progress_note.PatientProgressNoteDatas.Where(e=> !e.IsDeleted).ToList();

            if (string.IsNullOrEmpty(request["Id"]?.ToString()))
                CreatePatientProgressNoteData(patient_progress_note.Id, request);
            else
            {
                var user = GetUser();
                EDPatientProgressNoteData patient_progress_note_data = ppnd.FirstOrDefault(oc => oc.Id == new Guid(request["Id"]?.ToString()));
                if (patient_progress_note_data != null && patient_progress_note_data.CreatedBy == user.Username)
                    UpdatePatientProgressNoteData(patient_progress_note_data, request);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void CreatePatientProgressNoteData(Guid request_id, PatientProgressNoteParamModel request)
        {
            EDPatientProgressNoteData ppnd = new EDPatientProgressNoteData
            {
                PatientProgressNoteId = request_id,
                NoteAt = request.ConvertedNoteAt,
                Note = request.Note,
                Interventions = request.Interventions
            };
            unitOfWork.PatientProgressNoteDataRepository.Add(ppnd);
            unitOfWork.Commit();
        }

        private void CreatePatientProgressNoteData(Guid patient_progress_note_id, JObject request)
        {
            EDPatientProgressNoteData patient_progress_note_data = new EDPatientProgressNoteData
            {
                PatientProgressNoteId = patient_progress_note_id,
                NoteAt = DateTime.ParseExact(request["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null),
                Note = request["Note"]?.ToString(),
                Interventions = request["Interventions"]?.ToString()
            };
            unitOfWork.PatientProgressNoteDataRepository.Add(patient_progress_note_data);
        }

        private void UpdatePatientProgressNoteData(EDPatientProgressNoteData patient_progress_note_data, JObject request)
        {
            patient_progress_note_data.NoteAt = DateTime.ParseExact(request["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            patient_progress_note_data.Note = request["Note"]?.ToString();
            patient_progress_note_data.Interventions = request["Interventions"]?.ToString();
            unitOfWork.PatientProgressNoteDataRepository.Update(patient_progress_note_data);
        }

    }
}
