using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models.EDModels;

using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDPatientProgressNoteController : BaseApiController
    {
        [HttpGet]
        [Route("api/IPD/PatientProgressNote/{id}")]
        [Permission(Code = "IPDOB03")]
        public IHttpActionResult GetPatientProgressNote(Guid id)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var patient_progress_note = ipd.IPDPatientProgressNote;
            if (patient_progress_note == null || patient_progress_note.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_PPN_NOT_FOUND);

            return Ok(new
            {
                patient_progress_note.Id,
                ipd.RecordCode,
                EDId = ipd.Id,
                Datas = patient_progress_note.IPDPatientProgressNoteDatas.Where(ppnd => !ppnd.IsDeleted)
                .OrderByDescending(ppnd => ppnd.NoteAt)
                .Select(ppnd => new {
                    ppnd.Id,
                    NoteAt = ppnd.NoteAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    ppnd.Note,
                    ppnd.Interventions,
                    Username = ppnd.CreatedBy
                }),
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.TheoDoiDienTien)
            });
        }

        [HttpPost]
        [CSRFCheck]
        [Route("api/IPD/PatientProgressNote/{id}")]
        [Permission(Code = "IPDOB04")]
        public IHttpActionResult UpdatePatientProgressNote(Guid id, [FromBody]JObject request)
        {
            var ipd = GetIPD(id);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock(ipd, Constant.IPDFormCode.TheoDoiDienTien))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var patient_progress_note = ipd.IPDPatientProgressNote;
            if (patient_progress_note == null || patient_progress_note.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_PPN_NOT_FOUND);

            var ppnd = patient_progress_note.IPDPatientProgressNoteDatas.Where(e => !e.IsDeleted).ToList();

            if (string.IsNullOrEmpty(request["Id"]?.ToString()))
                CreatePatientProgressNoteData(patient_progress_note.Id, request);
            else
            {
                var user = GetUser();
                IPDPatientProgressNoteData patient_progress_note_data = ppnd.FirstOrDefault(oc => oc.Id == new Guid(request["Id"]?.ToString()));
                if (patient_progress_note_data != null && patient_progress_note_data.CreatedBy == user.Username)
                    UpdatePatientProgressNoteData(patient_progress_note_data, request);
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void CreatePatientProgressNoteData(Guid request_id, PatientProgressNoteParamModel request)
        {
            IPDPatientProgressNoteData ppnd = new IPDPatientProgressNoteData
            {
                IPDPatientProgressNoteId = request_id,
                NoteAt = request.ConvertedNoteAt,
                Note = request.Note,
                Interventions = request.Interventions
            };
            unitOfWork.IPDPatientProgressNoteDataRepository.Add(ppnd);
            unitOfWork.Commit();
        }

        private void CreatePatientProgressNoteData(Guid patient_progress_note_id, JObject request)
        {
            IPDPatientProgressNoteData patient_progress_note_data = new IPDPatientProgressNoteData
            {
                IPDPatientProgressNoteId = patient_progress_note_id,
                NoteAt = DateTime.ParseExact(request["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null),
                Note = request["Note"]?.ToString(),
                Interventions = request["Interventions"]?.ToString()
            };
            unitOfWork.IPDPatientProgressNoteDataRepository.Add(patient_progress_note_data);
        }

        private void UpdatePatientProgressNoteData(IPDPatientProgressNoteData patient_progress_note_data, JObject request)
        {
            patient_progress_note_data.NoteAt = DateTime.ParseExact(request["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            patient_progress_note_data.Note = request["Note"]?.ToString();
            patient_progress_note_data.Interventions = request["Interventions"]?.ToString();
            unitOfWork.IPDPatientProgressNoteDataRepository.Update(patient_progress_note_data);
        }
    }
}
