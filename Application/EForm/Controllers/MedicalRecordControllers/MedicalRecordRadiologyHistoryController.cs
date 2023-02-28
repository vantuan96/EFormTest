using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace EForm.Controllers.MedicalRecordControllers
{
    [SessionAuthorize]
    public class MedicalRecordRadiologyHistoryController : BaseApiController
    {
        [HttpGet]
        [Route("api/RadiologyHistory")]
        [Permission(Code = "VIEWRADIOLOGY")]
        public IHttpActionResult RadiologyHistory([FromUri] RadiologyParamester param)
        {
            if (string.IsNullOrEmpty(param.Assessor))
                return Content(HttpStatusCode.BadRequest, new { ViName = "", EnName = "PID is null or empty!" });

            var resultFromOh = OHClient.GetRadiologyFromOH(param.Assessor);
            var result = RemoveServiceNotRadiology(resultFromOh);

            if (!string.IsNullOrEmpty(param.FormCode))
                result = (from r in result where r.VisitCode == param.FormCode select r).ToList();

            if (!string.IsNullOrEmpty(param.FromDate))
            {
                var startDate = DateTime.ParseExact(param.FromDate, "HH:mm dd/MM/yyyy", null);
                result = (from r in result where r.ServiceDate >= startDate select r).ToList();
            }

            if (!string.IsNullOrEmpty(param.ToDate))
            {
                var endDate = DateTime.ParseExact(param.ToDate, "HH:mm dd/MM/yyyy", null);
                result = (from r in result where r.ServiceDate <= endDate select r).ToList();
            }

            var responsive = result.OrderByDescending(e => e.ServiceDate).ToList();

            return Content(HttpStatusCode.OK, responsive);
        }

        [HttpPost]
        [Route("api/RadiologyHistory/ViewReport")]
        [Permission(Code = "VIEWDETAILTRADIOLOGY")]
        public IHttpActionResult GetDetailRadiology([FromBody] JObject req)
        {
            string userNameParam = req["UserName"]?.ToString();
            string reportIdParam = req["ReportId"]?.ToString();
            if (string.IsNullOrEmpty(reportIdParam) || string.IsNullOrEmpty(userNameParam))
                return Content(HttpStatusCode.BadRequest, "Các tham số không được null hay rỗng");

            OHRadiologyServiceReport clien = new OHRadiologyServiceReport();

            var res = clien.ExeRequet(reportIdParam, userNameParam);

            string valueConten = res.Value;
            if (res.Key == HttpStatusCode.OK)
                valueConten = SubStringHtml(res.Value);

            return Content(res.Key, valueConten );
        }

        private List<RadiologyResult> RemoveServiceNotRadiology(List<RadiologyResult> listItem)
        {
            var code_IsDiagnosticReporting = (from cls in unitOfWork.ServiceRepository.AsQueryable()
                                              where !cls.IsDeleted && cls.IsDiagnosticReporting == true
                                              select cls.Code).ToList();
            var result = (from e in listItem
                          where !code_IsDiagnosticReporting.Contains(e.ItemCode)
                          select e).ToList();

            return result;
        }

        private string SubStringHtml(string conten)
        {
            if(!string.IsNullOrEmpty(conten))
            {
                XNamespace a = "http://www.orionhealth.com/his/integration";
                XNamespace b = "http://www.w3.org/TR/xhtml1/strict";

                XDocument xmlResponse;
                try
                {
                    xmlResponse = XDocument.Parse(conten);
                }
                catch(Exception e)
                {
                    xmlResponse = new XDocument();
                }

                var element = xmlResponse.Descendants(a + "GetByReportIdResponse");
                var node1 = element.FirstOrDefault();
                var node2 = node1?.Descendants(a + "GetByReportIdResult").FirstOrDefault();
                var node3 = node2?.Element(a + "Document");
                var result = node3?.Element(b + "html");
                StringBuilder buil = new StringBuilder();
                buil.Append(result?.ToString());

                return buil.ToString();
            }
            return "";
        }
    }
}
