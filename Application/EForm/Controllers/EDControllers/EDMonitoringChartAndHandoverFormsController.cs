using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json.Linq;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDMonitoringChartAndHandoverFormsController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/MonitoringChartAndHandoverForm/{id}")]
        [Permission(Code = "EMCHF1")]
        public IHttpActionResult GetMonitoringChartAndHandoverForm(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var monitoring_chart = ed.MonitoringChartAndHandoverForm;
            if (monitoring_chart == null || monitoring_chart.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_MCA_NOT_FOUND);

            return Content(HttpStatusCode.OK, new
            {
                monitoring_chart.Id,
                ed.RecordCode,
                EDId = ed.Id,
                monitoring_chart.Note,
                Datas = monitoring_chart.MonitoringChartAndHandoverFormDatas.Where(m => !m.IsDeleted).Select(m => m.Path).ToList(),
            });
        }


        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/MonitoringChartAndHandoverForm/{id}")]
        [Permission(Code = "EMCHF2")]
        public IHttpActionResult UpdateMonitoringChartAndHandoverForm(Guid id, [FromBody]JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var monitoring_chart = ed.MonitoringChartAndHandoverForm;
            if (monitoring_chart == null || monitoring_chart.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_MCA_NOT_FOUND);

            //var user = GetUser();
            //if (IsBlockAfter24h(ed.CreatedAt) && !HasUnlockPermission(ed.Id, "EDMCA", user.Username))
            //    return Content(HttpStatusCode.BadRequest, Message.TIME_FORBIDDEN);

            //if (!IsUserCreateFormAuto(user.Username, monitoring_chart.UpdatedBy, monitoring_chart.CreatedAt, monitoring_chart.UpdatedAt))
            //    return Content(HttpStatusCode.BadRequest, Message.OWNER_FORBIDDEN);

            HandleUpdateNote(monitoring_chart, request["Note"]?.ToString());
            HandleUPdateOrCreateMonitoringChartAndHandoverFormData(monitoring_chart, request["Datas"].ToObject<List<string>>());

            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }


        private void HandleUpdateNote(EDMonitoringChartAndHandoverForm monitoring_chart, string note)
        {
            if (note != monitoring_chart.Note)
            {
                monitoring_chart.Note = note;
                unitOfWork.MonitoringChartAndHandoverFormRepository.Update(monitoring_chart);
                unitOfWork.Commit();
            }
        }

        private void HandleUPdateOrCreateMonitoringChartAndHandoverFormData(EDMonitoringChartAndHandoverForm monitoring_chart, List<string> request_mc_data)
        {
            var monitoring_chart_datas = monitoring_chart.MonitoringChartAndHandoverFormDatas.Where(m => !m.IsDeleted).ToList();
            foreach (var item in monitoring_chart_datas)
            {
                if (request_mc_data.Contains(item.Path))
                    request_mc_data.Remove(item.Path);
                else
                    unitOfWork.MonitoringChartAndHandoverFormDataRepository.Delete(item);
            }
            foreach (var item in request_mc_data)
            {
                var mcd = new EDMonitoringChartAndHandoverFormData
                {
                    Path = item,
                    MonitoringChartAndHandoverFormId = monitoring_chart.Id
                };
                unitOfWork.MonitoringChartAndHandoverFormDataRepository.Add(mcd);
            }
            monitoring_chart.UpdatedBy = GetUser().Username;
            unitOfWork.MonitoringChartAndHandoverFormRepository.Update(monitoring_chart);
            unitOfWork.Commit();
        }
    }
}