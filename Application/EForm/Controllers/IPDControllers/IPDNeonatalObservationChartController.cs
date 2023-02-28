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
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class IPDNeonatalObservationChartController: BaseIPDApiController
    {
        private readonly string formCode = "A02_030_080322_V";

        [HttpPost]
        [Route("api/NeonatalObservationChart/Create/{visitId}")]
        [Permission(Code = "IPDNOC1")]
        public IHttpActionResult Create(Guid visitId)
        {
            dynamic visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock((IPD)visit, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoTreSoSinh))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);

            var formData = new IPDNeonatalObservationChart
            {
                VisitId = visitId,
                TransactionDate = DateTime.Now
            };

            unitOfWork.IPDNeonatalObservationChartRepository.Add(formData);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(visitId, formData.Id, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoTreSoSinh);
            return Content(HttpStatusCode.OK, new { formData.Id });
        }

        [HttpGet]
        [Route("api/NeonatalObservationChart/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "IPDNOC2")]
        public IHttpActionResult GetDetail(Guid visitId, Guid formId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = unitOfWork.IPDNeonatalObservationChartRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }
            var data = GetFormData(visitId, formId, formCode);

            if (data.Count > 0)
            {
                return Content(HttpStatusCode.OK, new
                {
                    formId,
                    Datas = data,
                    form.CreatedBy,
                    form.TransactionDate,
                    IsLocked = IPDIsBlock(GetIPD(visitId), Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoTreSoSinh)
                });
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("api/NeonatalObservationChart/Update/{visitId}/{formId}")]
        [Permission(Code = "IPDNOC3")]
        public IHttpActionResult Update(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            formId = (Guid)request["Id"];
            var form = unitOfWork.IPDNeonatalObservationChartRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }

            var listForms = unitOfWork.IPDNeonatalObservationChartRepository
                .Find(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == ipd.Id
                ).OrderByDescending(e => e.TransactionDate).Select(e => new
                {
                    TransactionDate = e.TransactionDate
                }).ToList();
            double second = 0;
            if (listForms.Count == 1)
            {
                second = listForms[0].TransactionDate.Second;
            }
            else
            {
                second = listForms[1].TransactionDate.Second;
            }

            form.TransactionDate = DateTime.ParseExact(request["TransactionDate"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture).Add(TimeSpan.FromSeconds(second + 1));

            HandleUpdateOrCreateFormDatas(visitId, formId, formCode, request["Datas"]);

            unitOfWork.IPDNeonatalObservationChartRepository.Update(form);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(visitId, form.Id, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoTreSoSinh);
            return Content(HttpStatusCode.OK, new { form.Id });
        }

        [HttpGet]
        [Route("api/NeonatalObservationChart/GetByVisitId/{visitId}")]
        [Permission(Code = "IPDNOC4")]
        public IHttpActionResult GetByVisitId(Guid visitId, [FromUri] VitalSignsForPregnantWomanParams request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            bool formStatus = IPDIsBlock(ipd, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoTreSoSinh);

            var data = unitOfWork.IPDNeonatalObservationChartRepository.FirstOrDefault(e => e.VisitId == visitId);
            if (data == null)
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    FormStatus = formStatus,
                    Message.FORM_NOT_FOUND
                });
            }

            var listForms = unitOfWork.IPDNeonatalObservationChartRepository
                .Find( e => !e.IsDeleted && e.VisitId != null && e.VisitId == ipd.Id
                ).OrderByDescending(e => e.TransactionDate)
                .Select(e => new
                {
                    Id = e.Id,
                    CreateAt = e.CreatedAt,
                    VisitId = e.VisitId,
                    CreateBy = e.CreatedBy,
                    UpdatedAt = e.UpdatedAt,
                    TransactionDate = e.TransactionDate,
                    Datas = GetFormData((Guid)e.VisitId, e.Id, formCode)
                });

            List<VitalSignsForPregnantWomanItem> listItems = new List<VitalSignsForPregnantWomanItem>();

            List<MEWItemModel> nhipThoMEWS = new List<MEWItemModel>();
            List<MEWItemModel> machMEWS = new List<MEWItemModel>();
            List<MEWItemModel> thanNhietMEWS = new List<MEWItemModel>();

            foreach (var item in listForms)
            {
                List<string> datas = new List<string>();
                string tongMEW = "";
                VitalSign vitalSign = new VitalSign();
                if (item.Datas.Count > 0)
                {
                    tongMEW = GetValueFromFormValueDatas("IPDNEWSOBC20", item.Datas);

                    vitalSign.NhipTho = GetValueFromFormValueDatas("IPDNEWSOBC2", item.Datas);
                    vitalSign.SuyHoHap = GetValueFromFormValueDatas("IPDNEWSOBC4", item.Datas);


                    vitalSign.Spo2 = GetValueFromFormValueDatas("IPDNEWSOBC6", item.Datas);
                    vitalSign.Mach = GetValueFromFormValueDatas("IPDNEWSOBC11", item.Datas);
                    vitalSign.ThanNhiet = GetValueFromFormValueDatas("IPDNEWSOBC13", item.Datas);
                    vitalSign.TriGiac = GetValueFromFormValueDatas("IPDNEWSOBC15", item.Datas);
                    vitalSign.HoTroHoHap = GetValueFromFormValueDatas("IPDNEWSOBC18", item.Datas);

                    datas.Add($"Nhịp thở: {vitalSign.NhipTho} (lần/phút), Suy hô hấp: {vitalSign.SuyHoHap}, SPO2: {vitalSign.Spo2} (%), Mạch: {vitalSign.Mach} (Nhịp/phút),Thân nhiệt: {vitalSign.ThanNhiet} (oC), Tri giác: {vitalSign.TriGiac}, Hỗ trợ hô hấp: {vitalSign.HoTroHoHap}");

                    vitalSign.DiemDau1 = GetValueFromFormValueDatas("IPDNEWSOBC22", item.Datas);
                    vitalSign.DiemDau2 = GetValueFromFormValueDatas("IPDNEWSOBC23", item.Datas);
                    string diemDau = $"Điểm đau: {vitalSign.DiemDau1} - {vitalSign.DiemDau2}";

                    vitalSign.CanNang = GetValueFromFormValueDatas("IPDNEWSOBC26", item.Datas);
                    string canNang = $"Cân nặng: {vitalSign.CanNang} (kg)";
                   
                    string duongMauToanPhan = "Đường máu toàn phần: ";
                    if (GetValueFromFormValueDatas("IPDNEWSOBC28", item.Datas) != null)
                    {
                       vitalSign.DuongMauToanPhan1 = GetValueFromFormValueDatas("IPDNEWSOBC28", item.Datas);
                       vitalSign.DuongMauToanPhan2 = GetValueFromFormValueDatas("IPDNEWSOBC29", item.Datas);
                       duongMauToanPhan += $"{vitalSign.DuongMauToanPhan1} - {vitalSign.DuongMauToanPhan2}";
                    }


                    datas.Add($"{diemDau}, {canNang}, {duongMauToanPhan}");

                    string an = $"Ăn: ";
                    string anValue = GetValueFromFormValueDatas("IPDNEWSOBC", item.Datas);

                    if (GetValueFromFormValueDatas("IPDNEWSOBC31", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDNEWSOBC31", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.AnMieng = GetValueFromFormValueDatas("IPDNEWSOBC32", item.Datas);
                            an += $"Miệng - {vitalSign.AnMieng}ml";
                        }
                    }
                    if (GetValueFromFormValueDatas("IPDNEWSOBC33", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDNEWSOBC33", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.AnSonde = GetValueFromFormValueDatas("IPDNEWSOBC34", item.Datas);
                            an += $"Sonde - {GetValueFromFormValueDatas("IPDNEWSOBC34", item.Datas)}ml";
                        }
                    }

                    datas.Add($"{an}");

                    vitalSign.NuocTieu = GetValueFromFormValueDatas("IPDNEWSOBC36", item.Datas);
                    string nuocTieu = $"Nước tiểu: {vitalSign.NuocTieu}ml";

                    vitalSign.Phan = GetValueFromFormValueDatas("IPDNEWSOBC38", item.Datas);
                    string phan = $"Phân: {vitalSign.Phan}ml";

                    datas.Add($"{nuocTieu}, {phan}");

                    vitalSign.TruyenDich1 = GetValueFromFormValueDatas("IPDNEWSOBC40", item.Datas);
                    vitalSign.TruyenDich2 = GetValueFromFormValueDatas("IPDNEWSOBC41", item.Datas);
                    string truyenDich = $"Truyền dịch: {vitalSign.TruyenDich1} - {vitalSign.TruyenDich2}ml/h";
                    datas.Add(truyenDich);

                    vitalSign.DanhGiaVeinTruyen = GetValueFromFormValueDatas("IPDNEWSOBC44", item.Datas);
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

                    datas.Add($"Đánh giá vein truyền (VIP Score): {vitalSign.DanhGiaVeinTruyen} {vitalSign.DanhGiaVeinTruyenDesc}");
                    vitalSign.DanhGiaSuyHoHapCoCoRutNgucTren = GetValueFromFormValueDatas("IPDNEWSOBC48", item.Datas);
                    vitalSign.DanhGiaSuyHoHapCoCoRutNgucDuoi = GetValueFromFormValueDatas("IPDNEWSOBC49", item.Datas);
                    vitalSign.DanhGiaSuyHoHapRutLomHomUc = GetValueFromFormValueDatas("IPDNEWSOBC50", item.Datas);
                    vitalSign.DanhGiaSuyHoHapNoCanhMui = GetValueFromFormValueDatas("IPDNEWSOBC51", item.Datas);
                    vitalSign.DanhGiaSuyHoHapRen = GetValueFromFormValueDatas("IPDNEWSOBC52", item.Datas);

                    string danhGiaSuyHoHap = $"Đánh giá suy hô hấp: Co rút cơ ngực trên: {vitalSign.DanhGiaSuyHoHapCoCoRutNgucTren}; " +
                        $"Co rút cơ ngực dưới: {vitalSign.DanhGiaSuyHoHapCoCoRutNgucDuoi}; " +
                        $"Rút lõm hõm ức: {vitalSign.DanhGiaSuyHoHapRutLomHomUc}; " +
                        $"Nở cánh mũi: {vitalSign.DanhGiaSuyHoHapNoCanhMui}; " +
                        $"Rên: {vitalSign.DanhGiaSuyHoHapRen}. ";

                    string mucDo = "";
                    double mucDoValue = -1;
                    try
                    {
                        mucDoValue = Convert.ToDouble(GetValueFromFormValueDatas("IPDNEWSOBC53", item.Datas));
                    }
                    catch (Exception)
                    {
                        mucDo = "N/A";
                    }

                    if (mucDoValue == 0)
                    {
                        vitalSign.MucDoSuyHoHap = $"{mucDoValue} - Không";
                       
                    }
                    else if (mucDoValue >= 1 && mucDoValue <= 2)
                    {
                        vitalSign.MucDoSuyHoHap = $"{mucDoValue} - Nhẹ";
                    }
                    else if (mucDoValue >= 3 && mucDoValue <= 5)
                    {
                        vitalSign.MucDoSuyHoHap = $"{mucDoValue} - Vừa";
                    }
                    else
                    {
                        if (mucDo == "N/A")
                        {
                            vitalSign.MucDoSuyHoHap = "N/A";
                        }
                        else
                        {
                            vitalSign.MucDoSuyHoHap = $"{mucDoValue} - Nặng";
                        }
                    }
                    danhGiaSuyHoHap += $"Mức độ: {vitalSign.MucDoSuyHoHap}";
                    datas.Add($"{danhGiaSuyHoHap}");

                    MEWItemModel nhipThoMEW = new MEWItemModel();
                    MEWItemModel machMEW = new MEWItemModel();
                    MEWItemModel thanNhietMEW = new MEWItemModel();

                    //List nhịp thở
                    if (GetValueFromFormValueDatas("IPDNEWSOBC2", item.Datas) != null && GetValueFromFormValueDatas("IPDNEWSOBC2", item.Datas) != "")
                    {
                        uint uintNhipTho = uint.Parse(GetValueFromFormValueDatas("IPDNEWSOBC2", item.Datas));
                        if (uintNhipTho >= 70 || uintNhipTho <= 19)
                        {
                            nhipThoMEW.MEWValue = 3;
                        }
                        else if ((uintNhipTho >= 65 && uintNhipTho <= 69) || (uintNhipTho >= 20 && uintNhipTho <= 24))
                        {
                            nhipThoMEW.MEWValue = 2;
                        }
                        else if ((uintNhipTho >= 25 && uintNhipTho <= 29) || (uintNhipTho >= 60 && uintNhipTho <= 64))
                        {
                            nhipThoMEW.MEWValue = 1;
                        }
                        else if (uintNhipTho >= 30 && uintNhipTho <= 59)
                        {
                            nhipThoMEW.MEWValue = 0;
                        }
                        nhipThoMEW.MEWRate = GetValueFromFormValueDatas("IPDNEWSOBC2", item.Datas);
                    }
                    else
                    {
                        nhipThoMEW.MEWValue = null;
                        nhipThoMEW.MEWRate = null;
                    }
                    nhipThoMEW.TransactionDate = item.TransactionDate;
                    nhipThoMEW.CreatedBy = item.CreateBy;
                    nhipThoMEWS.Add(nhipThoMEW);

                    //List mạch
                    if (GetValueFromFormValueDatas("IPDNEWSOBC11", item.Datas) != null && GetValueFromFormValueDatas("IPDNEWSOBC11", item.Datas) != "")
                    {
                        uint uintMach = uint.Parse(GetValueFromFormValueDatas("IPDNEWSOBC11", item.Datas));

                        if (uintMach >= 180 || uintMach <= 69)
                        {
                            machMEW.MEWValue = 3;
                        }
                        else if ((uintMach >= 170 && uintMach <= 179) || (uintMach >= 70 && uintMach <= 79))
                        {
                            machMEW.MEWValue = 2;
                        }
                        else if ((uintMach >= 160 && uintMach <= 169) || (uintMach >= 80 && uintMach <= 89))
                        {
                            machMEW.MEWValue = 1;
                        }
                        else if ((uintMach >= 90 && uintMach <= 159))
                        {
                            machMEW.MEWValue = 0;
                        }

                        machMEW.MEWRate = GetValueFromFormValueDatas("IPDNEWSOBC11", item.Datas);
                    }
                    else
                    {
                        machMEW.MEWValue = null;
                        machMEW.MEWRate = null;
                    }
                    machMEW.TransactionDate = item.TransactionDate;
                    machMEW.CreatedBy = item.CreateBy;
                    machMEWS.Add(machMEW);

                    //List thân nhiệt
                    if (GetValueFromFormValueDatas("IPDNEWSOBC13", item.Datas) != null && GetValueFromFormValueDatas("IPDNEWSOBC13", item.Datas) != "")
                    {
                        var doubleThanNhiet = double.Parse(GetValueFromFormValueDatas("IPDNEWSOBC13", item.Datas));
                        if (doubleThanNhiet >= 38)
                        {
                            thanNhietMEW.MEWValue = 3;
                        }
                        else if (doubleThanNhiet <= 35.4)
                        {
                            thanNhietMEW.MEWValue = 2;
                        }
                        else if ((doubleThanNhiet >= 35.5 && doubleThanNhiet <= 36.4) || (doubleThanNhiet >= 37.5 && doubleThanNhiet <= 37.9))
                        {
                            thanNhietMEW.MEWValue = 1;
                        }
                        else if ((doubleThanNhiet >= 36.5 && doubleThanNhiet <= 37.4))
                        {
                            thanNhietMEW.MEWValue = 0;
                        }

                        thanNhietMEW.MEWRate = GetValueFromFormValueDatas("IPDNEWSOBC13", item.Datas);
                    }
                    else
                    {
                        thanNhietMEW.MEWValue = null;
                        thanNhietMEW.MEWRate = null;
                    }

                    thanNhietMEW.TransactionDate = item.TransactionDate;
                    thanNhietMEW.CreatedBy = item.CreateBy;
                    thanNhietMEWS.Add(thanNhietMEW);
                }
                else
                {
                    datas.Add($"Nhịp thở: (lần/phút), SPO2: (%), Huyết áp: Tối thiểu: (mmHg) - Tối đa: (mmHg), Mạch: (Nhịp/phút), Thân nhiệt: (oC), Tri giác: , Hỗ trợ hô hấp: ");
                    datas.Add($"Điểm đau: , Cân nặng: (kg), Đường máu toàn phần: ");
                    datas.Add("Ăn: Miệng - ml, Sonde - ml");
                    datas.Add("Nước tiểu: ; Phân: ");
                    datas.Add("Truyền dịch: ml/h");
                    datas.Add("Đánh giá vein truyền:");
                    datas.Add("Đánh gia suy hô hấp: Co rút cơ ngực trên = ; Co rút cơ ngực dưới = ; Rút lõm hõm ức = ; Nở cánh mũi: ; Rên: . Mức độ:");

                    MEWItemModel nhipThoMEW = new MEWItemModel();
                    nhipThoMEW.MEWRate = null;
                    nhipThoMEW.MEWValue = null;
                    nhipThoMEW.TransactionDate = item.TransactionDate;
                    nhipThoMEWS.Add(nhipThoMEW);

                    MEWItemModel machMEW = new MEWItemModel();
                    machMEW.MEWRate = null;
                    machMEW.MEWValue = null;
                    machMEW.TransactionDate = item.TransactionDate;
                    machMEWS.Add(machMEW);

                    MEWItemModel thanNhietMEW = new MEWItemModel();
                    thanNhietMEW.MEWRate = null;
                    thanNhietMEW.MEWValue = null;
                    thanNhietMEW.TransactionDate = item.TransactionDate;
                    thanNhietMEWS.Add(thanNhietMEW);

                }

                bool isYellow = Convert.ToBoolean(GetValueFromFormValueDatas("IPDNEWSOBC55", item.Datas));
                listItems.Add(new VitalSignsForPregnantWomanItem
                {
                    Id = item.Id,
                    VisitId = item.VisitId,
                    CreatedAt = item.CreateAt,
                    UpdatedAt = item.UpdatedAt,
                    CreatedBy = item.CreateBy,
                    Content = datas,
                    MEWSTotal = tongMEW,
                    TransactionDate = item.TransactionDate,
                    VitalSign = vitalSign,
                    IsYellow = isYellow
                });

            }


            Nullable<DateTime> fromDate = new DateTime();
            Nullable<DateTime> toDate = new DateTime();
            string assessor = request.Assessor;

            if (request.FromDate != null)
            {
                fromDate = DateTime.ParseExact(request.FromDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US"));
            }
            else
            {
                fromDate = null;
            }

            if (request.ToDate != null)
            {
                toDate = DateTime.ParseExact(request.ToDate.ToString(), "HH:mm dd/MM/yyyy", new CultureInfo("en-US")).Add(TimeSpan.FromSeconds(59));
            }
            else
            {
                toDate = null;
            }

            if (fromDate != null && toDate != null)
            {
                listItems = listItems.Where(e => e.TransactionDate >= fromDate && e.TransactionDate <= toDate).ToList();
                nhipThoMEWS = nhipThoMEWS.Where(e => e.TransactionDate >= fromDate && e.TransactionDate <= toDate).ToList();
                thanNhietMEWS = thanNhietMEWS.Where(e => e.TransactionDate >= fromDate && e.TransactionDate <= toDate).ToList();
                machMEWS = machMEWS.Where(e => e.TransactionDate >= fromDate && e.TransactionDate <= toDate).ToList();
            }
            else if (fromDate == null && toDate != null)
            {
                listItems = listItems.Where(e => e.TransactionDate <= toDate).ToList();
                nhipThoMEWS = nhipThoMEWS.Where(e => e.TransactionDate <= toDate).ToList();
                thanNhietMEWS = thanNhietMEWS.Where(e => e.TransactionDate <= toDate).ToList();
                machMEWS = machMEWS.Where(e => e.TransactionDate <= toDate).ToList();
            }
            else if (fromDate != null && toDate == null)
            {
                listItems = listItems.Where(e => e.TransactionDate >= fromDate).ToList();
                nhipThoMEWS = nhipThoMEWS.Where(e => e.TransactionDate >= fromDate).ToList();
                thanNhietMEWS = thanNhietMEWS.Where(e => e.TransactionDate >= fromDate).ToList();
                machMEWS = machMEWS.Where(e => e.TransactionDate >= fromDate).ToList();
            }

            if (assessor != null)
            {
                listItems = listItems.Where(e => e.CreatedBy != null && e.CreatedBy.ToUpper() == assessor.ToUpper()).ToList();
                nhipThoMEWS = nhipThoMEWS.Where(e => e.CreatedBy != null && e.CreatedBy.ToUpper() == assessor.ToUpper()).ToList();
                thanNhietMEWS = thanNhietMEWS.Where(e => e.CreatedBy != null && e.CreatedBy.ToUpper() == assessor.ToUpper()).ToList();
                machMEWS = machMEWS.Where(e => e.CreatedBy != null && e.CreatedBy.ToUpper() == assessor.ToUpper()).ToList();
            }

            var count = listItems.Count();
            listItems = listItems.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            return Content(HttpStatusCode.OK, new
            {
                listItems = listItems.OrderByDescending(e => e.TransactionDate).ToList(),
                Count = count,
                Is24hLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoTreSoSinh),
                NhipThoMEWS = nhipThoMEWS.OrderBy(e => e.TransactionDate).ToList(),
                ThanNhietMEWS = thanNhietMEWS.OrderBy(e => e.TransactionDate).ToList(),
                MachMEWS = machMEWS.OrderBy(e => e.TransactionDate).ToList()
            });
        }
    }
}
