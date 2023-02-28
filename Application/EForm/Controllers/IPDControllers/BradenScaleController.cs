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
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class BradenScaleController : BaseIPDApiController
    {
        private readonly string formCode = "A02_056_050919_VE";

        /// <summary>
        /// Get BradenScale by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/IPD/BradenScale/GetById/{visitId}/{id}")]
        [Permission(Code = "IPDBRADEN_VW")]
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
        /// <param name="request">BradenScale object</param>
        /// <returns>List BradenScale</returns>
        [HttpPost]
        [Route("api/IPD/BradenScale/GetByVisitId/{visitId}")]
        [Permission(Code = "IPDBRADEN_VW")]
        public IHttpActionResult GetByVisitId(Guid visitId, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            bool formStatus = IPDIsBlock(ipd, Constant.IPDFormCode.BradenDanhGiaNguyCoLoet);

            BradenScale bradenScale = new BradenScale();
            bradenScale.VisitId = visitId;
            bradenScale.CreatedBy = request["CreatedBy"].ToString();
            bradenScale.PageIndex = (int)request["PageIndex"];
            bradenScale.PageSize = (int)request["PageSize"];
            bradenScale.DateFrom = request["DateFrom"]?.ToString() == "" ? string.Empty
                                                                        : DateTime.ParseExact(request["DateFrom"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null).ToString(Constant.DATETIME_SQL);
            bradenScale.DateTo = request["DateTo"]?.ToString() == "" ? string.Empty
                                                                    : DateTime.ParseExact(request["DateTo"]?.ToString(), Constant.TIME_DATE_FORMAT_WITHOUT_SECOND, null).ToString(Constant.DATETIME_SQL);

            var lstMews = BradenScaleManager.GetByVisitId(bradenScale);

            return Content(HttpStatusCode.OK, BradenScaleGridViewOutput(lstMews, formStatus));
        }

        private dynamic BradenScaleGridViewOutput(DataSet ds, bool formStatus)
        {
            List<BradenScale> lstVitalSign = new List<BradenScale>();
            if (ds != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {

                    BradenScale entity = new BradenScale();
                    entity.RowNum = int.Parse(item["RowNum"].ToString());
                    entity.Id = (Guid)item["Id"];
                    entity.VisitId = (Guid?)item["VISIT_ID"];
                    entity.TotalScore = item["TotalScore"] == DBNull.Value || item["TotalScore"].ToString() == string.Empty ? "N/A" : item["TotalScore"].ToString();
                    entity.Intervention = GetInventionContent(item["Intervention"].ToString());
                    entity.Classify = GetClassifyContent(item["TotalScore"]);
                    entity.CreatedBy = item["CreatedBy"].ToString();
                    entity.CreatedAt = (DateTime?)item["CreatedAt"];
                    entity.TransactionDate = (DateTime)item["TransactionDate"];
                    entity.TransDate = string.Format("{0:HH:mm dd/MM/yyyy}", item["TransactionDate"]);

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

        public string GetClassifyContent(object totalScore)
        {
            int scoreValue = totalScore == DBNull.Value || totalScore.ToString() == string.Empty ? -1 : Convert.ToInt32(totalScore);
            
            BradenScaleContent entity = new BradenScaleContent();
            if (scoreValue >= 19 && scoreValue <= 23)
            {
                entity.ViName = "Không có nguy cơ";
                entity.EnName = "No risk";
            }

            if (scoreValue >= 15 && scoreValue <= 18)
            {
                entity.ViName = "Nguy cơ thấp";
                entity.EnName = "At risk";
            }

            if (scoreValue >= 13 && scoreValue <= 14)
            {
                entity.ViName = "Nguy cơ trung bình";
                entity.EnName = "Moderate risk";
            }

            if (scoreValue >= 10 && scoreValue <= 12)
            {
                entity.ViName = "Nguy cơ cao";
                entity.EnName = "High risk";
            }

            if (scoreValue >= 0 && scoreValue <= 9)
            {
                entity.ViName = "Nguy cơ rất cao";
                entity.EnName = "Very high risk";
            }

            if (scoreValue < 0)
            {
                entity.ViName = string.Empty;
                entity.EnName = string.Empty;
            }

            return JsonConvert.SerializeObject(entity);
        }

        private string GetInventionContent(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                List<BradenScaleContent> lst = new List<BradenScaleContent>();
                var result = content.Split(';');
                foreach (var rs in result)
                {
                    BradenScaleContent bradenScaleInvention = new BradenScaleContent();
                    var strValue = rs.Split('-');
                    if (strValue[0] == "IPDBRADEN45" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Thường xuyên thay đổi tư thế";
                        bradenScaleInvention.EnName = "Frequent turning";
                    }

                    if (strValue[0] == "IPDBRADEN46" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Thời gian giữa các lần thay đổi tư thế người bệnh không quá 4 giờ";
                        bradenScaleInvention.EnName = "Turning scheduled at least once every 4 hours";
                    }

                    if (strValue[0] == "IPDBRADEN47" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Thời gian giữa các lần thay đổi tư thế người bệnh không quá 2 giờ";
                        bradenScaleInvention.EnName = "Turning scheduled at least once every 2 hours";
                    }

                    if (strValue[0] == "IPDBRADEN48" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Tăng cường vận động";
                        bradenScaleInvention.EnName = "Maximal remobilization";
                    }

                    if (strValue[0] == "IPDBRADEN49" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Kiểm soát độ ẩm da";
                        bradenScaleInvention.EnName = "Manage moisture";
                    }

                    if (strValue[0] == "IPDBRADEN50" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Kiểm soát sự cọ xát & tổn thương dạng vết cắt";
                        bradenScaleInvention.EnName = "Manage friction and shear";
                    }

                    if (strValue[0] == "IPDBRADEN51" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Kiểm soát dinh dưỡng";
                        bradenScaleInvention.EnName = "Manage nutrition";
                    }

                    if (strValue[0] == "IPDBRADEN52" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Lưu ý các vấn đề chăm sóc cơ bản khác";
                        bradenScaleInvention.EnName = "Take note of other general care issues";
                    }

                    if (strValue[0] == "IPDBRADEN53" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Sử dụng phương tiện hỗ trợ tư thế và hạn chế bề mặt tiếp xúc";
                        bradenScaleInvention.EnName = "Use posture-supporting or surface pressure reduction tools/equipments";
                    }

                    if (strValue[0] == "IPDBRADEN54" && strValue[1] == "True")
                    {
                        bradenScaleInvention.ViName = "Áp dụng các biện pháp giảm bề mặt đè ép nếu tình trạng người bệnh không cho phép lăn trở";
                        bradenScaleInvention.EnName = "Apply surface pressure relieving method if patient’s condition does not allow frequent turning.";
                    }
                    lst.Add(bradenScaleInvention);
                }
                return JsonConvert.SerializeObject(lst);
            }
            return string.Empty;
        }


        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/BradenScale/Insert/{visitId}")]
        [Permission(Code = "IPDBRADEN_INS")]
        public IHttpActionResult Insert(Guid visitId, [FromBody] JObject request)
        {
            //TODO: Check IPD existed
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            //TODO: Create draft data
            var formData = new BradenScale();
            formData.VisitId = visitId;
            if (string.IsNullOrEmpty(request["Id"].ToString()))
            {
                formData.Id = Guid.NewGuid();
                formData.TransactionDate = DateTime.Today;
                unitOfWork.IPDBradenScaleRepository.Add(formData);

                var form = unitOfWork.IPDBradenScaleRepository.GetById(formData.Id);
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
                var form = unitOfWork.IPDBradenScaleRepository.GetById((Guid)request["Id"]);
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
                unitOfWork.IPDBradenScaleRepository.Update(form);
                UpdateVisit(visit, "IPD");
            }

            return Content(HttpStatusCode.OK, new { formData.Id });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/IPD/BradenScale/Delete/{id}")]
        [Permission(Code = "IPDBRADEN_DEL")]
        public IHttpActionResult Delete(Guid id)
        {
            var vitalSignInfo = BradenScaleManager.GetById(id);
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

            var rowAffected = BradenScaleManager.Update(vitalSignInfo);
            if (rowAffected > 0)
            {
                return Content(HttpStatusCode.OK, rowAffected);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, rowAffected);
            }
        }


        private BradenScale GetForm(Guid visit_id, Guid formId)
        {
            return unitOfWork.IPDBradenScaleRepository.Find(e => !e.IsDeleted && e.VisitId == visit_id && e.Id == formId)
                                                        .FirstOrDefault();
        }

        private dynamic FormatOutput(BradenScale bradenScale, IPD ipd)
        {
            return new
            {
                Datas = GetFormData((Guid)bradenScale.VisitId, bradenScale.Id, formCode),
                bradenScale.Id,
                VisitId = bradenScale.VisitId,
                TransactionDate = bradenScale.TransactionDate.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                IsLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BradenDanhGiaNguyCoLoet),
                bradenScale.CreatedBy,
                CreatedAt = bradenScale.CreatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND),
                bradenScale.UpdatedBy,
                UpdatedAt = bradenScale.UpdatedAt?.ToString(Constant.TIME_DATE_FORMAT_WITHOUT_SECOND)
            };
        }
    }
}