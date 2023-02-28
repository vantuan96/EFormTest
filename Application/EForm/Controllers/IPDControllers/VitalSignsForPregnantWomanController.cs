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
using System.Web.Http;

namespace EForm.Controllers.IPDControllers
{
    [SessionAuthorize]
    public class VitalSignsForPregnantWomanController : BaseIPDApiController
    {
        private readonly string formCode = "A02_037_080322_V";

        [HttpGet]
        [Route("api/VitalSignsForPregnantWoman/GetByVisitId/{visitId}")]
        [Permission(Code = "VSFPW1")]
        public IHttpActionResult GetByVisitId(Guid visitId, [FromUri] VitalSignsForPregnantWomanParams request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            bool formStatus = IPDIsBlock(ipd, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoSanPhu);

            var data = unitOfWork.IPDVitalSignForPregnantWomanRepository.FirstOrDefault(e => e.VisitId == visitId);
            if (data == null)
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    FormStatus = formStatus,
                    Message.FORM_NOT_FOUND
                });
            }


            var listForms = unitOfWork.IPDVitalSignForPregnantWomanRepository
                .Find(
                    e => !e.IsDeleted &&
                    e.VisitId != null &&
                    e.VisitId == ipd.Id
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
            List<object> objs = new List<object>();
            foreach (var item in listForms)
            {
                List<string> datas = new List<string>();
                string tongMEW = "";
                VitalSign vitalSign = new VitalSign();
                
                if (item.Datas.Count > 0)
                {
                    // GetValueFromFormValueDatas(string code, List<FormDataValue> datas)
                    // IPDMEWSPW2
                    vitalSign.NhipTho = GetValueFromFormValueDatas("IPDMEWSPW2", item.Datas);
                    vitalSign.Spo2 = GetValueFromFormValueDatas("IPDMEWSPW4", item.Datas);
                    vitalSign.HuyetApToiThieu = GetValueFromFormValueDatas("IPDMEWSPW7", item.Datas);
                    vitalSign.HuyetApToiDa = GetValueFromFormValueDatas("IPDMEWSPW6", item.Datas);
                    vitalSign.Mach = GetValueFromFormValueDatas("IPDMEWSPW9", item.Datas);
                    vitalSign.ThanNhiet = GetValueFromFormValueDatas("IPDMEWSPW11", item.Datas);
                    vitalSign.TriGiac = GetValueFromFormValueDatas("IPDMEWSPW13", item.Datas);
                    vitalSign.HoTroHoHap = GetValueFromFormValueDatas("IPDMEWSPW16", item.Datas);

                    datas.Add($"Nhịp thở: {vitalSign.NhipTho} (lần/phút), SPO2: {vitalSign.Spo2} (%),Huyết áp: Tối thiểu: {vitalSign.HuyetApToiThieu} (mmHg) - Tối đa: {vitalSign.HuyetApToiDa} (mmHg), Mạch: {vitalSign.Mach} (Nhịp/phút), Thân nhiệt: {vitalSign.ThanNhiet} (oC), Tri giác: {vitalSign.TriGiac}, Hỗ trợ hô hấp: {vitalSign.HoTroHoHap}");
                    vitalSign.DiemDau1 = GetValueFromFormValueDatas("IPDMEWSPW20", item.Datas);
                    vitalSign.DiemDau2 = GetValueFromFormValueDatas("IPDMEWSPW21", item.Datas);
                    string diemDau = $"Điểm đau: {vitalSign.DiemDau1} - {vitalSign.DiemDau2}";
                    
                    if (GetValueFromFormValueDatas("IPDMEWSPW23", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW23", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.DuongMauToanPhan1 = "T";
                        }
                        else if (GetValueFromFormValueDatas("IPDMEWSPW24", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.DuongMauToanPhan1 = "S";
                        }
                        else
                        {
                            vitalSign.DuongMauToanPhan1 = "";
                        }
                    }
                    else
                    {
                        vitalSign.DuongMauToanPhan1 = "";
                    }

                    datas.Add($"{diemDau}, Đường máu toàn phần: {vitalSign.DuongMauToanPhan1}");

                    vitalSign.DanhGiaVeinTruyen = GetValueFromFormValueDatas("IPDMEWSPW27", item.Datas);
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

                    vitalSign.TanSoCoTuCung = GetValueFromFormValueDatas("IPDMEWSPW31", item.Datas);
                    datas.Add($"Tần số co tử cung: {vitalSign.TanSoCoTuCung} (lần/10')");

                    vitalSign.TanSoTimThai = GetValueFromFormValueDatas("IPDMEWSPW33", item.Datas);
                    datas.Add($"Tần số tim thai: {vitalSign.TanSoTimThai} (nhịp/phút)");

                    if (GetValueFromFormValueDatas("IPDMEWSPW35", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW35", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.MatDoCoHoiTuCung = "F";
                        }
                        else if (GetValueFromFormValueDatas("IPDMEWSPW36", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.MatDoCoHoiTuCung = "P";
                        }
                        else
                        {
                            vitalSign.MatDoCoHoiTuCung = "";
                        }
                    }
                    else
                    {
                        vitalSign.MatDoCoHoiTuCung = "";
                    }

                    datas.Add($"Mật độ co hồi tử cung: {vitalSign.MatDoCoHoiTuCung}");

                    if (GetValueFromFormValueDatas("IPDMEWSPW38", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW38", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SanDich = "N";
                        }
                        else if (GetValueFromFormValueDatas("IPDMEWSPW39", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SanDich = "A";
                        }
                        else
                        {
                            vitalSign.SanDich = "";
                        }
                    }
                    else
                    {
                        vitalSign.SanDich = "";
                    }

                    datas.Add($"Sản dịch: {vitalSign.SanDich}");

                    string soLuongDichVao = "Số lượng dịch vào: ";
                    if (GetValueFromFormValueDatas("IPDMEWSPW43", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW43", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichVaoT = GetValueFromFormValueDatas("IPDMEWSPW44", item.Datas);
                            soLuongDichVao += $"T({vitalSign.SoLuongDichVaoT}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW45", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW45", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichVaoP = GetValueFromFormValueDatas("IPDMEWSPW46", item.Datas);
                            soLuongDichVao += $"P({vitalSign.SoLuongDichVaoP}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW47", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW47", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichVaoM = GetValueFromFormValueDatas("IPDMEWSPW48", item.Datas);
                            soLuongDichVao += $"M({vitalSign.SoLuongDichVaoM}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW49", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW49", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichVaoS = GetValueFromFormValueDatas("IPDMEWSPW50", item.Datas);
                            soLuongDichVao += $"S({vitalSign.SoLuongDichVaoS}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW51", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW51", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichVaoAN = GetValueFromFormValueDatas("IPDMEWSPW52", item.Datas);
                            soLuongDichVao += $"AN({vitalSign.SoLuongDichVaoAN}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW53", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW53", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichVaoD = GetValueFromFormValueDatas("IPDMEWSPW54", item.Datas);
                            soLuongDichVao += $"D({vitalSign.SoLuongDichVaoD}ml),";
                        }
                    }

                    int index1 = soLuongDichVao.LastIndexOf(',');
                    if (index1 > 0)
                    {
                        soLuongDichVao = soLuongDichVao.Remove(index1);
                    }

                    vitalSign.TongDichVao = GetValueFromFormValueDatas("IPDMEWSPW42", item.Datas);

                    soLuongDichVao += $". Tổng dịch vào: {vitalSign.TongDichVao}ml";

                    datas.Add(soLuongDichVao);

                    // Số lượng dịch ra
                    string soLuongDichRa = "Số lượng dịch ra: ";
                    if (GetValueFromFormValueDatas("IPDMEWSPW58", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW58", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichRaSD = GetValueFromFormValueDatas("IPDMEWSPW59", item.Datas);
                            soLuongDichRa += $"SD({vitalSign.SoLuongDichRaSD}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW60", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW60", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichRaN = GetValueFromFormValueDatas("IPDMEWSPW61", item.Datas);
                            soLuongDichRa += $"N({vitalSign.SoLuongDichRaN}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW62", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW62", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichRaPh = GetValueFromFormValueDatas("IPDMEWSPW63", item.Datas);
                            soLuongDichRa += $"Ph({vitalSign.SoLuongDichRaPh}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW64", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW64", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichRaNT = GetValueFromFormValueDatas("IPDMEWSPW65", item.Datas);
                            soLuongDichRa += $"NT({vitalSign.SoLuongDichRaNT}ml), ";
                        }
                    }

                    if (GetValueFromFormValueDatas("IPDMEWSPW66", item.Datas) != null)
                    {
                        if (GetValueFromFormValueDatas("IPDMEWSPW66", item.Datas).ToUpper() == "TRUE")
                        {
                            vitalSign.SoLuongDichRaDL = GetValueFromFormValueDatas("IPDMEWSPW67", item.Datas);
                            soLuongDichRa += $"DL({vitalSign.SoLuongDichRaDL}ml),";
                        }
                    }

                    int index2 = soLuongDichRa.LastIndexOf(',');
                    if (index2 > 0)
                    {
                        soLuongDichRa = soLuongDichRa.Remove(index2);
                    }
                    vitalSign.TongDichRa = GetValueFromFormValueDatas("IPDMEWSPW57", item.Datas);

                    soLuongDichRa += $". Tổng dịch ra: {vitalSign.TongDichRa}ml";

                    datas.Add(soLuongDichRa);

                    vitalSign.BilanDich = GetValueFromFormValueDatas("IPDMEWSPW69", item.Datas);
                    datas.Add($"Bilan dịch: {vitalSign.BilanDich}ml");
                    tongMEW = GetValueFromFormValueDatas("IPDMEWSPW18", item.Datas);

                    MEWItemModel nhipThoMEW = new MEWItemModel();
                    MEWItemModel machMEW = new MEWItemModel();
                    MEWItemModel thanNhietMEW = new MEWItemModel();

                    //List nhịp thở
                    if (GetValueFromFormValueDatas("IPDMEWSPW2", item.Datas) != null && GetValueFromFormValueDatas("IPDMEWSPW2", item.Datas) != "")
                    {
                        var doubleNhipTho = double.Parse(GetValueFromFormValueDatas("IPDMEWSPW2", item.Datas));
                        if (doubleNhipTho >= 35 || doubleNhipTho <= 5)
                        {
                            nhipThoMEW.MEWValue = 3;
                        }
                        else if ((doubleNhipTho >= 25 && doubleNhipTho < 35) || (doubleNhipTho >= 6 && doubleNhipTho <= 9))
                        {
                            nhipThoMEW.MEWValue = 2;
                        }
                        else if ((doubleNhipTho >= 20 && doubleNhipTho < 25) || (doubleNhipTho >= 10 && doubleNhipTho < 15))
                        {
                            nhipThoMEW.MEWValue = 1;
                        }
                        else if (doubleNhipTho >= 15 && doubleNhipTho <= 19)
                        {
                            nhipThoMEW.MEWValue = 0;
                        }
                        nhipThoMEW.MEWRate = GetValueFromFormValueDatas("IPDMEWSPW2", item.Datas);
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
                    if (GetValueFromFormValueDatas("IPDMEWSPW9", item.Datas) != null && GetValueFromFormValueDatas("IPDMEWSPW9", item.Datas) != "")
                    {
                        var doubleMach = double.Parse(GetValueFromFormValueDatas("IPDMEWSPW9", item.Datas));

                        if (doubleMach >= 140 || doubleMach < 39)
                        {
                            machMEW.MEWValue = 3;
                        }
                        else if ((doubleMach >= 120 && doubleMach <= 139) || (doubleMach >= 40 && doubleMach <= 49))
                        {
                            machMEW.MEWValue = 2;
                        }
                        else if ((doubleMach >= 100 && doubleMach <= 119) || (doubleMach >= 50 && doubleMach <= 59))
                        {
                            machMEW.MEWValue = 1;
                        }
                        else if ((doubleMach >= 60 && doubleMach <= 99))
                        {
                            machMEW.MEWValue = 0;
                        }

                        machMEW.MEWRate = GetValueFromFormValueDatas("IPDMEWSPW9", item.Datas);
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
                    if (GetValueFromFormValueDatas("IPDMEWSPW11", item.Datas) != null && GetValueFromFormValueDatas("IPDMEWSPW11", item.Datas) != "")
                    {
                        var doubleThanNhiet = double.Parse(GetValueFromFormValueDatas("IPDMEWSPW11", item.Datas));
                        if ((doubleThanNhiet >= 41) || doubleThanNhiet < 35)
                        {
                            thanNhietMEW.MEWValue = 3;
                        }
                        else if ((doubleThanNhiet >= 38.5 && doubleThanNhiet <= 40.9) || (doubleThanNhiet >= 35 && doubleThanNhiet <= 35.4))
                        {
                            thanNhietMEW.MEWValue = 2;
                        }
                        else if ((doubleThanNhiet >= 35.5 && doubleThanNhiet <= 35.9) || (doubleThanNhiet >= 38 && doubleThanNhiet <= 38.4))
                        {
                            thanNhietMEW.MEWValue = 1;
                        }
                        else if ((doubleThanNhiet >= 36 && doubleThanNhiet <= 37.9))
                        {
                            thanNhietMEW.MEWValue = 0;
                        }

                        thanNhietMEW.MEWRate = GetValueFromFormValueDatas("IPDMEWSPW11", item.Datas);
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
                    datas.Add($"Nhịp thở: (lần/phút), SPO2: (%),Huyết áp: Tối thiểu: (mmHg) - Tối đa: (mmHg), Mạch: (Nhịp/phút), Thân nhiệt: (oC), Tri giác: , Hỗ trợ hô hấp: ");
                    datas.Add($"Điểm đau: , Đường máu toàn phần: ");
                    datas.Add("Đánh giá vein truyền: ");
                    datas.Add("Tần số cơn co tử cung: ");
                    datas.Add("Tần số tim thai: ");
                    datas.Add("Mật độ co hồi tử cung: ");
                    datas.Add("Sản dịch: ");
                    datas.Add("Số lượng dịch vào: .Tổng dịch vào: ");
                    datas.Add("Số lượng dịch ra: .Tổng dịch ra: ");
                    datas.Add("Bilan dịch: ml");

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

                listItems.Add(new VitalSignsForPregnantWomanItem
                {
                    Id = item.Id,
                    VisitId = item.VisitId,
                    CreatedAt = item.CreateAt,
                    UpdatedAt = item.UpdatedAt,
                    CreatedBy = item.CreateBy,
                    Content = datas,
                    MEWSTotal = tongMEW,
                    VitalSign = vitalSign,
                    TransactionDate = (DateTime)item.TransactionDate
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
            var test = from item in listItems
                       orderby item.TransactionDate descending
                       select item;
            return Content(HttpStatusCode.OK, new
            {
                // = listItems.OrderByDescending(e => e.TransactionDate)
                listItems = listItems.OrderByDescending(e => e.TransactionDate),
                Count = count,
                Is24hLocked = IPDIsBlock(ipd, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoSanPhu),
                NhipThoMEWS = nhipThoMEWS.OrderBy(e => e.TransactionDate).ToList(),
                ThanNhietMEWS = thanNhietMEWS.OrderBy(e => e.TransactionDate).ToList(),
                MachMEWS = machMEWS.OrderBy(e => e.TransactionDate).ToList()
            });
        }

        [HttpPost]
        [Route("api/VitalSignsForPregnantWoman/Create/{visitId}")]
        [Permission(Code = "VSFPW2")]
        public IHttpActionResult Create(Guid visitId)
        {
            dynamic visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            if (IPDIsBlock((IPD)visit, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoSanPhu))
                return Content(HttpStatusCode.Forbidden, Message.FORM_IS_LOCKED);


            var formData = new VitalSignForPregnantWoman
            {
                VisitId = visitId,
                TransactionDate = DateTime.Now
            };

            unitOfWork.IPDVitalSignForPregnantWomanRepository.Add(formData);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(visitId, formData.Id, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoSanPhu);
            return Content(HttpStatusCode.OK, new { formData.Id });
        }

        [HttpGet]
        [Route("api/VitalSignsForPregnantWoman/GetDetail/{visitId}/{formId}")]
        [Permission(Code = "VSFPW3")]
        public IHttpActionResult GetDetail(Guid visitId, Guid formId)
        {
            var visit = GetVisit(visitId, "IPD");
            if (visit == null)
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);

            var form = unitOfWork.IPDVitalSignForPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.VisitId == visitId && e.Id == formId);
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
                    IsLocked = IPDIsBlock(GetIPD(visitId), Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoSanPhu)
                });
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("api/VitalSignsForPregnantWoman/Update/{visitId}/{formId}")]
        [Permission(Code = "VSFPW4")]
        public IHttpActionResult Update(Guid visitId, Guid formId, [FromBody] JObject request)
        {
            var ipd = GetIPD(visitId);
            if (ipd == null)
            {
                return Content(HttpStatusCode.NotFound, Message.IPD_NOT_FOUND);
            }

            formId = (Guid)request["Id"];
            var form = unitOfWork.IPDVitalSignForPregnantWomanRepository.FirstOrDefault(e => !e.IsDeleted && e.Id == formId);
            if (form == null)
            {
                return Content(HttpStatusCode.NotFound, Message.FORM_NOT_FOUND);
            }

            var listForms = unitOfWork.IPDVitalSignForPregnantWomanRepository
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
                second  = listForms[0].TransactionDate.Value.Second;
            }
            else
            {
                second = listForms[1].TransactionDate.Value.Second;
            } 

            form.TransactionDate = DateTime.ParseExact(request["TransactionDate"].ToString(), "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture).Add(TimeSpan.FromSeconds(second + 1));
            HandleUpdateOrCreateFormDatas(visitId, formId, formCode, request["Datas"]);

            unitOfWork.IPDVitalSignForPregnantWomanRepository.Update(form);
            unitOfWork.Commit();
            CreateOrUpdateFormForSetupOfAdmin(visitId, form.Id, Constant.IPDFormCode.BangTheoDoiDauHieuSinhTonDanhChoSanPhu);
            return Content(HttpStatusCode.OK, new { form.Id });
        }
    }
}
