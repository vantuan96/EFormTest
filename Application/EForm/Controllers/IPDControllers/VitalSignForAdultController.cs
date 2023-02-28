using Bussiness.IPD;
using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class VitalSignForAdultController : BaseIPDApiController
    {
        private readonly string formCode = "A02_031_220321_V";

        /// <summary>
        /// Get vitalsign by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("api/IPD/VitalSignAdult/GetById/{visitId}/{id}")]
        [Permission(Code = "MEWS01")]
        public IHttpActionResult GetById(Guid visitId, Guid id)
        {
            //TODO: Check IPD existed
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = GetForm(visitId, id);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }

            return Content(HttpStatusCode.OK, FormatOutput(form, visit));
        }

        /// <summary>
        /// Get list VitalSign by visitId
        /// </summary>
        /// <param name="visitId">visitId</param>
        /// <param name="request">VitalSignForAdult object</param>
        /// <returns>List VitalSignForAdult</returns>
        [HttpPost]
        [Route("api/IPD/VitalSignAdult/GetByVisitId/{visitId}")]
        [Permission(Code = "MEWS01")]
        public IHttpActionResult GetByVisitId(Guid visitId, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            bool formStatus = IPDIsBlock(ipd, Constant.IPDFormCode.BangDanhGiaSinhTonNguoiLon);

            try
            {
                VitalSignForAdult vitalSign = new VitalSignForAdult();
                vitalSign.VisitId = visitId;
                vitalSign.CreatedBy = request["CreatedBy"].ToString();
                vitalSign.PageIndex = (int)request["PageIndex"];
                vitalSign.PageSize = (int)request["PageSize"];
                vitalSign.DateFrom = request["DateFrom"]?.ToString() == "" ? string.Empty
                                                                            : DateTime.ParseExact(request["DateFrom"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null).ToString(Constant.DATETIME_SQL);
                vitalSign.DateTo = request["DateTo"]?.ToString() == "" ? string.Empty
                                                                        : DateTime.ParseExact(request["DateTo"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null).ToString(Constant.DATETIME_SQL);

                var lstMews = VitalSignForAdultManager.GetByVisitId(vitalSign);
                return Content(HttpStatusCode.OK, VitalSignForAdultGridViewOutput(lstMews, formStatus));
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError, Message.INTERAL_SERVER_ERROR);
            }
        }
        private dynamic VitalSignForAdultGridViewOutput(DataSet ds, bool formStatus)
        {
            List<VitalSignForAdult> lstVitalSign = new List<VitalSignForAdult>();
            if (ds != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    VitalSignForAdult entity = new VitalSignForAdult();
                    entity.RowNum = int.Parse(item["RowNum"].ToString());
                    entity.Id = (Guid)item["Id"];
                    entity.VisitId = (Guid?)item["VISIT_ID"];
                    entity.BreathRate = item["BreathRate"] == DBNull.Value || item["BreathRate"].ToString() == string.Empty ? 0 : decimal.Parse(item["BreathRate"].ToString());
                    entity.SPO2 = item["SPO2"] == DBNull.Value || item["SPO2"].ToString() == string.Empty ? 0 : decimal.Parse(item["SPO2"].ToString());
                    entity.LowBP = item["LowBP"] == DBNull.Value || item["LowBP"].ToString() == string.Empty ? 0 : decimal.Parse(item["LowBP"].ToString());
                    entity.HighBP = item["HighBP"] == DBNull.Value || item["HighBP"].ToString() == string.Empty ? 0 : decimal.Parse(item["HighBP"].ToString());
                    entity.Pulse = item["Pulse"] == DBNull.Value || item["Pulse"].ToString() == string.Empty ? 0 : decimal.Parse(item["Pulse"].ToString());
                    entity.Temperature = item["Temperature"] == DBNull.Value || item["Temperature"].ToString() == string.Empty ? 0 : decimal.Parse(item["Temperature"].ToString());
                    entity.Sense = item["Sense"].ToString();
                    entity.RespiratorySupport = item["RespiratorySuport"].ToString();
                    entity.PainScore = item["PainScore"].ToString();
                    entity.CapillaryBlood = item["CapillaryBlood"].ToString();
                    entity.FluidIn = item["FluidIn"].ToString();
                    entity.TotalFluidIn = item["TotalFluidIn"].ToString();
                    entity.FluidOut = item["FluidOut"].ToString();
                    entity.TotalFluidOut = item["TotalFluidOut"].ToString();
                    entity.TotalBilan = item["TotalBilan"].ToString();
                    entity.VIPScore = item["VIPScore"].ToString();
                    entity.TotalMews = item["TotalMews"].ToString();
                    entity.CreatedBy = item["CreatedBy"].ToString();
                    entity.CreatedAt = (DateTime?)item["CreatedAt"];
                    entity.TransactionDate = (DateTime)item["TransactionDate"];
                    entity.TransDate = string.Format("{0:HH:mm dd/MM/yyyy}", item["TransactionDate"]);

                    List<VitalSignForAdultContent> lstContent = new List<VitalSignForAdultContent>
                        {
                            new VitalSignForAdultContent {
                                                            ViName = string.Format("Nhịp thở: {0} (lần/phút), SpO2: {1} (%), Huyết áp: Tối thiểu {2} - Tối đa {3} (mmHg), Mạch: {4} (Nhịp/phút), Thân nhiệt: {5} (oC), Tri giác: {6}, Hỗ trợ hô hấp: {7}",
                                                                                   item["BreathRate"] == DBNull.Value || item["BreathRate"].ToString() == string.Empty ? string.Empty : entity.BreathRate.ToString(),
                                                                                   item["SPO2"] == DBNull.Value || item["SPO2"].ToString() == string.Empty ? string.Empty : entity.SPO2.ToString(),
                                                                                   item["LowBP"] == DBNull.Value || item["LowBP"].ToString() == string.Empty ? string.Empty: entity.LowBP.ToString(),
                                                                                   item["HighBP"] == DBNull.Value || item["HighBP"].ToString() == string.Empty? string.Empty: entity.HighBP.ToString(),
                                                                                   item["Pulse"] == DBNull.Value || item["Pulse"].ToString() == string.Empty ? string.Empty :  entity.Pulse.ToString(),
                                                                                   item["Temperature"] == DBNull.Value || item["Temperature"].ToString() == string.Empty  ?string.Empty : entity.Temperature.ToString(),
                                                                                   entity.Sense,
                                                                                   entity.RespiratorySupport),
                                                           EnName = string.Format("Breath: {0} (per minute), SpO2: {1} (%), Blood pressure: Low {2} - high {3} (mmHg), Pulse: {4} (per minute), Temperature: {5} (oC), Sense: {6}, Respiratory support: {7}",
                                                                                   item["BreathRate"] == DBNull.Value || item["BreathRate"].ToString() == string.Empty ? string.Empty : entity.BreathRate.ToString(),
                                                                                   item["SPO2"] == DBNull.Value || item["SPO2"].ToString() == string.Empty ? string.Empty : entity.SPO2.ToString(),
                                                                                   item["LowBP"] == DBNull.Value || item["LowBP"].ToString() == string.Empty ? string.Empty: entity.LowBP.ToString(),
                                                                                   item["HighBP"] == DBNull.Value || item["HighBP"].ToString() == string.Empty? string.Empty: entity.HighBP.ToString(),
                                                                                   item["Pulse"] == DBNull.Value || item["Pulse"].ToString() == string.Empty ? string.Empty :  entity.Pulse.ToString(),
                                                                                   item["Temperature"] == DBNull.Value || item["Temperature"].ToString() == string.Empty  ?string.Empty : entity.Temperature.ToString(),
                                                                                   entity.Sense,
                                                                                   entity.RespiratorySupport)
                                                        },

                            new VitalSignForAdultContent { ViName = string.Format("Điểm đau: {0}. Đường máu mao mạch: {1}",entity.PainScore, entity.CapillaryBlood),
                                                            EnName = string.Format("Pain score: {0}. Capillary blood: {1}",entity.PainScore, entity.CapillaryBlood) },

                            new VitalSignForAdultContent { ViName = string.Format("Đánh giá vein truyền (VIP Score): {0}({1})",entity.VIPScore, entity.VIPScore_Vi),
                                                            EnName = string.Format("Vip Score: {0}({1})",entity.VIPScore, entity.VIPScore_En) },

                            new VitalSignForAdultContent {
                                                            ViName = string.Format("Số lượng dịch vào:{0}. Tổng dịch vào: {1} ml",
                                                                                    entity.FluidIn,
                                                                                    entity.TotalFluidIn == "0" ? string.Empty : entity.TotalFluidIn),
                                                            EnName = string.Format("Fluid in: {0}. Total fluid in: {1} ml",
                                                                                    entity.FluidIn,
                                                                                    entity.TotalFluidIn == "0" ? string.Empty : entity.TotalFluidIn)
                                                          },

                            new VitalSignForAdultContent {
                                                            ViName = string.Format("Số lượng dịch ra: {0}. Tổng dịch ra: {1} ml",
                                                                                    entity.FluidOut,
                                                                                    entity.TotalFluidOut == "0" ? string.Empty : entity.TotalFluidOut),
                                                            EnName = string.Format("Fluid out: {0}. Total fluid out: {1} ml",
                                                                                    entity.FluidOut,
                                                                                    entity.TotalFluidOut == "0" ? string.Empty : entity.TotalFluidOut) },

                            new VitalSignForAdultContent { ViName = string.Format("Bilan dịch: {0} ml.",entity.TotalBilan), EnName = string.Format("Bilan: {0} ml.",entity.TotalBilan) }
                        };
                    entity.Content = JsonConvert.SerializeObject(lstContent);

                    lstVitalSign.Add(entity);
                }
            }

            return new
            {
                Data = lstVitalSign,
                TotalRow = ds.Tables[1] != null ? (int)ds.Tables[1].Rows[0]["TotalRow"] : 0,
                RowExisted = ds.Tables[2] != null ? (int)ds.Tables[2].Rows[0]["RowExisted"] : 0,
                IsLocked = formStatus
            };
        }


        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/VitalSignAdult/Insert/{visitId}")]
        [Permission(Code = "MEWS02")]
        public IHttpActionResult Insert(Guid visitId, [FromBody] JObject request)
        {
            //TODO: Check IPD existed
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            //TODO: Create draft data
            var formData = new VitalSignForAdult();
            formData.VisitId = visitId;
            if (string.IsNullOrEmpty(request["Id"].ToString()))
            {
                formData.Id = Guid.NewGuid();
                formData.TransactionDate = DateTime.Today;
                unitOfWork.IPDVitalSignForAdultRespository.Add(formData);

                var form = unitOfWork.IPDVitalSignForAdultRespository.GetById(formData.Id);
                if (form == null)
                {
                    return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
                }
                //TODO: Update form
                HandleUpdateOrCreateFormDatas((Guid)formData.VisitId, formData.Id, formCode, request["Datas"]);
                if (request["TransactionDate"]?.ToString() != "" && request["TransactionDate"]?.ToString() != null)
                {
                    form.TransactionDate = DateTime.ParseExact(request["TransactionDate"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                }
                UpdateVisit(visit, "IPD");
            }
            else
            {
                var form = unitOfWork.IPDVitalSignForAdultRespository.GetById((Guid)request["Id"]);
                if (form == null)
                {
                    return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
                }
                formData.Id = (Guid)request["Id"];
                //TODO: Update form
                HandleUpdateOrCreateFormDatas((Guid)formData.VisitId, formData.Id, formCode, request["Datas"]);
                if (request["TransactionDate"]?.ToString() != "" && request["TransactionDate"]?.ToString() != null)
                {
                    form.TransactionDate = DateTime.ParseExact(request["TransactionDate"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null);
                }
                UpdateVisit(visit, "IPD");
            }

            return Content(HttpStatusCode.OK, new { formData.Id });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/VitalSignAdult/Delete/{id}")]
        [Permission(Code = "MEWS02")]
        public IHttpActionResult DeleteVitalSignForAdult(Guid id)
        {
            var vitalSignInfo = VitalSignForAdultManager.GetById(id);
            if (vitalSignInfo == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_MEWS_NOT_FOUND);
            }

            var user = GetUser();
            if (user != null && vitalSignInfo.CreatedBy != user.Username)
            {
                return Content(HttpStatusCode.Forbidden, Message.OWNER_FORBIDDEN);
            }

            vitalSignInfo.IsDeleted = true;
            vitalSignInfo.DeletedBy = user.Username;
            vitalSignInfo.DeletedAt = DateTime.Now;

            var rowAffected = VitalSignForAdultManager.Update(vitalSignInfo);
            if (rowAffected > 0)
            {
                return Content(HttpStatusCode.OK, rowAffected);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, rowAffected);
            }
        }


        /// <summary>
        /// Get list VitalSign by PID
        /// </summary>
        /// <param name="visitId">PID</param>
        /// <param name="request">VitalSignForAdult object</param>
        /// <returns>List VitalSignForAdult</returns>
        [HttpPost]
        [Route("api/IPD/VitalSignAdult/MewsChart/{visitId}")]
        [Permission(Code = "MEWS01")]
        public IHttpActionResult GetMewsChart(Guid visitId, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            VitalSignForAdult vitalSign = new VitalSignForAdult();
            vitalSign.VisitId = visitId;
            vitalSign.CreatedBy = request["CreatedBy"].ToString();
            vitalSign.PageIndex = 1;
            vitalSign.PageSize = 1000;
            vitalSign.DateFrom = request["DateFrom"]?.ToString() == "" ? string.Empty
                                                                        : DateTime.ParseExact(request["DateFrom"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null).ToString(Constant.DATETIME_SQL);
            vitalSign.DateTo = request["DateTo"]?.ToString() == "" ? string.Empty
                                                                    : DateTime.ParseExact(request["DateTo"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null).ToString(Constant.DATETIME_SQL);

            var lstChartData = VitalSignForAdultManager.GetChartData(vitalSign);

            return Content(HttpStatusCode.OK, lstChartData);
        }

        private VitalSignForAdult GetForm(Guid visit_id, Guid formId)
        {
            return unitOfWork.IPDVitalSignForAdultRespository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.Id == formId).FirstOrDefault();
        }

        private dynamic FormatOutput(VitalSignForAdult vitalSign, IPD ipd)
        {
            return new
            {
                vitalSign.Id,
                TransactionDate = vitalSign.TransactionDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                Datas = GetFormData((Guid)vitalSign.VisitId, vitalSign.Id, formCode),
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangDanhGiaSinhTonNguoiLon),
                vitalSign.CreatedBy,
                CreatedAt = vitalSign.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                VisitId = vitalSign.VisitId,
                UpdatedAt = vitalSign.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
            };
        }
    }
}