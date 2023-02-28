using DataAccess.Models.EDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;

using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.EDControllers
{
    [SessionAuthorize]
    public class EDObservationChartController : BaseEDApiController
    {
        [HttpGet]
        [Route("api/ED/ObservationChart/{id}")]
        [Permission(Code = "EOBCH1")]
        public IHttpActionResult GetObservationChart(Guid id)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                var ed = GetED(id);
                if (ed == null)
                    return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

                var observation_chart = ed.ObservationChart;
                if (observation_chart == null || observation_chart.IsDeleted)
                    return Content(HttpStatusCode.NotFound, Message.ED_OC0_NOT_FOUND);
                var dataa = observation_chart.ObservationChartDatas.Where(e => !e.IsDeleted).ToList().OrderBy(oc => oc.NoteAt).Select(oc => new
                {
                    oc.Id,
                    NoteAt = oc.NoteAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                    oc.Other,
                    oc.SysBP,
                    oc.DiaBP,
                    oc.Pulse,
                    oc.Temperature,
                    oc.Resp,
                    oc.SpO2,
                    oc.RestPainScore,
                    oc.MovePainScore,
                    Fullname = oc.CreatedBy
                });
                return Ok(new
                {
                    ed.RecordCode,
                    observation_chart.Id,
                    EDId = ed.Id,
                    Datas = dataa
                });
            }
        }

        [HttpGet]
        [Route("api/ED/ObservationChart/Chart/{id}")]
        [Permission(Code = "EOBCH2")]
        public IHttpActionResult GetChart(Guid id)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var user = GetUser();

            var observation_chart = ed.ObservationChart;
            if (observation_chart == null || observation_chart.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_OC0_NOT_FOUND);

            var created_by = unitOfWork.UserRepository.FirstOrDefault(u => !u.IsDeleted && u.CreatedBy == observation_chart.CreatedBy);
            var observation_chart_datas = observation_chart.ObservationChartDatas.Where(e => !e.IsDeleted).ToList();
            return Ok(new {
                observation_chart.Id,
                EDId = ed.Id,
                Time = observation_chart_datas.OrderBy(oc => oc.NoteAt).Select(oc => oc.NoteAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)).ToList(),
                SysBP = observation_chart_datas.OrderBy(oc => oc.NoteAt).Select(oc => oc.SysBP).ToList(),
                DiaBP = observation_chart_datas.OrderBy(oc => oc.NoteAt).Select(oc => oc.DiaBP).ToList(),
                Pulse = observation_chart_datas.OrderBy(oc => oc.NoteAt).Select(oc => oc.Pulse).ToList(),
                Temperature = observation_chart_datas.OrderBy(oc => oc.NoteAt).Take(10).Select(oc => oc.Temperature).ToList(),
                SpO2 = observation_chart_datas.OrderBy(oc => oc.NoteAt).Select(oc => oc.SpO2).ToList(),
                Resp = observation_chart_datas.OrderBy(oc => oc.NoteAt).Select(oc => oc.Resp).ToList(),
                RestPainScore = observation_chart_datas.OrderBy(oc => oc.NoteAt).Select(oc => oc.RestPainScore).ToList(),
                MovePainScore = observation_chart_datas.OrderBy(oc => oc.NoteAt).Take(10).Select(oc => oc.MovePainScore).ToList(),
            });
        }
 
        [HttpPost]
        [CSRFCheck]
        [Route("api/ED/ObservationChart/{id}")]
        [Permission(Code = "EOBCH3")]
        public IHttpActionResult UpdateObservationChart(Guid id, JObject request)
        {
            var ed = GetED(id);
            if (ed == null)
                return Content(HttpStatusCode.NotFound, Message.ED_NOT_FOUND);

            var user = GetUser();

            var observation_chart = ed.ObservationChart;
            if (observation_chart == null || observation_chart.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.ED_OC0_NOT_FOUND);

            var observation_chart_datas = observation_chart.ObservationChartDatas.Where(e=> !e.IsDeleted).ToList();
            foreach (var item in request["Datas"])
            {
                if (string.IsNullOrEmpty(item["Id"]?.ToString()))
                {
                    CreateObservationChartData(observation_chart.Id, item);
                }
                else
                {
                    EDObservationChartData observation_data = observation_chart_datas.FirstOrDefault(oc => oc.Id == new Guid(item["Id"]?.ToString()));
                    if (observation_data != null && observation_data.CreatedBy == user.Username)
                        UpdateObservationChartData(observation_data, item);
                }
            }
            unitOfWork.Commit();
            return Content(HttpStatusCode.OK, Message.SUCCESS);
        }

        private void CreateObservationChartData(Guid observation_chart_id, dynamic item)
        {
            EDObservationChartData new_observation_data = new EDObservationChartData
            {
                ObservationChartId = observation_chart_id,
                NoteAt = DateTime.ParseExact(item["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null),
                SysBP = item["SysBP"]?.ToString(),
                DiaBP = item["DiaBP"]?.ToString(),
                Pulse = item["Pulse"]?.ToString(),
                Temperature = item["Temperature"]?.ToString(),
                Resp = item["Resp"]?.ToString(),
                SpO2 = item["SpO2"]?.ToString(),
                RestPainScore = item["RestPainScore"]?.ToString(),
                MovePainScore = item["MovePainScore"]?.ToString(),
                Other = item["Other"]?.ToString()
            };
            unitOfWork.EDObservationChartDataRepository.Add(new_observation_data);
        }

        private void UpdateObservationChartData(EDObservationChartData observation_data, dynamic item)
        {
            observation_data.NoteAt = DateTime.ParseExact(item["NoteAt"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
            observation_data.SysBP = item["SysBP"]?.ToString();
            observation_data.DiaBP = item["DiaBP"]?.ToString();
            observation_data.Pulse = item["Pulse"]?.ToString();
            observation_data.Temperature = item["Temperature"]?.ToString();
            observation_data.Resp = item["Resp"]?.ToString();
            observation_data.SpO2 = item["SpO2"]?.ToString();
            observation_data.RestPainScore = item["RestPainScore"]?.ToString();
            observation_data.MovePainScore = item["MovePainScore"]?.ToString();
            observation_data.Other = item["Other"]?.ToString();
            unitOfWork.EDObservationChartDataRepository.Update(observation_data);
        }
    }
}
