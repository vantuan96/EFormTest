using DataAccess.Models.OPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.OPDControllers
{
    [SessionAuthorize]
    public class OPDSurgeryRespiratoryAndSurgeryCardiologyController : BaseOPDApiController
    {
        [HttpGet]
        [Route("api/OPD/SynsDataAnesthesia/{visitId}/{formCode}")]
        public IHttpActionResult OPDGetDataAnesthesia(Guid visitId, string formCode)
        {
            OPD visit = GetOPD(visitId);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);

            var form = unitOfWork.EIOFormRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.FormCode == formCode && e.VisitTypeGroupCode == "OPD");
            bool reques_form = GetRequetFromAnesthesiaByFormCode(visit, formCode);
            if (!reques_form && form == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    visitId = visitId,
                    FormId = form?.Id,
                    IsLock24h = true,
                    FORM_NOT_FOUND = new
                    {
                        ViMessage = "NB không có chỉ định khám chuyên khoa tại Phiếu khám gây mê",
                        EnMessage = "The patient does not have a prescription for a specialist examination at the Pre-Anesthesia Consultation",
                        NeedNew = true
                    }
                });

            var user = GetUser();
            var isLocked = Is24hLocked(visit.CreatedAt, visitId, formCode, user?.Username, null);
            var datas = GetDataAnesthesiaAndShortTerm(visit, formCode);

            return Content(HttpStatusCode.OK, new
            {
                visitId = visitId,
                FormId = form?.Id,
                IsLock24h = isLocked,
                Datas = datas
            });
        }

        private bool GetRequetFromAnesthesiaByFormCode(OPD visit, string formCode)
        {
            string code_masterData = "";
            if (formCode == "FORMOPDPYKKCKHHTP")
                code_masterData = "PRANCO344";
            else if (formCode == "A01_204_030320_VE")
                code_masterData = "PRANCO343";

            var datas = visit.OPDOutpatientExaminationNote?.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted && e.Code == code_masterData).FirstOrDefault();
            if (datas?.Value?.ToLower() == "true")
                return true;

            return false;
        }

        private dynamic GetDataAnesthesiaAndShortTerm(OPD visit, string formCode)
        {
            var datas = visit.OPDOutpatientExaminationNote?.OPDOutpatientExaminationNoteDatas.Where(e => !e.IsDeleted);
            var diagnos = GetVisitDiagnosisAndICD(visit.Id, "OPD", false, getForAnesthesia:true);
            var vitalSigns = OPDGetInforVitalSigns(visit);
            string code_requestdate = "";
            if (formCode.ToUpper().Equals("FORMOPDPYKKCKHHTP"))
                code_requestdate = "PRANCO344";
            else if (formCode.ToUpper().Equals("A01_204_030320_VE"))
                code_requestdate = "PRANCO343";

            var result = new
            {
                Requestdate = datas?.Where(e => e.Code == code_requestdate && e.Value != null && e.Value.ToLower().Equals("true")).Select(e => e.UpdatedAt).FirstOrDefault()?.ToString("HH:mm dd/MM/yyyy"),
                AnesthesiologistName = datas?.Where(e => e.Code == code_requestdate && e.Value != null && e.Value.ToLower().Equals("true")).Select(e => e.UpdatedBy).FirstOrDefault(),
                Diagnosis = diagnos,
                Treatment = datas?.Where(e => e.Code == "PRANCO136").Select(e => e.Value).FirstOrDefault(),
                TypeOfSurgery = datas?.Where(e => e.Code == "PRANCO7").Select(e => e.Value).FirstOrDefault(),
                DateOfSurgery = datas?.Where(e => e.Code == "PRANCO9").Select(e => e.Value).FirstOrDefault(),
                VitalSigns = vitalSigns
            };

            return result;
        }

        private object OPDGetInforVitalSigns(OPD visit)
        {
            var opdInitialAssessmentForShortTerm = visit.OPDInitialAssessmentForShortTerm?.OPDInitialAssessmentForShortTermDatas;

            return new
            {
                VitalSigns = new
                {
                    Weight = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIANS")?.Value,
                    Height = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPHEIANS")?.Value,
                    SpO2 = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPSPOANS")?.Value,
                    BP = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPBP0ANS")?.Value,
                    Pulse = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPPULANS")?.Value,
                    RR = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPRR0ANS")?.Value,
                    T = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPTEMANS")?.Value,
                    PrePregnancyWeight = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPWEIPRT")?.Value,
                    HistoryOfAllergiesDGBD = opdInitialAssessmentForShortTerm?.FirstOrDefault(x => x.Code == "OPDIAFSTOPALLANS")?.Value,
                }
            };
        }
    }
}
