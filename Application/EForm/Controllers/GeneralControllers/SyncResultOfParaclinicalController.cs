using Common;
using EForm.Authentication;
using EForm.BaseControllers;
using EForm.Client;
using EForm.Common;
using EForm.Models;
using EMRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EForm.Controllers.GeneralControllers
{
    [SessionAuthorize]
    public class SyncResultOfParaclinicalController : BaseApiController
    {
        [HttpGet]
        [CSRFCheck]
        [Permission(Code = "READSROP")]
        [Route("api/{type}/SyncLabAndXrayResults/{id}")]
        public IHttpActionResult Index(Guid id, string type = "ED")
        {
            dynamic visit = GetVisit(id, type);
            if (visit == null) return Content(HttpStatusCode.NotFound, Message.VISIT_NOT_FOUND);

            var site_code = GetSiteCode();
            if (string.IsNullOrEmpty(site_code))
                return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);

            var customer = visit.Customer;
            if (customer == null || customer.IsDeleted)
                return Content(HttpStatusCode.NotFound, Message.OPD_NOT_FOUND);
            else if (customer.PID == null)
                return Content(HttpStatusCode.NotFound, Message.PID_IS_MISSING);

            dynamic lab_result;
            dynamic xray_result;
            if (site_code == "times_city")
            {
                lab_result = EHosClient.GetLabResults(customer.PID);
                xray_result = EHosClient.GetXrayResults(customer.PID);
            }
            else
            {
                var api_code = GetSiteAPICode();
                lab_result = visit.IsEhos == true ? EHosClient.GetLabResults(customer.PIDEhos) : OHClient.GetLabResults(customer.PID, api_code);
                xray_result = visit.IsEhos == true ? EHosClient.GetXrayResults(customer.PIDEhos) : OHClient.GetXrayResultsByPID(customer.PID);
            }
            return Content(HttpStatusCode.OK, new
            {
                XetNghiem = lab_result,
                CDHA = xray_result,
                DiagnosticReporting = GroupDiagnosticReportingByPid(customer.Id)
            });
        }
        [HttpGet]
        [CSRFCheck]
        [Permission(Code = "READSROP")]
        [Route("api/SyncLabAndXrayResultsByPid/{pid}")]
        public IHttpActionResult SyncLabAndXrayResultsByPid(string pid)
        {
            if (string.IsNullOrEmpty(pid)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var customer = GetCustomerByPid(pid);

            if (customer == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);
            if (customer.IsVip)
            {
                if (!IsUnlockVipByPid(pid, UnlockVipType.LabAndXrayResults) && !HasEFORMViSitOpen(pid))
                {
                    return Content(HttpStatusCode.Forbidden, new MsgModel()
                    {
                        ViMessage = "Bạn không có quyền truy cập hồ sơ này",
                        EnMessage = "Bạn không có quyền truy cập hồ sơ này"
                    });
                }
            }
            var api_code = GetSiteAPICode();

            dynamic lab_result = OHClient.GetLabResults(pid, api_code);
            dynamic xray_result = OHClient.GetXrayResultsByPID(pid);
            return Content(HttpStatusCode.OK, new
            {
                XetNghiem = lab_result,
                CDHA = xray_result,
                DiagnosticReporting = GroupDiagnosticReportingByPid(customer.Id),
                Sites = unitOfWork.SiteRepository.Find(e => !e.IsDeleted && e.ApiCode != null && e.ApiCode != "TEST").ToList().Select(e => new SiteInfo()
                {
                    Id = e.Id,
                    ViName = e.ViName,
                    EnName = e.EnName,
                    ApiCode = e.ApiCode,
                    Code = e.Code,
                    Name = e.Name
                })
            });
        }
        [HttpGet]
        [CSRFCheck]
        [Permission(Code = "READSROP")]
        [Route("api/SyncLabByPid/{pid}/{site_code}")]
        public IHttpActionResult SyncLabResultsByPid(string pid, string site_code)
        {
            if (string.IsNullOrEmpty(pid)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var customer = GetCustomerByPid(pid);

            if (customer == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);
            if (customer.IsVip)
            {
                if (!IsUnlockVipByPid(pid, UnlockVipType.LabAndXrayResults) && !HasEFORMViSitOpen(pid))
                {
                    return Content(HttpStatusCode.Forbidden, new MsgModel()
                    {
                        ViMessage = "Bạn không có quyền truy cập hồ sơ này",
                        EnMessage = "Bạn không có quyền truy cập hồ sơ này"
                    });
                }
            }
            var api_code = string.IsNullOrWhiteSpace(site_code) ? GetSiteAPICode() : site_code;

            dynamic lab_result = OHClient.GetLabResults(pid, api_code);
            return Content(HttpStatusCode.OK, new
            {
                XetNghiem = lab_result
            });
        }


        [HttpGet]
        [CSRFCheck]
        [Permission(Code = "READSROP")]
        [Route("api/GetLISInforByPID/{pid}")]
        public IHttpActionResult GetLabAndXrayResultsByPid(string pid)
        {
            if (string.IsNullOrEmpty(pid)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var customer = GetCustomerByPid(pid);

            if (customer == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);
            if (customer.IsVip)
            {
                if (!IsUnlockVipByPid(pid, UnlockVipType.LabAndXrayResults) && !HasEFORMViSitOpen(pid))
                {
                    return Content(HttpStatusCode.Forbidden, new MsgModel()
                    {
                        ViMessage = "Bạn không có quyền truy cập hồ sơ này",
                        EnMessage = "Bạn không có quyền truy cập hồ sơ này"
                    });
                }
            }
            var results = OHClient.getLISInforByPID(pid, null, null);
            results = results.OrderBy(e => e.ServiceDate).ToList();
            return Content(HttpStatusCode.OK, results);
        }
        [HttpGet]
        [Permission(Code = "READSROP")]
        [Route("api/GetLISResultByPID/{pid}/{sid}/{sitecode}")]
        public IHttpActionResult GetLISResultByPID(string pid, string sid, string sitecode, [FromUri] ParamModel request)
        {
            if (string.IsNullOrEmpty(pid)) return Content(HttpStatusCode.BadRequest, Message.FORMAT_INVALID);
            var customer = GetCustomerByPid(pid);
            if (customer == null) return Content(HttpStatusCode.BadRequest, Message.CUSTOMER_NOT_FOUND);
            if (customer.IsVip)
            {
                if (!IsUnlockVipByPid(pid, UnlockVipType.LabAndXrayResults) && !HasEFORMViSitOpen(pid))
                {
                    return Content(HttpStatusCode.Forbidden, new MsgModel()
                    {
                        ViMessage = "Bạn không có quyền truy cập hồ sơ này",
                        EnMessage = "Bạn không có quyền truy cập hồ sơ này"
                    });
                }
            }
            var lab_result = OHClient.GetLabResultsV2(pid, sitecode);
            var results = OHClient.getLISInforByPID(pid, null, null);
            results = results.OrderByDescending(e => e.ServiceDate).ToList();            

            foreach (var lab in lab_result)
            {
                var sisd = lab.SID?.Trim();
                if (lab.SID.Contains('-'))
                {
                    var sub_sid = sisd.Split('-')[1];
                    lab.Sub_SID = sub_sid;
                    if (lab.ItemCode != null)
                    {
                        lab.itemCode = lab.ItemCode.ToString();
                    }
                    else
                    {
                        lab.itemCode = "";
                    }
                }
                else
                {
                    lab.Sub_SID = lab.SID;
                }
                
            }
            foreach (var result in results)
            {
                if (!String.IsNullOrEmpty(result.VisitCode) && !String.IsNullOrEmpty(result.SID))
                {
                    foreach (var item in lab_result)
                    {
                        if (item.Sub_SID == result.SID.Trim() && result.ItemCode.Trim() == item.itemCode.Trim())
                        {
                            item.VisitCode = result.VisitCode;
                        }
                    }

                }
            }
            var lab_result_cols = lab_result.Where(e => e.ResultAt != null && e.itemCode.Contains(request.itemcode)).ToList();           
            var items = lab_result.Where(e => e.ResultAt != null).ToList();
            //lab_result = lab_result.Where(e => e.Sub_SID == sid).ToList();

            var rows = lab_result_cols.OrderByDescending(e => e.TestCode).ToList();

            var cols = lab_result_cols.OrderBy(e => e.ResultAt).Select(e => new LISResultCols()
            {
                Id = e.ResultAt.Value.ToString(Constants.DATE_FORMAT),
                Title = e.ResultAt.Value.ToString(Constants.DATE_FORMAT),
                ResultAt = e.ResultAt.Value,
                SID = e.SID,
                VisitCode = e.VisitCode
            }).GroupBy(e => new { e.Id, e.VisitCode }).Select(e => e.FirstOrDefault()).ToList();
            foreach (var col in cols)
            {
                if (col.SID == null) continue;
                col.Item = items.Where(e => e.SID.Contains(col.SID)).FirstOrDefault();
            }
            List<dynamic> lis_results = new List<dynamic>();
            foreach (ResultModel item in rows)
            {
                var result = getCols(item, cols, lab_result_cols);
                List<dynamic> result_list = new List<dynamic>();
                if (result.Count > 0)
                {
                    foreach (var i in result)
                    {
                        if (!i.Equals(new { })) result_list.Add(i.Result);
                    }
                }
                lis_results.Add(new
                {
                    ViName = item.TestNameV,
                    EnName = item.TestNameE,
                    Code = item.TestCode,
                    Datas = result,
                    SID = item.Sub_SID,
                    ResultList = result_list,
                });
            }
            lis_results = lis_results.Where(e => e.SID == sid).ToList();
            return Content(HttpStatusCode.OK, new
            {
                results = lis_results,
                cols
            });
        }

        private List<dynamic> getCols(ResultModel labItem, List<LISResultCols> cols, List<ResultModel> allData)
        {
            List<dynamic> lis_results = new List<dynamic>();
            foreach (var col in cols)
            {
                allData = allData.Where(e => e.ResultAt != null).ToList();
                var data = allData.FirstOrDefault(e => e.TestCode == labItem.TestCode && col.Id == e.ResultAt.Value.ToString(Constants.DATE_FORMAT));
                if (data == null)
                {
                    if (col.ResultAt != null)
                    {
                        lis_results.Add(new ResultModel { ResultAt = col.ResultAt });
                    }
                    else
                    {
                        lis_results.Add(new ResultModel { });
                    }
                }
                else
                {
                    lis_results.Add(data);
                }
            }
            return lis_results;
        }
    }
}