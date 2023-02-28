using DataAccess.Models.EIOModel;
using DataAccess.Models.GeneralModel;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.EIOControllers
{
    [SessionAuthorize]
    public class HighlyRestrictedAntimicrobialConsultController : BaseApiController
    {
        private readonly string formCode = "IEOHIREANCO";

        [HttpGet]
        [Route("api/{area}/HighlyRestrictedAntimicrobialConsult/{type}/{visitId}")]
        [Permission(Code = "IEOHIREANCOGET")]
        public IHttpActionResult GetHighlyRestrictedAntimicrobialConsult(Guid visitId, string area = "ED")
        {
            var IsLocked = false;
            var visit = GetVisit(visitId, area);
            if (visit == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            var hracs = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.Find(x => !x.IsDeleted && x.VisitId == visitId).OrderBy(o => o.CreatedAt).Select(form => new
            {
                form.Id,
                form.VisitId,
                form.CreatedBy,
                CreatedAt = form.CreatedAt,
                UpdatedAt = form.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
            }).ToList();
           

            if (area == "IPD")
            {
                IsLocked = IPDIsBlock(visit, Constant.IPDFormCode.BienBanHoiChanSuDungKSQL);
            }
            return Content(HttpStatusCode.OK, new
            {
                Datas = hracs,
                IsLocked = IsLocked
            });
        }

        [HttpGet]
        [Route("api/{area}/HighlyRestrictedAntimicrobialConsult/{type}/{visitId}/{id}")]
        [Permission(Code = "IEOHIREANCOGET")]
        public IHttpActionResult Get(string type, Guid visitId, Guid id, string area = "ED")
        {
            var visit = GetVisit(visitId, area);
            if (visit == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            var hrac = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId == visitId && x.Id == id);
            if (hrac == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            else
            {
                return Content(HttpStatusCode.OK, formatOutput(hrac, visitId, area));
            }
        }
        [HttpGet]
        [Route("api/{area}/HighlyRestrictedAntimicrobialConsult/CheckFormLocked/{type}/{visitId}")]
        [Permission(Code = "IEOHIREANCOGET")]
        public IHttpActionResult CheckFormLocked(string type, Guid visitId, string area = "ED")
        {
            if (area == "IPD")
            {
                var ipd = GetIPD(visitId);
               var IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BienBanHoiChanSuDungKSQL);
                return Content(HttpStatusCode.OK, new
                {
                    IsLocked
                });

            }
            return Content(HttpStatusCode.OK, new
            {
                IsLocked = false
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/{area}/HighlyRestrictedAntimicrobialConsult/Create/{type}/{visitId}")]
        [Permission(Code = "IEOHIREANCOPOST")]
        public IHttpActionResult Post(string type, Guid visitId, string area = "ED")
        {

            dynamic visit = GetVisit(visitId, area);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);
            if (area == "IPD" && IPDIsBlock((IPD)visit, Constant.IPDFormCode.BienBanHoiChanSuDungKSQL))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var HiReAnCo = new HighlyRestrictedAntimicrobialConsult
            {
                VisitTypeGroupCode = area,
                VisitId = visitId
            };
            unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.Add(HiReAnCo);
            UpdateVisit(visit, area);
            return Content(HttpStatusCode.OK, new
            {
                HiReAnCo.Id,
                HiReAnCo.VisitId,
                HiReAnCo.CreatedBy,
                HiReAnCo.CreatedAt
            });
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/{area}/HighlyRestrictedAntimicrobialConsult/Update/{type}/{visitId}/{id}")]
        [Permission(Code = "IEOHIREANCOPUT")]
        public IHttpActionResult Update(string type, Guid visitId, Guid id, [FromBody] JObject request, string area = "ED")
        {

            dynamic visit = GetVisit(visitId, area);
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);            

            var hrac = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId == visitId && x.Id == id);
            if (hrac == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            else
            {
                if (area == "IPD" && IPDIsBlock((IPD)visit, Constant.IPDFormCode.BienBanHoiChanSuDungKSQL, hrac.Id))
                    return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);
                HandleUpdateOrCreateFormDatas(visitId, hrac.Id, formCode, request["Datas"]);
                MicrobiologicalResultsData(hrac.Id, request["HighlyRestrictedAntimicrobialConsultMicrobiologicalResults"]);
                AntimicrobialOrderData(hrac.Id, request["HighlyRestrictedAntimicrobialConsultAntimicrobialOrder"]);
                unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.Update(hrac);
                UpdateVisit(visit, area);
                return Content(HttpStatusCode.OK, new
                {
                    hrac.Id,
                    hrac.VisitId,
                    hrac.UpdatedAt,
                    hrac.UpdatedBy
                });
            }
        }
        [HttpPost]
        [CSRFCheck]
        [Route("api/{area}/HighlyRestrictedAntimicrobialConsult/Confirm/{type}/{visitId}/{id}")]
        [Permission(Code = "IEOHIREANCOCONFIRM")]
        public IHttpActionResult Confirm(string type, Guid visitId, Guid id, [FromBody] JObject request, string area = "ED")
        {

            var hrac = unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.FirstOrDefault(x => !x.IsDeleted && x.VisitId == visitId && x.Id == id);
            if (hrac == null) return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);            
            var username = request["username"]?.ToString();
            var password = request["password"]?.ToString();
            var user = GetAcceptUser(username, password);
            if (user == null)
                return Content(HttpStatusCode.BadRequest, Message.INFO_INCORRECT);

            var kind = request["kind"]?.ToString();
            var positions = user.PositionUsers.Where(e => !e.IsDeleted).Select(e => e.Position.EnName);
            if (!positions.Contains(kind))
                return Content(HttpStatusCode.Forbidden, Message.FORBIDDEN);

            hrac.ConfirmDate = DateTime.Now;
            hrac.ConfirmDoctorId = (Guid)user.Id;
            unitOfWork.HighlyRestrictedAntimicrobialConsultRepository.Update(hrac);
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, new { Id = hrac.Id });

        }
        private void AntimicrobialOrderData(dynamic formId, JToken datas)
        {
            foreach (var item in datas)
            {
                var data = item.ToObject<HighlyRestrictedAntimicrobialConsultAntimicrobialOrder>();
                data.HighlyRestrictedAntimicrobialConsultId = formId;
                if (string.IsNullOrEmpty(item["Id"]?.ToString()))
                {
                    unitOfWork.HighlyRestrictedAntimicrobialConsultAntimicrobialOrderRepository.Add(data);
                }
                else
                {
                    var finded = unitOfWork.HighlyRestrictedAntimicrobialConsultAntimicrobialOrderRepository.GetById((Guid)item["Id"]);
                    finded.HighlyRestrictedAntimicrobialConsultId = formId;
                    finded.Antimicrobial = data.Antimicrobial;
                    finded.Dose = data.Dose;
                    finded.Frequency = data.Frequency;
                    finded.Duration = data.Duration;
                    unitOfWork.HighlyRestrictedAntimicrobialConsultAntimicrobialOrderRepository.Update(finded);
                }
                unitOfWork.Commit();
            }
        }

        private void MicrobiologicalResultsData(Guid formId, JToken datas)
        {
            foreach (var item in datas)
            {
                var data = item.ToObject<HighlyRestrictedAntimicrobialConsultMicrobiologicalResults>();
                data.HighlyRestrictedAntimicrobialConsultId = formId;
                if (string.IsNullOrEmpty(item["Id"]?.ToString()))
                {
                    unitOfWork.HighlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepository.Add(data);
                }
                else
                {
                    var finded = unitOfWork.HighlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepository.GetById((Guid)item["Id"]);
                    finded.HighlyRestrictedAntimicrobialConsultId = formId;
                    finded.Others = data.Others;
                    finded.Specimen = data.Specimen;
                    finded.Date = data.Date;
                    finded.Culture = data.Culture;
                    unitOfWork.HighlyRestrictedAntimicrobialConsultMicrobiologicalResultsRepository.Update(finded);
                }
                unitOfWork.Commit();
            }
        }

        private dynamic formatOutput(HighlyRestrictedAntimicrobialConsult hrac, Guid visitId, string visitType)
        {
            return new
            {
                hrac.Id,
                isNew = hrac.CreatedAt != hrac.UpdatedAt,
                HighlyRestrictedAntimicrobialConsultMicrobiologicalResults = hrac.HighlyRestrictedAntimicrobialConsultMicrobiologicalResults.Where(e => !e.IsDeleted).OrderBy(e => e.CreatedAt).Select(e => new { e.Id, e.Specimen, e.Others, e.Culture, e.Date, e.HighlyRestrictedAntimicrobialConsultId }).ToList(),
                HighlyRestrictedAntimicrobialConsultAntimicrobialOrder = hrac.HighlyRestrictedAntimicrobialConsultAntimicrobialOrder.Where(e => !e.IsDeleted).OrderBy(e => e.CreatedAt).Select(e => new { e.Id, e.Antimicrobial, e.Dose, e.Frequency, e.Duration, e.HighlyRestrictedAntimicrobialConsultId }).ToList(),
                ConfirmDate = hrac.ConfirmDate?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                ConfirmDoctor = hrac.ConfirmDoctor?.Username,
                Datas = GetFormData(visitId, hrac.Id, formCode),
                IsLocked = visitType == "IPD" ? IPDIsBlock(GetIPD(visitId), Constant.IPDFormCode.BienBanHoiChanSuDungKSQL, hrac.Id) : false,
                Customer = GetCustomerInfoInVisit(visitId, visitType),
                hrac.CreatedBy
            };
        }
    }
}
