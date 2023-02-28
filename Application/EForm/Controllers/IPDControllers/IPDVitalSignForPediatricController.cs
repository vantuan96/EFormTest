using DataAccess.Models.IPDModel;
using EForm.Authentication;
using EForm.Common;
using EForm.Controllers.BaseControllers;
using EForm.Models;
using EForm.Models.IPDModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDVitalSignForPediatricController : BaseIPDApiController
    {
        [HttpGet]
        [Route("api/VitalSignsForPediatric/All")]
        [Permission(Code = "IPDVIEW13NHI")]
        public IHttpActionResult GetAll([FromUri] IPDVitalSignForPediatricParams request)
        {
            Guid id_visit = (Guid)request.IPDId;
            IPD ipd = GetIPD(id_visit);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            return Content(HttpStatusCode.OK, BuilReTurnResult(request, ipd));
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/VitalSignsForPediatric/Create/{visitId}/{formType}")]
        [Permission(Code = "IPDEDIT13NHI")]
        public IHttpActionResult Create(Guid visitId, string formType)
        {
            IPD visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            if (IPDIsBlock(visit, formType))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var newForm = new IPDVitalSignForPediatrics
            {
                VisitId = visitId,
                TransactionDate = DateTime.Now,
                FormType = formType
            };

            unitOfWork.IPDVitalSignForPediatricsReponsitory.Add(newForm);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(visit.Id, newForm.Id, formType);
            return Content(HttpStatusCode.OK, new { newForm.Id });
        }

        [CSRFCheck]
        [HttpPost]
        [Route("api/VitalSignsForPediatric/Update/{visitId}")]
        [Permission(Code = "IPDEDIT13NHI")]
        public IHttpActionResult Update(Guid visitId, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            Guid formId = (Guid)request["Id"];
            var form = unitOfWork.IPDVitalSignForPediatricsReponsitory.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            if (IPDIsBlock(ipd, form.FormType))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            form.TransactionDate = DateTime.ParseExact(request["TransactionDate"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
            form.BloodVesselPews = request["BloodVesselPews"]?.ToString();
            form.BreathingPews = request["BreathingPews"]?.ToString();
            form.HypothermiaPews = request["HypothermiaPews"]?.ToString();
            form.TotalPews = request["TotalPews"]?.ToString();

            HandleUpdateOrCreateFormDatas(visitId, formId, form.FormType, request["Datas"]);

            unitOfWork.IPDVitalSignForPediatricsReponsitory.Update(form);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(ipd.Id, form.Id, form.FormType);

            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [HttpGet]
        [Route("api/VitalSignsForPediatric/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "IPDVIEW13NHI")]
        public IHttpActionResult GetDetail(Guid visitId, Guid formId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = unitOfWork.IPDVitalSignForPediatricsReponsitory.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
            if (form == null)
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);

            var data = GetFormData(visitId, form.Id, form.FormType);

            return Content(HttpStatusCode.OK, new
            {
                formId,
                Datas = data,
                form.CreatedBy,
                TransactionDate = form.TransactionDate?.ToString("HH:mm dd/MM/yyyy"),
                form.BloodVesselPews,
                form.BreathingPews,
                form.HypothermiaPews,
                form.TotalPews,
                IsLocked = IPDIsBlock(visit, form.FormType)
            });
        }

        [HttpGet]
        [Route("api/VitalSignsForPediatric/GetHistory/{visitId}")]
        [Permission(Code = "IPDVIEW13NHI")]
        public IHttpActionResult GetHistoryInitialAssessment(Guid visitId)
        {
            IPD ipd = GetIPD(visitId);
            if (ipd == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var forms = (from p in unitOfWork.IPDVitalSignForPediatricsReponsitory.AsQueryable()
                         where !p.IsDeleted && p.VisitId == ipd.Id
                         select p.FormType).ToList();

            return Content(HttpStatusCode.OK, forms);
        }

        private dynamic BuilReTurnResult(IPDVitalSignForPediatricParams req, IPD visit)
        {
            List<MappingQueryWithResult> forms = FillTerGetForm(req);
            int numberForm = forms.Count;
            int numberPage = numberForm % req.PageSize == 0 ? numberForm / req.PageSize : numberForm / req.PageSize + 1;
            //forms = forms.Skip((req.PageNumber - 1) * req.PageSize).Take(req.PageSize).ToList(); bỏ phân trang theo yêu cầu
            forms = OrderFormByCreate(forms);
            var lastUpdate = (from l in unitOfWork.IPDVitalSignForPediatricsReponsitory.AsQueryable()
                              where !l.IsDeleted && l.VisitId == visit.Id && l.FormType == req.FormCode
                              orderby l.UpdatedAt descending
                              select new
                              {
                                  l.UpdatedAt,
                                  l.UpdatedBy
                              }).FirstOrDefault();
            List<VitalSignsForPregnantWomanItem> listItems = new List<VitalSignsForPregnantWomanItem>();
            foreach (var item in forms)
            {
                VitalSignsForPregnantWomanItem vitalSignsForPregnantWomanItem = new VitalSignsForPregnantWomanItem();
                VitalSign vitalSign = new VitalSign();
                vitalSignsForPregnantWomanItem.Content = GetStringContent(item, ref vitalSign);
                vitalSignsForPregnantWomanItem.CreatedAt = item.CreatedAt;
                vitalSignsForPregnantWomanItem.CreatedBy = item.CreatedBy;
                vitalSignsForPregnantWomanItem.Id = item.Id;
                vitalSignsForPregnantWomanItem.MEWSTotal = item.TotalPews;
                vitalSignsForPregnantWomanItem.TransactionDate = item.TransactionDate;
                vitalSignsForPregnantWomanItem.UpdatedAt = item.UpdatedAt;
                vitalSignsForPregnantWomanItem.VisitId = item.VisitId;
                vitalSignsForPregnantWomanItem.VitalSign = vitalSign;
                listItems.Add(vitalSignsForPregnantWomanItem);
            }    
            return new
            {
                listItems,
                Count = numberForm,
                MachMEWS = forms.Select(f => new { MEWValue = f.BloodVesselPews, MEWRate = GetDataForm(f.Datas, "IPDPEWSPDT9"), f.TransactionDate, f.CreatedBy }).ToList(),
                NhipThoMEWS = forms.Select(f => new { MEWValue = f.BreathingPews, MEWRate = GetDataForm(f.Datas, "IPDPEWSPDT2"), f.TransactionDate, f.CreatedBy }).ToList(),
                ThanNhietMEWS = forms.Select(f => new { MEWValue = f.HypothermiaPews, MEWRate = GetDataForm(f.Datas, "IPDPEWSPDT11"), f.TransactionDate, f.CreatedBy }).ToList(),
                Paging = new { numberPage = numberPage, PageNumber = req.PageNumber },
                IsLocked = IPDIsBlock(visit, req.FormCode),
                LastUpdate = new { UpdatedAt = lastUpdate?.UpdatedAt?.ToString("HH:mm dd/MM/yyyy"), UpdatedBy = lastUpdate?.UpdatedBy }
            };
        }
        private List<MappingQueryWithResult> FillTerGetForm(IPDVitalSignForPediatricParams paramsmeter)
        {
            Nullable<DateTime> fromDate = new DateTime();
            Nullable<DateTime> toDate = new DateTime();
            string assessor = paramsmeter.Assessor;

            if (paramsmeter.FromDate != null)
            {
                fromDate = DateTime.ParseExact(paramsmeter.FromDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }
            else
            {
                fromDate = null;
            }

            if (paramsmeter.ToDate != null)
            {
                toDate = DateTime.ParseExact(paramsmeter.ToDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }
            else
            {
                toDate = null;
            }

            var listForm = from lf in unitOfWork.IPDVitalSignForPediatricsReponsitory.AsQueryable()
                           where !lf.IsDeleted && lf.VisitId == paramsmeter.IPDId && lf.FormType == paramsmeter.FormCode
                           select lf;

            if (fromDate != null)
            {
                listForm = from f in listForm
                           where f.TransactionDate >= fromDate
                           select f;
            }

            if (toDate != null)
            {
                listForm = from l in listForm
                           where l.TransactionDate <= toDate
                           select l;
            }

            if (assessor != null)
            {
                listForm = from l in listForm
                           where l.CreatedBy.ToUpper() == assessor.ToUpper()
                           select l;
            }

            var listForms = listForm.ToList();

            var datas = (from l in listForm
                         join d in unitOfWork.FormDatasRepository.AsQueryable()
                         on l.Id equals d.FormId
                         where !d.IsDeleted && d.FormCode == l.FormType
                         select new FormDataValue()
                         {
                             Id = d.Id,
                             FormId = d.FormId,
                             FormCode = d.FormCode,
                             Code = d.Code,
                             Value = d.Value,
                         }).ToList();

            var listResult = listForms.GroupJoin(datas, f => f.Id, d => d.FormId,
                (form, data) =>
                {
                    return new MappingQueryWithResult()
                    {
                        CreatedAt = form.CreatedAt,
                        CreatedBy = form.CreatedBy,
                        TransactionDate = form.TransactionDate,
                        TotalPews = form.TotalPews,
                        BloodVesselPews = form.BloodVesselPews,
                        BreathingPews = form.BreathingPews,
                        HypothermiaPews = form.HypothermiaPews,
                        Id = form.Id,
                        VisitId = form.VisitId,
                        Datas = data.ToList()
                    };
                }).ToList();

            return listResult.OrderByDescending(e => e.TransactionDate).ToList();
        }

        private string GetDataForm(List<FormDataValue> datas, string code)
        {
            if (datas == null)
                return String.Empty;

            string data = (from d in datas
                           where d.Code == code
                           select d.Value).FirstOrDefault();

            if (data == null)
                return String.Empty;

            return data;
        }
        private List<string> GetStringContent(MappingQueryWithResult obj,ref VitalSign vitalSign)
        {
            if (obj == null)
                return new List<string>();
            List<string> datas_result = new List<string>();
            var data = obj.Datas;
            if (data != null && data.Count > 0)
            {
                vitalSign.NhipTho = GetDataForm(data, "IPDPEWSPDT2");
                vitalSign.Spo2 = GetDataForm(data, "IPDPEWSPDT4");
                vitalSign.HuyetApToiThieu = GetDataForm(data, "IPDPEWSPDT7");
                vitalSign.HuyetApToiDa = GetDataForm(data, "IPDPEWSPDT6");
                vitalSign.Mach = GetDataForm(data, "IPDPEWSPDT9");
                vitalSign.ThanNhiet = GetDataForm(data, "IPDPEWSPDT11");
                vitalSign.TriGiac = GetDataForm(data, "IPDPEWSPDT13");
                vitalSign.HoTroHoHap = GetDataForm(data, "IPDPEWSPDT16");

                datas_result.Add($"Nhịp thở: {vitalSign.NhipTho} (lần/phút), SPO2: {vitalSign.Spo2} (%), Huyết áp: Tối thiểu {vitalSign.HuyetApToiThieu} (mmHg) - Tối đa: {vitalSign.HuyetApToiDa} (mmHg), Mạch: {GetDataForm(data, "IPDPEWSPDT9")} (Nhịp/phút), Thân nhiệt: {vitalSign.ThanNhiet} (oC), Tri giác: {vitalSign.TriGiac}, Hỗ trợ hô hấp: {vitalSign.HoTroHoHap}");
                vitalSign.DiemDau1 = GetDataForm(data, "IPDPEWSPDT20");
                vitalSign.DiemDau2 = GetDataForm(data, "IPDPEWSPDT21");
                string diemDau = $"Điểm đau: {(vitalSign.DiemDau1)}";
                if (!string.IsNullOrEmpty(GetDataForm(data, "IPDPEWSPDT20")))
                    diemDau += "-";
                diemDau += vitalSign.DiemDau2;

                string duongMauToanPhan = "";
                if (!string.IsNullOrEmpty(GetDataForm(data, "IPDPEWSPDT23")))
                {
                    vitalSign.DuongMauToanPhan1 = GetDataForm(data, "IPDPEWSPDT23");
                    vitalSign.DuongMauToanPhan2 = GetDataForm(data, "IPDPEWSPDT24");
                    duongMauToanPhan = $"Đường máu toàn phần: {vitalSign.DuongMauToanPhan1} - {vitalSign.DuongMauToanPhan2}";
                }          
                else
                {
                    duongMauToanPhan = $"Đường máu toàn phần:";
                }

                datas_result.Add($"{diemDau}, {duongMauToanPhan}");

                vitalSign.DanhGiaVeinTruyen = GetDataForm(data, "IPDPEWSPDT27");
                switch (vitalSign.DanhGiaVeinTruyen)
                {
                    case "00":
                        vitalSign.DanhGiaVeinTruyenDesc = "(Không có dấu hiệu viêm tĩnh mạch. Tiếp tục THEO DÕI vị trí đặt Catheter)";
                        break;
                    case "01":
                        vitalSign.DanhGiaVeinTruyenDesc = "(Có thể là dấu hiệu KHỞI ĐẦU viêm tĩnh mạch. Tiếp tục THEO DÕI vị trí đặt Catheter)";
                        break;
                    case "02":
                        vitalSign.DanhGiaVeinTruyenDesc = "(Viêm tĩnh mạch giai đoạn SỚM. RÚT Catheter)";
                        break;
                    case "03":
                        vitalSign.DanhGiaVeinTruyenDesc = "(Viêm tĩnh mạch giai đoạn TRUNG BÌNH. RÚT Catheter và Cân nhắc điều trị)";
                        break;
                    case "04":
                        vitalSign.DanhGiaVeinTruyenDesc = "(Viêm tĩnh mạch tiến triển hoặc KHỞI ĐẦU viêm tĩnh mạch huyết khối. RÚT Catheter và Cân nhắc điều trị)";
                        break;
                    case "05":
                        vitalSign.DanhGiaVeinTruyenDesc = "(Viêm tĩnh mạch huyết khối tiến triển. RÚT Catheter và Điều trị ngay)";
                        break;
                    default:
                        break;
                }

                datas_result.Add($"Đánh giá vein truyền (VIP Score): {vitalSign.DanhGiaVeinTruyen} {vitalSign.DanhGiaVeinTruyenDesc}");

                string soLuongDichVao = "Số lượng dịch vào: ";
                if (GetDataForm(data, "IPDPEWSPDT33").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichVaoT = GetDataForm(data, "IPDPEWSPDT34");
                    soLuongDichVao += $"T({vitalSign.SoLuongDichVaoT}ml), ";
                }    
                if (GetDataForm(data, "IPDPEWSPDT35").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichVaoP = GetDataForm(data, "IPDPEWSPDT36");
                    soLuongDichVao += $"P({vitalSign.SoLuongDichVaoP}ml), ";
                }     
                if (GetDataForm(data, "IPDPEWSPDT37").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichVaoM = GetDataForm(data, "IPDPEWSPDT38");
                    soLuongDichVao += $"M({vitalSign.SoLuongDichVaoM}ml), ";
                }    
                    
                if (GetDataForm(data, "IPDPEWSPDT39").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichVaoS = GetDataForm(data, "IPDPEWSPDT40");
                    soLuongDichVao += $"S({GetDataForm(data, "IPDPEWSPDT40")}ml), ";
                }        
                if (GetDataForm(data, "IPDPEWSPDT41").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichVaoAN = GetDataForm(data, "IPDPEWSPDT42");
                    soLuongDichVao += $"AN({GetDataForm(data, "IPDPEWSPDT42")}ml), ";
                }     
                if (GetDataForm(data, "IPDPEWSPDT43").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichVaoD = GetDataForm(data, "IPDPEWSPDT44");
                    soLuongDichVao += $"D({GetDataForm(data, "IPDPEWSPDT44")}ml),";
                }    
                   

                int index1 = soLuongDichVao.LastIndexOf(',');
                if (index1 > 0)
                    soLuongDichVao = soLuongDichVao.Remove(index1);

                vitalSign.TongDichVao = GetDataForm(data, "IPDPEWSPDT32");
                soLuongDichVao += $". Tổng dịch vào: {vitalSign.TongDichVao}ml";

                datas_result.Add(soLuongDichVao);

                // Số lượng dịch ra
                string soLuongDichRa = "Số lượng dịch ra: ";

                if (GetDataForm(data, "IPDPEWSPDT48").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichRaSD = GetDataForm(data, "IPDPEWSPDT49");
                    soLuongDichRa += $"SD({GetDataForm(data, "IPDPEWSPDT49")}ml), ";
                }    
                if (GetDataForm(data, "IPDPEWSPDT50").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichRaN = GetDataForm(data, "IPDPEWSPDT51");
                    soLuongDichRa += $"N({GetDataForm(data, "IPDPEWSPDT51")}ml), ";
                }    
                if (GetDataForm(data, "IPDPEWSPDT52").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichRaPh = GetDataForm(data, "IPDPEWSPDT53");
                    soLuongDichRa += $"Ph({GetDataForm(data, "IPDPEWSPDT53")}ml), ";
                }    
                if (GetDataForm(data, "IPDPEWSPDT54").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichRaNT = GetDataForm(data, "IPDPEWSPDT55");
                    soLuongDichRa += $"NT({GetDataForm(data, "IPDPEWSPDT55")}ml), ";
                }    
                if (GetDataForm(data, "IPDPEWSPDT56").ToUpper() == "TRUE")
                {
                    vitalSign.SoLuongDichRaDL = GetDataForm(data, "IPDPEWSPDT57");
                    soLuongDichRa += $"DL({GetDataForm(data, "IPDPEWSPDT57")}ml),";
                }    
                int index2 = soLuongDichRa.LastIndexOf(',');
                if (index2 > 0)
                    soLuongDichRa = soLuongDichRa.Remove(index2);

                vitalSign.TongDichRa = GetDataForm(data, "IPDPEWSPDT47");
                soLuongDichRa += $". Tổng dịch ra: {vitalSign.TongDichRa}ml";

                datas_result.Add(soLuongDichRa);

                vitalSign.BilanDich = GetDataForm(data, "IPDPEWSPDT59");
                datas_result.Add($"Bilan dịch: {vitalSign.BilanDich}ml");
            }
            else
            {
                datas_result.Add($"Nhịp thở: (lần/phút), SPO2: (%), Huyết áp: Tối thiểu: (mmHg) - Tối đa: (mmHg), Mạch: (Nhịp/phút), Thân nhiệt: (oC), Tri giác: , Hỗ trợ hô hấp: ");
                datas_result.Add($"Điểm đau: , Đường máu toàn phần: ");
                datas_result.Add("Đánh giá vein truyền:");
                datas_result.Add("Số lượng dịch vào: .Tổng dịch vào");
                datas_result.Add("Số lượng dịch ra: .Tổng dịch ra");
                datas_result.Add("Bilan dịch: ml");
            }

            return datas_result;
        }

        private List<MappingQueryWithResult> OrderFormByCreate(List<MappingQueryWithResult> forms)
        {
            int lengthOfForms = forms.Count;
            for(int i =0; i < lengthOfForms; i++)
            {
                for(int j = i + 1; j < lengthOfForms; j++)
                {
                    if(forms[i].TransactionDate == forms[j].TransactionDate)
                    {
                        if(forms[i].CreatedAt < forms[j].CreatedAt)
                        {
                            var temp = forms[j];
                            forms[j] = forms[i];
                            forms[i] = temp;
                        }
                    }
                }
            }

            return forms;
        }
        
        class MappingQueryWithResult : IPDVitalSignForPediatrics
        {
            private List<FormDataValue> datas;
            public List<FormDataValue> Datas
            {
                get
                {
                    return datas;
                }
                set
                {
                    this.datas = new List<FormDataValue>(value);
                }
            }
        }
    }
    public class IPDVitalSignForPediatricParams : IPDThrombosisRiskFactorAssessmentParams
    {
    }
}

